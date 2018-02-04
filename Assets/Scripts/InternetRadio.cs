using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Audio;
using System;
using System.Reflection;
using System.Runtime;
using System.Runtime.InteropServices;


public class InternetRadio : MonoBehaviour
{
	//public string url = "http://sverigesradio.se/topsy/direkt/132-mp3.m3u";//"http://sverigesradio.se/topsy/direkt/132-ogg.m3u";
	public string url = "http://http-live.sr.se/p1-mp3-128";
    //"http://ia902707.us.archive.org/11/items/NearlyCompleteHPLovecraftCollection/01_The_Whisperer_in_Darkness_01.mp3";
	public Transform playerCamera;
	private int stream, downmix;

    public enum ErrorCodes {
        	BASS_OK             = 0,
        	BASS_ERROR_MEM      = 1,
        	BASS_ERROR_FILEOPEN = 2,
        	BASS_ERROR_DRIVER   = 3,
        	BASS_ERROR_BUFLOST  = 4,
        	BASS_ERROR_HANDLE   = 5,
        	BASS_ERROR_FORMAT   = 6,
        	BASS_ERROR_POSITION = 7,
        	BASS_ERROR_INIT     = 8,
        	BASS_ERROR_START    = 9,
        	BASS_ERROR_SSL      = 10,
        	BASS_ERROR_ALREADY  = 14,
        	BASS_ERROR_NOCHAN   = 18,
        	BASS_ERROR_ILLTYPE  = 19,
        	BASS_ERROR_ILLPARAM = 20,
        	BASS_ERROR_NO3D     = 21,
        	BASS_ERROR_NOEAX    = 22,
        	BASS_ERROR_DEVICE   = 23,
        	BASS_ERROR_NOPLAY   = 24,
        	BASS_ERROR_FREQ     = 25,
        	BASS_ERROR_NOTFILE  = 27,
        	BASS_ERROR_NOHW     = 29,
        	BASS_ERROR_EMPTY    = 31,
        	BASS_ERROR_NONET    = 32,
        	BASS_ERROR_CREATE   = 33,
        	BASS_ERROR_NOFX     = 34,
        	BASS_ERROR_NOTAVAIL = 37,
        	BASS_ERROR_DECODE   = 38,
        	BASS_ERROR_DX       = 39,
        	BASS_ERROR_TIMEOUT  = 40,
        	BASS_ERROR_FILEFORM = 41,
        	BASS_ERROR_SPEAKER  = 42,
        	BASS_ERROR_VERSION  = 43,
        	BASS_ERROR_CODEC    = 44,
        	BASS_ERROR_ENDED    = 45,
        	BASS_ERROR_BUSY     = 46,
        	BASS_ERROR_UNKNOWN  = -1
    }


	public enum flags
	{
        BASS_DEFAULT = 0,
        BASS_SAMPLE_8BITS = 1,
        BASS_SAMPLE_FLOAT,
        BASS_SAMPLE_SOFTWARE,
        BASS_SAMPLE_3D = 8,
        BASS_STREAM_DECODE = 2097152,
	}

	public enum configs
	{
 
		BASS_CONFIG_NET_PLAYLIST = 21
 
	}
    /*
	public enum mixerInitFlags{

        BASS_DEFAULT = 0,
        BASS_SAMPLE_8BITS = 1,
		BASS_SAMPLE_FLOAT,
		BASS_SAMPLE_SOFTWARE,
		BASS_SAMPLE_3D = 8,
	}
    */
	public enum mixerFlags{
        MIXER_MATRIX = 65536,
        MIXER_DOWNMIX = 4194304
	}

	public enum initFlags
	{
		BASS_DEVICE_DEFAULT = 0,
        BASS_DEVICE_8BITS = 1,
		BASS_DEVICE_MONO = 2,
		BASS_DEVICE_3D = 4
	}

	[DllImport ("bass")]
	public static extern bool BASS_Init (int device, int freq, initFlags flag, IntPtr hwnd, IntPtr clsid);

	[DllImport ("bass")]
	public static extern bool BASS_SetConfig (configs config, int valuer);

	[DllImport ("bass")]
	//public static extern Int32 BASS_StreamCreateURL(string url,int offset,   flags flag,  IntPtr user);
    public static extern Int32 BASS_StreamCreateURL (string url, int offset, flags Flag, IntPtr dproc, IntPtr user) ;

	[DllImport ("bass")]
	public static extern bool BASS_ChannelPlay (int stream, bool restart);

	[DllImport ("bass")]
	public static extern bool BASS_StreamFree (int stream);

	[DllImport ("bass")]
	public static extern bool BASS_Free ();

	[DllImport ("bass")]
	public static extern bool BASS_Set3DPosition (Vector3 pos, Vector3 vel, Vector3 front, Vector3 top);

	[DllImport ("bass")]
	public static extern bool BASS_Set3DFactors (float distf, float rollf, float doppf);

	[DllImport ("bass")]
	public static extern void BASS_Apply3D ();

	[DllImport ("bass")]
    public static extern ErrorCodes BASS_ErrorGetCode();

	[DllImport ("bassmix")]
	public static extern int BASS_Mixer_StreamCreate(int freq, int channels, flags flags);

	[DllImport ("bassmix")]
	public static extern bool BASS_Mixer_StreamAddChannel(int mix, int stream, mixerFlags flags);



	void Start (){
		if (BASS_Init (-1, 44100, initFlags.BASS_DEVICE_3D, IntPtr.Zero, IntPtr.Zero)) {
			print ("Init: "+BASS_ErrorGetCode ());

			BASS_SetConfig (configs.BASS_CONFIG_NET_PLAYLIST, 2);

			stream = BASS_StreamCreateURL (url, 0, flags.BASS_STREAM_DECODE, IntPtr.Zero, IntPtr.Zero);
            print("CreateStream: " + BASS_ErrorGetCode());

			downmix = BASS_Mixer_StreamCreate (44100, 1, flags.BASS_SAMPLE_3D);
            print("CreateMixer: " + BASS_ErrorGetCode());

			print (BASS_Mixer_StreamAddChannel(downmix, stream, mixerFlags.MIXER_DOWNMIX));

            print("AddChannel: " + BASS_ErrorGetCode());

			print(BASS_Set3DFactors (0.1f, 0.1f, 0.1f));
			print(BASS_Set3DPosition (playerCamera.position - transform.position, Vector3.zero, playerCamera.rotation.eulerAngles, playerCamera.up));
            print("3D: " + BASS_ErrorGetCode());
			BASS_Apply3D ();
					
			if (stream != 0)
				BASS_ChannelPlay (downmix, false);          
		} else {
			print ("INIT ERROR "+BASS_ErrorGetCode ());
		}
	}

	void Update(){
		BASS_Set3DPosition ((playerCamera.position - transform.position) * 100, Vector3.zero, playerCamera.rotation.eulerAngles, playerCamera.up);
		BASS_Apply3D ();
	}
 
	void OnApplicationQuit ()
	{
		BASS_StreamFree (stream);
		BASS_Free ();
	}

	/*
	WWW www, oggwww;
	string oggurl;
	AudioClip clip;
	new AudioSource audio;

	// Use this for initialization
	void Start () {
		www = new WWW (url);
		audio = GetComponent<AudioSource> ();
	}
	
	// Update is called once per frame
	void Update () {
		if (www.isDone && oggurl == null) {
			string[] oggurls = www.text.Split("\n".ToCharArray());
			foreach (string u in oggurls) {
				if (u.StartsWith ("http")) {
					oggurl = u;
					print (oggurl);
					oggwww = new WWW (oggurl+ "&dummy=fake.mp3");
					break;
				}
			}
		}
		if (oggurl != null) {
			if (oggwww.isDone && clip == null) {

				clip = oggwww.GetAudioClip (true, true, AudioType.MPEG);

				//AudioSource.PlayClipAtPoint (clip, transform.position);
				//audio.clip = clip;
			}
		}
		if (!audio.isPlaying && clip != null && audio.clip.loadState == AudioDataLoadState.Loaded) {
			audio.Play ();
		}
	}
	*/
}


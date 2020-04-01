using UnityEngine;
#if PLATFORM_ANDROID
using UnityEngine.Android;
#endif
using System.Linq;

public class Noise : MonoBehaviour
{
    GameObject dialog = null;
    private float[] sound = null;
    private float[] sound2 = null;
    private float[] sound3 = null;

    public float RmsValue;
    public float DbValue;
    public float PitchValue;

    private const int QSamples = 4096;
    private const float RefValue = 0.1f;
    private const float Threshold = 0.02f;

    float[] _samples;
    private float[] _spectrum;
    private float _fSample;


    void Start()
    {
#if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            Permission.RequestUserPermission(Permission.Microphone);
            dialog = new GameObject();
        }
#endif

        _samples = new float[QSamples];
        _spectrum = new float[QSamples];
        _fSample = AudioSettings.outputSampleRate;
    }

    void AnalyzeSound()
    {
        GetComponent<AudioSource>().GetOutputData(_samples, 0); // fill array with samples
        int i;
        float sum = 0;
        for (i = 0; i < QSamples; i++)
        {
            sum += _samples[i] * _samples[i]; // sum squared samples
        }
        RmsValue = Mathf.Sqrt(sum / QSamples); // rms = square root of average
        DbValue = 20 * Mathf.Log10(RmsValue / RefValue); // calculate dB
        if (DbValue < -160) DbValue = -160; // clamp it to -160dB min
                                            // get sound spectrum
        GetComponent<AudioSource>().GetSpectrumData(_spectrum, 0, FFTWindow.BlackmanHarris);
        float maxV = 0;
        var maxN = 0;
        for (i = 0; i < QSamples; i++)
        { // find max 
            if (!(_spectrum[i] > maxV) || !(_spectrum[i] > Threshold))
                continue;

            maxV = _spectrum[i];
            maxN = i; // maxN is the index of max
        }
        float freqN = maxN; // pass the index to a float variable
        if (maxN > 0 && maxN < QSamples - 1)
        { // interpolate index using neighbours
            var dL = _spectrum[maxN - 1] / _spectrum[maxN];
            var dR = _spectrum[maxN + 1] / _spectrum[maxN];
            freqN += 0.5f * (dR * dR - dL * dL);
        }
        PitchValue = freqN * (_fSample / 2) / QSamples; // convert index to frequency
        Debug.Log("Pitch");
        Debug.Log(PitchValue);
        Debug.Log(_samples.Max());
        Debug.Log(_samples);
    }

    void OnGUI()
    {
#if PLATFORM_ANDROID
        if (!Permission.HasUserAuthorizedPermission(Permission.Microphone))
        {
            // The user denied permission to use the microphone.
            // Display a message explaining why you need it with Yes/No buttons.
            // If the user says yes then present the request again
            // Display a dialog here.
            dialog.AddComponent<PermissionsRationaleDialog>();
            return;
        }
        else if (dialog != null)
        {
            Destroy(dialog);
        }
#endif

        // Now you can do things with the microphone
    }

    // Start recording with built-in Microphone and play the recorded audio right away
    [System.Obsolete]
    void Update()
    {
        Debug.Log("ici");
        AnalyzeSound();
        AudioSource audioSource = GetComponent<AudioSource>();
        if (!audioSource.isPlaying)
        {
            sound = new float[1024];
            sound2 = new float[1024];
            sound3 = new float[1024];
            Debug.Log("ici2");
            // Pour trouver le nom du bon peripherique
            foreach (string device in Microphone.devices) { Debug.Log("Name: " + device); }
            audioSource.clip = Microphone.Start(null, true, 1, 44100);
            audioSource.Play();
            // 
            //audioSource.GetOutputData(sound, 0);
            /*audioSource.clip = Microphone.Start("Android audio input", true, 1, 44100);
            audioSource.Play();
            audioSource.GetOutputData(sound2, 0);
            audioSource.clip = Microphone.Start("Android camcorder input", true, 1, 44100);
            audioSource.Play();
            audioSource.GetOutputData(sound3, 0);*/
        }
        /*else
        {
            Debug.Log(sound);
            Debug.Log(sound2);
            Debug.Log(sound3);
        }*/
        Debug.Log("la");
    }
}
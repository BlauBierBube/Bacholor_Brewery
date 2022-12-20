using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

public class StartGame : MonoBehaviour
{
    public Volume PostProcessEffect;

    public AudioSource Explosion;
    public AudioSource CurrentOn;
    public AudioSource CurrentOff;
    public AudioSource Sirene;
    public AudioSource TextSirene;
    public AudioSource TextWiederhergestellt;

    public bool Online = false;
    private bool isplaying = false;

    public float ExplosionZeit = 1f;
    public float currentOff = 3f;
    public float SireneZeit = 1f;
    public float NotfallZeit = 3f;
    public float NotfallLoop = 10f;
    public float currentOn = 1f;
    public float redLight = 1f;
    public float whiteLight = 2f;

    ColorAdjustments colorAdjustments;
    Bloom bloom;
    ChromaticAberration chromatic;
    // Start is called before the first frame update
    //public void OnLevelWasLoaded()
    void Start()
    {
        
        
        PostProcessEffect.profile.TryGet<ColorAdjustments>(out colorAdjustments);
        //PostProcessEffect.profile.TryGet<Bloom>(out bloom);
        //PostProcessEffect.profile.TryGet<ChromaticAberration>(out chromatic);
    }

    public void Update()
    {

        if (Online == true)
        {
            Sirene.Stop();
            TextSirene.Stop();
            if(!isplaying)
                Invoke("currentOnline", 1f);

        }
    }
    public void start()
    {
        Invoke("startgame", ExplosionZeit);
    }

    void startgame()
    {
        OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.All);
        //bloom.intensity.value = Mathf.PingPong(Time.time* 6, 8f);
        //chromatic.intensity.value = Mathf.PingPong(Time.time*6, 1f);
        Explosion.Play();
        Invoke("current", currentOff);
    }

    void current()
    {
        CurrentOff.Play();
        Invoke("sirene", SireneZeit);

        colorAdjustments.colorFilter.value = Color.black;
        
    }
    void sirene()
    {
        Sirene.Play();
        Invoke("texts", NotfallZeit);

        Invoke("RedLight",0);

    }
    void texts()
    {
        TextSirene.Play();
        if (!Online)
            Invoke("texts", NotfallLoop);
    }
    void RedLight()
    {
        if (!Online)
        {
            colorAdjustments.colorFilter.value = Color.red;
            Invoke("WhiteLight", redLight);
        }

    }
    void WhiteLight()
    {
        if (!Online)
        {
            colorAdjustments.colorFilter.value = Color.white;
            Invoke("RedLight", whiteLight);
        }

    }

    void currentOnline()
    {
        isplaying = true;
        CurrentOn.Play();
        colorAdjustments.colorFilter.value = Color.white;
        Invoke("wiederhergestelt", currentOn);
    }
    void wiederhergestelt()
    {
        TextWiederhergestellt.Play();
    }


    public void Vibrate()
    {
        OVRInput.SetControllerVibration(0.5f, 0.8f, OVRInput.Controller.All);
    }

    public void ElectricOnline()
    {
        Online = true;
    }
}

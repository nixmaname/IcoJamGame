using Cinemachine;
using System.Collections;
using TMPro;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeTime;

    public ParticleSystem explosion;

    public TextMeshProUGUI timerText;

    private CinemachineVirtualCamera cam;

    private const float timeBeforeExplosion = 3;

    private void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
    }

    private void Update()
    {
        if (shakeTime > 0)
        {
            shakeTime -= Time.deltaTime;
            if (shakeTime < 0)
            {
                CinemachineBasicMultiChannelPerlin noice = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
                noice.m_AmplitudeGain = 0;
                this.transform.rotation = Quaternion.identity;
                shakeTime = 0;
            }
        }
        if (Input.GetKeyDown(KeyCode.F)) {
            StartCoroutine(Shake());
        }
    }

    IEnumerator Shake()
    {
        float timeLeft = 3f;
        while (timeLeft >= 0)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = timeLeft.ToString("0") + "...";
            Debug.Log((int)timeLeft);
            yield return null;
        }
        timerText.text = "";
        Skaking(5f, 0.1f);         
        //CinemachineBasicMultiChannelPerlin noice = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        //explosion.Play();
        //noice.m_AmplitudeGain = intensity;

        //shakeTime = time;
    }

    void Skaking(float intensity, float time)
    {
        CinemachineBasicMultiChannelPerlin noice = cam.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
        explosion.Play();
        noice.m_AmplitudeGain = intensity;

        shakeTime = time;
        //StartCoroutine(Shake(5f, 0.1f));
    }
}

using Cinemachine;
using System.Collections;
using TMPro;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public float shakeTime;

    public GameObject explosion;

    public TextMeshProUGUI timerText;

    private CinemachineVirtualCamera cam;

    private const float timeBeforeExplosion = 3;

    Recorder rec;

    private void Start()
    {
        cam = GetComponent<CinemachineVirtualCamera>();
        rec = GameObject.FindGameObjectWithTag("Player").transform.GetComponent<Recorder>();
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
    }

    public IEnumerator Shake()
    {
        float timeLeft = 3f;
        while (timeLeft >= 0)
        {
            timeLeft -= Time.deltaTime;
            timerText.text = timeLeft.ToString("F1");
            if (timeLeft > 2)
            {
                timerText.color = new Color(0,1,0,0.4f);
            }
            else if (timeLeft > 1)
            {
                timerText.color = new Color(1,0.92f,0.016f,0.4f);
            }
            else if (timeLeft > 0.5f)
            {
                timerText.color = new Color(1f,0.7f,0f,0.4f);
            }
            else
            {
                timerText.color = new Color(1,0,0,0.4f);
            }
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
        Instantiate(explosion,rec.transform.position,Quaternion.identity);
        noice.m_AmplitudeGain = intensity;

        shakeTime = time;
        rec.Unalive();
        //StartCoroutine(Shake(5f, 0.1f));
    }
}

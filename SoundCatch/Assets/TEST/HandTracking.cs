using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HandTracking : MonoBehaviour
{
    // Start is called before the first frame update
    public UDPReceive udpReceive;
    public GameObject[] handPoints;

    public bool isCameraOn = false;

    private RaycastHit hit;
    private RaycastHit preHit;
    private bool preHitBool = false;
    private AudioSource audioSource;
    private AudioSource subAscr;
    private Sound sound;

    void Start()
    {
        preHitBool = Physics.Raycast(Vector3.zero, Vector3.forward * -1, out preHit, 0f);
        audioSource = GetComponent<AudioSource>();
    }

    // Update is called once per frame
    void Update()
    {
        string data = udpReceive.data;

        if (data != "")
        {
            if (!data.Equals("true"))
            {
                data = data.Remove(0, 1);
                data = data.Remove(data.Length - 1, 1);

                string[] points = data.Split(',');

                //0        1*3      2*3
                //x1,y1,z1,x2,y2,z2,x3,y3,z3

                for (int i = 0; i < 21; i++)
                {

                    float x = 7 - float.Parse(points[i * 3]) / 100;
                    float y = float.Parse(points[i * 3 + 1]) / 100;
                    float z = float.Parse(points[i * 3 + 2]) / 100;

                    handPoints[i].transform.localPosition = new Vector3(x, y, z);

                }

                Debug.DrawRay(handPoints[9].transform.localPosition, Vector3.forward * -1, Color.red, 300.0f);
                if (Physics.Raycast(handPoints[9].transform.localPosition, Vector3.forward * -1, out hit, 300.0f, 1 << 3))
                {
                    if (!preHitBool || hit.transform.gameObject != preHit.transform.gameObject)
                    {
                        if (hit.transform.name.Equals("Background"))
                        {
                            audioSource.Stop();
                        }
                        else
                        {
                            preHitBool = true;

                            sound = hit.transform.GetComponent<Sound>();

                            if (sound.isSub)
                            {
                                audioSource.panStereo = 1;

                                subAscr = hit.transform.GetComponentInChildren<AudioSource>();
                                subAscr.clip = sound.subSound;
                                subAscr.panStereo = -1;
                                subAscr.volume = 0.1f;

                                subAscr.Play();

                            }
                            else
                            {
                                audioSource.panStereo = 0;

                                if (subAscr != null)
                                {
                                    subAscr.Stop();
                                }
                            }

                            audioSource.clip = sound.cubeSound;
                            audioSource.volume = 0.1f;
                            audioSource.Play();
                        }

                        preHit = hit;
                    }
                }
            } else
            {
                isCameraOn = true;
                Debug.Log("true");
            } 
        }
    }
}

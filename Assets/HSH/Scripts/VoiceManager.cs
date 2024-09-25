using Photon.Voice.PUN;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Voice.Unity;

public class VoiceManager : MonoBehaviourPun
{
    //GameObject speaker;
    //PunVoiceClient pvc;
    //GameObject pmg;

    Recorder recorder;
    // Start is called before the first frame update
    void Start()
    {
        //pmg = GameObject.Find("PlayerManager");
        //PlayerManager pm = pmg.GetComponent<PlayerManager>();
        //pvc = GetComponent<PunVoiceClient>();
        //speaker = Resources.Load<GameObject>("Player" + pm.AvatarNum());
        //pvc.SpeakerPrefab = speaker;

        recorder = GetComponent<Recorder>();

        recorder.TransmitEnabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            recorder.TransmitEnabled = !recorder.TransmitEnabled;
        }
    }
}

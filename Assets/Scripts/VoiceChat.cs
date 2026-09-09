using UnityEngine;
using Photon.Pun;

public class VoiceChat : MonoBehaviourPun
{
    private PhotonVoiceNetwork voiceNetwork;
    private bool isMuted = false;
    
    private void Start()
    {
        voiceNetwork = FindObjectOfType<PhotonVoiceNetwork>();
        
        if (voiceNetwork == null)
        {
            Debug.LogWarning("PhotonVoiceNetwork not found in scene!");
        }
    }
    
    private void Update()
    {
        // Toggle voice mute with button press
        if (OVRInput.GetDown(OVRInput.Button.One))
        {
            ToggleMute();
        }
    }
    
    public void ToggleMute()
    {
        if (voiceNetwork != null && voiceNetwork.Client != null)
        {
            isMuted = !isMuted;
            voiceNetwork.Client.LocalPlayer.SetMuted(isMuted);
            
            photonView.RPC("RPC_OnPlayerMuted", RpcTarget.AllBuffered, isMuted);
            Debug.Log("Microphone " + (isMuted ? "muted" : "unmuted"));
        }
    }
    
    [PunRPC]
    void RPC_OnPlayerMuted(bool muted)
    {
        // Visual indicator that player is muted can be set here
        Debug.Log("Player muted status changed: " + muted);
    }
    
    public bool IsMuted()
    {
        return isMuted;
    }
    
    public void EnableVoiceChat()
    {
        if (voiceNetwork != null)
        {
            voiceNetwork.enabled = true;
        }
    }
    
    public void DisableVoiceChat()
    {
        if (voiceNetwork != null)
        {
            voiceNetwork.enabled = false;
        }
    }
}

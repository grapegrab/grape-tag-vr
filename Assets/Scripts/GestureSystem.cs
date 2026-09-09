using UnityEngine;
using Photon.Pun;

public class GestureSystem : MonoBehaviourPun
{
    public enum Gesture
    {
        Wave,
        ThumbsUp,
        Point,
        Victory,
        Dance,
        Clap
    }
    
    [SerializeField] private OVRHand leftHand;
    [SerializeField] private OVRHand rightHand;
    [SerializeField] private Animator avatarAnimator;
    
    private float gestureDetectionThreshold = 0.7f;
    
    private void Update()
    {
        if (!photonView.IsMine)
            return;
            
        DetectAndPlayGestures();
    }
    
    private void DetectAndPlayGestures()
    {
        // Detect wave gesture (hand raised, open palm)
        if (IsWaveGesture())
        {
            PlayGesture(Gesture.Wave);
        }
        
        // Detect thumbs up gesture
        if (IsThumbsUpGesture())
        {
            PlayGesture(Gesture.ThumbsUp);
        }
        
        // Detect pointing gesture
        if (IsPointingGesture())
        {
            PlayGesture(Gesture.Point);
        }
        
        // Detect victory gesture (both hands up, peace sign)
        if (IsVictoryGesture())
        {
            PlayGesture(Gesture.Victory);
        }
    }
    
    private bool IsWaveGesture()
    {
        // Check if hand is raised and palm is open
        if (rightHand.IsTracked)
        {
            float fingerCurl = rightHand.GetFingerCurl(OVRHand.HandFinger.Thumb);
            float palmOpen = 1f - rightHand.GetFingerCurl(OVRHand.HandFinger.Middle);
            
            return palmOpen > gestureDetectionThreshold && rightHand.transform.position.y > 1.5f;
        }
        return false;
    }
    
    private bool IsThumbsUpGesture()
    {
        if (rightHand.IsTracked)
        {
            float thumbExtension = 1f - rightHand.GetFingerCurl(OVRHand.HandFinger.Thumb);
            float otherFingersCurled = rightHand.GetFingerCurl(OVRHand.HandFinger.Index);
            
            return thumbExtension > gestureDetectionThreshold && otherFingersCurled > gestureDetectionThreshold;
        }
        return false;
    }
    
    private bool IsPointingGesture()
    {
        if (rightHand.IsTracked)
        {
            float indexExtension = 1f - rightHand.GetFingerCurl(OVRHand.HandFinger.Index);
            float otherFingersCurled = rightHand.GetFingerCurl(OVRHand.HandFinger.Middle);
            
            return indexExtension > gestureDetectionThreshold && otherFingersCurled > gestureDetectionThreshold;
        }
        return false;
    }
    
    private bool IsVictoryGesture()
    {
        if (leftHand.IsTracked && rightHand.IsTracked)
        {
            float leftHandHigh = leftHand.transform.position.y > 1.5f ? 1f : 0f;
            float rightHandHigh = rightHand.transform.position.y > 1.5f ? 1f : 0f;
            
            return (leftHandHigh + rightHandHigh) > 1f;
        }
        return false;
    }
    
    public void PlayGesture(Gesture gesture)
    {
        // Send gesture to all players
        photonView.RPC("RPC_PlayGesture", RpcTarget.AllBuffered, gesture);
    }
    
    [PunRPC]
    void RPC_PlayGesture(Gesture gesture)
    {
        if (avatarAnimator != null)
        {
            avatarAnimator.SetTrigger(gesture.ToString());
        }
        
        Debug.Log("Playing gesture: " + gesture.ToString());
    }
    
    public void PlayEmote(string emoteName)
    {
        photonView.RPC("RPC_PlayEmote", RpcTarget.AllBuffered, emoteName);
    }
    
    [PunRPC]
    void RPC_PlayEmote(string emoteName)
    {
        if (avatarAnimator != null)
        {
            avatarAnimator.SetTrigger(emoteName);
        }
        
        Debug.Log("Playing emote: " + emoteName);
    }
}

using UnityEngine;
using Photon.Pun;
using Photon.Realtime;

public class PlayerAvatar : MonoBehaviourPunCallbacks
{
    [SerializeField] private GameObject headVisualsTarget;
    [SerializeField] private GameObject leftHandTarget;
    [SerializeField] private GameObject rightHandTarget;
    
    [SerializeField] private float avatarUpdateRate = 0.1f;
    
    private OVRCameraRig cameraRig;
    private Vector3 headPosition;
    private Quaternion headRotation;
    private Vector3 leftHandPosition;
    private Vector3 rightHandPosition;
    
    private float lastUpdateTime;
    
    private void Start()
    {
        if (photonView.IsMine)
        {
            // Disable remote visuals for local player
            gameObject.SetActive(false);
            
            // Get local camera rig
            cameraRig = GetComponent<OVRCameraRig>();
        }
    }
    
    private void Update()
    {
        if (photonView.IsMine)
        {
            // Update local player head position from camera
            if (cameraRig != null)
            {
                headPosition = cameraRig.centerEyeAnchor.position;
                headRotation = cameraRig.centerEyeAnchor.rotation;
            }
            
            // Send updates at regular intervals to reduce network traffic
            if (Time.time - lastUpdateTime >= avatarUpdateRate)
            {
                photonView.RPC("UpdateRemoteAvatar", RpcTarget.Others, 
                    headPosition, headRotation, leftHandPosition, rightHandPosition);
                lastUpdateTime = Time.time;
            }
        }
    }
    
    [PunRPC]
    void UpdateRemoteAvatar(Vector3 newHeadPos, Quaternion newHeadRot, 
                           Vector3 leftHandPos, Vector3 rightHandPos)
    {
        // Update remote player avatar visuals
        if (headVisualsTarget != null)
        {
            headVisualsTarget.transform.position = newHeadPos;
            headVisualsTarget.transform.rotation = newHeadRot;
        }
        
        if (leftHandTarget != null)
            leftHandTarget.transform.position = leftHandPos;
            
        if (rightHandTarget != null)
            rightHandTarget.transform.position = rightHandPos;
    }
    
    public void SetAvatarColor(Color color)
    {
        photonView.RPC("RPC_SetAvatarColor", RpcTarget.AllBuffered, color);
    }
    
    [PunRPC]
    void RPC_SetAvatarColor(Color color)
    {
        Renderer[] renderers = GetComponentsInChildren<Renderer>();
        foreach (Renderer renderer in renderers)
        {
            renderer.material.color = color;
        }
    }
}

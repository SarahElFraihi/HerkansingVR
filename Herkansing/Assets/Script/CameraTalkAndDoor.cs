using UnityEngine;

public class CameraTalkAndDoor : MonoBehaviour
{
    public AudioSource cameraAudio; // assign in Inspector
    public GameObject door;         // assign the door object
    public Animator doorAnimator;   // if using animation

    private bool hasSpoken = false;

    void Start()
    {
        StartCoroutine(PlayCameraAndOpenDoor());
    }

    private System.Collections.IEnumerator PlayCameraAndOpenDoor()
    {
        cameraAudio.Play();
        yield return new WaitForSeconds(cameraAudio.clip.length);

        // If you use animation
        if (doorAnimator != null)
        {
            doorAnimator.SetTrigger("Open");
        }
        else
        {
            // Fallback: just deactivate the door
            door.SetActive(false);
        }

        hasSpoken = true;
    }
}

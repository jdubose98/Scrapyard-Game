using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class MenuManager : MonoBehaviour {

    [SerializeField]
    GameObject MenuFrame;

    [SerializeField]
    GameObject InstructorFrame;

	public void ToggleFrames(int FrameLoadStyle) {
        switch (FrameLoadStyle)
        {
            case 1:
                MenuFrame.SetActive(false);
                InstructorFrame.SetActive(true);
                break;
            case 2:
                MenuFrame.SetActive(true);
                InstructorFrame.SetActive(false);
                break;
            default:
                break;
        }
    }

    public void Quit() {
        Application.Quit();
    }
}

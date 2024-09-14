using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewFile : MonoBehaviour
{
    [SerializeField]
    GameObject note;
    [SerializeField]
    GameObject fileDir;

    public void File_On()
    {
        note.SetActive(false);
        fileDir.SetActive(true);
    }
}

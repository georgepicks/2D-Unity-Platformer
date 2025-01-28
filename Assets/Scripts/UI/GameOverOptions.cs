using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OptionSelection : MonoBehaviour
{

    [SerializeField] private List<Transform> options;
    [SerializeField] private GameObject Arrow;
    private int arrowIndex = 0;

    private bool option;

    // Start is called before the first frame update
    void Start()
    { 
        option = true;
        ArrowPos();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && !option)
        {
            arrowIndex = 0;
            option = true;
            ArrowPos();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && option)
        {
            arrowIndex = 1;
            option = false;
            ArrowPos();
        }


        if (option && Input.GetKeyDown(KeyCode.Return))
        {
            string mainGameScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(mainGameScene);
        } 
        else if (!option && Input.GetKeyDown(KeyCode.Return))
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }

    private void ArrowPos()
    {
        if (option)
        {
            Arrow.transform.position = new Vector3((options[arrowIndex].position.x) - 250, options[arrowIndex].position.y, options[arrowIndex].position.z);
        }
        else
        {
            Arrow.transform.position = new Vector3((options[arrowIndex].position.x) - 150, options[arrowIndex].position.y, options[arrowIndex].position.z);
        }
    }
}

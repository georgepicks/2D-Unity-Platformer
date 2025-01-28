using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseMenuOption : MonoBehaviour
{

    [SerializeField] private List<Transform> options;
    [SerializeField] private GameObject Arrow;
    private int arrowIndex = 0;

    public UIManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<UIManager>();
        ArrowPos();
    }

    // Update is called once per frame
    void Update()
    {
        if(arrowIndex < 0)
        {
            arrowIndex = 0;
        } else if(arrowIndex > 2) 
        {
            arrowIndex = 2;     
        }

        if (arrowIndex >=0 && arrowIndex <=2)
        {
            if (Input.GetKeyDown(KeyCode.UpArrow))
            {
                arrowIndex--;
                ArrowPos();
            }
            else if (Input.GetKeyDown(KeyCode.DownArrow))
            {
                arrowIndex++;
                ArrowPos();
            }
        }

        if (Input.GetKeyDown(KeyCode.Return))
        {
            Option();
        }
    }

    private void ArrowPos()
    {
        if (arrowIndex == 0)
        {
            Arrow.transform.position = new Vector3((options[arrowIndex].position.x) - 250, options[arrowIndex].position.y, options[arrowIndex].position.z);
        }
        else if (arrowIndex == 1)
        {
            Arrow.transform.position = new Vector3((options[arrowIndex].position.x) - 240, options[arrowIndex].position.y, options[arrowIndex].position.z);
        }
        else if (arrowIndex ==2)
        {
            Arrow.transform.position = new Vector3((options[arrowIndex].position.x) - 175, options[arrowIndex].position.y, options[arrowIndex].position.z);
        }
    }

    private void Option()
    {
        if (arrowIndex == 0)
        {
            manager.UnpauseMenu();
        }
        else if (arrowIndex == 1)
        {
            string mainGameScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(mainGameScene);
        }
        else
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }
}


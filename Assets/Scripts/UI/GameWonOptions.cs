using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameWonOptions : MonoBehaviour
{

    [SerializeField] private List<Transform> options;
    [SerializeField] private GameObject Arrow;
    private int arrowIndex = 0;

    private UIManager manager;

    // Start is called before the first frame update
    void Start()
    {
        manager = FindObjectOfType<UIManager>();
        ArrowPos();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow) && arrowIndex == 1)
        {
            arrowIndex = 0;
            ArrowPos();
        }
        else if (Input.GetKeyDown(KeyCode.DownArrow) && arrowIndex == 0)
        {
            arrowIndex = 1;
            ArrowPos();
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
            Arrow.transform.position = new Vector3((options[arrowIndex].position.x) - 260, options[arrowIndex].position.y, options[arrowIndex].position.z);
        }
        else
        {
            Arrow.transform.position = new Vector3((options[arrowIndex].position.x) - 150, options[arrowIndex].position.y, options[arrowIndex].position.z);
        }
    }

    private void Option()
    {
        if (arrowIndex == 0)
        {
            string mainGameScene = SceneManager.GetActiveScene().name;
            SceneManager.LoadScene(mainGameScene);
        }
        else if (arrowIndex == 1)
        {
            UnityEditor.EditorApplication.isPlaying = false;
            Application.Quit();
        }
    }
}

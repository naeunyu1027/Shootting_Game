using System.Collections;
using UnityEngine.SceneManagement;
using UnityEngine;

public class NextScene : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame  
    void Update()
    {   
        
    }

    public void Exit()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        // PC, 모바일 등 빌드된 게임일 경우 종료
        Application.Quit();
#endif
    }
    
    public void Scene()
    {
        SceneManager.LoadScene(0);
    }
}

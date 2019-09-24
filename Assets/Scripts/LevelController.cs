using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelController : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        
    }

    public void LevelReset()
    {
        SceneManager.LoadScene(0);
    }
}

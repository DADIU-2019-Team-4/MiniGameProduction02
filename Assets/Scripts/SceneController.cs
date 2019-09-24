using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneController : MonoBehaviour
{
    public void SceneReset()
    {
        SceneManager.LoadScene(0);
    }
}

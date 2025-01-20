using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    public GameObject BoxSpawner;
    public Slider level;
    private void OnEnable()
    {
        StartCoroutine(WaitToLoad());
    }
    IEnumerator WaitToLoad()
    {
        yield return new WaitForSeconds(1f);
        level.maxValue = BoxSpawner.transform.childCount;

    }
    private void Update()
    {
    }
    private void LateUpdate()
    {
        level.value = BoxSpawner.transform.childCount;
    }
    public void LoadScenes(int idx)
    {
        SceneManager.LoadScene(idx);
    }
}

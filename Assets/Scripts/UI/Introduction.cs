using System.Collections;
using UnityEngine;

public class Introduction : MonoBehaviour
{
    [SerializeField] private float m_IntroductionTime;
    [SerializeField] private SceneLoader m_SceneLoader;
    [SerializeField] private string m_SceneName;

    private void Start()
    {
        StartCoroutine(IntroductionTimer());
    }
    IEnumerator IntroductionTimer()
    {
        yield return new WaitForSeconds(m_IntroductionTime);
        m_SceneLoader.LoadScene(m_SceneName);
    }
}

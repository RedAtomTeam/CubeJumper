using UnityEngine;

public class EndScreen : MonoBehaviour
{
    [Header("Player")]
    [SerializeField] private PlayerLifeChecker playerLifeChecker;

    [Header("Panel")]
    [SerializeField] private GameObject obj;

    private void Start()
    {
        playerLifeChecker.dieEvent += OpenLooseWindow;
    }

    private void OpenLooseWindow()
    {
        obj.SetActive(true);
    }
}

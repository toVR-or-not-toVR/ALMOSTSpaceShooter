using UnityEngine;
using UnityEngine.SceneManagement;
using DG.Tweening;
using UnityEngine.UI;
using System;
using System.Collections;

public class StartMenu : MonoBehaviour
{
    [Space]
    [SerializeField] private Transform leftSide;
    [SerializeField] private Transform rightSide;
    [SerializeField] private Transform center;
    [SerializeField] private GameObject[] pages;

    [Space]
    [SerializeField] public float delayPage;
    [SerializeField] private static int _pageNow = 1;
    public static int pageNow { get { return _pageNow; } private set { } } //tak delat nelzya))



    public void TurnPage(int to) //to = 2
    {
        Debug.Log("TurnPage " + (pageNow - 1));
        pages[pageNow - 1].transform.DOMoveX(leftSide.position.x, delayPage, false);
        Debug.Log(to - 1 + " Array: " + pages);
        pages[to - 1].transform.DOMoveX(center.position.x, delayPage, false);
        pageNow = to - 1;
        Debug.Log("------------");
    }

    public void CancelPage(int pagePrevious) //pagePrevious = 1
    {
        Debug.Log("cancelPage " + pageNow);
        pages[pageNow].transform.DOMoveX(rightSide.position.x, delayPage, false);
        Debug.Log(pagePrevious - 1 + " Array: " + pages);
        pages[pagePrevious - 1].transform.DOMoveX(center.position.x, delayPage, false);
        pageNow = pagePrevious - 1;
        Debug.Log("------------");
    }

    private void Start()
    {
        Debug.Log(pageNow);
    }
}

using HUX;
using HUX.Interaction;
using HUX.Receivers;
using MRDL;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TutorialManager_4Tut : InteractionReceiver {


    public List<GameObject> Pages;
    public HandCoach HandCoach;
    public TextMesh percentageText;
    private float percentage;

    [SerializeField]
    private string currentPageName;

    [SerializeField]
    private float minProgressAmount;

    [SerializeField]
    private float progress;

    [SerializeField]
    private int page;

    private bool reachedEnd = false;

    public float Progress { get { return progress; } set { progress = value; } }


    private void Start()
    {
        Activate();
    }

    public void Activate()
    {
        gameObject.SetActive(true);
        page = 0;
        SetPage(page);
        StartCoroutine(DoTutorialOverTime());
    }

    public void NextPage()
    {
        page++;

        if (page >= Pages.Count)
            page = Pages.Count - 1;

        progress = 0f;

        SetPage(page);
    }

    public void PrevPage()
    {
        page--;
        if (page < 0)
            page = 0;
        progress = 0f;
        SetPage(page);
    }

    public void StartOver()
    {
        // get the current scene name 
        string sceneName = SceneManager.GetActiveScene().name;

        // load the same scene
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    public string CurrentPageName
    {
        get
        {
            if (page >= 0 && page < Pages.Count)
            {
                return Pages[page].name;
            }
            return string.Empty;
        }
    }

    protected override void OnTapped(GameObject obj, InteractionManager.InteractionEventArgs eventArgs)
    {
        base.OnTapped(obj, eventArgs);

        if (obj == null)
            return;

        switch (obj.name)
        {
            case "NextPage":
                NextPage();
                break;

            case "PrevPage":
                PrevPage();
                break;

            case "StartOver":
                StartOver();
                break;
        }
    }

    private void SetPage(int newPage)
    {
        page = newPage;
        for (int i = 0; i < Pages.Count; i++)
        {
            Pages[i].SetActive(i == page);
        }
    }


    IEnumerator DoTutorialOverTime()
    {
        while (!reachedEnd)
        {
            switch (CurrentPageName)
            {
                case "TutorialSegment_01":
                    HandCoach.Visibility = HandCoach.HandVisibilityEnum.Both;
                    HandCoach.CheckTracking = HandCoach.HandVisibilityEnum.Both;
                    HandCoach.Ghosting = HandCoach.HandVisibilityEnum.None;
                    HandCoach.Highlight = HandCoach.HandVisibilityEnum.Both;
                    HandCoach.RightGesture = HandCoach.HandGestureEnum.Ready;
                    HandCoach.LeftGesture = HandCoach.HandGestureEnum.Ready;
#if UNITY_WSA
                    if (HandCoach.Tracking == HandCoach.HandVisibilityEnum.Both)
                    {
                        progress += Time.deltaTime * 4;
                    }
                    if (progress >= minProgressAmount)
                    {
                        NextPage();
                    }
#endif
#if UNITY_EDITOR
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        NextPage();
                    }
#endif

                    break;
                case "TutorialSegment_02":
                    HandCoach.Visibility = HandCoach.HandVisibilityEnum.Both;
                    HandCoach.RightGesture = HandCoach.HandGestureEnum.TapHold;
                    HandCoach.LeftGesture = HandCoach.HandGestureEnum.TapHold;
                    HandCoach.CheckTracking = HandCoach.HandVisibilityEnum.Both;
 #if UNITY_WSA
                    if (Progress > minProgressAmount)
                    {
                        NextPage();
                    }
                    break;
#endif
#if UNITY_EDITOR
                    if (Input.GetKeyDown(KeyCode.Space))
                    {
                        NextPage();
                    }
#endif
                    break;
                case "TutorialSegment_03":
                    HandCoach.Visibility = HandCoach.HandVisibilityEnum.Both;
                    HandCoach.RightGesture = HandCoach.HandGestureEnum.TapHold;
                    HandCoach.LeftGesture = HandCoach.HandGestureEnum.TapHold;
                    HandCoach.RightDirection = HandCoach.HandDirectionEnum.Right;
                    HandCoach.RightMovement = HandCoach.HandMovementEnum.PingPong;
                    HandCoach.LeftDirection = HandCoach.HandDirectionEnum.Left;
                    HandCoach.LeftMovement = HandCoach.HandMovementEnum.PingPong;
                    HandCoach.CheckTracking = HandCoach.HandVisibilityEnum.Both;
                    break;

                //add as many segments as you need

                default:
                    Debug.LogError("No segment found in Pages List that matches that name");
                    break;
            }
            yield return null;
        }
    }

    void PushPercentage()
    {
        //if Text is less than 100 percent, Text Counts upward
        if (percentage <= 100)
        {
            percentage += Time.deltaTime*30.0f;
            percentageText.text = percentage.ToString("F0") + "%";
        }
        else
        {
            percentageText.gameObject.SetActive(false);
            NextPage();
        }
    }
}

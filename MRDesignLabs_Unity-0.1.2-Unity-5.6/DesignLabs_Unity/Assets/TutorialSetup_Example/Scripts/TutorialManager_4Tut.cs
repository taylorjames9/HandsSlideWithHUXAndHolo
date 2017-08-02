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
    public GameObject Arrow;
    public Color ArrowColor;
    public HandCoach HandCoach;
    public float MinThrusterAmount = 1f;
    public TextMesh percentageText;
    private float percentage;

    [SerializeField]
    private float segmentProgressMultiplier;

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
    /// <summary>
    /// Set any relevant starting conditions for the tutorial
    /// </summary>
    public void Activate()
    {
        gameObject.SetActive(true);
        page = 0;
        SetPage(page);
        StartCoroutine(DoTutorialOverTime());
    }
    /// <summary>
    /// Goes to next page/segment of tutorial
    /// </summary>
    public void NextPage()
    {
        page++;

        if (page >= Pages.Count)
            page = Pages.Count - 1;

        progress = 0f;

        SetPage(page);
    }

    /// <summary>
    /// Goes to prev page/segment of tutorial
    /// </summary>
    public void PrevPage()
    {
        page--;
        if (page < 0)
            page = 0;
        progress = 0f;
        SetPage(page);
    }
    /// <summary>
    /// Resets the tutorial by reloading the scene. 
    /// Note: A real tutorial inside of a game will require a way to reset all relevant variables in order to restart the tutorial experience
    /// </summary>
    public void StartOver()
    {
        // get the current scene name 
        string sceneName = SceneManager.GetActiveScene().name;

        // load the same scene
        SceneManager.LoadScene(sceneName, LoadSceneMode.Single);
    }

    /// <summary>
    /// Returns the current page/segment name of the tutorial
    /// </summary>
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

    /// <summary>
    /// Extends the OnTapped functionality inherited from InteractionReceiver
    /// Specify the names of the gameObjects that will be tapped in the course of your tutorial
    /// </summary>
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

    /// <summary>
    /// The gameobject for each Page (or section) of the tutorial should correspond to a case in this tutorial
    /// This coroutine will step through each page/segment of the tutorial, moving to the next one when a.) user hits Next button 
    /// or b.) progress fills up to a point where user has spent enough time in the tutorial segment
    /// </summary>
    IEnumerator DoTutorialOverTime()
    {
        while (!reachedEnd)
        {
            switch (CurrentPageName)
            {
                case "TutorialSegment_01":
                    LanderInput_4Tut.Instance.gameObject.SetActive(false);
                    HandCoach.Visibility = HandCoach.HandVisibilityEnum.Both;
                    HandCoach.CheckTracking = HandCoach.HandVisibilityEnum.Both;
                    HandCoach.Ghosting = HandCoach.HandVisibilityEnum.None;
                    HandCoach.Highlight = HandCoach.HandVisibilityEnum.Both;
                    HandCoach.RightGesture = HandCoach.HandGestureEnum.Ready;
                    HandCoach.LeftGesture = HandCoach.HandGestureEnum.Ready;
                    if (HandCoach.Tracking == HandCoach.HandVisibilityEnum.Both)
                    {
                        progress += Time.deltaTime * 4;
                    }
                    if (progress >= minProgressAmount)
                    {
                        NextPage();
                    }
                    break;
                case "TutorialSegment_02":
                    LanderInput_4Tut.Instance.gameObject.SetActive(true);
                    HandCoach.Visibility = HandCoach.HandVisibilityEnum.Both;
                    HandCoach.RightGesture = HandCoach.HandGestureEnum.Tap;
                    HandCoach.LeftGesture = HandCoach.HandGestureEnum.Tap;
                    HandCoach.LeftMovement = HandCoach.HandMovementEnum.PingPong;
                    HandCoach.RightMovement = HandCoach.HandMovementEnum.PingPong;

                    if (Progress > minProgressAmount)
                    {
                        Debug.Log("lander y position before switching to next segment " + LanderInput_4Tut.Instance.transform.position.y);
                        LanderInput_4Tut.Instance.gameObject.SetActive(false);
                        NextPage();
                    }
                    break;
                case "TutorialSegment_03":

                     HandCoach.RightGesture = HandCoach.HandGestureEnum.TapHold;
                     HandCoach.LeftGesture = HandCoach.HandGestureEnum.TapHold;

                     percentageText.gameObject.SetActive(true);
                     //we shouldn't be referring to the lunar lander here
                     if (InputSources.Instance.hands.IsHandVisible(InputSourceHands.HandednessEnum.Left) && InputSources.Instance.hands.IsHandVisible(InputSourceHands.HandednessEnum.Right))
                     {
                         if ((LanderInput_4Tut.Instance.LeftHandInput.Pressed && LanderInput_4Tut.Instance.RightHandInput.Pressed))
                         {
                             PushPercentage();
                         }
                     } 
#if UNITY_EDITOR
                    if (Input.GetKey(KeyCode.Space))
                    {
                        Debug.Log("We should now be pushing percentage");
                        PushPercentage();
                    }
#endif
                    break;
                case "TutorialSegment_04":
                    //Congrats
                    break;

                //add as many segments as you need

                default:
                    Debug.LogError("No segment found in Pages List that matches that name");
                    break;
            }
            yield return null;
        }
    }

    /// <summary>
    /// Increases the progress of the tutorial segment progress bar until it hits a threshold
    /// </summary>
    void PushPercentage()
    {
        //if Text is less than 100 percent, Text Counts upward
        if (percentage <= 100)
        {
            percentage += Time.deltaTime*segmentProgressMultiplier;
            percentageText.text = percentage.ToString("F0") + "%";
        }
        else
        {
            percentageText.gameObject.SetActive(false);
            NextPage();
        }
    }
}

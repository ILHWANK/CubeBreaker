using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class GameManager : MonoBehaviour
{
    public UnityEvent onReset;
    //onSet에 등록된 모든 함수를 실행 시킬수 있다.

    public static GameManager instance;
    //싱글턴 패턴을 위한 Static 변수 선언

    public GameObject readyPannel;

    public Text scoreText;

    public Text bestScoreText;

    public Text messageText;

    public bool isRoundActive = false;

    private int score = 0;

    public ShooterRotator ShooterRotator;

    public CamFollow cam;

    // Start is called before the first frame update
    void Awake()
    {
        instance = this;
        UpdateUI();
    }

    private void Start()
    {
        StartCoroutine("RoundRoutine"); 
    }

    public void AddScore(int newScore)
    {
        score += newScore;
        UpdateBestSocre();
        UpdateUI(); 
    }

    void UpdateBestSocre()
    {
        if(GetBestScore() < score)
        {
            PlayerPrefs.SetInt("BestScore", score);
        }
    }

    int GetBestScore()
    {
        int bestScore = PlayerPrefs.GetInt("BestScore");
        return bestScore;

    }

    void UpdateUI()
    {
        scoreText.text = "Score : " + score;
        bestScoreText.text = "Best Score : " + GetBestScore(); 
    }

    public void OnBallDestory()
    {
        UpdateUI();
        isRoundActive = false; 
    }

    public void Reset()
    {
        score = 0;
        UpdateUI();

        //라운드 다시 시작을 위한 코드
        StartCoroutine("RoundRoutine");
    }

    IEnumerator RoundRoutine()
    {
        //Ready
        onReset.Invoke();

        readyPannel.SetActive(true);
        cam.SetTarget(ShooterRotator.transform, CamFollow.State.Idle);
        ShooterRotator.enabled = false;

        isRoundActive = false;

        messageText.text = "Ready...";

        yield return new WaitForSeconds(3f);

        //Play
        isRoundActive = true;
        readyPannel.SetActive(false);
        ShooterRotator.enabled = true;

        cam.SetTarget(ShooterRotator.transform, CamFollow.State.Ready);

        while(isRoundActive == true)
        {
            yield return null;
        }

        //End
        readyPannel.SetActive(true);
        ShooterRotator.enabled = false;

        messageText.text = "Wait For Next Round...";

        yield return new WaitForSeconds(3f);
        Reset();
    }
}

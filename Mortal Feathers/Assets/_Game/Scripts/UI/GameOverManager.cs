using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class GameOverManager : MonoBehaviour
{
    [SerializeField]
    private float restartFadeInterval;

    [SerializeField]
    private TextMeshProUGUI txtYourKillCount;

    [SerializeField]
    private TextMeshProUGUI txtYourTime;

    [SerializeField]
    private TextMeshProUGUI txtRestart;

    #region Engine Functions
    // Start is called before the first frame update
    private void Start()
    {
        StartCoroutine(RestartFade(restartFadeInterval));
    }

    // Update is called once per frame
    private void Update()
    {
        
    }
    #endregion

    #region My Functions

    private IEnumerator RestartFade(float interval)
    {
        yield return new WaitForSeconds(interval);
        txtRestart.enabled = (txtRestart.enabled) ? false : true;

        StartCoroutine(RestartFade(restartFadeInterval));
    }

    public void SetUIValues(string killCount, string time)
    {
        txtYourKillCount.text = killCount;
        txtYourTime.text = time;
    }

    #endregion
}

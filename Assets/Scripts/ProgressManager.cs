using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProgressManager : MonoBehaviour {

    public UnityEngine.UI.Scrollbar progress;
    public UnityEngine.UI.Text progressText;
    public int scoreMax;
    public int score;
	// Use this for initialization
	void Start () {
        score = 0;

        UpdateUI();
	}

    public void UpdateScore(int n) {
        for (int i = 0; i < n; i++) {
            StartCoroutine("AddOneToUI");
        }
    }

    public IEnumerator AddOneToUI()
    {
        yield return new WaitForSeconds(Random.value);

        score += 1;
        UpdateUI();

    }

    private void UpdateUI() {
        progress.size = (float)score / (float)scoreMax;
        progressText.text = score/scoreMax + " %";
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text _timerText;
    public AudioClip _brmusic;
    public AudioClip _openingMusic;
    // Start is called before the first frame update
    void Start()
    {
        AudioSource.PlayClipAtPoint(_openingMusic, new Vector3(-9, -2, 0), 1f);
        StartCoroutine(musicDelay());
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void updateTime(float _time)
    {
        _timerText.text = "" + _time.ToString(".0");
    }

    public IEnumerator musicDelay()
    {
        yield return new WaitForSeconds(2);
        AudioSource.PlayClipAtPoint(_brmusic, new Vector3(-9, -2, 0), 1f);
    }
}

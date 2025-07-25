using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayButton : MonoBehaviour
{

    [SerializeField] private AudioSource startingGameSound;
    private float delay = 1f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void PlayGame()
    {
        startingGameSound.Play();
        StartCoroutine(DelayBeforePlay());
    }

    IEnumerator DelayBeforePlay()
    {
        yield return new WaitForSeconds(delay);

        SceneManager.LoadScene("Main Game");
    }
}

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class countdowntimer : MonoBehaviour
{

    public Text Countdowntext;
    public Text RunTimer;
    public int count;

    [SerializeField]
    private Player player;
    [SerializeField]
    private enemy police;

    // Start is called before the first frame update
    public void Start()
    {
        StartCoroutine(Countdown());
    }

    IEnumerator Countdown()
    {
        //player.ismove = false;
        //olice.enabled = false;

        while(count > 0)
        {
            Countdowntext.text = count.ToString();

            yield return new WaitForSeconds(1f);

            count--;
        }

        Countdowntext.text = "GO!";

        yield return new WaitForSeconds(1f);

        //enable player and enemy movement
        //player.ismove = true;
        //police.enabled = true;

        Countdowntext.gameObject.SetActive(false);
    }

}

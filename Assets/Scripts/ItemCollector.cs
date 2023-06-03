using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemCollector : MonoBehaviour
{
    private int cherries = 0;
    [SerializeField]
    private TextMeshProUGUI cherriesText;

    [SerializeField]
    private AudioSource collectSoundEffect;

    public void OnTriggerEnter2D(Collider2D collision)
    {
        // 如果玩家与一个带有“Cherry”标签的游戏对象发生碰撞，游戏应该销毁该游戏对象。
        if (collision.gameObject.CompareTag("Cherry"))
        {
            collectSoundEffect.Play();
            Destroy(collision.gameObject);
            cherries++;
            cherriesText.text = string.Format("Cherries: {0}", cherries);
        }
    }

}

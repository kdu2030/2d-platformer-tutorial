using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{
    [SerializeField]
    private Transform player;

    // Update is called once per frame
    private void Update()
    {
        // 让摄像机跟随Player对象
        transform.position = new Vector3(player.position.x, player.position.y, transform.position.z);
    }
}

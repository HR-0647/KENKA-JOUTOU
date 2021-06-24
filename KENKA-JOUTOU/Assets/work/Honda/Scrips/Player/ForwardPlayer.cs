using UnityEngine;

public class ForwardPlayer : MonoBehaviour
{
    [SerializeField]
    private GameObject player;   //プレイヤー情報格納用
    [SerializeField]
    private GameObject player2;
    private Vector3 offset;      //相対距離取得用

    // Use this for initialization
    void Start()
    {
        // MainCamera(自分自身)とplayerとの相対距離を求める
        offset = transform.position - player.transform.position - player2.transform.position;

    }

    // Update is called once per frame
    void Update()
    {
        //新しいトランスフォームの値を代入する
        transform.position = player.transform.position + offset;
    }
}

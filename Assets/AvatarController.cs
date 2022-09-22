using Photon.Pun;
using UnityEngine;

// MonoBehaviourPunCallbacksを継承して、photonViewプロパティを使えるようにする
public class AvatarController : MonoBehaviourPunCallbacks
{
    private Vector2 touchPos, prePos;
    private float startTime, distance, present_Location = 2f;

    public float speed = 4.0f;  // スピード


    private void Start()
    {
        var materialColor = GetComponent<SpriteRenderer>();
        if (photonView.IsMine)
            materialColor.color = Color.red;
        else
            materialColor.color = Color.green;
    }

    private void Update()
    {
    #if UNITY_EDITOR
        // Unityエディタの処理  

        // 自身が生成したオブジェクトだけに移動処理を行う
        if (photonView.IsMine && Input.GetMouseButtonDown(0))
        {
            present_Location = 0f;
            
            prePos = transform.position;
            touchPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(touchPos);

            distance = Vector2.Distance(prePos, touchPos);

            //var input = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0f);
            //transform.Translate(6f * Time.deltaTime * input.normalized);
        }
#elif UNITY_ANDROID
        // スマホの処理（※厳密にはUnityエディタ以外の処理）  

            if (photonView.IsMine && Input.touchCount > 0)
            {
                present_Location = 0f;

                touchPos = Camera.main.ScreenToWorldPoint(Input.GetTouch(0).position);
                Debug.Log(touchPos);

                distance = Vector2.Distance(prePos, touchPos);
            }
#endif

        if (present_Location < 1.1f)
        {
            // 現在の位置
            present_Location += speed * Time.deltaTime;

            // オブジェクトの移動
            transform.position = Vector3.Lerp(prePos, touchPos, present_Location);
        }
    }
}
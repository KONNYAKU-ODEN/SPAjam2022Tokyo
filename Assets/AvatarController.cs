using Photon.Pun;
using UnityEngine;

// MonoBehaviourPunCallbacks���p�����āAphotonView�v���p�e�B���g����悤�ɂ���
public class AvatarController : MonoBehaviourPunCallbacks
{
    private Vector2 touchPos, prePos;
    private float startTime, distance, present_Location = 2f;

    public float speed = 4.0f;  // �X�s�[�h


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
        // Unity�G�f�B�^�̏���  

        // ���g�����������I�u�W�F�N�g�����Ɉړ��������s��
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
        // �X�}�z�̏����i�������ɂ�Unity�G�f�B�^�ȊO�̏����j  

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
            // ���݂̈ʒu
            present_Location += speed * Time.deltaTime;

            // �I�u�W�F�N�g�̈ړ�
            transform.position = Vector3.Lerp(prePos, touchPos, present_Location);
        }
    }
}
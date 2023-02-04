using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateItem : MonoBehaviour
{
    //  �n���I�u�W�F�N�g
    [SerializeField] private GameObject ground;

    //  ���I�u�W�F�N�g
    [SerializeField] private GameObject water;
    //  �n��I�u�W�F�N�g
    [SerializeField] private GameObject lava;

    //  ���ꂼ��̐������鐔
    [SerializeField] private int waterNum = 2;
    [SerializeField] private int lavaNum = 2;

    //  �A�C�e�����m�̊Ԋu
    [SerializeField] private float ItoIDistance = 1.5f;
    //  y������̐�������
    [SerializeField] private float yDistance = 0.3f;

    //  �~��

    // Start is called before the first frame update
    void Start()
    {
        CreateItems();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //  �A�C�e���𐶐�����
    public void CreateItems()
    {
        //  �����p�̃I�u�W�F�N�g�z��
        List<GameObject> objs = new List<GameObject>();

        //  ���I�u�W�F�N�g�̐���
        for(int i = 0; i<waterNum;i++)
        {
            //  �����ł��邩
            bool createflag = true;

            //  �����ꏊ�����߂�
            Vector3 pos = CreatePlace(i, water.transform.localScale, objs, ref createflag) ;
            //  �����ł��Ȃ��ꍇ�͔�΂�
            if (!createflag) continue;
            //  ��������
            objs.Add(Instantiate(water, pos, Quaternion.identity));
        }

        //  �n��I�u�W�F�N�g�̐���
        for (int i = 0; i < lavaNum; i++)
        {
            //  �����ł��邩
            bool createflag = true;

            //  �����ꏊ�����߂�
            Vector3 pos = CreatePlace(i, water.transform.localScale, objs, ref createflag);
            //  �����ł��Ȃ��ꍇ�͔�΂�
            if (!createflag) continue;
            //  ��������
            objs.Add(Instantiate(lava, pos, Quaternion.identity));
        }

        //  �S�Ďq�I�u�W�F�N�g�ɂ���
        foreach(var obj in objs)
        {
            obj.transform.parent = ground.transform;
        }
    }

    //  �����ꏊ�����߂�
    private Vector3 CreatePlace(int num, Vector3 itemScale, List<GameObject> list, ref bool createFlag)
    {
        //  �㉺�̐������W(�n���̐�Δ��a - �A�C�e���̑��Δ��a)
        float top = ground.transform.localScale.x / 2.0f - 1.5f / 2.0f;
        //  ���E�̐������W
        float side = ground.transform.localScale.y / 2.0f - 1.5f / 2.0f;
        //  ���W
        Vector3 ret = Vector3.zero;
        int i = 0;
        //  �����̏ꍇ�i�n���̏���ɍ��j
        if (num % 2 == 0)
        {
            //  ���W���m�肷��܂ŌJ��Ԃ��E�S�������Đ����s��������I��
            while(i < 1000000)
            {
                i++;
                //  ���W�������_���Ŏ擾
                ret = new Vector3(Random.Range(-side, side), Random.Range(yDistance, top), 0.0f);
                //  ���W�ƒ��S�_�܂ł̋������擾
                float distance = Vector3.Distance(new Vector3(0.0f, 0.0f, 0.0f), ret);

                //  ���a����������
                if (top > distance)
                {   
                    //  0�ȉ��Ȃ牽�����Ȃ�
                    if(list.Count <= 0) return ret;

                    //  �m�F�p�̕ϐ�
                    int hit = 0;
                    //  �S�ẴI�u�W�F�N�g�Ɠ������Ă��邩�m�F����
                    foreach(var obj in list)
                    {
                        //  ��r�p�̋���
                        float length = Vector3.Distance(obj.transform.position, ret);
                        //  ��ł��ڂ��Ă���Ȃ珈���𔲂���
                        if (length <= ItoIDistance) break;
                        hit++;
                    }

                    if (hit >= list.Count) return ret;
                }
            }
        }
        //  ��̏ꍇ�i�n���̉����ɍ��j
        else
        {
            //  ���W���m�肷��܂ŌJ��Ԃ��E�S�������Đ����s��������I��
            while (i < 1000000)
            {
                i++;
                //  ���W�������_���Ŏ擾
                ret = new Vector3(Random.Range(-side, side), Random.Range(-top, -yDistance), 0.0f);
                //  ���W�ƒ��S�_�܂ł̋������擾
                float distance = Vector3.Distance(new Vector3(0.0f, 0.0f, 0.0f), ret);
                //  ���a�����������Ȃ�OK
                if (top > distance)
                {
                    //  0�ȉ��Ȃ牽�����Ȃ�
                    if (list.Count <= 0) return ret;
                    //  �m�F�p�̕ϐ�
                    int hit = 0;
                    //  �S�ẴI�u�W�F�N�g�Ɠ������Ă��邩�m�F����
                    foreach (var obj in list)
                    {
                        //  ��r�p�̋���
                        float length = Vector3.Distance(obj.transform.position, ret);
                        //  ��ł��ڂ��Ă���Ȃ珈���𔲂���
                        if (length <= ItoIDistance) break;
                        hit++;
                    }

                    if (hit >= list.Count) return ret;
                }
            }
        }

        createFlag = false;

        return ret;
    }
}

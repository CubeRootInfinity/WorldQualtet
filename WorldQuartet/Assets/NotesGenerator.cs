using UnityEngine;
using System;

public class NotesGenerator : MonoBehaviour
{
    [Serializable]
    public class InputJson
    {
        public Notes[] notes;
        public int BPM;
    }

    [Serializable]
    public class Notes
    {
        public int num;
        public int block;
        public int LPB;
    }

    public int[] scoreNum;//�m�[�c�̔ԍ������ɓ����
    public int[] scoreBlock;//�m�[�c�̎�ނ����ɓ����
    public int BPM;
    public int LPB;

    void Awake()
    {
        MusicReading("a");
    }

    /// <summary>
    /// ���ʂ̓ǂݍ���
    /// </summary>
    void MusicReading(string name)
    {
        string inputString = Resources.Load<TextAsset>(name).ToString();
        InputJson inputJson = JsonUtility.FromJson<InputJson>(inputString);

        scoreNum = new int[inputJson.notes.Length];
        scoreBlock = new int[inputJson.notes.Length];
        BPM = inputJson.BPM;
        LPB = inputJson.notes[0].LPB;

        for (int i = 0; i < inputJson.notes.Length; i++)
        {
            //�m�[�c������ꏊ������
            scoreNum[i] = inputJson.notes[i].num;
            //�m�[�c�̎�ނ�����(scoreBlock[i]��scoreNum[i]�̎��)
            scoreBlock[i] = inputJson.notes[i].block;
        }

    }
    public class NotesCreator : MonoBehaviour
    {
        [SerializeField]
        private GameObject notesPre;

        private float moveSpan = 0.01f;
        private float nowTime;// ���y�̍Đ�����Ă��鎞��
        private int beatNum;// ���̔���
        private int beatCount;// json�z��p(����)�̃J�E���g
        private bool isBeat;// �r�[�g��ł��Ă��邩(�����̃^�C�~���O)

        int BPM = NotesGenerator.BPM;

        void Awake()
        {
            InvokeRepeating("NotesIns", 0f, moveSpan);
        }


        /// <summary>
        /// ���ʏ�̎��ԂƃQ�[���̎��Ԃ̃J�E���g�Ɛ���
        /// </summary>
        void GetScoreTime()
        {
            //���̉��y�̎��Ԃ̎擾
            nowTime += moveSpan; //(1)

            //�m�[�c�������Ȃ����珈���I��
            if (beatCount > scoreNum.Length) return;

            //�y����łǂ����̎擾
            beatNum = (int)(nowTime * BPM / 60 * LPB); //(2)
        }

        /// <summary>
        /// �m�[�c�𐶐�����
        /// </summary>
        void NotesIns()
        {
            GetScoreTime();

            //json��ł̃J�E���g�Ɗy����ł̃J�E���g�̈�v
            if (beatCount < scoreNum.Length)
            {
                isBeat = (scoreNum[beatCount] == beatNum); //(3)
            }

            //�����̃^�C�~���O�Ȃ�
            if (isBeat)
            {
                //�m�[�c0�̐���
                if (scoreBlock[beatCount] == 0)
                {
                }

                //�m�[�c1�̐���
                if (scoreBlock[beatCount] == 1)
                {
                    Instantiate(notesPre);
                }

                beatCount++; //(5)
                isBeat = false;

            }
        }
    }


}

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

    private int[] scoreNum;//�m�[�c�̔ԍ������ɓ����
    private int[] scoreBlock;//�m�[�c�̎�ނ����ɓ����
    private int BPM;
    private int LPB;

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

}
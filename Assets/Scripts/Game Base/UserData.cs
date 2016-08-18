using UnityEngine;
using System.Collections;
using System.Collections.Generic;


    [System.Serializable]
    public class UserData
    {
        public int hightScore = 0;
        public int balans = 0;
        public int gems = 0;
        public List<int> payedCharacters = new List<int>();
        public int currentCharacter = 0;

        public int Balans
        {
            get
            {
                return balans;
            }
            set
            {
                if(value < 0)
                    value = 0;
                balans = value;
            }
        }

        public int Gems
        {
            get
            {
                return gems;
            }
            set
            {
                if(value < 0)
                    value = 0;
                gems = value;
            }
        }

    public int Hightscore
        {
            get
            {
                return hightScore;
            }
            set
            {
                if(value < 0)
                    value = 0;
                hightScore = value;
            }
        }
}

    static public class UDSave
    {
        static private UserData _data;

        static private string _saveName = "userDataSave";
        static private int _version = 1;
        static private string _cryptPassword = "qWg+!k85%2sgr305f4d~#S";

        static public UserData Get()
        {
            if(_data == null)
            {
                if(!PlayerPrefs.HasKey(_saveName))
                {
                    _data = new UserData();
                }
                else
                {
                    string _stringDataEncoded = PlayerPrefs.GetString(_saveName);
                    string _stringData = _stringDataEncoded;
                    _data = JsonUtility.FromJson<UserData>(_stringData);
                }
            }
            
            return _data;
        }

        static public void Reset()
        {
            _data = new UserData();
            Save();
        }

        static public void Save()
        {
            if(_data == null)
                _data = Get();

            string _stringData = JsonUtility.ToJson(_data);
            string _stringDataEncoded = _stringData;
            PlayerPrefs.SetString(_saveName, _stringDataEncoded);
        }
    }


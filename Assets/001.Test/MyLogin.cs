using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using GooglePlayGames;

public class MyLogin : MonoBehaviour
{
    [SerializeField]
    Text resultText;


    private void Start()
    {
       PlayGamesPlatform.DebugLogEnabled = true;
        PlayGamesPlatform.Activate();   //이걸 안하면 로그인이 작동안함
    }

    public void Login()
    {
        Social.localUser.Authenticate((bool success) =>
        {
            if (success)
            {
                resultText.text = $"{Social.localUser.userName}님 환영합니다.";
                resultText.text += $" ID : {Social.localUser.id}";
            }
            else resultText.text = "로그인 실패";
        });
    }
    public void Logout()
    {
        resultText.text = "로그아웃";

        var pgp = (PlayGamesPlatform)Social.Active;
        pgp.SignOut();
    }
    public void Register()
    {
        resultText.text = "회원가입을 하시겠습니까?";

    }


}

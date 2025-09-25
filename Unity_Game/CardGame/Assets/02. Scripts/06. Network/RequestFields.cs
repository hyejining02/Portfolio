using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RequestFields
{
    public string educat_cd;    // 학원코드
    public string studentId;    // 접속자 고유번호
    public string studentName;  // 접속자명
    public string authToken;    // 접속 인증 토큰
    public int resultCode;      // 결과코드 ( 200=정상, 그 외는 오류 )
    public string resultMsg;    // 결과 메세지
}
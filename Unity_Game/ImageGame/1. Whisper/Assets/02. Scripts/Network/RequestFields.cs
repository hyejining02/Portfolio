using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class RequestFields
{
    public string educat_cd;    // �п��ڵ�
    public string studentId;    // ������ ������ȣ
    public string studentName;  // �����ڸ�
    public string authToken;    // ���� ���� ��ū
    public int resultCode;      // ����ڵ� ( 200=����, �� �ܴ� ���� )
    public string resultMsg;    // ��� �޼���
}
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// ����� ��� ������ ������������� �� ������
/// </summary>
public class TeacherPicker : MonoBehaviour
{


    [Tooltip("���� ��������������")]
    [SerializeField]
    private List<Teacher> teachersPool;

    public Teacher PickNewTeacher()
   {
        int index = Random.Range(0, teachersPool.Count - 1);
        Teacher pickedTeacher=teachersPool[index];
        teachersPool.RemoveAt(index);
        return pickedTeacher;
   }



}

using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Класс для выбора преподавателя на неделю
/// </summary>
public class TeacherPicker : MonoBehaviour
{


    [Tooltip("Пулл преподавателей")]
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

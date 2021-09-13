using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// Class that stores a pool of teachers
/// </summary>
public class TeachersPool : MonoBehaviour
{ 
    [SerializeField] private List<Teacher> teachersPool;

    /// <summary>
    /// Gets a random teacher from the pool
    /// </summary>
    /// <returns></returns>
    public Teacher PickNewTeacher()
   {
        int index = Random.Range(0, teachersPool.Count - 1);
        Teacher pickedTeacher=teachersPool[index];
        teachersPool.RemoveAt(index);
        return pickedTeacher;
   }



}

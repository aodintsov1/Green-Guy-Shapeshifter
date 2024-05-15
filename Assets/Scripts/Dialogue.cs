using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Dialogue
{
    [SerializeField] List<string> lines;
    [SerializeField] List<string> names;
    public List<string> Lines => lines;
    public List<string> Names => names;
}

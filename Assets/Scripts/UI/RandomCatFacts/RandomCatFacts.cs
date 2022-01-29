using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomCatFacts : MonoBehaviour
{
    public List<string> facts = new List<string>();
    public UnityEngine.UI.Text text;

    float lastFactPresented;
    float timeBetweenFacts = 14;

    private void Awake()
    {
        ChangeText();
    }

    private void Update()
    {
        if (Time.time - lastFactPresented > timeBetweenFacts)
        {
            ChangeText();
        }
    }

    void ChangeText()
    {
        text.text = facts[Random.Range(0, facts.Count-1)];
        lastFactPresented = Time.time;
    }
}

using UnityEngine;

public class Test : MonoBehaviour
{
    public Material material;
    public float frame;
    public float timePerFrame;

    public int columns = 10;
    public int rows = 4;
    public float framesPerSecond = 10.0f;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        frame += Time.deltaTime / timePerFrame;
        int currentFrame = (int)frame % (columns * rows);
        material.SetFloat("_Frame", currentFrame);
    }
}

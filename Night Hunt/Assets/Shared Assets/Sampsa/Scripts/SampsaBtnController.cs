using UnityEngine;

public class SampsaBtnController : MonoBehaviour
{
    private Animator SampsaBtnMator;

    private bool isHovering;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        SampsaBtnMator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (isHovering)
        {
            SampsaBtnMator.SetBool("isHovering", true);
        }
        else
        {
            SampsaBtnMator.SetBool("isHovering", false);
        }
    }
}

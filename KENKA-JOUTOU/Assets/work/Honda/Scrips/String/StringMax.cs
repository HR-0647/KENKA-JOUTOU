using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Obi;

public class StringMax : MonoBehaviour
{
    private ObiRopeCursor cursor;

    private ObiRope rope;

    void Start()
    {
        cursor = GetComponent<ObiRopeCursor>();

        rope = GetComponent<ObiRope>();
    }

    void Update()
    {
        if(rope.restLength > 1.5f)
        {
            cursor.ChangeLength(rope.restLength + 1f * Time.deltaTime);
        }

        if(rope.restLength < 1.5f)
        {
            cursor.ChangeLength(rope.restLength - 1f * Time.deltaTime);
        }
    }
}

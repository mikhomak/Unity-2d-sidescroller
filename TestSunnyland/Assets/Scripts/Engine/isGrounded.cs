using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class isGrounded{

    
    // Checking if the character is standing on the ground
    public static bool checkGrounded(this Transform trans)
    {
        LayerMask layerMask = LayerMask.NameToLayer("ground");
        Vector2 position = trans.position;
        Vector2 direction = Vector2.down;
        float distance = 0.3f;

        RaycastHit2D hit = Physics2D.Raycast(position, direction, distance, layerMask);
        if (hit.collider != null)
        {

            return true;
        }

        return false;
    }
}

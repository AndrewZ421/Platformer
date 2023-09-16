using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Platformer
{
    public class CameraFollow : MonoBehaviour
    {
        public Transform target; // The player's Transform to follow
        public Vector3 offset;   // Offset between the camera and the player

        // Update is called once per frame
        void Update()
        {
            if (target != null)
            {
                // Calculate the desired camera position based on the player's position and the offset
                Vector3 desiredPosition = target.position + offset;

                // Smoothly move the camera towards the desired position
                transform.position = Vector3.Lerp(transform.position, desiredPosition, Time.deltaTime * 5f);
            }
        }
    }
}

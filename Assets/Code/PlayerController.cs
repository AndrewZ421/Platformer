using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Platformer
{
    public class PlayerController : MonoBehaviour
    {
        // Outlet
        Rigidbody2D _rigidbody2D;

        // State Tracking
        public int jumpsLeft;
        public int keyCount = 0;
        public int currentAmmo = 0;
        public GameObject bulletPrefab;

        // Character Scale
        private Vector3 normalScale = new Vector3(1f, 1f, 1f);
        private Vector3 enlargedScale = new Vector3(2f, 2f, 2f);
        private bool isEnlarged = false;

        // Jump Force
        private float normalJumpForce = 8f;
        private float enlargedJumpForce = 12f;

        // Start is called before the first frame update
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
        }

        // Update is called once per frame
        void Update()
        {
            // Move Player Left
            if (Input.GetKey(KeyCode.A))
            {
                _rigidbody2D.AddForce(Vector2.left * 18f * Time.deltaTime, ForceMode2D.Impulse);
            }

            // Move Player Right
            if (Input.GetKey(KeyCode.D))
            {
                _rigidbody2D.AddForce(Vector2.right * 18f * Time.deltaTime, ForceMode2D.Impulse);
            }

            // Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (jumpsLeft > 0)
                {
                    jumpsLeft--;
                    float jumpForce = isEnlarged ? enlargedJumpForce : normalJumpForce;
                    _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }
            }

            // Toggle character size with the "Q" key
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ToggleCharacterSize();
            }

            // Shoot
            if (Input.GetMouseButtonDown(0) && currentAmmo > 0)
            {
                Shoot();
            }
        }

        public void AddAmmo(int amount)
        {
            currentAmmo += amount;
        }

        void Shoot()
        {
            GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            bullet.transform.localScale = new Vector3(Mathf.Sign(transform.localScale.x), 1, 1);
            currentAmmo--;
        }


        
        void OnCollisionStay2D(Collision2D other)
        {
            // Reset jumpsLeft (Double Jump)
            // Check that we collided with Ground
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                // Check what is directly below our character's feet
                RaycastHit2D[] hits = Physics2D.RaycastAll(transform.position, Vector2.down, 0.85f);

                for (int i = 0; i < hits.Length; i++)
                {
                    RaycastHit2D hit = hits[i];

                    //Check that we collided with ground below our feet
                    if (hit.collider.gameObject.layer == LayerMask.NameToLayer("Ground"))
                    {
                        // Reset jump count 
                        jumpsLeft = 2;
                    }
                }
            }

        }

        void OnTriggerEnter2D(Collider2D other)
        {
            // AmmoBox
            if (other.gameObject.CompareTag("AmmoBox"))
            {
                AddAmmo(5);
                Destroy(other.gameObject);
            }

            // Key
            if (other.gameObject.CompareTag("Key"))
            {
                keyCount++;
                Destroy(other.gameObject);
            }

            // Exit
            if (other.gameObject.CompareTag("Exit"))
            {
                if (keyCount > 0)
                {
                    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                }
            }
        }

        // Toggle character size
        void ToggleCharacterSize()
        {
            isEnlarged = !isEnlarged; // Toggle the size state.

            // Change the character's scale based on the size state.
            transform.localScale = isEnlarged ? enlargedScale : normalScale;
        }
    }
}

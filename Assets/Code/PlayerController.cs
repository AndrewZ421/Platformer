using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

namespace Platformer
{
    public class PlayerController : MonoBehaviour
    {
        public static PlayerController instance;

        // Outlet
        Rigidbody2D _rigidbody2D;
        public TMP_Text textBulletNum;
        public TMP_Text textKey;
        public Transform Bottom;
        public Transform aimPivot;
        public TMP_Text textHP;

        // State Tracking
        public int jumpsLeft;
        public int keyCount = 0;
        public int currentAmmo = 0;
        public GameObject bulletPrefab;
        public float sight = 0.5f;
        private bool shouldDecelerate = false;
        public bool isPaused;

        // Character Scale
        private Vector3 normalScale = new Vector3(1f, 1f, 1f);
        private Vector3 enlargedScale = Vector3.one * 1.5f;
        private bool isEnlarged = false;
        Animator animator;

        // Jump Force
        private float normalJumpForce = 5f;
        private float enlargedJumpForce = 8f;

        void Awake()
        {
            instance = this;
        }

        // Start is called before the first frame update
        void Start()
        {
            _rigidbody2D = GetComponent<Rigidbody2D>();
            animator = GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (isPaused)
            {
                return;
            }

            if (Input.GetKeyDown(KeyCode.Escape))
            {
                MenuController.instance.Show();
            }

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

            // Stop moving
            if (Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D))
            {
                shouldDecelerate = true;
            }

            // Jump
            if (Input.GetKeyDown(KeyCode.Space))
            {
                Debug.Log(jumpsLeft);
                if (jumpsLeft > 0)
                {
                    jumpsLeft--;
                    float jumpForce = isEnlarged ? enlargedJumpForce : normalJumpForce;
                    // _rigidbody2D.velocity = Vector2.zero;
                    _rigidbody2D.AddForce(Vector2.up * jumpForce, ForceMode2D.Impulse);
                }
            }
            animator.SetInteger("JumpsLeft", jumpsLeft);

            // Toggle character size with the "Q" key
            if (Input.GetKeyDown(KeyCode.Q))
            {
                ToggleCharacterSize();
            }

            // Aim Toward Mouse
            Vector3 mousePosition = Input.mousePosition;
            Vector3 mousePositionInWorld = Camera.main.ScreenToWorldPoint(mousePosition);
            Vector3 directionFromPlayerToMouse = mousePositionInWorld - transform.position;

            float radiansToMouse = Mathf.Atan2(directionFromPlayerToMouse.y, directionFromPlayerToMouse.x);
            float angleToMouse = radiansToMouse * Mathf.Rad2Deg;

            aimPivot.rotation = Quaternion.Euler(0, 0, angleToMouse);

            // Shoot
            if (Input.GetMouseButtonDown(0) && currentAmmo > 0)
            {
                Shoot();
            }

            UpdateDisplay();
        }

        void UpdateDisplay()
        {
            textBulletNum.text = "Bullet: " + currentAmmo.ToString();
            textKey.text = "Key: " + keyCount.ToString();
        }

        void FixedUpdate()
        {
            animator.SetFloat("Speed", _rigidbody2D.velocity.magnitude);

            // Stop moving
            if (shouldDecelerate)
            {
                float decelerationSpeed = 5f;
                Vector2 currentVelocity = _rigidbody2D.velocity;
                float newHorizontalSpeed = Mathf.Lerp(currentVelocity.x, 0, decelerationSpeed * Time.fixedDeltaTime);
                _rigidbody2D.velocity = new Vector2(newHorizontalSpeed, currentVelocity.y);

                if (Mathf.Abs(newHorizontalSpeed) < 0.01f)
                {
                    _rigidbody2D.velocity = new Vector2(0, currentVelocity.y);
                    shouldDecelerate = false;
                }
            }

            float maxHorizontalSpeed = 3f;
            if (Mathf.Abs(_rigidbody2D.velocity.x) > maxHorizontalSpeed)
            {
                _rigidbody2D.velocity = new Vector2(Mathf.Sign(_rigidbody2D.velocity.x) * maxHorizontalSpeed, _rigidbody2D.velocity.y);
            }
        }

        public void AddAmmo(int amount)
        {
            currentAmmo += amount;
        }

        public void UpdateHPDisplay(int currentHP)
        {
            textHP.text = "HP: " + currentHP.ToString();
        }

        void Shoot()
        {
            //  GameObject bullet = Instantiate(bulletPrefab, transform.position, Quaternion.identity);

            // bullet.transform.localScale = new Vector3(Mathf.Sign(transform.localScale.x), 1, 1);

            GameObject newProjectile = Instantiate(bulletPrefab);
            newProjectile.transform.position = transform.position;
            newProjectile.transform.rotation = aimPivot.rotation;
            currentAmmo--;
        }

        void OnCollisionStay2D(Collision2D other)
        {
            //Check that we collided with Ground
            if (other.gameObject.layer == LayerMask.NameToLayer("Ground"))
            {
                // Check what is directly below our character's feet
                RaycastHit2D[] hits = Physics2D.RaycastAll(Bottom.position, Vector2.down, 0.1f);

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
                    //if (SceneManager.GetActiveScene().name == "level1")
                    //{
                    //    SceneManager.LoadScene("level2");
                    //    GameController.instance.UpdateTextLevel();
                    //}
                    //else if (SceneManager.GetActiveScene().name == "level2")
                    //{
                    //    SceneManager.LoadScene("level3");
                    //    GameController.instance.UpdateTextLevel();
                    //}
                    //else
                    //{
                    //    SceneManager.LoadScene(SceneManager.GetActiveScene().name);
                    //}

                    SoundManager.instance.PlaySoundLevelUp();
                    LevelUpController.instance.Show();
                }
            }
        }

        // Toggle character size
        void ToggleCharacterSize()
        {

            isEnlarged = !isEnlarged; // Toggle the size state.
            Debug.Log($"is enlarged:{isEnlarged}");

            // Change the character's scale based on the size state.

            transform.localScale = isEnlarged ? enlargedScale : normalScale;
            // Update the jump force based on the size state.
            float jumpForce = isEnlarged ? enlargedJumpForce : normalJumpForce;
        }

    }
}

using SpaceShip;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerShip
{
    public class PlayerController : MonoBehaviour
    {
        [SerializeField] private PlayerSpaceShip playerSpaceShip;
        private Vector2 movementInput = Vector2.zero;
        private ShipInputAction inputAction;
        private float minX;
        private float maxX;
        private float minY;
        private float maxY;

        private TrailRenderer jetEngine;


        private void Awake()
        {
            InitInput();
            CreateMovementBoundary();

            jetEngine = GetComponent<TrailRenderer>();
        }

        private void InitInput()
        {
            inputAction = new ShipInputAction();
            inputAction.Player.Move.performed += OnMove;
            inputAction.Player.Move.canceled += OnMove;
            inputAction.Player.Fire.performed += OnFire;
        }

        private void OnFire(InputAction.CallbackContext obj)
        {
            PlayerSpaceShip.Instance.Fire();
        }

        public void OnMove(InputAction.CallbackContext obj)
        {
            if (obj.performed)
            {
                movementInput = obj.ReadValue<Vector2>();
            }

            if (obj.canceled)
            {
                movementInput = Vector2.zero;
            }
        }

        private void Update()
        {
            Move();
            TrailTurbo();
        }

        private void TrailTurbo()
        {
            if (movementInput == Vector2.right || movementInput == Vector2.left)
            {
                jetEngine.emitting = false;
            }
            else if (movementInput != Vector2.zero)
            {
                jetEngine.emitting = true;
            }
        }

        private void Move()
        {
            var inputVelocity = movementInput * PlayerSpaceShip.Instance.Speed;

            var newPosition = transform.position;
            newPosition.x = transform.position.x + inputVelocity.x * Time.smoothDeltaTime; //maybe optimize operator
            newPosition.y = transform.position.y + inputVelocity.y * Time.smoothDeltaTime;

            //Clamp movement within boundary
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            newPosition.y = Mathf.Clamp(newPosition.y, minY, maxY);

            transform.position = newPosition;
        }

        private void CreateMovementBoundary()
        {
            var mainCamera = Camera.main;
            Debug.Assert(mainCamera != null, "main Camera can't ne null");
            Debug.Assert(playerSpaceShip != null, "playerSpaceShip can't ne null");

            //Do not let the plane fall across the screen.
            //TODO : Why I can't use PlayerSpaceShip.Instance.GetComponent<SpriteRenderer>()?
            var offset = playerSpaceShip.GetComponent<SpriteRenderer>().bounds.size;
            minX = mainCamera.ViewportToWorldPoint(mainCamera.rect.min).x + offset.x / 2;
            maxX = mainCamera.ViewportToWorldPoint(mainCamera.rect.max).x - offset.x / 2;
            minY = mainCamera.ViewportToWorldPoint(mainCamera.rect.min).y + offset.y / 2;
            maxY = mainCamera.ViewportToWorldPoint(mainCamera.rect.max).y - offset.y / 2;
        }

        private void OnEnable()
        {
            inputAction.Enable();
        }

        private void OnDisable()
        {
            inputAction.Disable();
        }
    }
}
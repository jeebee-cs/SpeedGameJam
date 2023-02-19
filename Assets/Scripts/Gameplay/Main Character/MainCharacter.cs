using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

public class MainCharacter : MonoBehaviour
{
  MainCharacterInput _mainCharacterInput;
  Vector2 _moveDirection = new Vector2();
  bool _isMoving = false;
  [SerializeField] TimerManager _timerManager;
  [SerializeField] float _motionMultiplier;
  [SerializeField] float _acceleration;
  [SerializeField] float _maxSpeed;
  [SerializeField] float _holdTimeRestart;
  [SerializeField] GameObject[] _goldStacks;
  int _goldStacksCollected = 0;
  Rigidbody2D _rb2D;

  void Awake()
  {
    _rb2D = GetComponent<Rigidbody2D>();
  }
  void OnEnable()
  {
    #region EnableInput
    _mainCharacterInput = new MainCharacterInput();

    _mainCharacterInput.Control.Movement.performed += context => InputMove(context);
    _mainCharacterInput.Control.Movement.canceled += context => InputMove(context);
    _mainCharacterInput.Control.Movement.Enable();

    _mainCharacterInput.Control.SlowMotion.performed += context => InputSlowMotion(context);
    _mainCharacterInput.Control.SlowMotion.canceled += context => InputSlowMotion(context);
    _mainCharacterInput.Control.SlowMotion.Enable();

    _mainCharacterInput.Control.AccelerateMotion.performed += context => InputAccelerateMotion(context);
    _mainCharacterInput.Control.AccelerateMotion.canceled += context => InputAccelerateMotion(context);
    _mainCharacterInput.Control.AccelerateMotion.Enable();

    _mainCharacterInput.Control.Reset.performed += context => InputReset(context);
    _mainCharacterInput.Control.Reset.canceled += context => InputReset(context);
    _mainCharacterInput.Control.Reset.Enable();
    #endregion
  }
  void OnDisable()
  {
    #region DisableInput
    _mainCharacterInput.Control.Movement.performed -= context => InputMove(context);
    _mainCharacterInput.Control.Movement.canceled -= context => InputMove(context);
    _mainCharacterInput.Control.Movement.Disable();

    _mainCharacterInput.Control.SlowMotion.performed -= context => InputSlowMotion(context);
    _mainCharacterInput.Control.SlowMotion.canceled -= context => InputSlowMotion(context);
    _mainCharacterInput.Control.SlowMotion.Disable();

    _mainCharacterInput.Control.AccelerateMotion.performed -= context => InputAccelerateMotion(context);
    _mainCharacterInput.Control.AccelerateMotion.canceled -= context => InputAccelerateMotion(context);
    _mainCharacterInput.Control.AccelerateMotion.Disable();

    _mainCharacterInput.Control.Reset.performed -= context => InputReset(context);
    _mainCharacterInput.Control.Reset.canceled -= context => InputReset(context);
    _mainCharacterInput.Control.Reset.Disable();
    #endregion
  }

  void Update()
  {
    if (!_isMoving) Friction();
  }

  void InputMove(InputAction.CallbackContext context)
  {
    if (context.performed)
    {
      _moveDirection = new Vector2(Mathf.RoundToInt(context.ReadValue<Vector2>().x), Mathf.RoundToInt(context.ReadValue<Vector2>().y)).normalized;

      if (!_isMoving && !(_moveDirection == Vector2.zero)) StartCoroutine(InputMoveCoroutine());
    }
    else _moveDirection = Vector2.zero;

    _isMoving = !(_moveDirection == Vector2.zero);
  }
  IEnumerator InputMoveCoroutine()
  {
    _isMoving = true;

    MoveStart();
    while (_isMoving && !(_moveDirection == Vector2.zero))
    {
      MovePerform();
      yield return null;
    }

    MoveCancel();
  }

  void MoveStart()
  {
    //MoveVFX(true);
  }
  void MovePerform()
  {
    Vector2 speed = _rb2D.velocity + _moveDirection * _acceleration;

    if (_moveDirection.x == 0) speed.x = Deceleration(speed.x, _rb2D.velocity.x);
    if (_moveDirection.y == 0) speed.y = Deceleration(speed.y, _rb2D.velocity.y);

    speed.x = Matho.Clamp(speed.x, 0, _maxSpeed * Mathf.Sign(speed.x));
    speed.y = Matho.Clamp(speed.y, 0, _maxSpeed * Mathf.Sign(speed.y));

    _rb2D.velocity = speed;
  }
  void MoveCancel()
  {
    //MoveVFX(false);
  }
  void Friction()
  {
    Vector2 speed = _rb2D.velocity;

    speed.x = Deceleration(speed.x, _rb2D.velocity.x);
    speed.y = Deceleration(speed.y, _rb2D.velocity.y);

    _rb2D.velocity = speed;
  }

  float Deceleration(float speed, float velocity)
  {
    speed = velocity - Mathf.Sign(velocity) * _acceleration;
    speed = Matho.Clamp(speed, 0, _maxSpeed * Mathf.Sign(velocity));

    return speed;
  }

  void InputSlowMotion(InputAction.CallbackContext context)
  {
    if (context.performed) SlowMotionStart();
    else SlowMotionCancel();
  }
  void SlowMotionStart()
  {
    Time.timeScale *= (1 / _motionMultiplier);
  }
  void SlowMotionCancel()
  {
    Time.timeScale *= _motionMultiplier;
  }
  void InputAccelerateMotion(InputAction.CallbackContext context)
  {
    if (context.performed) AccelerateMotionStart();
    else AccelerateMotionCancel();
  }
  void AccelerateMotionStart()
  {
    Time.timeScale *= _motionMultiplier;
  }
  void AccelerateMotionCancel()
  {
    Time.timeScale *= (1 / _motionMultiplier);
  }
  void InputReset(InputAction.CallbackContext context)
  {
    if (context.performed) ResetStart();
  }

  void ResetStart()
  {
    GlobalManager.LoadGame();
  }

  public void Die()
  {
    StartCoroutine(DieCoroutine());
  }
  IEnumerator DieCoroutine()
  {
    yield return new WaitForSeconds(0.01f);
    GlobalManager.LoadGame();
  }
  private void OnTriggerEnter2D(Collider2D other)
  {
    if (other.tag == "GoldStack")
    {
      _goldStacksCollected++;
      Destroy(other.gameObject);
    }

    if ( _goldStacksCollected >= _goldStacks.Length)
    {
      GlobalManager.IsAllGoldCollected = true;

      if (other.tag == "Exit")
        {
          GlobalManager.SolvedGame(_timerManager.timer.TimePass() * 100);
        }
    }
  }
}

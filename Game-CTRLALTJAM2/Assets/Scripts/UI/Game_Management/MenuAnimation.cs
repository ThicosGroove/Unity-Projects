using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class MenuAnimation : MonoBehaviour
{
    public RawImage bg;
    public GameObject txt;
    public GameObject menuPanel;

    [SerializeField] private float _finalPos = 0.2f;

    private bool _keyPressed, _transitionOver;

    void Start()
    {
        menuPanel.SetActive(false);
        _keyPressed = false;
        _transitionOver = false;
    }

    void Update()
    {
        if (Keyboard.current.anyKey.wasPressedThisFrame)
        {
            txt.SetActive(false);
            _keyPressed = true;
        }

        if (_keyPressed)
            MenuTransition();
    }

    private void MenuTransition()
    {
        if (!_transitionOver)
        {
            if (bg.uvRect.position.x < _finalPos)
            {
                bg.uvRect = new Rect(bg.uvRect.position + new Vector2(0.0001f, 0) * Time.deltaTime * 1000, bg.uvRect.size);
            }
            else
            {
                menuPanel.SetActive(true);
                _transitionOver = true;
            }
        }
    }
}

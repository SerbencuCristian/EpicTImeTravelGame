using UnityEngine;
using UnityEngine.InputSystem;
using System.IO;
using TMPro;
using UnityEngine.UI;

public class KeybindManager : MonoBehaviour
{
    public PlayerControls controls;
    public static KeybindManager Instance;
    public string rebindsSaveKey = "keybinds";
    public TextMeshProUGUI JumpKeyText;
    public TextMeshProUGUI MoveLeftKeyText;
    public TextMeshProUGUI MoveRightKeyText;
    public TextMeshProUGUI MoveDownKeyText;
    public TextMeshProUGUI TimeTravelKeyText;
    public TextMeshProUGUI ShootKeyText;
    public TextMeshProUGUI PauseKeyText;
    public TextMeshProUGUI SelectPastKeyText;
    public TextMeshProUGUI SelectFutureKeyText;
    public TextMeshProUGUI SelectPresentKeyText;
    public TextMeshProUGUI TimeSightKeyText;

    public TextMeshProUGUI GpJumpKeyText;
    public TextMeshProUGUI GpMoveLeftKeyText;
    public TextMeshProUGUI GpMoveRightKeyText;
    public TextMeshProUGUI GpMoveDownKeyText;
    public TextMeshProUGUI GpTimeTravelKeyText;
    public TextMeshProUGUI GpShootKeyText;
    public TextMeshProUGUI GpPauseKeyText;
    public TextMeshProUGUI GpSelectPastKeyText;
    public TextMeshProUGUI GpSelectFutureKeyText;
    public TextMeshProUGUI GpSelectPresentKeyText;
    void Awake()
    {

        if (Instance == null)
        {
            Instance = this;
            controls = new PlayerControls();
            LoadRebinds();
            controls.Enable();
            DontDestroyOnLoad(gameObject); // Optional: persist across scenes
        }
        else
        {
            Destroy(gameObject);
        }
    }
    void Start()
    {
        controls.Enable();
    }
    void Update()
    {
        var action = controls.FindAction("Jump");
        string readableKey = InputControlPath.ToHumanReadableString(
        action.bindings[0].effectivePath,
        InputControlPath.HumanReadableStringOptions.OmitDevice);
        JumpKeyText.text = readableKey;
        action = controls.FindAction("MoveLeft");
        readableKey = InputControlPath.ToHumanReadableString(
        action.bindings[0].effectivePath,
        InputControlPath.HumanReadableStringOptions.OmitDevice);
        MoveLeftKeyText.text = readableKey;
        action = controls.FindAction("MoveRight");
        readableKey = InputControlPath.ToHumanReadableString(
        action.bindings[0].effectivePath,
        InputControlPath.HumanReadableStringOptions.OmitDevice);
        MoveRightKeyText.text = readableKey;
        action = controls.FindAction("Drop");
        readableKey = InputControlPath.ToHumanReadableString(
        action.bindings[0].effectivePath,
        InputControlPath.HumanReadableStringOptions.OmitDevice);
        MoveDownKeyText.text = readableKey;
        action = controls.FindAction("TimeTravel");
        readableKey = InputControlPath.ToHumanReadableString(
        action.bindings[0].effectivePath,
        InputControlPath.HumanReadableStringOptions.OmitDevice);
        TimeTravelKeyText.text = readableKey;
        action = controls.FindAction("Pause");
        readableKey = InputControlPath.ToHumanReadableString(
        action.bindings[0].effectivePath,
        InputControlPath.HumanReadableStringOptions.OmitDevice);
        PauseKeyText.text = readableKey;
        action = controls.FindAction("Past");
        readableKey = InputControlPath.ToHumanReadableString(
        action.bindings[0].effectivePath,
        InputControlPath.HumanReadableStringOptions.OmitDevice);
        SelectPastKeyText.text = readableKey;
        action = controls.FindAction("Future");
        readableKey = InputControlPath.ToHumanReadableString(
        action.bindings[0].effectivePath,
        InputControlPath.HumanReadableStringOptions.OmitDevice);
        SelectFutureKeyText.text = readableKey;
        action = controls.FindAction("Present");
        readableKey = InputControlPath.ToHumanReadableString(
        action.bindings[0].effectivePath,
        InputControlPath.HumanReadableStringOptions.OmitDevice);
        SelectPresentKeyText.text = readableKey;
        action = controls.FindAction("TimeSight");
        readableKey = InputControlPath.ToHumanReadableString(
        action.bindings[0].effectivePath,
        InputControlPath.HumanReadableStringOptions.OmitDevice);
        TimeSightKeyText.text = readableKey;
        action = controls.FindAction("Shoot");
        readableKey = InputControlPath.ToHumanReadableString(
        action.bindings[0].effectivePath,
        InputControlPath.HumanReadableStringOptions.OmitDevice);
        ShootKeyText.text = readableKey;
        // Gamepad bindings
        action = controls.FindAction("Jump");
        readableKey = InputControlPath.ToHumanReadableString(
        action.bindings[1].effectivePath,
        InputControlPath.HumanReadableStringOptions.OmitDevice);
        GpJumpKeyText.text = readableKey;
        action = controls.FindAction("MoveLeft");
        readableKey = InputControlPath.ToHumanReadableString(
        action.bindings[1].effectivePath,
        InputControlPath.HumanReadableStringOptions.OmitDevice);
        GpMoveLeftKeyText.text = readableKey;
        action = controls.FindAction("MoveRight");
        readableKey = InputControlPath.ToHumanReadableString(
        action.bindings[1].effectivePath,
        InputControlPath.HumanReadableStringOptions.OmitDevice);
        GpMoveRightKeyText.text = readableKey;
        action = controls.FindAction("Drop");
        readableKey = InputControlPath.ToHumanReadableString(
        action.bindings[1].effectivePath,
        InputControlPath.HumanReadableStringOptions.OmitDevice);
        GpMoveDownKeyText.text = readableKey;
        action = controls.FindAction("TimeTravel");
        readableKey = InputControlPath.ToHumanReadableString(
        action.bindings[1].effectivePath,
        InputControlPath.HumanReadableStringOptions.OmitDevice);
        GpTimeTravelKeyText.text = readableKey;
        action = controls.FindAction("Pause");
        readableKey = InputControlPath.ToHumanReadableString(
        action.bindings[1].effectivePath,
        InputControlPath.HumanReadableStringOptions.OmitDevice);
        GpPauseKeyText.text = readableKey;
        action = controls.FindAction("Past");
        readableKey = InputControlPath.ToHumanReadableString(
        action.bindings[1].effectivePath,
        InputControlPath.HumanReadableStringOptions.OmitDevice);
        GpSelectPastKeyText.text = readableKey;
        action = controls.FindAction("Future");
        readableKey = InputControlPath.ToHumanReadableString(
        action.bindings[1].effectivePath,
        InputControlPath.HumanReadableStringOptions.OmitDevice);
        GpSelectFutureKeyText.text = readableKey;
        action = controls.FindAction("Present");
        readableKey = InputControlPath.ToHumanReadableString(
        action.bindings[1].effectivePath,
        InputControlPath.HumanReadableStringOptions.OmitDevice);
        GpSelectPresentKeyText.text = readableKey;
    }

public void StartRebind(string actionName)
{
    controls.Disable();
    var action = controls.FindAction(actionName);
    if (action == null) return;
    action.PerformInteractiveRebinding()
        .WithTargetBinding(0)
        .OnComplete(operation =>
        {
            operation.Dispose();
            SaveRebinds();
        })
        .Start();
    controls.Enable();
}
public void StartControllerRebind(string actionName)
{
    controls.Disable();
    var action = controls.FindAction(actionName);
    if (action == null) return;
    action.PerformInteractiveRebinding()
        .WithTargetBinding(1)
        .OnComplete(operation =>
        {
            operation.Dispose();
            SaveRebinds();
        })
        .Start();
    controls.Enable();
}
    public void SaveRebinds()
    {
        string rebinds = controls.SaveBindingOverridesAsJson();
        PlayerPrefs.SetString(rebindsSaveKey, rebinds);
        PlayerPrefs.Save();
    }
    public void ResetRebinds()
    {
        controls.RemoveAllBindingOverrides();
        PlayerPrefs.DeleteKey(rebindsSaveKey);
        controls.Player.Jump.ChangeBinding(0).WithPath("<Keyboard>/space");
        controls.Player.MoveLeft.ChangeBinding(0).WithPath("<Keyboard>/a");
        controls.Player.MoveRight.ChangeBinding(0).WithPath("<Keyboard>/d");
        controls.Player.Drop.ChangeBinding(0).WithPath("<Keyboard>/s");
        controls.Player.TimeTravel.ChangeBinding(0).WithPath("<Keyboard>/e");
        controls.Player.Pause.ChangeBinding(0).WithPath("<Keyboard>/escape");
        controls.Player.Past.ChangeBinding(0).WithPath("<Keyboard>/1");
        controls.Player.Future.ChangeBinding(0).WithPath("<Keyboard>/3");
        controls.Player.Present.ChangeBinding(0).WithPath("<Keyboard>/2");
        controls.Player.TimeSight.ChangeBinding(0).WithPath("<Keyboard>/q");
        controls.Player.Shoot.ChangeBinding(0).WithPath("<Mouse>/leftButton");
        controls.Player.Jump.ChangeBinding(1).WithPath("<Gamepad>/buttonSouth");
        controls.Player.MoveLeft.ChangeBinding(1).WithPath("<Gamepad>/leftStick/left");
        controls.Player.MoveRight.ChangeBinding(1).WithPath("<Gamepad>/leftStick/right");
        controls.Player.Drop.ChangeBinding(1).WithPath("<Gamepad>/buttonEast");
        controls.Player.TimeTravel.ChangeBinding(1).WithPath("<Gamepad>/buttonWest");
        controls.Player.Pause.ChangeBinding(1).WithPath("<Gamepad>/start");
        controls.Player.Past.ChangeBinding(1).WithPath("<Gamepad>/dpad/up");
        controls.Player.Future.ChangeBinding(1).WithPath("<Gamepad>/dpad/down");
        controls.Player.Present.ChangeBinding(1).WithPath("<Gamepad>/dpad/left");
        controls.Player.TimeSight.ChangeBinding(1).WithPath("<Gamepad>/leftShoulder");
        controls.Player.Shoot.ChangeBinding(1).WithPath("<Gamepad>/rightTrigger");
        SaveRebinds(); // Save default bindings if no previous save exists
    }
    public void LoadRebinds()
    {
        if (PlayerPrefs.HasKey(rebindsSaveKey))
        {
            string rebinds = PlayerPrefs.GetString(rebindsSaveKey);
            controls.LoadBindingOverridesFromJson(rebinds);
        }
        else
        {
            controls.Player.Jump.ChangeBinding(0).WithPath("<Keyboard>/space");
            controls.Player.MoveLeft.ChangeBinding(0).WithPath("<Keyboard>/a");
            controls.Player.MoveRight.ChangeBinding(0).WithPath("<Keyboard>/d");
            controls.Player.Drop.ChangeBinding(0).WithPath("<Keyboard>/s");
            controls.Player.TimeTravel.ChangeBinding(0).WithPath("<Keyboard>/e");
            controls.Player.Pause.ChangeBinding(0).WithPath("<Keyboard>/escape");
            controls.Player.Past.ChangeBinding(0).WithPath("<Keyboard>/1");
            controls.Player.Future.ChangeBinding(0).WithPath("<Keyboard>/3");
            controls.Player.Present.ChangeBinding(0).WithPath("<Keyboard>/2");
            controls.Player.TimeSight.ChangeBinding(0).WithPath("<Keyboard>/q");
            controls.Player.Shoot.ChangeBinding(0).WithPath("<Mouse>/leftButton");
            controls.Player.Jump.ChangeBinding(1).WithPath("<Gamepad>/buttonSouth");
            controls.Player.MoveLeft.ChangeBinding(1).WithPath("<Gamepad>/leftStick/left");
            controls.Player.MoveRight.ChangeBinding(1).WithPath("<Gamepad>/leftStick/right");
            controls.Player.Drop.ChangeBinding(1).WithPath("<Gamepad>/buttonEast");
            controls.Player.TimeTravel.ChangeBinding(1).WithPath("<Gamepad>/buttonWest");
            controls.Player.Pause.ChangeBinding(1).WithPath("<Gamepad>/start");
            controls.Player.Past.ChangeBinding(1).WithPath("<Gamepad>/dpad/up");
            controls.Player.Future.ChangeBinding(1).WithPath("<Gamepad>/dpad/down");
            controls.Player.Present.ChangeBinding(1).WithPath("<Gamepad>/dpad/left");
            controls.Player.TimeSight.ChangeBinding(1).WithPath("<Gamepad>/leftShoulder");
            controls.Player.Shoot.ChangeBinding(1).WithPath("<Gamepad>/rightTrigger");
            SaveRebinds(); // Save default bindings if no previous save exists
        }
    }
}
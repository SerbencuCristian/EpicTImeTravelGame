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
    public TextMeshProUGUI GpTimeSightKeyText;
    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            controls = new PlayerControls();
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        controls.Enable();
        }


    void Start()
    {
        controls.Enable();
    }
    public void GetOverrides()
    {
        controls.Disable();
        var saveData = FindObjectOfType<SaveData>();
        if (saveData != null && !string.IsNullOrEmpty(saveData.data.keybindOverrides))
        {
            controls.LoadBindingOverridesFromJson(saveData.data.keybindOverrides);
        }
        FixUnsetBindings();
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
        action = controls.FindAction("TimeSight");
        readableKey = InputControlPath.ToHumanReadableString(
        action.bindings[1].effectivePath,
        InputControlPath.HumanReadableStringOptions.OmitDevice);
        GpTimeSightKeyText.text = readableKey;
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
        var saveData = FindObjectOfType<SaveData>();
        if (saveData != null)
        {
            saveData.data.keybindOverrides = controls.SaveBindingOverridesAsJson();
            saveData.SaveToJson(GameObject.FindObjectOfType<Carry>().save);
        }
    }
    public string SaveOverridesToString()
    {
        return controls.SaveBindingOverridesAsJson();
    }
    void FixUnsetBindings()
{
    // Keyboard bindings
    if (controls.Player.Jump.bindings[0].effectivePath == "<Keyboard>/")
        controls.Player.Jump.ChangeBinding(0).WithPath("<Keyboard>/space");
    if (controls.Player.MoveLeft.bindings[0].effectivePath == "<Keyboard>/")
        controls.Player.MoveLeft.ChangeBinding(0).WithPath("<Keyboard>/a");
    if (controls.Player.MoveRight.bindings[0].effectivePath == "<Keyboard>/")
        controls.Player.MoveRight.ChangeBinding(0).WithPath("<Keyboard>/d");
    if (controls.Player.Drop.bindings[0].effectivePath == "<Keyboard>/")
        controls.Player.Drop.ChangeBinding(0).WithPath("<Keyboard>/s");
    if (controls.Player.TimeTravel.bindings[0].effectivePath == "<Keyboard>/")
        controls.Player.TimeTravel.ChangeBinding(0).WithPath("<Keyboard>/e");
    if (controls.Player.Pause.bindings[0].effectivePath == "<Keyboard>/")
        controls.Player.Pause.ChangeBinding(0).WithPath("<Keyboard>/escape");
    if (controls.Player.Past.bindings[0].effectivePath == "<Keyboard>/")
        controls.Player.Past.ChangeBinding(0).WithPath("<Keyboard>/1");
    if (controls.Player.Future.bindings[0].effectivePath == "<Keyboard>/")
        controls.Player.Future.ChangeBinding(0).WithPath("<Keyboard>/3");
    if (controls.Player.Present.bindings[0].effectivePath == "<Keyboard>/")
        controls.Player.Present.ChangeBinding(0).WithPath("<Keyboard>/2");
    if (controls.Player.TimeSight.bindings[0].effectivePath == "<Keyboard>/")
        controls.Player.TimeSight.ChangeBinding(0).WithPath("<Keyboard>/q");
    if (controls.Player.Shoot.bindings[0].effectivePath == "<Mouse>/")
        controls.Player.Shoot.ChangeBinding(0).WithPath("<Mouse>/leftButton");

    // Gamepad bindings
    if (controls.Player.Jump.bindings[1].effectivePath == "<Gamepad>/")
        controls.Player.Jump.ChangeBinding(1).WithPath("<Gamepad>/buttonSouth");
    if (controls.Player.MoveLeft.bindings[1].effectivePath == "<Gamepad>/")
        controls.Player.MoveLeft.ChangeBinding(1).WithPath("<Gamepad>/leftStick/left");
    if (controls.Player.MoveRight.bindings[1].effectivePath == "<Gamepad>/")
        controls.Player.MoveRight.ChangeBinding(1).WithPath("<Gamepad>/leftStick/right");
    if (controls.Player.Drop.bindings[1].effectivePath == "<Gamepad>/")
        controls.Player.Drop.ChangeBinding(1).WithPath("<Gamepad>/buttonEast");
    if (controls.Player.TimeTravel.bindings[1].effectivePath == "<Gamepad>/")
        controls.Player.TimeTravel.ChangeBinding(1).WithPath("<Gamepad>/buttonWest");
    if (controls.Player.Pause.bindings[1].effectivePath == "<Gamepad>/")
        controls.Player.Pause.ChangeBinding(1).WithPath("<Gamepad>/start");
    if (controls.Player.Past.bindings[1].effectivePath == "<Gamepad>/")
        controls.Player.Past.ChangeBinding(1).WithPath("<Gamepad>/dpad/up");
    if (controls.Player.Future.bindings[1].effectivePath == "<Gamepad>/")
        controls.Player.Future.ChangeBinding(1).WithPath("<Gamepad>/dpad/down");
    if (controls.Player.Present.bindings[1].effectivePath == "<Gamepad>/")
        controls.Player.Present.ChangeBinding(1).WithPath("<Gamepad>/dpad/left");
    if (controls.Player.TimeSight.bindings[1].effectivePath == "<Gamepad>/")
        controls.Player.TimeSight.ChangeBinding(1).WithPath("<Gamepad>/leftShoulder");
    if (controls.Player.Shoot.bindings[1].effectivePath == "<Gamepad>/")
        controls.Player.Shoot.ChangeBinding(1).WithPath("<Gamepad>/rightTrigger");
}
    public void ResetRebinds()
    {
        controls.RemoveAllBindingOverrides();
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
        SaveRebinds();
    }
}
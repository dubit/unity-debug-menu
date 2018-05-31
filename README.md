# unity-debug-menu

## What is it?
A prebuilt debug menu with some customizable options.

The DebugMenu is designed to reduce the time and effort made per project to build custom debug menus. It's customizability with summoning makes it easy for your unique project to implement a custom method to open the DebugMenu.

Built-in Features:
* Custom buttons
* Access and display the cloud build manifest.
* Visible debug logs and exceptions with thier stacktrace.
* Email the log and callstack.

## How to use it.
Have the DebugMenu prefab active in the scene.

### Buttons
Register a custom button with the DebugMenu by using the `AddButton` method. This method requires a unique identifier for the button (which it will also display as the the label for the button) and an Action to invoke when the button is clicked. The method also inclused an optional parameter (`hideDebugMenuOnClick`) for closing the DebugMenu when this custom button is clicked.  
`DebugMenu.Instance.AddButton("Button", () => { Debug.Log("My custom button was clicked."); });`

### Summoners
The ready-made prefab that ships with DUCK, has a DefaultDebugMenuSummoner. Use `F7` on PC or `7 touches` on mobile to open the DebugMenu.

If you want to use your own logic to summon the debug menu then implement your own `IDebugMenuSummoner` and register it.

To register a custom `IDebugMenuSummoner` with the DebugMenu either use `DebugMenu.Instance.AddSummoner`, or add it to the instance of the DebugMenu prefab in the scene, and it will find it on `Start()`.

### Email Addresses
Email addresses can be added to the dropdown via a JSON text asset. You can store this asset anywhere in you Unity project Assets directory as long as you reference it in the `DebugMenu (GameObject) -> EmailPage (GameObject) -> emailAddressesJson (SerializedField)`.

### Stacktrace
To view a stacktrace simpley click on the log and a view will open up with the visible log and stacktrace.
Use the email button in the top right of the screen to generate and send the callstack to an email address.
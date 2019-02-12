function createMySilverlightPlugin()
{  
    Silverlight.createObject(
        "main.xaml",                  // Source property value.
        parentElement,                  // DOM reference to hosting DIV tag.
        "mySilverlightPlugin",         // Unique plug-in ID value.
        {                               // Per-instance properties.
            width:'300',                // Width of rectangular region of
                                        // plug-in area in pixels.
            height:'300',               // Height of rectangular region of
                                        // plug-in area in pixels.
            inplaceInstallPrompt:false, // Determines whether to display
                                        // in-place install prompt if
                                        // invalid version detected.
            background:'white',       // Background color of plug-in.
            isWindowless:'false',       // Determines whether to display plug-in
                                        // in Windowless mode.
            framerate:'24',             // MaxFrameRate property value.
            version:'1.0'               // Silverlight version to use.
        },
        {
            onError:null,               // OnError property value --
                                        // event handler function name.
            onLoad:null                 // OnLoad property value --
                                        // event handler function name.
        },
        null);                          // Context value -- event handler function name.
}
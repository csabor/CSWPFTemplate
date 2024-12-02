using System.Windows;
using System.Windows.Media;

[assembly: ThemeInfo(
    ResourceDictionaryLocation.None,            //where theme specific resource dictionaries are located
                                                //(used if a resource is not found in the page,
                                                // or application resource dictionaries)
    ResourceDictionaryLocation.SourceAssembly   //where the generic resource dictionary is located
                                                //(used if a resource is not found in the page,
                                                // app, or any theme specific resource dictionaries)
)]

//disable DPI awareness at this level, so on application startup,
//the newest available DPI awareness logic (rerendering instead of scaling) can get registered
[assembly: DisableDpiAwareness]

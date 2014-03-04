Sharpie
=======
Philip's C# Library

ImageFinder
- Finds a bitmap within another bitmap with colour tolerance.
- Lockbitmap is used to increase the colour detection speed.

Lockbitmap
-Locks the bitmap and converts it to a pointer to byte array.
-A point (X,Y) cordinate can be given to retreive the colour of the pixel at the given point.

PanelBook
- A custom interface control which inherits from TabControl where it fuctions exactly the same as TabControl
  with the tabs invisible.
  
Native
- Contains native fuctions from the WinAPI.
- Currently contains mouse_event and register and unregister key hooks.

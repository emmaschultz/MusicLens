# MusicLens
User manual

The following user manual will explain how to use the sections of our application that we have created thus far.

	Metronome
	
	The user is able to fully customize their metronome. The metronome can be
	clicked in order to turn it on or off. The user can also start and stop the
	metronome using the voice commands “Start metronome” and “Stop
	metronome”. The metronome can be double clicked to bring up the metronome
	menu. The metronome menu can also be brought up by gazing at the
	metronome widget and saying “Show menu”. From this menu, the user is able to
	choose what style of metronome they would like by clicking the Change Style
	button and then choosing which metronome they would like to use. The user can
	also click the Change Tempo button to bring up options to change the BPM and
	number of subdivisions. The user can either single click or press and hold the ‘+’
	and ‘-‘ buttons to change the BPM. The user can also say “Increase tempo by 10”
	or “Decrease tempo by 10” in order to more rapidly change the BPM. The user
	can also click the Hide Metronome button in order to disable the metronome
	widget. The metronome can always be shown again via the main menu.
	
	Tuner
	
	The user can choose between the two different types of tuners we have created.
	The first tuner view contains a tuning fork and a list of notes. The user can select
	a note to hear the tuning fork play that note. In addition to this, the user can click
	the tuning fork to play whatever note is currently selected. The second tuner view
	contains a scale that determines what note the user is playing. The user just
	needs to play a note, and the tuner will show them how far off they are from the
	note that it is closest to. If the tuner widget is double clicked, the tuner menu is
	brought up and the user can switch in between tuner styles and disable the tuner
	all together. The tuner menu can also be brought up by gazing at the tuner
	widget and saying “Show menu”.
	
	Sheet Music
	
	The user is able to play and pause the sheet music by clicking on the sheet
	music component. The user can also play/pause the music by saying “Start
	music” and “Pause music”. Double clicking on the sheet music component will
	bring up a menu above the sheet music. The sheet music menu can also be
	brought up by gazing at the sheet music widget and saying “Show menu”. From
	here, the user can press the Reset Music button to start the sheet music from the
	beginning again or the Hide Sheet Music button to hide the sheet music
	
	Main Menu
	
	The main menu button will always be in the user’s view and cannot be disabled.
	Upon clicking the Main Menu button, four more buttons will appear below it. The
	first will allow the user to change the theme of the menu buttons. The user can
	click this button and then click any of the four color options to change the theme.
	The other three buttons that are beneath the Main Menu button allow you to
	hide/show the metronome, tuner, and sheet music widgets.

Using MusicLens in Unity
	
	We have an editor set up to mimic what this will look like and act like on the Hololens, 
	although there is black in the background instead of what would be your normal field of vision on the Hololens. 
	In order to move around the virtual cursor provided by the Hololens, simply move your cursor around on the 
	screen. The other inputs are mapped as follows:
	
	1. Single click on an element: make sure the cursor (the ball following your field of vision) is hovering over 
	what element you want to click. Then, simply click using your mouses left click.
	
	2. Double click on an element: make sure the cursor (the ball following your field of vision) is hovering over 
	what element you want to double click. Then, hit the “return” button on your keyboard to simulate a double click. 
	
	3. Hold click on an element: make sure the cursor (the ball following your field of vision) is hovering over
	what element you want to hold click. Then, click and hold your right-click on your mouse to simulate a 
	hold-click down and hold-click up (when you release the right-click). 
	
	**NOTE: Voice commands will NOT work in the unity editor. They only work on the Hololens**

Programmers manual

	Our code is available for use on GitHub. The project must opened the project in Unity
	5.3.1 or later. Our project comes loaded with a modified HoloLens HDK that allows
	developers to do local development that works directly in Unity. This is achieved by, for
	example, mapping single clicks and double for the HoloLens to mouse single clicks and
	hitting the Enter key respectively in Unity. This allows for testing in the Unity scene
	without deploying to the HoloLens. The developer can replace the modified version of
	the HDK with Microsoft’s actual HoloLens HDK if they have purchased the developer
	edition of the HoloLens.

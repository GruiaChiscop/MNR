# MNR (Musical notes recognizer)
A game where you must guess the played musical note
## Description
MNR stands for musical notes recognizer. This app plays a note from a selected octave and you, as a player, must guess what note was played
I started making this project a few months ago, when a friend asked me to make an app for him that plays a note then he must try to guess what was played. Initially, it was a very basic app, with just a list of notes, a submit and exit button. But this app grew along the way because he was requesting me to add new features, fix bugs and so on.
Although this app got improved a lot, it still has some bugs that are not solved yet. And some of them are:
- UI controls: Unfortunately,the main problem with this app is that it is not operable by a sighted user, because of the bad arrangement of the controls on the screen. Some of the items are visible, but most of them are not;
** If you wish to arrange and fix all UI bugs, don't hesitate to open a PR. **
- Playing midi notes: The most annoying bug, in my opinion, is that the library Ii'm using right now which is NAudio, has a slightly poor midi interface and I'm limited to use only the classic windows midi instruments, which are not the best. Probably in the future I'll consider replacing this library with [drywetmidi](https://github.com/melanchall/drywetmidi);
- Efficiency: Because I didn't invest so much time in this app, you'll see many things a programmer should never do, such as code repetition;
- Live regions bug: Another bug I'm aware of is that the screen readers don't always speak the label's content whenever it's modified. And since this app was built for a blind user, this is something that really matters for me.
# Why these morse messages in the app?
Because I'm a ham radio operator and I have a callsign and I'm a developer, I decided to make a sort of logo that represents me. But you have an option in the app settings that turns off all these messages, excepting the welcome message, which is the logo itself, and the "73" message (best regards) that can be heared only when the app closes.
** Please do not remove these messages, especially the welcome message if you modify the source code of this app. **
# Some future plans?
Not really. I published this code just not to lose it and initially I didn't even want to make it public, since it has many bugs that weren't solved. So, officially, I mark it as discontinued, but any suggestion, issue or PR are more than welcome. Also, there's always a possibility to update it in any moment.

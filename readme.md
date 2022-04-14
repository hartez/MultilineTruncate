# MAUI.Controls Multiline Text with Tail Truncation

In MAUI.Controls, the default behavior on Android if you set a Label's `LineBreakMode` to `TailTruncation` is to force the Label to a single line, even if you've set the `MaxLines` property to something greater than 1. This is a backward-compatibility carryover from Xamarin.Forms.

However, Android _does_ support some truncation modes ("Ellipsize", on Android) for multiple lines, and `TailTruncation` (`TextUtils.TruncateAt.End` on Android) is one of them. 

If Xamarin.Forms, adding custom support in your app for this feature requires creating a custom renderer. But with the MAUI handler architecture, you can simply make a quick modification to the mappings for Labels to support this feature. Just write a method to correct the default behavior and append it to the existing mappings for the `LineBreakMode` and `MaxLines` properties. Take a look at https://github.com/hartez/MultilineTruncate/blob/main/MultilineTruncate/MauiProgram.cs to see how it's done.

![Screenshot of an Android phone demonstrating multiline text with tail truncation](https://github.com/hartez/MultilineTruncate/blob/main/multiline.png "Screenshot")
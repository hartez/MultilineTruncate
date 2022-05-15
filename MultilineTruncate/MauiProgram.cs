namespace MultilineTruncate;

public static class MauiProgram
{
	public static MauiApp CreateMauiApp()
	{
		var builder = MauiApp.CreateBuilder();
		builder
			.UseMauiApp<App>()
			.ConfigureFonts(fonts =>
			{
				fonts.AddFont("OpenSans-Regular.ttf", "OpenSansRegular");
				fonts.AddFont("OpenSans-Semibold.ttf", "OpenSansSemibold");
			});

		AllowMultiLineTruncationOnAndroid();

		return builder.Build();
	}

	static void AllowMultiLineTruncationOnAndroid() 
	{
#if ANDROID

        /* 
		 * The default Controls handling of LineBreakMode and MaxLines on Android
		 * only allows single lines when using text truncation. However, combining
		 * setMaxLines() and TextUtils.TruncateAt.END _is_ supported on Android 
		 * (see https://developer.android.com/reference/android/widget/TextView#setEllipsize(android.text.TextUtils.TruncateAt))
		 * 
		 * The following code updates the mappings for Label on Android to support
		 * this scenario. Truncation and max lines both affect the platform setting
		 * of maximum lines, so we need to modify the mappings for both properties. 
		 * We append a second mapping that checks for our target situation (end truncation)
		 * and sets the maximum lines to the target value.
		*/

        static void UpdateMaxLines(Microsoft.Maui.Handlers.LabelHandler handler, ILabel label) 
		{
			var textView = handler.PlatformView;
			if (label is Label controlsLabel && textView.Ellipsize == Android.Text.TextUtils.TruncateAt.End)
			{
				textView.SetMaxLines(controlsLabel.MaxLines);
			}
		};

		Label.ControlsLabelMapper.AppendToMapping(
		   nameof(Label.LineBreakMode), UpdateMaxLines);

		Label.ControlsLabelMapper.AppendToMapping(
			nameof(Label.MaxLines), UpdateMaxLines);

#endif
	}
}

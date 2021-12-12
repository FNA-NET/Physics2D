using Microsoft.Xna.Framework;

namespace AndroidGame1
{
	[Activity(
		Label = "@string/app_name",
		MainLauncher = true,
		HardwareAccelerated = true,
		ScreenOrientation = Android.Content.PM.ScreenOrientation.Landscape
	)]
	public class MainActivity : AndroidGameActivity
	{
		protected override void SDLMain()
		{
            using (var game = new Game1())
                game.Run();
		}
    }
}

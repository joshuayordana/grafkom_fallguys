using OpenTK.Windowing.Desktop;
using OpenTK.Mathematics;

namespace FallGuys
{
	class Program
	{
		static void Main(string[] args)
		{
			var ourWindow = new NativeWindowSettings()
			{
				Size = new Vector2i(600, 600),
				Title = "Fall Guys"
			};

			using (var window = new Window(GameWindowSettings.Default, ourWindow))
			{
				window.Run();
			}
		}
	}
}

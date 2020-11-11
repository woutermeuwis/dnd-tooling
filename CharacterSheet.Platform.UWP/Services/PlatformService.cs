using CharacterSheet.Core.Interfaces;
using System;
using Windows.ApplicationModel.DataTransfer;

namespace CharacterSheet.Platform.UWP.Services
{
	public class PlatformService : IPlatformService
	{
		public void CopyTextToClipboard(string text)
		{
			var pkg = new DataPackage
			{
				RequestedOperation = DataPackageOperation.Copy
			};

			pkg.SetText(text);

			Clipboard.SetContent(pkg);
		}
	}
}

using CharacterSheet.Core.Interfaces;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;

namespace CharacterSheet.Platform.UWP.Services
{
	public class FileService : IFileService
	{
		public async Task<bool> SaveTextToFile(string text, string suggestedFileName = null)
		{
			var savePicker = new Windows.Storage.Pickers.FileSavePicker
			{
				SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary,
				SuggestedFileName = suggestedFileName ?? "export"
			};
			savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });

			var file = await savePicker.PickSaveFileAsync();

			if (file == null)
				return false;

			// Defer updates to filesystem cache until we finish
			Windows.Storage.CachedFileManager.DeferUpdates(file);

			// write data away
			await Windows.Storage.FileIO.WriteTextAsync(file, text);

			// Let Windows know that we're finished changing the file so
			// the other app can update the remote version of the file.
			// Completing updates may require Windows to ask for user input.
			var status = await Windows.Storage.CachedFileManager.CompleteUpdatesAsync(file);

			switch (status)
			{
				case Windows.Storage.Provider.FileUpdateStatus.Complete:
				case Windows.Storage.Provider.FileUpdateStatus.CompleteAndRenamed:
					return true;
				case Windows.Storage.Provider.FileUpdateStatus.Incomplete:
				case Windows.Storage.Provider.FileUpdateStatus.UserInputNeeded:
				case Windows.Storage.Provider.FileUpdateStatus.CurrentlyUnavailable:
				case Windows.Storage.Provider.FileUpdateStatus.Failed:
				default:
					return false;
			}
		}

		public async Task<string> ReadTextFromFile()
		{
			var openPicker = new Windows.Storage.Pickers.FileOpenPicker()
			{
				SuggestedStartLocation = Windows.Storage.Pickers.PickerLocationId.DocumentsLibrary
			};
			openPicker.FileTypeFilter.Add(".txt");

			var file = await openPicker.PickSingleFileAsync();
			if (file == null)
				return null;

			var text = await Windows.Storage.FileIO.ReadTextAsync(file);
			return text;
		}
	}
}

using CharacterSheet.Core.Interfaces;
using System;
using System.IO;
using System.Threading.Tasks;
using Windows.Storage;

namespace CharacterSheet.Platform.UWP.Services
{
    public class StorageService : IStorageService
    {
        private readonly ApplicationDataContainer _localSettings;
        private readonly StorageFolder _localFolder;

        public StorageService()
        {
            _localSettings = ApplicationData.Current.LocalSettings;
            _localFolder = ApplicationData.Current.LocalFolder;
        }

        public void StoreSettings<T>(string key, T item)
        {
            _localSettings.Values[key] = item;
        }

        public T FetchSettings<T>(string key, T defaultValue = default)
        {
            if (_localSettings.Values.ContainsKey(key))
                return (T)_localSettings.Values[key];

            return defaultValue;
        }


        public async Task StoreData(string filename, string data)
        {
            IStorageFile file;
            try
            {
                file = await _localFolder.CreateFileAsync(filename, CreationCollisionOption.OpenIfExists);
            }
            catch (FileNotFoundException)
            {
                Console.WriteLine($"Could not create file with invalid filename: {filename}");
                return;
            }
            catch(UnauthorizedAccessException)
            {
                Console.WriteLine("No permission to access file system");
                return;
            }

            await FileIO.WriteTextAsync(file, data);
        }


        public async Task<string> Fetchdata(string filename)
        {
            IStorageFile file;
            
            try
            {
                file = await _localFolder.GetFileAsync(filename);
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine($"File not found in localstorage: {filename}");
                return null;
            }
            catch(UnauthorizedAccessException)
            {
                Console.WriteLine("No permission to access file system");
                return null;
            }
            catch(ArgumentException)
            {
                Console.WriteLine($"Invalid filename!\n'{filename}' is not a valie URI format!");
                return null;
            }
            
            var text = await FileIO.ReadTextAsync(file);
            return text;
        }

    }
}

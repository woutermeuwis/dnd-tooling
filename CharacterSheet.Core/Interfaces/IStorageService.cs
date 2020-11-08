using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace CharacterSheet.Core.Interfaces
{
    public interface IStorageService
    {

        void StoreSettings<T>(string key, T item);
        T FetchSettings<T>(string key, T defaultValue = default);

        Task StoreData(string filename, string data);
        Task<string> Fetchdata(string filename);
    }
}

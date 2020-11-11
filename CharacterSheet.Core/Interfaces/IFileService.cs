using System.Threading.Tasks;

namespace CharacterSheet.Core.Interfaces
{
	public interface IFileService
	{
		Task<bool> SaveTextToFile(string filename, string text);
		Task<string> ReadTextFromFile();
	}
}

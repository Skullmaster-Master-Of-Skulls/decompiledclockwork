using System;
using TechnoPro.Common.Public.Entities.Updates;

namespace TechnoPro.Common.Public.Entities.FileStorage
{
	// Token: 0x0200033F RID: 831
	public static class ServerStorageSpecialFolderAdapter
	{
		// Token: 0x060019D4 RID: 6612 RVA: 0x0001E260 File Offset: 0x0001C460
		public static string GetSpecialFolderPath(this eServerStorageSpecialFolders specialFolder, string serverFileSystemStorage)
		{
			string result;
			switch (specialFolder)
			{
			case eServerStorageSpecialFolders.FileSystemStorage:
				result = serverFileSystemStorage;
				break;
			case eServerStorageSpecialFolders.UpdatesComputer:
				result = ClockWorkUpdateSystemPathVariables.UPDATES_COMPUTER_PATH;
				break;
			case eServerStorageSpecialFolders.UpdatesPublic:
				result = ClockWorkUpdateSystemPathVariables.UPDATES_PUBLIC_PATH;
				break;
			default:
				result = null;
				break;
			}
			return result;
		}
	}
}

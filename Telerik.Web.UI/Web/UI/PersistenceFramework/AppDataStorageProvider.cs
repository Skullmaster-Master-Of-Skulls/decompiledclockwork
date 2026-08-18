using System;
using System.IO;

namespace Telerik.Web.UI.PersistenceFramework
{
	// Token: 0x02000C20 RID: 3104
	public class AppDataStorageProvider : BaseStateStorageProvider
	{
		// Token: 0x06007614 RID: 30228 RVA: 0x001B6AE1 File Offset: 0x001B4CE1
		public AppDataStorageProvider(string stateFileLocation)
		{
			this.stateFileLocation = stateFileLocation;
		}

		// Token: 0x06007615 RID: 30229 RVA: 0x001B6AF0 File Offset: 0x001B4CF0
		public override void SaveStateToStorage(string key, string serializedState)
		{
			FileStream fileStream = null;
			StreamWriter streamWriter = null;
			try
			{
				fileStream = File.Create(this.stateFileLocation + key);
				streamWriter = new StreamWriter(fileStream);
				streamWriter.Write(serializedState);
			}
			catch (Exception ex)
			{
				throw new PersistenceFrameworkStorageException("Unable to store state. " + ex.Message, ex.InnerException);
			}
			finally
			{
				if (streamWriter != null)
				{
					streamWriter.Close();
				}
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
		}

		// Token: 0x06007616 RID: 30230 RVA: 0x001B6B74 File Offset: 0x001B4D74
		public override string LoadStateFromStorage(string key)
		{
			if (string.IsNullOrEmpty(key))
			{
				throw new PersistenceFrameworkArgumentException("The parameter 'key' cannot be null or empty string.");
			}
			if (key.IndexOf("./") > -1 || key.IndexOf("/.") > -1)
			{
				throw new PersistenceFrameworkArgumentException("The parameter 'key' contains invalid characters.");
			}
			string result = string.Empty;
			string path = Path.Combine(this.stateFileLocation, key);
			try
			{
				result = File.ReadAllText(path);
			}
			catch (Exception ex)
			{
				throw new PersistenceFrameworkStorageException("Unable to read storage content. " + ex.Message, ex.InnerException);
			}
			return result;
		}

		// Token: 0x04002063 RID: 8291
		private readonly string stateFileLocation;
	}
}

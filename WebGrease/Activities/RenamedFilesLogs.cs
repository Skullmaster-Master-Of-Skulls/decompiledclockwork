using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;

namespace WebGrease.Activities
{
	// Token: 0x02000040 RID: 64
	internal sealed class RenamedFilesLogs
	{
		// Token: 0x060003DC RID: 988 RVA: 0x0000C578 File Offset: 0x0000A778
		public RenamedFilesLogs(ICollection<string> logFiles)
		{
			if (logFiles == null || logFiles.Count == 0)
			{
				return;
			}
			foreach (string text in logFiles)
			{
				RenamedFilesLog renamedFilesLog = new RenamedFilesLog(text);
				if (File.Exists(text))
				{
					renamedFilesLog.RenamedFiles.ForEach(delegate(RenamedFile renamedFile)
					{
						renamedFile.InputNames.ForEach(delegate(string inputName)
						{
							this.dictionary.Add(RenamedFilesLogs.NormalizeSlash(inputName).ToLowerInvariant(), renamedFile.OutputName);
						});
					});
					renamedFilesLog.RenamedFiles.ForEach(delegate(RenamedFile renamedFile)
					{
						this.m_reverseDictionary.Add(renamedFile.OutputName, (from inputName in renamedFile.InputNames
						select inputName.ToLowerInvariant()).ToList<string>());
					});
				}
			}
		}

		// Token: 0x060003DD RID: 989 RVA: 0x0000C634 File Offset: 0x0000A834
		public static RenamedFilesLogs LoadHashedImagesLogs(string hashedImagesLogFile)
		{
			if (!string.IsNullOrWhiteSpace(hashedImagesLogFile) && File.Exists(hashedImagesLogFile))
			{
				try
				{
					return new RenamedFilesLogs(new string[]
					{
						hashedImagesLogFile
					});
				}
				catch (Exception inner)
				{
					throw new BuildWorkflowException(string.Format(CultureInfo.CurrentUICulture, "Unable to parse the log with the hashed image replacement names from '{0}'", new object[]
					{
						hashedImagesLogFile
					}), inner);
				}
			}
			return null;
		}

		// Token: 0x060003DE RID: 990 RVA: 0x0000C69C File Offset: 0x0000A89C
		public static string NormalizeSlash(string path)
		{
			if (path != null && path.StartsWith(Path.AltDirectorySeparatorChar.ToString(), StringComparison.OrdinalIgnoreCase))
			{
				return path.Remove(0, 1);
			}
			return path;
		}

		// Token: 0x060003DF RID: 991 RVA: 0x0000C6CC File Offset: 0x0000A8CC
		public bool HasItems()
		{
			return this.dictionary.Count != 0;
		}

		// Token: 0x060003E0 RID: 992 RVA: 0x0000C6E0 File Offset: 0x0000A8E0
		public string FindHashPath(string inputName)
		{
			if (string.IsNullOrWhiteSpace(inputName))
			{
				return null;
			}
			inputName = RenamedFilesLogs.NormalizeSlash(inputName).ToLowerInvariant();
			string result;
			if (!this.dictionary.TryGetValue(inputName, out result))
			{
				return null;
			}
			return result;
		}

		// Token: 0x060003E1 RID: 993 RVA: 0x0000C718 File Offset: 0x0000A918
		public bool AllInputFileNamesMatch(string hashedFileName, List<string> inputFileNames)
		{
			if (string.IsNullOrWhiteSpace(hashedFileName) || !this.m_reverseDictionary.ContainsKey(hashedFileName))
			{
				return false;
			}
			List<string> list = this.m_reverseDictionary[hashedFileName];
			if (list.Count != inputFileNames.Count)
			{
				return false;
			}
			foreach (string item in list)
			{
				if (!inputFileNames.Contains(item))
				{
					return false;
				}
				inputFileNames.Remove(item);
			}
			return true;
		}

		// Token: 0x040000DF RID: 223
		private readonly Dictionary<string, string> dictionary = new Dictionary<string, string>();

		// Token: 0x040000E0 RID: 224
		private readonly Dictionary<string, List<string>> m_reverseDictionary = new Dictionary<string, List<string>>();
	}
}

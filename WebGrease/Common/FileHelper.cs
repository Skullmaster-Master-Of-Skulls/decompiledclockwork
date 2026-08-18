using System;
using System.IO;

namespace WebGrease.Common
{
	// Token: 0x020000EC RID: 236
	internal static class FileHelper
	{
		// Token: 0x06000F57 RID: 3927 RVA: 0x00046D48 File Offset: 0x00044F48
		internal static void WriteFile(string path, string content)
		{
			string directoryName = Path.GetDirectoryName(path);
			if (!string.IsNullOrWhiteSpace(directoryName))
			{
				Directory.CreateDirectory(directoryName);
			}
			File.WriteAllText(path, content);
		}
	}
}

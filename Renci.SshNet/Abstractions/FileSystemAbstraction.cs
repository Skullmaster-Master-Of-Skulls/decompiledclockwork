using System;
using System.Collections.Generic;
using System.IO;

namespace Renci.SshNet.Abstractions
{
	// Token: 0x02000117 RID: 279
	internal class FileSystemAbstraction
	{
		// Token: 0x06000C10 RID: 3088 RVA: 0x000271C1 File Offset: 0x000253C1
		public static IEnumerable<FileInfo> EnumerateFiles(DirectoryInfo directoryInfo, string searchPattern)
		{
			if (directoryInfo == null)
			{
				throw new ArgumentNullException("directoryInfo");
			}
			return directoryInfo.GetFiles(searchPattern);
		}
	}
}

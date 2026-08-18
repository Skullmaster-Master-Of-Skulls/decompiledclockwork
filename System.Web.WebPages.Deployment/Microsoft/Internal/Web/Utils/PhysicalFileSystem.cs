using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Internal.Web.Utils
{
	// Token: 0x02000009 RID: 9
	internal sealed class PhysicalFileSystem : IFileSystem
	{
		// Token: 0x06000035 RID: 53 RVA: 0x00002D9F File Offset: 0x00000F9F
		public bool FileExists(string path)
		{
			return File.Exists(path);
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002DA7 File Offset: 0x00000FA7
		public Stream ReadFile(string path)
		{
			return File.OpenRead(path);
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002DB0 File Offset: 0x00000FB0
		public Stream OpenFile(string path)
		{
			string directoryName = Path.GetDirectoryName(path);
			PhysicalFileSystem.EnsureDirectory(directoryName);
			return File.OpenWrite(path);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002DD0 File Offset: 0x00000FD0
		public IEnumerable<string> EnumerateFiles(string path)
		{
			return Directory.EnumerateFiles(path);
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002DD8 File Offset: 0x00000FD8
		private static void EnsureDirectory(string path)
		{
			if (!Directory.Exists(path))
			{
				Directory.CreateDirectory(path);
			}
		}
	}
}

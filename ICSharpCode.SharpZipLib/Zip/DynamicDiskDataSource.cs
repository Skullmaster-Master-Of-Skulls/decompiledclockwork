using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200002A RID: 42
	public class DynamicDiskDataSource : IDynamicDataSource
	{
		// Token: 0x06000198 RID: 408 RVA: 0x000094D4 File Offset: 0x000084D4
		public Stream GetSource(ZipEntry entry, string name)
		{
			Stream result = null;
			if (name != null)
			{
				result = File.Open(name, FileMode.Open, FileAccess.Read, FileShare.Read);
			}
			return result;
		}
	}
}

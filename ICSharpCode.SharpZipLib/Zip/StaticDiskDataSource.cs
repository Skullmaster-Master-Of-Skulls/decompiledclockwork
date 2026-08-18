using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000029 RID: 41
	public class StaticDiskDataSource : IStaticDataSource
	{
		// Token: 0x06000195 RID: 405 RVA: 0x000094AB File Offset: 0x000084AB
		public StaticDiskDataSource(string fileName)
		{
			this.fileName_ = fileName;
		}

		// Token: 0x06000196 RID: 406 RVA: 0x000094BA File Offset: 0x000084BA
		public Stream GetSource()
		{
			return File.Open(this.fileName_, FileMode.Open, FileAccess.Read, FileShare.Read);
		}

		// Token: 0x04000107 RID: 263
		private string fileName_;
	}
}

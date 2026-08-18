using System;
using System.IO;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x02000028 RID: 40
	public interface IDynamicDataSource
	{
		// Token: 0x06000194 RID: 404
		Stream GetSource(ZipEntry entry, string name);
	}
}

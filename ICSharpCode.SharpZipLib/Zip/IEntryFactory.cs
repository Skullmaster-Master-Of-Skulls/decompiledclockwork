using System;
using ICSharpCode.SharpZipLib.Core;

namespace ICSharpCode.SharpZipLib.Zip
{
	// Token: 0x0200007B RID: 123
	public interface IEntryFactory
	{
		// Token: 0x060004CE RID: 1230
		ZipEntry MakeFileEntry(string fileName);

		// Token: 0x060004CF RID: 1231
		ZipEntry MakeFileEntry(string fileName, bool useFileSystem);

		// Token: 0x060004D0 RID: 1232
		ZipEntry MakeFileEntry(string fileName, string entryName, bool useFileSystem);

		// Token: 0x060004D1 RID: 1233
		ZipEntry MakeDirectoryEntry(string directoryName);

		// Token: 0x060004D2 RID: 1234
		ZipEntry MakeDirectoryEntry(string directoryName, bool useFileSystem);

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x060004D3 RID: 1235
		// (set) Token: 0x060004D4 RID: 1236
		INameTransform NameTransform { get; set; }
	}
}

using System;
using System.Runtime.InteropServices;

namespace Ionic.Zip
{
	// Token: 0x02000002 RID: 2
	[Guid("ebc25cf6-9120-4283-b972-0e5520d0000F")]
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	public class ComHelper
	{
		// Token: 0x06000001 RID: 1 RVA: 0x000020D0 File Offset: 0x000002D0
		public bool IsZipFile(string filename)
		{
			return ZipFile.IsZipFile(filename);
		}

		// Token: 0x06000002 RID: 2 RVA: 0x000020D8 File Offset: 0x000002D8
		public bool IsZipFileWithExtract(string filename)
		{
			return ZipFile.IsZipFile(filename, true);
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020E1 File Offset: 0x000002E1
		public bool CheckZip(string filename)
		{
			return ZipFile.CheckZip(filename);
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020E9 File Offset: 0x000002E9
		public bool CheckZipPassword(string filename, string password)
		{
			return ZipFile.CheckZipPassword(filename, password);
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020F2 File Offset: 0x000002F2
		public void FixZipDirectory(string filename)
		{
			ZipFile.FixZipDirectory(filename);
		}

		// Token: 0x06000006 RID: 6 RVA: 0x000020FA File Offset: 0x000002FA
		public string GetZipLibraryVersion()
		{
			return ZipFile.LibraryVersion.ToString();
		}
	}
}

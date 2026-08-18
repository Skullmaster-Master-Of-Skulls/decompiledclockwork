using System;
using System.Collections.Generic;
using System.IO;

namespace Microsoft.Internal.Web.Utils
{
	// Token: 0x02000008 RID: 8
	internal interface IFileSystem
	{
		// Token: 0x06000031 RID: 49
		bool FileExists(string path);

		// Token: 0x06000032 RID: 50
		Stream ReadFile(string path);

		// Token: 0x06000033 RID: 51
		Stream OpenFile(string path);

		// Token: 0x06000034 RID: 52
		IEnumerable<string> EnumerateFiles(string root);
	}
}

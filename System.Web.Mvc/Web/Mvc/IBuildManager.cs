using System;
using System.Collections;
using System.IO;

namespace System.Web.Mvc
{
	// Token: 0x020001D3 RID: 467
	internal interface IBuildManager
	{
		// Token: 0x06000DDB RID: 3547
		bool FileExists(string virtualPath);

		// Token: 0x06000DDC RID: 3548
		Type GetCompiledType(string virtualPath);

		// Token: 0x06000DDD RID: 3549
		ICollection GetReferencedAssemblies();

		// Token: 0x06000DDE RID: 3550
		Stream ReadCachedFile(string fileName);

		// Token: 0x06000DDF RID: 3551
		Stream CreateCachedFile(string fileName);
	}
}

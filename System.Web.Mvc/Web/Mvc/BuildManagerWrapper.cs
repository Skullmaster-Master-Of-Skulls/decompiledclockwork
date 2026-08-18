using System;
using System.Collections;
using System.IO;
using System.Web.Compilation;

namespace System.Web.Mvc
{
	// Token: 0x020001D4 RID: 468
	internal sealed class BuildManagerWrapper : IBuildManager
	{
		// Token: 0x06000DE0 RID: 3552 RVA: 0x00024C30 File Offset: 0x00022E30
		bool IBuildManager.FileExists(string virtualPath)
		{
			return BuildManager.GetObjectFactory(virtualPath, false) != null;
		}

		// Token: 0x06000DE1 RID: 3553 RVA: 0x00024C3F File Offset: 0x00022E3F
		Type IBuildManager.GetCompiledType(string virtualPath)
		{
			return BuildManager.GetCompiledType(virtualPath);
		}

		// Token: 0x06000DE2 RID: 3554 RVA: 0x00024C47 File Offset: 0x00022E47
		ICollection IBuildManager.GetReferencedAssemblies()
		{
			return BuildManager.GetReferencedAssemblies();
		}

		// Token: 0x06000DE3 RID: 3555 RVA: 0x00024C4E File Offset: 0x00022E4E
		Stream IBuildManager.ReadCachedFile(string fileName)
		{
			return BuildManager.ReadCachedFile(fileName);
		}

		// Token: 0x06000DE4 RID: 3556 RVA: 0x00024C56 File Offset: 0x00022E56
		Stream IBuildManager.CreateCachedFile(string fileName)
		{
			return BuildManager.CreateCachedFile(fileName);
		}
	}
}

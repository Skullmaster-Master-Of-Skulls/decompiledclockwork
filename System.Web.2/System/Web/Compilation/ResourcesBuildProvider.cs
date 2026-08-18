using System;
using System.IO;
using System.Resources;

namespace System.Web.Compilation
{
	// Token: 0x0200085C RID: 2140
	internal class ResourcesBuildProvider : BaseResourcesBuildProvider
	{
		// Token: 0x0600654C RID: 25932 RVA: 0x0016498F File Offset: 0x00162B8F
		protected override IResourceReader GetResourceReader(Stream inputStream)
		{
			return new ResourceReader(inputStream);
		}
	}
}

using System;
using System.IO;
using System.Resources;
using System.Web.Hosting;

namespace System.Web.Compilation
{
	// Token: 0x0200085D RID: 2141
	internal sealed class ResXBuildProvider : BaseResourcesBuildProvider
	{
		// Token: 0x0600654E RID: 25934 RVA: 0x001649A0 File Offset: 0x00162BA0
		protected override IResourceReader GetResourceReader(Stream inputStream)
		{
			ResXResourceReader resXResourceReader = new ResXResourceReader(inputStream);
			string path = HostingEnvironment.MapPath(base.VirtualPath);
			resXResourceReader.BasePath = Path.GetDirectoryName(path);
			return resXResourceReader;
		}
	}
}

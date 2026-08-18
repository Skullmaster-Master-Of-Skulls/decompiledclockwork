using System;
using System.Web.Hosting;

namespace System.Web.Compilation
{
	// Token: 0x02000801 RID: 2049
	internal class ApplicationBrowserCapabilitiesBuildProvider : BuildProvider
	{
		// Token: 0x060061CF RID: 25039 RVA: 0x00156290 File Offset: 0x00154490
		internal ApplicationBrowserCapabilitiesBuildProvider()
		{
			this._codeGenerator = new ApplicationBrowserCapabilitiesCodeGenerator(this);
		}

		// Token: 0x060061D0 RID: 25040 RVA: 0x001562A4 File Offset: 0x001544A4
		internal void AddFile(string virtualPath)
		{
			string filePath = HostingEnvironment.MapPathInternal(virtualPath);
			this._codeGenerator.AddFile(filePath);
		}

		// Token: 0x060061D1 RID: 25041 RVA: 0x001562C4 File Offset: 0x001544C4
		public override void GenerateCode(AssemblyBuilder assemblyBuilder)
		{
			this._codeGenerator.GenerateCode(assemblyBuilder);
		}

		// Token: 0x040032D9 RID: 13017
		private ApplicationBrowserCapabilitiesCodeGenerator _codeGenerator;
	}
}

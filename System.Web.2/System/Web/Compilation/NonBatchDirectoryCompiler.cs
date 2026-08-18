using System;
using System.Web.Configuration;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x0200084D RID: 2125
	internal class NonBatchDirectoryCompiler
	{
		// Token: 0x060064E3 RID: 25827 RVA: 0x001616B8 File Offset: 0x0015F8B8
		internal NonBatchDirectoryCompiler(VirtualDirectory vdir)
		{
			this._vdir = vdir;
			this._compConfig = MTConfigUtil.GetCompilationConfig(this._vdir.VirtualPath);
		}

		// Token: 0x060064E4 RID: 25828 RVA: 0x001616E0 File Offset: 0x0015F8E0
		internal void Process()
		{
			foreach (object obj in this._vdir.Files)
			{
				VirtualFile virtualFile = (VirtualFile)obj;
				string extension = UrlPath.GetExtension(virtualFile.VirtualPath);
				Type buildProviderTypeFromExtension = CompilationUtil.GetBuildProviderTypeFromExtension(this._compConfig, extension, BuildProviderAppliesTo.Web, false);
				if (!(buildProviderTypeFromExtension == null) && !(buildProviderTypeFromExtension == typeof(SourceFileBuildProvider)) && !(buildProviderTypeFromExtension == typeof(ResXBuildProvider)))
				{
					BuildManager.GetVPathBuildResult(virtualFile.VirtualPathObject);
				}
			}
		}

		// Token: 0x04003409 RID: 13321
		private CompilationSection _compConfig;

		// Token: 0x0400340A RID: 13322
		private VirtualDirectory _vdir;
	}
}

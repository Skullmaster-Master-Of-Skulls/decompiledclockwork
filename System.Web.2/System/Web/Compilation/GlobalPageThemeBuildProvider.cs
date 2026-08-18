using System;
using System.Collections;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000853 RID: 2131
	internal class GlobalPageThemeBuildProvider : PageThemeBuildProvider
	{
		// Token: 0x06006509 RID: 25865 RVA: 0x00162F4A File Offset: 0x0016114A
		internal GlobalPageThemeBuildProvider(VirtualPath virtualDirPath) : base(virtualDirPath)
		{
			this._virtualDirPath = virtualDirPath;
		}

		// Token: 0x17001C6E RID: 7278
		// (get) Token: 0x0600650A RID: 25866 RVA: 0x00162F5A File Offset: 0x0016115A
		internal override string AssemblyNamePrefix
		{
			get
			{
				return "App_GlobalTheme_";
			}
		}

		// Token: 0x17001C6F RID: 7279
		// (get) Token: 0x0600650B RID: 25867 RVA: 0x00162F64 File Offset: 0x00161164
		public override ICollection VirtualPathDependencies
		{
			get
			{
				ICollection virtualPathDependencies = base.VirtualPathDependencies;
				string fileName = this._virtualDirPath.FileName;
				CaseInsensitiveStringSet caseInsensitiveStringSet = new CaseInsensitiveStringSet();
				caseInsensitiveStringSet.AddCollection(virtualPathDependencies);
				string text = UrlPath.SimpleCombine(HttpRuntime.AppDomainAppVirtualPathString, "App_Themes");
				string text2 = text + "/" + fileName;
				if (HostingEnvironment.VirtualPathProvider.DirectoryExists(text2))
				{
					caseInsensitiveStringSet.Add(text2);
				}
				else
				{
					caseInsensitiveStringSet.Add(text);
				}
				return caseInsensitiveStringSet;
			}
		}

		// Token: 0x0400341C RID: 13340
		private VirtualPath _virtualDirPath;
	}
}

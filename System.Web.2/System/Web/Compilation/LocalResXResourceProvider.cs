using System;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Resources;
using System.Security.Permissions;

namespace System.Web.Compilation
{
	// Token: 0x02000849 RID: 2121
	internal class LocalResXResourceProvider : BaseResXResourceProvider
	{
		// Token: 0x060064B5 RID: 25781 RVA: 0x00160C29 File Offset: 0x0015EE29
		internal LocalResXResourceProvider(VirtualPath virtualPath)
		{
			this._virtualPath = virtualPath;
		}

		// Token: 0x060064B6 RID: 25782 RVA: 0x00160C38 File Offset: 0x0015EE38
		protected override ResourceManager CreateResourceManager()
		{
			Assembly localResourceAssembly = this.GetLocalResourceAssembly();
			if (localResourceAssembly != null)
			{
				string fileName = this._virtualPath.FileName;
				return new ResourceManager(fileName, localResourceAssembly)
				{
					IgnoreCase = true
				};
			}
			throw new InvalidOperationException(SR.GetString("ResourceExpresionBuilder_PageResourceNotFound"));
		}

		// Token: 0x17001C5E RID: 7262
		// (get) Token: 0x060064B7 RID: 25783 RVA: 0x00160C88 File Offset: 0x0015EE88
		public override IResourceReader ResourceReader
		{
			get
			{
				Assembly localResourceAssembly = this.GetLocalResourceAssembly();
				if (localResourceAssembly == null)
				{
					return null;
				}
				string text = this._virtualPath.FileName + ".resources";
				text = text.ToLower(CultureInfo.InvariantCulture);
				Stream manifestResourceStream = localResourceAssembly.GetManifestResourceStream(text);
				if (manifestResourceStream == null)
				{
					return null;
				}
				return new ResourceReader(manifestResourceStream);
			}
		}

		// Token: 0x060064B8 RID: 25784 RVA: 0x00160CDC File Offset: 0x0015EEDC
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private Assembly GetLocalResourceAssembly()
		{
			VirtualPath parent = this._virtualPath.Parent;
			string localResourcesAssemblyName = BuildManager.GetLocalResourcesAssemblyName(parent);
			BuildResult buildResultFromCache = BuildManager.GetBuildResultFromCache(localResourcesAssemblyName);
			if (buildResultFromCache != null)
			{
				return ((BuildResultCompiledAssembly)buildResultFromCache).ResultAssembly;
			}
			return null;
		}

		// Token: 0x040033F7 RID: 13303
		private VirtualPath _virtualPath;
	}
}

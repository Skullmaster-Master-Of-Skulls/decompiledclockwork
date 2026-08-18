using System;
using System.Reflection;
using System.Web.Util;

namespace System.Web.Compilation
{
	// Token: 0x02000817 RID: 2071
	internal class BuildResultResourceAssembly : BuildResultCompiledAssembly
	{
		// Token: 0x06006338 RID: 25400 RVA: 0x0015B9AF File Offset: 0x00159BAF
		internal BuildResultResourceAssembly()
		{
		}

		// Token: 0x06006339 RID: 25401 RVA: 0x0015BE20 File Offset: 0x0015A020
		internal BuildResultResourceAssembly(Assembly a) : base(a)
		{
		}

		// Token: 0x0600633A RID: 25402 RVA: 0x0015BE29 File Offset: 0x0015A029
		internal override BuildResultTypeCode GetCode()
		{
			return BuildResultTypeCode.BuildResultResourceAssembly;
		}

		// Token: 0x0600633B RID: 25403 RVA: 0x0015BE30 File Offset: 0x0015A030
		internal override string ComputeSourceDependenciesHashCode(VirtualPath virtualPath)
		{
			if (virtualPath == null)
			{
				virtualPath = base.VirtualPath;
			}
			HashCodeCombiner hashCodeCombiner = new HashCodeCombiner();
			hashCodeCombiner.AddResourcesDirectory(virtualPath.MapPathInternal());
			return hashCodeCombiner.CombinedHashString;
		}

		// Token: 0x17001C19 RID: 7193
		// (get) Token: 0x0600633C RID: 25404 RVA: 0x0015BE66 File Offset: 0x0015A066
		// (set) Token: 0x0600633D RID: 25405 RVA: 0x0015BE74 File Offset: 0x0015A074
		internal string ResourcesDependenciesHash
		{
			get
			{
				this.EnsureResourcesDependenciesHashComputed();
				return this._resourcesDependenciesHash;
			}
			set
			{
				this._resourcesDependenciesHash = value;
			}
		}

		// Token: 0x0600633E RID: 25406 RVA: 0x0015BE7D File Offset: 0x0015A07D
		private void EnsureResourcesDependenciesHashComputed()
		{
			if (this._resourcesDependenciesHash != null)
			{
				return;
			}
			this._resourcesDependenciesHash = HashCodeCombiner.GetDirectoryHash(base.VirtualPath);
		}

		// Token: 0x0600633F RID: 25407 RVA: 0x0015BE99 File Offset: 0x0015A099
		internal override void GetPreservedAttributes(PreservationFileReader pfr)
		{
			base.GetPreservedAttributes(pfr);
			this.ResourcesDependenciesHash = pfr.GetAttribute("resHash");
		}

		// Token: 0x06006340 RID: 25408 RVA: 0x0015BEB3 File Offset: 0x0015A0B3
		internal override void SetPreservedAttributes(PreservationFileWriter pfw)
		{
			base.SetPreservedAttributes(pfw);
			pfw.SetAttribute("resHash", this.ResourcesDependenciesHash);
		}

		// Token: 0x04003376 RID: 13174
		private string _resourcesDependenciesHash;
	}
}

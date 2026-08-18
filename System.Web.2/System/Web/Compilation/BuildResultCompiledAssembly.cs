using System;
using System.Reflection;

namespace System.Web.Compilation
{
	// Token: 0x02000815 RID: 2069
	internal class BuildResultCompiledAssembly : BuildResultCompiledAssemblyBase
	{
		// Token: 0x0600632A RID: 25386 RVA: 0x0015BC5C File Offset: 0x00159E5C
		internal BuildResultCompiledAssembly()
		{
		}

		// Token: 0x0600632B RID: 25387 RVA: 0x0015BC64 File Offset: 0x00159E64
		internal BuildResultCompiledAssembly(Assembly a)
		{
			this._assembly = a;
		}

		// Token: 0x0600632C RID: 25388 RVA: 0x000097B7 File Offset: 0x000079B7
		internal override BuildResultTypeCode GetCode()
		{
			return BuildResultTypeCode.BuildResultCompiledAssembly;
		}

		// Token: 0x17001C18 RID: 7192
		// (get) Token: 0x0600632D RID: 25389 RVA: 0x0015BC73 File Offset: 0x00159E73
		// (set) Token: 0x0600632E RID: 25390 RVA: 0x0015BC7B File Offset: 0x00159E7B
		internal override Assembly ResultAssembly
		{
			get
			{
				return this._assembly;
			}
			set
			{
				this._assembly = value;
			}
		}

		// Token: 0x0600632F RID: 25391 RVA: 0x0015BC84 File Offset: 0x00159E84
		internal override void GetPreservedAttributes(PreservationFileReader pfr)
		{
			base.GetPreservedAttributes(pfr);
			this.ResultAssembly = BuildResultCompiledAssemblyBase.GetPreservedAssembly(pfr);
		}

		// Token: 0x04003373 RID: 13171
		private Assembly _assembly;
	}
}

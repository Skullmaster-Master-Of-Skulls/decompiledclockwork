using System;
using System.Collections;

namespace System.Web.Compilation
{
	// Token: 0x02000802 RID: 2050
	public sealed class BuildDependencySet
	{
		// Token: 0x060061D2 RID: 25042 RVA: 0x001562D2 File Offset: 0x001544D2
		internal BuildDependencySet(BuildResult result)
		{
			this._result = result;
		}

		// Token: 0x17001BC5 RID: 7109
		// (get) Token: 0x060061D3 RID: 25043 RVA: 0x001562E1 File Offset: 0x001544E1
		public string HashCode
		{
			get
			{
				return this._result.VirtualPathDependenciesHash;
			}
		}

		// Token: 0x17001BC6 RID: 7110
		// (get) Token: 0x060061D4 RID: 25044 RVA: 0x001562EE File Offset: 0x001544EE
		public IEnumerable VirtualPaths
		{
			get
			{
				return this._result.VirtualPathDependencies;
			}
		}

		// Token: 0x040032DA RID: 13018
		private BuildResult _result;
	}
}

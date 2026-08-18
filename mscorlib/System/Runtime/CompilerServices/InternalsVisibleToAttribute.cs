using System;

namespace System.Runtime.CompilerServices
{
	// Token: 0x020005E9 RID: 1513
	[AttributeUsage(AttributeTargets.Assembly, AllowMultiple = true, Inherited = false)]
	public sealed class InternalsVisibleToAttribute : Attribute
	{
		// Token: 0x060037EC RID: 14316 RVA: 0x000BBCA9 File Offset: 0x000BACA9
		public InternalsVisibleToAttribute(string assemblyName)
		{
			this._assemblyName = assemblyName;
		}

		// Token: 0x1700096A RID: 2410
		// (get) Token: 0x060037ED RID: 14317 RVA: 0x000BBCBF File Offset: 0x000BACBF
		public string AssemblyName
		{
			get
			{
				return this._assemblyName;
			}
		}

		// Token: 0x1700096B RID: 2411
		// (get) Token: 0x060037EE RID: 14318 RVA: 0x000BBCC7 File Offset: 0x000BACC7
		// (set) Token: 0x060037EF RID: 14319 RVA: 0x000BBCCF File Offset: 0x000BACCF
		public bool AllInternalsVisible
		{
			get
			{
				return this._allInternalsVisible;
			}
			set
			{
				this._allInternalsVisible = value;
			}
		}

		// Token: 0x04001CEE RID: 7406
		private string _assemblyName;

		// Token: 0x04001CEF RID: 7407
		private bool _allInternalsVisible = true;
	}
}

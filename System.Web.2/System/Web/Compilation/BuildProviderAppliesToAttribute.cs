using System;

namespace System.Web.Compilation
{
	// Token: 0x0200080D RID: 2061
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = false)]
	public sealed class BuildProviderAppliesToAttribute : Attribute
	{
		// Token: 0x060062D7 RID: 25303 RVA: 0x0015A5E8 File Offset: 0x001587E8
		public BuildProviderAppliesToAttribute(BuildProviderAppliesTo appliesTo)
		{
			this._appliesTo = appliesTo;
		}

		// Token: 0x17001BFD RID: 7165
		// (get) Token: 0x060062D8 RID: 25304 RVA: 0x0015A5F7 File Offset: 0x001587F7
		public BuildProviderAppliesTo AppliesTo
		{
			get
			{
				return this._appliesTo;
			}
		}

		// Token: 0x04003347 RID: 13127
		private BuildProviderAppliesTo _appliesTo;
	}
}

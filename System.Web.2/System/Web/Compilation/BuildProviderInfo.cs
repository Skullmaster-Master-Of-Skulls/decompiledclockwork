using System;

namespace System.Web.Compilation
{
	// Token: 0x0200080B RID: 2059
	internal abstract class BuildProviderInfo
	{
		// Token: 0x17001BFB RID: 7163
		// (get) Token: 0x060062D4 RID: 25300
		internal abstract Type Type { get; }

		// Token: 0x17001BFC RID: 7164
		// (get) Token: 0x060062D5 RID: 25301 RVA: 0x0015A58C File Offset: 0x0015878C
		internal BuildProviderAppliesTo AppliesTo
		{
			get
			{
				if (this._appliesTo != (BuildProviderAppliesTo)0)
				{
					return this._appliesTo;
				}
				object[] customAttributes = this.Type.GetCustomAttributes(typeof(BuildProviderAppliesToAttribute), true);
				if (customAttributes != null && customAttributes.Length != 0)
				{
					this._appliesTo = ((BuildProviderAppliesToAttribute)customAttributes[0]).AppliesTo;
				}
				else
				{
					this._appliesTo = BuildProviderAppliesTo.All;
				}
				return this._appliesTo;
			}
		}

		// Token: 0x04003341 RID: 13121
		private BuildProviderAppliesTo _appliesTo;
	}
}

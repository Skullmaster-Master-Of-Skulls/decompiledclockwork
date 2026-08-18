using System;

// Token: 0x02000112 RID: 274
[AttributeUsage(AttributeTargets.Module, AllowMultiple = true)]
internal sealed class BidMetaTextAttribute : Attribute
{
	// Token: 0x0600114E RID: 4430 RVA: 0x002330B8 File Offset: 0x002324B8
	internal BidMetaTextAttribute(string str)
	{
		this._metaText = str;
	}

	// Token: 0x17000243 RID: 579
	// (get) Token: 0x0600114F RID: 4431 RVA: 0x002330D8 File Offset: 0x002324D8
	internal string MetaText
	{
		get
		{
			return this._metaText;
		}
	}

	// Token: 0x04000B69 RID: 2921
	private string _metaText;
}

using System;

// Token: 0x02000030 RID: 48
[AttributeUsage(AttributeTargets.Module, AllowMultiple = true)]
internal sealed class BidMetaTextAttribute : Attribute
{
	// Token: 0x06000127 RID: 295 RVA: 0x00038588 File Offset: 0x00037988
	internal BidMetaTextAttribute(string str)
	{
		this._metaText = str;
	}

	// Token: 0x1700000C RID: 12
	// (get) Token: 0x06000128 RID: 296 RVA: 0x000385A4 File Offset: 0x000379A4
	internal string MetaText
	{
		get
		{
			return this._metaText;
		}
	}

	// Token: 0x040000C0 RID: 192
	private string _metaText;
}

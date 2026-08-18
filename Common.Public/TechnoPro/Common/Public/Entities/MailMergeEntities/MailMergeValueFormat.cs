using System;

namespace TechnoPro.Common.Public.Entities.MailMergeEntities
{
	// Token: 0x020002C6 RID: 710
	public class MailMergeValueFormat : ICloneable<MailMergeValueFormat>, ICloneable
	{
		// Token: 0x06001595 RID: 5525 RVA: 0x0001AF44 File Offset: 0x00019144
		public MailMergeValueFormat()
		{
			this.CustomFormat = "";
			this.ValueFormatType = eValueFormatType.DefaultToStringFormat;
		}

		// Token: 0x170008F2 RID: 2290
		// (get) Token: 0x06001596 RID: 5526 RVA: 0x0001AF62 File Offset: 0x00019162
		// (set) Token: 0x06001597 RID: 5527 RVA: 0x0001AF6A File Offset: 0x0001916A
		public string CustomFormat { get; set; }

		// Token: 0x170008F3 RID: 2291
		// (get) Token: 0x06001598 RID: 5528 RVA: 0x0001AF73 File Offset: 0x00019173
		// (set) Token: 0x06001599 RID: 5529 RVA: 0x0001AF7B File Offset: 0x0001917B
		public eValueFormatType ValueFormatType { get; set; }

		// Token: 0x0600159A RID: 5530 RVA: 0x0001AF84 File Offset: 0x00019184
		public MailMergeValueFormat Clone()
		{
			return new MailMergeValueFormat(this);
		}

		// Token: 0x0600159B RID: 5531 RVA: 0x0001AF9C File Offset: 0x0001919C
		object ICloneable.Clone()
		{
			return this.Clone();
		}

		// Token: 0x0600159C RID: 5532 RVA: 0x0001AFB4 File Offset: 0x000191B4
		public MailMergeValueFormat(MailMergeValueFormat item)
		{
			this.CustomFormat = "";
			this.ValueFormatType = eValueFormatType.DefaultToStringFormat;
			bool flag = item == null;
			if (!flag)
			{
				this.CustomFormat = item.CustomFormat;
				this.ValueFormatType = item.ValueFormatType;
			}
		}

		// Token: 0x040011E0 RID: 4576
		public static MailMergeValueFormat DefaultMailMergeValueFormat = new MailMergeValueFormat
		{
			ValueFormatType = eValueFormatType.DefaultToStringFormat,
			CustomFormat = ""
		};
	}
}

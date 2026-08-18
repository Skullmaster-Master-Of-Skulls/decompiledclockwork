using System;

namespace TechnoPro.Common.UI.Web.Entity.WebLogin
{
	// Token: 0x0200001F RID: 31
	public class WebPageTargetAudienceAttribute : Attribute
	{
		// Token: 0x0600007C RID: 124 RVA: 0x00002050 File Offset: 0x00000250
		public WebPageTargetAudienceAttribute()
		{
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00002814 File Offset: 0x00000A14
		public WebPageTargetAudienceAttribute(string title, eWebPageTargetAudience fallbackAudienceToUse)
		{
			this.Title = title;
			this.FallbackAudienceToUse = fallbackAudienceToUse;
		}

		// Token: 0x1700002B RID: 43
		// (get) Token: 0x0600007E RID: 126 RVA: 0x0000282E File Offset: 0x00000A2E
		// (set) Token: 0x0600007F RID: 127 RVA: 0x00002836 File Offset: 0x00000A36
		public eWebPageTargetAudience FallbackAudienceToUse { get; set; }

		// Token: 0x1700002C RID: 44
		// (get) Token: 0x06000080 RID: 128 RVA: 0x0000283F File Offset: 0x00000A3F
		// (set) Token: 0x06000081 RID: 129 RVA: 0x00002847 File Offset: 0x00000A47
		public string Title { get; set; }
	}
}

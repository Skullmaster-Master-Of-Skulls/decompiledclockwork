using System;

namespace TechnoPro.Common.UI.Web.Entity
{
	// Token: 0x02000006 RID: 6
	[Serializable]
	public class ClockWorkWebPageAttribute : Attribute
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public ClockWorkWebPageAttribute()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205A File Offset: 0x0000025A
		public ClockWorkWebPageAttribute(eClockWorkWebPageModule module, string title, string navigatePage, bool isDefault = false)
		{
			this.Module = module;
			this.Title = title;
			this.NavigatePage = navigatePage;
			this.IsDefault = isDefault;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x00002085 File Offset: 0x00000285
		// (set) Token: 0x06000004 RID: 4 RVA: 0x0000208D File Offset: 0x0000028D
		public eClockWorkWebPageModule Module { get; set; }

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x06000005 RID: 5 RVA: 0x00002096 File Offset: 0x00000296
		// (set) Token: 0x06000006 RID: 6 RVA: 0x0000209E File Offset: 0x0000029E
		public string NavigatePage { get; set; }

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000007 RID: 7 RVA: 0x000020A7 File Offset: 0x000002A7
		// (set) Token: 0x06000008 RID: 8 RVA: 0x000020AF File Offset: 0x000002AF
		public string Title { get; set; }

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020B8 File Offset: 0x000002B8
		// (set) Token: 0x0600000A RID: 10 RVA: 0x000020C0 File Offset: 0x000002C0
		public eClockWorkWebPage EnumValue { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000B RID: 11 RVA: 0x000020C9 File Offset: 0x000002C9
		// (set) Token: 0x0600000C RID: 12 RVA: 0x000020D1 File Offset: 0x000002D1
		public bool IsDefault { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600000D RID: 13 RVA: 0x000020DA File Offset: 0x000002DA
		// (set) Token: 0x0600000E RID: 14 RVA: 0x000020E2 File Offset: 0x000002E2
		public bool IsSubmitCommentPage { get; set; }

		// Token: 0x0600000F RID: 15 RVA: 0x000020EC File Offset: 0x000002EC
		public static ClockWorkWebPageAttribute GetAttribute(eClockWorkWebPage clockWorkWebPage)
		{
			ClockWorkWebPageAttribute attribute = ClockWorkWebPageModuleAttribute.GetAttribute<ClockWorkWebPageAttribute>(clockWorkWebPage);
			attribute.EnumValue = clockWorkWebPage;
			return attribute;
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000010 RID: 16 RVA: 0x00002113 File Offset: 0x00000313
		// (set) Token: 0x06000011 RID: 17 RVA: 0x0000211B File Offset: 0x0000031B
		public bool IsHidden { get; set; }
	}
}

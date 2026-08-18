using System;
using System.ComponentModel;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001813 RID: 6163
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class ComboBoxStrings : LocalizationStrings
	{
		// Token: 0x0600F005 RID: 61445 RVA: 0x0036A1BB File Offset: 0x003683BB
		internal ComboBoxStrings(LocalizationProvider provider) : base(provider)
		{
		}

		// Token: 0x1700489A RID: 18586
		// (get) Token: 0x0600F006 RID: 61446 RVA: 0x0036A1C4 File Offset: 0x003683C4
		// (set) Token: 0x0600F007 RID: 61447 RVA: 0x0036A1D1 File Offset: 0x003683D1
		[Localizable(true)]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		[DefaultValue("No matches")]
		public string NoMatches
		{
			get
			{
				return this.GetString("NoMatches");
			}
			set
			{
				this.SetString("NoMatches", value);
			}
		}

		// Token: 0x1700489B RID: 18587
		// (get) Token: 0x0600F008 RID: 61448 RVA: 0x0036A1DF File Offset: 0x003683DF
		// (set) Token: 0x0600F009 RID: 61449 RVA: 0x0036A1EC File Offset: 0x003683EC
		[Localizable(true)]
		[DefaultValue("Items <b>1</b>-<b>{0}</b> out of <b>{1}</b>")]
		[ScriptIgnore]
		[NotifyParentProperty(true)]
		public string ShowMoreFormatString
		{
			get
			{
				return this.GetString("ShowMoreFormatString");
			}
			set
			{
				this.SetString("ShowMoreFormatString", value);
			}
		}

		// Token: 0x1700489C RID: 18588
		// (get) Token: 0x0600F00A RID: 61450 RVA: 0x0036A1FA File Offset: 0x003683FA
		// (set) Token: 0x0600F00B RID: 61451 RVA: 0x0036A207 File Offset: 0x00368407
		[Localizable(true)]
		[DefaultValue("All items checked")]
		[NotifyParentProperty(true)]
		public string AllItemsCheckedString
		{
			get
			{
				return this.GetString("AllItemsCheckedString");
			}
			set
			{
				this.SetString("AllItemsCheckedString", value);
			}
		}

		// Token: 0x1700489D RID: 18589
		// (get) Token: 0x0600F00C RID: 61452 RVA: 0x0036A215 File Offset: 0x00368415
		// (set) Token: 0x0600F00D RID: 61453 RVA: 0x0036A222 File Offset: 0x00368422
		[DefaultValue("items checked")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public string ItemsCheckedString
		{
			get
			{
				return this.GetString("ItemsCheckedString");
			}
			set
			{
				this.SetString("ItemsCheckedString", value);
			}
		}

		// Token: 0x1700489E RID: 18590
		// (get) Token: 0x0600F00E RID: 61454 RVA: 0x0036A230 File Offset: 0x00368430
		// (set) Token: 0x0600F00F RID: 61455 RVA: 0x0036A23D File Offset: 0x0036843D
		[Localizable(true)]
		[DefaultValue("Check All")]
		[NotifyParentProperty(true)]
		public string CheckAllString
		{
			get
			{
				return this.GetString("CheckAllString");
			}
			set
			{
				this.SetString("CheckAllString", value);
			}
		}
	}
}

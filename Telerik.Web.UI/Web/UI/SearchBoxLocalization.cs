using System;
using System.ComponentModel;

namespace Telerik.Web.UI
{
	// Token: 0x0200086C RID: 2156
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class SearchBoxLocalization : LocalizationStrings
	{
		// Token: 0x06004F26 RID: 20262 RVA: 0x000F81AD File Offset: 0x000F63AD
		internal SearchBoxLocalization(LocalizationProvider provider) : base(provider)
		{
		}

		// Token: 0x140000C7 RID: 199
		// (add) Token: 0x06004F27 RID: 20263 RVA: 0x000F81B8 File Offset: 0x000F63B8
		// (remove) Token: 0x06004F28 RID: 20264 RVA: 0x000F81F0 File Offset: 0x000F63F0
		internal event PropertyChangedEventHandler PropertyChanged;

		// Token: 0x170019DA RID: 6618
		// (get) Token: 0x06004F29 RID: 20265 RVA: 0x000F8225 File Offset: 0x000F6425
		// (set) Token: 0x06004F2A RID: 20266 RVA: 0x000F8232 File Offset: 0x000F6432
		[NotifyParentProperty(true)]
		[Localizable(true)]
		[DefaultValue("Show All Results")]
		public string ShowAllResults
		{
			get
			{
				return this.GetString("ShowAllResults");
			}
			set
			{
				this.SetString("ShowAllResults", value);
			}
		}

		// Token: 0x170019DB RID: 6619
		// (get) Token: 0x06004F2B RID: 20267 RVA: 0x000F8240 File Offset: 0x000F6440
		// (set) Token: 0x06004F2C RID: 20268 RVA: 0x000F824D File Offset: 0x000F644D
		[NotifyParentProperty(true)]
		[DefaultValue("All")]
		[Localizable(true)]
		public string DefaultItemText
		{
			get
			{
				return this.GetString("DefaultItemText");
			}
			set
			{
				this.SetString("DefaultItemText", value);
				this.PropertyChanged(this, new PropertyChangedEventArgs("DefaultItemText"));
			}
		}

		// Token: 0x170019DC RID: 6620
		// (get) Token: 0x06004F2D RID: 20269 RVA: 0x000F8271 File Offset: 0x000F6471
		// (set) Token: 0x06004F2E RID: 20270 RVA: 0x000F827E File Offset: 0x000F647E
		[Localizable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue("Loading")]
		public string LoadingItemsMessage
		{
			get
			{
				return this.GetString("LoadingItemsMessage");
			}
			set
			{
				this.SetString("LoadingItemsMessage", value);
				this.PropertyChanged(this, new PropertyChangedEventArgs("LoadingItemsMessage"));
			}
		}
	}
}

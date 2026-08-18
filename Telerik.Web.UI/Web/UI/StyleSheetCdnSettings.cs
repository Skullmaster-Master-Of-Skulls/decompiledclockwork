using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001AAD RID: 6829
	public class StyleSheetCdnSettings : CdnSettings
	{
		// Token: 0x0601081A RID: 67610 RVA: 0x003B019A File Offset: 0x003AE39A
		internal StyleSheetCdnSettings() : base("StyleSheetCdnSettings", new StateBag())
		{
		}

		// Token: 0x0601081B RID: 67611 RVA: 0x003B01AC File Offset: 0x003AE3AC
		internal StyleSheetCdnSettings(string keyPrefix, StateBag ownerViewState) : base(keyPrefix, ownerViewState)
		{
		}

		// Token: 0x17005030 RID: 20528
		// (get) Token: 0x0601081C RID: 67612 RVA: 0x003B01B6 File Offset: 0x003AE3B6
		// (set) Token: 0x0601081D RID: 67613 RVA: 0x003B01BE File Offset: 0x003AE3BE
		[NotifyParentProperty(true)]
		[Description("Enables or disables the usage of the Telerik CDN for loading control skins.")]
		[Category("Behavior")]
		[DefaultValue(TelerikCdnMode.Auto)]
		public override TelerikCdnMode TelerikCdn
		{
			get
			{
				return base.TelerikCdn;
			}
			set
			{
				base.TelerikCdn = value;
			}
		}

		// Token: 0x17005031 RID: 20529
		// (get) Token: 0x0601081E RID: 67614 RVA: 0x003B01C7 File Offset: 0x003AE3C7
		// (set) Token: 0x0601081F RID: 67615 RVA: 0x003B01E7 File Offset: 0x003AE3E7
		[DefaultValue("http://aspnet-skins.telerikstatic.com")]
		[Description("Base URL of the CDN that hosts the control skins.")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		public override string BaseUrl
		{
			get
			{
				return (string)(base.ViewState["BaseUrl"] ?? "http://aspnet-skins.telerikstatic.com");
			}
			set
			{
				base.ViewState["BaseUrl"] = value;
			}
		}

		// Token: 0x17005032 RID: 20530
		// (get) Token: 0x06010820 RID: 67616 RVA: 0x003B01FA File Offset: 0x003AE3FA
		// (set) Token: 0x06010821 RID: 67617 RVA: 0x003B021A File Offset: 0x003AE41A
		[DefaultValue("https://d35islomi5rx1v.cloudfront.net")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[Description("Base secure (HTTPS) URL of the CDN that hosts the control skins.")]
		public override string BaseSecureUrl
		{
			get
			{
				return (string)(base.ViewState["BaseSecureUrl"] ?? "https://d35islomi5rx1v.cloudfront.net");
			}
			set
			{
				base.ViewState["BaseSecureUrl"] = value;
			}
		}

		// Token: 0x17005033 RID: 20531
		// (get) Token: 0x06010822 RID: 67618 RVA: 0x003B022D File Offset: 0x003AE42D
		protected override string TelerikCdnAppSettingsKey
		{
			get
			{
				return "Telerik.StyleSheetManager.TelerikCdn";
			}
		}

		// Token: 0x17005034 RID: 20532
		// (get) Token: 0x06010823 RID: 67619 RVA: 0x003B0234 File Offset: 0x003AE434
		protected override string BaseUrlAppSettingsKey
		{
			get
			{
				return "Telerik.StyleSheetManager.TelerikCdn.BaseUrl";
			}
		}

		// Token: 0x17005035 RID: 20533
		// (get) Token: 0x06010824 RID: 67620 RVA: 0x003B023B File Offset: 0x003AE43B
		protected override string BaseSecureUrlAppSettingsKey
		{
			get
			{
				return "Telerik.StyleSheetManager.TelerikCdn.BaseSecureUrl";
			}
		}

		// Token: 0x17005036 RID: 20534
		// (get) Token: 0x06010825 RID: 67621 RVA: 0x003B0242 File Offset: 0x003AE442
		protected override string TelerikCombinedResourceAppSettingsKey
		{
			get
			{
				return "Telerik.StyleSheetManager.TelerikCdn.CombinedResource";
			}
		}
	}
}

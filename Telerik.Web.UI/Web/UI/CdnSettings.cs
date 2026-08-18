using System;
using System.ComponentModel;
using System.Configuration;
using System.Reflection;
using System.Text.RegularExpressions;
using System.Web;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001AA9 RID: 6825
	public class CdnSettings : ObjectWithState, ICdnSettings
	{
		// Token: 0x060107EE RID: 67566 RVA: 0x003AF9D9 File Offset: 0x003ADBD9
		internal CdnSettings() : base("CdnSettings", new StateBag())
		{
		}

		// Token: 0x060107EF RID: 67567 RVA: 0x003AF9EB File Offset: 0x003ADBEB
		internal CdnSettings(string keyPrefix, StateBag ownerViewState) : base(keyPrefix, ownerViewState)
		{
		}

		// Token: 0x17005018 RID: 20504
		// (get) Token: 0x060107F0 RID: 67568 RVA: 0x003AF9F5 File Offset: 0x003ADBF5
		// (set) Token: 0x060107F1 RID: 67569 RVA: 0x003AFA16 File Offset: 0x003ADC16
		[Description("Enables or disables the usage of the Telerik CDN for loading control scripts.")]
		[DefaultValue(TelerikCdnMode.Auto)]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		public virtual TelerikCdnMode TelerikCdn
		{
			get
			{
				return (TelerikCdnMode)(base.ViewState["TelerikCdnMode"] ?? TelerikCdnMode.Auto);
			}
			set
			{
				base.ViewState["TelerikCdnMode"] = value;
			}
		}

		// Token: 0x17005019 RID: 20505
		// (get) Token: 0x060107F2 RID: 67570 RVA: 0x003AFA2E File Offset: 0x003ADC2E
		// (set) Token: 0x060107F3 RID: 67571 RVA: 0x003AFA4F File Offset: 0x003ADC4F
		[Category("Behavior")]
		[Description("Enables or disables the usage of combined resource files in CDN scenarios")]
		[DefaultValue(CombinedResourceMode.Disabled)]
		[NotifyParentProperty(true)]
		public virtual CombinedResourceMode CombinedResource
		{
			get
			{
				return (CombinedResourceMode)(base.ViewState["CombinedResource"] ?? CombinedResourceMode.Disabled);
			}
			set
			{
				base.ViewState["CombinedResource"] = value;
			}
		}

		// Token: 0x1700501A RID: 20506
		// (get) Token: 0x060107F4 RID: 67572 RVA: 0x003AFA67 File Offset: 0x003ADC67
		// (set) Token: 0x060107F5 RID: 67573 RVA: 0x003AFA87 File Offset: 0x003ADC87
		[Description("Base URL of the CDN that hosts the control scripts.")]
		[Category("Behavior")]
		[DefaultValue("http://aspnet-scripts.telerikstatic.com")]
		[NotifyParentProperty(true)]
		public virtual string BaseUrl
		{
			get
			{
				return (string)(base.ViewState["BaseUrl"] ?? "http://aspnet-scripts.telerikstatic.com");
			}
			set
			{
				base.ViewState["BaseUrl"] = value;
			}
		}

		// Token: 0x1700501B RID: 20507
		// (get) Token: 0x060107F6 RID: 67574 RVA: 0x003AFA9A File Offset: 0x003ADC9A
		// (set) Token: 0x060107F7 RID: 67575 RVA: 0x003AFABA File Offset: 0x003ADCBA
		[Description("Base secure (HTTPS) URL of the CDN that hosts the control scripts.")]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[DefaultValue("https://d2i2wahzwrm1n5.cloudfront.net")]
		public virtual string BaseSecureUrl
		{
			get
			{
				return (string)(base.ViewState["BaseSecureUrl"] ?? "https://d2i2wahzwrm1n5.cloudfront.net");
			}
			set
			{
				base.ViewState["BaseSecureUrl"] = value;
			}
		}

		// Token: 0x1700501C RID: 20508
		// (get) Token: 0x060107F8 RID: 67576 RVA: 0x003AFACD File Offset: 0x003ADCCD
		protected virtual string TelerikCdnAppSettingsKey
		{
			get
			{
				return "Telerik.ScriptManager.TelerikCdn";
			}
		}

		// Token: 0x1700501D RID: 20509
		// (get) Token: 0x060107F9 RID: 67577 RVA: 0x003AFAD4 File Offset: 0x003ADCD4
		protected virtual string TelerikCombinedResourceAppSettingsKey
		{
			get
			{
				return "Telerik.ScriptManager.TelerikCdn.CombinedScript";
			}
		}

		// Token: 0x1700501E RID: 20510
		// (get) Token: 0x060107FA RID: 67578 RVA: 0x003AFADB File Offset: 0x003ADCDB
		protected virtual string BaseUrlAppSettingsKey
		{
			get
			{
				return "Telerik.ScriptManager.TelerikCdn.BaseUrl";
			}
		}

		// Token: 0x1700501F RID: 20511
		// (get) Token: 0x060107FB RID: 67579 RVA: 0x003AFAE2 File Offset: 0x003ADCE2
		protected virtual string BaseSecureUrlAppSettingsKey
		{
			get
			{
				return "Telerik.ScriptManager.TelerikCdn.BaseSecureUrl";
			}
		}

		// Token: 0x17005020 RID: 20512
		// (get) Token: 0x060107FC RID: 67580 RVA: 0x003AFAEC File Offset: 0x003ADCEC
		internal TelerikCdnMode TelerikCdnResolved
		{
			get
			{
				TelerikCdnMode telerikCdnMode = TelerikCdnMode.Auto;
				if (this.IsTelerikCdnSet)
				{
					if (this.TelerikCdn != TelerikCdnMode.Auto)
					{
						return this.TelerikCdn;
					}
				}
				else
				{
					string value = ConfigurationManager.AppSettings[this.TelerikCdnAppSettingsKey];
					if (!string.IsNullOrEmpty(value))
					{
						telerikCdnMode = (TelerikCdnMode)Enum.Parse(typeof(TelerikCdnMode), value);
					}
				}
				if (telerikCdnMode == TelerikCdnMode.Auto)
				{
					HttpContext httpContext = HttpContext.Current;
					if (httpContext != null)
					{
						Page page = httpContext.Handler as Page;
						if (page != null)
						{
							ScriptManager current = ScriptManager.GetCurrent(page);
							if (current != null && current.EnableCdn)
							{
								telerikCdnMode = TelerikCdnMode.Enabled;
							}
						}
					}
				}
				return telerikCdnMode;
			}
		}

		// Token: 0x17005021 RID: 20513
		// (get) Token: 0x060107FD RID: 67581 RVA: 0x003AFB7E File Offset: 0x003ADD7E
		protected internal virtual CombinedResourceMode CombinedResourceResloved
		{
			get
			{
				return this.ResolveCombinedResource(() => this.CombinedResource, this.TelerikCombinedResourceAppSettingsKey);
			}
		}

		// Token: 0x060107FE RID: 67582 RVA: 0x003AFB98 File Offset: 0x003ADD98
		protected virtual CombinedResourceMode ResolveCombinedResource(Func<CombinedResourceMode> propertyValue, string webConfigKey)
		{
			CombinedResourceMode combinedResourceMode = CombinedResourceMode.Disabled;
			if (this.IsCombinedResourceSet)
			{
				combinedResourceMode = propertyValue();
			}
			else
			{
				string value = this.ReadFromConfig(webConfigKey);
				if (!string.IsNullOrEmpty(value))
				{
					combinedResourceMode = (CombinedResourceMode)Enum.Parse(typeof(CombinedResourceMode), value);
				}
			}
			if (combinedResourceMode != CombinedResourceMode.Enabled || this.TelerikCdnResolved != TelerikCdnMode.Enabled)
			{
				return CombinedResourceMode.Disabled;
			}
			return CombinedResourceMode.Enabled;
		}

		// Token: 0x17005022 RID: 20514
		// (get) Token: 0x060107FF RID: 67583 RVA: 0x003AFBF0 File Offset: 0x003ADDF0
		string ICdnSettings.BaseSecureUrl
		{
			get
			{
				if (this.IsBaseSecureUrlSet)
				{
					return this.BaseSecureUrl;
				}
				string text = ConfigurationManager.AppSettings[this.BaseSecureUrlAppSettingsKey];
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
				return this.BaseSecureUrl;
			}
		}

		// Token: 0x17005023 RID: 20515
		// (get) Token: 0x06010800 RID: 67584 RVA: 0x003AFC30 File Offset: 0x003ADE30
		string ICdnSettings.BaseUrl
		{
			get
			{
				if (this.IsBaseUrlSet)
				{
					return this.BaseUrl;
				}
				string text = ConfigurationManager.AppSettings[this.BaseUrlAppSettingsKey];
				if (!string.IsNullOrEmpty(text))
				{
					return text;
				}
				return this.BaseUrl;
			}
		}

		// Token: 0x17005024 RID: 20516
		// (get) Token: 0x06010801 RID: 67585 RVA: 0x003AFC6D File Offset: 0x003ADE6D
		string ICdnSettings.BasePath
		{
			get
			{
				return "ajax/" + this.VersionString;
			}
		}

		// Token: 0x17005025 RID: 20517
		// (get) Token: 0x06010802 RID: 67586 RVA: 0x003AFC7F File Offset: 0x003ADE7F
		string ICdnSettings.BaseCompressedPath
		{
			get
			{
				return "ajaxz/" + this.VersionString;
			}
		}

		// Token: 0x17005026 RID: 20518
		// (get) Token: 0x06010803 RID: 67587 RVA: 0x003AFC94 File Offset: 0x003ADE94
		private string VersionString
		{
			get
			{
				if (this._versionString == null)
				{
					string fullName = Assembly.GetExecutingAssembly().FullName;
					this._versionString = CdnSettings.versionStringRegex.Match(fullName).Groups["ShortVersion"].Value;
				}
				return this._versionString;
			}
		}

		// Token: 0x17005027 RID: 20519
		// (get) Token: 0x06010804 RID: 67588 RVA: 0x003AFCDF File Offset: 0x003ADEDF
		private bool IsTelerikCdnSet
		{
			get
			{
				return base.ViewState["TelerikCdnMode"] != null;
			}
		}

		// Token: 0x17005028 RID: 20520
		// (get) Token: 0x06010805 RID: 67589 RVA: 0x003AFCF7 File Offset: 0x003ADEF7
		private bool IsCombinedResourceSet
		{
			get
			{
				return base.ViewState["CombinedResource"] != null;
			}
		}

		// Token: 0x17005029 RID: 20521
		// (get) Token: 0x06010806 RID: 67590 RVA: 0x003AFD0F File Offset: 0x003ADF0F
		private bool IsBaseUrlSet
		{
			get
			{
				return base.ViewState["BaseUrl"] != null;
			}
		}

		// Token: 0x1700502A RID: 20522
		// (get) Token: 0x06010807 RID: 67591 RVA: 0x003AFD27 File Offset: 0x003ADF27
		private bool IsBaseSecureUrlSet
		{
			get
			{
				return base.ViewState["BaseSecureUrl"] != null;
			}
		}

		// Token: 0x06010808 RID: 67592 RVA: 0x003AFD3F File Offset: 0x003ADF3F
		protected virtual string ReadFromConfig(string key)
		{
			return ConfigurationManager.AppSettings[this.TelerikCombinedResourceAppSettingsKey];
		}

		// Token: 0x040049E3 RID: 18915
		private string _versionString;

		// Token: 0x040049E4 RID: 18916
		private static readonly Regex versionStringRegex = new Regex("Version=(?<ShortVersion>\\d*\\.\\d*\\.\\d*)");
	}
}

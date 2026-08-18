using System;
using System.Configuration;
using System.Globalization;
using System.Text;
using System.Web.Compilation;
using System.Web.Util;
using System.Xml;

namespace System.Web.Configuration
{
	// Token: 0x020006E8 RID: 1768
	public sealed class GlobalizationSection : ConfigurationSection
	{
		// Token: 0x060054E6 RID: 21734 RVA: 0x00128AF4 File Offset: 0x00126CF4
		static GlobalizationSection()
		{
			GlobalizationSection._properties = new ConfigurationPropertyCollection();
			GlobalizationSection._properties.Add(GlobalizationSection._propRequestEncoding);
			GlobalizationSection._properties.Add(GlobalizationSection._propResponseEncoding);
			GlobalizationSection._properties.Add(GlobalizationSection._propFileEncoding);
			GlobalizationSection._properties.Add(GlobalizationSection._propCulture);
			GlobalizationSection._properties.Add(GlobalizationSection._propUICulture);
			GlobalizationSection._properties.Add(GlobalizationSection._propEnableClientBasedCulture);
			GlobalizationSection._properties.Add(GlobalizationSection._propResponseHeaderEncoding);
			GlobalizationSection._properties.Add(GlobalizationSection._propResourceProviderFactoryType);
			GlobalizationSection._properties.Add(GlobalizationSection._propEnableBestFitResponseEncoding);
		}

		// Token: 0x1700183A RID: 6202
		// (get) Token: 0x060054E8 RID: 21736 RVA: 0x00128CBA File Offset: 0x00126EBA
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return GlobalizationSection._properties;
			}
		}

		// Token: 0x1700183B RID: 6203
		// (get) Token: 0x060054E9 RID: 21737 RVA: 0x00128CC1 File Offset: 0x00126EC1
		// (set) Token: 0x060054EA RID: 21738 RVA: 0x00128CDC File Offset: 0x00126EDC
		[ConfigurationProperty("requestEncoding", DefaultValue = "utf-8")]
		public Encoding RequestEncoding
		{
			get
			{
				if (this.requestEncodingCache == null)
				{
					this.requestEncodingCache = Encoding.UTF8;
				}
				return this.requestEncodingCache;
			}
			set
			{
				if (value != null)
				{
					base[GlobalizationSection._propRequestEncoding] = value.WebName;
					this.requestEncodingCache = value;
					return;
				}
				base[GlobalizationSection._propRequestEncoding] = value;
				this.requestEncodingCache = Encoding.UTF8;
			}
		}

		// Token: 0x1700183C RID: 6204
		// (get) Token: 0x060054EB RID: 21739 RVA: 0x00128D11 File Offset: 0x00126F11
		// (set) Token: 0x060054EC RID: 21740 RVA: 0x00128D2C File Offset: 0x00126F2C
		[ConfigurationProperty("responseEncoding", DefaultValue = "utf-8")]
		public Encoding ResponseEncoding
		{
			get
			{
				if (this.responseEncodingCache == null)
				{
					this.responseEncodingCache = Encoding.UTF8;
				}
				return this.responseEncodingCache;
			}
			set
			{
				if (value != null)
				{
					base[GlobalizationSection._propResponseEncoding] = value.WebName;
					this.responseEncodingCache = value;
					return;
				}
				base[GlobalizationSection._propResponseEncoding] = value;
				this.responseEncodingCache = Encoding.UTF8;
			}
		}

		// Token: 0x1700183D RID: 6205
		// (get) Token: 0x060054ED RID: 21741 RVA: 0x00128D61 File Offset: 0x00126F61
		// (set) Token: 0x060054EE RID: 21742 RVA: 0x00128D7C File Offset: 0x00126F7C
		[ConfigurationProperty("responseHeaderEncoding", DefaultValue = "utf-8")]
		public Encoding ResponseHeaderEncoding
		{
			get
			{
				if (this.responseHeaderEncodingCache == null)
				{
					this.responseHeaderEncodingCache = Encoding.UTF8;
				}
				return this.responseHeaderEncodingCache;
			}
			set
			{
				if (value != null)
				{
					base[GlobalizationSection._propResponseHeaderEncoding] = value.WebName;
					this.responseHeaderEncodingCache = value;
					return;
				}
				base[GlobalizationSection._propResponseHeaderEncoding] = value;
				this.responseHeaderEncodingCache = Encoding.UTF8;
			}
		}

		// Token: 0x1700183E RID: 6206
		// (get) Token: 0x060054EF RID: 21743 RVA: 0x00128DB1 File Offset: 0x00126FB1
		// (set) Token: 0x060054F0 RID: 21744 RVA: 0x00128DCC File Offset: 0x00126FCC
		[ConfigurationProperty("fileEncoding")]
		public Encoding FileEncoding
		{
			get
			{
				if (this.fileEncodingCache == null)
				{
					this.fileEncodingCache = Encoding.Default;
				}
				return this.fileEncodingCache;
			}
			set
			{
				if (value != null)
				{
					base[GlobalizationSection._propFileEncoding] = value.WebName;
					this.fileEncodingCache = value;
					return;
				}
				base[GlobalizationSection._propFileEncoding] = value;
				this.fileEncodingCache = Encoding.Default;
			}
		}

		// Token: 0x1700183F RID: 6207
		// (get) Token: 0x060054F1 RID: 21745 RVA: 0x00128E01 File Offset: 0x00127001
		// (set) Token: 0x060054F2 RID: 21746 RVA: 0x00128E27 File Offset: 0x00127027
		[ConfigurationProperty("culture", DefaultValue = "")]
		public string Culture
		{
			get
			{
				if (this.cultureCache == null)
				{
					this.cultureCache = (string)base[GlobalizationSection._propCulture];
				}
				return this.cultureCache;
			}
			set
			{
				base[GlobalizationSection._propCulture] = value;
				this.cultureCache = value;
			}
		}

		// Token: 0x17001840 RID: 6208
		// (get) Token: 0x060054F3 RID: 21747 RVA: 0x00128E3C File Offset: 0x0012703C
		// (set) Token: 0x060054F4 RID: 21748 RVA: 0x00128E62 File Offset: 0x00127062
		[ConfigurationProperty("uiCulture", DefaultValue = "")]
		public string UICulture
		{
			get
			{
				if (this.uiCultureCache == null)
				{
					this.uiCultureCache = (string)base[GlobalizationSection._propUICulture];
				}
				return this.uiCultureCache;
			}
			set
			{
				base[GlobalizationSection._propUICulture] = value;
				this.uiCultureCache = value;
			}
		}

		// Token: 0x17001841 RID: 6209
		// (get) Token: 0x060054F5 RID: 21749 RVA: 0x00128E77 File Offset: 0x00127077
		// (set) Token: 0x060054F6 RID: 21750 RVA: 0x00128E89 File Offset: 0x00127089
		[ConfigurationProperty("enableClientBasedCulture", DefaultValue = false)]
		public bool EnableClientBasedCulture
		{
			get
			{
				return (bool)base[GlobalizationSection._propEnableClientBasedCulture];
			}
			set
			{
				base[GlobalizationSection._propEnableClientBasedCulture] = value;
			}
		}

		// Token: 0x17001842 RID: 6210
		// (get) Token: 0x060054F7 RID: 21751 RVA: 0x00128E9C File Offset: 0x0012709C
		// (set) Token: 0x060054F8 RID: 21752 RVA: 0x00128EAE File Offset: 0x001270AE
		[ConfigurationProperty("resourceProviderFactoryType", DefaultValue = "")]
		public string ResourceProviderFactoryType
		{
			get
			{
				return (string)base[GlobalizationSection._propResourceProviderFactoryType];
			}
			set
			{
				base[GlobalizationSection._propResourceProviderFactoryType] = value;
			}
		}

		// Token: 0x17001843 RID: 6211
		// (get) Token: 0x060054F9 RID: 21753 RVA: 0x00128EBC File Offset: 0x001270BC
		// (set) Token: 0x060054FA RID: 21754 RVA: 0x00128ECE File Offset: 0x001270CE
		[ConfigurationProperty("enableBestFitResponseEncoding", DefaultValue = false)]
		public bool EnableBestFitResponseEncoding
		{
			get
			{
				return (bool)base[GlobalizationSection._propEnableBestFitResponseEncoding];
			}
			set
			{
				base[GlobalizationSection._propEnableBestFitResponseEncoding] = value;
			}
		}

		// Token: 0x17001844 RID: 6212
		// (get) Token: 0x060054FB RID: 21755 RVA: 0x00128EE4 File Offset: 0x001270E4
		internal Type ResourceProviderFactoryTypeInternal
		{
			get
			{
				if (this._resourceProviderFactoryType == null && !string.IsNullOrEmpty(this.ResourceProviderFactoryType))
				{
					lock (this)
					{
						if (this._resourceProviderFactoryType == null)
						{
							Type type = ConfigUtil.GetType(this.ResourceProviderFactoryType, "resourceProviderFactoryType", this);
							ConfigUtil.CheckBaseType(typeof(ResourceProviderFactory), type, "resourceProviderFactoryType", this);
							this._resourceProviderFactoryType = type;
						}
					}
				}
				return this._resourceProviderFactoryType;
			}
		}

		// Token: 0x060054FC RID: 21756 RVA: 0x00128F78 File Offset: 0x00127178
		private void CheckCulture(string configCulture)
		{
			if (StringUtil.EqualsIgnoreCase(configCulture, HttpApplication.AutoCulture))
			{
				return;
			}
			if (StringUtil.StringStartsWithIgnoreCase(configCulture, HttpApplication.AutoCulture))
			{
				CultureInfo cultureInfo = new CultureInfo(configCulture.Substring(5));
				return;
			}
			new CultureInfo(configCulture);
		}

		// Token: 0x060054FD RID: 21757 RVA: 0x00128FB5 File Offset: 0x001271B5
		protected override void PreSerialize(XmlWriter writer)
		{
			this.PostDeserialize();
		}

		// Token: 0x060054FE RID: 21758 RVA: 0x00128FC0 File Offset: 0x001271C0
		protected override void PostDeserialize()
		{
			ConfigurationPropertyCollection properties = this.Properties;
			ConfigurationProperty configurationProperty = null;
			int num = int.MaxValue;
			try
			{
				if (!string.IsNullOrEmpty((string)base[GlobalizationSection._propResponseEncoding]))
				{
					this.responseEncodingCache = Encoding.GetEncoding((string)base[GlobalizationSection._propResponseEncoding]);
				}
			}
			catch
			{
				configurationProperty = GlobalizationSection._propResponseEncoding;
				num = base.ElementInformation.Properties[configurationProperty.Name].LineNumber;
			}
			try
			{
				if (!string.IsNullOrEmpty((string)base[GlobalizationSection._propResponseHeaderEncoding]))
				{
					this.responseHeaderEncodingCache = Encoding.GetEncoding((string)base[GlobalizationSection._propResponseHeaderEncoding]);
				}
			}
			catch
			{
				if (num > base.ElementInformation.Properties[GlobalizationSection._propResponseHeaderEncoding.Name].LineNumber)
				{
					configurationProperty = GlobalizationSection._propResponseHeaderEncoding;
					num = base.ElementInformation.Properties[configurationProperty.Name].LineNumber;
				}
			}
			try
			{
				if (!string.IsNullOrEmpty((string)base[GlobalizationSection._propRequestEncoding]))
				{
					this.requestEncodingCache = Encoding.GetEncoding((string)base[GlobalizationSection._propRequestEncoding]);
				}
			}
			catch
			{
				if (num > base.ElementInformation.Properties[GlobalizationSection._propRequestEncoding.Name].LineNumber)
				{
					configurationProperty = GlobalizationSection._propRequestEncoding;
					num = base.ElementInformation.Properties[configurationProperty.Name].LineNumber;
				}
			}
			try
			{
				if (!string.IsNullOrEmpty((string)base[GlobalizationSection._propFileEncoding]))
				{
					this.fileEncodingCache = Encoding.GetEncoding((string)base[GlobalizationSection._propFileEncoding]);
				}
			}
			catch
			{
				if (num > base.ElementInformation.Properties[GlobalizationSection._propFileEncoding.Name].LineNumber)
				{
					configurationProperty = GlobalizationSection._propFileEncoding;
					num = base.ElementInformation.Properties[configurationProperty.Name].LineNumber;
				}
			}
			try
			{
				if (!string.IsNullOrEmpty((string)base[GlobalizationSection._propCulture]))
				{
					this.CheckCulture((string)base[GlobalizationSection._propCulture]);
				}
			}
			catch
			{
				if (num > base.ElementInformation.Properties[GlobalizationSection._propCulture.Name].LineNumber)
				{
					configurationProperty = GlobalizationSection._propCulture;
					num = base.ElementInformation.Properties[GlobalizationSection._propCulture.Name].LineNumber;
				}
			}
			try
			{
				if (!string.IsNullOrEmpty((string)base[GlobalizationSection._propUICulture]))
				{
					this.CheckCulture((string)base[GlobalizationSection._propUICulture]);
				}
			}
			catch
			{
				if (num > base.ElementInformation.Properties[GlobalizationSection._propUICulture.Name].LineNumber)
				{
					configurationProperty = GlobalizationSection._propUICulture;
					num = base.ElementInformation.Properties[GlobalizationSection._propUICulture.Name].LineNumber;
				}
			}
			if (configurationProperty != null)
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_value_for_globalization_attr", new object[]
				{
					configurationProperty.Name
				}), base.ElementInformation.Properties[configurationProperty.Name].Source, base.ElementInformation.Properties[configurationProperty.Name].LineNumber);
			}
		}

		// Token: 0x04002C84 RID: 11396
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002C85 RID: 11397
		private static readonly ConfigurationProperty _propRequestEncoding = new ConfigurationProperty("requestEncoding", typeof(string), Encoding.UTF8.WebName, ConfigurationPropertyOptions.None);

		// Token: 0x04002C86 RID: 11398
		private static readonly ConfigurationProperty _propResponseEncoding = new ConfigurationProperty("responseEncoding", typeof(string), Encoding.UTF8.WebName, ConfigurationPropertyOptions.None);

		// Token: 0x04002C87 RID: 11399
		private static readonly ConfigurationProperty _propFileEncoding = new ConfigurationProperty("fileEncoding", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04002C88 RID: 11400
		private static readonly ConfigurationProperty _propCulture = new ConfigurationProperty("culture", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04002C89 RID: 11401
		private static readonly ConfigurationProperty _propUICulture = new ConfigurationProperty("uiCulture", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04002C8A RID: 11402
		private static readonly ConfigurationProperty _propEnableClientBasedCulture = new ConfigurationProperty("enableClientBasedCulture", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002C8B RID: 11403
		private static readonly ConfigurationProperty _propResponseHeaderEncoding = new ConfigurationProperty("responseHeaderEncoding", typeof(string), Encoding.UTF8.WebName, ConfigurationPropertyOptions.None);

		// Token: 0x04002C8C RID: 11404
		private static readonly ConfigurationProperty _propResourceProviderFactoryType = new ConfigurationProperty("resourceProviderFactoryType", typeof(string), string.Empty, ConfigurationPropertyOptions.None);

		// Token: 0x04002C8D RID: 11405
		private static readonly ConfigurationProperty _propEnableBestFitResponseEncoding = new ConfigurationProperty("enableBestFitResponseEncoding", typeof(bool), false, ConfigurationPropertyOptions.None);

		// Token: 0x04002C8E RID: 11406
		private Encoding responseEncodingCache;

		// Token: 0x04002C8F RID: 11407
		private Encoding responseHeaderEncodingCache;

		// Token: 0x04002C90 RID: 11408
		private Encoding requestEncodingCache;

		// Token: 0x04002C91 RID: 11409
		private Encoding fileEncodingCache;

		// Token: 0x04002C92 RID: 11410
		private string cultureCache;

		// Token: 0x04002C93 RID: 11411
		private string uiCultureCache;

		// Token: 0x04002C94 RID: 11412
		private Type _resourceProviderFactoryType;
	}
}

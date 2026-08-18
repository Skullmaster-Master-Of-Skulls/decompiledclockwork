using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x02000658 RID: 1624
	public sealed class ProxyElement : ConfigurationElement
	{
		// Token: 0x06003239 RID: 12857 RVA: 0x000D5EA0 File Offset: 0x000D4EA0
		public ProxyElement()
		{
			this.properties.Add(this.autoDetect);
			this.properties.Add(this.scriptLocation);
			this.properties.Add(this.bypassonlocal);
			this.properties.Add(this.proxyaddress);
			this.properties.Add(this.usesystemdefault);
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x0600323A RID: 12858 RVA: 0x000D5FEC File Offset: 0x000D4FEC
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x0600323B RID: 12859 RVA: 0x000D5FF4 File Offset: 0x000D4FF4
		// (set) Token: 0x0600323C RID: 12860 RVA: 0x000D6007 File Offset: 0x000D5007
		[ConfigurationProperty("autoDetect", DefaultValue = ProxyElement.AutoDetectValues.Unspecified)]
		public ProxyElement.AutoDetectValues AutoDetect
		{
			get
			{
				return (ProxyElement.AutoDetectValues)base[this.autoDetect];
			}
			set
			{
				base[this.autoDetect] = value;
			}
		}

		// Token: 0x17000B9D RID: 2973
		// (get) Token: 0x0600323D RID: 12861 RVA: 0x000D601B File Offset: 0x000D501B
		// (set) Token: 0x0600323E RID: 12862 RVA: 0x000D602E File Offset: 0x000D502E
		[ConfigurationProperty("scriptLocation")]
		public Uri ScriptLocation
		{
			get
			{
				return (Uri)base[this.scriptLocation];
			}
			set
			{
				base[this.scriptLocation] = value;
			}
		}

		// Token: 0x17000B9E RID: 2974
		// (get) Token: 0x0600323F RID: 12863 RVA: 0x000D603D File Offset: 0x000D503D
		// (set) Token: 0x06003240 RID: 12864 RVA: 0x000D6050 File Offset: 0x000D5050
		[ConfigurationProperty("bypassonlocal", DefaultValue = ProxyElement.BypassOnLocalValues.Unspecified)]
		public ProxyElement.BypassOnLocalValues BypassOnLocal
		{
			get
			{
				return (ProxyElement.BypassOnLocalValues)base[this.bypassonlocal];
			}
			set
			{
				base[this.bypassonlocal] = value;
			}
		}

		// Token: 0x17000B9F RID: 2975
		// (get) Token: 0x06003241 RID: 12865 RVA: 0x000D6064 File Offset: 0x000D5064
		// (set) Token: 0x06003242 RID: 12866 RVA: 0x000D6077 File Offset: 0x000D5077
		[ConfigurationProperty("proxyaddress")]
		public Uri ProxyAddress
		{
			get
			{
				return (Uri)base[this.proxyaddress];
			}
			set
			{
				base[this.proxyaddress] = value;
			}
		}

		// Token: 0x17000BA0 RID: 2976
		// (get) Token: 0x06003243 RID: 12867 RVA: 0x000D6086 File Offset: 0x000D5086
		// (set) Token: 0x06003244 RID: 12868 RVA: 0x000D6099 File Offset: 0x000D5099
		[ConfigurationProperty("usesystemdefault", DefaultValue = ProxyElement.UseSystemDefaultValues.Unspecified)]
		public ProxyElement.UseSystemDefaultValues UseSystemDefault
		{
			get
			{
				return (ProxyElement.UseSystemDefaultValues)base[this.usesystemdefault];
			}
			set
			{
				base[this.usesystemdefault] = value;
			}
		}

		// Token: 0x04002F15 RID: 12053
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002F16 RID: 12054
		private readonly ConfigurationProperty autoDetect = new ConfigurationProperty("autoDetect", typeof(ProxyElement.AutoDetectValues), ProxyElement.AutoDetectValues.Unspecified, new EnumConverter(typeof(ProxyElement.AutoDetectValues)), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F17 RID: 12055
		private readonly ConfigurationProperty scriptLocation = new ConfigurationProperty("scriptLocation", typeof(Uri), null, new UriTypeConverter(UriKind.Absolute), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F18 RID: 12056
		private readonly ConfigurationProperty bypassonlocal = new ConfigurationProperty("bypassonlocal", typeof(ProxyElement.BypassOnLocalValues), ProxyElement.BypassOnLocalValues.Unspecified, new EnumConverter(typeof(ProxyElement.BypassOnLocalValues)), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F19 RID: 12057
		private readonly ConfigurationProperty proxyaddress = new ConfigurationProperty("proxyaddress", typeof(Uri), null, new UriTypeConverter(UriKind.Absolute), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F1A RID: 12058
		private readonly ConfigurationProperty usesystemdefault = new ConfigurationProperty("usesystemdefault", typeof(ProxyElement.UseSystemDefaultValues), ProxyElement.UseSystemDefaultValues.Unspecified, new EnumConverter(typeof(ProxyElement.UseSystemDefaultValues)), null, ConfigurationPropertyOptions.None);

		// Token: 0x02000659 RID: 1625
		public enum BypassOnLocalValues
		{
			// Token: 0x04002F1C RID: 12060
			Unspecified = -1,
			// Token: 0x04002F1D RID: 12061
			False,
			// Token: 0x04002F1E RID: 12062
			True
		}

		// Token: 0x0200065A RID: 1626
		public enum UseSystemDefaultValues
		{
			// Token: 0x04002F20 RID: 12064
			Unspecified = -1,
			// Token: 0x04002F21 RID: 12065
			False,
			// Token: 0x04002F22 RID: 12066
			True
		}

		// Token: 0x0200065B RID: 1627
		public enum AutoDetectValues
		{
			// Token: 0x04002F24 RID: 12068
			Unspecified = -1,
			// Token: 0x04002F25 RID: 12069
			False,
			// Token: 0x04002F26 RID: 12070
			True
		}
	}
}

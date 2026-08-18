using System;
using System.ComponentModel;
using System.Configuration;

namespace System.Net.Configuration
{
	// Token: 0x0200033C RID: 828
	public sealed class ProxyElement : ConfigurationElement
	{
		// Token: 0x06001D8F RID: 7567 RVA: 0x0008C1B4 File Offset: 0x0008A3B4
		public ProxyElement()
		{
			this.properties.Add(this.autoDetect);
			this.properties.Add(this.scriptLocation);
			this.properties.Add(this.bypassonlocal);
			this.properties.Add(this.proxyaddress);
			this.properties.Add(this.usesystemdefault);
		}

		// Token: 0x1700076E RID: 1902
		// (get) Token: 0x06001D90 RID: 7568 RVA: 0x0008C300 File Offset: 0x0008A500
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x1700076F RID: 1903
		// (get) Token: 0x06001D91 RID: 7569 RVA: 0x0008C308 File Offset: 0x0008A508
		// (set) Token: 0x06001D92 RID: 7570 RVA: 0x0008C31B File Offset: 0x0008A51B
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

		// Token: 0x17000770 RID: 1904
		// (get) Token: 0x06001D93 RID: 7571 RVA: 0x0008C32F File Offset: 0x0008A52F
		// (set) Token: 0x06001D94 RID: 7572 RVA: 0x0008C342 File Offset: 0x0008A542
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

		// Token: 0x17000771 RID: 1905
		// (get) Token: 0x06001D95 RID: 7573 RVA: 0x0008C351 File Offset: 0x0008A551
		// (set) Token: 0x06001D96 RID: 7574 RVA: 0x0008C364 File Offset: 0x0008A564
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

		// Token: 0x17000772 RID: 1906
		// (get) Token: 0x06001D97 RID: 7575 RVA: 0x0008C378 File Offset: 0x0008A578
		// (set) Token: 0x06001D98 RID: 7576 RVA: 0x0008C38B File Offset: 0x0008A58B
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

		// Token: 0x17000773 RID: 1907
		// (get) Token: 0x06001D99 RID: 7577 RVA: 0x0008C39A File Offset: 0x0008A59A
		// (set) Token: 0x06001D9A RID: 7578 RVA: 0x0008C3AD File Offset: 0x0008A5AD
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

		// Token: 0x04001C56 RID: 7254
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04001C57 RID: 7255
		private readonly ConfigurationProperty autoDetect = new ConfigurationProperty("autoDetect", typeof(ProxyElement.AutoDetectValues), ProxyElement.AutoDetectValues.Unspecified, new EnumConverter(typeof(ProxyElement.AutoDetectValues)), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C58 RID: 7256
		private readonly ConfigurationProperty scriptLocation = new ConfigurationProperty("scriptLocation", typeof(Uri), null, new UriTypeConverter(UriKind.Absolute), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C59 RID: 7257
		private readonly ConfigurationProperty bypassonlocal = new ConfigurationProperty("bypassonlocal", typeof(ProxyElement.BypassOnLocalValues), ProxyElement.BypassOnLocalValues.Unspecified, new EnumConverter(typeof(ProxyElement.BypassOnLocalValues)), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C5A RID: 7258
		private readonly ConfigurationProperty proxyaddress = new ConfigurationProperty("proxyaddress", typeof(Uri), null, new UriTypeConverter(UriKind.Absolute), null, ConfigurationPropertyOptions.None);

		// Token: 0x04001C5B RID: 7259
		private readonly ConfigurationProperty usesystemdefault = new ConfigurationProperty("usesystemdefault", typeof(ProxyElement.UseSystemDefaultValues), ProxyElement.UseSystemDefaultValues.Unspecified, new EnumConverter(typeof(ProxyElement.UseSystemDefaultValues)), null, ConfigurationPropertyOptions.None);

		// Token: 0x020007C2 RID: 1986
		public enum BypassOnLocalValues
		{
			// Token: 0x04003477 RID: 13431
			Unspecified = -1,
			// Token: 0x04003478 RID: 13432
			False,
			// Token: 0x04003479 RID: 13433
			True
		}

		// Token: 0x020007C3 RID: 1987
		public enum UseSystemDefaultValues
		{
			// Token: 0x0400347B RID: 13435
			Unspecified = -1,
			// Token: 0x0400347C RID: 13436
			False,
			// Token: 0x0400347D RID: 13437
			True
		}

		// Token: 0x020007C4 RID: 1988
		public enum AutoDetectValues
		{
			// Token: 0x0400347F RID: 13439
			Unspecified = -1,
			// Token: 0x04003480 RID: 13440
			False,
			// Token: 0x04003481 RID: 13441
			True
		}
	}
}

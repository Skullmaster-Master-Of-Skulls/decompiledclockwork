using System;

namespace System.Configuration
{
	// Token: 0x02000671 RID: 1649
	public sealed class UriSection : ConfigurationSection
	{
		// Token: 0x060032F7 RID: 13047 RVA: 0x000D7C64 File Offset: 0x000D6C64
		public UriSection()
		{
			this.properties.Add(this.idn);
			this.properties.Add(this.iriParsing);
		}

		// Token: 0x17000BFA RID: 3066
		// (get) Token: 0x060032F8 RID: 13048 RVA: 0x000D7CDC File Offset: 0x000D6CDC
		[ConfigurationProperty("idn")]
		public IdnElement Idn
		{
			get
			{
				return (IdnElement)base[this.idn];
			}
		}

		// Token: 0x17000BFB RID: 3067
		// (get) Token: 0x060032F9 RID: 13049 RVA: 0x000D7CEF File Offset: 0x000D6CEF
		[ConfigurationProperty("iriParsing")]
		public IriParsingElement IriParsing
		{
			get
			{
				return (IriParsingElement)base[this.iriParsing];
			}
		}

		// Token: 0x17000BFC RID: 3068
		// (get) Token: 0x060032FA RID: 13050 RVA: 0x000D7D02 File Offset: 0x000D6D02
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return this.properties;
			}
		}

		// Token: 0x04002F80 RID: 12160
		private ConfigurationPropertyCollection properties = new ConfigurationPropertyCollection();

		// Token: 0x04002F81 RID: 12161
		private readonly ConfigurationProperty idn = new ConfigurationProperty("idn", typeof(IdnElement), null, ConfigurationPropertyOptions.None);

		// Token: 0x04002F82 RID: 12162
		private readonly ConfigurationProperty iriParsing = new ConfigurationProperty("iriParsing", typeof(IriParsingElement), null, ConfigurationPropertyOptions.None);
	}
}

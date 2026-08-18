using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020006B9 RID: 1721
	public sealed class ClientTarget : ConfigurationElement
	{
		// Token: 0x06005331 RID: 21297 RVA: 0x00124A18 File Offset: 0x00122C18
		static ClientTarget()
		{
			ClientTarget._properties = new ConfigurationPropertyCollection();
			ClientTarget._properties.Add(ClientTarget._propAlias);
			ClientTarget._properties.Add(ClientTarget._propUserAgent);
		}

		// Token: 0x06005332 RID: 21298 RVA: 0x00117E9E File Offset: 0x0011609E
		internal ClientTarget()
		{
		}

		// Token: 0x06005333 RID: 21299 RVA: 0x00124A8F File Offset: 0x00122C8F
		public ClientTarget(string alias, string userAgent)
		{
			base[ClientTarget._propAlias] = alias;
			base[ClientTarget._propUserAgent] = userAgent;
		}

		// Token: 0x170017AE RID: 6062
		// (get) Token: 0x06005334 RID: 21300 RVA: 0x00124AAF File Offset: 0x00122CAF
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ClientTarget._properties;
			}
		}

		// Token: 0x170017AF RID: 6063
		// (get) Token: 0x06005335 RID: 21301 RVA: 0x00124AB6 File Offset: 0x00122CB6
		[ConfigurationProperty("alias", IsRequired = true, IsKey = true)]
		[StringValidator(MinLength = 1)]
		public string Alias
		{
			get
			{
				return (string)base[ClientTarget._propAlias];
			}
		}

		// Token: 0x170017B0 RID: 6064
		// (get) Token: 0x06005336 RID: 21302 RVA: 0x00124AC8 File Offset: 0x00122CC8
		[ConfigurationProperty("userAgent", IsRequired = true)]
		[StringValidator(MinLength = 1)]
		public string UserAgent
		{
			get
			{
				return (string)base[ClientTarget._propUserAgent];
			}
		}

		// Token: 0x04002BA7 RID: 11175
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002BA8 RID: 11176
		private static readonly ConfigurationProperty _propAlias = new ConfigurationProperty("alias", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired | ConfigurationPropertyOptions.IsKey);

		// Token: 0x04002BA9 RID: 11177
		private static readonly ConfigurationProperty _propUserAgent = new ConfigurationProperty("userAgent", typeof(string), null, null, StdValidatorsAndConverters.NonEmptyStringValidator, ConfigurationPropertyOptions.IsRequired);
	}
}

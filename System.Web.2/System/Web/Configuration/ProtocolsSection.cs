using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x0200073C RID: 1852
	public sealed class ProtocolsSection : ConfigurationSection
	{
		// Token: 0x06005940 RID: 22848 RVA: 0x00137680 File Offset: 0x00135880
		static ProtocolsSection()
		{
			ProtocolsSection._properties = new ConfigurationPropertyCollection();
			ProtocolsSection._properties.Add(ProtocolsSection._propProtocols);
		}

		// Token: 0x170019DC RID: 6620
		// (get) Token: 0x06005942 RID: 22850 RVA: 0x001376B2 File Offset: 0x001358B2
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return ProtocolsSection._properties;
			}
		}

		// Token: 0x170019DD RID: 6621
		// (get) Token: 0x06005943 RID: 22851 RVA: 0x001376B9 File Offset: 0x001358B9
		[ConfigurationProperty("protocols", IsRequired = true, IsDefaultCollection = true)]
		public ProtocolCollection Protocols
		{
			get
			{
				return (ProtocolCollection)base[ProtocolsSection._propProtocols];
			}
		}

		// Token: 0x04002F5A RID: 12122
		private static readonly ConfigurationPropertyCollection _properties;

		// Token: 0x04002F5B RID: 12123
		private static readonly ConfigurationProperty _propProtocols = new ConfigurationProperty(null, typeof(ProtocolCollection), null, ConfigurationPropertyOptions.IsDefaultCollection | ConfigurationPropertyOptions.IsRequired);
	}
}

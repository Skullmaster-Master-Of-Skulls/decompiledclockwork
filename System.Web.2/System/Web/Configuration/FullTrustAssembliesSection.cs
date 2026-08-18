using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x020006E3 RID: 1763
	public sealed class FullTrustAssembliesSection : ConfigurationSection
	{
		// Token: 0x060054C7 RID: 21703 RVA: 0x001288A2 File Offset: 0x00126AA2
		static FullTrustAssembliesSection()
		{
			FullTrustAssembliesSection._properties = new ConfigurationPropertyCollection();
			FullTrustAssembliesSection._properties.Add(FullTrustAssembliesSection._propFullTrustAssemblies);
		}

		// Token: 0x17001832 RID: 6194
		// (get) Token: 0x060054C9 RID: 21705 RVA: 0x001288D4 File Offset: 0x00126AD4
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return FullTrustAssembliesSection._properties;
			}
		}

		// Token: 0x17001833 RID: 6195
		// (get) Token: 0x060054CA RID: 21706 RVA: 0x001288DB File Offset: 0x00126ADB
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public FullTrustAssemblyCollection FullTrustAssemblies
		{
			get
			{
				return this.GetFullTrustAssembliesCollection();
			}
		}

		// Token: 0x060054CB RID: 21707 RVA: 0x001288E3 File Offset: 0x00126AE3
		private FullTrustAssemblyCollection GetFullTrustAssembliesCollection()
		{
			return (FullTrustAssemblyCollection)base[FullTrustAssembliesSection._propFullTrustAssemblies];
		}

		// Token: 0x04002C7D RID: 11389
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002C7E RID: 11390
		private static readonly ConfigurationProperty _propFullTrustAssemblies = new ConfigurationProperty(null, typeof(FullTrustAssemblyCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}

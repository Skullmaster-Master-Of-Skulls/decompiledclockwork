using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x02000725 RID: 1829
	public sealed class PartialTrustVisibleAssembliesSection : ConfigurationSection
	{
		// Token: 0x06005834 RID: 22580 RVA: 0x00134D8D File Offset: 0x00132F8D
		static PartialTrustVisibleAssembliesSection()
		{
			PartialTrustVisibleAssembliesSection._properties = new ConfigurationPropertyCollection();
			PartialTrustVisibleAssembliesSection._properties.Add(PartialTrustVisibleAssembliesSection._propPartialTrustVisibleAssemblies);
		}

		// Token: 0x17001985 RID: 6533
		// (get) Token: 0x06005836 RID: 22582 RVA: 0x00134DBF File Offset: 0x00132FBF
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				return PartialTrustVisibleAssembliesSection._properties;
			}
		}

		// Token: 0x17001986 RID: 6534
		// (get) Token: 0x06005837 RID: 22583 RVA: 0x00134DC6 File Offset: 0x00132FC6
		[ConfigurationProperty("", IsDefaultCollection = true)]
		public PartialTrustVisibleAssemblyCollection PartialTrustVisibleAssemblies
		{
			get
			{
				return this.GetPartialTrustVisibleAssembliesCollection();
			}
		}

		// Token: 0x06005838 RID: 22584 RVA: 0x00134DCE File Offset: 0x00132FCE
		private PartialTrustVisibleAssemblyCollection GetPartialTrustVisibleAssembliesCollection()
		{
			return (PartialTrustVisibleAssemblyCollection)base[PartialTrustVisibleAssembliesSection._propPartialTrustVisibleAssemblies];
		}

		// Token: 0x04002EE5 RID: 12005
		private static ConfigurationPropertyCollection _properties;

		// Token: 0x04002EE6 RID: 12006
		private static readonly ConfigurationProperty _propPartialTrustVisibleAssemblies = new ConfigurationProperty(null, typeof(PartialTrustVisibleAssemblyCollection), null, ConfigurationPropertyOptions.IsDefaultCollection);
	}
}

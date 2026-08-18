using System;
using System.Collections.ObjectModel;
using System.Configuration;
using System.ServiceModel.Description;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200063A RID: 1594
	public sealed class MetadataElement : ConfigurationElement
	{
		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x06003D5C RID: 15708 RVA: 0x000EA6D9 File Offset: 0x000E88D9
		[ConfigurationProperty("policyImporters")]
		public PolicyImporterElementCollection PolicyImporters
		{
			get
			{
				return (PolicyImporterElementCollection)base["policyImporters"];
			}
		}

		// Token: 0x17000F01 RID: 3841
		// (get) Token: 0x06003D5D RID: 15709 RVA: 0x000EA6EB File Offset: 0x000E88EB
		[ConfigurationProperty("wsdlImporters")]
		public WsdlImporterElementCollection WsdlImporters
		{
			get
			{
				return (WsdlImporterElementCollection)base["wsdlImporters"];
			}
		}

		// Token: 0x06003D5E RID: 15710 RVA: 0x000EA6FD File Offset: 0x000E88FD
		public Collection<IWsdlImportExtension> LoadWsdlImportExtensions()
		{
			return ConfigLoader.LoadWsdlImporters(this.WsdlImporters, base.EvaluationContext);
		}

		// Token: 0x06003D5F RID: 15711 RVA: 0x000EA710 File Offset: 0x000E8910
		public Collection<IPolicyImportExtension> LoadPolicyImportExtensions()
		{
			return ConfigLoader.LoadPolicyImporters(this.PolicyImporters, base.EvaluationContext);
		}

		// Token: 0x06003D60 RID: 15712 RVA: 0x000EA723 File Offset: 0x000E8923
		internal void SetDefaults()
		{
			this.PolicyImporters.SetDefaults();
			this.WsdlImporters.SetDefaults();
		}

		// Token: 0x17000F02 RID: 3842
		// (get) Token: 0x06003D61 RID: 15713 RVA: 0x000EA73C File Offset: 0x000E893C
		protected override ConfigurationPropertyCollection Properties
		{
			get
			{
				if (this.properties == null)
				{
					this.properties = new ConfigurationPropertyCollection
					{
						new ConfigurationProperty("policyImporters", typeof(PolicyImporterElementCollection), null, null, null, ConfigurationPropertyOptions.None),
						new ConfigurationProperty("wsdlImporters", typeof(WsdlImporterElementCollection), null, null, null, ConfigurationPropertyOptions.None)
					};
				}
				return this.properties;
			}
		}

		// Token: 0x04002C8F RID: 11407
		private ConfigurationPropertyCollection properties;
	}
}

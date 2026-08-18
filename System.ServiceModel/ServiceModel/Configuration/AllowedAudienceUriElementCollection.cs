using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005E1 RID: 1505
	[ConfigurationCollection(typeof(AllowedAudienceUriElement), CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class AllowedAudienceUriElementCollection : ServiceModelConfigurationElementCollection<AllowedAudienceUriElement>
	{
		// Token: 0x06003A4A RID: 14922 RVA: 0x000E0825 File Offset: 0x000DEA25
		public AllowedAudienceUriElementCollection() : base(ConfigurationElementCollectionType.BasicMap, "add")
		{
		}

		// Token: 0x06003A4B RID: 14923 RVA: 0x000E0833 File Offset: 0x000DEA33
		protected override ConfigurationElement CreateNewElement()
		{
			return new AllowedAudienceUriElement();
		}

		// Token: 0x06003A4C RID: 14924 RVA: 0x000E083C File Offset: 0x000DEA3C
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			AllowedAudienceUriElement allowedAudienceUriElement = (AllowedAudienceUriElement)element;
			return allowedAudienceUriElement.AllowedAudienceUri;
		}

		// Token: 0x17000DBD RID: 3517
		// (get) Token: 0x06003A4D RID: 14925 RVA: 0x000E0869 File Offset: 0x000DEA69
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return true;
			}
		}
	}
}

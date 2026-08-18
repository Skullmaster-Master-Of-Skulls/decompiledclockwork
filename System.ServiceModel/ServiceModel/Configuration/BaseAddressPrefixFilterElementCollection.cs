using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005EB RID: 1515
	[ConfigurationCollection(typeof(BaseAddressPrefixFilterElement))]
	public sealed class BaseAddressPrefixFilterElementCollection : ServiceModelConfigurationElementCollection<BaseAddressPrefixFilterElement>
	{
		// Token: 0x06003A75 RID: 14965 RVA: 0x000E0DEE File Offset: 0x000DEFEE
		public BaseAddressPrefixFilterElementCollection() : base(ConfigurationElementCollectionType.AddRemoveClearMap, "add")
		{
		}

		// Token: 0x06003A76 RID: 14966 RVA: 0x000E0DFC File Offset: 0x000DEFFC
		protected override ConfigurationElement CreateNewElement()
		{
			return new BaseAddressPrefixFilterElement();
		}

		// Token: 0x06003A77 RID: 14967 RVA: 0x000E0E04 File Offset: 0x000DF004
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			BaseAddressPrefixFilterElement baseAddressPrefixFilterElement = (BaseAddressPrefixFilterElement)element;
			return baseAddressPrefixFilterElement.Prefix;
		}

		// Token: 0x17000DCB RID: 3531
		// (get) Token: 0x06003A78 RID: 14968 RVA: 0x000E0E31 File Offset: 0x000DF031
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return true;
			}
		}
	}
}

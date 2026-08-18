using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020005E9 RID: 1513
	[ConfigurationCollection(typeof(BaseAddressElement), CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class BaseAddressElementCollection : ServiceModelConfigurationElementCollection<BaseAddressElement>
	{
		// Token: 0x06003A6C RID: 14956 RVA: 0x000E0D11 File Offset: 0x000DEF11
		public BaseAddressElementCollection() : base(ConfigurationElementCollectionType.BasicMap, "add")
		{
		}

		// Token: 0x06003A6D RID: 14957 RVA: 0x000E0D1F File Offset: 0x000DEF1F
		protected override ConfigurationElement CreateNewElement()
		{
			return new BaseAddressElement();
		}

		// Token: 0x06003A6E RID: 14958 RVA: 0x000E0D28 File Offset: 0x000DEF28
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			BaseAddressElement baseAddressElement = (BaseAddressElement)element;
			return baseAddressElement.BaseAddress;
		}

		// Token: 0x17000DC8 RID: 3528
		// (get) Token: 0x06003A6F RID: 14959 RVA: 0x000E0D55 File Offset: 0x000DEF55
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return true;
			}
		}
	}
}

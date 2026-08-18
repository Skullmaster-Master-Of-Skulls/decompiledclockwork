using System;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x02000616 RID: 1558
	[ConfigurationCollection(typeof(DelegatingHandlerElement), AddItemName = "handler", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	public sealed class DelegatingHandlerElementCollection : ServiceModelConfigurationElementCollection<DelegatingHandlerElement>
	{
		// Token: 0x06003BF0 RID: 15344 RVA: 0x000E53E5 File Offset: 0x000E35E5
		public DelegatingHandlerElementCollection() : base(ConfigurationElementCollectionType.BasicMap, "handler")
		{
		}

		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x06003BF1 RID: 15345 RVA: 0x000E53F3 File Offset: 0x000E35F3
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003BF2 RID: 15346 RVA: 0x000E53F8 File Offset: 0x000E35F8
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw FxTrace.Exception.ArgumentNull("element");
			}
			DelegatingHandlerElement delegatingHandlerElement = element as DelegatingHandlerElement;
			if (delegatingHandlerElement == null)
			{
				throw FxTrace.Exception.Argument("element", SR.GetString("InputMustBeDelegatingHandlerElementError", new object[]
				{
					typeof(ConfigurationElement).Name,
					typeof(DelegatingHandlerElement).Name
				}));
			}
			return delegatingHandlerElement.Id;
		}
	}
}

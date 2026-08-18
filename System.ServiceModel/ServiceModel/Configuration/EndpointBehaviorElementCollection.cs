using System;
using System.Collections.Generic;
using System.Configuration;

namespace System.ServiceModel.Configuration
{
	// Token: 0x0200061C RID: 1564
	[ConfigurationCollection(typeof(EndpointBehaviorElement), AddItemName = "behavior")]
	public sealed class EndpointBehaviorElementCollection : ServiceModelEnhancedConfigurationElementCollection<EndpointBehaviorElement>
	{
		// Token: 0x06003C16 RID: 15382 RVA: 0x000E5AC0 File Offset: 0x000E3CC0
		public EndpointBehaviorElementCollection() : base("behavior")
		{
		}

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x06003C17 RID: 15383 RVA: 0x000E5ACD File Offset: 0x000E3CCD
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06003C18 RID: 15384 RVA: 0x000E5AD0 File Offset: 0x000E3CD0
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			EndpointBehaviorElement endpointBehaviorElement = (EndpointBehaviorElement)element;
			return endpointBehaviorElement.Name;
		}

		// Token: 0x06003C19 RID: 15385 RVA: 0x000E5B00 File Offset: 0x000E3D00
		protected override void BaseAdd(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			EndpointBehaviorElement endpointBehaviorElement = element as EndpointBehaviorElement;
			string name = endpointBehaviorElement.Name;
			EndpointBehaviorElement endpointBehaviorElement2 = base.BaseGet(name) as EndpointBehaviorElement;
			List<BehaviorExtensionElement> list = new List<BehaviorExtensionElement>();
			if (endpointBehaviorElement2 != null)
			{
				foreach (BehaviorExtensionElement item in endpointBehaviorElement2)
				{
					list.Add(item);
				}
			}
			endpointBehaviorElement.MergeWith(list);
			base.BaseAdd(endpointBehaviorElement);
		}
	}
}

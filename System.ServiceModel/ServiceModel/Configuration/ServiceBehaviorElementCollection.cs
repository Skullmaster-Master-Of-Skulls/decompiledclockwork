using System;
using System.Collections.Generic;
using System.Configuration;
using System.Xml;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006C6 RID: 1734
	[ConfigurationCollection(typeof(ServiceBehaviorElement), AddItemName = "behavior")]
	public sealed class ServiceBehaviorElementCollection : ServiceModelEnhancedConfigurationElementCollection<ServiceBehaviorElement>
	{
		// Token: 0x0600432C RID: 17196 RVA: 0x000FDDB6 File Offset: 0x000FBFB6
		public ServiceBehaviorElementCollection() : base("behavior")
		{
		}

		// Token: 0x17001161 RID: 4449
		// (get) Token: 0x0600432D RID: 17197 RVA: 0x000FDDC3 File Offset: 0x000FBFC3
		protected override bool ThrowOnDuplicate
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600432E RID: 17198 RVA: 0x000FDDC6 File Offset: 0x000FBFC6
		protected override void DeserializeElement(XmlReader reader, bool serializeCollectionKey)
		{
			base.DeserializeElement(reader, serializeCollectionKey);
		}

		// Token: 0x0600432F RID: 17199 RVA: 0x000FDDD0 File Offset: 0x000FBFD0
		protected override object GetElementKey(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			ServiceBehaviorElement serviceBehaviorElement = (ServiceBehaviorElement)element;
			return serviceBehaviorElement.Name;
		}

		// Token: 0x06004330 RID: 17200 RVA: 0x000FDE00 File Offset: 0x000FC000
		protected override void BaseAdd(ConfigurationElement element)
		{
			if (element == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("element");
			}
			ServiceBehaviorElement serviceBehaviorElement = element as ServiceBehaviorElement;
			string name = serviceBehaviorElement.Name;
			ServiceBehaviorElement serviceBehaviorElement2 = base.BaseGet(name) as ServiceBehaviorElement;
			List<BehaviorExtensionElement> list = new List<BehaviorExtensionElement>();
			if (serviceBehaviorElement2 != null)
			{
				foreach (BehaviorExtensionElement item in serviceBehaviorElement2)
				{
					list.Add(item);
				}
			}
			serviceBehaviorElement.MergeWith(list);
			base.BaseAdd(serviceBehaviorElement);
		}
	}
}

using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x020004A1 RID: 1185
	[ConfigurationCollection(typeof(SourceElement), AddItemName = "source", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	internal class SourceElementsCollection : ConfigurationElementCollection
	{
		// Token: 0x17000AA1 RID: 2721
		public SourceElement this[string name]
		{
			get
			{
				return (SourceElement)base.BaseGet(name);
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06002BF5 RID: 11253 RVA: 0x000C6D0F File Offset: 0x000C4F0F
		protected override string ElementName
		{
			get
			{
				return "source";
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06002BF6 RID: 11254 RVA: 0x000C6D16 File Offset: 0x000C4F16
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x06002BF7 RID: 11255 RVA: 0x000C6D1C File Offset: 0x000C4F1C
		protected override ConfigurationElement CreateNewElement()
		{
			SourceElement sourceElement = new SourceElement();
			sourceElement.Listeners.InitializeDefaultInternal();
			return sourceElement;
		}

		// Token: 0x06002BF8 RID: 11256 RVA: 0x000C6D3B File Offset: 0x000C4F3B
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SourceElement)element).Name;
		}
	}
}

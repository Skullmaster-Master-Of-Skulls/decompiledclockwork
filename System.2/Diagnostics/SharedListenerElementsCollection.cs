using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x0200049D RID: 1181
	[ConfigurationCollection(typeof(ListenerElement), AddItemName = "add", CollectionType = ConfigurationElementCollectionType.BasicMap)]
	internal class SharedListenerElementsCollection : ListenerElementsCollection
	{
		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06002BD5 RID: 11221 RVA: 0x000C6520 File Offset: 0x000C4720
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.BasicMap;
			}
		}

		// Token: 0x06002BD6 RID: 11222 RVA: 0x000C6523 File Offset: 0x000C4723
		protected override ConfigurationElement CreateNewElement()
		{
			return new ListenerElement(false);
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06002BD7 RID: 11223 RVA: 0x000C652B File Offset: 0x000C472B
		protected override string ElementName
		{
			get
			{
				return "add";
			}
		}
	}
}

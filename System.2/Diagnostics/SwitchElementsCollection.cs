using System;
using System.Configuration;

namespace System.Diagnostics
{
	// Token: 0x020004A8 RID: 1192
	[ConfigurationCollection(typeof(SwitchElement))]
	internal class SwitchElementsCollection : ConfigurationElementCollection
	{
		// Token: 0x17000AB5 RID: 2741
		public SwitchElement this[string name]
		{
			get
			{
				return (SwitchElement)base.BaseGet(name);
			}
		}

		// Token: 0x17000AB6 RID: 2742
		// (get) Token: 0x06002C31 RID: 11313 RVA: 0x000C7814 File Offset: 0x000C5A14
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.AddRemoveClearMap;
			}
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x000C7817 File Offset: 0x000C5A17
		protected override ConfigurationElement CreateNewElement()
		{
			return new SwitchElement();
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x000C781E File Offset: 0x000C5A1E
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((SwitchElement)element).Name;
		}
	}
}

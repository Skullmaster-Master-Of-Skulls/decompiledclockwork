using System;
using System.Configuration;

namespace AjaxControlToolkit
{
	// Token: 0x02000013 RID: 19
	public class CustomControlsCollection : ConfigurationElementCollection
	{
		// Token: 0x1700004A RID: 74
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x000040A3 File Offset: 0x000022A3
		public override ConfigurationElementCollectionType CollectionType
		{
			get
			{
				return ConfigurationElementCollectionType.AddRemoveClearMap;
			}
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x000040A6 File Offset: 0x000022A6
		protected override ConfigurationElement CreateNewElement()
		{
			return new CustomControlElement();
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000040AD File Offset: 0x000022AD
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((CustomControlElement)element).Type;
		}
	}
}

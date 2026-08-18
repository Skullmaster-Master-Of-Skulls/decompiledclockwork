using System;
using System.Collections;
using System.Configuration;

namespace System.Data
{
	// Token: 0x0200012C RID: 300
	internal sealed class LocalDBInstancesCollection : ConfigurationElementCollection
	{
		// Token: 0x060011F4 RID: 4596 RVA: 0x00089D9C File Offset: 0x0008919C
		internal LocalDBInstancesCollection() : base(LocalDBInstancesCollection.s_comparer)
		{
		}

		// Token: 0x060011F5 RID: 4597 RVA: 0x00089DB4 File Offset: 0x000891B4
		protected override ConfigurationElement CreateNewElement()
		{
			return new LocalDBInstanceElement();
		}

		// Token: 0x060011F6 RID: 4598 RVA: 0x00089DC8 File Offset: 0x000891C8
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((LocalDBInstanceElement)element).Name;
		}

		// Token: 0x0400061C RID: 1564
		private static readonly LocalDBInstancesCollection.TrimOrdinalIgnoreCaseStringComparer s_comparer = new LocalDBInstancesCollection.TrimOrdinalIgnoreCaseStringComparer();

		// Token: 0x02000361 RID: 865
		private class TrimOrdinalIgnoreCaseStringComparer : IComparer
		{
			// Token: 0x0600343C RID: 13372 RVA: 0x00140448 File Offset: 0x0013F848
			public int Compare(object x, object y)
			{
				string text = x as string;
				if (text != null)
				{
					x = text.Trim();
				}
				string text2 = y as string;
				if (text2 != null)
				{
					y = text2.Trim();
				}
				return StringComparer.OrdinalIgnoreCase.Compare(x, y);
			}
		}
	}
}

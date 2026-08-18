using System;
using System.Collections;
using System.Configuration;

namespace System.Data
{
	// Token: 0x0200033D RID: 829
	internal sealed class LocalDBInstancesCollection : ConfigurationElementCollection
	{
		// Token: 0x06002B30 RID: 11056 RVA: 0x002C3928 File Offset: 0x002C2D28
		internal LocalDBInstancesCollection() : base(LocalDBInstancesCollection.s_comparer)
		{
		}

		// Token: 0x06002B31 RID: 11057 RVA: 0x002C3948 File Offset: 0x002C2D48
		protected override ConfigurationElement CreateNewElement()
		{
			return new LocalDBInstanceElement();
		}

		// Token: 0x06002B32 RID: 11058 RVA: 0x002C3968 File Offset: 0x002C2D68
		protected override object GetElementKey(ConfigurationElement element)
		{
			return ((LocalDBInstanceElement)element).Name;
		}

		// Token: 0x04001C5E RID: 7262
		private static readonly LocalDBInstancesCollection.TrimOrdinalIgnoreCaseStringComparer s_comparer = new LocalDBInstancesCollection.TrimOrdinalIgnoreCaseStringComparer();

		// Token: 0x0200033E RID: 830
		private class TrimOrdinalIgnoreCaseStringComparer : IComparer
		{
			// Token: 0x06002B34 RID: 11060 RVA: 0x002C39A8 File Offset: 0x002C2DA8
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

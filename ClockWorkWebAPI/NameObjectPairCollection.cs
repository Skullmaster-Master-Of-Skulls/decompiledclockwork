using System;
using System.Collections;

namespace ClockWorkWebAPI
{
	// Token: 0x0200001E RID: 30
	public class NameObjectPairCollection : CollectionBase
	{
		// Token: 0x060001BB RID: 443 RVA: 0x0000D180 File Offset: 0x0000B380
		public int Add(string name, object val)
		{
			return base.List.Add(new NameObjectPair(name, val));
		}

		// Token: 0x060001BC RID: 444 RVA: 0x0000D1A4 File Offset: 0x0000B3A4
		public int Add(NameObjectPair nameObjectPair)
		{
			return base.List.Add(nameObjectPair);
		}

		// Token: 0x1700007A RID: 122
		public NameObjectPair this[string name]
		{
			get
			{
				return this.Find(name);
			}
		}

		// Token: 0x060001BE RID: 446 RVA: 0x0000D1E0 File Offset: 0x0000B3E0
		private NameObjectPair Find(string name)
		{
			string strB = name.ToLower();
			foreach (object obj in base.List)
			{
				NameObjectPair nameObjectPair = (NameObjectPair)obj;
				bool flag = nameObjectPair.Name.ToLower().CompareTo(strB) == 0;
				if (flag)
				{
					return nameObjectPair;
				}
			}
			return null;
		}
	}
}

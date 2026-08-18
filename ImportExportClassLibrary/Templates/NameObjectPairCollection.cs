using System;
using System.Collections;

namespace ImportExportClassLibrary.Templates
{
	// Token: 0x0200001B RID: 27
	public class NameObjectPairCollection : CollectionBase
	{
		// Token: 0x060000C1 RID: 193 RVA: 0x0000474C File Offset: 0x0000374C
		public int Add(NameObjectPair nop)
		{
			return base.List.Add(nop);
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x0000475C File Offset: 0x0000375C
		public int Add(string name, object obj)
		{
			NameObjectPair value = new NameObjectPair(name, obj);
			return base.List.Add(value);
		}

		// Token: 0x17000016 RID: 22
		public NameObjectPair this[string name]
		{
			get
			{
				return this.Find(name);
			}
		}

		// Token: 0x060000C4 RID: 196 RVA: 0x00004798 File Offset: 0x00003798
		private NameObjectPair Find(string name)
		{
			string nameToMatch = name.ToLower();
			foreach (object obj in base.List)
			{
				NameObjectPair nameObjectPair = (NameObjectPair)obj;
				if (nameObjectPair.MatchesWith(nameToMatch))
				{
					return nameObjectPair;
				}
			}
			return null;
		}
	}
}

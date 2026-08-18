using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x02000318 RID: 792
	internal class WorkItems
	{
		// Token: 0x17000938 RID: 2360
		internal ImportStructWorkItem this[int index]
		{
			get
			{
				return (ImportStructWorkItem)this.list[index];
			}
			set
			{
				this.list[index] = value;
			}
		}

		// Token: 0x17000939 RID: 2361
		// (get) Token: 0x06002586 RID: 9606 RVA: 0x000B34E0 File Offset: 0x000B24E0
		internal int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x06002587 RID: 9607 RVA: 0x000B34ED File Offset: 0x000B24ED
		internal void Add(ImportStructWorkItem item)
		{
			this.list.Add(item);
		}

		// Token: 0x06002588 RID: 9608 RVA: 0x000B34FC File Offset: 0x000B24FC
		internal bool Contains(StructMapping mapping)
		{
			return this.IndexOf(mapping) >= 0;
		}

		// Token: 0x06002589 RID: 9609 RVA: 0x000B350C File Offset: 0x000B250C
		internal int IndexOf(StructMapping mapping)
		{
			for (int i = 0; i < this.Count; i++)
			{
				if (this[i].Mapping == mapping)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x0600258A RID: 9610 RVA: 0x000B353C File Offset: 0x000B253C
		internal void RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		// Token: 0x040015A5 RID: 5541
		private ArrayList list = new ArrayList();
	}
}

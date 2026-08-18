using System;
using System.Collections;

namespace System.Xml.Serialization
{
	// Token: 0x0200019E RID: 414
	internal class WorkItems
	{
		// Token: 0x170005FD RID: 1533
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

		// Token: 0x170005FE RID: 1534
		// (get) Token: 0x06001B54 RID: 6996 RVA: 0x0007C183 File Offset: 0x0007A383
		internal int Count
		{
			get
			{
				return this.list.Count;
			}
		}

		// Token: 0x06001B55 RID: 6997 RVA: 0x0007C190 File Offset: 0x0007A390
		internal void Add(ImportStructWorkItem item)
		{
			this.list.Add(item);
		}

		// Token: 0x06001B56 RID: 6998 RVA: 0x0007C19F File Offset: 0x0007A39F
		internal bool Contains(StructMapping mapping)
		{
			return this.IndexOf(mapping) >= 0;
		}

		// Token: 0x06001B57 RID: 6999 RVA: 0x0007C1B0 File Offset: 0x0007A3B0
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

		// Token: 0x06001B58 RID: 7000 RVA: 0x0007C1E0 File Offset: 0x0007A3E0
		internal void RemoveAt(int index)
		{
			this.list.RemoveAt(index);
		}

		// Token: 0x04000C16 RID: 3094
		private ArrayList list = new ArrayList();
	}
}

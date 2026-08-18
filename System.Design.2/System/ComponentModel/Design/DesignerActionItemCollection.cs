using System;
using System.Collections;

namespace System.ComponentModel.Design
{
	// Token: 0x020001BF RID: 447
	public class DesignerActionItemCollection : CollectionBase
	{
		// Token: 0x170003C9 RID: 969
		public DesignerActionItem this[int index]
		{
			get
			{
				return (DesignerActionItem)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x0600102F RID: 4143 RVA: 0x0005B7CC File Offset: 0x000599CC
		public int Add(DesignerActionItem value)
		{
			return base.List.Add(value);
		}

		// Token: 0x06001030 RID: 4144 RVA: 0x00057A39 File Offset: 0x00055C39
		public bool Contains(DesignerActionItem value)
		{
			return base.List.Contains(value);
		}

		// Token: 0x06001031 RID: 4145 RVA: 0x00057A55 File Offset: 0x00055C55
		public void CopyTo(DesignerActionItem[] array, int index)
		{
			base.List.CopyTo(array, index);
		}

		// Token: 0x06001032 RID: 4146 RVA: 0x00057A2B File Offset: 0x00055C2B
		public int IndexOf(DesignerActionItem value)
		{
			return base.List.IndexOf(value);
		}

		// Token: 0x06001033 RID: 4147 RVA: 0x00057A1C File Offset: 0x00055C1C
		public void Insert(int index, DesignerActionItem value)
		{
			base.List.Insert(index, value);
		}

		// Token: 0x06001034 RID: 4148 RVA: 0x00057A47 File Offset: 0x00055C47
		public void Remove(DesignerActionItem value)
		{
			base.List.Remove(value);
		}
	}
}

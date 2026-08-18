using System;
using System.Collections;

namespace AutoComboBox.InputDialogControls.TableFilters
{
	// Token: 0x02000099 RID: 153
	public class TableFilterCollection : CollectionBase
	{
		// Token: 0x060005D5 RID: 1493 RVA: 0x000305F8 File Offset: 0x0002F5F8
		public int Add(TableFilter tableFilter)
		{
			return base.List.Add(tableFilter);
		}

		// Token: 0x060005D6 RID: 1494 RVA: 0x00030616 File Offset: 0x0002F616
		public void Remove(TableFilter tableFilter)
		{
			base.List.Remove(tableFilter);
		}

		// Token: 0x17000138 RID: 312
		public TableFilter this[int index]
		{
			get
			{
				return (TableFilter)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}
	}
}

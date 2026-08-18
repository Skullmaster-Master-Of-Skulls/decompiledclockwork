using System;
using System.Collections;

namespace AutoComboBox.InputDialogControls.TableFilters
{
	// Token: 0x02000073 RID: 115
	public class TableFilterControlCollection : CollectionBase
	{
		// Token: 0x06000460 RID: 1120 RVA: 0x00023F60 File Offset: 0x00022F60
		public int Add(TableFilterControl tableFilterControl)
		{
			return base.List.Add(tableFilterControl);
		}

		// Token: 0x170000EB RID: 235
		public TableFilterControl this[int index]
		{
			get
			{
				return (TableFilterControl)base.List[index];
			}
			set
			{
				base.List[index] = value;
			}
		}

		// Token: 0x06000463 RID: 1123 RVA: 0x00023FB4 File Offset: 0x00022FB4
		public void Remove(TableFilterControl tableFilterControl)
		{
			base.List.Remove(tableFilterControl);
		}
	}
}

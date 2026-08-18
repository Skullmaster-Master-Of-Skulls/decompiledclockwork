using System;
using System.Collections;
using System.Windows.Forms;

namespace AutoComboBox
{
	// Token: 0x0200008D RID: 141
	public class ListViewMultipleColCompare : IComparer
	{
		// Token: 0x1700012A RID: 298
		// (get) Token: 0x060005A5 RID: 1445 RVA: 0x0002F814 File Offset: 0x0002E814
		public int[] Cols
		{
			get
			{
				return this.cols;
			}
		}

		// Token: 0x060005A6 RID: 1446 RVA: 0x0002F82C File Offset: 0x0002E82C
		public ListViewMultipleColCompare()
		{
			this.cols = null;
			this.ascending = true;
		}

		// Token: 0x060005A7 RID: 1447 RVA: 0x0002F848 File Offset: 0x0002E848
		public ListViewMultipleColCompare(int column)
		{
			this.cols = new int[]
			{
				column
			};
			this.ascending = true;
		}

		// Token: 0x060005A8 RID: 1448 RVA: 0x0002F878 File Offset: 0x0002E878
		public ListViewMultipleColCompare(int column, bool Ascending)
		{
			this.cols = new int[]
			{
				column
			};
			this.ascending = Ascending;
		}

		// Token: 0x060005A9 RID: 1449 RVA: 0x0002F8A7 File Offset: 0x0002E8A7
		public ListViewMultipleColCompare(int[] columns)
		{
			this.cols = columns;
			this.ascending = true;
		}

		// Token: 0x060005AA RID: 1450 RVA: 0x0002F8C0 File Offset: 0x0002E8C0
		public ListViewMultipleColCompare(int[] columns, bool Ascending)
		{
			this.cols = columns;
			this.ascending = Ascending;
		}

		// Token: 0x060005AB RID: 1451 RVA: 0x0002F8DC File Offset: 0x0002E8DC
		public int Compare(object x, object y)
		{
			ListViewItem listViewItem = (ListViewItem)x;
			ListViewItem listViewItem2 = (ListViewItem)y;
			string text = "";
			string text2 = "";
			for (int i = 0; i < this.cols.Length; i++)
			{
				int index = this.cols[i];
				if (!false)
				{
					text += listViewItem.SubItems[index].Text;
					text2 += listViewItem2.SubItems[index].Text;
				}
			}
			int result;
			if (this.ascending)
			{
				result = string.Compare(text, text2);
			}
			else
			{
				result = string.Compare(text2, text);
			}
			return result;
		}

		// Token: 0x040004A4 RID: 1188
		private int[] cols;

		// Token: 0x040004A5 RID: 1189
		public bool ascending;
	}
}

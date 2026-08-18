using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Windows.Forms;
using System.Xml.Serialization;

namespace AutoComboBox
{
	// Token: 0x020000B1 RID: 177
	[Serializable]
	public class ListViewSettings
	{
		// Token: 0x060006BB RID: 1723
		[DllImport("user32.dll")]
		private static extern bool SendMessage(IntPtr hWnd, int msg, int wParam, ref ListViewSettings.LV_COLUMN lParam);

		// Token: 0x060006BC RID: 1724 RVA: 0x00035C40 File Offset: 0x00034C40
		public ListViewSettings(ListView listView)
		{
			try
			{
				foreach (object obj in listView.Columns)
				{
					ColumnHeader columnHeader = (ColumnHeader)obj;
					ListViewSettings.LV_COLUMN lv_COLUMN = default(ListViewSettings.LV_COLUMN);
					lv_COLUMN.mask = 32U;
					bool flag = ListViewSettings.SendMessage(listView.Handle, 4191, columnHeader.Index, ref lv_COLUMN);
					this.listViewCols.Add(new ListViewColumn(columnHeader.Text, columnHeader.Width, lv_COLUMN.iOrder));
				}
			}
			catch
			{
			}
		}

		// Token: 0x060006BD RID: 1725 RVA: 0x00035D20 File Offset: 0x00034D20
		public void RestoreFormat(ListView listView)
		{
			try
			{
				listView.Hide();
				for (int i = 0; i < this.listViewCols.Count; i++)
				{
					foreach (object obj in listView.Columns)
					{
						ColumnHeader columnHeader = (ColumnHeader)obj;
						if (columnHeader.Text == ((ListViewColumn)this.listViewCols[i]).header)
						{
							ListViewSettings.LV_COLUMN lv_COLUMN = default(ListViewSettings.LV_COLUMN);
							lv_COLUMN.mask = 32U;
							lv_COLUMN.iOrder = ((ListViewColumn)this.listViewCols[i]).order;
							bool flag = ListViewSettings.SendMessage(listView.Handle, 4192, columnHeader.Index, ref lv_COLUMN);
							columnHeader.Width = ((ListViewColumn)this.listViewCols[i]).width;
							break;
						}
					}
				}
				listView.Show();
			}
			catch
			{
			}
		}

		// Token: 0x04000541 RID: 1345
		private const int LVM_FIRST = 4096;

		// Token: 0x04000542 RID: 1346
		private const int LVM_GETCOLUMN = 4191;

		// Token: 0x04000543 RID: 1347
		private const int LVM_SETCOLUMN = 4192;

		// Token: 0x04000544 RID: 1348
		private const int LVCF_ORDER = 32;

		// Token: 0x04000545 RID: 1349
		[XmlElement("ListViewColumns", typeof(ListViewColumn))]
		public ArrayList listViewCols = new ArrayList();

		// Token: 0x020000B2 RID: 178
		private struct LV_COLUMN
		{
			// Token: 0x04000546 RID: 1350
			public uint mask;

			// Token: 0x04000547 RID: 1351
			public int fmt;

			// Token: 0x04000548 RID: 1352
			public int cx;

			// Token: 0x04000549 RID: 1353
			public string pszText;

			// Token: 0x0400054A RID: 1354
			public int cchTextMax;

			// Token: 0x0400054B RID: 1355
			public int iSubItem;

			// Token: 0x0400054C RID: 1356
			public int iImage;

			// Token: 0x0400054D RID: 1357
			public int iOrder;
		}
	}
}

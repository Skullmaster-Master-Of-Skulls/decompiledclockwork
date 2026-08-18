using System;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox
{
	// Token: 0x020000A9 RID: 169
	public partial class InputCheckedList : Form
	{
		// Token: 0x06000651 RID: 1617 RVA: 0x0003261C File Offset: 0x0003161C
		public InputCheckedList(DataView dv, int[] preSelectedIndices, string title, string caption)
		{
			this.InitializeComponent();
			this.dv = dv;
			this.preSelectedIndices = preSelectedIndices;
			this.title = title;
			this.caption = caption;
			this.displayMember = this.displayMember;
		}

		// Token: 0x06000652 RID: 1618 RVA: 0x0003266C File Offset: 0x0003166C
		public InputCheckedList(DataTable t, int[] preSelectedIndices, string title, string caption, string displayMember)
		{
			this.InitializeComponent();
			this.dv = new DataView(t);
			this.preSelectedIndices = preSelectedIndices;
			this.title = title;
			this.caption = caption;
			this.displayMember = displayMember;
		}

		// Token: 0x1700014E RID: 334
		// (get) Token: 0x06000653 RID: 1619 RVA: 0x000326BC File Offset: 0x000316BC
		public CheckedListBox ListBox
		{
			get
			{
				return this.lb;
			}
		}

		// Token: 0x06000654 RID: 1620 RVA: 0x000326D4 File Offset: 0x000316D4
		private void Init()
		{
			this.Text = this.title;
			this.lbl_caption.Text = this.caption;
			this.ResizeCaptionToFit();
			this.lb.BeginUpdate();
			bool flag = this.preSelectedIndices != null && this.preSelectedIndices.Length > 0 && this.preSelectedIndices[0] < 0;
			for (int i = 0; i < this.dv.Count; i++)
			{
				DataRow row = this.dv[i].Row;
				InputCheckedList.ListBoxDataRowObject item = new InputCheckedList.ListBoxDataRowObject(row, this.displayMember);
				this.lb.Items.Add(item);
				bool flag2 = flag || InputCheckedList.IntArrayContains(this.preSelectedIndices, i);
				if (flag2)
				{
					this.lb.SetItemChecked(i, true);
				}
			}
			this.lb.EndUpdate();
		}

		// Token: 0x06000655 RID: 1621 RVA: 0x000327C0 File Offset: 0x000317C0
		private static bool IntArrayContains(int[] array, int integer)
		{
			bool result;
			if (array == null)
			{
				result = false;
			}
			else
			{
				for (int i = 0; i < array.Length; i++)
				{
					if (array[i] == integer)
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x06000656 RID: 1622 RVA: 0x00032808 File Offset: 0x00031808
		private void ResizeCaptionToFit()
		{
			Graphics graphics = this.lbl_caption.CreateGraphics();
			SizeF layoutArea = new SizeF((float)this.lbl_caption.ClientRectangle.Width, (float)this.lbl_caption.ClientRectangle.Height);
			StringFormat stringFormat = new StringFormat();
			int num2;
			int num3;
			int num = Convert.ToInt32(graphics.MeasureString(this.lbl_caption.Text, this.lbl_caption.Font, layoutArea, stringFormat, out num2, out num3).Height - layoutArea.Height);
			int num4 = this.lbl_caption.Height + num;
			if (num4 > 0)
			{
				this.lbl_caption.Height += num;
			}
		}

		// Token: 0x06000659 RID: 1625 RVA: 0x00032F2D File Offset: 0x00031F2D
		private void InputCheckedList_Load(object sender, EventArgs e)
		{
			this.Init();
		}

		// Token: 0x0600065A RID: 1626 RVA: 0x00032F37 File Offset: 0x00031F37
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600065B RID: 1627 RVA: 0x00032F41 File Offset: 0x00031F41
		private void btn_ok_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x0600065C RID: 1628 RVA: 0x00032F54 File Offset: 0x00031F54
		private void btn_selectNone_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < this.lb.Items.Count; i++)
			{
				this.lb.SetItemChecked(i, false);
			}
		}

		// Token: 0x0600065D RID: 1629 RVA: 0x00032F90 File Offset: 0x00031F90
		private void btn_selectAll_Click(object sender, EventArgs e)
		{
			for (int i = 0; i < this.lb.Items.Count; i++)
			{
				this.lb.SetItemChecked(i, true);
			}
		}

		// Token: 0x0600065E RID: 1630 RVA: 0x00032FCC File Offset: 0x00031FCC
		public DataRow[] GetCheckedDataRows()
		{
			DataRow[] result;
			if (this.lb.CheckedItems.Count > 0)
			{
				DataRow[] array = new DataRow[this.lb.CheckedItems.Count];
				for (int i = 0; i < this.lb.CheckedItems.Count; i++)
				{
					array[i] = ((InputCheckedList.ListBoxDataRowObject)this.lb.CheckedItems[i]).Dr;
				}
				result = array;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x040004F9 RID: 1273
		private DataView dv;

		// Token: 0x040004FA RID: 1274
		private int[] preSelectedIndices;

		// Token: 0x040004FB RID: 1275
		private string title;

		// Token: 0x040004FC RID: 1276
		private string caption;

		// Token: 0x040004FD RID: 1277
		private string displayMember;

		// Token: 0x020000AA RID: 170
		private class ListBoxDataRowObject
		{
			// Token: 0x1700014F RID: 335
			// (get) Token: 0x0600065F RID: 1631 RVA: 0x00033054 File Offset: 0x00032054
			public DataRow Dr
			{
				get
				{
					return this.dr;
				}
			}

			// Token: 0x17000150 RID: 336
			// (get) Token: 0x06000660 RID: 1632 RVA: 0x0003306C File Offset: 0x0003206C
			public string DisplayMember
			{
				get
				{
					return this.displayMember;
				}
			}

			// Token: 0x06000661 RID: 1633 RVA: 0x00033084 File Offset: 0x00032084
			public ListBoxDataRowObject(DataRow dr, string displayMember)
			{
				this.dr = dr;
				this.displayMember = displayMember;
			}

			// Token: 0x06000662 RID: 1634 RVA: 0x000330A0 File Offset: 0x000320A0
			public override string ToString()
			{
				string result;
				if (this.dr.Table.Columns.Contains(this.displayMember))
				{
					result = this.dr[this.displayMember].ToString();
				}
				else
				{
					result = "";
				}
				return result;
			}

			// Token: 0x040004FE RID: 1278
			private DataRow dr;

			// Token: 0x040004FF RID: 1279
			private string displayMember;

			// Token: 0x04000500 RID: 1280
			private bool isChecked;
		}
	}
}

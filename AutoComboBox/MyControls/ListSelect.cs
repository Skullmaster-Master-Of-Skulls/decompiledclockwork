using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox.MyControls
{
	// Token: 0x020000B4 RID: 180
	public class ListSelect : UserControl
	{
		// Token: 0x060006BF RID: 1727 RVA: 0x00035EA4 File Offset: 0x00034EA4
		public ListSelect()
		{
			this.InitializeComponent();
		}

		// Token: 0x060006C0 RID: 1728 RVA: 0x00035EC4 File Offset: 0x00034EC4
		public void ConvertToDropList()
		{
			this.displayType = ListSelect.DisplayType.combobox;
			this.panel1.Visible = false;
			this.panel2.Visible = false;
			this.label1.Visible = false;
			this.label2.Visible = false;
			this.listBox1.Visible = false;
			this.listBox2.Visible = false;
			this.btm_panel.Visible = false;
			this.cmb.Visible = true;
			this.cmb.Dock = DockStyle.Fill;
		}

		// Token: 0x060006C1 RID: 1729 RVA: 0x00035F50 File Offset: 0x00034F50
		public void addItem(object item)
		{
			if (this.displayType == ListSelect.DisplayType.combobox)
			{
				this.cmb.Items.Add(item);
			}
			else
			{
				this.listBox2.Items.Add(item);
			}
		}

		// Token: 0x060006C2 RID: 1730 RVA: 0x00035F98 File Offset: 0x00034F98
		public void addItems(object[] items)
		{
			if (this.displayType == ListSelect.DisplayType.combobox)
			{
				this.cmb.Items.AddRange(items);
			}
			else
			{
				this.listBox2.Items.AddRange(items);
			}
		}

		// Token: 0x060006C3 RID: 1731 RVA: 0x00035FE0 File Offset: 0x00034FE0
		public void SetChecked(int controlId)
		{
			if (!this.IsChecked(controlId))
			{
				if (this.displayType == ListSelect.DisplayType.combobox)
				{
					for (int i = 0; i < this.cmb.Items.Count; i++)
					{
						ListSelectItem listSelectItem = (ListSelectItem)this.cmb.Items[i];
						if (listSelectItem.ControlId == controlId)
						{
							this.cmb.SetItemChecked(i, true);
							break;
						}
					}
				}
				else
				{
					int num = -1;
					for (int i = 0; i < this.listBox2.Items.Count; i++)
					{
						ListSelectItem listSelectItem = (ListSelectItem)this.listBox2.Items[i];
						if (listSelectItem.ControlId == controlId)
						{
							num = i;
							break;
						}
					}
					if (num >= 0)
					{
						ListSelectItem listSelectItem = (ListSelectItem)this.listBox2.Items[num];
						this.listBox1.Items.Add(listSelectItem);
						this.listBox2.Items.RemoveAt(num);
					}
				}
			}
		}

		// Token: 0x060006C4 RID: 1732 RVA: 0x00036108 File Offset: 0x00035108
		public List<int> GetCids()
		{
			List<int> list = new List<int>();
			if (this.displayType == ListSelect.DisplayType.combobox)
			{
				foreach (object obj in this.cmb.Items)
				{
					ListSelectItem listSelectItem = (ListSelectItem)obj;
					if (!list.Contains(listSelectItem.ControlId))
					{
						list.Add(listSelectItem.ControlId);
					}
				}
			}
			else
			{
				foreach (object obj2 in this.listBox1.Items)
				{
					ListSelectItem listSelectItem = (ListSelectItem)obj2;
					if (!list.Contains(listSelectItem.ControlId))
					{
						list.Add(listSelectItem.ControlId);
					}
				}
				foreach (object obj3 in this.listBox2.Items)
				{
					ListSelectItem listSelectItem = (ListSelectItem)obj3;
					if (!list.Contains(listSelectItem.ControlId))
					{
						list.Add(listSelectItem.ControlId);
					}
				}
			}
			return list;
		}

		// Token: 0x060006C5 RID: 1733 RVA: 0x000362A0 File Offset: 0x000352A0
		public bool IsChecked(int controlid)
		{
			bool result;
			if (this.displayType == ListSelect.DisplayType.combobox)
			{
				foreach (object obj in this.cmb.CheckedItems)
				{
					ListSelectItem listSelectItem = (ListSelectItem)obj;
					if (listSelectItem.ControlId == controlid)
					{
						return true;
					}
				}
				result = false;
			}
			else
			{
				foreach (object obj2 in this.listBox1.Items)
				{
					ListSelectItem listSelectItem = (ListSelectItem)obj2;
					if (listSelectItem.ControlId == controlid)
					{
						return true;
					}
				}
				result = false;
			}
			return result;
		}

		// Token: 0x060006C6 RID: 1734 RVA: 0x000363A4 File Offset: 0x000353A4
		public object[] getSelectedItems()
		{
			object[] result;
			if (this.displayType == ListSelect.DisplayType.combobox)
			{
				result = this.convertICollectionToArray(this.cmb.CheckedItems);
			}
			else
			{
				result = this.convertICollectionToArray(this.listBox1.Items);
			}
			return result;
		}

		// Token: 0x060006C7 RID: 1735 RVA: 0x000363F0 File Offset: 0x000353F0
		private object[] convertICollectionToArray(ICollection collection)
		{
			object[] array = new object[collection.Count];
			collection.CopyTo(array, 0);
			return array;
		}

		// Token: 0x060006C8 RID: 1736 RVA: 0x00036418 File Offset: 0x00035418
		private void btm_RemoveFromList1_Click_1(object sender, EventArgs e)
		{
			this.moveSelected(this.listBox1, this.listBox2);
		}

		// Token: 0x060006C9 RID: 1737 RVA: 0x0003642E File Offset: 0x0003542E
		private void btm_AddToList1_Click(object sender, EventArgs e)
		{
			this.moveSelected(this.listBox2, this.listBox1);
		}

		// Token: 0x060006CA RID: 1738 RVA: 0x00036444 File Offset: 0x00035444
		private void moveSelected(ListBox source, ListBox target)
		{
			ListBox.SelectedObjectCollection selectedItems = source.SelectedItems;
			target.Items.AddRange(this.convertICollectionToArray(selectedItems));
			foreach (object value in this.convertICollectionToArray(selectedItems))
			{
				source.Items.Remove(value);
			}
		}

		// Token: 0x060006CB RID: 1739 RVA: 0x0003649C File Offset: 0x0003549C
		private void ListSelect_SizeChanged(object sender, EventArgs e)
		{
			this.ResizeMe();
		}

		// Token: 0x060006CC RID: 1740 RVA: 0x000364A8 File Offset: 0x000354A8
		private void ResizeMe()
		{
			double num = (double)(base.Bounds.Width - this.btm_panel.Width - 4);
			int num2 = Convert.ToInt32(num / 2.0);
			if (num2 < 2)
			{
				num2 = 2;
			}
			if (this.displayType != ListSelect.DisplayType.combobox)
			{
				this.panel1.Width = num2;
				this.panel2.Width = num2;
				Graphics graphics = base.CreateGraphics();
				SizeF sizeF = graphics.MeasureString(this.List1Label, this.label1.Font, num2 - 4);
				this.label1.Height = ((sizeF.Height > 0f) ? Convert.ToInt32(sizeF.Height) : 5);
				sizeF = graphics.MeasureString(this.List2Label, this.label2.Font, num2 - 4);
				this.label2.Height = ((sizeF.Height > 0f) ? Convert.ToInt32(sizeF.Height) : 5);
			}
		}

		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060006CD RID: 1741 RVA: 0x000365BC File Offset: 0x000355BC
		// (set) Token: 0x060006CE RID: 1742 RVA: 0x000365D9 File Offset: 0x000355D9
		public string List1Label
		{
			get
			{
				return this.label1.Text;
			}
			set
			{
				this.label1.Text = value;
				this.ResizeMe();
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060006CF RID: 1743 RVA: 0x000365F0 File Offset: 0x000355F0
		// (set) Token: 0x060006D0 RID: 1744 RVA: 0x0003660D File Offset: 0x0003560D
		public string List2Label
		{
			get
			{
				return this.label2.Text;
			}
			set
			{
				this.label2.Text = value;
				this.ResizeMe();
			}
		}

		// Token: 0x060006D1 RID: 1745 RVA: 0x00036624 File Offset: 0x00035624
		private void listBox1_KeyUp(object sender, KeyEventArgs e)
		{
			if (e.Control && e.KeyCode == Keys.A)
			{
				ListBox listBox = (ListBox)sender;
				for (int i = 0; i < listBox.Items.Count; i++)
				{
					listBox.SetSelected(i, true);
				}
			}
		}

		// Token: 0x060006D2 RID: 1746 RVA: 0x0003667C File Offset: 0x0003567C
		private void listBox2_DoubleClick(object sender, EventArgs e)
		{
			this.moveSelected(this.listBox2, this.listBox1);
		}

		// Token: 0x060006D3 RID: 1747 RVA: 0x00036692 File Offset: 0x00035692
		private void listBox1_DoubleClick(object sender, EventArgs e)
		{
			this.moveSelected(this.listBox1, this.listBox2);
		}

		// Token: 0x060006D4 RID: 1748 RVA: 0x000366A8 File Offset: 0x000356A8
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060006D5 RID: 1749 RVA: 0x000366E0 File Offset: 0x000356E0
		private void InitializeComponent()
		{
			this.listBox1 = new ListBox();
			this.listBox2 = new ListBox();
			this.btm_RemoveFromList1 = new Button();
			this.btm_AddToList1 = new Button();
			this.btm_panel = new Panel();
			this.panel2 = new Panel();
			this.label2 = new Label();
			this.panel1 = new Panel();
			this.label1 = new Label();
			this.cmb = new MyMultiCheckBox2();
			this.btm_panel.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel1.SuspendLayout();
			base.SuspendLayout();
			this.listBox1.Dock = DockStyle.Fill;
			this.listBox1.FormattingEnabled = true;
			this.listBox1.Location = new Point(0, 13);
			this.listBox1.Name = "listBox1";
			this.listBox1.SelectionMode = SelectionMode.MultiSimple;
			this.listBox1.Size = new Size(101, 197);
			this.listBox1.TabIndex = 0;
			this.listBox1.DoubleClick += this.listBox1_DoubleClick;
			this.listBox1.KeyUp += this.listBox1_KeyUp;
			this.listBox2.Dock = DockStyle.Fill;
			this.listBox2.FormattingEnabled = true;
			this.listBox2.Location = new Point(0, 13);
			this.listBox2.Name = "listBox2";
			this.listBox2.SelectionMode = SelectionMode.MultiSimple;
			this.listBox2.Size = new Size(101, 197);
			this.listBox2.TabIndex = 1;
			this.listBox2.DoubleClick += this.listBox2_DoubleClick;
			this.listBox2.KeyUp += this.listBox1_KeyUp;
			this.btm_RemoveFromList1.AccessibleDescription = "Remove from list";
			this.btm_RemoveFromList1.AccessibleName = "Remove from list";
			this.btm_RemoveFromList1.Anchor = AnchorStyles.None;
			this.btm_RemoveFromList1.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.btm_RemoveFromList1.Location = new Point(5, 163);
			this.btm_RemoveFromList1.Name = "btm_RemoveFromList1";
			this.btm_RemoveFromList1.Size = new Size(28, 32);
			this.btm_RemoveFromList1.TabIndex = 2;
			this.btm_RemoveFromList1.Text = ">";
			this.btm_RemoveFromList1.UseVisualStyleBackColor = true;
			this.btm_RemoveFromList1.Click += this.btm_RemoveFromList1_Click_1;
			this.btm_AddToList1.AccessibleDescription = "Add to list";
			this.btm_AddToList1.AccessibleName = "Add to list";
			this.btm_AddToList1.Anchor = AnchorStyles.None;
			this.btm_AddToList1.Font = new Font("Microsoft Sans Serif", 15f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.btm_AddToList1.Location = new Point(5, 110);
			this.btm_AddToList1.Name = "btm_AddToList1";
			this.btm_AddToList1.Size = new Size(28, 32);
			this.btm_AddToList1.TabIndex = 3;
			this.btm_AddToList1.Text = "<";
			this.btm_AddToList1.UseVisualStyleBackColor = true;
			this.btm_AddToList1.Click += this.btm_AddToList1_Click;
			this.btm_panel.Controls.Add(this.btm_AddToList1);
			this.btm_panel.Controls.Add(this.btm_RemoveFromList1);
			this.btm_panel.Dock = DockStyle.Left;
			this.btm_panel.Location = new Point(101, 0);
			this.btm_panel.Name = "btm_panel";
			this.btm_panel.Size = new Size(38, 210);
			this.btm_panel.TabIndex = 4;
			this.panel2.Controls.Add(this.listBox2);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Dock = DockStyle.Left;
			this.panel2.Location = new Point(139, 0);
			this.panel2.Name = "panel2";
			this.panel2.Size = new Size(101, 210);
			this.panel2.TabIndex = 5;
			this.label2.Dock = DockStyle.Top;
			this.label2.Location = new Point(0, 0);
			this.label2.Name = "label2";
			this.label2.Size = new Size(101, 13);
			this.label2.TabIndex = 2;
			this.panel1.Controls.Add(this.listBox1);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = DockStyle.Left;
			this.panel1.Location = new Point(0, 0);
			this.panel1.Name = "panel1";
			this.panel1.Size = new Size(101, 210);
			this.panel1.TabIndex = 6;
			this.label1.Dock = DockStyle.Top;
			this.label1.Location = new Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Size = new Size(101, 13);
			this.label1.TabIndex = 1;
			this.cmb.CheckOnClick = true;
			this.cmb.DrawMode = DrawMode.OwnerDrawVariable;
			this.cmb.DropDownHeight = 1;
			this.cmb.FormattingEnabled = true;
			this.cmb.IntegralHeight = false;
			this.cmb.Location = new Point(247, 120);
			this.cmb.Name = "cmb";
			this.cmb.Size = new Size(121, 21);
			this.cmb.TabIndex = 7;
			this.cmb.ValueSeparator = ", ";
			this.cmb.Visible = false;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.cmb);
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.btm_panel);
			base.Controls.Add(this.panel1);
			base.Name = "ListSelect";
			base.Size = new Size(269, 210);
			base.SizeChanged += this.ListSelect_SizeChanged;
			this.btm_panel.ResumeLayout(false);
			this.panel2.ResumeLayout(false);
			this.panel1.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x04000551 RID: 1361
		private const int HORZ_PADDING = 10;

		// Token: 0x04000552 RID: 1362
		private const int VERT_PADDING = 10;

		// Token: 0x04000553 RID: 1363
		private ListSelect.DisplayType displayType = ListSelect.DisplayType.normal;

		// Token: 0x04000554 RID: 1364
		private IContainer components = null;

		// Token: 0x04000555 RID: 1365
		private ListBox listBox1;

		// Token: 0x04000556 RID: 1366
		private ListBox listBox2;

		// Token: 0x04000557 RID: 1367
		private Button btm_RemoveFromList1;

		// Token: 0x04000558 RID: 1368
		private Button btm_AddToList1;

		// Token: 0x04000559 RID: 1369
		private Panel btm_panel;

		// Token: 0x0400055A RID: 1370
		private Panel panel2;

		// Token: 0x0400055B RID: 1371
		private Label label2;

		// Token: 0x0400055C RID: 1372
		private Panel panel1;

		// Token: 0x0400055D RID: 1373
		private Label label1;

		// Token: 0x0400055E RID: 1374
		private MyMultiCheckBox2 cmb;

		// Token: 0x020000B5 RID: 181
		private enum DisplayType
		{
			// Token: 0x04000560 RID: 1376
			normal,
			// Token: 0x04000561 RID: 1377
			combobox
		}
	}
}

using System;
using System.Collections;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox
{
	// Token: 0x02000040 RID: 64
	public class MyRadioGroup : UserControl
	{
		// Token: 0x06000237 RID: 567 RVA: 0x00012F78 File Offset: 0x00011F78
		public MyRadioGroup()
		{
			this.InitializeComponent();
			this.AutoScroll = false;
		}

		// Token: 0x06000238 RID: 568 RVA: 0x00012FD4 File Offset: 0x00011FD4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.components != null)
				{
					this.components.Dispose();
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x00013010 File Offset: 0x00012010
		private void InitializeComponent()
		{
			this.contextMenu1 = new ContextMenu();
			this.MENU_clearSelected = new MenuItem();
			this.p_rbs = new Panel();
			this.label1 = new Label();
			base.SuspendLayout();
			this.contextMenu1.MenuItems.AddRange(new MenuItem[]
			{
				this.MENU_clearSelected
			});
			this.MENU_clearSelected.Index = 0;
			this.MENU_clearSelected.Text = "&Clear selected";
			this.MENU_clearSelected.Click += this.MENU_clearSelected_Click;
			this.p_rbs.Dock = DockStyle.Fill;
			this.p_rbs.Location = new Point(0, 14);
			this.p_rbs.Name = "p_rbs";
			this.p_rbs.Size = new Size(282, 16);
			this.p_rbs.TabIndex = 0;
			this.label1.AutoSize = true;
			this.label1.Dock = DockStyle.Top;
			this.label1.Font = new Font("Arial", 8.25f, FontStyle.Bold, GraphicsUnit.Point, 0);
			this.label1.Location = new Point(0, 0);
			this.label1.Name = "label1";
			this.label1.Padding = new Padding(0, 0, 10, 0);
			this.label1.Size = new Size(41, 14);
			this.label1.TabIndex = 0;
			this.label1.Text = "Title";
			this.label1.TextAlign = ContentAlignment.MiddleLeft;
			this.label1.Visible = false;
			this.AutoScroll = true;
			this.BackColor = SystemColors.Control;
			this.ContextMenu = this.contextMenu1;
			base.Controls.Add(this.p_rbs);
			base.Controls.Add(this.label1);
			base.Name = "MyRadioGroup";
			base.Size = new Size(282, 30);
			base.KeyDown += this.MyRadioGroup_KeyDown;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x17000076 RID: 118
		// (get) Token: 0x0600023A RID: 570 RVA: 0x00013248 File Offset: 0x00012248
		// (set) Token: 0x0600023B RID: 571 RVA: 0x00013260 File Offset: 0x00012260
		public MyRadioGroup.DisplayFormat DisplayType
		{
			get
			{
				return this.displayFormat;
			}
			set
			{
				this.displayFormat = value;
				switch (this.displayFormat)
				{
				case MyRadioGroup.DisplayFormat.NoLabel:
				case MyRadioGroup.DisplayFormat.LabelLeftAsSeparateControl:
					this.label1.Visible = false;
					break;
				case MyRadioGroup.DisplayFormat.LabelAbove:
					this.label1.Dock = DockStyle.Top;
					this.label1.Visible = true;
					break;
				case MyRadioGroup.DisplayFormat.LabelLeft:
					this.label1.Dock = DockStyle.Left;
					this.label1.Visible = true;
					break;
				}
				this.OrganizeRadioButtons();
			}
		}

		// Token: 0x0600023C RID: 572 RVA: 0x000132E4 File Offset: 0x000122E4
		public void SetForeColour(Color c)
		{
			this.label1.ForeColor = c;
			ArrayList radioButtons = this.RadioButtons;
			foreach (object obj in radioButtons)
			{
				RadioButton radioButton = (RadioButton)obj;
				radioButton.ForeColor = c;
			}
		}

		// Token: 0x0600023D RID: 573 RVA: 0x00013360 File Offset: 0x00012360
		public void SetBackColour(Color c)
		{
			this.label1.BackColor = c;
		}

		// Token: 0x17000077 RID: 119
		// (get) Token: 0x0600023E RID: 574 RVA: 0x00013370 File Offset: 0x00012370
		// (set) Token: 0x0600023F RID: 575 RVA: 0x00013388 File Offset: 0x00012388
		public int NumHorizontal
		{
			get
			{
				return this.numHorizontal;
			}
			set
			{
				this.numHorizontal = value;
				this.OrganizeRadioButtons();
			}
		}

		// Token: 0x17000078 RID: 120
		// (get) Token: 0x06000240 RID: 576 RVA: 0x0001339C File Offset: 0x0001239C
		// (set) Token: 0x06000241 RID: 577 RVA: 0x000133B4 File Offset: 0x000123B4
		public int DefaultId
		{
			get
			{
				return this.defaultId;
			}
			set
			{
				this.defaultId = value;
			}
		}

		// Token: 0x17000079 RID: 121
		// (get) Token: 0x06000242 RID: 578 RVA: 0x000133C0 File Offset: 0x000123C0
		// (set) Token: 0x06000243 RID: 579 RVA: 0x000133D8 File Offset: 0x000123D8
		public DataView DataSource
		{
			get
			{
				return this.dataSource;
			}
			set
			{
				this.dataSource = value;
				this.CreateRadioButtons();
				this.OrganizeRadioButtons();
			}
		}

		// Token: 0x1700007A RID: 122
		// (get) Token: 0x06000244 RID: 580 RVA: 0x000133F0 File Offset: 0x000123F0
		// (set) Token: 0x06000245 RID: 581 RVA: 0x00013408 File Offset: 0x00012408
		public string DisplayMember
		{
			get
			{
				return this.displayMember;
			}
			set
			{
				this.displayMember = value;
				this.CreateRadioButtons();
				this.OrganizeRadioButtons();
			}
		}

		// Token: 0x1700007B RID: 123
		// (get) Token: 0x06000246 RID: 582 RVA: 0x00013420 File Offset: 0x00012420
		// (set) Token: 0x06000247 RID: 583 RVA: 0x00013438 File Offset: 0x00012438
		public string ValueMember
		{
			get
			{
				return this.valueMember;
			}
			set
			{
				this.valueMember = value;
			}
		}

		// Token: 0x1700007C RID: 124
		// (get) Token: 0x06000248 RID: 584 RVA: 0x00013444 File Offset: 0x00012444
		public int ValueMemberIndex
		{
			get
			{
				return (this.valueMember != null && this.valueMember.Trim().Length > 0 && this.dataSource != null && this.dataSource.Table.Columns.Contains(this.valueMember)) ? this.dataSource.Table.Columns.IndexOf(this.valueMember) : -1;
			}
		}

		// Token: 0x1700007D RID: 125
		// (get) Token: 0x06000249 RID: 585 RVA: 0x000134B8 File Offset: 0x000124B8
		public ArrayList RadioButtons
		{
			get
			{
				ArrayList arrayList = new ArrayList();
				foreach (object obj in this.p_rbs.Controls)
				{
					Control control = (Control)obj;
					if (control is RadioButton)
					{
						arrayList.Add(control);
					}
				}
				return arrayList;
			}
		}

		// Token: 0x1700007E RID: 126
		// (get) Token: 0x0600024A RID: 586 RVA: 0x00013548 File Offset: 0x00012548
		public string SelectedText
		{
			get
			{
				int selectedIndex = this.SelectedIndex;
				string result;
				if (selectedIndex >= 0 && this.displayMember.Trim().Length > 0 && this.dataSource != null && this.dataSource.Table.Columns.Contains(this.displayMember))
				{
					ArrayList radioButtons = this.RadioButtons;
					RadioButton radioButton = (RadioButton)radioButtons[selectedIndex];
					DataRow dataRow = (DataRow)radioButton.Tag;
					result = dataRow[this.displayMember].ToString();
				}
				else
				{
					result = "";
				}
				return result;
			}
		}

		// Token: 0x1700007F RID: 127
		// (get) Token: 0x0600024B RID: 587 RVA: 0x000135E4 File Offset: 0x000125E4
		// (set) Token: 0x0600024C RID: 588 RVA: 0x00013601 File Offset: 0x00012601
		public string Title
		{
			get
			{
				return this.label1.Text;
			}
			set
			{
				this.label1.Text = value;
			}
		}

		// Token: 0x17000080 RID: 128
		// (get) Token: 0x0600024D RID: 589 RVA: 0x00013614 File Offset: 0x00012614
		public bool FilledIn
		{
			get
			{
				return this.SelectedId > -1;
			}
		}

		// Token: 0x17000081 RID: 129
		// (get) Token: 0x0600024E RID: 590 RVA: 0x00013630 File Offset: 0x00012630
		// (set) Token: 0x0600024F RID: 591 RVA: 0x000136C4 File Offset: 0x000126C4
		public int SelectedId
		{
			get
			{
				ArrayList radioButtons = this.RadioButtons;
				foreach (object obj in radioButtons)
				{
					RadioButton radioButton = (RadioButton)obj;
					if (radioButton.Checked)
					{
						return this.GetValue((DataRow)radioButton.Tag);
					}
				}
				return -1;
			}
			set
			{
				if (value < 0)
				{
					this.ClearCheckedRadioButtons();
				}
				else if (this.dataSource != null)
				{
					int valueMemberIndex = this.ValueMemberIndex;
					if (this.dataSource.Table.Columns[valueMemberIndex].DataType == typeof(int))
					{
						ArrayList radioButtons = this.RadioButtons;
						foreach (object obj in radioButtons)
						{
							RadioButton radioButton = (RadioButton)obj;
							DataRow dataRow = (DataRow)radioButton.Tag;
							if (dataRow[valueMemberIndex] != DBNull.Value)
							{
								int num = (int)dataRow[valueMemberIndex];
								if (num == value)
								{
									radioButton.Checked = true;
									return;
								}
							}
						}
						this.ClearCheckedRadioButtons();
					}
				}
			}
		}

		// Token: 0x17000082 RID: 130
		// (get) Token: 0x06000250 RID: 592 RVA: 0x000137F0 File Offset: 0x000127F0
		// (set) Token: 0x06000251 RID: 593 RVA: 0x00013844 File Offset: 0x00012844
		public int SelectedIndex
		{
			get
			{
				ArrayList radioButtons = this.RadioButtons;
				for (int i = 0; i < radioButtons.Count; i++)
				{
					RadioButton radioButton = (RadioButton)radioButtons[i];
					if (radioButton.Checked)
					{
						return i;
					}
				}
				return -1;
			}
			set
			{
				ArrayList radioButtons = this.RadioButtons;
				if (value >= 0 && value < radioButtons.Count)
				{
					RadioButton radioButton = (RadioButton)radioButtons[value];
					radioButton.Checked = true;
				}
				else
				{
					foreach (object obj in radioButtons)
					{
						RadioButton radioButton = (RadioButton)obj;
						radioButton.Checked = false;
					}
				}
			}
		}

		// Token: 0x06000252 RID: 594 RVA: 0x000138E8 File Offset: 0x000128E8
		public int GetValue(DataRow dr)
		{
			int result;
			if (this.dataSource != null)
			{
				int valueMemberIndex = this.ValueMemberIndex;
				if (valueMemberIndex >= 0 && dr[valueMemberIndex] != DBNull.Value && dr[valueMemberIndex].GetType() == typeof(int))
				{
					result = (int)dr[valueMemberIndex];
				}
				else
				{
					result = -1;
				}
			}
			else
			{
				result = -1;
			}
			return result;
		}

		// Token: 0x06000253 RID: 595 RVA: 0x00013958 File Offset: 0x00012958
		private void ClearRadioButtons()
		{
			ArrayList radioButtons = this.RadioButtons;
			foreach (object obj in radioButtons)
			{
				RadioButton radioButton = (RadioButton)obj;
				radioButton.Tag = null;
				radioButton.ContextMenu = null;
				this.p_rbs.Controls.Remove(radioButton);
				radioButton.Dispose();
			}
			radioButtons.Clear();
		}

		// Token: 0x06000254 RID: 596 RVA: 0x000139EC File Offset: 0x000129EC
		public void ClearCheckedRadioButtons()
		{
			ArrayList radioButtons = this.RadioButtons;
			foreach (object obj in radioButtons)
			{
				RadioButton radioButton = (RadioButton)obj;
				radioButton.Checked = false;
			}
		}

		// Token: 0x06000255 RID: 597 RVA: 0x00013A58 File Offset: 0x00012A58
		public void CheckRadioButton(MyRadioButton rbtn)
		{
			ArrayList radioButtons = this.RadioButtons;
			foreach (object obj in radioButtons)
			{
				RadioButton radioButton = (RadioButton)obj;
				if (radioButton != rbtn)
				{
					if (radioButton.Checked)
					{
						radioButton.Checked = false;
						radioButton.Invalidate();
					}
				}
				else if (!radioButton.Checked)
				{
					radioButton.Checked = true;
					radioButton.Invalidate();
				}
			}
		}

		// Token: 0x06000256 RID: 598 RVA: 0x00013B04 File Offset: 0x00012B04
		private void CreateRadioButtons()
		{
			if (this.displayMember != null && this.displayMember.Trim().Length > 0 && this.dataSource != null && this.dataSource.Table.Columns.Contains(this.displayMember))
			{
				this.ClearRadioButtons();
				int valueMemberIndex = this.ValueMemberIndex;
				for (int i = 0; i < this.dataSource.Count; i++)
				{
					DataRowView dataRowView = this.dataSource[i];
					DataRow row = dataRowView.Row;
					string text = row[this.displayMember].ToString();
					MyRadioButton myRadioButton = new MyRadioButton();
					myRadioButton.TabStop = true;
					myRadioButton.Text = text;
					myRadioButton.TabStop = (i == 0);
					myRadioButton.BackColor = this.BackColor;
					if (i == 0)
					{
						myRadioButton.AccessibleName = string.Format("{0}: {1}", this.Title, text);
					}
					else
					{
						myRadioButton.AccessibleName = text;
					}
					myRadioButton.AccessibleDescription = myRadioButton.AccessibleName;
					if (this.defaultId > -1 && valueMemberIndex >= 0 && row[valueMemberIndex] != DBNull.Value && row[valueMemberIndex].GetType() == typeof(int))
					{
						int num = (int)row[valueMemberIndex];
						if (num == this.defaultId)
						{
							myRadioButton.Checked = true;
						}
					}
					myRadioButton.ContextMenu = this.contextMenu1;
					myRadioButton.Tag = row;
					myRadioButton.AutoSizeHeight = true;
					this.p_rbs.Controls.Add(myRadioButton);
				}
			}
		}

		// Token: 0x06000257 RID: 599 RVA: 0x00013CD4 File Offset: 0x00012CD4
		public string GetText(int id)
		{
			foreach (object obj in this.dataSource)
			{
				DataRowView dataRowView = (DataRowView)obj;
				DataRow row = dataRowView.Row;
				if (row[this.valueMember] != DBNull.Value && row[this.valueMember] is int && (int)row[this.valueMember] == id)
				{
					return row[this.displayMember].ToString();
				}
			}
			return "";
		}

		// Token: 0x06000258 RID: 600 RVA: 0x00013DA8 File Offset: 0x00012DA8
		private int GetRadioWidth()
		{
			int num;
			if (this.p_rbs.ClientRectangle.Width > 0)
			{
				num = ((this.numHorizontal > 1) ? Convert.ToInt32(this.p_rbs.ClientSize.Width / this.numHorizontal) : this.p_rbs.ClientSize.Width);
			}
			else
			{
				num = 10;
			}
			num--;
			if (num < 0)
			{
				num = 10;
			}
			return num;
		}

		// Token: 0x06000259 RID: 601 RVA: 0x00013E34 File Offset: 0x00012E34
		protected override void OnSizeChanged(EventArgs e)
		{
			base.OnSizeChanged(e);
			if (base.Size != this.lastSize)
			{
				this.lastSize = base.Size;
				this.OrganizeRadioButtons();
			}
		}

		// Token: 0x0600025A RID: 602 RVA: 0x00013E78 File Offset: 0x00012E78
		private void OrganizeRadioButtons()
		{
			int radioWidth = this.GetRadioWidth();
			int left = this.p_rbs.ClientRectangle.Left;
			int num = left;
			int top = this.p_rbs.ClientRectangle.Top;
			int num2 = top;
			int num3 = 4;
			int num4 = 4;
			int num5 = 0;
			ArrayList radioButtons = this.RadioButtons;
			int num6;
			if (this.numHorizontal <= 1)
			{
				num6 = 0;
			}
			else
			{
				num6 = Convert.ToInt32(radioButtons.Count / this.numHorizontal);
				if (radioButtons.Count % this.numHorizontal > 0)
				{
					num6++;
				}
			}
			foreach (object obj in radioButtons)
			{
				RadioButton radioButton = (RadioButton)obj;
				int height = radioButton.Height;
				radioButton.Width = radioWidth;
				radioButton.Left = num;
				radioButton.Top = num2;
				num5++;
				if (num6 <= 0 || num5 < num6)
				{
					num2 += height + num4;
				}
				else
				{
					num += radioWidth + num3;
					num2 = top;
					num5 = 0;
				}
			}
			this.ResizeGroupPanel(num4);
		}

		// Token: 0x0600025B RID: 603 RVA: 0x00013FDC File Offset: 0x00012FDC
		private void ResizeGroupPanel(int vPad)
		{
			int num = 0;
			foreach (object obj in this.p_rbs.Controls)
			{
				Control control = (Control)obj;
				int num2 = control.Top + control.Height;
				if (num2 > num)
				{
					num = num2;
				}
			}
			if (this.label1.Visible && this.displayFormat == MyRadioGroup.DisplayFormat.LabelAbove)
			{
				num += this.label1.Height;
			}
			num += vPad * 2;
			if (num > 0)
			{
				base.Height = num;
			}
		}

		// Token: 0x0600025C RID: 604 RVA: 0x000140B4 File Offset: 0x000130B4
		private void MENU_clearSelected_Click(object sender, EventArgs e)
		{
			this.ClearCheckedRadioButtons();
		}

		// Token: 0x0600025D RID: 605 RVA: 0x000140BE File Offset: 0x000130BE
		private void MyRadioGroup_KeyDown(object sender, KeyEventArgs e)
		{
		}

		// Token: 0x040001E1 RID: 481
		private Container components = null;

		// Token: 0x040001E2 RID: 482
		private int defaultId = -1;

		// Token: 0x040001E3 RID: 483
		private DataView dataSource = null;

		// Token: 0x040001E4 RID: 484
		private string displayMember = "";

		// Token: 0x040001E5 RID: 485
		private string valueMember = "";

		// Token: 0x040001E6 RID: 486
		private ContextMenu contextMenu1;

		// Token: 0x040001E7 RID: 487
		private MenuItem MENU_clearSelected;

		// Token: 0x040001E8 RID: 488
		private Panel p_rbs;

		// Token: 0x040001E9 RID: 489
		private Label label1;

		// Token: 0x040001EA RID: 490
		private int numHorizontal;

		// Token: 0x040001EB RID: 491
		private MyRadioGroup.DisplayFormat displayFormat;

		// Token: 0x040001EC RID: 492
		private Size lastSize = Size.Empty;

		// Token: 0x02000041 RID: 65
		public enum DisplayFormat
		{
			// Token: 0x040001EE RID: 494
			NoLabel,
			// Token: 0x040001EF RID: 495
			LabelAbove,
			// Token: 0x040001F0 RID: 496
			LabelLeft,
			// Token: 0x040001F1 RID: 497
			LabelLeftAsSeparateControl
		}
	}
}

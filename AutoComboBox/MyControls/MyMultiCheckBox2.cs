using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace AutoComboBox.MyControls
{
	// Token: 0x02000034 RID: 52
	public class MyMultiCheckBox2 : ComboBox
	{
		// Token: 0x17000041 RID: 65
		// (get) Token: 0x060001AC RID: 428 RVA: 0x00010248 File Offset: 0x0000F248
		// (set) Token: 0x060001AD RID: 429 RVA: 0x00010260 File Offset: 0x0000F260
		public string ValueSeparator
		{
			get
			{
				return this.valueSeparator;
			}
			set
			{
				this.valueSeparator = value;
			}
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x060001AE RID: 430 RVA: 0x0001026C File Offset: 0x0000F26C
		// (set) Token: 0x060001AF RID: 431 RVA: 0x0001028E File Offset: 0x0000F28E
		public bool CheckOnClick
		{
			get
			{
				return this.dropdown.List.CheckOnClick;
			}
			set
			{
				this.dropdown.List.CheckOnClick = value;
			}
		}

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x060001B0 RID: 432 RVA: 0x000102A4 File Offset: 0x0000F2A4
		// (set) Token: 0x060001B1 RID: 433 RVA: 0x000102C6 File Offset: 0x0000F2C6
		public new string DisplayMember
		{
			get
			{
				return this.dropdown.List.DisplayMember;
			}
			set
			{
				this.dropdown.List.DisplayMember = value;
			}
		}

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x060001B2 RID: 434 RVA: 0x000102DC File Offset: 0x0000F2DC
		public new CheckedListBox.ObjectCollection Items
		{
			get
			{
				return this.dropdown.List.Items;
			}
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x060001B3 RID: 435 RVA: 0x00010300 File Offset: 0x0000F300
		public CheckedListBox.CheckedItemCollection CheckedItems
		{
			get
			{
				return this.dropdown.List.CheckedItems;
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x060001B4 RID: 436 RVA: 0x00010324 File Offset: 0x0000F324
		public CheckedListBox.CheckedIndexCollection CheckedIndices
		{
			get
			{
				return this.dropdown.List.CheckedIndices;
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x060001B5 RID: 437 RVA: 0x00010348 File Offset: 0x0000F348
		public bool ValueChanged
		{
			get
			{
				return this.dropdown.ValueChanged;
			}
		}

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x060001B6 RID: 438 RVA: 0x00010368 File Offset: 0x0000F368
		// (remove) Token: 0x060001B7 RID: 439 RVA: 0x000103A4 File Offset: 0x0000F3A4
		public event ItemCheckEventHandler ItemCheck;

		// Token: 0x060001B8 RID: 440 RVA: 0x000103E0 File Offset: 0x0000F3E0
		public MyMultiCheckBox2()
		{
			base.DrawMode = DrawMode.OwnerDrawVariable;
			this.valueSeparator = ", ";
			base.DropDownHeight = 1;
			base.DropDownStyle = ComboBoxStyle.DropDown;
			this.dropdown = new MyMultiCheckBox2.Dropdown(this);
			this.CheckOnClick = true;
		}

		// Token: 0x060001B9 RID: 441 RVA: 0x00010434 File Offset: 0x0000F434
		protected override void OnDropDown(EventArgs e)
		{
			base.OnDropDown(e);
			this.DoDropDown();
		}

		// Token: 0x060001BA RID: 442 RVA: 0x00010448 File Offset: 0x0000F448
		private void DoDropDown()
		{
			if (!this.dropdown.Visible)
			{
				Rectangle rectangle = base.RectangleToScreen(base.ClientRectangle);
				this.dropdown.Location = new Point(rectangle.X, rectangle.Y + base.Size.Height);
				int num = this.dropdown.List.Items.Count;
				if (num > base.MaxDropDownItems)
				{
					num = base.MaxDropDownItems;
				}
				else if (num == 0)
				{
					num = 1;
				}
				this.dropdown.Size = new Size(base.Size.Width, this.dropdown.List.ItemHeight * num + 2);
				this.dropdown.Show(this);
			}
		}

		// Token: 0x060001BB RID: 443 RVA: 0x00010528 File Offset: 0x0000F528
		protected override void OnDropDownClosed(EventArgs e)
		{
			if (e is MyMultiCheckBox2.Dropdown.CCBoxEventArgs)
			{
				base.OnDropDownClosed(e);
			}
		}

		// Token: 0x060001BC RID: 444 RVA: 0x00010550 File Offset: 0x0000F550
		protected override void OnKeyDown(KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Down)
			{
				this.OnDropDown(null);
			}
			e.Handled = (!e.Alt && e.KeyCode != Keys.Tab && (e.KeyCode != Keys.Left && e.KeyCode != Keys.Right && e.KeyCode != Keys.Home) && e.KeyCode != Keys.End);
			base.OnKeyDown(e);
		}

		// Token: 0x060001BD RID: 445 RVA: 0x000105CB File Offset: 0x0000F5CB
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			e.Handled = true;
			base.OnKeyPress(e);
		}

		// Token: 0x060001BE RID: 446 RVA: 0x000105E0 File Offset: 0x0000F5E0
		public bool GetItemChecked(int index)
		{
			if (index < 0 || index > this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("index", "value out of range");
			}
			return this.dropdown.List.GetItemChecked(index);
		}

		// Token: 0x060001BF RID: 447 RVA: 0x00010634 File Offset: 0x0000F634
		public void SetItemChecked(int index, bool isChecked)
		{
			if (index < 0 || index > this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("index", "value out of range");
			}
			this.dropdown.List.SetItemChecked(index, isChecked);
			this.Text = this.dropdown.GetCheckedItemsStringValue();
		}

		// Token: 0x060001C0 RID: 448 RVA: 0x00010698 File Offset: 0x0000F698
		public CheckState GetItemCheckState(int index)
		{
			if (index < 0 || index > this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("index", "value out of range");
			}
			return this.dropdown.List.GetItemCheckState(index);
		}

		// Token: 0x060001C1 RID: 449 RVA: 0x000106EC File Offset: 0x0000F6EC
		public void SetItemCheckState(int index, CheckState state)
		{
			if (index < 0 || index > this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("index", "value out of range");
			}
			this.dropdown.List.SetItemCheckState(index, state);
			this.Text = this.dropdown.GetCheckedItemsStringValue();
		}

		// Token: 0x060001C2 RID: 450 RVA: 0x00010750 File Offset: 0x0000F750
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060001C3 RID: 451 RVA: 0x00010787 File Offset: 0x0000F787
		private void InitializeComponent()
		{
			this.components = new Container();
		}

		// Token: 0x040001B7 RID: 439
		private MyMultiCheckBox2.Dropdown dropdown;

		// Token: 0x040001B8 RID: 440
		private string valueSeparator;

		// Token: 0x040001BA RID: 442
		private IContainer components = null;

		// Token: 0x02000035 RID: 53
		internal class Dropdown : Form
		{
			// Token: 0x17000048 RID: 72
			// (get) Token: 0x060001C4 RID: 452 RVA: 0x00010798 File Offset: 0x0000F798
			public bool ValueChanged
			{
				get
				{
					string text = this.ccbParent.Text;
					bool result;
					if (this.oldStrValue.Length > 0 && text.Length > 0)
					{
						result = (this.oldStrValue.CompareTo(text) != 0);
					}
					else
					{
						result = (this.oldStrValue.Length != text.Length);
					}
					return result;
				}
			}

			// Token: 0x17000049 RID: 73
			// (get) Token: 0x060001C5 RID: 453 RVA: 0x00010804 File Offset: 0x0000F804
			// (set) Token: 0x060001C6 RID: 454 RVA: 0x0001081C File Offset: 0x0000F81C
			public MyMultiCheckBox2.Dropdown.CustomCheckedListBox List
			{
				get
				{
					return this.cclb;
				}
				set
				{
					this.cclb = value;
				}
			}

			// Token: 0x060001C7 RID: 455 RVA: 0x00010828 File Offset: 0x0000F828
			public Dropdown(MyMultiCheckBox2 ccbParent)
			{
				this.ccbParent = ccbParent;
				this.InitializeComponent();
				base.ShowInTaskbar = false;
				this.cclb.ItemCheck += this.cclb_ItemCheck;
			}

			// Token: 0x060001C8 RID: 456 RVA: 0x00010880 File Offset: 0x0000F880
			private void InitializeComponent()
			{
				this.cclb = new MyMultiCheckBox2.Dropdown.CustomCheckedListBox();
				base.SuspendLayout();
				this.cclb.BorderStyle = BorderStyle.None;
				this.cclb.Dock = DockStyle.Fill;
				this.cclb.FormattingEnabled = true;
				this.cclb.Location = new Point(0, 0);
				this.cclb.Name = "cclb";
				this.cclb.Size = new Size(47, 15);
				this.cclb.TabIndex = 0;
				base.AutoScaleDimensions = new SizeF(6f, 13f);
				base.AutoScaleMode = AutoScaleMode.Font;
				this.BackColor = SystemColors.Menu;
				base.ClientSize = new Size(47, 16);
				base.ControlBox = false;
				base.Controls.Add(this.cclb);
				this.ForeColor = SystemColors.ControlText;
				base.FormBorderStyle = FormBorderStyle.FixedToolWindow;
				base.MinimizeBox = false;
				base.Name = "ccbParent";
				base.StartPosition = FormStartPosition.Manual;
				base.ResumeLayout(false);
			}

			// Token: 0x060001C9 RID: 457 RVA: 0x0001099C File Offset: 0x0000F99C
			public string GetCheckedItemsStringValue()
			{
				StringBuilder stringBuilder = new StringBuilder("");
				for (int i = 0; i < this.cclb.CheckedItems.Count; i++)
				{
					stringBuilder.Append(this.cclb.GetItemText(this.cclb.CheckedItems[i])).Append(this.ccbParent.ValueSeparator);
				}
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Remove(stringBuilder.Length - this.ccbParent.ValueSeparator.Length, this.ccbParent.ValueSeparator.Length);
				}
				return stringBuilder.ToString();
			}

			// Token: 0x060001CA RID: 458 RVA: 0x00010A54 File Offset: 0x0000FA54
			public void CloseDropdown(bool enactChanges)
			{
				if (!this.dropdownClosed)
				{
					if (enactChanges)
					{
						this.ccbParent.SelectedIndex = -1;
						this.ccbParent.Text = this.GetCheckedItemsStringValue();
					}
					else
					{
						for (int i = 0; i < this.cclb.Items.Count; i++)
						{
							this.cclb.SetItemChecked(i, this.checkedStateArr[i]);
						}
					}
					this.dropdownClosed = true;
					this.ccbParent.Focus();
					base.Hide();
					this.ccbParent.OnDropDownClosed(new MyMultiCheckBox2.Dropdown.CCBoxEventArgs(null, false));
				}
			}

			// Token: 0x060001CB RID: 459 RVA: 0x00010B08 File Offset: 0x0000FB08
			protected override void OnActivated(EventArgs e)
			{
				base.OnActivated(e);
				this.dropdownClosed = false;
				this.oldStrValue = this.ccbParent.Text;
				this.checkedStateArr = new bool[this.cclb.Items.Count];
				for (int i = 0; i < this.cclb.Items.Count; i++)
				{
					this.checkedStateArr[i] = this.cclb.GetItemChecked(i);
				}
			}

			// Token: 0x060001CC RID: 460 RVA: 0x00010B88 File Offset: 0x0000FB88
			protected override void OnDeactivate(EventArgs e)
			{
				base.OnDeactivate(e);
				MyMultiCheckBox2.Dropdown.CCBoxEventArgs ccboxEventArgs = e as MyMultiCheckBox2.Dropdown.CCBoxEventArgs;
				if (ccboxEventArgs != null)
				{
					this.CloseDropdown(ccboxEventArgs.AssignValues);
				}
				else
				{
					this.CloseDropdown(true);
				}
			}

			// Token: 0x060001CD RID: 461 RVA: 0x00010BC8 File Offset: 0x0000FBC8
			private void cclb_ItemCheck(object sender, ItemCheckEventArgs e)
			{
				if (this.ccbParent.ItemCheck != null)
				{
					this.ccbParent.ItemCheck(sender, e);
				}
			}

			// Token: 0x040001BB RID: 443
			private MyMultiCheckBox2 ccbParent;

			// Token: 0x040001BC RID: 444
			private string oldStrValue = "";

			// Token: 0x040001BD RID: 445
			private bool[] checkedStateArr;

			// Token: 0x040001BE RID: 446
			private bool dropdownClosed = true;

			// Token: 0x040001BF RID: 447
			private MyMultiCheckBox2.Dropdown.CustomCheckedListBox cclb;

			// Token: 0x02000036 RID: 54
			internal class CCBoxEventArgs : EventArgs
			{
				// Token: 0x1700004A RID: 74
				// (get) Token: 0x060001CE RID: 462 RVA: 0x00010C00 File Offset: 0x0000FC00
				// (set) Token: 0x060001CF RID: 463 RVA: 0x00010C18 File Offset: 0x0000FC18
				public bool AssignValues
				{
					get
					{
						return this.assignValues;
					}
					set
					{
						this.assignValues = value;
					}
				}

				// Token: 0x1700004B RID: 75
				// (get) Token: 0x060001D0 RID: 464 RVA: 0x00010C24 File Offset: 0x0000FC24
				// (set) Token: 0x060001D1 RID: 465 RVA: 0x00010C3C File Offset: 0x0000FC3C
				public EventArgs EventArgs
				{
					get
					{
						return this.e;
					}
					set
					{
						this.e = value;
					}
				}

				// Token: 0x060001D2 RID: 466 RVA: 0x00010C46 File Offset: 0x0000FC46
				public CCBoxEventArgs(EventArgs e, bool assignValues)
				{
					this.e = e;
					this.assignValues = assignValues;
				}

				// Token: 0x040001C0 RID: 448
				private bool assignValues;

				// Token: 0x040001C1 RID: 449
				private EventArgs e;
			}

			// Token: 0x02000037 RID: 55
			internal class CustomCheckedListBox : CheckedListBox
			{
				// Token: 0x060001D3 RID: 467 RVA: 0x00010C5F File Offset: 0x0000FC5F
				public CustomCheckedListBox()
				{
					this.SelectionMode = SelectionMode.One;
					base.HorizontalScrollbar = true;
				}

				// Token: 0x060001D4 RID: 468 RVA: 0x00010C84 File Offset: 0x0000FC84
				protected override void OnKeyDown(KeyEventArgs e)
				{
					if (e.KeyCode == Keys.Return)
					{
						((MyMultiCheckBox2.Dropdown)base.Parent).OnDeactivate(new MyMultiCheckBox2.Dropdown.CCBoxEventArgs(null, true));
						e.Handled = true;
					}
					else if (e.KeyCode == Keys.Escape)
					{
						((MyMultiCheckBox2.Dropdown)base.Parent).OnDeactivate(new MyMultiCheckBox2.Dropdown.CCBoxEventArgs(null, false));
						e.Handled = true;
					}
					else if (e.KeyCode == Keys.Delete)
					{
						for (int i = 0; i < base.Items.Count; i++)
						{
							base.SetItemChecked(i, e.Shift);
						}
						e.Handled = true;
					}
					base.OnKeyDown(e);
				}

				// Token: 0x060001D5 RID: 469 RVA: 0x00010D4C File Offset: 0x0000FD4C
				protected override void OnMouseMove(MouseEventArgs e)
				{
					base.OnMouseMove(e);
					int num = base.IndexFromPoint(e.Location);
					if (num >= 0 && num != this.curSelIndex)
					{
						this.curSelIndex = num;
						base.SetSelected(num, true);
					}
				}

				// Token: 0x040001C2 RID: 450
				private int curSelIndex = -1;
			}
		}
	}
}

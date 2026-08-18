using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Security.Permissions;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200029E RID: 670
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public class BorderSidesEditor : UITypeEditor
	{
		// Token: 0x060019DE RID: 6622 RVA: 0x00093DC4 File Offset: 0x00091FC4
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			if (provider != null)
			{
				IWindowsFormsEditorService windowsFormsEditorService = (IWindowsFormsEditorService)provider.GetService(typeof(IWindowsFormsEditorService));
				if (windowsFormsEditorService != null)
				{
					if (this.borderSidesEditorUI == null)
					{
						this.borderSidesEditorUI = DpiHelper.CreateInstanceInSystemAwareContext<BorderSidesEditor.BorderSidesEditorUI>(() => new BorderSidesEditor.BorderSidesEditorUI(this));
					}
					this.borderSidesEditorUI.Start(windowsFormsEditorService, value);
					windowsFormsEditorService.DropDownControl(this.borderSidesEditorUI);
					if (this.borderSidesEditorUI.Value != null)
					{
						value = this.borderSidesEditorUI.Value;
					}
					this.borderSidesEditorUI.End();
				}
			}
			return value;
		}

		// Token: 0x060019DF RID: 6623 RVA: 0x0003DFAE File Offset: 0x0003C1AE
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.DropDown;
		}

		// Token: 0x040015C0 RID: 5568
		private BorderSidesEditor.BorderSidesEditorUI borderSidesEditorUI;

		// Token: 0x02000530 RID: 1328
		private class BorderSidesEditorUI : UserControl
		{
			// Token: 0x06003061 RID: 12385 RVA: 0x00109975 File Offset: 0x00107B75
			public BorderSidesEditorUI(BorderSidesEditor editor)
			{
				this.editor = editor;
				this.End();
				this.InitializeComponent();
				base.Size = base.PreferredSize;
			}

			// Token: 0x17000964 RID: 2404
			// (get) Token: 0x06003062 RID: 12386 RVA: 0x0010999C File Offset: 0x00107B9C
			public IWindowsFormsEditorService EditorService
			{
				get
				{
					return this.edSvc;
				}
			}

			// Token: 0x17000965 RID: 2405
			// (get) Token: 0x06003063 RID: 12387 RVA: 0x001099A4 File Offset: 0x00107BA4
			public object Value
			{
				get
				{
					return this.currentValue;
				}
			}

			// Token: 0x06003064 RID: 12388 RVA: 0x001099AC File Offset: 0x00107BAC
			public void End()
			{
				this.edSvc = null;
				this.originalValue = null;
				this.currentValue = null;
				this.updateCurrentValue = false;
			}

			// Token: 0x06003065 RID: 12389 RVA: 0x001099CA File Offset: 0x00107BCA
			protected override void OnGotFocus(EventArgs e)
			{
				base.OnGotFocus(e);
				this.noneCheckBox.Focus();
			}

			// Token: 0x06003066 RID: 12390 RVA: 0x001099E0 File Offset: 0x00107BE0
			private void InitializeComponent()
			{
				ComponentResourceManager componentResourceManager = new ComponentResourceManager(typeof(BorderSidesEditor));
				this.tableLayoutPanel1 = new TableLayoutPanel();
				this.noneCheckBox = new CheckBox();
				this.allCheckBox = new CheckBox();
				this.topCheckBox = new CheckBox();
				this.bottomCheckBox = new CheckBox();
				this.rightCheckBox = new CheckBox();
				this.leftCheckBox = new CheckBox();
				this.splitterLabel = new Label();
				this.tableLayoutPanel1.SuspendLayout();
				base.SuspendLayout();
				componentResourceManager.ApplyResources(this.tableLayoutPanel1, "tableLayoutPanel1");
				this.tableLayoutPanel1.AutoSizeMode = AutoSizeMode.GrowAndShrink;
				this.tableLayoutPanel1.BackColor = SystemColors.Window;
				this.tableLayoutPanel1.ColumnStyles.Add(new ColumnStyle());
				this.tableLayoutPanel1.Controls.Add(this.noneCheckBox, 0, 0);
				this.tableLayoutPanel1.Controls.Add(this.allCheckBox, 0, 2);
				this.tableLayoutPanel1.Controls.Add(this.topCheckBox, 0, 3);
				this.tableLayoutPanel1.Controls.Add(this.bottomCheckBox, 0, 4);
				this.tableLayoutPanel1.Controls.Add(this.rightCheckBox, 0, 6);
				this.tableLayoutPanel1.Controls.Add(this.leftCheckBox, 0, 5);
				this.tableLayoutPanel1.Controls.Add(this.splitterLabel, 0, 1);
				this.tableLayoutPanel1.Name = "tableLayoutPanel1";
				this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
				this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
				this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
				this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
				this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
				this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
				this.tableLayoutPanel1.RowStyles.Add(new RowStyle());
				this.tableLayoutPanel1.Margin = new Padding(0);
				componentResourceManager.ApplyResources(this.noneCheckBox, "noneCheckBox");
				this.noneCheckBox.Name = "noneCheckBox";
				this.noneCheckBox.Margin = new Padding(3, 3, 3, 1);
				componentResourceManager.ApplyResources(this.allCheckBox, "allCheckBox");
				this.allCheckBox.Name = "allCheckBox";
				this.allCheckBox.Margin = new Padding(3, 3, 3, 1);
				componentResourceManager.ApplyResources(this.topCheckBox, "topCheckBox");
				this.topCheckBox.Margin = new Padding(20, 1, 3, 1);
				this.topCheckBox.Name = "topCheckBox";
				componentResourceManager.ApplyResources(this.bottomCheckBox, "bottomCheckBox");
				this.bottomCheckBox.Margin = new Padding(20, 1, 3, 1);
				this.bottomCheckBox.Name = "bottomCheckBox";
				componentResourceManager.ApplyResources(this.rightCheckBox, "rightCheckBox");
				this.rightCheckBox.Margin = new Padding(20, 1, 3, 1);
				this.rightCheckBox.Name = "rightCheckBox";
				componentResourceManager.ApplyResources(this.leftCheckBox, "leftCheckBox");
				this.leftCheckBox.Margin = new Padding(20, 1, 3, 1);
				this.leftCheckBox.Name = "leftCheckBox";
				componentResourceManager.ApplyResources(this.splitterLabel, "splitterLabel");
				this.splitterLabel.BackColor = SystemColors.ControlDark;
				this.splitterLabel.Name = "splitterLabel";
				componentResourceManager.ApplyResources(this, "$this");
				base.Controls.Add(this.tableLayoutPanel1);
				base.Padding = new Padding(1, 1, 1, 1);
				base.AutoSizeMode = AutoSizeMode.GrowAndShrink;
				base.AutoScaleMode = AutoScaleMode.Font;
				base.AutoScaleDimensions = new SizeF(6f, 13f);
				this.tableLayoutPanel1.ResumeLayout(false);
				this.tableLayoutPanel1.PerformLayout();
				base.ResumeLayout(false);
				base.PerformLayout();
				this.rightCheckBox.CheckedChanged += this.rightCheckBox_CheckedChanged;
				this.leftCheckBox.CheckedChanged += this.leftCheckBox_CheckedChanged;
				this.bottomCheckBox.CheckedChanged += this.bottomCheckBox_CheckedChanged;
				this.topCheckBox.CheckedChanged += this.topCheckBox_CheckedChanged;
				this.noneCheckBox.CheckedChanged += this.noneCheckBox_CheckedChanged;
				this.allCheckBox.CheckedChanged += this.allCheckBox_CheckedChanged;
				this.noneCheckBox.Click += this.noneCheckBoxClicked;
				this.allCheckBox.Click += this.allCheckBoxClicked;
			}

			// Token: 0x06003067 RID: 12391 RVA: 0x00109EAC File Offset: 0x001080AC
			private void rightCheckBox_CheckedChanged(object sender, EventArgs e)
			{
				CheckBox checkBox = sender as CheckBox;
				if (checkBox.Checked)
				{
					this.noneCheckBox.Checked = false;
				}
				else if (this.allCheckBox.Checked)
				{
					this.allCheckBox.Checked = false;
				}
				this.UpdateCurrentValue();
			}

			// Token: 0x06003068 RID: 12392 RVA: 0x00109EF8 File Offset: 0x001080F8
			private void leftCheckBox_CheckedChanged(object sender, EventArgs e)
			{
				CheckBox checkBox = sender as CheckBox;
				if (checkBox.Checked)
				{
					this.noneCheckBox.Checked = false;
				}
				else if (this.allCheckBox.Checked)
				{
					this.allCheckBox.Checked = false;
				}
				this.UpdateCurrentValue();
			}

			// Token: 0x06003069 RID: 12393 RVA: 0x00109F44 File Offset: 0x00108144
			private void bottomCheckBox_CheckedChanged(object sender, EventArgs e)
			{
				CheckBox checkBox = sender as CheckBox;
				if (checkBox.Checked)
				{
					this.noneCheckBox.Checked = false;
				}
				else if (this.allCheckBox.Checked)
				{
					this.allCheckBox.Checked = false;
				}
				this.UpdateCurrentValue();
			}

			// Token: 0x0600306A RID: 12394 RVA: 0x00109F90 File Offset: 0x00108190
			private void topCheckBox_CheckedChanged(object sender, EventArgs e)
			{
				CheckBox checkBox = sender as CheckBox;
				if (checkBox.Checked)
				{
					this.noneCheckBox.Checked = false;
				}
				else if (this.allCheckBox.Checked)
				{
					this.allCheckBox.Checked = false;
				}
				this.UpdateCurrentValue();
			}

			// Token: 0x0600306B RID: 12395 RVA: 0x00109FDC File Offset: 0x001081DC
			private void noneCheckBox_CheckedChanged(object sender, EventArgs e)
			{
				CheckBox checkBox = sender as CheckBox;
				if (checkBox.Checked)
				{
					this.allCheckBox.Checked = false;
					this.topCheckBox.Checked = false;
					this.bottomCheckBox.Checked = false;
					this.leftCheckBox.Checked = false;
					this.rightCheckBox.Checked = false;
				}
				this.UpdateCurrentValue();
			}

			// Token: 0x0600306C RID: 12396 RVA: 0x0010A03C File Offset: 0x0010823C
			private void allCheckBox_CheckedChanged(object sender, EventArgs e)
			{
				CheckBox checkBox = sender as CheckBox;
				if (checkBox.Checked)
				{
					this.noneCheckBox.Checked = false;
					this.topCheckBox.Checked = true;
					this.bottomCheckBox.Checked = true;
					this.leftCheckBox.Checked = true;
					this.rightCheckBox.Checked = true;
				}
				this.UpdateCurrentValue();
			}

			// Token: 0x0600306D RID: 12397 RVA: 0x0010A09A File Offset: 0x0010829A
			private void noneCheckBoxClicked(object sender, EventArgs e)
			{
				if (this.noneChecked)
				{
					this.noneCheckBox.Checked = true;
				}
			}

			// Token: 0x0600306E RID: 12398 RVA: 0x0010A0B0 File Offset: 0x001082B0
			private void allCheckBoxClicked(object sender, EventArgs e)
			{
				if (this.allChecked)
				{
					this.allCheckBox.Checked = true;
				}
			}

			// Token: 0x0600306F RID: 12399 RVA: 0x0010A0C8 File Offset: 0x001082C8
			private void ResetCheckBoxState()
			{
				this.allCheckBox.Checked = false;
				this.noneCheckBox.Checked = false;
				this.topCheckBox.Checked = false;
				this.bottomCheckBox.Checked = false;
				this.leftCheckBox.Checked = false;
				this.rightCheckBox.Checked = false;
			}

			// Token: 0x06003070 RID: 12400 RVA: 0x0010A120 File Offset: 0x00108320
			private void SetCheckBoxCheckState(ToolStripStatusLabelBorderSides sides)
			{
				this.ResetCheckBoxState();
				if ((sides & ToolStripStatusLabelBorderSides.All) == ToolStripStatusLabelBorderSides.All)
				{
					this.allCheckBox.Checked = true;
					this.topCheckBox.Checked = true;
					this.bottomCheckBox.Checked = true;
					this.leftCheckBox.Checked = true;
					this.rightCheckBox.Checked = true;
					this.allCheckBox.Checked = true;
					return;
				}
				this.noneCheckBox.Checked = ((sides & ToolStripStatusLabelBorderSides.None) == ToolStripStatusLabelBorderSides.None);
				this.topCheckBox.Checked = ((sides & ToolStripStatusLabelBorderSides.Top) == ToolStripStatusLabelBorderSides.Top);
				this.bottomCheckBox.Checked = ((sides & ToolStripStatusLabelBorderSides.Bottom) == ToolStripStatusLabelBorderSides.Bottom);
				this.leftCheckBox.Checked = ((sides & ToolStripStatusLabelBorderSides.Left) == ToolStripStatusLabelBorderSides.Left);
				this.rightCheckBox.Checked = ((sides & ToolStripStatusLabelBorderSides.Right) == ToolStripStatusLabelBorderSides.Right);
			}

			// Token: 0x06003071 RID: 12401 RVA: 0x0010A1DC File Offset: 0x001083DC
			public void Start(IWindowsFormsEditorService edSvc, object value)
			{
				this.edSvc = edSvc;
				this.currentValue = value;
				this.originalValue = value;
				ToolStripStatusLabelBorderSides checkBoxCheckState = (ToolStripStatusLabelBorderSides)value;
				this.SetCheckBoxCheckState(checkBoxCheckState);
				this.updateCurrentValue = true;
			}

			// Token: 0x06003072 RID: 12402 RVA: 0x0010A218 File Offset: 0x00108418
			private void UpdateCurrentValue()
			{
				if (!this.updateCurrentValue)
				{
					return;
				}
				ToolStripStatusLabelBorderSides toolStripStatusLabelBorderSides = ToolStripStatusLabelBorderSides.None;
				if (this.allCheckBox.Checked)
				{
					toolStripStatusLabelBorderSides |= ToolStripStatusLabelBorderSides.All;
					this.currentValue = toolStripStatusLabelBorderSides;
					this.allChecked = true;
					this.noneChecked = false;
					return;
				}
				if (this.noneCheckBox.Checked)
				{
					toolStripStatusLabelBorderSides |= ToolStripStatusLabelBorderSides.None;
				}
				if (this.topCheckBox.Checked)
				{
					toolStripStatusLabelBorderSides |= ToolStripStatusLabelBorderSides.Top;
				}
				if (this.bottomCheckBox.Checked)
				{
					toolStripStatusLabelBorderSides |= ToolStripStatusLabelBorderSides.Bottom;
				}
				if (this.leftCheckBox.Checked)
				{
					toolStripStatusLabelBorderSides |= ToolStripStatusLabelBorderSides.Left;
				}
				if (this.rightCheckBox.Checked)
				{
					toolStripStatusLabelBorderSides |= ToolStripStatusLabelBorderSides.Right;
				}
				if (toolStripStatusLabelBorderSides == ToolStripStatusLabelBorderSides.None)
				{
					this.allChecked = false;
					this.noneChecked = true;
					this.noneCheckBox.Checked = true;
				}
				if (toolStripStatusLabelBorderSides == ToolStripStatusLabelBorderSides.All)
				{
					this.allChecked = true;
					this.noneChecked = false;
					this.allCheckBox.Checked = true;
				}
				this.currentValue = toolStripStatusLabelBorderSides;
			}

			// Token: 0x040020DE RID: 8414
			private BorderSidesEditor editor;

			// Token: 0x040020DF RID: 8415
			private IWindowsFormsEditorService edSvc;

			// Token: 0x040020E0 RID: 8416
			private object originalValue;

			// Token: 0x040020E1 RID: 8417
			private object currentValue;

			// Token: 0x040020E2 RID: 8418
			private bool updateCurrentValue;

			// Token: 0x040020E3 RID: 8419
			private TableLayoutPanel tableLayoutPanel1;

			// Token: 0x040020E4 RID: 8420
			private CheckBox allCheckBox;

			// Token: 0x040020E5 RID: 8421
			private CheckBox noneCheckBox;

			// Token: 0x040020E6 RID: 8422
			private CheckBox topCheckBox;

			// Token: 0x040020E7 RID: 8423
			private CheckBox bottomCheckBox;

			// Token: 0x040020E8 RID: 8424
			private CheckBox leftCheckBox;

			// Token: 0x040020E9 RID: 8425
			private CheckBox rightCheckBox;

			// Token: 0x040020EA RID: 8426
			private Label splitterLabel;

			// Token: 0x040020EB RID: 8427
			private bool allChecked;

			// Token: 0x040020EC RID: 8428
			private bool noneChecked;
		}
	}
}

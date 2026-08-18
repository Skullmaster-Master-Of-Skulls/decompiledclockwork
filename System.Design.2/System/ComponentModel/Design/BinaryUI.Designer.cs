namespace System.ComponentModel.Design
{
	// Token: 0x02000195 RID: 405
	internal partial class BinaryUI : global::System.Windows.Forms.Form
	{
		// Token: 0x06000EB1 RID: 3761 RVA: 0x00054A04 File Offset: 0x00052C04
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::System.ComponentModel.Design.BinaryEditor));
			this.byteViewer = new global::System.ComponentModel.Design.ByteViewer();
			this.buttonOK = new global::System.Windows.Forms.Button();
			this.buttonSave = new global::System.Windows.Forms.Button();
			this.groupBoxMode = new global::System.Windows.Forms.GroupBox();
			this.radioButtonsTableLayoutPanel = new global::System.Windows.Forms.TableLayoutPanel();
			this.radioUnicode = new global::System.Windows.Forms.RadioButton();
			this.radioAuto = new global::System.Windows.Forms.RadioButton();
			this.radioAnsi = new global::System.Windows.Forms.RadioButton();
			this.radioHex = new global::System.Windows.Forms.RadioButton();
			this.okSaveTableLayoutPanel = new global::System.Windows.Forms.TableLayoutPanel();
			this.overarchingTableLayoutPanel = new global::System.Windows.Forms.TableLayoutPanel();
			this.byteViewer.SuspendLayout();
			this.groupBoxMode.SuspendLayout();
			this.radioButtonsTableLayoutPanel.SuspendLayout();
			this.okSaveTableLayoutPanel.SuspendLayout();
			this.overarchingTableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.byteViewer, "byteViewer");
			this.byteViewer.SetDisplayMode(global::System.ComponentModel.Design.DisplayMode.Auto);
			this.byteViewer.Name = "byteViewer";
			this.byteViewer.Margin = global::System.Windows.Forms.Padding.Empty;
			this.byteViewer.Dock = global::System.Windows.Forms.DockStyle.Fill;
			componentResourceManager.ApplyResources(this.buttonOK, "buttonOK");
			this.buttonOK.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.buttonOK.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.buttonOK.Margin = new global::System.Windows.Forms.Padding(0, 0, 3, 0);
			this.buttonOK.MinimumSize = new global::System.Drawing.Size(75, 23);
			this.buttonOK.Name = "buttonOK";
			this.buttonOK.Padding = new global::System.Windows.Forms.Padding(10, 0, 10, 0);
			this.buttonOK.Click += new global::System.EventHandler(this.ButtonOK_click);
			componentResourceManager.ApplyResources(this.buttonSave, "buttonSave");
			this.buttonSave.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.buttonSave.Margin = new global::System.Windows.Forms.Padding(3, 0, 0, 0);
			this.buttonSave.MinimumSize = new global::System.Drawing.Size(75, 23);
			this.buttonSave.Name = "buttonSave";
			this.buttonSave.Padding = new global::System.Windows.Forms.Padding(10, 0, 10, 0);
			this.buttonSave.Click += new global::System.EventHandler(this.ButtonSave_click);
			componentResourceManager.ApplyResources(this.groupBoxMode, "groupBoxMode");
			this.groupBoxMode.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.groupBoxMode.Controls.Add(this.radioButtonsTableLayoutPanel);
			this.groupBoxMode.Margin = new global::System.Windows.Forms.Padding(0, 3, 0, 3);
			this.groupBoxMode.Name = "groupBoxMode";
			this.groupBoxMode.Padding = new global::System.Windows.Forms.Padding(0);
			this.groupBoxMode.TabStop = false;
			componentResourceManager.ApplyResources(this.radioButtonsTableLayoutPanel, "radioButtonsTableLayoutPanel");
			this.radioButtonsTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 25f));
			this.radioButtonsTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 25f));
			this.radioButtonsTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 25f));
			this.radioButtonsTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 25f));
			this.radioButtonsTableLayoutPanel.Controls.Add(this.radioUnicode, 3, 0);
			this.radioButtonsTableLayoutPanel.Controls.Add(this.radioAuto, 0, 0);
			this.radioButtonsTableLayoutPanel.Controls.Add(this.radioAnsi, 2, 0);
			this.radioButtonsTableLayoutPanel.Controls.Add(this.radioHex, 1, 0);
			this.radioButtonsTableLayoutPanel.Margin = new global::System.Windows.Forms.Padding(9);
			this.radioButtonsTableLayoutPanel.Name = "radioButtonsTableLayoutPanel";
			this.radioButtonsTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			componentResourceManager.ApplyResources(this.radioUnicode, "radioUnicode");
			this.radioUnicode.Margin = new global::System.Windows.Forms.Padding(3, 0, 0, 0);
			this.radioUnicode.Name = "radioUnicode";
			this.radioUnicode.CheckedChanged += new global::System.EventHandler(this.RadioUnicode_checkedChanged);
			componentResourceManager.ApplyResources(this.radioAuto, "radioAuto");
			this.radioAuto.Checked = true;
			this.radioAuto.Margin = new global::System.Windows.Forms.Padding(0, 0, 3, 0);
			this.radioAuto.Name = "radioAuto";
			this.radioAuto.CheckedChanged += new global::System.EventHandler(this.RadioAuto_checkedChanged);
			componentResourceManager.ApplyResources(this.radioAnsi, "radioAnsi");
			this.radioAnsi.Margin = new global::System.Windows.Forms.Padding(3, 0, 3, 0);
			this.radioAnsi.Name = "radioAnsi";
			this.radioAnsi.CheckedChanged += new global::System.EventHandler(this.RadioAnsi_checkedChanged);
			componentResourceManager.ApplyResources(this.radioHex, "radioHex");
			this.radioHex.Margin = new global::System.Windows.Forms.Padding(3, 0, 3, 0);
			this.radioHex.Name = "radioHex";
			this.radioHex.CheckedChanged += new global::System.EventHandler(this.RadioHex_checkedChanged);
			componentResourceManager.ApplyResources(this.okSaveTableLayoutPanel, "okSaveTableLayoutPanel");
			this.okSaveTableLayoutPanel.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.okSaveTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.okSaveTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.okSaveTableLayoutPanel.Controls.Add(this.buttonOK, 0, 0);
			this.okSaveTableLayoutPanel.Controls.Add(this.buttonSave, 1, 0);
			this.okSaveTableLayoutPanel.Margin = new global::System.Windows.Forms.Padding(0, 9, 0, 0);
			this.okSaveTableLayoutPanel.Name = "okSaveTableLayoutPanel";
			this.okSaveTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			componentResourceManager.ApplyResources(this.overarchingTableLayoutPanel, "overarchingTableLayoutPanel");
			this.overarchingTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.overarchingTableLayoutPanel.Controls.Add(this.byteViewer, 0, 0);
			this.overarchingTableLayoutPanel.Controls.Add(this.groupBoxMode, 0, 1);
			this.overarchingTableLayoutPanel.Controls.Add(this.okSaveTableLayoutPanel, 0, 2);
			this.overarchingTableLayoutPanel.Name = "overarchingTableLayoutPanel";
			this.overarchingTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.overarchingTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.overarchingTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			base.AcceptButton = this.buttonOK;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.buttonOK;
			base.Controls.Add(this.overarchingTableLayoutPanel);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.HelpButton = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "BinaryUI";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.HelpRequested += new global::System.Windows.Forms.HelpEventHandler(this.Form_HelpRequested);
			base.HelpButtonClicked += new global::System.ComponentModel.CancelEventHandler(this.Form_HelpButtonClicked);
			this.byteViewer.ResumeLayout(false);
			this.byteViewer.PerformLayout();
			this.groupBoxMode.ResumeLayout(false);
			this.groupBoxMode.PerformLayout();
			this.radioButtonsTableLayoutPanel.ResumeLayout(false);
			this.radioButtonsTableLayoutPanel.PerformLayout();
			this.okSaveTableLayoutPanel.ResumeLayout(false);
			this.okSaveTableLayoutPanel.PerformLayout();
			this.overarchingTableLayoutPanel.ResumeLayout(false);
			this.overarchingTableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x040008A7 RID: 2215
		private global::System.Windows.Forms.RadioButton radioAuto;

		// Token: 0x040008A8 RID: 2216
		private global::System.Windows.Forms.Button buttonSave;

		// Token: 0x040008A9 RID: 2217
		private global::System.Windows.Forms.Button buttonOK;

		// Token: 0x040008AA RID: 2218
		private global::System.ComponentModel.Design.ByteViewer byteViewer;

		// Token: 0x040008AB RID: 2219
		private global::System.Windows.Forms.GroupBox groupBoxMode;

		// Token: 0x040008AC RID: 2220
		private global::System.Windows.Forms.RadioButton radioHex;

		// Token: 0x040008AD RID: 2221
		private global::System.Windows.Forms.RadioButton radioAnsi;

		// Token: 0x040008AE RID: 2222
		private global::System.Windows.Forms.TableLayoutPanel radioButtonsTableLayoutPanel;

		// Token: 0x040008AF RID: 2223
		private global::System.Windows.Forms.TableLayoutPanel okSaveTableLayoutPanel;

		// Token: 0x040008B0 RID: 2224
		private global::System.Windows.Forms.TableLayoutPanel overarchingTableLayoutPanel;

		// Token: 0x040008B1 RID: 2225
		private global::System.Windows.Forms.RadioButton radioUnicode;
	}
}

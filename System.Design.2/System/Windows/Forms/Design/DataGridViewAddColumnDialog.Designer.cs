namespace System.Windows.Forms.Design
{
	// Token: 0x020002B8 RID: 696
	internal partial class DataGridViewAddColumnDialog : global::System.Windows.Forms.Form
	{
		// Token: 0x06001B86 RID: 7046 RVA: 0x000A3A89 File Offset: 0x000A1C89
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001B89 RID: 7049 RVA: 0x000A3C0C File Offset: 0x000A1E0C
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::System.Windows.Forms.Design.DataGridViewAddColumnDialog));
			this.dataBoundColumnRadioButton = new global::System.Windows.Forms.RadioButton();
			this.overarchingTableLayoutPanel = new global::System.Windows.Forms.TableLayoutPanel();
			this.checkBoxesTableLayoutPanel = new global::System.Windows.Forms.TableLayoutPanel();
			this.frozenCheckBox = new global::System.Windows.Forms.CheckBox();
			this.visibleCheckBox = new global::System.Windows.Forms.CheckBox();
			this.readOnlyCheckBox = new global::System.Windows.Forms.CheckBox();
			this.okCancelTableLayoutPanel = new global::System.Windows.Forms.TableLayoutPanel();
			this.addButton = new global::System.Windows.Forms.Button();
			this.cancelButton = new global::System.Windows.Forms.Button();
			this.columnInDataSourceLabel = new global::System.Windows.Forms.Label();
			this.dataColumns = new global::System.Windows.Forms.ListBox();
			this.unboundColumnRadioButton = new global::System.Windows.Forms.RadioButton();
			this.nameLabel = new global::System.Windows.Forms.Label();
			this.nameTextBox = new global::System.Windows.Forms.TextBox();
			this.typeLabel = new global::System.Windows.Forms.Label();
			this.columnTypesCombo = new global::System.Windows.Forms.ComboBox();
			this.headerTextLabel = new global::System.Windows.Forms.Label();
			this.headerTextBox = new global::System.Windows.Forms.TextBox();
			this.overarchingTableLayoutPanel.SuspendLayout();
			this.checkBoxesTableLayoutPanel.SuspendLayout();
			this.okCancelTableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.dataBoundColumnRadioButton, "dataBoundColumnRadioButton");
			this.dataBoundColumnRadioButton.Checked = true;
			this.overarchingTableLayoutPanel.SetColumnSpan(this.dataBoundColumnRadioButton, 3);
			this.dataBoundColumnRadioButton.Margin = new global::System.Windows.Forms.Padding(0);
			this.dataBoundColumnRadioButton.Name = "dataBoundColumnRadioButton";
			this.dataBoundColumnRadioButton.CheckedChanged += new global::System.EventHandler(this.dataBoundColumnRadioButton_CheckedChanged);
			componentResourceManager.ApplyResources(this.overarchingTableLayoutPanel, "overarchingTableLayoutPanel");
			this.overarchingTableLayoutPanel.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			this.overarchingTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 14f));
			this.overarchingTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle());
			this.overarchingTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 250f));
			this.overarchingTableLayoutPanel.Controls.Add(this.checkBoxesTableLayoutPanel, 0, 10);
			this.overarchingTableLayoutPanel.Controls.Add(this.okCancelTableLayoutPanel, 2, 11);
			this.overarchingTableLayoutPanel.Controls.Add(this.dataBoundColumnRadioButton, 0, 0);
			this.overarchingTableLayoutPanel.Controls.Add(this.columnInDataSourceLabel, 1, 1);
			this.overarchingTableLayoutPanel.Controls.Add(this.dataColumns, 1, 2);
			this.overarchingTableLayoutPanel.Controls.Add(this.unboundColumnRadioButton, 0, 4);
			this.overarchingTableLayoutPanel.Controls.Add(this.nameLabel, 1, 5);
			this.overarchingTableLayoutPanel.Controls.Add(this.nameTextBox, 2, 5);
			this.overarchingTableLayoutPanel.Controls.Add(this.typeLabel, 1, 7);
			this.overarchingTableLayoutPanel.Controls.Add(this.columnTypesCombo, 2, 7);
			this.overarchingTableLayoutPanel.Controls.Add(this.headerTextLabel, 1, 9);
			this.overarchingTableLayoutPanel.Controls.Add(this.headerTextBox, 2, 9);
			this.overarchingTableLayoutPanel.Margin = new global::System.Windows.Forms.Padding(12, 12, 12, 13);
			this.overarchingTableLayoutPanel.Name = "overarchingTableLayoutPanel";
			this.overarchingTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 22f));
			this.overarchingTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 16f));
			this.overarchingTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 91f));
			this.overarchingTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 12f));
			this.overarchingTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 22f));
			this.overarchingTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.overarchingTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 4f));
			this.overarchingTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.overarchingTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 4f));
			this.overarchingTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.overarchingTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Absolute, 27f));
			this.overarchingTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			componentResourceManager.ApplyResources(this.checkBoxesTableLayoutPanel, "checkBoxesTableLayoutPanel");
			this.checkBoxesTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle());
			this.checkBoxesTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle());
			this.checkBoxesTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle());
			this.checkBoxesTableLayoutPanel.Controls.Add(this.frozenCheckBox, 2, 0);
			this.checkBoxesTableLayoutPanel.Controls.Add(this.visibleCheckBox, 0, 0);
			this.checkBoxesTableLayoutPanel.Controls.Add(this.readOnlyCheckBox, 1, 0);
			this.checkBoxesTableLayoutPanel.Margin = new global::System.Windows.Forms.Padding(0);
			this.overarchingTableLayoutPanel.SetColumnSpan(this.checkBoxesTableLayoutPanel, 3);
			this.checkBoxesTableLayoutPanel.Name = "checkBoxesTableLayoutPanel";
			this.checkBoxesTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			componentResourceManager.ApplyResources(this.frozenCheckBox, "frozenCheckBox");
			this.frozenCheckBox.Margin = new global::System.Windows.Forms.Padding(0);
			this.frozenCheckBox.Name = "frozenCheckBox";
			componentResourceManager.ApplyResources(this.visibleCheckBox, "visibleCheckBox");
			this.visibleCheckBox.Checked = true;
			this.visibleCheckBox.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.visibleCheckBox.Margin = new global::System.Windows.Forms.Padding(0);
			this.visibleCheckBox.Name = "visibleCheckBox";
			componentResourceManager.ApplyResources(this.readOnlyCheckBox, "readOnlyCheckBox");
			this.readOnlyCheckBox.Margin = new global::System.Windows.Forms.Padding(0);
			this.readOnlyCheckBox.Name = "readOnlyCheckBox";
			componentResourceManager.ApplyResources(this.okCancelTableLayoutPanel, "okCancelTableLayoutPanel");
			this.okCancelTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.okCancelTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.okCancelTableLayoutPanel.Controls.Add(this.addButton, 0, 0);
			this.okCancelTableLayoutPanel.Controls.Add(this.cancelButton, 1, 0);
			this.okCancelTableLayoutPanel.Margin = new global::System.Windows.Forms.Padding(0, 6, 0, 0);
			this.okCancelTableLayoutPanel.Name = "okCancelTableLayoutPanel";
			this.okCancelTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			componentResourceManager.ApplyResources(this.addButton, "addButton");
			this.addButton.Margin = new global::System.Windows.Forms.Padding(0, 0, 3, 0);
			this.addButton.Name = "addButton";
			this.addButton.Click += new global::System.EventHandler(this.addButton_Click);
			componentResourceManager.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.Margin = new global::System.Windows.Forms.Padding(3, 0, 0, 0);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Click += new global::System.EventHandler(this.cancelButton_Click);
			componentResourceManager.ApplyResources(this.columnInDataSourceLabel, "columnInDataSourceLabel");
			this.overarchingTableLayoutPanel.SetColumnSpan(this.columnInDataSourceLabel, 2);
			this.columnInDataSourceLabel.Margin = new global::System.Windows.Forms.Padding(0);
			this.columnInDataSourceLabel.Name = "columnInDataSourceLabel";
			componentResourceManager.ApplyResources(this.dataColumns, "dataColumns");
			this.overarchingTableLayoutPanel.SetColumnSpan(this.dataColumns, 2);
			this.dataColumns.FormattingEnabled = true;
			this.dataColumns.Margin = new global::System.Windows.Forms.Padding(0);
			this.dataColumns.Name = "dataColumns";
			this.dataColumns.SelectedIndexChanged += new global::System.EventHandler(this.dataColumns_SelectedIndexChanged);
			componentResourceManager.ApplyResources(this.unboundColumnRadioButton, "unboundColumnRadioButton");
			this.overarchingTableLayoutPanel.SetColumnSpan(this.unboundColumnRadioButton, 3);
			this.unboundColumnRadioButton.Margin = new global::System.Windows.Forms.Padding(0);
			this.unboundColumnRadioButton.Name = "unboundColumnRadioButton";
			this.unboundColumnRadioButton.CheckedChanged += new global::System.EventHandler(this.unboundColumnRadioButton_CheckedChanged);
			componentResourceManager.ApplyResources(this.nameLabel, "nameLabel");
			this.nameLabel.Margin = new global::System.Windows.Forms.Padding(0);
			this.nameLabel.Name = "nameLabel";
			componentResourceManager.ApplyResources(this.nameTextBox, "nameTextBox");
			this.nameTextBox.Margin = new global::System.Windows.Forms.Padding(0);
			this.nameTextBox.Name = "nameTextBox";
			this.nameTextBox.Validating += new global::System.ComponentModel.CancelEventHandler(this.nameTextBox_Validating);
			componentResourceManager.ApplyResources(this.typeLabel, "typeLabel");
			this.typeLabel.Margin = new global::System.Windows.Forms.Padding(0);
			this.typeLabel.Name = "typeLabel";
			componentResourceManager.ApplyResources(this.columnTypesCombo, "columnTypesCombo");
			this.columnTypesCombo.DropDownStyle = global::System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.columnTypesCombo.FormattingEnabled = true;
			this.columnTypesCombo.Margin = new global::System.Windows.Forms.Padding(0);
			this.columnTypesCombo.Name = "columnTypesCombo";
			this.columnTypesCombo.Sorted = true;
			componentResourceManager.ApplyResources(this.headerTextLabel, "headerTextLabel");
			this.headerTextLabel.Margin = new global::System.Windows.Forms.Padding(0);
			this.headerTextLabel.Name = "headerTextLabel";
			componentResourceManager.ApplyResources(this.headerTextBox, "headerTextBox");
			this.headerTextBox.Margin = new global::System.Windows.Forms.Padding(0);
			this.headerTextBox.Name = "headerTextBox";
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.cancelButton;
			base.AutoSizeMode = global::System.Windows.Forms.AutoSizeMode.GrowAndShrink;
			base.Controls.Add(this.overarchingTableLayoutPanel);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.HelpButton = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DataGridViewAddColumnDialog";
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.HelpButtonClicked += new global::System.ComponentModel.CancelEventHandler(this.DataGridViewAddColumnDialog_HelpButtonClicked);
			base.Closed += new global::System.EventHandler(this.DataGridViewAddColumnDialog_Closed);
			base.VisibleChanged += new global::System.EventHandler(this.DataGridViewAddColumnDialog_VisibleChanged);
			base.Load += new global::System.EventHandler(this.DataGridViewAddColumnDialog_Load);
			base.HelpRequested += new global::System.Windows.Forms.HelpEventHandler(this.DataGridViewAddColumnDialog_HelpRequested);
			this.overarchingTableLayoutPanel.ResumeLayout(false);
			this.overarchingTableLayoutPanel.PerformLayout();
			this.checkBoxesTableLayoutPanel.ResumeLayout(false);
			this.checkBoxesTableLayoutPanel.PerformLayout();
			this.okCancelTableLayoutPanel.ResumeLayout(false);
			this.okCancelTableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04001653 RID: 5715
		private global::System.Windows.Forms.RadioButton dataBoundColumnRadioButton;

		// Token: 0x04001654 RID: 5716
		private global::System.Windows.Forms.Label columnInDataSourceLabel;

		// Token: 0x04001655 RID: 5717
		private global::System.Windows.Forms.ListBox dataColumns;

		// Token: 0x04001656 RID: 5718
		private global::System.Windows.Forms.RadioButton unboundColumnRadioButton;

		// Token: 0x04001657 RID: 5719
		private global::System.Windows.Forms.TextBox nameTextBox;

		// Token: 0x04001658 RID: 5720
		private global::System.Windows.Forms.ComboBox columnTypesCombo;

		// Token: 0x04001659 RID: 5721
		private global::System.Windows.Forms.TextBox headerTextBox;

		// Token: 0x0400165A RID: 5722
		private global::System.Windows.Forms.Label nameLabel;

		// Token: 0x0400165B RID: 5723
		private global::System.Windows.Forms.Label typeLabel;

		// Token: 0x0400165C RID: 5724
		private global::System.Windows.Forms.Label headerTextLabel;

		// Token: 0x0400165D RID: 5725
		private global::System.Windows.Forms.CheckBox visibleCheckBox;

		// Token: 0x0400165E RID: 5726
		private global::System.Windows.Forms.CheckBox readOnlyCheckBox;

		// Token: 0x0400165F RID: 5727
		private global::System.Windows.Forms.CheckBox frozenCheckBox;

		// Token: 0x04001660 RID: 5728
		private global::System.Windows.Forms.Button addButton;

		// Token: 0x04001661 RID: 5729
		private global::System.Windows.Forms.Button cancelButton;

		// Token: 0x04001672 RID: 5746
		private global::System.Windows.Forms.TableLayoutPanel okCancelTableLayoutPanel;

		// Token: 0x04001673 RID: 5747
		private global::System.Windows.Forms.TableLayoutPanel checkBoxesTableLayoutPanel;

		// Token: 0x04001674 RID: 5748
		private global::System.Windows.Forms.TableLayoutPanel overarchingTableLayoutPanel;

		// Token: 0x04001675 RID: 5749
		private global::System.ComponentModel.IContainer components;
	}
}

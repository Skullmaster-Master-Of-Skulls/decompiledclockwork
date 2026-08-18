namespace System.Windows.Forms.Design
{
	// Token: 0x02000312 RID: 786
	internal partial class MaskDesignerDialog : global::System.Windows.Forms.Form
	{
		// Token: 0x06001F02 RID: 7938 RVA: 0x000BA0D0 File Offset: 0x000B82D0
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::System.Windows.Forms.Design.MaskDesignerDialog));
			this.lblHeader = new global::System.Windows.Forms.Label();
			this.listViewCannedMasks = new global::System.Windows.Forms.ListView();
			this.maskDescriptionHeader = new global::System.Windows.Forms.ColumnHeader(componentResourceManager.GetString("listViewCannedMasks.Columns"));
			this.dataFormatHeader = new global::System.Windows.Forms.ColumnHeader(componentResourceManager.GetString("listViewCannedMasks.Columns1"));
			this.validatingTypeHeader = new global::System.Windows.Forms.ColumnHeader(componentResourceManager.GetString("listViewCannedMasks.Columns2"));
			this.btnOK = new global::System.Windows.Forms.Button();
			this.btnCancel = new global::System.Windows.Forms.Button();
			this.checkBoxUseValidatingType = new global::System.Windows.Forms.CheckBox();
			this.maskTryItTable = new global::System.Windows.Forms.TableLayoutPanel();
			this.lblMask = new global::System.Windows.Forms.Label();
			this.txtBoxMask = new global::System.Windows.Forms.TextBox();
			this.lblTryIt = new global::System.Windows.Forms.Label();
			this.overarchingTableLayoutPanel = new global::System.Windows.Forms.TableLayoutPanel();
			this.okCancelTableLayoutPanel = new global::System.Windows.Forms.TableLayoutPanel();
			this.errorProvider = new global::System.Windows.Forms.ErrorProvider(this.components);
			this.maskTryItTable.SuspendLayout();
			this.overarchingTableLayoutPanel.SuspendLayout();
			this.okCancelTableLayoutPanel.SuspendLayout();
			((global::System.ComponentModel.ISupportInitialize)this.errorProvider).BeginInit();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.maskedTextBox, "maskedTextBox");
			this.maskedTextBox.Margin = new global::System.Windows.Forms.Padding(3, 3, 18, 0);
			this.maskedTextBox.Name = "maskedTextBox";
			componentResourceManager.ApplyResources(this.lblHeader, "lblHeader");
			this.lblHeader.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 3);
			this.lblHeader.Name = "lblHeader";
			componentResourceManager.ApplyResources(this.listViewCannedMasks, "listViewCannedMasks");
			this.listViewCannedMasks.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.maskDescriptionHeader,
				this.dataFormatHeader,
				this.validatingTypeHeader
			});
			this.listViewCannedMasks.FullRowSelect = true;
			this.listViewCannedMasks.HideSelection = false;
			this.listViewCannedMasks.Margin = new global::System.Windows.Forms.Padding(0, 3, 0, 3);
			this.listViewCannedMasks.MultiSelect = false;
			this.listViewCannedMasks.Name = "listViewCannedMasks";
			this.listViewCannedMasks.Sorting = global::System.Windows.Forms.SortOrder.None;
			this.listViewCannedMasks.View = global::System.Windows.Forms.View.Details;
			componentResourceManager.ApplyResources(this.maskDescriptionHeader, "maskDescriptionHeader");
			componentResourceManager.ApplyResources(this.dataFormatHeader, "dataFormatHeader");
			componentResourceManager.ApplyResources(this.validatingTypeHeader, "validatingTypeHeader");
			componentResourceManager.ApplyResources(this.btnOK, "btnOK");
			this.btnOK.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.btnOK.Margin = new global::System.Windows.Forms.Padding(0, 0, 3, 0);
			this.btnOK.MinimumSize = new global::System.Drawing.Size(75, 23);
			this.btnOK.Name = "btnOK";
			this.btnOK.Padding = new global::System.Windows.Forms.Padding(10, 0, 10, 0);
			componentResourceManager.ApplyResources(this.btnCancel, "btnCancel");
			this.btnCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.btnCancel.Margin = new global::System.Windows.Forms.Padding(3, 0, 0, 0);
			this.btnCancel.MinimumSize = new global::System.Drawing.Size(75, 23);
			this.btnCancel.Name = "btnCancel";
			this.btnCancel.Padding = new global::System.Windows.Forms.Padding(10, 0, 10, 0);
			componentResourceManager.ApplyResources(this.checkBoxUseValidatingType, "checkBoxUseValidatingType");
			this.checkBoxUseValidatingType.Checked = true;
			this.checkBoxUseValidatingType.CheckState = global::System.Windows.Forms.CheckState.Checked;
			this.checkBoxUseValidatingType.Margin = new global::System.Windows.Forms.Padding(0, 0, 0, 3);
			this.checkBoxUseValidatingType.Name = "checkBoxUseValidatingType";
			componentResourceManager.ApplyResources(this.maskTryItTable, "maskTryItTable");
			this.maskTryItTable.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle());
			this.maskTryItTable.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 100f));
			this.maskTryItTable.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle());
			this.maskTryItTable.Controls.Add(this.checkBoxUseValidatingType, 2, 0);
			this.maskTryItTable.Controls.Add(this.lblMask, 0, 0);
			this.maskTryItTable.Controls.Add(this.txtBoxMask, 1, 0);
			this.maskTryItTable.Controls.Add(this.lblTryIt, 0, 1);
			this.maskTryItTable.Controls.Add(this.maskedTextBox, 1, 1);
			this.maskTryItTable.Margin = new global::System.Windows.Forms.Padding(0, 3, 0, 3);
			this.maskTryItTable.Name = "maskTryItTable";
			this.maskTryItTable.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.maskTryItTable.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			componentResourceManager.ApplyResources(this.lblMask, "lblMask");
			this.lblMask.Margin = new global::System.Windows.Forms.Padding(0, 0, 3, 3);
			this.lblMask.Name = "lblMask";
			componentResourceManager.ApplyResources(this.txtBoxMask, "txtBoxMask");
			this.txtBoxMask.Margin = new global::System.Windows.Forms.Padding(3, 0, 18, 3);
			this.txtBoxMask.Name = "txtBoxMask";
			componentResourceManager.ApplyResources(this.lblTryIt, "lblTryIt");
			this.lblTryIt.Margin = new global::System.Windows.Forms.Padding(0, 3, 3, 0);
			this.lblTryIt.Name = "lblTryIt";
			componentResourceManager.ApplyResources(this.overarchingTableLayoutPanel, "overarchingTableLayoutPanel");
			this.overarchingTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.overarchingTableLayoutPanel.Controls.Add(this.maskTryItTable, 0, 3);
			this.overarchingTableLayoutPanel.Controls.Add(this.okCancelTableLayoutPanel, 0, 4);
			this.overarchingTableLayoutPanel.Controls.Add(this.lblHeader, 0, 1);
			this.overarchingTableLayoutPanel.Controls.Add(this.listViewCannedMasks, 0, 2);
			this.overarchingTableLayoutPanel.Name = "overarchingTableLayoutPanel";
			this.overarchingTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.overarchingTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.overarchingTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.overarchingTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.overarchingTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			componentResourceManager.ApplyResources(this.okCancelTableLayoutPanel, "okCancelTableLayoutPanel");
			this.okCancelTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.okCancelTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.okCancelTableLayoutPanel.Controls.Add(this.btnCancel, 1, 0);
			this.okCancelTableLayoutPanel.Controls.Add(this.btnOK, 0, 0);
			this.okCancelTableLayoutPanel.Margin = new global::System.Windows.Forms.Padding(0, 6, 0, 0);
			this.okCancelTableLayoutPanel.Name = "okCancelTableLayoutPanel";
			this.okCancelTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.errorProvider.BlinkStyle = global::System.Windows.Forms.ErrorBlinkStyle.NeverBlink;
			this.errorProvider.ContainerControl = this;
			componentResourceManager.ApplyResources(this, "$this");
			base.AcceptButton = this.btnOK;
			base.CancelButton = this.btnCancel;
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.overarchingTableLayoutPanel);
			base.FormBorderStyle = global::System.Windows.Forms.FormBorderStyle.FixedDialog;
			base.HelpButton = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "MaskDesignerDialog";
			base.ShowInTaskbar = false;
			base.SizeGripStyle = global::System.Windows.Forms.SizeGripStyle.Hide;
			this.maskTryItTable.ResumeLayout(false);
			this.maskTryItTable.PerformLayout();
			this.overarchingTableLayoutPanel.ResumeLayout(false);
			this.overarchingTableLayoutPanel.PerformLayout();
			this.okCancelTableLayoutPanel.ResumeLayout(false);
			this.okCancelTableLayoutPanel.PerformLayout();
			((global::System.ComponentModel.ISupportInitialize)this.errorProvider).EndInit();
			base.ResumeLayout(false);
		}

		// Token: 0x040017E9 RID: 6121
		private global::System.Windows.Forms.Label lblHeader;

		// Token: 0x040017EA RID: 6122
		private global::System.Windows.Forms.ListView listViewCannedMasks;

		// Token: 0x040017EB RID: 6123
		private global::System.Windows.Forms.CheckBox checkBoxUseValidatingType;

		// Token: 0x040017EC RID: 6124
		private global::System.Windows.Forms.ColumnHeader maskDescriptionHeader;

		// Token: 0x040017ED RID: 6125
		private global::System.Windows.Forms.ColumnHeader dataFormatHeader;

		// Token: 0x040017EE RID: 6126
		private global::System.Windows.Forms.ColumnHeader validatingTypeHeader;

		// Token: 0x040017EF RID: 6127
		private global::System.Windows.Forms.TableLayoutPanel maskTryItTable;

		// Token: 0x040017F0 RID: 6128
		private global::System.Windows.Forms.Label lblMask;

		// Token: 0x040017F1 RID: 6129
		private global::System.Windows.Forms.TextBox txtBoxMask;

		// Token: 0x040017F2 RID: 6130
		private global::System.Windows.Forms.Label lblTryIt;

		// Token: 0x040017F3 RID: 6131
		private global::System.Windows.Forms.MaskedTextBox maskedTextBox;

		// Token: 0x040017F4 RID: 6132
		private global::System.Windows.Forms.TableLayoutPanel okCancelTableLayoutPanel;

		// Token: 0x040017F5 RID: 6133
		private global::System.Windows.Forms.TableLayoutPanel overarchingTableLayoutPanel;

		// Token: 0x040017F6 RID: 6134
		private global::System.Windows.Forms.Button btnOK;

		// Token: 0x040017F7 RID: 6135
		private global::System.Windows.Forms.Button btnCancel;

		// Token: 0x040017F8 RID: 6136
		private global::System.Windows.Forms.ErrorProvider errorProvider;

		// Token: 0x040017FD RID: 6141
		private global::System.ComponentModel.IContainer components;
	}
}

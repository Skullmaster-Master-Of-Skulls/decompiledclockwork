namespace System.Windows.Forms.Design
{
	// Token: 0x020002BB RID: 699
	internal partial class DataGridViewColumnCollectionDialog : global::System.Windows.Forms.Form
	{
		// Token: 0x06001BBA RID: 7098 RVA: 0x000A696C File Offset: 0x000A4B6C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06001BBD RID: 7101 RVA: 0x000A69D8 File Offset: 0x000A4BD8
		private void InitializeComponent()
		{
			global::System.ComponentModel.ComponentResourceManager componentResourceManager = new global::System.ComponentModel.ComponentResourceManager(typeof(global::System.Windows.Forms.Design.DataGridViewColumnCollectionDialog));
			this.addButton = new global::System.Windows.Forms.Button();
			this.deleteButton = new global::System.Windows.Forms.Button();
			this.moveDown = new global::System.Windows.Forms.Button();
			this.moveUp = new global::System.Windows.Forms.Button();
			this.selectedColumns = new global::System.Windows.Forms.ListBox();
			this.overarchingTableLayoutPanel = new global::System.Windows.Forms.TableLayoutPanel();
			this.addRemoveTableLayoutPanel = new global::System.Windows.Forms.TableLayoutPanel();
			this.selectedColumnsLabel = new global::System.Windows.Forms.Label();
			this.propertyGridLabel = new global::System.Windows.Forms.Label();
			this.propertyGrid1 = new global::System.Windows.Forms.Design.VsPropertyGrid(this.serviceProvider);
			this.okCancelTableLayoutPanel = new global::System.Windows.Forms.TableLayoutPanel();
			this.cancelButton = new global::System.Windows.Forms.Button();
			this.okButton = new global::System.Windows.Forms.Button();
			this.overarchingTableLayoutPanel.SuspendLayout();
			this.addRemoveTableLayoutPanel.SuspendLayout();
			this.okCancelTableLayoutPanel.SuspendLayout();
			base.SuspendLayout();
			componentResourceManager.ApplyResources(this.addButton, "addButton");
			this.addButton.Margin = new global::System.Windows.Forms.Padding(0, 0, 3, 0);
			this.addButton.Name = "addButton";
			this.addButton.Padding = new global::System.Windows.Forms.Padding(10, 0, 10, 0);
			this.addButton.Click += new global::System.EventHandler(this.addButton_Click);
			componentResourceManager.ApplyResources(this.deleteButton, "deleteButton");
			this.deleteButton.Margin = new global::System.Windows.Forms.Padding(3, 0, 0, 0);
			this.deleteButton.Name = "deleteButton";
			this.deleteButton.Padding = new global::System.Windows.Forms.Padding(10, 0, 10, 0);
			this.deleteButton.Click += new global::System.EventHandler(this.deleteButton_Click);
			componentResourceManager.ApplyResources(this.moveDown, "moveDown");
			this.moveDown.Margin = new global::System.Windows.Forms.Padding(0, 1, 18, 0);
			this.moveDown.Name = "moveDown";
			this.moveDown.Click += new global::System.EventHandler(this.moveDown_Click);
			componentResourceManager.ApplyResources(this.moveUp, "moveUp");
			this.moveUp.Margin = new global::System.Windows.Forms.Padding(0, 0, 18, 1);
			this.moveUp.Name = "moveUp";
			this.moveUp.Click += new global::System.EventHandler(this.moveUp_Click);
			componentResourceManager.ApplyResources(this.selectedColumns, "selectedColumns");
			this.selectedColumns.DrawMode = global::System.Windows.Forms.DrawMode.OwnerDrawFixed;
			this.selectedColumns.Margin = new global::System.Windows.Forms.Padding(0, 2, 3, 3);
			this.selectedColumns.Name = "selectedColumns";
			this.overarchingTableLayoutPanel.SetRowSpan(this.selectedColumns, 2);
			this.selectedColumns.SelectedIndexChanged += new global::System.EventHandler(this.selectedColumns_SelectedIndexChanged);
			this.selectedColumns.KeyPress += new global::System.Windows.Forms.KeyPressEventHandler(this.selectedColumns_KeyPress);
			this.selectedColumns.DrawItem += new global::System.Windows.Forms.DrawItemEventHandler(this.selectedColumns_DrawItem);
			this.selectedColumns.KeyUp += new global::System.Windows.Forms.KeyEventHandler(this.selectedColumns_KeyUp);
			componentResourceManager.ApplyResources(this.overarchingTableLayoutPanel, "overarchingTableLayoutPanel");
			this.overarchingTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle());
			this.overarchingTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle());
			this.overarchingTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent));
			this.overarchingTableLayoutPanel.Controls.Add(this.addRemoveTableLayoutPanel, 0, 3);
			this.overarchingTableLayoutPanel.Controls.Add(this.moveUp, 1, 1);
			this.overarchingTableLayoutPanel.Controls.Add(this.selectedColumnsLabel, 0, 0);
			this.overarchingTableLayoutPanel.Controls.Add(this.moveDown, 1, 2);
			this.overarchingTableLayoutPanel.Controls.Add(this.propertyGridLabel, 2, 0);
			this.overarchingTableLayoutPanel.Controls.Add(this.selectedColumns, 0, 1);
			this.overarchingTableLayoutPanel.Controls.Add(this.propertyGrid1, 2, 1);
			this.overarchingTableLayoutPanel.Controls.Add(this.okCancelTableLayoutPanel, 0, 4);
			this.overarchingTableLayoutPanel.Name = "overarchingTableLayoutPanel";
			this.overarchingTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.overarchingTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			this.overarchingTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.overarchingTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			componentResourceManager.ApplyResources(this.addRemoveTableLayoutPanel, "addRemoveTableLayoutPanel");
			this.addRemoveTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.addRemoveTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.addRemoveTableLayoutPanel.Controls.Add(this.addButton, 0, 0);
			this.addRemoveTableLayoutPanel.Controls.Add(this.deleteButton, 1, 0);
			this.addRemoveTableLayoutPanel.Margin = new global::System.Windows.Forms.Padding(0, 3, 3, 3);
			this.addRemoveTableLayoutPanel.Name = "addRemoveTableLayoutPanel";
			this.addRemoveTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			componentResourceManager.ApplyResources(this.selectedColumnsLabel, "selectedColumnsLabel");
			this.selectedColumnsLabel.Margin = new global::System.Windows.Forms.Padding(0);
			this.selectedColumnsLabel.Name = "selectedColumnsLabel";
			componentResourceManager.ApplyResources(this.propertyGridLabel, "propertyGridLabel");
			this.propertyGridLabel.Margin = new global::System.Windows.Forms.Padding(3, 0, 0, 0);
			this.propertyGridLabel.Name = "propertyGridLabel";
			componentResourceManager.ApplyResources(this.propertyGrid1, "propertyGrid1");
			this.propertyGrid1.LineColor = global::System.Drawing.SystemColors.ScrollBar;
			this.propertyGrid1.Margin = new global::System.Windows.Forms.Padding(3, 2, 0, 3);
			this.propertyGrid1.Name = "propertyGrid1";
			this.overarchingTableLayoutPanel.SetRowSpan(this.propertyGrid1, 3);
			this.propertyGrid1.PropertyValueChanged += new global::System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid1_PropertyValueChanged);
			componentResourceManager.ApplyResources(this.okCancelTableLayoutPanel, "okCancelTableLayoutPanel");
			this.okCancelTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.okCancelTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Percent, 50f));
			this.okCancelTableLayoutPanel.ColumnStyles.Add(new global::System.Windows.Forms.ColumnStyle(global::System.Windows.Forms.SizeType.Absolute, 20f));
			this.okCancelTableLayoutPanel.Controls.Add(this.cancelButton, 1, 0);
			this.okCancelTableLayoutPanel.Controls.Add(this.okButton, 0, 0);
			this.okCancelTableLayoutPanel.Name = "okCancelTableLayoutPanel";
			this.overarchingTableLayoutPanel.SetColumnSpan(this.okCancelTableLayoutPanel, 3);
			this.okCancelTableLayoutPanel.RowStyles.Add(new global::System.Windows.Forms.RowStyle());
			componentResourceManager.ApplyResources(this.cancelButton, "cancelButton");
			this.cancelButton.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.cancelButton.Margin = new global::System.Windows.Forms.Padding(3, 0, 0, 0);
			this.cancelButton.Name = "cancelButton";
			this.cancelButton.Padding = new global::System.Windows.Forms.Padding(10, 0, 10, 0);
			componentResourceManager.ApplyResources(this.okButton, "okButton");
			this.okButton.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.okButton.Margin = new global::System.Windows.Forms.Padding(0, 0, 3, 0);
			this.okButton.Name = "okButton";
			this.okButton.Padding = new global::System.Windows.Forms.Padding(10, 0, 10, 0);
			this.okButton.Click += new global::System.EventHandler(this.okButton_Click);
			base.AcceptButton = this.okButton;
			componentResourceManager.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.cancelButton;
			base.Controls.Add(this.overarchingTableLayoutPanel);
			base.HelpButton = true;
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "DataGridViewColumnCollectionDialog";
			base.Padding = new global::System.Windows.Forms.Padding(12);
			base.ShowIcon = false;
			base.ShowInTaskbar = false;
			base.HelpButtonClicked += new global::System.ComponentModel.CancelEventHandler(this.DataGridViewColumnCollectionDialog_HelpButtonClicked);
			base.Closed += new global::System.EventHandler(this.DataGridViewColumnCollectionDialog_Closed);
			base.Load += new global::System.EventHandler(this.DataGridViewColumnCollectionDialog_Load);
			base.HelpRequested += new global::System.Windows.Forms.HelpEventHandler(this.DataGridViewColumnCollectionDialog_HelpRequested);
			this.overarchingTableLayoutPanel.ResumeLayout(false);
			this.overarchingTableLayoutPanel.PerformLayout();
			this.addRemoveTableLayoutPanel.ResumeLayout(false);
			this.addRemoveTableLayoutPanel.PerformLayout();
			this.okCancelTableLayoutPanel.ResumeLayout(false);
			this.okCancelTableLayoutPanel.PerformLayout();
			base.ResumeLayout(false);
		}

		// Token: 0x0400168B RID: 5771
		private global::System.Windows.Forms.Label selectedColumnsLabel;

		// Token: 0x0400168C RID: 5772
		private global::System.Windows.Forms.ListBox selectedColumns;

		// Token: 0x0400168D RID: 5773
		private global::System.Windows.Forms.Button moveUp;

		// Token: 0x0400168E RID: 5774
		private global::System.Windows.Forms.Button moveDown;

		// Token: 0x0400168F RID: 5775
		private global::System.Windows.Forms.Button deleteButton;

		// Token: 0x04001690 RID: 5776
		private global::System.Windows.Forms.Button addButton;

		// Token: 0x04001691 RID: 5777
		private global::System.Windows.Forms.Label propertyGridLabel;

		// Token: 0x04001692 RID: 5778
		private global::System.Windows.Forms.PropertyGrid propertyGrid1;

		// Token: 0x04001693 RID: 5779
		private global::System.Windows.Forms.TableLayoutPanel okCancelTableLayoutPanel;

		// Token: 0x04001694 RID: 5780
		private global::System.Windows.Forms.Button okButton;

		// Token: 0x04001695 RID: 5781
		private global::System.Windows.Forms.Button cancelButton;

		// Token: 0x040016AA RID: 5802
		private global::System.Windows.Forms.TableLayoutPanel overarchingTableLayoutPanel;

		// Token: 0x040016AB RID: 5803
		private global::System.Windows.Forms.TableLayoutPanel addRemoveTableLayoutPanel;

		// Token: 0x040016AD RID: 5805
		private global::System.ComponentModel.IContainer components;

		// Token: 0x040016AE RID: 5806
		private global::System.IServiceProvider serviceProvider;
	}
}

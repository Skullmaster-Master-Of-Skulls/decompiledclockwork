namespace Telerik.Charting.Styles
{
	// Token: 0x0200176F RID: 5999
	internal partial class CustomShapeEditorForm : global::System.Windows.Forms.Form
	{
		// Token: 0x0600EA15 RID: 59925 RVA: 0x00355098 File Offset: 0x00353298
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600EA16 RID: 59926 RVA: 0x003550B8 File Offset: 0x003532B8
		private void InitializeComponent()
		{
			this.radShapeEditorControl1 = new global::Telerik.Charting.Styles.RadShapeEditorControl();
			this.propertyGrid1 = new global::System.Windows.Forms.PropertyGrid();
			this.buttonOk = new global::System.Windows.Forms.Button();
			this.buttonCancel = new global::System.Windows.Forms.Button();
			base.SuspendLayout();
			this.radShapeEditorControl1.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Left | global::System.Windows.Forms.AnchorStyles.Right);
			this.radShapeEditorControl1.Dimension = new global::System.Drawing.Rectangle(20, 20, 252, 211);
			this.radShapeEditorControl1.Location = new global::System.Drawing.Point(3, 3);
			this.radShapeEditorControl1.Name = "radShapeEditorControl1";
			this.radShapeEditorControl1.Size = new global::System.Drawing.Size(313, 251);
			this.radShapeEditorControl1.TabIndex = 0;
			this.propertyGrid1.Anchor = (global::System.Windows.Forms.AnchorStyles.Top | global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.propertyGrid1.Location = new global::System.Drawing.Point(322, 3);
			this.propertyGrid1.Name = "propertyGrid1";
			this.propertyGrid1.Size = new global::System.Drawing.Size(156, 222);
			this.propertyGrid1.TabIndex = 1;
			this.buttonOk.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonOk.DialogResult = global::System.Windows.Forms.DialogResult.OK;
			this.buttonOk.Location = new global::System.Drawing.Point(322, 231);
			this.buttonOk.Name = "buttonOk";
			this.buttonOk.Size = new global::System.Drawing.Size(75, 23);
			this.buttonOk.TabIndex = 2;
			this.buttonOk.Text = "Ok";
			this.buttonOk.UseVisualStyleBackColor = true;
			this.buttonCancel.Anchor = (global::System.Windows.Forms.AnchorStyles.Bottom | global::System.Windows.Forms.AnchorStyles.Right);
			this.buttonCancel.DialogResult = global::System.Windows.Forms.DialogResult.Cancel;
			this.buttonCancel.Location = new global::System.Drawing.Point(403, 231);
			this.buttonCancel.Name = "buttonCancel";
			this.buttonCancel.Size = new global::System.Drawing.Size(75, 23);
			this.buttonCancel.TabIndex = 3;
			this.buttonCancel.Text = "Cancel";
			this.buttonCancel.UseVisualStyleBackColor = true;
			base.AcceptButton = this.buttonOk;
			base.AutoScaleDimensions = new global::System.Drawing.SizeF(6f, 13f);
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.CancelButton = this.buttonCancel;
			base.ClientSize = new global::System.Drawing.Size(481, 257);
			base.Controls.Add(this.buttonCancel);
			base.Controls.Add(this.buttonOk);
			base.Controls.Add(this.propertyGrid1);
			base.Controls.Add(this.radShapeEditorControl1);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "CustomShapeEditorForm";
			base.ShowInTaskbar = false;
			base.StartPosition = global::System.Windows.Forms.FormStartPosition.CenterScreen;
			this.Text = "Shape Designer";
			base.ResumeLayout(false);
		}

		// Token: 0x04004368 RID: 17256
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04004369 RID: 17257
		private global::Telerik.Charting.Styles.RadShapeEditorControl radShapeEditorControl1;

		// Token: 0x0400436A RID: 17258
		private global::System.Windows.Forms.PropertyGrid propertyGrid1;

		// Token: 0x0400436B RID: 17259
		private global::System.Windows.Forms.Button buttonOk;

		// Token: 0x0400436C RID: 17260
		private global::System.Windows.Forms.Button buttonCancel;
	}
}

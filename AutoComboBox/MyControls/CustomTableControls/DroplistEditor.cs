using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x02000028 RID: 40
	public class DroplistEditor : ColumnTypeEditorPanel
	{
		// Token: 0x06000119 RID: 281 RVA: 0x0000C035 File Offset: 0x0000B035
		public DroplistEditor(DroplistDef target) : this()
		{
			this.__target = target;
		}

		// Token: 0x0600011A RID: 282 RVA: 0x0000C047 File Offset: 0x0000B047
		private DroplistEditor()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600011B RID: 283 RVA: 0x0000C060 File Offset: 0x0000B060
		private void DroplistEditor_Load(object sender, EventArgs e)
		{
			foreach (string item in this.__target.Selections)
			{
				this.list_Selections.Items.Add(item);
			}
		}

		// Token: 0x0600011C RID: 284 RVA: 0x0000C0A8 File Offset: 0x0000B0A8
		public override void save()
		{
			this.__target.Clear();
			foreach (object obj in this.list_Selections.Items)
			{
				string item = (string)obj;
				this.__target.Add(item);
			}
		}

		// Token: 0x0600011D RID: 285 RVA: 0x0000C128 File Offset: 0x0000B128
		private void tb_Add_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Return)
			{
				this.addItem();
			}
		}

		// Token: 0x0600011E RID: 286 RVA: 0x0000C150 File Offset: 0x0000B150
		private void btn_Add_Click(object sender, EventArgs e)
		{
			this.addItem();
		}

		// Token: 0x0600011F RID: 287 RVA: 0x0000C15C File Offset: 0x0000B15C
		private void addItem()
		{
			string text = this.tb_Add.Text;
			if (string.IsNullOrEmpty(text))
			{
				this.tb_Add.Text = "";
			}
			else
			{
				ListBox.ObjectCollection items = this.list_Selections.Items;
				if (!items.Contains(text))
				{
					items.Add(text);
				}
				this.tb_Add.Text = "";
				this.tb_Add.Focus();
			}
		}

		// Token: 0x06000120 RID: 288 RVA: 0x0000C1D8 File Offset: 0x0000B1D8
		private void list_Selections_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				this.deleteItems();
			}
		}

		// Token: 0x06000121 RID: 289 RVA: 0x0000C200 File Offset: 0x0000B200
		private void btn_Remove_Click(object sender, EventArgs e)
		{
			this.deleteItems();
		}

		// Token: 0x06000122 RID: 290 RVA: 0x0000C20C File Offset: 0x0000B20C
		private void deleteItems()
		{
			ListBox.SelectedIndexCollection selectedIndices = this.list_Selections.SelectedIndices;
			ListBox.ObjectCollection items = this.list_Selections.Items;
			for (int i = selectedIndices.Count - 1; i >= 0; i--)
			{
				items.RemoveAt(selectedIndices[i]);
			}
		}

		// Token: 0x06000123 RID: 291 RVA: 0x0000C25C File Offset: 0x0000B25C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000124 RID: 292 RVA: 0x0000C294 File Offset: 0x0000B294
		private void InitializeComponent()
		{
			this.list_Selections = new ListBox();
			this.label1 = new Label();
			this.tb_Add = new TextBox();
			this.btn_Add = new Button();
			this.btn_Remove = new Button();
			base.SuspendLayout();
			this.list_Selections.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.list_Selections.FormattingEnabled = true;
			this.list_Selections.Location = new Point(3, 25);
			this.list_Selections.Name = "list_Selections";
			this.list_Selections.SelectionMode = SelectionMode.MultiSimple;
			this.list_Selections.Size = new Size(401, 498);
			this.list_Selections.TabIndex = 0;
			this.list_Selections.KeyDown += this.list_Selections_KeyDown;
			this.label1.AutoSize = true;
			this.label1.Location = new Point(3, 9);
			this.label1.Name = "label1";
			this.label1.Size = new Size(175, 13);
			this.label1.TabIndex = 1;
			this.label1.Text = "Selections available for this column:";
			this.tb_Add.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.tb_Add.Location = new Point(3, 533);
			this.tb_Add.Name = "tb_Add";
			this.tb_Add.Size = new Size(401, 20);
			this.tb_Add.TabIndex = 2;
			this.tb_Add.KeyDown += this.tb_Add_KeyDown;
			this.btn_Add.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.btn_Add.Location = new Point(410, 533);
			this.btn_Add.Name = "btn_Add";
			this.btn_Add.Size = new Size(75, 23);
			this.btn_Add.TabIndex = 3;
			this.btn_Add.Text = "Add";
			this.btn_Add.UseVisualStyleBackColor = true;
			this.btn_Add.Click += this.btn_Add_Click;
			this.btn_Remove.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.btn_Remove.Location = new Point(410, 485);
			this.btn_Remove.Name = "btn_Remove";
			this.btn_Remove.Size = new Size(75, 38);
			this.btn_Remove.TabIndex = 4;
			this.btn_Remove.Text = "Remove Selected";
			this.btn_Remove.UseVisualStyleBackColor = true;
			this.btn_Remove.Click += this.btn_Remove_Click;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.btn_Remove);
			base.Controls.Add(this.btn_Add);
			base.Controls.Add(this.tb_Add);
			base.Controls.Add(this.label1);
			base.Controls.Add(this.list_Selections);
			base.Name = "DroplistEditor";
			base.Size = new Size(491, 558);
			base.Load += this.DroplistEditor_Load;
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x0400015D RID: 349
		private DroplistDef __target;

		// Token: 0x0400015E RID: 350
		private IContainer components = null;

		// Token: 0x0400015F RID: 351
		private ListBox list_Selections;

		// Token: 0x04000160 RID: 352
		private Label label1;

		// Token: 0x04000161 RID: 353
		private TextBox tb_Add;

		// Token: 0x04000162 RID: 354
		private Button btn_Add;

		// Token: 0x04000163 RID: 355
		private Button btn_Remove;
	}
}

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms;
using System.Xml;
using System.Xml.Serialization;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x0200001D RID: 29
	public class TablePropertyEditor : UserControl
	{
		// Token: 0x060000AD RID: 173 RVA: 0x0000850C File Offset: 0x0000750C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060000AE RID: 174 RVA: 0x00008544 File Offset: 0x00007544
		private void InitializeComponent()
		{
			this.list_Columns = new ListBox();
			this.btn_Add = new Button();
			this.btn_Remove = new Button();
			base.SuspendLayout();
			this.list_Columns.Anchor = (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right);
			this.list_Columns.FormattingEnabled = true;
			this.list_Columns.Location = new Point(0, 0);
			this.list_Columns.Name = "list_Columns";
			this.list_Columns.SelectionMode = SelectionMode.MultiExtended;
			this.list_Columns.Size = new Size(207, 355);
			this.list_Columns.TabIndex = 0;
			this.list_Columns.DoubleClick += this.list_DoubleClicked;
			this.list_Columns.KeyDown += this.list_KeyDown;
			this.list_Columns.MouseHover += this.list_MouseHover;
			this.btn_Add.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.btn_Add.Location = new Point(3, 361);
			this.btn_Add.Name = "btn_Add";
			this.btn_Add.Size = new Size(90, 23);
			this.btn_Add.TabIndex = 2;
			this.btn_Add.Text = "Add Column";
			this.btn_Add.UseVisualStyleBackColor = true;
			this.btn_Add.Click += this.btn_Add_Click;
			this.btn_Remove.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.btn_Remove.Location = new Point(99, 361);
			this.btn_Remove.Name = "btn_Remove";
			this.btn_Remove.Size = new Size(106, 23);
			this.btn_Remove.TabIndex = 3;
			this.btn_Remove.Text = "Remove Columns";
			this.btn_Remove.UseVisualStyleBackColor = true;
			this.btn_Remove.Click += this.btn_Remove_Click;
			base.MouseClick += this.list_MouseClick;
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			this.BackColor = SystemColors.Control;
			base.Controls.Add(this.btn_Remove);
			base.Controls.Add(this.btn_Add);
			base.Controls.Add(this.list_Columns);
			base.Name = "TablePropertyEditor";
			base.Size = new Size(207, 384);
			base.ResumeLayout(false);
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000087EE File Offset: 0x000077EE
		public TablePropertyEditor()
		{
			this.__tp = new TableProperty();
			this.InitializeComponent();
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00008828 File Offset: 0x00007828
		public TablePropertyEditor(TableProperty TableProperty)
		{
			this.__tp = TableProperty;
			this.InitializeComponent();
		}

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000B1 RID: 177 RVA: 0x00008860 File Offset: 0x00007860
		// (set) Token: 0x060000B2 RID: 178 RVA: 0x0000887D File Offset: 0x0000787D
		public string XmlDefinition
		{
			get
			{
				return this.__tp.XmlDefinition;
			}
			set
			{
			}
		}

		// Token: 0x060000B3 RID: 179 RVA: 0x00008880 File Offset: 0x00007880
		private DialogResult showEditorDialog(ColumnDefinition colDef, bool showApply)
		{
			ColumnDefinitionUIEditorForm columnDefinitionUIEditorForm = new ColumnDefinitionUIEditorForm(colDef, this.__existedColumnNames, showApply);
			return columnDefinitionUIEditorForm.ShowDialog();
		}

		// Token: 0x060000B4 RID: 180 RVA: 0x000088A8 File Offset: 0x000078A8
		private void btn_Add_Click(object sender, EventArgs e)
		{
			ColumnDefinition columnDefinition = new ColumnDefinition();
			if (this.showEditorDialog(columnDefinition, false) == DialogResult.OK)
			{
				this.list_Columns.Items.Add(columnDefinition);
				this.__tp.Add(columnDefinition);
			}
		}

		// Token: 0x060000B5 RID: 181 RVA: 0x000088F0 File Offset: 0x000078F0
		private void list_DoubleClicked(object sender, EventArgs e)
		{
			int selectedIndex = this.list_Columns.SelectedIndex;
			if (selectedIndex != -1)
			{
				ColumnDefinition columnDefinition = this.list_Columns.Items[selectedIndex] as ColumnDefinition;
				if (columnDefinition != null)
				{
					this.showEditorDialog(columnDefinition, true);
				}
				this.list_Columns.Items[selectedIndex] = this.list_Columns.Items[selectedIndex];
			}
		}

		// Token: 0x060000B6 RID: 182 RVA: 0x00008961 File Offset: 0x00007961
		private void btn_Remove_Click(object sender, EventArgs e)
		{
			this.DeleteSelected();
		}

		// Token: 0x060000B7 RID: 183 RVA: 0x0000896C File Offset: 0x0000796C
		private void list_KeyDown(object sender, KeyEventArgs e)
		{
			if (e.KeyCode == Keys.Delete)
			{
				this.DeleteSelected();
			}
		}

		// Token: 0x060000B8 RID: 184 RVA: 0x00008994 File Offset: 0x00007994
		public void DeleteSelected()
		{
			ListBox.SelectedIndexCollection selectedIndices = this.list_Columns.SelectedIndices;
			for (int i = selectedIndices.Count - 1; i >= 0; i--)
			{
				this.list_Columns.Items.RemoveAt(selectedIndices[i]);
			}
		}

		// Token: 0x060000B9 RID: 185 RVA: 0x000089E4 File Offset: 0x000079E4
		private void list_MouseHover(object sender, EventArgs args)
		{
			Point point = this.list_Columns.PointToScreen(new Point(0, 0));
			Point position = Cursor.Position;
			Point point2 = new Point(position.X - point.X, position.Y - point.Y);
			int num = this.list_Columns.IndexFromPoint(point2);
			if (num != -1)
			{
				try
				{
					ColumnDefinition columnDefinition = this.list_Columns.Items[num] as ColumnDefinition;
					ColumnTypeDef columnType = columnDefinition.ColumnType;
					MemoryStream memoryStream = new MemoryStream();
					XmlSerializer xmlSerializer = new XmlSerializer(typeof(ColumnTypeDef));
					XmlTextWriter xmlTextWriter = new XmlTextWriter(memoryStream, Encoding.UTF8);
					xmlTextWriter.Formatting = Formatting.Indented;
					xmlSerializer.Serialize(xmlTextWriter, columnType);
					memoryStream = (MemoryStream)xmlTextWriter.BaseStream;
					string @string = new UTF8Encoding().GetString(memoryStream.ToArray());
					this.ToolTip1.Show(@string, this, point2);
				}
				catch (Exception ex)
				{
					MessageBox.Show(ex.StackTrace);
				}
			}
		}

		// Token: 0x060000BA RID: 186 RVA: 0x00008B08 File Offset: 0x00007B08
		private void list_MouseClick(object sender, MouseEventArgs args)
		{
			this.ToolTip1.Hide(this);
		}

		// Token: 0x04000127 RID: 295
		private IContainer components = null;

		// Token: 0x04000128 RID: 296
		private ListBox list_Columns;

		// Token: 0x04000129 RID: 297
		private Button btn_Add;

		// Token: 0x0400012A RID: 298
		private Button btn_Remove;

		// Token: 0x0400012B RID: 299
		private TableProperty __tp;

		// Token: 0x0400012C RID: 300
		private Dictionary<string, string> __existedColumnNames = new Dictionary<string, string>();

		// Token: 0x0400012D RID: 301
		private ToolTip ToolTip1 = new ToolTip();
	}
}

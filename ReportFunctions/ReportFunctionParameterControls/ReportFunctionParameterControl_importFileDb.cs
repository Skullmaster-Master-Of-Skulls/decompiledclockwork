using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Windows.Forms;

namespace ReportFunctions.ReportFunctionParameterControls
{
	// Token: 0x0200002D RID: 45
	public class ReportFunctionParameterControl_importFileDb : UserControl, iReportFunctionParameter
	{
		// Token: 0x060002F1 RID: 753 RVA: 0x0003BA7C File Offset: 0x0003AA7C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x060002F2 RID: 754 RVA: 0x0003BAB4 File Offset: 0x0003AAB4
		private void InitializeComponent()
		{
			this.panel1 = new Panel();
			this.txt_filename = new TextBox();
			this.btn_chooseFile = new Button();
			this.label1 = new Label();
			this.ofd = new OpenFileDialog();
			this.chk_1stRowHoldsColumnNames = new CheckBox();
			this.label2 = new Label();
			this.txt_columnsToEncrypt = new TextBox();
			this.panel2 = new Panel();
			this.panel3 = new Panel();
			this.txt_clockWorkTableToStore = new TextBox();
			this.label3 = new Label();
			this.p_optionalDelimiter = new Panel();
			this.txt_delimiter = new TextBox();
			this.label4 = new Label();
			this.panel1.SuspendLayout();
			this.panel2.SuspendLayout();
			this.panel3.SuspendLayout();
			this.p_optionalDelimiter.SuspendLayout();
			base.SuspendLayout();
			this.panel1.BorderStyle = BorderStyle.Fixed3D;
			this.panel1.Controls.Add(this.txt_filename);
			this.panel1.Controls.Add(this.btn_chooseFile);
			this.panel1.Controls.Add(this.label1);
			this.panel1.Dock = DockStyle.Top;
			this.panel1.Location = new Point(0, 0);
			this.panel1.Margin = new Padding(3, 4, 3, 4);
			this.panel1.Name = "panel1";
			this.panel1.Padding = new Padding(8);
			this.panel1.Size = new Size(438, 44);
			this.panel1.TabIndex = 0;
			this.txt_filename.Dock = DockStyle.Fill;
			this.txt_filename.Location = new Point(73, 8);
			this.txt_filename.Name = "txt_filename";
			this.txt_filename.Size = new Size(310, 22);
			this.txt_filename.TabIndex = 0;
			this.btn_chooseFile.Dock = DockStyle.Right;
			this.btn_chooseFile.Location = new Point(383, 8);
			this.btn_chooseFile.Name = "btn_chooseFile";
			this.btn_chooseFile.Size = new Size(43, 24);
			this.btn_chooseFile.TabIndex = 2;
			this.btn_chooseFile.Text = "...";
			this.btn_chooseFile.UseVisualStyleBackColor = true;
			this.btn_chooseFile.Click += this.btn_chooseFile_Click;
			this.label1.AutoSize = true;
			this.label1.Dock = DockStyle.Left;
			this.label1.Location = new Point(8, 8);
			this.label1.Name = "label1";
			this.label1.Size = new Size(65, 16);
			this.label1.TabIndex = 1;
			this.label1.Text = "Filename:";
			this.label1.TextAlign = ContentAlignment.MiddleLeft;
			this.ofd.FileName = "openFileDialog1";
			this.chk_1stRowHoldsColumnNames.AutoSize = true;
			this.chk_1stRowHoldsColumnNames.Dock = DockStyle.Top;
			this.chk_1stRowHoldsColumnNames.Location = new Point(0, 44);
			this.chk_1stRowHoldsColumnNames.Name = "chk_1stRowHoldsColumnNames";
			this.chk_1stRowHoldsColumnNames.Padding = new Padding(8);
			this.chk_1stRowHoldsColumnNames.Size = new Size(438, 36);
			this.chk_1stRowHoldsColumnNames.TabIndex = 1;
			this.chk_1stRowHoldsColumnNames.Text = "First row holds column names";
			this.chk_1stRowHoldsColumnNames.UseVisualStyleBackColor = true;
			this.label2.AutoSize = true;
			this.label2.Dock = DockStyle.Left;
			this.label2.Location = new Point(4, 4);
			this.label2.Name = "label2";
			this.label2.Size = new Size(125, 16);
			this.label2.TabIndex = 2;
			this.label2.Text = "Columns to encrypt:";
			this.txt_columnsToEncrypt.Dock = DockStyle.Fill;
			this.txt_columnsToEncrypt.Location = new Point(129, 4);
			this.txt_columnsToEncrypt.Name = "txt_columnsToEncrypt";
			this.txt_columnsToEncrypt.Size = new Size(305, 22);
			this.txt_columnsToEncrypt.TabIndex = 3;
			this.panel2.Controls.Add(this.txt_columnsToEncrypt);
			this.panel2.Controls.Add(this.label2);
			this.panel2.Dock = DockStyle.Top;
			this.panel2.Location = new Point(0, 80);
			this.panel2.Name = "panel2";
			this.panel2.Padding = new Padding(4);
			this.panel2.Size = new Size(438, 38);
			this.panel2.TabIndex = 4;
			this.panel3.Controls.Add(this.txt_clockWorkTableToStore);
			this.panel3.Controls.Add(this.label3);
			this.panel3.Dock = DockStyle.Top;
			this.panel3.Location = new Point(0, 118);
			this.panel3.Name = "panel3";
			this.panel3.Padding = new Padding(4);
			this.panel3.Size = new Size(438, 38);
			this.panel3.TabIndex = 5;
			this.txt_clockWorkTableToStore.Dock = DockStyle.Fill;
			this.txt_clockWorkTableToStore.Location = new Point(160, 4);
			this.txt_clockWorkTableToStore.Name = "txt_clockWorkTableToStore";
			this.txt_clockWorkTableToStore.Size = new Size(274, 22);
			this.txt_clockWorkTableToStore.TabIndex = 3;
			this.label3.AutoSize = true;
			this.label3.Dock = DockStyle.Left;
			this.label3.Location = new Point(4, 4);
			this.label3.Name = "label3";
			this.label3.Size = new Size(156, 16);
			this.label3.TabIndex = 2;
			this.label3.Text = "ClockWork table to store:";
			this.p_optionalDelimiter.Controls.Add(this.txt_delimiter);
			this.p_optionalDelimiter.Controls.Add(this.label4);
			this.p_optionalDelimiter.Dock = DockStyle.Top;
			this.p_optionalDelimiter.Location = new Point(0, 156);
			this.p_optionalDelimiter.Name = "p_optionalDelimiter";
			this.p_optionalDelimiter.Padding = new Padding(4);
			this.p_optionalDelimiter.Size = new Size(438, 36);
			this.p_optionalDelimiter.TabIndex = 6;
			this.txt_delimiter.Dock = DockStyle.Left;
			this.txt_delimiter.Location = new Point(117, 4);
			this.txt_delimiter.Name = "txt_delimiter";
			this.txt_delimiter.Size = new Size(43, 22);
			this.txt_delimiter.TabIndex = 3;
			this.label4.AutoSize = true;
			this.label4.Dock = DockStyle.Left;
			this.label4.Location = new Point(4, 4);
			this.label4.Name = "label4";
			this.label4.Size = new Size(113, 16);
			this.label4.TabIndex = 2;
			this.label4.Text = "Optional delimiter:";
			base.AutoScaleDimensions = new SizeF(7f, 16f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Controls.Add(this.p_optionalDelimiter);
			base.Controls.Add(this.panel3);
			base.Controls.Add(this.panel2);
			base.Controls.Add(this.chk_1stRowHoldsColumnNames);
			base.Controls.Add(this.panel1);
			this.Font = new Font("Arial", 9.75f, FontStyle.Regular, GraphicsUnit.Point, 0);
			base.Margin = new Padding(3, 4, 3, 4);
			base.Name = "ReportFunctionParameterControl_importFileDb";
			base.Size = new Size(438, 192);
			this.panel1.ResumeLayout(false);
			this.panel1.PerformLayout();
			this.panel2.ResumeLayout(false);
			this.panel2.PerformLayout();
			this.panel3.ResumeLayout(false);
			this.panel3.PerformLayout();
			this.p_optionalDelimiter.ResumeLayout(false);
			this.p_optionalDelimiter.PerformLayout();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x060002F3 RID: 755 RVA: 0x0003C3D1 File Offset: 0x0003B3D1
		public ReportFunctionParameterControl_importFileDb()
		{
			this.InitializeComponent();
		}

		// Token: 0x060002F4 RID: 756 RVA: 0x0003C3EC File Offset: 0x0003B3EC
		public void Initialize(string FileDialogFilter, bool showOptionalDelimiter)
		{
			this.ofd.Filter = FileDialogFilter;
			if (!showOptionalDelimiter)
			{
				this.p_optionalDelimiter.Visible = false;
			}
		}

		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060002F5 RID: 757 RVA: 0x0003C41C File Offset: 0x0003B41C
		// (set) Token: 0x060002F6 RID: 758 RVA: 0x0003C4DC File Offset: 0x0003B4DC
		public string Parameter
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				string newLine = Environment.NewLine;
				stringBuilder.Append(this.txt_filename.Text);
				stringBuilder.Append(newLine);
				stringBuilder.Append(this.chk_1stRowHoldsColumnNames.Checked ? "1" : "0");
				stringBuilder.Append(newLine);
				stringBuilder.Append(this.txt_columnsToEncrypt.Text);
				stringBuilder.Append(newLine);
				stringBuilder.Append(this.txt_clockWorkTableToStore.Text);
				stringBuilder.Append(newLine);
				if (this.p_optionalDelimiter.Visible)
				{
					stringBuilder.Append(this.txt_delimiter.Text);
				}
				return stringBuilder.ToString();
			}
			set
			{
				string[] array = ReportFunction.SplitStringIntoNEWLINE_delimitered_parts(value, true);
				if (array.Length > 0)
				{
					this.txt_filename.Text = array[0];
				}
				if (array.Length > 1)
				{
					this.chk_1stRowHoldsColumnNames.Checked = (array[1] == "1");
				}
				if (array.Length > 2)
				{
					this.txt_columnsToEncrypt.Text = array[2];
				}
				if (array.Length > 3)
				{
					this.txt_clockWorkTableToStore.Text = array[3];
				}
				if (array.Length > 4 && this.p_optionalDelimiter.Visible)
				{
					this.txt_delimiter.Text = array[4];
				}
			}
		}

		// Token: 0x060002F7 RID: 759 RVA: 0x0003C598 File Offset: 0x0003B598
		private void btn_chooseFile_Click(object sender, EventArgs e)
		{
			DialogResult dialogResult = this.ofd.ShowDialog();
			if (dialogResult == DialogResult.OK)
			{
				this.txt_filename.Text = this.ofd.FileName;
			}
		}

		// Token: 0x04000157 RID: 343
		private IContainer components = null;

		// Token: 0x04000158 RID: 344
		private Panel panel1;

		// Token: 0x04000159 RID: 345
		private TextBox txt_filename;

		// Token: 0x0400015A RID: 346
		private Button btn_chooseFile;

		// Token: 0x0400015B RID: 347
		private Label label1;

		// Token: 0x0400015C RID: 348
		private OpenFileDialog ofd;

		// Token: 0x0400015D RID: 349
		private CheckBox chk_1stRowHoldsColumnNames;

		// Token: 0x0400015E RID: 350
		private Label label2;

		// Token: 0x0400015F RID: 351
		private TextBox txt_columnsToEncrypt;

		// Token: 0x04000160 RID: 352
		private Panel panel2;

		// Token: 0x04000161 RID: 353
		private Panel panel3;

		// Token: 0x04000162 RID: 354
		private TextBox txt_clockWorkTableToStore;

		// Token: 0x04000163 RID: 355
		private Label label3;

		// Token: 0x04000164 RID: 356
		private Panel p_optionalDelimiter;

		// Token: 0x04000165 RID: 357
		private TextBox txt_delimiter;

		// Token: 0x04000166 RID: 358
		private Label label4;
	}
}

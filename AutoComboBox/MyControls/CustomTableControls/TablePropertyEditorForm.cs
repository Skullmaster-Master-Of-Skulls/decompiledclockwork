using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using AutoComboBox.Properties;

namespace AutoComboBox.MyControls.CustomTableControls
{
	// Token: 0x02000078 RID: 120
	public partial class TablePropertyEditorForm : Form
	{
		// Token: 0x060004CA RID: 1226 RVA: 0x00026EC0 File Offset: 0x00025EC0
		public TablePropertyEditorForm()
		{
			this.InitializeComponent();
		}

		// Token: 0x1700010A RID: 266
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x00026EDC File Offset: 0x00025EDC
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x00026EFC File Offset: 0x00025EFC
		public string XmlDefinition
		{
			get
			{
				return this.tp.XmlDefinition;
			}
			set
			{
				if (this.tp != null)
				{
					this.tp.XmlDefinition = value;
				}
			}
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00026F24 File Offset: 0x00025F24
		private void TablePropertyEditorForm_Load(object sender, EventArgs e)
		{
			this.tp = new TableProperty();
			this.tpe = new TablePropertyEditor(this.tp);
			base.Controls.Add(this.tpe);
			this.tpe.Dock = DockStyle.Fill;
			this.tpe.BringToFront();
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00026F79 File Offset: 0x00025F79
		private void toolStripButton1_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x060004CF RID: 1231 RVA: 0x00026F83 File Offset: 0x00025F83
		private void toolStripButton2_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x04000401 RID: 1025
		private TablePropertyEditor tpe;

		// Token: 0x04000402 RID: 1026
		private TableProperty tp;
	}
}

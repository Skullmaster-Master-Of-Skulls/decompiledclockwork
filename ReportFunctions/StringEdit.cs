using System;
using System.ComponentModel;
using System.Drawing;
using System.Windows.Forms;
using ReportFunctions.Properties;

namespace ReportFunctions
{
	// Token: 0x02000035 RID: 53
	public partial class StringEdit : Form
	{
		// Token: 0x06000320 RID: 800 RVA: 0x0003DE80 File Offset: 0x0003CE80
		public StringEdit(string _title, string _message, string _text)
		{
			this.InitializeComponent();
			this.Text = _title;
			this.label1.Text = _message;
			this.textBox1.Text = _text;
		}

		// Token: 0x17000074 RID: 116
		// (get) Token: 0x06000323 RID: 803 RVA: 0x0003E3B0 File Offset: 0x0003D3B0
		// (set) Token: 0x06000324 RID: 804 RVA: 0x0003E3CD File Offset: 0x0003D3CD
		public string UserText
		{
			get
			{
				return this.textBox1.Text;
			}
			set
			{
				this.textBox1.Text = value;
			}
		}

		// Token: 0x06000325 RID: 805 RVA: 0x0003E3DD File Offset: 0x0003D3DD
		private void StringEdit_Load(object sender, EventArgs e)
		{
		}

		// Token: 0x06000326 RID: 806 RVA: 0x0003E3E0 File Offset: 0x0003D3E0
		private void btn_fakeCancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x06000327 RID: 807 RVA: 0x0003E3EA File Offset: 0x0003D3EA
		private void btn_OK_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x06000328 RID: 808 RVA: 0x0003E3FC File Offset: 0x0003D3FC
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}
	}
}

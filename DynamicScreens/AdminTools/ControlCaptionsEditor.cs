using System;
using System.ComponentModel;
using System.Drawing;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using DynamicScreens.Properties;

namespace DynamicScreens.AdminTools
{
	// Token: 0x02000007 RID: 7
	public partial class ControlCaptionsEditor : Form
	{
		// Token: 0x06000079 RID: 121 RVA: 0x00003F82 File Offset: 0x00002F82
		public ControlCaptionsEditor()
		{
			this.InitializeComponent();
		}

		// Token: 0x0600007A RID: 122 RVA: 0x00003F9B File Offset: 0x00002F9B
		public ControlCaptionsEditor(string existingCaptions)
		{
			this.InitializeComponent();
			this.txt.Text = existingCaptions;
		}

		// Token: 0x0600007B RID: 123 RVA: 0x00003FC1 File Offset: 0x00002FC1
		private void btn_ok_Click(object sender, EventArgs e)
		{
			base.DialogResult = DialogResult.OK;
			base.Close();
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00003FD3 File Offset: 0x00002FD3
		private void btn_cancel_Click(object sender, EventArgs e)
		{
			base.Close();
		}

		// Token: 0x0600007D RID: 125 RVA: 0x00003FE0 File Offset: 0x00002FE0
		private void btn_autoFormat_Click(object sender, EventArgs e)
		{
			string text = this.txt.Text.Trim();
			text = Regex.Replace(text, "[^\\u0000-\\u007F]", "");
			string[] array = ScreenEditor.SplitStringIntoNEWLINE_delimitered_parts(text, true);
			this.txt.Text = "";
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < array.Length; i++)
			{
				string text2 = array[i];
				if (i > 0)
				{
					stringBuilder.Append(Environment.NewLine);
				}
				stringBuilder.Append(text2.Trim());
			}
			this.txt.Text = stringBuilder.ToString();
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600007E RID: 126 RVA: 0x00004084 File Offset: 0x00003084
		// (set) Token: 0x0600007F RID: 127 RVA: 0x000040A1 File Offset: 0x000030A1
		public string SelectedControlCaptions
		{
			get
			{
				return this.txt.Text;
			}
			set
			{
				this.txt.Text = value;
			}
		}

		// Token: 0x06000080 RID: 128 RVA: 0x000040B4 File Offset: 0x000030B4
		public static string GetUserInput(string defaultCaptions)
		{
			return ControlCaptionsEditor.GetUserInput(null, defaultCaptions);
		}

		// Token: 0x06000081 RID: 129 RVA: 0x000040D0 File Offset: 0x000030D0
		public static string GetUserInput(IWin32Window owner, string defaultCaptions)
		{
			ControlCaptionsEditor controlCaptionsEditor = new ControlCaptionsEditor(defaultCaptions);
			DialogResult dialogResult = (owner == null) ? controlCaptionsEditor.ShowDialog() : controlCaptionsEditor.ShowDialog(owner);
			string result;
			if (dialogResult == DialogResult.OK)
			{
				result = controlCaptionsEditor.SelectedControlCaptions;
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000082 RID: 130 RVA: 0x00004111 File Offset: 0x00003111
		private void ControlCaptionsEditor_Load(object sender, EventArgs e)
		{
			this.txt.SelectAll();
			base.ActiveControl = this.txt;
		}
	}
}

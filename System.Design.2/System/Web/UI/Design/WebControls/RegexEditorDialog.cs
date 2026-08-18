using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Security.Permissions;
using System.Text.RegularExpressions;
using System.Web.UI.Design.Util;
using System.Windows.Forms;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x020000FC RID: 252
	[ToolboxItem(false)]
	[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
	public partial class RegexEditorDialog : Form
	{
		// Token: 0x17000213 RID: 531
		// (get) Token: 0x060008D2 RID: 2258 RVA: 0x000331D0 File Offset: 0x000313D0
		// (set) Token: 0x060008D3 RID: 2259 RVA: 0x000331D8 File Offset: 0x000313D8
		public string RegularExpression
		{
			get
			{
				return this.regularExpression;
			}
			set
			{
				this.regularExpression = value;
			}
		}

		// Token: 0x060008D4 RID: 2260 RVA: 0x000331E1 File Offset: 0x000313E1
		public RegexEditorDialog(ISite site)
		{
			this.site = site;
			this.InitializeComponent();
			this.settingValue = false;
			this.regularExpression = string.Empty;
		}

		// Token: 0x060008D7 RID: 2263 RVA: 0x00033852 File Offset: 0x00031A52
		protected void txtExpression_TextChanged(object sender, EventArgs e)
		{
			if (this.settingValue || this.firstActivate)
			{
				return;
			}
			this.lblTestResult.Text = string.Empty;
			this.UpdateExpressionList();
		}

		// Token: 0x060008D8 RID: 2264 RVA: 0x0003387C File Offset: 0x00031A7C
		protected void lstStandardExpressions_SelectedIndexChanged(object sender, EventArgs e)
		{
			if (this.settingValue)
			{
				return;
			}
			if (this.lstStandardExpressions.SelectedIndex >= 1)
			{
				RegexEditorDialog.CannedExpression cannedExpression = (RegexEditorDialog.CannedExpression)this.lstStandardExpressions.SelectedItem;
				this.settingValue = true;
				this.txtExpression.Text = cannedExpression.Expression;
				this.settingValue = false;
			}
		}

		// Token: 0x060008D9 RID: 2265 RVA: 0x000338D0 File Offset: 0x00031AD0
		protected void RegexTypeEditor_Activated(object sender, EventArgs e)
		{
			if (!this.firstActivate)
			{
				return;
			}
			this.txtExpression.Text = this.RegularExpression;
			this.UpdateExpressionList();
			this.firstActivate = false;
		}

		// Token: 0x060008DA RID: 2266 RVA: 0x000338FC File Offset: 0x00031AFC
		private void UpdateExpressionList()
		{
			bool flag = false;
			this.settingValue = true;
			string text = this.txtExpression.Text;
			for (int i = 1; i < this.lstStandardExpressions.Items.Count; i++)
			{
				if (text == ((RegexEditorDialog.CannedExpression)this.lstStandardExpressions.Items[i]).Expression)
				{
					this.lstStandardExpressions.SelectedIndex = i;
					flag = true;
				}
			}
			if (!flag)
			{
				this.lstStandardExpressions.SelectedIndex = 0;
			}
			this.settingValue = false;
		}

		// Token: 0x060008DB RID: 2267 RVA: 0x00033980 File Offset: 0x00031B80
		protected void cmdTestValidate_Click(object sender, EventArgs args)
		{
			try
			{
				Match match = Regex.Match(this.txtSampleInput.Text, this.txtExpression.Text);
				bool flag = match.Success && match.Index == 0 && match.Length == this.txtSampleInput.Text.Length;
				if (this.txtSampleInput.Text.Length == 0)
				{
					flag = true;
				}
				this.lblTestResult.Text = (flag ? SR.GetString("RegexEditor_InputValid") : SR.GetString("RegexEditor_InputInvalid"));
				this.lblTestResult.ForeColor = (flag ? Color.Black : Color.Red);
			}
			catch
			{
				this.lblTestResult.Text = SR.GetString("RegexEditor_BadExpression");
				this.lblTestResult.ForeColor = Color.Red;
			}
		}

		// Token: 0x060008DC RID: 2268 RVA: 0x00033A64 File Offset: 0x00031C64
		private void ShowHelp()
		{
			IHelpService helpService = (IHelpService)this.site.GetService(typeof(IHelpService));
			if (helpService != null)
			{
				helpService.ShowHelpFromKeyword("net.Asp.RegularExpressionEditor");
			}
		}

		// Token: 0x060008DD RID: 2269 RVA: 0x00033A9A File Offset: 0x00031C9A
		protected void cmdHelp_Click(object sender, EventArgs e)
		{
			this.ShowHelp();
		}

		// Token: 0x060008DE RID: 2270 RVA: 0x00033A9A File Offset: 0x00031C9A
		private void Form_HelpRequested(object sender, HelpEventArgs e)
		{
			this.ShowHelp();
		}

		// Token: 0x060008DF RID: 2271 RVA: 0x00033AA2 File Offset: 0x00031CA2
		private void HelpButton_Click(object sender, CancelEventArgs e)
		{
			e.Cancel = true;
			this.ShowHelp();
		}

		// Token: 0x060008E0 RID: 2272 RVA: 0x00033AB1 File Offset: 0x00031CB1
		protected void cmdOK_Click(object sender, EventArgs e)
		{
			this.RegularExpression = this.txtExpression.Text;
		}

		// Token: 0x17000214 RID: 532
		// (get) Token: 0x060008E1 RID: 2273 RVA: 0x00033AC4 File Offset: 0x00031CC4
		private object[] CannedExpressions
		{
			get
			{
				if (RegexEditorDialog.cannedExpressions == null)
				{
					ArrayList arrayList = new ArrayList();
					arrayList.Add(SR.GetString("RegexCanned_Custom"));
					arrayList.Add(new RegexEditorDialog.CannedExpression(SR.GetString("RegexCanned_Email"), "\\w+([-+.']\\w+)*@\\w+([-.]\\w+)*\\.\\w+([-.]\\w+)*"));
					arrayList.Add(new RegexEditorDialog.CannedExpression(SR.GetString("RegexCanned_URL"), "http(s)?://([\\w-]+\\.)+[\\w-]+(/[\\w- ./?%&=]*)?"));
					foreach (RegexEditorDialog.RegExpEntry regExpEntry in RegexEditorDialog._entries)
					{
						if (regExpEntry.Name.Length > 0)
						{
							arrayList.Add(new RegexEditorDialog.CannedExpression(SR.GetString(regExpEntry.Name), SR.GetString(regExpEntry.Format)));
						}
					}
					RegexEditorDialog.cannedExpressions = new object[arrayList.Count];
					arrayList.CopyTo(RegexEditorDialog.cannedExpressions);
				}
				return RegexEditorDialog.cannedExpressions;
			}
		}

		// Token: 0x04000548 RID: 1352
		private string regularExpression;

		// Token: 0x04000549 RID: 1353
		private bool settingValue;

		// Token: 0x0400054A RID: 1354
		private bool firstActivate = true;

		// Token: 0x0400054C RID: 1356
		private static object[] cannedExpressions;

		// Token: 0x0400054D RID: 1357
		private static readonly RegexEditorDialog.RegExpEntry[] _entries = new RegexEditorDialog.RegExpEntry[]
		{
			new RegexEditorDialog.RegExpEntry("RegexCanned_SocialSecurity", "RegexCanned_SocialSecurity_Format"),
			new RegexEditorDialog.RegExpEntry("RegexCanned_USPhone", "RegexCanned_USPhone_Format"),
			new RegexEditorDialog.RegExpEntry("RegexCanned_Zip", "RegexCanned_Zip_Format"),
			new RegexEditorDialog.RegExpEntry("RegexCanned_FrZip", "RegexCanned_FrZip_Format"),
			new RegexEditorDialog.RegExpEntry("RegexCanned_FrPhone", "RegexCanned_FrPhone_Format"),
			new RegexEditorDialog.RegExpEntry("RegexCanned_DeZip", "RegexCanned_DeZip_Format"),
			new RegexEditorDialog.RegExpEntry("RegexCanned_DePhone", "RegexCanned_DePhone_Format"),
			new RegexEditorDialog.RegExpEntry("RegexCanned_JpnZip", "RegexCanned_JpnZip_Format"),
			new RegexEditorDialog.RegExpEntry("RegexCanned_JpnPhone", "RegexCanned_JpnPhone_Format"),
			new RegexEditorDialog.RegExpEntry("RegexCanned_PrcZip", "RegexCanned_PrcZip_Format"),
			new RegexEditorDialog.RegExpEntry("RegexCanned_PrcPhone", "RegexCanned_PrcPhone_Format"),
			new RegexEditorDialog.RegExpEntry("RegexCanned_PrcSocialSecurity", "RegexCanned_PrcSocialSecurity_Format")
		};

		// Token: 0x02000429 RID: 1065
		private class CannedExpression
		{
			// Token: 0x06002885 RID: 10373 RVA: 0x000F79BE File Offset: 0x000F5BBE
			public CannedExpression(string description, string expression)
			{
				this.Description = description;
				this.Expression = expression;
			}

			// Token: 0x06002886 RID: 10374 RVA: 0x000F79D4 File Offset: 0x000F5BD4
			public override string ToString()
			{
				return this.Description;
			}

			// Token: 0x04001CD1 RID: 7377
			public string Description;

			// Token: 0x04001CD2 RID: 7378
			public string Expression;
		}

		// Token: 0x0200042A RID: 1066
		private class RegExpEntry
		{
			// Token: 0x06002887 RID: 10375 RVA: 0x000F79DC File Offset: 0x000F5BDC
			public RegExpEntry(string name, string format)
			{
				this.Name = name;
				this.Format = format;
			}

			// Token: 0x04001CD3 RID: 7379
			public string Name;

			// Token: 0x04001CD4 RID: 7380
			public string Format;
		}
	}
}

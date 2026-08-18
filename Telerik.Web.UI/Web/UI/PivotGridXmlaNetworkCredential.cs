using System;
using System.ComponentModel;
using Telerik.Web.UI.PivotGrid.Xmla;

namespace Telerik.Web.UI
{
	// Token: 0x02000DE9 RID: 3561
	public class PivotGridXmlaNetworkCredential : StateManager
	{
		// Token: 0x170029CA RID: 10698
		// (get) Token: 0x0600843A RID: 33850 RVA: 0x001E28AC File Offset: 0x001E0AAC
		// (set) Token: 0x0600843B RID: 33851 RVA: 0x001E28F0 File Offset: 0x001E0AF0
		[DefaultValue("")]
		public string UserName
		{
			get
			{
				string result = string.Empty;
				if (base.ViewState["UserName"] == null)
				{
					result = string.Empty;
				}
				else
				{
					result = base.ViewState["UserName"].ToString();
				}
				return result;
			}
			set
			{
				base.ViewState["UserName"] = value;
			}
		}

		// Token: 0x170029CB RID: 10699
		// (get) Token: 0x0600843C RID: 33852 RVA: 0x001E2904 File Offset: 0x001E0B04
		// (set) Token: 0x0600843D RID: 33853 RVA: 0x001E2948 File Offset: 0x001E0B48
		[DefaultValue("")]
		public string PassWord
		{
			get
			{
				string result = string.Empty;
				if (base.ViewState["PassWord"] == null)
				{
					result = string.Empty;
				}
				else
				{
					result = base.ViewState["PassWord"].ToString();
				}
				return result;
			}
			set
			{
				base.ViewState["PassWord"] = value;
			}
		}

		// Token: 0x170029CC RID: 10700
		// (get) Token: 0x0600843E RID: 33854 RVA: 0x001E295C File Offset: 0x001E0B5C
		// (set) Token: 0x0600843F RID: 33855 RVA: 0x001E29A0 File Offset: 0x001E0BA0
		[DefaultValue("")]
		public string Domain
		{
			get
			{
				string result = string.Empty;
				if (base.ViewState["Domain"] == null)
				{
					result = string.Empty;
				}
				else
				{
					result = base.ViewState["Domain"].ToString();
				}
				return result;
			}
			set
			{
				base.ViewState["Domain"] = value;
			}
		}

		// Token: 0x06008440 RID: 33856 RVA: 0x001E29B4 File Offset: 0x001E0BB4
		public XmlaNetworkCredential ToCoreXmlaNetworkCredentials()
		{
			XmlaNetworkCredential result = null;
			if (!string.IsNullOrEmpty(this.UserName) && !string.IsNullOrEmpty(this.PassWord) && !string.IsNullOrEmpty(this.Domain))
			{
				result = new XmlaNetworkCredential(this.UserName, this.PassWord, this.Domain);
			}
			else if (!string.IsNullOrEmpty(this.UserName))
			{
				result = new XmlaNetworkCredential(this.UserName, this.PassWord);
			}
			return result;
		}
	}
}

using System;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x020005AC RID: 1452
	internal sealed class WebPartRestoreVerb : WebPartActionVerb
	{
		// Token: 0x1700159A RID: 5530
		// (get) Token: 0x0600498E RID: 18830 RVA: 0x000F4A38 File Offset: 0x000F2C38
		private string DefaultDescription
		{
			get
			{
				if (this._defaultDescription == null)
				{
					this._defaultDescription = SR.GetString("WebPartRestoreVerb_Description");
				}
				return this._defaultDescription;
			}
		}

		// Token: 0x1700159B RID: 5531
		// (get) Token: 0x0600498F RID: 18831 RVA: 0x000F4A58 File Offset: 0x000F2C58
		private string DefaultText
		{
			get
			{
				if (this._defaultText == null)
				{
					this._defaultText = SR.GetString("WebPartRestoreVerb_Text");
				}
				return this._defaultText;
			}
		}

		// Token: 0x1700159C RID: 5532
		// (get) Token: 0x06004990 RID: 18832 RVA: 0x000F4A78 File Offset: 0x000F2C78
		// (set) Token: 0x06004991 RID: 18833 RVA: 0x000EA88A File Offset: 0x000E8A8A
		[WebSysDefaultValue("WebPartRestoreVerb_Description")]
		public override string Description
		{
			get
			{
				object obj = base.ViewState["Description"];
				if (obj != null)
				{
					return (string)obj;
				}
				return this.DefaultDescription;
			}
			set
			{
				base.ViewState["Description"] = value;
			}
		}

		// Token: 0x1700159D RID: 5533
		// (get) Token: 0x06004992 RID: 18834 RVA: 0x000F4AA8 File Offset: 0x000F2CA8
		// (set) Token: 0x06004993 RID: 18835 RVA: 0x000EA8D2 File Offset: 0x000E8AD2
		[WebSysDefaultValue("WebPartRestoreVerb_Text")]
		public override string Text
		{
			get
			{
				object obj = base.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return this.DefaultText;
			}
			set
			{
				base.ViewState["Text"] = value;
			}
		}

		// Token: 0x040027AD RID: 10157
		private string _defaultDescription;

		// Token: 0x040027AE RID: 10158
		private string _defaultText;
	}
}

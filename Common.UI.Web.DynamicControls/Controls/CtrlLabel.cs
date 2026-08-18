using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.UI.Web.DynamicControls.Entity;

namespace TechnoPro.Common.UI.Web.DynamicControls.Controls
{
	// Token: 0x02000008 RID: 8
	[ToolboxData("<{0}:CtrlLabel runat=server></{0}:CtrlLabel>")]
	public class CtrlLabel : Label, IDynamicWebControl, INamingContainer
	{
		// Token: 0x06000067 RID: 103 RVA: 0x00003978 File Offset: 0x00001B78
		public CtrlLabel()
		{
			this.EnableViewState = false;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00003992 File Offset: 0x00001B92
		public CtrlLabel(DynamicFieldDTO Field)
		{
			this.DynamicField = Field;
			this.EnableViewState = false;
		}

		// Token: 0x06000069 RID: 105 RVA: 0x000039B3 File Offset: 0x00001BB3
		public override void Dispose()
		{
			if (this.lbl != null)
			{
				this.lbl.Dispose();
			}
			base.Dispose();
		}

		// Token: 0x0600006A RID: 106 RVA: 0x000039CE File Offset: 0x00001BCE
		protected override void CreateChildControls()
		{
			this.BuildControlHeiarchy();
			base.CreateChildControls();
		}

		// Token: 0x0600006B RID: 107 RVA: 0x000039DC File Offset: 0x00001BDC
		protected override void OnInit(EventArgs e)
		{
			this.InitializeControls();
			base.OnInit(e);
		}

		// Token: 0x0600006C RID: 108 RVA: 0x000039EC File Offset: 0x00001BEC
		private void InitializeControls()
		{
			int num;
			bool flag;
			string text;
			int num2;
			int num3;
			if (this.DynamicField != null)
			{
				num = this.DynamicField.ControlId;
				flag = this.DynamicField.HideCaption;
				text = this.DynamicField.ControlCaption;
				num2 = this.DynamicField.DefaultValue;
				int setting = this.DynamicField.Setting4;
				num3 = this.DynamicField.Setting1;
			}
			else
			{
				num = 0;
				flag = false;
				text = "lbl";
				num2 = 0;
				num3 = 0;
			}
			string str = num.ToString();
			this.lbl.ID = "lbl_" + str;
			if (flag)
			{
				this.lbl.Text = "";
			}
			else
			{
				this.lbl.Text = text;
			}
			if (num2 > 0)
			{
				int num4 = Convert.ToInt32(Convert.ToDouble(num2) / 100.0);
				this.lbl.Style.Add(HtmlTextWriterStyle.FontSize, num4.ToString() + "em");
			}
			if (num3 > 0)
			{
				int num5 = num3;
				if ((num5 & 1) == 1)
				{
					this.lbl.Style.Add(HtmlTextWriterStyle.FontWeight, "bold");
				}
				if ((num5 & 2) == 2)
				{
					this.lbl.Style.Add(HtmlTextWriterStyle.TextDecoration, "italic");
				}
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00003B18 File Offset: 0x00001D18
		private void BuildControlHeiarchy()
		{
			this.Controls.Add(this.lbl);
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00003B2B File Offset: 0x00001D2B
		protected override void Render(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "cxform");
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			this.lbl.RenderControl(writer);
			writer.RenderEndTag();
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x0600006F RID: 111 RVA: 0x00003B54 File Offset: 0x00001D54
		// (set) Token: 0x06000070 RID: 112 RVA: 0x00003B5C File Offset: 0x00001D5C
		public DynamicFieldDTO DynamicField { get; set; }

		// Token: 0x17000015 RID: 21
		// (get) Token: 0x06000071 RID: 113 RVA: 0x00003B65 File Offset: 0x00001D65
		// (set) Token: 0x06000072 RID: 114 RVA: 0x00003B6D File Offset: 0x00001D6D
		public DynamicDataDTO DynamicData { get; set; }

		// Token: 0x06000073 RID: 115 RVA: 0x00003883 File Offset: 0x00001A83
		public void ChildLoadViewState(object dataFromViewState)
		{
		}

		// Token: 0x06000074 RID: 116 RVA: 0x00003885 File Offset: 0x00001A85
		public object ChildSaveViewState()
		{
			return null;
		}

		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000075 RID: 117 RVA: 0x00003885 File Offset: 0x00001A85
		public string ViewStateKey
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06000076 RID: 118 RVA: 0x00003883 File Offset: 0x00001A83
		public void ClearData()
		{
		}

		// Token: 0x06000077 RID: 119 RVA: 0x00003883 File Offset: 0x00001A83
		public void ShowData(DynamicDataDTO data)
		{
		}

		// Token: 0x06000078 RID: 120 RVA: 0x00003948 File Offset: 0x00001B48
		public DynamicDataDTO GetCurrentData(out bool isEmpty)
		{
			isEmpty = true;
			return null;
		}

		// Token: 0x0400001E RID: 30
		private const string ID_PREFIX = "lbl_";

		// Token: 0x0400001F RID: 31
		private Label lbl = new Label();
	}
}

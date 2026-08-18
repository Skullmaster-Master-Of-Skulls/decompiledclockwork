using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.UI.Web.DynamicControls.Entity;

namespace TechnoPro.Common.UI.Web.DynamicControls.Controls
{
	// Token: 0x0200000B RID: 11
	[ToolboxData("<{0}:CtrlTextBox runat=server></{0}:CtrlTextBox>")]
	public class CtrlTextBox : WebControl, IDynamicWebControl, INamingContainer
	{
		// Token: 0x0600009F RID: 159 RVA: 0x0000434A File Offset: 0x0000254A
		public CtrlTextBox()
		{
			this.EnableViewState = false;
		}

		// Token: 0x060000A0 RID: 160 RVA: 0x0000437A File Offset: 0x0000257A
		public CtrlTextBox(DynamicFieldDTO Field)
		{
			this.DynamicField = Field;
			this.EnableViewState = false;
		}

		// Token: 0x060000A1 RID: 161 RVA: 0x000043B1 File Offset: 0x000025B1
		public override void Dispose()
		{
			if (this.txt != null)
			{
				this.txt.Dispose();
			}
			if (this.lbl != null)
			{
				this.lbl.Dispose();
			}
			base.Dispose();
		}

		// Token: 0x060000A2 RID: 162 RVA: 0x000043DF File Offset: 0x000025DF
		protected override void CreateChildControls()
		{
			this.BuildControlHeiarchy();
			base.CreateChildControls();
		}

		// Token: 0x060000A3 RID: 163 RVA: 0x000043ED File Offset: 0x000025ED
		protected override void OnInit(EventArgs e)
		{
			this.InitializeControls();
			base.OnInit(e);
		}

		// Token: 0x060000A4 RID: 164 RVA: 0x000043FC File Offset: 0x000025FC
		private void InitializeControls()
		{
			int num;
			string text;
			bool flag;
			bool flag2;
			eEnforceTypeDTO eEnforceTypeDTO;
			int num2;
			int num3;
			if (this.DynamicField != null)
			{
				num = this.DynamicField.ControlId;
				text = this.DynamicField.ControlCaption;
				flag = this.DynamicField.HideCaption;
				flag2 = this.DynamicField.IsReadOnly;
				eEnforceTypeDTO = this.DynamicField.EnforceMethod;
				num2 = this.DynamicField.Setting1;
				num3 = this.DynamicField.Setting2;
			}
			else
			{
				num = 0;
				text = "text box";
				flag = false;
				flag2 = false;
				eEnforceTypeDTO = eEnforceTypeDTO.Optional;
				num2 = 0;
				num3 = 0;
			}
			string str = num.ToString();
			this.txt.ID = "txt_" + str;
			this.validator.ID = "val_txt_" + str;
			this.lbl.ID = "hlbl_txt_" + str;
			this.lbl.AssociatedControlID = this.txt.ID;
			this.validator.CssClass = "cxformval";
			if (num2 > 1)
			{
				this.txt.CssClass = "cxformtxtmulti";
				this.txt.TextMode = TextBoxMode.MultiLine;
				this.txt.Rows = num2;
			}
			else if (num3 > 0)
			{
				this.txt.CssClass = "cxformtxtsmall";
				this.txt.Columns = num3;
			}
			else
			{
				this.txt.CssClass = "cxformctrl";
			}
			if (flag)
			{
				this.lbl.Text = "";
			}
			else if (text.Length > 25)
			{
				this.lbl.Text = text + "<br />";
				this.lbl.CssClass = "cxformtitlewide";
			}
			else
			{
				this.lbl.Text = text;
				this.lbl.CssClass = "cxformtitle";
			}
			if (flag2)
			{
				this.txt.Enabled = false;
			}
			this.validator.ControlToValidate = this.txt.ID;
			if (eEnforceTypeDTO == eEnforceTypeDTO.Error)
			{
				this.validator.ErrorMessage = "Please enter something in order to continue.";
				this.validator.Display = ValidatorDisplay.Dynamic;
				return;
			}
			this.validator.Enabled = false;
			this.validator.Visible = false;
		}

		// Token: 0x060000A5 RID: 165 RVA: 0x00004618 File Offset: 0x00002818
		protected override void Render(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "cxform");
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			this.lbl.RenderControl(writer);
			if (this.lbl.CssClass.Equals("cxformtitlewide"))
			{
				writer.Write("<span class='cxformtitle'> </span>");
			}
			this.txt.RenderControl(writer);
			if (this.validator != null)
			{
				this.validator.RenderControl(writer);
			}
			writer.RenderEndTag();
		}

		// Token: 0x060000A6 RID: 166 RVA: 0x0000468E File Offset: 0x0000288E
		private void BuildControlHeiarchy()
		{
			this.Controls.Add(this.lbl);
			this.Controls.Add(this.txt);
			this.Controls.Add(this.validator);
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x060000A7 RID: 167 RVA: 0x000046C3 File Offset: 0x000028C3
		// (set) Token: 0x060000A8 RID: 168 RVA: 0x000046CB File Offset: 0x000028CB
		public DynamicFieldDTO DynamicField { get; set; }

		// Token: 0x1700001E RID: 30
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x000046D4 File Offset: 0x000028D4
		// (set) Token: 0x060000AA RID: 170 RVA: 0x000046DC File Offset: 0x000028DC
		public DynamicDataDTO DynamicData { get; set; }

		// Token: 0x060000AB RID: 171 RVA: 0x000046E8 File Offset: 0x000028E8
		public void ChildLoadViewState(object dataFromViewState)
		{
			if (dataFromViewState == null)
			{
				this.txt.Text = "";
				return;
			}
			this.txt.Text = (string)dataFromViewState;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x0000471C File Offset: 0x0000291C
		public object ChildSaveViewState()
		{
			return this.txt.Text;
		}

		// Token: 0x1700001F RID: 31
		// (get) Token: 0x060000AD RID: 173 RVA: 0x0000472C File Offset: 0x0000292C
		public string ViewStateKey
		{
			get
			{
				if (!string.IsNullOrEmpty(this.txt.ID))
				{
					return "v" + this.txt.ID;
				}
				if (this.DynamicField != null)
				{
					this.txt.ID = "txt_" + this.DynamicField.ControlId.ToString();
					return "v" + this.txt.ID;
				}
				return "txt_nocid";
			}
		}

		// Token: 0x060000AE RID: 174 RVA: 0x000047AC File Offset: 0x000029AC
		public void ClearData()
		{
			this.txt.Text = "";
		}

		// Token: 0x060000AF RID: 175 RVA: 0x000047C0 File Offset: 0x000029C0
		public void ShowData(DynamicDataDTO data)
		{
			string text;
			if (data == null || data.Value == null)
			{
				text = "";
			}
			else if (data.Value is string)
			{
				text = (string)data.Value;
			}
			else
			{
				text = data.Value.ToString();
			}
			this.txt.Text = text;
		}

		// Token: 0x060000B0 RID: 176 RVA: 0x00004814 File Offset: 0x00002A14
		public DynamicDataDTO GetCurrentData(out bool isEmpty)
		{
			string text = this.txt.Text.Trim();
			isEmpty = (text.Length < 1);
			if (this.DynamicData == null)
			{
				this.DynamicData = new DynamicDataDTO
				{
					Field = this.DynamicField,
					DataId = 0,
					Value = text
				};
			}
			else
			{
				this.DynamicData.Value = text;
			}
			return this.DynamicData;
		}

		// Token: 0x0400002B RID: 43
		private RequiredFieldValidator validator = new RequiredFieldValidator();

		// Token: 0x0400002C RID: 44
		private const string ID_PREFIX = "txt_";

		// Token: 0x0400002D RID: 45
		private Label lbl = new Label();

		// Token: 0x0400002E RID: 46
		private TextBox txt = new TextBox();
	}
}

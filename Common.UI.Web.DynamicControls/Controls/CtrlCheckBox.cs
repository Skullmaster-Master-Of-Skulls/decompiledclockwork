using System;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;
using skmValidators;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.UI.Web.DynamicControls.Entity;

namespace TechnoPro.Common.UI.Web.DynamicControls.Controls
{
	// Token: 0x02000003 RID: 3
	[ToolboxData("<{0}:CtrlCheckBox runat=server></{0}:CtrlCheckBox>")]
	public class CtrlCheckBox : WebControl, IDynamicWebControl, INamingContainer
	{
		// Token: 0x0600000B RID: 11 RVA: 0x00002050 File Offset: 0x00000250
		public CtrlCheckBox()
		{
			base.EnableViewState = false;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002075 File Offset: 0x00000275
		public CtrlCheckBox(DynamicFieldDTO Field)
		{
			this.DynamicField = Field;
			base.EnableViewState = false;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x000020A1 File Offset: 0x000002A1
		public override void Dispose()
		{
			if (this.chk != null)
			{
				this.chk.Dispose();
			}
			base.Dispose();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000020BC File Offset: 0x000002BC
		protected override void CreateChildControls()
		{
			this.BuildControlHeiarchy();
			base.CreateChildControls();
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000020CA File Offset: 0x000002CA
		protected override void OnInit(EventArgs e)
		{
			this.InitializeControls();
			base.OnInit(e);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x000020DC File Offset: 0x000002DC
		private void InitializeControls()
		{
			int num;
			bool flag;
			string text;
			int num2;
			int num3;
			int num4;
			bool flag2;
			eEnforceTypeDTO eEnforceTypeDTO;
			if (this.DynamicField != null)
			{
				num = this.DynamicField.ControlId;
				flag = this.DynamicField.HideCaption;
				text = this.DynamicField.ControlCaption;
				num2 = this.DynamicField.Setting3;
				num3 = this.DynamicField.Setting4;
				int defaultValue = this.DynamicField.DefaultValue;
				num4 = this.DynamicField.DefaultValue >> 1;
				flag2 = this.DynamicField.IsReadOnly;
				eEnforceTypeDTO = this.DynamicField.EnforceMethod;
			}
			else
			{
				int num5 = this.idCounter;
				this.idCounter = num5 + 1;
				num = num5;
				flag = false;
				text = "no cid";
				num2 = 0;
				num3 = 0;
				num4 = 0;
				flag2 = false;
				eEnforceTypeDTO = eEnforceTypeDTO.Optional;
			}
			string str = num.ToString();
			this.chk.ID = "chk_" + str;
			this.validator.ID = "val_chk_" + str;
			this.chk.CssClass = "cxformctrl";
			this.chk.Attributes.Add("onClick", "hideDynamicControl(2)");
			if (flag)
			{
				this.chk.Text = "";
			}
			else
			{
				this.chk.Text = text;
			}
			if (flag2)
			{
				this.chk.Enabled = false;
			}
			if (num2 > 0)
			{
				double num6 = Convert.ToDouble(num2) / 100.0;
				this.chk.Attributes.CssStyle[HtmlTextWriterStyle.FontSize] = num6.ToString();
			}
			if (num3 > 0)
			{
				this.chk.Attributes.CssStyle[HtmlTextWriterStyle.BackgroundColor] = ColorTranslator.ToHtml(Color.FromArgb(num3));
			}
			if (num4 > 0)
			{
				this.chk.Attributes.CssStyle[HtmlTextWriterStyle.PaddingLeft] = num4.ToString();
			}
			this.validator.ControlToValidate = this.chk.ID;
			this.validator.CssClass = "cxformchk";
			if (eEnforceTypeDTO == eEnforceTypeDTO.Error)
			{
				this.validator.ErrorMessage = "Please check the box in order to continue.";
				this.validator.Display = ValidatorDisplay.Dynamic;
			}
			else
			{
				this.validator.Enabled = false;
				this.validator.Visible = false;
			}
			this.chk.CheckedChanged += this.chk_CheckedChanged;
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002318 File Offset: 0x00000518
		protected override void Render(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "cxform");
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			this.chk.RenderControl(writer);
			if (this.validator.Enabled)
			{
				this.validator.RenderControl(writer);
			}
			writer.RenderEndTag();
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002368 File Offset: 0x00000568
		private void BuildControlHeiarchy()
		{
			this.Controls.Add(this.chk);
			if (this.validator != null)
			{
				this.Controls.Add(this.validator);
			}
			if (this.DynamicField != null && (this.DynamicField.DefaultValue & 1) == 1)
			{
				this.chk.Checked = true;
			}
		}

		// Token: 0x06000013 RID: 19 RVA: 0x000023C5 File Offset: 0x000005C5
		private void chk_CheckedChanged(object sender, EventArgs e)
		{
			this.FireOnCheckedChanged();
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000023CD File Offset: 0x000005CD
		private void FireOnCheckedChanged()
		{
			if (this.OnCheckedChanged != null)
			{
				this.OnCheckedChanged(this, new EventArgs());
			}
		}

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000015 RID: 21 RVA: 0x000023E8 File Offset: 0x000005E8
		// (remove) Token: 0x06000016 RID: 22 RVA: 0x00002420 File Offset: 0x00000620
		public event EventHandler OnCheckedChanged;

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000017 RID: 23 RVA: 0x00002455 File Offset: 0x00000655
		// (set) Token: 0x06000018 RID: 24 RVA: 0x0000245D File Offset: 0x0000065D
		public DynamicFieldDTO DynamicField { get; set; }

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000019 RID: 25 RVA: 0x00002466 File Offset: 0x00000666
		// (set) Token: 0x0600001A RID: 26 RVA: 0x0000246E File Offset: 0x0000066E
		public DynamicDataDTO DynamicData { get; set; }

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x0600001B RID: 27 RVA: 0x00002478 File Offset: 0x00000678
		public string ViewStateKey
		{
			get
			{
				if (!string.IsNullOrEmpty(this.chk.ID))
				{
					return "v" + this.chk.ID;
				}
				if (this.DynamicField != null)
				{
					this.chk.ID = "chk_" + this.DynamicField.ControlId.ToString();
					return "v" + this.chk.ID;
				}
				return "chkNocid";
			}
		}

		// Token: 0x0600001C RID: 28 RVA: 0x000024F8 File Offset: 0x000006F8
		public void ChildLoadViewState(object dataFromViewState)
		{
			if (dataFromViewState == null)
			{
				this.chk.Checked = false;
				return;
			}
			this.chk.Checked = Convert.ToBoolean(dataFromViewState);
		}

		// Token: 0x0600001D RID: 29 RVA: 0x00002528 File Offset: 0x00000728
		public object ChildSaveViewState()
		{
			return this.chk.Checked;
		}

		// Token: 0x0600001E RID: 30 RVA: 0x0000253C File Offset: 0x0000073C
		public void ShowData(DynamicDataDTO data)
		{
			this.DynamicData = data;
			bool @checked;
			if (data == null || data.Value == null)
			{
				@checked = false;
			}
			else if (data.Value is bool)
			{
				@checked = (bool)data.Value;
			}
			else if (data.Value is int)
			{
				@checked = ((int)data.Value != 0);
			}
			else
			{
				string text = data.Value.ToString();
				@checked = ("1yestrue".IndexOf(text.ToLower()) >= 0);
			}
			this.chk.Checked = @checked;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x000025C8 File Offset: 0x000007C8
		public DynamicDataDTO GetCurrentData(out bool isEmpty)
		{
			int num = this.chk.Checked ? 1 : 0;
			isEmpty = (num != 1);
			if (this.DynamicData == null)
			{
				this.DynamicData = new DynamicDataDTO
				{
					Field = this.DynamicField,
					DataId = 0,
					Value = num,
					ValueId = num
				};
			}
			else
			{
				this.DynamicData.Value = num;
				this.DynamicData.ValueId = num;
			}
			return this.DynamicData;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x0000264E File Offset: 0x0000084E
		public void ClearData()
		{
			this.chk.Checked = false;
		}

		// Token: 0x04000001 RID: 1
		private CheckBoxValidator validator = new CheckBoxValidator();

		// Token: 0x04000002 RID: 2
		private const string ID_PREFIX = "chk_";

		// Token: 0x04000003 RID: 3
		private CheckBox chk = new CheckBox();

		// Token: 0x04000004 RID: 4
		private int idCounter;
	}
}

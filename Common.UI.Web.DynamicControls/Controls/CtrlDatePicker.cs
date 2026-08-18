using System;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using Telerik.Web.UI;

namespace TechnoPro.Common.UI.Web.DynamicControls.Controls
{
	// Token: 0x02000004 RID: 4
	[ToolboxData("<{0}:CtrlDatePicker runat=server></{0}:CtrlDatePicker>")]
	public class CtrlDatePicker : WebControl
	{
		// Token: 0x06000021 RID: 33 RVA: 0x0000265C File Offset: 0x0000085C
		public CtrlDatePicker()
		{
			this.EnableViewState = false;
		}

		// Token: 0x06000022 RID: 34 RVA: 0x0000268C File Offset: 0x0000088C
		public CtrlDatePicker(DynamicFieldDTO Field)
		{
			this.DynamicField = Field;
			this.EnableViewState = false;
		}

		// Token: 0x06000023 RID: 35 RVA: 0x000026C3 File Offset: 0x000008C3
		public override void Dispose()
		{
			if (this.dtp != null)
			{
				this.dtp.Dispose();
			}
			if (this.lbl != null)
			{
				this.lbl.Dispose();
			}
			base.Dispose();
		}

		// Token: 0x06000024 RID: 36 RVA: 0x000026F1 File Offset: 0x000008F1
		protected override void CreateChildControls()
		{
			this.BuildControlHeiarchy();
			base.CreateChildControls();
		}

		// Token: 0x06000025 RID: 37 RVA: 0x000026FF File Offset: 0x000008FF
		protected override void OnInit(EventArgs e)
		{
			this.InitializeControls();
			base.OnInit(e);
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00002710 File Offset: 0x00000910
		private void InitializeControls()
		{
			int num;
			string text;
			bool flag;
			bool flag2;
			eEnforceTypeDTO eEnforceTypeDTO;
			if (this.DynamicField != null)
			{
				num = this.DynamicField.ControlId;
				text = this.DynamicField.ControlCaption;
				flag = this.DynamicField.HideCaption;
				flag2 = this.DynamicField.IsReadOnly;
				eEnforceTypeDTO = this.DynamicField.EnforceMethod;
			}
			else
			{
				num = 0;
				text = "text box";
				flag = false;
				flag2 = false;
				eEnforceTypeDTO = eEnforceTypeDTO.Optional;
			}
			string str = num.ToString();
			this.dtp.ID = "dtp_" + str;
			this.lbl.ID = "hlbl_dtp_" + str;
			this.validator.ID = "val_dtp_" + str;
			this.lbl.CssClass = "cxformtitle";
			this.dtp.CssClass = "cxformctrl";
			this.validator.CssClass = "cxformval";
			this.lbl.AssociatedControlID = this.dtp.ID;
			if (flag)
			{
				this.lbl.Text = "";
			}
			else
			{
				this.lbl.Text = text;
			}
			if (flag2)
			{
				this.dtp.Enabled = false;
			}
			this.validator.ControlToValidate = this.dtp.ID;
			if (eEnforceTypeDTO == eEnforceTypeDTO.Error)
			{
				this.validator.ErrorMessage = "Please enter something in order to continue.";
				this.validator.Display = ValidatorDisplay.Dynamic;
				return;
			}
			this.validator.Enabled = false;
			this.validator.Visible = false;
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002888 File Offset: 0x00000A88
		protected override void Render(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "cxform");
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			this.lbl.RenderControl(writer);
			this.dtp.RenderControl(writer);
			if (this.validator != null)
			{
				this.validator.RenderControl(writer);
			}
			writer.RenderEndTag();
		}

		// Token: 0x06000028 RID: 40 RVA: 0x000028DC File Offset: 0x00000ADC
		private void BuildControlHeiarchy()
		{
			this.Controls.Add(this.lbl);
			this.Controls.Add(this.dtp);
			this.Controls.Add(this.validator);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002911 File Offset: 0x00000B11
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002919 File Offset: 0x00000B19
		public DynamicFieldDTO DynamicField { get; set; }

		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002922 File Offset: 0x00000B22
		// (set) Token: 0x0600002C RID: 44 RVA: 0x0000292A File Offset: 0x00000B2A
		public DynamicDataDTO DynamicData { get; set; }

		// Token: 0x0600002D RID: 45 RVA: 0x00002934 File Offset: 0x00000B34
		public void ChildLoadViewState(object dataFromViewState)
		{
			if (dataFromViewState == null)
			{
				this.dtp.SelectedDate = null;
				return;
			}
			this.dtp.SelectedDate = (DateTime?)dataFromViewState;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x0000296C File Offset: 0x00000B6C
		public object ChildSaveViewState()
		{
			return this.dtp.SelectedDate;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x0600002F RID: 47 RVA: 0x00002980 File Offset: 0x00000B80
		public string ViewStateKey
		{
			get
			{
				if (!string.IsNullOrEmpty(this.dtp.ID))
				{
					return "v" + this.dtp.ID;
				}
				if (this.DynamicField != null)
				{
					this.dtp.ID = "dtp_" + this.DynamicField.ControlId.ToString();
					return "v" + this.dtp.ID;
				}
				return "txt_nocid";
			}
		}

		// Token: 0x06000030 RID: 48 RVA: 0x00002A00 File Offset: 0x00000C00
		public void ShowData(DynamicDataDTO data)
		{
			this.DynamicData = data;
			DateTime? selectedDate;
			if (data == null || data.Value == null)
			{
				selectedDate = null;
			}
			else if (data.Value is DateTime)
			{
				selectedDate = new DateTime?((DateTime)data.Value);
			}
			else
			{
				selectedDate = null;
			}
			this.dtp.SelectedDate = selectedDate;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002A60 File Offset: 0x00000C60
		public DynamicDataDTO GetCurrentData(out bool isEmpty)
		{
			DateTime? selectedDate = this.dtp.SelectedDate;
			isEmpty = (selectedDate == null);
			if (this.DynamicData == null)
			{
				this.DynamicData = new DynamicDataDTO
				{
					Field = this.DynamicField,
					DataId = 0,
					Value = selectedDate
				};
			}
			else
			{
				this.DynamicData.Value = selectedDate;
			}
			return this.DynamicData;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00002AD0 File Offset: 0x00000CD0
		public void ClearData()
		{
			this.dtp.SelectedDate = null;
		}

		// Token: 0x04000008 RID: 8
		private RequiredFieldValidator validator = new RequiredFieldValidator();

		// Token: 0x04000009 RID: 9
		private const string ID_PREFIX = "dtp_";

		// Token: 0x0400000A RID: 10
		private Label lbl = new Label();

		// Token: 0x0400000B RID: 11
		private RadDateTimePicker dtp = new RadDateTimePicker();
	}
}

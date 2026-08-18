using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Caching;
using System.Web.UI;
using System.Web.UI.WebControls;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.ClientManager.Core.DynamicForms;
using TechnoPro.Common.ClientManager.ICore.DynamicForms;
using TechnoPro.Common.UI.Web.DynamicControls.Entity;

namespace TechnoPro.Common.UI.Web.DynamicControls.Controls
{
	// Token: 0x0200000A RID: 10
	[ToolboxData("<{0}:CtrlRadioGroup runat=server></{0}:CtrlRadioGroup>")]
	public class CtrlRadioGroup : WebControl, IDynamicWebControl, INamingContainer
	{
		// Token: 0x0600008B RID: 139 RVA: 0x00003C06 File Offset: 0x00001E06
		public CtrlRadioGroup()
		{
			this.EnableViewState = false;
		}

		// Token: 0x0600008C RID: 140 RVA: 0x00003C44 File Offset: 0x00001E44
		public CtrlRadioGroup(DynamicFieldDTO Field)
		{
			this.DynamicField = Field;
			this.EnableViewState = false;
		}

		// Token: 0x0600008D RID: 141 RVA: 0x00003C91 File Offset: 0x00001E91
		public override void Dispose()
		{
			if (this.rbGroup != null)
			{
				this.rbGroup.Dispose();
			}
			if (this.lbl != null)
			{
				this.lbl.Dispose();
			}
			base.Dispose();
		}

		// Token: 0x0600008E RID: 142 RVA: 0x00003CBF File Offset: 0x00001EBF
		protected override void CreateChildControls()
		{
			this.BuildControlHeiarchy();
			base.CreateChildControls();
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00003CCD File Offset: 0x00001ECD
		protected override void OnInit(EventArgs e)
		{
			this.InitializeControls();
			base.OnInit(e);
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00003CDC File Offset: 0x00001EDC
		private void InitializeControls()
		{
			int num;
			bool flag;
			string text;
			bool flag2;
			eEnforceTypeDTO eEnforceTypeDTO;
			int num2;
			int num3;
			if (this.DynamicField != null)
			{
				num = this.DynamicField.ControlId;
				flag = this.DynamicField.HideCaption;
				text = this.DynamicField.ControlCaption;
				flag2 = this.DynamicField.IsReadOnly;
				eEnforceTypeDTO = this.DynamicField.EnforceMethod;
				if (this.DynamicField.Setting4 == 0)
				{
					num2 = this.DynamicField.Setting1;
				}
				else
				{
					num2 = 0;
				}
				num3 = this.DynamicField.Setting2;
				if (num3 < 1)
				{
					num3 = 1;
				}
			}
			else
			{
				num = 0;
				flag = false;
				text = "Radio group";
				flag2 = false;
				eEnforceTypeDTO = eEnforceTypeDTO.Optional;
				num2 = 0;
				num3 = 1;
			}
			string str = num.ToString();
			this.rbGroup.ID = "rbg_" + str;
			this.validator.ID = "val_rbg_" + str;
			this.lbl.CssClass = "cxformtitle";
			this.rbGroup.CssClass = "cxformctrl";
			this.validator.CssClass = "cxformval";
			this.lbl.AssociatedControlID = this.rbGroup.ID;
			this.rbGroup.CellSpacing = 8;
			if (num2 > 0 && this.rbGroup.Items.Count < 1)
			{
				List<DynamicListItemDTO> list = this.LoadLookupList(num2);
				if (list != null)
				{
					foreach (DynamicListItemDTO dynamicListItemDTO in list)
					{
						ListItem item = new ListItem(dynamicListItemDTO.LookupText, dynamicListItemDTO.LookupListId.ToString());
						this.rbGroup.Items.Add(item);
					}
				}
			}
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
				this.rbGroup.Enabled = false;
			}
			if (num3 > 1)
			{
				this.rbGroup.RepeatColumns = num3;
				this.rbGroup.RepeatLayout = RepeatLayout.Flow;
			}
			this.validator.ControlToValidate = this.rbGroup.ID;
			if (eEnforceTypeDTO == eEnforceTypeDTO.Error)
			{
				this.validator.ErrorMessage = "Please select an item in order to continue.";
				this.validator.Display = ValidatorDisplay.Dynamic;
				return;
			}
			this.validator.Enabled = false;
			this.validator.Visible = false;
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00003F30 File Offset: 0x00002130
		protected override void Render(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "cxform");
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			this.lbl.RenderControl(writer);
			this.rbGroup.RenderControl(writer);
			if (this.validator != null)
			{
				this.validator.RenderControl(writer);
			}
			writer.RenderEndTag();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00003F84 File Offset: 0x00002184
		private List<DynamicListItemDTO> LoadLookupList(int lookupGroupId)
		{
			string key = "formlookuplist_" + lookupGroupId.ToString();
			Cache cache = HttpContext.Current.Cache;
			List<DynamicListItemDTO> list = (List<DynamicListItemDTO>)cache[key];
			if (list == null)
			{
				IList<DynamicListItemDTO> list2 = ((IDynamicFieldClientManager)new DynamicFieldClientManager()).LoadListItems(lookupGroupId);
				list = ((list2 != null) ? list2.ToList<DynamicListItemDTO>() : null);
				cache.Insert(key, list, null, DateTime.Now.AddMinutes(5.0), TimeSpan.Zero);
			}
			return list;
		}

		// Token: 0x06000093 RID: 147 RVA: 0x00003FFB File Offset: 0x000021FB
		private void BuildControlHeiarchy()
		{
			this.Controls.Add(this.lbl);
			this.Controls.Add(this.rbGroup);
			this.Controls.Add(this.validator);
		}

		// Token: 0x1700001A RID: 26
		// (get) Token: 0x06000094 RID: 148 RVA: 0x00004030 File Offset: 0x00002230
		// (set) Token: 0x06000095 RID: 149 RVA: 0x00004038 File Offset: 0x00002238
		public DynamicFieldDTO DynamicField { get; set; }

		// Token: 0x1700001B RID: 27
		// (get) Token: 0x06000096 RID: 150 RVA: 0x00004041 File Offset: 0x00002241
		// (set) Token: 0x06000097 RID: 151 RVA: 0x00004049 File Offset: 0x00002249
		public DynamicDataDTO DynamicData { get; set; }

		// Token: 0x06000098 RID: 152 RVA: 0x00004054 File Offset: 0x00002254
		public void ChildLoadViewState(object dataFromViewState)
		{
			if (dataFromViewState == null)
			{
				this.rbGroup.SelectedIndex = -1;
				return;
			}
			int selectedIndex = (int)dataFromViewState;
			this.EnsureRbGroupListItemsLoaded();
			this.rbGroup.SelectedIndex = selectedIndex;
		}

		// Token: 0x06000099 RID: 153 RVA: 0x0000408C File Offset: 0x0000228C
		private void EnsureRbGroupListItemsLoaded()
		{
			if (this.rbGroup.Items.Count < 1 && this.DynamicField != null)
			{
				int num;
				if (this.DynamicField.Setting4 == 0)
				{
					num = this.DynamicField.Setting1;
				}
				else
				{
					num = 0;
				}
				if (num > 0)
				{
					List<DynamicListItemDTO> list = this.LoadLookupList(num);
					if (list != null)
					{
						foreach (DynamicListItemDTO dynamicListItemDTO in list)
						{
							ListItem item = new ListItem(dynamicListItemDTO.LookupText, dynamicListItemDTO.LookupListId.ToString());
							this.rbGroup.Items.Add(item);
						}
					}
				}
			}
		}

		// Token: 0x0600009A RID: 154 RVA: 0x00004150 File Offset: 0x00002350
		public object ChildSaveViewState()
		{
			return this.rbGroup.SelectedIndex;
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600009B RID: 155 RVA: 0x00004164 File Offset: 0x00002364
		public string ViewStateKey
		{
			get
			{
				if (!string.IsNullOrEmpty(this.rbGroup.ID))
				{
					return "v" + this.rbGroup.ID;
				}
				if (this.DynamicField != null)
				{
					this.rbGroup.ID = "rbg_" + this.DynamicField.ControlId.ToString();
					return "v" + this.rbGroup.ID;
				}
				return "rblist_nocid";
			}
		}

		// Token: 0x0600009C RID: 156 RVA: 0x000041E4 File Offset: 0x000023E4
		public void ShowData(DynamicDataDTO data)
		{
			this.DynamicData = data;
			int num;
			if (data == null || data.Value == null)
			{
				num = -1;
			}
			else if (data.Value is int)
			{
				num = (int)data.Value;
			}
			else
			{
				num = -1;
			}
			if (num < 0)
			{
				this.rbGroup.SelectedIndex = -1;
				return;
			}
			this.EnsureRbGroupListItemsLoaded();
			int selectedIndex = -1;
			for (int i = 0; i < this.rbGroup.Items.Count; i++)
			{
				string text = this.rbGroup.Items[i].Value ?? "";
				int num2;
				if (!string.IsNullOrEmpty(text) && int.TryParse(text, out num2) && num2 == num)
				{
					selectedIndex = i;
					break;
				}
			}
			this.rbGroup.SelectedIndex = selectedIndex;
		}

		// Token: 0x0600009D RID: 157 RVA: 0x000042A0 File Offset: 0x000024A0
		public DynamicDataDTO GetCurrentData(out bool isEmpty)
		{
			int selectedIndex = this.rbGroup.SelectedIndex;
			int num;
			if (selectedIndex <= 0)
			{
				num = -1;
			}
			else
			{
				int.TryParse(this.rbGroup.Items[selectedIndex].Value ?? "", out num);
			}
			isEmpty = (num <= 0);
			if (this.DynamicData == null)
			{
				this.DynamicData = new DynamicDataDTO
				{
					Field = this.DynamicField,
					DataId = 0,
					Value = num
				};
			}
			else
			{
				this.DynamicData.Value = num;
			}
			return this.DynamicData;
		}

		// Token: 0x0600009E RID: 158 RVA: 0x0000433C File Offset: 0x0000253C
		public void ClearData()
		{
			this.rbGroup.SelectedIndex = -1;
		}

		// Token: 0x04000024 RID: 36
		private RequiredFieldValidator validator = new RequiredFieldValidator();

		// Token: 0x04000025 RID: 37
		private const string ID_PREFIX = "rbg_";

		// Token: 0x04000026 RID: 38
		private Label lbl = new Label();

		// Token: 0x04000027 RID: 39
		private RadioButtonList rbGroup = new RadioButtonList();

		// Token: 0x04000028 RID: 40
		private Wizard wizard = new Wizard();
	}
}

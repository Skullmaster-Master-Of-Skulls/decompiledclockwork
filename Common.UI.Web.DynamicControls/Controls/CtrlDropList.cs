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
	// Token: 0x02000005 RID: 5
	[ToolboxData("<{0}:CtrlDropList runat=server></{0}:CtrlDropList>")]
	public class CtrlDropList : WebControl, IDynamicWebControl, INamingContainer
	{
		// Token: 0x06000033 RID: 51 RVA: 0x00002AF1 File Offset: 0x00000CF1
		public CtrlDropList()
		{
			this.EnableViewState = false;
		}

		// Token: 0x06000034 RID: 52 RVA: 0x00002B21 File Offset: 0x00000D21
		public CtrlDropList(DynamicFieldDTO Field)
		{
			this.DynamicField = Field;
			this.EnableViewState = false;
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00002B58 File Offset: 0x00000D58
		public override void Dispose()
		{
			if (this.dropList != null)
			{
				this.dropList.Dispose();
			}
			if (this.lbl != null)
			{
				this.lbl.Dispose();
			}
			base.Dispose();
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00002B86 File Offset: 0x00000D86
		protected override void CreateChildControls()
		{
			this.BuildControlHeiarchy();
			base.CreateChildControls();
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00002B94 File Offset: 0x00000D94
		protected override void OnInit(EventArgs e)
		{
			this.InitializeControls();
			base.OnInit(e);
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00002BA4 File Offset: 0x00000DA4
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
				num2 = this.DynamicField.Setting4;
				if (this.DynamicField.Setting3 == 0)
				{
					num3 = this.DynamicField.Setting1;
				}
				else
				{
					num3 = 0;
				}
			}
			else
			{
				num = 0;
				flag = false;
				text = "Drop list";
				flag2 = false;
				eEnforceTypeDTO = eEnforceTypeDTO.Optional;
				num3 = 0;
				num2 = 0;
			}
			string str = num.ToString();
			this.dropList.ID = "cmb_" + str;
			this.lbl.ID = "hlbl_cmb_" + str;
			this.validator.ID = "val_cmb_" + str;
			this.validator.CssClass = "cxformval";
			if (num2 > 0)
			{
				this.dropList.Width = num2 * 8;
				this.dropList.CssClass = "cxformcmbsmall";
			}
			else
			{
				this.dropList.CssClass = "cxformctrl";
			}
			this.lbl.AssociatedControlID = this.dropList.ID;
			if (num3 > 0 && this.dropList.Items.Count < 1)
			{
				List<DynamicListItemDTO> list = this.LoadLookupList(num3);
				if (list != null)
				{
					ListItem item = new ListItem("", "0");
					this.dropList.Items.Add(item);
					foreach (DynamicListItemDTO dynamicListItemDTO in list)
					{
						ListItem item2 = new ListItem(dynamicListItemDTO.LookupText, dynamicListItemDTO.LookupListId.ToString());
						this.dropList.Items.Add(item2);
					}
				}
			}
			if (flag)
			{
				this.lbl.Text = "";
			}
			else if (text.Length > 25)
			{
				this.lbl.CssClass = "cxformtitlewide";
				this.lbl.Text = text + "<br />";
			}
			else
			{
				this.lbl.CssClass = "cxformtitle";
				this.lbl.Text = text;
			}
			if (flag2)
			{
				this.dropList.Enabled = false;
			}
			this.validator.ControlToValidate = this.dropList.ID;
			if (eEnforceTypeDTO == eEnforceTypeDTO.Error)
			{
				this.validator.ErrorMessage = "Please select an item in order to continue.";
				this.validator.Display = ValidatorDisplay.Dynamic;
				return;
			}
			this.validator.Enabled = false;
			this.validator.Visible = false;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002E68 File Offset: 0x00001068
		protected override void Render(HtmlTextWriter writer)
		{
			writer.AddAttribute(HtmlTextWriterAttribute.Class, "cxform");
			writer.RenderBeginTag(HtmlTextWriterTag.Li);
			this.lbl.RenderControl(writer);
			if (this.lbl.CssClass.Equals("cxformtitlewide"))
			{
				writer.Write("<span class='cxformtitle'> </span>");
			}
			this.dropList.RenderControl(writer);
			if (this.validator != null)
			{
				this.validator.RenderControl(writer);
			}
			writer.RenderEndTag();
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002EE0 File Offset: 0x000010E0
		private List<DynamicListItemDTO> LoadLookupList(int lookupGroupId)
		{
			string key = "formlookuplist_" + lookupGroupId.ToString();
			Cache cache = HttpContext.Current.Cache;
			List<DynamicListItemDTO> list = (List<DynamicListItemDTO>)cache[key];
			if (list == null)
			{
				IList<DynamicListItemDTO> list2 = ((IDynamicFieldClientManager)new DynamicFieldClientManager()).LoadListItems(lookupGroupId);
				list = ((list2 != null) ? list2.ToList<DynamicListItemDTO>() : null);
				cache.Insert(key, list, null, DateTime.Now.AddMinutes(480.0), TimeSpan.Zero);
			}
			return list;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002F57 File Offset: 0x00001157
		private void BuildControlHeiarchy()
		{
			this.Controls.Add(this.lbl);
			this.Controls.Add(this.dropList);
			this.Controls.Add(this.validator);
		}

		// Token: 0x1700000A RID: 10
		// (get) Token: 0x0600003C RID: 60 RVA: 0x00002F8C File Offset: 0x0000118C
		// (set) Token: 0x0600003D RID: 61 RVA: 0x00002F94 File Offset: 0x00001194
		public DynamicFieldDTO DynamicField { get; set; }

		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600003E RID: 62 RVA: 0x00002F9D File Offset: 0x0000119D
		// (set) Token: 0x0600003F RID: 63 RVA: 0x00002FA5 File Offset: 0x000011A5
		public DynamicDataDTO DynamicData { get; set; }

		// Token: 0x06000040 RID: 64 RVA: 0x00002FB0 File Offset: 0x000011B0
		public void ChildLoadViewState(object dataFromViewState)
		{
			if (dataFromViewState == null)
			{
				this.dropList.SelectedIndex = -1;
				return;
			}
			int selectedIndex = (int)dataFromViewState;
			this.EnsureDropListItemsLoaded();
			this.dropList.SelectedIndex = selectedIndex;
		}

		// Token: 0x06000041 RID: 65 RVA: 0x00002FE8 File Offset: 0x000011E8
		private void EnsureDropListItemsLoaded()
		{
			if (this.dropList.Items.Count < 1 && this.DynamicField != null)
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
						ListItem item = new ListItem("", "0");
						this.dropList.Items.Add(item);
						foreach (DynamicListItemDTO dynamicListItemDTO in list)
						{
							ListItem item2 = new ListItem(dynamicListItemDTO.LookupText, dynamicListItemDTO.LookupListId.ToString());
							this.dropList.Items.Add(item2);
						}
					}
				}
			}
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000030D4 File Offset: 0x000012D4
		public object ChildSaveViewState()
		{
			return this.dropList.SelectedIndex;
		}

		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000043 RID: 67 RVA: 0x000030E8 File Offset: 0x000012E8
		public string ViewStateKey
		{
			get
			{
				if (!string.IsNullOrEmpty(this.dropList.ID))
				{
					return "v" + this.dropList.ID;
				}
				if (this.DynamicField != null)
				{
					this.dropList.ID = "cmb_" + this.DynamicField.ControlId.ToString();
					return "v" + this.dropList.ID;
				}
				return "cmb_nocid";
			}
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00003168 File Offset: 0x00001368
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
				this.dropList.SelectedIndex = -1;
				return;
			}
			this.EnsureDropListItemsLoaded();
			int selectedIndex = -1;
			for (int i = 0; i < this.dropList.Items.Count; i++)
			{
				string text = this.dropList.Items[i].Value ?? "";
				int num2;
				if (!string.IsNullOrEmpty(text) && int.TryParse(text, out num2) && num2 == num)
				{
					selectedIndex = i;
					break;
				}
			}
			this.dropList.SelectedIndex = selectedIndex;
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00003224 File Offset: 0x00001424
		public DynamicDataDTO GetCurrentData(out bool isEmpty)
		{
			int selectedIndex = this.dropList.SelectedIndex;
			int num;
			if (selectedIndex <= 0)
			{
				num = -1;
			}
			else
			{
				int.TryParse(this.dropList.Items[selectedIndex].Value ?? "", out num);
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

		// Token: 0x06000046 RID: 70 RVA: 0x000032C0 File Offset: 0x000014C0
		public void ClearData()
		{
			this.dropList.SelectedIndex = -1;
		}

		// Token: 0x0400000E RID: 14
		private RequiredFieldValidator validator = new RequiredFieldValidator();

		// Token: 0x0400000F RID: 15
		private const string ID_PREFIX = "cmb_";

		// Token: 0x04000010 RID: 16
		private Label lbl = new Label();

		// Token: 0x04000011 RID: 17
		private DropDownList dropList = new DropDownList();
	}
}

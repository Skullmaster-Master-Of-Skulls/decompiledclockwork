using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200046E RID: 1134
	public class RadFilterDropDownEditor : RadFilterDataFieldEditor
	{
		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x060028B8 RID: 10424 RVA: 0x00083B08 File Offset: 0x00081D08
		// (set) Token: 0x060028B9 RID: 10425 RVA: 0x00083B31 File Offset: 0x00081D31
		[NotifyParentProperty(true)]
		[Description("Gets or sets the type of drop down control which will be created. The default value is RadDropDownList.")]
		[DefaultValue(RadFilterDropDownType.RadDropDownList)]
		public RadFilterDropDownType DropDownType
		{
			get
			{
				object obj = base.ViewState["DropDownType"];
				if (obj != null)
				{
					return (RadFilterDropDownType)obj;
				}
				return RadFilterDropDownType.RadDropDownList;
			}
			set
			{
				base.ViewState["DropDownType"] = value;
			}
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x060028BA RID: 10426 RVA: 0x00083B49 File Offset: 0x00081D49
		// (set) Token: 0x060028BB RID: 10427 RVA: 0x00083B69 File Offset: 0x00081D69
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("Gets or sets the DataTextField property value of the drop down control created by the editor.")]
		public string DataTextField
		{
			get
			{
				return ((string)base.ViewState["DataTextField"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["DataTextField"] = value;
			}
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x060028BC RID: 10428 RVA: 0x00083B7C File Offset: 0x00081D7C
		// (set) Token: 0x060028BD RID: 10429 RVA: 0x00083B9C File Offset: 0x00081D9C
		[Description("Gets or sets the DataValueField property value of the drop down control created by the editor.")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public string DataValueField
		{
			get
			{
				return ((string)base.ViewState["DataValueField"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["DataValueField"] = value;
			}
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x060028BE RID: 10430 RVA: 0x00083BAF File Offset: 0x00081DAF
		// (set) Token: 0x060028BF RID: 10431 RVA: 0x00083BCF File Offset: 0x00081DCF
		[DefaultValue("")]
		[Description("Gets or sets the DataSourceID property value of the drop down control created by the editor.")]
		[NotifyParentProperty(true)]
		public string DataSourceID
		{
			get
			{
				return ((string)base.ViewState["DataSourceID"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["DataSourceID"] = value;
			}
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x060028C0 RID: 10432 RVA: 0x00083BE2 File Offset: 0x00081DE2
		// (set) Token: 0x060028C1 RID: 10433 RVA: 0x00083C02 File Offset: 0x00081E02
		private string Value
		{
			get
			{
				return ((string)base.ViewState["RadFilterDropDownControlValue"]) ?? string.Empty;
			}
			set
			{
				base.ViewState["RadFilterDropDownControlValue"] = value;
			}
		}

		// Token: 0x060028C2 RID: 10434 RVA: 0x00083C3C File Offset: 0x00081E3C
		public override void InitializeEditor(Control container)
		{
			switch (this.DropDownType)
			{
			case RadFilterDropDownType.RadComboBox:
				this.dropDownControl = this.CreateRadComboBox();
				break;
			case RadFilterDropDownType.RadDropDownList:
				this.dropDownControl = this.CreateRadDropDownList();
				break;
			}
			this.dropDownControl.PreRender += delegate(object sender, EventArgs args)
			{
				this.dropDownControl.Skin = ((RadFilterExpressionItem)this.dropDownControl.NamingContainer).OwnerFilter.RuntimeSkin;
			};
			this.dropDownControl.EnableEmbeddedSkins = base.Owner.EnableEmbeddedSkins;
			this.dropDownControl.EnableEmbeddedScripts = base.Owner.EnableEmbeddedScripts;
			this.dropDownControl.EnableEmbeddedBaseStylesheet = base.Owner.EnableEmbeddedBaseStylesheet;
			this.dropDownControl.RegisterWithScriptManager = base.Owner.RegisterWithScriptManager;
			this.dropDownControl.RenderMode = base.Owner.ResolvedRenderMode;
			this.dropDownControl.AppendDataBoundItems = true;
			this.dropDownControl.DataTextField = this.DataTextField;
			this.dropDownControl.DataValueField = this.DataValueField;
			this.dropDownControl.DataSourceID = this.DataSourceID;
			container.Controls.Add(this.dropDownControl);
		}

		// Token: 0x060028C3 RID: 10435 RVA: 0x00083D50 File Offset: 0x00081F50
		protected virtual RadDropDownList CreateRadDropDownList()
		{
			return new RadDropDownList
			{
				ToolTip = this.ToolTip
			};
		}

		// Token: 0x060028C4 RID: 10436 RVA: 0x00083D70 File Offset: 0x00081F70
		protected virtual RadComboBox CreateRadComboBox()
		{
			return new RadComboBox
			{
				InputTitle = this.ToolTip
			};
		}

		// Token: 0x060028C5 RID: 10437 RVA: 0x00083D90 File Offset: 0x00081F90
		public override ArrayList ExtractValues()
		{
			ArrayList arrayList = new ArrayList();
			RadDropDownList radDropDownList = this.dropDownControl as RadDropDownList;
			RadComboBox radComboBox = this.dropDownControl as RadComboBox;
			if (radDropDownList != null)
			{
				if (radDropDownList.SelectedItem != null)
				{
					arrayList.Add(radDropDownList.SelectedItem.Value);
				}
				else
				{
					arrayList.Add(this.Value);
				}
			}
			else if (radComboBox != null)
			{
				if (radComboBox.SelectedItem != null)
				{
					arrayList.Add(radComboBox.SelectedItem.Value);
				}
				else
				{
					arrayList.Add(this.Value);
				}
			}
			return arrayList;
		}

		// Token: 0x060028C6 RID: 10438 RVA: 0x00083E80 File Offset: 0x00082080
		public override void SetEditorValues(ArrayList values)
		{
			if (values != null && values.Count > 0)
			{
				if (values[0] == null)
				{
					return;
				}
				this.Value = values[0].ToString();
				RadDropDownList dropDownList = this.dropDownControl as RadDropDownList;
				RadComboBox comboBox = this.dropDownControl as RadComboBox;
				if (dropDownList != null)
				{
					dropDownList.DataBound += delegate(object sender, EventArgs args)
					{
						DropDownListItem dropDownListItem = dropDownList.FindItemByValue(this.Value);
						if (dropDownListItem != null)
						{
							dropDownListItem.Selected = true;
						}
					};
					return;
				}
				if (comboBox != null)
				{
					comboBox.DataBound += delegate(object sender, EventArgs args)
					{
						RadComboBoxItem radComboBoxItem = comboBox.FindItemByValue(this.Value);
						if (radComboBoxItem != null)
						{
							radComboBoxItem.Selected = true;
						}
					};
				}
			}
		}

		// Token: 0x060028C7 RID: 10439 RVA: 0x00083F37 File Offset: 0x00082137
		internal override WebControl GetFirstInputControl(Control container)
		{
			return this.dropDownControl;
		}

		// Token: 0x060028C8 RID: 10440 RVA: 0x00083F40 File Offset: 0x00082140
		protected override void CopySettings(RadFilterDataFieldEditor baseEditor)
		{
			base.CopySettings(baseEditor);
			RadFilterDropDownEditor radFilterDropDownEditor = baseEditor as RadFilterDropDownEditor;
			if (radFilterDropDownEditor != null)
			{
				this.DropDownType = radFilterDropDownEditor.DropDownType;
				this.DataTextField = radFilterDropDownEditor.DataTextField;
				this.DataValueField = radFilterDropDownEditor.DataValueField;
				this.DataSourceID = radFilterDropDownEditor.DataSourceID;
			}
		}

		// Token: 0x04000A53 RID: 2643
		private ControlItemContainer dropDownControl;
	}
}

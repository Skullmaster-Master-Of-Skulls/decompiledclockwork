using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200038A RID: 906
	public class ButtonField : ButtonFieldBase
	{
		// Token: 0x17000BBB RID: 3003
		// (get) Token: 0x06002A29 RID: 10793 RVA: 0x00088668 File Offset: 0x00086868
		// (set) Token: 0x06002A2A RID: 10794 RVA: 0x00088695 File Offset: 0x00086895
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("WebControl_CommandName")]
		public virtual string CommandName
		{
			get
			{
				object obj = base.ViewState["CommandName"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (!object.Equals(value, base.ViewState["CommandName"]))
				{
					base.ViewState["CommandName"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000BBC RID: 3004
		// (get) Token: 0x06002A2B RID: 10795 RVA: 0x000886C8 File Offset: 0x000868C8
		// (set) Token: 0x06002A2C RID: 10796 RVA: 0x000886F5 File Offset: 0x000868F5
		[WebCategory("Data")]
		[DefaultValue("")]
		[WebSysDescription("ButtonField_DataTextField")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		public virtual string DataTextField
		{
			get
			{
				object obj = base.ViewState["DataTextField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (!object.Equals(value, base.ViewState["DataTextField"]))
				{
					base.ViewState["DataTextField"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000BBD RID: 3005
		// (get) Token: 0x06002A2D RID: 10797 RVA: 0x00088728 File Offset: 0x00086928
		// (set) Token: 0x06002A2E RID: 10798 RVA: 0x00088755 File Offset: 0x00086955
		[WebCategory("Data")]
		[DefaultValue("")]
		[WebSysDescription("ButtonField_DataTextFormatString")]
		public virtual string DataTextFormatString
		{
			get
			{
				object obj = base.ViewState["DataTextFormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (!object.Equals(value, base.ViewState["DataTextFormatString"]))
				{
					base.ViewState["DataTextFormatString"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000BBE RID: 3006
		// (get) Token: 0x06002A2F RID: 10799 RVA: 0x00088788 File Offset: 0x00086988
		// (set) Token: 0x06002A30 RID: 10800 RVA: 0x000887B5 File Offset: 0x000869B5
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebSysDescription("ButtonField_ImageUrl")]
		[UrlProperty]
		public virtual string ImageUrl
		{
			get
			{
				object obj = base.ViewState["ImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (!object.Equals(value, base.ViewState["ImageUrl"]))
				{
					base.ViewState["ImageUrl"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000BBF RID: 3007
		// (get) Token: 0x06002A31 RID: 10801 RVA: 0x000887E8 File Offset: 0x000869E8
		// (set) Token: 0x06002A32 RID: 10802 RVA: 0x00088815 File Offset: 0x00086A15
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("ButtonField_Text")]
		public virtual string Text
		{
			get
			{
				object obj = base.ViewState["Text"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (!object.Equals(value, base.ViewState["Text"]))
				{
					base.ViewState["Text"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x06002A33 RID: 10803 RVA: 0x00088848 File Offset: 0x00086A48
		protected override void CopyProperties(DataControlField newField)
		{
			((ButtonField)newField).CommandName = this.CommandName;
			((ButtonField)newField).DataTextField = this.DataTextField;
			((ButtonField)newField).DataTextFormatString = this.DataTextFormatString;
			((ButtonField)newField).ImageUrl = this.ImageUrl;
			((ButtonField)newField).Text = this.Text;
			base.CopyProperties(newField);
		}

		// Token: 0x06002A34 RID: 10804 RVA: 0x000888B1 File Offset: 0x00086AB1
		protected override DataControlField CreateField()
		{
			return new ButtonField();
		}

		// Token: 0x06002A35 RID: 10805 RVA: 0x000888B8 File Offset: 0x00086AB8
		protected virtual string FormatDataTextValue(object dataTextValue)
		{
			string result = string.Empty;
			if (!DataBinder.IsNull(dataTextValue))
			{
				string dataTextFormatString = this.DataTextFormatString;
				if (dataTextFormatString.Length == 0)
				{
					result = dataTextValue.ToString();
				}
				else
				{
					result = string.Format(CultureInfo.CurrentCulture, dataTextFormatString, new object[]
					{
						dataTextValue
					});
				}
			}
			return result;
		}

		// Token: 0x06002A36 RID: 10806 RVA: 0x00088902 File Offset: 0x00086B02
		public override bool Initialize(bool sortingEnabled, Control control)
		{
			base.Initialize(sortingEnabled, control);
			this.textFieldDesc = null;
			return false;
		}

		// Token: 0x06002A37 RID: 10807 RVA: 0x00088918 File Offset: 0x00086B18
		public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
		{
			base.InitializeCell(cell, cellType, rowState, rowIndex);
			if (cellType != DataControlCellType.Header && cellType != DataControlCellType.Footer)
			{
				IPostBackContainer postBackContainer = base.Control as IPostBackContainer;
				bool causesValidation = this.CausesValidation;
				bool flag = true;
				IButtonControl buttonControl;
				switch (this.ButtonType)
				{
				case ButtonType.Button:
					if (postBackContainer != null && !causesValidation)
					{
						buttonControl = new DataControlButton(postBackContainer);
						flag = false;
						goto IL_A5;
					}
					buttonControl = new Button();
					goto IL_A5;
				case ButtonType.Link:
					if (postBackContainer != null && !causesValidation)
					{
						buttonControl = new DataControlLinkButton(postBackContainer);
						flag = false;
						goto IL_A5;
					}
					buttonControl = new DataControlLinkButton(null);
					goto IL_A5;
				}
				if (postBackContainer != null && !causesValidation)
				{
					buttonControl = new DataControlImageButton(postBackContainer);
					flag = false;
				}
				else
				{
					buttonControl = new ImageButton();
				}
				((ImageButton)buttonControl).ImageUrl = this.ImageUrl;
				IL_A5:
				buttonControl.Text = this.Text;
				buttonControl.CommandName = this.CommandName;
				buttonControl.CommandArgument = rowIndex.ToString(CultureInfo.InvariantCulture);
				if (flag)
				{
					buttonControl.CausesValidation = causesValidation;
				}
				buttonControl.ValidationGroup = this.ValidationGroup;
				if (this.DataTextField.Length != 0 && base.Visible)
				{
					((WebControl)buttonControl).DataBinding += this.OnDataBindField;
				}
				cell.Controls.Add((WebControl)buttonControl);
			}
		}

		// Token: 0x06002A38 RID: 10808 RVA: 0x00088A48 File Offset: 0x00086C48
		private void OnDataBindField(object sender, EventArgs e)
		{
			Control control = (Control)sender;
			Control namingContainer = control.NamingContainer;
			if (namingContainer == null)
			{
				throw new HttpException(SR.GetString("DataControlField_NoContainer"));
			}
			object dataItem = DataBinder.GetDataItem(namingContainer);
			if (dataItem == null && !base.DesignMode)
			{
				throw new HttpException(SR.GetString("DataItem_Not_Found"));
			}
			if (this.textFieldDesc == null && dataItem != null)
			{
				string dataTextField = this.DataTextField;
				this.textFieldDesc = TypeDescriptor.GetProperties(dataItem).Find(dataTextField, true);
				if (this.textFieldDesc == null && !base.DesignMode)
				{
					throw new HttpException(SR.GetString("Field_Not_Found", new object[]
					{
						dataTextField
					}));
				}
			}
			string text;
			if (this.textFieldDesc != null && dataItem != null)
			{
				object value = this.textFieldDesc.GetValue(dataItem);
				text = this.FormatDataTextValue(value);
			}
			else
			{
				text = SR.GetString("Sample_Databound_Text");
			}
			((IButtonControl)control).Text = text;
		}

		// Token: 0x06002A39 RID: 10809 RVA: 0x00006164 File Offset: 0x00004364
		public override void ValidateSupportsCallback()
		{
		}

		// Token: 0x04001E9A RID: 7834
		private PropertyDescriptor textFieldDesc;
	}
}

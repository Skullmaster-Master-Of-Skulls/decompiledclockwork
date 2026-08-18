using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000388 RID: 904
	public class ButtonColumn : DataGridColumn
	{
		// Token: 0x17000BB4 RID: 2996
		// (get) Token: 0x06002A16 RID: 10774 RVA: 0x00088268 File Offset: 0x00086468
		// (set) Token: 0x06002A17 RID: 10775 RVA: 0x00088291 File Offset: 0x00086491
		[WebCategory("Appearance")]
		[DefaultValue(ButtonColumnType.LinkButton)]
		[WebSysDescription("ButtonColumn_ButtonType")]
		public virtual ButtonColumnType ButtonType
		{
			get
			{
				object obj = base.ViewState["ButtonType"];
				if (obj != null)
				{
					return (ButtonColumnType)obj;
				}
				return ButtonColumnType.LinkButton;
			}
			set
			{
				if (value < ButtonColumnType.LinkButton || value > ButtonColumnType.PushButton)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["ButtonType"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000BB5 RID: 2997
		// (get) Token: 0x06002A18 RID: 10776 RVA: 0x000882C4 File Offset: 0x000864C4
		// (set) Token: 0x06002A19 RID: 10777 RVA: 0x000882ED File Offset: 0x000864ED
		[DefaultValue(false)]
		[WebSysDescription("ButtonColumn_CausesValidation")]
		public virtual bool CausesValidation
		{
			get
			{
				object obj = base.ViewState["CausesValidation"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["CausesValidation"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000BB6 RID: 2998
		// (get) Token: 0x06002A1A RID: 10778 RVA: 0x0008830C File Offset: 0x0008650C
		// (set) Token: 0x06002A1B RID: 10779 RVA: 0x00088339 File Offset: 0x00086539
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
				base.ViewState["CommandName"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x06002A1C RID: 10780 RVA: 0x00088354 File Offset: 0x00086554
		// (set) Token: 0x06002A1D RID: 10781 RVA: 0x00088381 File Offset: 0x00086581
		[WebCategory("Data")]
		[DefaultValue("")]
		[WebSysDescription("ButtonColumn_DataTextField")]
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
				base.ViewState["DataTextField"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x06002A1E RID: 10782 RVA: 0x0008839C File Offset: 0x0008659C
		// (set) Token: 0x06002A1F RID: 10783 RVA: 0x000883C9 File Offset: 0x000865C9
		[WebCategory("Data")]
		[DefaultValue("")]
		[WebSysDescription("ButtonColumn_DataTextFormatString")]
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
				base.ViewState["DataTextFormatString"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x06002A20 RID: 10784 RVA: 0x000883E4 File Offset: 0x000865E4
		// (set) Token: 0x06002A21 RID: 10785 RVA: 0x00088411 File Offset: 0x00086611
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("ButtonColumn_Text")]
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
				base.ViewState["Text"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000BBA RID: 3002
		// (get) Token: 0x06002A22 RID: 10786 RVA: 0x0008842C File Offset: 0x0008662C
		// (set) Token: 0x06002A23 RID: 10787 RVA: 0x00088459 File Offset: 0x00086659
		[DefaultValue("")]
		[WebSysDescription("ButtonColumn_ValidationGroup")]
		public virtual string ValidationGroup
		{
			get
			{
				object obj = base.ViewState["ValidationGroup"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["ValidationGroup"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x06002A24 RID: 10788 RVA: 0x00088474 File Offset: 0x00086674
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

		// Token: 0x06002A25 RID: 10789 RVA: 0x000884BE File Offset: 0x000866BE
		public override void Initialize()
		{
			base.Initialize();
			this.textFieldDesc = null;
		}

		// Token: 0x06002A26 RID: 10790 RVA: 0x000884D0 File Offset: 0x000866D0
		public override void InitializeCell(TableCell cell, int columnIndex, ListItemType itemType)
		{
			base.InitializeCell(cell, columnIndex, itemType);
			if (itemType != ListItemType.Header && itemType != ListItemType.Footer)
			{
				WebControl webControl;
				if (this.ButtonType == ButtonColumnType.LinkButton)
				{
					webControl = new DataGridLinkButton
					{
						Text = this.Text,
						CommandName = this.CommandName,
						CausesValidation = this.CausesValidation,
						ValidationGroup = this.ValidationGroup
					};
				}
				else
				{
					webControl = new Button
					{
						Text = this.Text,
						CommandName = this.CommandName,
						CausesValidation = this.CausesValidation,
						ValidationGroup = this.ValidationGroup
					};
				}
				if (this.DataTextField.Length != 0)
				{
					webControl.DataBinding += this.OnDataBindColumn;
				}
				cell.Controls.Add(webControl);
			}
		}

		// Token: 0x06002A27 RID: 10791 RVA: 0x0008859C File Offset: 0x0008679C
		private void OnDataBindColumn(object sender, EventArgs e)
		{
			Control control = (Control)sender;
			DataGridItem dataGridItem = (DataGridItem)control.NamingContainer;
			object dataItem = dataGridItem.DataItem;
			if (this.textFieldDesc == null)
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
			if (this.textFieldDesc != null)
			{
				object value = this.textFieldDesc.GetValue(dataItem);
				text = this.FormatDataTextValue(value);
			}
			else
			{
				text = SR.GetString("Sample_Databound_Text");
			}
			if (control is LinkButton)
			{
				((LinkButton)control).Text = text;
				return;
			}
			((Button)control).Text = text;
		}

		// Token: 0x04001E96 RID: 7830
		private PropertyDescriptor textFieldDesc;
	}
}

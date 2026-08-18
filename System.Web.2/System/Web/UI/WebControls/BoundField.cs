using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000381 RID: 897
	public class BoundField : DataControlField
	{
		// Token: 0x17000B92 RID: 2962
		// (get) Token: 0x060029A8 RID: 10664 RVA: 0x00086BC8 File Offset: 0x00084DC8
		// (set) Token: 0x060029A9 RID: 10665 RVA: 0x00086BD0 File Offset: 0x00084DD0
		[WebCategory("Behavior")]
		[WebSysDescription("Control_ValidateRequestMode")]
		[DefaultValue(ValidateRequestMode.Inherit)]
		public new ValidateRequestMode ValidateRequestMode
		{
			get
			{
				return base.ValidateRequestMode;
			}
			set
			{
				base.ValidateRequestMode = value;
			}
		}

		// Token: 0x17000B93 RID: 2963
		// (get) Token: 0x060029AA RID: 10666 RVA: 0x00086BDC File Offset: 0x00084DDC
		// (set) Token: 0x060029AB RID: 10667 RVA: 0x00086C05 File Offset: 0x00084E05
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		[WebSysDescription("BoundField_ApplyFormatInEditMode")]
		public virtual bool ApplyFormatInEditMode
		{
			get
			{
				object obj = base.ViewState["ApplyFormatInEditMode"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["ApplyFormatInEditMode"] = value;
			}
		}

		// Token: 0x17000B94 RID: 2964
		// (get) Token: 0x060029AC RID: 10668 RVA: 0x00086C20 File Offset: 0x00084E20
		// (set) Token: 0x060029AD RID: 10669 RVA: 0x00086C49 File Offset: 0x00084E49
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		[WebSysDescription("BoundField_ConvertEmptyStringToNull")]
		public virtual bool ConvertEmptyStringToNull
		{
			get
			{
				object obj = base.ViewState["ConvertEmptyStringToNull"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["ConvertEmptyStringToNull"] = value;
			}
		}

		// Token: 0x17000B95 RID: 2965
		// (get) Token: 0x060029AE RID: 10670 RVA: 0x00086C64 File Offset: 0x00084E64
		// (set) Token: 0x060029AF RID: 10671 RVA: 0x00086CAC File Offset: 0x00084EAC
		[WebCategory("Data")]
		[DefaultValue("")]
		[TypeConverter("System.Web.UI.Design.DataSourceViewSchemaConverter, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
		[WebSysDescription("BoundField_DataField")]
		public virtual string DataField
		{
			get
			{
				if (this._dataField == null)
				{
					object obj = base.ViewState["DataField"];
					if (obj != null)
					{
						this._dataField = (string)obj;
					}
					else
					{
						this._dataField = string.Empty;
					}
				}
				return this._dataField;
			}
			set
			{
				if (!object.Equals(value, base.ViewState["DataField"]))
				{
					base.ViewState["DataField"] = value;
					this._dataField = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000B96 RID: 2966
		// (get) Token: 0x060029B0 RID: 10672 RVA: 0x00086CE4 File Offset: 0x00084EE4
		// (set) Token: 0x060029B1 RID: 10673 RVA: 0x00086D2C File Offset: 0x00084F2C
		[WebCategory("Data")]
		[DefaultValue("")]
		[WebSysDescription("BoundField_DataFormatString")]
		public virtual string DataFormatString
		{
			get
			{
				if (this._dataFormatString == null)
				{
					object obj = base.ViewState["DataFormatString"];
					if (obj != null)
					{
						this._dataFormatString = (string)obj;
					}
					else
					{
						this._dataFormatString = string.Empty;
					}
				}
				return this._dataFormatString;
			}
			set
			{
				if (!object.Equals(value, base.ViewState["DataFormatString"]))
				{
					base.ViewState["DataFormatString"] = value;
					this._dataFormatString = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000B97 RID: 2967
		// (get) Token: 0x060029B2 RID: 10674 RVA: 0x00086D64 File Offset: 0x00084F64
		// (set) Token: 0x060029B3 RID: 10675 RVA: 0x00086D6C File Offset: 0x00084F6C
		public override string HeaderText
		{
			get
			{
				return base.HeaderText;
			}
			set
			{
				if (!object.Equals(value, base.ViewState["HeaderText"]))
				{
					base.ViewState["HeaderText"] = value;
					if (!this._suppressHeaderTextFieldChange)
					{
						this.OnFieldChanged();
					}
				}
			}
		}

		// Token: 0x17000B98 RID: 2968
		// (get) Token: 0x060029B4 RID: 10676 RVA: 0x00086DA8 File Offset: 0x00084FA8
		// (set) Token: 0x060029B5 RID: 10677 RVA: 0x00086DF4 File Offset: 0x00084FF4
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		[WebSysDescription("BoundField_HtmlEncode")]
		public virtual bool HtmlEncode
		{
			get
			{
				if (!this._htmlEncodeSet)
				{
					object obj = base.ViewState["HtmlEncode"];
					if (obj != null)
					{
						this._htmlEncode = (bool)obj;
					}
					else
					{
						this._htmlEncode = true;
					}
					this._htmlEncodeSet = true;
				}
				return this._htmlEncode;
			}
			set
			{
				object obj = base.ViewState["HtmlEncode"];
				if (obj == null || (bool)obj != value)
				{
					base.ViewState["HtmlEncode"] = value;
					this._htmlEncode = value;
					this._htmlEncodeSet = true;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000B99 RID: 2969
		// (get) Token: 0x060029B6 RID: 10678 RVA: 0x00086E48 File Offset: 0x00085048
		// (set) Token: 0x060029B7 RID: 10679 RVA: 0x00086E94 File Offset: 0x00085094
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		public virtual bool HtmlEncodeFormatString
		{
			get
			{
				if (!this._htmlEncodeFormatStringSet)
				{
					object obj = base.ViewState["HtmlEncodeFormatString"];
					if (obj != null)
					{
						this._htmlEncodeFormatString = (bool)obj;
					}
					else
					{
						this._htmlEncodeFormatString = true;
					}
					this._htmlEncodeFormatStringSet = true;
				}
				return this._htmlEncodeFormatString;
			}
			set
			{
				object obj = base.ViewState["HtmlEncodeFormatString"];
				if (obj == null || (bool)obj != value)
				{
					base.ViewState["HtmlEncodeFormatString"] = value;
					this._htmlEncodeFormatString = value;
					this._htmlEncodeFormatStringSet = true;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000B9A RID: 2970
		// (get) Token: 0x060029B8 RID: 10680 RVA: 0x00086EE8 File Offset: 0x000850E8
		// (set) Token: 0x060029B9 RID: 10681 RVA: 0x00086F15 File Offset: 0x00085115
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("BoundField_NullDisplayText")]
		public virtual string NullDisplayText
		{
			get
			{
				object obj = base.ViewState["NullDisplayText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (!object.Equals(value, base.ViewState["NullDisplayText"]))
				{
					base.ViewState["NullDisplayText"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000B9B RID: 2971
		// (get) Token: 0x060029BA RID: 10682 RVA: 0x00086F48 File Offset: 0x00085148
		// (set) Token: 0x060029BB RID: 10683 RVA: 0x00086F74 File Offset: 0x00085174
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		[WebSysDescription("BoundField_ReadOnly")]
		public virtual bool ReadOnly
		{
			get
			{
				object obj = base.ViewState["ReadOnly"];
				return obj != null && (bool)obj;
			}
			set
			{
				object obj = base.ViewState["ReadOnly"];
				if (obj == null || (bool)obj != value)
				{
					base.ViewState["ReadOnly"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000B9C RID: 2972
		// (get) Token: 0x060029BC RID: 10684 RVA: 0x000097B7 File Offset: 0x000079B7
		protected virtual bool SupportsHtmlEncode
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060029BD RID: 10685 RVA: 0x00086FBC File Offset: 0x000851BC
		protected override void CopyProperties(DataControlField newField)
		{
			((BoundField)newField).ApplyFormatInEditMode = this.ApplyFormatInEditMode;
			((BoundField)newField).ConvertEmptyStringToNull = this.ConvertEmptyStringToNull;
			((BoundField)newField).DataField = this.DataField;
			((BoundField)newField).DataFormatString = this.DataFormatString;
			((BoundField)newField).HtmlEncode = this.HtmlEncode;
			((BoundField)newField).HtmlEncodeFormatString = this.HtmlEncodeFormatString;
			((BoundField)newField).NullDisplayText = this.NullDisplayText;
			((BoundField)newField).ReadOnly = this.ReadOnly;
			base.CopyProperties(newField);
		}

		// Token: 0x060029BE RID: 10686 RVA: 0x00087058 File Offset: 0x00085258
		protected override DataControlField CreateField()
		{
			return new BoundField();
		}

		// Token: 0x060029BF RID: 10687 RVA: 0x00087060 File Offset: 0x00085260
		public override void ExtractValuesFromCell(IOrderedDictionary dictionary, DataControlFieldCell cell, DataControlRowState rowState, bool includeReadOnly)
		{
			string dataField = this.DataField;
			object obj = null;
			string nullDisplayText = this.NullDisplayText;
			if ((rowState & DataControlRowState.Insert) != DataControlRowState.Normal && !this.InsertVisible)
			{
				return;
			}
			if (cell.Controls.Count > 0)
			{
				Control control = cell.Controls[0];
				TextBox textBox = control as TextBox;
				if (textBox != null)
				{
					obj = textBox.Text;
				}
			}
			else if (includeReadOnly)
			{
				string text = cell.Text;
				if (text == "&nbsp;")
				{
					obj = string.Empty;
				}
				else if (this.SupportsHtmlEncode && this.HtmlEncode)
				{
					obj = HttpUtility.HtmlDecode(text);
				}
				else
				{
					obj = text;
				}
			}
			if (obj != null)
			{
				if (obj is string && ((string)obj).Length == 0 && this.ConvertEmptyStringToNull)
				{
					obj = null;
				}
				if (obj is string && (string)obj == nullDisplayText && nullDisplayText.Length > 0)
				{
					obj = null;
				}
				if (dictionary.Contains(dataField))
				{
					dictionary[dataField] = obj;
					return;
				}
				dictionary.Add(dataField, obj);
			}
		}

		// Token: 0x060029C0 RID: 10688 RVA: 0x0008715C File Offset: 0x0008535C
		protected virtual string FormatDataValue(object dataValue, bool encode)
		{
			string result = string.Empty;
			if (!DataBinder.IsNull(dataValue))
			{
				string text = dataValue.ToString();
				string dataFormatString = this.DataFormatString;
				int length = text.Length;
				if (!this.HtmlEncodeFormatString)
				{
					if (length > 0 && encode)
					{
						text = HttpUtility.HtmlEncode(text);
					}
					if (length == 0 && this.ConvertEmptyStringToNull)
					{
						result = this.NullDisplayText;
					}
					else if (dataFormatString.Length == 0)
					{
						result = text;
					}
					else if (encode)
					{
						result = string.Format(CultureInfo.CurrentCulture, dataFormatString, new object[]
						{
							text
						});
					}
					else
					{
						result = string.Format(CultureInfo.CurrentCulture, dataFormatString, new object[]
						{
							dataValue
						});
					}
				}
				else
				{
					if (length == 0 && this.ConvertEmptyStringToNull)
					{
						text = this.NullDisplayText;
					}
					else
					{
						if (!string.IsNullOrEmpty(dataFormatString))
						{
							text = string.Format(CultureInfo.CurrentCulture, dataFormatString, new object[]
							{
								dataValue
							});
						}
						if (!string.IsNullOrEmpty(text) && encode)
						{
							text = HttpUtility.HtmlEncode(text);
						}
					}
					result = text;
				}
			}
			else
			{
				result = this.NullDisplayText;
			}
			return result;
		}

		// Token: 0x060029C1 RID: 10689 RVA: 0x00087251 File Offset: 0x00085451
		protected virtual object GetDesignTimeValue()
		{
			return SR.GetString("Sample_Databound_Text");
		}

		// Token: 0x060029C2 RID: 10690 RVA: 0x00087260 File Offset: 0x00085460
		protected virtual object GetValue(Control controlContainer)
		{
			object result = null;
			string dataField = this.DataField;
			if (controlContainer == null)
			{
				throw new HttpException(SR.GetString("DataControlField_NoContainer"));
			}
			object dataItem = DataBinder.GetDataItem(controlContainer);
			if (dataItem == null)
			{
				if (base.DesignMode)
				{
					return this.GetDesignTimeValue();
				}
				throw new HttpException(SR.GetString("DataItem_Not_Found"));
			}
			else
			{
				if (!dataField.Equals(BoundField.ThisExpression))
				{
					if (!this.TryGetSimplePropertyValue(dataItem, out result))
					{
						result = DataBinder.Eval(dataItem, dataField);
					}
					return result;
				}
				if (base.DesignMode)
				{
					return this.GetDesignTimeValue();
				}
				return dataItem;
			}
		}

		// Token: 0x060029C3 RID: 10691 RVA: 0x000872E8 File Offset: 0x000854E8
		private bool TryGetSimplePropertyValue(object dataItem, out object data)
		{
			string dataField = this.DataField;
			data = null;
			if (!this._boundFieldDescInitialized)
			{
				this._boundFieldDesc = TypeDescriptor.GetProperties(dataItem).Find(dataField, true);
				this._boundFieldDescInitialized = true;
			}
			if (this._boundFieldDesc != null)
			{
				data = this._boundFieldDesc.GetValue(dataItem);
				return true;
			}
			if (base.DesignMode)
			{
				data = this.GetDesignTimeValue();
				return true;
			}
			if (!dataField.Contains(BoundField._expressionPartSeparator))
			{
				throw new HttpException(SR.GetString("Field_Not_Found", new object[]
				{
					dataField
				}));
			}
			return false;
		}

		// Token: 0x060029C4 RID: 10692 RVA: 0x00087373 File Offset: 0x00085573
		public override bool Initialize(bool enableSorting, Control control)
		{
			base.Initialize(enableSorting, control);
			this._boundFieldDesc = null;
			this._boundFieldDescInitialized = false;
			return false;
		}

		// Token: 0x060029C5 RID: 10693 RVA: 0x00087390 File Offset: 0x00085590
		public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
		{
			string text = null;
			bool flag = false;
			bool flag2 = false;
			if (cellType == DataControlCellType.Header && this.SupportsHtmlEncode && this.HtmlEncode)
			{
				text = this.HeaderText;
				flag2 = true;
			}
			if (flag2 && !string.IsNullOrEmpty(text))
			{
				this._suppressHeaderTextFieldChange = true;
				this.HeaderText = HttpUtility.HtmlEncode(text);
				flag = true;
			}
			base.InitializeCell(cell, cellType, rowState, rowIndex);
			if (flag)
			{
				this.HeaderText = text;
				this._suppressHeaderTextFieldChange = false;
			}
			if (cellType == DataControlCellType.DataCell)
			{
				this.InitializeDataCell(cell, rowState);
			}
		}

		// Token: 0x060029C6 RID: 10694 RVA: 0x00087408 File Offset: 0x00085608
		protected virtual void InitializeDataCell(DataControlFieldCell cell, DataControlRowState rowState)
		{
			Control control = null;
			Control control2 = null;
			if (((rowState & DataControlRowState.Edit) != DataControlRowState.Normal && !this.ReadOnly) || (rowState & DataControlRowState.Insert) != DataControlRowState.Normal)
			{
				TextBox textBox = new TextBox();
				textBox.ToolTip = this.HeaderText;
				control = textBox;
				if (this.DataField.Length != 0 && (rowState & DataControlRowState.Edit) != DataControlRowState.Normal)
				{
					control2 = textBox;
				}
			}
			else if (this.DataField.Length != 0)
			{
				control2 = cell;
			}
			if (control != null)
			{
				cell.Controls.Add(control);
			}
			if (control2 != null && base.Visible)
			{
				control2.DataBinding += this.OnDataBindField;
			}
		}

		// Token: 0x060029C7 RID: 10695 RVA: 0x00087494 File Offset: 0x00085694
		protected virtual void OnDataBindField(object sender, EventArgs e)
		{
			Control control = (Control)sender;
			Control namingContainer = control.NamingContainer;
			object value = this.GetValue(namingContainer);
			bool encode = this.SupportsHtmlEncode && this.HtmlEncode && control is TableCell;
			string text = this.FormatDataValue(value, encode);
			if (control is TableCell)
			{
				if (text.Length == 0)
				{
					text = "&nbsp;";
				}
				((TableCell)control).Text = text;
				return;
			}
			if (!(control is TextBox))
			{
				throw new HttpException(SR.GetString("BoundField_WrongControlType", new object[]
				{
					this.DataField
				}));
			}
			if (this.ApplyFormatInEditMode)
			{
				((TextBox)control).Text = text;
			}
			else if (value != null)
			{
				((TextBox)control).Text = value.ToString();
			}
			if (value != null && value.GetType().IsPrimitive)
			{
				((TextBox)control).Columns = 5;
			}
		}

		// Token: 0x060029C8 RID: 10696 RVA: 0x00087572 File Offset: 0x00085772
		protected override void LoadViewState(object state)
		{
			this._dataField = null;
			this._dataFormatString = null;
			this._htmlEncodeSet = false;
			this._htmlEncodeFormatStringSet = false;
			base.LoadViewState(state);
		}

		// Token: 0x060029C9 RID: 10697 RVA: 0x00006164 File Offset: 0x00004364
		public override void ValidateSupportsCallback()
		{
		}

		// Token: 0x04001E75 RID: 7797
		public static readonly string ThisExpression = "!";

		// Token: 0x04001E76 RID: 7798
		private static readonly string _expressionPartSeparator = ".";

		// Token: 0x04001E77 RID: 7799
		private PropertyDescriptor _boundFieldDesc;

		// Token: 0x04001E78 RID: 7800
		private bool _boundFieldDescInitialized;

		// Token: 0x04001E79 RID: 7801
		private string _dataField;

		// Token: 0x04001E7A RID: 7802
		private string _dataFormatString;

		// Token: 0x04001E7B RID: 7803
		private bool _htmlEncode;

		// Token: 0x04001E7C RID: 7804
		private bool _htmlEncodeSet;

		// Token: 0x04001E7D RID: 7805
		private bool _suppressHeaderTextFieldChange;

		// Token: 0x04001E7E RID: 7806
		private bool _htmlEncodeFormatString;

		// Token: 0x04001E7F RID: 7807
		private bool _htmlEncodeFormatStringSet;
	}
}

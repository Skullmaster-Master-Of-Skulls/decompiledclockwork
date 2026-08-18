using System;
using System.ComponentModel;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000380 RID: 896
	public class BoundColumn : DataGridColumn
	{
		// Token: 0x17000B8F RID: 2959
		// (get) Token: 0x0600299C RID: 10652 RVA: 0x000868D0 File Offset: 0x00084AD0
		// (set) Token: 0x0600299D RID: 10653 RVA: 0x000868FD File Offset: 0x00084AFD
		[WebCategory("Data")]
		[DefaultValue("")]
		[WebSysDescription("BoundColumn_DataField")]
		public virtual string DataField
		{
			get
			{
				object obj = base.ViewState["DataField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataField"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000B90 RID: 2960
		// (get) Token: 0x0600299E RID: 10654 RVA: 0x00086918 File Offset: 0x00084B18
		// (set) Token: 0x0600299F RID: 10655 RVA: 0x00086945 File Offset: 0x00084B45
		[WebCategory("Behavior")]
		[DefaultValue("")]
		[WebSysDescription("BoundColumn_DataFormatString")]
		public virtual string DataFormatString
		{
			get
			{
				object obj = base.ViewState["DataFormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["DataFormatString"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000B91 RID: 2961
		// (get) Token: 0x060029A0 RID: 10656 RVA: 0x00086960 File Offset: 0x00084B60
		// (set) Token: 0x060029A1 RID: 10657 RVA: 0x00086989 File Offset: 0x00084B89
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		[WebSysDescription("BoundColumn_ReadOnly")]
		public virtual bool ReadOnly
		{
			get
			{
				object obj = base.ViewState["ReadOnly"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["ReadOnly"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x060029A2 RID: 10658 RVA: 0x000869A8 File Offset: 0x00084BA8
		protected virtual string FormatDataValue(object dataValue)
		{
			string result = string.Empty;
			if (!DataBinder.IsNull(dataValue))
			{
				if (this.formatting.Length == 0)
				{
					result = dataValue.ToString();
				}
				else
				{
					result = string.Format(CultureInfo.CurrentCulture, this.formatting, new object[]
					{
						dataValue
					});
				}
			}
			return result;
		}

		// Token: 0x060029A3 RID: 10659 RVA: 0x000869F5 File Offset: 0x00084BF5
		public override void Initialize()
		{
			base.Initialize();
			this.boundFieldDesc = null;
			this.boundFieldDescValid = false;
			this.boundField = this.DataField;
			this.formatting = this.DataFormatString;
		}

		// Token: 0x060029A4 RID: 10660 RVA: 0x00086A24 File Offset: 0x00084C24
		public override void InitializeCell(TableCell cell, int columnIndex, ListItemType itemType)
		{
			base.InitializeCell(cell, columnIndex, itemType);
			Control control = null;
			Control control2 = null;
			switch (itemType)
			{
			case ListItemType.Header:
			case ListItemType.Footer:
				goto IL_5D;
			case ListItemType.Item:
			case ListItemType.AlternatingItem:
			case ListItemType.SelectedItem:
				break;
			case ListItemType.EditItem:
				if (!this.ReadOnly)
				{
					TextBox textBox = new TextBox();
					control = textBox;
					if (this.boundField.Length != 0)
					{
						control2 = textBox;
						goto IL_5D;
					}
					goto IL_5D;
				}
				break;
			default:
				goto IL_5D;
			}
			if (this.DataField.Length != 0)
			{
				control2 = cell;
			}
			IL_5D:
			if (control != null)
			{
				cell.Controls.Add(control);
			}
			if (control2 != null)
			{
				control2.DataBinding += this.OnDataBindColumn;
			}
		}

		// Token: 0x060029A5 RID: 10661 RVA: 0x00086AB4 File Offset: 0x00084CB4
		private void OnDataBindColumn(object sender, EventArgs e)
		{
			Control control = (Control)sender;
			DataGridItem dataGridItem = (DataGridItem)control.NamingContainer;
			object dataItem = dataGridItem.DataItem;
			if (!this.boundFieldDescValid)
			{
				if (!this.boundField.Equals(BoundColumn.thisExpr))
				{
					this.boundFieldDesc = TypeDescriptor.GetProperties(dataItem).Find(this.boundField, true);
					if (this.boundFieldDesc == null && !base.DesignMode)
					{
						throw new HttpException(SR.GetString("Field_Not_Found", new object[]
						{
							this.boundField
						}));
					}
				}
				this.boundFieldDescValid = true;
			}
			object dataValue = dataItem;
			string text;
			if (this.boundFieldDesc == null && base.DesignMode)
			{
				text = SR.GetString("Sample_Databound_Text");
			}
			else
			{
				if (this.boundFieldDesc != null)
				{
					dataValue = this.boundFieldDesc.GetValue(dataItem);
				}
				text = this.FormatDataValue(dataValue);
			}
			if (control is TableCell)
			{
				if (text.Length == 0)
				{
					text = "&nbsp;";
				}
				((TableCell)control).Text = text;
				return;
			}
			((TextBox)control).Text = text;
		}

		// Token: 0x04001E70 RID: 7792
		public static readonly string thisExpr = "!";

		// Token: 0x04001E71 RID: 7793
		private PropertyDescriptor boundFieldDesc;

		// Token: 0x04001E72 RID: 7794
		private bool boundFieldDescValid;

		// Token: 0x04001E73 RID: 7795
		private string boundField;

		// Token: 0x04001E74 RID: 7796
		private string formatting;
	}
}

using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020011FF RID: 4607
	public class TreeListButtonColumn : TreeListColumn
	{
		// Token: 0x0600BE2D RID: 48685 RVA: 0x002A1FFC File Offset: 0x002A01FC
		private string GetCommandArgument(TreeListDataItem inItem)
		{
			string result = string.Empty;
			if (!string.IsNullOrEmpty(this.CommandArgument))
			{
				result = this.CommandArgument;
			}
			else
			{
				result = inItem.DisplayIndex.ToString(CultureInfo.InvariantCulture);
			}
			return result;
		}

		// Token: 0x0600BE2E RID: 48686 RVA: 0x002A211C File Offset: 0x002A031C
		protected override void InitializeDataCells(TableCell cell, int columnIndex, TreeListDataItem inItem)
		{
			WebControl webControl;
			switch (this.ButtonType)
			{
			case TreeListButtonColumnType.PushButton:
			{
				Button button = new Button();
				button.Text = this.Text;
				AccessibilityHelper.AddToolTip(button, this.ToolTip);
				button.CommandName = this.CommandName;
				button.CommandArgument = this.GetCommandArgument(inItem);
				webControl = button;
				break;
			}
			case TreeListButtonColumnType.ImageButton:
			{
				string spriteImageButtonCssClass = this.GetSpriteImageButtonCssClass(this.CommandName);
				if (string.IsNullOrEmpty(this.ImageUrl) && !string.IsNullOrEmpty(spriteImageButtonCssClass))
				{
					Button button2 = new Button();
					button2.Text = " ";
					if (string.IsNullOrEmpty(this.ToolTip))
					{
						button2.ToolTip = this.Text;
					}
					else
					{
						AccessibilityHelper.AddToolTip(button2, this.ToolTip);
					}
					button2.CssClass = spriteImageButtonCssClass;
					button2.CommandName = this.CommandName;
					button2.CommandArgument = this.CommandArgument;
					webControl = button2;
				}
				else
				{
					ImageButton imageButton = new ImageButton();
					imageButton.ID = "tlbc" + this.UniqueName;
					imageButton.AlternateText = this.Text;
					if (string.IsNullOrEmpty(this.ToolTip))
					{
						imageButton.ToolTip = this.Text;
					}
					else
					{
						AccessibilityHelper.AddToolTip(imageButton, this.ToolTip);
					}
					imageButton.ToolTip = this.Text;
					imageButton.CommandName = this.CommandName;
					imageButton.CommandArgument = this.GetCommandArgument(inItem);
					imageButton.ImageUrl = this.ImageUrl;
					imageButton.BorderWidth = Unit.Pixel(0);
					webControl = imageButton;
				}
				break;
			}
			case TreeListButtonColumnType.FontIconButton:
				if (base.Owner.ResolvedRenderMode == RenderMode.Lightweight || base.Owner.ResolvedRenderMode == RenderMode.Mobile)
				{
					ElasticButton elasticButton = new ElasticButton();
					elasticButton.ToolTip = this.CommandName;
					elasticButton.Text = this.Text;
					elasticButton.CommandName = this.CommandName;
					elasticButton.CommandArgument = this.CommandArgument;
					elasticButton.CausesValidation = false;
					elasticButton.UseSubmitBehavior = false;
					elasticButton.ID = "tbc" + this.UniqueName;
					elasticButton.PreRender += delegate(object sender, EventArgs e)
					{
						ElasticButton elasticButton2 = sender as ElasticButton;
						if (!string.IsNullOrEmpty(this.ImageUrl))
						{
							return;
						}
						if (elasticButton2.CommandName == "Delete")
						{
							elasticButton2.FirstSpanClass = "t-font-icon rtlIcon rtlDelIcon";
							elasticButton2.CssClass = "t-button rtlActionButton rtlDel";
							return;
						}
						if (elasticButton2.CommandName == "Edit")
						{
							elasticButton2.FirstSpanClass = "t-font-icon rtlIcon rtlEditIcon";
							elasticButton2.CssClass = "t-button rtlActionButton rtlEdit";
							return;
						}
						if (!string.IsNullOrEmpty(this.CommandName) || !string.IsNullOrEmpty(this.Text))
						{
							elasticButton2.FirstSpanClass = "t-font-icon rtlIcon";
							if (!string.IsNullOrEmpty(this.CommandName))
							{
								ElasticButton elasticButton3 = elasticButton2;
								elasticButton3.FirstSpanClass = elasticButton3.FirstSpanClass + " rtl" + this.CommandName + "Icon";
							}
							elasticButton2.SecondSpanClass = "rtlButtonText";
							elasticButton2.SecondSpanInnerText = this.Text;
						}
					};
					webControl = elasticButton;
				}
				else
				{
					webControl = new LinkButton
					{
						Text = this.Text,
						CommandName = this.CommandName,
						CommandArgument = this.GetCommandArgument(inItem)
					};
				}
				break;
			default:
				webControl = new LinkButton
				{
					Text = this.Text,
					CommandName = this.CommandName,
					CommandArgument = this.GetCommandArgument(inItem)
				};
				break;
			}
			if (this.DataTextField.Length != 0 || this.ConfirmTextFields.Length != 0)
			{
				webControl.DataBinding += this.OnDataBindColumn;
			}
			this.SetConfirmMessage(webControl);
			if (!string.IsNullOrEmpty(this.ButtonCssClass.Trim()))
			{
				string cssClass = webControl.CssClass;
				if (string.IsNullOrEmpty(cssClass))
				{
					webControl.CssClass = this.ButtonCssClass;
				}
				else
				{
					webControl.CssClass = cssClass + " " + this.ButtonCssClass;
				}
			}
			IButtonControl buttonControl = (IButtonControl)webControl;
			if (this.ShouldCauseValidation(buttonControl.CommandName))
			{
				buttonControl.CausesValidation = true;
				buttonControl.ValidationGroup = base.Owner.ValidationSettings.ValidationGroup;
			}
			else
			{
				buttonControl.CausesValidation = false;
			}
			cell.Controls.Add(webControl);
		}

		// Token: 0x0600BE2F RID: 48687 RVA: 0x002A2478 File Offset: 0x002A0678
		protected virtual bool ShouldCauseValidation(string command)
		{
			if (!base.Owner.ValidationSettings.EnableValidation)
			{
				return false;
			}
			string[] commandsToValidate = base.Owner.ValidationSettings.CommandsToValidate;
			for (int i = 0; i < commandsToValidate.Length; i++)
			{
				if (string.Compare(commandsToValidate[i], command, true, CultureInfo.CurrentCulture) == 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x0600BE30 RID: 48688 RVA: 0x002A24CC File Offset: 0x002A06CC
		private string GetSpriteImageButtonCssClass(string commandName)
		{
			if (commandName != null)
			{
				if (commandName == "Edit")
				{
					return "rtlEdit";
				}
				if (commandName == "InitInsert")
				{
					return "rtlAdd";
				}
				if (commandName == "PerformInsert")
				{
					return "rtlUpdate";
				}
				if (commandName == "Update")
				{
					return "rtlUpdate";
				}
				if (commandName == "Cancel")
				{
					return "rtlCancel";
				}
				if (commandName == "Delete")
				{
					return "rtlDel";
				}
			}
			return string.Empty;
		}

		// Token: 0x17003D59 RID: 15705
		// (get) Token: 0x0600BE31 RID: 48689 RVA: 0x002A2558 File Offset: 0x002A0758
		// (set) Token: 0x0600BE32 RID: 48690 RVA: 0x002A25A7 File Offset: 0x002A07A7
		[DefaultValue(typeof(TreeListButtonColumnType), "LinkButton")]
		[Description("The type of button contained within the column.")]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		public virtual TreeListButtonColumnType ButtonType
		{
			get
			{
				object obj = base.ViewState["_bt"];
				if (obj != null)
				{
					return (TreeListButtonColumnType)obj;
				}
				if (base.Owner == null || (base.Owner.ResolvedRenderMode != RenderMode.Lightweight && base.Owner.ResolvedRenderMode != RenderMode.Mobile))
				{
					return TreeListButtonColumnType.LinkButton;
				}
				return TreeListButtonColumnType.FontIconButton;
			}
			set
			{
				if (value < TreeListButtonColumnType.LinkButton || value > TreeListButtonColumnType.FontIconButton)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["_bt"] = value;
			}
		}

		// Token: 0x17003D5A RID: 15706
		// (get) Token: 0x0600BE33 RID: 48691 RVA: 0x002A25D4 File Offset: 0x002A07D4
		// (set) Token: 0x0600BE34 RID: 48692 RVA: 0x002A2601 File Offset: 0x002A0801
		[DefaultValue("")]
		[Description("The command associated with the button.")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		public virtual string CommandName
		{
			get
			{
				object obj = base.ViewState["_cn"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["_cn"] = value;
			}
		}

		// Token: 0x17003D5B RID: 15707
		// (get) Token: 0x0600BE35 RID: 48693 RVA: 0x002A2614 File Offset: 0x002A0814
		// (set) Token: 0x0600BE36 RID: 48694 RVA: 0x002A2641 File Offset: 0x002A0841
		[Category("Behavior")]
		[Description("The command argument associated with the button.")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string CommandArgument
		{
			get
			{
				object obj = base.ViewState["_cna"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["_cna"] = value;
			}
		}

		// Token: 0x17003D5C RID: 15708
		// (get) Token: 0x0600BE37 RID: 48695 RVA: 0x002A2654 File Offset: 0x002A0854
		// (set) Token: 0x0600BE38 RID: 48696 RVA: 0x002A2681 File Offset: 0x002A0881
		[Localizable(true)]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("The formatting applied to the value bound to the ConfirmText property.")]
		public virtual string ConfirmTextFormatString
		{
			get
			{
				object obj = base.ViewState["_ctfs"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["_ctfs"] = value;
			}
		}

		// Token: 0x17003D5D RID: 15709
		// (get) Token: 0x0600BE39 RID: 48697 RVA: 0x002A2694 File Offset: 0x002A0894
		// (set) Token: 0x0600BE3A RID: 48698 RVA: 0x002A26C1 File Offset: 0x002A08C1
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string ConfirmTitle
		{
			get
			{
				object obj = base.ViewState["_ct"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				base.ViewState["_ct"] = value;
			}
		}

		// Token: 0x17003D5E RID: 15710
		// (get) Token: 0x0600BE3B RID: 48699 RVA: 0x002A26D4 File Offset: 0x002A08D4
		// (set) Token: 0x0600BE3C RID: 48700 RVA: 0x002A2701 File Offset: 0x002A0901
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string ConfirmText
		{
			get
			{
				object obj = base.ViewState["_cf"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				base.ViewState["_cf"] = value;
			}
		}

		// Token: 0x17003D5F RID: 15711
		// (get) Token: 0x0600BE3D RID: 48701 RVA: 0x002A2714 File Offset: 0x002A0914
		// (set) Token: 0x0600BE3E RID: 48702 RVA: 0x002A2742 File Offset: 0x002A0942
		[DefaultValue("")]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[TypeConverter(typeof(GridStringArrayConverter))]
		[Description("the DataFields from the data source, which will be applied to the formatting specified in the ConfirmTextFormatString property")]
		[NotifyParentProperty(true)]
		public virtual string[] ConfirmTextFields
		{
			get
			{
				object obj = base.ViewState["ConfirmTextFields"];
				if (obj != null)
				{
					return (string[])obj;
				}
				return new string[0];
			}
			set
			{
				base.ViewState["ConfirmTextFields"] = value;
			}
		}

		// Token: 0x17003D60 RID: 15712
		// (get) Token: 0x0600BE3F RID: 48703 RVA: 0x002A2758 File Offset: 0x002A0958
		// (set) Token: 0x0600BE40 RID: 48704 RVA: 0x002A2781 File Offset: 0x002A0981
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		[Description("The type of confirm dialog.")]
		[DefaultValue(typeof(TreeListConfirmDialogType), "Classic")]
		public virtual TreeListConfirmDialogType ConfirmDialogType
		{
			get
			{
				object obj = base.ViewState["_cdt"];
				if (obj != null)
				{
					return (TreeListConfirmDialogType)obj;
				}
				return TreeListConfirmDialogType.Classic;
			}
			set
			{
				if (value < TreeListConfirmDialogType.Classic || value > TreeListConfirmDialogType.RadWindow)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["_cdt"] = value;
			}
		}

		// Token: 0x17003D61 RID: 15713
		// (get) Token: 0x0600BE41 RID: 48705 RVA: 0x002A27AC File Offset: 0x002A09AC
		// (set) Token: 0x0600BE42 RID: 48706 RVA: 0x002A27D9 File Offset: 0x002A09D9
		[DefaultValue(typeof(Unit), "")]
		[Description("Gets or sets the width of the Confirm Dialog (if it is a RadWindow)")]
		public virtual Unit ConfirmDialogWidth
		{
			get
			{
				object obj = base.ViewState["_cdw"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				return Unit.Empty;
			}
			set
			{
				base.ViewState["_cdw"] = value;
			}
		}

		// Token: 0x17003D62 RID: 15714
		// (get) Token: 0x0600BE43 RID: 48707 RVA: 0x002A27F4 File Offset: 0x002A09F4
		// (set) Token: 0x0600BE44 RID: 48708 RVA: 0x002A2821 File Offset: 0x002A0A21
		[DefaultValue(typeof(Unit), "")]
		[Description("Gets or sets the height of the Confirm Dialog (if it is a RadWindow)")]
		public virtual Unit ConfirmDialogHeight
		{
			get
			{
				object obj = base.ViewState["_cdh"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				return Unit.Empty;
			}
			set
			{
				base.ViewState["_cdh"] = value;
			}
		}

		// Token: 0x17003D63 RID: 15715
		// (get) Token: 0x0600BE45 RID: 48709 RVA: 0x002A283C File Offset: 0x002A0A3C
		// (set) Token: 0x0600BE46 RID: 48710 RVA: 0x002A2869 File Offset: 0x002A0A69
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		[DefaultValue("")]
		[Description("The text used for the button.")]
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
			}
		}

		// Token: 0x17003D64 RID: 15716
		// (get) Token: 0x0600BE47 RID: 48711 RVA: 0x002A287C File Offset: 0x002A0A7C
		// (set) Token: 0x0600BE48 RID: 48712 RVA: 0x002A28A9 File Offset: 0x002A0AA9
		[NotifyParentProperty(true)]
		[SuppressMessage("Microsoft.Design", "CA1056:UriPropertiesShouldNotBeStrings")]
		[DefaultValue("")]
		public virtual string ImageUrl
		{
			get
			{
				object obj = base.ViewState["_iurl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				base.ViewState["_iurl"] = value;
			}
		}

		// Token: 0x17003D65 RID: 15717
		// (get) Token: 0x0600BE49 RID: 48713 RVA: 0x002A28BC File Offset: 0x002A0ABC
		// (set) Token: 0x0600BE4A RID: 48714 RVA: 0x002A28E9 File Offset: 0x002A0AE9
		[DefaultValue("")]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
		public virtual string ButtonCssClass
		{
			get
			{
				object obj = base.ViewState["_bcc"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				base.ViewState["_bcc"] = value;
			}
		}

		// Token: 0x17003D66 RID: 15718
		// (get) Token: 0x0600BE4B RID: 48715 RVA: 0x002A28FC File Offset: 0x002A0AFC
		// (set) Token: 0x0600BE4C RID: 48716 RVA: 0x002A2929 File Offset: 0x002A0B29
		[NotifyParentProperty(true)]
		[Category("Data")]
		[Description("The field bound to the text property of the button.")]
		[DefaultValue("")]
		public virtual string DataTextField
		{
			get
			{
				object obj = base.ViewState["_dtf"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["_dtf"] = value;
			}
		}

		// Token: 0x17003D67 RID: 15719
		// (get) Token: 0x0600BE4D RID: 48717 RVA: 0x002A293C File Offset: 0x002A0B3C
		// (set) Token: 0x0600BE4E RID: 48718 RVA: 0x002A2969 File Offset: 0x002A0B69
		[Category("Data")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Description("The formatting applied to the value bound to the Text property.")]
		[Localizable(true)]
		public virtual string DataTextFormatString
		{
			get
			{
				object obj = base.ViewState["_dtfs"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["_dtfs"] = value;
			}
		}

		// Token: 0x17003D68 RID: 15720
		// (get) Token: 0x0600BE4F RID: 48719 RVA: 0x002A297C File Offset: 0x002A0B7C
		// (set) Token: 0x0600BE50 RID: 48720 RVA: 0x002A299C File Offset: 0x002A0B9C
		[NotifyParentProperty(true)]
		[Description("Gets or sets the tooltip for each of buttons")]
		[DefaultValue("")]
		[Localizable(true)]
		public virtual string ToolTip
		{
			get
			{
				return (base.ViewState["ToolTip"] as string) ?? string.Empty;
			}
			set
			{
				base.ViewState["ToolTip"] = value;
			}
		}

		// Token: 0x0600BE51 RID: 48721 RVA: 0x002A29B0 File Offset: 0x002A0BB0
		private void OnDataBindColumn(object sender, EventArgs e)
		{
			Control control = (Control)sender;
			TreeListDataItem treeListDataItem = TreeListColumn.GetBindingParentItem(control) as TreeListDataItem;
			object dataItem = treeListDataItem.DataItem;
			object dataValue = null;
			string text = string.Empty;
			if (!string.IsNullOrEmpty(this.DataTextField) && base.TryExtractDataValue(dataItem, this.DataTextField, out dataValue))
			{
				text = this.FormatDataValue(dataValue, treeListDataItem);
			}
			if (string.IsNullOrEmpty(text))
			{
				text = this.Text;
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (this.ConfirmTextFields.Length > 0)
			{
				ArrayList arrayList = new ArrayList(this.ConfirmTextFields);
				foreach (object obj in arrayList)
				{
					string text2 = (string)obj;
					if (!base.DesignMode && !string.IsNullOrEmpty(text2))
					{
						object value = null;
						if (base.TryExtractDataValue(dataItem, text2, out value))
						{
							dictionary.Add(text2, value);
						}
					}
				}
			}
			if (dictionary.Count > 0)
			{
				object[] array = new object[dictionary.Count];
				int num = 0;
				foreach (string key in this.ConfirmTextFields)
				{
					if (dictionary.ContainsKey(key))
					{
						array[num] = dictionary[key];
					}
					num++;
				}
				this.ConfirmText = this.FormatConfirmTextValue(array);
				this.SetConfirmMessage(sender as WebControl);
			}
			LinkButton linkButton = control as LinkButton;
			if (linkButton != null)
			{
				linkButton.Text = text;
				return;
			}
			Button button = control as Button;
			if (button != null)
			{
				button.Text = text;
				return;
			}
			ImageButton imageButton = control as ImageButton;
			imageButton.AlternateText = text;
			imageButton.ToolTip = text;
		}

		// Token: 0x0600BE52 RID: 48722 RVA: 0x002A2B6C File Offset: 0x002A0D6C
		protected virtual string FormatDataValue(object dataValue, TreeListDataItem item)
		{
			if (dataValue == null || dataValue == DBNull.Value)
			{
				return string.Empty;
			}
			if (this.DataTextFormatString.Length == 0)
			{
				return dataValue.ToString();
			}
			return string.Format(CultureInfo.InvariantCulture, this.DataTextFormatString, new object[]
			{
				dataValue
			});
		}

		// Token: 0x0600BE53 RID: 48723 RVA: 0x002A2BBC File Offset: 0x002A0DBC
		protected virtual string FormatConfirmTextValue(object[] confirmTextFields)
		{
			for (int i = 0; i < confirmTextFields.Length; i++)
			{
				if (confirmTextFields[i] == null || confirmTextFields[i] == DBNull.Value)
				{
					confirmTextFields[i] = string.Empty;
				}
			}
			string confirmTextFormatString = this.ConfirmTextFormatString;
			if (confirmTextFormatString.Length == 0)
			{
				return confirmTextFields[0].ToString();
			}
			string result = string.Empty;
			try
			{
				result = string.Format(confirmTextFormatString, confirmTextFields);
			}
			catch (Exception)
			{
				throw new FormatException("Illegal ConfirmTextFormatString for column: " + this.UniqueName);
			}
			return result;
		}

		// Token: 0x0600BE54 RID: 48724 RVA: 0x002A2C40 File Offset: 0x002A0E40
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		protected virtual void SetConfirmMessage(WebControl control)
		{
			if (!string.IsNullOrEmpty(this.ConfirmText))
			{
				string text = "";
				if (control is LinkButton)
				{
					text = (control as LinkButton).OnClientClick;
					(control as LinkButton).OnClientClick = "";
				}
				else if (control is Button)
				{
					text = (control as Button).OnClientClick;
					(control as Button).OnClientClick = "";
				}
				else if (control is ImageButton)
				{
					text = (control as ImageButton).OnClientClick;
					(control as ImageButton).OnClientClick = "";
				}
				string text2 = string.Format("$find('{0}').confirm", base.Owner.ClientID);
				string text3 = (this.ConfirmDialogType == TreeListConfirmDialogType.Classic) ? "confirm" : text2;
				string text4 = this.ConfirmText.Replace("'", "\\'");
				if (this.ConfirmDialogType == TreeListConfirmDialogType.Classic)
				{
					control.Attributes["onclick"] = string.Format("if(!{0}('{1}'))return false;{2}", text3, text4, text);
					return;
				}
				string text5 = "";
				if (!this.ConfirmDialogWidth.IsEmpty && !this.ConfirmDialogHeight.IsEmpty)
				{
					text5 = string.Concat(new string[]
					{
						", '",
						this.ConfirmDialogWidth.ToString(),
						"', '",
						this.ConfirmDialogHeight.ToString(),
						"'"
					});
				}
				else if (!this.ConfirmDialogWidth.IsEmpty)
				{
					text5 = ", '" + this.ConfirmDialogWidth.ToString() + "'";
				}
				else if (!this.ConfirmDialogHeight.IsEmpty)
				{
					text5 = ", null, '" + this.ConfirmDialogHeight.ToString() + "'";
				}
				control.Attributes["onclick"] = string.Format("if(!{0}('{1}', event, '{2}'{4}))return false;{3}", new object[]
				{
					text3,
					text4,
					this.ConfirmTitle,
					text,
					text5
				});
			}
		}
	}
}

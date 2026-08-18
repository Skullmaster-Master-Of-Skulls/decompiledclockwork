using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020010A7 RID: 4263
	public class GridButtonColumn : GridColumn
	{
		// Token: 0x170037F7 RID: 14327
		// (get) Token: 0x0600AD45 RID: 44357 RVA: 0x00255B59 File Offset: 0x00253D59
		public override bool Selectable
		{
			get
			{
				return false;
			}
		}

		// Token: 0x0600AD46 RID: 44358 RVA: 0x00255B5C File Offset: 0x00253D5C
		protected virtual string FormatDataTextValue(object dataTextValue)
		{
			string empty = string.Empty;
			if (base.Owner != null && base.Owner.OwnerGrid.IsExporting && base.Owner.OwnerGrid.ExportSettings.SuppressColumnDataFormatStrings)
			{
				return dataTextValue.ToString();
			}
			if (dataTextValue == null || dataTextValue == DBNull.Value)
			{
				return empty;
			}
			string dataTextFormatString = this.DataTextFormatString;
			if (dataTextFormatString.Length == 0)
			{
				return dataTextValue.ToString();
			}
			return string.Format(dataTextFormatString, dataTextValue);
		}

		// Token: 0x0600AD47 RID: 44359 RVA: 0x00255BD4 File Offset: 0x00253DD4
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

		// Token: 0x0600AD48 RID: 44360 RVA: 0x00255C58 File Offset: 0x00253E58
		public override void Initialize()
		{
			base.Initialize();
			this.textFieldDesc = null;
			this.confirmFieldsDesc = new PropertyDescriptorCollection(new PropertyDescriptor[0]);
		}

		// Token: 0x0600AD49 RID: 44361 RVA: 0x00255DA4 File Offset: 0x00253FA4
		public override void InitializeCell(TableCell cell, int columnIndex, GridItem inItem)
		{
			GridGroupFooterItem gridGroupFooterItem = inItem as GridGroupFooterItem;
			if (gridGroupFooterItem != null && gridGroupFooterItem.OwnerTableView.GroupFooterTemplate != null)
			{
				return;
			}
			base.InitializeCell(cell, columnIndex, inItem);
			if (!(inItem is GridHeaderItem) && !(inItem is GridFooterItem) && !(inItem is GridFilteringItem) && gridGroupFooterItem == null)
			{
				WebControl webControl;
				if (this.ButtonType == GridButtonColumnType.LinkButton)
				{
					webControl = new GridLinkButton
					{
						Text = this.Text,
						CommandName = this.CommandName,
						CommandArgument = this.CommandArgument,
						CausesValidation = false
					};
				}
				else if (this.ButtonType == GridButtonColumnType.PushButton)
				{
					webControl = new Button
					{
						Text = this.Text,
						ToolTip = this.Text,
						CommandName = this.CommandName,
						CommandArgument = this.CommandArgument,
						CausesValidation = false
					};
				}
				else if (this.ButtonType == GridButtonColumnType.FontIconButton && base.Owner != null && (base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile || base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight))
				{
					ElasticButton elasticButton = new ElasticButton();
					elasticButton.ToolTip = this.CommandName;
					elasticButton.Attributes.Add("aria-label", this.CommandName);
					elasticButton.Text = this.Text;
					elasticButton.CommandName = this.CommandName;
					elasticButton.CommandArgument = this.CommandArgument;
					elasticButton.CausesValidation = false;
					elasticButton.UseSubmitBehavior = false;
					elasticButton.PreRender += delegate(object sender, EventArgs e)
					{
						ElasticButton elasticButton2 = sender as ElasticButton;
						if (!string.IsNullOrEmpty(this.ImageUrl))
						{
							return;
						}
						if (elasticButton2.CommandName == "Delete")
						{
							elasticButton2.FirstSpanClass = "t-font-icon rgIcon rgDelIcon";
							elasticButton2.CssClass = "t-button rgActionButton rgDel";
							return;
						}
						if (elasticButton2.CommandName == "Edit")
						{
							elasticButton2.FirstSpanClass = "t-font-icon rgIcon rgEditIcon";
							elasticButton2.CssClass = "t-button rgActionButton rgEdit";
							return;
						}
						if (!string.IsNullOrEmpty(this.CommandName) || !string.IsNullOrEmpty(this.Text))
						{
							elasticButton2.FirstSpanClass = "t-font-icon rgIcon";
							elasticButton2.CssClass = "t-button";
							if (!string.IsNullOrEmpty(this.CommandName))
							{
								ElasticButton elasticButton3 = elasticButton2;
								elasticButton3.FirstSpanClass = elasticButton3.FirstSpanClass + " rg" + this.CommandName + "Icon";
								ElasticButton elasticButton4 = elasticButton2;
								elasticButton4.CssClass = elasticButton4.CssClass + " rg" + this.CommandName;
							}
							elasticButton2.SecondSpanClass = "t-text rgButtonText";
							elasticButton2.SecondSpanInnerText = this.Text;
						}
					};
					webControl = elasticButton;
				}
				else
				{
					GridButtonColumn.GridButtonColumnImageButton gridButtonColumnImageButton = new GridButtonColumn.GridButtonColumnImageButton();
					gridButtonColumnImageButton.AlternateText = this.Text;
					gridButtonColumnImageButton.ToolTip = this.Text;
					gridButtonColumnImageButton.CommandName = this.CommandName;
					gridButtonColumnImageButton.CommandArgument = this.CommandArgument;
					gridButtonColumnImageButton.CausesValidation = false;
					gridButtonColumnImageButton.ImageUrl = this.ImageUrl;
					gridButtonColumnImageButton.BorderWidth = Unit.Pixel(0);
					gridButtonColumnImageButton.ID = "gbc" + this.UniqueName;
					gridButtonColumnImageButton.PreRender += delegate(object sender, EventArgs e)
					{
						GridButtonColumn.GridButtonColumnImageButton target = sender as GridButtonColumn.GridButtonColumnImageButton;
						this.ConfigureButtonImage(target);
						this.ConfigureButtonImageAltText(target);
					};
					webControl = gridButtonColumnImageButton;
				}
				string text = string.IsNullOrEmpty(this.CommandArgument) ? inItem.ItemIndexHierarchical : this.CommandArgument;
				this.TrySetOnClientClickScript(webControl, inItem, "fireCommand", new string[]
				{
					this.CommandName,
					text
				});
				if (this.ConfirmTextFields.Length != 0 || this.DataTextField.Length != 0)
				{
					webControl.DataBinding += this.OnDataBindColumn;
				}
				this.SetConfirmMessage(webControl);
				if (inItem.IsInEditMode)
				{
					webControl.Visible = this.ShowInEditForm;
				}
				if (this.UniqueName == "AutoGeneratedDeleteColumn")
				{
					webControl.ID = "AutoGeneratedDeleteButton";
				}
				if (!string.IsNullOrEmpty(this.ButtonCssClass.Trim()))
				{
					if ((base.Owner != null && base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Lightweight) || base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile)
					{
						if (string.IsNullOrEmpty(webControl.CssClass))
						{
							webControl.CssClass = this.ButtonCssClass;
						}
						else
						{
							webControl.CssClass = " " + this.ButtonCssClass;
						}
					}
					else
					{
						webControl.CssClass = this.ButtonCssClass;
					}
				}
				cell.Controls.Add(webControl);
			}
		}

		// Token: 0x0600AD4A RID: 44362 RVA: 0x00256118 File Offset: 0x00254318
		private void ConfigureButtonImage(GridButtonColumn.GridButtonColumnImageButton target)
		{
			if (!string.IsNullOrEmpty(this.ImageUrl) || (!string.IsNullOrEmpty(target.ImageUrl) && target.ImageUrl.GetHashCode() != target.ImageUrlSetHash))
			{
				return;
			}
			if (target.CommandName == "Delete")
			{
				target.ImageUrl = base.Owner.OwnerGrid.ResolveGridImageUrl("Delete.gif", false);
			}
			else if (target.CommandName == "Edit")
			{
				target.ImageUrl = base.Owner.OwnerGrid.ResolveGridImageUrl("Edit.gif", false);
			}
			target.ImageUrlSetHash = target.ImageUrl.GetHashCode();
		}

		// Token: 0x0600AD4B RID: 44363 RVA: 0x002561C4 File Offset: 0x002543C4
		private void ConfigureButtonImageAltText(ImageButton target)
		{
			if (!string.IsNullOrEmpty(target.AlternateText) || !string.IsNullOrEmpty(target.ToolTip))
			{
				return;
			}
			if (target.CommandName == "Delete")
			{
				target.AlternateText = "Delete";
				target.ToolTip = "Delete";
				return;
			}
			if (target.CommandName == "Edit")
			{
				target.AlternateText = "Edit";
				target.ToolTip = "Edit";
			}
		}

		// Token: 0x0600AD4C RID: 44364 RVA: 0x00256240 File Offset: 0x00254440
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
				string text2 = string.Format("$find('{0}').confirm", base.Owner.OwnerGrid.ClientID);
				string text3 = (this.ConfirmDialogType == GridConfirmDialogType.Classic) ? "confirm" : text2;
				string text4 = this.ConfirmText.Replace("'", "\\'");
				if (this.ConfirmDialogType == GridConfirmDialogType.Classic)
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
				control.Attributes["onclick"] = string.Format("if(!{0}('{1}', event, '{5}', '{2}'{4}))return false;{3}", new object[]
				{
					text3,
					text4,
					this.ConfirmTitle,
					text,
					text5,
					base.Owner.OwnerGrid.ClientID
				});
			}
		}

		// Token: 0x0600AD4D RID: 44365 RVA: 0x0025648C File Offset: 0x0025468C
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		private void OnDataBindColumn(object sender, EventArgs e)
		{
			Control control = (Control)sender;
			GridItem bindingParentItem = GridColumn.GetBindingParentItem(control);
			object obj = bindingParentItem.DataItem;
			if (this.textFieldDesc == null || this.textFieldDesc.ComponentType != obj.GetType())
			{
				string dataTextField = this.DataTextField;
				this.textFieldDesc = TypeDescriptor.GetProperties(obj).Find(dataTextField, true);
				if (this.textFieldDesc == null && !base.DesignMode && !string.IsNullOrEmpty(this.DataTextField))
				{
					if (this.DataTextField.IndexOf(".") > -1)
					{
						try
						{
							obj = DataBinder.Eval(obj, this.DataTextField);
							goto IL_BA;
						}
						catch
						{
							goto IL_BA;
						}
					}
					try
					{
						obj = DataBinder.GetPropertyValue(obj, this.DataTextField);
					}
					catch
					{
						try
						{
							obj = DataBinder.Eval(obj, this.DataTextField);
						}
						catch
						{
						}
					}
				}
			}
			IL_BA:
			PropertyDescriptorCollection propertyDescriptorCollection = null;
			if (this.confirmFieldsDesc == null)
			{
				propertyDescriptorCollection = TypeDescriptor.GetProperties(obj);
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (this.confirmFieldsDesc != null && this.ConfirmTextFields.Length > 0)
			{
				ArrayList arrayList = new ArrayList(this.ConfirmTextFields);
				foreach (object obj2 in arrayList)
				{
					string text = (string)obj2;
					if (!string.IsNullOrEmpty(text))
					{
						PropertyDescriptor propertyDescriptor = this.confirmFieldsDesc.Find(text, true);
						if (propertyDescriptor == null)
						{
							if (propertyDescriptorCollection != null)
							{
								propertyDescriptor = propertyDescriptorCollection.Find(text, true);
								if (propertyDescriptor != null)
								{
									this.confirmFieldsDesc.Add(propertyDescriptor);
									dictionary.Add(text, propertyDescriptor);
								}
							}
						}
						else
						{
							dictionary.Add(text, propertyDescriptor);
						}
						if (propertyDescriptor == null && !base.DesignMode && !string.IsNullOrEmpty(text))
						{
							object value = this.ExtractPropertyValue(obj, text);
							dictionary.Add(text, value);
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
					if (dictionary.ContainsKey(key) && obj != null)
					{
						if (dictionary[key] is PropertyDescriptor)
						{
							array[num] = ((PropertyDescriptor)dictionary[key]).GetValue(obj);
						}
						else
						{
							array[num] = dictionary[key];
						}
					}
					num++;
				}
				this.ConfirmText = this.FormatConfirmTextValue(array);
				string text2 = string.IsNullOrEmpty(this.CommandArgument) ? bindingParentItem.ItemIndexHierarchical : this.CommandArgument;
				this.TrySetOnClientClickScript(control, bindingParentItem, "fireCommand", new string[]
				{
					this.CommandName,
					text2
				});
				this.SetConfirmMessage(control as WebControl);
			}
			if (this.DataTextField.Length != 0)
			{
				object dataTextValue = obj;
				string text3;
				if (this.textFieldDesc == null && base.DesignMode)
				{
					text3 = "ButtonColumn";
				}
				else
				{
					if (this.textFieldDesc != null)
					{
						dataTextValue = this.textFieldDesc.GetValue(obj);
					}
					text3 = this.FormatDataTextValue(dataTextValue);
				}
				if (control is LinkButton)
				{
					((LinkButton)control).Text = text3;
					return;
				}
				if (control is Button)
				{
					((Button)control).Text = text3;
					return;
				}
				((ImageButton)control).AlternateText = text3;
				((ImageButton)control).ToolTip = text3;
			}
		}

		// Token: 0x0600AD4E RID: 44366 RVA: 0x002567FC File Offset: 0x002549FC
		private object ExtractPropertyValue(object obj1, string dataFieldName)
		{
			object result = null;
			if (!string.IsNullOrEmpty(dataFieldName))
			{
				if (dataFieldName.IndexOf(".") > -1)
				{
					try
					{
						return DataBinder.Eval(obj1, dataFieldName);
					}
					catch
					{
						if (!GridBaseDataList.IsBindableType(obj1.GetType()))
						{
							result = null;
						}
						return result;
					}
				}
				try
				{
					result = DataBinder.GetPropertyValue(obj1, dataFieldName);
				}
				catch
				{
					try
					{
						result = DataBinder.Eval(obj1, dataFieldName);
					}
					catch
					{
						if (!GridBaseDataList.IsBindableType(obj1.GetType()))
						{
							result = null;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x170037F8 RID: 14328
		// (get) Token: 0x0600AD4F RID: 44367 RVA: 0x00256890 File Offset: 0x00254A90
		// (set) Token: 0x0600AD50 RID: 44368 RVA: 0x002568BD File Offset: 0x00254ABD
		[DefaultValue("")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
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
				this.OnColumnChanged();
			}
		}

		// Token: 0x170037F9 RID: 14329
		// (get) Token: 0x0600AD51 RID: 44369 RVA: 0x002568D8 File Offset: 0x00254AD8
		// (set) Token: 0x0600AD52 RID: 44370 RVA: 0x00256931 File Offset: 0x00254B31
		[DefaultValue(typeof(GridButtonColumnType), "LinkButton")]
		[Category("Appearance")]
		[Description("The type of button contained within the column.")]
		[NotifyParentProperty(true)]
		public virtual GridButtonColumnType ButtonType
		{
			get
			{
				object obj = base.ViewState["_bt"];
				if (obj != null)
				{
					return (GridButtonColumnType)obj;
				}
				if (base.Owner == null || (base.Owner.OwnerGrid.ResolvedRenderMode != RenderMode.Lightweight && base.Owner.OwnerGrid.ResolvedRenderMode != RenderMode.Mobile))
				{
					return GridButtonColumnType.LinkButton;
				}
				return GridButtonColumnType.FontIconButton;
			}
			set
			{
				if (value < GridButtonColumnType.LinkButton || value > GridButtonColumnType.FontIconButton)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["_bt"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x170037FA RID: 14330
		// (get) Token: 0x0600AD53 RID: 44371 RVA: 0x00256964 File Offset: 0x00254B64
		// (set) Token: 0x0600AD54 RID: 44372 RVA: 0x00256991 File Offset: 0x00254B91
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
				this.OnColumnChanged();
			}
		}

		// Token: 0x170037FB RID: 14331
		// (get) Token: 0x0600AD55 RID: 44373 RVA: 0x002569AC File Offset: 0x00254BAC
		// (set) Token: 0x0600AD56 RID: 44374 RVA: 0x002569D5 File Offset: 0x00254BD5
		[Category("Appearance")]
		[DefaultValue(typeof(GridConfirmDialogType), "Classic")]
		[Description("The type of confirm dialog.")]
		[NotifyParentProperty(true)]
		public virtual GridConfirmDialogType ConfirmDialogType
		{
			get
			{
				object obj = base.ViewState["_cdt"];
				if (obj != null)
				{
					return (GridConfirmDialogType)obj;
				}
				return GridConfirmDialogType.Classic;
			}
			set
			{
				if (value < GridConfirmDialogType.Classic || value > GridConfirmDialogType.RadWindow)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["_cdt"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x170037FC RID: 14332
		// (get) Token: 0x0600AD57 RID: 44375 RVA: 0x00256A08 File Offset: 0x00254C08
		// (set) Token: 0x0600AD58 RID: 44376 RVA: 0x00256A35 File Offset: 0x00254C35
		[Description("Gets or sets the width of the Confirm Dialog (if it is a RadWindow)")]
		[DefaultValue(typeof(Unit), "")]
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
				this.OnColumnChanged();
			}
		}

		// Token: 0x170037FD RID: 14333
		// (get) Token: 0x0600AD59 RID: 44377 RVA: 0x00256A54 File Offset: 0x00254C54
		// (set) Token: 0x0600AD5A RID: 44378 RVA: 0x00256A81 File Offset: 0x00254C81
		[Description("Gets or sets the height of the Confirm Dialog (if it is a RadWindow)")]
		[DefaultValue(typeof(Unit), "")]
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
				this.OnColumnChanged();
			}
		}

		// Token: 0x170037FE RID: 14334
		// (get) Token: 0x0600AD5B RID: 44379 RVA: 0x00256AA0 File Offset: 0x00254CA0
		// (set) Token: 0x0600AD5C RID: 44380 RVA: 0x00256ACD File Offset: 0x00254CCD
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
				this.OnColumnChanged();
			}
		}

		// Token: 0x170037FF RID: 14335
		// (get) Token: 0x0600AD5D RID: 44381 RVA: 0x00256AE8 File Offset: 0x00254CE8
		// (set) Token: 0x0600AD5E RID: 44382 RVA: 0x00256B15 File Offset: 0x00254D15
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
				this.OnColumnChanged();
			}
		}

		// Token: 0x17003800 RID: 14336
		// (get) Token: 0x0600AD5F RID: 44383 RVA: 0x00256B30 File Offset: 0x00254D30
		// (set) Token: 0x0600AD60 RID: 44384 RVA: 0x00256B5D File Offset: 0x00254D5D
		[Description("The field bound to the text property of the button.")]
		[Category("Data")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
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
				this.OnColumnChanged();
			}
		}

		// Token: 0x17003801 RID: 14337
		// (get) Token: 0x0600AD61 RID: 44385 RVA: 0x00256B78 File Offset: 0x00254D78
		// (set) Token: 0x0600AD62 RID: 44386 RVA: 0x00256BA5 File Offset: 0x00254DA5
		[Description("The formatting applied to the value bound to the Text property.")]
		[Localizable(true)]
		[Category("Data")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
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
				this.OnColumnChanged();
			}
		}

		// Token: 0x17003802 RID: 14338
		// (get) Token: 0x0600AD63 RID: 44387 RVA: 0x00256BC0 File Offset: 0x00254DC0
		// (set) Token: 0x0600AD64 RID: 44388 RVA: 0x00256BED File Offset: 0x00254DED
		[Localizable(true)]
		[DefaultValue("")]
		[Description("The text used for the button.")]
		[Category("Appearance")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17003803 RID: 14339
		// (get) Token: 0x0600AD65 RID: 44389 RVA: 0x00256C08 File Offset: 0x00254E08
		// (set) Token: 0x0600AD66 RID: 44390 RVA: 0x00256C35 File Offset: 0x00254E35
		[DefaultValue("")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
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
				this.OnColumnChanged();
			}
		}

		// Token: 0x17003804 RID: 14340
		// (get) Token: 0x0600AD67 RID: 44391 RVA: 0x00256C50 File Offset: 0x00254E50
		// (set) Token: 0x0600AD68 RID: 44392 RVA: 0x00256C7D File Offset: 0x00254E7D
		[DefaultValue("")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
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
				this.OnColumnChanged();
			}
		}

		// Token: 0x17003805 RID: 14341
		// (get) Token: 0x0600AD69 RID: 44393 RVA: 0x00256C98 File Offset: 0x00254E98
		// (set) Token: 0x0600AD6A RID: 44394 RVA: 0x00256CC5 File Offset: 0x00254EC5
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Localizable(true)]
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
				this.OnColumnChanged();
			}
		}

		// Token: 0x17003806 RID: 14342
		// (get) Token: 0x0600AD6B RID: 44395 RVA: 0x00256CE0 File Offset: 0x00254EE0
		// (set) Token: 0x0600AD6C RID: 44396 RVA: 0x00256D0E File Offset: 0x00254F0E
		[Description("the DataFields from the data source, which will be applied to the formatting specified in the ConfirmTextFormatString property")]
		[TypeConverter(typeof(GridStringArrayConverter))]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[DefaultValue("")]
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
				this.OnColumnChanged();
			}
		}

		// Token: 0x17003807 RID: 14343
		// (get) Token: 0x0600AD6D RID: 44397 RVA: 0x00256D27 File Offset: 0x00254F27
		// (set) Token: 0x0600AD6E RID: 44398 RVA: 0x00256D2A File Offset: 0x00254F2A
		[DefaultValue(false)]
		[Browsable(false)]
		public override bool Groupable
		{
			get
			{
				return false;
			}
			set
			{
			}
		}

		// Token: 0x17003808 RID: 14344
		// (get) Token: 0x0600AD6F RID: 44399 RVA: 0x00256D2C File Offset: 0x00254F2C
		// (set) Token: 0x0600AD70 RID: 44400 RVA: 0x00256D34 File Offset: 0x00254F34
		[Browsable(false)]
		public override string GroupByExpression
		{
			get
			{
				return base.GroupByExpression;
			}
			set
			{
				base.GroupByExpression = value;
			}
		}

		// Token: 0x17003809 RID: 14345
		// (get) Token: 0x0600AD71 RID: 44401 RVA: 0x00256D3D File Offset: 0x00254F3D
		public override bool IsEditable
		{
			get
			{
				return this.ShowInEditForm;
			}
		}

		// Token: 0x1700380A RID: 14346
		// (get) Token: 0x0600AD72 RID: 44402 RVA: 0x00256D48 File Offset: 0x00254F48
		// (set) Token: 0x0600AD73 RID: 44403 RVA: 0x00256D76 File Offset: 0x00254F76
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public virtual bool ShowInEditForm
		{
			get
			{
				object obj = base.ViewState["_eem"];
				if (obj == null)
				{
					obj = false;
				}
				return (bool)obj;
			}
			set
			{
				base.ViewState["_eem"] = value;
			}
		}

		// Token: 0x1700380B RID: 14347
		// (get) Token: 0x0600AD74 RID: 44404 RVA: 0x00256D8E File Offset: 0x00254F8E
		// (set) Token: 0x0600AD75 RID: 44405 RVA: 0x00256D96 File Offset: 0x00254F96
		[Browsable(false)]
		public override GridKnownFunction CurrentFilterFunction
		{
			get
			{
				return base.CurrentFilterFunction;
			}
			set
			{
				base.CurrentFilterFunction = value;
			}
		}

		// Token: 0x1700380C RID: 14348
		// (get) Token: 0x0600AD76 RID: 44406 RVA: 0x00256D9F File Offset: 0x00254F9F
		// (set) Token: 0x0600AD77 RID: 44407 RVA: 0x00256DA7 File Offset: 0x00254FA7
		[Browsable(false)]
		public override GridKnownFunction AndCurrentFilterFunction
		{
			get
			{
				return base.AndCurrentFilterFunction;
			}
			set
			{
				base.AndCurrentFilterFunction = value;
			}
		}

		// Token: 0x1700380D RID: 14349
		// (get) Token: 0x0600AD78 RID: 44408 RVA: 0x00256DB0 File Offset: 0x00254FB0
		// (set) Token: 0x0600AD79 RID: 44409 RVA: 0x00256DB8 File Offset: 0x00254FB8
		[Browsable(false)]
		public override string CurrentFilterValue
		{
			get
			{
				return base.CurrentFilterValue;
			}
			set
			{
				base.CurrentFilterValue = value;
			}
		}

		// Token: 0x1700380E RID: 14350
		// (get) Token: 0x0600AD7A RID: 44410 RVA: 0x00256DC1 File Offset: 0x00254FC1
		// (set) Token: 0x0600AD7B RID: 44411 RVA: 0x00256DC9 File Offset: 0x00254FC9
		[Browsable(false)]
		public override string AndCurrentFilterValue
		{
			get
			{
				return base.AndCurrentFilterValue;
			}
			set
			{
				base.AndCurrentFilterValue = value;
			}
		}

		// Token: 0x1700380F RID: 14351
		// (get) Token: 0x0600AD7C RID: 44412 RVA: 0x00256DD2 File Offset: 0x00254FD2
		// (set) Token: 0x0600AD7D RID: 44413 RVA: 0x00256DDA File Offset: 0x00254FDA
		[Browsable(false)]
		public override bool AutoPostBackOnFilter
		{
			get
			{
				return base.AutoPostBackOnFilter;
			}
			set
			{
				base.AutoPostBackOnFilter = value;
			}
		}

		// Token: 0x17003810 RID: 14352
		// (get) Token: 0x0600AD7E RID: 44414 RVA: 0x00256DE3 File Offset: 0x00254FE3
		// (set) Token: 0x0600AD7F RID: 44415 RVA: 0x00256DEB File Offset: 0x00254FEB
		[Browsable(false)]
		public override string FilterControlAltText
		{
			get
			{
				return base.FilterControlAltText;
			}
			set
			{
				base.FilterControlAltText = value;
			}
		}

		// Token: 0x17003811 RID: 14353
		// (get) Token: 0x0600AD80 RID: 44416 RVA: 0x00256DF4 File Offset: 0x00254FF4
		// (set) Token: 0x0600AD81 RID: 44417 RVA: 0x00256DFC File Offset: 0x00254FFC
		[Browsable(false)]
		public override string FilterControlToolTip
		{
			get
			{
				return base.FilterControlToolTip;
			}
			set
			{
				base.FilterControlToolTip = value;
			}
		}

		// Token: 0x17003812 RID: 14354
		// (get) Token: 0x0600AD82 RID: 44418 RVA: 0x00256E05 File Offset: 0x00255005
		// (set) Token: 0x0600AD83 RID: 44419 RVA: 0x00256E0D File Offset: 0x0025500D
		[Browsable(false)]
		public override Unit FilterControlWidth
		{
			get
			{
				return base.FilterControlWidth;
			}
			set
			{
				base.FilterControlWidth = value;
			}
		}

		// Token: 0x17003813 RID: 14355
		// (get) Token: 0x0600AD84 RID: 44420 RVA: 0x00256E16 File Offset: 0x00255016
		// (set) Token: 0x0600AD85 RID: 44421 RVA: 0x00256E1E File Offset: 0x0025501E
		[Browsable(false)]
		public override int? FilterDelay
		{
			get
			{
				return base.FilterDelay;
			}
			set
			{
				base.FilterDelay = value;
			}
		}

		// Token: 0x17003814 RID: 14356
		// (get) Token: 0x0600AD86 RID: 44422 RVA: 0x00256E27 File Offset: 0x00255027
		// (set) Token: 0x0600AD87 RID: 44423 RVA: 0x00256E2F File Offset: 0x0025502F
		[Browsable(false)]
		public override string FilterImageToolTip
		{
			get
			{
				return base.FilterImageToolTip;
			}
			set
			{
				base.FilterImageToolTip = value;
			}
		}

		// Token: 0x17003815 RID: 14357
		// (get) Token: 0x0600AD88 RID: 44424 RVA: 0x00256E38 File Offset: 0x00255038
		// (set) Token: 0x0600AD89 RID: 44425 RVA: 0x00256E40 File Offset: 0x00255040
		[Browsable(false)]
		public override string FilterImageUrl
		{
			get
			{
				return base.FilterImageUrl;
			}
			set
			{
				base.FilterImageUrl = value;
			}
		}

		// Token: 0x17003816 RID: 14358
		// (get) Token: 0x0600AD8A RID: 44426 RVA: 0x00256E49 File Offset: 0x00255049
		// (set) Token: 0x0600AD8B RID: 44427 RVA: 0x00256E51 File Offset: 0x00255051
		[Browsable(false)]
		public override GridFilterListOptions FilterListOptions
		{
			get
			{
				return base.FilterListOptions;
			}
			set
			{
				base.FilterListOptions = value;
			}
		}

		// Token: 0x17003817 RID: 14359
		// (get) Token: 0x0600AD8C RID: 44428 RVA: 0x00256E5A File Offset: 0x0025505A
		// (set) Token: 0x0600AD8D RID: 44429 RVA: 0x00256E62 File Offset: 0x00255062
		[Browsable(false)]
		public override bool ShowFilterIcon
		{
			get
			{
				return base.ShowFilterIcon;
			}
			set
			{
				base.ShowFilterIcon = value;
			}
		}

		// Token: 0x17003818 RID: 14360
		// (get) Token: 0x0600AD8E RID: 44430 RVA: 0x00256E6B File Offset: 0x0025506B
		// (set) Token: 0x0600AD8F RID: 44431 RVA: 0x00256E73 File Offset: 0x00255073
		[Browsable(false)]
		public override bool ShowSortIcon
		{
			get
			{
				return base.ShowSortIcon;
			}
			set
			{
				base.ShowSortIcon = value;
			}
		}

		// Token: 0x17003819 RID: 14361
		// (get) Token: 0x0600AD90 RID: 44432 RVA: 0x00256E7C File Offset: 0x0025507C
		// (set) Token: 0x0600AD91 RID: 44433 RVA: 0x00256E84 File Offset: 0x00255084
		[Browsable(false)]
		public override string SortAscImageUrl
		{
			get
			{
				return base.SortAscImageUrl;
			}
			set
			{
				base.SortAscImageUrl = value;
			}
		}

		// Token: 0x1700381A RID: 14362
		// (get) Token: 0x0600AD92 RID: 44434 RVA: 0x00256E8D File Offset: 0x0025508D
		// (set) Token: 0x0600AD93 RID: 44435 RVA: 0x00256E95 File Offset: 0x00255095
		[Browsable(false)]
		public override string SortDescImageUrl
		{
			get
			{
				return base.SortDescImageUrl;
			}
			set
			{
				base.SortDescImageUrl = value;
			}
		}

		// Token: 0x1700381B RID: 14363
		// (get) Token: 0x0600AD94 RID: 44436 RVA: 0x00256E9E File Offset: 0x0025509E
		// (set) Token: 0x0600AD95 RID: 44437 RVA: 0x00256EA6 File Offset: 0x002550A6
		[Browsable(false)]
		public override Color SortedBackColor
		{
			get
			{
				return base.SortedBackColor;
			}
			set
			{
				base.SortedBackColor = value;
			}
		}

		// Token: 0x1700381C RID: 14364
		// (get) Token: 0x0600AD96 RID: 44438 RVA: 0x00256EAF File Offset: 0x002550AF
		// (set) Token: 0x0600AD97 RID: 44439 RVA: 0x00256EB7 File Offset: 0x002550B7
		[Browsable(false)]
		public override string SortExpression
		{
			get
			{
				return base.SortExpression;
			}
			set
			{
				base.SortExpression = value;
			}
		}

		// Token: 0x1700381D RID: 14365
		// (get) Token: 0x0600AD98 RID: 44440 RVA: 0x00256EC0 File Offset: 0x002550C0
		// (set) Token: 0x0600AD99 RID: 44441 RVA: 0x00256EC8 File Offset: 0x002550C8
		[Browsable(false)]
		public override GridHeaderButtonType HeaderButtonType
		{
			get
			{
				return base.HeaderButtonType;
			}
			set
			{
				base.HeaderButtonType = value;
			}
		}

		// Token: 0x0600AD9A RID: 44442 RVA: 0x00256ED4 File Offset: 0x002550D4
		public override GridColumn Clone()
		{
			GridButtonColumn gridButtonColumn = new GridButtonColumn();
			gridButtonColumn.CopyBaseProperties(this);
			return gridButtonColumn;
		}

		// Token: 0x0600AD9B RID: 44443 RVA: 0x00256EF0 File Offset: 0x002550F0
		protected override void CopyBaseProperties(GridColumn FromColumn)
		{
			GridButtonColumn gridButtonColumn = (GridButtonColumn)FromColumn;
			base.CopyBaseProperties(FromColumn);
			this.Text = gridButtonColumn.Text;
			this.DataTextFormatString = gridButtonColumn.DataTextFormatString;
			this.DataTextField = gridButtonColumn.DataTextField;
			this.CommandName = gridButtonColumn.CommandName;
			this.CommandArgument = gridButtonColumn.CommandArgument;
			this.ButtonType = gridButtonColumn.ButtonType;
			this.ButtonCssClass = gridButtonColumn.ButtonCssClass;
			this.ImageUrl = gridButtonColumn.ImageUrl;
			this.ConfirmText = gridButtonColumn.ConfirmText;
			this.ConfirmTitle = gridButtonColumn.ConfirmTitle;
			this.ConfirmTextFields = gridButtonColumn.ConfirmTextFields;
			this.ConfirmTextFormatString = gridButtonColumn.ConfirmTextFormatString;
			this.ShowInEditForm = gridButtonColumn.ShowInEditForm;
			this.ConfirmDialogType = gridButtonColumn.ConfirmDialogType;
			this.ConfirmDialogWidth = gridButtonColumn.ConfirmDialogWidth;
			this.ConfirmDialogHeight = gridButtonColumn.ConfirmDialogHeight;
		}

		// Token: 0x04002DEF RID: 11759
		private PropertyDescriptor textFieldDesc;

		// Token: 0x04002DF0 RID: 11760
		private PropertyDescriptorCollection confirmFieldsDesc = new PropertyDescriptorCollection(new PropertyDescriptor[0]);

		// Token: 0x020010A8 RID: 4264
		private class GridButtonColumnImageButton : ImageButton
		{
			// Token: 0x1700381E RID: 14366
			// (get) Token: 0x0600AD9F RID: 44447 RVA: 0x00256FD4 File Offset: 0x002551D4
			// (set) Token: 0x0600ADA0 RID: 44448 RVA: 0x00256FFD File Offset: 0x002551FD
			internal int ImageUrlSetHash
			{
				get
				{
					object obj = base.ViewState["_urlHash"];
					if (obj != null)
					{
						return (int)obj;
					}
					return 0;
				}
				set
				{
					base.ViewState["_urlHash"] = value;
				}
			}
		}
	}
}

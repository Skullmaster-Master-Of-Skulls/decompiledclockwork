using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x020018F9 RID: 6393
	public class GridAttachmentColumn : GridEditableColumn
	{
		// Token: 0x17004A1D RID: 18973
		// (get) Token: 0x0600F63B RID: 63035 RVA: 0x0037DD30 File Offset: 0x0037BF30
		// (set) Token: 0x0600F63C RID: 63036 RVA: 0x0037DD5D File Offset: 0x0037BF5D
		[NotifyParentProperty(true)]
		[Category("Data")]
		[DefaultValue("")]
		public string DataSourceID
		{
			get
			{
				object obj = base.ViewState["DataSourceID"];
				if (obj == null)
				{
					obj = "";
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["DataSourceID"] = value;
			}
		}

		// Token: 0x17004A1E RID: 18974
		// (get) Token: 0x0600F63D RID: 63037 RVA: 0x0037DD70 File Offset: 0x0037BF70
		// (set) Token: 0x0600F63E RID: 63038 RVA: 0x0037DDA8 File Offset: 0x0037BFA8
		[NotifyParentProperty(true)]
		[Description("Gets or sets an array of file extensions that are allowed for uploading.")]
		[DefaultValue("")]
		[TypeConverter(typeof(GridStringArrayConverter))]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[Category("Data")]
		public string[] AllowedFileExtensions
		{
			get
			{
				object obj = base.ViewState["AllowedFileExtensions"];
				if (obj != null)
				{
					return (string[])((string[])obj).Clone();
				}
				return new string[0];
			}
			set
			{
				if (value != null)
				{
					base.ViewState["AllowedFileExtensions"] = (string[])value.Clone();
					return;
				}
				base.ViewState["AllowedFileExtensions"] = null;
			}
		}

		// Token: 0x17004A1F RID: 18975
		// (get) Token: 0x0600F63F RID: 63039 RVA: 0x0037DDDC File Offset: 0x0037BFDC
		// (set) Token: 0x0600F640 RID: 63040 RVA: 0x0037DE05 File Offset: 0x0037C005
		[NotifyParentProperty(true)]
		[Category("Data")]
		[DefaultValue(0)]
		[Description("Gets or sets the maximum allowed size (in bytes) of the uploaded attachment.")]
		public int MaxFileSize
		{
			get
			{
				object obj = base.ViewState["MaxFileSize"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				base.ViewState["MaxFileSize"] = value;
			}
		}

		// Token: 0x17004A20 RID: 18976
		// (get) Token: 0x0600F641 RID: 63041 RVA: 0x0037DE20 File Offset: 0x0037C020
		// (set) Token: 0x0600F642 RID: 63042 RVA: 0x0037DE49 File Offset: 0x0037C049
		[Category("Appearance")]
		[Description("The type of button contained within the column.")]
		[DefaultValue(typeof(GridButtonColumnType), "LinkButton")]
		[NotifyParentProperty(true)]
		public virtual GridButtonColumnType ButtonType
		{
			get
			{
				object obj = base.ViewState["ButtonType"];
				if (obj != null)
				{
					return (GridButtonColumnType)obj;
				}
				return GridButtonColumnType.LinkButton;
			}
			set
			{
				if (value < GridButtonColumnType.LinkButton || value > GridButtonColumnType.FontIconButton)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				base.ViewState["ButtonType"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004A21 RID: 18977
		// (get) Token: 0x0600F643 RID: 63043 RVA: 0x0037DE7C File Offset: 0x0037C07C
		// (set) Token: 0x0600F644 RID: 63044 RVA: 0x0037DEA9 File Offset: 0x0037C0A9
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Category("Appearance")]
		public virtual string ButtonCssClass
		{
			get
			{
				object obj = base.ViewState["ButtonCssClass"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				base.ViewState["ButtonCssClass"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004A22 RID: 18978
		// (get) Token: 0x0600F645 RID: 63045 RVA: 0x0037DEC4 File Offset: 0x0037C0C4
		// (set) Token: 0x0600F646 RID: 63046 RVA: 0x0037DEF2 File Offset: 0x0037C0F2
		[Description("Gets or sets a string, representing a comma-separated enumeration of DataFields from the data source, that uniquely identify an attachment from the column's data source")]
		[SuppressMessage("Microsoft.Performance", "CA1819:PropertiesShouldNotReturnArrays")]
		[TypeConverter(typeof(GridStringArrayConverter))]
		[Category("Data")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string[] AttachmentKeyFields
		{
			get
			{
				object obj = base.ViewState["AttachmentKeyFields"];
				if (obj != null)
				{
					return (string[])obj;
				}
				return new string[0];
			}
			set
			{
				base.ViewState["AttachmentKeyFields"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004A23 RID: 18979
		// (get) Token: 0x0600F647 RID: 63047 RVA: 0x0037DF0C File Offset: 0x0037C10C
		// (set) Token: 0x0600F648 RID: 63048 RVA: 0x0037DF39 File Offset: 0x0037C139
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("Gets or sets the name of the data field from the column's data source where the binary attachment data is stored.")]
		[Category("Data")]
		public virtual string AttachmentDataField
		{
			get
			{
				object obj = base.ViewState["AttachmentDataField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["AttachmentDataField"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004A24 RID: 18980
		// (get) Token: 0x0600F649 RID: 63049 RVA: 0x0037DF54 File Offset: 0x0037C154
		// (set) Token: 0x0600F64A RID: 63050 RVA: 0x0037DF81 File Offset: 0x0037C181
		[NotifyParentProperty(true)]
		[Description("The field bound to the text property of the button.")]
		[DefaultValue("")]
		[Category("Data")]
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
				base.UpdateUniqueNameIfDefault(value);
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004A25 RID: 18981
		// (get) Token: 0x0600F64B RID: 63051 RVA: 0x0037DFA4 File Offset: 0x0037C1A4
		// (set) Token: 0x0600F64C RID: 63052 RVA: 0x0037E008 File Offset: 0x0037C208
		[NotifyParentProperty(true)]
		[Description("The formatting applied to the value bound to the Text property.")]
		[Localizable(true)]
		[DefaultValue("")]
		[Category("Data")]
		public virtual string DataTextFormatString
		{
			get
			{
				object obj = base.ViewState["DataTextFormatString"];
				if (obj == null)
				{
					return string.Empty;
				}
				if (base.Owner != null && base.Owner.OwnerGrid.ExportSettings.SuppressColumnDataFormatStrings && base.Owner.OwnerGrid.IsExporting)
				{
					return "{0}";
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["DataTextFormatString"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004A26 RID: 18982
		// (get) Token: 0x0600F64D RID: 63053 RVA: 0x0037E024 File Offset: 0x0037C224
		// (set) Token: 0x0600F64E RID: 63054 RVA: 0x0037E051 File Offset: 0x0037C251
		[Category("Appearance")]
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		[Localizable(true)]
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
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004A27 RID: 18983
		// (get) Token: 0x0600F64F RID: 63055 RVA: 0x0037E06C File Offset: 0x0037C26C
		// (set) Token: 0x0600F650 RID: 63056 RVA: 0x0037E099 File Offset: 0x0037C299
		[DefaultValue("")]
		[Description("Gets or sets the name of the field bound to the file name of the attachment.")]
		[Category("Data")]
		[NotifyParentProperty(true)]
		public virtual string FileNameTextField
		{
			get
			{
				object obj = base.ViewState["FileNameTextField"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["FileNameTextField"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004A28 RID: 18984
		// (get) Token: 0x0600F651 RID: 63057 RVA: 0x0037E0B4 File Offset: 0x0037C2B4
		// (set) Token: 0x0600F652 RID: 63058 RVA: 0x0037E0E1 File Offset: 0x0037C2E1
		[Localizable(true)]
		[DefaultValue("")]
		[Category("Data")]
		[Description("The formatting applied to the value bound to the FileNameTextField property.")]
		[NotifyParentProperty(true)]
		public virtual string FileNameTextFormatString
		{
			get
			{
				object obj = base.ViewState["FileNameTextFormatString"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				base.ViewState["FileNameTextFormatString"] = value;
				if (string.IsNullOrEmpty(this.DataTextField))
				{
					base.UpdateUniqueNameIfDefault(value);
				}
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004A29 RID: 18985
		// (get) Token: 0x0600F653 RID: 63059 RVA: 0x0037E110 File Offset: 0x0037C310
		// (set) Token: 0x0600F654 RID: 63060 RVA: 0x0037E13D File Offset: 0x0037C33D
		[DefaultValue("")]
		[Description("Gets or sets the file name of the attachment.")]
		[Category("Data")]
		[NotifyParentProperty(true)]
		public virtual string FileName
		{
			get
			{
				object obj = base.ViewState["FileName"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "attachment";
			}
			set
			{
				base.ViewState["FileName"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004A2A RID: 18986
		// (get) Token: 0x0600F655 RID: 63061 RVA: 0x0037E158 File Offset: 0x0037C358
		// (set) Token: 0x0600F656 RID: 63062 RVA: 0x0037E185 File Offset: 0x0037C385
		[DefaultValue("")]
		[Localizable(true)]
		[NotifyParentProperty(true)]
		public virtual string ImageUrl
		{
			get
			{
				object obj = base.ViewState["ImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				base.ViewState["ImageUrl"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17004A2B RID: 18987
		// (get) Token: 0x0600F657 RID: 63063 RVA: 0x0037E1A0 File Offset: 0x0037C3A0
		// (set) Token: 0x0600F658 RID: 63064 RVA: 0x0037E1C9 File Offset: 0x0037C3C9
		[NotifyParentProperty(true)]
		[DefaultValue(GridUploadControlType.RadUpload)]
		[Description("UploadControlType")]
		[Category("Behavior")]
		public virtual GridUploadControlType UploadControlType
		{
			get
			{
				object obj = base.ViewState["UploadControlType"];
				if (obj != null)
				{
					return (GridUploadControlType)obj;
				}
				return GridUploadControlType.RadUpload;
			}
			set
			{
				base.ViewState["UploadControlType"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x0600F659 RID: 63065 RVA: 0x0037E1E8 File Offset: 0x0037C3E8
		public override void InitializeCell(TableCell cell, int columnIndex, GridItem inItem)
		{
			GridGroupFooterItem gridGroupFooterItem = inItem as GridGroupFooterItem;
			if (gridGroupFooterItem != null && gridGroupFooterItem.OwnerTableView.GroupFooterTemplate != null)
			{
				return;
			}
			base.InitializeCell(cell, columnIndex, inItem);
			if (inItem.IsDataBound && !(inItem is GridHeaderItem) && !(inItem is GridFooterItem) && !(inItem is GridFilteringItem) && gridGroupFooterItem == null)
			{
				WebControl webControl = this.InitializeButtonInCell(inItem) as WebControl;
				inItem.CellDataBound += this.OnCellDataBound;
				if (!string.IsNullOrEmpty(this.ButtonCssClass.Trim()))
				{
					webControl.CssClass = this.ButtonCssClass;
				}
				if (inItem.IsInEditMode && !base.IsReadOnly(inItem))
				{
					this.CurrentColumnEditor.InitializeInControl(cell);
					return;
				}
				cell.Controls.Add(webControl);
			}
		}

		// Token: 0x0600F65A RID: 63066 RVA: 0x0037E2A4 File Offset: 0x0037C4A4
		private IButtonControl InitializeButtonInCell(GridItem inItem)
		{
			IButtonControl result;
			if (this.ButtonType == GridButtonColumnType.ImageButton)
			{
				ImageButton imageButton = new ImageButton();
				imageButton.ID = "gac_" + this.UniqueName;
				imageButton.AlternateText = this.Text;
				imageButton.ToolTip = this.Text;
				imageButton.CommandName = "DownloadAttachment";
				imageButton.CausesValidation = false;
				imageButton.ImageUrl = this.ImageUrl;
				imageButton.BorderWidth = Unit.Pixel(0);
				this.ConfigureButtonImage(imageButton);
				result = imageButton;
			}
			else if (this.ButtonType == GridButtonColumnType.LinkButton)
			{
				result = new GridLinkButton
				{
					ID = "gac_" + this.UniqueName,
					Text = this.Text,
					CommandName = "DownloadAttachment",
					CausesValidation = false
				};
			}
			else
			{
				result = new Button
				{
					ID = "gac_" + this.UniqueName,
					Text = this.Text,
					CommandName = "DownloadAttachment",
					CausesValidation = false
				};
			}
			return result;
		}

		// Token: 0x0600F65B RID: 63067 RVA: 0x0037E3AB File Offset: 0x0037C5AB
		protected virtual string ClientClickScript(string commandName, string commandArgument)
		{
			if (base.Owner.OwnerGrid.IsClientCommandAssigned)
			{
				return string.Format("if(!$find('{0}').fireCommand('{1}','{2}')) return false;", base.Owner.ClientID, commandName, commandArgument);
			}
			return string.Empty;
		}

		// Token: 0x0600F65C RID: 63068 RVA: 0x0037E3DC File Offset: 0x0037C5DC
		private void ConfigureButtonImage(ImageButton target)
		{
			if (!string.IsNullOrEmpty(target.ImageUrl))
			{
				return;
			}
			target.ImageUrl = base.Owner.OwnerGrid.ResolveGridImageUrl("Download.gif", false);
		}

		// Token: 0x0600F65D RID: 63069 RVA: 0x0037E408 File Offset: 0x0037C608
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		protected virtual void OnCellDataBound(object sender, GridCellDataBoundEventArgs args)
		{
			if (args.Column != this)
			{
				return;
			}
			GridItem gridItem = (GridItem)sender;
			object dataItem = gridItem.DataItem;
			if (dataItem == null)
			{
				return;
			}
			TableCell cell = args.Cell;
			if (cell == null || !gridItem.IsDataBound)
			{
				return;
			}
			if (!base.DesignMode && gridItem.IsInEditMode && !base.IsReadOnly(gridItem))
			{
				this.CurrentColumnEditor.InitializeFromControl(cell);
				return;
			}
			string text = this.Text;
			if (!string.IsNullOrEmpty(this.DataTextField))
			{
				string text2 = this.ExtractValueFromDataItem<string>(dataItem, this.DataTextField) ?? string.Empty;
				if (!string.IsNullOrEmpty(text2))
				{
					if (string.IsNullOrEmpty(this.DataTextFormatString) || (base.Owner != null && base.Owner.OwnerGrid.IsExporting && base.Owner.OwnerGrid.ExportSettings.SuppressColumnDataFormatStrings))
					{
						text = text2;
					}
					else
					{
						text = string.Format(this.DataTextFormatString, text2);
					}
				}
			}
			if (string.IsNullOrEmpty(text) && base.DesignMode)
			{
				text = "AttachmentColumn";
			}
			IButtonControl buttonInCell = this.GetButtonInCell(cell);
			buttonInCell.Text = text;
			buttonInCell.CommandArgument = this.BuildSerializedCommandArgument(dataItem);
			if (buttonInCell is ImageButton)
			{
				ImageButton imageButton = buttonInCell as ImageButton;
				imageButton.OnClientClick = this.ClientClickScript(imageButton.CommandName, (!string.IsNullOrEmpty(imageButton.CommandArgument)) ? imageButton.CommandArgument : gridItem.ItemIndexHierarchical);
				imageButton.ToolTip = text;
			}
			else if (buttonInCell is LinkButton)
			{
				LinkButton linkButton = buttonInCell as LinkButton;
				linkButton.OnClientClick = this.ClientClickScript(linkButton.CommandName, (!string.IsNullOrEmpty(linkButton.CommandArgument)) ? linkButton.CommandArgument : gridItem.ItemIndexHierarchical);
			}
			else
			{
				Button button = buttonInCell as Button;
				button.OnClientClick = this.ClientClickScript(button.CommandName, (!string.IsNullOrEmpty(button.CommandArgument)) ? button.CommandArgument : gridItem.ItemIndexHierarchical);
			}
			this.ApplyButtonVisibility(buttonInCell);
		}

		// Token: 0x0600F65E RID: 63070 RVA: 0x0037E5FC File Offset: 0x0037C7FC
		private void ApplyButtonVisibility(IButtonControl button)
		{
			if (string.IsNullOrEmpty(button.Text))
			{
				WebControl webControl = (WebControl)button;
				webControl.Visible = false;
				TableCell tableCell = (TableCell)webControl.Parent;
				if (tableCell.Controls.Count == 1)
				{
					tableCell.Controls.Add(new LiteralControl("&nbsp;"));
				}
			}
		}

		// Token: 0x0600F65F RID: 63071 RVA: 0x0037E654 File Offset: 0x0037C854
		private IButtonControl GetButtonInCell(TableCell cell)
		{
			if (this.ButtonType == GridButtonColumnType.ImageButton)
			{
				return (ImageButton)cell.Controls[0];
			}
			if (this.ButtonType == GridButtonColumnType.LinkButton)
			{
				return (LinkButton)cell.Controls[0];
			}
			return (Button)cell.Controls[0];
		}

		// Token: 0x0600F660 RID: 63072 RVA: 0x0037E6A8 File Offset: 0x0037C8A8
		private string BuildSerializedCommandArgument(object dataItem)
		{
			string fileName = this.GetFileName(dataItem);
			IDictionary dictionary = this.BuildAttachmentKeyValueCollection(dataItem);
			dictionary["ColumnUniqueName"] = this.UniqueName;
			dictionary["FileName"] = fileName;
			return GridAttachmentColumn.SerializeDownloadArgument(dictionary);
		}

		// Token: 0x0600F661 RID: 63073 RVA: 0x0037E6E8 File Offset: 0x0037C8E8
		private string GetFileName(object dataItem)
		{
			string text = this.FileName;
			if (!string.IsNullOrEmpty(this.FileNameTextField))
			{
				object obj = this.ExtractValueFromDataItem<object>(dataItem, this.FileNameTextField);
				if (obj != null)
				{
					text = obj.ToString();
					if (!string.IsNullOrEmpty(this.FileNameTextFormatString))
					{
						text = string.Format(this.FileNameTextFormatString, text);
					}
				}
			}
			return text.Replace(" ", "_");
		}

		// Token: 0x0600F662 RID: 63074 RVA: 0x0037E74C File Offset: 0x0037C94C
		internal static string SerializeDownloadArgument(IDictionary collection)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			return javaScriptSerializer.Serialize(collection);
		}

		// Token: 0x0600F663 RID: 63075 RVA: 0x0037E768 File Offset: 0x0037C968
		internal static IDictionary DeserializeDownloadArgument(string json)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			return javaScriptSerializer.Deserialize<IDictionary>(json);
		}

		// Token: 0x0600F664 RID: 63076 RVA: 0x0037E784 File Offset: 0x0037C984
		internal static GridAttachmentColumn GetFirstAttachmentColumn(GridTableView view)
		{
			foreach (object obj in view.Columns)
			{
				GridColumn gridColumn = (GridColumn)obj;
				if (gridColumn.ColumnType == typeof(GridAttachmentColumn).ToString())
				{
					return gridColumn as GridAttachmentColumn;
				}
			}
			return null;
		}

		// Token: 0x0600F665 RID: 63077 RVA: 0x0037E800 File Offset: 0x0037CA00
		private IDictionary BuildAttachmentKeyValueCollection(object dataItem)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (string text in this.AttachmentKeyFields)
			{
				object value = this.ExtractValueFromDataItem<object>(dataItem, text);
				dictionary.Add(text, value);
			}
			return dictionary;
		}

		// Token: 0x0600F666 RID: 63078 RVA: 0x0037E844 File Offset: 0x0037CA44
		public virtual void StreamDownloadAttachment(Control source, string fileName, IDictionary attachmentKeyValueCollection)
		{
			if (attachmentKeyValueCollection.Count == 0 || string.IsNullOrEmpty(this.DataSourceID))
			{
				return;
			}
			this.attachmentFileName = fileName;
			Control control = this.SetupDataSourceControl(source, attachmentKeyValueCollection);
			IDataSource dataSource = control as IDataSource;
			if (dataSource != null)
			{
				DataSourceSelectArguments arguments = new DataSourceSelectArguments();
				DataSourceViewSelectCallback callback = new DataSourceViewSelectCallback(this.OnDownloadSelected);
				dataSource.GetView("DefaultView").Select(arguments, callback);
				return;
			}
			throw new GridException("Cannot find DataSourceControl with ID '" + this.DataSourceID + "'");
		}

		// Token: 0x0600F667 RID: 63079 RVA: 0x0037E8C4 File Offset: 0x0037CAC4
		private Control SetupDataSourceControl(Control source, IDictionary attachmentKeyValueCollection)
		{
			Control control = DataSourceControlHelper.FindControl(source, this.DataSourceID);
			ParameterCollection parameterCollection = GridPropertyEvaluator.GetPropertyValue(control, "WhereParameters") as ParameterCollection;
			if (parameterCollection == null)
			{
				parameterCollection = (GridPropertyEvaluator.GetPropertyValue(control, "SelectParameters") as ParameterCollection);
				if (parameterCollection == null)
				{
					parameterCollection = (GridPropertyEvaluator.GetPropertyValue(control, "QueryParameters") as ParameterCollection);
				}
			}
			if (parameterCollection != null && parameterCollection.Count > 0)
			{
				foreach (object obj in parameterCollection)
				{
					Parameter parameter = (Parameter)obj;
					object obj2 = attachmentKeyValueCollection[parameter.Name.Replace("@", "")];
					if (obj2 != null)
					{
						parameter.DefaultValue = obj2.ToString();
					}
				}
			}
			return control;
		}

		// Token: 0x0600F668 RID: 63080 RVA: 0x0037E998 File Offset: 0x0037CB98
		private void OnDownloadSelected(IEnumerable data)
		{
			if (data == null)
			{
				return;
			}
			IEnumerator enumerator = data.GetEnumerator();
			if (enumerator.MoveNext())
			{
				object dataItem = enumerator.Current;
				byte[] array = this.ExtractValueFromDataItem<object>(dataItem, this.AttachmentDataField) as byte[];
				if (array != null && array.Length > 0)
				{
					this.CurrentHttpResponse.Clear();
					this.CurrentHttpResponse.ContentType = "application/octet-stream";
					this.CurrentHttpResponse.AddHeader("content-disposition", "attachment; filename=" + this.attachmentFileName);
					this.CurrentHttpResponse.BinaryWrite(array);
					this.CurrentHttpResponse.End();
				}
			}
		}

		// Token: 0x17004A2C RID: 18988
		// (get) Token: 0x0600F669 RID: 63081 RVA: 0x0037EA2D File Offset: 0x0037CC2D
		private HttpResponse CurrentHttpResponse
		{
			get
			{
				return HttpContext.Current.Response;
			}
		}

		// Token: 0x0600F66A RID: 63082 RVA: 0x0037EA3C File Offset: 0x0037CC3C
		private T ExtractValueFromDataItem<T>(object dataItem, string dataFieldName)
		{
			object obj = null;
			if (dataFieldName.IndexOf(".") > -1)
			{
				try
				{
					obj = DataBinder.GetPropertyValue(dataItem, dataFieldName);
					goto IL_44;
				}
				catch
				{
					try
					{
						obj = DataBinder.Eval(dataItem, dataFieldName);
					}
					catch
					{
						if (obj != null && !GridBaseDataList.IsBindableType(obj.GetType()))
						{
							obj = null;
						}
					}
					goto IL_44;
				}
			}
			obj = DataBinder.Eval(dataItem, dataFieldName);
			IL_44:
			if (obj == null)
			{
				PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(dataItem).Find(dataFieldName, true);
				if (propertyDescriptor != null)
				{
					obj = propertyDescriptor.GetValue(dataItem);
				}
			}
			if (obj == null || !(obj is T))
			{
				obj = default(T);
			}
			return (T)((object)obj);
		}

		// Token: 0x0600F66B RID: 63083 RVA: 0x0037EAE8 File Offset: 0x0037CCE8
		public override bool ShouldExtractValues(GridEditableItem item)
		{
			return false;
		}

		// Token: 0x0600F66C RID: 63084 RVA: 0x0037EAEB File Offset: 0x0037CCEB
		protected override IGridColumnEditor CreateDefaultColumnEditor()
		{
			return new GridAttachmentColumnEditor(this);
		}

		// Token: 0x0600F66D RID: 63085 RVA: 0x0037EAF3 File Offset: 0x0037CCF3
		protected override void ColumnEditorChange(IGridColumnEditor newValue)
		{
			if (!(newValue is GridAttachmentColumnEditor))
			{
				throw new GridColumnEditorException(this.ToString() + " accepts only editor of type: " + typeof(GridAttachmentColumnEditor).ToString());
			}
			base.ColumnEditorChange(newValue);
		}

		// Token: 0x0600F66E RID: 63086 RVA: 0x0037EB2C File Offset: 0x0037CD2C
		public override void FillValues(IDictionary newValues, GridEditableItem editableItem)
		{
			if (editableItem.IsInEditMode)
			{
				GridAttachmentColumnEditor gridAttachmentColumnEditor = (GridAttachmentColumnEditor)editableItem.EditManager.GetColumnEditor(this);
				if ((this.UploadControlType == GridUploadControlType.RadUpload && gridAttachmentColumnEditor.RadUploadControl.UploadedFiles.Count > 0) || (this.UploadControlType == GridUploadControlType.RadAsyncUpload && gridAttachmentColumnEditor.RadAsyncUploadControl.UploadedFiles.Count > 0))
				{
					newValues[this.DataTextField] = gridAttachmentColumnEditor.UploadedFileContent;
					return;
				}
				newValues[this.DataTextField] = null;
			}
		}

		// Token: 0x0600F66F RID: 63087 RVA: 0x0037EBAC File Offset: 0x0037CDAC
		public override GridColumn Clone()
		{
			GridAttachmentColumn gridAttachmentColumn = new GridAttachmentColumn();
			gridAttachmentColumn.CopyBaseProperties(this);
			return gridAttachmentColumn;
		}

		// Token: 0x0600F670 RID: 63088 RVA: 0x0037EBC8 File Offset: 0x0037CDC8
		protected override void CopyBaseProperties(GridColumn fromColumn)
		{
			base.CopyBaseProperties(fromColumn);
			GridAttachmentColumn gridAttachmentColumn = (GridAttachmentColumn)fromColumn;
			this.AttachmentKeyFields = gridAttachmentColumn.AttachmentKeyFields;
			this.AttachmentDataField = gridAttachmentColumn.AttachmentDataField;
			this.FileNameTextField = gridAttachmentColumn.FileNameTextField;
			this.FileNameTextFormatString = gridAttachmentColumn.FileNameTextFormatString;
			this.FileName = gridAttachmentColumn.FileName;
			this.DataSourceID = gridAttachmentColumn.DataSourceID;
			this.AllowedFileExtensions = gridAttachmentColumn.AllowedFileExtensions;
			this.MaxFileSize = gridAttachmentColumn.MaxFileSize;
			this.ButtonType = gridAttachmentColumn.ButtonType;
			this.ButtonCssClass = gridAttachmentColumn.ButtonCssClass;
			this.DataTextField = gridAttachmentColumn.DataTextField;
			this.DataTextFormatString = gridAttachmentColumn.DataTextFormatString;
			this.Text = gridAttachmentColumn.Text;
			this.ImageUrl = gridAttachmentColumn.ImageUrl;
			this.UploadControlType = gridAttachmentColumn.UploadControlType;
		}

		// Token: 0x17004A2D RID: 18989
		// (get) Token: 0x0600F671 RID: 63089 RVA: 0x0037EC97 File Offset: 0x0037CE97
		public override bool IsEditable
		{
			get
			{
				return !this.ReadOnly;
			}
		}

		// Token: 0x0600F672 RID: 63090 RVA: 0x0037ECA2 File Offset: 0x0037CEA2
		public override bool IsBoundToFieldName(string name)
		{
			if (string.IsNullOrEmpty(this.DataTextField))
			{
				return string.Compare(this.FileNameTextField, name, true) == 0;
			}
			return string.Compare(this.DataTextField, name, true) == 0;
		}

		// Token: 0x0600F673 RID: 63091 RVA: 0x0037ECD4 File Offset: 0x0037CED4
		protected override string GenerateUniqueName()
		{
			if (!string.IsNullOrEmpty(this.DataTextField))
			{
				return base.GenerateUniqueNameBase(this.DataTextField);
			}
			return base.GenerateUniqueNameBase(this.FileNameTextField);
		}

		// Token: 0x0600F674 RID: 63092 RVA: 0x0037ECFC File Offset: 0x0037CEFC
		public override bool SupportsFiltering()
		{
			return this.AllowFiltering;
		}

		// Token: 0x0600F675 RID: 63093 RVA: 0x0037ED04 File Offset: 0x0037CF04
		protected override string GetFilterDataField()
		{
			if (!string.IsNullOrEmpty(this.DataTextField))
			{
				return this.DataTextField;
			}
			return this.FileNameTextField;
		}

		// Token: 0x0400467E RID: 18046
		private string attachmentFileName;
	}
}

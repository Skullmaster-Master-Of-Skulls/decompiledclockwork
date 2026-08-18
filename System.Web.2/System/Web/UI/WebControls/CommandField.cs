using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200039A RID: 922
	public class CommandField : ButtonFieldBase
	{
		// Token: 0x17000C73 RID: 3187
		// (get) Token: 0x06002BFF RID: 11263 RVA: 0x0008F6CC File Offset: 0x0008D8CC
		// (set) Token: 0x06002C00 RID: 11264 RVA: 0x0008F6F9 File Offset: 0x0008D8F9
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebSysDescription("CommandField_CancelImageUrl")]
		[UrlProperty]
		public virtual string CancelImageUrl
		{
			get
			{
				object obj = base.ViewState["CancelImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (!object.Equals(value, base.ViewState["CancelImageUrl"]))
				{
					base.ViewState["CancelImageUrl"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000C74 RID: 3188
		// (get) Token: 0x06002C01 RID: 11265 RVA: 0x0008F72C File Offset: 0x0008D92C
		// (set) Token: 0x06002C02 RID: 11266 RVA: 0x0008F75E File Offset: 0x0008D95E
		[WebCategory("Appearance")]
		[Localizable(true)]
		[WebSysDefaultValue("CommandField_DefaultCancelCaption")]
		[WebSysDescription("CommandField_CancelText")]
		public virtual string CancelText
		{
			get
			{
				object obj = base.ViewState["CancelText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CommandField_DefaultCancelCaption");
			}
			set
			{
				if (!object.Equals(value, base.ViewState["CancelText"]))
				{
					base.ViewState["CancelText"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x06002C03 RID: 11267 RVA: 0x0008F790 File Offset: 0x0008D990
		// (set) Token: 0x06002C04 RID: 11268 RVA: 0x0008F7B9 File Offset: 0x0008D9B9
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		[WebSysDescription("ButtonFieldBase_CausesValidation")]
		public override bool CausesValidation
		{
			get
			{
				object obj = base.ViewState["CausesValidation"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.CausesValidation = value;
			}
		}

		// Token: 0x17000C76 RID: 3190
		// (get) Token: 0x06002C05 RID: 11269 RVA: 0x0008F7C4 File Offset: 0x0008D9C4
		// (set) Token: 0x06002C06 RID: 11270 RVA: 0x0008F7F1 File Offset: 0x0008D9F1
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebSysDescription("CommandField_DeleteImageUrl")]
		[UrlProperty]
		public virtual string DeleteImageUrl
		{
			get
			{
				object obj = base.ViewState["DeleteImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (!object.Equals(value, base.ViewState["DeleteImageUrl"]))
				{
					base.ViewState["DeleteImageUrl"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000C77 RID: 3191
		// (get) Token: 0x06002C07 RID: 11271 RVA: 0x0008F824 File Offset: 0x0008DA24
		// (set) Token: 0x06002C08 RID: 11272 RVA: 0x0008F856 File Offset: 0x0008DA56
		[WebCategory("Appearance")]
		[Localizable(true)]
		[WebSysDefaultValue("CommandField_DefaultDeleteCaption")]
		[WebSysDescription("CommandField_DeleteText")]
		public virtual string DeleteText
		{
			get
			{
				object obj = base.ViewState["DeleteText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CommandField_DefaultDeleteCaption");
			}
			set
			{
				if (!object.Equals(value, base.ViewState["DeleteText"]))
				{
					base.ViewState["DeleteText"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000C78 RID: 3192
		// (get) Token: 0x06002C09 RID: 11273 RVA: 0x0008F888 File Offset: 0x0008DA88
		// (set) Token: 0x06002C0A RID: 11274 RVA: 0x0008F8B5 File Offset: 0x0008DAB5
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebSysDescription("CommandField_EditImageUrl")]
		[UrlProperty]
		public virtual string EditImageUrl
		{
			get
			{
				object obj = base.ViewState["EditImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (!object.Equals(value, base.ViewState["EditImageUrl"]))
				{
					base.ViewState["EditImageUrl"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000C79 RID: 3193
		// (get) Token: 0x06002C0B RID: 11275 RVA: 0x0008F8E8 File Offset: 0x0008DAE8
		// (set) Token: 0x06002C0C RID: 11276 RVA: 0x0008F91A File Offset: 0x0008DB1A
		[WebCategory("Appearance")]
		[Localizable(true)]
		[WebSysDefaultValue("CommandField_DefaultEditCaption")]
		[WebSysDescription("CommandField_EditText")]
		public virtual string EditText
		{
			get
			{
				object obj = base.ViewState["EditText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CommandField_DefaultEditCaption");
			}
			set
			{
				if (!object.Equals(value, base.ViewState["EditText"]))
				{
					base.ViewState["EditText"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000C7A RID: 3194
		// (get) Token: 0x06002C0D RID: 11277 RVA: 0x0008F94C File Offset: 0x0008DB4C
		// (set) Token: 0x06002C0E RID: 11278 RVA: 0x0008F979 File Offset: 0x0008DB79
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebSysDescription("CommandField_InsertImageUrl")]
		[UrlProperty]
		public virtual string InsertImageUrl
		{
			get
			{
				object obj = base.ViewState["InsertImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (!object.Equals(value, base.ViewState["InsertImageUrl"]))
				{
					base.ViewState["InsertImageUrl"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000C7B RID: 3195
		// (get) Token: 0x06002C0F RID: 11279 RVA: 0x0008F9AC File Offset: 0x0008DBAC
		// (set) Token: 0x06002C10 RID: 11280 RVA: 0x0008F9DE File Offset: 0x0008DBDE
		[WebCategory("Appearance")]
		[Localizable(true)]
		[WebSysDefaultValue("CommandField_DefaultInsertCaption")]
		[WebSysDescription("CommandField_InsertText")]
		public virtual string InsertText
		{
			get
			{
				object obj = base.ViewState["InsertText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CommandField_DefaultInsertCaption");
			}
			set
			{
				if (!object.Equals(value, base.ViewState["InsertText"]))
				{
					base.ViewState["InsertText"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000C7C RID: 3196
		// (get) Token: 0x06002C11 RID: 11281 RVA: 0x0008FA10 File Offset: 0x0008DC10
		// (set) Token: 0x06002C12 RID: 11282 RVA: 0x0008FA3D File Offset: 0x0008DC3D
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebSysDescription("CommandField_NewImageUrl")]
		[UrlProperty]
		public virtual string NewImageUrl
		{
			get
			{
				object obj = base.ViewState["NewImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (!object.Equals(value, base.ViewState["NewImageUrl"]))
				{
					base.ViewState["NewImageUrl"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000C7D RID: 3197
		// (get) Token: 0x06002C13 RID: 11283 RVA: 0x0008FA70 File Offset: 0x0008DC70
		// (set) Token: 0x06002C14 RID: 11284 RVA: 0x0008FAA2 File Offset: 0x0008DCA2
		[WebCategory("Appearance")]
		[Localizable(true)]
		[WebSysDefaultValue("CommandField_DefaultNewCaption")]
		[WebSysDescription("CommandField_NewText")]
		public virtual string NewText
		{
			get
			{
				object obj = base.ViewState["NewText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CommandField_DefaultNewCaption");
			}
			set
			{
				if (!object.Equals(value, base.ViewState["NewText"]))
				{
					base.ViewState["NewText"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x06002C15 RID: 11285 RVA: 0x0008FAD4 File Offset: 0x0008DCD4
		// (set) Token: 0x06002C16 RID: 11286 RVA: 0x0008FB01 File Offset: 0x0008DD01
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebSysDescription("CommandField_SelectImageUrl")]
		[UrlProperty]
		public virtual string SelectImageUrl
		{
			get
			{
				object obj = base.ViewState["SelectImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (!object.Equals(value, base.ViewState["SelectImageUrl"]))
				{
					base.ViewState["SelectImageUrl"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x06002C17 RID: 11287 RVA: 0x0008FB34 File Offset: 0x0008DD34
		// (set) Token: 0x06002C18 RID: 11288 RVA: 0x0008FB66 File Offset: 0x0008DD66
		[WebCategory("Appearance")]
		[Localizable(true)]
		[WebSysDefaultValue("CommandField_DefaultSelectCaption")]
		[WebSysDescription("CommandField_SelectText")]
		public virtual string SelectText
		{
			get
			{
				object obj = base.ViewState["SelectText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CommandField_DefaultSelectCaption");
			}
			set
			{
				if (!object.Equals(value, base.ViewState["SelectText"]))
				{
					base.ViewState["SelectText"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x06002C19 RID: 11289 RVA: 0x0008FB98 File Offset: 0x0008DD98
		// (set) Token: 0x06002C1A RID: 11290 RVA: 0x0008FBC4 File Offset: 0x0008DDC4
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		[WebSysDescription("CommandField_ShowCancelButton")]
		public virtual bool ShowCancelButton
		{
			get
			{
				object obj = base.ViewState["ShowCancelButton"];
				return obj == null || (bool)obj;
			}
			set
			{
				object obj = base.ViewState["ShowCancelButton"];
				if (obj == null || (bool)obj != value)
				{
					base.ViewState["ShowCancelButton"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000C81 RID: 3201
		// (get) Token: 0x06002C1B RID: 11291 RVA: 0x0008FC0C File Offset: 0x0008DE0C
		// (set) Token: 0x06002C1C RID: 11292 RVA: 0x0008FC38 File Offset: 0x0008DE38
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		[WebSysDescription("CommandField_ShowDeleteButton")]
		public virtual bool ShowDeleteButton
		{
			get
			{
				object obj = base.ViewState["ShowDeleteButton"];
				return obj != null && (bool)obj;
			}
			set
			{
				object obj = base.ViewState["ShowDeleteButton"];
				if (obj == null || (bool)obj != value)
				{
					base.ViewState["ShowDeleteButton"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000C82 RID: 3202
		// (get) Token: 0x06002C1D RID: 11293 RVA: 0x0008FC80 File Offset: 0x0008DE80
		// (set) Token: 0x06002C1E RID: 11294 RVA: 0x0008FCAC File Offset: 0x0008DEAC
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		[WebSysDescription("CommandField_ShowEditButton")]
		public virtual bool ShowEditButton
		{
			get
			{
				object obj = base.ViewState["ShowEditButton"];
				return obj != null && (bool)obj;
			}
			set
			{
				object obj = base.ViewState["ShowEditButton"];
				if (obj == null || (bool)obj != value)
				{
					base.ViewState["ShowEditButton"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000C83 RID: 3203
		// (get) Token: 0x06002C1F RID: 11295 RVA: 0x0008FCF4 File Offset: 0x0008DEF4
		// (set) Token: 0x06002C20 RID: 11296 RVA: 0x0008FD20 File Offset: 0x0008DF20
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		[WebSysDescription("CommandField_ShowSelectButton")]
		public virtual bool ShowSelectButton
		{
			get
			{
				object obj = base.ViewState["ShowSelectButton"];
				return obj != null && (bool)obj;
			}
			set
			{
				object obj = base.ViewState["ShowSelectButton"];
				if (obj == null || (bool)obj != value)
				{
					base.ViewState["ShowSelectButton"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000C84 RID: 3204
		// (get) Token: 0x06002C21 RID: 11297 RVA: 0x0008FD68 File Offset: 0x0008DF68
		// (set) Token: 0x06002C22 RID: 11298 RVA: 0x0008FD94 File Offset: 0x0008DF94
		[WebCategory("Behavior")]
		[DefaultValue(false)]
		[WebSysDescription("CommandField_ShowInsertButton")]
		public virtual bool ShowInsertButton
		{
			get
			{
				object obj = base.ViewState["ShowInsertButton"];
				return obj != null && (bool)obj;
			}
			set
			{
				object obj = base.ViewState["ShowInsertButton"];
				if (obj == null || (bool)obj != value)
				{
					base.ViewState["ShowInsertButton"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000C85 RID: 3205
		// (get) Token: 0x06002C23 RID: 11299 RVA: 0x0008FDDC File Offset: 0x0008DFDC
		// (set) Token: 0x06002C24 RID: 11300 RVA: 0x0008FE09 File Offset: 0x0008E009
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[WebSysDescription("CommandField_UpdateImageUrl")]
		[UrlProperty]
		public virtual string UpdateImageUrl
		{
			get
			{
				object obj = base.ViewState["UpdateImageUrl"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				if (!object.Equals(value, base.ViewState["UpdateImageUrl"]))
				{
					base.ViewState["UpdateImageUrl"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x17000C86 RID: 3206
		// (get) Token: 0x06002C25 RID: 11301 RVA: 0x0008FE3C File Offset: 0x0008E03C
		// (set) Token: 0x06002C26 RID: 11302 RVA: 0x0008FE6E File Offset: 0x0008E06E
		[WebCategory("Appearance")]
		[Localizable(true)]
		[WebSysDefaultValue("CommandField_DefaultUpdateCaption")]
		[WebSysDescription("CommandField_UpdateText")]
		public virtual string UpdateText
		{
			get
			{
				object obj = base.ViewState["UpdateText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return SR.GetString("CommandField_DefaultUpdateCaption");
			}
			set
			{
				if (!object.Equals(value, base.ViewState["UpdateText"]))
				{
					base.ViewState["UpdateText"] = value;
					this.OnFieldChanged();
				}
			}
		}

		// Token: 0x06002C27 RID: 11303 RVA: 0x0008FEA0 File Offset: 0x0008E0A0
		private void AddButtonToCell(DataControlFieldCell cell, string commandName, string buttonText, bool causesValidation, string validationGroup, int rowIndex, string imageUrl)
		{
			IPostBackContainer postBackContainer = base.Control as IPostBackContainer;
			bool flag = true;
			IButtonControl buttonControl;
			switch (this.ButtonType)
			{
			case ButtonType.Button:
				if (postBackContainer != null && !causesValidation)
				{
					buttonControl = new DataControlButton(postBackContainer);
					flag = false;
					goto IL_83;
				}
				buttonControl = new Button();
				goto IL_83;
			case ButtonType.Link:
				if (postBackContainer != null && !causesValidation)
				{
					buttonControl = new DataControlLinkButton(postBackContainer);
					flag = false;
					goto IL_83;
				}
				buttonControl = new DataControlLinkButton(null);
				goto IL_83;
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
			((ImageButton)buttonControl).ImageUrl = imageUrl;
			IL_83:
			buttonControl.Text = buttonText;
			buttonControl.CommandName = commandName;
			buttonControl.CommandArgument = rowIndex.ToString(CultureInfo.InvariantCulture);
			if (flag)
			{
				buttonControl.CausesValidation = causesValidation;
			}
			buttonControl.ValidationGroup = validationGroup;
			cell.Controls.Add((WebControl)buttonControl);
		}

		// Token: 0x06002C28 RID: 11304 RVA: 0x0008FF74 File Offset: 0x0008E174
		protected override void CopyProperties(DataControlField newField)
		{
			((CommandField)newField).CancelImageUrl = this.CancelImageUrl;
			((CommandField)newField).CancelText = this.CancelText;
			((CommandField)newField).DeleteImageUrl = this.DeleteImageUrl;
			((CommandField)newField).DeleteText = this.DeleteText;
			((CommandField)newField).EditImageUrl = this.EditImageUrl;
			((CommandField)newField).EditText = this.EditText;
			((CommandField)newField).InsertImageUrl = this.InsertImageUrl;
			((CommandField)newField).InsertText = this.InsertText;
			((CommandField)newField).NewImageUrl = this.NewImageUrl;
			((CommandField)newField).NewText = this.NewText;
			((CommandField)newField).SelectImageUrl = this.SelectImageUrl;
			((CommandField)newField).SelectText = this.SelectText;
			((CommandField)newField).UpdateImageUrl = this.UpdateImageUrl;
			((CommandField)newField).UpdateText = this.UpdateText;
			((CommandField)newField).ShowCancelButton = this.ShowCancelButton;
			((CommandField)newField).ShowDeleteButton = this.ShowDeleteButton;
			((CommandField)newField).ShowEditButton = this.ShowEditButton;
			((CommandField)newField).ShowSelectButton = this.ShowSelectButton;
			((CommandField)newField).ShowInsertButton = this.ShowInsertButton;
			base.CopyProperties(newField);
		}

		// Token: 0x06002C29 RID: 11305 RVA: 0x000900CB File Offset: 0x0008E2CB
		protected override DataControlField CreateField()
		{
			return new CommandField();
		}

		// Token: 0x06002C2A RID: 11306 RVA: 0x000900D4 File Offset: 0x0008E2D4
		public override void InitializeCell(DataControlFieldCell cell, DataControlCellType cellType, DataControlRowState rowState, int rowIndex)
		{
			base.InitializeCell(cell, cellType, rowState, rowIndex);
			bool showEditButton = this.ShowEditButton;
			bool showDeleteButton = this.ShowDeleteButton;
			bool showInsertButton = this.ShowInsertButton;
			bool showSelectButton = this.ShowSelectButton;
			bool showCancelButton = this.ShowCancelButton;
			bool flag = true;
			bool causesValidation = this.CausesValidation;
			string validationGroup = this.ValidationGroup;
			if (cellType == DataControlCellType.DataCell)
			{
				if ((rowState & (DataControlRowState.Edit | DataControlRowState.Insert)) != DataControlRowState.Normal)
				{
					if ((rowState & DataControlRowState.Edit) > DataControlRowState.Normal && showEditButton)
					{
						this.AddButtonToCell(cell, "Update", this.UpdateText, causesValidation, validationGroup, rowIndex, this.UpdateImageUrl);
						if (showCancelButton)
						{
							LiteralControl child = new LiteralControl("&nbsp;");
							cell.Controls.Add(child);
							this.AddButtonToCell(cell, "Cancel", this.CancelText, false, string.Empty, rowIndex, this.CancelImageUrl);
						}
					}
					if ((rowState & DataControlRowState.Insert) > DataControlRowState.Normal && showInsertButton)
					{
						this.AddButtonToCell(cell, "Insert", this.InsertText, causesValidation, validationGroup, rowIndex, this.InsertImageUrl);
						if (showCancelButton)
						{
							LiteralControl child = new LiteralControl("&nbsp;");
							cell.Controls.Add(child);
							this.AddButtonToCell(cell, "Cancel", this.CancelText, false, string.Empty, rowIndex, this.CancelImageUrl);
							return;
						}
					}
				}
				else
				{
					if (showEditButton)
					{
						this.AddButtonToCell(cell, "Edit", this.EditText, false, string.Empty, rowIndex, this.EditImageUrl);
						flag = false;
					}
					if (showDeleteButton)
					{
						if (!flag)
						{
							LiteralControl child = new LiteralControl("&nbsp;");
							cell.Controls.Add(child);
						}
						this.AddButtonToCell(cell, "Delete", this.DeleteText, false, string.Empty, rowIndex, this.DeleteImageUrl);
						flag = false;
					}
					if (showInsertButton)
					{
						if (!flag)
						{
							LiteralControl child = new LiteralControl("&nbsp;");
							cell.Controls.Add(child);
						}
						this.AddButtonToCell(cell, "New", this.NewText, false, string.Empty, rowIndex, this.NewImageUrl);
						flag = false;
					}
					if (showSelectButton)
					{
						if (!flag)
						{
							LiteralControl child = new LiteralControl("&nbsp;");
							cell.Controls.Add(child);
						}
						this.AddButtonToCell(cell, "Select", this.SelectText, false, string.Empty, rowIndex, this.SelectImageUrl);
					}
				}
			}
		}

		// Token: 0x06002C2B RID: 11307 RVA: 0x000902F3 File Offset: 0x0008E4F3
		public override void ValidateSupportsCallback()
		{
			if (this.ShowSelectButton)
			{
				throw new NotSupportedException(SR.GetString("CommandField_CallbacksNotSupported", new object[]
				{
					base.Control.ID
				}));
			}
		}
	}
}

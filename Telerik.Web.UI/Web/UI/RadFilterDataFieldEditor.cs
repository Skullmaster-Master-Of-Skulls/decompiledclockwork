using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x0200046D RID: 1133
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[SuppressMessage("Microsoft.Design", "CA1001:TypesThatOwnDisposableFieldsShouldBeDisposable")]
	public abstract class RadFilterDataFieldEditor : StateManager
	{
		// Token: 0x17000D31 RID: 3377
		// (get) Token: 0x06002899 RID: 10393 RVA: 0x00083758 File Offset: 0x00081958
		// (set) Token: 0x0600289A RID: 10394 RVA: 0x00083785 File Offset: 0x00081985
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public virtual string FieldName
		{
			get
			{
				object obj = base.ViewState["FieldName"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["FieldName"] = value;
			}
		}

		// Token: 0x17000D32 RID: 3378
		// (get) Token: 0x0600289B RID: 10395 RVA: 0x00083798 File Offset: 0x00081998
		// (set) Token: 0x0600289C RID: 10396 RVA: 0x000837C5 File Offset: 0x000819C5
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		public virtual string DisplayName
		{
			get
			{
				object obj = base.ViewState["DisplayName"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["DisplayName"] = value;
			}
		}

		// Token: 0x17000D33 RID: 3379
		// (get) Token: 0x0600289D RID: 10397 RVA: 0x000837D8 File Offset: 0x000819D8
		// (set) Token: 0x0600289E RID: 10398 RVA: 0x00083805 File Offset: 0x00081A05
		[DefaultValue("")]
		[NotifyParentProperty(true)]
		public virtual string ToolTip
		{
			get
			{
				object obj = base.ViewState["ToolTip"];
				if (obj == null)
				{
					return string.Empty;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["ToolTip"] = value;
			}
		}

		// Token: 0x17000D34 RID: 3380
		// (get) Token: 0x0600289F RID: 10399 RVA: 0x00083818 File Offset: 0x00081A18
		// (set) Token: 0x060028A0 RID: 10400 RVA: 0x00083845 File Offset: 0x00081A45
		[NotifyParentProperty(true)]
		[DefaultValue("'{0}'")]
		public virtual string PreviewDataFormat
		{
			get
			{
				object obj = base.ViewState["PreviewDataFormat"];
				if (obj == null)
				{
					return "'{0}'";
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["PreviewDataFormat"] = value;
			}
		}

		// Token: 0x17000D35 RID: 3381
		// (get) Token: 0x060028A1 RID: 10401 RVA: 0x00083858 File Offset: 0x00081A58
		// (set) Token: 0x060028A2 RID: 10402 RVA: 0x0008388A File Offset: 0x00081A8A
		[TypeConverter(typeof(GridDataTypeConverter))]
		[DefaultValue(typeof(string))]
		[NotifyParentProperty(true)]
		public virtual Type DataType
		{
			get
			{
				object obj = base.ViewState["DataType"];
				if (obj == null)
				{
					return typeof(string);
				}
				return (Type)obj;
			}
			set
			{
				base.ViewState["DataType"] = value;
			}
		}

		// Token: 0x17000D36 RID: 3382
		// (get) Token: 0x060028A3 RID: 10403 RVA: 0x000838A0 File Offset: 0x00081AA0
		// (set) Token: 0x060028A4 RID: 10404 RVA: 0x000838CE File Offset: 0x00081ACE
		[NotifyParentProperty(true)]
		[Description("Gets or sets the default filter function that will be set to the editor item when it is first created.")]
		[DefaultValue(RadFilterFunction.EqualTo)]
		public virtual RadFilterFunction DefaultFilterFunction
		{
			get
			{
				object obj = base.ViewState["DefaultFilterFunction"] ?? RadFilterFunction.EqualTo;
				return (RadFilterFunction)obj;
			}
			set
			{
				base.ViewState["DefaultFilterFunction"] = value;
			}
		}

		// Token: 0x060028A5 RID: 10405
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public abstract void InitializeEditor(Control container);

		// Token: 0x060028A6 RID: 10406
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public abstract ArrayList ExtractValues();

		// Token: 0x060028A7 RID: 10407
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public abstract void SetEditorValues(ArrayList values);

		// Token: 0x060028A8 RID: 10408 RVA: 0x000838E8 File Offset: 0x00081AE8
		internal static RadFilterDataFieldEditor CreateEditorFromTypeName(string typeInfo, RadFilter ownerFilter)
		{
			RadFilterDataFieldEditor radFilterDataFieldEditor = null;
			if (typeInfo.StartsWith("RadFilterTextFieldEditor"))
			{
				radFilterDataFieldEditor = new RadFilterTextFieldEditor();
			}
			else if (typeInfo.StartsWith("RadFilterNumericFieldEditor"))
			{
				radFilterDataFieldEditor = new RadFilterNumericFieldEditor();
			}
			else if (typeInfo.StartsWith("RadFilterBooleanFieldEditor"))
			{
				radFilterDataFieldEditor = new RadFilterBooleanFieldEditor();
			}
			else if (typeInfo.StartsWith("RadFilterDateFieldEditor"))
			{
				radFilterDataFieldEditor = new RadFilterDateFieldEditor();
			}
			else if (typeInfo.StartsWith("RadFilterDropDownEditor"))
			{
				radFilterDataFieldEditor = new RadFilterDropDownEditor();
			}
			else if (typeInfo.StartsWith("RadFilterMaskedTextBoxEditor"))
			{
				radFilterDataFieldEditor = new RadFilterMaskedTextBoxEditor();
			}
			if (radFilterDataFieldEditor == null)
			{
				RadFilterFieldEditorCreatingEventArgs radFilterFieldEditorCreatingEventArgs = new RadFilterFieldEditorCreatingEventArgs(null, typeInfo);
				ownerFilter.CallOnFieldEditorCreating(radFilterFieldEditorCreatingEventArgs);
				radFilterDataFieldEditor = radFilterFieldEditorCreatingEventArgs.Editor;
			}
			return radFilterDataFieldEditor;
		}

		// Token: 0x060028A9 RID: 10409 RVA: 0x00083990 File Offset: 0x00081B90
		internal static RadFilterDataFieldEditor CreateEditorFrom(RadFilterDataFieldEditor baseEditor)
		{
			RadFilterDataFieldEditor radFilterDataFieldEditor = RadFilterDataFieldEditor.CreateEditorFromTypeName(baseEditor.GetType().Name, baseEditor.Owner);
			radFilterDataFieldEditor.CopySettings(baseEditor);
			return radFilterDataFieldEditor;
		}

		// Token: 0x060028AA RID: 10410 RVA: 0x000839BC File Offset: 0x00081BBC
		protected virtual void CopySettings(RadFilterDataFieldEditor baseEditor)
		{
			this.SetOwner(baseEditor.Owner);
			this.FieldName = baseEditor.FieldName;
			this.DataType = baseEditor.DataType;
			this.DisplayName = baseEditor.DisplayName;
			this.ToolTip = baseEditor.ToolTip;
			this.DefaultFilterFunction = baseEditor.DefaultFilterFunction;
		}

		// Token: 0x060028AB RID: 10411 RVA: 0x00083A14 File Offset: 0x00081C14
		protected void AddBetweenDelimeterControl(Control container)
		{
			if (this.Owner.IsClientOperationMode)
			{
				this.betweenDelimeterControl = new Label
				{
					Text = string.Format(" {0} ", this.BetweenDelimeterText),
					CssClass = "rfDelimiter"
				};
			}
			else
			{
				this.betweenDelimeterControl = new LiteralControl(string.Format(" {0} ", this.BetweenDelimeterText));
			}
			container.Controls.Add(this.betweenDelimeterControl);
		}

		// Token: 0x060028AC RID: 10412 RVA: 0x00083A8A File Offset: 0x00081C8A
		internal Control GetBetweenDelimeterControl()
		{
			return this.betweenDelimeterControl;
		}

		// Token: 0x060028AD RID: 10413 RVA: 0x00083A92 File Offset: 0x00081C92
		internal virtual WebControl GetFirstInputControl(Control container)
		{
			return null;
		}

		// Token: 0x060028AE RID: 10414 RVA: 0x00083A95 File Offset: 0x00081C95
		internal virtual WebControl GetSecondInputControl(Control container)
		{
			return null;
		}

		// Token: 0x060028AF RID: 10415 RVA: 0x00083A98 File Offset: 0x00081C98
		internal string RetrieveDisplayText()
		{
			if (!string.IsNullOrEmpty(this.DisplayName))
			{
				return this.DisplayName;
			}
			return this.FieldName;
		}

		// Token: 0x17000D37 RID: 3383
		// (get) Token: 0x060028B0 RID: 10416 RVA: 0x00083AB4 File Offset: 0x00081CB4
		// (set) Token: 0x060028B1 RID: 10417 RVA: 0x00083ABC File Offset: 0x00081CBC
		protected internal bool IsSingleValue { get; set; }

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x060028B2 RID: 10418 RVA: 0x00083AC5 File Offset: 0x00081CC5
		// (set) Token: 0x060028B3 RID: 10419 RVA: 0x00083ACD File Offset: 0x00081CCD
		protected internal string BetweenDelimeterText { get; set; }

		// Token: 0x060028B4 RID: 10420 RVA: 0x00083AD6 File Offset: 0x00081CD6
		internal void SetOwner(RadFilter owner)
		{
			this._owner = owner;
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x060028B5 RID: 10421 RVA: 0x00083ADF File Offset: 0x00081CDF
		protected virtual string FilterOnBlurClientScript
		{
			get
			{
				return string.Format("Telerik.Web.UI.RadFilter.HandleFilterOnBlur('{0}',event)", this.Owner.ClientID);
			}
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x060028B6 RID: 10422 RVA: 0x00083AF6 File Offset: 0x00081CF6
		public RadFilter Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x04000A4F RID: 2639
		protected Control betweenDelimeterControl;

		// Token: 0x04000A50 RID: 2640
		private RadFilter _owner;
	}
}

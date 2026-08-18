using System;
using System.Collections;
using System.ComponentModel;
using System.Configuration;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020004BF RID: 1215
	public abstract class GridEditableColumn : GridColumn, IGridEditableColumn, IGridDataColumn
	{
		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x06002BE7 RID: 11239 RVA: 0x0008FD74 File Offset: 0x0008DF74
		// (set) Token: 0x06002BE8 RID: 11240 RVA: 0x0008FDA1 File Offset: 0x0008DFA1
		[DefaultValue("")]
		[Description("Select the column editor ID that will be used when the column is displayed in edit mode")]
		[NotifyParentProperty(true)]
		public string ColumnEditorID
		{
			get
			{
				object obj = base.ViewState["_ceID"];
				if (obj == null)
				{
					obj = string.Empty;
				}
				return (string)obj;
			}
			set
			{
				base.ViewState["_ceID"] = value;
			}
		}

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x06002BE9 RID: 11241 RVA: 0x0008FDB4 File Offset: 0x0008DFB4
		// (set) Token: 0x06002BEA RID: 11242 RVA: 0x0008FDBC File Offset: 0x0008DFBC
		public IGridColumnEditor MobileEditor { get; set; }

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x06002BEB RID: 11243 RVA: 0x0008FDC8 File Offset: 0x0008DFC8
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual IGridColumnEditor CurrentColumnEditor
		{
			get
			{
				if (this.InnerColumnEditor == null)
				{
					this.SetDefaultColumnEditor(this.CreateDefaultColumnEditor());
					if (this.ColumnEditorID != null && !string.IsNullOrEmpty(this.ColumnEditorID))
					{
						Control control = base.Owner.OwnerGrid.NamingContainer.FindControl(this.ColumnEditorID);
						if (control != null)
						{
							if (control is GridColumnEditorBase)
							{
								((IGridColumnEditor)control).SetOwner(this);
								((GridColumnEditorBase)this.InnerColumnEditor).CopySettingsFrom((IGridColumnEditor)control);
								this.ColumnEditor.SetOwner(this);
							}
							else
							{
								((IGridColumnEditor)control).SetOwner(this);
								this.SetDefaultColumnEditor((IGridColumnEditor)control);
							}
						}
					}
					if (this.InnerColumnEditor == null)
					{
						this.SetDefaultColumnEditor(this.CreateDefaultColumnEditor());
					}
				}
				return this.InnerColumnEditor;
			}
		}

		// Token: 0x06002BEC RID: 11244 RVA: 0x0008FE8A File Offset: 0x0008E08A
		protected virtual IGridColumnEditor CreateDefaultColumnEditor()
		{
			throw new GridException("This column is not editable");
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06002BED RID: 11245 RVA: 0x0008FE96 File Offset: 0x0008E096
		protected IGridColumnEditor InnerColumnEditor
		{
			get
			{
				return this._columnEditor;
			}
		}

		// Token: 0x06002BEE RID: 11246 RVA: 0x0008FEA0 File Offset: 0x0008E0A0
		protected void SetDefaultColumnEditor(IGridColumnEditor defaultEditor)
		{
			GridCreateColumnEditorEventArgs gridCreateColumnEditorEventArgs = new GridCreateColumnEditorEventArgs();
			gridCreateColumnEditorEventArgs.Column = this;
			gridCreateColumnEditorEventArgs.ColumnEditor = defaultEditor;
			base.Owner.OwnerGrid.CallOnCreateColumnEditor(gridCreateColumnEditorEventArgs);
			this.ColumnEditorChange(gridCreateColumnEditorEventArgs.ColumnEditor);
		}

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06002BEF RID: 11247 RVA: 0x0008FEDE File Offset: 0x0008E0DE
		// (set) Token: 0x06002BF0 RID: 11248 RVA: 0x0008FEE6 File Offset: 0x0008E0E6
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		public virtual IGridColumnEditor ColumnEditor
		{
			get
			{
				return this._columnEditor;
			}
			set
			{
				if (value == null)
				{
					this._columnEditor = null;
					return;
				}
				this.ColumnEditorChange(value);
			}
		}

		// Token: 0x06002BF1 RID: 11249 RVA: 0x0008FEFA File Offset: 0x0008E0FA
		protected virtual void ColumnEditorChange(IGridColumnEditor newValue)
		{
			if (this._columnEditor != null && base.Owner.OwnerGrid.ResolvedRenderMode == RenderMode.Mobile)
			{
				return;
			}
			this._columnEditor = newValue;
		}

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x06002BF2 RID: 11250 RVA: 0x0008FF20 File Offset: 0x0008E120
		// (set) Token: 0x06002BF3 RID: 11251 RVA: 0x0008FF4E File Offset: 0x0008E14E
		[Description("Convert the emty values to null when extracting values for inserting, updating, deleting")]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public bool ConvertEmptyStringToNull
		{
			get
			{
				object obj = base.ViewState["_cestn"];
				if (obj == null)
				{
					obj = true;
				}
				return (bool)obj;
			}
			set
			{
				base.ViewState["_cestn"] = value;
			}
		}

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x06002BF4 RID: 11252 RVA: 0x0008FF68 File Offset: 0x0008E168
		// (set) Token: 0x06002BF5 RID: 11253 RVA: 0x0008FFBD File Offset: 0x0008E1BD
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Description("Gets or sets whether the column editor will be native when gird's RenderMode is set to Mobile")]
		public bool UseNativeEditorsInMobileMode
		{
			get
			{
				object obj = base.ViewState["_uneimm"];
				if (obj == null)
				{
					obj = ((ConfigurationManager.AppSettings["UseGridNativeEditorsInMobileMode"] == null) ? null : ConfigurationManager.AppSettings["UseGridNativeEditorsInMobileMode"]);
					if (obj == null)
					{
						obj = true;
					}
				}
				return Convert.ToBoolean(obj);
			}
			set
			{
				base.ViewState["_uneimm"] = value;
			}
		}

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x06002BF6 RID: 11254 RVA: 0x0008FFD5 File Offset: 0x0008E1D5
		// (set) Token: 0x06002BF7 RID: 11255 RVA: 0x00090013 File Offset: 0x0008E213
		[NotifyParentProperty(true)]
		[DefaultValue("")]
		[Description("Gets or sets a default value for the column when the row is in Insert mode")]
		public string DefaultInsertValue
		{
			get
			{
				if (base.ViewState["_definv"] == null)
				{
					base.ViewState["_definv"] = string.Empty;
				}
				return (string)base.ViewState["_definv"];
			}
			set
			{
				base.ViewState["_definv"] = value;
			}
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x06002BF8 RID: 11256 RVA: 0x00090028 File Offset: 0x0008E228
		// (set) Token: 0x06002BF9 RID: 11257 RVA: 0x00090051 File Offset: 0x0008E251
		[DefaultValue(false)]
		[SuppressMessage("Microsoft.Naming", "CA1716:IdentifiersShouldNotMatchKeywords", MessageId = "ReadOnly")]
		[Description("Gets or sets the readonly status of the column.")]
		[Category("Behavior")]
		[NotifyParentProperty(true)]
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
				if (base.Owner != null && base.OriginalColumn != null)
				{
					((GridEditableColumn)base.OriginalColumn).ReadOnly = value;
				}
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x06002BFA RID: 11258 RVA: 0x00090090 File Offset: 0x0008E290
		// (set) Token: 0x06002BFB RID: 11259 RVA: 0x000900B9 File Offset: 0x0008E2B9
		[DefaultValue(GridColumnVisibilityMode.Inherited)]
		[NotifyParentProperty(true)]
		public GridColumnVisibilityMode InsertVisiblityMode
		{
			get
			{
				object obj = base.ViewState["InsertVisibilityMode"];
				if (obj == null)
				{
					return GridColumnVisibilityMode.Inherited;
				}
				return (GridColumnVisibilityMode)obj;
			}
			set
			{
				base.ViewState["InsertVisibilityMode"] = value;
			}
		}

		// Token: 0x06002BFC RID: 11260 RVA: 0x000900D4 File Offset: 0x0008E2D4
		internal bool IsReadOnly(GridItem item)
		{
			if (item is IGridInsertItem)
			{
				if (this.InsertVisiblityMode == GridColumnVisibilityMode.AlwaysHidden)
				{
					return true;
				}
				if (this.InsertVisiblityMode == GridColumnVisibilityMode.AlwaysVisible)
				{
					return false;
				}
			}
			if (this != null)
			{
				return this.ReadOnly;
			}
			return this.IsEditable;
		}

		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x06002BFD RID: 11261 RVA: 0x00090111 File Offset: 0x0008E311
		GridEditableColumn IGridEditableColumn.Column
		{
			get
			{
				return this.GetEditableColumn();
			}
		}

		// Token: 0x06002BFE RID: 11262 RVA: 0x00090119 File Offset: 0x0008E319
		protected virtual GridEditableColumn GetEditableColumn()
		{
			return this;
		}

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x06002BFF RID: 11263 RVA: 0x0009011C File Offset: 0x0008E31C
		// (set) Token: 0x06002C00 RID: 11264 RVA: 0x0009014A File Offset: 0x0008E34A
		[NotifyParentProperty(true)]
		[DefaultValue(GridForceExtractValues.None)]
		public GridForceExtractValues ForceExtractValue
		{
			get
			{
				object obj = base.ViewState["_fev"];
				if (obj == null)
				{
					obj = GridForceExtractValues.None;
				}
				return (GridForceExtractValues)obj;
			}
			set
			{
				base.ViewState["_fev"] = value;
			}
		}

		// Token: 0x06002C01 RID: 11265
		public abstract void FillValues(IDictionary newValues, GridEditableItem editableItem);

		// Token: 0x06002C02 RID: 11266 RVA: 0x00090162 File Offset: 0x0008E362
		public virtual bool ShouldExtractValues(GridEditableItem item)
		{
			return (item.IsInEditMode && (this.ForceExtractValue == GridForceExtractValues.InEditMode || this.ForceExtractValue == GridForceExtractValues.Always)) || (!item.IsInEditMode && (this.ForceExtractValue == GridForceExtractValues.InBrowseMode || this.ForceExtractValue == GridForceExtractValues.Always)) || this.IsEditable;
		}

		// Token: 0x06002C03 RID: 11267 RVA: 0x000901A2 File Offset: 0x0008E3A2
		protected virtual object ConvertValueIfEmpty(string value)
		{
			if (this.ConvertEmptyStringToNull && string.IsNullOrEmpty(value))
			{
				return null;
			}
			return value;
		}

		// Token: 0x06002C04 RID: 11268 RVA: 0x000901B8 File Offset: 0x0008E3B8
		protected override void CopyBaseProperties(GridColumn fromColumn)
		{
			base.CopyBaseProperties(fromColumn);
			GridEditableColumn gridEditableColumn = (GridEditableColumn)fromColumn;
			this.ColumnEditorID = gridEditableColumn.ColumnEditorID;
			this.ColumnEditor = gridEditableColumn.ColumnEditor;
			this.ConvertEmptyStringToNull = gridEditableColumn.ConvertEmptyStringToNull;
			this.ForceExtractValue = gridEditableColumn.ForceExtractValue;
			this.ReadOnly = gridEditableColumn.ReadOnly;
			this.DefaultInsertValue = gridEditableColumn.DefaultInsertValue;
			this.InsertVisiblityMode = gridEditableColumn.InsertVisiblityMode;
			this.AllowSorting = gridEditableColumn.AllowSorting;
			this.AllowFiltering = gridEditableColumn.AllowFiltering;
			this.UseNativeEditorsInMobileMode = gridEditableColumn.UseNativeEditorsInMobileMode;
		}

		// Token: 0x06002C05 RID: 11269 RVA: 0x0009024B File Offset: 0x0008E44B
		public virtual string GetActiveDataField()
		{
			return this.GetFilterDataField();
		}

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x06002C06 RID: 11270 RVA: 0x00090254 File Offset: 0x0008E454
		// (set) Token: 0x06002C07 RID: 11271 RVA: 0x0009027D File Offset: 0x0008E47D
		[DefaultValue(true)]
		[NotifyParentProperty(true)]
		[Description("AllowSorting")]
		[Category("Behavior")]
		public virtual bool AllowSorting
		{
			get
			{
				object obj = base.ViewState["_as"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["_as"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x06002C08 RID: 11272 RVA: 0x0009029B File Offset: 0x0008E49B
		protected override bool Sortable
		{
			get
			{
				return this.AllowSorting;
			}
		}

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x06002C09 RID: 11273 RVA: 0x000902A4 File Offset: 0x0008E4A4
		// (set) Token: 0x06002C0A RID: 11274 RVA: 0x000902CD File Offset: 0x0008E4CD
		[Category("Behavior")]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Description("AllowFiltering")]
		public virtual bool AllowFiltering
		{
			get
			{
				object obj = base.ViewState["_af"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["_af"] = value;
				this.OnColumnChanged();
			}
		}

		// Token: 0x04000B62 RID: 2914
		private IGridColumnEditor _columnEditor;
	}
}

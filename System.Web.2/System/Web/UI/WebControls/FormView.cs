using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing.Design;
using System.Globalization;
using System.Web.UI.WebControls.Adapters;
using System.Web.Util;

namespace System.Web.UI.WebControls
{
	// Token: 0x020003FE RID: 1022
	[Designer("System.Web.UI.Design.WebControls.FormViewDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ControlValueProperty("SelectedValue")]
	[DefaultEvent("PageIndexChanging")]
	[SupportsEventValidation]
	[DataKeyProperty("DataKey")]
	public class FormView : CompositeDataBoundControl, IDataItemContainer, INamingContainer, IPostBackEventHandler, IPostBackContainer, IDataBoundItemControl, IDataBoundControl, IRenderOuterTableControl
	{
		// Token: 0x17000E29 RID: 3625
		// (get) Token: 0x0600313B RID: 12603 RVA: 0x0009A3DF File Offset: 0x000985DF
		// (set) Token: 0x0600313C RID: 12604 RVA: 0x0009A3E7 File Offset: 0x000985E7
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Data")]
		[WebSysDescription("DataBoundControl_UpdateMethod")]
		public new virtual string UpdateMethod
		{
			get
			{
				return base.UpdateMethod;
			}
			set
			{
				base.UpdateMethod = value;
			}
		}

		// Token: 0x17000E2A RID: 3626
		// (get) Token: 0x0600313D RID: 12605 RVA: 0x0009A3F0 File Offset: 0x000985F0
		// (set) Token: 0x0600313E RID: 12606 RVA: 0x0009A3F8 File Offset: 0x000985F8
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Data")]
		[WebSysDescription("DataBoundControl_DeleteMethod")]
		public new virtual string DeleteMethod
		{
			get
			{
				return base.DeleteMethod;
			}
			set
			{
				base.DeleteMethod = value;
			}
		}

		// Token: 0x17000E2B RID: 3627
		// (get) Token: 0x0600313F RID: 12607 RVA: 0x0009A401 File Offset: 0x00098601
		// (set) Token: 0x06003140 RID: 12608 RVA: 0x0009A409 File Offset: 0x00098609
		[DefaultValue("")]
		[Themeable(false)]
		[WebCategory("Data")]
		[WebSysDescription("DataBoundControl_InsertMethod")]
		public new virtual string InsertMethod
		{
			get
			{
				return base.InsertMethod;
			}
			set
			{
				base.InsertMethod = value;
			}
		}

		// Token: 0x17000E2C RID: 3628
		// (get) Token: 0x06003141 RID: 12609 RVA: 0x000A05A8 File Offset: 0x0009E7A8
		// (set) Token: 0x06003142 RID: 12610 RVA: 0x000A05D4 File Offset: 0x0009E7D4
		[WebCategory("Paging")]
		[DefaultValue(false)]
		[WebSysDescription("FormView_AllowPaging")]
		public virtual bool AllowPaging
		{
			get
			{
				object obj = this.ViewState["AllowPaging"];
				return obj != null && (bool)obj;
			}
			set
			{
				bool allowPaging = this.AllowPaging;
				if (value != allowPaging)
				{
					this.ViewState["AllowPaging"] = value;
					if (base.Initialized)
					{
						base.RequiresDataBinding = true;
					}
				}
			}
		}

		// Token: 0x17000E2D RID: 3629
		// (get) Token: 0x06003143 RID: 12611 RVA: 0x000963E9 File Offset: 0x000945E9
		// (set) Token: 0x06003144 RID: 12612 RVA: 0x00096409 File Offset: 0x00094609
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[Editor("System.Web.UI.Design.ImageUrlEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[UrlProperty]
		[WebSysDescription("WebControl_BackImageUrl")]
		public virtual string BackImageUrl
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return string.Empty;
				}
				return ((TableStyle)base.ControlStyle).BackImageUrl;
			}
			set
			{
				((TableStyle)base.ControlStyle).BackImageUrl = value;
			}
		}

		// Token: 0x17000E2E RID: 3630
		// (get) Token: 0x06003145 RID: 12613 RVA: 0x000A0611 File Offset: 0x0009E811
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual FormViewRow BottomPagerRow
		{
			get
			{
				if (this._bottomPagerRow == null)
				{
					this.EnsureChildControls();
				}
				return this._bottomPagerRow;
			}
		}

		// Token: 0x17000E2F RID: 3631
		// (get) Token: 0x06003146 RID: 12614 RVA: 0x000A0628 File Offset: 0x0009E828
		private IOrderedDictionary BoundFieldValues
		{
			get
			{
				if (this._boundFieldValues == null)
				{
					int capacity = 25;
					this._boundFieldValues = new OrderedDictionary(capacity);
				}
				return this._boundFieldValues;
			}
		}

		// Token: 0x17000E30 RID: 3632
		// (get) Token: 0x06003147 RID: 12615 RVA: 0x000A0654 File Offset: 0x0009E854
		// (set) Token: 0x06003148 RID: 12616 RVA: 0x00085605 File Offset: 0x00083805
		[Localizable(true)]
		[DefaultValue("")]
		[WebCategory("Accessibility")]
		[WebSysDescription("DataControls_Caption")]
		public virtual string Caption
		{
			get
			{
				string text = (string)this.ViewState["Caption"];
				if (text == null)
				{
					return string.Empty;
				}
				return text;
			}
			set
			{
				this.ViewState["Caption"] = value;
			}
		}

		// Token: 0x17000E31 RID: 3633
		// (get) Token: 0x06003149 RID: 12617 RVA: 0x000A0684 File Offset: 0x0009E884
		// (set) Token: 0x0600314A RID: 12618 RVA: 0x00085641 File Offset: 0x00083841
		[DefaultValue(TableCaptionAlign.NotSet)]
		[WebCategory("Accessibility")]
		[WebSysDescription("WebControl_CaptionAlign")]
		public virtual TableCaptionAlign CaptionAlign
		{
			get
			{
				object obj = this.ViewState["CaptionAlign"];
				if (obj == null)
				{
					return TableCaptionAlign.NotSet;
				}
				return (TableCaptionAlign)obj;
			}
			set
			{
				if (value < TableCaptionAlign.NotSet || value > TableCaptionAlign.Right)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.ViewState["CaptionAlign"] = value;
			}
		}

		// Token: 0x17000E32 RID: 3634
		// (get) Token: 0x0600314B RID: 12619 RVA: 0x0008566C File Offset: 0x0008386C
		// (set) Token: 0x0600314C RID: 12620 RVA: 0x00085688 File Offset: 0x00083888
		[WebCategory("Layout")]
		[DefaultValue(-1)]
		[WebSysDescription("FormView_CellPadding")]
		public virtual int CellPadding
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return -1;
				}
				return ((TableStyle)base.ControlStyle).CellPadding;
			}
			set
			{
				((TableStyle)base.ControlStyle).CellPadding = value;
			}
		}

		// Token: 0x17000E33 RID: 3635
		// (get) Token: 0x0600314D RID: 12621 RVA: 0x0008569B File Offset: 0x0008389B
		// (set) Token: 0x0600314E RID: 12622 RVA: 0x000856B7 File Offset: 0x000838B7
		[WebCategory("Layout")]
		[DefaultValue(0)]
		[WebSysDescription("FormView_CellSpacing")]
		public virtual int CellSpacing
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return 0;
				}
				return ((TableStyle)base.ControlStyle).CellSpacing;
			}
			set
			{
				((TableStyle)base.ControlStyle).CellSpacing = value;
			}
		}

		// Token: 0x17000E34 RID: 3636
		// (get) Token: 0x0600314F RID: 12623 RVA: 0x000A06AD File Offset: 0x0009E8AD
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public FormViewMode CurrentMode
		{
			get
			{
				return this.Mode;
			}
		}

		// Token: 0x17000E35 RID: 3637
		// (get) Token: 0x06003150 RID: 12624 RVA: 0x000A06B5 File Offset: 0x0009E8B5
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual object DataItem
		{
			get
			{
				if (this.CurrentMode == FormViewMode.Insert)
				{
					return null;
				}
				return this._dataItem;
			}
		}

		// Token: 0x17000E36 RID: 3638
		// (get) Token: 0x06003151 RID: 12625 RVA: 0x000A06C8 File Offset: 0x0009E8C8
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public int DataItemCount
		{
			get
			{
				return this.PageCount;
			}
		}

		// Token: 0x17000E37 RID: 3639
		// (get) Token: 0x06003152 RID: 12626 RVA: 0x000A06D0 File Offset: 0x0009E8D0
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual int DataItemIndex
		{
			get
			{
				if (this.CurrentMode == FormViewMode.Insert)
				{
					return -1;
				}
				return this._dataItemIndex;
			}
		}

		// Token: 0x17000E38 RID: 3640
		// (get) Token: 0x06003153 RID: 12627 RVA: 0x000A06E4 File Offset: 0x0009E8E4
		// (set) Token: 0x06003154 RID: 12628 RVA: 0x000A0714 File Offset: 0x0009E914
		[DefaultValue(null)]
		[Editor("System.Web.UI.Design.WebControls.DataFieldEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[TypeConverter(typeof(StringArrayConverter))]
		[WebCategory("Data")]
		[WebSysDescription("DataControls_DataKeyNames")]
		public virtual string[] DataKeyNames
		{
			get
			{
				object dataKeyNames = this._dataKeyNames;
				if (dataKeyNames != null)
				{
					return (string[])((string[])dataKeyNames).Clone();
				}
				return new string[0];
			}
			set
			{
				if (!DataBoundControlHelper.CompareStringArrays(value, this.DataKeyNamesInternal))
				{
					if (value != null)
					{
						this._dataKeyNames = (string[])value.Clone();
					}
					else
					{
						this._dataKeyNames = null;
					}
					this._keyTable = null;
					if (base.Initialized)
					{
						base.RequiresDataBinding = true;
					}
				}
			}
		}

		// Token: 0x17000E39 RID: 3641
		// (get) Token: 0x06003155 RID: 12629 RVA: 0x000A0764 File Offset: 0x0009E964
		private string[] DataKeyNamesInternal
		{
			get
			{
				object dataKeyNames = this._dataKeyNames;
				if (dataKeyNames != null)
				{
					return (string[])dataKeyNames;
				}
				return new string[0];
			}
		}

		// Token: 0x17000E3A RID: 3642
		// (get) Token: 0x06003156 RID: 12630 RVA: 0x000A0788 File Offset: 0x0009E988
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("FormView_DataKey")]
		public virtual DataKey DataKey
		{
			get
			{
				if (this._dataKey == null)
				{
					this._dataKey = new DataKey(this.KeyTable);
				}
				return this._dataKey;
			}
		}

		// Token: 0x17000E3B RID: 3643
		// (get) Token: 0x06003157 RID: 12631 RVA: 0x000A07A9 File Offset: 0x0009E9A9
		// (set) Token: 0x06003158 RID: 12632 RVA: 0x000A07B1 File Offset: 0x0009E9B1
		[WebCategory("Behavior")]
		[DefaultValue(FormViewMode.ReadOnly)]
		[WebSysDescription("View_DefaultMode")]
		public virtual FormViewMode DefaultMode
		{
			get
			{
				return this._defaultMode;
			}
			set
			{
				if (value < FormViewMode.ReadOnly || value > FormViewMode.Insert)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._defaultMode = value;
			}
		}

		// Token: 0x17000E3C RID: 3644
		// (get) Token: 0x06003159 RID: 12633 RVA: 0x000A07CD File Offset: 0x0009E9CD
		// (set) Token: 0x0600315A RID: 12634 RVA: 0x000A07D5 File Offset: 0x0009E9D5
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(FormView), BindingDirection.TwoWay)]
		[WebSysDescription("FormView_EditItemTemplate")]
		public virtual ITemplate EditItemTemplate
		{
			get
			{
				return this._editItemTemplate;
			}
			set
			{
				this._editItemTemplate = value;
			}
		}

		// Token: 0x17000E3D RID: 3645
		// (get) Token: 0x0600315B RID: 12635 RVA: 0x000A07DE File Offset: 0x0009E9DE
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("View_EditRowStyle")]
		public TableItemStyle EditRowStyle
		{
			get
			{
				if (this._editRowStyle == null)
				{
					this._editRowStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._editRowStyle).TrackViewState();
					}
				}
				return this._editRowStyle;
			}
		}

		// Token: 0x17000E3E RID: 3646
		// (get) Token: 0x0600315C RID: 12636 RVA: 0x000A080C File Offset: 0x0009EA0C
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("View_EmptyDataRowStyle")]
		public TableItemStyle EmptyDataRowStyle
		{
			get
			{
				if (this._emptyDataRowStyle == null)
				{
					this._emptyDataRowStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._emptyDataRowStyle).TrackViewState();
					}
				}
				return this._emptyDataRowStyle;
			}
		}

		// Token: 0x17000E3F RID: 3647
		// (get) Token: 0x0600315D RID: 12637 RVA: 0x000A083A File Offset: 0x0009EA3A
		// (set) Token: 0x0600315E RID: 12638 RVA: 0x000A0842 File Offset: 0x0009EA42
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(FormView))]
		[WebSysDescription("View_EmptyDataTemplate")]
		public virtual ITemplate EmptyDataTemplate
		{
			get
			{
				return this._emptyDataTemplate;
			}
			set
			{
				this._emptyDataTemplate = value;
			}
		}

		// Token: 0x17000E40 RID: 3648
		// (get) Token: 0x0600315F RID: 12639 RVA: 0x000A084C File Offset: 0x0009EA4C
		// (set) Token: 0x06003160 RID: 12640 RVA: 0x0009A8F5 File Offset: 0x00098AF5
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("View_EmptyDataText")]
		public virtual string EmptyDataText
		{
			get
			{
				object obj = this.ViewState["EmptyDataText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["EmptyDataText"] = value;
			}
		}

		// Token: 0x17000E41 RID: 3649
		// (get) Token: 0x06003161 RID: 12641 RVA: 0x000A087C File Offset: 0x0009EA7C
		// (set) Token: 0x06003162 RID: 12642 RVA: 0x0009A931 File Offset: 0x00098B31
		[WebCategory("Behavior")]
		[DefaultValue(true)]
		[WebSysDescription("DataBoundControl_EnableModelValidation")]
		public virtual bool EnableModelValidation
		{
			get
			{
				object obj = this.ViewState["EnableModelValidation"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["EnableModelValidation"] = value;
			}
		}

		// Token: 0x17000E42 RID: 3650
		// (get) Token: 0x06003163 RID: 12643 RVA: 0x000A08A8 File Offset: 0x0009EAA8
		// (set) Token: 0x06003164 RID: 12644 RVA: 0x0008BC71 File Offset: 0x00089E71
		[WebCategory("Layout")]
		[DefaultValue(true)]
		[WebSysDescription("FormView_RenderOuterTable")]
		public virtual bool RenderOuterTable
		{
			get
			{
				object obj = this.ViewState["RenderOuterTable"];
				return obj == null || (bool)obj;
			}
			set
			{
				this.ViewState["RenderOuterTable"] = value;
			}
		}

		// Token: 0x17000E43 RID: 3651
		// (get) Token: 0x06003165 RID: 12645 RVA: 0x000A08D4 File Offset: 0x0009EAD4
		// (set) Token: 0x06003166 RID: 12646 RVA: 0x000A08FD File Offset: 0x0009EAFD
		private int FirstDisplayedPageIndex
		{
			get
			{
				object obj = this.ViewState["FirstDisplayedPageIndex"];
				if (obj != null)
				{
					return (int)obj;
				}
				return -1;
			}
			set
			{
				this.ViewState["FirstDisplayedPageIndex"] = value;
			}
		}

		// Token: 0x17000E44 RID: 3652
		// (get) Token: 0x06003167 RID: 12647 RVA: 0x000A0915 File Offset: 0x0009EB15
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual FormViewRow FooterRow
		{
			get
			{
				if (this._footerRow == null)
				{
					this.EnsureChildControls();
				}
				return this._footerRow;
			}
		}

		// Token: 0x17000E45 RID: 3653
		// (get) Token: 0x06003168 RID: 12648 RVA: 0x000A092B File Offset: 0x0009EB2B
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("FormView_FooterStyle")]
		public TableItemStyle FooterStyle
		{
			get
			{
				if (this._footerStyle == null)
				{
					this._footerStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._footerStyle).TrackViewState();
					}
				}
				return this._footerStyle;
			}
		}

		// Token: 0x17000E46 RID: 3654
		// (get) Token: 0x06003169 RID: 12649 RVA: 0x000A0959 File Offset: 0x0009EB59
		// (set) Token: 0x0600316A RID: 12650 RVA: 0x000A0961 File Offset: 0x0009EB61
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(FormView))]
		[WebSysDescription("FormView_FooterTemplate")]
		public virtual ITemplate FooterTemplate
		{
			get
			{
				return this._footerTemplate;
			}
			set
			{
				this._footerTemplate = value;
			}
		}

		// Token: 0x17000E47 RID: 3655
		// (get) Token: 0x0600316B RID: 12651 RVA: 0x000A096C File Offset: 0x0009EB6C
		// (set) Token: 0x0600316C RID: 12652 RVA: 0x0009AAD1 File Offset: 0x00098CD1
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("View_FooterText")]
		public virtual string FooterText
		{
			get
			{
				object obj = this.ViewState["FooterText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["FooterText"] = value;
			}
		}

		// Token: 0x17000E48 RID: 3656
		// (get) Token: 0x0600316D RID: 12653 RVA: 0x00098EF0 File Offset: 0x000970F0
		// (set) Token: 0x0600316E RID: 12654 RVA: 0x0008587A File Offset: 0x00083A7A
		[WebCategory("Appearance")]
		[DefaultValue(GridLines.None)]
		[WebSysDescription("DataControls_GridLines")]
		public virtual GridLines GridLines
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return GridLines.None;
				}
				return ((TableStyle)base.ControlStyle).GridLines;
			}
			set
			{
				((TableStyle)base.ControlStyle).GridLines = value;
			}
		}

		// Token: 0x17000E49 RID: 3657
		// (get) Token: 0x0600316F RID: 12655 RVA: 0x000A0999 File Offset: 0x0009EB99
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual FormViewRow HeaderRow
		{
			get
			{
				if (this._headerRow == null)
				{
					this.EnsureChildControls();
				}
				return this._headerRow;
			}
		}

		// Token: 0x17000E4A RID: 3658
		// (get) Token: 0x06003170 RID: 12656 RVA: 0x000A09AF File Offset: 0x0009EBAF
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("WebControl_HeaderStyle")]
		public TableItemStyle HeaderStyle
		{
			get
			{
				if (this._headerStyle == null)
				{
					this._headerStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._headerStyle).TrackViewState();
					}
				}
				return this._headerStyle;
			}
		}

		// Token: 0x17000E4B RID: 3659
		// (get) Token: 0x06003171 RID: 12657 RVA: 0x000A09DD File Offset: 0x0009EBDD
		// (set) Token: 0x06003172 RID: 12658 RVA: 0x000A09E5 File Offset: 0x0009EBE5
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(FormView))]
		[WebSysDescription("WebControl_HeaderTemplate")]
		public virtual ITemplate HeaderTemplate
		{
			get
			{
				return this._headerTemplate;
			}
			set
			{
				this._headerTemplate = value;
			}
		}

		// Token: 0x17000E4C RID: 3660
		// (get) Token: 0x06003173 RID: 12659 RVA: 0x000A09F0 File Offset: 0x0009EBF0
		// (set) Token: 0x06003174 RID: 12660 RVA: 0x000A0A1D File Offset: 0x0009EC1D
		[Localizable(true)]
		[WebCategory("Appearance")]
		[DefaultValue("")]
		[WebSysDescription("View_HeaderText")]
		public virtual string HeaderText
		{
			get
			{
				object obj = this.ViewState["HeaderText"];
				if (obj != null)
				{
					return (string)obj;
				}
				return string.Empty;
			}
			set
			{
				this.ViewState["HeaderText"] = value;
			}
		}

		// Token: 0x17000E4D RID: 3661
		// (get) Token: 0x06003175 RID: 12661 RVA: 0x0008588D File Offset: 0x00083A8D
		// (set) Token: 0x06003176 RID: 12662 RVA: 0x000858A9 File Offset: 0x00083AA9
		[Category("Layout")]
		[DefaultValue(HorizontalAlign.NotSet)]
		[WebSysDescription("WebControl_HorizontalAlign")]
		public virtual HorizontalAlign HorizontalAlign
		{
			get
			{
				if (!base.ControlStyleCreated)
				{
					return HorizontalAlign.NotSet;
				}
				return ((TableStyle)base.ControlStyle).HorizontalAlign;
			}
			set
			{
				((TableStyle)base.ControlStyle).HorizontalAlign = value;
			}
		}

		// Token: 0x17000E4E RID: 3662
		// (get) Token: 0x06003177 RID: 12663 RVA: 0x000A0A30 File Offset: 0x0009EC30
		// (set) Token: 0x06003178 RID: 12664 RVA: 0x000A0A38 File Offset: 0x0009EC38
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(FormView), BindingDirection.TwoWay)]
		[WebSysDescription("FormView_InsertItemTemplate")]
		public virtual ITemplate InsertItemTemplate
		{
			get
			{
				return this._insertItemTemplate;
			}
			set
			{
				this._insertItemTemplate = value;
			}
		}

		// Token: 0x17000E4F RID: 3663
		// (get) Token: 0x06003179 RID: 12665 RVA: 0x000A0A41 File Offset: 0x0009EC41
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("View_InsertRowStyle")]
		public TableItemStyle InsertRowStyle
		{
			get
			{
				if (this._insertRowStyle == null)
				{
					this._insertRowStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._insertRowStyle).TrackViewState();
					}
				}
				return this._insertRowStyle;
			}
		}

		// Token: 0x17000E50 RID: 3664
		// (get) Token: 0x0600317A RID: 12666 RVA: 0x000A0A6F File Offset: 0x0009EC6F
		// (set) Token: 0x0600317B RID: 12667 RVA: 0x000A0A77 File Offset: 0x0009EC77
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(FormView), BindingDirection.TwoWay)]
		[WebSysDescription("View_InsertRowStyle")]
		public virtual ITemplate ItemTemplate
		{
			get
			{
				return this._itemTemplate;
			}
			set
			{
				this._itemTemplate = value;
			}
		}

		// Token: 0x17000E51 RID: 3665
		// (get) Token: 0x0600317C RID: 12668 RVA: 0x000A0A80 File Offset: 0x0009EC80
		private OrderedDictionary KeyTable
		{
			get
			{
				if (this._keyTable == null)
				{
					this._keyTable = new OrderedDictionary(this.DataKeyNamesInternal.Length);
				}
				return this._keyTable;
			}
		}

		// Token: 0x17000E52 RID: 3666
		// (get) Token: 0x0600317D RID: 12669 RVA: 0x000A0AA3 File Offset: 0x0009ECA3
		// (set) Token: 0x0600317E RID: 12670 RVA: 0x000A0ACE File Offset: 0x0009ECCE
		private FormViewMode Mode
		{
			get
			{
				if (!this._modeSet || base.DesignMode)
				{
					this._mode = this.DefaultMode;
					this._modeSet = true;
				}
				return this._mode;
			}
			set
			{
				if (value < FormViewMode.ReadOnly || value > FormViewMode.Insert)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this._modeSet = true;
				if (this._mode != value)
				{
					this._mode = value;
					if (base.Initialized)
					{
						base.RequiresDataBinding = true;
					}
				}
			}
		}

		// Token: 0x17000E53 RID: 3667
		// (get) Token: 0x0600317F RID: 12671 RVA: 0x000A0B09 File Offset: 0x0009ED09
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual int PageCount
		{
			get
			{
				return this._pageCount;
			}
		}

		// Token: 0x17000E54 RID: 3668
		// (get) Token: 0x06003180 RID: 12672 RVA: 0x000A0B11 File Offset: 0x0009ED11
		// (set) Token: 0x06003181 RID: 12673 RVA: 0x000A0B1C File Offset: 0x0009ED1C
		private int PageIndexInternal
		{
			get
			{
				return this._pageIndex;
			}
			set
			{
				int pageIndexInternal = this.PageIndexInternal;
				if (value != pageIndexInternal)
				{
					this._pageIndex = value;
					if (base.Initialized)
					{
						base.RequiresDataBinding = true;
					}
				}
			}
		}

		// Token: 0x17000E55 RID: 3669
		// (get) Token: 0x06003182 RID: 12674 RVA: 0x000A0B4A File Offset: 0x0009ED4A
		// (set) Token: 0x06003183 RID: 12675 RVA: 0x000A0B65 File Offset: 0x0009ED65
		[Bindable(true)]
		[DefaultValue(0)]
		[WebCategory("Data")]
		[WebSysDescription("FormView_PageIndex")]
		public virtual int PageIndex
		{
			get
			{
				if (this.Mode == FormViewMode.Insert && !base.DesignMode)
				{
					return -1;
				}
				return this.PageIndexInternal;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				if (value >= 0)
				{
					this.PageIndexInternal = value;
				}
			}
		}

		// Token: 0x17000E56 RID: 3670
		// (get) Token: 0x06003184 RID: 12676 RVA: 0x000A0B84 File Offset: 0x0009ED84
		[WebCategory("Paging")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("GridView_PagerSettings")]
		public virtual PagerSettings PagerSettings
		{
			get
			{
				if (this._pagerSettings == null)
				{
					this._pagerSettings = new PagerSettings();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._pagerSettings).TrackViewState();
					}
					this._pagerSettings.PropertyChanged += this.OnPagerPropertyChanged;
				}
				return this._pagerSettings;
			}
		}

		// Token: 0x17000E57 RID: 3671
		// (get) Token: 0x06003185 RID: 12677 RVA: 0x000A0BD4 File Offset: 0x0009EDD4
		[WebCategory("Styles")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("WebControl_PagerStyle")]
		public TableItemStyle PagerStyle
		{
			get
			{
				if (this._pagerStyle == null)
				{
					this._pagerStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._pagerStyle).TrackViewState();
					}
				}
				return this._pagerStyle;
			}
		}

		// Token: 0x17000E58 RID: 3672
		// (get) Token: 0x06003186 RID: 12678 RVA: 0x000A0C02 File Offset: 0x0009EE02
		// (set) Token: 0x06003187 RID: 12679 RVA: 0x000A0C0A File Offset: 0x0009EE0A
		[Browsable(false)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TemplateContainer(typeof(FormView))]
		[WebSysDescription("View_PagerTemplate")]
		public virtual ITemplate PagerTemplate
		{
			get
			{
				return this._pagerTemplate;
			}
			set
			{
				this._pagerTemplate = value;
			}
		}

		// Token: 0x17000E59 RID: 3673
		// (get) Token: 0x06003188 RID: 12680 RVA: 0x000A0C13 File Offset: 0x0009EE13
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[WebSysDescription("FormView_Rows")]
		public virtual FormViewRow Row
		{
			get
			{
				if (this._row == null)
				{
					this.EnsureChildControls();
				}
				return this._row;
			}
		}

		// Token: 0x17000E5A RID: 3674
		// (get) Token: 0x06003189 RID: 12681 RVA: 0x000A0C29 File Offset: 0x0009EE29
		[WebCategory("Styles")]
		[DefaultValue(null)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[WebSysDescription("View_RowStyle")]
		public TableItemStyle RowStyle
		{
			get
			{
				if (this._rowStyle == null)
				{
					this._rowStyle = new TableItemStyle();
					if (base.IsTrackingViewState)
					{
						((IStateManager)this._rowStyle).TrackViewState();
					}
				}
				return this._rowStyle;
			}
		}

		// Token: 0x17000E5B RID: 3675
		// (get) Token: 0x0600318A RID: 12682 RVA: 0x000A0C57 File Offset: 0x0009EE57
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public object SelectedValue
		{
			get
			{
				return this.DataKey.Value;
			}
		}

		// Token: 0x17000E5C RID: 3676
		// (get) Token: 0x0600318B RID: 12683 RVA: 0x0008BDAD File Offset: 0x00089FAD
		protected override HtmlTextWriterTag TagKey
		{
			get
			{
				return HtmlTextWriterTag.Table;
			}
		}

		// Token: 0x17000E5D RID: 3677
		// (get) Token: 0x0600318C RID: 12684 RVA: 0x000A0C64 File Offset: 0x0009EE64
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual FormViewRow TopPagerRow
		{
			get
			{
				if (this._topPagerRow == null)
				{
					this.EnsureChildControls();
				}
				return this._topPagerRow;
			}
		}

		// Token: 0x1400008A RID: 138
		// (add) Token: 0x0600318D RID: 12685 RVA: 0x000A0C7A File Offset: 0x0009EE7A
		// (remove) Token: 0x0600318E RID: 12686 RVA: 0x000A0C8D File Offset: 0x0009EE8D
		[WebCategory("Action")]
		[WebSysDescription("FormView_OnPageIndexChanged")]
		public event EventHandler PageIndexChanged
		{
			add
			{
				base.Events.AddHandler(FormView.EventPageIndexChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(FormView.EventPageIndexChanged, value);
			}
		}

		// Token: 0x1400008B RID: 139
		// (add) Token: 0x0600318F RID: 12687 RVA: 0x000A0CA0 File Offset: 0x0009EEA0
		// (remove) Token: 0x06003190 RID: 12688 RVA: 0x000A0CB3 File Offset: 0x0009EEB3
		[WebCategory("Action")]
		[WebSysDescription("FormView_OnPageIndexChanging")]
		public event FormViewPageEventHandler PageIndexChanging
		{
			add
			{
				base.Events.AddHandler(FormView.EventPageIndexChanging, value);
			}
			remove
			{
				base.Events.RemoveHandler(FormView.EventPageIndexChanging, value);
			}
		}

		// Token: 0x1400008C RID: 140
		// (add) Token: 0x06003191 RID: 12689 RVA: 0x000A0CC6 File Offset: 0x0009EEC6
		// (remove) Token: 0x06003192 RID: 12690 RVA: 0x000A0CD9 File Offset: 0x0009EED9
		[WebCategory("Action")]
		[WebSysDescription("FormView_OnItemCommand")]
		public event FormViewCommandEventHandler ItemCommand
		{
			add
			{
				base.Events.AddHandler(FormView.EventItemCommand, value);
			}
			remove
			{
				base.Events.RemoveHandler(FormView.EventItemCommand, value);
			}
		}

		// Token: 0x1400008D RID: 141
		// (add) Token: 0x06003193 RID: 12691 RVA: 0x000A0CEC File Offset: 0x0009EEEC
		// (remove) Token: 0x06003194 RID: 12692 RVA: 0x000A0CFF File Offset: 0x0009EEFF
		[WebCategory("Behavior")]
		[WebSysDescription("FormView_OnItemCreated")]
		public event EventHandler ItemCreated
		{
			add
			{
				base.Events.AddHandler(FormView.EventItemCreated, value);
			}
			remove
			{
				base.Events.RemoveHandler(FormView.EventItemCreated, value);
			}
		}

		// Token: 0x1400008E RID: 142
		// (add) Token: 0x06003195 RID: 12693 RVA: 0x000A0D12 File Offset: 0x0009EF12
		// (remove) Token: 0x06003196 RID: 12694 RVA: 0x000A0D25 File Offset: 0x0009EF25
		[WebCategory("Action")]
		[WebSysDescription("DataControls_OnItemDeleted")]
		public event FormViewDeletedEventHandler ItemDeleted
		{
			add
			{
				base.Events.AddHandler(FormView.EventItemDeleted, value);
			}
			remove
			{
				base.Events.RemoveHandler(FormView.EventItemDeleted, value);
			}
		}

		// Token: 0x1400008F RID: 143
		// (add) Token: 0x06003197 RID: 12695 RVA: 0x000A0D38 File Offset: 0x0009EF38
		// (remove) Token: 0x06003198 RID: 12696 RVA: 0x000A0D4B File Offset: 0x0009EF4B
		[WebCategory("Action")]
		[WebSysDescription("DataControls_OnItemDeleting")]
		public event FormViewDeleteEventHandler ItemDeleting
		{
			add
			{
				base.Events.AddHandler(FormView.EventItemDeleting, value);
			}
			remove
			{
				base.Events.RemoveHandler(FormView.EventItemDeleting, value);
			}
		}

		// Token: 0x14000090 RID: 144
		// (add) Token: 0x06003199 RID: 12697 RVA: 0x000A0D5E File Offset: 0x0009EF5E
		// (remove) Token: 0x0600319A RID: 12698 RVA: 0x000A0D71 File Offset: 0x0009EF71
		[WebCategory("Action")]
		[WebSysDescription("DataControls_OnItemInserted")]
		public event FormViewInsertedEventHandler ItemInserted
		{
			add
			{
				base.Events.AddHandler(FormView.EventItemInserted, value);
			}
			remove
			{
				base.Events.RemoveHandler(FormView.EventItemInserted, value);
			}
		}

		// Token: 0x14000091 RID: 145
		// (add) Token: 0x0600319B RID: 12699 RVA: 0x000A0D84 File Offset: 0x0009EF84
		// (remove) Token: 0x0600319C RID: 12700 RVA: 0x000A0D97 File Offset: 0x0009EF97
		[WebCategory("Action")]
		[WebSysDescription("DataControls_OnItemInserting")]
		public event FormViewInsertEventHandler ItemInserting
		{
			add
			{
				base.Events.AddHandler(FormView.EventItemInserting, value);
			}
			remove
			{
				base.Events.RemoveHandler(FormView.EventItemInserting, value);
			}
		}

		// Token: 0x14000092 RID: 146
		// (add) Token: 0x0600319D RID: 12701 RVA: 0x000A0DAA File Offset: 0x0009EFAA
		// (remove) Token: 0x0600319E RID: 12702 RVA: 0x000A0DBD File Offset: 0x0009EFBD
		[WebCategory("Action")]
		[WebSysDescription("DataControls_OnItemUpdated")]
		public event FormViewUpdatedEventHandler ItemUpdated
		{
			add
			{
				base.Events.AddHandler(FormView.EventItemUpdated, value);
			}
			remove
			{
				base.Events.RemoveHandler(FormView.EventItemUpdated, value);
			}
		}

		// Token: 0x14000093 RID: 147
		// (add) Token: 0x0600319F RID: 12703 RVA: 0x000A0DD0 File Offset: 0x0009EFD0
		// (remove) Token: 0x060031A0 RID: 12704 RVA: 0x000A0DE3 File Offset: 0x0009EFE3
		[WebCategory("Action")]
		[WebSysDescription("DataControls_OnItemUpdating")]
		public event FormViewUpdateEventHandler ItemUpdating
		{
			add
			{
				base.Events.AddHandler(FormView.EventItemUpdating, value);
			}
			remove
			{
				base.Events.RemoveHandler(FormView.EventItemUpdating, value);
			}
		}

		// Token: 0x14000094 RID: 148
		// (add) Token: 0x060031A1 RID: 12705 RVA: 0x000A0DF6 File Offset: 0x0009EFF6
		// (remove) Token: 0x060031A2 RID: 12706 RVA: 0x000A0E09 File Offset: 0x0009F009
		[WebCategory("Action")]
		[WebSysDescription("FormView_OnModeChanged")]
		public event EventHandler ModeChanged
		{
			add
			{
				base.Events.AddHandler(FormView.EventModeChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(FormView.EventModeChanged, value);
			}
		}

		// Token: 0x14000095 RID: 149
		// (add) Token: 0x060031A3 RID: 12707 RVA: 0x000A0E1C File Offset: 0x0009F01C
		// (remove) Token: 0x060031A4 RID: 12708 RVA: 0x000A0E2F File Offset: 0x0009F02F
		[WebCategory("Action")]
		[WebSysDescription("FormView_OnModeChanging")]
		public event FormViewModeEventHandler ModeChanging
		{
			add
			{
				base.Events.AddHandler(FormView.EventModeChanging, value);
			}
			remove
			{
				base.Events.RemoveHandler(FormView.EventModeChanging, value);
			}
		}

		// Token: 0x060031A5 RID: 12709 RVA: 0x000A0E42 File Offset: 0x0009F042
		public void ChangeMode(FormViewMode newMode)
		{
			this.Mode = newMode;
		}

		// Token: 0x060031A6 RID: 12710 RVA: 0x000A0E4C File Offset: 0x0009F04C
		protected override int CreateChildControls(IEnumerable dataSource, bool dataBinding)
		{
			PagedDataSource pagedDataSource = null;
			int num = this.PageIndex;
			bool allowPaging = this.AllowPaging;
			int num2 = 0;
			FormViewMode mode = this.Mode;
			if (base.DesignMode && mode == FormViewMode.Insert)
			{
				num = -1;
			}
			if (dataBinding)
			{
				DataSourceView data = this.GetData();
				DataSourceSelectArguments selectArguments = base.SelectArguments;
				if (data == null)
				{
					throw new HttpException(SR.GetString("DataBoundControl_NullView", new object[]
					{
						this.ID
					}));
				}
				if (mode != FormViewMode.Insert)
				{
					if (allowPaging && !data.CanPage && dataSource != null && !(dataSource is ICollection))
					{
						selectArguments.StartRowIndex = num;
						selectArguments.MaximumRows = 1;
						data.Select(selectArguments, new DataSourceViewSelectCallback(this.SelectCallback));
					}
					if (this._useServerPaging)
					{
						if (data.CanRetrieveTotalRowCount)
						{
							pagedDataSource = this.CreateServerPagedDataSource(selectArguments.TotalRowCount);
						}
						else
						{
							ICollection collection = dataSource as ICollection;
							if (collection == null)
							{
								throw new HttpException(SR.GetString("DataBoundControl_NeedICollectionOrTotalRowCount", new object[]
								{
									base.GetType().Name
								}));
							}
							pagedDataSource = this.CreateServerPagedDataSource(checked(this.PageIndex + collection.Count));
						}
					}
					else
					{
						pagedDataSource = this.CreatePagedDataSource();
					}
				}
			}
			else
			{
				pagedDataSource = this.CreatePagedDataSource();
			}
			if (mode != FormViewMode.Insert)
			{
				pagedDataSource.DataSource = dataSource;
			}
			IEnumerator enumerator = null;
			OrderedDictionary keyTable = this.KeyTable;
			if (!dataBinding)
			{
				enumerator = dataSource.GetEnumerator();
				ICollection collection2 = dataSource as ICollection;
				if (collection2 == null)
				{
					throw new HttpException(SR.GetString("DataControls_DataSourceMustBeCollectionWhenNotDataBinding"));
				}
				num2 = collection2.Count;
			}
			else
			{
				keyTable.Clear();
				if (dataSource != null)
				{
					if (mode != FormViewMode.Insert)
					{
						ICollection collection3 = dataSource as ICollection;
						if (collection3 == null && pagedDataSource.IsPagingEnabled && !pagedDataSource.IsServerPagingEnabled)
						{
							throw new HttpException(SR.GetString("FormView_DataSourceMustBeCollection", new object[]
							{
								this.ID
							}));
						}
						if (pagedDataSource.IsPagingEnabled)
						{
							num2 = pagedDataSource.DataSourceCount;
						}
						else if (collection3 != null)
						{
							num2 = collection3.Count;
						}
					}
					enumerator = dataSource.GetEnumerator();
				}
			}
			Table table = this.CreateTable();
			TableRowCollection rows = table.Rows;
			bool flag = false;
			object dataItem = null;
			this.Controls.Add(table);
			if (enumerator != null)
			{
				flag = enumerator.MoveNext();
			}
			if (!flag && mode != FormViewMode.Insert)
			{
				if (this.EmptyDataText.Length > 0 || this._emptyDataTemplate != null)
				{
					this._row = this.CreateRow(0, DataControlRowType.EmptyDataRow, DataControlRowState.Normal, rows, null);
				}
				num2 = 0;
			}
			else
			{
				int i = 0;
				if (!this._useServerPaging)
				{
					while (i < num)
					{
						dataItem = enumerator.Current;
						flag = enumerator.MoveNext();
						if (!flag)
						{
							this._pageIndex = i;
							pagedDataSource.CurrentPageIndex = i;
							num = i;
							break;
						}
						i++;
					}
				}
				if (flag)
				{
					this._dataItem = enumerator.Current;
				}
				else
				{
					this._dataItem = dataItem;
				}
				if ((!this._useServerPaging && !(dataSource is ICollection)) || (this._useServerPaging && num2 < 0))
				{
					num2 = i;
					while (flag)
					{
						num2++;
						flag = enumerator.MoveNext();
					}
				}
				this._dataItemIndex = i;
				bool flag2 = num2 <= 1 && !this._useServerPaging;
				if (allowPaging && this.PagerSettings.Visible && this._pagerSettings.IsPagerOnTop && mode != FormViewMode.Insert && !flag2)
				{
					this._topPagerRow = this.CreateRow(num, DataControlRowType.Pager, DataControlRowState.Normal, rows, pagedDataSource);
				}
				this._headerRow = this.CreateRow(num, DataControlRowType.Header, DataControlRowState.Normal, rows, null);
				if (this._headerTemplate == null && this.HeaderText.Length == 0)
				{
					this._headerRow.Visible = false;
				}
				this._row = this.CreateDataRow(dataBinding, rows, this._dataItem);
				if (num >= 0)
				{
					string[] dataKeyNamesInternal = this.DataKeyNamesInternal;
					if (dataBinding && dataKeyNamesInternal.Length != 0)
					{
						foreach (string text in dataKeyNamesInternal)
						{
							object propertyValue = DataBinder.GetPropertyValue(this._dataItem, text);
							keyTable.Add(text, propertyValue);
						}
						this._dataKey = new DataKey(keyTable);
					}
				}
				this._footerRow = this.CreateRow(num, DataControlRowType.Footer, DataControlRowState.Normal, rows, null);
				if (this._footerTemplate == null && this.FooterText.Length == 0)
				{
					this._footerRow.Visible = false;
				}
				if (allowPaging && this.PagerSettings.Visible && this._pagerSettings.IsPagerOnBottom && mode != FormViewMode.Insert && !flag2)
				{
					this._bottomPagerRow = this.CreateRow(num, DataControlRowType.Pager, DataControlRowState.Normal, rows, pagedDataSource);
				}
			}
			this._pageCount = num2;
			this.OnItemCreated(EventArgs.Empty);
			if (dataBinding)
			{
				this.DataBind(false);
			}
			return num2;
		}

		// Token: 0x060031A7 RID: 12711 RVA: 0x000A12A4 File Offset: 0x0009F4A4
		protected override Style CreateControlStyle()
		{
			return new TableStyle
			{
				CellSpacing = 0
			};
		}

		// Token: 0x060031A8 RID: 12712 RVA: 0x000A12C0 File Offset: 0x0009F4C0
		private FormViewRow CreateDataRow(bool dataBinding, TableRowCollection rows, object dataItem)
		{
			ITemplate template = null;
			switch (this.Mode)
			{
			case FormViewMode.ReadOnly:
				template = this._itemTemplate;
				break;
			case FormViewMode.Edit:
				template = this._editItemTemplate;
				break;
			case FormViewMode.Insert:
				if (this._insertItemTemplate != null)
				{
					template = this._insertItemTemplate;
				}
				else
				{
					template = this._editItemTemplate;
				}
				break;
			}
			if (template != null)
			{
				return this.CreateDataRowFromTemplates(dataBinding, rows);
			}
			return null;
		}

		// Token: 0x060031A9 RID: 12713 RVA: 0x000A1324 File Offset: 0x0009F524
		private FormViewRow CreateDataRowFromTemplates(bool dataBinding, TableRowCollection rows)
		{
			int pageIndex = this.PageIndex;
			FormViewMode mode = this.Mode;
			DataControlRowState dataControlRowState = DataControlRowState.Normal;
			if (mode == FormViewMode.Edit)
			{
				dataControlRowState |= DataControlRowState.Edit;
			}
			else if (mode == FormViewMode.Insert)
			{
				dataControlRowState |= DataControlRowState.Insert;
			}
			return this.CreateRow(this.PageIndex, DataControlRowType.DataRow, dataControlRowState, rows, null);
		}

		// Token: 0x060031AA RID: 12714 RVA: 0x000A1368 File Offset: 0x0009F568
		protected override DataSourceSelectArguments CreateDataSourceSelectArguments()
		{
			DataSourceSelectArguments dataSourceSelectArguments = new DataSourceSelectArguments();
			DataSourceView data = this.GetData();
			this._useServerPaging = (this.AllowPaging && data.CanPage);
			if (this._useServerPaging)
			{
				dataSourceSelectArguments.StartRowIndex = this.PageIndex;
				if (data.CanRetrieveTotalRowCount)
				{
					dataSourceSelectArguments.RetrieveTotalRowCount = true;
					dataSourceSelectArguments.MaximumRows = 1;
				}
				else
				{
					dataSourceSelectArguments.MaximumRows = -1;
				}
			}
			return dataSourceSelectArguments;
		}

		// Token: 0x060031AB RID: 12715 RVA: 0x000A13D0 File Offset: 0x0009F5D0
		private void CreateNextPrevPager(TableRow row, PagedDataSource pagedDataSource, bool addFirstLastPageButtons)
		{
			PagerSettings pagerSettings = this.PagerSettings;
			string previousPageImageUrl = pagerSettings.PreviousPageImageUrl;
			string nextPageImageUrl = pagerSettings.NextPageImageUrl;
			bool isFirstPage = pagedDataSource.IsFirstPage;
			bool isLastPage = pagedDataSource.IsLastPage;
			if (addFirstLastPageButtons && !isFirstPage)
			{
				string firstPageImageUrl = pagerSettings.FirstPageImageUrl;
				TableCell tableCell = new TableCell();
				row.Cells.Add(tableCell);
				IButtonControl buttonControl;
				if (firstPageImageUrl.Length > 0)
				{
					buttonControl = new DataControlImageButton(this);
					((ImageButton)buttonControl).ImageUrl = firstPageImageUrl;
					((ImageButton)buttonControl).AlternateText = HttpUtility.HtmlDecode(pagerSettings.FirstPageText);
				}
				else
				{
					buttonControl = new DataControlPagerLinkButton(this);
					((DataControlPagerLinkButton)buttonControl).Text = pagerSettings.FirstPageText;
				}
				buttonControl.CommandName = "Page";
				buttonControl.CommandArgument = "First";
				tableCell.Controls.Add((Control)buttonControl);
			}
			if (!isFirstPage)
			{
				TableCell tableCell2 = new TableCell();
				row.Cells.Add(tableCell2);
				IButtonControl buttonControl2;
				if (previousPageImageUrl.Length > 0)
				{
					buttonControl2 = new DataControlImageButton(this);
					((ImageButton)buttonControl2).ImageUrl = previousPageImageUrl;
					((ImageButton)buttonControl2).AlternateText = HttpUtility.HtmlDecode(pagerSettings.PreviousPageText);
				}
				else
				{
					buttonControl2 = new DataControlPagerLinkButton(this);
					((DataControlPagerLinkButton)buttonControl2).Text = pagerSettings.PreviousPageText;
				}
				buttonControl2.CommandName = "Page";
				buttonControl2.CommandArgument = "Prev";
				tableCell2.Controls.Add((Control)buttonControl2);
			}
			if (!isLastPage)
			{
				TableCell tableCell3 = new TableCell();
				row.Cells.Add(tableCell3);
				IButtonControl buttonControl3;
				if (nextPageImageUrl.Length > 0)
				{
					buttonControl3 = new DataControlImageButton(this);
					((ImageButton)buttonControl3).ImageUrl = nextPageImageUrl;
					((ImageButton)buttonControl3).AlternateText = HttpUtility.HtmlDecode(pagerSettings.NextPageText);
				}
				else
				{
					buttonControl3 = new DataControlPagerLinkButton(this);
					((DataControlPagerLinkButton)buttonControl3).Text = pagerSettings.NextPageText;
				}
				buttonControl3.CommandName = "Page";
				buttonControl3.CommandArgument = "Next";
				tableCell3.Controls.Add((Control)buttonControl3);
			}
			if (addFirstLastPageButtons && !isLastPage)
			{
				string lastPageImageUrl = pagerSettings.LastPageImageUrl;
				TableCell tableCell4 = new TableCell();
				row.Cells.Add(tableCell4);
				IButtonControl buttonControl4;
				if (lastPageImageUrl.Length > 0)
				{
					buttonControl4 = new DataControlImageButton(this);
					((ImageButton)buttonControl4).ImageUrl = lastPageImageUrl;
					((ImageButton)buttonControl4).AlternateText = HttpUtility.HtmlDecode(pagerSettings.LastPageText);
				}
				else
				{
					buttonControl4 = new DataControlPagerLinkButton(this);
					((DataControlPagerLinkButton)buttonControl4).Text = pagerSettings.LastPageText;
				}
				buttonControl4.CommandName = "Page";
				buttonControl4.CommandArgument = "Last";
				tableCell4.Controls.Add((Control)buttonControl4);
			}
		}

		// Token: 0x060031AC RID: 12716 RVA: 0x000A1680 File Offset: 0x0009F880
		private void CreateNumericPager(TableRow row, PagedDataSource pagedDataSource, bool addFirstLastPageButtons)
		{
			PagerSettings pagerSettings = this.PagerSettings;
			int pageCount = pagedDataSource.PageCount;
			int num = pagedDataSource.CurrentPageIndex + 1;
			int pageButtonCount = pagerSettings.PageButtonCount;
			int num2 = pageButtonCount;
			int num3 = this.FirstDisplayedPageIndex + 1;
			if (pageCount < num2)
			{
				num2 = pageCount;
			}
			int num4 = 1;
			int num5 = num2;
			if (num > num5)
			{
				int num6 = (num - 1) / pageButtonCount;
				bool flag = num - num3 >= 0 && num - num3 < pageButtonCount;
				if (num3 > 0 && flag)
				{
					num4 = num3;
				}
				else
				{
					num4 = num6 * pageButtonCount + 1;
				}
				num5 = num4 + pageButtonCount - 1;
				if (num5 > pageCount)
				{
					num5 = pageCount;
				}
				if (num5 - num4 + 1 < pageButtonCount)
				{
					num4 = Math.Max(1, num5 - pageButtonCount + 1);
				}
				this.FirstDisplayedPageIndex = num4 - 1;
			}
			if (addFirstLastPageButtons && num != 1 && num4 != 1)
			{
				string firstPageImageUrl = pagerSettings.FirstPageImageUrl;
				TableCell tableCell = new TableCell();
				row.Cells.Add(tableCell);
				IButtonControl buttonControl;
				if (firstPageImageUrl.Length > 0)
				{
					buttonControl = new DataControlImageButton(this);
					((ImageButton)buttonControl).ImageUrl = firstPageImageUrl;
					((ImageButton)buttonControl).AlternateText = HttpUtility.HtmlDecode(pagerSettings.FirstPageText);
				}
				else
				{
					buttonControl = new DataControlPagerLinkButton(this);
					((DataControlPagerLinkButton)buttonControl).Text = pagerSettings.FirstPageText;
				}
				buttonControl.CommandName = "Page";
				buttonControl.CommandArgument = "First";
				tableCell.Controls.Add((Control)buttonControl);
			}
			if (num4 != 1)
			{
				TableCell tableCell2 = new TableCell();
				row.Cells.Add(tableCell2);
				LinkButton linkButton = new DataControlPagerLinkButton(this);
				linkButton.Text = "...";
				linkButton.CommandName = "Page";
				linkButton.CommandArgument = (num4 - 1).ToString(NumberFormatInfo.InvariantInfo);
				tableCell2.Controls.Add(linkButton);
			}
			for (int i = num4; i <= num5; i++)
			{
				TableCell tableCell3 = new TableCell();
				row.Cells.Add(tableCell3);
				string text = i.ToString(NumberFormatInfo.InvariantInfo);
				if (i == num)
				{
					Label label = new Label();
					label.Text = text;
					tableCell3.Controls.Add(label);
				}
				else
				{
					LinkButton linkButton = new DataControlPagerLinkButton(this);
					linkButton.Text = text;
					linkButton.CommandName = "Page";
					linkButton.CommandArgument = text;
					tableCell3.Controls.Add(linkButton);
				}
			}
			if (pageCount > num5)
			{
				TableCell tableCell4 = new TableCell();
				row.Cells.Add(tableCell4);
				LinkButton linkButton = new DataControlPagerLinkButton(this);
				linkButton.Text = "...";
				linkButton.CommandName = "Page";
				linkButton.CommandArgument = (num5 + 1).ToString(NumberFormatInfo.InvariantInfo);
				tableCell4.Controls.Add(linkButton);
			}
			bool flag2 = num5 == pageCount;
			if (addFirstLastPageButtons && num != pageCount && !flag2)
			{
				string lastPageImageUrl = pagerSettings.LastPageImageUrl;
				TableCell tableCell5 = new TableCell();
				row.Cells.Add(tableCell5);
				IButtonControl buttonControl2;
				if (lastPageImageUrl.Length > 0)
				{
					buttonControl2 = new DataControlImageButton(this);
					((ImageButton)buttonControl2).ImageUrl = lastPageImageUrl;
					((ImageButton)buttonControl2).AlternateText = HttpUtility.HtmlDecode(pagerSettings.LastPageText);
				}
				else
				{
					buttonControl2 = new DataControlPagerLinkButton(this);
					((DataControlPagerLinkButton)buttonControl2).Text = pagerSettings.LastPageText;
				}
				buttonControl2.CommandName = "Page";
				buttonControl2.CommandArgument = "Last";
				tableCell5.Controls.Add((Control)buttonControl2);
			}
		}

		// Token: 0x060031AD RID: 12717 RVA: 0x000A19EC File Offset: 0x0009FBEC
		private PagedDataSource CreatePagedDataSource()
		{
			return new PagedDataSource
			{
				CurrentPageIndex = this.PageIndex,
				PageSize = 1,
				AllowPaging = this.AllowPaging,
				AllowCustomPaging = false,
				AllowServerPaging = false,
				VirtualCount = 0
			};
		}

		// Token: 0x060031AE RID: 12718 RVA: 0x000A1A34 File Offset: 0x0009FC34
		private PagedDataSource CreateServerPagedDataSource(int totalRowCount)
		{
			return new PagedDataSource
			{
				CurrentPageIndex = this.PageIndex,
				PageSize = 1,
				AllowPaging = this.AllowPaging,
				AllowCustomPaging = false,
				AllowServerPaging = true,
				VirtualCount = totalRowCount
			};
		}

		// Token: 0x060031AF RID: 12719 RVA: 0x000A1A7C File Offset: 0x0009FC7C
		private FormViewRow CreateRow(int itemIndex, DataControlRowType rowType, DataControlRowState rowState, TableRowCollection rows, PagedDataSource pagedDataSource)
		{
			FormViewRow formViewRow = this.CreateRow(itemIndex, rowType, rowState);
			formViewRow.RenderTemplateContainer = this.RenderOuterTable;
			rows.Add(formViewRow);
			if (rowType != DataControlRowType.Pager)
			{
				this.InitializeRow(formViewRow);
			}
			else
			{
				this.InitializePager(formViewRow, pagedDataSource);
			}
			return formViewRow;
		}

		// Token: 0x060031B0 RID: 12720 RVA: 0x000A1ABF File Offset: 0x0009FCBF
		protected virtual FormViewRow CreateRow(int itemIndex, DataControlRowType rowType, DataControlRowState rowState)
		{
			if (rowType == DataControlRowType.Pager)
			{
				return new FormViewPagerRow(itemIndex, rowType, rowState);
			}
			return new FormViewRow(itemIndex, rowType, rowState);
		}

		// Token: 0x060031B1 RID: 12721 RVA: 0x0009BFF0 File Offset: 0x0009A1F0
		protected virtual Table CreateTable()
		{
			return new ChildTable(string.IsNullOrEmpty(this.ID) ? null : this.ClientID);
		}

		// Token: 0x060031B2 RID: 12722 RVA: 0x0009C00D File Offset: 0x0009A20D
		public sealed override void DataBind()
		{
			base.DataBind();
		}

		// Token: 0x060031B3 RID: 12723 RVA: 0x000A1AD6 File Offset: 0x0009FCD6
		public virtual void DeleteItem()
		{
			this.ResetModelValidationGroup(this.EnableModelValidation, string.Empty);
			this.HandleDelete(string.Empty);
		}

		// Token: 0x060031B4 RID: 12724 RVA: 0x000A1AF4 File Offset: 0x0009FCF4
		protected override void EnsureDataBound()
		{
			if (base.RequiresDataBinding && this.Mode == FormViewMode.Insert)
			{
				this.OnDataBinding(EventArgs.Empty);
				base.RequiresDataBinding = false;
				base.MarkAsDataBound();
				if (base.AdapterInternal != null)
				{
					DataBoundControlAdapter dataBoundControlAdapter = base.AdapterInternal as DataBoundControlAdapter;
					if (dataBoundControlAdapter != null)
					{
						dataBoundControlAdapter.PerformDataBinding(null);
					}
					else
					{
						this.PerformDataBinding(null);
					}
				}
				else
				{
					this.PerformDataBinding(null);
				}
				this.OnDataBound(EventArgs.Empty);
				return;
			}
			base.EnsureDataBound();
		}

		// Token: 0x060031B5 RID: 12725 RVA: 0x000A1B6C File Offset: 0x0009FD6C
		protected virtual void ExtractRowValues(IOrderedDictionary fieldValues, bool includeKeys)
		{
			if (fieldValues == null)
			{
				return;
			}
			DataBoundControlHelper.ExtractValuesFromBindableControls(fieldValues, this);
			IBindableTemplate bindableTemplate = null;
			if (this.Mode == FormViewMode.ReadOnly && this.ItemTemplate != null)
			{
				bindableTemplate = (this.ItemTemplate as IBindableTemplate);
			}
			else if ((this.Mode == FormViewMode.Edit || (this.Mode == FormViewMode.Insert && this.InsertItemTemplate == null)) && this.EditItemTemplate != null)
			{
				bindableTemplate = (this.EditItemTemplate as IBindableTemplate);
			}
			else if (this.Mode == FormViewMode.Insert && this.InsertItemTemplate != null)
			{
				bindableTemplate = (this.InsertItemTemplate as IBindableTemplate);
			}
			string[] dataKeyNamesInternal = this.DataKeyNamesInternal;
			if (bindableTemplate != null && this != null && bindableTemplate != null)
			{
				foreach (object obj in bindableTemplate.ExtractValues(this))
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					if (!includeKeys)
					{
						object[] array = dataKeyNamesInternal;
						if (Array.IndexOf<object>(array, dictionaryEntry.Key) != -1)
						{
							continue;
						}
					}
					fieldValues[dictionaryEntry.Key] = dictionaryEntry.Value;
				}
			}
		}

		// Token: 0x060031B6 RID: 12726 RVA: 0x000A1C78 File Offset: 0x0009FE78
		private void HandleCancel()
		{
			bool isDataBindingAutomatic = base.IsDataBindingAutomatic;
			FormViewModeEventArgs formViewModeEventArgs = new FormViewModeEventArgs(this.DefaultMode, true);
			this.OnModeChanging(formViewModeEventArgs);
			if (formViewModeEventArgs.Cancel)
			{
				return;
			}
			if (isDataBindingAutomatic)
			{
				this.Mode = formViewModeEventArgs.NewMode;
				this.OnModeChanged(EventArgs.Empty);
			}
			base.RequiresDataBinding = true;
		}

		// Token: 0x060031B7 RID: 12727 RVA: 0x000A1CCC File Offset: 0x0009FECC
		private void HandleDelete(string commandArg)
		{
			int pageIndex = this.PageIndex;
			if (pageIndex < 0)
			{
				return;
			}
			DataSourceView dataSourceView = null;
			int pageIndex2 = this.PageIndex;
			bool isDataBindingAutomatic = base.IsDataBindingAutomatic;
			if (isDataBindingAutomatic)
			{
				dataSourceView = this.GetData();
				if (dataSourceView == null)
				{
					throw new HttpException(SR.GetString("View_DataSourceReturnedNullView", new object[]
					{
						this.ID
					}));
				}
			}
			FormViewDeleteEventArgs formViewDeleteEventArgs = new FormViewDeleteEventArgs(pageIndex2);
			this.ExtractRowValues(formViewDeleteEventArgs.Values, false);
			foreach (object obj in this.DataKey.Values)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				formViewDeleteEventArgs.Keys.Add(dictionaryEntry.Key, dictionaryEntry.Value);
				if (formViewDeleteEventArgs.Values.Contains(dictionaryEntry.Key))
				{
					formViewDeleteEventArgs.Values.Remove(dictionaryEntry.Key);
				}
			}
			this.OnItemDeleting(formViewDeleteEventArgs);
			if (formViewDeleteEventArgs.Cancel)
			{
				return;
			}
			if (isDataBindingAutomatic)
			{
				this._deleteKeys = formViewDeleteEventArgs.Keys;
				this._deleteValues = formViewDeleteEventArgs.Values;
				dataSourceView.Delete(formViewDeleteEventArgs.Keys, formViewDeleteEventArgs.Values, new DataSourceViewOperationCallback(this.HandleDeleteCallback));
			}
		}

		// Token: 0x060031B8 RID: 12728 RVA: 0x000A1E1C File Offset: 0x000A001C
		private bool HandleDeleteCallback(int affectedRows, Exception ex)
		{
			int pageIndex = this.PageIndex;
			FormViewDeletedEventArgs formViewDeletedEventArgs = new FormViewDeletedEventArgs(affectedRows, ex);
			formViewDeletedEventArgs.SetKeys(this._deleteKeys);
			formViewDeletedEventArgs.SetValues(this._deleteValues);
			this.OnItemDeleted(formViewDeletedEventArgs);
			this._deleteKeys = null;
			this._deleteValues = null;
			if (ex != null && !formViewDeletedEventArgs.ExceptionHandled && this.PageIsValidAfterModelException())
			{
				return false;
			}
			if (pageIndex == this._pageCount - 1)
			{
				this.HandlePage(pageIndex - 1);
			}
			base.RequiresDataBinding = true;
			return true;
		}

		// Token: 0x060031B9 RID: 12729 RVA: 0x000A1E98 File Offset: 0x000A0098
		private void HandleEdit()
		{
			if (this.PageIndex < 0)
			{
				return;
			}
			FormViewModeEventArgs formViewModeEventArgs = new FormViewModeEventArgs(FormViewMode.Edit, false);
			this.OnModeChanging(formViewModeEventArgs);
			if (formViewModeEventArgs.Cancel)
			{
				return;
			}
			if (base.IsDataBindingAutomatic)
			{
				this.Mode = formViewModeEventArgs.NewMode;
				this.OnModeChanged(EventArgs.Empty);
			}
			base.RequiresDataBinding = true;
		}

		// Token: 0x060031BA RID: 12730 RVA: 0x000A1EF0 File Offset: 0x000A00F0
		private bool HandleEvent(EventArgs e, bool causesValidation, string validationGroup)
		{
			bool result = false;
			this.ResetModelValidationGroup(causesValidation, validationGroup);
			FormViewCommandEventArgs formViewCommandEventArgs = e as FormViewCommandEventArgs;
			if (formViewCommandEventArgs != null)
			{
				this.OnItemCommand(formViewCommandEventArgs);
				if (formViewCommandEventArgs.Handled)
				{
					return true;
				}
				result = true;
				string commandName = formViewCommandEventArgs.CommandName;
				int num = this.PageIndex;
				if (StringUtil.EqualsIgnoreCase(commandName, "Page"))
				{
					string text = (string)formViewCommandEventArgs.CommandArgument;
					if (StringUtil.EqualsIgnoreCase(text, "Next"))
					{
						num++;
					}
					else if (StringUtil.EqualsIgnoreCase(text, "Prev"))
					{
						num--;
					}
					else if (StringUtil.EqualsIgnoreCase(text, "First"))
					{
						num = 0;
					}
					else if (StringUtil.EqualsIgnoreCase(text, "Last"))
					{
						num = this.PageCount - 1;
					}
					else
					{
						num = Convert.ToInt32(text, CultureInfo.InvariantCulture) - 1;
					}
					this.HandlePage(num);
				}
				else if (StringUtil.EqualsIgnoreCase(commandName, "Edit"))
				{
					this.HandleEdit();
				}
				else if (StringUtil.EqualsIgnoreCase(commandName, "Update"))
				{
					this.HandleUpdate((string)formViewCommandEventArgs.CommandArgument, causesValidation);
				}
				else if (StringUtil.EqualsIgnoreCase(commandName, "Cancel"))
				{
					this.HandleCancel();
				}
				else if (StringUtil.EqualsIgnoreCase(commandName, "Delete"))
				{
					this.HandleDelete((string)formViewCommandEventArgs.CommandArgument);
				}
				else if (StringUtil.EqualsIgnoreCase(commandName, "Insert"))
				{
					this.HandleInsert((string)formViewCommandEventArgs.CommandArgument, causesValidation);
				}
				else if (StringUtil.EqualsIgnoreCase(commandName, "New"))
				{
					this.HandleNew();
				}
				else
				{
					result = this.HandleCommand(commandName);
				}
			}
			return result;
		}

		// Token: 0x060031BB RID: 12731 RVA: 0x000A206C File Offset: 0x000A026C
		private bool HandleCommand(string commandName)
		{
			DataSourceView dataSourceView = null;
			if (!base.IsDataBindingAutomatic)
			{
				return false;
			}
			dataSourceView = this.GetData();
			if (dataSourceView == null)
			{
				throw new HttpException(SR.GetString("View_DataSourceReturnedNullView", new object[]
				{
					this.ID
				}));
			}
			if (!dataSourceView.CanExecute(commandName))
			{
				return false;
			}
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			OrderedDictionary orderedDictionary2 = new OrderedDictionary();
			this.ExtractRowValues(orderedDictionary, false);
			foreach (object obj in this.DataKey.Values)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				orderedDictionary2.Add(dictionaryEntry.Key, dictionaryEntry.Value);
				if (orderedDictionary.Contains(dictionaryEntry.Key))
				{
					orderedDictionary.Remove(dictionaryEntry.Key);
				}
			}
			dataSourceView.ExecuteCommand(commandName, orderedDictionary2, orderedDictionary, new DataSourceViewOperationCallback(this.HandleCommandCallback));
			return true;
		}

		// Token: 0x060031BC RID: 12732 RVA: 0x000A2164 File Offset: 0x000A0364
		private bool HandleCommandCallback(int affectedRows, Exception ex)
		{
			if (ex != null && this.PageIsValidAfterModelException())
			{
				return false;
			}
			base.RequiresDataBinding = true;
			return true;
		}

		// Token: 0x060031BD RID: 12733 RVA: 0x000A217C File Offset: 0x000A037C
		private void HandleInsert(string commandArg, bool causesValidation)
		{
			if (causesValidation && this.Page != null && !this.Page.IsValid)
			{
				return;
			}
			if (this.Mode != FormViewMode.Insert)
			{
				throw new HttpException(SR.GetString("DetailsViewFormView_ControlMustBeInInsertMode", new object[]
				{
					"FormView",
					this.ID
				}));
			}
			DataSourceView dataSourceView = null;
			bool isDataBindingAutomatic = base.IsDataBindingAutomatic;
			if (isDataBindingAutomatic)
			{
				dataSourceView = this.GetData();
				if (dataSourceView == null)
				{
					throw new HttpException(SR.GetString("View_DataSourceReturnedNullView", new object[]
					{
						this.ID
					}));
				}
			}
			FormViewInsertEventArgs formViewInsertEventArgs = new FormViewInsertEventArgs(commandArg);
			this.ExtractRowValues(formViewInsertEventArgs.Values, true);
			this.OnItemInserting(formViewInsertEventArgs);
			if (formViewInsertEventArgs.Cancel)
			{
				return;
			}
			if (isDataBindingAutomatic)
			{
				this._insertValues = formViewInsertEventArgs.Values;
				dataSourceView.Insert(formViewInsertEventArgs.Values, new DataSourceViewOperationCallback(this.HandleInsertCallback));
			}
		}

		// Token: 0x060031BE RID: 12734 RVA: 0x000A2254 File Offset: 0x000A0454
		private bool HandleInsertCallback(int affectedRows, Exception ex)
		{
			FormViewInsertedEventArgs formViewInsertedEventArgs = new FormViewInsertedEventArgs(affectedRows, ex);
			formViewInsertedEventArgs.SetValues(this._insertValues);
			this.OnItemInserted(formViewInsertedEventArgs);
			this._insertValues = null;
			if (ex != null && !formViewInsertedEventArgs.ExceptionHandled)
			{
				if (this.PageIsValidAfterModelException())
				{
					return false;
				}
				formViewInsertedEventArgs.KeepInInsertMode = true;
			}
			if (this.IsUsingModelBinders && !this.Page.ModelState.IsValid)
			{
				formViewInsertedEventArgs.KeepInInsertMode = true;
			}
			if (!formViewInsertedEventArgs.KeepInInsertMode)
			{
				FormViewModeEventArgs formViewModeEventArgs = new FormViewModeEventArgs(this.DefaultMode, false);
				this.OnModeChanging(formViewModeEventArgs);
				if (!formViewModeEventArgs.Cancel)
				{
					this.Mode = formViewModeEventArgs.NewMode;
					this.OnModeChanged(EventArgs.Empty);
					base.RequiresDataBinding = true;
				}
			}
			return true;
		}

		// Token: 0x060031BF RID: 12735 RVA: 0x000A2304 File Offset: 0x000A0504
		private void HandleNew()
		{
			FormViewModeEventArgs formViewModeEventArgs = new FormViewModeEventArgs(FormViewMode.Insert, false);
			this.OnModeChanging(formViewModeEventArgs);
			if (formViewModeEventArgs.Cancel)
			{
				return;
			}
			if (base.IsDataBindingAutomatic)
			{
				this.Mode = formViewModeEventArgs.NewMode;
				this.OnModeChanged(EventArgs.Empty);
			}
			base.RequiresDataBinding = true;
		}

		// Token: 0x060031C0 RID: 12736 RVA: 0x000A2350 File Offset: 0x000A0550
		private void HandlePage(int newPage)
		{
			if (!this.AllowPaging)
			{
				return;
			}
			if (this.PageIndex < 0)
			{
				return;
			}
			FormViewPageEventArgs formViewPageEventArgs = new FormViewPageEventArgs(newPage);
			this.OnPageIndexChanging(formViewPageEventArgs);
			if (formViewPageEventArgs.Cancel)
			{
				return;
			}
			if (formViewPageEventArgs.NewPageIndex <= -1)
			{
				return;
			}
			if (formViewPageEventArgs.NewPageIndex >= this.PageCount && this._pageIndex == this.PageCount - 1)
			{
				return;
			}
			this._keyTable = null;
			this._pageIndex = formViewPageEventArgs.NewPageIndex;
			this.OnPageIndexChanged(EventArgs.Empty);
			base.RequiresDataBinding = true;
		}

		// Token: 0x060031C1 RID: 12737 RVA: 0x000A23D8 File Offset: 0x000A05D8
		private void HandleUpdate(string commandArg, bool causesValidation)
		{
			if (causesValidation && this.Page != null && !this.Page.IsValid)
			{
				return;
			}
			if (this.Mode != FormViewMode.Edit)
			{
				throw new HttpException(SR.GetString("DetailsViewFormView_ControlMustBeInEditMode", new object[]
				{
					"FormView",
					this.ID
				}));
			}
			if (this.PageIndex < 0)
			{
				return;
			}
			DataSourceView dataSourceView = null;
			bool isDataBindingAutomatic = base.IsDataBindingAutomatic;
			if (isDataBindingAutomatic)
			{
				dataSourceView = this.GetData();
				if (dataSourceView == null)
				{
					throw new HttpException(SR.GetString("View_DataSourceReturnedNullView", new object[]
					{
						this.ID
					}));
				}
			}
			FormViewUpdateEventArgs formViewUpdateEventArgs = new FormViewUpdateEventArgs(commandArg);
			foreach (object obj in this.BoundFieldValues)
			{
				DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
				formViewUpdateEventArgs.OldValues.Add(dictionaryEntry.Key, dictionaryEntry.Value);
			}
			this.ExtractRowValues(formViewUpdateEventArgs.NewValues, true);
			foreach (object obj2 in this.DataKey.Values)
			{
				DictionaryEntry dictionaryEntry2 = (DictionaryEntry)obj2;
				formViewUpdateEventArgs.Keys.Add(dictionaryEntry2.Key, dictionaryEntry2.Value);
			}
			this.OnItemUpdating(formViewUpdateEventArgs);
			if (formViewUpdateEventArgs.Cancel)
			{
				return;
			}
			if (isDataBindingAutomatic)
			{
				this._updateKeys = formViewUpdateEventArgs.Keys;
				this._updateOldValues = formViewUpdateEventArgs.OldValues;
				this._updateNewValues = formViewUpdateEventArgs.NewValues;
				dataSourceView.Update(formViewUpdateEventArgs.Keys, formViewUpdateEventArgs.NewValues, formViewUpdateEventArgs.OldValues, new DataSourceViewOperationCallback(this.HandleUpdateCallback));
			}
		}

		// Token: 0x060031C2 RID: 12738 RVA: 0x000A25A8 File Offset: 0x000A07A8
		private bool HandleUpdateCallback(int affectedRows, Exception ex)
		{
			FormViewUpdatedEventArgs formViewUpdatedEventArgs = new FormViewUpdatedEventArgs(affectedRows, ex);
			formViewUpdatedEventArgs.SetOldValues(this._updateOldValues);
			formViewUpdatedEventArgs.SetNewValues(this._updateNewValues);
			formViewUpdatedEventArgs.SetKeys(this._updateKeys);
			this.OnItemUpdated(formViewUpdatedEventArgs);
			this._updateKeys = null;
			this._updateOldValues = null;
			this._updateNewValues = null;
			if (ex != null && !formViewUpdatedEventArgs.ExceptionHandled)
			{
				if (this.PageIsValidAfterModelException())
				{
					return false;
				}
				formViewUpdatedEventArgs.KeepInEditMode = true;
			}
			if (this.IsUsingModelBinders && !this.Page.ModelState.IsValid)
			{
				formViewUpdatedEventArgs.KeepInEditMode = true;
			}
			if (!formViewUpdatedEventArgs.KeepInEditMode)
			{
				FormViewModeEventArgs formViewModeEventArgs = new FormViewModeEventArgs(this.DefaultMode, false);
				this.OnModeChanging(formViewModeEventArgs);
				if (!formViewModeEventArgs.Cancel)
				{
					this.Mode = formViewModeEventArgs.NewMode;
					this.OnModeChanged(EventArgs.Empty);
					base.RequiresDataBinding = true;
				}
			}
			return true;
		}

		// Token: 0x060031C3 RID: 12739 RVA: 0x000A2680 File Offset: 0x000A0880
		protected virtual void InitializePager(FormViewRow row, PagedDataSource pagedDataSource)
		{
			TableCell tableCell = new TableCell();
			PagerSettings pagerSettings = this.PagerSettings;
			if (this._pagerTemplate != null)
			{
				this._pagerTemplate.InstantiateIn(tableCell);
			}
			else
			{
				PagerTable pagerTable = new PagerTable();
				TableRow row2 = new TableRow();
				tableCell.Controls.Add(pagerTable);
				pagerTable.Rows.Add(row2);
				switch (pagerSettings.Mode)
				{
				case PagerButtons.NextPrevious:
					this.CreateNextPrevPager(row2, pagedDataSource, false);
					break;
				case PagerButtons.Numeric:
					this.CreateNumericPager(row2, pagedDataSource, false);
					break;
				case PagerButtons.NextPreviousFirstLast:
					this.CreateNextPrevPager(row2, pagedDataSource, true);
					break;
				case PagerButtons.NumericFirstLast:
					this.CreateNumericPager(row2, pagedDataSource, true);
					break;
				}
			}
			tableCell.ColumnSpan = 2;
			row.Cells.Add(tableCell);
		}

		// Token: 0x060031C4 RID: 12740 RVA: 0x000A2734 File Offset: 0x000A0934
		protected virtual void InitializeRow(FormViewRow row)
		{
			TableCellCollection cells = row.Cells;
			TableCell tableCell = new TableCell();
			ITemplate template = this._itemTemplate;
			int itemIndex = row.ItemIndex;
			DataControlRowState rowState = row.RowState;
			switch (row.RowType)
			{
			case DataControlRowType.Header:
			{
				template = this._headerTemplate;
				tableCell.ColumnSpan = 2;
				string headerText = this.HeaderText;
				if (this._headerTemplate == null && headerText.Length > 0)
				{
					tableCell.Text = headerText;
				}
				break;
			}
			case DataControlRowType.Footer:
			{
				template = this._footerTemplate;
				tableCell.ColumnSpan = 2;
				string footerText = this.FooterText;
				if (this._footerTemplate == null && footerText.Length > 0)
				{
					tableCell.Text = footerText;
				}
				break;
			}
			case DataControlRowType.DataRow:
				tableCell.ColumnSpan = 2;
				if ((rowState & DataControlRowState.Edit) != DataControlRowState.Normal && this._editItemTemplate != null)
				{
					template = this._editItemTemplate;
				}
				if ((rowState & DataControlRowState.Insert) != DataControlRowState.Normal)
				{
					if (this._insertItemTemplate != null)
					{
						template = this._insertItemTemplate;
					}
					else
					{
						template = this._editItemTemplate;
					}
				}
				break;
			case DataControlRowType.EmptyDataRow:
			{
				template = this._emptyDataTemplate;
				string emptyDataText = this.EmptyDataText;
				if (this._emptyDataTemplate == null && emptyDataText.Length > 0)
				{
					tableCell.Text = emptyDataText;
				}
				break;
			}
			}
			if (template != null)
			{
				template.InstantiateIn(tableCell);
			}
			cells.Add(tableCell);
		}

		// Token: 0x060031C5 RID: 12741 RVA: 0x000A2874 File Offset: 0x000A0A74
		public virtual void InsertItem(bool causesValidation)
		{
			this.ResetModelValidationGroup(causesValidation, string.Empty);
			this.HandleInsert(string.Empty, causesValidation);
		}

		// Token: 0x060031C6 RID: 12742 RVA: 0x0009CFF3 File Offset: 0x0009B1F3
		public virtual bool IsBindableType(Type type)
		{
			return DataBoundControlHelper.IsBindableType(type, this.RenderingCompatibility >= VersionUtil.Framework45);
		}

		// Token: 0x060031C7 RID: 12743 RVA: 0x000A2890 File Offset: 0x000A0A90
		protected internal override void LoadControlState(object savedState)
		{
			this._pageIndex = 0;
			this._defaultMode = FormViewMode.ReadOnly;
			this._dataKeyNames = new string[0];
			this._pageCount = 0;
			object[] array = savedState as object[];
			if (array != null)
			{
				base.LoadControlState(array[0]);
				if (array[1] != null)
				{
					this._pageIndex = (int)array[1];
				}
				if (array[2] != null)
				{
					this._defaultMode = (FormViewMode)array[2];
				}
				if (array[3] != null)
				{
					this.Mode = (FormViewMode)array[3];
				}
				if (array[4] != null)
				{
					this._dataKeyNames = (string[])array[4];
				}
				if (array[5] != null)
				{
					this.KeyTable.Clear();
					OrderedDictionaryStateHelper.LoadViewState(this.KeyTable, (ArrayList)array[5]);
				}
				if (array[6] != null)
				{
					this._pageCount = (int)array[6];
					return;
				}
			}
			else
			{
				base.LoadControlState(null);
			}
		}

		// Token: 0x060031C8 RID: 12744 RVA: 0x000A2960 File Offset: 0x000A0B60
		protected override void LoadViewState(object savedState)
		{
			if (savedState != null)
			{
				object[] array = (object[])savedState;
				base.LoadViewState(array[0]);
				if (array[1] != null)
				{
					((IStateManager)this.PagerStyle).LoadViewState(array[1]);
				}
				if (array[2] != null)
				{
					((IStateManager)this.HeaderStyle).LoadViewState(array[2]);
				}
				if (array[3] != null)
				{
					((IStateManager)this.FooterStyle).LoadViewState(array[3]);
				}
				if (array[4] != null)
				{
					((IStateManager)this.RowStyle).LoadViewState(array[4]);
				}
				if (array[5] != null)
				{
					((IStateManager)this.EditRowStyle).LoadViewState(array[5]);
				}
				if (array[6] != null)
				{
					((IStateManager)this.InsertRowStyle).LoadViewState(array[6]);
				}
				if (array[7] != null)
				{
					OrderedDictionaryStateHelper.LoadViewState((OrderedDictionary)this.BoundFieldValues, (ArrayList)array[7]);
				}
				if (array[8] != null)
				{
					((IStateManager)this.PagerSettings).LoadViewState(array[8]);
				}
				if (array[9] != null)
				{
					((IStateManager)base.ControlStyle).LoadViewState(array[9]);
					return;
				}
			}
			else
			{
				base.LoadViewState(null);
			}
		}

		// Token: 0x060031C9 RID: 12745 RVA: 0x000A2A44 File Offset: 0x000A0C44
		protected internal virtual string ModifiedOuterTableStylePropertyName()
		{
			if (!string.IsNullOrEmpty(this.BackImageUrl))
			{
				return "BackImageUrl";
			}
			if (this.CellPadding != -1)
			{
				return "CellPadding";
			}
			if (this.CellSpacing != 0)
			{
				return "CellSpacing";
			}
			if (this.GridLines != GridLines.None)
			{
				return "GridLines";
			}
			if (this.HorizontalAlign != HorizontalAlign.NotSet)
			{
				return "HorizontalAlign";
			}
			if (this.Font.Bold || this.Font.Italic || !string.IsNullOrEmpty(this.Font.Name) || this.Font.Names.Length != 0 || this.Font.Overline || this.Font.Size != FontUnit.Empty || this.Font.Strikeout || this.Font.Underline)
			{
				return "Font";
			}
			return LoginUtil.ModifiedOuterTableBasicStylePropertyName(this);
		}

		// Token: 0x060031CA RID: 12746 RVA: 0x000A2B24 File Offset: 0x000A0D24
		protected override bool OnBubbleEvent(object source, EventArgs e)
		{
			bool causesValidation = false;
			string validationGroup = string.Empty;
			FormViewCommandEventArgs formViewCommandEventArgs = e as FormViewCommandEventArgs;
			if (formViewCommandEventArgs != null)
			{
				IButtonControl buttonControl = formViewCommandEventArgs.CommandSource as IButtonControl;
				if (buttonControl != null)
				{
					causesValidation = buttonControl.CausesValidation;
					validationGroup = buttonControl.ValidationGroup;
				}
			}
			return this.HandleEvent(e, causesValidation, validationGroup);
		}

		// Token: 0x060031CB RID: 12747 RVA: 0x000A2B6C File Offset: 0x000A0D6C
		protected virtual void OnPageIndexChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[FormView.EventPageIndexChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060031CC RID: 12748 RVA: 0x000A2B9C File Offset: 0x000A0D9C
		protected virtual void OnPageIndexChanging(FormViewPageEventArgs e)
		{
			bool isDataBindingAutomatic = base.IsDataBindingAutomatic;
			FormViewPageEventHandler formViewPageEventHandler = (FormViewPageEventHandler)base.Events[FormView.EventPageIndexChanging];
			if (formViewPageEventHandler != null)
			{
				formViewPageEventHandler(this, e);
				return;
			}
			if (!isDataBindingAutomatic && !e.Cancel)
			{
				throw new HttpException(SR.GetString("FormView_UnhandledEvent", new object[]
				{
					this.ID,
					"PageIndexChanging"
				}));
			}
		}

		// Token: 0x060031CD RID: 12749 RVA: 0x000A2C04 File Offset: 0x000A0E04
		protected internal override void OnInit(EventArgs e)
		{
			base.OnInit(e);
			if (this.Page != null)
			{
				if (this.DataKeyNames.Length != 0)
				{
					this.Page.RegisterRequiresViewStateEncryption();
				}
				this.Page.RegisterRequiresControlState(this);
			}
			if (!base.DesignMode && !string.IsNullOrEmpty(this.ItemType))
			{
				DataBoundControlHelper.EnableDynamicData(this, this.ItemType);
			}
		}

		// Token: 0x060031CE RID: 12750 RVA: 0x000A2C64 File Offset: 0x000A0E64
		protected virtual void OnItemCommand(FormViewCommandEventArgs e)
		{
			FormViewCommandEventHandler formViewCommandEventHandler = (FormViewCommandEventHandler)base.Events[FormView.EventItemCommand];
			if (formViewCommandEventHandler != null)
			{
				formViewCommandEventHandler(this, e);
			}
		}

		// Token: 0x060031CF RID: 12751 RVA: 0x000A2C94 File Offset: 0x000A0E94
		protected virtual void OnItemCreated(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[FormView.EventItemCreated];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060031D0 RID: 12752 RVA: 0x000A2CC4 File Offset: 0x000A0EC4
		protected virtual void OnItemDeleted(FormViewDeletedEventArgs e)
		{
			FormViewDeletedEventHandler formViewDeletedEventHandler = (FormViewDeletedEventHandler)base.Events[FormView.EventItemDeleted];
			if (formViewDeletedEventHandler != null)
			{
				formViewDeletedEventHandler(this, e);
			}
		}

		// Token: 0x060031D1 RID: 12753 RVA: 0x000A2CF4 File Offset: 0x000A0EF4
		protected virtual void OnItemDeleting(FormViewDeleteEventArgs e)
		{
			bool isDataBindingAutomatic = base.IsDataBindingAutomatic;
			FormViewDeleteEventHandler formViewDeleteEventHandler = (FormViewDeleteEventHandler)base.Events[FormView.EventItemDeleting];
			if (formViewDeleteEventHandler != null)
			{
				formViewDeleteEventHandler(this, e);
				return;
			}
			if (!isDataBindingAutomatic && !e.Cancel)
			{
				throw new HttpException(SR.GetString("FormView_UnhandledEvent", new object[]
				{
					this.ID,
					"ItemDeleting"
				}));
			}
		}

		// Token: 0x060031D2 RID: 12754 RVA: 0x000A2D5C File Offset: 0x000A0F5C
		protected virtual void OnItemInserted(FormViewInsertedEventArgs e)
		{
			FormViewInsertedEventHandler formViewInsertedEventHandler = (FormViewInsertedEventHandler)base.Events[FormView.EventItemInserted];
			if (formViewInsertedEventHandler != null)
			{
				formViewInsertedEventHandler(this, e);
			}
		}

		// Token: 0x060031D3 RID: 12755 RVA: 0x000A2D8C File Offset: 0x000A0F8C
		protected virtual void OnItemInserting(FormViewInsertEventArgs e)
		{
			bool isDataBindingAutomatic = base.IsDataBindingAutomatic;
			FormViewInsertEventHandler formViewInsertEventHandler = (FormViewInsertEventHandler)base.Events[FormView.EventItemInserting];
			if (formViewInsertEventHandler != null)
			{
				formViewInsertEventHandler(this, e);
				return;
			}
			if (!isDataBindingAutomatic && !e.Cancel)
			{
				throw new HttpException(SR.GetString("FormView_UnhandledEvent", new object[]
				{
					this.ID,
					"ItemInserting"
				}));
			}
		}

		// Token: 0x060031D4 RID: 12756 RVA: 0x000A2DF4 File Offset: 0x000A0FF4
		protected virtual void OnItemUpdated(FormViewUpdatedEventArgs e)
		{
			FormViewUpdatedEventHandler formViewUpdatedEventHandler = (FormViewUpdatedEventHandler)base.Events[FormView.EventItemUpdated];
			if (formViewUpdatedEventHandler != null)
			{
				formViewUpdatedEventHandler(this, e);
			}
		}

		// Token: 0x060031D5 RID: 12757 RVA: 0x000A2E24 File Offset: 0x000A1024
		protected virtual void OnItemUpdating(FormViewUpdateEventArgs e)
		{
			bool isDataBindingAutomatic = base.IsDataBindingAutomatic;
			FormViewUpdateEventHandler formViewUpdateEventHandler = (FormViewUpdateEventHandler)base.Events[FormView.EventItemUpdating];
			if (formViewUpdateEventHandler != null)
			{
				formViewUpdateEventHandler(this, e);
				return;
			}
			if (!isDataBindingAutomatic && !e.Cancel)
			{
				throw new HttpException(SR.GetString("FormView_UnhandledEvent", new object[]
				{
					this.ID,
					"ItemUpdating"
				}));
			}
		}

		// Token: 0x060031D6 RID: 12758 RVA: 0x000A2E8C File Offset: 0x000A108C
		protected virtual void OnModeChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[FormView.EventModeChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060031D7 RID: 12759 RVA: 0x000A2EBC File Offset: 0x000A10BC
		protected virtual void OnModeChanging(FormViewModeEventArgs e)
		{
			bool isDataBindingAutomatic = base.IsDataBindingAutomatic;
			FormViewModeEventHandler formViewModeEventHandler = (FormViewModeEventHandler)base.Events[FormView.EventModeChanging];
			if (formViewModeEventHandler != null)
			{
				formViewModeEventHandler(this, e);
				return;
			}
			if (!isDataBindingAutomatic && !e.Cancel)
			{
				throw new HttpException(SR.GetString("FormView_UnhandledEvent", new object[]
				{
					this.ID,
					"ModeChanging"
				}));
			}
		}

		// Token: 0x060031D8 RID: 12760 RVA: 0x0009D2FA File Offset: 0x0009B4FA
		private void OnPagerPropertyChanged(object sender, EventArgs e)
		{
			if (base.Initialized)
			{
				base.RequiresDataBinding = true;
			}
		}

		// Token: 0x060031D9 RID: 12761 RVA: 0x000A2F24 File Offset: 0x000A1124
		private bool PageIsValidAfterModelException()
		{
			if (this._modelValidationGroup == null)
			{
				return true;
			}
			this.Page.Validate(this._modelValidationGroup);
			return this.Page.IsValid;
		}

		// Token: 0x060031DA RID: 12762 RVA: 0x000A2F4C File Offset: 0x000A114C
		protected internal override void PerformDataBinding(IEnumerable data)
		{
			base.PerformDataBinding(data);
			if (base.IsDataBindingAutomatic && this.Mode == FormViewMode.Edit && base.IsViewStateEnabled)
			{
				this.ExtractRowValues(this.BoundFieldValues, false);
			}
		}

		// Token: 0x060031DB RID: 12763 RVA: 0x000A2F7C File Offset: 0x000A117C
		protected internal virtual void PrepareControlHierarchy()
		{
			if (this.Controls.Count < 1)
			{
				return;
			}
			Table table = (Table)this.Controls[0];
			table.CopyBaseAttributes(this);
			if (base.ControlStyleCreated && !base.ControlStyle.IsEmpty)
			{
				table.ApplyStyle(base.ControlStyle);
			}
			else
			{
				table.GridLines = GridLines.None;
				table.CellSpacing = 0;
			}
			table.Caption = this.Caption;
			table.CaptionAlign = this.CaptionAlign;
			TableRowCollection rows = table.Rows;
			foreach (object obj in rows)
			{
				FormViewRow formViewRow = (FormViewRow)obj;
				Style style = new TableItemStyle();
				DataControlRowState rowState = formViewRow.RowState;
				switch (formViewRow.RowType)
				{
				case DataControlRowType.Header:
					style = this._headerStyle;
					break;
				case DataControlRowType.Footer:
					style = this._footerStyle;
					break;
				case DataControlRowType.DataRow:
					style.CopyFrom(this._rowStyle);
					if ((rowState & DataControlRowState.Edit) != DataControlRowState.Normal)
					{
						style.CopyFrom(this._editRowStyle);
					}
					if ((rowState & DataControlRowState.Insert) != DataControlRowState.Normal)
					{
						if (this._insertRowStyle != null)
						{
							style.CopyFrom(this._insertRowStyle);
						}
						else
						{
							style.CopyFrom(this._editRowStyle);
						}
					}
					break;
				case DataControlRowType.Pager:
					style = this._pagerStyle;
					break;
				case DataControlRowType.EmptyDataRow:
					style = this._emptyDataRowStyle;
					break;
				}
				if (style != null && formViewRow.Visible)
				{
					formViewRow.MergeStyle(style);
				}
			}
		}

		// Token: 0x060031DC RID: 12764 RVA: 0x000A3104 File Offset: 0x000A1304
		protected virtual void RaisePostBackEvent(string eventArgument)
		{
			base.ValidateEvent(this.UniqueID, eventArgument);
			int num = eventArgument.IndexOf('$');
			if (num < 0)
			{
				return;
			}
			CommandEventArgs originalArgs = new CommandEventArgs(eventArgument.Substring(0, num), eventArgument.Substring(num + 1));
			FormViewCommandEventArgs e = new FormViewCommandEventArgs(this, originalArgs);
			this.HandleEvent(e, false, string.Empty);
		}

		// Token: 0x060031DD RID: 12765 RVA: 0x000A315C File Offset: 0x000A135C
		protected internal override void Render(HtmlTextWriter writer)
		{
			if (this.Page != null)
			{
				this.Page.VerifyRenderingInServerForm(this);
			}
			if (this.RenderOuterTable)
			{
				this.PrepareControlHierarchy();
				this.RenderContents(writer);
				return;
			}
			string text = this.ModifiedOuterTableStylePropertyName();
			if (!string.IsNullOrEmpty(text))
			{
				throw new InvalidOperationException(SR.GetString("IRenderOuterTableControl_CannotSetStyleWhenDisableRenderOuterTable", new object[]
				{
					text,
					base.GetType().Name,
					this.ID
				}));
			}
			if (this.Controls.Count > 0)
			{
				this.Controls[0].RenderChildren(writer);
			}
		}

		// Token: 0x060031DE RID: 12766 RVA: 0x000A31F3 File Offset: 0x000A13F3
		private void ResetModelValidationGroup(bool causesValidation, string validationGroup)
		{
			this._modelValidationGroup = null;
			if (causesValidation && this.Page != null)
			{
				this.Page.Validate(validationGroup);
				if (this.EnableModelValidation)
				{
					this._modelValidationGroup = validationGroup;
				}
			}
		}

		// Token: 0x060031DF RID: 12767 RVA: 0x000A3224 File Offset: 0x000A1424
		protected internal override object SaveControlState()
		{
			object obj = base.SaveControlState();
			if (obj != null || this._pageIndex != 0 || this._mode != this._defaultMode || this._defaultMode != FormViewMode.ReadOnly || (this._dataKeyNames != null && this._dataKeyNames.Length != 0) || (this._keyTable != null && this._keyTable.Count > 0) || this._pageCount != 0)
			{
				object[] array = new object[7];
				object obj2 = null;
				object obj3 = null;
				object obj4 = null;
				object obj5 = null;
				object obj6 = null;
				object obj7 = null;
				if (this._pageIndex != 0)
				{
					obj2 = this._pageIndex;
				}
				if (this._defaultMode != FormViewMode.ReadOnly)
				{
					obj4 = (int)this._defaultMode;
				}
				if (this._mode != this._defaultMode && this._modeSet)
				{
					obj3 = (int)this._mode;
				}
				if (this._dataKeyNames != null && this._dataKeyNames.Length != 0)
				{
					obj5 = this._dataKeyNames;
				}
				if (this._keyTable != null)
				{
					obj6 = OrderedDictionaryStateHelper.SaveViewState(this._keyTable);
				}
				if (this._pageCount != 0)
				{
					obj7 = this._pageCount;
				}
				array[0] = obj;
				array[1] = obj2;
				array[2] = obj4;
				array[3] = obj3;
				array[4] = obj5;
				array[5] = obj6;
				array[6] = obj7;
				return array;
			}
			return true;
		}

		// Token: 0x060031E0 RID: 12768 RVA: 0x000A3358 File Offset: 0x000A1558
		protected override object SaveViewState()
		{
			object obj = base.SaveViewState();
			object obj2 = (this._pagerStyle != null) ? ((IStateManager)this._pagerStyle).SaveViewState() : null;
			object obj3 = (this._headerStyle != null) ? ((IStateManager)this._headerStyle).SaveViewState() : null;
			object obj4 = (this._footerStyle != null) ? ((IStateManager)this._footerStyle).SaveViewState() : null;
			object obj5 = (this._rowStyle != null) ? ((IStateManager)this._rowStyle).SaveViewState() : null;
			object obj6 = (this._editRowStyle != null) ? ((IStateManager)this._editRowStyle).SaveViewState() : null;
			object obj7 = (this._insertRowStyle != null) ? ((IStateManager)this._insertRowStyle).SaveViewState() : null;
			object obj8 = (this._boundFieldValues != null) ? OrderedDictionaryStateHelper.SaveViewState(this._boundFieldValues) : null;
			object obj9 = (this._pagerSettings != null) ? ((IStateManager)this._pagerSettings).SaveViewState() : null;
			object obj10 = base.ControlStyleCreated ? ((IStateManager)base.ControlStyle).SaveViewState() : null;
			return new object[]
			{
				obj,
				obj2,
				obj3,
				obj4,
				obj5,
				obj6,
				obj7,
				obj8,
				obj9,
				obj10
			};
		}

		// Token: 0x060031E1 RID: 12769 RVA: 0x000A3485 File Offset: 0x000A1685
		public void SetPageIndex(int index)
		{
			this.HandlePage(index);
		}

		// Token: 0x060031E2 RID: 12770 RVA: 0x0009E0A7 File Offset: 0x0009C2A7
		private void SelectCallback(IEnumerable data)
		{
			throw new HttpException(SR.GetString("DataBoundControl_DataSourceDoesntSupportPaging"));
		}

		// Token: 0x060031E3 RID: 12771 RVA: 0x000A3490 File Offset: 0x000A1690
		protected override void TrackViewState()
		{
			base.TrackViewState();
			if (this._pagerStyle != null)
			{
				((IStateManager)this._pagerStyle).TrackViewState();
			}
			if (this._headerStyle != null)
			{
				((IStateManager)this._headerStyle).TrackViewState();
			}
			if (this._footerStyle != null)
			{
				((IStateManager)this._footerStyle).TrackViewState();
			}
			if (this._rowStyle != null)
			{
				((IStateManager)this._rowStyle).TrackViewState();
			}
			if (this._editRowStyle != null)
			{
				((IStateManager)this._editRowStyle).TrackViewState();
			}
			if (this._insertRowStyle != null)
			{
				((IStateManager)this._insertRowStyle).TrackViewState();
			}
			if (this._pagerSettings != null)
			{
				((IStateManager)this._pagerSettings).TrackViewState();
			}
			if (base.ControlStyleCreated)
			{
				((IStateManager)base.ControlStyle).TrackViewState();
			}
		}

		// Token: 0x060031E4 RID: 12772 RVA: 0x000A353B File Offset: 0x000A173B
		public virtual void UpdateItem(bool causesValidation)
		{
			this.ResetModelValidationGroup(causesValidation, string.Empty);
			this.HandleUpdate(string.Empty, causesValidation);
		}

		// Token: 0x060031E5 RID: 12773 RVA: 0x000A3558 File Offset: 0x000A1758
		internal override void UpdateModelDataSourceProperties(ModelDataSource modelDataSource)
		{
			string dataKeyName = (this.DataKeyNamesInternal.Length != 0) ? this.DataKeyNamesInternal[0] : "";
			modelDataSource.UpdateProperties(this.ItemType, this.SelectMethod, this.UpdateMethod, this.InsertMethod, this.DeleteMethod, dataKeyName);
		}

		// Token: 0x060031E6 RID: 12774 RVA: 0x000A35A4 File Offset: 0x000A17A4
		PostBackOptions IPostBackContainer.GetPostBackOptions(IButtonControl buttonControl)
		{
			if (buttonControl == null)
			{
				throw new ArgumentNullException("buttonControl");
			}
			if (buttonControl.CausesValidation)
			{
				throw new InvalidOperationException(SR.GetString("CannotUseParentPostBackWhenValidating", new object[]
				{
					base.GetType().Name,
					this.ID
				}));
			}
			return new PostBackOptions(this, buttonControl.CommandName + "$" + buttonControl.CommandArgument)
			{
				RequiresJavaScriptProtocol = true
			};
		}

		// Token: 0x060031E7 RID: 12775 RVA: 0x000A3619 File Offset: 0x000A1819
		void IPostBackEventHandler.RaisePostBackEvent(string eventArgument)
		{
			this.RaisePostBackEvent(eventArgument);
		}

		// Token: 0x17000E5E RID: 3678
		// (get) Token: 0x060031E8 RID: 12776 RVA: 0x000A3622 File Offset: 0x000A1822
		int IDataItemContainer.DataItemIndex
		{
			get
			{
				return this.DataItemIndex;
			}
		}

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x060031E9 RID: 12777 RVA: 0x00007722 File Offset: 0x00005922
		int IDataItemContainer.DisplayIndex
		{
			get
			{
				return 0;
			}
		}

		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x060031EA RID: 12778 RVA: 0x000A362A File Offset: 0x000A182A
		DataKey IDataBoundItemControl.DataKey
		{
			get
			{
				return this.DataKey;
			}
		}

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x060031EB RID: 12779 RVA: 0x000A3634 File Offset: 0x000A1834
		DataBoundControlMode IDataBoundItemControl.Mode
		{
			get
			{
				switch (this.Mode)
				{
				case FormViewMode.ReadOnly:
					return DataBoundControlMode.ReadOnly;
				case FormViewMode.Edit:
					return DataBoundControlMode.Edit;
				case FormViewMode.Insert:
					return DataBoundControlMode.Insert;
				default:
					return DataBoundControlMode.ReadOnly;
				}
			}
		}

		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x060031EC RID: 12780 RVA: 0x0009E2DB File Offset: 0x0009C4DB
		// (set) Token: 0x060031ED RID: 12781 RVA: 0x0009E2E3 File Offset: 0x0009C4E3
		string IDataBoundControl.DataSourceID
		{
			get
			{
				return this.DataSourceID;
			}
			set
			{
				this.DataSourceID = value;
			}
		}

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x060031EE RID: 12782 RVA: 0x0009E2EC File Offset: 0x0009C4EC
		IDataSource IDataBoundControl.DataSourceObject
		{
			get
			{
				return base.DataSourceObject;
			}
		}

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x060031EF RID: 12783 RVA: 0x0009E2F4 File Offset: 0x0009C4F4
		// (set) Token: 0x060031F0 RID: 12784 RVA: 0x0009E2FC File Offset: 0x0009C4FC
		object IDataBoundControl.DataSource
		{
			get
			{
				return this.DataSource;
			}
			set
			{
				this.DataSource = value;
			}
		}

		// Token: 0x17000E65 RID: 3685
		// (get) Token: 0x060031F1 RID: 12785 RVA: 0x000A3663 File Offset: 0x000A1863
		// (set) Token: 0x060031F2 RID: 12786 RVA: 0x000A366B File Offset: 0x000A186B
		string[] IDataBoundControl.DataKeyNames
		{
			get
			{
				return this.DataKeyNames;
			}
			set
			{
				this.DataKeyNames = value;
			}
		}

		// Token: 0x17000E66 RID: 3686
		// (get) Token: 0x060031F3 RID: 12787 RVA: 0x0009E316 File Offset: 0x0009C516
		// (set) Token: 0x060031F4 RID: 12788 RVA: 0x0009E31E File Offset: 0x0009C51E
		string IDataBoundControl.DataMember
		{
			get
			{
				return this.DataMember;
			}
			set
			{
				this.DataMember = value;
			}
		}

		// Token: 0x040020BD RID: 8381
		private static readonly object EventPageIndexChanged = new object();

		// Token: 0x040020BE RID: 8382
		private static readonly object EventPageIndexChanging = new object();

		// Token: 0x040020BF RID: 8383
		private static readonly object EventItemCommand = new object();

		// Token: 0x040020C0 RID: 8384
		private static readonly object EventItemCreated = new object();

		// Token: 0x040020C1 RID: 8385
		private static readonly object EventItemDeleted = new object();

		// Token: 0x040020C2 RID: 8386
		private static readonly object EventItemDeleting = new object();

		// Token: 0x040020C3 RID: 8387
		private static readonly object EventItemInserting = new object();

		// Token: 0x040020C4 RID: 8388
		private static readonly object EventItemInserted = new object();

		// Token: 0x040020C5 RID: 8389
		private static readonly object EventItemUpdating = new object();

		// Token: 0x040020C6 RID: 8390
		private static readonly object EventItemUpdated = new object();

		// Token: 0x040020C7 RID: 8391
		private static readonly object EventModeChanged = new object();

		// Token: 0x040020C8 RID: 8392
		private static readonly object EventModeChanging = new object();

		// Token: 0x040020C9 RID: 8393
		private ITemplate _itemTemplate;

		// Token: 0x040020CA RID: 8394
		private ITemplate _editItemTemplate;

		// Token: 0x040020CB RID: 8395
		private ITemplate _insertItemTemplate;

		// Token: 0x040020CC RID: 8396
		private ITemplate _headerTemplate;

		// Token: 0x040020CD RID: 8397
		private ITemplate _footerTemplate;

		// Token: 0x040020CE RID: 8398
		private ITemplate _pagerTemplate;

		// Token: 0x040020CF RID: 8399
		private ITemplate _emptyDataTemplate;

		// Token: 0x040020D0 RID: 8400
		private TableItemStyle _rowStyle;

		// Token: 0x040020D1 RID: 8401
		private TableItemStyle _headerStyle;

		// Token: 0x040020D2 RID: 8402
		private TableItemStyle _footerStyle;

		// Token: 0x040020D3 RID: 8403
		private TableItemStyle _editRowStyle;

		// Token: 0x040020D4 RID: 8404
		private TableItemStyle _insertRowStyle;

		// Token: 0x040020D5 RID: 8405
		private TableItemStyle _emptyDataRowStyle;

		// Token: 0x040020D6 RID: 8406
		private FormViewRow _bottomPagerRow;

		// Token: 0x040020D7 RID: 8407
		private FormViewRow _footerRow;

		// Token: 0x040020D8 RID: 8408
		private FormViewRow _headerRow;

		// Token: 0x040020D9 RID: 8409
		private FormViewRow _topPagerRow;

		// Token: 0x040020DA RID: 8410
		private FormViewRow _row;

		// Token: 0x040020DB RID: 8411
		private TableItemStyle _pagerStyle;

		// Token: 0x040020DC RID: 8412
		private PagerSettings _pagerSettings;

		// Token: 0x040020DD RID: 8413
		private int _pageCount;

		// Token: 0x040020DE RID: 8414
		private object _dataItem;

		// Token: 0x040020DF RID: 8415
		private int _dataItemIndex;

		// Token: 0x040020E0 RID: 8416
		private OrderedDictionary _boundFieldValues;

		// Token: 0x040020E1 RID: 8417
		private DataKey _dataKey;

		// Token: 0x040020E2 RID: 8418
		private OrderedDictionary _keyTable;

		// Token: 0x040020E3 RID: 8419
		private string[] _dataKeyNames;

		// Token: 0x040020E4 RID: 8420
		private int _pageIndex;

		// Token: 0x040020E5 RID: 8421
		private FormViewMode _defaultMode;

		// Token: 0x040020E6 RID: 8422
		private FormViewMode _mode;

		// Token: 0x040020E7 RID: 8423
		private bool _modeSet;

		// Token: 0x040020E8 RID: 8424
		private bool _useServerPaging;

		// Token: 0x040020E9 RID: 8425
		private string _modelValidationGroup;

		// Token: 0x040020EA RID: 8426
		private IOrderedDictionary _deleteKeys;

		// Token: 0x040020EB RID: 8427
		private IOrderedDictionary _deleteValues;

		// Token: 0x040020EC RID: 8428
		private IOrderedDictionary _insertValues;

		// Token: 0x040020ED RID: 8429
		private IOrderedDictionary _updateKeys;

		// Token: 0x040020EE RID: 8430
		private IOrderedDictionary _updateOldValues;

		// Token: 0x040020EF RID: 8431
		private IOrderedDictionary _updateNewValues;
	}
}

using System;
using System.ComponentModel;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001169 RID: 4457
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class GridScrolling : ObjectWithState
	{
		// Token: 0x0600B5B1 RID: 46513 RVA: 0x0028025F File Offset: 0x0027E45F
		public GridScrolling(StateBag OwnerStateBag, RadGrid ownerGrid) : base("cs_scroll_", OwnerStateBag)
		{
			this.ownerGrid = ownerGrid;
		}

		// Token: 0x17003AC0 RID: 15040
		// (get) Token: 0x0600B5B2 RID: 46514 RVA: 0x00280274 File Offset: 0x0027E474
		// (set) Token: 0x0600B5B3 RID: 46515 RVA: 0x0028029D File Offset: 0x0027E49D
		[Description("RadGrid_AllowScroll")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public virtual bool AllowScroll
		{
			get
			{
				object obj = base.ViewState["AllowScroll"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["AllowScroll"] = value;
			}
		}

		// Token: 0x17003AC1 RID: 15041
		// (get) Token: 0x0600B5B4 RID: 46516 RVA: 0x002802B8 File Offset: 0x0027E4B8
		// (set) Token: 0x0600B5B5 RID: 46517 RVA: 0x002802EA File Offset: 0x0027E4EA
		[Category("Client")]
		[Description("RadGrid_ScrollHeight")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "300px")]
		public virtual Unit ScrollHeight
		{
			get
			{
				object obj = base.ViewState["ScrollHeight"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				return Unit.Pixel(300);
			}
			set
			{
				base.ViewState["ScrollHeight"] = value;
			}
		}

		// Token: 0x17003AC2 RID: 15042
		// (get) Token: 0x0600B5B6 RID: 46518 RVA: 0x00280304 File Offset: 0x0027E504
		// (set) Token: 0x0600B5B7 RID: 46519 RVA: 0x00280331 File Offset: 0x0027E531
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual Unit ScrollBarWidth
		{
			get
			{
				object obj = base.ViewState["ScrollBarWidth"];
				if (obj != null)
				{
					return (Unit)obj;
				}
				return Unit.Empty;
			}
			set
			{
				base.ViewState["ScrollBarWidth"] = value;
			}
		}

		// Token: 0x17003AC3 RID: 15043
		// (get) Token: 0x0600B5B8 RID: 46520 RVA: 0x0028034C File Offset: 0x0027E54C
		// (set) Token: 0x0600B5B9 RID: 46521 RVA: 0x00280379 File Offset: 0x0027E579
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string ScrollTop
		{
			get
			{
				object obj = base.ViewState["ScrollTop"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				base.ViewState["ScrollTop"] = value;
			}
		}

		// Token: 0x17003AC4 RID: 15044
		// (get) Token: 0x0600B5BA RID: 46522 RVA: 0x0028038C File Offset: 0x0027E58C
		// (set) Token: 0x0600B5BB RID: 46523 RVA: 0x002803B9 File Offset: 0x0027E5B9
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string AJAXScrollTop
		{
			get
			{
				object obj = base.ViewState["AJAXScrollTop"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				base.ViewState["AJAXScrollTop"] = value;
			}
		}

		// Token: 0x17003AC5 RID: 15045
		// (get) Token: 0x0600B5BC RID: 46524 RVA: 0x002803CC File Offset: 0x0027E5CC
		// (set) Token: 0x0600B5BD RID: 46525 RVA: 0x002803F9 File Offset: 0x0027E5F9
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public virtual string ScrollLeft
		{
			get
			{
				object obj = base.ViewState["ScrollLeft"];
				if (obj != null)
				{
					return (string)obj;
				}
				return "";
			}
			set
			{
				base.ViewState["ScrollLeft"] = value;
			}
		}

		// Token: 0x17003AC6 RID: 15046
		// (get) Token: 0x0600B5BE RID: 46526 RVA: 0x0028040C File Offset: 0x0027E60C
		// (set) Token: 0x0600B5BF RID: 46527 RVA: 0x00280435 File Offset: 0x0027E635
		[Description("RadGrid_UseStaticHeaders")]
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		[Category("Client")]
		public virtual bool UseStaticHeaders
		{
			get
			{
				object obj = base.ViewState["UseStaticHeaders"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["UseStaticHeaders"] = value;
			}
		}

		// Token: 0x17003AC7 RID: 15047
		// (get) Token: 0x0600B5C0 RID: 46528 RVA: 0x00280450 File Offset: 0x0027E650
		// (set) Token: 0x0600B5C1 RID: 46529 RVA: 0x00280479 File Offset: 0x0027E679
		[Description("RadGrid_SaveScrollPosition")]
		[Category("Client")]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public virtual bool SaveScrollPosition
		{
			get
			{
				object obj = base.ViewState["SaveScrollPosition"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["SaveScrollPosition"] = value;
			}
		}

		// Token: 0x17003AC8 RID: 15048
		// (get) Token: 0x0600B5C2 RID: 46530 RVA: 0x00280494 File Offset: 0x0027E694
		// (set) Token: 0x0600B5C3 RID: 46531 RVA: 0x002804BD File Offset: 0x0027E6BD
		[Description("RadGrid_EnableVirtualScrollPaging")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public virtual bool EnableVirtualScrollPaging
		{
			get
			{
				object obj = base.ViewState["EnableVirtualScrollPaging"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["EnableVirtualScrollPaging"] = value;
			}
		}

		// Token: 0x17003AC9 RID: 15049
		// (get) Token: 0x0600B5C4 RID: 46532 RVA: 0x002804D8 File Offset: 0x0027E6D8
		// (set) Token: 0x0600B5C5 RID: 46533 RVA: 0x00280501 File Offset: 0x0027E701
		[Description("")]
		[DefaultValue(0)]
		[NotifyParentProperty(true)]
		[Category("Client")]
		public virtual int FrozenColumnsCount
		{
			get
			{
				object obj = base.ViewState["FrozenColumnsCount"];
				if (obj != null)
				{
					return (int)obj;
				}
				return 0;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				bool useStaticHeaders = this.UseStaticHeaders;
				base.ViewState["FrozenColumnsCount"] = value;
			}
		}

		// Token: 0x17003ACA RID: 15050
		// (get) Token: 0x0600B5C6 RID: 46534 RVA: 0x00280530 File Offset: 0x0027E730
		// (set) Token: 0x0600B5C7 RID: 46535 RVA: 0x00280579 File Offset: 0x0027E779
		[Category("Client")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public virtual bool EnableNextPrevFrozenColumns
		{
			get
			{
				object obj = base.ViewState["EnableNextPrevFrozenColumns"];
				if (obj != null)
				{
					return (bool)obj;
				}
				return this.ownerGrid != null && this.ownerGrid.ResolvedRenderMode == RenderMode.Mobile && this.FrozenColumnsCount > 0;
			}
			set
			{
				base.ViewState["EnableNextPrevFrozenColumns"] = value;
			}
		}

		// Token: 0x17003ACB RID: 15051
		// (get) Token: 0x0600B5C8 RID: 46536 RVA: 0x00280594 File Offset: 0x0027E794
		// (set) Token: 0x0600B5C9 RID: 46537 RVA: 0x002805BD File Offset: 0x0027E7BD
		[Category("Client")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		public virtual bool EnableColumnClientFreeze
		{
			get
			{
				object obj = base.ViewState["EnableColumnClientFreeze"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["EnableColumnClientFreeze"] = value;
			}
		}

		// Token: 0x17003ACC RID: 15052
		// (get) Token: 0x0600B5CA RID: 46538 RVA: 0x002805D8 File Offset: 0x0027E7D8
		// (set) Token: 0x0600B5CB RID: 46539 RVA: 0x00280601 File Offset: 0x0027E801
		[NotifyParentProperty(true)]
		[Category("Client")]
		[DefaultValue(true)]
		public virtual bool CountGroupSplitterColumnAsFrozen
		{
			get
			{
				object obj = base.ViewState["CountGroupSplitterColumnAsFrozen"];
				return obj == null || (bool)obj;
			}
			set
			{
				base.ViewState["CountGroupSplitterColumnAsFrozen"] = value;
			}
		}

		// Token: 0x04002FE9 RID: 12265
		private readonly RadGrid ownerGrid;
	}
}

using System;
using System.ComponentModel;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000E0B RID: 3595
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class PivotGridScrolling : StateManager
	{
		// Token: 0x17002A25 RID: 10789
		// (get) Token: 0x06008538 RID: 34104 RVA: 0x001E64E8 File Offset: 0x001E46E8
		// (set) Token: 0x06008539 RID: 34105 RVA: 0x001E6511 File Offset: 0x001E4711
		[Category("Client")]
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
		[Description("RadPivotGrid_AllowVerticalScroll")]
		public virtual bool AllowVerticalScroll
		{
			get
			{
				object obj = base.ViewState["AllowVerticalScroll"];
				return obj != null && (bool)obj;
			}
			set
			{
				base.ViewState["AllowVerticalScroll"] = value;
			}
		}

		// Token: 0x17002A26 RID: 10790
		// (get) Token: 0x0600853A RID: 34106 RVA: 0x001E6529 File Offset: 0x001E4729
		internal bool ShouldSerializeAllowVerticalScroll
		{
			get
			{
				return this.AllowVerticalScroll;
			}
		}

		// Token: 0x17002A27 RID: 10791
		// (get) Token: 0x0600853B RID: 34107 RVA: 0x001E6534 File Offset: 0x001E4734
		// (set) Token: 0x0600853C RID: 34108 RVA: 0x001E6566 File Offset: 0x001E4766
		[Description("RadPivotGrid_ScrollHeight")]
		[DefaultValue(typeof(Unit), "300px")]
		[Category("Client")]
		[NotifyParentProperty(true)]
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

		// Token: 0x17002A28 RID: 10792
		// (get) Token: 0x0600853D RID: 34109 RVA: 0x001E6580 File Offset: 0x001E4780
		// (set) Token: 0x0600853E RID: 34110 RVA: 0x001E65A9 File Offset: 0x001E47A9
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		[Description("RadPivotGrid_SaveScrollPosition")]
		[Category("Client")]
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

		// Token: 0x17002A29 RID: 10793
		// (get) Token: 0x0600853F RID: 34111 RVA: 0x001E65C1 File Offset: 0x001E47C1
		internal bool ShouldSerializeSaveScrollPosition
		{
			get
			{
				return !this.SaveScrollPosition;
			}
		}

		// Token: 0x17002A2A RID: 10794
		// (get) Token: 0x06008540 RID: 34112 RVA: 0x001E65CC File Offset: 0x001E47CC
		// (set) Token: 0x06008541 RID: 34113 RVA: 0x001E65F9 File Offset: 0x001E47F9
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
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

		// Token: 0x17002A2B RID: 10795
		// (get) Token: 0x06008542 RID: 34114 RVA: 0x001E660C File Offset: 0x001E480C
		internal bool ShouldSerializeScrollTop
		{
			get
			{
				return this.SaveScrollPosition;
			}
		}

		// Token: 0x17002A2C RID: 10796
		// (get) Token: 0x06008543 RID: 34115 RVA: 0x001E6614 File Offset: 0x001E4814
		// (set) Token: 0x06008544 RID: 34116 RVA: 0x001E6641 File Offset: 0x001E4841
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

		// Token: 0x17002A2D RID: 10797
		// (get) Token: 0x06008545 RID: 34117 RVA: 0x001E6654 File Offset: 0x001E4854
		internal bool ShouldSerializeScrollLeft
		{
			get
			{
				return this.SaveScrollPosition;
			}
		}
	}
}

using System;
using System.ComponentModel;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017D5 RID: 6101
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class StyleLabelHidden : StyleLabel
	{
		// Token: 0x170047E0 RID: 18400
		// (get) Token: 0x0600ED72 RID: 60786 RVA: 0x0036253E File Offset: 0x0036073E
		// (set) Token: 0x0600ED73 RID: 60787 RVA: 0x0036255F File Offset: 0x0036075F
		[NotifyParentProperty(true)]
		[DefaultValue(false)]
		public override bool Visible
		{
			get
			{
				return (bool)(base.ViewState["Visible"] ?? false);
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x0600ED74 RID: 60788 RVA: 0x00362568 File Offset: 0x00360768
		public StyleLabelHidden(object containerObject) : base(containerObject)
		{
		}

		// Token: 0x0600ED75 RID: 60789 RVA: 0x00362571 File Offset: 0x00360771
		public StyleLabelHidden()
		{
		}

		// Token: 0x0600ED76 RID: 60790 RVA: 0x00362579 File Offset: 0x00360779
		public StyleLabelHidden(FillStyle fillStyle) : base(fillStyle)
		{
		}

		// Token: 0x0600ED77 RID: 60791 RVA: 0x00362582 File Offset: 0x00360782
		public StyleLabelHidden(Position position) : base(position)
		{
		}

		// Token: 0x0600ED78 RID: 60792 RVA: 0x0036258B File Offset: 0x0036078B
		public StyleLabelHidden(FillStyle fillStyle, Position position) : base(fillStyle, position)
		{
		}

		// Token: 0x0600ED79 RID: 60793 RVA: 0x00362595 File Offset: 0x00360795
		public StyleLabelHidden(FillStyle fillStyle, Position position, Dimensions dimensions) : base(fillStyle, position, dimensions)
		{
		}

		// Token: 0x0600ED7A RID: 60794 RVA: 0x003625A0 File Offset: 0x003607A0
		internal override void Reset()
		{
			base.Reset();
			this.Visible = false;
		}
	}
}

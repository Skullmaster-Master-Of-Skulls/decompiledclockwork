using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017E4 RID: 6116
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[PersistChildren(false)]
	[ParseChildren(true)]
	public class StyleMarkerPositionNone : StyleMarker
	{
		// Token: 0x0600EDE9 RID: 60905 RVA: 0x003636EB File Offset: 0x003618EB
		public StyleMarkerPositionNone()
		{
			this.position = new Position(AlignedPositions.None);
		}

		// Token: 0x17004800 RID: 18432
		// (get) Token: 0x0600EDEA RID: 60906 RVA: 0x003636FF File Offset: 0x003618FF
		// (set) Token: 0x0600EDEB RID: 60907 RVA: 0x00363720 File Offset: 0x00361920
		[DefaultValue(false)]
		[NotifyParentProperty(true)]
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

		// Token: 0x0600EDEC RID: 60908 RVA: 0x00363729 File Offset: 0x00361929
		internal override void Reset()
		{
			base.Reset();
			this.Visible = false;
			this.position = new Position(AlignedPositions.None);
		}
	}
}

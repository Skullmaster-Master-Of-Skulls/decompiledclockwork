using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017B6 RID: 6070
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class PositionTop : Position
	{
		// Token: 0x17004789 RID: 18313
		// (get) Token: 0x0600EC60 RID: 60512 RVA: 0x0035E683 File Offset: 0x0035C883
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(AlignedPositions), "Top")]
		[Editor("System.Drawing.Design.ContentAlignmentEditor, System.Drawing.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SkinnableProperty]
		public override AlignedPositions AlignedPosition
		{
			get
			{
				return (AlignedPositions)(base.ViewState["AlignedPosition"] ?? AlignedPositions.Top);
			}
		}

		// Token: 0x0600EC61 RID: 60513 RVA: 0x0035E6A4 File Offset: 0x0035C8A4
		internal override void Reset()
		{
			base.Reset();
			this.AlignedPosition = AlignedPositions.Top;
		}
	}
}

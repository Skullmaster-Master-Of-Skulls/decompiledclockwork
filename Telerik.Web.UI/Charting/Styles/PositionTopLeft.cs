using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017B8 RID: 6072
	[ParseChildren(true)]
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class PositionTopLeft : Position
	{
		// Token: 0x1700478B RID: 18315
		// (get) Token: 0x0600EC66 RID: 60518 RVA: 0x0035E6FB File Offset: 0x0035C8FB
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(AlignedPositions), "TopLeft")]
		[Editor("System.Drawing.Design.ContentAlignmentEditor, System.Drawing.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SkinnableProperty]
		public override AlignedPositions AlignedPosition
		{
			get
			{
				return (AlignedPositions)(base.ViewState["AlignedPosition"] ?? AlignedPositions.TopLeft);
			}
		}

		// Token: 0x0600EC67 RID: 60519 RVA: 0x0035E71C File Offset: 0x0035C91C
		internal override void Reset()
		{
			base.Reset();
			this.AlignedPosition = AlignedPositions.TopLeft;
		}
	}
}

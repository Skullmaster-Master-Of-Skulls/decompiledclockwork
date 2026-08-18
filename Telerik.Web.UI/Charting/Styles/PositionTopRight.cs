using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017BA RID: 6074
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class PositionTopRight : Position
	{
		// Token: 0x1700478D RID: 18317
		// (get) Token: 0x0600EC6C RID: 60524 RVA: 0x0035E76D File Offset: 0x0035C96D
		[DefaultValue(typeof(AlignedPositions), "TopRight")]
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[Editor("System.Drawing.Design.ContentAlignmentEditor, System.Drawing.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public override AlignedPositions AlignedPosition
		{
			get
			{
				return (AlignedPositions)(base.ViewState["AlignedPosition"] ?? AlignedPositions.TopRight);
			}
		}

		// Token: 0x0600EC6D RID: 60525 RVA: 0x0035E78E File Offset: 0x0035C98E
		internal override void Reset()
		{
			base.Reset();
			this.AlignedPosition = AlignedPositions.TopRight;
		}
	}
}

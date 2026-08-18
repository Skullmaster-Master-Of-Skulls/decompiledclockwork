using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017BB RID: 6075
	[PersistChildren(false)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	public class PositionLeft : Position
	{
		// Token: 0x1700478E RID: 18318
		// (get) Token: 0x0600EC6F RID: 60527 RVA: 0x0035E7A5 File Offset: 0x0035C9A5
		[Editor("System.Drawing.Design.ContentAlignmentEditor, System.Drawing.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(AlignedPositions), "Left")]
		public override AlignedPositions AlignedPosition
		{
			get
			{
				return (AlignedPositions)(base.ViewState["AlignedPosition"] ?? AlignedPositions.Left);
			}
		}

		// Token: 0x0600EC70 RID: 60528 RVA: 0x0035E7C7 File Offset: 0x0035C9C7
		internal override void Reset()
		{
			base.Reset();
			this.AlignedPosition = AlignedPositions.Left;
		}
	}
}

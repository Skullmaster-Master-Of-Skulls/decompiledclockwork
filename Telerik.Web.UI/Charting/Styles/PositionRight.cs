using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017B9 RID: 6073
	[PersistChildren(false)]
	[ParseChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class PositionRight : Position
	{
		// Token: 0x1700478C RID: 18316
		// (get) Token: 0x0600EC69 RID: 60521 RVA: 0x0035E733 File Offset: 0x0035C933
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[Editor("System.Drawing.Design.ContentAlignmentEditor, System.Drawing.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue(typeof(AlignedPositions), "Right")]
		public override AlignedPositions AlignedPosition
		{
			get
			{
				return (AlignedPositions)(base.ViewState["AlignedPosition"] ?? AlignedPositions.Right);
			}
		}

		// Token: 0x0600EC6A RID: 60522 RVA: 0x0035E755 File Offset: 0x0035C955
		internal override void Reset()
		{
			base.Reset();
			this.AlignedPosition = AlignedPositions.Right;
		}
	}
}

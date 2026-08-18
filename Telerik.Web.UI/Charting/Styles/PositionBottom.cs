using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017B7 RID: 6071
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class PositionBottom : Position
	{
		// Token: 0x1700478A RID: 18314
		// (get) Token: 0x0600EC63 RID: 60515 RVA: 0x0035E6BB File Offset: 0x0035C8BB
		[SkinnableProperty]
		[Editor("System.Drawing.Design.ContentAlignmentEditor, System.Drawing.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue(typeof(AlignedPositions), "Bottom")]
		[NotifyParentProperty(true)]
		public override AlignedPositions AlignedPosition
		{
			get
			{
				return (AlignedPositions)(base.ViewState["AlignedPosition"] ?? AlignedPositions.Bottom);
			}
		}

		// Token: 0x0600EC64 RID: 60516 RVA: 0x0035E6E0 File Offset: 0x0035C8E0
		internal override void Reset()
		{
			base.Reset();
			this.AlignedPosition = AlignedPositions.Bottom;
		}
	}
}

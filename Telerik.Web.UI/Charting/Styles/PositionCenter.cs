using System;
using System.ComponentModel;
using System.Drawing.Design;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x020017B5 RID: 6069
	[PersistChildren(false)]
	[ParseChildren(true)]
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class PositionCenter : Position
	{
		// Token: 0x17004788 RID: 18312
		// (get) Token: 0x0600EC5D RID: 60509 RVA: 0x0035E649 File Offset: 0x0035C849
		[DefaultValue(typeof(AlignedPositions), "Center")]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		[Editor("System.Drawing.Design.ContentAlignmentEditor, System.Drawing.Design, Version=2.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		public override AlignedPositions AlignedPosition
		{
			get
			{
				return (AlignedPositions)(base.ViewState["AlignedPosition"] ?? AlignedPositions.Center);
			}
		}

		// Token: 0x0600EC5E RID: 60510 RVA: 0x0035E66B File Offset: 0x0035C86B
		internal override void Reset()
		{
			base.Reset();
			this.AlignedPosition = AlignedPositions.Center;
		}
	}
}

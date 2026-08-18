using System;
using System.Collections;
using System.ComponentModel;

namespace Telerik.Charting.Styles
{
	// Token: 0x02001799 RID: 6041
	public class LayoutStyle : Style, IPosition
	{
		// Token: 0x1700474A RID: 18250
		// (get) Token: 0x0600EB6F RID: 60271 RVA: 0x00359CA2 File Offset: 0x00357EA2
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SkinnableProperty]
		public virtual Position Position
		{
			get
			{
				return this.position;
			}
		}

		// Token: 0x1700474B RID: 18251
		// (get) Token: 0x0600EB70 RID: 60272 RVA: 0x00359CAA File Offset: 0x00357EAA
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public virtual Dimensions Dimensions
		{
			get
			{
				return this.dimensions;
			}
		}

		// Token: 0x0600EB71 RID: 60273 RVA: 0x00359CB2 File Offset: 0x00357EB2
		public LayoutStyle(object containerObject) : this(new Position(containerObject), new Dimensions(containerObject))
		{
		}

		// Token: 0x0600EB72 RID: 60274 RVA: 0x00359CC6 File Offset: 0x00357EC6
		public LayoutStyle(Position position) : this(position, new Dimensions())
		{
		}

		// Token: 0x0600EB73 RID: 60275 RVA: 0x00359CD4 File Offset: 0x00357ED4
		public LayoutStyle(Dimensions dimensions) : this(new Position(), dimensions)
		{
		}

		// Token: 0x0600EB74 RID: 60276 RVA: 0x00359CE2 File Offset: 0x00357EE2
		public LayoutStyle(Position position, Dimensions dimensions) : this(null, true, null, position, dimensions)
		{
		}

		// Token: 0x0600EB75 RID: 60277 RVA: 0x00359CEF File Offset: 0x00357EEF
		public LayoutStyle(StyleBorder border, bool visible, ShadowStyle shadowStyle, Position position, Dimensions dimensions) : base(border, visible, shadowStyle)
		{
			this.position = (position ?? new Position());
			this.dimensions = (dimensions ?? new Dimensions());
		}

		// Token: 0x0600EB76 RID: 60278 RVA: 0x00359D1C File Offset: 0x00357F1C
		internal override void Reset()
		{
			base.Reset();
			this.position = new Position();
			this.dimensions = new Dimensions();
		}

		// Token: 0x0600EB77 RID: 60279 RVA: 0x00359D3A File Offset: 0x00357F3A
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.dimensions).TrackViewState();
			((IChartingStateManager)this.position).TrackViewState();
		}

		// Token: 0x0600EB78 RID: 60280 RVA: 0x00359D58 File Offset: 0x00357F58
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.dimensions).LoadViewState(array[1]);
				((IChartingStateManager)this.position).LoadViewState(array[2]);
			}
		}

		// Token: 0x0600EB79 RID: 60281 RVA: 0x00359D94 File Offset: 0x00357F94
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.dimensions).SaveViewState(),
				((IChartingStateManager)this.position).SaveViewState()
			}.ToArray();
		}

		// Token: 0x0600EB7A RID: 60282 RVA: 0x00359DDE File Offset: 0x00357FDE
		protected override void Dispose(bool disposing)
		{
			if (this.position != null)
			{
				this.position.Dispose();
				this.position = null;
			}
			if (this.dimensions != null)
			{
				this.dimensions.Dispose();
				this.dimensions = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x04004417 RID: 17431
		internal Position position;

		// Token: 0x04004418 RID: 17432
		internal Dimensions dimensions;
	}
}

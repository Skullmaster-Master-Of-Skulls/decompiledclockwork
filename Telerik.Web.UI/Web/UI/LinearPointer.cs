using System;
using System.ComponentModel;
using System.Web.UI;
using Telerik.Web.UI.Gauge;

namespace Telerik.Web.UI
{
	// Token: 0x02000B65 RID: 2917
	[ToolboxItem(false)]
	public class LinearPointer : PointerBase
	{
		// Token: 0x1700241C RID: 9244
		// (get) Token: 0x06006E1D RID: 28189 RVA: 0x00198AF3 File Offset: 0x00196CF3
		// (set) Token: 0x06006E1E RID: 28190 RVA: 0x00198B22 File Offset: 0x00196D22
		[Description("Gets or sets the transparency of the pointer of the LinearGauge.")]
		[Category("Behavior")]
		[DefaultValue(1f)]
		public float Opacity
		{
			get
			{
				if (base.ViewState["Opacity"] == null)
				{
					return 1f;
				}
				return (float)base.ViewState["Opacity"];
			}
			set
			{
				base.ViewState["Opacity"] = value;
			}
		}

		// Token: 0x1700241D RID: 9245
		// (get) Token: 0x06006E1F RID: 28191 RVA: 0x00198B3A File Offset: 0x00196D3A
		// (set) Token: 0x06006E20 RID: 28192 RVA: 0x00198B5B File Offset: 0x00196D5B
		[DefaultValue(PointerShape.BarIndicator)]
		[Description("Gets or sets the shape of the LinearGauge's pointer.")]
		[Category("Behavior")]
		public PointerShape Shape
		{
			get
			{
				return (PointerShape)(base.ViewState["Shape"] ?? PointerShape.BarIndicator);
			}
			set
			{
				base.ViewState["Shape"] = value;
			}
		}

		// Token: 0x1700241E RID: 9246
		// (get) Token: 0x06006E21 RID: 28193 RVA: 0x00198B73 File Offset: 0x00196D73
		// (set) Token: 0x06006E22 RID: 28194 RVA: 0x00198B8F File Offset: 0x00196D8F
		[Category("Behavior")]
		[Description("Gets or sets the size of the pointer.")]
		[DefaultValue(null)]
		public float? Size
		{
			get
			{
				return (float?)(base.ViewState["Size"] ?? null);
			}
			set
			{
				base.ViewState["Size"] = value;
			}
		}

		// Token: 0x1700241F RID: 9247
		// (get) Token: 0x06006E23 RID: 28195 RVA: 0x00198BA7 File Offset: 0x00196DA7
		// (set) Token: 0x06006E24 RID: 28196 RVA: 0x00198BC3 File Offset: 0x00196DC3
		[Category("Appearance")]
		[Description("Gets or sets the margin of the pointer.")]
		[DefaultValue(null)]
		public double? Margin
		{
			get
			{
				return (double?)(base.ViewState["Margin"] ?? null);
			}
			set
			{
				base.ViewState["Margin"] = value;
			}
		}

		// Token: 0x17002420 RID: 9248
		// (get) Token: 0x06006E25 RID: 28197 RVA: 0x00198BDB File Offset: 0x00196DDB
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[Category("Behavior")]
		[DefaultValue(null)]
		[Browsable(true)]
		[Description("Defines the settings of the track of the LinearGauge's pointer.")]
		public Track Track
		{
			get
			{
				if (this._track == null)
				{
					this._track = new Track();
				}
				return this._track;
			}
		}

		// Token: 0x06006E26 RID: 28198 RVA: 0x00198BF8 File Offset: 0x00196DF8
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.Track).LoadViewState(array[1]);
		}

		// Token: 0x06006E27 RID: 28199 RVA: 0x00198C24 File Offset: 0x00196E24
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Track).SaveViewState()
			};
		}

		// Token: 0x06006E28 RID: 28200 RVA: 0x00198C52 File Offset: 0x00196E52
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Track).TrackViewState();
		}

		// Token: 0x04001DC2 RID: 7618
		private Track _track;
	}
}

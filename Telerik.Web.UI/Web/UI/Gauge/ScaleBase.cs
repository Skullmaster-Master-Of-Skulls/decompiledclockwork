using System;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI.Gauge
{
	// Token: 0x02000B6B RID: 2923
	[ToolboxItem(false)]
	public abstract class ScaleBase : StateManager
	{
		// Token: 0x17002428 RID: 9256
		// (get) Token: 0x06006E3E RID: 28222 RVA: 0x00198F19 File Offset: 0x00197119
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Browsable(true)]
		[Category("Behavior")]
		[DefaultValue(null)]
		[NotifyParentProperty(true)]
		[Description("Defines the settings of the Scale's Labels.")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public ScaleLabels Labels
		{
			get
			{
				if (this._scaleLabels == null)
				{
					this._scaleLabels = new ScaleLabels();
				}
				return this._scaleLabels;
			}
		}

		// Token: 0x17002429 RID: 9257
		// (get) Token: 0x06006E3F RID: 28223 RVA: 0x00198F34 File Offset: 0x00197134
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[Category("Behavior")]
		[DefaultValue(null)]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Description("Defines the settings of the Scale's Minor ticks.")]
		[NotifyParentProperty(true)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Ticks MinorTicks
		{
			get
			{
				if (this._minorTicks == null)
				{
					this._minorTicks = new Ticks();
				}
				return this._minorTicks;
			}
		}

		// Token: 0x1700242A RID: 9258
		// (get) Token: 0x06006E40 RID: 28224 RVA: 0x00198F4F File Offset: 0x0019714F
		[Category("Behavior")]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		[Description("Defines the settings of the Scale's Major ticks.")]
		[Browsable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[DefaultValue(null)]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		public Ticks MajorTicks
		{
			get
			{
				if (this._majorTicks == null)
				{
					this._majorTicks = new Ticks();
				}
				return this._majorTicks;
			}
		}

		// Token: 0x1700242B RID: 9259
		// (get) Token: 0x06006E41 RID: 28225 RVA: 0x00198F6A File Offset: 0x0019716A
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[Category("Behavior")]
		[Description("Defines a collection of gauge ranges.")]
		[DefaultValue(null)]
		public GaugeRangeCollection Ranges
		{
			get
			{
				if (this._ranges == null)
				{
					this._ranges = new GaugeRangeCollection();
				}
				return this._ranges;
			}
		}

		// Token: 0x1700242C RID: 9260
		// (get) Token: 0x06006E42 RID: 28226 RVA: 0x00198F88 File Offset: 0x00197188
		// (set) Token: 0x06006E43 RID: 28227 RVA: 0x00198FC2 File Offset: 0x001971C2
		[DefaultValue(typeof(decimal), "0")]
		[Bindable(true)]
		[Description("Gets or sets the minimum value of the scale.")]
		[Category("Behavior")]
		public virtual decimal Min
		{
			get
			{
				decimal? num = (decimal?)base.ViewState["Min"];
				if (num == null)
				{
					return 0m;
				}
				return num.GetValueOrDefault();
			}
			set
			{
				base.ViewState["Min"] = value;
			}
		}

		// Token: 0x1700242D RID: 9261
		// (get) Token: 0x06006E44 RID: 28228 RVA: 0x00198FDC File Offset: 0x001971DC
		// (set) Token: 0x06006E45 RID: 28229 RVA: 0x00199017 File Offset: 0x00197217
		[Category("Behavior")]
		[Description("Gets or sets the maximum value of the scale.")]
		[Bindable(true)]
		[DefaultValue(typeof(decimal), "100")]
		public virtual decimal Max
		{
			get
			{
				decimal? num = (decimal?)base.ViewState["Max"];
				if (num == null)
				{
					return 100m;
				}
				return num.GetValueOrDefault();
			}
			set
			{
				base.ViewState["Max"] = value;
			}
		}

		// Token: 0x1700242E RID: 9262
		// (get) Token: 0x06006E46 RID: 28230 RVA: 0x0019902F File Offset: 0x0019722F
		// (set) Token: 0x06006E47 RID: 28231 RVA: 0x0019904B File Offset: 0x0019724B
		[Category("Behavior")]
		[DefaultValue(null)]
		[Description("Gets or sets the interval between the minor divisions.")]
		public virtual decimal? MinorUnit
		{
			get
			{
				return (decimal?)(base.ViewState["MinorUnit"] ?? null);
			}
			set
			{
				base.ViewState["MinorUnit"] = value;
			}
		}

		// Token: 0x1700242F RID: 9263
		// (get) Token: 0x06006E48 RID: 28232 RVA: 0x00199063 File Offset: 0x00197263
		// (set) Token: 0x06006E49 RID: 28233 RVA: 0x0019907F File Offset: 0x0019727F
		[DefaultValue(null)]
		[Category("Behavior")]
		[Description("Gets or sets the interval between the major divisions.")]
		public virtual decimal? MajorUnit
		{
			get
			{
				return (decimal?)(base.ViewState["MajorUnit"] ?? null);
			}
			set
			{
				base.ViewState["MajorUnit"] = value;
			}
		}

		// Token: 0x17002430 RID: 9264
		// (get) Token: 0x06006E4A RID: 28234 RVA: 0x00199097 File Offset: 0x00197297
		// (set) Token: 0x06006E4B RID: 28235 RVA: 0x001990B8 File Offset: 0x001972B8
		[Description("Gets or sets a bool value indicating whether the direction of the scale values will be reversed.")]
		[Category("Behavior")]
		[DefaultValue(false)]
		public virtual bool Reverse
		{
			get
			{
				return (bool)(base.ViewState["Reverse"] ?? false);
			}
			set
			{
				base.ViewState["Reverse"] = value;
			}
		}

		// Token: 0x06006E4C RID: 28236 RVA: 0x001990D0 File Offset: 0x001972D0
		protected override void LoadViewState(object state)
		{
			object[] array = (object[])state;
			base.LoadViewState(array[0]);
			((IStateManager)this.Labels).LoadViewState(array[1]);
			((IStateManager)this.MinorTicks).LoadViewState(array[2]);
			((IStateManager)this.MajorTicks).LoadViewState(array[3]);
			if (array[4] == null)
			{
				this.Ranges.Clear();
				return;
			}
			((IStateManager)this.Ranges).LoadViewState(array[4]);
		}

		// Token: 0x06006E4D RID: 28237 RVA: 0x00199138 File Offset: 0x00197338
		protected override object SaveViewState()
		{
			return new object[]
			{
				base.SaveViewState(),
				((IStateManager)this.Labels).SaveViewState(),
				((IStateManager)this.MinorTicks).SaveViewState(),
				((IStateManager)this.MajorTicks).SaveViewState(),
				((IStateManager)this.Ranges).SaveViewState()
			};
		}

		// Token: 0x06006E4E RID: 28238 RVA: 0x00199190 File Offset: 0x00197390
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IStateManager)this.Labels).TrackViewState();
			((IStateManager)this.MinorTicks).TrackViewState();
			((IStateManager)this.MajorTicks).TrackViewState();
			((IStateManager)this.Ranges).TrackViewState();
		}

		// Token: 0x04001DCA RID: 7626
		private ScaleLabels _scaleLabels;

		// Token: 0x04001DCB RID: 7627
		private Ticks _minorTicks;

		// Token: 0x04001DCC RID: 7628
		private Ticks _majorTicks;

		// Token: 0x04001DCD RID: 7629
		private GaugeRangeCollection _ranges;
	}
}

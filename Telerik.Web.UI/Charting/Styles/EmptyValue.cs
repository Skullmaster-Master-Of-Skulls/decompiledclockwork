using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x02001782 RID: 6018
	[TypeConverter(typeof(ExpandableObjectConverter))]
	public class EmptyValue : StateManagedObject, ICloneable
	{
		// Token: 0x1700471C RID: 18204
		// (get) Token: 0x0600EAB6 RID: 60086 RVA: 0x003576D2 File Offset: 0x003558D2
		// (set) Token: 0x0600EAB7 RID: 60087 RVA: 0x003576F3 File Offset: 0x003558F3
		[PersistenceMode(PersistenceMode.Attribute)]
		[SkinnableProperty]
		[Description("Empty values representation mode")]
		[NotifyParentProperty(true)]
		[DefaultValue(EmtyValuesMode.Approximation)]
		public EmtyValuesMode Mode
		{
			get
			{
				return (EmtyValuesMode)(base.ViewState["Mode"] ?? EmtyValuesMode.Approximation);
			}
			set
			{
				base.ViewState["Mode"] = value;
			}
		}

		// Token: 0x1700471D RID: 18205
		// (get) Token: 0x0600EAB8 RID: 60088 RVA: 0x0035770B File Offset: 0x0035590B
		[Description("Empty item's line style")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[SkinnableProperty]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		[NotifyParentProperty(true)]
		public StyleEmptyLineSeries Line
		{
			get
			{
				return this.emptyValueLine;
			}
		}

		// Token: 0x1700471E RID: 18206
		// (get) Token: 0x0600EAB9 RID: 60089 RVA: 0x00357713 File Offset: 0x00355913
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		[Description("Specifies the empty item's point mark")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public StyleMarkerEmptyValue PointMark
		{
			get
			{
				return this.emptyValueMarker;
			}
		}

		// Token: 0x1700471F RID: 18207
		// (get) Token: 0x0600EABA RID: 60090 RVA: 0x0035771B File Offset: 0x0035591B
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[Browsable(true)]
		[Description(" Specifies an empty bar fill style")]
		[PersistenceMode(PersistenceMode.InnerProperty)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[TypeConverter(typeof(ExpandableObjectConverter))]
		public FillStyle FillStyle
		{
			get
			{
				return this.emptyValueFillStyle;
			}
		}

		// Token: 0x0600EABB RID: 60091 RVA: 0x00357723 File Offset: 0x00355923
		public EmptyValue()
		{
			this.emptyValueMarker = new StyleMarkerEmptyValue();
			this.emptyValueLine = new StyleEmptyLineSeries();
			this.emptyValueFillStyle = new FillStyle();
		}

		// Token: 0x0600EABC RID: 60092 RVA: 0x0035774C File Offset: 0x0035594C
		internal void Reset()
		{
			this.Mode = EmtyValuesMode.Approximation;
			this.emptyValueMarker = new StyleMarkerEmptyValue();
			this.emptyValueLine = new StyleEmptyLineSeries();
			this.emptyValueFillStyle = new FillStyle();
		}

		// Token: 0x0600EABD RID: 60093 RVA: 0x00357778 File Offset: 0x00355978
		public object Clone()
		{
			return new EmptyValue
			{
				ViewState = base.CloneState(),
				emptyValueLine = (StyleEmptyLineSeries)this.emptyValueLine.Clone(),
				emptyValueMarker = (StyleMarkerEmptyValue)this.emptyValueMarker.Clone(),
				emptyValueFillStyle = (FillStyle)this.emptyValueFillStyle.Clone()
			};
		}

		// Token: 0x0600EABE RID: 60094 RVA: 0x003577DA File Offset: 0x003559DA
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.emptyValueMarker).TrackViewState();
			((IChartingStateManager)this.emptyValueLine).TrackViewState();
			((IChartingStateManager)this.emptyValueFillStyle).TrackViewState();
		}

		// Token: 0x0600EABF RID: 60095 RVA: 0x00357804 File Offset: 0x00355A04
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.emptyValueMarker).LoadViewState(array[1]);
				((IChartingStateManager)this.emptyValueLine).LoadViewState(array[2]);
				((IChartingStateManager)this.emptyValueFillStyle).LoadViewState(array[3]);
			}
		}

		// Token: 0x0600EAC0 RID: 60096 RVA: 0x00357850 File Offset: 0x00355A50
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.emptyValueMarker).SaveViewState(),
				((IChartingStateManager)this.emptyValueLine).SaveViewState(),
				((IChartingStateManager)this.emptyValueFillStyle).SaveViewState()
			}.ToArray();
		}

		// Token: 0x0600EAC1 RID: 60097 RVA: 0x003578AC File Offset: 0x00355AAC
		protected override void Dispose(bool disposing)
		{
			if (this.emptyValueFillStyle != null)
			{
				this.emptyValueFillStyle.Dispose();
				this.emptyValueFillStyle = null;
			}
			if (this.emptyValueLine != null)
			{
				this.emptyValueLine.Dispose();
				this.emptyValueLine = null;
			}
			if (this.emptyValueMarker != null)
			{
				this.emptyValueMarker.Dispose();
				this.emptyValueMarker = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x040043D5 RID: 17365
		private StyleMarkerEmptyValue emptyValueMarker;

		// Token: 0x040043D6 RID: 17366
		private StyleEmptyLineSeries emptyValueLine;

		// Token: 0x040043D7 RID: 17367
		private FillStyle emptyValueFillStyle;
	}
}

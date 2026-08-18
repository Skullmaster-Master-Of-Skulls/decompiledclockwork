using System;
using System.Collections;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Web.UI;

namespace Telerik.Charting.Styles
{
	// Token: 0x02001779 RID: 6009
	[TypeConverter(typeof(ExpandableObjectConverter))]
	[ParseChildren(true)]
	[PersistChildren(false)]
	public class Dimensions : StateManagedObject, ISizesAndPaddings, ICloneable
	{
		// Token: 0x17004705 RID: 18181
		// (get) Token: 0x0600EA58 RID: 59992 RVA: 0x00356BA7 File Offset: 0x00354DA7
		// (set) Token: 0x0600EA59 RID: 59993 RVA: 0x00356BC8 File Offset: 0x00354DC8
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		[DefaultValue(true)]
		public virtual bool AutoSize
		{
			get
			{
				return (bool)(base.ViewState["AutoSize"] ?? true);
			}
			set
			{
				base.ViewState["AutoSize"] = value;
			}
		}

		// Token: 0x17004706 RID: 18182
		// (get) Token: 0x0600EA5A RID: 59994 RVA: 0x00356BE0 File Offset: 0x00354DE0
		// (set) Token: 0x0600EA5B RID: 59995 RVA: 0x00356C00 File Offset: 0x00354E00
		[TypeConverter(typeof(UnitConverter))]
		[NotifyParentProperty(true)]
		public virtual Unit Height
		{
			get
			{
				return (Unit)(base.ViewState["Height"] ?? DefaultValues.DEFAULT_PIXEL_VALUE);
			}
			set
			{
				if (value.PixelValue >= 0f)
				{
					base.ViewState["Height"] = value;
					return;
				}
				base.ViewState["Height"] = Unit.Pixel(1f);
			}
		}

		// Token: 0x0600EA5C RID: 59996 RVA: 0x00356C3B File Offset: 0x00354E3B
		protected virtual bool ShouldSerializeHeight()
		{
			return !this.AutoSize;
		}

		// Token: 0x0600EA5D RID: 59997 RVA: 0x00356C46 File Offset: 0x00354E46
		protected virtual void ResetHeight()
		{
			this.Height = DefaultValues.DEFAULT_PIXEL_VALUE.Clone();
		}

		// Token: 0x17004707 RID: 18183
		// (get) Token: 0x0600EA5E RID: 59998 RVA: 0x00356C58 File Offset: 0x00354E58
		// (set) Token: 0x0600EA5F RID: 59999 RVA: 0x00356C78 File Offset: 0x00354E78
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(UnitConverter))]
		public virtual Unit Width
		{
			get
			{
				return (Unit)(base.ViewState["Width"] ?? DefaultValues.DEFAULT_PIXEL_VALUE);
			}
			set
			{
				if (value.PixelValue >= 0f)
				{
					base.ViewState["Width"] = value;
					return;
				}
				base.ViewState["Width"] = Unit.Pixel(1f);
			}
		}

		// Token: 0x0600EA60 RID: 60000 RVA: 0x00356CB3 File Offset: 0x00354EB3
		protected virtual bool ShouldSerializeWidth()
		{
			return !this.AutoSize;
		}

		// Token: 0x0600EA61 RID: 60001 RVA: 0x00356CBE File Offset: 0x00354EBE
		protected virtual void ResetWidth()
		{
			this.Width = DefaultValues.DEFAULT_PIXEL_VALUE.Clone();
		}

		// Token: 0x17004708 RID: 18184
		// (get) Token: 0x0600EA62 RID: 60002 RVA: 0x00356CD0 File Offset: 0x00354ED0
		// (set) Token: 0x0600EA63 RID: 60003 RVA: 0x00356CD8 File Offset: 0x00354ED8
		[PersistenceMode(PersistenceMode.Attribute)]
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[TypeConverter(typeof(MarginsConverter))]
		[DefaultValue(typeof(ChartMargins), "1px, 1px, 1px, 1px")]
		public virtual ChartMargins Margins
		{
			get
			{
				return this.dimensionsMargins;
			}
			set
			{
				this.dimensionsMargins = value;
			}
		}

		// Token: 0x17004709 RID: 18185
		// (get) Token: 0x0600EA64 RID: 60004 RVA: 0x00356CE1 File Offset: 0x00354EE1
		// (set) Token: 0x0600EA65 RID: 60005 RVA: 0x00356CE9 File Offset: 0x00354EE9
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		[DefaultValue(typeof(ChartPaddings), "1px, 1px, 1px, 1px")]
		[TypeConverter(typeof(PaddingsConverter))]
		[PersistenceMode(PersistenceMode.Attribute)]
		public virtual ChartPaddings Paddings
		{
			get
			{
				return this.dimensionsPaddings;
			}
			set
			{
				this.dimensionsPaddings = value;
			}
		}

		// Token: 0x1700470A RID: 18186
		internal object this[StyleProperties name]
		{
			get
			{
				switch (name)
				{
				case StyleProperties.AutoSize:
					return this.AutoSize;
				case StyleProperties.Height:
					return this.Height;
				case StyleProperties.Width:
					return this.Width;
				case StyleProperties.Margins:
					return this.dimensionsMargins;
				case StyleProperties.Paddings:
					return this.dimensionsPaddings;
				default:
					return null;
				}
			}
		}

		// Token: 0x0600EA67 RID: 60007 RVA: 0x00356D4A File Offset: 0x00354F4A
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Dimensions(object containerObject) : this()
		{
			this.containerObject = containerObject;
			this.dimensionsMargins = new ChartMargins(containerObject);
			this.dimensionsPaddings = new ChartPaddings(containerObject);
			this.ResetWidth();
			this.ResetHeight();
		}

		// Token: 0x0600EA68 RID: 60008 RVA: 0x00356D7D File Offset: 0x00354F7D
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Dimensions()
		{
			this.dimensionsMargins = new ChartMargins();
			this.dimensionsPaddings = new ChartPaddings();
			this.ResetWidth();
			this.ResetHeight();
		}

		// Token: 0x0600EA69 RID: 60009 RVA: 0x00356DA7 File Offset: 0x00354FA7
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Dimensions(float width, float height) : this()
		{
			this.Height = Unit.Pixel(height);
			this.Width = Unit.Pixel(width);
		}

		// Token: 0x0600EA6A RID: 60010 RVA: 0x00356DC7 File Offset: 0x00354FC7
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public Dimensions(Unit width, Unit height)
		{
			this.Width = width;
			this.Height = height;
		}

		// Token: 0x0600EA6B RID: 60011 RVA: 0x00356DDD File Offset: 0x00354FDD
		public Dimensions(ChartMargins margins) : this()
		{
			this.dimensionsMargins = margins;
		}

		// Token: 0x0600EA6C RID: 60012 RVA: 0x00356DEC File Offset: 0x00354FEC
		public Dimensions(ChartPaddings paddings) : this()
		{
			this.dimensionsPaddings = paddings;
		}

		// Token: 0x0600EA6D RID: 60013 RVA: 0x00356DFB File Offset: 0x00354FFB
		public Dimensions(ChartMargins margins, ChartPaddings paddings) : this(margins)
		{
			this.dimensionsPaddings = paddings;
		}

		// Token: 0x1700470B RID: 18187
		// (get) Token: 0x0600EA6E RID: 60014 RVA: 0x00356E0B File Offset: 0x0035500B
		// (set) Token: 0x0600EA6F RID: 60015 RVA: 0x00356E1D File Offset: 0x0035501D
		internal Dimensions Copy
		{
			get
			{
				if (this.dimensionsCopy == null)
				{
					return this;
				}
				return this.dimensionsCopy;
			}
			set
			{
				this.dimensionsCopy = (Dimensions)value.Clone();
			}
		}

		// Token: 0x0600EA70 RID: 60016 RVA: 0x00356E30 File Offset: 0x00355030
		internal virtual void Reset()
		{
			this.AutoSize = true;
			this.Height = (this.Width = DefaultValues.DEFAULT_PIXEL_VALUE);
			this.dimensionsMargins = new ChartMargins();
			this.dimensionsPaddings = new ChartPaddings();
		}

		// Token: 0x0600EA71 RID: 60017 RVA: 0x00356E70 File Offset: 0x00355070
		internal bool EqualsWithoutMarginsPaddings(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			Dimensions dimensions = obj as Dimensions;
			if (dimensions != null)
			{
				return dimensions.AutoSize == this.AutoSize && dimensions.Height.Equals(this.Height) && dimensions.Width.Equals(this.Width);
			}
			return base.Equals(obj);
		}

		// Token: 0x0600EA72 RID: 60018 RVA: 0x00356EC8 File Offset: 0x003550C8
		public void SetDimensions(float width, float height)
		{
			this.Width = width;
			this.Height = height;
		}

		// Token: 0x0600EA73 RID: 60019 RVA: 0x00356EE4 File Offset: 0x003550E4
		public void SetDimensions(Unit width, Unit height)
		{
			this.Width = width.Clone();
			this.Height = height.Clone();
		}

		// Token: 0x0600EA74 RID: 60020 RVA: 0x00356F00 File Offset: 0x00355100
		public void SetDimensions(Dimensions source)
		{
			this.Width = source.Width.Clone();
			this.Height = source.Height.Clone();
			this.dimensionsMargins.CopyFrom(source.Margins);
			this.dimensionsPaddings.CopyFrom(source.Paddings);
		}

		// Token: 0x0600EA75 RID: 60021 RVA: 0x00356F54 File Offset: 0x00355154
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			Dimensions dimensions = obj as Dimensions;
			if (dimensions != null)
			{
				return dimensions.AutoSize == this.AutoSize && dimensions.Height == this.Height && dimensions.Width == this.Width && dimensions.dimensionsMargins.Equals(this.dimensionsMargins) && dimensions.dimensionsPaddings.Equals(this.dimensionsPaddings);
			}
			return base.Equals(obj);
		}

		// Token: 0x0600EA76 RID: 60022 RVA: 0x00356FD4 File Offset: 0x003551D4
		public override int GetHashCode()
		{
			return this.AutoSize.GetHashCode() ^ this.Height.GetHashCode() ^ this.Width.GetHashCode() ^ this.dimensionsMargins.GetHashCode() ^ this.dimensionsPaddings.GetHashCode();
		}

		// Token: 0x0600EA77 RID: 60023 RVA: 0x0035701F File Offset: 0x0035521F
		public bool IsZero()
		{
			return this.Width.PixelValue == 0f | this.Height.PixelValue == 0f;
		}

		// Token: 0x0600EA78 RID: 60024 RVA: 0x00357046 File Offset: 0x00355246
		protected override void Dispose(bool disposing)
		{
			if (this.dimensionsMargins != null)
			{
				this.dimensionsMargins.Dispose();
			}
			if (this.dimensionsPaddings != null)
			{
				this.dimensionsPaddings.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600EA79 RID: 60025 RVA: 0x00357084 File Offset: 0x00355284
		public virtual object Clone()
		{
			Dimensions dimensions = (Dimensions)base.MemberwiseClone();
			dimensions.ViewState = base.CloneState();
			dimensions.Width = new Unit(this.Width.Value, this.Width.Type);
			dimensions.Height = new Unit(this.Height.Value, this.Height.Type);
			dimensions.dimensionsMargins = (ChartMargins)this.dimensionsMargins.Clone();
			dimensions.dimensionsPaddings = (ChartPaddings)this.dimensionsPaddings.Clone();
			dimensions.containerObject = null;
			return dimensions;
		}

		// Token: 0x0600EA7A RID: 60026 RVA: 0x0035711F File Offset: 0x0035531F
		protected override void TrackViewState()
		{
			base.TrackViewState();
			((IChartingStateManager)this.dimensionsMargins).TrackViewState();
			((IChartingStateManager)this.dimensionsPaddings).TrackViewState();
		}

		// Token: 0x0600EA7B RID: 60027 RVA: 0x00357140 File Offset: 0x00355340
		protected override void LoadViewState(object savedState)
		{
			object[] array = (object[])savedState;
			if (array != null)
			{
				base.LoadViewState(array[0]);
				((IChartingStateManager)this.dimensionsMargins).LoadViewState(array[1]);
				((IChartingStateManager)this.dimensionsPaddings).LoadViewState(array[2]);
			}
		}

		// Token: 0x0600EA7C RID: 60028 RVA: 0x0035717C File Offset: 0x0035537C
		protected override object SaveViewState()
		{
			return new ArrayList
			{
				base.SaveViewState(),
				((IChartingStateManager)this.dimensionsMargins).SaveViewState(),
				((IChartingStateManager)this.dimensionsPaddings).SaveViewState()
			}.ToArray();
		}

		// Token: 0x040043CC RID: 17356
		protected ChartMargins dimensionsMargins;

		// Token: 0x040043CD RID: 17357
		protected ChartPaddings dimensionsPaddings;

		// Token: 0x040043CE RID: 17358
		internal object containerObject;

		// Token: 0x040043CF RID: 17359
		internal Dimensions dimensionsCopy;
	}
}

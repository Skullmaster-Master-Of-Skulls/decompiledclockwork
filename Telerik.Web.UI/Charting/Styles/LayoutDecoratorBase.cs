using System;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Charting.Styles
{
	// Token: 0x02001797 RID: 6039
	[DesignTimeVisible(true)]
	[TypeConverter(typeof(MarginsConverter))]
	public class LayoutDecoratorBase : StateManagedObject, ICloneable
	{
		// Token: 0x17004741 RID: 18241
		// (get) Token: 0x0600EB41 RID: 60225 RVA: 0x00359367 File Offset: 0x00357567
		// (set) Token: 0x0600EB42 RID: 60226 RVA: 0x00359387 File Offset: 0x00357587
		[SkinnableProperty]
		[Browsable(true)]
		[Description("Left side.")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "1px")]
		[TypeConverter(typeof(UnitConverter))]
		public virtual Unit Left
		{
			get
			{
				return (Unit)(base.ViewState["Left"] ?? DefaultValues.ONE_PIXEL);
			}
			set
			{
				base.ViewState["Left"] = value;
			}
		}

		// Token: 0x17004742 RID: 18242
		// (get) Token: 0x0600EB43 RID: 60227 RVA: 0x0035939A File Offset: 0x0035759A
		// (set) Token: 0x0600EB44 RID: 60228 RVA: 0x003593BA File Offset: 0x003575BA
		[SkinnableProperty]
		[DefaultValue(typeof(Unit), "1px")]
		[TypeConverter(typeof(UnitConverter))]
		[Description("Right side.")]
		[Browsable(true)]
		[NotifyParentProperty(true)]
		public virtual Unit Right
		{
			get
			{
				return (Unit)(base.ViewState["Right"] ?? DefaultValues.ONE_PIXEL);
			}
			set
			{
				base.ViewState["Right"] = value;
			}
		}

		// Token: 0x17004743 RID: 18243
		// (get) Token: 0x0600EB45 RID: 60229 RVA: 0x003593CD File Offset: 0x003575CD
		// (set) Token: 0x0600EB46 RID: 60230 RVA: 0x003593ED File Offset: 0x003575ED
		[SkinnableProperty]
		[Browsable(true)]
		[Description("Top side.")]
		[NotifyParentProperty(true)]
		[DefaultValue(typeof(Unit), "1px")]
		[TypeConverter(typeof(UnitConverter))]
		public virtual Unit Top
		{
			get
			{
				return (Unit)(base.ViewState["Top"] ?? DefaultValues.ONE_PIXEL);
			}
			set
			{
				base.ViewState["Top"] = value;
			}
		}

		// Token: 0x17004744 RID: 18244
		// (get) Token: 0x0600EB47 RID: 60231 RVA: 0x00359400 File Offset: 0x00357600
		// (set) Token: 0x0600EB48 RID: 60232 RVA: 0x00359420 File Offset: 0x00357620
		[Browsable(true)]
		[SkinnableProperty]
		[NotifyParentProperty(true)]
		[TypeConverter(typeof(UnitConverter))]
		[Description("Bottom side.")]
		[DefaultValue(typeof(Unit), "1px")]
		public virtual Unit Bottom
		{
			get
			{
				return (Unit)(base.ViewState["Bottom"] ?? DefaultValues.ONE_PIXEL);
			}
			set
			{
				base.ViewState["Bottom"] = value;
			}
		}

		// Token: 0x0600EB49 RID: 60233 RVA: 0x00359433 File Offset: 0x00357633
		public LayoutDecoratorBase(object containerObject) : this()
		{
			this.chartLayoutDecoratorBaseContainerObject = containerObject;
		}

		// Token: 0x0600EB4A RID: 60234 RVA: 0x00359442 File Offset: 0x00357642
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public LayoutDecoratorBase()
		{
			this.Reset();
		}

		// Token: 0x0600EB4B RID: 60235 RVA: 0x00359450 File Offset: 0x00357650
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public LayoutDecoratorBase(object containerObject, Unit top, Unit right, Unit bottom, Unit left) : this(containerObject)
		{
			this.Top = top;
			this.Right = right;
			this.Bottom = bottom;
			this.Left = left;
		}

		// Token: 0x0600EB4C RID: 60236 RVA: 0x00359477 File Offset: 0x00357677
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public LayoutDecoratorBase(Unit top, Unit right, Unit bottom, Unit left)
		{
			this.Top = top;
			this.Right = right;
			this.Bottom = bottom;
			this.Left = left;
		}

		// Token: 0x0600EB4D RID: 60237 RVA: 0x0035949C File Offset: 0x0035769C
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public LayoutDecoratorBase(int top, int right, int bottom, int left)
		{
			this.Top = (float)top;
			this.Right = (float)right;
			this.Bottom = (float)bottom;
			this.Left = (float)left;
		}

		// Token: 0x0600EB4E RID: 60238 RVA: 0x003594D9 File Offset: 0x003576D9
		[SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
		public LayoutDecoratorBase(Unit value)
		{
			this.Reset(value);
		}

		// Token: 0x0600EB4F RID: 60239 RVA: 0x003594E8 File Offset: 0x003576E8
		internal virtual void Reset()
		{
			this.Top = DefaultValues.ONE_PIXEL.Clone();
			this.Right = DefaultValues.ONE_PIXEL.Clone();
			this.Bottom = DefaultValues.ONE_PIXEL.Clone();
			this.Left = DefaultValues.ONE_PIXEL.Clone();
		}

		// Token: 0x0600EB50 RID: 60240 RVA: 0x00359535 File Offset: 0x00357735
		internal virtual void Reset(Unit value)
		{
			this.Top = value.Clone();
			this.Right = value.Clone();
			this.Bottom = value.Clone();
			this.Left = value.Clone();
		}

		// Token: 0x0600EB51 RID: 60241 RVA: 0x00359568 File Offset: 0x00357768
		public override bool Equals(object obj)
		{
			LayoutDecoratorBase layoutDecoratorBase = obj as LayoutDecoratorBase;
			if (layoutDecoratorBase != null)
			{
				return layoutDecoratorBase.Bottom.Equals(this.Bottom) && layoutDecoratorBase.Top.Equals(this.Top) && layoutDecoratorBase.Left.Equals(this.Left) && layoutDecoratorBase.Right.Equals(this.Right);
			}
			return base.Equals(obj);
		}

		// Token: 0x0600EB52 RID: 60242 RVA: 0x003595D9 File Offset: 0x003577D9
		public override int GetHashCode()
		{
			return this.Top.GetHashCode() ^ this.Right.GetHashCode() ^ this.Bottom.GetHashCode() ^ this.Left.GetHashCode();
		}

		// Token: 0x0600EB53 RID: 60243 RVA: 0x0035960C File Offset: 0x0035780C
		public static bool operator ==(LayoutDecoratorBase layoutDecoratorOne, LayoutDecoratorBase layoutDecoratorTwo)
		{
			if (object.ReferenceEquals(layoutDecoratorOne, null) == object.ReferenceEquals(layoutDecoratorTwo, null))
			{
				if (object.ReferenceEquals(layoutDecoratorOne, null))
				{
					return true;
				}
				if (layoutDecoratorOne.Left.Equals(layoutDecoratorTwo.Left) && layoutDecoratorOne.Top.Equals(layoutDecoratorTwo.Top) && layoutDecoratorOne.Right.Equals(layoutDecoratorTwo.Right))
				{
					return layoutDecoratorOne.Bottom.Equals(layoutDecoratorTwo.Bottom);
				}
			}
			return false;
		}

		// Token: 0x0600EB54 RID: 60244 RVA: 0x00359680 File Offset: 0x00357880
		public static bool operator !=(LayoutDecoratorBase layoutDecoratorOne, LayoutDecoratorBase layoutDecoratorTwo)
		{
			return !(layoutDecoratorOne == layoutDecoratorTwo);
		}

		// Token: 0x0600EB55 RID: 60245 RVA: 0x0035968C File Offset: 0x0035788C
		public object Clone()
		{
			LayoutDecoratorBase layoutDecoratorBase = (LayoutDecoratorBase)base.MemberwiseClone();
			layoutDecoratorBase.ViewState = base.CloneState();
			return layoutDecoratorBase;
		}

		// Token: 0x0600EB56 RID: 60246 RVA: 0x003596B2 File Offset: 0x003578B2
		public void CopyFrom(LayoutDecoratorBase layoutDecorator)
		{
			base.ViewState = layoutDecorator.CloneState();
		}

		// Token: 0x04004412 RID: 17426
		internal object chartLayoutDecoratorBaseContainerObject;
	}
}

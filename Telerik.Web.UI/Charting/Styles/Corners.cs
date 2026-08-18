using System;
using System.ComponentModel;
using System.Globalization;

namespace Telerik.Charting.Styles
{
	// Token: 0x02001766 RID: 5990
	[TypeConverter(typeof(CornersConverter))]
	public class Corners : StateManagedObject
	{
		// Token: 0x0600E9B6 RID: 59830 RVA: 0x00352478 File Offset: 0x00350678
		public Corners(object containerObject) : this()
		{
			this.cornersContainerObject = containerObject;
		}

		// Token: 0x0600E9B7 RID: 59831 RVA: 0x00352487 File Offset: 0x00350687
		public Corners()
		{
		}

		// Token: 0x0600E9B8 RID: 59832 RVA: 0x00352490 File Offset: 0x00350690
		public Corners(int roundSize) : this()
		{
			this.BottomLeft = (this.BottomRight = (this.TopLeft = (this.TopRight = CornerType.Round)));
			this.RoundSize = roundSize;
		}

		// Token: 0x0600E9B9 RID: 59833 RVA: 0x003524CC File Offset: 0x003506CC
		public Corners(CornerType topLeft, CornerType topRight, CornerType bottomLeft, CornerType bottomRight, int roundSize)
		{
			this.TopLeft = topLeft;
			this.TopRight = topRight;
			this.BottomLeft = bottomLeft;
			this.BottomRight = bottomRight;
			this.RoundSize = roundSize;
		}

		// Token: 0x170046E9 RID: 18153
		// (get) Token: 0x0600E9BA RID: 59834 RVA: 0x003524F9 File Offset: 0x003506F9
		// (set) Token: 0x0600E9BB RID: 59835 RVA: 0x0035251A File Offset: 0x0035071A
		[DefaultValue(CornerType.Rectangle)]
		[NotifyParentProperty(true)]
		[Description("Sets the type of the top left corner of the rectangular shape.")]
		[Browsable(true)]
		[Bindable(true)]
		[SkinnableProperty]
		public CornerType TopLeft
		{
			get
			{
				return (CornerType)(base.ViewState["TopLeft"] ?? CornerType.Rectangle);
			}
			set
			{
				base.ViewState["TopLeft"] = value;
			}
		}

		// Token: 0x170046EA RID: 18154
		// (get) Token: 0x0600E9BC RID: 59836 RVA: 0x00352532 File Offset: 0x00350732
		// (set) Token: 0x0600E9BD RID: 59837 RVA: 0x00352553 File Offset: 0x00350753
		[Description("Sets the type of the top right corner of the rectangular shape.")]
		[SkinnableProperty]
		[Browsable(true)]
		[Bindable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue(CornerType.Rectangle)]
		public CornerType TopRight
		{
			get
			{
				return (CornerType)(base.ViewState["TopRight"] ?? CornerType.Rectangle);
			}
			set
			{
				base.ViewState["TopRight"] = value;
			}
		}

		// Token: 0x170046EB RID: 18155
		// (get) Token: 0x0600E9BE RID: 59838 RVA: 0x0035256B File Offset: 0x0035076B
		// (set) Token: 0x0600E9BF RID: 59839 RVA: 0x0035258C File Offset: 0x0035078C
		[Bindable(true)]
		[SkinnableProperty]
		[Browsable(true)]
		[Description("Sets the type of the bottom left corner of the rectangular shape.")]
		[NotifyParentProperty(true)]
		[DefaultValue(CornerType.Rectangle)]
		public CornerType BottomLeft
		{
			get
			{
				return (CornerType)(base.ViewState["BottomLeft"] ?? CornerType.Rectangle);
			}
			set
			{
				base.ViewState["BottomLeft"] = value;
			}
		}

		// Token: 0x170046EC RID: 18156
		// (get) Token: 0x0600E9C0 RID: 59840 RVA: 0x003525A4 File Offset: 0x003507A4
		// (set) Token: 0x0600E9C1 RID: 59841 RVA: 0x003525C5 File Offset: 0x003507C5
		[Description("Sets the type of the bottom right corner of the rectangular shape.")]
		[SkinnableProperty]
		[Browsable(true)]
		[Bindable(true)]
		[NotifyParentProperty(true)]
		[DefaultValue(CornerType.Rectangle)]
		public CornerType BottomRight
		{
			get
			{
				return (CornerType)(base.ViewState["BottomRight"] ?? CornerType.Rectangle);
			}
			set
			{
				base.ViewState["BottomRight"] = value;
			}
		}

		// Token: 0x170046ED RID: 18157
		// (get) Token: 0x0600E9C2 RID: 59842 RVA: 0x003525DD File Offset: 0x003507DD
		// (set) Token: 0x0600E9C3 RID: 59843 RVA: 0x003525FE File Offset: 0x003507FE
		[Description("Sets the round size of the corner.")]
		[Browsable(true)]
		[DefaultValue(3)]
		[Bindable(true)]
		[NotifyParentProperty(true)]
		[SkinnableProperty]
		public int RoundSize
		{
			get
			{
				return (int)(base.ViewState["RoundSize"] ?? 3);
			}
			set
			{
				base.ViewState["RoundSize"] = value;
			}
		}

		// Token: 0x0600E9C4 RID: 59844 RVA: 0x00352616 File Offset: 0x00350816
		public static implicit operator Corners(string value)
		{
			return Corners.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x0600E9C5 RID: 59845 RVA: 0x00352623 File Offset: 0x00350823
		public static Corners Parse(string value)
		{
			return Corners.Parse(value, CultureInfo.CurrentCulture);
		}

		// Token: 0x0600E9C6 RID: 59846 RVA: 0x00352630 File Offset: 0x00350830
		public static Corners Parse(string value, CultureInfo culture)
		{
			return (Corners)new CornersConverter().ConvertFromInvariantString(value);
		}

		// Token: 0x0600E9C7 RID: 59847 RVA: 0x00352644 File Offset: 0x00350844
		public void SetCornersType(CornerType cornerType)
		{
			this.BottomRight = cornerType;
			this.BottomLeft = cornerType;
			this.TopRight = cornerType;
			this.TopLeft = cornerType;
		}

		// Token: 0x0600E9C8 RID: 59848 RVA: 0x00352674 File Offset: 0x00350874
		public override bool Equals(object obj)
		{
			if (obj == null)
			{
				return false;
			}
			Corners corners = obj as Corners;
			if (corners != null)
			{
				return corners.BottomLeft.Equals(this.BottomLeft) && corners.BottomRight.Equals(this.BottomRight) && corners.TopLeft.Equals(this.TopLeft) && corners.TopRight.Equals(this.TopRight) && corners.RoundSize == this.RoundSize;
			}
			return base.Equals(obj);
		}

		// Token: 0x0600E9C9 RID: 59849 RVA: 0x00352720 File Offset: 0x00350920
		public override int GetHashCode()
		{
			return this.BottomLeft.GetHashCode() ^ this.BottomRight.GetHashCode() ^ this.TopLeft.GetHashCode() ^ this.TopRight.GetHashCode() ^ this.RoundSize.GetHashCode();
		}

		// Token: 0x0600E9CA RID: 59850 RVA: 0x0035277F File Offset: 0x0035097F
		public object Clone()
		{
			return new Corners(this.TopLeft, this.TopRight, this.BottomLeft, this.BottomRight, this.RoundSize);
		}

		// Token: 0x170046EE RID: 18158
		// (get) Token: 0x0600E9CB RID: 59851 RVA: 0x003527A4 File Offset: 0x003509A4
		internal bool IsRectangle
		{
			get
			{
				return this.TopLeft == CornerType.Rectangle && this.TopRight == CornerType.Rectangle && this.BottomLeft == CornerType.Rectangle && this.BottomRight == CornerType.Rectangle;
			}
		}

		// Token: 0x0600E9CC RID: 59852 RVA: 0x003527C9 File Offset: 0x003509C9
		protected internal void CopyFrom(Corners sourceCorners)
		{
			this.TopLeft = sourceCorners.TopLeft;
			this.TopRight = sourceCorners.TopRight;
			this.BottomLeft = sourceCorners.BottomLeft;
			this.BottomRight = sourceCorners.BottomRight;
			this.RoundSize = sourceCorners.RoundSize;
		}

		// Token: 0x0600E9CD RID: 59853 RVA: 0x00352807 File Offset: 0x00350A07
		internal void Reset()
		{
			this.SetCornersType(CornerType.Rectangle);
			this.RoundSize = 3;
		}

		// Token: 0x0400433D RID: 17213
		internal object cornersContainerObject;
	}
}

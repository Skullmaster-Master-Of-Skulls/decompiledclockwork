using System;
using System.ComponentModel;
using System.Globalization;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x0200035F RID: 863
	public abstract class ScrollProperties
	{
		// Token: 0x17000D5B RID: 3419
		// (get) Token: 0x06003861 RID: 14433 RVA: 0x000FA5E4 File Offset: 0x000F87E4
		protected ScrollableControl ParentControl
		{
			get
			{
				return this.parent;
			}
		}

		// Token: 0x06003862 RID: 14434 RVA: 0x000FA5EC File Offset: 0x000F87EC
		protected ScrollProperties(ScrollableControl container)
		{
			this.parent = container;
		}

		// Token: 0x17000D5C RID: 3420
		// (get) Token: 0x06003863 RID: 14435 RVA: 0x000FA619 File Offset: 0x000F8819
		// (set) Token: 0x06003864 RID: 14436 RVA: 0x000FA621 File Offset: 0x000F8821
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("ScrollBarEnableDescr")]
		public bool Enabled
		{
			get
			{
				return this.enabled;
			}
			set
			{
				if (this.parent.AutoScroll)
				{
					return;
				}
				if (value != this.enabled)
				{
					this.enabled = value;
					this.EnableScroll(value);
				}
			}
		}

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x06003865 RID: 14437 RVA: 0x000FA648 File Offset: 0x000F8848
		// (set) Token: 0x06003866 RID: 14438 RVA: 0x000FA664 File Offset: 0x000F8864
		[SRCategory("CatBehavior")]
		[DefaultValue(10)]
		[SRDescription("ScrollBarLargeChangeDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public int LargeChange
		{
			get
			{
				return Math.Min(this.largeChange, this.maximum - this.minimum + 1);
			}
			set
			{
				if (this.largeChange != value)
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("LargeChange", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"LargeChange",
							value.ToString(CultureInfo.CurrentCulture),
							0.ToString(CultureInfo.CurrentCulture)
						}));
					}
					this.largeChange = value;
					this.largeChangeSetExternally = true;
					this.UpdateScrollInfo();
				}
			}
		}

		// Token: 0x17000D5E RID: 3422
		// (get) Token: 0x06003867 RID: 14439 RVA: 0x000FA6D5 File Offset: 0x000F88D5
		// (set) Token: 0x06003868 RID: 14440 RVA: 0x000FA6E0 File Offset: 0x000F88E0
		[SRCategory("CatBehavior")]
		[DefaultValue(100)]
		[SRDescription("ScrollBarMaximumDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public int Maximum
		{
			get
			{
				return this.maximum;
			}
			set
			{
				if (this.parent.AutoScroll)
				{
					return;
				}
				if (this.maximum != value)
				{
					if (this.minimum > value)
					{
						this.minimum = value;
					}
					if (value < this.value)
					{
						this.Value = value;
					}
					this.maximum = value;
					this.maximumSetExternally = true;
					this.UpdateScrollInfo();
				}
			}
		}

		// Token: 0x17000D5F RID: 3423
		// (get) Token: 0x06003869 RID: 14441 RVA: 0x000FA738 File Offset: 0x000F8938
		// (set) Token: 0x0600386A RID: 14442 RVA: 0x000FA740 File Offset: 0x000F8940
		[SRCategory("CatBehavior")]
		[DefaultValue(0)]
		[SRDescription("ScrollBarMinimumDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public int Minimum
		{
			get
			{
				return this.minimum;
			}
			set
			{
				if (this.parent.AutoScroll)
				{
					return;
				}
				if (this.minimum != value)
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("Minimum", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"Minimum",
							value.ToString(CultureInfo.CurrentCulture),
							0.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (this.maximum < value)
					{
						this.maximum = value;
					}
					if (value > this.value)
					{
						this.value = value;
					}
					this.minimum = value;
					this.UpdateScrollInfo();
				}
			}
		}

		// Token: 0x17000D60 RID: 3424
		// (get) Token: 0x0600386B RID: 14443
		internal abstract int PageSize { get; }

		// Token: 0x17000D61 RID: 3425
		// (get) Token: 0x0600386C RID: 14444
		internal abstract int Orientation { get; }

		// Token: 0x17000D62 RID: 3426
		// (get) Token: 0x0600386D RID: 14445
		internal abstract int HorizontalDisplayPosition { get; }

		// Token: 0x17000D63 RID: 3427
		// (get) Token: 0x0600386E RID: 14446
		internal abstract int VerticalDisplayPosition { get; }

		// Token: 0x17000D64 RID: 3428
		// (get) Token: 0x0600386F RID: 14447 RVA: 0x000FA7D8 File Offset: 0x000F89D8
		// (set) Token: 0x06003870 RID: 14448 RVA: 0x000FA7EC File Offset: 0x000F89EC
		[SRCategory("CatBehavior")]
		[DefaultValue(1)]
		[SRDescription("ScrollBarSmallChangeDescr")]
		public int SmallChange
		{
			get
			{
				return Math.Min(this.smallChange, this.LargeChange);
			}
			set
			{
				if (this.smallChange != value)
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("SmallChange", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"SmallChange",
							value.ToString(CultureInfo.CurrentCulture),
							0.ToString(CultureInfo.CurrentCulture)
						}));
					}
					this.smallChange = value;
					this.smallChangeSetExternally = true;
					this.UpdateScrollInfo();
				}
			}
		}

		// Token: 0x17000D65 RID: 3429
		// (get) Token: 0x06003871 RID: 14449 RVA: 0x000FA85D File Offset: 0x000F8A5D
		// (set) Token: 0x06003872 RID: 14450 RVA: 0x000FA868 File Offset: 0x000F8A68
		[SRCategory("CatBehavior")]
		[DefaultValue(0)]
		[Bindable(true)]
		[SRDescription("ScrollBarValueDescr")]
		public int Value
		{
			get
			{
				return this.value;
			}
			set
			{
				if (this.value != value)
				{
					if (value < this.minimum || value > this.maximum)
					{
						throw new ArgumentOutOfRangeException("Value", SR.GetString("InvalidBoundArgument", new object[]
						{
							"Value",
							value.ToString(CultureInfo.CurrentCulture),
							"'minimum'",
							"'maximum'"
						}));
					}
					this.value = value;
					this.UpdateScrollInfo();
					this.parent.SetDisplayFromScrollProps(this.HorizontalDisplayPosition, this.VerticalDisplayPosition);
				}
			}
		}

		// Token: 0x17000D66 RID: 3430
		// (get) Token: 0x06003873 RID: 14451 RVA: 0x000FA8F6 File Offset: 0x000F8AF6
		// (set) Token: 0x06003874 RID: 14452 RVA: 0x000FA900 File Offset: 0x000F8B00
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("ScrollBarVisibleDescr")]
		public bool Visible
		{
			get
			{
				return this.visible;
			}
			set
			{
				if (this.parent.AutoScroll)
				{
					return;
				}
				if (value != this.visible)
				{
					this.visible = value;
					this.parent.UpdateStylesCore();
					this.UpdateScrollInfo();
					this.parent.SetDisplayFromScrollProps(this.HorizontalDisplayPosition, this.VerticalDisplayPosition);
				}
			}
		}

		// Token: 0x06003875 RID: 14453 RVA: 0x000FA954 File Offset: 0x000F8B54
		internal void UpdateScrollInfo()
		{
			if (this.parent.IsHandleCreated && this.visible)
			{
				NativeMethods.SCROLLINFO scrollinfo = new NativeMethods.SCROLLINFO();
				scrollinfo.cbSize = Marshal.SizeOf(typeof(NativeMethods.SCROLLINFO));
				scrollinfo.fMask = 23;
				scrollinfo.nMin = this.minimum;
				scrollinfo.nMax = this.maximum;
				scrollinfo.nPage = (this.parent.AutoScroll ? this.PageSize : this.LargeChange);
				scrollinfo.nPos = this.value;
				scrollinfo.nTrackPos = 0;
				UnsafeNativeMethods.SetScrollInfo(new HandleRef(this.parent, this.parent.Handle), this.Orientation, scrollinfo, true);
			}
		}

		// Token: 0x06003876 RID: 14454 RVA: 0x000FAA10 File Offset: 0x000F8C10
		private void EnableScroll(bool enable)
		{
			if (enable)
			{
				UnsafeNativeMethods.EnableScrollBar(new HandleRef(this.parent, this.parent.Handle), this.Orientation, 0);
				return;
			}
			UnsafeNativeMethods.EnableScrollBar(new HandleRef(this.parent, this.parent.Handle), this.Orientation, 3);
		}

		// Token: 0x040021AF RID: 8623
		internal int minimum;

		// Token: 0x040021B0 RID: 8624
		internal int maximum = 100;

		// Token: 0x040021B1 RID: 8625
		internal int smallChange = 1;

		// Token: 0x040021B2 RID: 8626
		internal int largeChange = 10;

		// Token: 0x040021B3 RID: 8627
		internal int value;

		// Token: 0x040021B4 RID: 8628
		internal bool maximumSetExternally;

		// Token: 0x040021B5 RID: 8629
		internal bool smallChangeSetExternally;

		// Token: 0x040021B6 RID: 8630
		internal bool largeChangeSetExternally;

		// Token: 0x040021B7 RID: 8631
		private ScrollableControl parent;

		// Token: 0x040021B8 RID: 8632
		private const int SCROLL_LINE = 5;

		// Token: 0x040021B9 RID: 8633
		internal bool visible;

		// Token: 0x040021BA RID: 8634
		private bool enabled = true;
	}
}

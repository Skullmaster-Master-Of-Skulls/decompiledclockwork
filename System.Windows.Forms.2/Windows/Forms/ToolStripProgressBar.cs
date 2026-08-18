using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms
{
	// Token: 0x020003F8 RID: 1016
	[DefaultProperty("Value")]
	public class ToolStripProgressBar : ToolStripControlHost
	{
		// Token: 0x060045CD RID: 17869 RVA: 0x00126DE4 File Offset: 0x00124FE4
		public ToolStripProgressBar() : base(ToolStripProgressBar.CreateControlInstance())
		{
			ToolStripProgressBar.ToolStripProgressBarControl toolStripProgressBarControl = base.Control as ToolStripProgressBar.ToolStripProgressBarControl;
			if (toolStripProgressBarControl != null)
			{
				toolStripProgressBarControl.Owner = this;
			}
			if (DpiHelper.EnableToolStripHighDpiImprovements)
			{
				this.scaledDefaultMargin = DpiHelper.LogicalToDeviceUnits(ToolStripProgressBar.defaultMargin, 0);
				this.scaledDefaultStatusStripMargin = DpiHelper.LogicalToDeviceUnits(ToolStripProgressBar.defaultStatusStripMargin, 0);
			}
		}

		// Token: 0x060045CE RID: 17870 RVA: 0x00126E51 File Offset: 0x00125051
		public ToolStripProgressBar(string name) : this()
		{
			base.Name = name;
		}

		// Token: 0x17001129 RID: 4393
		// (get) Token: 0x060045CF RID: 17871 RVA: 0x00126E60 File Offset: 0x00125060
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ProgressBar ProgressBar
		{
			get
			{
				return base.Control as ProgressBar;
			}
		}

		// Token: 0x1700112A RID: 4394
		// (get) Token: 0x060045D0 RID: 17872 RVA: 0x000111A3 File Offset: 0x0000F3A3
		// (set) Token: 0x060045D1 RID: 17873 RVA: 0x000111AB File Offset: 0x0000F3AB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override Image BackgroundImage
		{
			get
			{
				return base.BackgroundImage;
			}
			set
			{
				base.BackgroundImage = value;
			}
		}

		// Token: 0x1700112B RID: 4395
		// (get) Token: 0x060045D2 RID: 17874 RVA: 0x000111B4 File Offset: 0x0000F3B4
		// (set) Token: 0x060045D3 RID: 17875 RVA: 0x000111BC File Offset: 0x0000F3BC
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override ImageLayout BackgroundImageLayout
		{
			get
			{
				return base.BackgroundImageLayout;
			}
			set
			{
				base.BackgroundImageLayout = value;
			}
		}

		// Token: 0x1700112C RID: 4396
		// (get) Token: 0x060045D4 RID: 17876 RVA: 0x00126E6D File Offset: 0x0012506D
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 15);
			}
		}

		// Token: 0x1700112D RID: 4397
		// (get) Token: 0x060045D5 RID: 17877 RVA: 0x00126E78 File Offset: 0x00125078
		protected internal override Padding DefaultMargin
		{
			get
			{
				if (base.Owner != null && base.Owner is StatusStrip)
				{
					return this.scaledDefaultStatusStripMargin;
				}
				return this.scaledDefaultMargin;
			}
		}

		// Token: 0x1700112E RID: 4398
		// (get) Token: 0x060045D6 RID: 17878 RVA: 0x00126E9C File Offset: 0x0012509C
		// (set) Token: 0x060045D7 RID: 17879 RVA: 0x00126EA9 File Offset: 0x001250A9
		[DefaultValue(100)]
		[SRCategory("CatBehavior")]
		[SRDescription("ProgressBarMarqueeAnimationSpeed")]
		public int MarqueeAnimationSpeed
		{
			get
			{
				return this.ProgressBar.MarqueeAnimationSpeed;
			}
			set
			{
				this.ProgressBar.MarqueeAnimationSpeed = value;
			}
		}

		// Token: 0x1700112F RID: 4399
		// (get) Token: 0x060045D8 RID: 17880 RVA: 0x00126EB7 File Offset: 0x001250B7
		// (set) Token: 0x060045D9 RID: 17881 RVA: 0x00126EC4 File Offset: 0x001250C4
		[DefaultValue(100)]
		[SRCategory("CatBehavior")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("ProgressBarMaximumDescr")]
		public int Maximum
		{
			get
			{
				return this.ProgressBar.Maximum;
			}
			set
			{
				this.ProgressBar.Maximum = value;
			}
		}

		// Token: 0x17001130 RID: 4400
		// (get) Token: 0x060045DA RID: 17882 RVA: 0x00126ED2 File Offset: 0x001250D2
		// (set) Token: 0x060045DB RID: 17883 RVA: 0x00126EDF File Offset: 0x001250DF
		[DefaultValue(0)]
		[SRCategory("CatBehavior")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("ProgressBarMinimumDescr")]
		public int Minimum
		{
			get
			{
				return this.ProgressBar.Minimum;
			}
			set
			{
				this.ProgressBar.Minimum = value;
			}
		}

		// Token: 0x17001131 RID: 4401
		// (get) Token: 0x060045DC RID: 17884 RVA: 0x00126EED File Offset: 0x001250ED
		// (set) Token: 0x060045DD RID: 17885 RVA: 0x00126EFA File Offset: 0x001250FA
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("ControlRightToLeftLayoutDescr")]
		public virtual bool RightToLeftLayout
		{
			get
			{
				return this.ProgressBar.RightToLeftLayout;
			}
			set
			{
				this.ProgressBar.RightToLeftLayout = value;
			}
		}

		// Token: 0x17001132 RID: 4402
		// (get) Token: 0x060045DE RID: 17886 RVA: 0x00126F08 File Offset: 0x00125108
		// (set) Token: 0x060045DF RID: 17887 RVA: 0x00126F15 File Offset: 0x00125115
		[DefaultValue(10)]
		[SRCategory("CatBehavior")]
		[SRDescription("ProgressBarStepDescr")]
		public int Step
		{
			get
			{
				return this.ProgressBar.Step;
			}
			set
			{
				this.ProgressBar.Step = value;
			}
		}

		// Token: 0x17001133 RID: 4403
		// (get) Token: 0x060045E0 RID: 17888 RVA: 0x00126F23 File Offset: 0x00125123
		// (set) Token: 0x060045E1 RID: 17889 RVA: 0x00126F30 File Offset: 0x00125130
		[DefaultValue(ProgressBarStyle.Blocks)]
		[SRCategory("CatBehavior")]
		[SRDescription("ProgressBarStyleDescr")]
		public ProgressBarStyle Style
		{
			get
			{
				return this.ProgressBar.Style;
			}
			set
			{
				this.ProgressBar.Style = value;
			}
		}

		// Token: 0x17001134 RID: 4404
		// (get) Token: 0x060045E2 RID: 17890 RVA: 0x00111FB1 File Offset: 0x001101B1
		// (set) Token: 0x060045E3 RID: 17891 RVA: 0x00111FBE File Offset: 0x001101BE
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public override string Text
		{
			get
			{
				return base.Control.Text;
			}
			set
			{
				base.Control.Text = value;
			}
		}

		// Token: 0x17001135 RID: 4405
		// (get) Token: 0x060045E4 RID: 17892 RVA: 0x00126F3E File Offset: 0x0012513E
		// (set) Token: 0x060045E5 RID: 17893 RVA: 0x00126F4B File Offset: 0x0012514B
		[DefaultValue(0)]
		[SRCategory("CatBehavior")]
		[Bindable(true)]
		[SRDescription("ProgressBarValueDescr")]
		public int Value
		{
			get
			{
				return this.ProgressBar.Value;
			}
			set
			{
				this.ProgressBar.Value = value;
			}
		}

		// Token: 0x060045E6 RID: 17894 RVA: 0x00126F59 File Offset: 0x00125159
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new ToolStripProgressBar.ToolStripProgressBarAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x060045E7 RID: 17895 RVA: 0x00126F70 File Offset: 0x00125170
		private static Control CreateControlInstance()
		{
			ProgressBar progressBar = AccessibilityImprovements.Level3 ? new ToolStripProgressBar.ToolStripProgressBarControl() : new ProgressBar();
			progressBar.Size = new Size(100, 15);
			return progressBar;
		}

		// Token: 0x060045E8 RID: 17896 RVA: 0x00126FA1 File Offset: 0x001251A1
		private void HandleRightToLeftLayoutChanged(object sender, EventArgs e)
		{
			this.OnRightToLeftLayoutChanged(e);
		}

		// Token: 0x060045E9 RID: 17897 RVA: 0x00126FAA File Offset: 0x001251AA
		protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
		{
			base.RaiseEvent(ToolStripProgressBar.EventRightToLeftLayoutChanged, e);
		}

		// Token: 0x060045EA RID: 17898 RVA: 0x00126FB8 File Offset: 0x001251B8
		protected override void OnSubscribeControlEvents(Control control)
		{
			ProgressBar progressBar = control as ProgressBar;
			if (progressBar != null)
			{
				progressBar.RightToLeftLayoutChanged += this.HandleRightToLeftLayoutChanged;
			}
			base.OnSubscribeControlEvents(control);
		}

		// Token: 0x060045EB RID: 17899 RVA: 0x00126FE8 File Offset: 0x001251E8
		protected override void OnUnsubscribeControlEvents(Control control)
		{
			ProgressBar progressBar = control as ProgressBar;
			if (progressBar != null)
			{
				progressBar.RightToLeftLayoutChanged -= this.HandleRightToLeftLayoutChanged;
			}
			base.OnUnsubscribeControlEvents(control);
		}

		// Token: 0x1400036A RID: 874
		// (add) Token: 0x060045EC RID: 17900 RVA: 0x00127018 File Offset: 0x00125218
		// (remove) Token: 0x060045ED RID: 17901 RVA: 0x00127021 File Offset: 0x00125221
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyEventHandler KeyDown
		{
			add
			{
				base.KeyDown += value;
			}
			remove
			{
				base.KeyDown -= value;
			}
		}

		// Token: 0x1400036B RID: 875
		// (add) Token: 0x060045EE RID: 17902 RVA: 0x0012702A File Offset: 0x0012522A
		// (remove) Token: 0x060045EF RID: 17903 RVA: 0x00127033 File Offset: 0x00125233
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyPressEventHandler KeyPress
		{
			add
			{
				base.KeyPress += value;
			}
			remove
			{
				base.KeyPress -= value;
			}
		}

		// Token: 0x1400036C RID: 876
		// (add) Token: 0x060045F0 RID: 17904 RVA: 0x0012703C File Offset: 0x0012523C
		// (remove) Token: 0x060045F1 RID: 17905 RVA: 0x00127045 File Offset: 0x00125245
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event KeyEventHandler KeyUp
		{
			add
			{
				base.KeyUp += value;
			}
			remove
			{
				base.KeyUp -= value;
			}
		}

		// Token: 0x1400036D RID: 877
		// (add) Token: 0x060045F2 RID: 17906 RVA: 0x0012704E File Offset: 0x0012524E
		// (remove) Token: 0x060045F3 RID: 17907 RVA: 0x00127057 File Offset: 0x00125257
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler LocationChanged
		{
			add
			{
				base.LocationChanged += value;
			}
			remove
			{
				base.LocationChanged -= value;
			}
		}

		// Token: 0x1400036E RID: 878
		// (add) Token: 0x060045F4 RID: 17908 RVA: 0x00127060 File Offset: 0x00125260
		// (remove) Token: 0x060045F5 RID: 17909 RVA: 0x00127069 File Offset: 0x00125269
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler OwnerChanged
		{
			add
			{
				base.OwnerChanged += value;
			}
			remove
			{
				base.OwnerChanged -= value;
			}
		}

		// Token: 0x1400036F RID: 879
		// (add) Token: 0x060045F6 RID: 17910 RVA: 0x00127072 File Offset: 0x00125272
		// (remove) Token: 0x060045F7 RID: 17911 RVA: 0x00127085 File Offset: 0x00125285
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnRightToLeftLayoutChangedDescr")]
		public event EventHandler RightToLeftLayoutChanged
		{
			add
			{
				base.Events.AddHandler(ToolStripProgressBar.EventRightToLeftLayoutChanged, value);
			}
			remove
			{
				base.Events.RemoveHandler(ToolStripProgressBar.EventRightToLeftLayoutChanged, value);
			}
		}

		// Token: 0x14000370 RID: 880
		// (add) Token: 0x060045F8 RID: 17912 RVA: 0x00127098 File Offset: 0x00125298
		// (remove) Token: 0x060045F9 RID: 17913 RVA: 0x001270A1 File Offset: 0x001252A1
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TextChanged
		{
			add
			{
				base.TextChanged += value;
			}
			remove
			{
				base.TextChanged -= value;
			}
		}

		// Token: 0x14000371 RID: 881
		// (add) Token: 0x060045FA RID: 17914 RVA: 0x001270AA File Offset: 0x001252AA
		// (remove) Token: 0x060045FB RID: 17915 RVA: 0x001270B3 File Offset: 0x001252B3
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler Validated
		{
			add
			{
				base.Validated += value;
			}
			remove
			{
				base.Validated -= value;
			}
		}

		// Token: 0x14000372 RID: 882
		// (add) Token: 0x060045FC RID: 17916 RVA: 0x001270BC File Offset: 0x001252BC
		// (remove) Token: 0x060045FD RID: 17917 RVA: 0x001270C5 File Offset: 0x001252C5
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event CancelEventHandler Validating
		{
			add
			{
				base.Validating += value;
			}
			remove
			{
				base.Validating -= value;
			}
		}

		// Token: 0x060045FE RID: 17918 RVA: 0x001270CE File Offset: 0x001252CE
		public void Increment(int value)
		{
			this.ProgressBar.Increment(value);
		}

		// Token: 0x060045FF RID: 17919 RVA: 0x001270DC File Offset: 0x001252DC
		public void PerformStep()
		{
			this.ProgressBar.PerformStep();
		}

		// Token: 0x0400267F RID: 9855
		internal static readonly object EventRightToLeftLayoutChanged = new object();

		// Token: 0x04002680 RID: 9856
		private static readonly Padding defaultMargin = new Padding(1, 2, 1, 1);

		// Token: 0x04002681 RID: 9857
		private static readonly Padding defaultStatusStripMargin = new Padding(1, 3, 1, 3);

		// Token: 0x04002682 RID: 9858
		private Padding scaledDefaultMargin = ToolStripProgressBar.defaultMargin;

		// Token: 0x04002683 RID: 9859
		private Padding scaledDefaultStatusStripMargin = ToolStripProgressBar.defaultStatusStripMargin;

		// Token: 0x02000816 RID: 2070
		[ComVisible(true)]
		internal class ToolStripProgressBarAccessibleObject : ToolStripItem.ToolStripItemAccessibleObject
		{
			// Token: 0x06006FBE RID: 28606 RVA: 0x0019AC47 File Offset: 0x00198E47
			public ToolStripProgressBarAccessibleObject(ToolStripProgressBar ownerItem) : base(ownerItem)
			{
				this.ownerItem = ownerItem;
			}

			// Token: 0x06006FBF RID: 28607 RVA: 0x0019AC57 File Offset: 0x00198E57
			internal override void ClearOwnerItem()
			{
				this.ownerItem = null;
				base.ClearOwnerItem();
			}

			// Token: 0x17001867 RID: 6247
			// (get) Token: 0x06006FC0 RID: 28608 RVA: 0x0019AC68 File Offset: 0x00198E68
			public override AccessibleRole Role
			{
				get
				{
					if (base.IsOwnerItemCleared())
					{
						return AccessibleRole.ProgressBar;
					}
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return AccessibleRole.ProgressBar;
				}
			}

			// Token: 0x06006FC1 RID: 28609 RVA: 0x0019AC94 File Offset: 0x00198E94
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (base.IsOwnerItemCleared())
				{
					return null;
				}
				if (direction == UnsafeNativeMethods.NavigateDirection.FirstChild || direction == UnsafeNativeMethods.NavigateDirection.LastChild)
				{
					return this.ownerItem.ProgressBar.AccessibilityObject;
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x04004329 RID: 17193
			private ToolStripProgressBar ownerItem;
		}

		// Token: 0x02000817 RID: 2071
		internal class ToolStripProgressBarControl : ProgressBar
		{
			// Token: 0x17001868 RID: 6248
			// (get) Token: 0x06006FC2 RID: 28610 RVA: 0x0019ACC0 File Offset: 0x00198EC0
			// (set) Token: 0x06006FC3 RID: 28611 RVA: 0x0019ACC8 File Offset: 0x00198EC8
			public ToolStripProgressBar Owner
			{
				get
				{
					return this.ownerItem;
				}
				set
				{
					this.ownerItem = value;
				}
			}

			// Token: 0x17001869 RID: 6249
			// (get) Token: 0x06006FC4 RID: 28612 RVA: 0x000A8615 File Offset: 0x000A6815
			internal override bool SupportsUiaProviders
			{
				get
				{
					return AccessibilityImprovements.Level3;
				}
			}

			// Token: 0x06006FC5 RID: 28613 RVA: 0x0019ACD1 File Offset: 0x00198ED1
			protected override AccessibleObject CreateAccessibilityInstance()
			{
				if (AccessibilityImprovements.Level3)
				{
					return new ToolStripProgressBar.ToolStripProgressBarControlAccessibleObject(this);
				}
				return base.CreateAccessibilityInstance();
			}

			// Token: 0x0400432A RID: 17194
			private ToolStripProgressBar ownerItem;
		}

		// Token: 0x02000818 RID: 2072
		internal class ToolStripProgressBarControlAccessibleObject : ProgressBar.ProgressBarAccessibleObject
		{
			// Token: 0x06006FC7 RID: 28615 RVA: 0x0019ACEF File Offset: 0x00198EEF
			public ToolStripProgressBarControlAccessibleObject(ToolStripProgressBar.ToolStripProgressBarControl toolStripProgressBarControl) : base(toolStripProgressBarControl)
			{
			}

			// Token: 0x1700186A RID: 6250
			// (get) Token: 0x06006FC8 RID: 28616 RVA: 0x0019ACF8 File Offset: 0x00198EF8
			internal override UnsafeNativeMethods.IRawElementProviderFragmentRoot FragmentRoot
			{
				get
				{
					ToolStripProgressBar.ToolStripProgressBarControl toolStripProgressBarControl = base.Owner as ToolStripProgressBar.ToolStripProgressBarControl;
					if (toolStripProgressBarControl != null)
					{
						return toolStripProgressBarControl.Owner.Owner.AccessibilityObject;
					}
					return base.FragmentRoot;
				}
			}

			// Token: 0x06006FC9 RID: 28617 RVA: 0x0019AD2C File Offset: 0x00198F2C
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (direction <= UnsafeNativeMethods.NavigateDirection.PreviousSibling)
				{
					ToolStripProgressBar.ToolStripProgressBarControl toolStripProgressBarControl = base.Owner as ToolStripProgressBar.ToolStripProgressBarControl;
					if (toolStripProgressBarControl != null)
					{
						return toolStripProgressBarControl.Owner.AccessibilityObject.FragmentNavigate(direction);
					}
				}
				return base.FragmentNavigate(direction);
			}

			// Token: 0x06006FCA RID: 28618 RVA: 0x0019AD68 File Offset: 0x00198F68
			internal override object GetPropertyValue(int propertyID)
			{
				if (AccessibilityImprovements.Level5 && propertyID == 30022)
				{
					if (!base.IsOwnerControlDestroyed())
					{
						ToolStripProgressBar.ToolStripProgressBarControl toolStripProgressBarControl = base.Owner as ToolStripProgressBar.ToolStripProgressBarControl;
						if (toolStripProgressBarControl != null)
						{
							return ToolStripItem.GetIsOffscreenPropertyValue(toolStripProgressBarControl.Owner.Placement, this.Bounds);
						}
					}
					return true;
				}
				return base.GetPropertyValue(propertyID);
			}
		}
	}
}

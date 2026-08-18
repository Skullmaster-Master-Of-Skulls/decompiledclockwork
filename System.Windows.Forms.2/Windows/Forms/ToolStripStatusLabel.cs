using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Windows.Forms.Automation;
using System.Windows.Forms.Design;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x02000406 RID: 1030
	[ToolStripItemDesignerAvailability(ToolStripItemDesignerAvailability.StatusStrip)]
	public class ToolStripStatusLabel : ToolStripLabel, IAutomationLiveRegion
	{
		// Token: 0x06004738 RID: 18232 RVA: 0x0012B0A4 File Offset: 0x001292A4
		public ToolStripStatusLabel()
		{
			this.Initialize();
		}

		// Token: 0x06004739 RID: 18233 RVA: 0x0012B0C8 File Offset: 0x001292C8
		public ToolStripStatusLabel(string text) : base(text, null, false, null)
		{
			this.Initialize();
		}

		// Token: 0x0600473A RID: 18234 RVA: 0x0012B0F0 File Offset: 0x001292F0
		public ToolStripStatusLabel(Image image) : base(null, image, false, null)
		{
			this.Initialize();
		}

		// Token: 0x0600473B RID: 18235 RVA: 0x0012B118 File Offset: 0x00129318
		public ToolStripStatusLabel(string text, Image image) : base(text, image, false, null)
		{
			this.Initialize();
		}

		// Token: 0x0600473C RID: 18236 RVA: 0x0012B140 File Offset: 0x00129340
		public ToolStripStatusLabel(string text, Image image, EventHandler onClick) : base(text, image, false, onClick, null)
		{
			this.Initialize();
		}

		// Token: 0x0600473D RID: 18237 RVA: 0x0012B169 File Offset: 0x00129369
		public ToolStripStatusLabel(string text, Image image, EventHandler onClick, string name) : base(text, image, false, onClick, name)
		{
			this.Initialize();
		}

		// Token: 0x0600473E RID: 18238 RVA: 0x0012B193 File Offset: 0x00129393
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new ToolStripStatusLabel.ToolStripStatusLabelAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x0600473F RID: 18239 RVA: 0x0012B1A9 File Offset: 0x001293A9
		internal override ToolStripItemInternalLayout CreateInternalLayout()
		{
			return new ToolStripStatusLabel.ToolStripStatusLabelLayout(this);
		}

		// Token: 0x1700117D RID: 4477
		// (get) Token: 0x06004740 RID: 18240 RVA: 0x0012B1B1 File Offset: 0x001293B1
		// (set) Token: 0x06004741 RID: 18241 RVA: 0x0012B1B9 File Offset: 0x001293B9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public new ToolStripItemAlignment Alignment
		{
			get
			{
				return base.Alignment;
			}
			set
			{
				base.Alignment = value;
			}
		}

		// Token: 0x1700117E RID: 4478
		// (get) Token: 0x06004742 RID: 18242 RVA: 0x0012B1C2 File Offset: 0x001293C2
		// (set) Token: 0x06004743 RID: 18243 RVA: 0x0012B1CC File Offset: 0x001293CC
		[DefaultValue(Border3DStyle.Flat)]
		[SRDescription("ToolStripStatusLabelBorderStyleDescr")]
		[SRCategory("CatAppearance")]
		public Border3DStyle BorderStyle
		{
			get
			{
				return this.borderStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid_NotSequential(value, (int)value, new int[]
				{
					8192,
					9,
					6,
					16394,
					5,
					4,
					1,
					10,
					8,
					2
				}))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(Border3DStyle));
				}
				if (this.borderStyle != value)
				{
					this.borderStyle = value;
					base.Invalidate();
				}
			}
		}

		// Token: 0x1700117F RID: 4479
		// (get) Token: 0x06004744 RID: 18244 RVA: 0x0012B225 File Offset: 0x00129425
		// (set) Token: 0x06004745 RID: 18245 RVA: 0x0012B22D File Offset: 0x0012942D
		[DefaultValue(ToolStripStatusLabelBorderSides.None)]
		[SRDescription("ToolStripStatusLabelBorderSidesDescr")]
		[SRCategory("CatAppearance")]
		public ToolStripStatusLabelBorderSides BorderSides
		{
			get
			{
				return this.borderSides;
			}
			set
			{
				if (this.borderSides != value)
				{
					this.borderSides = value;
					LayoutTransaction.DoLayout(base.Owner, this, PropertyNames.BorderStyle);
					base.Invalidate();
				}
			}
		}

		// Token: 0x06004746 RID: 18246 RVA: 0x0012B256 File Offset: 0x00129456
		private void Initialize()
		{
			if (DpiHelper.EnableToolStripHighDpiImprovements)
			{
				this.scaledDefaultMargin = DpiHelper.LogicalToDeviceUnits(ToolStripStatusLabel.defaultMargin, 0);
			}
		}

		// Token: 0x17001180 RID: 4480
		// (get) Token: 0x06004747 RID: 18247 RVA: 0x0012B270 File Offset: 0x00129470
		protected internal override Padding DefaultMargin
		{
			get
			{
				return this.scaledDefaultMargin;
			}
		}

		// Token: 0x17001181 RID: 4481
		// (get) Token: 0x06004748 RID: 18248 RVA: 0x0012B278 File Offset: 0x00129478
		// (set) Token: 0x06004749 RID: 18249 RVA: 0x0012B280 File Offset: 0x00129480
		[DefaultValue(false)]
		[SRDescription("ToolStripStatusLabelSpringDescr")]
		[SRCategory("CatAppearance")]
		public bool Spring
		{
			get
			{
				return this.spring;
			}
			set
			{
				if (this.spring != value)
				{
					this.spring = value;
					if (base.ParentInternal != null)
					{
						LayoutTransaction.DoLayout(base.ParentInternal, this, PropertyNames.Spring);
					}
				}
			}
		}

		// Token: 0x17001182 RID: 4482
		// (get) Token: 0x0600474A RID: 18250 RVA: 0x0012B2AB File Offset: 0x001294AB
		// (set) Token: 0x0600474B RID: 18251 RVA: 0x0012B2B3 File Offset: 0x001294B3
		[SRCategory("CatAccessibility")]
		[DefaultValue(AutomationLiveSetting.Off)]
		[SRDescription("LiveRegionAutomationLiveSettingDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public AutomationLiveSetting LiveSetting
		{
			get
			{
				return this.liveSetting;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(AutomationLiveSetting));
				}
				this.liveSetting = value;
			}
		}

		// Token: 0x0600474C RID: 18252 RVA: 0x0012B2E2 File Offset: 0x001294E2
		protected override void OnTextChanged(EventArgs e)
		{
			base.OnTextChanged(e);
			if (AccessibilityImprovements.Level3 && this.LiveSetting != AutomationLiveSetting.Off)
			{
				base.AccessibilityObject.RaiseLiveRegionChanged();
			}
		}

		// Token: 0x0600474D RID: 18253 RVA: 0x0012B306 File Offset: 0x00129506
		public override Size GetPreferredSize(Size constrainingSize)
		{
			if (this.BorderSides != ToolStripStatusLabelBorderSides.None)
			{
				return base.GetPreferredSize(constrainingSize) + new Size(4, 4);
			}
			return base.GetPreferredSize(constrainingSize);
		}

		// Token: 0x0600474E RID: 18254 RVA: 0x0012B32C File Offset: 0x0012952C
		protected override void OnPaint(PaintEventArgs e)
		{
			if (base.Owner != null)
			{
				ToolStripRenderer renderer = base.Renderer;
				renderer.DrawToolStripStatusLabelBackground(new ToolStripItemRenderEventArgs(e.Graphics, this));
				if ((this.DisplayStyle & ToolStripItemDisplayStyle.Image) == ToolStripItemDisplayStyle.Image)
				{
					renderer.DrawItemImage(new ToolStripItemImageRenderEventArgs(e.Graphics, this, base.InternalLayout.ImageRectangle));
				}
				base.PaintText(e.Graphics);
			}
		}

		// Token: 0x040026D9 RID: 9945
		private static readonly Padding defaultMargin = new Padding(0, 3, 0, 2);

		// Token: 0x040026DA RID: 9946
		private Padding scaledDefaultMargin = ToolStripStatusLabel.defaultMargin;

		// Token: 0x040026DB RID: 9947
		private Border3DStyle borderStyle = Border3DStyle.Flat;

		// Token: 0x040026DC RID: 9948
		private ToolStripStatusLabelBorderSides borderSides;

		// Token: 0x040026DD RID: 9949
		private bool spring;

		// Token: 0x040026DE RID: 9950
		private AutomationLiveSetting liveSetting;

		// Token: 0x02000821 RID: 2081
		[ComVisible(true)]
		internal class ToolStripStatusLabelAccessibleObject : ToolStripLabel.ToolStripLabelAccessibleObject
		{
			// Token: 0x06006FFD RID: 28669 RVA: 0x0019B35E File Offset: 0x0019955E
			public ToolStripStatusLabelAccessibleObject(ToolStripStatusLabel ownerItem) : base(ownerItem)
			{
				this.ownerItem = ownerItem;
			}

			// Token: 0x06006FFE RID: 28670 RVA: 0x0019B36E File Offset: 0x0019956E
			internal override void ClearOwnerItem()
			{
				this.ownerItem = null;
				base.ClearOwnerItem();
			}

			// Token: 0x06006FFF RID: 28671 RVA: 0x0019B37D File Offset: 0x0019957D
			public override bool RaiseLiveRegionChanged()
			{
				return base.RaiseAutomationEvent(20024);
			}

			// Token: 0x06007000 RID: 28672 RVA: 0x0019B38A File Offset: 0x0019958A
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30003)
				{
					return 50020;
				}
				if (propertyID == 30135)
				{
					return this.ownerItem.LiveSetting;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x04004337 RID: 17207
			private ToolStripStatusLabel ownerItem;
		}

		// Token: 0x02000822 RID: 2082
		private class ToolStripStatusLabelLayout : ToolStripItemInternalLayout
		{
			// Token: 0x06007001 RID: 28673 RVA: 0x0019B3BF File Offset: 0x001995BF
			public ToolStripStatusLabelLayout(ToolStripStatusLabel owner) : base(owner)
			{
				this.owner = owner;
			}

			// Token: 0x06007002 RID: 28674 RVA: 0x0019B3D0 File Offset: 0x001995D0
			protected override ToolStripItemInternalLayout.ToolStripItemLayoutOptions CommonLayoutOptions()
			{
				ToolStripItemInternalLayout.ToolStripItemLayoutOptions toolStripItemLayoutOptions = base.CommonLayoutOptions();
				toolStripItemLayoutOptions.borderSize = 0;
				return toolStripItemLayoutOptions;
			}

			// Token: 0x04004338 RID: 17208
			private ToolStripStatusLabel owner;
		}
	}
}

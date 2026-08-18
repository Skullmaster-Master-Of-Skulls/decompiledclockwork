using System;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x0200031B RID: 795
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("BorderStyle")]
	[DefaultEvent("Paint")]
	[Docking(DockingBehavior.Ask)]
	[Designer("System.Windows.Forms.Design.PanelDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionPanel")]
	public class Panel : ScrollableControl
	{
		// Token: 0x0600326D RID: 12909 RVA: 0x000E2771 File Offset: 0x000E0971
		public Panel()
		{
			base.SetState2(2048, true);
			this.TabStop = false;
			base.SetStyle(ControlStyles.Selectable | ControlStyles.AllPaintingInWmPaint, false);
			base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
		}

		// Token: 0x17000BD2 RID: 3026
		// (get) Token: 0x0600326E RID: 12910 RVA: 0x00011A45 File Offset: 0x0000FC45
		// (set) Token: 0x0600326F RID: 12911 RVA: 0x00011A4D File Offset: 0x0000FC4D
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override bool AutoSize
		{
			get
			{
				return base.AutoSize;
			}
			set
			{
				base.AutoSize = value;
			}
		}

		// Token: 0x1400024C RID: 588
		// (add) Token: 0x06003270 RID: 12912 RVA: 0x00011A56 File Offset: 0x0000FC56
		// (remove) Token: 0x06003271 RID: 12913 RVA: 0x00011A5F File Offset: 0x0000FC5F
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnAutoSizeChangedDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public new event EventHandler AutoSizeChanged
		{
			add
			{
				base.AutoSizeChanged += value;
			}
			remove
			{
				base.AutoSizeChanged -= value;
			}
		}

		// Token: 0x17000BD3 RID: 3027
		// (get) Token: 0x06003272 RID: 12914 RVA: 0x000236ED File Offset: 0x000218ED
		// (set) Token: 0x06003273 RID: 12915 RVA: 0x000E27A4 File Offset: 0x000E09A4
		[SRDescription("ControlAutoSizeModeDescr")]
		[SRCategory("CatLayout")]
		[Browsable(true)]
		[DefaultValue(AutoSizeMode.GrowOnly)]
		[Localizable(true)]
		public virtual AutoSizeMode AutoSizeMode
		{
			get
			{
				return base.GetAutoSizeMode();
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(AutoSizeMode));
				}
				if (base.GetAutoSizeMode() != value)
				{
					base.SetAutoSizeMode(value);
					if (this.ParentInternal != null)
					{
						if (this.ParentInternal.LayoutEngine == DefaultLayout.Instance)
						{
							this.ParentInternal.LayoutEngine.InitLayout(this, BoundsSpecified.Size);
						}
						LayoutTransaction.DoLayout(this.ParentInternal, this, PropertyNames.AutoSize);
					}
				}
			}
		}

		// Token: 0x17000BD4 RID: 3028
		// (get) Token: 0x06003274 RID: 12916 RVA: 0x000E2825 File Offset: 0x000E0A25
		// (set) Token: 0x06003275 RID: 12917 RVA: 0x000E282D File Offset: 0x000E0A2D
		[SRCategory("CatAppearance")]
		[DefaultValue(BorderStyle.None)]
		[DispId(-504)]
		[SRDescription("PanelBorderStyleDescr")]
		public BorderStyle BorderStyle
		{
			get
			{
				return this.borderStyle;
			}
			set
			{
				if (this.borderStyle != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(BorderStyle));
					}
					this.borderStyle = value;
					base.UpdateStyles();
				}
			}
		}

		// Token: 0x17000BD5 RID: 3029
		// (get) Token: 0x06003276 RID: 12918 RVA: 0x000E286C File Offset: 0x000E0A6C
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ExStyle |= 65536;
				createParams.ExStyle &= -513;
				createParams.Style &= -8388609;
				BorderStyle borderStyle = this.borderStyle;
				if (borderStyle != BorderStyle.FixedSingle)
				{
					if (borderStyle == BorderStyle.Fixed3D)
					{
						createParams.ExStyle |= 512;
					}
				}
				else
				{
					createParams.Style |= 8388608;
				}
				return createParams;
			}
		}

		// Token: 0x17000BD6 RID: 3030
		// (get) Token: 0x06003277 RID: 12919 RVA: 0x000B91B8 File Offset: 0x000B73B8
		protected override Size DefaultSize
		{
			get
			{
				return new Size(200, 100);
			}
		}

		// Token: 0x06003278 RID: 12920 RVA: 0x000E28EC File Offset: 0x000E0AEC
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			Size sz = this.SizeFromClientSize(Size.Empty);
			Size sz2 = sz + base.Padding.Size;
			return this.LayoutEngine.GetPreferredSize(this, proposedSize - sz2) + sz2;
		}

		// Token: 0x1400024D RID: 589
		// (add) Token: 0x06003279 RID: 12921 RVA: 0x000B9380 File Offset: 0x000B7580
		// (remove) Token: 0x0600327A RID: 12922 RVA: 0x000B9389 File Offset: 0x000B7589
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

		// Token: 0x1400024E RID: 590
		// (add) Token: 0x0600327B RID: 12923 RVA: 0x000B9392 File Offset: 0x000B7592
		// (remove) Token: 0x0600327C RID: 12924 RVA: 0x000B939B File Offset: 0x000B759B
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

		// Token: 0x1400024F RID: 591
		// (add) Token: 0x0600327D RID: 12925 RVA: 0x000B93A4 File Offset: 0x000B75A4
		// (remove) Token: 0x0600327E RID: 12926 RVA: 0x000B93AD File Offset: 0x000B75AD
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

		// Token: 0x17000BD7 RID: 3031
		// (get) Token: 0x0600327F RID: 12927 RVA: 0x000B2611 File Offset: 0x000B0811
		// (set) Token: 0x06003280 RID: 12928 RVA: 0x000B2619 File Offset: 0x000B0819
		[DefaultValue(false)]
		public new bool TabStop
		{
			get
			{
				return base.TabStop;
			}
			set
			{
				base.TabStop = value;
			}
		}

		// Token: 0x17000BD8 RID: 3032
		// (get) Token: 0x06003281 RID: 12929 RVA: 0x00013A28 File Offset: 0x00011C28
		// (set) Token: 0x06003282 RID: 12930 RVA: 0x00024185 File Offset: 0x00022385
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
			}
		}

		// Token: 0x14000250 RID: 592
		// (add) Token: 0x06003283 RID: 12931 RVA: 0x00046771 File Offset: 0x00044971
		// (remove) Token: 0x06003284 RID: 12932 RVA: 0x0004677A File Offset: 0x0004497A
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

		// Token: 0x06003285 RID: 12933 RVA: 0x000E2933 File Offset: 0x000E0B33
		protected override void OnResize(EventArgs eventargs)
		{
			if (base.DesignMode && this.borderStyle == BorderStyle.None)
			{
				base.Invalidate();
			}
			base.OnResize(eventargs);
		}

		// Token: 0x06003286 RID: 12934 RVA: 0x000E2954 File Offset: 0x000E0B54
		internal override void PrintToMetaFileRecursive(HandleRef hDC, IntPtr lParam, Rectangle bounds)
		{
			base.PrintToMetaFileRecursive(hDC, lParam, bounds);
			using (new WindowsFormsUtils.DCMapping(hDC, bounds))
			{
				using (Graphics graphics = Graphics.FromHdcInternal(hDC.Handle))
				{
					ControlPaint.PrintBorder(graphics, new Rectangle(Point.Empty, base.Size), this.BorderStyle, Border3DStyle.Sunken);
				}
			}
		}

		// Token: 0x06003287 RID: 12935 RVA: 0x000E29D4 File Offset: 0x000E0BD4
		private static string StringFromBorderStyle(BorderStyle value)
		{
			Type typeFromHandle = typeof(BorderStyle);
			if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
			{
				return "[Invalid BorderStyle]";
			}
			return typeFromHandle.ToString() + "." + value.ToString();
		}

		// Token: 0x06003288 RID: 12936 RVA: 0x000E2A20 File Offset: 0x000E0C20
		public override string ToString()
		{
			string str = base.ToString();
			return str + ", BorderStyle: " + Panel.StringFromBorderStyle(this.borderStyle);
		}

		// Token: 0x04001E7D RID: 7805
		private BorderStyle borderStyle;
	}
}

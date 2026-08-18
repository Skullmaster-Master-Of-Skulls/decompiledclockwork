using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Text;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x0200039E RID: 926
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Designer("System.Windows.Forms.Design.TabPageDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItem(false)]
	[DesignTimeVisible(false)]
	[DefaultEvent("Click")]
	[DefaultProperty("Text")]
	public class TabPage : Panel
	{
		// Token: 0x06003C63 RID: 15459 RVA: 0x00107215 File Offset: 0x00105415
		public TabPage()
		{
			base.SetStyle(ControlStyles.CacheText, true);
			this.Text = null;
		}

		// Token: 0x17000EB5 RID: 3765
		// (get) Token: 0x06003C64 RID: 15460 RVA: 0x00013062 File Offset: 0x00011262
		// (set) Token: 0x06003C65 RID: 15461 RVA: 0x000072B6 File Offset: 0x000054B6
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[Localizable(false)]
		public override AutoSizeMode AutoSizeMode
		{
			get
			{
				return AutoSizeMode.GrowOnly;
			}
			set
			{
			}
		}

		// Token: 0x17000EB6 RID: 3766
		// (get) Token: 0x06003C66 RID: 15462 RVA: 0x000FFEE1 File Offset: 0x000FE0E1
		// (set) Token: 0x06003C67 RID: 15463 RVA: 0x000FFEE9 File Offset: 0x000FE0E9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x140002DF RID: 735
		// (add) Token: 0x06003C68 RID: 15464 RVA: 0x000FFEF2 File Offset: 0x000FE0F2
		// (remove) Token: 0x06003C69 RID: 15465 RVA: 0x000FFEFB File Offset: 0x000FE0FB
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnAutoSizeChangedDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x17000EB7 RID: 3767
		// (get) Token: 0x06003C6A RID: 15466 RVA: 0x0010723C File Offset: 0x0010543C
		// (set) Token: 0x06003C6B RID: 15467 RVA: 0x0010728C File Offset: 0x0010548C
		[SRCategory("CatAppearance")]
		[SRDescription("ControlBackColorDescr")]
		public override Color BackColor
		{
			get
			{
				Color backColor = base.BackColor;
				if (backColor != Control.DefaultBackColor)
				{
					return backColor;
				}
				TabControl tabControl = this.ParentInternal as TabControl;
				if (Application.RenderWithVisualStyles && this.UseVisualStyleBackColor && tabControl != null && tabControl.Appearance == TabAppearance.Normal)
				{
					return Color.Transparent;
				}
				return backColor;
			}
			set
			{
				if (base.DesignMode)
				{
					if (value != Color.Empty)
					{
						PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this)["UseVisualStyleBackColor"];
						if (propertyDescriptor != null)
						{
							propertyDescriptor.SetValue(this, false);
						}
					}
				}
				else
				{
					this.UseVisualStyleBackColor = false;
				}
				base.BackColor = value;
			}
		}

		// Token: 0x06003C6C RID: 15468 RVA: 0x001072DF File Offset: 0x001054DF
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new TabPage.TabPageControlCollection(this);
		}

		// Token: 0x17000EB8 RID: 3768
		// (get) Token: 0x06003C6D RID: 15469 RVA: 0x001072E7 File Offset: 0x001054E7
		internal ImageList.Indexer ImageIndexer
		{
			get
			{
				if (this.imageIndexer == null)
				{
					this.imageIndexer = new ImageList.Indexer();
				}
				return this.imageIndexer;
			}
		}

		// Token: 0x17000EB9 RID: 3769
		// (get) Token: 0x06003C6E RID: 15470 RVA: 0x00107302 File Offset: 0x00105502
		// (set) Token: 0x06003C6F RID: 15471 RVA: 0x00107310 File Offset: 0x00105510
		[TypeConverter(typeof(ImageIndexConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(-1)]
		[SRDescription("TabItemImageIndexDescr")]
		public int ImageIndex
		{
			get
			{
				return this.ImageIndexer.Index;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("ImageIndex", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"imageIndex",
						value.ToString(CultureInfo.CurrentCulture),
						-1.ToString(CultureInfo.CurrentCulture)
					}));
				}
				TabControl tabControl = this.ParentInternal as TabControl;
				if (tabControl != null)
				{
					this.ImageIndexer.ImageList = tabControl.ImageList;
				}
				this.ImageIndexer.Index = value;
				this.UpdateParent();
			}
		}

		// Token: 0x17000EBA RID: 3770
		// (get) Token: 0x06003C70 RID: 15472 RVA: 0x00107396 File Offset: 0x00105596
		// (set) Token: 0x06003C71 RID: 15473 RVA: 0x001073A4 File Offset: 0x001055A4
		[TypeConverter(typeof(ImageKeyConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[DefaultValue("")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("TabItemImageIndexDescr")]
		public string ImageKey
		{
			get
			{
				return this.ImageIndexer.Key;
			}
			set
			{
				this.ImageIndexer.Key = value;
				TabControl tabControl = this.ParentInternal as TabControl;
				if (tabControl != null)
				{
					this.ImageIndexer.ImageList = tabControl.ImageList;
				}
				this.UpdateParent();
			}
		}

		// Token: 0x06003C72 RID: 15474 RVA: 0x001073E3 File Offset: 0x001055E3
		public TabPage(string text) : this()
		{
			this.Text = text;
		}

		// Token: 0x17000EBB RID: 3771
		// (get) Token: 0x06003C73 RID: 15475 RVA: 0x000FFF04 File Offset: 0x000FE104
		// (set) Token: 0x06003C74 RID: 15476 RVA: 0x000FFF0C File Offset: 0x000FE10C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override AnchorStyles Anchor
		{
			get
			{
				return base.Anchor;
			}
			set
			{
				base.Anchor = value;
			}
		}

		// Token: 0x17000EBC RID: 3772
		// (get) Token: 0x06003C75 RID: 15477 RVA: 0x000FC6F6 File Offset: 0x000FA8F6
		// (set) Token: 0x06003C76 RID: 15478 RVA: 0x000FFF26 File Offset: 0x000FE126
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				base.Dock = value;
			}
		}

		// Token: 0x140002E0 RID: 736
		// (add) Token: 0x06003C77 RID: 15479 RVA: 0x00100028 File Offset: 0x000FE228
		// (remove) Token: 0x06003C78 RID: 15480 RVA: 0x00100031 File Offset: 0x000FE231
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler DockChanged
		{
			add
			{
				base.DockChanged += value;
			}
			remove
			{
				base.DockChanged -= value;
			}
		}

		// Token: 0x17000EBD RID: 3773
		// (get) Token: 0x06003C79 RID: 15481 RVA: 0x0001A261 File Offset: 0x00018461
		// (set) Token: 0x06003C7A RID: 15482 RVA: 0x0001A269 File Offset: 0x00018469
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool Enabled
		{
			get
			{
				return base.Enabled;
			}
			set
			{
				base.Enabled = value;
			}
		}

		// Token: 0x140002E1 RID: 737
		// (add) Token: 0x06003C7B RID: 15483 RVA: 0x001073F2 File Offset: 0x001055F2
		// (remove) Token: 0x06003C7C RID: 15484 RVA: 0x001073FB File Offset: 0x001055FB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler EnabledChanged
		{
			add
			{
				base.EnabledChanged += value;
			}
			remove
			{
				base.EnabledChanged -= value;
			}
		}

		// Token: 0x17000EBE RID: 3774
		// (get) Token: 0x06003C7D RID: 15485 RVA: 0x00107404 File Offset: 0x00105604
		// (set) Token: 0x06003C7E RID: 15486 RVA: 0x0010740C File Offset: 0x0010560C
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[SRDescription("TabItemUseVisualStyleBackColorDescr")]
		public bool UseVisualStyleBackColor
		{
			get
			{
				return this.useVisualStyleBackColor;
			}
			set
			{
				this.useVisualStyleBackColor = value;
				base.Invalidate(true);
			}
		}

		// Token: 0x17000EBF RID: 3775
		// (get) Token: 0x06003C7F RID: 15487 RVA: 0x000B184D File Offset: 0x000AFA4D
		// (set) Token: 0x06003C80 RID: 15488 RVA: 0x000B1855 File Offset: 0x000AFA55
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Point Location
		{
			get
			{
				return base.Location;
			}
			set
			{
				base.Location = value;
			}
		}

		// Token: 0x140002E2 RID: 738
		// (add) Token: 0x06003C81 RID: 15489 RVA: 0x0010003A File Offset: 0x000FE23A
		// (remove) Token: 0x06003C82 RID: 15490 RVA: 0x00100043 File Offset: 0x000FE243
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

		// Token: 0x17000EC0 RID: 3776
		// (get) Token: 0x06003C83 RID: 15491 RVA: 0x00011C22 File Offset: 0x0000FE22
		// (set) Token: 0x06003C84 RID: 15492 RVA: 0x000FFF77 File Offset: 0x000FE177
		[DefaultValue(typeof(Size), "0, 0")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Size MaximumSize
		{
			get
			{
				return base.MaximumSize;
			}
			set
			{
				base.MaximumSize = value;
			}
		}

		// Token: 0x17000EC1 RID: 3777
		// (get) Token: 0x06003C85 RID: 15493 RVA: 0x00011C3F File Offset: 0x0000FE3F
		// (set) Token: 0x06003C86 RID: 15494 RVA: 0x000FFF6E File Offset: 0x000FE16E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Size MinimumSize
		{
			get
			{
				return base.MinimumSize;
			}
			set
			{
				base.MinimumSize = value;
			}
		}

		// Token: 0x17000EC2 RID: 3778
		// (get) Token: 0x06003C87 RID: 15495 RVA: 0x0010741C File Offset: 0x0010561C
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Size PreferredSize
		{
			get
			{
				return base.PreferredSize;
			}
		}

		// Token: 0x17000EC3 RID: 3779
		// (get) Token: 0x06003C88 RID: 15496 RVA: 0x000B25EE File Offset: 0x000B07EE
		// (set) Token: 0x06003C89 RID: 15497 RVA: 0x000B25F6 File Offset: 0x000B07F6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new int TabIndex
		{
			get
			{
				return base.TabIndex;
			}
			set
			{
				base.TabIndex = value;
			}
		}

		// Token: 0x17000EC4 RID: 3780
		// (get) Token: 0x06003C8A RID: 15498 RVA: 0x00013062 File Offset: 0x00011262
		internal override bool RenderTransparencyWithVisualStyles
		{
			get
			{
				return true;
			}
		}

		// Token: 0x140002E3 RID: 739
		// (add) Token: 0x06003C8B RID: 15499 RVA: 0x000B25FF File Offset: 0x000B07FF
		// (remove) Token: 0x06003C8C RID: 15500 RVA: 0x000B2608 File Offset: 0x000B0808
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TabIndexChanged
		{
			add
			{
				base.TabIndexChanged += value;
			}
			remove
			{
				base.TabIndexChanged -= value;
			}
		}

		// Token: 0x17000EC5 RID: 3781
		// (get) Token: 0x06003C8D RID: 15501 RVA: 0x000FFFC0 File Offset: 0x000FE1C0
		// (set) Token: 0x06003C8E RID: 15502 RVA: 0x000FFFC8 File Offset: 0x000FE1C8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x140002E4 RID: 740
		// (add) Token: 0x06003C8F RID: 15503 RVA: 0x000B2622 File Offset: 0x000B0822
		// (remove) Token: 0x06003C90 RID: 15504 RVA: 0x000B262B File Offset: 0x000B082B
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler TabStopChanged
		{
			add
			{
				base.TabStopChanged += value;
			}
			remove
			{
				base.TabStopChanged -= value;
			}
		}

		// Token: 0x17000EC6 RID: 3782
		// (get) Token: 0x06003C91 RID: 15505 RVA: 0x00107424 File Offset: 0x00105624
		// (set) Token: 0x06003C92 RID: 15506 RVA: 0x0010742C File Offset: 0x0010562C
		[Localizable(true)]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public override string Text
		{
			get
			{
				return base.Text;
			}
			set
			{
				base.Text = value;
				this.UpdateParent();
			}
		}

		// Token: 0x140002E5 RID: 741
		// (add) Token: 0x06003C93 RID: 15507 RVA: 0x0010743B File Offset: 0x0010563B
		// (remove) Token: 0x06003C94 RID: 15508 RVA: 0x00107444 File Offset: 0x00105644
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
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

		// Token: 0x17000EC7 RID: 3783
		// (get) Token: 0x06003C95 RID: 15509 RVA: 0x0010744D File Offset: 0x0010564D
		// (set) Token: 0x06003C96 RID: 15510 RVA: 0x00107455 File Offset: 0x00105655
		[DefaultValue("")]
		[Localizable(true)]
		[SRDescription("TabItemToolTipTextDescr")]
		public string ToolTipText
		{
			get
			{
				return this.toolTipText;
			}
			set
			{
				if (value == null)
				{
					value = "";
				}
				if (value == this.toolTipText)
				{
					return;
				}
				this.toolTipText = value;
				this.UpdateParent();
			}
		}

		// Token: 0x17000EC8 RID: 3784
		// (get) Token: 0x06003C97 RID: 15511 RVA: 0x000FFFD1 File Offset: 0x000FE1D1
		// (set) Token: 0x06003C98 RID: 15512 RVA: 0x000FFFD9 File Offset: 0x000FE1D9
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new bool Visible
		{
			get
			{
				return base.Visible;
			}
			set
			{
				base.Visible = value;
			}
		}

		// Token: 0x140002E6 RID: 742
		// (add) Token: 0x06003C99 RID: 15513 RVA: 0x00100016 File Offset: 0x000FE216
		// (remove) Token: 0x06003C9A RID: 15514 RVA: 0x0010001F File Offset: 0x000FE21F
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler VisibleChanged
		{
			add
			{
				base.VisibleChanged += value;
			}
			remove
			{
				base.VisibleChanged -= value;
			}
		}

		// Token: 0x06003C9B RID: 15515 RVA: 0x0010747D File Offset: 0x0010567D
		internal override void AssignParent(Control value)
		{
			if (value != null && !(value is TabControl))
			{
				throw new ArgumentException(SR.GetString("TABCONTROLTabPageNotOnTabControl", new object[]
				{
					value.GetType().FullName
				}));
			}
			base.AssignParent(value);
		}

		// Token: 0x06003C9C RID: 15516 RVA: 0x001074B8 File Offset: 0x001056B8
		public static TabPage GetTabPageOfComponent(object comp)
		{
			if (!(comp is Control))
			{
				return null;
			}
			Control control = (Control)comp;
			while (control != null && !(control is TabPage))
			{
				control = control.ParentInternal;
			}
			return (TabPage)control;
		}

		// Token: 0x06003C9D RID: 15517 RVA: 0x001074F0 File Offset: 0x001056F0
		internal NativeMethods.TCITEM_T GetTCITEM()
		{
			NativeMethods.TCITEM_T tcitem_T = new NativeMethods.TCITEM_T();
			tcitem_T.mask = 0;
			tcitem_T.pszText = null;
			tcitem_T.cchTextMax = 0;
			tcitem_T.lParam = IntPtr.Zero;
			string text = this.Text;
			this.PrefixAmpersands(ref text);
			if (text != null)
			{
				tcitem_T.mask |= 1;
				tcitem_T.pszText = text;
				tcitem_T.cchTextMax = text.Length;
			}
			int imageIndex = this.ImageIndex;
			tcitem_T.mask |= 2;
			tcitem_T.iImage = this.ImageIndexer.ActualIndex;
			return tcitem_T;
		}

		// Token: 0x06003C9E RID: 15518 RVA: 0x00107580 File Offset: 0x00105780
		private void PrefixAmpersands(ref string value)
		{
			if (value == null || value.Length == 0)
			{
				return;
			}
			if (value.IndexOf('&') < 0)
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < value.Length; i++)
			{
				if (value[i] == '&')
				{
					if (i < value.Length - 1 && value[i + 1] == '&')
					{
						i++;
					}
					stringBuilder.Append("&&");
				}
				else
				{
					stringBuilder.Append(value[i]);
				}
			}
			value = stringBuilder.ToString();
		}

		// Token: 0x06003C9F RID: 15519 RVA: 0x0010760F File Offset: 0x0010580F
		internal void FireLeave(EventArgs e)
		{
			this.leaveFired = true;
			this.OnLeave(e);
		}

		// Token: 0x06003CA0 RID: 15520 RVA: 0x0010761F File Offset: 0x0010581F
		internal void FireEnter(EventArgs e)
		{
			this.enterFired = true;
			this.OnEnter(e);
		}

		// Token: 0x06003CA1 RID: 15521 RVA: 0x00107630 File Offset: 0x00105830
		protected override void OnEnter(EventArgs e)
		{
			TabControl tabControl = this.ParentInternal as TabControl;
			if (tabControl != null)
			{
				if (this.enterFired)
				{
					base.OnEnter(e);
				}
				this.enterFired = false;
			}
		}

		// Token: 0x06003CA2 RID: 15522 RVA: 0x00107664 File Offset: 0x00105864
		protected override void OnLeave(EventArgs e)
		{
			TabControl tabControl = this.ParentInternal as TabControl;
			if (tabControl != null)
			{
				if (this.leaveFired)
				{
					base.OnLeave(e);
				}
				this.leaveFired = false;
			}
		}

		// Token: 0x06003CA3 RID: 15523 RVA: 0x00107698 File Offset: 0x00105898
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			TabControl tabControl = this.ParentInternal as TabControl;
			if (Application.RenderWithVisualStyles && this.UseVisualStyleBackColor && tabControl != null && tabControl.Appearance == TabAppearance.Normal)
			{
				Color backColor = this.UseVisualStyleBackColor ? Color.Transparent : this.BackColor;
				Rectangle rectangle = LayoutUtils.InflateRect(this.DisplayRectangle, base.Padding);
				Rectangle bounds = new Rectangle(rectangle.X - 4, rectangle.Y - 2, rectangle.Width + 8, rectangle.Height + 6);
				TabRenderer.DrawTabPage(e.Graphics, bounds);
				if (this.BackgroundImage != null)
				{
					ControlPaint.DrawBackgroundImage(e.Graphics, this.BackgroundImage, backColor, this.BackgroundImageLayout, rectangle, rectangle, this.DisplayRectangle.Location);
					return;
				}
			}
			else
			{
				base.OnPaintBackground(e);
			}
		}

		// Token: 0x06003CA4 RID: 15524 RVA: 0x00107770 File Offset: 0x00105970
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			Control parentInternal = this.ParentInternal;
			if (parentInternal is TabControl && parentInternal.IsHandleCreated)
			{
				Rectangle displayRectangle = parentInternal.DisplayRectangle;
				base.SetBoundsCore(displayRectangle.X, displayRectangle.Y, displayRectangle.Width, displayRectangle.Height, (specified == BoundsSpecified.None) ? BoundsSpecified.None : BoundsSpecified.All);
				return;
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		// Token: 0x06003CA5 RID: 15525 RVA: 0x000B6809 File Offset: 0x000B4A09
		[EditorBrowsable(EditorBrowsableState.Never)]
		private bool ShouldSerializeLocation()
		{
			return base.Left != 0 || base.Top != 0;
		}

		// Token: 0x06003CA6 RID: 15526 RVA: 0x001077D4 File Offset: 0x001059D4
		public override string ToString()
		{
			return "TabPage: {" + this.Text + "}";
		}

		// Token: 0x06003CA7 RID: 15527 RVA: 0x001077EC File Offset: 0x001059EC
		internal void UpdateParent()
		{
			TabControl tabControl = this.ParentInternal as TabControl;
			if (tabControl != null)
			{
				tabControl.UpdateTab(this);
			}
		}

		// Token: 0x040023A4 RID: 9124
		private ImageList.Indexer imageIndexer;

		// Token: 0x040023A5 RID: 9125
		private string toolTipText = "";

		// Token: 0x040023A6 RID: 9126
		private bool enterFired;

		// Token: 0x040023A7 RID: 9127
		private bool leaveFired;

		// Token: 0x040023A8 RID: 9128
		private bool useVisualStyleBackColor;

		// Token: 0x020007F3 RID: 2035
		[ComVisible(false)]
		public class TabPageControlCollection : Control.ControlCollection
		{
			// Token: 0x06006E75 RID: 28277 RVA: 0x0019513B File Offset: 0x0019333B
			public TabPageControlCollection(TabPage owner) : base(owner)
			{
			}

			// Token: 0x06006E76 RID: 28278 RVA: 0x00195144 File Offset: 0x00193344
			public override void Add(Control value)
			{
				if (value is TabPage)
				{
					throw new ArgumentException(SR.GetString("TABCONTROLTabPageOnTabPage"));
				}
				base.Add(value);
			}
		}
	}
}

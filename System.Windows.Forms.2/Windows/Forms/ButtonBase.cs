using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.ButtonInternal;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x02000144 RID: 324
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Designer("System.Windows.Forms.Design.ButtonBaseDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public abstract class ButtonBase : Control
	{
		// Token: 0x06000C6A RID: 3178 RVA: 0x00023B54 File Offset: 0x00021D54
		protected ButtonBase()
		{
			base.SetStyle(ControlStyles.Opaque | ControlStyles.ResizeRedraw | ControlStyles.StandardClick | ControlStyles.SupportsTransparentBackColor | ControlStyles.CacheText | ControlStyles.OptimizedDoubleBuffer, true);
			base.SetState2(2048, true);
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.UserMouse, this.OwnerDraw);
			this.SetFlag(128, true);
			this.SetFlag(256, false);
		}

		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06000C6B RID: 3179 RVA: 0x00023BD1 File Offset: 0x00021DD1
		// (set) Token: 0x06000C6C RID: 3180 RVA: 0x00023BDB File Offset: 0x00021DDB
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[SRDescription("ButtonAutoEllipsisDescr")]
		public bool AutoEllipsis
		{
			get
			{
				return this.GetFlag(32);
			}
			set
			{
				if (this.AutoEllipsis != value)
				{
					this.SetFlag(32, value);
					if (value && this.textToolTip == null)
					{
						this.textToolTip = new ToolTip();
					}
					base.Invalidate();
				}
			}
		}

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06000C6D RID: 3181 RVA: 0x00011A45 File Offset: 0x0000FC45
		// (set) Token: 0x06000C6E RID: 3182 RVA: 0x00023C0B File Offset: 0x00021E0B
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
				if (value)
				{
					this.AutoEllipsis = false;
				}
			}
		}

		// Token: 0x14000064 RID: 100
		// (add) Token: 0x06000C6F RID: 3183 RVA: 0x00011A56 File Offset: 0x0000FC56
		// (remove) Token: 0x06000C70 RID: 3184 RVA: 0x00011A5F File Offset: 0x0000FC5F
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

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06000C71 RID: 3185 RVA: 0x0001A1E5 File Offset: 0x000183E5
		// (set) Token: 0x06000C72 RID: 3186 RVA: 0x00023C20 File Offset: 0x00021E20
		[SRCategory("CatAppearance")]
		[SRDescription("ControlBackColorDescr")]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
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

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06000C73 RID: 3187 RVA: 0x0001A256 File Offset: 0x00018456
		protected override Size DefaultSize
		{
			get
			{
				return new Size(75, 23);
			}
		}

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x00023C74 File Offset: 0x00021E74
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				if (!this.OwnerDraw)
				{
					createParams.ExStyle &= -4097;
					createParams.Style |= 8192;
					if (this.IsDefault)
					{
						createParams.Style |= 1;
					}
					ContentAlignment contentAlignment = base.RtlTranslateContent(this.TextAlign);
					if ((contentAlignment & WindowsFormsUtils.AnyLeftAlign) != (ContentAlignment)0)
					{
						createParams.Style |= 256;
					}
					else if ((contentAlignment & WindowsFormsUtils.AnyRightAlign) != (ContentAlignment)0)
					{
						createParams.Style |= 512;
					}
					else
					{
						createParams.Style |= 768;
					}
					if ((contentAlignment & WindowsFormsUtils.AnyTopAlign) != (ContentAlignment)0)
					{
						createParams.Style |= 1024;
					}
					else if ((contentAlignment & WindowsFormsUtils.AnyBottomAlign) != (ContentAlignment)0)
					{
						createParams.Style |= 2048;
					}
					else
					{
						createParams.Style |= 3072;
					}
				}
				return createParams;
			}
		}

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06000C75 RID: 3189 RVA: 0x00023D73 File Offset: 0x00021F73
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Disable;
			}
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x00023D76 File Offset: 0x00021F76
		// (set) Token: 0x06000C77 RID: 3191 RVA: 0x00023D80 File Offset: 0x00021F80
		protected internal bool IsDefault
		{
			get
			{
				return this.GetFlag(64);
			}
			set
			{
				if (this.GetFlag(64) != value)
				{
					this.SetFlag(64, value);
					if (base.IsHandleCreated)
					{
						if (this.OwnerDraw)
						{
							base.Invalidate();
							return;
						}
						base.UpdateStyles();
					}
				}
			}
		}

		// Token: 0x17000311 RID: 785
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x00023DB3 File Offset: 0x00021FB3
		// (set) Token: 0x06000C79 RID: 3193 RVA: 0x00023DBC File Offset: 0x00021FBC
		[SRCategory("CatAppearance")]
		[DefaultValue(FlatStyle.Standard)]
		[Localizable(true)]
		[SRDescription("ButtonFlatStyleDescr")]
		public FlatStyle FlatStyle
		{
			get
			{
				return this.flatStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(FlatStyle));
				}
				this.flatStyle = value;
				LayoutTransaction.DoLayoutIf(this.AutoSize, this.ParentInternal, this, PropertyNames.FlatStyle);
				base.Invalidate();
				this.UpdateOwnerDraw();
			}
		}

		// Token: 0x17000312 RID: 786
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x00023E19 File Offset: 0x00022019
		[Browsable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("ButtonFlatAppearance")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public FlatButtonAppearance FlatAppearance
		{
			get
			{
				if (this.flatAppearance == null)
				{
					this.flatAppearance = new FlatButtonAppearance(this);
				}
				return this.flatAppearance;
			}
		}

		// Token: 0x17000313 RID: 787
		// (get) Token: 0x06000C7B RID: 3195 RVA: 0x00023E38 File Offset: 0x00022038
		// (set) Token: 0x06000C7C RID: 3196 RVA: 0x00023EA4 File Offset: 0x000220A4
		[SRDescription("ButtonImageDescr")]
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		public Image Image
		{
			get
			{
				if (this.image == null && this.imageList != null)
				{
					int num = this.imageIndex.ActualIndex;
					if (num >= this.imageList.Images.Count)
					{
						num = this.imageList.Images.Count - 1;
					}
					if (num >= 0)
					{
						return this.imageList.Images[num];
					}
				}
				return this.image;
			}
			set
			{
				if (this.Image != value)
				{
					this.StopAnimate();
					this.image = value;
					if (this.image != null)
					{
						this.ImageIndex = -1;
						this.ImageList = null;
					}
					LayoutTransaction.DoLayoutIf(this.AutoSize, this.ParentInternal, this, PropertyNames.Image);
					this.Animate();
					base.Invalidate();
				}
			}
		}

		// Token: 0x17000314 RID: 788
		// (get) Token: 0x06000C7D RID: 3197 RVA: 0x00023F00 File Offset: 0x00022100
		// (set) Token: 0x06000C7E RID: 3198 RVA: 0x00023F08 File Offset: 0x00022108
		[DefaultValue(ContentAlignment.MiddleCenter)]
		[Localizable(true)]
		[SRDescription("ButtonImageAlignDescr")]
		[SRCategory("CatAppearance")]
		public ContentAlignment ImageAlign
		{
			get
			{
				return this.imageAlign;
			}
			set
			{
				if (!WindowsFormsUtils.EnumValidator.IsValidContentAlignment(value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ContentAlignment));
				}
				if (value != this.imageAlign)
				{
					this.imageAlign = value;
					LayoutTransaction.DoLayoutIf(this.AutoSize, this.ParentInternal, this, PropertyNames.ImageAlign);
					base.Invalidate();
				}
			}
		}

		// Token: 0x17000315 RID: 789
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x00023F60 File Offset: 0x00022160
		// (set) Token: 0x06000C80 RID: 3200 RVA: 0x00023FC0 File Offset: 0x000221C0
		[TypeConverter(typeof(ImageIndexConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[DefaultValue(-1)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("ButtonImageIndexDescr")]
		[SRCategory("CatAppearance")]
		public int ImageIndex
		{
			get
			{
				if (this.imageIndex.Index != -1 && this.imageList != null && this.imageIndex.Index >= this.imageList.Images.Count)
				{
					return this.imageList.Images.Count - 1;
				}
				return this.imageIndex.Index;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("ImageIndex", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"ImageIndex",
						value.ToString(CultureInfo.CurrentCulture),
						-1.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.imageIndex.Index != value)
				{
					if (value != -1)
					{
						this.image = null;
					}
					this.imageIndex.Index = value;
					base.Invalidate();
				}
			}
		}

		// Token: 0x17000316 RID: 790
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x0002403F File Offset: 0x0002223F
		// (set) Token: 0x06000C82 RID: 3202 RVA: 0x0002404C File Offset: 0x0002224C
		[TypeConverter(typeof(ImageKeyConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[Localizable(true)]
		[DefaultValue("")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("ButtonImageIndexDescr")]
		[SRCategory("CatAppearance")]
		public string ImageKey
		{
			get
			{
				return this.imageIndex.Key;
			}
			set
			{
				if (this.imageIndex.Key != value)
				{
					if (value != null)
					{
						this.image = null;
					}
					this.imageIndex.Key = value;
					base.Invalidate();
				}
			}
		}

		// Token: 0x17000317 RID: 791
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x0002407D File Offset: 0x0002227D
		// (set) Token: 0x06000C84 RID: 3204 RVA: 0x00024088 File Offset: 0x00022288
		[DefaultValue(null)]
		[SRDescription("ButtonImageListDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRCategory("CatAppearance")]
		public ImageList ImageList
		{
			get
			{
				return this.imageList;
			}
			set
			{
				if (this.imageList != value)
				{
					EventHandler value2 = new EventHandler(this.ImageListRecreateHandle);
					EventHandler value3 = new EventHandler(this.DetachImageList);
					if (this.imageList != null)
					{
						this.imageList.RecreateHandle -= value2;
						this.imageList.Disposed -= value3;
					}
					if (value != null)
					{
						this.image = null;
					}
					this.imageList = value;
					this.imageIndex.ImageList = value;
					if (value != null)
					{
						value.RecreateHandle += value2;
						value.Disposed += value3;
					}
					base.Invalidate();
				}
			}
		}

		// Token: 0x17000318 RID: 792
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x0001A1ED File Offset: 0x000183ED
		// (set) Token: 0x06000C86 RID: 3206 RVA: 0x0001A1F5 File Offset: 0x000183F5
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new ImeMode ImeMode
		{
			get
			{
				return base.ImeMode;
			}
			set
			{
				base.ImeMode = value;
			}
		}

		// Token: 0x14000065 RID: 101
		// (add) Token: 0x06000C87 RID: 3207 RVA: 0x0002410C File Offset: 0x0002230C
		// (remove) Token: 0x06000C88 RID: 3208 RVA: 0x00024115 File Offset: 0x00022315
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ImeModeChanged
		{
			add
			{
				base.ImeModeChanged += value;
			}
			remove
			{
				base.ImeModeChanged -= value;
			}
		}

		// Token: 0x17000319 RID: 793
		// (get) Token: 0x06000C89 RID: 3209 RVA: 0x00013062 File Offset: 0x00011262
		internal override bool IsMnemonicsListenerAxSourced
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700031A RID: 794
		// (get) Token: 0x06000C8A RID: 3210 RVA: 0x0002411E File Offset: 0x0002231E
		internal virtual Rectangle OverChangeRectangle
		{
			get
			{
				if (this.FlatStyle == FlatStyle.Standard)
				{
					return new Rectangle(-1, -1, 1, 1);
				}
				return base.ClientRectangle;
			}
		}

		// Token: 0x1700031B RID: 795
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x00024139 File Offset: 0x00022339
		internal bool OwnerDraw
		{
			get
			{
				return this.FlatStyle != FlatStyle.System;
			}
		}

		// Token: 0x1700031C RID: 796
		// (get) Token: 0x06000C8C RID: 3212 RVA: 0x00024147 File Offset: 0x00022347
		internal virtual Rectangle DownChangeRectangle
		{
			get
			{
				return base.ClientRectangle;
			}
		}

		// Token: 0x1700031D RID: 797
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0002414F File Offset: 0x0002234F
		internal bool MouseIsPressed
		{
			get
			{
				return this.GetFlag(4);
			}
		}

		// Token: 0x1700031E RID: 798
		// (get) Token: 0x06000C8E RID: 3214 RVA: 0x00024158 File Offset: 0x00022358
		internal bool MouseIsDown
		{
			get
			{
				return this.GetFlag(2);
			}
		}

		// Token: 0x1700031F RID: 799
		// (get) Token: 0x06000C8F RID: 3215 RVA: 0x00024161 File Offset: 0x00022361
		internal bool MouseIsOver
		{
			get
			{
				return this.GetFlag(1);
			}
		}

		// Token: 0x17000320 RID: 800
		// (get) Token: 0x06000C90 RID: 3216 RVA: 0x0002416A File Offset: 0x0002236A
		// (set) Token: 0x06000C91 RID: 3217 RVA: 0x00024177 File Offset: 0x00022377
		internal bool ShowToolTip
		{
			get
			{
				return this.GetFlag(256);
			}
			set
			{
				this.SetFlag(256, value);
			}
		}

		// Token: 0x17000321 RID: 801
		// (get) Token: 0x06000C92 RID: 3218 RVA: 0x00013A28 File Offset: 0x00011C28
		// (set) Token: 0x06000C93 RID: 3219 RVA: 0x00024185 File Offset: 0x00022385
		[Editor("System.ComponentModel.Design.MultilineStringEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SettingsBindable(true)]
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

		// Token: 0x17000322 RID: 802
		// (get) Token: 0x06000C94 RID: 3220 RVA: 0x0002418E File Offset: 0x0002238E
		// (set) Token: 0x06000C95 RID: 3221 RVA: 0x00024198 File Offset: 0x00022398
		[DefaultValue(ContentAlignment.MiddleCenter)]
		[Localizable(true)]
		[SRDescription("ButtonTextAlignDescr")]
		[SRCategory("CatAppearance")]
		public virtual ContentAlignment TextAlign
		{
			get
			{
				return this.textAlign;
			}
			set
			{
				if (!WindowsFormsUtils.EnumValidator.IsValidContentAlignment(value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ContentAlignment));
				}
				if (value != this.textAlign)
				{
					this.textAlign = value;
					LayoutTransaction.DoLayoutIf(this.AutoSize, this.ParentInternal, this, PropertyNames.TextAlign);
					if (this.OwnerDraw)
					{
						base.Invalidate();
						return;
					}
					base.UpdateStyles();
				}
			}
		}

		// Token: 0x17000323 RID: 803
		// (get) Token: 0x06000C96 RID: 3222 RVA: 0x000241FF File Offset: 0x000223FF
		// (set) Token: 0x06000C97 RID: 3223 RVA: 0x00024208 File Offset: 0x00022408
		[DefaultValue(TextImageRelation.Overlay)]
		[Localizable(true)]
		[SRDescription("ButtonTextImageRelationDescr")]
		[SRCategory("CatAppearance")]
		public TextImageRelation TextImageRelation
		{
			get
			{
				return this.textImageRelation;
			}
			set
			{
				if (!WindowsFormsUtils.EnumValidator.IsValidTextImageRelation(value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(TextImageRelation));
				}
				if (value != this.TextImageRelation)
				{
					this.textImageRelation = value;
					LayoutTransaction.DoLayoutIf(this.AutoSize, this.ParentInternal, this, PropertyNames.TextImageRelation);
					base.Invalidate();
				}
			}
		}

		// Token: 0x17000324 RID: 804
		// (get) Token: 0x06000C98 RID: 3224 RVA: 0x00024260 File Offset: 0x00022460
		// (set) Token: 0x06000C99 RID: 3225 RVA: 0x0002426D File Offset: 0x0002246D
		[SRDescription("ButtonUseMnemonicDescr")]
		[DefaultValue(true)]
		[SRCategory("CatAppearance")]
		public bool UseMnemonic
		{
			get
			{
				return this.GetFlag(128);
			}
			set
			{
				this.SetFlag(128, value);
				LayoutTransaction.DoLayoutIf(this.AutoSize, this.ParentInternal, this, PropertyNames.Text);
				base.Invalidate();
			}
		}

		// Token: 0x06000C9A RID: 3226 RVA: 0x00024298 File Offset: 0x00022498
		private void Animate()
		{
			this.Animate(!base.DesignMode && base.Visible && base.Enabled && this.ParentInternal != null);
		}

		// Token: 0x06000C9B RID: 3227 RVA: 0x000242C4 File Offset: 0x000224C4
		private void StopAnimate()
		{
			this.Animate(false);
		}

		// Token: 0x06000C9C RID: 3228 RVA: 0x000242D0 File Offset: 0x000224D0
		private void Animate(bool animate)
		{
			if (animate != this.GetFlag(16))
			{
				if (animate)
				{
					if (this.image != null)
					{
						ImageAnimator.Animate(this.image, new EventHandler(this.OnFrameChanged));
						this.SetFlag(16, animate);
						return;
					}
				}
				else if (this.image != null)
				{
					ImageAnimator.StopAnimate(this.image, new EventHandler(this.OnFrameChanged));
					this.SetFlag(16, animate);
				}
			}
		}

		// Token: 0x06000C9D RID: 3229 RVA: 0x0002433C File Offset: 0x0002253C
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new ButtonBase.ButtonBaseAccessibleObject(this);
		}

		// Token: 0x06000C9E RID: 3230 RVA: 0x00024344 File Offset: 0x00022544
		private void DetachImageList(object sender, EventArgs e)
		{
			this.ImageList = null;
		}

		// Token: 0x06000C9F RID: 3231 RVA: 0x00024350 File Offset: 0x00022550
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.StopAnimate();
				if (this.imageList != null)
				{
					this.imageList.Disposed -= this.DetachImageList;
				}
				if (this.textToolTip != null)
				{
					this.textToolTip.Dispose();
					this.textToolTip = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06000CA0 RID: 3232 RVA: 0x000243A6 File Offset: 0x000225A6
		private bool GetFlag(int flag)
		{
			return (this.state & flag) == flag;
		}

		// Token: 0x06000CA1 RID: 3233 RVA: 0x000243B3 File Offset: 0x000225B3
		private void ImageListRecreateHandle(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				base.Invalidate();
			}
		}

		// Token: 0x06000CA2 RID: 3234 RVA: 0x000243C3 File Offset: 0x000225C3
		protected override void OnGotFocus(EventArgs e)
		{
			base.OnGotFocus(e);
			base.Invalidate();
		}

		// Token: 0x06000CA3 RID: 3235 RVA: 0x000243D2 File Offset: 0x000225D2
		protected override void OnLostFocus(EventArgs e)
		{
			base.OnLostFocus(e);
			this.SetFlag(2, false);
			base.CaptureInternal = false;
			base.Invalidate();
		}

		// Token: 0x06000CA4 RID: 3236 RVA: 0x000243F0 File Offset: 0x000225F0
		protected override void OnMouseEnter(EventArgs eventargs)
		{
			this.SetFlag(1, true);
			base.Invalidate();
			if (!base.DesignMode && this.AutoEllipsis && this.ShowToolTip && this.textToolTip != null)
			{
				IntSecurity.AllWindows.Assert();
				try
				{
					this.textToolTip.Show(WindowsFormsUtils.TextWithoutMnemonics(this.Text), this);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			base.OnMouseEnter(eventargs);
		}

		// Token: 0x06000CA5 RID: 3237 RVA: 0x0002446C File Offset: 0x0002266C
		protected override void OnMouseLeave(EventArgs eventargs)
		{
			this.SetFlag(1, false);
			if (this.textToolTip != null)
			{
				IntSecurity.AllWindows.Assert();
				try
				{
					this.textToolTip.Hide(this);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
			base.Invalidate();
			base.OnMouseLeave(eventargs);
		}

		// Token: 0x06000CA6 RID: 3238 RVA: 0x000244C4 File Offset: 0x000226C4
		protected override void OnMouseMove(MouseEventArgs mevent)
		{
			if (mevent.Button != MouseButtons.None && this.GetFlag(4))
			{
				if (!base.ClientRectangle.Contains(mevent.X, mevent.Y))
				{
					if (this.GetFlag(2))
					{
						this.SetFlag(2, false);
						base.Invalidate(this.DownChangeRectangle);
					}
				}
				else if (!this.GetFlag(2))
				{
					this.SetFlag(2, true);
					base.Invalidate(this.DownChangeRectangle);
				}
			}
			base.OnMouseMove(mevent);
		}

		// Token: 0x06000CA7 RID: 3239 RVA: 0x00024541 File Offset: 0x00022741
		protected override void OnMouseDown(MouseEventArgs mevent)
		{
			if (mevent.Button == MouseButtons.Left)
			{
				this.SetFlag(2, true);
				this.SetFlag(4, true);
				base.Invalidate(this.DownChangeRectangle);
			}
			base.OnMouseDown(mevent);
		}

		// Token: 0x06000CA8 RID: 3240 RVA: 0x00024573 File Offset: 0x00022773
		protected override void OnMouseUp(MouseEventArgs mevent)
		{
			base.OnMouseUp(mevent);
		}

		// Token: 0x06000CA9 RID: 3241 RVA: 0x0002457C File Offset: 0x0002277C
		protected void ResetFlagsandPaint()
		{
			this.SetFlag(4, false);
			this.SetFlag(2, false);
			base.Invalidate(this.DownChangeRectangle);
			base.Update();
		}

		// Token: 0x06000CAA RID: 3242 RVA: 0x000245A0 File Offset: 0x000227A0
		private void PaintControl(PaintEventArgs pevent)
		{
			this.Adapter.Paint(pevent);
		}

		// Token: 0x06000CAB RID: 3243 RVA: 0x000245AE File Offset: 0x000227AE
		public override Size GetPreferredSize(Size proposedSize)
		{
			if (proposedSize.Width == 1)
			{
				proposedSize.Width = 0;
			}
			if (proposedSize.Height == 1)
			{
				proposedSize.Height = 0;
			}
			return base.GetPreferredSize(proposedSize);
		}

		// Token: 0x06000CAC RID: 3244 RVA: 0x000245DC File Offset: 0x000227DC
		internal override Size GetPreferredSizeCore(Size proposedConstraints)
		{
			Size preferredSizeCore = this.Adapter.GetPreferredSizeCore(proposedConstraints);
			return LayoutUtils.UnionSizes(preferredSizeCore + base.Padding.Size, this.MinimumSize);
		}

		// Token: 0x17000325 RID: 805
		// (get) Token: 0x06000CAD RID: 3245 RVA: 0x00024618 File Offset: 0x00022818
		internal ButtonBaseAdapter Adapter
		{
			get
			{
				if (this._adapter == null || this.FlatStyle != this._cachedAdapterType)
				{
					switch (this.FlatStyle)
					{
					case FlatStyle.Flat:
						this._adapter = this.CreateFlatAdapter();
						break;
					case FlatStyle.Popup:
						this._adapter = this.CreatePopupAdapter();
						break;
					case FlatStyle.Standard:
						this._adapter = this.CreateStandardAdapter();
						break;
					}
					this._cachedAdapterType = this.FlatStyle;
				}
				return this._adapter;
			}
		}

		// Token: 0x06000CAE RID: 3246 RVA: 0x00015ECC File Offset: 0x000140CC
		internal virtual ButtonBaseAdapter CreateFlatAdapter()
		{
			return null;
		}

		// Token: 0x06000CAF RID: 3247 RVA: 0x00015ECC File Offset: 0x000140CC
		internal virtual ButtonBaseAdapter CreatePopupAdapter()
		{
			return null;
		}

		// Token: 0x06000CB0 RID: 3248 RVA: 0x00015ECC File Offset: 0x000140CC
		internal virtual ButtonBaseAdapter CreateStandardAdapter()
		{
			return null;
		}

		// Token: 0x06000CB1 RID: 3249 RVA: 0x00024690 File Offset: 0x00022890
		internal virtual StringFormat CreateStringFormat()
		{
			if (this.Adapter == null)
			{
				return new StringFormat();
			}
			return this.Adapter.CreateStringFormat();
		}

		// Token: 0x06000CB2 RID: 3250 RVA: 0x000246AB File Offset: 0x000228AB
		internal virtual TextFormatFlags CreateTextFormatFlags()
		{
			if (this.Adapter == null)
			{
				return TextFormatFlags.Default;
			}
			return this.Adapter.CreateTextFormatFlags();
		}

		// Token: 0x06000CB3 RID: 3251 RVA: 0x000246C4 File Offset: 0x000228C4
		private void OnFrameChanged(object o, EventArgs e)
		{
			if (base.Disposing || base.IsDisposed)
			{
				return;
			}
			if (base.IsHandleCreated && base.InvokeRequired)
			{
				base.BeginInvoke(new EventHandler(this.OnFrameChanged), new object[]
				{
					o,
					e
				});
				return;
			}
			base.Invalidate();
		}

		// Token: 0x06000CB4 RID: 3252 RVA: 0x0002471A File Offset: 0x0002291A
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			this.Animate();
			if (!base.Enabled)
			{
				this.SetFlag(2, false);
				this.SetFlag(1, false);
				base.Invalidate();
			}
		}

		// Token: 0x06000CB5 RID: 3253 RVA: 0x00024748 File Offset: 0x00022948
		protected override void OnTextChanged(EventArgs e)
		{
			using (LayoutTransaction.CreateTransactionIf(this.AutoSize, this.ParentInternal, this, PropertyNames.Text))
			{
				base.OnTextChanged(e);
				base.Invalidate();
			}
		}

		// Token: 0x06000CB6 RID: 3254 RVA: 0x00024798 File Offset: 0x00022998
		protected override void OnKeyDown(KeyEventArgs kevent)
		{
			if (kevent.KeyData == Keys.Space)
			{
				if (!this.GetFlag(2))
				{
					this.SetFlag(2, true);
					if (!this.OwnerDraw)
					{
						base.SendMessage(243, 1, 0);
					}
					base.Invalidate(this.DownChangeRectangle);
				}
				kevent.Handled = true;
			}
			base.OnKeyDown(kevent);
		}

		// Token: 0x06000CB7 RID: 3255 RVA: 0x000247F0 File Offset: 0x000229F0
		protected override void OnKeyUp(KeyEventArgs kevent)
		{
			if (this.GetFlag(2) && !base.ValidationCancelled)
			{
				if (this.OwnerDraw)
				{
					this.ResetFlagsandPaint();
				}
				else
				{
					this.SetFlag(4, false);
					this.SetFlag(2, false);
					base.SendMessage(243, 0, 0);
				}
				if (kevent.KeyCode == Keys.Return || kevent.KeyCode == Keys.Space)
				{
					this.OnClick(EventArgs.Empty);
				}
				kevent.Handled = true;
			}
			base.OnKeyUp(kevent);
		}

		// Token: 0x06000CB8 RID: 3256 RVA: 0x0002486C File Offset: 0x00022A6C
		protected override void OnPaint(PaintEventArgs pevent)
		{
			if (this.AutoEllipsis)
			{
				Size preferredSize = base.PreferredSize;
				this.ShowToolTip = (base.ClientRectangle.Width < preferredSize.Width || base.ClientRectangle.Height < preferredSize.Height);
			}
			else
			{
				this.ShowToolTip = false;
			}
			if (base.GetStyle(ControlStyles.UserPaint))
			{
				this.Animate();
				ImageAnimator.UpdateFrames(this.Image);
				this.PaintControl(pevent);
			}
			base.OnPaint(pevent);
		}

		// Token: 0x06000CB9 RID: 3257 RVA: 0x000248F0 File Offset: 0x00022AF0
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
			this.Animate();
		}

		// Token: 0x06000CBA RID: 3258 RVA: 0x000248FF File Offset: 0x00022AFF
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			this.Animate();
		}

		// Token: 0x06000CBB RID: 3259 RVA: 0x0002490E File Offset: 0x00022B0E
		private void ResetImage()
		{
			this.Image = null;
		}

		// Token: 0x06000CBC RID: 3260 RVA: 0x00024918 File Offset: 0x00022B18
		private void SetFlag(int flag, bool value)
		{
			bool flag2 = (this.state & flag) != 0;
			if (value)
			{
				this.state |= flag;
			}
			else
			{
				this.state &= ~flag;
			}
			if (this.OwnerDraw && (flag & 2) != 0 && value != flag2)
			{
				base.AccessibilityNotifyClients(AccessibleEvents.StateChange, -1);
			}
		}

		// Token: 0x06000CBD RID: 3261 RVA: 0x00024970 File Offset: 0x00022B70
		private bool ShouldSerializeImage()
		{
			return this.image != null;
		}

		// Token: 0x06000CBE RID: 3262 RVA: 0x0002497B File Offset: 0x00022B7B
		private void UpdateOwnerDraw()
		{
			if (this.OwnerDraw != base.GetStyle(ControlStyles.UserPaint))
			{
				base.SetStyle(ControlStyles.UserPaint | ControlStyles.UserMouse, this.OwnerDraw);
				base.RecreateHandle();
			}
		}

		// Token: 0x17000326 RID: 806
		// (get) Token: 0x06000CBF RID: 3263 RVA: 0x000249A3 File Offset: 0x00022BA3
		// (set) Token: 0x06000CC0 RID: 3264 RVA: 0x000249AB File Offset: 0x00022BAB
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("UseCompatibleTextRenderingDescr")]
		public bool UseCompatibleTextRendering
		{
			get
			{
				return base.UseCompatibleTextRenderingInt;
			}
			set
			{
				base.UseCompatibleTextRenderingInt = value;
			}
		}

		// Token: 0x17000327 RID: 807
		// (get) Token: 0x06000CC1 RID: 3265 RVA: 0x00013062 File Offset: 0x00011262
		internal override bool SupportsUseCompatibleTextRendering
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000328 RID: 808
		// (get) Token: 0x06000CC2 RID: 3266 RVA: 0x000249B4 File Offset: 0x00022BB4
		// (set) Token: 0x06000CC3 RID: 3267 RVA: 0x000249F3 File Offset: 0x00022BF3
		[SRCategory("CatAppearance")]
		[SRDescription("ButtonUseVisualStyleBackColorDescr")]
		public bool UseVisualStyleBackColor
		{
			get
			{
				return (this.isEnableVisualStyleBackgroundSet || (base.RawBackColor.IsEmpty && this.BackColor == SystemColors.Control)) && this.enableVisualStyleBackground;
			}
			set
			{
				this.isEnableVisualStyleBackgroundSet = true;
				this.enableVisualStyleBackground = value;
				base.Invalidate();
			}
		}

		// Token: 0x06000CC4 RID: 3268 RVA: 0x00024A09 File Offset: 0x00022C09
		private void ResetUseVisualStyleBackColor()
		{
			this.isEnableVisualStyleBackgroundSet = false;
			this.enableVisualStyleBackground = true;
			base.Invalidate();
		}

		// Token: 0x06000CC5 RID: 3269 RVA: 0x00024A1F File Offset: 0x00022C1F
		private bool ShouldSerializeUseVisualStyleBackColor()
		{
			return this.isEnableVisualStyleBackgroundSet;
		}

		// Token: 0x06000CC6 RID: 3270 RVA: 0x00024A28 File Offset: 0x00022C28
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg != 245)
			{
				if (this.OwnerDraw)
				{
					int msg2 = m.Msg;
					if (msg2 > 243)
					{
						if (msg2 <= 517)
						{
							if (msg2 != 514 && msg2 != 517)
							{
								goto IL_E6;
							}
						}
						else if (msg2 != 520)
						{
							if (msg2 == 533)
							{
								goto IL_8C;
							}
							goto IL_E6;
						}
						try
						{
							this.SetFlag(8, true);
							base.WndProc(ref m);
							return;
						}
						finally
						{
							this.SetFlag(8, false);
						}
						goto IL_E6;
					}
					if (msg2 != 8 && msg2 != 31)
					{
						if (msg2 != 243)
						{
							goto IL_E6;
						}
						return;
					}
					IL_8C:
					if (!this.GetFlag(8) && this.GetFlag(4))
					{
						this.SetFlag(4, false);
						if (this.GetFlag(2))
						{
							this.SetFlag(2, false);
							base.Invalidate(this.DownChangeRectangle);
						}
					}
					base.WndProc(ref m);
					return;
					IL_E6:
					base.WndProc(ref m);
					return;
				}
				int msg3 = m.Msg;
				if (msg3 == 8465)
				{
					if (NativeMethods.Util.HIWORD(m.WParam) == 0 && !base.ValidationCancelled)
					{
						this.OnClick(EventArgs.Empty);
						return;
					}
				}
				else
				{
					base.WndProc(ref m);
				}
				return;
			}
			if (this is IButtonControl)
			{
				((IButtonControl)this).PerformClick();
				return;
			}
			this.OnClick(EventArgs.Empty);
		}

		// Token: 0x04000732 RID: 1842
		private FlatStyle flatStyle = FlatStyle.Standard;

		// Token: 0x04000733 RID: 1843
		private ContentAlignment imageAlign = ContentAlignment.MiddleCenter;

		// Token: 0x04000734 RID: 1844
		private ContentAlignment textAlign = ContentAlignment.MiddleCenter;

		// Token: 0x04000735 RID: 1845
		private TextImageRelation textImageRelation;

		// Token: 0x04000736 RID: 1846
		private ImageList.Indexer imageIndex = new ImageList.Indexer();

		// Token: 0x04000737 RID: 1847
		private FlatButtonAppearance flatAppearance;

		// Token: 0x04000738 RID: 1848
		private ImageList imageList;

		// Token: 0x04000739 RID: 1849
		private Image image;

		// Token: 0x0400073A RID: 1850
		private const int FlagMouseOver = 1;

		// Token: 0x0400073B RID: 1851
		private const int FlagMouseDown = 2;

		// Token: 0x0400073C RID: 1852
		private const int FlagMousePressed = 4;

		// Token: 0x0400073D RID: 1853
		private const int FlagInButtonUp = 8;

		// Token: 0x0400073E RID: 1854
		private const int FlagCurrentlyAnimating = 16;

		// Token: 0x0400073F RID: 1855
		private const int FlagAutoEllipsis = 32;

		// Token: 0x04000740 RID: 1856
		private const int FlagIsDefault = 64;

		// Token: 0x04000741 RID: 1857
		private const int FlagUseMnemonic = 128;

		// Token: 0x04000742 RID: 1858
		private const int FlagShowToolTip = 256;

		// Token: 0x04000743 RID: 1859
		private int state;

		// Token: 0x04000744 RID: 1860
		private ToolTip textToolTip;

		// Token: 0x04000745 RID: 1861
		private bool enableVisualStyleBackground = true;

		// Token: 0x04000746 RID: 1862
		private bool isEnableVisualStyleBackgroundSet;

		// Token: 0x04000747 RID: 1863
		private ButtonBaseAdapter _adapter;

		// Token: 0x04000748 RID: 1864
		private FlatStyle _cachedAdapterType;

		// Token: 0x0200061C RID: 1564
		[ComVisible(true)]
		public class ButtonBaseAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x06006305 RID: 25349 RVA: 0x0009B963 File Offset: 0x00099B63
			public ButtonBaseAccessibleObject(Control owner) : base(owner)
			{
			}

			// Token: 0x06006306 RID: 25350 RVA: 0x0016E5E1 File Offset: 0x0016C7E1
			[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			public override void DoDefaultAction()
			{
				if (base.IsOwnerControlDestroyed())
				{
					return;
				}
				((ButtonBase)base.Owner).OnClick(EventArgs.Empty);
			}

			// Token: 0x17001518 RID: 5400
			// (get) Token: 0x06006307 RID: 25351 RVA: 0x0016E604 File Offset: 0x0016C804
			public override AccessibleStates State
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return AccessibleStates.None;
					}
					AccessibleStates accessibleStates = base.State;
					ButtonBase buttonBase = (ButtonBase)base.Owner;
					if (buttonBase.OwnerDraw && buttonBase.MouseIsDown)
					{
						accessibleStates |= AccessibleStates.Pressed;
					}
					return accessibleStates;
				}
			}
		}
	}
}

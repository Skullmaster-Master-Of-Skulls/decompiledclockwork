using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.Automation;
using System.Windows.Forms.Internal;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x020002BA RID: 698
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Text")]
	[DefaultBindingProperty("Text")]
	[Designer("System.Windows.Forms.Design.LabelDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[ToolboxItem("System.Windows.Forms.Design.AutoSizeToolboxItem,System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionLabel")]
	public class Label : Control, IAutomationLiveRegion
	{
		// Token: 0x06002AA4 RID: 10916 RVA: 0x000C0AC0 File Offset: 0x000BECC0
		public Label()
		{
			base.SetState2(2048, true);
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, this.IsOwnerDraw());
			base.SetStyle(ControlStyles.FixedHeight | ControlStyles.Selectable, false);
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			CommonProperties.SetSelfAutoSizeInDefaultLayout(this, true);
			this.labelState[Label.StateFlatStyle] = 2;
			this.labelState[Label.StateUseMnemonic] = 1;
			this.labelState[Label.StateBorderStyle] = 0;
			this.TabStop = false;
			this.requestedHeight = base.Height;
			this.requestedWidth = base.Width;
		}

		// Token: 0x170009F8 RID: 2552
		// (get) Token: 0x06002AA5 RID: 10917 RVA: 0x00011A45 File Offset: 0x0000FC45
		// (set) Token: 0x06002AA6 RID: 10918 RVA: 0x000C0B5E File Offset: 0x000BED5E
		[SRCategory("CatLayout")]
		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.All)]
		[Localizable(true)]
		[SRDescription("LabelAutoSizeDescr")]
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
				if (this.AutoSize != value)
				{
					base.AutoSize = value;
					this.AdjustSize();
				}
			}
		}

		// Token: 0x140001EB RID: 491
		// (add) Token: 0x06002AA7 RID: 10919 RVA: 0x00011A56 File Offset: 0x0000FC56
		// (remove) Token: 0x06002AA8 RID: 10920 RVA: 0x00011A5F File Offset: 0x0000FC5F
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

		// Token: 0x170009F9 RID: 2553
		// (get) Token: 0x06002AA9 RID: 10921 RVA: 0x000C0B76 File Offset: 0x000BED76
		// (set) Token: 0x06002AAA RID: 10922 RVA: 0x000C0B8C File Offset: 0x000BED8C
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[SRDescription("LabelAutoEllipsisDescr")]
		public bool AutoEllipsis
		{
			get
			{
				return this.labelState[Label.StateAutoEllipsis] != 0;
			}
			set
			{
				if (this.AutoEllipsis != value)
				{
					this.labelState[Label.StateAutoEllipsis] = (value ? 1 : 0);
					this.MeasureTextCache.InvalidateCache();
					this.OnAutoEllipsisChanged();
					if (value && this.textToolTip == null)
					{
						this.textToolTip = new ToolTip();
					}
					if (this.ParentInternal != null)
					{
						LayoutTransaction.DoLayoutIf(this.AutoSize, this.ParentInternal, this, PropertyNames.AutoEllipsis);
					}
					base.Invalidate();
				}
			}
		}

		// Token: 0x170009FA RID: 2554
		// (get) Token: 0x06002AAB RID: 10923 RVA: 0x00011A90 File Offset: 0x0000FC90
		// (set) Token: 0x06002AAC RID: 10924 RVA: 0x00011A98 File Offset: 0x0000FC98
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("LabelBackgroundImageDescr")]
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

		// Token: 0x140001EC RID: 492
		// (add) Token: 0x06002AAD RID: 10925 RVA: 0x00011AA1 File Offset: 0x0000FCA1
		// (remove) Token: 0x06002AAE RID: 10926 RVA: 0x00011AAA File Offset: 0x0000FCAA
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageChanged
		{
			add
			{
				base.BackgroundImageChanged += value;
			}
			remove
			{
				base.BackgroundImageChanged -= value;
			}
		}

		// Token: 0x170009FB RID: 2555
		// (get) Token: 0x06002AAF RID: 10927 RVA: 0x00011AB3 File Offset: 0x0000FCB3
		// (set) Token: 0x06002AB0 RID: 10928 RVA: 0x00011ABB File Offset: 0x0000FCBB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x140001ED RID: 493
		// (add) Token: 0x06002AB1 RID: 10929 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		// (remove) Token: 0x06002AB2 RID: 10930 RVA: 0x00011ACD File Offset: 0x0000FCCD
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackgroundImageLayoutChanged
		{
			add
			{
				base.BackgroundImageLayoutChanged += value;
			}
			remove
			{
				base.BackgroundImageLayoutChanged -= value;
			}
		}

		// Token: 0x170009FC RID: 2556
		// (get) Token: 0x06002AB3 RID: 10931 RVA: 0x000C0C05 File Offset: 0x000BEE05
		// (set) Token: 0x06002AB4 RID: 10932 RVA: 0x000C0C18 File Offset: 0x000BEE18
		[SRCategory("CatAppearance")]
		[DefaultValue(BorderStyle.None)]
		[DispId(-504)]
		[SRDescription("LabelBorderDescr")]
		public virtual BorderStyle BorderStyle
		{
			get
			{
				return (BorderStyle)this.labelState[Label.StateBorderStyle];
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(BorderStyle));
				}
				if (this.BorderStyle != value)
				{
					this.labelState[Label.StateBorderStyle] = (int)value;
					if (this.ParentInternal != null)
					{
						LayoutTransaction.DoLayoutIf(this.AutoSize, this.ParentInternal, this, PropertyNames.BorderStyle);
					}
					if (this.AutoSize)
					{
						this.AdjustSize();
					}
					base.RecreateHandle();
				}
			}
		}

		// Token: 0x170009FD RID: 2557
		// (get) Token: 0x06002AB5 RID: 10933 RVA: 0x00013062 File Offset: 0x00011262
		internal virtual bool CanUseTextRenderer
		{
			get
			{
				return true;
			}
		}

		// Token: 0x170009FE RID: 2558
		// (get) Token: 0x06002AB6 RID: 10934 RVA: 0x000C0C98 File Offset: 0x000BEE98
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "STATIC";
				if (this.OwnerDraw)
				{
					createParams.Style |= 13;
					createParams.ExStyle &= -4097;
				}
				if (!this.OwnerDraw)
				{
					ContentAlignment textAlign = this.TextAlign;
					if (textAlign <= ContentAlignment.MiddleCenter)
					{
						switch (textAlign)
						{
						case ContentAlignment.TopLeft:
							break;
						case ContentAlignment.TopCenter:
							goto IL_BF;
						case (ContentAlignment)3:
							goto IL_DD;
						case ContentAlignment.TopRight:
							goto IL_AF;
						default:
							if (textAlign != ContentAlignment.MiddleLeft)
							{
								if (textAlign != ContentAlignment.MiddleCenter)
								{
									goto IL_DD;
								}
								goto IL_BF;
							}
							break;
						}
					}
					else if (textAlign <= ContentAlignment.BottomLeft)
					{
						if (textAlign == ContentAlignment.MiddleRight)
						{
							goto IL_AF;
						}
						if (textAlign != ContentAlignment.BottomLeft)
						{
							goto IL_DD;
						}
					}
					else
					{
						if (textAlign == ContentAlignment.BottomCenter)
						{
							goto IL_BF;
						}
						if (textAlign != ContentAlignment.BottomRight)
						{
							goto IL_DD;
						}
						goto IL_AF;
					}
					createParams.Style |= 0;
					goto IL_DD;
					IL_AF:
					createParams.Style |= 2;
					goto IL_DD;
					IL_BF:
					createParams.Style |= 1;
				}
				else
				{
					createParams.Style |= 0;
				}
				IL_DD:
				BorderStyle borderStyle = this.BorderStyle;
				if (borderStyle != BorderStyle.FixedSingle)
				{
					if (borderStyle == BorderStyle.Fixed3D)
					{
						createParams.Style |= 4096;
					}
				}
				else
				{
					createParams.Style |= 8388608;
				}
				if (!this.UseMnemonic)
				{
					createParams.Style |= 128;
				}
				return createParams;
			}
		}

		// Token: 0x170009FF RID: 2559
		// (get) Token: 0x06002AB7 RID: 10935 RVA: 0x00023D73 File Offset: 0x00021F73
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Disable;
			}
		}

		// Token: 0x17000A00 RID: 2560
		// (get) Token: 0x06002AB8 RID: 10936 RVA: 0x000C0DD4 File Offset: 0x000BEFD4
		protected override Padding DefaultMargin
		{
			get
			{
				return new Padding(3, 0, 3, 0);
			}
		}

		// Token: 0x17000A01 RID: 2561
		// (get) Token: 0x06002AB9 RID: 10937 RVA: 0x000C0DDF File Offset: 0x000BEFDF
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, this.AutoSize ? this.PreferredHeight : 23);
			}
		}

		// Token: 0x17000A02 RID: 2562
		// (get) Token: 0x06002ABA RID: 10938 RVA: 0x000C0DFA File Offset: 0x000BEFFA
		// (set) Token: 0x06002ABB RID: 10939 RVA: 0x000C0E0C File Offset: 0x000BF00C
		[SRCategory("CatAppearance")]
		[DefaultValue(FlatStyle.Standard)]
		[SRDescription("ButtonFlatStyleDescr")]
		public FlatStyle FlatStyle
		{
			get
			{
				return (FlatStyle)this.labelState[Label.StateFlatStyle];
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(FlatStyle));
				}
				if (this.labelState[Label.StateFlatStyle] != (int)value)
				{
					bool flag = this.labelState[Label.StateFlatStyle] == 3 || value == FlatStyle.System;
					this.labelState[Label.StateFlatStyle] = (int)value;
					base.SetStyle(ControlStyles.UserPaint | ControlStyles.SupportsTransparentBackColor | ControlStyles.OptimizedDoubleBuffer, this.OwnerDraw);
					if (flag)
					{
						LayoutTransaction.DoLayoutIf(this.AutoSize, this.ParentInternal, this, PropertyNames.BorderStyle);
						if (this.AutoSize)
						{
							this.AdjustSize();
						}
						base.RecreateHandle();
						return;
					}
					this.Refresh();
				}
			}
		}

		// Token: 0x17000A03 RID: 2563
		// (get) Token: 0x06002ABC RID: 10940 RVA: 0x000C0EC4 File Offset: 0x000BF0C4
		// (set) Token: 0x06002ABD RID: 10941 RVA: 0x000C0F1D File Offset: 0x000BF11D
		[Localizable(true)]
		[SRDescription("ButtonImageDescr")]
		[SRCategory("CatAppearance")]
		public Image Image
		{
			get
			{
				Image image = (Image)base.Properties.GetObject(Label.PropImage);
				if (image == null && this.ImageList != null && this.ImageIndexer.ActualIndex >= 0)
				{
					return this.ImageList.Images[this.ImageIndexer.ActualIndex];
				}
				return image;
			}
			set
			{
				if (this.Image != value)
				{
					this.StopAnimate();
					base.Properties.SetObject(Label.PropImage, value);
					if (value != null)
					{
						this.ImageIndex = -1;
						this.ImageList = null;
					}
					this.Animate();
					base.Invalidate();
				}
			}
		}

		// Token: 0x17000A04 RID: 2564
		// (get) Token: 0x06002ABE RID: 10942 RVA: 0x000C0F5C File Offset: 0x000BF15C
		// (set) Token: 0x06002ABF RID: 10943 RVA: 0x000C0FB0 File Offset: 0x000BF1B0
		[TypeConverter(typeof(ImageIndexConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue(-1)]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("ButtonImageIndexDescr")]
		[SRCategory("CatAppearance")]
		public int ImageIndex
		{
			get
			{
				if (this.ImageIndexer == null)
				{
					return -1;
				}
				int index = this.ImageIndexer.Index;
				if (this.ImageList != null && index >= this.ImageList.Images.Count)
				{
					return this.ImageList.Images.Count - 1;
				}
				return index;
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
				if (this.ImageIndex != value)
				{
					if (value != -1)
					{
						base.Properties.SetObject(Label.PropImage, null);
					}
					this.ImageIndexer.Index = value;
					base.Invalidate();
				}
			}
		}

		// Token: 0x17000A05 RID: 2565
		// (get) Token: 0x06002AC0 RID: 10944 RVA: 0x000C1034 File Offset: 0x000BF234
		// (set) Token: 0x06002AC1 RID: 10945 RVA: 0x000C104B File Offset: 0x000BF24B
		[TypeConverter(typeof(ImageKeyConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("ButtonImageIndexDescr")]
		[SRCategory("CatAppearance")]
		public string ImageKey
		{
			get
			{
				if (this.ImageIndexer != null)
				{
					return this.ImageIndexer.Key;
				}
				return null;
			}
			set
			{
				if (this.ImageKey != value)
				{
					base.Properties.SetObject(Label.PropImage, null);
					this.ImageIndexer.Key = value;
					base.Invalidate();
				}
			}
		}

		// Token: 0x17000A06 RID: 2566
		// (get) Token: 0x06002AC2 RID: 10946 RVA: 0x000C1080 File Offset: 0x000BF280
		// (set) Token: 0x06002AC3 RID: 10947 RVA: 0x000C10BA File Offset: 0x000BF2BA
		internal LabelImageIndexer ImageIndexer
		{
			get
			{
				bool flag;
				LabelImageIndexer labelImageIndexer = base.Properties.GetObject(Label.PropImageIndex, out flag) as LabelImageIndexer;
				if (labelImageIndexer == null || !flag)
				{
					labelImageIndexer = new LabelImageIndexer(this);
					this.ImageIndexer = labelImageIndexer;
				}
				return labelImageIndexer;
			}
			set
			{
				base.Properties.SetObject(Label.PropImageIndex, value);
			}
		}

		// Token: 0x17000A07 RID: 2567
		// (get) Token: 0x06002AC4 RID: 10948 RVA: 0x000C10CD File Offset: 0x000BF2CD
		// (set) Token: 0x06002AC5 RID: 10949 RVA: 0x000C10E4 File Offset: 0x000BF2E4
		[DefaultValue(null)]
		[SRDescription("ButtonImageListDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRCategory("CatAppearance")]
		public ImageList ImageList
		{
			get
			{
				return (ImageList)base.Properties.GetObject(Label.PropImageList);
			}
			set
			{
				if (this.ImageList != value)
				{
					EventHandler value2 = new EventHandler(this.ImageListRecreateHandle);
					EventHandler value3 = new EventHandler(this.DetachImageList);
					ImageList imageList = this.ImageList;
					if (imageList != null)
					{
						imageList.RecreateHandle -= value2;
						imageList.Disposed -= value3;
					}
					if (value != null)
					{
						base.Properties.SetObject(Label.PropImage, null);
					}
					base.Properties.SetObject(Label.PropImageList, value);
					if (value != null)
					{
						value.RecreateHandle += value2;
						value.Disposed += value3;
					}
					base.Invalidate();
				}
			}
		}

		// Token: 0x17000A08 RID: 2568
		// (get) Token: 0x06002AC6 RID: 10950 RVA: 0x000C1168 File Offset: 0x000BF368
		// (set) Token: 0x06002AC7 RID: 10951 RVA: 0x000C1190 File Offset: 0x000BF390
		[DefaultValue(ContentAlignment.MiddleCenter)]
		[Localizable(true)]
		[SRDescription("ButtonImageAlignDescr")]
		[SRCategory("CatAppearance")]
		public ContentAlignment ImageAlign
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(Label.PropImageAlign, out flag);
				if (flag)
				{
					return (ContentAlignment)integer;
				}
				return ContentAlignment.MiddleCenter;
			}
			set
			{
				if (!WindowsFormsUtils.EnumValidator.IsValidContentAlignment(value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ContentAlignment));
				}
				if (value != this.ImageAlign)
				{
					base.Properties.SetInteger(Label.PropImageAlign, (int)value);
					LayoutTransaction.DoLayoutIf(this.AutoSize, this.ParentInternal, this, PropertyNames.ImageAlign);
					base.Invalidate();
				}
			}
		}

		// Token: 0x17000A09 RID: 2569
		// (get) Token: 0x06002AC8 RID: 10952 RVA: 0x000C11F2 File Offset: 0x000BF3F2
		// (set) Token: 0x06002AC9 RID: 10953 RVA: 0x000C11FA File Offset: 0x000BF3FA
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

		// Token: 0x17000A0A RID: 2570
		// (get) Token: 0x06002ACA RID: 10954 RVA: 0x0001A1ED File Offset: 0x000183ED
		// (set) Token: 0x06002ACB RID: 10955 RVA: 0x0001A1F5 File Offset: 0x000183F5
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

		// Token: 0x140001EE RID: 494
		// (add) Token: 0x06002ACC RID: 10956 RVA: 0x0002410C File Offset: 0x0002230C
		// (remove) Token: 0x06002ACD RID: 10957 RVA: 0x00024115 File Offset: 0x00022315
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

		// Token: 0x140001EF RID: 495
		// (add) Token: 0x06002ACE RID: 10958 RVA: 0x000B9380 File Offset: 0x000B7580
		// (remove) Token: 0x06002ACF RID: 10959 RVA: 0x000B9389 File Offset: 0x000B7589
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

		// Token: 0x140001F0 RID: 496
		// (add) Token: 0x06002AD0 RID: 10960 RVA: 0x000B9392 File Offset: 0x000B7592
		// (remove) Token: 0x06002AD1 RID: 10961 RVA: 0x000B939B File Offset: 0x000B759B
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

		// Token: 0x140001F1 RID: 497
		// (add) Token: 0x06002AD2 RID: 10962 RVA: 0x000B93A4 File Offset: 0x000B75A4
		// (remove) Token: 0x06002AD3 RID: 10963 RVA: 0x000B93AD File Offset: 0x000B75AD
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

		// Token: 0x17000A0B RID: 2571
		// (get) Token: 0x06002AD4 RID: 10964 RVA: 0x000C1229 File Offset: 0x000BF429
		internal LayoutUtils.MeasureTextCache MeasureTextCache
		{
			get
			{
				if (this.textMeasurementCache == null)
				{
					this.textMeasurementCache = new LayoutUtils.MeasureTextCache();
				}
				return this.textMeasurementCache;
			}
		}

		// Token: 0x17000A0C RID: 2572
		// (get) Token: 0x06002AD5 RID: 10965 RVA: 0x000C1244 File Offset: 0x000BF444
		internal virtual bool OwnerDraw
		{
			get
			{
				return this.IsOwnerDraw();
			}
		}

		// Token: 0x17000A0D RID: 2573
		// (get) Token: 0x06002AD6 RID: 10966 RVA: 0x000C124C File Offset: 0x000BF44C
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("LabelPreferredHeightDescr")]
		public virtual int PreferredHeight
		{
			get
			{
				return base.PreferredSize.Height;
			}
		}

		// Token: 0x17000A0E RID: 2574
		// (get) Token: 0x06002AD7 RID: 10967 RVA: 0x000C1268 File Offset: 0x000BF468
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("LabelPreferredWidthDescr")]
		public virtual int PreferredWidth
		{
			get
			{
				return base.PreferredSize.Width;
			}
		}

		// Token: 0x17000A0F RID: 2575
		// (get) Token: 0x06002AD8 RID: 10968 RVA: 0x000C1283 File Offset: 0x000BF483
		// (set) Token: 0x06002AD9 RID: 10969 RVA: 0x000072B6 File Offset: 0x000054B6
		[Obsolete("This property has been deprecated. Use BackColor instead.  http://go.microsoft.com/fwlink/?linkid=14202")]
		protected new virtual bool RenderTransparent
		{
			get
			{
				return base.RenderTransparent;
			}
			set
			{
			}
		}

		// Token: 0x17000A10 RID: 2576
		// (get) Token: 0x06002ADA RID: 10970 RVA: 0x000C128B File Offset: 0x000BF48B
		private bool SelfSizing
		{
			get
			{
				return CommonProperties.ShouldSelfSize(this);
			}
		}

		// Token: 0x17000A11 RID: 2577
		// (get) Token: 0x06002ADB RID: 10971 RVA: 0x000B2611 File Offset: 0x000B0811
		// (set) Token: 0x06002ADC RID: 10972 RVA: 0x000B2619 File Offset: 0x000B0819
		[DefaultValue(false)]
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

		// Token: 0x140001F2 RID: 498
		// (add) Token: 0x06002ADD RID: 10973 RVA: 0x000B2622 File Offset: 0x000B0822
		// (remove) Token: 0x06002ADE RID: 10974 RVA: 0x000B262B File Offset: 0x000B082B
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

		// Token: 0x17000A12 RID: 2578
		// (get) Token: 0x06002ADF RID: 10975 RVA: 0x000C1294 File Offset: 0x000BF494
		// (set) Token: 0x06002AE0 RID: 10976 RVA: 0x000C12BC File Offset: 0x000BF4BC
		[SRDescription("LabelTextAlignDescr")]
		[Localizable(true)]
		[DefaultValue(ContentAlignment.TopLeft)]
		[SRCategory("CatAppearance")]
		public virtual ContentAlignment TextAlign
		{
			get
			{
				bool flag;
				int integer = base.Properties.GetInteger(Label.PropTextAlign, out flag);
				if (flag)
				{
					return (ContentAlignment)integer;
				}
				return ContentAlignment.TopLeft;
			}
			set
			{
				if (!WindowsFormsUtils.EnumValidator.IsValidContentAlignment(value))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ContentAlignment));
				}
				if (this.TextAlign != value)
				{
					base.Properties.SetInteger(Label.PropTextAlign, (int)value);
					base.Invalidate();
					if (!this.OwnerDraw)
					{
						base.RecreateHandle();
					}
					this.OnTextAlignChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000A13 RID: 2579
		// (get) Token: 0x06002AE1 RID: 10977 RVA: 0x00013A28 File Offset: 0x00011C28
		// (set) Token: 0x06002AE2 RID: 10978 RVA: 0x00024185 File Offset: 0x00022385
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

		// Token: 0x140001F3 RID: 499
		// (add) Token: 0x06002AE3 RID: 10979 RVA: 0x000C1320 File Offset: 0x000BF520
		// (remove) Token: 0x06002AE4 RID: 10980 RVA: 0x000C1333 File Offset: 0x000BF533
		[SRCategory("CatPropertyChanged")]
		[SRDescription("LabelOnTextAlignChangedDescr")]
		public event EventHandler TextAlignChanged
		{
			add
			{
				base.Events.AddHandler(Label.EVENT_TEXTALIGNCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(Label.EVENT_TEXTALIGNCHANGED, value);
			}
		}

		// Token: 0x17000A14 RID: 2580
		// (get) Token: 0x06002AE5 RID: 10981 RVA: 0x000C1346 File Offset: 0x000BF546
		// (set) Token: 0x06002AE6 RID: 10982 RVA: 0x000C1358 File Offset: 0x000BF558
		[DefaultValue(false)]
		[SRCategory("CatBehavior")]
		[SRDescription("UseCompatibleTextRenderingDescr")]
		public bool UseCompatibleTextRendering
		{
			get
			{
				return !this.CanUseTextRenderer || base.UseCompatibleTextRenderingInt;
			}
			set
			{
				if (base.UseCompatibleTextRenderingInt != value)
				{
					base.UseCompatibleTextRenderingInt = value;
					this.AdjustSize();
				}
			}
		}

		// Token: 0x17000A15 RID: 2581
		// (get) Token: 0x06002AE7 RID: 10983 RVA: 0x00013062 File Offset: 0x00011262
		internal override bool SupportsUseCompatibleTextRendering
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000A16 RID: 2582
		// (get) Token: 0x06002AE8 RID: 10984 RVA: 0x000C1370 File Offset: 0x000BF570
		// (set) Token: 0x06002AE9 RID: 10985 RVA: 0x000C1388 File Offset: 0x000BF588
		[SRDescription("LabelUseMnemonicDescr")]
		[DefaultValue(true)]
		[SRCategory("CatAppearance")]
		public bool UseMnemonic
		{
			get
			{
				return this.labelState[Label.StateUseMnemonic] != 0;
			}
			set
			{
				if (this.UseMnemonic != value)
				{
					this.labelState[Label.StateUseMnemonic] = (value ? 1 : 0);
					this.MeasureTextCache.InvalidateCache();
					using (LayoutTransaction.CreateTransactionIf(this.AutoSize, this.ParentInternal, this, PropertyNames.Text))
					{
						this.AdjustSize();
						base.Invalidate();
					}
					if (base.IsHandleCreated)
					{
						int num = base.WindowStyle;
						if (!this.UseMnemonic)
						{
							num |= 128;
						}
						else
						{
							num &= -129;
						}
						base.WindowStyle = num;
					}
				}
			}
		}

		// Token: 0x06002AEA RID: 10986 RVA: 0x000C1434 File Offset: 0x000BF634
		internal void AdjustSize()
		{
			if (!this.SelfSizing)
			{
				return;
			}
			if (!this.AutoSize && ((this.Anchor & (AnchorStyles.Left | AnchorStyles.Right)) == (AnchorStyles.Left | AnchorStyles.Right) || (this.Anchor & (AnchorStyles.Top | AnchorStyles.Bottom)) == (AnchorStyles.Top | AnchorStyles.Bottom)))
			{
				return;
			}
			int height = this.requestedHeight;
			int width = this.requestedWidth;
			try
			{
				Size size = this.AutoSize ? base.PreferredSize : new Size(width, height);
				base.Size = size;
			}
			finally
			{
				this.requestedHeight = height;
				this.requestedWidth = width;
			}
		}

		// Token: 0x06002AEB RID: 10987 RVA: 0x000C14BC File Offset: 0x000BF6BC
		internal void Animate()
		{
			this.Animate(!base.DesignMode && base.Visible && base.Enabled && this.ParentInternal != null);
		}

		// Token: 0x06002AEC RID: 10988 RVA: 0x000C14E8 File Offset: 0x000BF6E8
		internal void StopAnimate()
		{
			this.Animate(false);
		}

		// Token: 0x06002AED RID: 10989 RVA: 0x000C14F4 File Offset: 0x000BF6F4
		private void Animate(bool animate)
		{
			bool flag = this.labelState[Label.StateAnimating] != 0;
			if (animate != flag)
			{
				Image image = (Image)base.Properties.GetObject(Label.PropImage);
				if (animate)
				{
					if (image != null)
					{
						ImageAnimator.Animate(image, new EventHandler(this.OnFrameChanged));
						this.labelState[Label.StateAnimating] = (animate ? 1 : 0);
						return;
					}
				}
				else if (image != null)
				{
					ImageAnimator.StopAnimate(image, new EventHandler(this.OnFrameChanged));
					this.labelState[Label.StateAnimating] = (animate ? 1 : 0);
				}
			}
		}

		// Token: 0x06002AEE RID: 10990 RVA: 0x000C158C File Offset: 0x000BF78C
		protected Rectangle CalcImageRenderBounds(Image image, Rectangle r, ContentAlignment align)
		{
			Size size = image.Size;
			int x = r.X + 2;
			int y = r.Y + 2;
			if ((align & WindowsFormsUtils.AnyRightAlign) != (ContentAlignment)0)
			{
				x = r.X + r.Width - 4 - size.Width;
			}
			else if ((align & WindowsFormsUtils.AnyCenterAlign) != (ContentAlignment)0)
			{
				x = r.X + (r.Width - size.Width) / 2;
			}
			if ((align & WindowsFormsUtils.AnyBottomAlign) != (ContentAlignment)0)
			{
				y = r.Y + r.Height - 4 - size.Height;
			}
			else if ((align & WindowsFormsUtils.AnyTopAlign) != (ContentAlignment)0)
			{
				y = r.Y + 2;
			}
			else
			{
				y = r.Y + (r.Height - size.Height) / 2;
			}
			return new Rectangle(x, y, size.Width, size.Height);
		}

		// Token: 0x06002AEF RID: 10991 RVA: 0x000C1665 File Offset: 0x000BF865
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new Label.LabelAccessibleObject(this);
		}

		// Token: 0x06002AF0 RID: 10992 RVA: 0x000C166D File Offset: 0x000BF86D
		internal virtual StringFormat CreateStringFormat()
		{
			return ControlPaint.CreateStringFormat(this, this.TextAlign, this.AutoEllipsis, this.UseMnemonic);
		}

		// Token: 0x06002AF1 RID: 10993 RVA: 0x000C1687 File Offset: 0x000BF887
		private TextFormatFlags CreateTextFormatFlags()
		{
			return this.CreateTextFormatFlags(base.Size - this.GetBordersAndPadding());
		}

		// Token: 0x06002AF2 RID: 10994 RVA: 0x000C16A0 File Offset: 0x000BF8A0
		internal virtual TextFormatFlags CreateTextFormatFlags(Size constrainingSize)
		{
			TextFormatFlags textFormatFlags = ControlPaint.CreateTextFormatFlags(this, this.TextAlign, this.AutoEllipsis, this.UseMnemonic);
			if (!this.MeasureTextCache.TextRequiresWordBreak(this.Text, this.Font, constrainingSize, textFormatFlags))
			{
				textFormatFlags &= ~(TextFormatFlags.TextBoxControl | TextFormatFlags.WordBreak);
			}
			return textFormatFlags;
		}

		// Token: 0x06002AF3 RID: 10995 RVA: 0x000C16EA File Offset: 0x000BF8EA
		private void DetachImageList(object sender, EventArgs e)
		{
			this.ImageList = null;
		}

		// Token: 0x06002AF4 RID: 10996 RVA: 0x000C16F4 File Offset: 0x000BF8F4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				this.StopAnimate();
				if (this.ImageList != null)
				{
					this.ImageList.Disposed -= this.DetachImageList;
					this.ImageList.RecreateHandle -= this.ImageListRecreateHandle;
					base.Properties.SetObject(Label.PropImageList, null);
				}
				if (this.Image != null)
				{
					base.Properties.SetObject(Label.PropImage, null);
				}
				if (this.textToolTip != null)
				{
					this.textToolTip.Dispose();
					this.textToolTip = null;
				}
				this.controlToolTip = false;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002AF5 RID: 10997 RVA: 0x000C1798 File Offset: 0x000BF998
		protected void DrawImage(Graphics g, Image image, Rectangle r, ContentAlignment align)
		{
			Rectangle rectangle = this.CalcImageRenderBounds(image, r, align);
			if (!base.Enabled)
			{
				ControlPaint.DrawImageDisabled(g, image, rectangle.X, rectangle.Y, this.BackColor);
				return;
			}
			g.DrawImage(image, rectangle.X, rectangle.Y, image.Width, image.Height);
		}

		// Token: 0x06002AF6 RID: 10998 RVA: 0x000C17F8 File Offset: 0x000BF9F8
		private Size GetBordersAndPadding()
		{
			Size size = base.Padding.Size;
			if (this.UseCompatibleTextRendering)
			{
				if (this.BorderStyle != BorderStyle.None)
				{
					size.Height += 6;
					size.Width += 2;
				}
				else
				{
					size.Height += 3;
				}
			}
			else
			{
				size += this.SizeFromClientSize(Size.Empty);
				if (this.BorderStyle == BorderStyle.Fixed3D)
				{
					size += new Size(2, 2);
				}
			}
			return size;
		}

		// Token: 0x06002AF7 RID: 10999 RVA: 0x000245AE File Offset: 0x000227AE
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

		// Token: 0x06002AF8 RID: 11000 RVA: 0x000C187F File Offset: 0x000BFA7F
		internal virtual bool UseGDIMeasuring()
		{
			return this.FlatStyle == FlatStyle.System || !this.UseCompatibleTextRendering;
		}

		// Token: 0x06002AF9 RID: 11001 RVA: 0x000C1898 File Offset: 0x000BFA98
		internal override Size GetPreferredSizeCore(Size proposedConstraints)
		{
			Size bordersAndPadding = this.GetBordersAndPadding();
			proposedConstraints -= bordersAndPadding;
			proposedConstraints = LayoutUtils.UnionSizes(proposedConstraints, Size.Empty);
			Size size;
			if (string.IsNullOrEmpty(this.Text))
			{
				using (WindowsFont windowsFont = WindowsFont.FromFont(this.Font))
				{
					size = WindowsGraphicsCacheManager.MeasurementGraphics.GetTextExtent("0", windowsFont);
					size.Width = 0;
					goto IL_111;
				}
			}
			if (this.UseGDIMeasuring())
			{
				TextFormatFlags flags = (this.FlatStyle == FlatStyle.System) ? TextFormatFlags.Default : this.CreateTextFormatFlags(proposedConstraints);
				size = this.MeasureTextCache.GetTextSize(this.Text, this.Font, proposedConstraints, flags);
			}
			else
			{
				using (Graphics graphics = WindowsFormsUtils.CreateMeasurementGraphics())
				{
					using (StringFormat stringFormat = this.CreateStringFormat())
					{
						SizeF layoutArea = (proposedConstraints.Width == 1) ? new SizeF(0f, (float)proposedConstraints.Height) : new SizeF((float)proposedConstraints.Width, (float)proposedConstraints.Height);
						size = Size.Ceiling(graphics.MeasureString(this.Text, this.Font, layoutArea, stringFormat));
					}
				}
			}
			IL_111:
			size += bordersAndPadding;
			return size;
		}

		// Token: 0x06002AFA RID: 11002 RVA: 0x000C19E8 File Offset: 0x000BFBE8
		private int GetLeadingTextPaddingFromTextFormatFlags()
		{
			if (!base.IsHandleCreated)
			{
				return 0;
			}
			if (this.UseCompatibleTextRendering && this.FlatStyle != FlatStyle.System)
			{
				return 0;
			}
			int iLeftMargin;
			using (WindowsGraphics windowsGraphics = WindowsGraphics.FromHwnd(base.Handle))
			{
				TextFormatFlags textFormatFlags = this.CreateTextFormatFlags();
				if ((textFormatFlags & TextFormatFlags.NoPadding) == TextFormatFlags.NoPadding)
				{
					windowsGraphics.TextPadding = TextPaddingOptions.NoPadding;
				}
				else if ((textFormatFlags & TextFormatFlags.LeftAndRightPadding) == TextFormatFlags.LeftAndRightPadding)
				{
					windowsGraphics.TextPadding = TextPaddingOptions.LeftAndRightPadding;
				}
				using (WindowsFont windowsFont = WindowsGraphicsCacheManager.GetWindowsFont(this.Font))
				{
					IntNativeMethods.DRAWTEXTPARAMS textMargins = windowsGraphics.GetTextMargins(windowsFont);
					iLeftMargin = textMargins.iLeftMargin;
				}
			}
			return iLeftMargin;
		}

		// Token: 0x06002AFB RID: 11003 RVA: 0x000243B3 File Offset: 0x000225B3
		private void ImageListRecreateHandle(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				base.Invalidate();
			}
		}

		// Token: 0x17000A17 RID: 2583
		// (get) Token: 0x06002AFC RID: 11004 RVA: 0x00013062 File Offset: 0x00011262
		internal override bool IsMnemonicsListenerAxSourced
		{
			get
			{
				return true;
			}
		}

		// Token: 0x06002AFD RID: 11005 RVA: 0x000C1AA4 File Offset: 0x000BFCA4
		internal bool IsOwnerDraw()
		{
			return this.FlatStyle != FlatStyle.System;
		}

		// Token: 0x06002AFE RID: 11006 RVA: 0x000C1AB4 File Offset: 0x000BFCB4
		protected override void OnMouseEnter(EventArgs e)
		{
			if (!this.controlToolTip && !base.DesignMode && this.AutoEllipsis && this.showToolTip && this.textToolTip != null)
			{
				IntSecurity.AllWindows.Assert();
				try
				{
					this.controlToolTip = true;
					this.textToolTip.Show(WindowsFormsUtils.TextWithoutMnemonics(this.Text), this);
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
					this.controlToolTip = false;
				}
			}
			base.OnMouseEnter(e);
		}

		// Token: 0x06002AFF RID: 11007 RVA: 0x000C1B38 File Offset: 0x000BFD38
		protected override void OnMouseLeave(EventArgs e)
		{
			if (!this.controlToolTip && this.textToolTip != null && this.textToolTip.GetHandleCreated())
			{
				this.textToolTip.RemoveAll();
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
			base.OnMouseLeave(e);
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x000C1BA4 File Offset: 0x000BFDA4
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

		// Token: 0x06002B01 RID: 11009 RVA: 0x000C1BFA File Offset: 0x000BFDFA
		protected override void OnFontChanged(EventArgs e)
		{
			this.MeasureTextCache.InvalidateCache();
			base.OnFontChanged(e);
			this.AdjustSize();
			base.Invalidate();
		}

		// Token: 0x06002B02 RID: 11010 RVA: 0x000C1C1A File Offset: 0x000BFE1A
		protected override void OnHandleDestroyed(EventArgs e)
		{
			base.OnHandleDestroyed(e);
			if (this.textToolTip != null && this.textToolTip.GetHandleCreated())
			{
				this.textToolTip.DestroyHandle();
			}
		}

		// Token: 0x06002B03 RID: 11011 RVA: 0x000C1C44 File Offset: 0x000BFE44
		protected override void OnTextChanged(EventArgs e)
		{
			using (LayoutTransaction.CreateTransactionIf(this.AutoSize, this.ParentInternal, this, PropertyNames.Text))
			{
				this.MeasureTextCache.InvalidateCache();
				base.OnTextChanged(e);
				this.AdjustSize();
				base.Invalidate();
			}
			if (AccessibilityImprovements.Level3 && this.LiveSetting != AutomationLiveSetting.Off)
			{
				base.AccessibilityObject.RaiseLiveRegionChanged();
			}
		}

		// Token: 0x06002B04 RID: 11012 RVA: 0x000C1CC0 File Offset: 0x000BFEC0
		protected virtual void OnTextAlignChanged(EventArgs e)
		{
			EventHandler eventHandler = base.Events[Label.EVENT_TEXTALIGNCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002B05 RID: 11013 RVA: 0x000C1CEE File Offset: 0x000BFEEE
		protected override void OnPaddingChanged(EventArgs e)
		{
			base.OnPaddingChanged(e);
			this.AdjustSize();
		}

		// Token: 0x06002B06 RID: 11014 RVA: 0x000C1D00 File Offset: 0x000BFF00
		protected override void OnPaint(PaintEventArgs e)
		{
			this.Animate();
			ImageAnimator.UpdateFrames(this.Image);
			Rectangle rectangle = LayoutUtils.DeflateRect(base.ClientRectangle, base.Padding);
			Image image = this.Image;
			if (image != null)
			{
				this.DrawImage(e.Graphics, image, rectangle, base.RtlTranslateAlignment(this.ImageAlign));
			}
			IntPtr hdc = e.Graphics.GetHdc();
			Color nearestColor;
			try
			{
				using (WindowsGraphics windowsGraphics = WindowsGraphics.FromHdc(hdc))
				{
					nearestColor = windowsGraphics.GetNearestColor(base.Enabled ? this.ForeColor : base.DisabledColor);
				}
			}
			finally
			{
				e.Graphics.ReleaseHdc();
			}
			if (this.AutoEllipsis)
			{
				Rectangle clientRectangle = base.ClientRectangle;
				Size preferredSize = this.GetPreferredSize(new Size(clientRectangle.Width, clientRectangle.Height));
				this.showToolTip = (clientRectangle.Width < preferredSize.Width || clientRectangle.Height < preferredSize.Height);
			}
			else
			{
				this.showToolTip = false;
			}
			if (this.UseCompatibleTextRendering)
			{
				using (StringFormat stringFormat = this.CreateStringFormat())
				{
					if (base.Enabled)
					{
						using (Brush brush = new SolidBrush(nearestColor))
						{
							e.Graphics.DrawString(this.Text, this.Font, brush, rectangle, stringFormat);
							goto IL_1C6;
						}
					}
					ControlPaint.DrawStringDisabled(e.Graphics, this.Text, this.Font, nearestColor, rectangle, stringFormat);
					goto IL_1C6;
				}
			}
			TextFormatFlags flags = this.CreateTextFormatFlags();
			if (base.Enabled)
			{
				TextRenderer.DrawText(e.Graphics, this.Text, this.Font, rectangle, nearestColor, flags);
			}
			else
			{
				Color foreColor = TextRenderer.DisabledTextColor(this.BackColor);
				TextRenderer.DrawText(e.Graphics, this.Text, this.Font, rectangle, foreColor, flags);
			}
			IL_1C6:
			base.OnPaint(e);
		}

		// Token: 0x06002B07 RID: 11015 RVA: 0x000072B6 File Offset: 0x000054B6
		internal virtual void OnAutoEllipsisChanged()
		{
		}

		// Token: 0x06002B08 RID: 11016 RVA: 0x000C1F10 File Offset: 0x000C0110
		protected override void OnEnabledChanged(EventArgs e)
		{
			base.OnEnabledChanged(e);
			this.Animate();
		}

		// Token: 0x06002B09 RID: 11017 RVA: 0x000C1F1F File Offset: 0x000C011F
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
			if (this.SelfSizing)
			{
				this.AdjustSize();
			}
			this.Animate();
		}

		// Token: 0x06002B0A RID: 11018 RVA: 0x000C1F3C File Offset: 0x000C013C
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			this.MeasureTextCache.InvalidateCache();
			base.OnRightToLeftChanged(e);
		}

		// Token: 0x06002B0B RID: 11019 RVA: 0x000C1F50 File Offset: 0x000C0150
		protected override void OnVisibleChanged(EventArgs e)
		{
			base.OnVisibleChanged(e);
			this.Animate();
		}

		// Token: 0x06002B0C RID: 11020 RVA: 0x000C1F60 File Offset: 0x000C0160
		internal override void PrintToMetaFileRecursive(HandleRef hDC, IntPtr lParam, Rectangle bounds)
		{
			base.PrintToMetaFileRecursive(hDC, lParam, bounds);
			using (new WindowsFormsUtils.DCMapping(hDC, bounds))
			{
				using (Graphics graphics = Graphics.FromHdcInternal(hDC.Handle))
				{
					ControlPaint.PrintBorder(graphics, new Rectangle(Point.Empty, base.Size), this.BorderStyle, Border3DStyle.SunkenOuter);
				}
			}
		}

		// Token: 0x06002B0D RID: 11021 RVA: 0x000C1FE0 File Offset: 0x000C01E0
		[UIPermission(SecurityAction.LinkDemand, Window = UIPermissionWindow.AllWindows)]
		protected internal override bool ProcessMnemonic(char charCode)
		{
			if (this.UseMnemonic && Control.IsMnemonic(charCode, this.Text) && this.CanProcessMnemonic())
			{
				Control parentInternal = this.ParentInternal;
				if (parentInternal != null)
				{
					IntSecurity.ModifyFocus.Assert();
					try
					{
						if (parentInternal.SelectNextControl(this, true, false, true, false) && !parentInternal.ContainsFocus)
						{
							parentInternal.Focus();
						}
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
				return true;
			}
			return false;
		}

		// Token: 0x06002B0E RID: 11022 RVA: 0x000C2058 File Offset: 0x000C0258
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if ((specified & BoundsSpecified.Height) != BoundsSpecified.None)
			{
				this.requestedHeight = height;
			}
			if ((specified & BoundsSpecified.Width) != BoundsSpecified.None)
			{
				this.requestedWidth = width;
			}
			if (this.AutoSize && this.SelfSizing)
			{
				Size preferredSize = base.PreferredSize;
				width = preferredSize.Width;
				height = preferredSize.Height;
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		// Token: 0x06002B0F RID: 11023 RVA: 0x000C20B6 File Offset: 0x000C02B6
		private void ResetImage()
		{
			this.Image = null;
		}

		// Token: 0x06002B10 RID: 11024 RVA: 0x000C20BF File Offset: 0x000C02BF
		private bool ShouldSerializeImage()
		{
			return base.Properties.GetObject(Label.PropImage) != null;
		}

		// Token: 0x06002B11 RID: 11025 RVA: 0x000C20D4 File Offset: 0x000C02D4
		internal void SetToolTip(ToolTip toolTip)
		{
			if (toolTip != null && !this.controlToolTip)
			{
				this.controlToolTip = true;
			}
		}

		// Token: 0x17000A18 RID: 2584
		// (get) Token: 0x06002B12 RID: 11026 RVA: 0x00028D57 File Offset: 0x00026F57
		internal override bool SupportsUiaProviders
		{
			get
			{
				return AccessibilityImprovements.Level3 && !base.DesignMode;
			}
		}

		// Token: 0x06002B13 RID: 11027 RVA: 0x000C20E8 File Offset: 0x000C02E8
		public override string ToString()
		{
			string str = base.ToString();
			return str + ", Text: " + this.Text;
		}

		// Token: 0x06002B14 RID: 11028 RVA: 0x000C2110 File Offset: 0x000C0310
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg == 132)
			{
				Rectangle rectangle = base.RectangleToScreen(new Rectangle(0, 0, base.Width, base.Height));
				Point pt = new Point((int)((long)m.LParam));
				m.Result = (IntPtr)(rectangle.Contains(pt) ? 1 : 0);
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x06002B15 RID: 11029 RVA: 0x000C217A File Offset: 0x000C037A
		protected override void RescaleConstantsForDpi(int deviceDpiOld, int deviceDpiNew)
		{
			base.RescaleConstantsForDpi(deviceDpiOld, deviceDpiNew);
			if (!DpiHelper.EnableDpiChangedHighDpiImprovements)
			{
				return;
			}
			this.MeasureTextCache.InvalidateCache();
		}

		// Token: 0x04001212 RID: 4626
		private static readonly object EVENT_TEXTALIGNCHANGED = new object();

		// Token: 0x04001213 RID: 4627
		private static readonly BitVector32.Section StateUseMnemonic = BitVector32.CreateSection(1);

		// Token: 0x04001214 RID: 4628
		private static readonly BitVector32.Section StateAutoSize = BitVector32.CreateSection(1, Label.StateUseMnemonic);

		// Token: 0x04001215 RID: 4629
		private static readonly BitVector32.Section StateAnimating = BitVector32.CreateSection(1, Label.StateAutoSize);

		// Token: 0x04001216 RID: 4630
		private static readonly BitVector32.Section StateFlatStyle = BitVector32.CreateSection(3, Label.StateAnimating);

		// Token: 0x04001217 RID: 4631
		private static readonly BitVector32.Section StateBorderStyle = BitVector32.CreateSection(2, Label.StateFlatStyle);

		// Token: 0x04001218 RID: 4632
		private static readonly BitVector32.Section StateAutoEllipsis = BitVector32.CreateSection(1, Label.StateBorderStyle);

		// Token: 0x04001219 RID: 4633
		private static readonly int PropImageList = PropertyStore.CreateKey();

		// Token: 0x0400121A RID: 4634
		private static readonly int PropImage = PropertyStore.CreateKey();

		// Token: 0x0400121B RID: 4635
		private static readonly int PropTextAlign = PropertyStore.CreateKey();

		// Token: 0x0400121C RID: 4636
		private static readonly int PropImageAlign = PropertyStore.CreateKey();

		// Token: 0x0400121D RID: 4637
		private static readonly int PropImageIndex = PropertyStore.CreateKey();

		// Token: 0x0400121E RID: 4638
		private BitVector32 labelState;

		// Token: 0x0400121F RID: 4639
		private int requestedHeight;

		// Token: 0x04001220 RID: 4640
		private int requestedWidth;

		// Token: 0x04001221 RID: 4641
		private LayoutUtils.MeasureTextCache textMeasurementCache;

		// Token: 0x04001222 RID: 4642
		internal bool showToolTip;

		// Token: 0x04001223 RID: 4643
		private ToolTip textToolTip;

		// Token: 0x04001224 RID: 4644
		private bool controlToolTip;

		// Token: 0x04001225 RID: 4645
		private AutomationLiveSetting liveSetting;

		// Token: 0x020006B9 RID: 1721
		[ComVisible(true)]
		internal class LabelAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x060068DD RID: 26845 RVA: 0x0009B963 File Offset: 0x00099B63
			public LabelAccessibleObject(Label owner) : base(owner)
			{
			}

			// Token: 0x170016A4 RID: 5796
			// (get) Token: 0x060068DE RID: 26846 RVA: 0x0018624C File Offset: 0x0018444C
			public override AccessibleRole Role
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return AccessibleRole.StaticText;
					}
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return AccessibleRole.StaticText;
				}
			}

			// Token: 0x060068DF RID: 26847 RVA: 0x0009B96C File Offset: 0x00099B6C
			internal override bool IsIAccessibleExSupported()
			{
				return !base.IsOwnerControlDestroyed() && (AccessibilityImprovements.Level3 || base.IsIAccessibleExSupported());
			}

			// Token: 0x060068E0 RID: 26848 RVA: 0x001841CC File Offset: 0x001823CC
			internal override bool IsPatternSupported(int patternId)
			{
				return !base.IsOwnerControlDestroyed() && ((AccessibilityImprovements.Level3 && patternId == 10018) || base.IsPatternSupported(patternId));
			}

			// Token: 0x060068E1 RID: 26849 RVA: 0x00186278 File Offset: 0x00184478
			internal override object GetPropertyValue(int propertyID)
			{
				if (AccessibilityImprovements.Level3 && propertyID == 30005)
				{
					return this.Name;
				}
				if (propertyID == 30003)
				{
					return 50020;
				}
				return base.GetPropertyValue(propertyID);
			}
		}
	}
}

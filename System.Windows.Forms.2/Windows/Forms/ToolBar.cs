using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;

namespace System.Windows.Forms
{
	// Token: 0x020003A9 RID: 937
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultEvent("ButtonClick")]
	[Designer("System.Windows.Forms.Design.ToolBarDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("Buttons")]
	public class ToolBar : Control
	{
		// Token: 0x06003D09 RID: 15625 RVA: 0x001095E0 File Offset: 0x001077E0
		public ToolBar()
		{
			this.toolBarState = new BitVector32(31);
			base.SetStyle(ControlStyles.UserPaint, false);
			base.SetStyle(ControlStyles.FixedHeight, this.AutoSize);
			base.SetStyle(ControlStyles.FixedWidth, false);
			this.TabStop = false;
			this.Dock = DockStyle.Top;
			this.buttonsCollection = new ToolBar.ToolBarButtonCollection(this);
		}

		// Token: 0x17000EDC RID: 3804
		// (get) Token: 0x06003D0A RID: 15626 RVA: 0x00109668 File Offset: 0x00107868
		// (set) Token: 0x06003D0B RID: 15627 RVA: 0x00109670 File Offset: 0x00107870
		[SRCategory("CatBehavior")]
		[DefaultValue(ToolBarAppearance.Normal)]
		[Localizable(true)]
		[SRDescription("ToolBarAppearanceDescr")]
		public ToolBarAppearance Appearance
		{
			get
			{
				return this.appearance;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ToolBarAppearance));
				}
				if (value != this.appearance)
				{
					this.appearance = value;
					base.RecreateHandle();
				}
			}
		}

		// Token: 0x17000EDD RID: 3805
		// (get) Token: 0x06003D0C RID: 15628 RVA: 0x001096AE File Offset: 0x001078AE
		// (set) Token: 0x06003D0D RID: 15629 RVA: 0x001096C0 File Offset: 0x001078C0
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[Localizable(true)]
		[SRDescription("ToolBarAutoSizeDescr")]
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Visible)]
		public override bool AutoSize
		{
			get
			{
				return this.toolBarState[16];
			}
			set
			{
				if (this.AutoSize != value)
				{
					this.toolBarState[16] = value;
					if (this.Dock == DockStyle.Left || this.Dock == DockStyle.Right)
					{
						base.SetStyle(ControlStyles.FixedWidth, this.AutoSize);
						base.SetStyle(ControlStyles.FixedHeight, false);
					}
					else
					{
						base.SetStyle(ControlStyles.FixedHeight, this.AutoSize);
						base.SetStyle(ControlStyles.FixedWidth, false);
					}
					this.AdjustSize(this.Dock);
					this.OnAutoSizeChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x140002EA RID: 746
		// (add) Token: 0x06003D0E RID: 15630 RVA: 0x00011A56 File Offset: 0x0000FC56
		// (remove) Token: 0x06003D0F RID: 15631 RVA: 0x00011A5F File Offset: 0x0000FC5F
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

		// Token: 0x17000EDE RID: 3806
		// (get) Token: 0x06003D10 RID: 15632 RVA: 0x0001A1E5 File Offset: 0x000183E5
		// (set) Token: 0x06003D11 RID: 15633 RVA: 0x00012F98 File Offset: 0x00011198
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor
		{
			get
			{
				return base.BackColor;
			}
			set
			{
				base.BackColor = value;
			}
		}

		// Token: 0x140002EB RID: 747
		// (add) Token: 0x06003D12 RID: 15634 RVA: 0x00058DD2 File Offset: 0x00056FD2
		// (remove) Token: 0x06003D13 RID: 15635 RVA: 0x00058DDB File Offset: 0x00056FDB
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler BackColorChanged
		{
			add
			{
				base.BackColorChanged += value;
			}
			remove
			{
				base.BackColorChanged -= value;
			}
		}

		// Token: 0x17000EDF RID: 3807
		// (get) Token: 0x06003D14 RID: 15636 RVA: 0x00011A90 File Offset: 0x0000FC90
		// (set) Token: 0x06003D15 RID: 15637 RVA: 0x00011A98 File Offset: 0x0000FC98
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
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

		// Token: 0x140002EC RID: 748
		// (add) Token: 0x06003D16 RID: 15638 RVA: 0x00011AA1 File Offset: 0x0000FCA1
		// (remove) Token: 0x06003D17 RID: 15639 RVA: 0x00011AAA File Offset: 0x0000FCAA
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

		// Token: 0x17000EE0 RID: 3808
		// (get) Token: 0x06003D18 RID: 15640 RVA: 0x00011AB3 File Offset: 0x0000FCB3
		// (set) Token: 0x06003D19 RID: 15641 RVA: 0x00011ABB File Offset: 0x0000FCBB
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

		// Token: 0x140002ED RID: 749
		// (add) Token: 0x06003D1A RID: 15642 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		// (remove) Token: 0x06003D1B RID: 15643 RVA: 0x00011ACD File Offset: 0x0000FCCD
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

		// Token: 0x17000EE1 RID: 3809
		// (get) Token: 0x06003D1C RID: 15644 RVA: 0x0010973D File Offset: 0x0010793D
		// (set) Token: 0x06003D1D RID: 15645 RVA: 0x00109745 File Offset: 0x00107945
		[SRCategory("CatAppearance")]
		[DefaultValue(BorderStyle.None)]
		[DispId(-504)]
		[SRDescription("ToolBarBorderStyleDescr")]
		public BorderStyle BorderStyle
		{
			get
			{
				return this.borderStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(BorderStyle));
				}
				if (this.borderStyle != value)
				{
					this.borderStyle = value;
					base.RecreateHandle();
				}
			}
		}

		// Token: 0x17000EE2 RID: 3810
		// (get) Token: 0x06003D1E RID: 15646 RVA: 0x00109783 File Offset: 0x00107983
		[SRCategory("CatBehavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[SRDescription("ToolBarButtonsDescr")]
		[MergableProperty(false)]
		public ToolBar.ToolBarButtonCollection Buttons
		{
			get
			{
				return this.buttonsCollection;
			}
		}

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x06003D1F RID: 15647 RVA: 0x0010978C File Offset: 0x0010798C
		// (set) Token: 0x06003D20 RID: 15648 RVA: 0x0010980C File Offset: 0x00107A0C
		[SRCategory("CatAppearance")]
		[RefreshProperties(RefreshProperties.All)]
		[Localizable(true)]
		[SRDescription("ToolBarButtonSizeDescr")]
		public Size ButtonSize
		{
			get
			{
				if (!this.buttonSize.IsEmpty)
				{
					return this.buttonSize;
				}
				if (base.IsHandleCreated && this.buttons != null && this.buttonCount > 0)
				{
					int num = (int)((long)base.SendMessage(1082, 0, 0));
					if (num > 0)
					{
						return new Size(NativeMethods.Util.LOWORD(num), NativeMethods.Util.HIWORD(num));
					}
				}
				if (this.TextAlign == ToolBarTextAlign.Underneath)
				{
					return new Size(39, 36);
				}
				return new Size(23, 22);
			}
			set
			{
				if (value.Width < 0 || value.Height < 0)
				{
					throw new ArgumentOutOfRangeException("ButtonSize", SR.GetString("InvalidArgument", new object[]
					{
						"ButtonSize",
						value.ToString()
					}));
				}
				if (this.buttonSize != value)
				{
					this.buttonSize = value;
					this.maxWidth = -1;
					base.RecreateHandle();
					this.AdjustSize(this.Dock);
				}
			}
		}

		// Token: 0x17000EE4 RID: 3812
		// (get) Token: 0x06003D21 RID: 15649 RVA: 0x00109890 File Offset: 0x00107A90
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "ToolbarWindow32";
				createParams.Style |= 12;
				if (!this.Divider)
				{
					createParams.Style |= 64;
				}
				if (this.Wrappable)
				{
					createParams.Style |= 512;
				}
				if (this.ShowToolTips && !base.DesignMode)
				{
					createParams.Style |= 256;
				}
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
				ToolBarAppearance toolBarAppearance = this.appearance;
				if (toolBarAppearance != ToolBarAppearance.Normal && toolBarAppearance == ToolBarAppearance.Flat)
				{
					createParams.Style |= 2048;
				}
				ToolBarTextAlign toolBarTextAlign = this.textAlign;
				if (toolBarTextAlign != ToolBarTextAlign.Underneath && toolBarTextAlign == ToolBarTextAlign.Right)
				{
					createParams.Style |= 4096;
				}
				return createParams;
			}
		}

		// Token: 0x17000EE5 RID: 3813
		// (get) Token: 0x06003D22 RID: 15650 RVA: 0x00023D73 File Offset: 0x00021F73
		protected override ImeMode DefaultImeMode
		{
			get
			{
				return ImeMode.Disable;
			}
		}

		// Token: 0x17000EE6 RID: 3814
		// (get) Token: 0x06003D23 RID: 15651 RVA: 0x000111DC File Offset: 0x0000F3DC
		protected override Size DefaultSize
		{
			get
			{
				return new Size(100, 22);
			}
		}

		// Token: 0x17000EE7 RID: 3815
		// (get) Token: 0x06003D24 RID: 15652 RVA: 0x001099AB File Offset: 0x00107BAB
		// (set) Token: 0x06003D25 RID: 15653 RVA: 0x001099B9 File Offset: 0x00107BB9
		[SRCategory("CatAppearance")]
		[DefaultValue(true)]
		[SRDescription("ToolBarDividerDescr")]
		public bool Divider
		{
			get
			{
				return this.toolBarState[4];
			}
			set
			{
				if (this.Divider != value)
				{
					this.toolBarState[4] = value;
					base.RecreateHandle();
				}
			}
		}

		// Token: 0x17000EE8 RID: 3816
		// (get) Token: 0x06003D26 RID: 15654 RVA: 0x000FC6F6 File Offset: 0x000FA8F6
		// (set) Token: 0x06003D27 RID: 15655 RVA: 0x001099D8 File Offset: 0x00107BD8
		[Localizable(true)]
		[DefaultValue(DockStyle.Top)]
		public override DockStyle Dock
		{
			get
			{
				return base.Dock;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 5))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DockStyle));
				}
				if (this.Dock != value)
				{
					if (value == DockStyle.Left || value == DockStyle.Right)
					{
						base.SetStyle(ControlStyles.FixedWidth, this.AutoSize);
						base.SetStyle(ControlStyles.FixedHeight, false);
					}
					else
					{
						base.SetStyle(ControlStyles.FixedHeight, this.AutoSize);
						base.SetStyle(ControlStyles.FixedWidth, false);
					}
					this.AdjustSize(value);
					base.Dock = value;
				}
			}
		}

		// Token: 0x17000EE9 RID: 3817
		// (get) Token: 0x06003D28 RID: 15656 RVA: 0x000131D7 File Offset: 0x000113D7
		// (set) Token: 0x06003D29 RID: 15657 RVA: 0x000131DF File Offset: 0x000113DF
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override bool DoubleBuffered
		{
			get
			{
				return base.DoubleBuffered;
			}
			set
			{
				base.DoubleBuffered = value;
			}
		}

		// Token: 0x17000EEA RID: 3818
		// (get) Token: 0x06003D2A RID: 15658 RVA: 0x00109A5A File Offset: 0x00107C5A
		// (set) Token: 0x06003D2B RID: 15659 RVA: 0x00109A68 File Offset: 0x00107C68
		[DefaultValue(false)]
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[SRDescription("ToolBarDropDownArrowsDescr")]
		public bool DropDownArrows
		{
			get
			{
				return this.toolBarState[2];
			}
			set
			{
				if (this.DropDownArrows != value)
				{
					this.toolBarState[2] = value;
					base.RecreateHandle();
				}
			}
		}

		// Token: 0x17000EEB RID: 3819
		// (get) Token: 0x06003D2C RID: 15660 RVA: 0x0001A283 File Offset: 0x00018483
		// (set) Token: 0x06003D2D RID: 15661 RVA: 0x00013238 File Offset: 0x00011438
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color ForeColor
		{
			get
			{
				return base.ForeColor;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		// Token: 0x140002EE RID: 750
		// (add) Token: 0x06003D2E RID: 15662 RVA: 0x0005AACE File Offset: 0x00058CCE
		// (remove) Token: 0x06003D2F RID: 15663 RVA: 0x0005AAD7 File Offset: 0x00058CD7
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler ForeColorChanged
		{
			add
			{
				base.ForeColorChanged += value;
			}
			remove
			{
				base.ForeColorChanged -= value;
			}
		}

		// Token: 0x17000EEC RID: 3820
		// (get) Token: 0x06003D30 RID: 15664 RVA: 0x00109A86 File Offset: 0x00107C86
		// (set) Token: 0x06003D31 RID: 15665 RVA: 0x00109A90 File Offset: 0x00107C90
		[SRCategory("CatBehavior")]
		[DefaultValue(null)]
		[SRDescription("ToolBarImageListDescr")]
		public ImageList ImageList
		{
			get
			{
				return this.imageList;
			}
			set
			{
				if (value != this.imageList)
				{
					EventHandler value2 = new EventHandler(this.ImageListRecreateHandle);
					EventHandler value3 = new EventHandler(this.DetachImageList);
					if (this.imageList != null)
					{
						this.imageList.Disposed -= value3;
						this.imageList.RecreateHandle -= value2;
					}
					this.imageList = value;
					if (value != null)
					{
						value.Disposed += value3;
						value.RecreateHandle += value2;
					}
					if (base.IsHandleCreated)
					{
						base.RecreateHandle();
					}
				}
			}
		}

		// Token: 0x17000EED RID: 3821
		// (get) Token: 0x06003D32 RID: 15666 RVA: 0x00109B06 File Offset: 0x00107D06
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ToolBarImageSizeDescr")]
		public Size ImageSize
		{
			get
			{
				if (this.imageList != null)
				{
					return this.imageList.ImageSize;
				}
				return new Size(0, 0);
			}
		}

		// Token: 0x17000EEE RID: 3822
		// (get) Token: 0x06003D33 RID: 15667 RVA: 0x0001A1ED File Offset: 0x000183ED
		// (set) Token: 0x06003D34 RID: 15668 RVA: 0x0001A1F5 File Offset: 0x000183F5
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

		// Token: 0x140002EF RID: 751
		// (add) Token: 0x06003D35 RID: 15669 RVA: 0x0002410C File Offset: 0x0002230C
		// (remove) Token: 0x06003D36 RID: 15670 RVA: 0x00024115 File Offset: 0x00022315
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

		// Token: 0x17000EEF RID: 3823
		// (get) Token: 0x06003D37 RID: 15671 RVA: 0x00109B24 File Offset: 0x00107D24
		internal int PreferredHeight
		{
			get
			{
				int num;
				if (this.buttons == null || this.buttonCount == 0 || !base.IsHandleCreated)
				{
					num = this.ButtonSize.Height;
				}
				else
				{
					NativeMethods.RECT rect = default(NativeMethods.RECT);
					int num2 = 0;
					while (num2 < this.buttons.Length && (this.buttons[num2] == null || !this.buttons[num2].Visible))
					{
						num2++;
					}
					if (num2 == this.buttons.Length)
					{
						num2 = 0;
					}
					base.SendMessage(1075, num2, ref rect);
					num = rect.bottom - rect.top;
				}
				if (this.Wrappable && base.IsHandleCreated)
				{
					num *= (int)((long)base.SendMessage(1064, 0, 0));
				}
				num = ((num > 0) ? num : 1);
				BorderStyle borderStyle = this.borderStyle;
				if (borderStyle != BorderStyle.FixedSingle)
				{
					if (borderStyle == BorderStyle.Fixed3D)
					{
						num += SystemInformation.Border3DSize.Height;
					}
				}
				else
				{
					num += SystemInformation.BorderSize.Height;
				}
				if (this.Divider)
				{
					num += 2;
				}
				return num + 4;
			}
		}

		// Token: 0x17000EF0 RID: 3824
		// (get) Token: 0x06003D38 RID: 15672 RVA: 0x00109C30 File Offset: 0x00107E30
		internal int PreferredWidth
		{
			get
			{
				if (this.maxWidth == -1)
				{
					if (!base.IsHandleCreated || this.buttons == null)
					{
						this.maxWidth = this.ButtonSize.Width;
					}
					else
					{
						NativeMethods.RECT rect = default(NativeMethods.RECT);
						for (int i = 0; i < this.buttonCount; i++)
						{
							base.SendMessage(1075, 0, ref rect);
							if (rect.right - rect.left > this.maxWidth)
							{
								this.maxWidth = rect.right - rect.left;
							}
						}
					}
				}
				int num = this.maxWidth;
				if (this.borderStyle != BorderStyle.None)
				{
					num += SystemInformation.BorderSize.Height * 4 + 3;
				}
				return num;
			}
		}

		// Token: 0x17000EF1 RID: 3825
		// (get) Token: 0x06003D39 RID: 15673 RVA: 0x000E34A7 File Offset: 0x000E16A7
		// (set) Token: 0x06003D3A RID: 15674 RVA: 0x000C619D File Offset: 0x000C439D
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override RightToLeft RightToLeft
		{
			get
			{
				return base.RightToLeft;
			}
			set
			{
				base.RightToLeft = value;
			}
		}

		// Token: 0x140002F0 RID: 752
		// (add) Token: 0x06003D3B RID: 15675 RVA: 0x000E34AF File Offset: 0x000E16AF
		// (remove) Token: 0x06003D3C RID: 15676 RVA: 0x000E34B8 File Offset: 0x000E16B8
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler RightToLeftChanged
		{
			add
			{
				base.RightToLeftChanged += value;
			}
			remove
			{
				base.RightToLeftChanged -= value;
			}
		}

		// Token: 0x06003D3D RID: 15677 RVA: 0x00109CE0 File Offset: 0x00107EE0
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void ScaleCore(float dx, float dy)
		{
			this.currentScaleDX = dx;
			this.currentScaleDY = dy;
			base.ScaleCore(dx, dy);
			this.UpdateButtons();
		}

		// Token: 0x06003D3E RID: 15678 RVA: 0x00109CFE File Offset: 0x00107EFE
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			this.currentScaleDX = factor.Width;
			this.currentScaleDY = factor.Height;
			base.ScaleControl(factor, specified);
		}

		// Token: 0x17000EF2 RID: 3826
		// (get) Token: 0x06003D3F RID: 15679 RVA: 0x00109D22 File Offset: 0x00107F22
		// (set) Token: 0x06003D40 RID: 15680 RVA: 0x00109D30 File Offset: 0x00107F30
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[Localizable(true)]
		[SRDescription("ToolBarShowToolTipsDescr")]
		public bool ShowToolTips
		{
			get
			{
				return this.toolBarState[8];
			}
			set
			{
				if (this.ShowToolTips != value)
				{
					this.toolBarState[8] = value;
					base.RecreateHandle();
				}
			}
		}

		// Token: 0x17000EF3 RID: 3827
		// (get) Token: 0x06003D41 RID: 15681 RVA: 0x000B2611 File Offset: 0x000B0811
		// (set) Token: 0x06003D42 RID: 15682 RVA: 0x000B2619 File Offset: 0x000B0819
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

		// Token: 0x17000EF4 RID: 3828
		// (get) Token: 0x06003D43 RID: 15683 RVA: 0x00013A28 File Offset: 0x00011C28
		// (set) Token: 0x06003D44 RID: 15684 RVA: 0x00024185 File Offset: 0x00022385
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[Bindable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
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

		// Token: 0x140002F1 RID: 753
		// (add) Token: 0x06003D45 RID: 15685 RVA: 0x00046771 File Offset: 0x00044971
		// (remove) Token: 0x06003D46 RID: 15686 RVA: 0x0004677A File Offset: 0x0004497A
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

		// Token: 0x17000EF5 RID: 3829
		// (get) Token: 0x06003D47 RID: 15687 RVA: 0x00109D4E File Offset: 0x00107F4E
		// (set) Token: 0x06003D48 RID: 15688 RVA: 0x00109D56 File Offset: 0x00107F56
		[SRCategory("CatAppearance")]
		[DefaultValue(ToolBarTextAlign.Underneath)]
		[Localizable(true)]
		[SRDescription("ToolBarTextAlignDescr")]
		public ToolBarTextAlign TextAlign
		{
			get
			{
				return this.textAlign;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ToolBarTextAlign));
				}
				if (this.textAlign == value)
				{
					return;
				}
				this.textAlign = value;
				base.RecreateHandle();
			}
		}

		// Token: 0x17000EF6 RID: 3830
		// (get) Token: 0x06003D49 RID: 15689 RVA: 0x00109D95 File Offset: 0x00107F95
		// (set) Token: 0x06003D4A RID: 15690 RVA: 0x00109DA3 File Offset: 0x00107FA3
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[Localizable(true)]
		[SRDescription("ToolBarWrappableDescr")]
		public bool Wrappable
		{
			get
			{
				return this.toolBarState[1];
			}
			set
			{
				if (this.Wrappable != value)
				{
					this.toolBarState[1] = value;
					base.RecreateHandle();
				}
			}
		}

		// Token: 0x140002F2 RID: 754
		// (add) Token: 0x06003D4B RID: 15691 RVA: 0x00109DC1 File Offset: 0x00107FC1
		// (remove) Token: 0x06003D4C RID: 15692 RVA: 0x00109DDA File Offset: 0x00107FDA
		[SRCategory("CatBehavior")]
		[SRDescription("ToolBarButtonClickDescr")]
		public event ToolBarButtonClickEventHandler ButtonClick
		{
			add
			{
				this.onButtonClick = (ToolBarButtonClickEventHandler)Delegate.Combine(this.onButtonClick, value);
			}
			remove
			{
				this.onButtonClick = (ToolBarButtonClickEventHandler)Delegate.Remove(this.onButtonClick, value);
			}
		}

		// Token: 0x140002F3 RID: 755
		// (add) Token: 0x06003D4D RID: 15693 RVA: 0x00109DF3 File Offset: 0x00107FF3
		// (remove) Token: 0x06003D4E RID: 15694 RVA: 0x00109E0C File Offset: 0x0010800C
		[SRCategory("CatBehavior")]
		[SRDescription("ToolBarButtonDropDownDescr")]
		public event ToolBarButtonClickEventHandler ButtonDropDown
		{
			add
			{
				this.onButtonDropDown = (ToolBarButtonClickEventHandler)Delegate.Combine(this.onButtonDropDown, value);
			}
			remove
			{
				this.onButtonDropDown = (ToolBarButtonClickEventHandler)Delegate.Remove(this.onButtonDropDown, value);
			}
		}

		// Token: 0x140002F4 RID: 756
		// (add) Token: 0x06003D4F RID: 15695 RVA: 0x00013F87 File Offset: 0x00012187
		// (remove) Token: 0x06003D50 RID: 15696 RVA: 0x00013F90 File Offset: 0x00012190
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event PaintEventHandler Paint
		{
			add
			{
				base.Paint += value;
			}
			remove
			{
				base.Paint -= value;
			}
		}

		// Token: 0x06003D51 RID: 15697 RVA: 0x00109E28 File Offset: 0x00108028
		private void AdjustSize(DockStyle dock)
		{
			int num = this.requestedSize;
			try
			{
				if (dock == DockStyle.Left || dock == DockStyle.Right)
				{
					base.Width = (this.AutoSize ? this.PreferredWidth : num);
				}
				else
				{
					base.Height = (this.AutoSize ? this.PreferredHeight : num);
				}
			}
			finally
			{
				this.requestedSize = num;
			}
		}

		// Token: 0x06003D52 RID: 15698 RVA: 0x00104241 File Offset: 0x00102441
		internal void BeginUpdate()
		{
			base.BeginUpdateInternal();
		}

		// Token: 0x06003D53 RID: 15699 RVA: 0x00109E90 File Offset: 0x00108090
		protected override void CreateHandle()
		{
			if (!base.RecreatingHandle)
			{
				IntPtr userCookie = UnsafeNativeMethods.ThemingScope.Activate();
				try
				{
					SafeNativeMethods.InitCommonControlsEx(new NativeMethods.INITCOMMONCONTROLSEX
					{
						dwICC = 4
					});
				}
				finally
				{
					UnsafeNativeMethods.ThemingScope.Deactivate(userCookie);
				}
			}
			base.CreateHandle();
		}

		// Token: 0x06003D54 RID: 15700 RVA: 0x00109EE0 File Offset: 0x001080E0
		private void DetachImageList(object sender, EventArgs e)
		{
			this.ImageList = null;
		}

		// Token: 0x06003D55 RID: 15701 RVA: 0x00109EEC File Offset: 0x001080EC
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				lock (this)
				{
					bool state = base.GetState(4096);
					try
					{
						base.SetState(4096, true);
						if (this.imageList != null)
						{
							this.imageList.Disposed -= this.DetachImageList;
							this.imageList = null;
						}
						if (this.buttonsCollection != null)
						{
							ToolBarButton[] array = new ToolBarButton[this.buttonsCollection.Count];
							((ICollection)this.buttonsCollection).CopyTo(array, 0);
							this.buttonsCollection.Clear();
							foreach (ToolBarButton toolBarButton in array)
							{
								toolBarButton.Dispose();
							}
						}
					}
					finally
					{
						base.SetState(4096, state);
					}
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06003D56 RID: 15702 RVA: 0x00109FDC File Offset: 0x001081DC
		internal void EndUpdate()
		{
			base.EndUpdateInternal();
		}

		// Token: 0x06003D57 RID: 15703 RVA: 0x00109FE8 File Offset: 0x001081E8
		private void ForceButtonWidths()
		{
			if (this.buttons != null && this.buttonSize.IsEmpty && base.IsHandleCreated)
			{
				this.maxWidth = -1;
				for (int i = 0; i < this.buttonCount; i++)
				{
					NativeMethods.TBBUTTONINFO tbbuttoninfo = new NativeMethods.TBBUTTONINFO
					{
						cbSize = Marshal.SizeOf(typeof(NativeMethods.TBBUTTONINFO)),
						cx = this.buttons[i].Width
					};
					if ((int)tbbuttoninfo.cx > this.maxWidth)
					{
						this.maxWidth = (int)tbbuttoninfo.cx;
					}
					tbbuttoninfo.dwMask = 64;
					UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.TB_SETBUTTONINFO, i, ref tbbuttoninfo);
				}
			}
		}

		// Token: 0x06003D58 RID: 15704 RVA: 0x0010A0A2 File Offset: 0x001082A2
		private void ImageListRecreateHandle(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				base.RecreateHandle();
			}
		}

		// Token: 0x06003D59 RID: 15705 RVA: 0x0010A0B4 File Offset: 0x001082B4
		private void Insert(int index, ToolBarButton button)
		{
			button.parent = this;
			if (this.buttons == null)
			{
				this.buttons = new ToolBarButton[4];
			}
			else if (this.buttons.Length == this.buttonCount)
			{
				ToolBarButton[] destinationArray = new ToolBarButton[this.buttonCount + 4];
				Array.Copy(this.buttons, 0, destinationArray, 0, this.buttonCount);
				this.buttons = destinationArray;
			}
			if (index < this.buttonCount)
			{
				Array.Copy(this.buttons, index, this.buttons, index + 1, this.buttonCount - index);
			}
			this.buttons[index] = button;
			this.buttonCount++;
		}

		// Token: 0x06003D5A RID: 15706 RVA: 0x0010A154 File Offset: 0x00108354
		private void InsertButton(int index, ToolBarButton value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if (index < 0 || (this.buttons != null && index > this.buttonCount))
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			this.Insert(index, value);
			if (base.IsHandleCreated)
			{
				NativeMethods.TBBUTTON tbbutton = value.GetTBBUTTON(index);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.TB_INSERTBUTTON, index, ref tbbutton);
			}
			this.UpdateButtons();
		}

		// Token: 0x06003D5B RID: 15707 RVA: 0x0010A1F0 File Offset: 0x001083F0
		private int InternalAddButton(ToolBarButton button)
		{
			if (button == null)
			{
				throw new ArgumentNullException("button");
			}
			int num = this.buttonCount;
			this.Insert(num, button);
			return num;
		}

		// Token: 0x06003D5C RID: 15708 RVA: 0x0010A21C File Offset: 0x0010841C
		internal void InternalSetButton(int index, ToolBarButton value, bool recreate, bool updateText)
		{
			this.buttons[index].parent = null;
			this.buttons[index].stringIndex = (IntPtr)(-1);
			this.buttons[index] = value;
			this.buttons[index].parent = this;
			if (base.IsHandleCreated)
			{
				NativeMethods.TBBUTTONINFO tbbuttoninfo = value.GetTBBUTTONINFO(updateText, index);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.TB_SETBUTTONINFO, index, ref tbbuttoninfo);
				if (tbbuttoninfo.pszText != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(tbbuttoninfo.pszText);
				}
				if (recreate)
				{
					this.UpdateButtons();
					return;
				}
				base.SendMessage(1057, 0, 0);
				this.ForceButtonWidths();
				base.Invalidate();
			}
		}

		// Token: 0x06003D5D RID: 15709 RVA: 0x0010A2CE File Offset: 0x001084CE
		protected virtual void OnButtonClick(ToolBarButtonClickEventArgs e)
		{
			if (this.onButtonClick != null)
			{
				this.onButtonClick(this, e);
			}
		}

		// Token: 0x06003D5E RID: 15710 RVA: 0x0010A2E5 File Offset: 0x001084E5
		protected virtual void OnButtonDropDown(ToolBarButtonClickEventArgs e)
		{
			if (this.onButtonDropDown != null)
			{
				this.onButtonDropDown(this, e);
			}
		}

		// Token: 0x06003D5F RID: 15711 RVA: 0x0010A2FC File Offset: 0x001084FC
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			base.SendMessage(1054, Marshal.SizeOf(typeof(NativeMethods.TBBUTTON)), 0);
			if (this.DropDownArrows)
			{
				base.SendMessage(1108, 0, 1);
			}
			if (this.imageList != null)
			{
				base.SendMessage(1072, 0, this.imageList.Handle);
			}
			this.RealizeButtons();
			this.BeginUpdate();
			try
			{
				Size size = base.Size;
				base.Size = new Size(size.Width + 1, size.Height);
				base.Size = size;
			}
			finally
			{
				this.EndUpdate();
			}
		}

		// Token: 0x06003D60 RID: 15712 RVA: 0x0010A3B0 File Offset: 0x001085B0
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (this.Wrappable)
			{
				this.AdjustSize(this.Dock);
			}
		}

		// Token: 0x06003D61 RID: 15713 RVA: 0x0010A3CD File Offset: 0x001085CD
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			if (base.IsHandleCreated)
			{
				if (!this.buttonSize.IsEmpty)
				{
					this.SendToolbarButtonSizeMessage();
					return;
				}
				this.AdjustSize(this.Dock);
				this.ForceButtonWidths();
			}
		}

		// Token: 0x06003D62 RID: 15714 RVA: 0x0010A404 File Offset: 0x00108604
		private void RealizeButtons()
		{
			if (this.buttons != null)
			{
				IntPtr intPtr = IntPtr.Zero;
				try
				{
					this.BeginUpdate();
					for (int i = 0; i < this.buttonCount; i++)
					{
						if (this.buttons[i].Text.Length > 0)
						{
							string lparam = this.buttons[i].Text + '\0'.ToString();
							this.buttons[i].stringIndex = base.SendMessage(NativeMethods.TB_ADDSTRING, 0, lparam);
						}
						else
						{
							this.buttons[i].stringIndex = (IntPtr)(-1);
						}
					}
					int num = Marshal.SizeOf(typeof(NativeMethods.TBBUTTON));
					int num2 = this.buttonCount;
					intPtr = Marshal.AllocHGlobal(checked(num * num2));
					for (int j = 0; j < num2; j++)
					{
						NativeMethods.TBBUTTON tbbutton = this.buttons[j].GetTBBUTTON(j);
						Marshal.StructureToPtr(tbbutton, (IntPtr)(checked((long)intPtr + unchecked((long)(checked(num * j))))), true);
						this.buttons[j].parent = this;
					}
					base.SendMessage(NativeMethods.TB_ADDBUTTONS, num2, intPtr);
					base.SendMessage(1057, 0, 0);
					if (!this.buttonSize.IsEmpty)
					{
						this.SendToolbarButtonSizeMessage();
					}
					else
					{
						this.ForceButtonWidths();
					}
					this.AdjustSize(this.Dock);
				}
				finally
				{
					Marshal.FreeHGlobal(intPtr);
					this.EndUpdate();
				}
			}
		}

		// Token: 0x06003D63 RID: 15715 RVA: 0x0010A57C File Offset: 0x0010877C
		private void RemoveAt(int index)
		{
			this.buttons[index].parent = null;
			this.buttons[index].stringIndex = (IntPtr)(-1);
			this.buttonCount--;
			if (index < this.buttonCount)
			{
				Array.Copy(this.buttons, index + 1, this.buttons, index, this.buttonCount - index);
			}
			this.buttons[this.buttonCount] = null;
		}

		// Token: 0x06003D64 RID: 15716 RVA: 0x0010A5EC File Offset: 0x001087EC
		private void ResetButtonSize()
		{
			this.buttonSize = Size.Empty;
			base.RecreateHandle();
		}

		// Token: 0x06003D65 RID: 15717 RVA: 0x0010A5FF File Offset: 0x001087FF
		private void SendToolbarButtonSizeMessage()
		{
			base.SendMessage(1055, 0, NativeMethods.Util.MAKELPARAM((int)((float)this.buttonSize.Width * this.currentScaleDX), (int)((float)this.buttonSize.Height * this.currentScaleDY)));
		}

		// Token: 0x06003D66 RID: 15718 RVA: 0x0010A63C File Offset: 0x0010883C
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			int num = height;
			int num2 = width;
			base.SetBoundsCore(x, y, width, height, specified);
			Rectangle bounds = base.Bounds;
			if (this.Dock == DockStyle.Left || this.Dock == DockStyle.Right)
			{
				if ((specified & BoundsSpecified.Width) != BoundsSpecified.None)
				{
					this.requestedSize = width;
				}
				if (this.AutoSize)
				{
					width = this.PreferredWidth;
				}
				if (width != num2 && this.Dock == DockStyle.Right)
				{
					int num3 = num2 - width;
					x += num3;
				}
			}
			else
			{
				if ((specified & BoundsSpecified.Height) != BoundsSpecified.None)
				{
					this.requestedSize = height;
				}
				if (this.AutoSize)
				{
					height = this.PreferredHeight;
				}
				if (height != num && this.Dock == DockStyle.Bottom)
				{
					int num4 = num - height;
					y += num4;
				}
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		// Token: 0x06003D67 RID: 15719 RVA: 0x0010A6EE File Offset: 0x001088EE
		private bool ShouldSerializeButtonSize()
		{
			return !this.buttonSize.IsEmpty;
		}

		// Token: 0x06003D68 RID: 15720 RVA: 0x0010A6FE File Offset: 0x001088FE
		internal void SetToolTip(ToolTip toolTip)
		{
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 1060, new HandleRef(toolTip, toolTip.Handle), 0);
		}

		// Token: 0x06003D69 RID: 15721 RVA: 0x0010A724 File Offset: 0x00108924
		public override string ToString()
		{
			string text = base.ToString();
			text = text + ", Buttons.Count: " + this.buttonCount.ToString(CultureInfo.CurrentCulture);
			if (this.buttonCount > 0)
			{
				text = text + ", Buttons[0]: " + this.buttons[0].ToString();
			}
			return text;
		}

		// Token: 0x06003D6A RID: 15722 RVA: 0x0010A0A2 File Offset: 0x001082A2
		internal void UpdateButtons()
		{
			if (base.IsHandleCreated)
			{
				base.RecreateHandle();
			}
		}

		// Token: 0x06003D6B RID: 15723 RVA: 0x0010A778 File Offset: 0x00108978
		private void WmNotifyDropDown(ref Message m)
		{
			NativeMethods.NMTOOLBAR nmtoolbar = (NativeMethods.NMTOOLBAR)m.GetLParam(typeof(NativeMethods.NMTOOLBAR));
			ToolBarButton toolBarButton = this.buttons[nmtoolbar.iItem];
			if (toolBarButton == null)
			{
				throw new InvalidOperationException(SR.GetString("ToolBarButtonNotFound"));
			}
			this.OnButtonDropDown(new ToolBarButtonClickEventArgs(toolBarButton));
			Menu dropDownMenu = toolBarButton.DropDownMenu;
			if (dropDownMenu != null)
			{
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				NativeMethods.TPMPARAMS tpmparams = new NativeMethods.TPMPARAMS();
				base.SendMessage(1075, nmtoolbar.iItem, ref rect);
				if (dropDownMenu.GetType().IsAssignableFrom(typeof(ContextMenu)))
				{
					((ContextMenu)dropDownMenu).Show(this, new Point(rect.left, rect.bottom));
					return;
				}
				Menu mainMenu = dropDownMenu.GetMainMenu();
				if (mainMenu != null)
				{
					mainMenu.ProcessInitMenuPopup(dropDownMenu.Handle);
				}
				UnsafeNativeMethods.MapWindowPoints(new HandleRef(nmtoolbar.hdr, nmtoolbar.hdr.hwndFrom), NativeMethods.NullHandleRef, ref rect, 2);
				tpmparams.rcExclude_left = rect.left;
				tpmparams.rcExclude_top = rect.top;
				tpmparams.rcExclude_right = rect.right;
				tpmparams.rcExclude_bottom = rect.bottom;
				SafeNativeMethods.TrackPopupMenuEx(new HandleRef(dropDownMenu, dropDownMenu.Handle), 64, rect.left, rect.bottom, new HandleRef(this, base.Handle), tpmparams);
			}
		}

		// Token: 0x06003D6C RID: 15724 RVA: 0x0010A8D4 File Offset: 0x00108AD4
		private void WmNotifyNeedText(ref Message m)
		{
			NativeMethods.TOOLTIPTEXT tooltiptext = (NativeMethods.TOOLTIPTEXT)m.GetLParam(typeof(NativeMethods.TOOLTIPTEXT));
			int num = (int)tooltiptext.hdr.idFrom;
			ToolBarButton toolBarButton = this.buttons[num];
			if (toolBarButton != null && toolBarButton.ToolTipText != null)
			{
				tooltiptext.lpszText = toolBarButton.ToolTipText;
			}
			else
			{
				tooltiptext.lpszText = null;
			}
			tooltiptext.hinst = IntPtr.Zero;
			if (this.RightToLeft == RightToLeft.Yes)
			{
				tooltiptext.uFlags |= 4;
			}
			Marshal.StructureToPtr(tooltiptext, m.LParam, false);
		}

		// Token: 0x06003D6D RID: 15725 RVA: 0x0010A960 File Offset: 0x00108B60
		private void WmNotifyNeedTextA(ref Message m)
		{
			NativeMethods.TOOLTIPTEXTA tooltiptexta = (NativeMethods.TOOLTIPTEXTA)m.GetLParam(typeof(NativeMethods.TOOLTIPTEXTA));
			int num = (int)tooltiptexta.hdr.idFrom;
			ToolBarButton toolBarButton = this.buttons[num];
			if (toolBarButton != null && toolBarButton.ToolTipText != null)
			{
				tooltiptexta.lpszText = toolBarButton.ToolTipText;
			}
			else
			{
				tooltiptexta.lpszText = null;
			}
			tooltiptexta.hinst = IntPtr.Zero;
			if (this.RightToLeft == RightToLeft.Yes)
			{
				tooltiptexta.uFlags |= 4;
			}
			Marshal.StructureToPtr(tooltiptexta, m.LParam, false);
		}

		// Token: 0x06003D6E RID: 15726 RVA: 0x0010A9EC File Offset: 0x00108BEC
		private void WmNotifyHotItemChange(ref Message m)
		{
			NativeMethods.NMTBHOTITEM nmtbhotitem = (NativeMethods.NMTBHOTITEM)m.GetLParam(typeof(NativeMethods.NMTBHOTITEM));
			if (16 == (nmtbhotitem.dwFlags & 16))
			{
				this.hotItem = nmtbhotitem.idNew;
				return;
			}
			if (32 == (nmtbhotitem.dwFlags & 32))
			{
				this.hotItem = -1;
				return;
			}
			if (1 == (nmtbhotitem.dwFlags & 1))
			{
				this.hotItem = nmtbhotitem.idNew;
				return;
			}
			if (2 == (nmtbhotitem.dwFlags & 2))
			{
				this.hotItem = nmtbhotitem.idNew;
				return;
			}
			if (4 == (nmtbhotitem.dwFlags & 4))
			{
				this.hotItem = nmtbhotitem.idNew;
				return;
			}
			if (8 == (nmtbhotitem.dwFlags & 8))
			{
				this.hotItem = nmtbhotitem.idNew;
				return;
			}
			if (64 == (nmtbhotitem.dwFlags & 64))
			{
				this.hotItem = nmtbhotitem.idNew;
				return;
			}
			if (128 == (nmtbhotitem.dwFlags & 128))
			{
				this.hotItem = nmtbhotitem.idNew;
				return;
			}
			if (256 == (nmtbhotitem.dwFlags & 256))
			{
				this.hotItem = nmtbhotitem.idNew;
			}
		}

		// Token: 0x06003D6F RID: 15727 RVA: 0x0010AAF8 File Offset: 0x00108CF8
		private void WmReflectCommand(ref Message m)
		{
			int num = NativeMethods.Util.LOWORD(m.WParam);
			ToolBarButton toolBarButton = this.buttons[num];
			if (toolBarButton != null)
			{
				ToolBarButtonClickEventArgs e = new ToolBarButtonClickEventArgs(toolBarButton);
				this.OnButtonClick(e);
			}
			base.WndProc(ref m);
			base.ResetMouseEventArgs();
		}

		// Token: 0x06003D70 RID: 15728 RVA: 0x0010AB38 File Offset: 0x00108D38
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg != 78 && msg != 8270)
			{
				if (msg == 8465)
				{
					this.WmReflectCommand(ref m);
				}
			}
			else
			{
				NativeMethods.NMHDR nmhdr = (NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR));
				int code = nmhdr.code;
				if (code <= -706)
				{
					if (code != -713)
					{
						if (code != -710)
						{
							if (code == -706)
							{
								m.Result = (IntPtr)1;
							}
						}
						else
						{
							this.WmNotifyDropDown(ref m);
						}
					}
					else
					{
						this.WmNotifyHotItemChange(ref m);
					}
				}
				else if (code != -530)
				{
					if (code != -521)
					{
						if (code == -520)
						{
							this.WmNotifyNeedTextA(ref m);
							m.Result = (IntPtr)1;
							return;
						}
					}
					else
					{
						NativeMethods.WINDOWPLACEMENT windowplacement = default(NativeMethods.WINDOWPLACEMENT);
						int windowPlacement = UnsafeNativeMethods.GetWindowPlacement(new HandleRef(null, nmhdr.hwndFrom), ref windowplacement);
						if (windowplacement.rcNormalPosition_left == 0 && windowplacement.rcNormalPosition_top == 0 && this.hotItem != -1)
						{
							int num = 0;
							for (int i = 0; i <= this.hotItem; i++)
							{
								num += this.buttonsCollection[i].GetButtonWidth();
							}
							int num2 = windowplacement.rcNormalPosition_right - windowplacement.rcNormalPosition_left;
							int num3 = windowplacement.rcNormalPosition_bottom - windowplacement.rcNormalPosition_top;
							int x = base.Location.X + num + 1;
							int y = base.Location.Y + this.ButtonSize.Height / 2;
							NativeMethods.POINT point = new NativeMethods.POINT(x, y);
							UnsafeNativeMethods.ClientToScreen(new HandleRef(this, base.Handle), point);
							if (point.y < SystemInformation.WorkingArea.Y)
							{
								point.y += this.ButtonSize.Height / 2 + 1;
							}
							if (point.y + num3 > SystemInformation.WorkingArea.Height)
							{
								point.y -= this.ButtonSize.Height / 2 + num3 + 1;
							}
							if (point.x + num2 > SystemInformation.WorkingArea.Right)
							{
								point.x -= this.ButtonSize.Width + num2 + 2;
							}
							SafeNativeMethods.SetWindowPos(new HandleRef(null, nmhdr.hwndFrom), NativeMethods.NullHandleRef, point.x, point.y, 0, 0, 21);
							m.Result = (IntPtr)1;
							return;
						}
					}
				}
				else if (Marshal.SystemDefaultCharSize == 2)
				{
					this.WmNotifyNeedText(ref m);
					m.Result = (IntPtr)1;
					return;
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x04002407 RID: 9223
		private ToolBar.ToolBarButtonCollection buttonsCollection;

		// Token: 0x04002408 RID: 9224
		internal Size buttonSize = Size.Empty;

		// Token: 0x04002409 RID: 9225
		private int requestedSize;

		// Token: 0x0400240A RID: 9226
		internal const int DDARROW_WIDTH = 15;

		// Token: 0x0400240B RID: 9227
		private ToolBarAppearance appearance;

		// Token: 0x0400240C RID: 9228
		private BorderStyle borderStyle;

		// Token: 0x0400240D RID: 9229
		private ToolBarButton[] buttons;

		// Token: 0x0400240E RID: 9230
		private int buttonCount;

		// Token: 0x0400240F RID: 9231
		private ToolBarTextAlign textAlign;

		// Token: 0x04002410 RID: 9232
		private ImageList imageList;

		// Token: 0x04002411 RID: 9233
		private int maxWidth = -1;

		// Token: 0x04002412 RID: 9234
		private int hotItem = -1;

		// Token: 0x04002413 RID: 9235
		private float currentScaleDX = 1f;

		// Token: 0x04002414 RID: 9236
		private float currentScaleDY = 1f;

		// Token: 0x04002415 RID: 9237
		private const int TOOLBARSTATE_wrappable = 1;

		// Token: 0x04002416 RID: 9238
		private const int TOOLBARSTATE_dropDownArrows = 2;

		// Token: 0x04002417 RID: 9239
		private const int TOOLBARSTATE_divider = 4;

		// Token: 0x04002418 RID: 9240
		private const int TOOLBARSTATE_showToolTips = 8;

		// Token: 0x04002419 RID: 9241
		private const int TOOLBARSTATE_autoSize = 16;

		// Token: 0x0400241A RID: 9242
		private BitVector32 toolBarState;

		// Token: 0x0400241B RID: 9243
		private ToolBarButtonClickEventHandler onButtonClick;

		// Token: 0x0400241C RID: 9244
		private ToolBarButtonClickEventHandler onButtonDropDown;

		// Token: 0x020007F5 RID: 2037
		public class ToolBarButtonCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06006E85 RID: 28293 RVA: 0x00195452 File Offset: 0x00193652
			public ToolBarButtonCollection(ToolBar owner)
			{
				this.owner = owner;
			}

			// Token: 0x1700181F RID: 6175
			public virtual ToolBarButton this[int index]
			{
				get
				{
					if (index < 0 || (this.owner.buttons != null && index >= this.owner.buttonCount))
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					return this.owner.buttons[index];
				}
				set
				{
					if (index < 0 || (this.owner.buttons != null && index >= this.owner.buttonCount))
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (value == null)
					{
						throw new ArgumentNullException("value");
					}
					this.owner.InternalSetButton(index, value, true, true);
				}
			}

			// Token: 0x17001820 RID: 6176
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					if (value is ToolBarButton)
					{
						this[index] = (ToolBarButton)value;
						return;
					}
					throw new ArgumentException(SR.GetString("ToolBarBadToolBarButton"), "value");
				}
			}

			// Token: 0x17001821 RID: 6177
			public virtual ToolBarButton this[string key]
			{
				get
				{
					if (string.IsNullOrEmpty(key))
					{
						return null;
					}
					int index = this.IndexOfKey(key);
					if (this.IsValidIndex(index))
					{
						return this[index];
					}
					return null;
				}
			}

			// Token: 0x17001822 RID: 6178
			// (get) Token: 0x06006E8B RID: 28299 RVA: 0x001955B5 File Offset: 0x001937B5
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.owner.buttonCount;
				}
			}

			// Token: 0x17001823 RID: 6179
			// (get) Token: 0x06006E8C RID: 28300 RVA: 0x00006C59 File Offset: 0x00004E59
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17001824 RID: 6180
			// (get) Token: 0x06006E8D RID: 28301 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001825 RID: 6181
			// (get) Token: 0x06006E8E RID: 28302 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001826 RID: 6182
			// (get) Token: 0x06006E8F RID: 28303 RVA: 0x00011A20 File Offset: 0x0000FC20
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06006E90 RID: 28304 RVA: 0x001955C4 File Offset: 0x001937C4
			public int Add(ToolBarButton button)
			{
				int result = this.owner.InternalAddButton(button);
				if (!this.suspendUpdate)
				{
					this.owner.UpdateButtons();
				}
				return result;
			}

			// Token: 0x06006E91 RID: 28305 RVA: 0x001955F4 File Offset: 0x001937F4
			public int Add(string text)
			{
				ToolBarButton button = new ToolBarButton(text);
				return this.Add(button);
			}

			// Token: 0x06006E92 RID: 28306 RVA: 0x0019560F File Offset: 0x0019380F
			int IList.Add(object button)
			{
				if (button is ToolBarButton)
				{
					return this.Add((ToolBarButton)button);
				}
				throw new ArgumentException(SR.GetString("ToolBarBadToolBarButton"), "button");
			}

			// Token: 0x06006E93 RID: 28307 RVA: 0x0019563C File Offset: 0x0019383C
			public void AddRange(ToolBarButton[] buttons)
			{
				if (buttons == null)
				{
					throw new ArgumentNullException("buttons");
				}
				try
				{
					this.suspendUpdate = true;
					foreach (ToolBarButton button in buttons)
					{
						this.Add(button);
					}
				}
				finally
				{
					this.suspendUpdate = false;
					this.owner.UpdateButtons();
				}
			}

			// Token: 0x06006E94 RID: 28308 RVA: 0x001956A0 File Offset: 0x001938A0
			public void Clear()
			{
				if (this.owner.buttons == null)
				{
					return;
				}
				for (int i = this.owner.buttonCount; i > 0; i--)
				{
					if (this.owner.IsHandleCreated)
					{
						this.owner.SendMessage(1046, i - 1, 0);
					}
					this.owner.RemoveAt(i - 1);
				}
				this.owner.buttons = null;
				this.owner.buttonCount = 0;
				if (!this.owner.Disposing)
				{
					this.owner.UpdateButtons();
				}
			}

			// Token: 0x06006E95 RID: 28309 RVA: 0x00195731 File Offset: 0x00193931
			public bool Contains(ToolBarButton button)
			{
				return this.IndexOf(button) != -1;
			}

			// Token: 0x06006E96 RID: 28310 RVA: 0x00195740 File Offset: 0x00193940
			bool IList.Contains(object button)
			{
				return button is ToolBarButton && this.Contains((ToolBarButton)button);
			}

			// Token: 0x06006E97 RID: 28311 RVA: 0x00195758 File Offset: 0x00193958
			public virtual bool ContainsKey(string key)
			{
				return this.IsValidIndex(this.IndexOfKey(key));
			}

			// Token: 0x06006E98 RID: 28312 RVA: 0x00195767 File Offset: 0x00193967
			void ICollection.CopyTo(Array dest, int index)
			{
				if (this.owner.buttonCount > 0)
				{
					Array.Copy(this.owner.buttons, 0, dest, index, this.owner.buttonCount);
				}
			}

			// Token: 0x06006E99 RID: 28313 RVA: 0x00195798 File Offset: 0x00193998
			public int IndexOf(ToolBarButton button)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (this[i] == button)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06006E9A RID: 28314 RVA: 0x001957C3 File Offset: 0x001939C3
			int IList.IndexOf(object button)
			{
				if (button is ToolBarButton)
				{
					return this.IndexOf((ToolBarButton)button);
				}
				return -1;
			}

			// Token: 0x06006E9B RID: 28315 RVA: 0x001957DC File Offset: 0x001939DC
			public virtual int IndexOfKey(string key)
			{
				if (string.IsNullOrEmpty(key))
				{
					return -1;
				}
				if (this.IsValidIndex(this.lastAccessedIndex) && WindowsFormsUtils.SafeCompareStrings(this[this.lastAccessedIndex].Name, key, true))
				{
					return this.lastAccessedIndex;
				}
				for (int i = 0; i < this.Count; i++)
				{
					if (WindowsFormsUtils.SafeCompareStrings(this[i].Name, key, true))
					{
						this.lastAccessedIndex = i;
						return i;
					}
				}
				this.lastAccessedIndex = -1;
				return -1;
			}

			// Token: 0x06006E9C RID: 28316 RVA: 0x00195859 File Offset: 0x00193A59
			public void Insert(int index, ToolBarButton button)
			{
				this.owner.InsertButton(index, button);
			}

			// Token: 0x06006E9D RID: 28317 RVA: 0x00195868 File Offset: 0x00193A68
			void IList.Insert(int index, object button)
			{
				if (button is ToolBarButton)
				{
					this.Insert(index, (ToolBarButton)button);
					return;
				}
				throw new ArgumentException(SR.GetString("ToolBarBadToolBarButton"), "button");
			}

			// Token: 0x06006E9E RID: 28318 RVA: 0x00195894 File Offset: 0x00193A94
			private bool IsValidIndex(int index)
			{
				return index >= 0 && index < this.Count;
			}

			// Token: 0x06006E9F RID: 28319 RVA: 0x001958A8 File Offset: 0x00193AA8
			public void RemoveAt(int index)
			{
				int num = (this.owner.buttons == null) ? 0 : this.owner.buttonCount;
				if (index < 0 || index >= num)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.owner.IsHandleCreated)
				{
					this.owner.SendMessage(1046, index, 0);
				}
				this.owner.RemoveAt(index);
				this.owner.UpdateButtons();
			}

			// Token: 0x06006EA0 RID: 28320 RVA: 0x00195944 File Offset: 0x00193B44
			public virtual void RemoveByKey(string key)
			{
				int index = this.IndexOfKey(key);
				if (this.IsValidIndex(index))
				{
					this.RemoveAt(index);
				}
			}

			// Token: 0x06006EA1 RID: 28321 RVA: 0x0019596C File Offset: 0x00193B6C
			public void Remove(ToolBarButton button)
			{
				int num = this.IndexOf(button);
				if (num != -1)
				{
					this.RemoveAt(num);
				}
			}

			// Token: 0x06006EA2 RID: 28322 RVA: 0x0019598C File Offset: 0x00193B8C
			void IList.Remove(object button)
			{
				if (button is ToolBarButton)
				{
					this.Remove((ToolBarButton)button);
				}
			}

			// Token: 0x06006EA3 RID: 28323 RVA: 0x001959A4 File Offset: 0x00193BA4
			public IEnumerator GetEnumerator()
			{
				object[] buttons = this.owner.buttons;
				return new WindowsFormsUtils.ArraySubsetEnumerator(buttons, this.owner.buttonCount);
			}

			// Token: 0x040042E8 RID: 17128
			private ToolBar owner;

			// Token: 0x040042E9 RID: 17129
			private bool suspendUpdate;

			// Token: 0x040042EA RID: 17130
			private int lastAccessedIndex = -1;
		}
	}
}

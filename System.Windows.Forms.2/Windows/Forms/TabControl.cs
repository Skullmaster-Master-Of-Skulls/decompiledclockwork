using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x02000387 RID: 903
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("TabPages")]
	[DefaultEvent("SelectedIndexChanged")]
	[Designer("System.Windows.Forms.Design.TabControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionTabControl")]
	public class TabControl : Control
	{
		// Token: 0x06003B17 RID: 15127 RVA: 0x00103688 File Offset: 0x00101888
		public TabControl()
		{
			this.tabControlState = new BitVector32(0);
			this.tabCollection = new TabControl.TabPageCollection(this);
			base.SetStyle(ControlStyles.UserPaint, false);
		}

		// Token: 0x17000E67 RID: 3687
		// (get) Token: 0x06003B18 RID: 15128 RVA: 0x00103713 File Offset: 0x00101913
		// (set) Token: 0x06003B19 RID: 15129 RVA: 0x0010371C File Offset: 0x0010191C
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[DefaultValue(TabAlignment.Top)]
		[RefreshProperties(RefreshProperties.All)]
		[SRDescription("TabBaseAlignmentDescr")]
		public TabAlignment Alignment
		{
			get
			{
				return this.alignment;
			}
			set
			{
				if (this.alignment != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(TabAlignment));
					}
					this.alignment = value;
					if (this.alignment == TabAlignment.Left || this.alignment == TabAlignment.Right)
					{
						this.Multiline = true;
					}
					base.RecreateHandle();
				}
			}
		}

		// Token: 0x17000E68 RID: 3688
		// (get) Token: 0x06003B1A RID: 15130 RVA: 0x0010377E File Offset: 0x0010197E
		// (set) Token: 0x06003B1B RID: 15131 RVA: 0x0010379C File Offset: 0x0010199C
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[DefaultValue(TabAppearance.Normal)]
		[SRDescription("TabBaseAppearanceDescr")]
		public TabAppearance Appearance
		{
			get
			{
				if (this.appearance == TabAppearance.FlatButtons && this.alignment != TabAlignment.Top)
				{
					return TabAppearance.Buttons;
				}
				return this.appearance;
			}
			set
			{
				if (this.appearance != value)
				{
					if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
					{
						throw new InvalidEnumArgumentException("value", (int)value, typeof(TabAppearance));
					}
					this.appearance = value;
					base.RecreateHandle();
					this.OnStyleChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000E69 RID: 3689
		// (get) Token: 0x06003B1C RID: 15132 RVA: 0x00030717 File Offset: 0x0002E917
		// (set) Token: 0x06003B1D RID: 15133 RVA: 0x000072B6 File Offset: 0x000054B6
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override Color BackColor
		{
			get
			{
				return SystemColors.Control;
			}
			set
			{
			}
		}

		// Token: 0x140002D1 RID: 721
		// (add) Token: 0x06003B1E RID: 15134 RVA: 0x00058DD2 File Offset: 0x00056FD2
		// (remove) Token: 0x06003B1F RID: 15135 RVA: 0x00058DDB File Offset: 0x00056FDB
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

		// Token: 0x17000E6A RID: 3690
		// (get) Token: 0x06003B20 RID: 15136 RVA: 0x00011A90 File Offset: 0x0000FC90
		// (set) Token: 0x06003B21 RID: 15137 RVA: 0x00011A98 File Offset: 0x0000FC98
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

		// Token: 0x140002D2 RID: 722
		// (add) Token: 0x06003B22 RID: 15138 RVA: 0x00011AA1 File Offset: 0x0000FCA1
		// (remove) Token: 0x06003B23 RID: 15139 RVA: 0x00011AAA File Offset: 0x0000FCAA
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

		// Token: 0x17000E6B RID: 3691
		// (get) Token: 0x06003B24 RID: 15140 RVA: 0x00011AB3 File Offset: 0x0000FCB3
		// (set) Token: 0x06003B25 RID: 15141 RVA: 0x00011ABB File Offset: 0x0000FCBB
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

		// Token: 0x140002D3 RID: 723
		// (add) Token: 0x06003B26 RID: 15142 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		// (remove) Token: 0x06003B27 RID: 15143 RVA: 0x00011ACD File Offset: 0x0000FCCD
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

		// Token: 0x17000E6C RID: 3692
		// (get) Token: 0x06003B28 RID: 15144 RVA: 0x000B91B8 File Offset: 0x000B73B8
		protected override Size DefaultSize
		{
			get
			{
				return new Size(200, 100);
			}
		}

		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x06003B29 RID: 15145 RVA: 0x000131D7 File Offset: 0x000113D7
		// (set) Token: 0x06003B2A RID: 15146 RVA: 0x000131DF File Offset: 0x000113DF
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

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x06003B2B RID: 15147 RVA: 0x0001A283 File Offset: 0x00018483
		// (set) Token: 0x06003B2C RID: 15148 RVA: 0x00013238 File Offset: 0x00011438
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

		// Token: 0x140002D4 RID: 724
		// (add) Token: 0x06003B2D RID: 15149 RVA: 0x0005AACE File Offset: 0x00058CCE
		// (remove) Token: 0x06003B2E RID: 15150 RVA: 0x0005AAD7 File Offset: 0x00058CD7
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

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x06003B2F RID: 15151 RVA: 0x001037F0 File Offset: 0x001019F0
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "SysTabControl32";
				if (this.Multiline)
				{
					createParams.Style |= 512;
				}
				if (this.drawMode == TabDrawMode.OwnerDrawFixed)
				{
					createParams.Style |= 8192;
				}
				if (this.ShowToolTips && !base.DesignMode)
				{
					createParams.Style |= 16384;
				}
				if (this.alignment == TabAlignment.Bottom || this.alignment == TabAlignment.Right)
				{
					createParams.Style |= 2;
				}
				if (this.alignment == TabAlignment.Left || this.alignment == TabAlignment.Right)
				{
					createParams.Style |= 640;
				}
				if (this.tabControlState[1])
				{
					createParams.Style |= 64;
				}
				if (this.appearance == TabAppearance.Normal)
				{
					createParams.Style |= 0;
				}
				else
				{
					createParams.Style |= 256;
					if (this.appearance == TabAppearance.FlatButtons && this.alignment == TabAlignment.Top)
					{
						createParams.Style |= 8;
					}
				}
				switch (this.sizeMode)
				{
				case TabSizeMode.Normal:
					createParams.Style |= 2048;
					break;
				case TabSizeMode.FillToRight:
					createParams.Style |= 0;
					break;
				case TabSizeMode.Fixed:
					createParams.Style |= 1024;
					break;
				}
				if (this.RightToLeft == RightToLeft.Yes && this.RightToLeftLayout)
				{
					createParams.ExStyle |= 5242880;
					createParams.ExStyle &= -28673;
				}
				return createParams;
			}
		}

		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x06003B30 RID: 15152 RVA: 0x00103998 File Offset: 0x00101B98
		public override Rectangle DisplayRectangle
		{
			get
			{
				if (!this.cachedDisplayRect.IsEmpty)
				{
					return this.cachedDisplayRect;
				}
				Rectangle bounds = base.Bounds;
				NativeMethods.RECT rect = NativeMethods.RECT.FromXYWH(bounds.X, bounds.Y, bounds.Width, bounds.Height);
				if (!base.IsDisposed)
				{
					if (!base.IsActiveX && !base.IsHandleCreated)
					{
						this.CreateHandle();
					}
					if (base.IsHandleCreated)
					{
						base.SendMessage(4904, 0, ref rect);
					}
				}
				Rectangle result = Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
				Point location = base.Location;
				result.X -= location.X;
				result.Y -= location.Y;
				this.cachedDisplayRect = result;
				return result;
			}
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x06003B31 RID: 15153 RVA: 0x00103A6E File Offset: 0x00101C6E
		// (set) Token: 0x06003B32 RID: 15154 RVA: 0x00103A76 File Offset: 0x00101C76
		[SRCategory("CatBehavior")]
		[DefaultValue(TabDrawMode.Normal)]
		[SRDescription("TabBaseDrawModeDescr")]
		public TabDrawMode DrawMode
		{
			get
			{
				return this.drawMode;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 1))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(TabDrawMode));
				}
				if (this.drawMode != value)
				{
					this.drawMode = value;
					base.RecreateHandle();
				}
			}
		}

		// Token: 0x17000E72 RID: 3698
		// (get) Token: 0x06003B33 RID: 15155 RVA: 0x00103AB4 File Offset: 0x00101CB4
		// (set) Token: 0x06003B34 RID: 15156 RVA: 0x00103AC2 File Offset: 0x00101CC2
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TabBaseHotTrackDescr")]
		public bool HotTrack
		{
			get
			{
				return this.tabControlState[1];
			}
			set
			{
				if (this.HotTrack != value)
				{
					this.tabControlState[1] = value;
					if (base.IsHandleCreated)
					{
						base.RecreateHandle();
					}
				}
			}
		}

		// Token: 0x17000E73 RID: 3699
		// (get) Token: 0x06003B35 RID: 15157 RVA: 0x00103AE8 File Offset: 0x00101CE8
		// (set) Token: 0x06003B36 RID: 15158 RVA: 0x00103AF0 File Offset: 0x00101CF0
		[SRCategory("CatAppearance")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(null)]
		[SRDescription("TabBaseImageListDescr")]
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
					this.imageList = value;
					IntPtr lparam = (value != null) ? value.Handle : IntPtr.Zero;
					if (base.IsHandleCreated)
					{
						base.SendMessage(4867, IntPtr.Zero, lparam);
					}
					foreach (object obj in this.TabPages)
					{
						TabPage tabPage = (TabPage)obj;
						tabPage.ImageIndexer.ImageList = value;
					}
					if (value != null)
					{
						value.RecreateHandle += value2;
						value.Disposed += value3;
					}
				}
			}
		}

		// Token: 0x17000E74 RID: 3700
		// (get) Token: 0x06003B37 RID: 15159 RVA: 0x00103BDC File Offset: 0x00101DDC
		// (set) Token: 0x06003B38 RID: 15160 RVA: 0x00103C28 File Offset: 0x00101E28
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[SRDescription("TabBaseItemSizeDescr")]
		public Size ItemSize
		{
			get
			{
				if (!this.itemSize.IsEmpty)
				{
					return this.itemSize;
				}
				if (base.IsHandleCreated)
				{
					this.tabControlState[8] = true;
					return this.GetTabRect(0).Size;
				}
				return TabControl.DEFAULT_ITEMSIZE;
			}
			set
			{
				if (value.Width < 0 || value.Height < 0)
				{
					throw new ArgumentOutOfRangeException("ItemSize", SR.GetString("InvalidArgument", new object[]
					{
						"ItemSize",
						value.ToString()
					}));
				}
				this.itemSize = value;
				this.ApplyItemSize();
				this.UpdateSize();
				base.Invalidate();
			}
		}

		// Token: 0x17000E75 RID: 3701
		// (get) Token: 0x06003B39 RID: 15161 RVA: 0x00103C95 File Offset: 0x00101E95
		// (set) Token: 0x06003B3A RID: 15162 RVA: 0x00103CA7 File Offset: 0x00101EA7
		private bool InsertingItem
		{
			get
			{
				return this.tabControlState[128];
			}
			set
			{
				this.tabControlState[128] = value;
			}
		}

		// Token: 0x17000E76 RID: 3702
		// (get) Token: 0x06003B3B RID: 15163 RVA: 0x00103CBA File Offset: 0x00101EBA
		// (set) Token: 0x06003B3C RID: 15164 RVA: 0x00103CC8 File Offset: 0x00101EC8
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TabBaseMultilineDescr")]
		public bool Multiline
		{
			get
			{
				return this.tabControlState[2];
			}
			set
			{
				if (this.Multiline != value)
				{
					this.tabControlState[2] = value;
					if (!this.Multiline && (this.alignment == TabAlignment.Left || this.alignment == TabAlignment.Right))
					{
						this.alignment = TabAlignment.Top;
					}
					base.RecreateHandle();
				}
			}
		}

		// Token: 0x17000E77 RID: 3703
		// (get) Token: 0x06003B3D RID: 15165 RVA: 0x00103D07 File Offset: 0x00101F07
		// (set) Token: 0x06003B3E RID: 15166 RVA: 0x00103D10 File Offset: 0x00101F10
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[SRDescription("TabBasePaddingDescr")]
		public new Point Padding
		{
			get
			{
				return this.padding;
			}
			set
			{
				if (value.X < 0 || value.Y < 0)
				{
					throw new ArgumentOutOfRangeException("Padding", SR.GetString("InvalidArgument", new object[]
					{
						"Padding",
						value.ToString()
					}));
				}
				if (this.padding != value)
				{
					this.padding = value;
					if (base.IsHandleCreated)
					{
						base.RecreateHandle();
					}
				}
			}
		}

		// Token: 0x17000E78 RID: 3704
		// (get) Token: 0x06003B3F RID: 15167 RVA: 0x00103D87 File Offset: 0x00101F87
		// (set) Token: 0x06003B40 RID: 15168 RVA: 0x00103D90 File Offset: 0x00101F90
		[SRCategory("CatAppearance")]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("ControlRightToLeftLayoutDescr")]
		public virtual bool RightToLeftLayout
		{
			get
			{
				return this.rightToLeftLayout;
			}
			set
			{
				if (value != this.rightToLeftLayout)
				{
					this.rightToLeftLayout = value;
					using (new LayoutTransaction(this, this, PropertyNames.RightToLeftLayout))
					{
						this.OnRightToLeftLayoutChanged(EventArgs.Empty);
					}
				}
			}
		}

		// Token: 0x17000E79 RID: 3705
		// (get) Token: 0x06003B41 RID: 15169 RVA: 0x00103DE4 File Offset: 0x00101FE4
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TabBaseRowCountDescr")]
		public int RowCount
		{
			get
			{
				return (int)((long)base.SendMessage(4908, 0, 0));
			}
		}

		// Token: 0x17000E7A RID: 3706
		// (get) Token: 0x06003B42 RID: 15170 RVA: 0x00103E08 File Offset: 0x00102008
		// (set) Token: 0x06003B43 RID: 15171 RVA: 0x00103E3C File Offset: 0x0010203C
		[Browsable(false)]
		[SRCategory("CatBehavior")]
		[DefaultValue(-1)]
		[SRDescription("selectedIndexDescr")]
		public int SelectedIndex
		{
			get
			{
				if (base.IsHandleCreated)
				{
					return (int)((long)base.SendMessage(4875, 0, 0));
				}
				return this.selectedIndex;
			}
			set
			{
				if (value < -1)
				{
					throw new ArgumentOutOfRangeException("SelectedIndex", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"SelectedIndex",
						value.ToString(CultureInfo.CurrentCulture),
						-1.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.SelectedIndex != value)
				{
					if (base.IsHandleCreated)
					{
						if (!this.tabControlState[16] && !this.tabControlState[64])
						{
							this.tabControlState[32] = true;
							if (this.WmSelChanging())
							{
								this.tabControlState[32] = false;
								return;
							}
							if (base.ValidationCancelled)
							{
								this.tabControlState[32] = false;
								return;
							}
						}
						base.SendMessage(4876, value, 0);
						if (!this.tabControlState[16] && !this.tabControlState[64])
						{
							this.tabControlState[64] = true;
							if (this.WmSelChange())
							{
								this.tabControlState[32] = false;
								this.tabControlState[64] = false;
								return;
							}
							this.tabControlState[64] = false;
							return;
						}
					}
					else
					{
						this.selectedIndex = value;
					}
				}
			}
		}

		// Token: 0x17000E7B RID: 3707
		// (get) Token: 0x06003B44 RID: 15172 RVA: 0x00103F76 File Offset: 0x00102176
		// (set) Token: 0x06003B45 RID: 15173 RVA: 0x00103F7E File Offset: 0x0010217E
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TabControlSelectedTabDescr")]
		public TabPage SelectedTab
		{
			get
			{
				return this.SelectedTabInternal;
			}
			set
			{
				this.SelectedTabInternal = value;
			}
		}

		// Token: 0x17000E7C RID: 3708
		// (get) Token: 0x06003B46 RID: 15174 RVA: 0x00103F88 File Offset: 0x00102188
		// (set) Token: 0x06003B47 RID: 15175 RVA: 0x00103FAC File Offset: 0x001021AC
		internal TabPage SelectedTabInternal
		{
			get
			{
				int num = this.SelectedIndex;
				if (num == -1)
				{
					return null;
				}
				return this.tabPages[num];
			}
			set
			{
				int num = this.FindTabPage(value);
				this.SelectedIndex = num;
			}
		}

		// Token: 0x17000E7D RID: 3709
		// (get) Token: 0x06003B48 RID: 15176 RVA: 0x00103FC8 File Offset: 0x001021C8
		// (set) Token: 0x06003B49 RID: 15177 RVA: 0x00103FD0 File Offset: 0x001021D0
		[SRCategory("CatBehavior")]
		[DefaultValue(TabSizeMode.Normal)]
		[SRDescription("TabBaseSizeModeDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public TabSizeMode SizeMode
		{
			get
			{
				return this.sizeMode;
			}
			set
			{
				if (this.sizeMode == value)
				{
					return;
				}
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(TabSizeMode));
				}
				this.sizeMode = value;
				base.RecreateHandle();
			}
		}

		// Token: 0x17000E7E RID: 3710
		// (get) Token: 0x06003B4A RID: 15178 RVA: 0x0010400F File Offset: 0x0010220F
		// (set) Token: 0x06003B4B RID: 15179 RVA: 0x0010401D File Offset: 0x0010221D
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[Localizable(true)]
		[SRDescription("TabBaseShowToolTipsDescr")]
		public bool ShowToolTips
		{
			get
			{
				return this.tabControlState[4];
			}
			set
			{
				if (this.ShowToolTips != value)
				{
					this.tabControlState[4] = value;
					base.RecreateHandle();
				}
			}
		}

		// Token: 0x17000E7F RID: 3711
		// (get) Token: 0x06003B4C RID: 15180 RVA: 0x0010403B File Offset: 0x0010223B
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TabBaseTabCountDescr")]
		public int TabCount
		{
			get
			{
				return this.tabPageCount;
			}
		}

		// Token: 0x17000E80 RID: 3712
		// (get) Token: 0x06003B4D RID: 15181 RVA: 0x00104043 File Offset: 0x00102243
		[SRCategory("CatBehavior")]
		[SRDescription("TabControlTabsDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Editor("System.Windows.Forms.Design.TabPageCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[MergableProperty(false)]
		public TabControl.TabPageCollection TabPages
		{
			get
			{
				return this.tabCollection;
			}
		}

		// Token: 0x17000E81 RID: 3713
		// (get) Token: 0x06003B4E RID: 15182 RVA: 0x00013A28 File Offset: 0x00011C28
		// (set) Token: 0x06003B4F RID: 15183 RVA: 0x00024185 File Offset: 0x00022385
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

		// Token: 0x140002D5 RID: 725
		// (add) Token: 0x06003B50 RID: 15184 RVA: 0x00046771 File Offset: 0x00044971
		// (remove) Token: 0x06003B51 RID: 15185 RVA: 0x0004677A File Offset: 0x0004497A
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

		// Token: 0x140002D6 RID: 726
		// (add) Token: 0x06003B52 RID: 15186 RVA: 0x0010404B File Offset: 0x0010224B
		// (remove) Token: 0x06003B53 RID: 15187 RVA: 0x00104064 File Offset: 0x00102264
		[SRCategory("CatBehavior")]
		[SRDescription("drawItemEventDescr")]
		public event DrawItemEventHandler DrawItem
		{
			add
			{
				this.onDrawItem = (DrawItemEventHandler)Delegate.Combine(this.onDrawItem, value);
			}
			remove
			{
				this.onDrawItem = (DrawItemEventHandler)Delegate.Remove(this.onDrawItem, value);
			}
		}

		// Token: 0x140002D7 RID: 727
		// (add) Token: 0x06003B54 RID: 15188 RVA: 0x0010407D File Offset: 0x0010227D
		// (remove) Token: 0x06003B55 RID: 15189 RVA: 0x00104090 File Offset: 0x00102290
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnRightToLeftLayoutChangedDescr")]
		public event EventHandler RightToLeftLayoutChanged
		{
			add
			{
				base.Events.AddHandler(TabControl.EVENT_RIGHTTOLEFTLAYOUTCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(TabControl.EVENT_RIGHTTOLEFTLAYOUTCHANGED, value);
			}
		}

		// Token: 0x140002D8 RID: 728
		// (add) Token: 0x06003B56 RID: 15190 RVA: 0x001040A3 File Offset: 0x001022A3
		// (remove) Token: 0x06003B57 RID: 15191 RVA: 0x001040BC File Offset: 0x001022BC
		[SRCategory("CatBehavior")]
		[SRDescription("selectedIndexChangedEventDescr")]
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				this.onSelectedIndexChanged = (EventHandler)Delegate.Combine(this.onSelectedIndexChanged, value);
			}
			remove
			{
				this.onSelectedIndexChanged = (EventHandler)Delegate.Remove(this.onSelectedIndexChanged, value);
			}
		}

		// Token: 0x140002D9 RID: 729
		// (add) Token: 0x06003B58 RID: 15192 RVA: 0x001040D5 File Offset: 0x001022D5
		// (remove) Token: 0x06003B59 RID: 15193 RVA: 0x001040E8 File Offset: 0x001022E8
		[SRCategory("CatAction")]
		[SRDescription("TabControlSelectingEventDescr")]
		public event TabControlCancelEventHandler Selecting
		{
			add
			{
				base.Events.AddHandler(TabControl.EVENT_SELECTING, value);
			}
			remove
			{
				base.Events.RemoveHandler(TabControl.EVENT_SELECTING, value);
			}
		}

		// Token: 0x140002DA RID: 730
		// (add) Token: 0x06003B5A RID: 15194 RVA: 0x001040FB File Offset: 0x001022FB
		// (remove) Token: 0x06003B5B RID: 15195 RVA: 0x0010410E File Offset: 0x0010230E
		[SRCategory("CatAction")]
		[SRDescription("TabControlSelectedEventDescr")]
		public event TabControlEventHandler Selected
		{
			add
			{
				base.Events.AddHandler(TabControl.EVENT_SELECTED, value);
			}
			remove
			{
				base.Events.RemoveHandler(TabControl.EVENT_SELECTED, value);
			}
		}

		// Token: 0x140002DB RID: 731
		// (add) Token: 0x06003B5C RID: 15196 RVA: 0x00104121 File Offset: 0x00102321
		// (remove) Token: 0x06003B5D RID: 15197 RVA: 0x00104134 File Offset: 0x00102334
		[SRCategory("CatAction")]
		[SRDescription("TabControlDeselectingEventDescr")]
		public event TabControlCancelEventHandler Deselecting
		{
			add
			{
				base.Events.AddHandler(TabControl.EVENT_DESELECTING, value);
			}
			remove
			{
				base.Events.RemoveHandler(TabControl.EVENT_DESELECTING, value);
			}
		}

		// Token: 0x140002DC RID: 732
		// (add) Token: 0x06003B5E RID: 15198 RVA: 0x00104147 File Offset: 0x00102347
		// (remove) Token: 0x06003B5F RID: 15199 RVA: 0x0010415A File Offset: 0x0010235A
		[SRCategory("CatAction")]
		[SRDescription("TabControlDeselectedEventDescr")]
		public event TabControlEventHandler Deselected
		{
			add
			{
				base.Events.AddHandler(TabControl.EVENT_DESELECTED, value);
			}
			remove
			{
				base.Events.RemoveHandler(TabControl.EVENT_DESELECTED, value);
			}
		}

		// Token: 0x140002DD RID: 733
		// (add) Token: 0x06003B60 RID: 15200 RVA: 0x00013F87 File Offset: 0x00012187
		// (remove) Token: 0x06003B61 RID: 15201 RVA: 0x00013F90 File Offset: 0x00012190
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

		// Token: 0x06003B62 RID: 15202 RVA: 0x00104170 File Offset: 0x00102370
		internal int AddTabPage(TabPage tabPage, NativeMethods.TCITEM_T tcitem)
		{
			int num = this.AddNativeTabPage(tcitem);
			if (num >= 0)
			{
				this.Insert(num, tabPage);
			}
			return num;
		}

		// Token: 0x06003B63 RID: 15203 RVA: 0x00104194 File Offset: 0x00102394
		internal int AddNativeTabPage(NativeMethods.TCITEM_T tcitem)
		{
			int result = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.TCM_INSERTITEM, this.tabPageCount + 1, tcitem);
			UnsafeNativeMethods.PostMessage(new HandleRef(this, base.Handle), this.tabBaseReLayoutMessage, IntPtr.Zero, IntPtr.Zero);
			return result;
		}

		// Token: 0x06003B64 RID: 15204 RVA: 0x001041EC File Offset: 0x001023EC
		internal void ApplyItemSize()
		{
			if (base.IsHandleCreated && this.ShouldSerializeItemSize())
			{
				base.SendMessage(4905, 0, (int)NativeMethods.Util.MAKELPARAM(this.itemSize.Width, this.itemSize.Height));
			}
			this.cachedDisplayRect = Rectangle.Empty;
		}

		// Token: 0x06003B65 RID: 15205 RVA: 0x00104241 File Offset: 0x00102441
		internal void BeginUpdate()
		{
			base.BeginUpdateInternal();
		}

		// Token: 0x06003B66 RID: 15206 RVA: 0x00104249 File Offset: 0x00102449
		protected override Control.ControlCollection CreateControlsInstance()
		{
			return new TabControl.ControlCollection(this);
		}

		// Token: 0x06003B67 RID: 15207 RVA: 0x00104254 File Offset: 0x00102454
		protected override void CreateHandle()
		{
			if (!base.RecreatingHandle)
			{
				IntPtr userCookie = UnsafeNativeMethods.ThemingScope.Activate();
				try
				{
					SafeNativeMethods.InitCommonControlsEx(new NativeMethods.INITCOMMONCONTROLSEX
					{
						dwICC = 8
					});
				}
				finally
				{
					UnsafeNativeMethods.ThemingScope.Deactivate(userCookie);
				}
			}
			base.CreateHandle();
		}

		// Token: 0x06003B68 RID: 15208 RVA: 0x001042A4 File Offset: 0x001024A4
		private void DetachImageList(object sender, EventArgs e)
		{
			this.ImageList = null;
		}

		// Token: 0x06003B69 RID: 15209 RVA: 0x001042B0 File Offset: 0x001024B0
		public void DeselectTab(int index)
		{
			TabPage tabPage = this.GetTabPage(index);
			if (this.SelectedTab == tabPage)
			{
				if (0 <= index && index < this.TabPages.Count - 1)
				{
					this.SelectedTab = this.GetTabPage(++index);
					return;
				}
				this.SelectedTab = this.GetTabPage(0);
			}
		}

		// Token: 0x06003B6A RID: 15210 RVA: 0x00104304 File Offset: 0x00102504
		public void DeselectTab(TabPage tabPage)
		{
			if (tabPage == null)
			{
				throw new ArgumentNullException("tabPage");
			}
			int index = this.FindTabPage(tabPage);
			this.DeselectTab(index);
		}

		// Token: 0x06003B6B RID: 15211 RVA: 0x00104330 File Offset: 0x00102530
		public void DeselectTab(string tabPageName)
		{
			if (tabPageName == null)
			{
				throw new ArgumentNullException("tabPageName");
			}
			TabPage tabPage = this.TabPages[tabPageName];
			this.DeselectTab(tabPage);
		}

		// Token: 0x06003B6C RID: 15212 RVA: 0x0010435F File Offset: 0x0010255F
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.imageList != null)
			{
				this.imageList.Disposed -= this.DetachImageList;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06003B6D RID: 15213 RVA: 0x0010438A File Offset: 0x0010258A
		internal void EndUpdate()
		{
			this.EndUpdate(true);
		}

		// Token: 0x06003B6E RID: 15214 RVA: 0x00104393 File Offset: 0x00102593
		internal void EndUpdate(bool invalidate)
		{
			base.EndUpdateInternal(invalidate);
		}

		// Token: 0x06003B6F RID: 15215 RVA: 0x001043A0 File Offset: 0x001025A0
		internal int FindTabPage(TabPage tabPage)
		{
			if (this.tabPages != null)
			{
				for (int i = 0; i < this.tabPageCount; i++)
				{
					if (this.tabPages[i].Equals(tabPage))
					{
						return i;
					}
				}
			}
			return -1;
		}

		// Token: 0x06003B70 RID: 15216 RVA: 0x001043D9 File Offset: 0x001025D9
		public Control GetControl(int index)
		{
			return this.GetTabPage(index);
		}

		// Token: 0x06003B71 RID: 15217 RVA: 0x001043E4 File Offset: 0x001025E4
		internal TabPage GetTabPage(int index)
		{
			if (index < 0 || index >= this.tabPageCount)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			return this.tabPages[index];
		}

		// Token: 0x06003B72 RID: 15218 RVA: 0x00104438 File Offset: 0x00102638
		protected virtual object[] GetItems()
		{
			TabPage[] array = new TabPage[this.tabPageCount];
			if (this.tabPageCount > 0)
			{
				Array.Copy(this.tabPages, 0, array, 0, this.tabPageCount);
			}
			return array;
		}

		// Token: 0x06003B73 RID: 15219 RVA: 0x00104474 File Offset: 0x00102674
		protected virtual object[] GetItems(Type baseType)
		{
			object[] array = (object[])Array.CreateInstance(baseType, this.tabPageCount);
			if (this.tabPageCount > 0)
			{
				Array.Copy(this.tabPages, 0, array, 0, this.tabPageCount);
			}
			return array;
		}

		// Token: 0x06003B74 RID: 15220 RVA: 0x001044B1 File Offset: 0x001026B1
		internal TabPage[] GetTabPages()
		{
			return (TabPage[])this.GetItems();
		}

		// Token: 0x06003B75 RID: 15221 RVA: 0x001044C0 File Offset: 0x001026C0
		public Rectangle GetTabRect(int index)
		{
			if (index < 0 || (index >= this.tabPageCount && !this.tabControlState[8]))
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			this.tabControlState[8] = false;
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			if (!base.IsHandleCreated)
			{
				this.CreateHandle();
			}
			base.SendMessage(4874, index, ref rect);
			return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
		}

		// Token: 0x06003B76 RID: 15222 RVA: 0x00104569 File Offset: 0x00102769
		protected string GetToolTipText(object item)
		{
			return ((TabPage)item).ToolTipText;
		}

		// Token: 0x06003B77 RID: 15223 RVA: 0x00104576 File Offset: 0x00102776
		private void ImageListRecreateHandle(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(4867, 0, this.ImageList.Handle);
			}
		}

		// Token: 0x06003B78 RID: 15224 RVA: 0x00104598 File Offset: 0x00102798
		internal void Insert(int index, TabPage tabPage)
		{
			if (this.tabPages == null)
			{
				this.tabPages = new TabPage[4];
			}
			else if (this.tabPages.Length == this.tabPageCount)
			{
				TabPage[] destinationArray = new TabPage[this.tabPageCount * 2];
				Array.Copy(this.tabPages, 0, destinationArray, 0, this.tabPageCount);
				this.tabPages = destinationArray;
			}
			if (index < this.tabPageCount)
			{
				Array.Copy(this.tabPages, index, this.tabPages, index + 1, this.tabPageCount - index);
			}
			this.tabPages[index] = tabPage;
			this.tabPageCount++;
			this.cachedDisplayRect = Rectangle.Empty;
			this.ApplyItemSize();
			if (this.Appearance == TabAppearance.FlatButtons)
			{
				base.Invalidate();
			}
		}

		// Token: 0x06003B79 RID: 15225 RVA: 0x00104654 File Offset: 0x00102854
		private void InsertItem(int index, TabPage tabPage)
		{
			if (index < 0 || (this.tabPages != null && index > this.tabPageCount))
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (tabPage == null)
			{
				throw new ArgumentNullException("tabPage");
			}
			if (base.IsHandleCreated)
			{
				NativeMethods.TCITEM_T tcitem = tabPage.GetTCITEM();
				int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.TCM_INSERTITEM, index, tcitem);
				if (num >= 0)
				{
					this.Insert(num, tabPage);
				}
			}
		}

		// Token: 0x06003B7A RID: 15226 RVA: 0x001046F0 File Offset: 0x001028F0
		protected override bool IsInputKey(Keys keyData)
		{
			if ((keyData & Keys.Alt) == Keys.Alt)
			{
				return false;
			}
			Keys keys = keyData & Keys.KeyCode;
			return keys - Keys.Prior <= 3 || base.IsInputKey(keyData);
		}

		// Token: 0x06003B7B RID: 15227 RVA: 0x00104728 File Offset: 0x00102928
		protected override void OnHandleCreated(EventArgs e)
		{
			NativeWindow.AddWindowToIDTable(this, base.Handle);
			this.handleInTable = true;
			if (!this.padding.IsEmpty)
			{
				base.SendMessage(4907, 0, NativeMethods.Util.MAKELPARAM(this.padding.X, this.padding.Y));
			}
			base.OnHandleCreated(e);
			this.cachedDisplayRect = Rectangle.Empty;
			this.ApplyItemSize();
			if (this.imageList != null)
			{
				base.SendMessage(4867, 0, this.imageList.Handle);
			}
			if (this.ShowToolTips)
			{
				IntPtr intPtr = base.SendMessage(4909, 0, 0);
				if (intPtr != IntPtr.Zero)
				{
					SafeNativeMethods.SetWindowPos(new HandleRef(this, intPtr), NativeMethods.HWND_TOPMOST, 0, 0, 0, 0, 19);
				}
			}
			foreach (object obj in this.TabPages)
			{
				TabPage tabPage = (TabPage)obj;
				this.AddNativeTabPage(tabPage.GetTCITEM());
			}
			this.ResizePages();
			if (this.selectedIndex != -1)
			{
				try
				{
					this.tabControlState[16] = true;
					this.SelectedIndex = this.selectedIndex;
				}
				finally
				{
					this.tabControlState[16] = false;
				}
				this.selectedIndex = -1;
			}
			this.UpdateTabSelection(false);
		}

		// Token: 0x06003B7C RID: 15228 RVA: 0x00104898 File Offset: 0x00102A98
		protected override void OnHandleDestroyed(EventArgs e)
		{
			if (!base.Disposing)
			{
				this.selectedIndex = this.SelectedIndex;
			}
			if (this.handleInTable)
			{
				this.handleInTable = false;
				NativeWindow.RemoveWindowFromIDTable(base.Handle);
			}
			base.OnHandleDestroyed(e);
		}

		// Token: 0x06003B7D RID: 15229 RVA: 0x001048CF File Offset: 0x00102ACF
		protected virtual void OnDrawItem(DrawItemEventArgs e)
		{
			if (this.onDrawItem != null)
			{
				this.onDrawItem(this, e);
			}
		}

		// Token: 0x06003B7E RID: 15230 RVA: 0x001048E6 File Offset: 0x00102AE6
		protected override void OnEnter(EventArgs e)
		{
			base.OnEnter(e);
			if (this.SelectedTab != null)
			{
				this.SelectedTab.FireEnter(e);
			}
		}

		// Token: 0x06003B7F RID: 15231 RVA: 0x00104903 File Offset: 0x00102B03
		protected override void OnLeave(EventArgs e)
		{
			if (this.SelectedTab != null)
			{
				this.SelectedTab.FireLeave(e);
			}
			base.OnLeave(e);
		}

		// Token: 0x06003B80 RID: 15232 RVA: 0x00104920 File Offset: 0x00102B20
		protected override void OnKeyDown(KeyEventArgs ke)
		{
			if (ke.KeyCode == Keys.Tab && (ke.KeyData & Keys.Control) != Keys.None)
			{
				bool forward = (ke.KeyData & Keys.Shift) == Keys.None;
				this.SelectNextTab(ke, forward);
			}
			if (ke.KeyCode == Keys.Next && (ke.KeyData & Keys.Control) != Keys.None)
			{
				this.SelectNextTab(ke, true);
			}
			if (ke.KeyCode == Keys.Prior && (ke.KeyData & Keys.Control) != Keys.None)
			{
				this.SelectNextTab(ke, false);
			}
			base.OnKeyDown(ke);
		}

		// Token: 0x06003B81 RID: 15233 RVA: 0x001049A4 File Offset: 0x00102BA4
		internal override void OnParentHandleRecreated()
		{
			this.skipUpdateSize = true;
			try
			{
				base.OnParentHandleRecreated();
			}
			finally
			{
				this.skipUpdateSize = false;
			}
		}

		// Token: 0x06003B82 RID: 15234 RVA: 0x001049D8 File Offset: 0x00102BD8
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			this.cachedDisplayRect = Rectangle.Empty;
			this.UpdateTabSelection(false);
		}

		// Token: 0x06003B83 RID: 15235 RVA: 0x001049F4 File Offset: 0x00102BF4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
		{
			if (base.GetAnyDisposingInHierarchy())
			{
				return;
			}
			if (this.RightToLeft == RightToLeft.Yes)
			{
				base.RecreateHandle();
			}
			EventHandler eventHandler = base.Events[TabControl.EVENT_RIGHTTOLEFTLAYOUTCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06003B84 RID: 15236 RVA: 0x00104A3C File Offset: 0x00102C3C
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			int num = this.SelectedIndex;
			this.cachedDisplayRect = Rectangle.Empty;
			this.UpdateTabSelection(this.tabControlState[32]);
			this.tabControlState[32] = false;
			if (this.onSelectedIndexChanged != null)
			{
				this.onSelectedIndexChanged(this, e);
			}
		}

		// Token: 0x06003B85 RID: 15237 RVA: 0x00104A94 File Offset: 0x00102C94
		protected virtual void OnSelecting(TabControlCancelEventArgs e)
		{
			TabControlCancelEventHandler tabControlCancelEventHandler = (TabControlCancelEventHandler)base.Events[TabControl.EVENT_SELECTING];
			if (tabControlCancelEventHandler != null)
			{
				tabControlCancelEventHandler(this, e);
			}
		}

		// Token: 0x06003B86 RID: 15238 RVA: 0x00104AC4 File Offset: 0x00102CC4
		protected virtual void OnSelected(TabControlEventArgs e)
		{
			TabControlEventHandler tabControlEventHandler = (TabControlEventHandler)base.Events[TabControl.EVENT_SELECTED];
			if (tabControlEventHandler != null)
			{
				tabControlEventHandler(this, e);
			}
			if (this.SelectedTab != null)
			{
				this.SelectedTab.FireEnter(EventArgs.Empty);
			}
		}

		// Token: 0x06003B87 RID: 15239 RVA: 0x00104B0C File Offset: 0x00102D0C
		protected virtual void OnDeselecting(TabControlCancelEventArgs e)
		{
			TabControlCancelEventHandler tabControlCancelEventHandler = (TabControlCancelEventHandler)base.Events[TabControl.EVENT_DESELECTING];
			if (tabControlCancelEventHandler != null)
			{
				tabControlCancelEventHandler(this, e);
			}
		}

		// Token: 0x06003B88 RID: 15240 RVA: 0x00104B3C File Offset: 0x00102D3C
		protected virtual void OnDeselected(TabControlEventArgs e)
		{
			TabControlEventHandler tabControlEventHandler = (TabControlEventHandler)base.Events[TabControl.EVENT_DESELECTED];
			if (tabControlEventHandler != null)
			{
				tabControlEventHandler(this, e);
			}
			if (this.SelectedTab != null)
			{
				this.SelectedTab.FireLeave(EventArgs.Empty);
			}
		}

		// Token: 0x06003B89 RID: 15241 RVA: 0x00104B82 File Offset: 0x00102D82
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override bool ProcessKeyPreview(ref Message m)
		{
			return this.ProcessKeyEventArgs(ref m) || base.ProcessKeyPreview(ref m);
		}

		// Token: 0x06003B8A RID: 15242 RVA: 0x00104B98 File Offset: 0x00102D98
		internal void UpdateSize()
		{
			if (this.skipUpdateSize)
			{
				return;
			}
			this.BeginUpdate();
			Size size = base.Size;
			base.Size = new Size(size.Width + 1, size.Height);
			base.Size = size;
			this.EndUpdate();
		}

		// Token: 0x06003B8B RID: 15243 RVA: 0x00104BE3 File Offset: 0x00102DE3
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.cachedDisplayRect = Rectangle.Empty;
			this.UpdateSize();
		}

		// Token: 0x06003B8C RID: 15244 RVA: 0x00104C00 File Offset: 0x00102E00
		internal override void RecreateHandleCore()
		{
			TabPage[] array = this.GetTabPages();
			int num = (array.Length != 0 && this.SelectedIndex == -1) ? 0 : this.SelectedIndex;
			if (base.IsHandleCreated)
			{
				base.SendMessage(4873, 0, 0);
			}
			this.tabPages = null;
			this.tabPageCount = 0;
			base.RecreateHandleCore();
			for (int i = 0; i < array.Length; i++)
			{
				this.TabPages.Add(array[i]);
			}
			try
			{
				this.tabControlState[16] = true;
				this.SelectedIndex = num;
			}
			finally
			{
				this.tabControlState[16] = false;
			}
			this.UpdateSize();
		}

		// Token: 0x06003B8D RID: 15245 RVA: 0x00104CB0 File Offset: 0x00102EB0
		protected void RemoveAll()
		{
			base.Controls.Clear();
			base.SendMessage(4873, 0, 0);
			this.tabPages = null;
			this.tabPageCount = 0;
		}

		// Token: 0x06003B8E RID: 15246 RVA: 0x00104CDC File Offset: 0x00102EDC
		internal void RemoveTabPage(int index)
		{
			if (index < 0 || index >= this.tabPageCount)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			this.tabPageCount--;
			if (index < this.tabPageCount)
			{
				Array.Copy(this.tabPages, index + 1, this.tabPages, index, this.tabPageCount - index);
			}
			this.tabPages[this.tabPageCount] = null;
			if (base.IsHandleCreated)
			{
				base.SendMessage(4872, index, 0);
			}
			this.cachedDisplayRect = Rectangle.Empty;
		}

		// Token: 0x06003B8F RID: 15247 RVA: 0x00104D8B File Offset: 0x00102F8B
		private void ResetItemSize()
		{
			this.ItemSize = TabControl.DEFAULT_ITEMSIZE;
		}

		// Token: 0x06003B90 RID: 15248 RVA: 0x00104D98 File Offset: 0x00102F98
		private void ResetPadding()
		{
			this.Padding = TabControl.DEFAULT_PADDING;
		}

		// Token: 0x06003B91 RID: 15249 RVA: 0x00104DA8 File Offset: 0x00102FA8
		private void ResizePages()
		{
			Rectangle displayRectangle = this.DisplayRectangle;
			TabPage[] array = this.GetTabPages();
			for (int i = 0; i < array.Length; i++)
			{
				array[i].Bounds = displayRectangle;
			}
		}

		// Token: 0x06003B92 RID: 15250 RVA: 0x00104DDA File Offset: 0x00102FDA
		internal void SetToolTip(ToolTip toolTip, string controlToolTipText)
		{
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4910, new HandleRef(toolTip, toolTip.Handle), 0);
			this.controlTipText = controlToolTipText;
		}

		// Token: 0x06003B93 RID: 15251 RVA: 0x00104E08 File Offset: 0x00103008
		internal void SetTabPage(int index, TabPage tabPage, NativeMethods.TCITEM_T tcitem)
		{
			if (index < 0 || index >= this.tabPageCount)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (base.IsHandleCreated)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.TCM_SETITEM, index, tcitem);
			}
			if (base.DesignMode && base.IsHandleCreated)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4876, (IntPtr)index, IntPtr.Zero);
			}
			this.tabPages[index] = tabPage;
		}

		// Token: 0x06003B94 RID: 15252 RVA: 0x00104EB0 File Offset: 0x001030B0
		public void SelectTab(int index)
		{
			TabPage tabPage = this.GetTabPage(index);
			if (tabPage != null)
			{
				this.SelectedTab = tabPage;
			}
		}

		// Token: 0x06003B95 RID: 15253 RVA: 0x00104ED0 File Offset: 0x001030D0
		public void SelectTab(TabPage tabPage)
		{
			if (tabPage == null)
			{
				throw new ArgumentNullException("tabPage");
			}
			int index = this.FindTabPage(tabPage);
			this.SelectTab(index);
		}

		// Token: 0x06003B96 RID: 15254 RVA: 0x00104EFC File Offset: 0x001030FC
		public void SelectTab(string tabPageName)
		{
			if (tabPageName == null)
			{
				throw new ArgumentNullException("tabPageName");
			}
			TabPage tabPage = this.TabPages[tabPageName];
			this.SelectTab(tabPage);
		}

		// Token: 0x06003B97 RID: 15255 RVA: 0x00104F2C File Offset: 0x0010312C
		private void SelectNextTab(KeyEventArgs ke, bool forward)
		{
			bool focused = this.Focused;
			if (this.WmSelChanging())
			{
				this.tabControlState[32] = false;
				return;
			}
			if (base.ValidationCancelled)
			{
				this.tabControlState[32] = false;
				return;
			}
			int num = this.SelectedIndex;
			if (num != -1)
			{
				int tabCount = this.TabCount;
				if (forward)
				{
					num = (num + 1) % tabCount;
				}
				else
				{
					num = (num + tabCount - 1) % tabCount;
				}
				try
				{
					this.tabControlState[32] = true;
					this.tabControlState[64] = true;
					this.SelectedIndex = num;
					this.tabControlState[64] = !focused;
					this.WmSelChange();
				}
				finally
				{
					this.tabControlState[64] = false;
					ke.Handled = true;
				}
			}
		}

		// Token: 0x06003B98 RID: 15256 RVA: 0x00013062 File Offset: 0x00011262
		internal override bool ShouldPerformContainerValidation()
		{
			return true;
		}

		// Token: 0x06003B99 RID: 15257 RVA: 0x00104FF8 File Offset: 0x001031F8
		private bool ShouldSerializeItemSize()
		{
			return !this.itemSize.Equals(TabControl.DEFAULT_ITEMSIZE);
		}

		// Token: 0x06003B9A RID: 15258 RVA: 0x00105018 File Offset: 0x00103218
		private new bool ShouldSerializePadding()
		{
			return !this.padding.Equals(TabControl.DEFAULT_PADDING);
		}

		// Token: 0x06003B9B RID: 15259 RVA: 0x00105038 File Offset: 0x00103238
		public override string ToString()
		{
			string text = base.ToString();
			if (this.TabPages != null)
			{
				text = text + ", TabPages.Count: " + this.TabPages.Count.ToString(CultureInfo.CurrentCulture);
				if (this.TabPages.Count > 0)
				{
					text = text + ", TabPages[0]: " + this.TabPages[0].ToString();
				}
			}
			return text;
		}

		// Token: 0x06003B9C RID: 15260 RVA: 0x001050A4 File Offset: 0x001032A4
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void ScaleCore(float dx, float dy)
		{
			this.currentlyScaling = true;
			base.ScaleCore(dx, dy);
			this.currentlyScaling = false;
		}

		// Token: 0x06003B9D RID: 15261 RVA: 0x001050BC File Offset: 0x001032BC
		protected void UpdateTabSelection(bool updateFocus)
		{
			if (base.IsHandleCreated)
			{
				int num = this.SelectedIndex;
				TabPage[] array = this.GetTabPages();
				if (num != -1)
				{
					if (this.currentlyScaling)
					{
						array[num].SuspendLayout();
					}
					array[num].Bounds = this.DisplayRectangle;
					array[num].Invalidate();
					if (this.currentlyScaling)
					{
						array[num].ResumeLayout(false);
					}
					array[num].Visible = true;
					if (updateFocus && (!this.Focused || this.tabControlState[64]))
					{
						this.tabControlState[32] = false;
						bool flag = false;
						IntSecurity.ModifyFocus.Assert();
						try
						{
							flag = array[num].SelectNextControl(null, true, true, false, false);
						}
						finally
						{
							CodeAccessPermission.RevertAssert();
						}
						if (flag)
						{
							if (!base.ContainsFocus)
							{
								IContainerControl containerControl = base.GetContainerControlInternal();
								if (containerControl != null)
								{
									while (containerControl.ActiveControl is ContainerControl)
									{
										containerControl = (IContainerControl)containerControl.ActiveControl;
									}
									if (containerControl.ActiveControl != null)
									{
										containerControl.ActiveControl.FocusInternal();
									}
								}
							}
						}
						else
						{
							IContainerControl containerControlInternal = base.GetContainerControlInternal();
							if (containerControlInternal != null && !base.DesignMode)
							{
								if (containerControlInternal is ContainerControl)
								{
									((ContainerControl)containerControlInternal).SetActiveControlInternal(this);
								}
								else
								{
									IntSecurity.ModifyFocus.Assert();
									try
									{
										containerControlInternal.ActiveControl = this;
									}
									finally
									{
										CodeAccessPermission.RevertAssert();
									}
								}
							}
						}
					}
				}
				for (int i = 0; i < array.Length; i++)
				{
					if (i != this.SelectedIndex)
					{
						array[i].Visible = false;
					}
				}
			}
		}

		// Token: 0x06003B9E RID: 15262 RVA: 0x0010524C File Offset: 0x0010344C
		protected override void OnStyleChanged(EventArgs e)
		{
			base.OnStyleChanged(e);
			this.cachedDisplayRect = Rectangle.Empty;
			this.UpdateTabSelection(false);
		}

		// Token: 0x06003B9F RID: 15263 RVA: 0x00105268 File Offset: 0x00103468
		internal void UpdateTab(TabPage tabPage)
		{
			int index = this.FindTabPage(tabPage);
			this.SetTabPage(index, tabPage, tabPage.GetTCITEM());
			this.cachedDisplayRect = Rectangle.Empty;
			this.UpdateTabSelection(false);
		}

		// Token: 0x06003BA0 RID: 15264 RVA: 0x001052A0 File Offset: 0x001034A0
		private void WmNeedText(ref Message m)
		{
			NativeMethods.TOOLTIPTEXT tooltiptext = (NativeMethods.TOOLTIPTEXT)m.GetLParam(typeof(NativeMethods.TOOLTIPTEXT));
			int index = (int)tooltiptext.hdr.idFrom;
			string toolTipText = this.GetToolTipText(this.GetTabPage(index));
			if (!string.IsNullOrEmpty(toolTipText))
			{
				tooltiptext.lpszText = toolTipText;
			}
			else
			{
				tooltiptext.lpszText = this.controlTipText;
			}
			tooltiptext.hinst = IntPtr.Zero;
			if (this.RightToLeft == RightToLeft.Yes)
			{
				tooltiptext.uFlags |= 4;
			}
			Marshal.StructureToPtr(tooltiptext, m.LParam, false);
		}

		// Token: 0x06003BA1 RID: 15265 RVA: 0x00105330 File Offset: 0x00103530
		private void WmReflectDrawItem(ref Message m)
		{
			NativeMethods.DRAWITEMSTRUCT drawitemstruct = (NativeMethods.DRAWITEMSTRUCT)m.GetLParam(typeof(NativeMethods.DRAWITEMSTRUCT));
			IntPtr intPtr = Control.SetUpPalette(drawitemstruct.hDC, false, false);
			using (Graphics graphics = Graphics.FromHdcInternal(drawitemstruct.hDC))
			{
				this.OnDrawItem(new DrawItemEventArgs(graphics, this.Font, Rectangle.FromLTRB(drawitemstruct.rcItem.left, drawitemstruct.rcItem.top, drawitemstruct.rcItem.right, drawitemstruct.rcItem.bottom), drawitemstruct.itemID, (DrawItemState)drawitemstruct.itemState));
			}
			if (intPtr != IntPtr.Zero)
			{
				SafeNativeMethods.SelectPalette(new HandleRef(null, drawitemstruct.hDC), new HandleRef(null, intPtr), 0);
			}
			m.Result = (IntPtr)1;
		}

		// Token: 0x06003BA2 RID: 15266 RVA: 0x0010540C File Offset: 0x0010360C
		private bool WmSelChange()
		{
			TabControlCancelEventArgs tabControlCancelEventArgs = new TabControlCancelEventArgs(this.SelectedTab, this.SelectedIndex, false, TabControlAction.Selecting);
			this.OnSelecting(tabControlCancelEventArgs);
			if (!tabControlCancelEventArgs.Cancel)
			{
				this.OnSelected(new TabControlEventArgs(this.SelectedTab, this.SelectedIndex, TabControlAction.Selected));
				this.OnSelectedIndexChanged(EventArgs.Empty);
			}
			else
			{
				base.SendMessage(4876, this.lastSelection, 0);
				this.UpdateTabSelection(true);
			}
			return tabControlCancelEventArgs.Cancel;
		}

		// Token: 0x06003BA3 RID: 15267 RVA: 0x00105484 File Offset: 0x00103684
		private bool WmSelChanging()
		{
			IContainerControl containerControlInternal = base.GetContainerControlInternal();
			if (containerControlInternal != null && !base.DesignMode)
			{
				if (containerControlInternal is ContainerControl)
				{
					((ContainerControl)containerControlInternal).SetActiveControlInternal(this);
				}
				else
				{
					IntSecurity.ModifyFocus.Assert();
					try
					{
						containerControlInternal.ActiveControl = this;
					}
					finally
					{
						CodeAccessPermission.RevertAssert();
					}
				}
			}
			this.lastSelection = this.SelectedIndex;
			TabControlCancelEventArgs tabControlCancelEventArgs = new TabControlCancelEventArgs(this.SelectedTab, this.SelectedIndex, false, TabControlAction.Deselecting);
			this.OnDeselecting(tabControlCancelEventArgs);
			if (!tabControlCancelEventArgs.Cancel)
			{
				this.OnDeselected(new TabControlEventArgs(this.SelectedTab, this.SelectedIndex, TabControlAction.Deselected));
			}
			return tabControlCancelEventArgs.Cancel;
		}

		// Token: 0x06003BA4 RID: 15268 RVA: 0x00105530 File Offset: 0x00103730
		private void WmTabBaseReLayout(ref Message m)
		{
			this.BeginUpdate();
			this.cachedDisplayRect = Rectangle.Empty;
			this.UpdateTabSelection(false);
			this.EndUpdate();
			base.Invalidate(true);
			NativeMethods.MSG msg = default(NativeMethods.MSG);
			IntPtr handle = base.Handle;
			while (UnsafeNativeMethods.PeekMessage(ref msg, new HandleRef(this, handle), this.tabBaseReLayoutMessage, this.tabBaseReLayoutMessage, 1))
			{
			}
		}

		// Token: 0x06003BA5 RID: 15269 RVA: 0x00105590 File Offset: 0x00103790
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 8235)
			{
				if (msg != 78)
				{
					if (msg != 8235)
					{
						goto IL_161;
					}
					this.WmReflectDrawItem(ref m);
					goto IL_161;
				}
			}
			else
			{
				if (msg == 8236)
				{
					goto IL_161;
				}
				if (msg != 8270)
				{
					goto IL_161;
				}
			}
			NativeMethods.NMHDR nmhdr = (NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR));
			int code = nmhdr.code;
			if (code <= -551)
			{
				if (code != -552)
				{
					if (code == -551)
					{
						if (this.WmSelChange())
						{
							m.Result = (IntPtr)1;
							this.tabControlState[32] = false;
							return;
						}
						this.tabControlState[32] = true;
					}
				}
				else
				{
					if (this.WmSelChanging())
					{
						m.Result = (IntPtr)1;
						this.tabControlState[32] = false;
						return;
					}
					if (base.ValidationCancelled)
					{
						m.Result = (IntPtr)1;
						this.tabControlState[32] = false;
						return;
					}
					this.tabControlState[32] = true;
				}
			}
			else if (code == -530 || code == -520)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(nmhdr, nmhdr.hwndFrom), 1048, 0, SystemInformation.MaxWindowTrackSize.Width);
				this.WmNeedText(ref m);
				m.Result = (IntPtr)1;
				return;
			}
			IL_161:
			if (m.Msg == this.tabBaseReLayoutMessage)
			{
				this.WmTabBaseReLayout(ref m);
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x0400234F RID: 9039
		private static readonly Size DEFAULT_ITEMSIZE = Size.Empty;

		// Token: 0x04002350 RID: 9040
		private static readonly Point DEFAULT_PADDING = new Point(6, 3);

		// Token: 0x04002351 RID: 9041
		private TabControl.TabPageCollection tabCollection;

		// Token: 0x04002352 RID: 9042
		private TabAlignment alignment;

		// Token: 0x04002353 RID: 9043
		private TabDrawMode drawMode;

		// Token: 0x04002354 RID: 9044
		private ImageList imageList;

		// Token: 0x04002355 RID: 9045
		private Size itemSize = TabControl.DEFAULT_ITEMSIZE;

		// Token: 0x04002356 RID: 9046
		private Point padding = TabControl.DEFAULT_PADDING;

		// Token: 0x04002357 RID: 9047
		private TabSizeMode sizeMode;

		// Token: 0x04002358 RID: 9048
		private TabAppearance appearance;

		// Token: 0x04002359 RID: 9049
		private Rectangle cachedDisplayRect = Rectangle.Empty;

		// Token: 0x0400235A RID: 9050
		private bool currentlyScaling;

		// Token: 0x0400235B RID: 9051
		private int selectedIndex = -1;

		// Token: 0x0400235C RID: 9052
		private Size cachedSize = Size.Empty;

		// Token: 0x0400235D RID: 9053
		private string controlTipText = string.Empty;

		// Token: 0x0400235E RID: 9054
		private bool handleInTable;

		// Token: 0x0400235F RID: 9055
		private EventHandler onSelectedIndexChanged;

		// Token: 0x04002360 RID: 9056
		private DrawItemEventHandler onDrawItem;

		// Token: 0x04002361 RID: 9057
		private static readonly object EVENT_DESELECTING = new object();

		// Token: 0x04002362 RID: 9058
		private static readonly object EVENT_DESELECTED = new object();

		// Token: 0x04002363 RID: 9059
		private static readonly object EVENT_SELECTING = new object();

		// Token: 0x04002364 RID: 9060
		private static readonly object EVENT_SELECTED = new object();

		// Token: 0x04002365 RID: 9061
		private static readonly object EVENT_RIGHTTOLEFTLAYOUTCHANGED = new object();

		// Token: 0x04002366 RID: 9062
		private const int TABCONTROLSTATE_hotTrack = 1;

		// Token: 0x04002367 RID: 9063
		private const int TABCONTROLSTATE_multiline = 2;

		// Token: 0x04002368 RID: 9064
		private const int TABCONTROLSTATE_showToolTips = 4;

		// Token: 0x04002369 RID: 9065
		private const int TABCONTROLSTATE_getTabRectfromItemSize = 8;

		// Token: 0x0400236A RID: 9066
		private const int TABCONTROLSTATE_fromCreateHandles = 16;

		// Token: 0x0400236B RID: 9067
		private const int TABCONTROLSTATE_UISelection = 32;

		// Token: 0x0400236C RID: 9068
		private const int TABCONTROLSTATE_selectFirstControl = 64;

		// Token: 0x0400236D RID: 9069
		private const int TABCONTROLSTATE_insertingItem = 128;

		// Token: 0x0400236E RID: 9070
		private const int TABCONTROLSTATE_autoSize = 256;

		// Token: 0x0400236F RID: 9071
		private BitVector32 tabControlState;

		// Token: 0x04002370 RID: 9072
		private readonly int tabBaseReLayoutMessage = SafeNativeMethods.RegisterWindowMessage(Application.WindowMessagesVersion + "_TabBaseReLayout");

		// Token: 0x04002371 RID: 9073
		private TabPage[] tabPages;

		// Token: 0x04002372 RID: 9074
		private int tabPageCount;

		// Token: 0x04002373 RID: 9075
		private int lastSelection;

		// Token: 0x04002374 RID: 9076
		private bool rightToLeftLayout;

		// Token: 0x04002375 RID: 9077
		private bool skipUpdateSize;

		// Token: 0x020007EE RID: 2030
		public class TabPageCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06006E37 RID: 28215 RVA: 0x001945B8 File Offset: 0x001927B8
			public TabPageCollection(TabControl owner)
			{
				if (owner == null)
				{
					throw new ArgumentNullException("owner");
				}
				this.owner = owner;
			}

			// Token: 0x17001813 RID: 6163
			public virtual TabPage this[int index]
			{
				get
				{
					return this.owner.GetTabPage(index);
				}
				set
				{
					this.owner.SetTabPage(index, value, value.GetTCITEM());
				}
			}

			// Token: 0x17001814 RID: 6164
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					if (value is TabPage)
					{
						this[index] = (TabPage)value;
						return;
					}
					throw new ArgumentException("value");
				}
			}

			// Token: 0x17001815 RID: 6165
			public virtual TabPage this[string key]
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

			// Token: 0x17001816 RID: 6166
			// (get) Token: 0x06006E3D RID: 28221 RVA: 0x0019465D File Offset: 0x0019285D
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.owner.tabPageCount;
				}
			}

			// Token: 0x17001817 RID: 6167
			// (get) Token: 0x06006E3E RID: 28222 RVA: 0x00006C59 File Offset: 0x00004E59
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17001818 RID: 6168
			// (get) Token: 0x06006E3F RID: 28223 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001819 RID: 6169
			// (get) Token: 0x06006E40 RID: 28224 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700181A RID: 6170
			// (get) Token: 0x06006E41 RID: 28225 RVA: 0x00011A20 File Offset: 0x0000FC20
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06006E42 RID: 28226 RVA: 0x0019466A File Offset: 0x0019286A
			public void Add(TabPage value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.owner.Controls.Add(value);
			}

			// Token: 0x06006E43 RID: 28227 RVA: 0x0019468B File Offset: 0x0019288B
			int IList.Add(object value)
			{
				if (value is TabPage)
				{
					this.Add((TabPage)value);
					return this.IndexOf((TabPage)value);
				}
				throw new ArgumentException("value");
			}

			// Token: 0x06006E44 RID: 28228 RVA: 0x001946B8 File Offset: 0x001928B8
			public void Add(string text)
			{
				this.Add(new TabPage
				{
					Text = text
				});
			}

			// Token: 0x06006E45 RID: 28229 RVA: 0x001946DC File Offset: 0x001928DC
			public void Add(string key, string text)
			{
				this.Add(new TabPage
				{
					Name = key,
					Text = text
				});
			}

			// Token: 0x06006E46 RID: 28230 RVA: 0x00194704 File Offset: 0x00192904
			public void Add(string key, string text, int imageIndex)
			{
				this.Add(new TabPage
				{
					Name = key,
					Text = text,
					ImageIndex = imageIndex
				});
			}

			// Token: 0x06006E47 RID: 28231 RVA: 0x00194734 File Offset: 0x00192934
			public void Add(string key, string text, string imageKey)
			{
				this.Add(new TabPage
				{
					Name = key,
					Text = text,
					ImageKey = imageKey
				});
			}

			// Token: 0x06006E48 RID: 28232 RVA: 0x00194764 File Offset: 0x00192964
			public void AddRange(TabPage[] pages)
			{
				if (pages == null)
				{
					throw new ArgumentNullException("pages");
				}
				foreach (TabPage value in pages)
				{
					this.Add(value);
				}
			}

			// Token: 0x06006E49 RID: 28233 RVA: 0x0019479A File Offset: 0x0019299A
			public bool Contains(TabPage page)
			{
				if (page == null)
				{
					throw new ArgumentNullException("value");
				}
				return this.IndexOf(page) != -1;
			}

			// Token: 0x06006E4A RID: 28234 RVA: 0x001947B7 File Offset: 0x001929B7
			bool IList.Contains(object page)
			{
				return page is TabPage && this.Contains((TabPage)page);
			}

			// Token: 0x06006E4B RID: 28235 RVA: 0x001947CF File Offset: 0x001929CF
			public virtual bool ContainsKey(string key)
			{
				return this.IsValidIndex(this.IndexOfKey(key));
			}

			// Token: 0x06006E4C RID: 28236 RVA: 0x001947E0 File Offset: 0x001929E0
			public int IndexOf(TabPage page)
			{
				if (page == null)
				{
					throw new ArgumentNullException("value");
				}
				for (int i = 0; i < this.Count; i++)
				{
					if (this[i] == page)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06006E4D RID: 28237 RVA: 0x00194819 File Offset: 0x00192A19
			int IList.IndexOf(object page)
			{
				if (page is TabPage)
				{
					return this.IndexOf((TabPage)page);
				}
				return -1;
			}

			// Token: 0x06006E4E RID: 28238 RVA: 0x00194834 File Offset: 0x00192A34
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

			// Token: 0x06006E4F RID: 28239 RVA: 0x001948B4 File Offset: 0x00192AB4
			public void Insert(int index, TabPage tabPage)
			{
				this.owner.InsertItem(index, tabPage);
				try
				{
					this.owner.InsertingItem = true;
					this.owner.Controls.Add(tabPage);
				}
				finally
				{
					this.owner.InsertingItem = false;
				}
				this.owner.Controls.SetChildIndex(tabPage, index);
			}

			// Token: 0x06006E50 RID: 28240 RVA: 0x0019491C File Offset: 0x00192B1C
			void IList.Insert(int index, object tabPage)
			{
				if (tabPage is TabPage)
				{
					this.Insert(index, (TabPage)tabPage);
					return;
				}
				throw new ArgumentException("tabPage");
			}

			// Token: 0x06006E51 RID: 28241 RVA: 0x00194940 File Offset: 0x00192B40
			public void Insert(int index, string text)
			{
				this.Insert(index, new TabPage
				{
					Text = text
				});
			}

			// Token: 0x06006E52 RID: 28242 RVA: 0x00194964 File Offset: 0x00192B64
			public void Insert(int index, string key, string text)
			{
				this.Insert(index, new TabPage
				{
					Name = key,
					Text = text
				});
			}

			// Token: 0x06006E53 RID: 28243 RVA: 0x00194990 File Offset: 0x00192B90
			public void Insert(int index, string key, string text, int imageIndex)
			{
				TabPage tabPage = new TabPage();
				tabPage.Name = key;
				tabPage.Text = text;
				this.Insert(index, tabPage);
				tabPage.ImageIndex = imageIndex;
			}

			// Token: 0x06006E54 RID: 28244 RVA: 0x001949C4 File Offset: 0x00192BC4
			public void Insert(int index, string key, string text, string imageKey)
			{
				TabPage tabPage = new TabPage();
				tabPage.Name = key;
				tabPage.Text = text;
				this.Insert(index, tabPage);
				tabPage.ImageKey = imageKey;
			}

			// Token: 0x06006E55 RID: 28245 RVA: 0x001949F5 File Offset: 0x00192BF5
			private bool IsValidIndex(int index)
			{
				return index >= 0 && index < this.Count;
			}

			// Token: 0x06006E56 RID: 28246 RVA: 0x00194A06 File Offset: 0x00192C06
			public virtual void Clear()
			{
				this.owner.RemoveAll();
			}

			// Token: 0x06006E57 RID: 28247 RVA: 0x00194A13 File Offset: 0x00192C13
			void ICollection.CopyTo(Array dest, int index)
			{
				if (this.Count > 0)
				{
					Array.Copy(this.owner.GetTabPages(), 0, dest, index, this.Count);
				}
			}

			// Token: 0x06006E58 RID: 28248 RVA: 0x00194A38 File Offset: 0x00192C38
			public IEnumerator GetEnumerator()
			{
				TabPage[] tabPages = this.owner.GetTabPages();
				if (tabPages != null)
				{
					return tabPages.GetEnumerator();
				}
				return new TabPage[0].GetEnumerator();
			}

			// Token: 0x06006E59 RID: 28249 RVA: 0x00194A66 File Offset: 0x00192C66
			public void Remove(TabPage value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				this.owner.Controls.Remove(value);
			}

			// Token: 0x06006E5A RID: 28250 RVA: 0x00194A87 File Offset: 0x00192C87
			void IList.Remove(object value)
			{
				if (value is TabPage)
				{
					this.Remove((TabPage)value);
				}
			}

			// Token: 0x06006E5B RID: 28251 RVA: 0x00194A9D File Offset: 0x00192C9D
			public void RemoveAt(int index)
			{
				this.owner.Controls.RemoveAt(index);
			}

			// Token: 0x06006E5C RID: 28252 RVA: 0x00194AB0 File Offset: 0x00192CB0
			public virtual void RemoveByKey(string key)
			{
				int index = this.IndexOfKey(key);
				if (this.IsValidIndex(index))
				{
					this.RemoveAt(index);
				}
			}

			// Token: 0x040042D7 RID: 17111
			private TabControl owner;

			// Token: 0x040042D8 RID: 17112
			private int lastAccessedIndex = -1;
		}

		// Token: 0x020007EF RID: 2031
		[ComVisible(false)]
		public new class ControlCollection : Control.ControlCollection
		{
			// Token: 0x06006E5D RID: 28253 RVA: 0x00194AD5 File Offset: 0x00192CD5
			public ControlCollection(TabControl owner) : base(owner)
			{
				this.owner = owner;
			}

			// Token: 0x06006E5E RID: 28254 RVA: 0x00194AE8 File Offset: 0x00192CE8
			public override void Add(Control value)
			{
				if (!(value is TabPage))
				{
					throw new ArgumentException(SR.GetString("TabControlInvalidTabPageType", new object[]
					{
						value.GetType().Name
					}));
				}
				TabPage tabPage = (TabPage)value;
				if (!this.owner.InsertingItem)
				{
					if (this.owner.IsHandleCreated)
					{
						this.owner.AddTabPage(tabPage, tabPage.GetTCITEM());
					}
					else
					{
						this.owner.Insert(this.owner.TabCount, tabPage);
					}
				}
				base.Add(tabPage);
				tabPage.Visible = false;
				if (this.owner.IsHandleCreated)
				{
					tabPage.Bounds = this.owner.DisplayRectangle;
				}
				ISite site = this.owner.Site;
				if (site != null && tabPage.Site == null)
				{
					IContainer container = site.Container;
					if (container != null)
					{
						container.Add(tabPage);
					}
				}
				this.owner.ApplyItemSize();
				this.owner.UpdateTabSelection(false);
			}

			// Token: 0x06006E5F RID: 28255 RVA: 0x00194BDC File Offset: 0x00192DDC
			public override void Remove(Control value)
			{
				base.Remove(value);
				if (!(value is TabPage))
				{
					return;
				}
				int num = this.owner.FindTabPage((TabPage)value);
				int selectedIndex = this.owner.SelectedIndex;
				if (num != -1)
				{
					this.owner.RemoveTabPage(num);
					if (num == selectedIndex)
					{
						this.owner.SelectedIndex = 0;
					}
				}
				this.owner.UpdateTabSelection(false);
			}

			// Token: 0x040042D9 RID: 17113
			private TabControl owner;
		}
	}
}

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms.Layout;
using System.Windows.Forms.VisualStyles;
using Accessibility;

namespace System.Windows.Forms
{
	// Token: 0x020002CD RID: 717
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Designer("System.Windows.Forms.Design.ListBoxDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultEvent("SelectedIndexChanged")]
	[DefaultProperty("Items")]
	[DefaultBindingProperty("SelectedValue")]
	[SRDescription("DescriptionListBox")]
	public class ListBox : ListControl
	{
		// Token: 0x06002BDD RID: 11229 RVA: 0x000C59D8 File Offset: 0x000C3BD8
		public ListBox()
		{
			base.SetStyle(ControlStyles.UserPaint | ControlStyles.StandardClick | ControlStyles.UseTextForAccessibility, false);
			base.SetState2(2048, true);
			base.SetBounds(0, 0, 120, 96);
			this.requestedHeight = base.Height;
			this.PrepareForDrawing();
		}

		// Token: 0x06002BDE RID: 11230 RVA: 0x000C5A68 File Offset: 0x000C3C68
		protected override void RescaleConstantsForDpi(int deviceDpiOld, int deviceDpiNew)
		{
			base.RescaleConstantsForDpi(deviceDpiOld, deviceDpiNew);
			this.PrepareForDrawing();
		}

		// Token: 0x06002BDF RID: 11231 RVA: 0x000C5A78 File Offset: 0x000C3C78
		private void PrepareForDrawing()
		{
			if (DpiHelper.EnableCheckedListBoxHighDpiImprovements)
			{
				this.scaledListItemStartPosition = base.LogicalToDeviceUnits(1);
				this.scaledListItemBordersHeight = 2 * base.LogicalToDeviceUnits(1);
				this.scaledListItemPaddingBuffer = base.LogicalToDeviceUnits(3);
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x06002BE0 RID: 11232 RVA: 0x00027F43 File Offset: 0x00026143
		// (set) Token: 0x06002BE1 RID: 11233 RVA: 0x00012F98 File Offset: 0x00011198
		public override Color BackColor
		{
			get
			{
				if (this.ShouldSerializeBackColor())
				{
					return base.BackColor;
				}
				return SystemColors.Window;
			}
			set
			{
				base.BackColor = value;
			}
		}

		// Token: 0x17000A48 RID: 2632
		// (get) Token: 0x06002BE2 RID: 11234 RVA: 0x00011A90 File Offset: 0x0000FC90
		// (set) Token: 0x06002BE3 RID: 11235 RVA: 0x00011A98 File Offset: 0x0000FC98
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

		// Token: 0x140001F6 RID: 502
		// (add) Token: 0x06002BE4 RID: 11236 RVA: 0x00011AA1 File Offset: 0x0000FCA1
		// (remove) Token: 0x06002BE5 RID: 11237 RVA: 0x00011AAA File Offset: 0x0000FCAA
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

		// Token: 0x17000A49 RID: 2633
		// (get) Token: 0x06002BE6 RID: 11238 RVA: 0x00011AB3 File Offset: 0x0000FCB3
		// (set) Token: 0x06002BE7 RID: 11239 RVA: 0x00011ABB File Offset: 0x0000FCBB
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

		// Token: 0x140001F7 RID: 503
		// (add) Token: 0x06002BE8 RID: 11240 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		// (remove) Token: 0x06002BE9 RID: 11241 RVA: 0x00011ACD File Offset: 0x0000FCCD
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

		// Token: 0x17000A4A RID: 2634
		// (get) Token: 0x06002BEA RID: 11242 RVA: 0x000C5AAA File Offset: 0x000C3CAA
		// (set) Token: 0x06002BEB RID: 11243 RVA: 0x000C5AB4 File Offset: 0x000C3CB4
		[SRCategory("CatAppearance")]
		[DefaultValue(BorderStyle.Fixed3D)]
		[DispId(-504)]
		[SRDescription("ListBoxBorderDescr")]
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
				if (value != this.borderStyle)
				{
					this.borderStyle = value;
					base.RecreateHandle();
					this.integralHeightAdjust = true;
					try
					{
						base.Height = this.requestedHeight;
					}
					finally
					{
						this.integralHeightAdjust = false;
					}
				}
			}
		}

		// Token: 0x17000A4B RID: 2635
		// (get) Token: 0x06002BEC RID: 11244 RVA: 0x000C5B2C File Offset: 0x000C3D2C
		// (set) Token: 0x06002BED RID: 11245 RVA: 0x000C5B34 File Offset: 0x000C3D34
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[DefaultValue(0)]
		[SRDescription("ListBoxColumnWidthDescr")]
		public int ColumnWidth
		{
			get
			{
				return this.columnWidth;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"value",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.columnWidth != value)
				{
					this.columnWidth = value;
					if (this.columnWidth == 0)
					{
						base.RecreateHandle();
						return;
					}
					if (base.IsHandleCreated)
					{
						base.SendMessage(405, this.columnWidth, 0);
					}
				}
			}
		}

		// Token: 0x17000A4C RID: 2636
		// (get) Token: 0x06002BEE RID: 11246 RVA: 0x000C5BC0 File Offset: 0x000C3DC0
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "LISTBOX";
				createParams.Style |= 2097217;
				if (this.scrollAlwaysVisible)
				{
					createParams.Style |= 4096;
				}
				if (!this.integralHeight)
				{
					createParams.Style |= 256;
				}
				if (this.useTabStops)
				{
					createParams.Style |= 128;
				}
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
				if (this.multiColumn)
				{
					createParams.Style |= 1049088;
				}
				else if (this.horizontalScrollbar)
				{
					createParams.Style |= 1048576;
				}
				switch (this.selectionMode)
				{
				case SelectionMode.None:
					createParams.Style |= 16384;
					break;
				case SelectionMode.MultiSimple:
					createParams.Style |= 8;
					break;
				case SelectionMode.MultiExtended:
					createParams.Style |= 2048;
					break;
				}
				switch (this.drawMode)
				{
				case DrawMode.OwnerDrawFixed:
					createParams.Style |= 16;
					break;
				case DrawMode.OwnerDrawVariable:
					createParams.Style |= 32;
					break;
				}
				return createParams;
			}
		}

		// Token: 0x17000A4D RID: 2637
		// (get) Token: 0x06002BEF RID: 11247 RVA: 0x000C5D3B File Offset: 0x000C3F3B
		// (set) Token: 0x06002BF0 RID: 11248 RVA: 0x000C5D43 File Offset: 0x000C3F43
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[Browsable(false)]
		public bool UseCustomTabOffsets
		{
			get
			{
				return this.useCustomTabOffsets;
			}
			set
			{
				if (this.useCustomTabOffsets != value)
				{
					this.useCustomTabOffsets = value;
					base.RecreateHandle();
				}
			}
		}

		// Token: 0x17000A4E RID: 2638
		// (get) Token: 0x06002BF1 RID: 11249 RVA: 0x000C5D5B File Offset: 0x000C3F5B
		protected override Size DefaultSize
		{
			get
			{
				return new Size(120, 96);
			}
		}

		// Token: 0x17000A4F RID: 2639
		// (get) Token: 0x06002BF2 RID: 11250 RVA: 0x000C5D66 File Offset: 0x000C3F66
		// (set) Token: 0x06002BF3 RID: 11251 RVA: 0x000C5D70 File Offset: 0x000C3F70
		[SRCategory("CatBehavior")]
		[DefaultValue(DrawMode.Normal)]
		[SRDescription("ListBoxDrawModeDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public virtual DrawMode DrawMode
		{
			get
			{
				return this.drawMode;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(DrawMode));
				}
				if (this.drawMode != value)
				{
					if (this.MultiColumn && value == DrawMode.OwnerDrawVariable)
					{
						throw new ArgumentException(SR.GetString("ListBoxVarHeightMultiCol"), "value");
					}
					this.drawMode = value;
					base.RecreateHandle();
					if (this.drawMode == DrawMode.OwnerDrawVariable)
					{
						LayoutTransaction.DoLayoutIf(this.AutoSize, this.ParentInternal, this, PropertyNames.DrawMode);
					}
				}
			}
		}

		// Token: 0x17000A50 RID: 2640
		// (get) Token: 0x06002BF4 RID: 11252 RVA: 0x000C5DFA File Offset: 0x000C3FFA
		internal int FocusedIndex
		{
			get
			{
				if (base.IsHandleCreated)
				{
					return (int)((long)base.SendMessage(415, 0, 0));
				}
				return -1;
			}
		}

		// Token: 0x17000A51 RID: 2641
		// (get) Token: 0x06002BF5 RID: 11253 RVA: 0x0001A272 File Offset: 0x00018472
		// (set) Token: 0x06002BF6 RID: 11254 RVA: 0x000C5E19 File Offset: 0x000C4019
		public override Font Font
		{
			get
			{
				return base.Font;
			}
			set
			{
				base.Font = value;
				if (!this.integralHeight)
				{
					this.RefreshItems();
				}
			}
		}

		// Token: 0x17000A52 RID: 2642
		// (get) Token: 0x06002BF7 RID: 11255 RVA: 0x00013222 File Offset: 0x00011422
		// (set) Token: 0x06002BF8 RID: 11256 RVA: 0x00013238 File Offset: 0x00011438
		public override Color ForeColor
		{
			get
			{
				if (this.ShouldSerializeForeColor())
				{
					return base.ForeColor;
				}
				return SystemColors.WindowText;
			}
			set
			{
				base.ForeColor = value;
			}
		}

		// Token: 0x17000A53 RID: 2643
		// (get) Token: 0x06002BF9 RID: 11257 RVA: 0x000C5E30 File Offset: 0x000C4030
		// (set) Token: 0x06002BFA RID: 11258 RVA: 0x000C5E38 File Offset: 0x000C4038
		[SRCategory("CatBehavior")]
		[DefaultValue(0)]
		[Localizable(true)]
		[SRDescription("ListBoxHorizontalExtentDescr")]
		public int HorizontalExtent
		{
			get
			{
				return this.horizontalExtent;
			}
			set
			{
				if (value != this.horizontalExtent)
				{
					this.horizontalExtent = value;
					this.UpdateHorizontalExtent();
				}
			}
		}

		// Token: 0x17000A54 RID: 2644
		// (get) Token: 0x06002BFB RID: 11259 RVA: 0x000C5E50 File Offset: 0x000C4050
		// (set) Token: 0x06002BFC RID: 11260 RVA: 0x000C5E58 File Offset: 0x000C4058
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[Localizable(true)]
		[SRDescription("ListBoxHorizontalScrollbarDescr")]
		public bool HorizontalScrollbar
		{
			get
			{
				return this.horizontalScrollbar;
			}
			set
			{
				if (value != this.horizontalScrollbar)
				{
					this.horizontalScrollbar = value;
					this.RefreshItems();
					if (!this.MultiColumn)
					{
						base.RecreateHandle();
					}
				}
			}
		}

		// Token: 0x17000A55 RID: 2645
		// (get) Token: 0x06002BFD RID: 11261 RVA: 0x000C5E7E File Offset: 0x000C407E
		// (set) Token: 0x06002BFE RID: 11262 RVA: 0x000C5E88 File Offset: 0x000C4088
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[Localizable(true)]
		[SRDescription("ListBoxIntegralHeightDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public bool IntegralHeight
		{
			get
			{
				return this.integralHeight;
			}
			set
			{
				if (this.integralHeight != value)
				{
					this.integralHeight = value;
					base.RecreateHandle();
					this.integralHeightAdjust = true;
					try
					{
						base.Height = this.requestedHeight;
					}
					finally
					{
						this.integralHeightAdjust = false;
					}
				}
			}
		}

		// Token: 0x17000A56 RID: 2646
		// (get) Token: 0x06002BFF RID: 11263 RVA: 0x000C5ED8 File Offset: 0x000C40D8
		// (set) Token: 0x06002C00 RID: 11264 RVA: 0x000C5EFC File Offset: 0x000C40FC
		[SRCategory("CatBehavior")]
		[DefaultValue(13)]
		[Localizable(true)]
		[SRDescription("ListBoxItemHeightDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
		public virtual int ItemHeight
		{
			get
			{
				if (this.drawMode == DrawMode.OwnerDrawFixed || this.drawMode == DrawMode.OwnerDrawVariable)
				{
					return this.itemHeight;
				}
				return this.GetItemHeight(0);
			}
			set
			{
				if (value < 1 || value > 255)
				{
					throw new ArgumentOutOfRangeException("ItemHeight", SR.GetString("InvalidExBoundArgument", new object[]
					{
						"ItemHeight",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture),
						"256"
					}));
				}
				if (this.itemHeight != value)
				{
					this.itemHeight = value;
					if (this.drawMode == DrawMode.OwnerDrawFixed && base.IsHandleCreated)
					{
						this.BeginUpdate();
						base.SendMessage(416, 0, value);
						if (this.IntegralHeight)
						{
							Size size = base.Size;
							base.Size = new Size(size.Width + 1, size.Height);
							base.Size = size;
						}
						this.EndUpdate();
					}
				}
			}
		}

		// Token: 0x17000A57 RID: 2647
		// (get) Token: 0x06002C01 RID: 11265 RVA: 0x000C5FCC File Offset: 0x000C41CC
		[SRCategory("CatData")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[SRDescription("ListBoxItemsDescr")]
		[Editor("System.Windows.Forms.Design.ListControlStringCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[MergableProperty(false)]
		public ListBox.ObjectCollection Items
		{
			get
			{
				if (this.itemsCollection == null)
				{
					this.itemsCollection = this.CreateItemCollection();
				}
				return this.itemsCollection;
			}
		}

		// Token: 0x17000A58 RID: 2648
		// (get) Token: 0x06002C02 RID: 11266 RVA: 0x000C5FE8 File Offset: 0x000C41E8
		internal virtual int MaxItemWidth
		{
			get
			{
				if (this.horizontalExtent > 0)
				{
					return this.horizontalExtent;
				}
				if (this.DrawMode != DrawMode.Normal)
				{
					return -1;
				}
				if (this.maxWidth > -1)
				{
					return this.maxWidth;
				}
				this.maxWidth = this.ComputeMaxItemWidth(this.maxWidth);
				return this.maxWidth;
			}
		}

		// Token: 0x17000A59 RID: 2649
		// (get) Token: 0x06002C03 RID: 11267 RVA: 0x000C6037 File Offset: 0x000C4237
		// (set) Token: 0x06002C04 RID: 11268 RVA: 0x000C603F File Offset: 0x000C423F
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("ListBoxMultiColumnDescr")]
		public bool MultiColumn
		{
			get
			{
				return this.multiColumn;
			}
			set
			{
				if (this.multiColumn != value)
				{
					if (value && this.drawMode == DrawMode.OwnerDrawVariable)
					{
						throw new ArgumentException(SR.GetString("ListBoxVarHeightMultiCol"), "value");
					}
					this.multiColumn = value;
					base.RecreateHandle();
				}
			}
		}

		// Token: 0x17000A5A RID: 2650
		// (get) Token: 0x06002C05 RID: 11269 RVA: 0x000C6078 File Offset: 0x000C4278
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ListBoxPreferredHeightDescr")]
		public int PreferredHeight
		{
			get
			{
				int num = 0;
				if (this.drawMode == DrawMode.OwnerDrawVariable)
				{
					if (base.RecreatingHandle || base.GetState(262144))
					{
						num = base.Height;
					}
					else if (this.itemsCollection != null)
					{
						int count = this.itemsCollection.Count;
						for (int i = 0; i < count; i++)
						{
							num += this.GetItemHeight(i);
						}
					}
				}
				else
				{
					int num2 = (this.itemsCollection == null || this.itemsCollection.Count == 0) ? 1 : this.itemsCollection.Count;
					num = this.GetItemHeight(0) * num2;
				}
				if (this.borderStyle != BorderStyle.None)
				{
					num += SystemInformation.BorderSize.Height * 4 + 3;
				}
				return num;
			}
		}

		// Token: 0x06002C06 RID: 11270 RVA: 0x000C6128 File Offset: 0x000C4328
		internal override Size GetPreferredSizeCore(Size proposedConstraints)
		{
			int preferredHeight = this.PreferredHeight;
			if (base.IsHandleCreated)
			{
				int num = this.SizeFromClientSize(new Size(this.MaxItemWidth, preferredHeight)).Width;
				num += SystemInformation.VerticalScrollBarWidth + 4;
				return new Size(num, preferredHeight) + this.Padding.Size;
			}
			return this.DefaultSize;
		}

		// Token: 0x17000A5B RID: 2651
		// (get) Token: 0x06002C07 RID: 11271 RVA: 0x000C618C File Offset: 0x000C438C
		// (set) Token: 0x06002C08 RID: 11272 RVA: 0x000C619D File Offset: 0x000C439D
		public override RightToLeft RightToLeft
		{
			get
			{
				if (!ListBox.RunningOnWin2K)
				{
					return RightToLeft.No;
				}
				return base.RightToLeft;
			}
			set
			{
				base.RightToLeft = value;
			}
		}

		// Token: 0x17000A5C RID: 2652
		// (get) Token: 0x06002C09 RID: 11273 RVA: 0x000C61A6 File Offset: 0x000C43A6
		private static bool RunningOnWin2K
		{
			get
			{
				if (!ListBox.checkedOS && (Environment.OSVersion.Platform != PlatformID.Win32NT || Environment.OSVersion.Version.Major < 5))
				{
					ListBox.runningOnWin2K = false;
					ListBox.checkedOS = true;
				}
				return ListBox.runningOnWin2K;
			}
		}

		// Token: 0x17000A5D RID: 2653
		// (get) Token: 0x06002C0A RID: 11274 RVA: 0x000C61DF File Offset: 0x000C43DF
		// (set) Token: 0x06002C0B RID: 11275 RVA: 0x000C61E7 File Offset: 0x000C43E7
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[Localizable(true)]
		[SRDescription("ListBoxScrollIsVisibleDescr")]
		public bool ScrollAlwaysVisible
		{
			get
			{
				return this.scrollAlwaysVisible;
			}
			set
			{
				if (this.scrollAlwaysVisible != value)
				{
					this.scrollAlwaysVisible = value;
					base.RecreateHandle();
				}
			}
		}

		// Token: 0x17000A5E RID: 2654
		// (get) Token: 0x06002C0C RID: 11276 RVA: 0x000C61FF File Offset: 0x000C43FF
		protected override bool AllowSelection
		{
			get
			{
				return this.selectionMode > SelectionMode.None;
			}
		}

		// Token: 0x17000A5F RID: 2655
		// (get) Token: 0x06002C0D RID: 11277 RVA: 0x000C620C File Offset: 0x000C440C
		// (set) Token: 0x06002C0E RID: 11278 RVA: 0x000C6284 File Offset: 0x000C4484
		[Browsable(false)]
		[Bindable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ListBoxSelectedIndexDescr")]
		public override int SelectedIndex
		{
			get
			{
				SelectionMode selectionMode = this.selectionModeChanging ? this.cachedSelectionMode : this.selectionMode;
				if (selectionMode == SelectionMode.None)
				{
					return -1;
				}
				if (selectionMode == SelectionMode.One && base.IsHandleCreated)
				{
					return (int)((long)base.SendMessage(392, 0, 0));
				}
				if (this.itemsCollection != null && this.SelectedItems.Count > 0)
				{
					return this.Items.IndexOfIdentifier(this.SelectedItems.GetObjectAt(0));
				}
				return -1;
			}
			set
			{
				int num = (this.itemsCollection == null) ? 0 : this.itemsCollection.Count;
				if (value < -1 || value >= num)
				{
					throw new ArgumentOutOfRangeException("SelectedIndex", SR.GetString("InvalidArgument", new object[]
					{
						"SelectedIndex",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.selectionMode == SelectionMode.None)
				{
					throw new ArgumentException(SR.GetString("ListBoxInvalidSelectionMode"), "SelectedIndex");
				}
				if (this.selectionMode == SelectionMode.One && value != -1)
				{
					int selectedIndex = this.SelectedIndex;
					if (selectedIndex != value)
					{
						if (selectedIndex != -1)
						{
							this.SelectedItems.SetSelected(selectedIndex, false);
						}
						this.SelectedItems.SetSelected(value, true);
						if (base.IsHandleCreated)
						{
							this.NativeSetSelected(value, true);
						}
						this.OnSelectedIndexChanged(EventArgs.Empty);
						return;
					}
				}
				else if (value == -1)
				{
					if (this.SelectedIndex != -1)
					{
						this.ClearSelected();
						return;
					}
				}
				else if (!this.SelectedItems.GetSelected(value))
				{
					this.SelectedItems.SetSelected(value, true);
					if (base.IsHandleCreated)
					{
						this.NativeSetSelected(value, true);
					}
					this.OnSelectedIndexChanged(EventArgs.Empty);
				}
			}
		}

		// Token: 0x17000A60 RID: 2656
		// (get) Token: 0x06002C0F RID: 11279 RVA: 0x000C639E File Offset: 0x000C459E
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ListBoxSelectedIndicesDescr")]
		public ListBox.SelectedIndexCollection SelectedIndices
		{
			get
			{
				if (this.selectedIndices == null)
				{
					this.selectedIndices = new ListBox.SelectedIndexCollection(this);
				}
				return this.selectedIndices;
			}
		}

		// Token: 0x17000A61 RID: 2657
		// (get) Token: 0x06002C10 RID: 11280 RVA: 0x000C63BA File Offset: 0x000C45BA
		// (set) Token: 0x06002C11 RID: 11281 RVA: 0x000C63D8 File Offset: 0x000C45D8
		[Browsable(false)]
		[Bindable(true)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ListBoxSelectedItemDescr")]
		public object SelectedItem
		{
			get
			{
				if (this.SelectedItems.Count > 0)
				{
					return this.SelectedItems[0];
				}
				return null;
			}
			set
			{
				if (this.itemsCollection != null)
				{
					if (value != null)
					{
						int num = this.itemsCollection.IndexOf(value);
						if (num != -1)
						{
							this.SelectedIndex = num;
							return;
						}
					}
					else
					{
						this.SelectedIndex = -1;
					}
				}
			}
		}

		// Token: 0x17000A62 RID: 2658
		// (get) Token: 0x06002C12 RID: 11282 RVA: 0x000C6410 File Offset: 0x000C4610
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ListBoxSelectedItemsDescr")]
		public ListBox.SelectedObjectCollection SelectedItems
		{
			get
			{
				if (this.selectedItems == null)
				{
					this.selectedItems = new ListBox.SelectedObjectCollection(this);
				}
				return this.selectedItems;
			}
		}

		// Token: 0x17000A63 RID: 2659
		// (get) Token: 0x06002C13 RID: 11283 RVA: 0x000C642C File Offset: 0x000C462C
		// (set) Token: 0x06002C14 RID: 11284 RVA: 0x000C6434 File Offset: 0x000C4634
		[SRCategory("CatBehavior")]
		[DefaultValue(SelectionMode.One)]
		[SRDescription("ListBoxSelectionModeDescr")]
		public virtual SelectionMode SelectionMode
		{
			get
			{
				return this.selectionMode;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 3))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(SelectionMode));
				}
				if (this.selectionMode != value)
				{
					this.SelectedItems.EnsureUpToDate();
					this.selectionMode = value;
					try
					{
						this.selectionModeChanging = true;
						base.RecreateHandle();
					}
					finally
					{
						this.selectionModeChanging = false;
						this.cachedSelectionMode = this.selectionMode;
						if (base.IsHandleCreated)
						{
							this.NativeUpdateSelection();
						}
					}
				}
			}
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06002C15 RID: 11285 RVA: 0x000C64C4 File Offset: 0x000C46C4
		// (set) Token: 0x06002C16 RID: 11286 RVA: 0x000C64CC File Offset: 0x000C46CC
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("ListBoxSortedDescr")]
		public bool Sorted
		{
			get
			{
				return this.sorted;
			}
			set
			{
				if (this.sorted != value)
				{
					this.sorted = value;
					if (this.sorted && this.itemsCollection != null && this.itemsCollection.Count >= 1)
					{
						this.Sort();
					}
				}
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06002C17 RID: 11287 RVA: 0x000C6502 File Offset: 0x000C4702
		// (set) Token: 0x06002C18 RID: 11288 RVA: 0x000C6544 File Offset: 0x000C4744
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Bindable(false)]
		public override string Text
		{
			get
			{
				if (this.SelectionMode == SelectionMode.None || this.SelectedItem == null)
				{
					return base.Text;
				}
				if (base.FormattingEnabled)
				{
					return base.GetItemText(this.SelectedItem);
				}
				return base.FilterItemOnProperty(this.SelectedItem).ToString();
			}
			set
			{
				base.Text = value;
				if (this.SelectionMode != SelectionMode.None && value != null && (this.SelectedItem == null || !value.Equals(base.GetItemText(this.SelectedItem))))
				{
					int count = this.Items.Count;
					for (int i = 0; i < count; i++)
					{
						if (string.Compare(value, base.GetItemText(this.Items[i]), true, CultureInfo.CurrentCulture) == 0)
						{
							this.SelectedIndex = i;
							return;
						}
					}
				}
			}
		}

		// Token: 0x140001F8 RID: 504
		// (add) Token: 0x06002C19 RID: 11289 RVA: 0x00046771 File Offset: 0x00044971
		// (remove) Token: 0x06002C1A RID: 11290 RVA: 0x0004677A File Offset: 0x0004497A
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
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

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06002C1B RID: 11291 RVA: 0x000C65BF File Offset: 0x000C47BF
		// (set) Token: 0x06002C1C RID: 11292 RVA: 0x000C65E3 File Offset: 0x000C47E3
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ListBoxTopIndexDescr")]
		public int TopIndex
		{
			get
			{
				if (base.IsHandleCreated)
				{
					return (int)((long)base.SendMessage(398, 0, 0));
				}
				return this.topIndex;
			}
			set
			{
				if (base.IsHandleCreated)
				{
					base.SendMessage(407, value, 0);
					return;
				}
				this.topIndex = value;
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06002C1D RID: 11293 RVA: 0x000C6603 File Offset: 0x000C4803
		// (set) Token: 0x06002C1E RID: 11294 RVA: 0x000C660B File Offset: 0x000C480B
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("ListBoxUseTabStopsDescr")]
		public bool UseTabStops
		{
			get
			{
				return this.useTabStops;
			}
			set
			{
				if (this.useTabStops != value)
				{
					this.useTabStops = value;
					base.RecreateHandle();
				}
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06002C1F RID: 11295 RVA: 0x000C6623 File Offset: 0x000C4823
		[SRCategory("CatBehavior")]
		[SRDescription("ListBoxCustomTabOffsetsDescr")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Browsable(false)]
		public ListBox.IntegerCollection CustomTabOffsets
		{
			get
			{
				if (this.customTabOffsets == null)
				{
					this.customTabOffsets = new ListBox.IntegerCollection(this);
				}
				return this.customTabOffsets;
			}
		}

		// Token: 0x06002C20 RID: 11296 RVA: 0x000C6640 File Offset: 0x000C4840
		[Obsolete("This method has been deprecated.  There is no replacement.  http://go.microsoft.com/fwlink/?linkid=14202")]
		protected virtual void AddItemsCore(object[] value)
		{
			if (value == null || value.Length == 0)
			{
				return;
			}
			this.Items.AddRangeInternal(value);
		}

		// Token: 0x140001F9 RID: 505
		// (add) Token: 0x06002C21 RID: 11297 RVA: 0x000131E8 File Offset: 0x000113E8
		// (remove) Token: 0x06002C22 RID: 11298 RVA: 0x000131F1 File Offset: 0x000113F1
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public new event EventHandler Click
		{
			add
			{
				base.Click += value;
			}
			remove
			{
				base.Click -= value;
			}
		}

		// Token: 0x140001FA RID: 506
		// (add) Token: 0x06002C23 RID: 11299 RVA: 0x000131FA File Offset: 0x000113FA
		// (remove) Token: 0x06002C24 RID: 11300 RVA: 0x00013203 File Offset: 0x00011403
		[Browsable(true)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public new event MouseEventHandler MouseClick
		{
			add
			{
				base.MouseClick += value;
			}
			remove
			{
				base.MouseClick -= value;
			}
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06002C25 RID: 11301 RVA: 0x00013656 File Offset: 0x00011856
		// (set) Token: 0x06002C26 RID: 11302 RVA: 0x0001365E File Offset: 0x0001185E
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public new Padding Padding
		{
			get
			{
				return base.Padding;
			}
			set
			{
				base.Padding = value;
			}
		}

		// Token: 0x140001FB RID: 507
		// (add) Token: 0x06002C27 RID: 11303 RVA: 0x00013667 File Offset: 0x00011867
		// (remove) Token: 0x06002C28 RID: 11304 RVA: 0x00013670 File Offset: 0x00011870
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new event EventHandler PaddingChanged
		{
			add
			{
				base.PaddingChanged += value;
			}
			remove
			{
				base.PaddingChanged -= value;
			}
		}

		// Token: 0x140001FC RID: 508
		// (add) Token: 0x06002C29 RID: 11305 RVA: 0x00013F87 File Offset: 0x00012187
		// (remove) Token: 0x06002C2A RID: 11306 RVA: 0x00013F90 File Offset: 0x00012190
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

		// Token: 0x140001FD RID: 509
		// (add) Token: 0x06002C2B RID: 11307 RVA: 0x000C6667 File Offset: 0x000C4867
		// (remove) Token: 0x06002C2C RID: 11308 RVA: 0x000C667A File Offset: 0x000C487A
		[SRCategory("CatBehavior")]
		[SRDescription("drawItemEventDescr")]
		public event DrawItemEventHandler DrawItem
		{
			add
			{
				base.Events.AddHandler(ListBox.EVENT_DRAWITEM, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListBox.EVENT_DRAWITEM, value);
			}
		}

		// Token: 0x140001FE RID: 510
		// (add) Token: 0x06002C2D RID: 11309 RVA: 0x000C668D File Offset: 0x000C488D
		// (remove) Token: 0x06002C2E RID: 11310 RVA: 0x000C66A0 File Offset: 0x000C48A0
		[SRCategory("CatBehavior")]
		[SRDescription("measureItemEventDescr")]
		public event MeasureItemEventHandler MeasureItem
		{
			add
			{
				base.Events.AddHandler(ListBox.EVENT_MEASUREITEM, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListBox.EVENT_MEASUREITEM, value);
			}
		}

		// Token: 0x140001FF RID: 511
		// (add) Token: 0x06002C2F RID: 11311 RVA: 0x000C66B3 File Offset: 0x000C48B3
		// (remove) Token: 0x06002C30 RID: 11312 RVA: 0x000C66C6 File Offset: 0x000C48C6
		[SRCategory("CatBehavior")]
		[SRDescription("selectedIndexChangedEventDescr")]
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(ListBox.EVENT_SELECTEDINDEXCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListBox.EVENT_SELECTEDINDEXCHANGED, value);
			}
		}

		// Token: 0x06002C31 RID: 11313 RVA: 0x000C66D9 File Offset: 0x000C48D9
		public void BeginUpdate()
		{
			base.BeginUpdateInternal();
			this.updateCount++;
		}

		// Token: 0x06002C32 RID: 11314 RVA: 0x000C66EF File Offset: 0x000C48EF
		private void CheckIndex(int index)
		{
			if (index < 0 || index >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("IndexOutOfRange", new object[]
				{
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
		}

		// Token: 0x06002C33 RID: 11315 RVA: 0x000C672D File Offset: 0x000C492D
		private void CheckNoDataSource()
		{
			if (base.DataSource != null)
			{
				throw new ArgumentException(SR.GetString("DataSourceLocksItems"));
			}
		}

		// Token: 0x06002C34 RID: 11316 RVA: 0x000C6747 File Offset: 0x000C4947
		protected virtual ListBox.ObjectCollection CreateItemCollection()
		{
			return new ListBox.ObjectCollection(this);
		}

		// Token: 0x06002C35 RID: 11317 RVA: 0x000C6750 File Offset: 0x000C4950
		internal virtual int ComputeMaxItemWidth(int oldMax)
		{
			string[] array = new string[this.Items.Count];
			for (int i = 0; i < this.Items.Count; i++)
			{
				array[i] = base.GetItemText(this.Items[i]);
			}
			return Math.Max(oldMax, LayoutUtils.OldGetLargestStringSizeInCollection(this.Font, array).Width);
		}

		// Token: 0x06002C36 RID: 11318 RVA: 0x000C67B4 File Offset: 0x000C49B4
		public void ClearSelected()
		{
			bool flag = false;
			int num = (this.itemsCollection == null) ? 0 : this.itemsCollection.Count;
			for (int i = 0; i < num; i++)
			{
				if (this.SelectedItems.GetSelected(i))
				{
					flag = true;
					this.SelectedItems.SetSelected(i, false);
					if (base.IsHandleCreated)
					{
						this.NativeSetSelected(i, false);
					}
				}
			}
			if (flag)
			{
				this.OnSelectedIndexChanged(EventArgs.Empty);
			}
		}

		// Token: 0x06002C37 RID: 11319 RVA: 0x000C6821 File Offset: 0x000C4A21
		public void EndUpdate()
		{
			base.EndUpdateInternal();
			this.updateCount--;
		}

		// Token: 0x06002C38 RID: 11320 RVA: 0x000C6838 File Offset: 0x000C4A38
		public int FindString(string s)
		{
			return this.FindString(s, -1);
		}

		// Token: 0x06002C39 RID: 11321 RVA: 0x000C6844 File Offset: 0x000C4A44
		public int FindString(string s, int startIndex)
		{
			if (s == null)
			{
				return -1;
			}
			int num = (this.itemsCollection == null) ? 0 : this.itemsCollection.Count;
			if (num == 0)
			{
				return -1;
			}
			if (startIndex < -1 || startIndex >= num)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			return base.FindStringInternal(s, this.Items, startIndex, false);
		}

		// Token: 0x06002C3A RID: 11322 RVA: 0x000C6894 File Offset: 0x000C4A94
		public int FindStringExact(string s)
		{
			return this.FindStringExact(s, -1);
		}

		// Token: 0x06002C3B RID: 11323 RVA: 0x000C68A0 File Offset: 0x000C4AA0
		public int FindStringExact(string s, int startIndex)
		{
			if (s == null)
			{
				return -1;
			}
			int num = (this.itemsCollection == null) ? 0 : this.itemsCollection.Count;
			if (num == 0)
			{
				return -1;
			}
			if (startIndex < -1 || startIndex >= num)
			{
				throw new ArgumentOutOfRangeException("startIndex");
			}
			return base.FindStringInternal(s, this.Items, startIndex, true);
		}

		// Token: 0x06002C3C RID: 11324 RVA: 0x000C68F0 File Offset: 0x000C4AF0
		public int GetItemHeight(int index)
		{
			int num = (this.itemsCollection == null) ? 0 : this.itemsCollection.Count;
			if (index < 0 || (index > 0 && index >= num))
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (this.drawMode != DrawMode.OwnerDrawVariable)
			{
				index = 0;
			}
			if (!base.IsHandleCreated)
			{
				return this.itemHeight;
			}
			int num2 = (int)((long)base.SendMessage(417, index, 0));
			if (num2 == -1)
			{
				throw new Win32Exception();
			}
			return num2;
		}

		// Token: 0x06002C3D RID: 11325 RVA: 0x000C698C File Offset: 0x000C4B8C
		public Rectangle GetItemRectangle(int index)
		{
			this.CheckIndex(index);
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			base.SendMessage(408, index, ref rect);
			return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
		}

		// Token: 0x06002C3E RID: 11326 RVA: 0x000C69D4 File Offset: 0x000C4BD4
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override Rectangle GetScaledBounds(Rectangle bounds, SizeF factor, BoundsSpecified specified)
		{
			bounds.Height = this.requestedHeight;
			return base.GetScaledBounds(bounds, factor, specified);
		}

		// Token: 0x06002C3F RID: 11327 RVA: 0x000C69EC File Offset: 0x000C4BEC
		public bool GetSelected(int index)
		{
			this.CheckIndex(index);
			return this.GetSelectedInternal(index);
		}

		// Token: 0x06002C40 RID: 11328 RVA: 0x000C69FC File Offset: 0x000C4BFC
		private bool GetSelectedInternal(int index)
		{
			if (!base.IsHandleCreated)
			{
				return this.itemsCollection != null && this.SelectedItems.GetSelected(index);
			}
			int num = (int)((long)base.SendMessage(391, index, 0));
			if (num == -1)
			{
				throw new Win32Exception();
			}
			return num > 0;
		}

		// Token: 0x06002C41 RID: 11329 RVA: 0x000C6A4D File Offset: 0x000C4C4D
		public int IndexFromPoint(Point p)
		{
			return this.IndexFromPoint(p.X, p.Y);
		}

		// Token: 0x06002C42 RID: 11330 RVA: 0x000C6A64 File Offset: 0x000C4C64
		public int IndexFromPoint(int x, int y)
		{
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			UnsafeNativeMethods.GetClientRect(new HandleRef(this, base.Handle), ref rect);
			if (rect.left <= x && x < rect.right && rect.top <= y && y < rect.bottom)
			{
				int n = (int)((long)base.SendMessage(425, 0, (int)((long)NativeMethods.Util.MAKELPARAM(x, y))));
				if (NativeMethods.Util.HIWORD(n) == 0)
				{
					return NativeMethods.Util.LOWORD(n);
				}
			}
			return -1;
		}

		// Token: 0x06002C43 RID: 11331 RVA: 0x000C6AE4 File Offset: 0x000C4CE4
		private int NativeAdd(object item)
		{
			int num = (int)((long)base.SendMessage(384, 0, base.GetItemText(item)));
			if (num == -2)
			{
				throw new OutOfMemoryException();
			}
			if (num == -1)
			{
				throw new OutOfMemoryException(SR.GetString("ListBoxItemOverflow"));
			}
			return num;
		}

		// Token: 0x06002C44 RID: 11332 RVA: 0x000C6B2B File Offset: 0x000C4D2B
		private void NativeClear()
		{
			base.SendMessage(388, 0, 0);
		}

		// Token: 0x06002C45 RID: 11333 RVA: 0x000C6B3C File Offset: 0x000C4D3C
		internal string NativeGetItemText(int index)
		{
			int num = (int)((long)base.SendMessage(394, index, 0));
			StringBuilder stringBuilder = new StringBuilder(num + 1);
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 393, index, stringBuilder);
			return stringBuilder.ToString();
		}

		// Token: 0x06002C46 RID: 11334 RVA: 0x000C6B88 File Offset: 0x000C4D88
		private int NativeInsert(int index, object item)
		{
			int num = (int)((long)base.SendMessage(385, index, base.GetItemText(item)));
			if (num == -2)
			{
				throw new OutOfMemoryException();
			}
			if (num == -1)
			{
				throw new OutOfMemoryException(SR.GetString("ListBoxItemOverflow"));
			}
			return num;
		}

		// Token: 0x06002C47 RID: 11335 RVA: 0x000C6BD0 File Offset: 0x000C4DD0
		private void NativeRemoveAt(int index)
		{
			bool flag = (int)((long)base.SendMessage(391, (IntPtr)index, IntPtr.Zero)) > 0;
			base.SendMessage(386, index, 0);
			if (flag)
			{
				this.OnSelectedIndexChanged(EventArgs.Empty);
			}
		}

		// Token: 0x06002C48 RID: 11336 RVA: 0x000C6C19 File Offset: 0x000C4E19
		private void NativeSetSelected(int index, bool value)
		{
			if (this.selectionMode == SelectionMode.One)
			{
				base.SendMessage(390, value ? index : -1, 0);
				return;
			}
			base.SendMessage(389, value ? -1 : 0, index);
		}

		// Token: 0x06002C49 RID: 11337 RVA: 0x000C6C50 File Offset: 0x000C4E50
		private void NativeUpdateSelection()
		{
			int count = this.Items.Count;
			for (int i = 0; i < count; i++)
			{
				this.SelectedItems.SetSelected(i, false);
			}
			int[] array = null;
			SelectionMode selectionMode = this.selectionMode;
			if (selectionMode != SelectionMode.One)
			{
				if (selectionMode - SelectionMode.MultiSimple <= 1)
				{
					int num = (int)((long)base.SendMessage(400, 0, 0));
					if (num > 0)
					{
						array = new int[num];
						UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 401, num, array);
					}
				}
			}
			else
			{
				int num2 = (int)((long)base.SendMessage(392, 0, 0));
				if (num2 >= 0)
				{
					array = new int[]
					{
						num2
					};
				}
			}
			if (array != null)
			{
				foreach (int index in array)
				{
					this.SelectedItems.SetSelected(index, true);
				}
			}
		}

		// Token: 0x06002C4A RID: 11338 RVA: 0x000C6D29 File Offset: 0x000C4F29
		protected override void OnChangeUICues(UICuesEventArgs e)
		{
			base.Invalidate();
			base.OnChangeUICues(e);
		}

		// Token: 0x06002C4B RID: 11339 RVA: 0x000C6D38 File Offset: 0x000C4F38
		protected virtual void OnDrawItem(DrawItemEventArgs e)
		{
			DrawItemEventHandler drawItemEventHandler = (DrawItemEventHandler)base.Events[ListBox.EVENT_DRAWITEM];
			if (drawItemEventHandler != null)
			{
				drawItemEventHandler(this, e);
			}
		}

		// Token: 0x06002C4C RID: 11340 RVA: 0x000C6D68 File Offset: 0x000C4F68
		protected override void OnHandleCreated(EventArgs e)
		{
			base.OnHandleCreated(e);
			base.SendMessage(421, CultureInfo.CurrentCulture.LCID, 0);
			if (this.columnWidth != 0)
			{
				base.SendMessage(405, this.columnWidth, 0);
			}
			if (this.drawMode == DrawMode.OwnerDrawFixed)
			{
				base.SendMessage(416, 0, this.ItemHeight);
			}
			if (this.topIndex != 0)
			{
				base.SendMessage(407, this.topIndex, 0);
			}
			if (this.UseCustomTabOffsets && this.CustomTabOffsets != null)
			{
				int count = this.CustomTabOffsets.Count;
				int[] array = new int[count];
				this.CustomTabOffsets.CopyTo(array, 0);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 402, count, array);
			}
			if (this.itemsCollection != null)
			{
				int count2 = this.itemsCollection.Count;
				for (int i = 0; i < count2; i++)
				{
					this.NativeAdd(this.itemsCollection[i]);
					if (this.selectionMode != SelectionMode.None && this.selectedItems != null)
					{
						this.selectedItems.PushSelectionIntoNativeListBox(i);
					}
				}
			}
			if (this.selectedItems != null && this.selectedItems.Count > 0 && this.selectionMode == SelectionMode.One)
			{
				this.SelectedItems.Dirty();
				this.SelectedItems.EnsureUpToDate();
			}
			this.UpdateHorizontalExtent();
		}

		// Token: 0x06002C4D RID: 11341 RVA: 0x000C6EB8 File Offset: 0x000C50B8
		protected override void OnHandleDestroyed(EventArgs e)
		{
			this.SelectedItems.EnsureUpToDate();
			if (base.Disposing)
			{
				this.itemsCollection = null;
			}
			base.OnHandleDestroyed(e);
		}

		// Token: 0x06002C4E RID: 11342 RVA: 0x000C6EDC File Offset: 0x000C50DC
		protected virtual void OnMeasureItem(MeasureItemEventArgs e)
		{
			MeasureItemEventHandler measureItemEventHandler = (MeasureItemEventHandler)base.Events[ListBox.EVENT_MEASUREITEM];
			if (measureItemEventHandler != null)
			{
				measureItemEventHandler(this, e);
			}
		}

		// Token: 0x06002C4F RID: 11343 RVA: 0x000C6F0A File Offset: 0x000C510A
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			this.UpdateFontCache();
		}

		// Token: 0x06002C50 RID: 11344 RVA: 0x000C6F19 File Offset: 0x000C5119
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
			if (this.ParentInternal != null)
			{
				base.RecreateHandle();
			}
		}

		// Token: 0x06002C51 RID: 11345 RVA: 0x000C6F30 File Offset: 0x000C5130
		protected override void OnResize(EventArgs e)
		{
			base.OnResize(e);
			if (this.RightToLeft == RightToLeft.Yes || this.HorizontalScrollbar)
			{
				base.Invalidate();
			}
		}

		// Token: 0x06002C52 RID: 11346 RVA: 0x000C6F50 File Offset: 0x000C5150
		protected override void OnSelectedIndexChanged(EventArgs e)
		{
			base.OnSelectedIndexChanged(e);
			if (base.DataManager != null && base.DataManager.Position != this.SelectedIndex && (!base.FormattingEnabled || this.SelectedIndex != -1))
			{
				base.DataManager.Position = this.SelectedIndex;
			}
			EventHandler eventHandler = (EventHandler)base.Events[ListBox.EVENT_SELECTEDINDEXCHANGED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002C53 RID: 11347 RVA: 0x000C6FC2 File Offset: 0x000C51C2
		protected override void OnSelectedValueChanged(EventArgs e)
		{
			base.OnSelectedValueChanged(e);
			this.selectedValueChangedFired = true;
		}

		// Token: 0x06002C54 RID: 11348 RVA: 0x000C6FD2 File Offset: 0x000C51D2
		protected override void OnDataSourceChanged(EventArgs e)
		{
			if (base.DataSource == null)
			{
				this.BeginUpdate();
				this.SelectedIndex = -1;
				this.Items.ClearInternal();
				this.EndUpdate();
			}
			base.OnDataSourceChanged(e);
			this.RefreshItems();
		}

		// Token: 0x06002C55 RID: 11349 RVA: 0x000C7007 File Offset: 0x000C5207
		protected override void OnDisplayMemberChanged(EventArgs e)
		{
			base.OnDisplayMemberChanged(e);
			this.RefreshItems();
			if (this.SelectionMode != SelectionMode.None && base.DataManager != null)
			{
				this.SelectedIndex = base.DataManager.Position;
			}
		}

		// Token: 0x06002C56 RID: 11350 RVA: 0x000C7038 File Offset: 0x000C5238
		public override void Refresh()
		{
			if (this.drawMode == DrawMode.OwnerDrawVariable)
			{
				int count = this.Items.Count;
				Graphics graphics = base.CreateGraphicsInternal();
				try
				{
					for (int i = 0; i < count; i++)
					{
						MeasureItemEventArgs e = new MeasureItemEventArgs(graphics, i, this.ItemHeight);
						this.OnMeasureItem(e);
					}
				}
				finally
				{
					graphics.Dispose();
				}
			}
			base.Refresh();
		}

		// Token: 0x06002C57 RID: 11351 RVA: 0x000C70A4 File Offset: 0x000C52A4
		protected override void RefreshItems()
		{
			ListBox.ObjectCollection objectCollection = this.itemsCollection;
			this.itemsCollection = null;
			this.selectedIndices = null;
			if (base.IsHandleCreated)
			{
				this.NativeClear();
			}
			object[] array = null;
			if (base.DataManager != null && base.DataManager.Count != -1)
			{
				array = new object[base.DataManager.Count];
				for (int i = 0; i < array.Length; i++)
				{
					array[i] = base.DataManager[i];
				}
			}
			else if (objectCollection != null)
			{
				array = new object[objectCollection.Count];
				objectCollection.CopyTo(array, 0);
			}
			if (array != null)
			{
				this.Items.AddRangeInternal(array);
			}
			if (this.SelectionMode != SelectionMode.None)
			{
				if (base.DataManager != null)
				{
					this.SelectedIndex = base.DataManager.Position;
					return;
				}
				if (objectCollection != null)
				{
					int count = objectCollection.Count;
					for (int j = 0; j < count; j++)
					{
						if (objectCollection.InnerArray.GetState(j, ListBox.SelectedObjectCollection.SelectedObjectMask))
						{
							this.SelectedItem = objectCollection[j];
						}
					}
				}
			}
		}

		// Token: 0x06002C58 RID: 11352 RVA: 0x000C71A0 File Offset: 0x000C53A0
		protected override void RefreshItem(int index)
		{
			this.Items.SetItemInternal(index, this.Items[index]);
		}

		// Token: 0x06002C59 RID: 11353 RVA: 0x000C71BA File Offset: 0x000C53BA
		public override void ResetBackColor()
		{
			base.ResetBackColor();
		}

		// Token: 0x06002C5A RID: 11354 RVA: 0x000C71C2 File Offset: 0x000C53C2
		public override void ResetForeColor()
		{
			base.ResetForeColor();
		}

		// Token: 0x06002C5B RID: 11355 RVA: 0x000C71CA File Offset: 0x000C53CA
		private void ResetItemHeight()
		{
			this.itemHeight = 13;
		}

		// Token: 0x06002C5C RID: 11356 RVA: 0x000C71D4 File Offset: 0x000C53D4
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			if (factor.Width != 1f && factor.Height != 1f)
			{
				this.UpdateFontCache();
			}
			base.ScaleControl(factor, specified);
		}

		// Token: 0x06002C5D RID: 11357 RVA: 0x000C7200 File Offset: 0x000C5400
		protected override void SetBoundsCore(int x, int y, int width, int height, BoundsSpecified specified)
		{
			if (!this.integralHeightAdjust && height != base.Height)
			{
				this.requestedHeight = height;
			}
			base.SetBoundsCore(x, y, width, height, specified);
		}

		// Token: 0x06002C5E RID: 11358 RVA: 0x000C722C File Offset: 0x000C542C
		protected override void SetItemsCore(IList value)
		{
			this.BeginUpdate();
			this.Items.ClearInternal();
			this.Items.AddRangeInternal(value);
			this.SelectedItems.Dirty();
			if (base.DataManager != null)
			{
				if (base.DataSource is ICurrencyManagerProvider)
				{
					this.selectedValueChangedFired = false;
				}
				if (base.IsHandleCreated)
				{
					base.SendMessage(390, base.DataManager.Position, 0);
				}
				if (!this.selectedValueChangedFired)
				{
					this.OnSelectedValueChanged(EventArgs.Empty);
					this.selectedValueChangedFired = false;
				}
			}
			this.EndUpdate();
		}

		// Token: 0x06002C5F RID: 11359 RVA: 0x000C72BD File Offset: 0x000C54BD
		protected override void SetItemCore(int index, object value)
		{
			this.Items.SetItemInternal(index, value);
		}

		// Token: 0x06002C60 RID: 11360 RVA: 0x000C72CC File Offset: 0x000C54CC
		public void SetSelected(int index, bool value)
		{
			int num = (this.itemsCollection == null) ? 0 : this.itemsCollection.Count;
			if (index < 0 || index >= num)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (this.selectionMode == SelectionMode.None)
			{
				throw new InvalidOperationException(SR.GetString("ListBoxInvalidSelectionMode"));
			}
			this.SelectedItems.SetSelected(index, value);
			if (base.IsHandleCreated)
			{
				this.NativeSetSelected(index, value);
			}
			this.SelectedItems.Dirty();
			this.OnSelectedIndexChanged(EventArgs.Empty);
		}

		// Token: 0x06002C61 RID: 11361 RVA: 0x000C7378 File Offset: 0x000C5578
		protected virtual void Sort()
		{
			this.CheckNoDataSource();
			ListBox.SelectedObjectCollection selectedObjectCollection = this.SelectedItems;
			selectedObjectCollection.EnsureUpToDate();
			if (this.sorted && this.itemsCollection != null)
			{
				this.itemsCollection.InnerArray.Sort();
				if (base.IsHandleCreated)
				{
					this.NativeClear();
					int count = this.itemsCollection.Count;
					for (int i = 0; i < count; i++)
					{
						this.NativeAdd(this.itemsCollection[i]);
						if (selectedObjectCollection.GetSelected(i))
						{
							this.NativeSetSelected(i, true);
						}
					}
				}
			}
		}

		// Token: 0x06002C62 RID: 11362 RVA: 0x000C7404 File Offset: 0x000C5604
		public override string ToString()
		{
			string text = base.ToString();
			if (this.itemsCollection != null)
			{
				text = text + ", Items.Count: " + this.Items.Count.ToString(CultureInfo.CurrentCulture);
				if (this.Items.Count > 0)
				{
					string itemText = base.GetItemText(this.Items[0]);
					string str = (itemText.Length > 40) ? itemText.Substring(0, 40) : itemText;
					text = text + ", Items[0]: " + str;
				}
			}
			return text;
		}

		// Token: 0x06002C63 RID: 11363 RVA: 0x000C748C File Offset: 0x000C568C
		private void UpdateFontCache()
		{
			this.fontIsChanged = true;
			this.integralHeightAdjust = true;
			try
			{
				base.Height = this.requestedHeight;
			}
			finally
			{
				this.integralHeightAdjust = false;
			}
			this.maxWidth = -1;
			this.UpdateHorizontalExtent();
			CommonProperties.xClearPreferredSizeCache(this);
		}

		// Token: 0x06002C64 RID: 11364 RVA: 0x000C74E0 File Offset: 0x000C56E0
		private void UpdateHorizontalExtent()
		{
			if (!this.multiColumn && this.horizontalScrollbar && base.IsHandleCreated)
			{
				int maxItemWidth = this.horizontalExtent;
				if (maxItemWidth == 0)
				{
					maxItemWidth = this.MaxItemWidth;
				}
				base.SendMessage(404, maxItemWidth, 0);
			}
		}

		// Token: 0x06002C65 RID: 11365 RVA: 0x000C7524 File Offset: 0x000C5724
		private void UpdateMaxItemWidth(object item, bool removing)
		{
			if (!this.horizontalScrollbar || this.horizontalExtent > 0)
			{
				this.maxWidth = -1;
				return;
			}
			if (this.maxWidth > -1)
			{
				int num;
				using (Graphics graphics = base.CreateGraphicsInternal())
				{
					num = (int)Math.Ceiling((double)graphics.MeasureString(base.GetItemText(item), this.Font).Width);
				}
				if (removing)
				{
					if (num >= this.maxWidth)
					{
						this.maxWidth = -1;
						return;
					}
				}
				else if (num > this.maxWidth)
				{
					this.maxWidth = num;
				}
			}
		}

		// Token: 0x06002C66 RID: 11366 RVA: 0x000C75C0 File Offset: 0x000C57C0
		private void UpdateCustomTabOffsets()
		{
			if (base.IsHandleCreated && this.UseCustomTabOffsets && this.CustomTabOffsets != null)
			{
				int count = this.CustomTabOffsets.Count;
				int[] array = new int[count];
				this.CustomTabOffsets.CopyTo(array, 0);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 402, count, array);
				base.Invalidate();
			}
		}

		// Token: 0x06002C67 RID: 11367 RVA: 0x000C7624 File Offset: 0x000C5824
		private void WmPrint(ref Message m)
		{
			base.WndProc(ref m);
			if ((2 & (int)m.LParam) != 0 && Application.RenderWithVisualStyles && this.BorderStyle == BorderStyle.Fixed3D)
			{
				IntSecurity.UnmanagedCode.Assert();
				try
				{
					using (Graphics graphics = Graphics.FromHdc(m.WParam))
					{
						Rectangle rect = new Rectangle(0, 0, base.Size.Width - 1, base.Size.Height - 1);
						using (Pen pen = new Pen(VisualStyleInformation.TextControlBorder))
						{
							graphics.DrawRectangle(pen, rect);
						}
						rect.Inflate(-1, -1);
						graphics.DrawRectangle(SystemPens.Window, rect);
					}
				}
				finally
				{
					CodeAccessPermission.RevertAssert();
				}
			}
		}

		// Token: 0x06002C68 RID: 11368 RVA: 0x000C7710 File Offset: 0x000C5910
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected virtual void WmReflectCommand(ref Message m)
		{
			int num = NativeMethods.Util.HIWORD(m.WParam);
			if (num != 1)
			{
				return;
			}
			if (this.selectedItems != null)
			{
				this.selectedItems.Dirty();
			}
			this.OnSelectedIndexChanged(EventArgs.Empty);
		}

		// Token: 0x06002C69 RID: 11369 RVA: 0x000C7750 File Offset: 0x000C5950
		private void WmReflectDrawItem(ref Message m)
		{
			NativeMethods.DRAWITEMSTRUCT drawitemstruct = (NativeMethods.DRAWITEMSTRUCT)m.GetLParam(typeof(NativeMethods.DRAWITEMSTRUCT));
			IntPtr hDC = drawitemstruct.hDC;
			IntPtr intPtr = Control.SetUpPalette(hDC, false, false);
			try
			{
				Graphics graphics = Graphics.FromHdcInternal(hDC);
				try
				{
					Rectangle rect = Rectangle.FromLTRB(drawitemstruct.rcItem.left, drawitemstruct.rcItem.top, drawitemstruct.rcItem.right, drawitemstruct.rcItem.bottom);
					if (this.HorizontalScrollbar)
					{
						if (this.MultiColumn)
						{
							rect.Width = Math.Max(this.ColumnWidth, rect.Width);
						}
						else
						{
							rect.Width = Math.Max(this.MaxItemWidth, rect.Width);
						}
					}
					this.OnDrawItem(new DrawItemEventArgs(graphics, this.Font, rect, drawitemstruct.itemID, (DrawItemState)drawitemstruct.itemState, this.ForeColor, this.BackColor));
				}
				finally
				{
					graphics.Dispose();
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					SafeNativeMethods.SelectPalette(new HandleRef(null, hDC), new HandleRef(null, intPtr), 0);
				}
			}
			m.Result = (IntPtr)1;
		}

		// Token: 0x06002C6A RID: 11370 RVA: 0x000C7884 File Offset: 0x000C5A84
		private void WmReflectMeasureItem(ref Message m)
		{
			NativeMethods.MEASUREITEMSTRUCT measureitemstruct = (NativeMethods.MEASUREITEMSTRUCT)m.GetLParam(typeof(NativeMethods.MEASUREITEMSTRUCT));
			if (this.drawMode == DrawMode.OwnerDrawVariable && measureitemstruct.itemID >= 0)
			{
				Graphics graphics = base.CreateGraphicsInternal();
				MeasureItemEventArgs measureItemEventArgs = new MeasureItemEventArgs(graphics, measureitemstruct.itemID, this.ItemHeight);
				try
				{
					this.OnMeasureItem(measureItemEventArgs);
					measureitemstruct.itemHeight = measureItemEventArgs.ItemHeight;
					goto IL_6A;
				}
				finally
				{
					graphics.Dispose();
				}
			}
			measureitemstruct.itemHeight = this.ItemHeight;
			IL_6A:
			Marshal.StructureToPtr(measureitemstruct, m.LParam, false);
			m.Result = (IntPtr)1;
		}

		// Token: 0x06002C6B RID: 11371 RVA: 0x000C7924 File Offset: 0x000C5B24
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 791)
			{
				if (msg != 71)
				{
					switch (msg)
					{
					case 513:
						if (this.selectedItems != null)
						{
							this.selectedItems.Dirty();
						}
						base.WndProc(ref m);
						return;
					case 514:
					{
						int x = NativeMethods.Util.SignedLOWORD(m.LParam);
						int y = NativeMethods.Util.SignedHIWORD(m.LParam);
						Point p = new Point(x, y);
						p = base.PointToScreen(p);
						bool capture = base.Capture;
						if (capture && UnsafeNativeMethods.WindowFromPoint(p.X, p.Y) == base.Handle)
						{
							if (!this.doubleClickFired && !base.ValidationCancelled)
							{
								this.OnClick(new MouseEventArgs(MouseButtons.Left, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
								this.OnMouseClick(new MouseEventArgs(MouseButtons.Left, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
							}
							else
							{
								this.doubleClickFired = false;
								if (!base.ValidationCancelled)
								{
									this.OnDoubleClick(new MouseEventArgs(MouseButtons.Left, 2, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
									this.OnMouseDoubleClick(new MouseEventArgs(MouseButtons.Left, 2, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
								}
							}
						}
						if (base.GetState(2048))
						{
							base.DefWndProc(ref m);
						}
						else
						{
							base.WndProc(ref m);
						}
						this.doubleClickFired = false;
						return;
					}
					case 515:
						this.doubleClickFired = true;
						base.WndProc(ref m);
						return;
					case 516:
						break;
					case 517:
					{
						int x2 = NativeMethods.Util.SignedLOWORD(m.LParam);
						int y2 = NativeMethods.Util.SignedHIWORD(m.LParam);
						Point p2 = new Point(x2, y2);
						p2 = base.PointToScreen(p2);
						bool capture2 = base.Capture;
						if (capture2 && UnsafeNativeMethods.WindowFromPoint(p2.X, p2.Y) == base.Handle && this.selectedItems != null)
						{
							this.selectedItems.Dirty();
						}
						base.WndProc(ref m);
						return;
					}
					default:
						if (msg == 791)
						{
							this.WmPrint(ref m);
							return;
						}
						break;
					}
				}
				else
				{
					base.WndProc(ref m);
					if (this.integralHeight && this.fontIsChanged)
					{
						base.Height = Math.Max(base.Height, this.ItemHeight);
						this.fontIsChanged = false;
						return;
					}
					return;
				}
			}
			else
			{
				if (msg == 8235)
				{
					this.WmReflectDrawItem(ref m);
					return;
				}
				if (msg == 8236)
				{
					this.WmReflectMeasureItem(ref m);
					return;
				}
				if (msg == 8465)
				{
					this.WmReflectCommand(ref m);
					return;
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x06002C6C RID: 11372 RVA: 0x000C7BD4 File Offset: 0x000C5DD4
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new ListBox.ListBoxAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x0400125D RID: 4701
		public const int NoMatches = -1;

		// Token: 0x0400125E RID: 4702
		public const int DefaultItemHeight = 13;

		// Token: 0x0400125F RID: 4703
		private const int maxWin9xHeight = 32767;

		// Token: 0x04001260 RID: 4704
		private static readonly object EVENT_SELECTEDINDEXCHANGED = new object();

		// Token: 0x04001261 RID: 4705
		private static readonly object EVENT_DRAWITEM = new object();

		// Token: 0x04001262 RID: 4706
		private static readonly object EVENT_MEASUREITEM = new object();

		// Token: 0x04001263 RID: 4707
		private static bool checkedOS = false;

		// Token: 0x04001264 RID: 4708
		private static bool runningOnWin2K = true;

		// Token: 0x04001265 RID: 4709
		private ListBox.SelectedObjectCollection selectedItems;

		// Token: 0x04001266 RID: 4710
		private ListBox.SelectedIndexCollection selectedIndices;

		// Token: 0x04001267 RID: 4711
		private ListBox.ObjectCollection itemsCollection;

		// Token: 0x04001268 RID: 4712
		private int itemHeight = 13;

		// Token: 0x04001269 RID: 4713
		private int columnWidth;

		// Token: 0x0400126A RID: 4714
		private int requestedHeight;

		// Token: 0x0400126B RID: 4715
		private int topIndex;

		// Token: 0x0400126C RID: 4716
		private int horizontalExtent;

		// Token: 0x0400126D RID: 4717
		private int maxWidth = -1;

		// Token: 0x0400126E RID: 4718
		private int updateCount;

		// Token: 0x0400126F RID: 4719
		private bool sorted;

		// Token: 0x04001270 RID: 4720
		private bool scrollAlwaysVisible;

		// Token: 0x04001271 RID: 4721
		private bool integralHeight = true;

		// Token: 0x04001272 RID: 4722
		private bool integralHeightAdjust;

		// Token: 0x04001273 RID: 4723
		private bool multiColumn;

		// Token: 0x04001274 RID: 4724
		private bool horizontalScrollbar;

		// Token: 0x04001275 RID: 4725
		private bool useTabStops = true;

		// Token: 0x04001276 RID: 4726
		private bool useCustomTabOffsets;

		// Token: 0x04001277 RID: 4727
		private bool fontIsChanged;

		// Token: 0x04001278 RID: 4728
		private bool doubleClickFired;

		// Token: 0x04001279 RID: 4729
		private bool selectedValueChangedFired;

		// Token: 0x0400127A RID: 4730
		private DrawMode drawMode;

		// Token: 0x0400127B RID: 4731
		private BorderStyle borderStyle = BorderStyle.Fixed3D;

		// Token: 0x0400127C RID: 4732
		private SelectionMode selectionMode = SelectionMode.One;

		// Token: 0x0400127D RID: 4733
		private SelectionMode cachedSelectionMode = SelectionMode.One;

		// Token: 0x0400127E RID: 4734
		private bool selectionModeChanging;

		// Token: 0x0400127F RID: 4735
		private ListBox.IntegerCollection customTabOffsets;

		// Token: 0x04001280 RID: 4736
		private const int defaultListItemStartPos = 1;

		// Token: 0x04001281 RID: 4737
		private const int defaultListItemBorderHeight = 1;

		// Token: 0x04001282 RID: 4738
		private const int defaultListItemPaddingBuffer = 3;

		// Token: 0x04001283 RID: 4739
		internal int scaledListItemStartPosition = 1;

		// Token: 0x04001284 RID: 4740
		internal int scaledListItemBordersHeight = 2;

		// Token: 0x04001285 RID: 4741
		internal int scaledListItemPaddingBuffer = 3;

		// Token: 0x020006C0 RID: 1728
		internal class ItemArray : IComparer
		{
			// Token: 0x0600693D RID: 26941 RVA: 0x001872B2 File Offset: 0x001854B2
			public ItemArray(ListControl listControl)
			{
				this.listControl = listControl;
			}

			// Token: 0x170016C2 RID: 5826
			// (get) Token: 0x0600693E RID: 26942 RVA: 0x001872C1 File Offset: 0x001854C1
			public int Version
			{
				get
				{
					return this.version;
				}
			}

			// Token: 0x0600693F RID: 26943 RVA: 0x001872CC File Offset: 0x001854CC
			public object Add(object item)
			{
				this.EnsureSpace(1);
				this.version++;
				this.entries[this.count] = new ListBox.ItemArray.Entry(item);
				ListBox.ItemArray.Entry[] array = this.entries;
				int num = this.count;
				this.count = num + 1;
				return array[num];
			}

			// Token: 0x06006940 RID: 26944 RVA: 0x0018731C File Offset: 0x0018551C
			public void AddRange(ICollection items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				this.EnsureSpace(items.Count);
				foreach (object item in items)
				{
					ListBox.ItemArray.Entry[] array = this.entries;
					int num = this.count;
					this.count = num + 1;
					array[num] = new ListBox.ItemArray.Entry(item);
				}
				this.version++;
			}

			// Token: 0x06006941 RID: 26945 RVA: 0x001873AC File Offset: 0x001855AC
			public void Clear()
			{
				if (this.count > 0)
				{
					Array.Clear(this.entries, 0, this.count);
				}
				this.count = 0;
				this.version++;
			}

			// Token: 0x06006942 RID: 26946 RVA: 0x001873E0 File Offset: 0x001855E0
			public static int CreateMask()
			{
				int result = ListBox.ItemArray.lastMask;
				ListBox.ItemArray.lastMask <<= 1;
				return result;
			}

			// Token: 0x06006943 RID: 26947 RVA: 0x00187400 File Offset: 0x00185600
			private void EnsureSpace(int elements)
			{
				if (this.entries == null)
				{
					this.entries = new ListBox.ItemArray.Entry[Math.Max(elements, 4)];
					return;
				}
				if (this.count + elements >= this.entries.Length)
				{
					int num = Math.Max(this.entries.Length * 2, this.entries.Length + elements);
					ListBox.ItemArray.Entry[] array = new ListBox.ItemArray.Entry[num];
					this.entries.CopyTo(array, 0);
					this.entries = array;
				}
			}

			// Token: 0x06006944 RID: 26948 RVA: 0x00187470 File Offset: 0x00185670
			public int GetActualIndex(int virtualIndex, int stateMask)
			{
				if (stateMask == 0)
				{
					return virtualIndex;
				}
				int num = -1;
				for (int i = 0; i < this.count; i++)
				{
					if ((this.entries[i].state & stateMask) != 0)
					{
						num++;
						if (num == virtualIndex)
						{
							return i;
						}
					}
				}
				return -1;
			}

			// Token: 0x06006945 RID: 26949 RVA: 0x001874B4 File Offset: 0x001856B4
			public int GetCount(int stateMask)
			{
				if (stateMask == 0)
				{
					return this.count;
				}
				int num = 0;
				for (int i = 0; i < this.count; i++)
				{
					if ((this.entries[i].state & stateMask) != 0)
					{
						num++;
					}
				}
				return num;
			}

			// Token: 0x06006946 RID: 26950 RVA: 0x001874F4 File Offset: 0x001856F4
			public IEnumerator GetEnumerator(int stateMask)
			{
				return this.GetEnumerator(stateMask, false);
			}

			// Token: 0x06006947 RID: 26951 RVA: 0x001874FE File Offset: 0x001856FE
			public IEnumerator GetEnumerator(int stateMask, bool anyBit)
			{
				return new ListBox.ItemArray.EntryEnumerator(this, stateMask, anyBit);
			}

			// Token: 0x06006948 RID: 26952 RVA: 0x00187508 File Offset: 0x00185708
			public object GetItem(int virtualIndex, int stateMask)
			{
				int actualIndex = this.GetActualIndex(virtualIndex, stateMask);
				if (actualIndex == -1)
				{
					throw new IndexOutOfRangeException();
				}
				return this.entries[actualIndex].item;
			}

			// Token: 0x06006949 RID: 26953 RVA: 0x00187538 File Offset: 0x00185738
			internal object GetEntryObject(int virtualIndex, int stateMask)
			{
				int actualIndex = this.GetActualIndex(virtualIndex, stateMask);
				if (actualIndex == -1)
				{
					throw new IndexOutOfRangeException();
				}
				return this.entries[actualIndex];
			}

			// Token: 0x0600694A RID: 26954 RVA: 0x00187560 File Offset: 0x00185760
			public bool GetState(int index, int stateMask)
			{
				return (this.entries[index].state & stateMask) == stateMask;
			}

			// Token: 0x0600694B RID: 26955 RVA: 0x00187574 File Offset: 0x00185774
			public int IndexOf(object item, int stateMask)
			{
				int num = -1;
				for (int i = 0; i < this.count; i++)
				{
					if (stateMask == 0 || (this.entries[i].state & stateMask) != 0)
					{
						num++;
						if (this.entries[i].item.Equals(item))
						{
							return num;
						}
					}
				}
				return -1;
			}

			// Token: 0x0600694C RID: 26956 RVA: 0x001875C4 File Offset: 0x001857C4
			public int IndexOfIdentifier(object identifier, int stateMask)
			{
				int num = -1;
				for (int i = 0; i < this.count; i++)
				{
					if (stateMask == 0 || (this.entries[i].state & stateMask) != 0)
					{
						num++;
						if (this.entries[i] == identifier)
						{
							return num;
						}
					}
				}
				return -1;
			}

			// Token: 0x0600694D RID: 26957 RVA: 0x0018760C File Offset: 0x0018580C
			public void Insert(int index, object item)
			{
				this.EnsureSpace(1);
				if (index < this.count)
				{
					Array.Copy(this.entries, index, this.entries, index + 1, this.count - index);
				}
				this.entries[index] = new ListBox.ItemArray.Entry(item);
				this.count++;
				this.version++;
			}

			// Token: 0x0600694E RID: 26958 RVA: 0x00187670 File Offset: 0x00185870
			public void Remove(object item)
			{
				int num = this.IndexOf(item, 0);
				if (num != -1)
				{
					this.RemoveAt(num);
				}
			}

			// Token: 0x0600694F RID: 26959 RVA: 0x00187694 File Offset: 0x00185894
			public void RemoveAt(int index)
			{
				this.count--;
				for (int i = index; i < this.count; i++)
				{
					this.entries[i] = this.entries[i + 1];
				}
				this.entries[this.count] = null;
				this.version++;
			}

			// Token: 0x06006950 RID: 26960 RVA: 0x001876EE File Offset: 0x001858EE
			public void SetItem(int index, object item)
			{
				this.entries[index].item = item;
			}

			// Token: 0x06006951 RID: 26961 RVA: 0x001876FE File Offset: 0x001858FE
			public void SetState(int index, int stateMask, bool value)
			{
				if (value)
				{
					this.entries[index].state |= stateMask;
				}
				else
				{
					this.entries[index].state &= ~stateMask;
				}
				this.version++;
			}

			// Token: 0x06006952 RID: 26962 RVA: 0x0018773E File Offset: 0x0018593E
			public int BinarySearch(object element)
			{
				return Array.BinarySearch(this.entries, 0, this.count, element, this);
			}

			// Token: 0x06006953 RID: 26963 RVA: 0x00187754 File Offset: 0x00185954
			public void Sort()
			{
				Array.Sort(this.entries, 0, this.count, this);
			}

			// Token: 0x06006954 RID: 26964 RVA: 0x00187769 File Offset: 0x00185969
			public void Sort(Array externalArray)
			{
				Array.Sort(externalArray, this);
			}

			// Token: 0x06006955 RID: 26965 RVA: 0x00187774 File Offset: 0x00185974
			int IComparer.Compare(object item1, object item2)
			{
				if (item1 == null)
				{
					if (item2 == null)
					{
						return 0;
					}
					return -1;
				}
				else
				{
					if (item2 == null)
					{
						return 1;
					}
					if (item1 is ListBox.ItemArray.Entry)
					{
						item1 = ((ListBox.ItemArray.Entry)item1).item;
					}
					if (item2 is ListBox.ItemArray.Entry)
					{
						item2 = ((ListBox.ItemArray.Entry)item2).item;
					}
					string itemText = this.listControl.GetItemText(item1);
					string itemText2 = this.listControl.GetItemText(item2);
					CompareInfo compareInfo = Application.CurrentCulture.CompareInfo;
					return compareInfo.Compare(itemText, itemText2, CompareOptions.StringSort);
				}
			}

			// Token: 0x04003B2B RID: 15147
			private static int lastMask = 1;

			// Token: 0x04003B2C RID: 15148
			private ListControl listControl;

			// Token: 0x04003B2D RID: 15149
			private ListBox.ItemArray.Entry[] entries;

			// Token: 0x04003B2E RID: 15150
			private int count;

			// Token: 0x04003B2F RID: 15151
			private int version;

			// Token: 0x020008BF RID: 2239
			private class Entry
			{
				// Token: 0x060072E5 RID: 29413 RVA: 0x001A4A24 File Offset: 0x001A2C24
				public Entry(object item)
				{
					this.item = item;
					this.state = 0;
				}

				// Token: 0x04004538 RID: 17720
				public object item;

				// Token: 0x04004539 RID: 17721
				public int state;
			}

			// Token: 0x020008C0 RID: 2240
			private class EntryEnumerator : IEnumerator
			{
				// Token: 0x060072E6 RID: 29414 RVA: 0x001A4A3A File Offset: 0x001A2C3A
				public EntryEnumerator(ListBox.ItemArray items, int state, bool anyBit)
				{
					this.items = items;
					this.state = state;
					this.anyBit = anyBit;
					this.version = items.version;
					this.current = -1;
				}

				// Token: 0x060072E7 RID: 29415 RVA: 0x001A4A6C File Offset: 0x001A2C6C
				bool IEnumerator.MoveNext()
				{
					if (this.version != this.items.version)
					{
						throw new InvalidOperationException(SR.GetString("ListEnumVersionMismatch"));
					}
					while (this.current < this.items.count - 1)
					{
						this.current++;
						if (this.anyBit)
						{
							if ((this.items.entries[this.current].state & this.state) != 0)
							{
								return true;
							}
						}
						else if ((this.items.entries[this.current].state & this.state) == this.state)
						{
							return true;
						}
					}
					this.current = this.items.count;
					return false;
				}

				// Token: 0x060072E8 RID: 29416 RVA: 0x001A4B23 File Offset: 0x001A2D23
				void IEnumerator.Reset()
				{
					if (this.version != this.items.version)
					{
						throw new InvalidOperationException(SR.GetString("ListEnumVersionMismatch"));
					}
					this.current = -1;
				}

				// Token: 0x17001935 RID: 6453
				// (get) Token: 0x060072E9 RID: 29417 RVA: 0x001A4B50 File Offset: 0x001A2D50
				object IEnumerator.Current
				{
					get
					{
						if (this.current == -1 || this.current == this.items.count)
						{
							throw new InvalidOperationException(SR.GetString("ListEnumCurrentOutOfRange"));
						}
						return this.items.entries[this.current].item;
					}
				}

				// Token: 0x0400453A RID: 17722
				private ListBox.ItemArray items;

				// Token: 0x0400453B RID: 17723
				private bool anyBit;

				// Token: 0x0400453C RID: 17724
				private int state;

				// Token: 0x0400453D RID: 17725
				private int current;

				// Token: 0x0400453E RID: 17726
				private int version;
			}
		}

		// Token: 0x020006C1 RID: 1729
		[ListBindable(false)]
		public class ObjectCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06006957 RID: 26967 RVA: 0x001877F4 File Offset: 0x001859F4
			public ObjectCollection(ListBox owner)
			{
				this.owner = owner;
			}

			// Token: 0x06006958 RID: 26968 RVA: 0x00187803 File Offset: 0x00185A03
			public ObjectCollection(ListBox owner, ListBox.ObjectCollection value)
			{
				this.owner = owner;
				this.AddRange(value);
			}

			// Token: 0x06006959 RID: 26969 RVA: 0x00187819 File Offset: 0x00185A19
			public ObjectCollection(ListBox owner, object[] value)
			{
				this.owner = owner;
				this.AddRange(value);
			}

			// Token: 0x170016C3 RID: 5827
			// (get) Token: 0x0600695A RID: 26970 RVA: 0x0018782F File Offset: 0x00185A2F
			public int Count
			{
				get
				{
					return this.InnerArray.GetCount(0);
				}
			}

			// Token: 0x170016C4 RID: 5828
			// (get) Token: 0x0600695B RID: 26971 RVA: 0x0018783D File Offset: 0x00185A3D
			internal ListBox.ItemArray InnerArray
			{
				get
				{
					if (this.items == null)
					{
						this.items = new ListBox.ItemArray(this.owner);
					}
					return this.items;
				}
			}

			// Token: 0x170016C5 RID: 5829
			// (get) Token: 0x0600695C RID: 26972 RVA: 0x00006C59 File Offset: 0x00004E59
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x170016C6 RID: 5830
			// (get) Token: 0x0600695D RID: 26973 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170016C7 RID: 5831
			// (get) Token: 0x0600695E RID: 26974 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170016C8 RID: 5832
			// (get) Token: 0x0600695F RID: 26975 RVA: 0x00011A20 File Offset: 0x0000FC20
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06006960 RID: 26976 RVA: 0x00187860 File Offset: 0x00185A60
			public int Add(object item)
			{
				this.owner.CheckNoDataSource();
				int result = this.AddInternal(item);
				this.owner.UpdateHorizontalExtent();
				return result;
			}

			// Token: 0x06006961 RID: 26977 RVA: 0x0018788C File Offset: 0x00185A8C
			private int AddInternal(object item)
			{
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				int num = -1;
				if (!this.owner.sorted)
				{
					this.InnerArray.Add(item);
				}
				else
				{
					if (this.Count > 0)
					{
						num = this.InnerArray.BinarySearch(item);
						if (num < 0)
						{
							num = ~num;
						}
					}
					else
					{
						num = 0;
					}
					this.InnerArray.Insert(num, item);
				}
				bool flag = false;
				try
				{
					if (this.owner.sorted)
					{
						if (this.owner.IsHandleCreated)
						{
							this.owner.NativeInsert(num, item);
							this.owner.UpdateMaxItemWidth(item, false);
							if (this.owner.selectedItems != null)
							{
								this.owner.selectedItems.Dirty();
							}
						}
					}
					else
					{
						num = this.Count - 1;
						if (this.owner.IsHandleCreated)
						{
							this.owner.NativeAdd(item);
							this.owner.UpdateMaxItemWidth(item, false);
						}
					}
					flag = true;
				}
				finally
				{
					if (!flag)
					{
						this.InnerArray.Remove(item);
					}
				}
				return num;
			}

			// Token: 0x06006962 RID: 26978 RVA: 0x001879A0 File Offset: 0x00185BA0
			int IList.Add(object item)
			{
				return this.Add(item);
			}

			// Token: 0x06006963 RID: 26979 RVA: 0x001879A9 File Offset: 0x00185BA9
			public void AddRange(ListBox.ObjectCollection value)
			{
				this.owner.CheckNoDataSource();
				this.AddRangeInternal(value);
			}

			// Token: 0x06006964 RID: 26980 RVA: 0x001879A9 File Offset: 0x00185BA9
			public void AddRange(object[] items)
			{
				this.owner.CheckNoDataSource();
				this.AddRangeInternal(items);
			}

			// Token: 0x06006965 RID: 26981 RVA: 0x001879C0 File Offset: 0x00185BC0
			internal void AddRangeInternal(ICollection items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				this.owner.BeginUpdate();
				try
				{
					foreach (object item in items)
					{
						this.AddInternal(item);
					}
				}
				finally
				{
					this.owner.UpdateHorizontalExtent();
					this.owner.EndUpdate();
				}
			}

			// Token: 0x170016C9 RID: 5833
			[Browsable(false)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public virtual object this[int index]
			{
				get
				{
					if (index < 0 || index >= this.InnerArray.GetCount(0))
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					return this.InnerArray.GetItem(index, 0);
				}
				set
				{
					this.owner.CheckNoDataSource();
					this.SetItemInternal(index, value);
				}
			}

			// Token: 0x06006968 RID: 26984 RVA: 0x00187AC0 File Offset: 0x00185CC0
			public virtual void Clear()
			{
				this.owner.CheckNoDataSource();
				this.ClearInternal();
			}

			// Token: 0x06006969 RID: 26985 RVA: 0x00187AD4 File Offset: 0x00185CD4
			internal void ClearInternal()
			{
				int count = this.owner.Items.Count;
				for (int i = 0; i < count; i++)
				{
					this.owner.UpdateMaxItemWidth(this.InnerArray.GetItem(i, 0), true);
				}
				if (this.owner.IsHandleCreated)
				{
					this.owner.NativeClear();
				}
				this.InnerArray.Clear();
				this.owner.maxWidth = -1;
				this.owner.UpdateHorizontalExtent();
			}

			// Token: 0x0600696A RID: 26986 RVA: 0x00187B51 File Offset: 0x00185D51
			public bool Contains(object value)
			{
				return this.IndexOf(value) != -1;
			}

			// Token: 0x0600696B RID: 26987 RVA: 0x00187B60 File Offset: 0x00185D60
			public void CopyTo(object[] destination, int arrayIndex)
			{
				int count = this.InnerArray.GetCount(0);
				for (int i = 0; i < count; i++)
				{
					destination[i + arrayIndex] = this.InnerArray.GetItem(i, 0);
				}
			}

			// Token: 0x0600696C RID: 26988 RVA: 0x00187B98 File Offset: 0x00185D98
			void ICollection.CopyTo(Array destination, int index)
			{
				int count = this.InnerArray.GetCount(0);
				for (int i = 0; i < count; i++)
				{
					destination.SetValue(this.InnerArray.GetItem(i, 0), i + index);
				}
			}

			// Token: 0x0600696D RID: 26989 RVA: 0x00187BD4 File Offset: 0x00185DD4
			public IEnumerator GetEnumerator()
			{
				return this.InnerArray.GetEnumerator(0);
			}

			// Token: 0x0600696E RID: 26990 RVA: 0x00187BE2 File Offset: 0x00185DE2
			public int IndexOf(object value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				return this.InnerArray.IndexOf(value, 0);
			}

			// Token: 0x0600696F RID: 26991 RVA: 0x00187BFF File Offset: 0x00185DFF
			internal int IndexOfIdentifier(object value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				return this.InnerArray.IndexOfIdentifier(value, 0);
			}

			// Token: 0x06006970 RID: 26992 RVA: 0x00187C1C File Offset: 0x00185E1C
			public void Insert(int index, object item)
			{
				this.owner.CheckNoDataSource();
				if (index < 0 || index > this.InnerArray.GetCount(0))
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (item == null)
				{
					throw new ArgumentNullException("item");
				}
				if (this.owner.sorted)
				{
					this.Add(item);
				}
				else
				{
					this.InnerArray.Insert(index, item);
					if (this.owner.IsHandleCreated)
					{
						bool flag = false;
						try
						{
							this.owner.NativeInsert(index, item);
							this.owner.UpdateMaxItemWidth(item, false);
							flag = true;
						}
						finally
						{
							if (!flag)
							{
								this.InnerArray.RemoveAt(index);
							}
						}
					}
				}
				this.owner.UpdateHorizontalExtent();
			}

			// Token: 0x06006971 RID: 26993 RVA: 0x00187D04 File Offset: 0x00185F04
			public void Remove(object value)
			{
				int num = this.InnerArray.IndexOf(value, 0);
				if (num != -1)
				{
					this.RemoveAt(num);
				}
			}

			// Token: 0x06006972 RID: 26994 RVA: 0x00187D2C File Offset: 0x00185F2C
			public void RemoveAt(int index)
			{
				this.owner.CheckNoDataSource();
				if (index < 0 || index >= this.InnerArray.GetCount(0))
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.owner.UpdateMaxItemWidth(this.InnerArray.GetItem(index, 0), true);
				this.InnerArray.RemoveAt(index);
				if (this.owner.IsHandleCreated)
				{
					this.owner.NativeRemoveAt(index);
				}
				this.owner.UpdateHorizontalExtent();
			}

			// Token: 0x06006973 RID: 26995 RVA: 0x00187DD4 File Offset: 0x00185FD4
			internal void SetItemInternal(int index, object value)
			{
				if (value == null)
				{
					throw new ArgumentNullException("value");
				}
				if (index < 0 || index >= this.InnerArray.GetCount(0))
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.owner.UpdateMaxItemWidth(this.InnerArray.GetItem(index, 0), true);
				this.InnerArray.SetItem(index, value);
				if (this.owner.IsHandleCreated)
				{
					bool flag = this.owner.SelectedIndex == index;
					if (string.Compare(this.owner.GetItemText(value), this.owner.NativeGetItemText(index), true, CultureInfo.CurrentCulture) != 0)
					{
						this.owner.NativeRemoveAt(index);
						this.owner.SelectedItems.SetSelected(index, false);
						this.owner.NativeInsert(index, value);
						this.owner.UpdateMaxItemWidth(value, false);
						if (flag)
						{
							this.owner.SelectedIndex = index;
						}
					}
					else if (flag)
					{
						this.owner.OnSelectedIndexChanged(EventArgs.Empty);
					}
				}
				this.owner.UpdateHorizontalExtent();
			}

			// Token: 0x04003B30 RID: 15152
			private ListBox owner;

			// Token: 0x04003B31 RID: 15153
			private ListBox.ItemArray items;
		}

		// Token: 0x020006C2 RID: 1730
		public class IntegerCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06006974 RID: 26996 RVA: 0x00187F06 File Offset: 0x00186106
			public IntegerCollection(ListBox owner)
			{
				this.owner = owner;
			}

			// Token: 0x170016CA RID: 5834
			// (get) Token: 0x06006975 RID: 26997 RVA: 0x00187F15 File Offset: 0x00186115
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.count;
				}
			}

			// Token: 0x170016CB RID: 5835
			// (get) Token: 0x06006976 RID: 26998 RVA: 0x00006C59 File Offset: 0x00004E59
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x170016CC RID: 5836
			// (get) Token: 0x06006977 RID: 26999 RVA: 0x00013062 File Offset: 0x00011262
			bool ICollection.IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170016CD RID: 5837
			// (get) Token: 0x06006978 RID: 27000 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170016CE RID: 5838
			// (get) Token: 0x06006979 RID: 27001 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool IList.IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x0600697A RID: 27002 RVA: 0x00187F1D File Offset: 0x0018611D
			public bool Contains(int item)
			{
				return this.IndexOf(item) != -1;
			}

			// Token: 0x0600697B RID: 27003 RVA: 0x00187F2C File Offset: 0x0018612C
			bool IList.Contains(object item)
			{
				return item is int && this.Contains((int)item);
			}

			// Token: 0x0600697C RID: 27004 RVA: 0x00187F44 File Offset: 0x00186144
			public void Clear()
			{
				this.count = 0;
				this.innerArray = null;
			}

			// Token: 0x0600697D RID: 27005 RVA: 0x00187F54 File Offset: 0x00186154
			public int IndexOf(int item)
			{
				int num = -1;
				if (this.innerArray != null)
				{
					num = Array.IndexOf<int>(this.innerArray, item);
					if (num >= this.count)
					{
						num = -1;
					}
				}
				return num;
			}

			// Token: 0x0600697E RID: 27006 RVA: 0x00187F84 File Offset: 0x00186184
			int IList.IndexOf(object item)
			{
				if (item is int)
				{
					return this.IndexOf((int)item);
				}
				return -1;
			}

			// Token: 0x0600697F RID: 27007 RVA: 0x00187F9C File Offset: 0x0018619C
			private int AddInternal(int item)
			{
				this.EnsureSpace(1);
				int num = this.IndexOf(item);
				if (num == -1)
				{
					int[] array = this.innerArray;
					int num2 = this.count;
					this.count = num2 + 1;
					array[num2] = item;
					Array.Sort<int>(this.innerArray, 0, this.count);
					num = this.IndexOf(item);
				}
				return num;
			}

			// Token: 0x06006980 RID: 27008 RVA: 0x00187FF0 File Offset: 0x001861F0
			public int Add(int item)
			{
				int result = this.AddInternal(item);
				this.owner.UpdateCustomTabOffsets();
				return result;
			}

			// Token: 0x06006981 RID: 27009 RVA: 0x00188011 File Offset: 0x00186211
			int IList.Add(object item)
			{
				if (!(item is int))
				{
					throw new ArgumentException("item");
				}
				return this.Add((int)item);
			}

			// Token: 0x06006982 RID: 27010 RVA: 0x00188032 File Offset: 0x00186232
			public void AddRange(int[] items)
			{
				this.AddRangeInternal(items);
			}

			// Token: 0x06006983 RID: 27011 RVA: 0x00188032 File Offset: 0x00186232
			public void AddRange(ListBox.IntegerCollection value)
			{
				this.AddRangeInternal(value);
			}

			// Token: 0x06006984 RID: 27012 RVA: 0x0018803C File Offset: 0x0018623C
			private void AddRangeInternal(ICollection items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				this.owner.BeginUpdate();
				try
				{
					this.EnsureSpace(items.Count);
					foreach (object obj in items)
					{
						if (!(obj is int))
						{
							throw new ArgumentException("item");
						}
						this.AddInternal((int)obj);
					}
					this.owner.UpdateCustomTabOffsets();
				}
				finally
				{
					this.owner.EndUpdate();
				}
			}

			// Token: 0x06006985 RID: 27013 RVA: 0x001880F0 File Offset: 0x001862F0
			private void EnsureSpace(int elements)
			{
				if (this.innerArray == null)
				{
					this.innerArray = new int[Math.Max(elements, 4)];
					return;
				}
				if (this.count + elements >= this.innerArray.Length)
				{
					int num = Math.Max(this.innerArray.Length * 2, this.innerArray.Length + elements);
					int[] array = new int[num];
					this.innerArray.CopyTo(array, 0);
					this.innerArray = array;
				}
			}

			// Token: 0x06006986 RID: 27014 RVA: 0x0018815F File Offset: 0x0018635F
			void IList.Clear()
			{
				this.Clear();
			}

			// Token: 0x06006987 RID: 27015 RVA: 0x00188167 File Offset: 0x00186367
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException(SR.GetString("ListBoxCantInsertIntoIntegerCollection"));
			}

			// Token: 0x06006988 RID: 27016 RVA: 0x00188178 File Offset: 0x00186378
			void IList.Remove(object value)
			{
				if (!(value is int))
				{
					throw new ArgumentException("value");
				}
				this.Remove((int)value);
			}

			// Token: 0x06006989 RID: 27017 RVA: 0x00188199 File Offset: 0x00186399
			void IList.RemoveAt(int index)
			{
				this.RemoveAt(index);
			}

			// Token: 0x0600698A RID: 27018 RVA: 0x001881A4 File Offset: 0x001863A4
			public void Remove(int item)
			{
				int num = this.IndexOf(item);
				if (num != -1)
				{
					this.RemoveAt(num);
				}
			}

			// Token: 0x0600698B RID: 27019 RVA: 0x001881C4 File Offset: 0x001863C4
			public void RemoveAt(int index)
			{
				if (index < 0 || index >= this.count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.count--;
				for (int i = index; i < this.count; i++)
				{
					this.innerArray[i] = this.innerArray[i + 1];
				}
			}

			// Token: 0x170016CF RID: 5839
			public int this[int index]
			{
				get
				{
					return this.innerArray[index];
				}
				set
				{
					if (index < 0 || index >= this.count)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					this.innerArray[index] = value;
					this.owner.UpdateCustomTabOffsets();
				}
			}

			// Token: 0x170016D0 RID: 5840
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					if (!(value is int))
					{
						throw new ArgumentException("value");
					}
					this[index] = (int)value;
				}
			}

			// Token: 0x06006990 RID: 27024 RVA: 0x001882DC File Offset: 0x001864DC
			public void CopyTo(Array destination, int index)
			{
				int num = this.Count;
				for (int i = 0; i < num; i++)
				{
					destination.SetValue(this[i], i + index);
				}
			}

			// Token: 0x06006991 RID: 27025 RVA: 0x00188311 File Offset: 0x00186511
			IEnumerator IEnumerable.GetEnumerator()
			{
				return new ListBox.IntegerCollection.CustomTabOffsetsEnumerator(this);
			}

			// Token: 0x04003B32 RID: 15154
			private ListBox owner;

			// Token: 0x04003B33 RID: 15155
			private int[] innerArray;

			// Token: 0x04003B34 RID: 15156
			private int count;

			// Token: 0x020008C1 RID: 2241
			private class CustomTabOffsetsEnumerator : IEnumerator
			{
				// Token: 0x060072EA RID: 29418 RVA: 0x001A4BA0 File Offset: 0x001A2DA0
				public CustomTabOffsetsEnumerator(ListBox.IntegerCollection items)
				{
					this.items = items;
					this.current = -1;
				}

				// Token: 0x060072EB RID: 29419 RVA: 0x001A4BB6 File Offset: 0x001A2DB6
				bool IEnumerator.MoveNext()
				{
					if (this.current < this.items.Count - 1)
					{
						this.current++;
						return true;
					}
					this.current = this.items.Count;
					return false;
				}

				// Token: 0x060072EC RID: 29420 RVA: 0x001A4BEF File Offset: 0x001A2DEF
				void IEnumerator.Reset()
				{
					this.current = -1;
				}

				// Token: 0x17001936 RID: 6454
				// (get) Token: 0x060072ED RID: 29421 RVA: 0x001A4BF8 File Offset: 0x001A2DF8
				object IEnumerator.Current
				{
					get
					{
						if (this.current == -1 || this.current == this.items.Count)
						{
							throw new InvalidOperationException(SR.GetString("ListEnumCurrentOutOfRange"));
						}
						return this.items[this.current];
					}
				}

				// Token: 0x0400453F RID: 17727
				private ListBox.IntegerCollection items;

				// Token: 0x04004540 RID: 17728
				private int current;
			}
		}

		// Token: 0x020006C3 RID: 1731
		public class SelectedIndexCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06006992 RID: 27026 RVA: 0x00188319 File Offset: 0x00186519
			public SelectedIndexCollection(ListBox owner)
			{
				this.owner = owner;
			}

			// Token: 0x170016D1 RID: 5841
			// (get) Token: 0x06006993 RID: 27027 RVA: 0x00188328 File Offset: 0x00186528
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.owner.SelectedItems.Count;
				}
			}

			// Token: 0x170016D2 RID: 5842
			// (get) Token: 0x06006994 RID: 27028 RVA: 0x00006C59 File Offset: 0x00004E59
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x170016D3 RID: 5843
			// (get) Token: 0x06006995 RID: 27029 RVA: 0x00013062 File Offset: 0x00011262
			bool ICollection.IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170016D4 RID: 5844
			// (get) Token: 0x06006996 RID: 27030 RVA: 0x00013062 File Offset: 0x00011262
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170016D5 RID: 5845
			// (get) Token: 0x06006997 RID: 27031 RVA: 0x00013062 File Offset: 0x00011262
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x06006998 RID: 27032 RVA: 0x0018833A File Offset: 0x0018653A
			public bool Contains(int selectedIndex)
			{
				return this.IndexOf(selectedIndex) != -1;
			}

			// Token: 0x06006999 RID: 27033 RVA: 0x00188349 File Offset: 0x00186549
			bool IList.Contains(object selectedIndex)
			{
				return selectedIndex is int && this.Contains((int)selectedIndex);
			}

			// Token: 0x0600699A RID: 27034 RVA: 0x00188364 File Offset: 0x00186564
			public int IndexOf(int selectedIndex)
			{
				if (selectedIndex >= 0 && selectedIndex < this.InnerArray.GetCount(0) && this.InnerArray.GetState(selectedIndex, ListBox.SelectedObjectCollection.SelectedObjectMask))
				{
					return this.InnerArray.IndexOf(this.InnerArray.GetItem(selectedIndex, 0), ListBox.SelectedObjectCollection.SelectedObjectMask);
				}
				return -1;
			}

			// Token: 0x0600699B RID: 27035 RVA: 0x001883B6 File Offset: 0x001865B6
			int IList.IndexOf(object selectedIndex)
			{
				if (selectedIndex is int)
				{
					return this.IndexOf((int)selectedIndex);
				}
				return -1;
			}

			// Token: 0x0600699C RID: 27036 RVA: 0x001883CE File Offset: 0x001865CE
			int IList.Add(object value)
			{
				throw new NotSupportedException(SR.GetString("ListBoxSelectedIndexCollectionIsReadOnly"));
			}

			// Token: 0x0600699D RID: 27037 RVA: 0x001883CE File Offset: 0x001865CE
			void IList.Clear()
			{
				throw new NotSupportedException(SR.GetString("ListBoxSelectedIndexCollectionIsReadOnly"));
			}

			// Token: 0x0600699E RID: 27038 RVA: 0x001883CE File Offset: 0x001865CE
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException(SR.GetString("ListBoxSelectedIndexCollectionIsReadOnly"));
			}

			// Token: 0x0600699F RID: 27039 RVA: 0x001883CE File Offset: 0x001865CE
			void IList.Remove(object value)
			{
				throw new NotSupportedException(SR.GetString("ListBoxSelectedIndexCollectionIsReadOnly"));
			}

			// Token: 0x060069A0 RID: 27040 RVA: 0x001883CE File Offset: 0x001865CE
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException(SR.GetString("ListBoxSelectedIndexCollectionIsReadOnly"));
			}

			// Token: 0x170016D6 RID: 5846
			public int this[int index]
			{
				get
				{
					object entryObject = this.InnerArray.GetEntryObject(index, ListBox.SelectedObjectCollection.SelectedObjectMask);
					return this.InnerArray.IndexOfIdentifier(entryObject, 0);
				}
			}

			// Token: 0x170016D7 RID: 5847
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotSupportedException(SR.GetString("ListBoxSelectedIndexCollectionIsReadOnly"));
				}
			}

			// Token: 0x170016D8 RID: 5848
			// (get) Token: 0x060069A4 RID: 27044 RVA: 0x0018841A File Offset: 0x0018661A
			private ListBox.ItemArray InnerArray
			{
				get
				{
					this.owner.SelectedItems.EnsureUpToDate();
					return this.owner.Items.InnerArray;
				}
			}

			// Token: 0x060069A5 RID: 27045 RVA: 0x0018843C File Offset: 0x0018663C
			public void CopyTo(Array destination, int index)
			{
				int count = this.Count;
				for (int i = 0; i < count; i++)
				{
					destination.SetValue(this[i], i + index);
				}
			}

			// Token: 0x060069A6 RID: 27046 RVA: 0x00188471 File Offset: 0x00186671
			public void Clear()
			{
				if (this.owner != null)
				{
					this.owner.ClearSelected();
				}
			}

			// Token: 0x060069A7 RID: 27047 RVA: 0x00188488 File Offset: 0x00186688
			public void Add(int index)
			{
				if (this.owner != null)
				{
					ListBox.ObjectCollection items = this.owner.Items;
					if (items != null && index != -1 && !this.Contains(index))
					{
						this.owner.SetSelected(index, true);
					}
				}
			}

			// Token: 0x060069A8 RID: 27048 RVA: 0x001884C8 File Offset: 0x001866C8
			public void Remove(int index)
			{
				if (this.owner != null)
				{
					ListBox.ObjectCollection items = this.owner.Items;
					if (items != null && index != -1 && this.Contains(index))
					{
						this.owner.SetSelected(index, false);
					}
				}
			}

			// Token: 0x060069A9 RID: 27049 RVA: 0x00188506 File Offset: 0x00186706
			public IEnumerator GetEnumerator()
			{
				return new ListBox.SelectedIndexCollection.SelectedIndexEnumerator(this);
			}

			// Token: 0x04003B35 RID: 15157
			private ListBox owner;

			// Token: 0x020008C2 RID: 2242
			private class SelectedIndexEnumerator : IEnumerator
			{
				// Token: 0x060072EE RID: 29422 RVA: 0x001A4C47 File Offset: 0x001A2E47
				public SelectedIndexEnumerator(ListBox.SelectedIndexCollection items)
				{
					this.items = items;
					this.current = -1;
				}

				// Token: 0x060072EF RID: 29423 RVA: 0x001A4C5D File Offset: 0x001A2E5D
				bool IEnumerator.MoveNext()
				{
					if (this.current < this.items.Count - 1)
					{
						this.current++;
						return true;
					}
					this.current = this.items.Count;
					return false;
				}

				// Token: 0x060072F0 RID: 29424 RVA: 0x001A4C96 File Offset: 0x001A2E96
				void IEnumerator.Reset()
				{
					this.current = -1;
				}

				// Token: 0x17001937 RID: 6455
				// (get) Token: 0x060072F1 RID: 29425 RVA: 0x001A4CA0 File Offset: 0x001A2EA0
				object IEnumerator.Current
				{
					get
					{
						if (this.current == -1 || this.current == this.items.Count)
						{
							throw new InvalidOperationException(SR.GetString("ListEnumCurrentOutOfRange"));
						}
						return this.items[this.current];
					}
				}

				// Token: 0x04004541 RID: 17729
				private ListBox.SelectedIndexCollection items;

				// Token: 0x04004542 RID: 17730
				private int current;
			}
		}

		// Token: 0x020006C4 RID: 1732
		public class SelectedObjectCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x060069AA RID: 27050 RVA: 0x0018850E File Offset: 0x0018670E
			public SelectedObjectCollection(ListBox owner)
			{
				this.owner = owner;
				this.stateDirty = true;
				this.lastVersion = -1;
			}

			// Token: 0x170016D9 RID: 5849
			// (get) Token: 0x060069AB RID: 27051 RVA: 0x0018852C File Offset: 0x0018672C
			public int Count
			{
				get
				{
					if (!this.owner.IsHandleCreated)
					{
						if (this.lastVersion != this.InnerArray.Version)
						{
							this.lastVersion = this.InnerArray.Version;
							this.count = this.InnerArray.GetCount(ListBox.SelectedObjectCollection.SelectedObjectMask);
						}
						return this.count;
					}
					switch (this.owner.selectionModeChanging ? this.owner.cachedSelectionMode : this.owner.selectionMode)
					{
					case SelectionMode.None:
						return 0;
					case SelectionMode.One:
					{
						int selectedIndex = this.owner.SelectedIndex;
						if (selectedIndex >= 0)
						{
							return 1;
						}
						return 0;
					}
					case SelectionMode.MultiSimple:
					case SelectionMode.MultiExtended:
						return (int)((long)this.owner.SendMessage(400, 0, 0));
					default:
						return 0;
					}
				}
			}

			// Token: 0x170016DA RID: 5850
			// (get) Token: 0x060069AC RID: 27052 RVA: 0x00006C59 File Offset: 0x00004E59
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x170016DB RID: 5851
			// (get) Token: 0x060069AD RID: 27053 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170016DC RID: 5852
			// (get) Token: 0x060069AE RID: 27054 RVA: 0x00013062 File Offset: 0x00011262
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060069AF RID: 27055 RVA: 0x001885F5 File Offset: 0x001867F5
			internal void Dirty()
			{
				this.stateDirty = true;
			}

			// Token: 0x170016DD RID: 5853
			// (get) Token: 0x060069B0 RID: 27056 RVA: 0x001885FE File Offset: 0x001867FE
			private ListBox.ItemArray InnerArray
			{
				get
				{
					this.EnsureUpToDate();
					return this.owner.Items.InnerArray;
				}
			}

			// Token: 0x060069B1 RID: 27057 RVA: 0x00188616 File Offset: 0x00186816
			internal void EnsureUpToDate()
			{
				if (this.stateDirty)
				{
					this.stateDirty = false;
					if (this.owner.IsHandleCreated)
					{
						this.owner.NativeUpdateSelection();
					}
				}
			}

			// Token: 0x170016DE RID: 5854
			// (get) Token: 0x060069B2 RID: 27058 RVA: 0x00013062 File Offset: 0x00011262
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060069B3 RID: 27059 RVA: 0x0018863F File Offset: 0x0018683F
			public bool Contains(object selectedObject)
			{
				return this.IndexOf(selectedObject) != -1;
			}

			// Token: 0x060069B4 RID: 27060 RVA: 0x0018864E File Offset: 0x0018684E
			public int IndexOf(object selectedObject)
			{
				return this.InnerArray.IndexOf(selectedObject, ListBox.SelectedObjectCollection.SelectedObjectMask);
			}

			// Token: 0x060069B5 RID: 27061 RVA: 0x00188661 File Offset: 0x00186861
			int IList.Add(object value)
			{
				throw new NotSupportedException(SR.GetString("ListBoxSelectedObjectCollectionIsReadOnly"));
			}

			// Token: 0x060069B6 RID: 27062 RVA: 0x00188661 File Offset: 0x00186861
			void IList.Clear()
			{
				throw new NotSupportedException(SR.GetString("ListBoxSelectedObjectCollectionIsReadOnly"));
			}

			// Token: 0x060069B7 RID: 27063 RVA: 0x00188661 File Offset: 0x00186861
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException(SR.GetString("ListBoxSelectedObjectCollectionIsReadOnly"));
			}

			// Token: 0x060069B8 RID: 27064 RVA: 0x00188661 File Offset: 0x00186861
			void IList.Remove(object value)
			{
				throw new NotSupportedException(SR.GetString("ListBoxSelectedObjectCollectionIsReadOnly"));
			}

			// Token: 0x060069B9 RID: 27065 RVA: 0x00188661 File Offset: 0x00186861
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException(SR.GetString("ListBoxSelectedObjectCollectionIsReadOnly"));
			}

			// Token: 0x060069BA RID: 27066 RVA: 0x00188672 File Offset: 0x00186872
			internal object GetObjectAt(int index)
			{
				return this.InnerArray.GetEntryObject(index, ListBox.SelectedObjectCollection.SelectedObjectMask);
			}

			// Token: 0x170016DF RID: 5855
			[Browsable(false)]
			[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
			public object this[int index]
			{
				get
				{
					return this.InnerArray.GetItem(index, ListBox.SelectedObjectCollection.SelectedObjectMask);
				}
				set
				{
					throw new NotSupportedException(SR.GetString("ListBoxSelectedObjectCollectionIsReadOnly"));
				}
			}

			// Token: 0x060069BD RID: 27069 RVA: 0x00188698 File Offset: 0x00186898
			public void CopyTo(Array destination, int index)
			{
				int num = this.InnerArray.GetCount(ListBox.SelectedObjectCollection.SelectedObjectMask);
				for (int i = 0; i < num; i++)
				{
					destination.SetValue(this.InnerArray.GetItem(i, ListBox.SelectedObjectCollection.SelectedObjectMask), i + index);
				}
			}

			// Token: 0x060069BE RID: 27070 RVA: 0x001886DC File Offset: 0x001868DC
			public IEnumerator GetEnumerator()
			{
				return this.InnerArray.GetEnumerator(ListBox.SelectedObjectCollection.SelectedObjectMask);
			}

			// Token: 0x060069BF RID: 27071 RVA: 0x001886EE File Offset: 0x001868EE
			internal bool GetSelected(int index)
			{
				return this.InnerArray.GetState(index, ListBox.SelectedObjectCollection.SelectedObjectMask);
			}

			// Token: 0x060069C0 RID: 27072 RVA: 0x00188704 File Offset: 0x00186904
			internal void PushSelectionIntoNativeListBox(int index)
			{
				bool state = this.owner.Items.InnerArray.GetState(index, ListBox.SelectedObjectCollection.SelectedObjectMask);
				if (state)
				{
					this.owner.NativeSetSelected(index, true);
				}
			}

			// Token: 0x060069C1 RID: 27073 RVA: 0x0018873D File Offset: 0x0018693D
			internal void SetSelected(int index, bool value)
			{
				this.InnerArray.SetState(index, ListBox.SelectedObjectCollection.SelectedObjectMask, value);
			}

			// Token: 0x060069C2 RID: 27074 RVA: 0x00188751 File Offset: 0x00186951
			public void Clear()
			{
				if (this.owner != null)
				{
					this.owner.ClearSelected();
				}
			}

			// Token: 0x060069C3 RID: 27075 RVA: 0x00188768 File Offset: 0x00186968
			public void Add(object value)
			{
				if (this.owner != null)
				{
					ListBox.ObjectCollection items = this.owner.Items;
					if (items != null && value != null)
					{
						int num = items.IndexOf(value);
						if (num != -1 && !this.GetSelected(num))
						{
							this.owner.SelectedIndex = num;
						}
					}
				}
			}

			// Token: 0x060069C4 RID: 27076 RVA: 0x001887B0 File Offset: 0x001869B0
			public void Remove(object value)
			{
				if (this.owner != null)
				{
					ListBox.ObjectCollection items = this.owner.Items;
					if (items != null & value != null)
					{
						int num = items.IndexOf(value);
						if (num != -1 && this.GetSelected(num))
						{
							this.owner.SetSelected(num, false);
						}
					}
				}
			}

			// Token: 0x04003B36 RID: 15158
			internal static int SelectedObjectMask = ListBox.ItemArray.CreateMask();

			// Token: 0x04003B37 RID: 15159
			private ListBox owner;

			// Token: 0x04003B38 RID: 15160
			private bool stateDirty;

			// Token: 0x04003B39 RID: 15161
			private int lastVersion;

			// Token: 0x04003B3A RID: 15162
			private int count;
		}

		// Token: 0x020006C5 RID: 1733
		private sealed class ListBoxAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x060069C6 RID: 27078 RVA: 0x0009B963 File Offset: 0x00099B63
			public ListBoxAccessibleObject(ListBox control) : base(control)
			{
			}

			// Token: 0x060069C7 RID: 27079 RVA: 0x00162A9D File Offset: 0x00160C9D
			internal override bool IsIAccessibleExSupported()
			{
				return !base.IsOwnerControlDestroyed();
			}

			// Token: 0x060069C8 RID: 27080 RVA: 0x0018880C File Offset: 0x00186A0C
			internal override object GetObjectForChild(int childId)
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				IAccessible systemIAccessibleInternal = base.GetSystemIAccessibleInternal();
				if (ListBox.ListBoxAccessibleObject.IsChildIdValid(childId, systemIAccessibleInternal) && (AccessibleRole)systemIAccessibleInternal.get_accRole(childId) == AccessibleRole.ListItem)
				{
					return new ListBox.ListBoxAccessibleObject.ListBoxItemAccessibleObject(this, childId);
				}
				return base.GetObjectForChild(childId);
			}

			// Token: 0x060069C9 RID: 27081 RVA: 0x00188857 File Offset: 0x00186A57
			private static bool IsChildIdValid(int childId, IAccessible systemIAccessible)
			{
				return childId > 0 && childId <= systemIAccessible.accChildCount;
			}

			// Token: 0x020008C3 RID: 2243
			private sealed class ListBoxItemAccessibleObject : AccessibleObject
			{
				// Token: 0x060072F2 RID: 29426 RVA: 0x001A4CEF File Offset: 0x001A2EEF
				public ListBoxItemAccessibleObject(ListBox.ListBoxAccessibleObject owner, int childId)
				{
					this.ownerObject = owner;
					this.childId = childId;
				}

				// Token: 0x060072F3 RID: 29427 RVA: 0x001A4D05 File Offset: 0x001A2F05
				internal override bool IsIAccessibleExSupported()
				{
					return !this.ownerObject.IsOwnerControlDestroyed();
				}

				// Token: 0x060072F4 RID: 29428 RVA: 0x001A4D15 File Offset: 0x001A2F15
				internal override bool IsPatternSupported(int patternId)
				{
					return !this.ownerObject.IsOwnerControlDestroyed() && (patternId == 10017 || base.IsPatternSupported(patternId));
				}

				// Token: 0x060072F5 RID: 29429 RVA: 0x001A4D38 File Offset: 0x001A2F38
				internal override void ScrollIntoView()
				{
					if (!this.ownerObject.IsOwnerControlDestroyed())
					{
						ListBox listBox = this.ownerObject.Owner as ListBox;
						if (listBox != null)
						{
							if (listBox.IsHandleCreated && ListBox.ListBoxAccessibleObject.IsChildIdValid(this.childId, listBox.AccessibilityObject.GetSystemIAccessibleInternal()))
							{
								listBox.TopIndex = this.childId - 1;
							}
							return;
						}
					}
				}

				// Token: 0x04004543 RID: 17731
				private readonly int childId;

				// Token: 0x04004544 RID: 17732
				private readonly ListBox.ListBoxAccessibleObject ownerObject;
			}
		}
	}
}

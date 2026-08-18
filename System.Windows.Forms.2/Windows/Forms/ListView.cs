using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Imaging;
using System.Globalization;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Security.Permissions;
using System.Text;
using System.Windows.Forms.Layout;
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x020002D4 RID: 724
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Docking(DockingBehavior.Ask)]
	[Designer("System.Windows.Forms.Design.ListViewDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[DefaultProperty("Items")]
	[DefaultEvent("SelectedIndexChanged")]
	[SRDescription("DescriptionListView")]
	public class ListView : Control
	{
		// Token: 0x06002CC0 RID: 11456 RVA: 0x000C91B8 File Offset: 0x000C73B8
		public ListView()
		{
			int num = 8392196;
			if (!AccessibilityImprovements.Level3)
			{
				num |= 64;
			}
			this.listViewState = new BitVector32(num);
			this.listViewState1 = new BitVector32(8);
			base.SetStyle(ControlStyles.UserPaint, false);
			base.SetStyle(ControlStyles.StandardClick, false);
			base.SetStyle(ControlStyles.UseTextForAccessibility, false);
			this.odCacheFont = this.Font;
			this.odCacheFontHandle = base.FontHandle;
			base.SetBounds(0, 0, 121, 97);
			this.listItemCollection = new ListView.ListViewItemCollection(new ListView.ListViewNativeItemCollection(this));
			this.columnHeaderCollection = new ListView.ColumnHeaderCollection(this);
		}

		// Token: 0x17000A77 RID: 2679
		// (get) Token: 0x06002CC1 RID: 11457 RVA: 0x000C92D1 File Offset: 0x000C74D1
		// (set) Token: 0x06002CC2 RID: 11458 RVA: 0x000C92DC File Offset: 0x000C74DC
		[SRCategory("CatBehavior")]
		[DefaultValue(ItemActivation.Standard)]
		[SRDescription("ListViewActivationDescr")]
		public ItemActivation Activation
		{
			get
			{
				return this.activation;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ItemActivation));
				}
				if (this.HotTracking && value != ItemActivation.OneClick)
				{
					throw new ArgumentException(SR.GetString("ListViewActivationMustBeOnWhenHotTrackingIsOn"), "value");
				}
				if (this.activation != value)
				{
					this.activation = value;
					this.UpdateExtendedStyles();
				}
			}
		}

		// Token: 0x17000A78 RID: 2680
		// (get) Token: 0x06002CC3 RID: 11459 RVA: 0x000C9346 File Offset: 0x000C7546
		// (set) Token: 0x06002CC4 RID: 11460 RVA: 0x000C9350 File Offset: 0x000C7550
		[SRCategory("CatBehavior")]
		[DefaultValue(ListViewAlignment.Top)]
		[Localizable(true)]
		[SRDescription("ListViewAlignmentDescr")]
		public ListViewAlignment Alignment
		{
			get
			{
				return this.alignStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid_NotSequential(value, (int)value, new int[]
				{
					0,
					2,
					1,
					5
				}))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ListViewAlignment));
				}
				if (this.alignStyle != value)
				{
					this.alignStyle = value;
					this.RecreateHandleInternal();
				}
			}
		}

		// Token: 0x17000A79 RID: 2681
		// (get) Token: 0x06002CC5 RID: 11461 RVA: 0x000C93A8 File Offset: 0x000C75A8
		// (set) Token: 0x06002CC6 RID: 11462 RVA: 0x000C93B6 File Offset: 0x000C75B6
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("ListViewAllowColumnReorderDescr")]
		public bool AllowColumnReorder
		{
			get
			{
				return this.listViewState[2];
			}
			set
			{
				if (this.AllowColumnReorder != value)
				{
					this.listViewState[2] = value;
					this.UpdateExtendedStyles();
				}
			}
		}

		// Token: 0x17000A7A RID: 2682
		// (get) Token: 0x06002CC7 RID: 11463 RVA: 0x000C93D4 File Offset: 0x000C75D4
		// (set) Token: 0x06002CC8 RID: 11464 RVA: 0x000C93E2 File Offset: 0x000C75E2
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("ListViewAutoArrangeDescr")]
		public bool AutoArrange
		{
			get
			{
				return this.listViewState[4];
			}
			set
			{
				if (this.AutoArrange != value)
				{
					this.listViewState[4] = value;
					base.UpdateStyles();
				}
			}
		}

		// Token: 0x17000A7B RID: 2683
		// (get) Token: 0x06002CC9 RID: 11465 RVA: 0x00027F43 File Offset: 0x00026143
		// (set) Token: 0x06002CCA RID: 11466 RVA: 0x000C9400 File Offset: 0x000C7600
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
				if (base.IsHandleCreated)
				{
					base.SendMessage(4097, 0, ColorTranslator.ToWin32(this.BackColor));
				}
			}
		}

		// Token: 0x17000A7C RID: 2684
		// (get) Token: 0x06002CCB RID: 11467 RVA: 0x00011AB3 File Offset: 0x0000FCB3
		// (set) Token: 0x06002CCC RID: 11468 RVA: 0x00011ABB File Offset: 0x0000FCBB
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

		// Token: 0x14000208 RID: 520
		// (add) Token: 0x06002CCD RID: 11469 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		// (remove) Token: 0x06002CCE RID: 11470 RVA: 0x00011ACD File Offset: 0x0000FCCD
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

		// Token: 0x17000A7D RID: 2685
		// (get) Token: 0x06002CCF RID: 11471 RVA: 0x000C9429 File Offset: 0x000C7629
		// (set) Token: 0x06002CD0 RID: 11472 RVA: 0x000C943C File Offset: 0x000C763C
		[SRCategory("CatAppearance")]
		[DefaultValue(false)]
		[SRDescription("ListViewBackgroundImageTiledDescr")]
		public bool BackgroundImageTiled
		{
			get
			{
				return this.listViewState[65536];
			}
			set
			{
				if (this.BackgroundImageTiled != value)
				{
					this.listViewState[65536] = value;
					if (base.IsHandleCreated && this.BackgroundImage != null)
					{
						NativeMethods.LVBKIMAGE lvbkimage = new NativeMethods.LVBKIMAGE();
						lvbkimage.xOffset = 0;
						lvbkimage.yOffset = 0;
						if (this.BackgroundImageTiled)
						{
							lvbkimage.ulFlags = 16;
						}
						else
						{
							lvbkimage.ulFlags = 0;
						}
						lvbkimage.ulFlags |= 2;
						lvbkimage.pszImage = this.backgroundImageFileName;
						lvbkimage.cchImageMax = this.backgroundImageFileName.Length + 1;
						UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.LVM_SETBKIMAGE, 0, lvbkimage);
					}
				}
			}
		}

		// Token: 0x17000A7E RID: 2686
		// (get) Token: 0x06002CD1 RID: 11473 RVA: 0x000C94E9 File Offset: 0x000C76E9
		// (set) Token: 0x06002CD2 RID: 11474 RVA: 0x000C94F1 File Offset: 0x000C76F1
		[SRCategory("CatAppearance")]
		[DefaultValue(BorderStyle.Fixed3D)]
		[DispId(-504)]
		[SRDescription("borderStyleDescr")]
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
					base.UpdateStyles();
				}
			}
		}

		// Token: 0x17000A7F RID: 2687
		// (get) Token: 0x06002CD3 RID: 11475 RVA: 0x000C952F File Offset: 0x000C772F
		// (set) Token: 0x06002CD4 RID: 11476 RVA: 0x000C9540 File Offset: 0x000C7740
		[SRCategory("CatAppearance")]
		[DefaultValue(false)]
		[SRDescription("ListViewCheckBoxesDescr")]
		public bool CheckBoxes
		{
			get
			{
				return this.listViewState[8];
			}
			set
			{
				if (this.UseCompatibleStateImageBehavior)
				{
					if (this.CheckBoxes != value)
					{
						if (value && this.View == View.Tile)
						{
							throw new NotSupportedException(SR.GetString("ListViewCheckBoxesNotSupportedInTileView"));
						}
						if (this.CheckBoxes)
						{
							this.savedCheckedItems = new List<ListViewItem>(this.CheckedItems.Count);
							ListViewItem[] array = new ListViewItem[this.CheckedItems.Count];
							this.CheckedItems.CopyTo(array, 0);
							for (int i = 0; i < array.Length; i++)
							{
								this.savedCheckedItems.Add(array[i]);
							}
						}
						this.listViewState[8] = value;
						this.UpdateExtendedStyles();
						if (this.CheckBoxes && this.savedCheckedItems != null)
						{
							if (this.savedCheckedItems.Count > 0)
							{
								foreach (ListViewItem listViewItem in this.savedCheckedItems)
								{
									listViewItem.Checked = true;
								}
							}
							this.savedCheckedItems = null;
						}
						if (this.AutoArrange)
						{
							this.ArrangeIcons(this.Alignment);
							return;
						}
					}
				}
				else if (this.CheckBoxes != value)
				{
					if (value && this.View == View.Tile)
					{
						throw new NotSupportedException(SR.GetString("ListViewCheckBoxesNotSupportedInTileView"));
					}
					if (this.CheckBoxes)
					{
						this.savedCheckedItems = new List<ListViewItem>(this.CheckedItems.Count);
						ListViewItem[] array2 = new ListViewItem[this.CheckedItems.Count];
						this.CheckedItems.CopyTo(array2, 0);
						for (int j = 0; j < array2.Length; j++)
						{
							this.savedCheckedItems.Add(array2[j]);
						}
					}
					this.listViewState[8] = value;
					if ((!value && this.StateImageList != null && base.IsHandleCreated) || (!value && this.Alignment == ListViewAlignment.Left && base.IsHandleCreated) || (value && this.View == View.List && base.IsHandleCreated) || (value && (this.View == View.SmallIcon || this.View == View.LargeIcon) && base.IsHandleCreated))
					{
						this.RecreateHandleInternal();
					}
					else
					{
						this.UpdateExtendedStyles();
					}
					if (this.CheckBoxes && this.savedCheckedItems != null)
					{
						if (this.savedCheckedItems.Count > 0)
						{
							foreach (ListViewItem listViewItem2 in this.savedCheckedItems)
							{
								listViewItem2.Checked = true;
							}
						}
						this.savedCheckedItems = null;
					}
					if (base.IsHandleCreated && this.imageListState != null)
					{
						if (this.CheckBoxes)
						{
							base.SendMessage(4099, 2, this.imageListState.Handle);
						}
						else
						{
							base.SendMessage(4099, 2, IntPtr.Zero);
						}
					}
					if (this.AutoArrange)
					{
						this.ArrangeIcons(this.Alignment);
					}
				}
			}
		}

		// Token: 0x17000A80 RID: 2688
		// (get) Token: 0x06002CD5 RID: 11477 RVA: 0x000C982C File Offset: 0x000C7A2C
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ListView.CheckedIndexCollection CheckedIndices
		{
			get
			{
				if (this.checkedIndexCollection == null)
				{
					this.checkedIndexCollection = new ListView.CheckedIndexCollection(this);
				}
				return this.checkedIndexCollection;
			}
		}

		// Token: 0x17000A81 RID: 2689
		// (get) Token: 0x06002CD6 RID: 11478 RVA: 0x000C9848 File Offset: 0x000C7A48
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ListView.CheckedListViewItemCollection CheckedItems
		{
			get
			{
				if (this.checkedListViewItemCollection == null)
				{
					this.checkedListViewItemCollection = new ListView.CheckedListViewItemCollection(this);
				}
				return this.checkedListViewItemCollection;
			}
		}

		// Token: 0x17000A82 RID: 2690
		// (get) Token: 0x06002CD7 RID: 11479 RVA: 0x000C9864 File Offset: 0x000C7A64
		[SRCategory("CatBehavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Editor("System.Windows.Forms.Design.ColumnHeaderCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SRDescription("ListViewColumnsDescr")]
		[Localizable(true)]
		[MergableProperty(false)]
		public ListView.ColumnHeaderCollection Columns
		{
			get
			{
				return this.columnHeaderCollection;
			}
		}

		// Token: 0x17000A83 RID: 2691
		// (get) Token: 0x06002CD8 RID: 11480 RVA: 0x000C986C File Offset: 0x000C7A6C
		private bool ComctlSupportsVisualStyles
		{
			get
			{
				if (!this.listViewState[4194304])
				{
					this.listViewState[4194304] = true;
					this.listViewState[2097152] = Application.ComCtlSupportsVisualStyles;
				}
				return this.listViewState[2097152];
			}
		}

		// Token: 0x17000A84 RID: 2692
		// (get) Token: 0x06002CD9 RID: 11481 RVA: 0x000C98C4 File Offset: 0x000C7AC4
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "SysListView32";
				if (base.IsHandleCreated)
				{
					int num = (int)((long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this, base.Handle), -16));
					createParams.Style |= (num & 3145728);
				}
				createParams.Style |= 64;
				ListViewAlignment listViewAlignment = this.alignStyle;
				if (listViewAlignment != ListViewAlignment.Left)
				{
					if (listViewAlignment == ListViewAlignment.Top)
					{
						createParams.Style |= 0;
					}
				}
				else
				{
					createParams.Style |= 2048;
				}
				if (this.AutoArrange)
				{
					createParams.Style |= 256;
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
				ColumnHeaderStyle columnHeaderStyle = this.headerStyle;
				if (columnHeaderStyle != ColumnHeaderStyle.None)
				{
					if (columnHeaderStyle == ColumnHeaderStyle.Nonclickable)
					{
						createParams.Style |= 32768;
					}
				}
				else
				{
					createParams.Style |= 16384;
				}
				if (this.LabelEdit)
				{
					createParams.Style |= 512;
				}
				if (!this.LabelWrap)
				{
					createParams.Style |= 128;
				}
				if (!this.HideSelection)
				{
					createParams.Style |= 8;
				}
				if (!this.MultiSelect)
				{
					createParams.Style |= 4;
				}
				if (this.listItemSorter == null)
				{
					SortOrder sortOrder = this.sorting;
					if (sortOrder != SortOrder.Ascending)
					{
						if (sortOrder == SortOrder.Descending)
						{
							createParams.Style |= 32;
						}
					}
					else
					{
						createParams.Style |= 16;
					}
				}
				if (this.VirtualMode)
				{
					createParams.Style |= 4096;
				}
				if (this.viewStyle != View.Tile)
				{
					createParams.Style |= (int)this.viewStyle;
				}
				if (this.RightToLeft == RightToLeft.Yes && this.RightToLeftLayout)
				{
					createParams.ExStyle |= 4194304;
					createParams.ExStyle &= -28673;
				}
				return createParams;
			}
		}

		// Token: 0x17000A85 RID: 2693
		// (get) Token: 0x06002CDA RID: 11482 RVA: 0x000C9AE9 File Offset: 0x000C7CE9
		internal ListViewGroup DefaultGroup
		{
			get
			{
				if (this.defaultGroup == null)
				{
					this.defaultGroup = new ListViewGroup(SR.GetString("ListViewGroupDefaultGroup", new object[]
					{
						"1"
					}));
				}
				return this.defaultGroup;
			}
		}

		// Token: 0x17000A86 RID: 2694
		// (get) Token: 0x06002CDB RID: 11483 RVA: 0x000C9B1C File Offset: 0x000C7D1C
		protected override Size DefaultSize
		{
			get
			{
				return new Size(121, 97);
			}
		}

		// Token: 0x17000A87 RID: 2695
		// (get) Token: 0x06002CDC RID: 11484 RVA: 0x000131D7 File Offset: 0x000113D7
		// (set) Token: 0x06002CDD RID: 11485 RVA: 0x000C9B27 File Offset: 0x000C7D27
		protected override bool DoubleBuffered
		{
			get
			{
				return base.DoubleBuffered;
			}
			set
			{
				if (this.DoubleBuffered != value)
				{
					base.DoubleBuffered = value;
					this.UpdateExtendedStyles();
				}
			}
		}

		// Token: 0x17000A88 RID: 2696
		// (get) Token: 0x06002CDE RID: 11486 RVA: 0x000C9B3F File Offset: 0x000C7D3F
		internal bool ExpectingMouseUp
		{
			get
			{
				return this.listViewState[1048576];
			}
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06002CDF RID: 11487 RVA: 0x000C9B54 File Offset: 0x000C7D54
		// (set) Token: 0x06002CE0 RID: 11488 RVA: 0x000C9B8F File Offset: 0x000C7D8F
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ListViewFocusedItemDescr")]
		public ListViewItem FocusedItem
		{
			get
			{
				if (base.IsHandleCreated)
				{
					int num = (int)((long)base.SendMessage(4108, -1, 1));
					if (num > -1)
					{
						return this.Items[num];
					}
				}
				return null;
			}
			set
			{
				if (base.IsHandleCreated && value != null)
				{
					value.Focused = true;
				}
			}
		}

		// Token: 0x17000A8A RID: 2698
		// (get) Token: 0x06002CE1 RID: 11489 RVA: 0x00013222 File Offset: 0x00011422
		// (set) Token: 0x06002CE2 RID: 11490 RVA: 0x000C9BA3 File Offset: 0x000C7DA3
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
				if (base.IsHandleCreated)
				{
					base.SendMessage(4132, 0, ColorTranslator.ToWin32(this.ForeColor));
				}
			}
		}

		// Token: 0x17000A8B RID: 2699
		// (get) Token: 0x06002CE3 RID: 11491 RVA: 0x000C9BCC File Offset: 0x000C7DCC
		// (set) Token: 0x06002CE4 RID: 11492 RVA: 0x000C9BDE File Offset: 0x000C7DDE
		private bool FlipViewToLargeIconAndSmallIcon
		{
			get
			{
				return this.listViewState[268435456];
			}
			set
			{
				this.listViewState[268435456] = value;
			}
		}

		// Token: 0x17000A8C RID: 2700
		// (get) Token: 0x06002CE5 RID: 11493 RVA: 0x000C9BF1 File Offset: 0x000C7DF1
		// (set) Token: 0x06002CE6 RID: 11494 RVA: 0x000C9C00 File Offset: 0x000C7E00
		[SRCategory("CatAppearance")]
		[DefaultValue(false)]
		[SRDescription("ListViewFullRowSelectDescr")]
		public bool FullRowSelect
		{
			get
			{
				return this.listViewState[16];
			}
			set
			{
				if (this.FullRowSelect != value)
				{
					this.listViewState[16] = value;
					this.UpdateExtendedStyles();
				}
			}
		}

		// Token: 0x17000A8D RID: 2701
		// (get) Token: 0x06002CE7 RID: 11495 RVA: 0x000C9C1F File Offset: 0x000C7E1F
		// (set) Token: 0x06002CE8 RID: 11496 RVA: 0x000C9C2E File Offset: 0x000C7E2E
		[SRCategory("CatAppearance")]
		[DefaultValue(false)]
		[SRDescription("ListViewGridLinesDescr")]
		public bool GridLines
		{
			get
			{
				return this.listViewState[32];
			}
			set
			{
				if (this.GridLines != value)
				{
					this.listViewState[32] = value;
					this.UpdateExtendedStyles();
				}
			}
		}

		// Token: 0x17000A8E RID: 2702
		// (get) Token: 0x06002CE9 RID: 11497 RVA: 0x000C9C4D File Offset: 0x000C7E4D
		[SRCategory("CatBehavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[Editor("System.Windows.Forms.Design.ListViewGroupCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SRDescription("ListViewGroupsDescr")]
		[MergableProperty(false)]
		public ListViewGroupCollection Groups
		{
			get
			{
				if (this.groups == null)
				{
					this.groups = new ListViewGroupCollection(this);
				}
				return this.groups;
			}
		}

		// Token: 0x17000A8F RID: 2703
		// (get) Token: 0x06002CEA RID: 11498 RVA: 0x000C9C69 File Offset: 0x000C7E69
		internal bool GroupsEnabled
		{
			get
			{
				return this.ShowGroups && this.groups != null && this.groups.Count > 0 && this.ComctlSupportsVisualStyles && !this.VirtualMode;
			}
		}

		// Token: 0x17000A90 RID: 2704
		// (get) Token: 0x06002CEB RID: 11499 RVA: 0x000C9C9C File Offset: 0x000C7E9C
		// (set) Token: 0x06002CEC RID: 11500 RVA: 0x000C9CA4 File Offset: 0x000C7EA4
		[SRCategory("CatBehavior")]
		[DefaultValue(ColumnHeaderStyle.Clickable)]
		[SRDescription("ListViewHeaderStyleDescr")]
		public ColumnHeaderStyle HeaderStyle
		{
			get
			{
				return this.headerStyle;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(ColumnHeaderStyle));
				}
				if (this.headerStyle != value)
				{
					this.headerStyle = value;
					if ((this.listViewState[8192] && value == ColumnHeaderStyle.Clickable) || (!this.listViewState[8192] && value == ColumnHeaderStyle.Nonclickable))
					{
						this.listViewState[8192] = !this.listViewState[8192];
						this.RecreateHandleInternal();
						return;
					}
					base.UpdateStyles();
				}
			}
		}

		// Token: 0x17000A91 RID: 2705
		// (get) Token: 0x06002CED RID: 11501 RVA: 0x000C9D43 File Offset: 0x000C7F43
		// (set) Token: 0x06002CEE RID: 11502 RVA: 0x000C9D52 File Offset: 0x000C7F52
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("ListViewHideSelectionDescr")]
		public bool HideSelection
		{
			get
			{
				return this.listViewState[64];
			}
			set
			{
				if (this.HideSelection != value)
				{
					this.listViewState[64] = value;
					base.UpdateStyles();
				}
			}
		}

		// Token: 0x17000A92 RID: 2706
		// (get) Token: 0x06002CEF RID: 11503 RVA: 0x000C9D71 File Offset: 0x000C7F71
		// (set) Token: 0x06002CF0 RID: 11504 RVA: 0x000C9D83 File Offset: 0x000C7F83
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("ListViewHotTrackingDescr")]
		public bool HotTracking
		{
			get
			{
				return this.listViewState[128];
			}
			set
			{
				if (this.HotTracking != value)
				{
					this.listViewState[128] = value;
					if (value)
					{
						this.HoverSelection = true;
						this.Activation = ItemActivation.OneClick;
					}
					this.UpdateExtendedStyles();
				}
			}
		}

		// Token: 0x17000A93 RID: 2707
		// (get) Token: 0x06002CF1 RID: 11505 RVA: 0x000C9DB6 File Offset: 0x000C7FB6
		// (set) Token: 0x06002CF2 RID: 11506 RVA: 0x000C9DC8 File Offset: 0x000C7FC8
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("ListViewHoverSelectDescr")]
		public bool HoverSelection
		{
			get
			{
				return this.listViewState[4096];
			}
			set
			{
				if (this.HoverSelection != value)
				{
					if (this.HotTracking && !value)
					{
						throw new ArgumentException(SR.GetString("ListViewHoverMustBeOnWhenHotTrackingIsOn"), "value");
					}
					this.listViewState[4096] = value;
					this.UpdateExtendedStyles();
				}
			}
		}

		// Token: 0x17000A94 RID: 2708
		// (get) Token: 0x06002CF3 RID: 11507 RVA: 0x000C9E15 File Offset: 0x000C8015
		internal bool InsertingItemsNatively
		{
			get
			{
				return this.listViewState1[1];
			}
		}

		// Token: 0x17000A95 RID: 2709
		// (get) Token: 0x06002CF4 RID: 11508 RVA: 0x000C9E23 File Offset: 0x000C8023
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ListViewInsertionMarkDescr")]
		public ListViewInsertionMark InsertionMark
		{
			get
			{
				if (this.insertionMark == null)
				{
					this.insertionMark = new ListViewInsertionMark(this);
				}
				return this.insertionMark;
			}
		}

		// Token: 0x17000A96 RID: 2710
		// (get) Token: 0x06002CF5 RID: 11509 RVA: 0x000C9E3F File Offset: 0x000C803F
		// (set) Token: 0x06002CF6 RID: 11510 RVA: 0x000C9E51 File Offset: 0x000C8051
		private bool ItemCollectionChangedInMouseDown
		{
			get
			{
				return this.listViewState[134217728];
			}
			set
			{
				this.listViewState[134217728] = value;
			}
		}

		// Token: 0x17000A97 RID: 2711
		// (get) Token: 0x06002CF7 RID: 11511 RVA: 0x000C9E64 File Offset: 0x000C8064
		[SRCategory("CatBehavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[Editor("System.Windows.Forms.Design.ListViewItemCollectionEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SRDescription("ListViewItemsDescr")]
		[MergableProperty(false)]
		public ListView.ListViewItemCollection Items
		{
			get
			{
				return this.listItemCollection;
			}
		}

		// Token: 0x17000A98 RID: 2712
		// (get) Token: 0x06002CF8 RID: 11512 RVA: 0x000C9E6C File Offset: 0x000C806C
		// (set) Token: 0x06002CF9 RID: 11513 RVA: 0x000C9E7E File Offset: 0x000C807E
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("ListViewLabelEditDescr")]
		public bool LabelEdit
		{
			get
			{
				return this.listViewState[256];
			}
			set
			{
				if (this.LabelEdit != value)
				{
					this.listViewState[256] = value;
					base.UpdateStyles();
				}
			}
		}

		// Token: 0x17000A99 RID: 2713
		// (get) Token: 0x06002CFA RID: 11514 RVA: 0x000C9EA0 File Offset: 0x000C80A0
		// (set) Token: 0x06002CFB RID: 11515 RVA: 0x000C9EB2 File Offset: 0x000C80B2
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[Localizable(true)]
		[SRDescription("ListViewLabelWrapDescr")]
		public bool LabelWrap
		{
			get
			{
				return this.listViewState[512];
			}
			set
			{
				if (this.LabelWrap != value)
				{
					this.listViewState[512] = value;
					base.UpdateStyles();
				}
			}
		}

		// Token: 0x17000A9A RID: 2714
		// (get) Token: 0x06002CFC RID: 11516 RVA: 0x000C9ED4 File Offset: 0x000C80D4
		// (set) Token: 0x06002CFD RID: 11517 RVA: 0x000C9EDC File Offset: 0x000C80DC
		[SRCategory("CatBehavior")]
		[DefaultValue(null)]
		[SRDescription("ListViewLargeImageListDescr")]
		public ImageList LargeImageList
		{
			get
			{
				return this.imageListLarge;
			}
			set
			{
				if (value != this.imageListLarge)
				{
					EventHandler value2 = new EventHandler(this.LargeImageListRecreateHandle);
					EventHandler value3 = new EventHandler(this.DetachImageList);
					EventHandler value4 = new EventHandler(this.LargeImageListChangedHandle);
					if (this.imageListLarge != null)
					{
						this.imageListLarge.RecreateHandle -= value2;
						this.imageListLarge.Disposed -= value3;
						this.imageListLarge.ChangeHandle -= value4;
					}
					this.imageListLarge = value;
					if (value != null)
					{
						value.RecreateHandle += value2;
						value.Disposed += value3;
						value.ChangeHandle += value4;
					}
					if (base.IsHandleCreated)
					{
						base.SendMessage(4099, (IntPtr)0, (value == null) ? IntPtr.Zero : value.Handle);
						if (this.AutoArrange && !this.listViewState1[4])
						{
							this.UpdateListViewItemsLocations();
						}
					}
				}
			}
		}

		// Token: 0x17000A9B RID: 2715
		// (get) Token: 0x06002CFE RID: 11518 RVA: 0x000C9FAD File Offset: 0x000C81AD
		// (set) Token: 0x06002CFF RID: 11519 RVA: 0x000C9FBF File Offset: 0x000C81BF
		internal bool ListViewHandleDestroyed
		{
			get
			{
				return this.listViewState[16777216];
			}
			set
			{
				this.listViewState[16777216] = value;
			}
		}

		// Token: 0x17000A9C RID: 2716
		// (get) Token: 0x06002D00 RID: 11520 RVA: 0x000C9FD2 File Offset: 0x000C81D2
		// (set) Token: 0x06002D01 RID: 11521 RVA: 0x000C9FDA File Offset: 0x000C81DA
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ListViewItemSorterDescr")]
		public IComparer ListViewItemSorter
		{
			get
			{
				return this.listItemSorter;
			}
			set
			{
				if (this.listItemSorter != value)
				{
					this.listItemSorter = value;
					if (!this.VirtualMode)
					{
						this.Sort();
					}
				}
			}
		}

		// Token: 0x17000A9D RID: 2717
		// (get) Token: 0x06002D02 RID: 11522 RVA: 0x000C9FFA File Offset: 0x000C81FA
		// (set) Token: 0x06002D03 RID: 11523 RVA: 0x000CA00C File Offset: 0x000C820C
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("ListViewMultiSelectDescr")]
		public bool MultiSelect
		{
			get
			{
				return this.listViewState[1024];
			}
			set
			{
				if (this.MultiSelect != value)
				{
					this.listViewState[1024] = value;
					base.UpdateStyles();
				}
			}
		}

		// Token: 0x17000A9E RID: 2718
		// (get) Token: 0x06002D04 RID: 11524 RVA: 0x000CA02E File Offset: 0x000C822E
		// (set) Token: 0x06002D05 RID: 11525 RVA: 0x000CA03C File Offset: 0x000C823C
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("ListViewOwnerDrawDescr")]
		public bool OwnerDraw
		{
			get
			{
				return this.listViewState[1];
			}
			set
			{
				if (this.OwnerDraw != value)
				{
					this.listViewState[1] = value;
					base.Invalidate(true);
				}
			}
		}

		// Token: 0x17000A9F RID: 2719
		// (get) Token: 0x06002D06 RID: 11526 RVA: 0x000CA05B File Offset: 0x000C825B
		// (set) Token: 0x06002D07 RID: 11527 RVA: 0x000CA064 File Offset: 0x000C8264
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

		// Token: 0x14000209 RID: 521
		// (add) Token: 0x06002D08 RID: 11528 RVA: 0x000CA0B8 File Offset: 0x000C82B8
		// (remove) Token: 0x06002D09 RID: 11529 RVA: 0x000CA0CB File Offset: 0x000C82CB
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnRightToLeftLayoutChangedDescr")]
		public event EventHandler RightToLeftLayoutChanged
		{
			add
			{
				base.Events.AddHandler(ListView.EVENT_RIGHTTOLEFTLAYOUTCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EVENT_RIGHTTOLEFTLAYOUTCHANGED, value);
			}
		}

		// Token: 0x17000AA0 RID: 2720
		// (get) Token: 0x06002D0A RID: 11530 RVA: 0x000CA0DE File Offset: 0x000C82DE
		// (set) Token: 0x06002D0B RID: 11531 RVA: 0x000CA0F0 File Offset: 0x000C82F0
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("ListViewScrollableDescr")]
		public bool Scrollable
		{
			get
			{
				return this.listViewState[2048];
			}
			set
			{
				if (this.Scrollable != value)
				{
					this.listViewState[2048] = value;
					this.RecreateHandleInternal();
				}
			}
		}

		// Token: 0x17000AA1 RID: 2721
		// (get) Token: 0x06002D0C RID: 11532 RVA: 0x000CA112 File Offset: 0x000C8312
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		public ListView.SelectedIndexCollection SelectedIndices
		{
			get
			{
				if (this.selectedIndexCollection == null)
				{
					this.selectedIndexCollection = new ListView.SelectedIndexCollection(this);
				}
				return this.selectedIndexCollection;
			}
		}

		// Token: 0x17000AA2 RID: 2722
		// (get) Token: 0x06002D0D RID: 11533 RVA: 0x000CA12E File Offset: 0x000C832E
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ListViewSelectedItemsDescr")]
		public ListView.SelectedListViewItemCollection SelectedItems
		{
			get
			{
				if (this.selectedListViewItemCollection == null)
				{
					this.selectedListViewItemCollection = new ListView.SelectedListViewItemCollection(this);
				}
				return this.selectedListViewItemCollection;
			}
		}

		// Token: 0x17000AA3 RID: 2723
		// (get) Token: 0x06002D0E RID: 11534 RVA: 0x000CA14A File Offset: 0x000C834A
		// (set) Token: 0x06002D0F RID: 11535 RVA: 0x000CA15C File Offset: 0x000C835C
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("ListViewShowGroupsDescr")]
		public bool ShowGroups
		{
			get
			{
				return this.listViewState[8388608];
			}
			set
			{
				if (value != this.ShowGroups)
				{
					this.listViewState[8388608] = value;
					if (base.IsHandleCreated)
					{
						this.UpdateGroupView();
					}
				}
			}
		}

		// Token: 0x17000AA4 RID: 2724
		// (get) Token: 0x06002D10 RID: 11536 RVA: 0x000CA186 File Offset: 0x000C8386
		// (set) Token: 0x06002D11 RID: 11537 RVA: 0x000CA190 File Offset: 0x000C8390
		[SRCategory("CatBehavior")]
		[DefaultValue(null)]
		[SRDescription("ListViewSmallImageListDescr")]
		public ImageList SmallImageList
		{
			get
			{
				return this.imageListSmall;
			}
			set
			{
				if (this.imageListSmall != value)
				{
					EventHandler value2 = new EventHandler(this.SmallImageListRecreateHandle);
					EventHandler value3 = new EventHandler(this.DetachImageList);
					if (this.imageListSmall != null)
					{
						this.imageListSmall.RecreateHandle -= value2;
						this.imageListSmall.Disposed -= value3;
					}
					this.imageListSmall = value;
					if (value != null)
					{
						value.RecreateHandle += value2;
						value.Disposed += value3;
					}
					if (base.IsHandleCreated)
					{
						base.SendMessage(4099, (IntPtr)1, (value == null) ? IntPtr.Zero : value.Handle);
						if (this.View == View.SmallIcon)
						{
							this.View = View.LargeIcon;
							this.View = View.SmallIcon;
						}
						else if (!this.listViewState1[4])
						{
							this.UpdateListViewItemsLocations();
						}
						if (this.View == View.Details)
						{
							base.Invalidate(true);
						}
					}
				}
			}
		}

		// Token: 0x17000AA5 RID: 2725
		// (get) Token: 0x06002D12 RID: 11538 RVA: 0x000CA262 File Offset: 0x000C8462
		// (set) Token: 0x06002D13 RID: 11539 RVA: 0x000CA274 File Offset: 0x000C8474
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("ListViewShowItemToolTipsDescr")]
		public bool ShowItemToolTips
		{
			get
			{
				return this.listViewState[32768];
			}
			set
			{
				if (this.ShowItemToolTips != value)
				{
					this.listViewState[32768] = value;
					this.RecreateHandleInternal();
				}
			}
		}

		// Token: 0x17000AA6 RID: 2726
		// (get) Token: 0x06002D14 RID: 11540 RVA: 0x000CA296 File Offset: 0x000C8496
		// (set) Token: 0x06002D15 RID: 11541 RVA: 0x000CA2A0 File Offset: 0x000C84A0
		[SRCategory("CatBehavior")]
		[DefaultValue(SortOrder.None)]
		[SRDescription("ListViewSortingDescr")]
		public SortOrder Sorting
		{
			get
			{
				return this.sorting;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(SortOrder));
				}
				if (this.sorting != value)
				{
					this.sorting = value;
					if (this.View == View.LargeIcon || this.View == View.SmallIcon)
					{
						if (this.listItemSorter == null)
						{
							this.listItemSorter = new ListView.IconComparer(this.sorting);
						}
						else if (this.listItemSorter is ListView.IconComparer)
						{
							((ListView.IconComparer)this.listItemSorter).SortOrder = this.sorting;
						}
					}
					else if (value == SortOrder.None)
					{
						this.listItemSorter = null;
					}
					if (value == SortOrder.None)
					{
						base.UpdateStyles();
						return;
					}
					this.RecreateHandleInternal();
				}
			}
		}

		// Token: 0x17000AA7 RID: 2727
		// (get) Token: 0x06002D16 RID: 11542 RVA: 0x000CA34E File Offset: 0x000C854E
		// (set) Token: 0x06002D17 RID: 11543 RVA: 0x000CA358 File Offset: 0x000C8558
		[SRCategory("CatBehavior")]
		[DefaultValue(null)]
		[SRDescription("ListViewStateImageListDescr")]
		public ImageList StateImageList
		{
			get
			{
				return this.imageListState;
			}
			set
			{
				if (this.UseCompatibleStateImageBehavior)
				{
					if (this.imageListState != value)
					{
						EventHandler value2 = new EventHandler(this.StateImageListRecreateHandle);
						EventHandler value3 = new EventHandler(this.DetachImageList);
						if (this.imageListState != null)
						{
							this.imageListState.RecreateHandle -= value2;
							this.imageListState.Disposed -= value3;
						}
						this.imageListState = value;
						if (value != null)
						{
							value.RecreateHandle += value2;
							value.Disposed += value3;
						}
						if (base.IsHandleCreated)
						{
							base.SendMessage(4099, 2, (value == null) ? IntPtr.Zero : value.Handle);
							return;
						}
					}
				}
				else if (this.imageListState != value)
				{
					EventHandler value4 = new EventHandler(this.StateImageListRecreateHandle);
					EventHandler value5 = new EventHandler(this.DetachImageList);
					if (this.imageListState != null)
					{
						this.imageListState.RecreateHandle -= value4;
						this.imageListState.Disposed -= value5;
					}
					if (base.IsHandleCreated && this.imageListState != null && this.CheckBoxes)
					{
						base.SendMessage(4099, 2, IntPtr.Zero);
					}
					this.imageListState = value;
					if (value != null)
					{
						value.RecreateHandle += value4;
						value.Disposed += value5;
					}
					if (base.IsHandleCreated)
					{
						if (this.CheckBoxes)
						{
							this.RecreateHandleInternal();
						}
						else
						{
							base.SendMessage(4099, 2, (this.imageListState == null || this.imageListState.Images.Count == 0) ? IntPtr.Zero : this.imageListState.Handle);
						}
						if (!this.listViewState1[4])
						{
							this.UpdateListViewItemsLocations();
						}
					}
				}
			}
		}

		// Token: 0x17000AA8 RID: 2728
		// (get) Token: 0x06002D18 RID: 11544 RVA: 0x00013A28 File Offset: 0x00011C28
		// (set) Token: 0x06002D19 RID: 11545 RVA: 0x00024185 File Offset: 0x00022385
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

		// Token: 0x1400020A RID: 522
		// (add) Token: 0x06002D1A RID: 11546 RVA: 0x00046771 File Offset: 0x00044971
		// (remove) Token: 0x06002D1B RID: 11547 RVA: 0x0004677A File Offset: 0x0004497A
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

		// Token: 0x17000AA9 RID: 2729
		// (get) Token: 0x06002D1C RID: 11548 RVA: 0x000CA4E4 File Offset: 0x000C86E4
		// (set) Token: 0x06002D1D RID: 11549 RVA: 0x000CA554 File Offset: 0x000C8754
		[SRCategory("CatAppearance")]
		[Browsable(true)]
		[SRDescription("ListViewTileSizeDescr")]
		public Size TileSize
		{
			get
			{
				if (!this.tileSize.IsEmpty)
				{
					return this.tileSize;
				}
				if (base.IsHandleCreated)
				{
					NativeMethods.LVTILEVIEWINFO lvtileviewinfo = new NativeMethods.LVTILEVIEWINFO();
					lvtileviewinfo.dwMask = 1;
					UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4259, 0, lvtileviewinfo);
					return new Size(lvtileviewinfo.sizeTile.cx, lvtileviewinfo.sizeTile.cy);
				}
				return Size.Empty;
			}
			set
			{
				if (this.tileSize != value)
				{
					if (value.IsEmpty || value.Height <= 0 || value.Width <= 0)
					{
						throw new ArgumentOutOfRangeException("TileSize", SR.GetString("ListViewTileSizeMustBePositive"));
					}
					this.tileSize = value;
					if (base.IsHandleCreated)
					{
						NativeMethods.LVTILEVIEWINFO lvtileviewinfo = new NativeMethods.LVTILEVIEWINFO();
						lvtileviewinfo.dwMask = 1;
						lvtileviewinfo.dwFlags = 3;
						lvtileviewinfo.sizeTile = new NativeMethods.SIZE(this.tileSize.Width, this.tileSize.Height);
						bool flag = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4258, 0, lvtileviewinfo);
						if (this.AutoArrange)
						{
							this.UpdateListViewItemsLocations();
						}
					}
				}
			}
		}

		// Token: 0x06002D1E RID: 11550 RVA: 0x000CA60F File Offset: 0x000C880F
		private bool ShouldSerializeTileSize()
		{
			return !this.tileSize.Equals(Size.Empty);
		}

		// Token: 0x17000AAA RID: 2730
		// (get) Token: 0x06002D1F RID: 11551 RVA: 0x000CA630 File Offset: 0x000C8830
		// (set) Token: 0x06002D20 RID: 11552 RVA: 0x000CA6D4 File Offset: 0x000C88D4
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("ListViewTopItemDescr")]
		public ListViewItem TopItem
		{
			get
			{
				if (this.viewStyle == View.LargeIcon || this.viewStyle == View.SmallIcon || this.viewStyle == View.Tile)
				{
					throw new InvalidOperationException(SR.GetString("ListViewGetTopItem"));
				}
				if (!base.IsHandleCreated)
				{
					if (this.Items.Count > 0)
					{
						return this.Items[0];
					}
					return null;
				}
				else
				{
					this.topIndex = (int)((long)base.SendMessage(4135, 0, 0));
					if (this.topIndex >= 0 && this.topIndex < this.Items.Count)
					{
						return this.Items[this.topIndex];
					}
					return null;
				}
			}
			set
			{
				if (this.viewStyle == View.LargeIcon || this.viewStyle == View.SmallIcon || this.viewStyle == View.Tile)
				{
					throw new InvalidOperationException(SR.GetString("ListViewSetTopItem"));
				}
				if (value == null)
				{
					return;
				}
				if (value.ListView != this)
				{
					return;
				}
				if (!base.IsHandleCreated)
				{
					this.CreateHandle();
				}
				if (value == this.TopItem)
				{
					return;
				}
				this.EnsureVisible(value.Index);
				ListViewItem topItem = this.TopItem;
				if (topItem == null && this.topIndex == this.Items.Count)
				{
					if (this.Scrollable)
					{
						this.EnsureVisible(0);
						this.Scroll(0, value.Index);
					}
					return;
				}
				if (value.Index == topItem.Index)
				{
					return;
				}
				if (this.Scrollable)
				{
					this.Scroll(topItem.Index, value.Index);
				}
			}
		}

		// Token: 0x17000AAB RID: 2731
		// (get) Token: 0x06002D21 RID: 11553 RVA: 0x000CA7A2 File Offset: 0x000C89A2
		// (set) Token: 0x06002D22 RID: 11554 RVA: 0x000CA7B0 File Offset: 0x000C89B0
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[DefaultValue(true)]
		public bool UseCompatibleStateImageBehavior
		{
			get
			{
				return this.listViewState1[8];
			}
			set
			{
				this.listViewState1[8] = value;
			}
		}

		// Token: 0x17000AAC RID: 2732
		// (get) Token: 0x06002D23 RID: 11555 RVA: 0x000CA7BF File Offset: 0x000C89BF
		// (set) Token: 0x06002D24 RID: 11556 RVA: 0x000CA7C8 File Offset: 0x000C89C8
		[SRCategory("CatAppearance")]
		[DefaultValue(View.LargeIcon)]
		[SRDescription("ListViewViewDescr")]
		public View View
		{
			get
			{
				return this.viewStyle;
			}
			set
			{
				if (value == View.Tile && this.CheckBoxes)
				{
					throw new NotSupportedException(SR.GetString("ListViewTileViewDoesNotSupportCheckBoxes"));
				}
				this.FlipViewToLargeIconAndSmallIcon = false;
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 4))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(View));
				}
				if (value == View.Tile && this.VirtualMode)
				{
					throw new NotSupportedException(SR.GetString("ListViewCantSetViewToTileViewInVirtualMode"));
				}
				if (this.viewStyle != value)
				{
					this.viewStyle = value;
					if (base.IsHandleCreated && this.ComctlSupportsVisualStyles)
					{
						base.SendMessage(4238, (int)this.viewStyle, 0);
						this.UpdateGroupView();
						if (this.viewStyle == View.Tile)
						{
							this.UpdateTileView();
						}
					}
					else
					{
						base.UpdateStyles();
					}
					this.UpdateListViewItemsLocations();
				}
			}
		}

		// Token: 0x17000AAD RID: 2733
		// (get) Token: 0x06002D25 RID: 11557 RVA: 0x000CA890 File Offset: 0x000C8A90
		// (set) Token: 0x06002D26 RID: 11558 RVA: 0x000CA898 File Offset: 0x000C8A98
		[SRCategory("CatBehavior")]
		[DefaultValue(0)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("ListViewVirtualListSizeDescr")]
		public int VirtualListSize
		{
			get
			{
				return this.virtualListSize;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentException(SR.GetString("ListViewVirtualListSizeInvalidArgument", new object[]
					{
						"value",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (value == this.virtualListSize)
				{
					return;
				}
				bool flag = base.IsHandleCreated && this.VirtualMode && this.View == View.Details && !base.DesignMode;
				int num = -1;
				if (flag)
				{
					num = (int)((long)base.SendMessage(4135, 0, 0));
				}
				this.virtualListSize = value;
				if (base.IsHandleCreated && this.VirtualMode && !base.DesignMode)
				{
					base.SendMessage(4143, this.virtualListSize, 0);
				}
				if (flag)
				{
					num = Math.Min(num, this.VirtualListSize - 1);
					if (num > 0)
					{
						ListViewItem topItem = this.Items[num];
						this.TopItem = topItem;
					}
				}
			}
		}

		// Token: 0x17000AAE RID: 2734
		// (get) Token: 0x06002D27 RID: 11559 RVA: 0x000CA97B File Offset: 0x000C8B7B
		// (set) Token: 0x06002D28 RID: 11560 RVA: 0x000CA990 File Offset: 0x000C8B90
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("ListViewVirtualModeDescr")]
		public bool VirtualMode
		{
			get
			{
				return this.listViewState[33554432];
			}
			set
			{
				if (value == this.VirtualMode)
				{
					return;
				}
				if (value && this.Items.Count > 0)
				{
					throw new InvalidOperationException(SR.GetString("ListViewVirtualListViewRequiresNoItems"));
				}
				if (value && this.CheckedItems.Count > 0)
				{
					throw new InvalidOperationException(SR.GetString("ListViewVirtualListViewRequiresNoCheckedItems"));
				}
				if (value && this.SelectedItems.Count > 0)
				{
					throw new InvalidOperationException(SR.GetString("ListViewVirtualListViewRequiresNoSelectedItems"));
				}
				if (value && this.View == View.Tile)
				{
					throw new NotSupportedException(SR.GetString("ListViewCantSetVirtualModeWhenInTileView"));
				}
				this.listViewState[33554432] = value;
				this.RecreateHandleInternal();
			}
		}

		// Token: 0x1400020B RID: 523
		// (add) Token: 0x06002D29 RID: 11561 RVA: 0x000CAA3D File Offset: 0x000C8C3D
		// (remove) Token: 0x06002D2A RID: 11562 RVA: 0x000CAA56 File Offset: 0x000C8C56
		[SRCategory("CatBehavior")]
		[SRDescription("ListViewAfterLabelEditDescr")]
		public event LabelEditEventHandler AfterLabelEdit
		{
			add
			{
				this.onAfterLabelEdit = (LabelEditEventHandler)Delegate.Combine(this.onAfterLabelEdit, value);
			}
			remove
			{
				this.onAfterLabelEdit = (LabelEditEventHandler)Delegate.Remove(this.onAfterLabelEdit, value);
			}
		}

		// Token: 0x1400020C RID: 524
		// (add) Token: 0x06002D2B RID: 11563 RVA: 0x000CAA6F File Offset: 0x000C8C6F
		// (remove) Token: 0x06002D2C RID: 11564 RVA: 0x000CAA88 File Offset: 0x000C8C88
		[SRCategory("CatBehavior")]
		[SRDescription("ListViewBeforeLabelEditDescr")]
		public event LabelEditEventHandler BeforeLabelEdit
		{
			add
			{
				this.onBeforeLabelEdit = (LabelEditEventHandler)Delegate.Combine(this.onBeforeLabelEdit, value);
			}
			remove
			{
				this.onBeforeLabelEdit = (LabelEditEventHandler)Delegate.Remove(this.onBeforeLabelEdit, value);
			}
		}

		// Token: 0x1400020D RID: 525
		// (add) Token: 0x06002D2D RID: 11565 RVA: 0x000CAAA1 File Offset: 0x000C8CA1
		// (remove) Token: 0x06002D2E RID: 11566 RVA: 0x000CAAB4 File Offset: 0x000C8CB4
		[SRCategory("CatAction")]
		[SRDescription("ListViewCacheVirtualItemsEventDescr")]
		public event CacheVirtualItemsEventHandler CacheVirtualItems
		{
			add
			{
				base.Events.AddHandler(ListView.EVENT_CACHEVIRTUALITEMS, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EVENT_CACHEVIRTUALITEMS, value);
			}
		}

		// Token: 0x1400020E RID: 526
		// (add) Token: 0x06002D2F RID: 11567 RVA: 0x000CAAC7 File Offset: 0x000C8CC7
		// (remove) Token: 0x06002D30 RID: 11568 RVA: 0x000CAAE0 File Offset: 0x000C8CE0
		[SRCategory("CatAction")]
		[SRDescription("ListViewColumnClickDescr")]
		public event ColumnClickEventHandler ColumnClick
		{
			add
			{
				this.onColumnClick = (ColumnClickEventHandler)Delegate.Combine(this.onColumnClick, value);
			}
			remove
			{
				this.onColumnClick = (ColumnClickEventHandler)Delegate.Remove(this.onColumnClick, value);
			}
		}

		// Token: 0x1400020F RID: 527
		// (add) Token: 0x06002D31 RID: 11569 RVA: 0x000CAAF9 File Offset: 0x000C8CF9
		// (remove) Token: 0x06002D32 RID: 11570 RVA: 0x000CAB0C File Offset: 0x000C8D0C
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ListViewColumnReorderedDscr")]
		public event ColumnReorderedEventHandler ColumnReordered
		{
			add
			{
				base.Events.AddHandler(ListView.EVENT_COLUMNREORDERED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EVENT_COLUMNREORDERED, value);
			}
		}

		// Token: 0x14000210 RID: 528
		// (add) Token: 0x06002D33 RID: 11571 RVA: 0x000CAB1F File Offset: 0x000C8D1F
		// (remove) Token: 0x06002D34 RID: 11572 RVA: 0x000CAB32 File Offset: 0x000C8D32
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ListViewColumnWidthChangedDscr")]
		public event ColumnWidthChangedEventHandler ColumnWidthChanged
		{
			add
			{
				base.Events.AddHandler(ListView.EVENT_COLUMNWIDTHCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EVENT_COLUMNWIDTHCHANGED, value);
			}
		}

		// Token: 0x14000211 RID: 529
		// (add) Token: 0x06002D35 RID: 11573 RVA: 0x000CAB45 File Offset: 0x000C8D45
		// (remove) Token: 0x06002D36 RID: 11574 RVA: 0x000CAB58 File Offset: 0x000C8D58
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ListViewColumnWidthChangingDscr")]
		public event ColumnWidthChangingEventHandler ColumnWidthChanging
		{
			add
			{
				base.Events.AddHandler(ListView.EVENT_COLUMNWIDTHCHANGING, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EVENT_COLUMNWIDTHCHANGING, value);
			}
		}

		// Token: 0x14000212 RID: 530
		// (add) Token: 0x06002D37 RID: 11575 RVA: 0x000CAB6B File Offset: 0x000C8D6B
		// (remove) Token: 0x06002D38 RID: 11576 RVA: 0x000CAB7E File Offset: 0x000C8D7E
		[SRCategory("CatBehavior")]
		[SRDescription("ListViewDrawColumnHeaderEventDescr")]
		public event DrawListViewColumnHeaderEventHandler DrawColumnHeader
		{
			add
			{
				base.Events.AddHandler(ListView.EVENT_DRAWCOLUMNHEADER, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EVENT_DRAWCOLUMNHEADER, value);
			}
		}

		// Token: 0x14000213 RID: 531
		// (add) Token: 0x06002D39 RID: 11577 RVA: 0x000CAB91 File Offset: 0x000C8D91
		// (remove) Token: 0x06002D3A RID: 11578 RVA: 0x000CABA4 File Offset: 0x000C8DA4
		[SRCategory("CatBehavior")]
		[SRDescription("ListViewDrawItemEventDescr")]
		public event DrawListViewItemEventHandler DrawItem
		{
			add
			{
				base.Events.AddHandler(ListView.EVENT_DRAWITEM, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EVENT_DRAWITEM, value);
			}
		}

		// Token: 0x14000214 RID: 532
		// (add) Token: 0x06002D3B RID: 11579 RVA: 0x000CABB7 File Offset: 0x000C8DB7
		// (remove) Token: 0x06002D3C RID: 11580 RVA: 0x000CABCA File Offset: 0x000C8DCA
		[SRCategory("CatBehavior")]
		[SRDescription("ListViewDrawSubItemEventDescr")]
		public event DrawListViewSubItemEventHandler DrawSubItem
		{
			add
			{
				base.Events.AddHandler(ListView.EVENT_DRAWSUBITEM, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EVENT_DRAWSUBITEM, value);
			}
		}

		// Token: 0x14000215 RID: 533
		// (add) Token: 0x06002D3D RID: 11581 RVA: 0x000CABDD File Offset: 0x000C8DDD
		// (remove) Token: 0x06002D3E RID: 11582 RVA: 0x000CABF6 File Offset: 0x000C8DF6
		[SRCategory("CatAction")]
		[SRDescription("ListViewItemClickDescr")]
		public event EventHandler ItemActivate
		{
			add
			{
				this.onItemActivate = (EventHandler)Delegate.Combine(this.onItemActivate, value);
			}
			remove
			{
				this.onItemActivate = (EventHandler)Delegate.Remove(this.onItemActivate, value);
			}
		}

		// Token: 0x14000216 RID: 534
		// (add) Token: 0x06002D3F RID: 11583 RVA: 0x000CAC0F File Offset: 0x000C8E0F
		// (remove) Token: 0x06002D40 RID: 11584 RVA: 0x000CAC28 File Offset: 0x000C8E28
		[SRCategory("CatBehavior")]
		[SRDescription("CheckedListBoxItemCheckDescr")]
		public event ItemCheckEventHandler ItemCheck
		{
			add
			{
				this.onItemCheck = (ItemCheckEventHandler)Delegate.Combine(this.onItemCheck, value);
			}
			remove
			{
				this.onItemCheck = (ItemCheckEventHandler)Delegate.Remove(this.onItemCheck, value);
			}
		}

		// Token: 0x14000217 RID: 535
		// (add) Token: 0x06002D41 RID: 11585 RVA: 0x000CAC41 File Offset: 0x000C8E41
		// (remove) Token: 0x06002D42 RID: 11586 RVA: 0x000CAC5A File Offset: 0x000C8E5A
		[SRCategory("CatBehavior")]
		[SRDescription("ListViewItemCheckedDescr")]
		public event ItemCheckedEventHandler ItemChecked
		{
			add
			{
				this.onItemChecked = (ItemCheckedEventHandler)Delegate.Combine(this.onItemChecked, value);
			}
			remove
			{
				this.onItemChecked = (ItemCheckedEventHandler)Delegate.Remove(this.onItemChecked, value);
			}
		}

		// Token: 0x14000218 RID: 536
		// (add) Token: 0x06002D43 RID: 11587 RVA: 0x000CAC73 File Offset: 0x000C8E73
		// (remove) Token: 0x06002D44 RID: 11588 RVA: 0x000CAC8C File Offset: 0x000C8E8C
		[SRCategory("CatAction")]
		[SRDescription("ListViewItemDragDescr")]
		public event ItemDragEventHandler ItemDrag
		{
			add
			{
				this.onItemDrag = (ItemDragEventHandler)Delegate.Combine(this.onItemDrag, value);
			}
			remove
			{
				this.onItemDrag = (ItemDragEventHandler)Delegate.Remove(this.onItemDrag, value);
			}
		}

		// Token: 0x14000219 RID: 537
		// (add) Token: 0x06002D45 RID: 11589 RVA: 0x000CACA5 File Offset: 0x000C8EA5
		// (remove) Token: 0x06002D46 RID: 11590 RVA: 0x000CACBE File Offset: 0x000C8EBE
		[SRCategory("CatAction")]
		[SRDescription("ListViewItemMouseHoverDescr")]
		public event ListViewItemMouseHoverEventHandler ItemMouseHover
		{
			add
			{
				this.onItemMouseHover = (ListViewItemMouseHoverEventHandler)Delegate.Combine(this.onItemMouseHover, value);
			}
			remove
			{
				this.onItemMouseHover = (ListViewItemMouseHoverEventHandler)Delegate.Remove(this.onItemMouseHover, value);
			}
		}

		// Token: 0x1400021A RID: 538
		// (add) Token: 0x06002D47 RID: 11591 RVA: 0x000CACD7 File Offset: 0x000C8ED7
		// (remove) Token: 0x06002D48 RID: 11592 RVA: 0x000CACEA File Offset: 0x000C8EEA
		[SRCategory("CatBehavior")]
		[SRDescription("ListViewItemSelectionChangedDescr")]
		public event ListViewItemSelectionChangedEventHandler ItemSelectionChanged
		{
			add
			{
				base.Events.AddHandler(ListView.EVENT_ITEMSELECTIONCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EVENT_ITEMSELECTIONCHANGED, value);
			}
		}

		// Token: 0x17000AAF RID: 2735
		// (get) Token: 0x06002D49 RID: 11593 RVA: 0x00013656 File Offset: 0x00011856
		// (set) Token: 0x06002D4A RID: 11594 RVA: 0x0001365E File Offset: 0x0001185E
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

		// Token: 0x1400021B RID: 539
		// (add) Token: 0x06002D4B RID: 11595 RVA: 0x00013667 File Offset: 0x00011867
		// (remove) Token: 0x06002D4C RID: 11596 RVA: 0x00013670 File Offset: 0x00011870
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

		// Token: 0x1400021C RID: 540
		// (add) Token: 0x06002D4D RID: 11597 RVA: 0x00013F87 File Offset: 0x00012187
		// (remove) Token: 0x06002D4E RID: 11598 RVA: 0x00013F90 File Offset: 0x00012190
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

		// Token: 0x1400021D RID: 541
		// (add) Token: 0x06002D4F RID: 11599 RVA: 0x000CACFD File Offset: 0x000C8EFD
		// (remove) Token: 0x06002D50 RID: 11600 RVA: 0x000CAD10 File Offset: 0x000C8F10
		[SRCategory("CatAction")]
		[SRDescription("ListViewRetrieveVirtualItemEventDescr")]
		public event RetrieveVirtualItemEventHandler RetrieveVirtualItem
		{
			add
			{
				base.Events.AddHandler(ListView.EVENT_RETRIEVEVIRTUALITEM, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EVENT_RETRIEVEVIRTUALITEM, value);
			}
		}

		// Token: 0x1400021E RID: 542
		// (add) Token: 0x06002D51 RID: 11601 RVA: 0x000CAD23 File Offset: 0x000C8F23
		// (remove) Token: 0x06002D52 RID: 11602 RVA: 0x000CAD36 File Offset: 0x000C8F36
		[SRCategory("CatAction")]
		[SRDescription("ListViewSearchForVirtualItemDescr")]
		public event SearchForVirtualItemEventHandler SearchForVirtualItem
		{
			add
			{
				base.Events.AddHandler(ListView.EVENT_SEARCHFORVIRTUALITEM, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EVENT_SEARCHFORVIRTUALITEM, value);
			}
		}

		// Token: 0x1400021F RID: 543
		// (add) Token: 0x06002D53 RID: 11603 RVA: 0x000CAD49 File Offset: 0x000C8F49
		// (remove) Token: 0x06002D54 RID: 11604 RVA: 0x000CAD5C File Offset: 0x000C8F5C
		[SRCategory("CatBehavior")]
		[SRDescription("ListViewSelectedIndexChangedDescr")]
		public event EventHandler SelectedIndexChanged
		{
			add
			{
				base.Events.AddHandler(ListView.EVENT_SELECTEDINDEXCHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EVENT_SELECTEDINDEXCHANGED, value);
			}
		}

		// Token: 0x14000220 RID: 544
		// (add) Token: 0x06002D55 RID: 11605 RVA: 0x000CAD6F File Offset: 0x000C8F6F
		// (remove) Token: 0x06002D56 RID: 11606 RVA: 0x000CAD82 File Offset: 0x000C8F82
		[SRCategory("CatBehavior")]
		[SRDescription("ListViewVirtualItemsSelectionRangeChangedDescr")]
		public event ListViewVirtualItemsSelectionRangeChangedEventHandler VirtualItemsSelectionRangeChanged
		{
			add
			{
				base.Events.AddHandler(ListView.EVENT_VIRTUALITEMSSELECTIONRANGECHANGED, value);
			}
			remove
			{
				base.Events.RemoveHandler(ListView.EVENT_VIRTUALITEMSSELECTIONRANGECHANGED, value);
			}
		}

		// Token: 0x06002D57 RID: 11607 RVA: 0x000CAD98 File Offset: 0x000C8F98
		private void ApplyUpdateCachedItems()
		{
			ArrayList arrayList = (ArrayList)base.Properties.GetObject(ListView.PropDelayedUpdateItems);
			if (arrayList != null)
			{
				base.Properties.SetObject(ListView.PropDelayedUpdateItems, null);
				ListViewItem[] array = (ListViewItem[])arrayList.ToArray(typeof(ListViewItem));
				if (array.Length != 0)
				{
					this.InsertItems(this.itemCount, array, false);
				}
			}
		}

		// Token: 0x06002D58 RID: 11608 RVA: 0x000CADF8 File Offset: 0x000C8FF8
		public void ArrangeIcons(ListViewAlignment value)
		{
			if (this.viewStyle != View.SmallIcon)
			{
				return;
			}
			int num = (int)value;
			if (num <= 2 || num == 5)
			{
				if (base.IsHandleCreated)
				{
					UnsafeNativeMethods.PostMessage(new HandleRef(this, base.Handle), 4118, (int)value, 0);
				}
				if (!this.VirtualMode && this.sorting != SortOrder.None)
				{
					this.Sort();
				}
				return;
			}
			throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
			{
				"value",
				value.ToString()
			}));
		}

		// Token: 0x06002D59 RID: 11609 RVA: 0x000CAE80 File Offset: 0x000C9080
		public void ArrangeIcons()
		{
			this.ArrangeIcons(ListViewAlignment.Default);
		}

		// Token: 0x06002D5A RID: 11610 RVA: 0x000CAE89 File Offset: 0x000C9089
		public void AutoResizeColumns(ColumnHeaderAutoResizeStyle headerAutoResize)
		{
			if (!base.IsHandleCreated)
			{
				this.CreateHandle();
			}
			this.UpdateColumnWidths(headerAutoResize);
		}

		// Token: 0x06002D5B RID: 11611 RVA: 0x000CAEA0 File Offset: 0x000C90A0
		public void AutoResizeColumn(int columnIndex, ColumnHeaderAutoResizeStyle headerAutoResize)
		{
			if (!base.IsHandleCreated)
			{
				this.CreateHandle();
			}
			this.SetColumnWidth(columnIndex, headerAutoResize);
		}

		// Token: 0x06002D5C RID: 11612 RVA: 0x000CAEB8 File Offset: 0x000C90B8
		public void BeginUpdate()
		{
			base.BeginUpdateInternal();
			int num = this.updateCounter;
			this.updateCounter = num + 1;
			if (num == 0 && base.Properties.GetObject(ListView.PropDelayedUpdateItems) == null)
			{
				base.Properties.SetObject(ListView.PropDelayedUpdateItems, new ArrayList());
			}
		}

		// Token: 0x06002D5D RID: 11613 RVA: 0x000CAF08 File Offset: 0x000C9108
		internal void CacheSelectedStateForItem(ListViewItem lvi, bool selected)
		{
			if (selected)
			{
				if (this.savedSelectedItems == null)
				{
					this.savedSelectedItems = new List<ListViewItem>();
				}
				if (!this.savedSelectedItems.Contains(lvi))
				{
					this.savedSelectedItems.Add(lvi);
					return;
				}
			}
			else if (this.savedSelectedItems != null && this.savedSelectedItems.Contains(lvi))
			{
				this.savedSelectedItems.Remove(lvi);
			}
		}

		// Token: 0x06002D5E RID: 11614 RVA: 0x000CAF6C File Offset: 0x000C916C
		private void CleanPreviousBackgroundImageFiles()
		{
			if (this.bkImgFileNames == null)
			{
				return;
			}
			FileIOPermission fileIOPermission = new FileIOPermission(PermissionState.Unrestricted);
			fileIOPermission.Assert();
			try
			{
				for (int i = 0; i <= this.bkImgFileNamesCount; i++)
				{
					FileInfo fileInfo = new FileInfo(this.bkImgFileNames[i]);
					if (fileInfo.Exists)
					{
						try
						{
							fileInfo.Delete();
						}
						catch (IOException)
						{
						}
					}
				}
			}
			finally
			{
				PermissionSet.RevertAssert();
			}
			this.bkImgFileNames = null;
			this.bkImgFileNamesCount = -1;
		}

		// Token: 0x06002D5F RID: 11615 RVA: 0x000CAFF4 File Offset: 0x000C91F4
		public void Clear()
		{
			this.Items.Clear();
			this.Columns.Clear();
		}

		// Token: 0x06002D60 RID: 11616 RVA: 0x000CB00C File Offset: 0x000C920C
		private int CompareFunc(IntPtr lparam1, IntPtr lparam2, IntPtr lparamSort)
		{
			if (this.listItemSorter != null)
			{
				return this.listItemSorter.Compare(this.listItemsTable[(int)lparam1], this.listItemsTable[(int)lparam2]);
			}
			return 0;
		}

		// Token: 0x06002D61 RID: 11617 RVA: 0x000CB05C File Offset: 0x000C925C
		private int CompensateColumnHeaderResize(Message m, bool columnResizeCancelled)
		{
			if (this.ComctlSupportsVisualStyles && this.View == View.Details && !columnResizeCancelled && this.Items.Count > 0)
			{
				NativeMethods.NMHEADER nmheader = (NativeMethods.NMHEADER)m.GetLParam(typeof(NativeMethods.NMHEADER));
				return this.CompensateColumnHeaderResize(nmheader.iItem, columnResizeCancelled);
			}
			return 0;
		}

		// Token: 0x06002D62 RID: 11618 RVA: 0x000CB0B4 File Offset: 0x000C92B4
		private int CompensateColumnHeaderResize(int columnIndex, bool columnResizeCancelled)
		{
			if (this.ComctlSupportsVisualStyles && this.View == View.Details && !columnResizeCancelled && this.Items.Count > 0 && columnIndex == 0)
			{
				ColumnHeader columnHeader = (this.columnHeaders != null && this.columnHeaders.Length != 0) ? this.columnHeaders[0] : null;
				if (columnHeader != null)
				{
					if (this.SmallImageList == null)
					{
						return 2;
					}
					bool flag = true;
					for (int i = 0; i < this.Items.Count; i++)
					{
						if (this.Items[i].ImageIndexer.ActualIndex > -1)
						{
							flag = false;
							break;
						}
					}
					if (flag)
					{
						return 18;
					}
				}
			}
			return 0;
		}

		// Token: 0x06002D63 RID: 11619 RVA: 0x000CB150 File Offset: 0x000C9350
		protected override void CreateHandle()
		{
			if (!base.RecreatingHandle)
			{
				IntPtr userCookie = UnsafeNativeMethods.ThemingScope.Activate();
				try
				{
					SafeNativeMethods.InitCommonControlsEx(new NativeMethods.INITCOMMONCONTROLSEX
					{
						dwICC = 1
					});
				}
				finally
				{
					UnsafeNativeMethods.ThemingScope.Deactivate(userCookie);
				}
			}
			base.CreateHandle();
			if (this.BackgroundImage != null)
			{
				this.SetBackgroundImage();
			}
		}

		// Token: 0x06002D64 RID: 11620 RVA: 0x000CB1B0 File Offset: 0x000C93B0
		private unsafe void CustomDraw(ref Message m)
		{
			bool flag = false;
			bool flag2 = false;
			try
			{
				NativeMethods.NMLVCUSTOMDRAW* ptr = (NativeMethods.NMLVCUSTOMDRAW*)((void*)m.LParam);
				int dwDrawStage = ptr->nmcd.dwDrawStage;
				if (dwDrawStage != 1)
				{
					int num;
					Rectangle itemRectOrEmpty;
					if (dwDrawStage != 65537)
					{
						if (dwDrawStage != 196609)
						{
							m.Result = (IntPtr)0;
							return;
						}
					}
					else
					{
						num = (int)ptr->nmcd.dwItemSpec;
						itemRectOrEmpty = this.GetItemRectOrEmpty(num);
						if (!base.ClientRectangle.IntersectsWith(itemRectOrEmpty))
						{
							return;
						}
						if (this.OwnerDraw)
						{
							Graphics graphics = Graphics.FromHdcInternal(ptr->nmcd.hdc);
							DrawListViewItemEventArgs drawListViewItemEventArgs = null;
							try
							{
								drawListViewItemEventArgs = new DrawListViewItemEventArgs(graphics, this.Items[(int)ptr->nmcd.dwItemSpec], itemRectOrEmpty, (int)ptr->nmcd.dwItemSpec, (ListViewItemStates)ptr->nmcd.uItemState);
								this.OnDrawItem(drawListViewItemEventArgs);
							}
							finally
							{
								graphics.Dispose();
							}
							flag2 = drawListViewItemEventArgs.DrawDefault;
							if (this.viewStyle == View.Details)
							{
								m.Result = (IntPtr)32;
							}
							else if (!drawListViewItemEventArgs.DrawDefault)
							{
								m.Result = (IntPtr)4;
							}
							if (!drawListViewItemEventArgs.DrawDefault)
							{
								return;
							}
						}
						if (this.viewStyle == View.Details || this.viewStyle == View.Tile)
						{
							m.Result = (IntPtr)34;
							flag = true;
						}
					}
					num = (int)ptr->nmcd.dwItemSpec;
					itemRectOrEmpty = this.GetItemRectOrEmpty(num);
					if (base.ClientRectangle.IntersectsWith(itemRectOrEmpty))
					{
						if (this.OwnerDraw && !flag2)
						{
							Graphics graphics2 = Graphics.FromHdcInternal(ptr->nmcd.hdc);
							bool flag3 = true;
							try
							{
								if (ptr->iSubItem < this.Items[num].SubItems.Count)
								{
									Rectangle subItemRect = this.GetSubItemRect(num, ptr->iSubItem);
									if (ptr->iSubItem == 0 && this.Items[num].SubItems.Count > 1)
									{
										subItemRect.Width = this.columnHeaders[0].Width;
									}
									if (base.ClientRectangle.IntersectsWith(subItemRect))
									{
										DrawListViewSubItemEventArgs drawListViewSubItemEventArgs = new DrawListViewSubItemEventArgs(graphics2, subItemRect, this.Items[num], this.Items[num].SubItems[ptr->iSubItem], num, ptr->iSubItem, this.columnHeaders[ptr->iSubItem], (ListViewItemStates)ptr->nmcd.uItemState);
										this.OnDrawSubItem(drawListViewSubItemEventArgs);
										flag3 = !drawListViewSubItemEventArgs.DrawDefault;
									}
								}
							}
							finally
							{
								graphics2.Dispose();
							}
							if (flag3)
							{
								m.Result = (IntPtr)4;
								return;
							}
						}
						ListViewItem listViewItem = this.Items[(int)ptr->nmcd.dwItemSpec];
						if (flag && listViewItem.UseItemStyleForSubItems)
						{
							m.Result = (IntPtr)2;
						}
						int num2 = ptr->nmcd.uItemState;
						if (!this.HideSelection)
						{
							int itemState = this.GetItemState((int)ptr->nmcd.dwItemSpec);
							if ((itemState & 2) == 0)
							{
								num2 &= -2;
							}
						}
						int num3 = ((ptr->nmcd.dwDrawStage & 131072) != 0) ? ptr->iSubItem : 0;
						Font font = null;
						Color color = Color.Empty;
						Color color2 = Color.Empty;
						bool flag4 = false;
						bool flag5 = false;
						if (listViewItem != null && num3 < listViewItem.SubItems.Count)
						{
							flag4 = true;
							if (num3 == 0 && (num2 & 64) != 0 && this.HotTracking)
							{
								flag5 = true;
								font = new Font(listViewItem.SubItems[0].Font, FontStyle.Underline);
							}
							else
							{
								font = listViewItem.SubItems[num3].Font;
							}
							if (num3 > 0 || (num2 & 71) == 0)
							{
								color = listViewItem.SubItems[num3].ForeColor;
								color2 = listViewItem.SubItems[num3].BackColor;
							}
						}
						Color c = Color.Empty;
						Color c2 = Color.Empty;
						if (flag4)
						{
							c = color;
							c2 = color2;
						}
						bool flag6 = true;
						if (!base.Enabled)
						{
							flag6 = false;
						}
						else if ((this.activation == ItemActivation.OneClick || this.activation == ItemActivation.TwoClick) && (num2 & 71) != 0)
						{
							flag6 = false;
						}
						if (flag6)
						{
							if (!flag4 || c.IsEmpty)
							{
								ptr->clrText = ColorTranslator.ToWin32(this.odCacheForeColor);
							}
							else
							{
								ptr->clrText = ColorTranslator.ToWin32(c);
							}
							if (ptr->clrText == ColorTranslator.ToWin32(SystemColors.HotTrack))
							{
								int num4 = 0;
								bool flag7 = false;
								int num5 = 16711680;
								do
								{
									int num6 = ptr->clrText & num5;
									if (num6 != 0 || num5 == 255)
									{
										int num7 = 16 - num4;
										if (num6 == num5)
										{
											num6 = (num6 >> num7) - 1 << num7;
										}
										else
										{
											num6 = (num6 >> num7) + 1 << num7;
										}
										ptr->clrText = ((ptr->clrText & ~num5) | num6);
										flag7 = true;
									}
									else
									{
										num5 >>= 8;
										num4 += 8;
									}
								}
								while (!flag7);
							}
							if (!flag4 || c2.IsEmpty)
							{
								ptr->clrTextBk = ColorTranslator.ToWin32(this.odCacheBackColor);
							}
							else
							{
								ptr->clrTextBk = ColorTranslator.ToWin32(c2);
							}
						}
						if (!flag4 || font == null)
						{
							if (this.odCacheFont != null)
							{
								SafeNativeMethods.SelectObject(new HandleRef(ptr->nmcd, ptr->nmcd.hdc), new HandleRef(null, this.odCacheFontHandle));
							}
						}
						else
						{
							if (this.odCacheFontHandleWrapper != null)
							{
								this.odCacheFontHandleWrapper.Dispose();
							}
							this.odCacheFontHandleWrapper = new Control.FontHandleWrapper(font);
							SafeNativeMethods.SelectObject(new HandleRef(ptr->nmcd, ptr->nmcd.hdc), new HandleRef(this.odCacheFontHandleWrapper, this.odCacheFontHandleWrapper.Handle));
						}
						if (!flag)
						{
							m.Result = (IntPtr)2;
						}
						if (flag5)
						{
							font.Dispose();
						}
					}
				}
				else if (this.OwnerDraw)
				{
					m.Result = (IntPtr)32;
				}
				else
				{
					m.Result = (IntPtr)34;
					this.odCacheBackColor = this.BackColor;
					this.odCacheForeColor = this.ForeColor;
					this.odCacheFont = this.Font;
					this.odCacheFontHandle = base.FontHandle;
					if (ptr->dwItemType == 1)
					{
						if (this.odCacheFontHandleWrapper != null)
						{
							this.odCacheFontHandleWrapper.Dispose();
						}
						this.odCacheFont = new Font(this.odCacheFont, FontStyle.Bold);
						this.odCacheFontHandleWrapper = new Control.FontHandleWrapper(this.odCacheFont);
						this.odCacheFontHandle = this.odCacheFontHandleWrapper.Handle;
						SafeNativeMethods.SelectObject(new HandleRef(ptr->nmcd, ptr->nmcd.hdc), new HandleRef(this.odCacheFontHandleWrapper, this.odCacheFontHandleWrapper.Handle));
						m.Result = (IntPtr)2;
					}
				}
			}
			catch (Exception ex)
			{
				m.Result = (IntPtr)0;
			}
		}

		// Token: 0x06002D65 RID: 11621 RVA: 0x000CB8E8 File Offset: 0x000C9AE8
		private void DeleteFileName(string fileName)
		{
			if (!string.IsNullOrEmpty(fileName))
			{
				FileIOPermission fileIOPermission = new FileIOPermission(PermissionState.Unrestricted);
				fileIOPermission.Assert();
				try
				{
					FileInfo fileInfo = new FileInfo(fileName);
					if (fileInfo.Exists)
					{
						try
						{
							fileInfo.Delete();
						}
						catch (IOException)
						{
						}
					}
				}
				finally
				{
					PermissionSet.RevertAssert();
				}
			}
		}

		// Token: 0x06002D66 RID: 11622 RVA: 0x000CB948 File Offset: 0x000C9B48
		private void DestroyLVGROUP(NativeMethods.LVGROUP lvgroup)
		{
			if (lvgroup.pszHeader != IntPtr.Zero)
			{
				Marshal.FreeHGlobal(lvgroup.pszHeader);
			}
		}

		// Token: 0x06002D67 RID: 11623 RVA: 0x000CB968 File Offset: 0x000C9B68
		private void DetachImageList(object sender, EventArgs e)
		{
			this.listViewState1[4] = true;
			try
			{
				if (sender == this.imageListSmall)
				{
					this.SmallImageList = null;
				}
				if (sender == this.imageListLarge)
				{
					this.LargeImageList = null;
				}
				if (sender == this.imageListState)
				{
					this.StateImageList = null;
				}
			}
			finally
			{
				this.listViewState1[4] = false;
			}
			this.UpdateListViewItemsLocations();
		}

		// Token: 0x06002D68 RID: 11624 RVA: 0x000CB9D8 File Offset: 0x000C9BD8
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				if (this.imageListSmall != null)
				{
					this.imageListSmall.Disposed -= this.DetachImageList;
					this.imageListSmall = null;
				}
				if (this.imageListLarge != null)
				{
					this.imageListLarge.Disposed -= this.DetachImageList;
					this.imageListLarge = null;
				}
				if (this.imageListState != null)
				{
					this.imageListState.Disposed -= this.DetachImageList;
					this.imageListState = null;
				}
				if (this.columnHeaders != null)
				{
					for (int i = this.columnHeaders.Length - 1; i >= 0; i--)
					{
						this.columnHeaders[i].OwnerListview = null;
						this.columnHeaders[i].Dispose();
					}
					this.columnHeaders = null;
				}
				this.Items.Clear();
				if (this.odCacheFontHandleWrapper != null)
				{
					this.odCacheFontHandleWrapper.Dispose();
					this.odCacheFontHandleWrapper = null;
				}
				if (!string.IsNullOrEmpty(this.backgroundImageFileName) || this.bkImgFileNames != null)
				{
					FileIOPermission fileIOPermission = new FileIOPermission(PermissionState.Unrestricted);
					fileIOPermission.Assert();
					try
					{
						if (!string.IsNullOrEmpty(this.backgroundImageFileName))
						{
							FileInfo fileInfo = new FileInfo(this.backgroundImageFileName);
							try
							{
								fileInfo.Delete();
							}
							catch (IOException)
							{
							}
							this.backgroundImageFileName = string.Empty;
						}
						for (int j = 0; j <= this.bkImgFileNamesCount; j++)
						{
							FileInfo fileInfo = new FileInfo(this.bkImgFileNames[j]);
							try
							{
								fileInfo.Delete();
							}
							catch (IOException)
							{
							}
						}
						this.bkImgFileNames = null;
						this.bkImgFileNamesCount = -1;
					}
					finally
					{
						PermissionSet.RevertAssert();
					}
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x06002D69 RID: 11625 RVA: 0x000CBB84 File Offset: 0x000C9D84
		public void EndUpdate()
		{
			int num = this.updateCounter - 1;
			this.updateCounter = num;
			if (num == 0 && base.Properties.GetObject(ListView.PropDelayedUpdateItems) != null)
			{
				this.ApplyUpdateCachedItems();
			}
			base.EndUpdateInternal();
		}

		// Token: 0x06002D6A RID: 11626 RVA: 0x000CBBC4 File Offset: 0x000C9DC4
		private void EnsureDefaultGroup()
		{
			if (base.IsHandleCreated && this.ComctlSupportsVisualStyles && this.GroupsEnabled && base.SendMessage(4257, this.DefaultGroup.ID, 0) == IntPtr.Zero)
			{
				this.UpdateGroupView();
				this.InsertGroupNative(0, this.DefaultGroup);
			}
		}

		// Token: 0x06002D6B RID: 11627 RVA: 0x000CBC20 File Offset: 0x000C9E20
		public void EnsureVisible(int index)
		{
			if (index < 0 || index >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (base.IsHandleCreated)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4115, index, 0);
			}
		}

		// Token: 0x06002D6C RID: 11628 RVA: 0x000CBC92 File Offset: 0x000C9E92
		public ListViewItem FindItemWithText(string text)
		{
			if (this.Items.Count == 0)
			{
				return null;
			}
			return this.FindItemWithText(text, true, 0, true);
		}

		// Token: 0x06002D6D RID: 11629 RVA: 0x000CBCAD File Offset: 0x000C9EAD
		public ListViewItem FindItemWithText(string text, bool includeSubItemsInSearch, int startIndex)
		{
			return this.FindItemWithText(text, includeSubItemsInSearch, startIndex, true);
		}

		// Token: 0x06002D6E RID: 11630 RVA: 0x000CBCBC File Offset: 0x000C9EBC
		public ListViewItem FindItemWithText(string text, bool includeSubItemsInSearch, int startIndex, bool isPrefixSearch)
		{
			if (startIndex < 0 || startIndex >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("startIndex", SR.GetString("InvalidArgument", new object[]
				{
					"startIndex",
					startIndex.ToString(CultureInfo.CurrentCulture)
				}));
			}
			return this.FindItem(true, text, isPrefixSearch, new Point(0, 0), SearchDirectionHint.Down, startIndex, includeSubItemsInSearch);
		}

		// Token: 0x06002D6F RID: 11631 RVA: 0x000CBD22 File Offset: 0x000C9F22
		public ListViewItem FindNearestItem(SearchDirectionHint dir, Point point)
		{
			return this.FindNearestItem(dir, point.X, point.Y);
		}

		// Token: 0x06002D70 RID: 11632 RVA: 0x000CBD3C File Offset: 0x000C9F3C
		public ListViewItem FindNearestItem(SearchDirectionHint searchDirection, int x, int y)
		{
			if (this.View != View.SmallIcon && this.View != View.LargeIcon)
			{
				throw new InvalidOperationException(SR.GetString("ListViewFindNearestItemWorksOnlyInIconView"));
			}
			if (searchDirection < SearchDirectionHint.Left || searchDirection > SearchDirectionHint.Down)
			{
				throw new ArgumentOutOfRangeException("searchDirection", SR.GetString("InvalidArgument", new object[]
				{
					"searchDirection",
					searchDirection.ToString()
				}));
			}
			ListViewItem itemAt = this.GetItemAt(x, y);
			if (itemAt != null)
			{
				Rectangle bounds = itemAt.Bounds;
				Rectangle itemRect = this.GetItemRect(itemAt.Index, ItemBoundsPortion.Icon);
				switch (searchDirection)
				{
				case SearchDirectionHint.Left:
					x = Math.Max(bounds.Left, itemRect.Left) - 1;
					break;
				case SearchDirectionHint.Up:
					y = Math.Max(bounds.Top, itemRect.Top) - 1;
					break;
				case SearchDirectionHint.Right:
					x = Math.Max(bounds.Left, itemRect.Left) + 1;
					break;
				case SearchDirectionHint.Down:
					y = Math.Max(bounds.Top, itemRect.Top) + 1;
					break;
				}
			}
			return this.FindItem(false, string.Empty, false, new Point(x, y), searchDirection, -1, false);
		}

		// Token: 0x06002D71 RID: 11633 RVA: 0x000CBE60 File Offset: 0x000CA060
		private ListViewItem FindItem(bool isTextSearch, string text, bool isPrefixSearch, Point pt, SearchDirectionHint dir, int startIndex, bool includeSubItemsInSearch)
		{
			if (this.Items.Count == 0)
			{
				return null;
			}
			if (!base.IsHandleCreated)
			{
				this.CreateHandle();
			}
			if (this.VirtualMode)
			{
				SearchForVirtualItemEventArgs searchForVirtualItemEventArgs = new SearchForVirtualItemEventArgs(isTextSearch, isPrefixSearch, includeSubItemsInSearch, text, pt, dir, startIndex);
				this.OnSearchForVirtualItem(searchForVirtualItemEventArgs);
				if (searchForVirtualItemEventArgs.Index != -1)
				{
					return this.Items[searchForVirtualItemEventArgs.Index];
				}
				return null;
			}
			else
			{
				NativeMethods.LVFINDINFO lvfindinfo = default(NativeMethods.LVFINDINFO);
				if (isTextSearch)
				{
					lvfindinfo.flags = 2;
					lvfindinfo.flags |= (isPrefixSearch ? 8 : 0);
					lvfindinfo.psz = text;
				}
				else
				{
					lvfindinfo.flags = 64;
					lvfindinfo.ptX = pt.X;
					lvfindinfo.ptY = pt.Y;
					lvfindinfo.vkDirection = (int)dir;
				}
				lvfindinfo.lParam = IntPtr.Zero;
				int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.LVM_FINDITEM, startIndex - 1, ref lvfindinfo);
				if (num >= 0)
				{
					return this.Items[num];
				}
				if (isTextSearch && includeSubItemsInSearch)
				{
					for (int i = startIndex; i < this.Items.Count; i++)
					{
						ListViewItem listViewItem = this.Items[i];
						for (int j = 0; j < listViewItem.SubItems.Count; j++)
						{
							ListViewItem.ListViewSubItem listViewSubItem = listViewItem.SubItems[j];
							if (string.Equals(text, listViewSubItem.Text, StringComparison.OrdinalIgnoreCase))
							{
								return listViewItem;
							}
							if (isPrefixSearch && CultureInfo.CurrentCulture.CompareInfo.IsPrefix(listViewSubItem.Text, text, CompareOptions.IgnoreCase))
							{
								return listViewItem;
							}
						}
					}
					return null;
				}
				return null;
			}
		}

		// Token: 0x06002D72 RID: 11634 RVA: 0x000CBFF4 File Offset: 0x000CA1F4
		private void ForceCheckBoxUpdate()
		{
			if (this.CheckBoxes && base.IsHandleCreated)
			{
				base.SendMessage(4150, 4, 0);
				base.SendMessage(4150, 4, 4);
				if (this.AutoArrange)
				{
					this.ArrangeIcons(this.Alignment);
				}
			}
		}

		// Token: 0x06002D73 RID: 11635 RVA: 0x000CC044 File Offset: 0x000CA244
		private string GenerateRandomName()
		{
			Bitmap bitmap = new Bitmap(this.BackgroundImage);
			int num = 0;
			try
			{
				num = (int)((long)bitmap.GetHicon());
			}
			catch
			{
				bitmap.Dispose();
			}
			Random random;
			if (num == 0)
			{
				random = new Random((int)DateTime.Now.Ticks);
			}
			else
			{
				random = new Random(num);
			}
			return random.Next().ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x06002D74 RID: 11636 RVA: 0x000CC0BC File Offset: 0x000CA2BC
		private int GenerateUniqueID()
		{
			int num = this.nextID;
			this.nextID = num + 1;
			int num2 = num;
			if (num2 == -1)
			{
				num2 = 0;
				this.nextID = 1;
			}
			return num2;
		}

		// Token: 0x06002D75 RID: 11637 RVA: 0x000CC0EC File Offset: 0x000CA2EC
		internal int GetDisplayIndex(ListViewItem item, int lastIndex)
		{
			this.ApplyUpdateCachedItems();
			if (base.IsHandleCreated && !this.ListViewHandleDestroyed)
			{
				NativeMethods.LVFINDINFO lvfindinfo = default(NativeMethods.LVFINDINFO);
				lvfindinfo.lParam = (IntPtr)item.ID;
				lvfindinfo.flags = 1;
				int num = -1;
				if (lastIndex != -1)
				{
					num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.LVM_FINDITEM, lastIndex - 1, ref lvfindinfo);
				}
				if (num == -1)
				{
					num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.LVM_FINDITEM, -1, ref lvfindinfo);
				}
				return num;
			}
			int num2 = 0;
			foreach (object obj in this.listItemsArray)
			{
				if (obj == item)
				{
					return num2;
				}
				num2++;
			}
			return -1;
		}

		// Token: 0x06002D76 RID: 11638 RVA: 0x000CC1D8 File Offset: 0x000CA3D8
		internal int GetColumnIndex(ColumnHeader ch)
		{
			if (this.columnHeaders == null)
			{
				return -1;
			}
			for (int i = 0; i < this.columnHeaders.Length; i++)
			{
				if (this.columnHeaders[i] == ch)
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06002D77 RID: 11639 RVA: 0x000CC210 File Offset: 0x000CA410
		public ListViewItem GetItemAt(int x, int y)
		{
			NativeMethods.LVHITTESTINFO lvhittestinfo = new NativeMethods.LVHITTESTINFO();
			lvhittestinfo.pt_x = x;
			lvhittestinfo.pt_y = y;
			int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4114, 0, lvhittestinfo);
			ListViewItem result = null;
			if (num >= 0 && (lvhittestinfo.flags & 14) != 0)
			{
				result = this.Items[num];
			}
			return result;
		}

		// Token: 0x06002D78 RID: 11640 RVA: 0x000CC26E File Offset: 0x000CA46E
		internal int GetNativeGroupId(ListViewItem item)
		{
			item.UpdateGroupFromName();
			if (item.Group != null && this.Groups.Contains(item.Group))
			{
				return item.Group.ID;
			}
			this.EnsureDefaultGroup();
			return this.DefaultGroup.ID;
		}

		// Token: 0x06002D79 RID: 11641 RVA: 0x000CC2B0 File Offset: 0x000CA4B0
		internal void GetSubItemAt(int x, int y, out int iItem, out int iSubItem)
		{
			NativeMethods.LVHITTESTINFO lvhittestinfo = new NativeMethods.LVHITTESTINFO();
			lvhittestinfo.pt_x = x;
			lvhittestinfo.pt_y = y;
			int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4153, 0, lvhittestinfo);
			if (num > -1)
			{
				iItem = lvhittestinfo.iItem;
				iSubItem = lvhittestinfo.iSubItem;
				return;
			}
			iItem = -1;
			iSubItem = -1;
		}

		// Token: 0x06002D7A RID: 11642 RVA: 0x000CC30C File Offset: 0x000CA50C
		internal Point GetItemPosition(int index)
		{
			NativeMethods.POINT point = new NativeMethods.POINT();
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4112, index, point);
			return new Point(point.x, point.y);
		}

		// Token: 0x06002D7B RID: 11643 RVA: 0x000CC349 File Offset: 0x000CA549
		internal int GetItemState(int index)
		{
			return this.GetItemState(index, 65295);
		}

		// Token: 0x06002D7C RID: 11644 RVA: 0x000CC358 File Offset: 0x000CA558
		internal int GetItemState(int index, int mask)
		{
			if (index < 0 || (this.VirtualMode && index >= this.VirtualListSize) || (!this.VirtualMode && index >= this.itemCount))
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			return (int)((long)base.SendMessage(4140, index, mask));
		}

		// Token: 0x06002D7D RID: 11645 RVA: 0x000CC3D0 File Offset: 0x000CA5D0
		public Rectangle GetItemRect(int index)
		{
			return this.GetItemRect(index, ItemBoundsPortion.Entire);
		}

		// Token: 0x06002D7E RID: 11646 RVA: 0x000CC3DC File Offset: 0x000CA5DC
		public Rectangle GetItemRect(int index, ItemBoundsPortion portion)
		{
			if (index < 0 || index >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (!ClientUtils.IsEnumValid(portion, (int)portion, 0, 3))
			{
				throw new InvalidEnumArgumentException("portion", (int)portion, typeof(ItemBoundsPortion));
			}
			if (this.View == View.Details && this.Columns.Count == 0)
			{
				return Rectangle.Empty;
			}
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			rect.left = (int)portion;
			if ((int)((long)base.SendMessage(4110, index, ref rect)) == 0)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
		}

		// Token: 0x06002D7F RID: 11647 RVA: 0x000CC4E0 File Offset: 0x000CA6E0
		private Rectangle GetItemRectOrEmpty(int index)
		{
			if (index < 0 || index >= this.Items.Count)
			{
				return Rectangle.Empty;
			}
			if (this.View == View.Details && this.Columns.Count == 0)
			{
				return Rectangle.Empty;
			}
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			rect.left = 0;
			if ((int)((long)base.SendMessage(4110, index, ref rect)) == 0)
			{
				return Rectangle.Empty;
			}
			return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
		}

		// Token: 0x06002D80 RID: 11648 RVA: 0x000CC56C File Offset: 0x000CA76C
		private NativeMethods.LVGROUP GetLVGROUP(ListViewGroup group)
		{
			NativeMethods.LVGROUP lvgroup = new NativeMethods.LVGROUP();
			lvgroup.mask = 25U;
			string header = group.Header;
			lvgroup.pszHeader = Marshal.StringToHGlobalAuto(header);
			lvgroup.cchHeader = header.Length;
			lvgroup.iGroupId = group.ID;
			switch (group.HeaderAlignment)
			{
			case HorizontalAlignment.Left:
				lvgroup.uAlign = 1U;
				break;
			case HorizontalAlignment.Right:
				lvgroup.uAlign = 4U;
				break;
			case HorizontalAlignment.Center:
				lvgroup.uAlign = 2U;
				break;
			}
			return lvgroup;
		}

		// Token: 0x06002D81 RID: 11649 RVA: 0x000CC5E7 File Offset: 0x000CA7E7
		internal Rectangle GetSubItemRect(int itemIndex, int subItemIndex)
		{
			return this.GetSubItemRect(itemIndex, subItemIndex, ItemBoundsPortion.Entire);
		}

		// Token: 0x06002D82 RID: 11650 RVA: 0x000CC5F4 File Offset: 0x000CA7F4
		internal Rectangle GetSubItemRect(int itemIndex, int subItemIndex, ItemBoundsPortion portion)
		{
			if (this.View != View.Details)
			{
				return Rectangle.Empty;
			}
			if (itemIndex < 0 || itemIndex >= this.Items.Count)
			{
				throw new ArgumentOutOfRangeException("itemIndex", SR.GetString("InvalidArgument", new object[]
				{
					"itemIndex",
					itemIndex.ToString(CultureInfo.CurrentCulture)
				}));
			}
			int count = this.Items[itemIndex].SubItems.Count;
			if (subItemIndex < 0 || subItemIndex >= count)
			{
				throw new ArgumentOutOfRangeException("subItemIndex", SR.GetString("InvalidArgument", new object[]
				{
					"subItemIndex",
					subItemIndex.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (!ClientUtils.IsEnumValid(portion, (int)portion, 0, 3))
			{
				throw new InvalidEnumArgumentException("portion", (int)portion, typeof(ItemBoundsPortion));
			}
			if (this.Columns.Count == 0)
			{
				return Rectangle.Empty;
			}
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			rect.left = (int)portion;
			rect.top = subItemIndex;
			if ((int)((long)base.SendMessage(4152, itemIndex, ref rect)) == 0)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					"itemIndex",
					itemIndex.ToString(CultureInfo.CurrentCulture)
				}));
			}
			return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
		}

		// Token: 0x06002D83 RID: 11651 RVA: 0x000CC758 File Offset: 0x000CA958
		public ListViewHitTestInfo HitTest(Point point)
		{
			return this.HitTest(point.X, point.Y);
		}

		// Token: 0x06002D84 RID: 11652 RVA: 0x000CC770 File Offset: 0x000CA970
		public ListViewHitTestInfo HitTest(int x, int y)
		{
			if (!base.ClientRectangle.Contains(x, y))
			{
				return new ListViewHitTestInfo(null, null, ListViewHitTestLocations.None);
			}
			NativeMethods.LVHITTESTINFO lvhittestinfo = new NativeMethods.LVHITTESTINFO();
			lvhittestinfo.pt_x = x;
			lvhittestinfo.pt_y = y;
			int num;
			if (this.View == View.Details)
			{
				num = (int)((long)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4153, 0, lvhittestinfo));
			}
			else
			{
				num = (int)((long)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4114, 0, lvhittestinfo));
			}
			ListViewItem listViewItem = (num == -1) ? null : this.Items[num];
			ListViewHitTestLocations hitLocation;
			if (listViewItem == null && (8 & lvhittestinfo.flags) == 8)
			{
				hitLocation = (ListViewHitTestLocations)((247 & lvhittestinfo.flags) | 256);
			}
			else if (listViewItem != null && (8 & lvhittestinfo.flags) == 8)
			{
				hitLocation = (ListViewHitTestLocations)((247 & lvhittestinfo.flags) | 512);
			}
			else
			{
				hitLocation = (ListViewHitTestLocations)lvhittestinfo.flags;
			}
			if (this.View != View.Details || listViewItem == null)
			{
				return new ListViewHitTestInfo(listViewItem, null, hitLocation);
			}
			if (lvhittestinfo.iSubItem < listViewItem.SubItems.Count)
			{
				return new ListViewHitTestInfo(listViewItem, listViewItem.SubItems[lvhittestinfo.iSubItem], hitLocation);
			}
			return new ListViewHitTestInfo(listViewItem, null, hitLocation);
		}

		// Token: 0x06002D85 RID: 11653 RVA: 0x000CC8A4 File Offset: 0x000CAAA4
		private void InvalidateColumnHeaders()
		{
			if (this.viewStyle == View.Details && base.IsHandleCreated)
			{
				IntPtr intPtr = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4127, 0, 0);
				if (intPtr != IntPtr.Zero)
				{
					SafeNativeMethods.InvalidateRect(new HandleRef(this, intPtr), null, true);
				}
			}
		}

		// Token: 0x06002D86 RID: 11654 RVA: 0x000CC8F7 File Offset: 0x000CAAF7
		internal ColumnHeader InsertColumn(int index, ColumnHeader ch)
		{
			return this.InsertColumn(index, ch, true);
		}

		// Token: 0x06002D87 RID: 11655 RVA: 0x000CC904 File Offset: 0x000CAB04
		internal ColumnHeader InsertColumn(int index, ColumnHeader ch, bool refreshSubItems)
		{
			if (ch == null)
			{
				throw new ArgumentNullException("ch");
			}
			if (ch.OwnerListview != null)
			{
				throw new ArgumentException(SR.GetString("OnlyOneControl", new object[]
				{
					ch.Text
				}), "ch");
			}
			int num;
			if (base.IsHandleCreated && this.View != View.Tile)
			{
				num = this.InsertColumnNative(index, ch);
			}
			else
			{
				num = index;
			}
			if (-1 == num)
			{
				throw new InvalidOperationException(SR.GetString("ListViewAddColumnFailed"));
			}
			int num2 = (this.columnHeaders == null) ? 0 : this.columnHeaders.Length;
			if (num2 > 0)
			{
				ColumnHeader[] destinationArray = new ColumnHeader[num2 + 1];
				if (num2 > 0)
				{
					Array.Copy(this.columnHeaders, 0, destinationArray, 0, num2);
				}
				this.columnHeaders = destinationArray;
			}
			else
			{
				this.columnHeaders = new ColumnHeader[1];
			}
			if (num < num2)
			{
				Array.Copy(this.columnHeaders, num, this.columnHeaders, num + 1, num2 - num);
			}
			this.columnHeaders[num] = ch;
			ch.OwnerListview = this;
			if (ch.ActualImageIndex_Internal != -1 && base.IsHandleCreated && this.View != View.Tile)
			{
				this.SetColumnInfo(16, ch);
			}
			int[] array = new int[this.Columns.Count];
			for (int i = 0; i < this.Columns.Count; i++)
			{
				ColumnHeader columnHeader = this.Columns[i];
				if (columnHeader == ch)
				{
					columnHeader.DisplayIndexInternal = index;
				}
				else if (columnHeader.DisplayIndex >= index)
				{
					ColumnHeader columnHeader2 = columnHeader;
					int displayIndexInternal = columnHeader2.DisplayIndexInternal;
					columnHeader2.DisplayIndexInternal = displayIndexInternal + 1;
				}
				array[i] = columnHeader.DisplayIndexInternal;
			}
			this.SetDisplayIndices(array);
			if (base.IsHandleCreated && this.View == View.Tile)
			{
				this.RecreateHandleInternal();
			}
			else if (base.IsHandleCreated && refreshSubItems)
			{
				this.RealizeAllSubItems();
			}
			return ch;
		}

		// Token: 0x06002D88 RID: 11656 RVA: 0x000CCAB8 File Offset: 0x000CACB8
		private int InsertColumnNative(int index, ColumnHeader ch)
		{
			NativeMethods.LVCOLUMN_T lvcolumn_T = new NativeMethods.LVCOLUMN_T();
			lvcolumn_T.mask = 7;
			if (ch.OwnerListview != null && ch.ActualImageIndex_Internal != -1)
			{
				lvcolumn_T.mask |= 16;
				lvcolumn_T.iImage = ch.ActualImageIndex_Internal;
			}
			lvcolumn_T.fmt = (int)ch.TextAlign;
			lvcolumn_T.cx = ch.Width;
			lvcolumn_T.pszText = ch.Text;
			return (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.LVM_INSERTCOLUMN, index, lvcolumn_T);
		}

		// Token: 0x06002D89 RID: 11657 RVA: 0x000CCB40 File Offset: 0x000CAD40
		internal void InsertGroupInListView(int index, ListViewGroup group)
		{
			bool flag = this.groups.Count == 1 && this.GroupsEnabled;
			this.UpdateGroupView();
			this.EnsureDefaultGroup();
			this.InsertGroupNative(index, group);
			if (flag)
			{
				for (int i = 0; i < this.Items.Count; i++)
				{
					ListViewItem listViewItem = this.Items[i];
					if (listViewItem.Group == null)
					{
						listViewItem.UpdateStateToListView(listViewItem.Index);
					}
				}
			}
		}

		// Token: 0x06002D8A RID: 11658 RVA: 0x000CCBB4 File Offset: 0x000CADB4
		private void InsertGroupNative(int index, ListViewGroup group)
		{
			NativeMethods.LVGROUP lvgroup = new NativeMethods.LVGROUP();
			try
			{
				lvgroup = this.GetLVGROUP(group);
				int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4241, index, lvgroup);
			}
			finally
			{
				this.DestroyLVGROUP(lvgroup);
			}
		}

		// Token: 0x06002D8B RID: 11659 RVA: 0x000CCC08 File Offset: 0x000CAE08
		private void InsertItems(int displayIndex, ListViewItem[] items, bool checkHosting)
		{
			if (items == null || items.Length == 0)
			{
				return;
			}
			if (base.IsHandleCreated && this.Items.Count == 0 && this.View == View.SmallIcon && this.ComctlSupportsVisualStyles)
			{
				this.FlipViewToLargeIconAndSmallIcon = true;
			}
			if (this.updateCounter > 0 && base.Properties.GetObject(ListView.PropDelayedUpdateItems) != null)
			{
				if (checkHosting)
				{
					for (int i = 0; i < items.Length; i++)
					{
						if (items[i].listView != null)
						{
							throw new ArgumentException(SR.GetString("OnlyOneControl", new object[]
							{
								items[i].Text
							}), "item");
						}
					}
				}
				ArrayList arrayList = (ArrayList)base.Properties.GetObject(ListView.PropDelayedUpdateItems);
				if (arrayList != null)
				{
					arrayList.AddRange(items);
				}
				for (int j = 0; j < items.Length; j++)
				{
					items[j].Host(this, this.GenerateUniqueID(), -1);
				}
				this.FlipViewToLargeIconAndSmallIcon = false;
				return;
			}
			for (int k = 0; k < items.Length; k++)
			{
				ListViewItem listViewItem = items[k];
				if (checkHosting && listViewItem.listView != null)
				{
					throw new ArgumentException(SR.GetString("OnlyOneControl", new object[]
					{
						listViewItem.Text
					}), "item");
				}
				int num = this.GenerateUniqueID();
				this.listItemsTable.Add(num, listViewItem);
				this.itemCount++;
				listViewItem.Host(this, num, -1);
				if (!base.IsHandleCreated)
				{
					this.listItemsArray.Insert(displayIndex + k, listViewItem);
				}
			}
			if (base.IsHandleCreated)
			{
				this.InsertItemsNative(displayIndex, items);
			}
			base.Invalidate();
			this.ArrangeIcons(this.alignStyle);
			if (!this.VirtualMode)
			{
				this.Sort();
			}
		}

		// Token: 0x06002D8C RID: 11660 RVA: 0x000CCDBC File Offset: 0x000CAFBC
		private int InsertItemsNative(int index, ListViewItem[] items)
		{
			if (items == null || items.Length == 0)
			{
				return 0;
			}
			if (index == this.itemCount - 1)
			{
				index++;
			}
			NativeMethods.LVITEM lvitem = default(NativeMethods.LVITEM);
			int num = -1;
			IntPtr intPtr = IntPtr.Zero;
			int num2 = 0;
			this.listViewState1[1] = true;
			try
			{
				base.SendMessage(4143, this.itemCount, 0);
				for (int i = 0; i < items.Length; i++)
				{
					ListViewItem listViewItem = items[i];
					lvitem.Reset();
					lvitem.mask = 23;
					lvitem.iItem = index + i;
					lvitem.pszText = listViewItem.Text;
					lvitem.iImage = listViewItem.ImageIndexer.ActualIndex;
					lvitem.iIndent = listViewItem.IndentCount;
					lvitem.lParam = (IntPtr)listViewItem.ID;
					if (this.GroupsEnabled)
					{
						lvitem.mask |= 256;
						lvitem.iGroupId = this.GetNativeGroupId(listViewItem);
					}
					lvitem.mask |= 512;
					lvitem.cColumns = ((this.columnHeaders != null) ? Math.Min(20, this.columnHeaders.Length) : 0);
					if (lvitem.cColumns > num2 || intPtr == IntPtr.Zero)
					{
						if (intPtr != IntPtr.Zero)
						{
							Marshal.FreeHGlobal(intPtr);
						}
						intPtr = Marshal.AllocHGlobal(lvitem.cColumns * Marshal.SizeOf(typeof(int)));
						num2 = lvitem.cColumns;
					}
					lvitem.puColumns = intPtr;
					int[] array = new int[lvitem.cColumns];
					for (int j = 0; j < lvitem.cColumns; j++)
					{
						array[j] = j + 1;
					}
					Marshal.Copy(array, 0, lvitem.puColumns, lvitem.cColumns);
					ItemCheckEventHandler itemCheckEventHandler = this.onItemCheck;
					this.onItemCheck = null;
					int num3;
					try
					{
						listViewItem.UpdateStateToListView(lvitem.iItem, ref lvitem, false);
						num3 = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.LVM_INSERTITEM, 0, ref lvitem);
						if (num == -1)
						{
							num = num3;
							index = num;
						}
					}
					finally
					{
						this.onItemCheck = itemCheckEventHandler;
					}
					if (-1 == num3)
					{
						throw new InvalidOperationException(SR.GetString("ListViewAddItemFailed"));
					}
					for (int k = 1; k < listViewItem.SubItems.Count; k++)
					{
						this.SetItemText(num3, k, listViewItem.SubItems[k].Text, ref lvitem);
					}
					if (listViewItem.StateImageSet || listViewItem.StateSelected)
					{
						this.SetItemState(num3, lvitem.state, lvitem.stateMask);
					}
				}
			}
			finally
			{
				if (intPtr != IntPtr.Zero)
				{
					Marshal.FreeHGlobal(intPtr);
				}
				this.listViewState1[1] = false;
			}
			if (this.listViewState1[16])
			{
				this.listViewState1[16] = false;
				this.OnSelectedIndexChanged(EventArgs.Empty);
			}
			if (this.FlipViewToLargeIconAndSmallIcon)
			{
				this.FlipViewToLargeIconAndSmallIcon = false;
				this.View = View.LargeIcon;
				this.View = View.SmallIcon;
			}
			return num;
		}

		// Token: 0x06002D8D RID: 11661 RVA: 0x000CD0E0 File Offset: 0x000CB2E0
		protected override bool IsInputKey(Keys keyData)
		{
			if ((keyData & Keys.Alt) == Keys.Alt)
			{
				return false;
			}
			Keys keys = keyData & Keys.KeyCode;
			if (keys - Keys.Prior <= 3)
			{
				return true;
			}
			bool flag = base.IsInputKey(keyData);
			if (flag)
			{
				return true;
			}
			if (this.listViewState[16384])
			{
				Keys keys2 = keyData & Keys.KeyCode;
				if (keys2 == Keys.Return || keys2 == Keys.Escape)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x06002D8E RID: 11662 RVA: 0x000CD144 File Offset: 0x000CB344
		private void LargeImageListRecreateHandle(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				IntPtr lparam = (this.LargeImageList == null) ? IntPtr.Zero : this.LargeImageList.Handle;
				base.SendMessage(4099, (IntPtr)0, lparam);
				this.ForceCheckBoxUpdate();
			}
		}

		// Token: 0x06002D8F RID: 11663 RVA: 0x000CD190 File Offset: 0x000CB390
		private void LargeImageListChangedHandle(object sender, EventArgs e)
		{
			if (!this.VirtualMode && sender != null && sender == this.imageListLarge && base.IsHandleCreated)
			{
				foreach (object obj in this.Items)
				{
					ListViewItem listViewItem = (ListViewItem)obj;
					if (listViewItem.ImageIndexer.ActualIndex != -1 && listViewItem.ImageIndexer.ActualIndex >= this.imageListLarge.Images.Count)
					{
						this.SetItemImage(listViewItem.Index, this.imageListLarge.Images.Count - 1);
					}
					else
					{
						this.SetItemImage(listViewItem.Index, listViewItem.ImageIndexer.ActualIndex);
					}
				}
			}
		}

		// Token: 0x06002D90 RID: 11664 RVA: 0x000CD26C File Offset: 0x000CB46C
		internal void ListViewItemToolTipChanged(ListViewItem item)
		{
			if (base.IsHandleCreated)
			{
				this.SetItemText(item.Index, 0, item.Text);
			}
		}

		// Token: 0x06002D91 RID: 11665 RVA: 0x000CD28C File Offset: 0x000CB48C
		private void LvnBeginDrag(MouseButtons buttons, NativeMethods.NMLISTVIEW nmlv)
		{
			ListViewItem item = this.Items[nmlv.iItem];
			this.OnItemDrag(new ItemDragEventArgs(buttons, item));
		}

		// Token: 0x06002D92 RID: 11666 RVA: 0x000CD2B8 File Offset: 0x000CB4B8
		protected virtual void OnAfterLabelEdit(LabelEditEventArgs e)
		{
			if (this.onAfterLabelEdit != null)
			{
				this.onAfterLabelEdit(this, e);
			}
		}

		// Token: 0x06002D93 RID: 11667 RVA: 0x000CD2CF File Offset: 0x000CB4CF
		protected override void OnBackgroundImageChanged(EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				this.SetBackgroundImage();
			}
			base.OnBackgroundImageChanged(e);
		}

		// Token: 0x06002D94 RID: 11668 RVA: 0x000CD2E6 File Offset: 0x000CB4E6
		protected override void OnMouseLeave(EventArgs e)
		{
			this.hoveredAlready = false;
			base.OnMouseLeave(e);
		}

		// Token: 0x06002D95 RID: 11669 RVA: 0x000CD2F8 File Offset: 0x000CB4F8
		protected override void OnMouseHover(EventArgs e)
		{
			ListViewItem listViewItem = null;
			if (this.Items.Count > 0)
			{
				Point p = Cursor.Position;
				p = base.PointToClientInternal(p);
				listViewItem = this.GetItemAt(p.X, p.Y);
			}
			if (listViewItem != this.prevHoveredItem && listViewItem != null)
			{
				this.OnItemMouseHover(new ListViewItemMouseHoverEventArgs(listViewItem));
				this.prevHoveredItem = listViewItem;
			}
			if (!this.hoveredAlready)
			{
				base.OnMouseHover(e);
				this.hoveredAlready = true;
			}
			base.ResetMouseEventArgs();
		}

		// Token: 0x06002D96 RID: 11670 RVA: 0x000CD373 File Offset: 0x000CB573
		protected virtual void OnBeforeLabelEdit(LabelEditEventArgs e)
		{
			if (this.onBeforeLabelEdit != null)
			{
				this.onBeforeLabelEdit(this, e);
			}
		}

		// Token: 0x06002D97 RID: 11671 RVA: 0x000CD38C File Offset: 0x000CB58C
		protected virtual void OnCacheVirtualItems(CacheVirtualItemsEventArgs e)
		{
			CacheVirtualItemsEventHandler cacheVirtualItemsEventHandler = (CacheVirtualItemsEventHandler)base.Events[ListView.EVENT_CACHEVIRTUALITEMS];
			if (cacheVirtualItemsEventHandler != null)
			{
				cacheVirtualItemsEventHandler(this, e);
			}
		}

		// Token: 0x06002D98 RID: 11672 RVA: 0x000CD3BA File Offset: 0x000CB5BA
		protected virtual void OnColumnClick(ColumnClickEventArgs e)
		{
			if (this.onColumnClick != null)
			{
				this.onColumnClick(this, e);
			}
		}

		// Token: 0x06002D99 RID: 11673 RVA: 0x000CD3D4 File Offset: 0x000CB5D4
		protected virtual void OnColumnReordered(ColumnReorderedEventArgs e)
		{
			ColumnReorderedEventHandler columnReorderedEventHandler = (ColumnReorderedEventHandler)base.Events[ListView.EVENT_COLUMNREORDERED];
			if (columnReorderedEventHandler != null)
			{
				columnReorderedEventHandler(this, e);
			}
		}

		// Token: 0x06002D9A RID: 11674 RVA: 0x000CD404 File Offset: 0x000CB604
		protected virtual void OnColumnWidthChanged(ColumnWidthChangedEventArgs e)
		{
			ColumnWidthChangedEventHandler columnWidthChangedEventHandler = (ColumnWidthChangedEventHandler)base.Events[ListView.EVENT_COLUMNWIDTHCHANGED];
			if (columnWidthChangedEventHandler != null)
			{
				columnWidthChangedEventHandler(this, e);
			}
		}

		// Token: 0x06002D9B RID: 11675 RVA: 0x000CD434 File Offset: 0x000CB634
		protected virtual void OnColumnWidthChanging(ColumnWidthChangingEventArgs e)
		{
			ColumnWidthChangingEventHandler columnWidthChangingEventHandler = (ColumnWidthChangingEventHandler)base.Events[ListView.EVENT_COLUMNWIDTHCHANGING];
			if (columnWidthChangingEventHandler != null)
			{
				columnWidthChangingEventHandler(this, e);
			}
		}

		// Token: 0x06002D9C RID: 11676 RVA: 0x000CD464 File Offset: 0x000CB664
		protected virtual void OnDrawColumnHeader(DrawListViewColumnHeaderEventArgs e)
		{
			DrawListViewColumnHeaderEventHandler drawListViewColumnHeaderEventHandler = (DrawListViewColumnHeaderEventHandler)base.Events[ListView.EVENT_DRAWCOLUMNHEADER];
			if (drawListViewColumnHeaderEventHandler != null)
			{
				drawListViewColumnHeaderEventHandler(this, e);
			}
		}

		// Token: 0x06002D9D RID: 11677 RVA: 0x000CD494 File Offset: 0x000CB694
		protected virtual void OnDrawItem(DrawListViewItemEventArgs e)
		{
			DrawListViewItemEventHandler drawListViewItemEventHandler = (DrawListViewItemEventHandler)base.Events[ListView.EVENT_DRAWITEM];
			if (drawListViewItemEventHandler != null)
			{
				drawListViewItemEventHandler(this, e);
			}
		}

		// Token: 0x06002D9E RID: 11678 RVA: 0x000CD4C4 File Offset: 0x000CB6C4
		protected virtual void OnDrawSubItem(DrawListViewSubItemEventArgs e)
		{
			DrawListViewSubItemEventHandler drawListViewSubItemEventHandler = (DrawListViewSubItemEventHandler)base.Events[ListView.EVENT_DRAWSUBITEM];
			if (drawListViewSubItemEventHandler != null)
			{
				drawListViewSubItemEventHandler(this, e);
			}
		}

		// Token: 0x06002D9F RID: 11679 RVA: 0x000CD4F4 File Offset: 0x000CB6F4
		protected override void OnFontChanged(EventArgs e)
		{
			base.OnFontChanged(e);
			if (!this.VirtualMode && base.IsHandleCreated && this.AutoArrange)
			{
				this.BeginUpdate();
				try
				{
					base.SendMessage(4138, -1, 0);
				}
				finally
				{
					this.EndUpdate();
				}
			}
			this.InvalidateColumnHeaders();
		}

		// Token: 0x06002DA0 RID: 11680 RVA: 0x000CD554 File Offset: 0x000CB754
		protected override void OnHandleCreated(EventArgs e)
		{
			this.listViewState[4194304] = false;
			this.FlipViewToLargeIconAndSmallIcon = false;
			base.OnHandleCreated(e);
			int num = (int)((long)base.SendMessage(8200, 0, 0));
			if (num < 5)
			{
				base.SendMessage(8199, 5, 0);
			}
			this.UpdateExtendedStyles();
			this.RealizeProperties();
			int lparam = ColorTranslator.ToWin32(this.BackColor);
			base.SendMessage(4097, 0, lparam);
			base.SendMessage(4132, 0, ColorTranslator.ToWin32(base.ForeColor));
			base.SendMessage(4134, 0, -1);
			if (!this.Scrollable)
			{
				int num2 = (int)((long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this, base.Handle), -16));
				num2 |= 8192;
				UnsafeNativeMethods.SetWindowLong(new HandleRef(this, base.Handle), -16, new HandleRef(null, (IntPtr)num2));
			}
			if (this.VirtualMode)
			{
				int num3 = (int)((long)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4106, 0, 0));
				num3 |= 61440;
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4107, num3, 0);
			}
			if (this.ComctlSupportsVisualStyles)
			{
				base.SendMessage(4238, (int)this.viewStyle, 0);
				this.UpdateGroupView();
				if (this.groups != null)
				{
					for (int i = 0; i < this.groups.Count; i++)
					{
						this.InsertGroupNative(i, this.groups[i]);
					}
				}
				if (this.viewStyle == View.Tile)
				{
					this.UpdateTileView();
				}
			}
			this.ListViewHandleDestroyed = false;
			ListViewItem[] array = null;
			if (this.listItemsArray != null)
			{
				array = (ListViewItem[])this.listItemsArray.ToArray(typeof(ListViewItem));
				this.listItemsArray = null;
			}
			int num4 = (this.columnHeaders == null) ? 0 : this.columnHeaders.Length;
			if (num4 > 0)
			{
				int[] array2 = new int[this.columnHeaders.Length];
				int num5 = 0;
				foreach (ColumnHeader columnHeader in this.columnHeaders)
				{
					array2[num5] = columnHeader.DisplayIndex;
					this.InsertColumnNative(num5++, columnHeader);
				}
				this.SetDisplayIndices(array2);
			}
			if (this.itemCount > 0 && array != null)
			{
				this.InsertItemsNative(0, array);
			}
			if (this.VirtualMode && this.VirtualListSize > -1 && !base.DesignMode)
			{
				base.SendMessage(4143, this.VirtualListSize, 0);
			}
			if (num4 > 0)
			{
				this.UpdateColumnWidths(ColumnHeaderAutoResizeStyle.None);
			}
			this.ArrangeIcons(this.alignStyle);
			this.UpdateListViewItemsLocations();
			if (!this.VirtualMode)
			{
				this.Sort();
			}
			if (this.ComctlSupportsVisualStyles && this.InsertionMark.Index > 0)
			{
				this.InsertionMark.UpdateListView();
			}
			this.savedCheckedItems = null;
			if (!this.CheckBoxes && !this.VirtualMode)
			{
				for (int k = 0; k < this.Items.Count; k++)
				{
					if (this.Items[k].Checked)
					{
						this.UpdateSavedCheckedItems(this.Items[k], true);
					}
				}
			}
		}

		// Token: 0x06002DA1 RID: 11681 RVA: 0x000CD87C File Offset: 0x000CBA7C
		protected override void OnHandleDestroyed(EventArgs e)
		{
			if (!base.Disposing && !this.VirtualMode)
			{
				int count = this.Items.Count;
				for (int i = 0; i < count; i++)
				{
					this.Items[i].UpdateStateFromListView(i, true);
				}
				if (this.SelectedItems != null && !this.VirtualMode)
				{
					ListViewItem[] array = new ListViewItem[this.SelectedItems.Count];
					this.SelectedItems.CopyTo(array, 0);
					this.savedSelectedItems = new List<ListViewItem>(array.Length);
					for (int j = 0; j < array.Length; j++)
					{
						this.savedSelectedItems.Add(array[j]);
					}
				}
				ListViewItem[] array2 = null;
				ListView.ListViewItemCollection items = this.Items;
				if (items != null)
				{
					array2 = new ListViewItem[items.Count];
					items.CopyTo(array2, 0);
				}
				if (array2 != null)
				{
					this.listItemsArray = new ArrayList(array2.Length);
					this.listItemsArray.AddRange(array2);
				}
				this.ListViewHandleDestroyed = true;
			}
			base.OnHandleDestroyed(e);
		}

		// Token: 0x06002DA2 RID: 11682 RVA: 0x000CD977 File Offset: 0x000CBB77
		protected virtual void OnItemActivate(EventArgs e)
		{
			if (this.onItemActivate != null)
			{
				this.onItemActivate(this, e);
			}
		}

		// Token: 0x06002DA3 RID: 11683 RVA: 0x000CD98E File Offset: 0x000CBB8E
		protected virtual void OnItemCheck(ItemCheckEventArgs ice)
		{
			if (this.onItemCheck != null)
			{
				this.onItemCheck(this, ice);
			}
		}

		// Token: 0x06002DA4 RID: 11684 RVA: 0x000CD9A5 File Offset: 0x000CBBA5
		protected virtual void OnItemChecked(ItemCheckedEventArgs e)
		{
			if (this.onItemChecked != null)
			{
				this.onItemChecked(this, e);
			}
		}

		// Token: 0x06002DA5 RID: 11685 RVA: 0x000CD9BC File Offset: 0x000CBBBC
		protected virtual void OnItemDrag(ItemDragEventArgs e)
		{
			if (this.onItemDrag != null)
			{
				this.onItemDrag(this, e);
			}
		}

		// Token: 0x06002DA6 RID: 11686 RVA: 0x000CD9D3 File Offset: 0x000CBBD3
		protected virtual void OnItemMouseHover(ListViewItemMouseHoverEventArgs e)
		{
			if (this.onItemMouseHover != null)
			{
				this.onItemMouseHover(this, e);
			}
		}

		// Token: 0x06002DA7 RID: 11687 RVA: 0x000CD9EC File Offset: 0x000CBBEC
		protected virtual void OnItemSelectionChanged(ListViewItemSelectionChangedEventArgs e)
		{
			ListViewItemSelectionChangedEventHandler listViewItemSelectionChangedEventHandler = (ListViewItemSelectionChangedEventHandler)base.Events[ListView.EVENT_ITEMSELECTIONCHANGED];
			if (listViewItemSelectionChangedEventHandler != null)
			{
				listViewItemSelectionChangedEventHandler(this, e);
			}
		}

		// Token: 0x06002DA8 RID: 11688 RVA: 0x000CDA1A File Offset: 0x000CBC1A
		protected override void OnParentChanged(EventArgs e)
		{
			base.OnParentChanged(e);
			if (base.IsHandleCreated)
			{
				this.RecreateHandleInternal();
			}
		}

		// Token: 0x06002DA9 RID: 11689 RVA: 0x000CDA31 File Offset: 0x000CBC31
		protected override void OnResize(EventArgs e)
		{
			if (this.View == View.Details && !this.Scrollable && base.IsHandleCreated)
			{
				this.PositionHeader();
			}
			base.OnResize(e);
		}

		// Token: 0x06002DAA RID: 11690 RVA: 0x000CDA5C File Offset: 0x000CBC5C
		protected virtual void OnRetrieveVirtualItem(RetrieveVirtualItemEventArgs e)
		{
			RetrieveVirtualItemEventHandler retrieveVirtualItemEventHandler = (RetrieveVirtualItemEventHandler)base.Events[ListView.EVENT_RETRIEVEVIRTUALITEM];
			if (retrieveVirtualItemEventHandler != null)
			{
				retrieveVirtualItemEventHandler(this, e);
			}
		}

		// Token: 0x06002DAB RID: 11691 RVA: 0x000CDA8C File Offset: 0x000CBC8C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void OnRightToLeftLayoutChanged(EventArgs e)
		{
			if (base.GetAnyDisposingInHierarchy())
			{
				return;
			}
			if (this.RightToLeft == RightToLeft.Yes)
			{
				this.RecreateHandleInternal();
			}
			EventHandler eventHandler = base.Events[ListView.EVENT_RIGHTTOLEFTLAYOUTCHANGED] as EventHandler;
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002DAC RID: 11692 RVA: 0x000CDAD4 File Offset: 0x000CBCD4
		protected virtual void OnSearchForVirtualItem(SearchForVirtualItemEventArgs e)
		{
			SearchForVirtualItemEventHandler searchForVirtualItemEventHandler = (SearchForVirtualItemEventHandler)base.Events[ListView.EVENT_SEARCHFORVIRTUALITEM];
			if (searchForVirtualItemEventHandler != null)
			{
				searchForVirtualItemEventHandler(this, e);
			}
		}

		// Token: 0x06002DAD RID: 11693 RVA: 0x000CDB04 File Offset: 0x000CBD04
		protected virtual void OnSelectedIndexChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[ListView.EVENT_SELECTEDINDEXCHANGED];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x06002DAE RID: 11694 RVA: 0x000CDB34 File Offset: 0x000CBD34
		protected override void OnSystemColorsChanged(EventArgs e)
		{
			base.OnSystemColorsChanged(e);
			if (base.IsHandleCreated)
			{
				int lparam = ColorTranslator.ToWin32(this.BackColor);
				base.SendMessage(4097, 0, lparam);
				base.SendMessage(4134, 0, -1);
			}
		}

		// Token: 0x06002DAF RID: 11695 RVA: 0x000CDB78 File Offset: 0x000CBD78
		protected virtual void OnVirtualItemsSelectionRangeChanged(ListViewVirtualItemsSelectionRangeChangedEventArgs e)
		{
			ListViewVirtualItemsSelectionRangeChangedEventHandler listViewVirtualItemsSelectionRangeChangedEventHandler = (ListViewVirtualItemsSelectionRangeChangedEventHandler)base.Events[ListView.EVENT_VIRTUALITEMSSELECTIONRANGECHANGED];
			if (listViewVirtualItemsSelectionRangeChangedEventHandler != null)
			{
				listViewVirtualItemsSelectionRangeChangedEventHandler(this, e);
			}
		}

		// Token: 0x06002DB0 RID: 11696 RVA: 0x000CDBA8 File Offset: 0x000CBDA8
		private void PositionHeader()
		{
			IntPtr window = UnsafeNativeMethods.GetWindow(new HandleRef(this, base.Handle), 5);
			if (window != IntPtr.Zero)
			{
				IntPtr intPtr = IntPtr.Zero;
				IntPtr intPtr2 = IntPtr.Zero;
				intPtr = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeMethods.RECT)));
				if (intPtr == IntPtr.Zero)
				{
					return;
				}
				try
				{
					intPtr2 = Marshal.AllocHGlobal(Marshal.SizeOf(typeof(NativeMethods.WINDOWPOS)));
					if (!(intPtr == IntPtr.Zero))
					{
						UnsafeNativeMethods.GetClientRect(new HandleRef(this, base.Handle), intPtr);
						NativeMethods.HDLAYOUT hdlayout = default(NativeMethods.HDLAYOUT);
						hdlayout.prc = intPtr;
						hdlayout.pwpos = intPtr2;
						UnsafeNativeMethods.SendMessage(new HandleRef(this, window), 4613, 0, ref hdlayout);
						NativeMethods.WINDOWPOS windowpos = (NativeMethods.WINDOWPOS)Marshal.PtrToStructure(intPtr2, typeof(NativeMethods.WINDOWPOS));
						SafeNativeMethods.SetWindowPos(new HandleRef(this, window), new HandleRef(this, windowpos.hwndInsertAfter), windowpos.x, windowpos.y, windowpos.cx, windowpos.cy, windowpos.flags | 64);
					}
				}
				finally
				{
					if (intPtr != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(intPtr);
					}
					if (intPtr2 != IntPtr.Zero)
					{
						Marshal.FreeHGlobal(intPtr2);
					}
				}
			}
		}

		// Token: 0x06002DB1 RID: 11697 RVA: 0x000CDD00 File Offset: 0x000CBF00
		private void RealizeAllSubItems()
		{
			NativeMethods.LVITEM lvitem = default(NativeMethods.LVITEM);
			for (int i = 0; i < this.itemCount; i++)
			{
				int count = this.Items[i].SubItems.Count;
				for (int j = 0; j < count; j++)
				{
					this.SetItemText(i, j, this.Items[i].SubItems[j].Text, ref lvitem);
				}
			}
		}

		// Token: 0x06002DB2 RID: 11698 RVA: 0x000CDD70 File Offset: 0x000CBF70
		protected void RealizeProperties()
		{
			Color color = this.BackColor;
			if (color != SystemColors.Window)
			{
				base.SendMessage(4097, 0, ColorTranslator.ToWin32(color));
			}
			color = this.ForeColor;
			if (color != SystemColors.WindowText)
			{
				base.SendMessage(4132, 0, ColorTranslator.ToWin32(color));
			}
			if (this.imageListLarge != null)
			{
				base.SendMessage(4099, 0, this.imageListLarge.Handle);
			}
			if (this.imageListSmall != null)
			{
				base.SendMessage(4099, 1, this.imageListSmall.Handle);
			}
			if (this.imageListState != null)
			{
				base.SendMessage(4099, 2, this.imageListState.Handle);
			}
		}

		// Token: 0x06002DB3 RID: 11699 RVA: 0x000CDE2C File Offset: 0x000CC02C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		public void RedrawItems(int startIndex, int endIndex, bool invalidateOnly)
		{
			if (this.VirtualMode)
			{
				if (startIndex < 0 || startIndex >= this.VirtualListSize)
				{
					throw new ArgumentOutOfRangeException("startIndex", SR.GetString("InvalidArgument", new object[]
					{
						"startIndex",
						startIndex.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (endIndex < 0 || endIndex >= this.VirtualListSize)
				{
					throw new ArgumentOutOfRangeException("endIndex", SR.GetString("InvalidArgument", new object[]
					{
						"endIndex",
						endIndex.ToString(CultureInfo.CurrentCulture)
					}));
				}
			}
			else
			{
				if (startIndex < 0 || startIndex >= this.Items.Count)
				{
					throw new ArgumentOutOfRangeException("startIndex", SR.GetString("InvalidArgument", new object[]
					{
						"startIndex",
						startIndex.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (endIndex < 0 || endIndex >= this.Items.Count)
				{
					throw new ArgumentOutOfRangeException("endIndex", SR.GetString("InvalidArgument", new object[]
					{
						"endIndex",
						endIndex.ToString(CultureInfo.CurrentCulture)
					}));
				}
			}
			if (startIndex > endIndex)
			{
				throw new ArgumentException(SR.GetString("ListViewStartIndexCannotBeLargerThanEndIndex"));
			}
			if (base.IsHandleCreated)
			{
				int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4117, startIndex, endIndex);
				if (this.View == View.LargeIcon || this.View == View.SmallIcon)
				{
					Rectangle rectangle = this.Items[startIndex].Bounds;
					for (int i = startIndex + 1; i <= endIndex; i++)
					{
						rectangle = Rectangle.Union(rectangle, this.Items[i].Bounds);
					}
					if (startIndex > 0)
					{
						rectangle = Rectangle.Union(rectangle, this.Items[startIndex - 1].Bounds);
					}
					else
					{
						rectangle.Width += rectangle.X;
						rectangle.Height += rectangle.Y;
						rectangle.X = (rectangle.Y = 0);
					}
					if (endIndex < this.Items.Count - 1)
					{
						rectangle = Rectangle.Union(rectangle, this.Items[endIndex + 1].Bounds);
					}
					else
					{
						rectangle.Height += base.ClientRectangle.Bottom - rectangle.Bottom;
						rectangle.Width += base.ClientRectangle.Right - rectangle.Right;
					}
					if (this.View == View.LargeIcon)
					{
						rectangle.Inflate(1, this.Font.Height + 1);
					}
					base.Invalidate(rectangle);
				}
				if (!invalidateOnly)
				{
					base.Update();
				}
			}
		}

		// Token: 0x06002DB4 RID: 11700 RVA: 0x000CE0D8 File Offset: 0x000CC2D8
		internal void RemoveGroupFromListView(ListViewGroup group)
		{
			this.EnsureDefaultGroup();
			foreach (object obj in group.Items)
			{
				ListViewItem listViewItem = (ListViewItem)obj;
				if (listViewItem.ListView == this)
				{
					listViewItem.UpdateStateToListView(listViewItem.Index);
				}
			}
			this.RemoveGroupNative(group);
			this.UpdateGroupView();
		}

		// Token: 0x06002DB5 RID: 11701 RVA: 0x000CE154 File Offset: 0x000CC354
		private void RemoveGroupNative(ListViewGroup group)
		{
			int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4246, group.ID, IntPtr.Zero);
		}

		// Token: 0x06002DB6 RID: 11702 RVA: 0x000CE188 File Offset: 0x000CC388
		private void Scroll(int fromLVItem, int toLVItem)
		{
			int lParam = this.GetItemPosition(toLVItem).Y - this.GetItemPosition(fromLVItem).Y;
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4116, 0, lParam);
		}

		// Token: 0x06002DB7 RID: 11703 RVA: 0x000CE1D0 File Offset: 0x000CC3D0
		private void SetBackgroundImage()
		{
			Application.OleRequired();
			NativeMethods.LVBKIMAGE lvbkimage = new NativeMethods.LVBKIMAGE();
			lvbkimage.xOffset = 0;
			lvbkimage.yOffset = 0;
			string text = this.backgroundImageFileName;
			if (this.BackgroundImage != null)
			{
				EnvironmentPermission perm = new EnvironmentPermission(EnvironmentPermissionAccess.Read, "TEMP");
				FileIOPermission perm2 = new FileIOPermission(PermissionState.Unrestricted);
				PermissionSet permissionSet = new PermissionSet(PermissionState.Unrestricted);
				permissionSet.AddPermission(perm);
				permissionSet.AddPermission(perm2);
				permissionSet.Assert();
				try
				{
					string tempPath = Path.GetTempPath();
					StringBuilder stringBuilder = new StringBuilder(1024);
					UnsafeNativeMethods.GetTempFileName(tempPath, this.GenerateRandomName(), 0, stringBuilder);
					this.backgroundImageFileName = stringBuilder.ToString();
					this.BackgroundImage.Save(this.backgroundImageFileName, ImageFormat.Bmp);
				}
				finally
				{
					PermissionSet.RevertAssert();
				}
				lvbkimage.pszImage = this.backgroundImageFileName;
				lvbkimage.cchImageMax = this.backgroundImageFileName.Length + 1;
				lvbkimage.ulFlags = 2;
				if (this.BackgroundImageTiled)
				{
					lvbkimage.ulFlags |= 16;
				}
				else
				{
					lvbkimage.ulFlags |= 0;
				}
			}
			else
			{
				lvbkimage.ulFlags = 0;
				this.backgroundImageFileName = string.Empty;
			}
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.LVM_SETBKIMAGE, 0, lvbkimage);
			if (string.IsNullOrEmpty(text))
			{
				return;
			}
			if (this.bkImgFileNames == null)
			{
				this.bkImgFileNames = new string[8];
				this.bkImgFileNamesCount = -1;
			}
			if (this.bkImgFileNamesCount == 7)
			{
				this.DeleteFileName(this.bkImgFileNames[0]);
				this.bkImgFileNames[0] = this.bkImgFileNames[1];
				this.bkImgFileNames[1] = this.bkImgFileNames[2];
				this.bkImgFileNames[2] = this.bkImgFileNames[3];
				this.bkImgFileNames[3] = this.bkImgFileNames[4];
				this.bkImgFileNames[4] = this.bkImgFileNames[5];
				this.bkImgFileNames[5] = this.bkImgFileNames[6];
				this.bkImgFileNames[6] = this.bkImgFileNames[7];
				this.bkImgFileNames[7] = null;
				this.bkImgFileNamesCount--;
			}
			this.bkImgFileNamesCount++;
			this.bkImgFileNames[this.bkImgFileNamesCount] = text;
			this.Refresh();
		}

		// Token: 0x06002DB8 RID: 11704 RVA: 0x000CE3FC File Offset: 0x000CC5FC
		internal void SetColumnInfo(int mask, ColumnHeader ch)
		{
			if (base.IsHandleCreated)
			{
				NativeMethods.LVCOLUMN lvcolumn = new NativeMethods.LVCOLUMN();
				lvcolumn.mask = mask;
				if ((mask & 16) != 0 || (mask & 1) != 0)
				{
					lvcolumn.mask |= 1;
					if (ch.ActualImageIndex_Internal > -1)
					{
						lvcolumn.iImage = ch.ActualImageIndex_Internal;
						lvcolumn.fmt |= 2048;
					}
					lvcolumn.fmt |= (int)ch.TextAlign;
				}
				if ((mask & 4) != 0)
				{
					lvcolumn.pszText = Marshal.StringToHGlobalAuto(ch.Text);
				}
				int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.LVM_SETCOLUMN, ch.Index, lvcolumn);
				if ((mask & 4) != 0)
				{
					Marshal.FreeHGlobal(lvcolumn.pszText);
				}
				if (num == 0)
				{
					throw new InvalidOperationException(SR.GetString("ListViewColumnInfoSet"));
				}
				this.InvalidateColumnHeaders();
			}
		}

		// Token: 0x06002DB9 RID: 11705 RVA: 0x000CE4D8 File Offset: 0x000CC6D8
		internal void SetColumnWidth(int columnIndex, ColumnHeaderAutoResizeStyle headerAutoResize)
		{
			if (columnIndex < 0 || (columnIndex >= 0 && this.columnHeaders == null) || columnIndex >= this.columnHeaders.Length)
			{
				throw new ArgumentOutOfRangeException("columnIndex", SR.GetString("InvalidArgument", new object[]
				{
					"columnIndex",
					columnIndex.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (!ClientUtils.IsEnumValid(headerAutoResize, (int)headerAutoResize, 0, 2))
			{
				throw new InvalidEnumArgumentException("headerAutoResize", (int)headerAutoResize, typeof(ColumnHeaderAutoResizeStyle));
			}
			int num = 0;
			int num2 = 0;
			if (headerAutoResize == ColumnHeaderAutoResizeStyle.None)
			{
				num = this.columnHeaders[columnIndex].WidthInternal;
				if (num == -2)
				{
					headerAutoResize = ColumnHeaderAutoResizeStyle.HeaderSize;
				}
				else if (num == -1)
				{
					headerAutoResize = ColumnHeaderAutoResizeStyle.ColumnContent;
				}
			}
			if (headerAutoResize == ColumnHeaderAutoResizeStyle.HeaderSize)
			{
				num2 = this.CompensateColumnHeaderResize(columnIndex, false);
				num = -2;
			}
			else if (headerAutoResize == ColumnHeaderAutoResizeStyle.ColumnContent)
			{
				num2 = this.CompensateColumnHeaderResize(columnIndex, false);
				num = -1;
			}
			if (base.IsHandleCreated)
			{
				base.SendMessage(4126, columnIndex, NativeMethods.Util.MAKELPARAM(num, 0));
			}
			if (base.IsHandleCreated && (headerAutoResize == ColumnHeaderAutoResizeStyle.ColumnContent || headerAutoResize == ColumnHeaderAutoResizeStyle.HeaderSize) && num2 != 0)
			{
				int low = this.columnHeaders[columnIndex].Width + num2;
				base.SendMessage(4126, columnIndex, NativeMethods.Util.MAKELPARAM(low, 0));
			}
		}

		// Token: 0x06002DBA RID: 11706 RVA: 0x000CE5F2 File Offset: 0x000CC7F2
		private void SetColumnWidth(int index, int width)
		{
			if (base.IsHandleCreated)
			{
				base.SendMessage(4126, index, NativeMethods.Util.MAKELPARAM(width, 0));
			}
		}

		// Token: 0x06002DBB RID: 11707 RVA: 0x000CE610 File Offset: 0x000CC810
		private void SetDisplayIndices(int[] indices)
		{
			int[] array = new int[indices.Length];
			for (int i = 0; i < indices.Length; i++)
			{
				this.Columns[i].DisplayIndexInternal = indices[i];
				array[indices[i]] = i;
			}
			if (base.IsHandleCreated && !base.Disposing)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4154, array.Length, array);
			}
		}

		// Token: 0x06002DBC RID: 11708 RVA: 0x000CE679 File Offset: 0x000CC879
		internal void UpdateSavedCheckedItems(ListViewItem item, bool addItem)
		{
			if (addItem && this.savedCheckedItems == null)
			{
				this.savedCheckedItems = new List<ListViewItem>();
			}
			if (addItem)
			{
				this.savedCheckedItems.Add(item);
				return;
			}
			if (this.savedCheckedItems != null)
			{
				this.savedCheckedItems.Remove(item);
			}
		}

		// Token: 0x06002DBD RID: 11709 RVA: 0x000CE6B8 File Offset: 0x000CC8B8
		internal void SetToolTip(ToolTip toolTip, string toolTipCaption)
		{
			this.toolTipCaption = toolTipCaption;
			IntPtr handle = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4170, new HandleRef(toolTip, toolTip.Handle), 0);
			UnsafeNativeMethods.DestroyWindow(new HandleRef(null, handle));
		}

		// Token: 0x06002DBE RID: 11710 RVA: 0x000CE700 File Offset: 0x000CC900
		internal void SetItemImage(int index, int image)
		{
			if (index < 0 || (this.VirtualMode && index >= this.VirtualListSize) || (!this.VirtualMode && index >= this.itemCount))
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (base.IsHandleCreated)
			{
				NativeMethods.LVITEM lvitem = default(NativeMethods.LVITEM);
				lvitem.mask = 2;
				lvitem.iItem = index;
				lvitem.iImage = image;
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.LVM_SETITEM, 0, ref lvitem);
			}
		}

		// Token: 0x06002DBF RID: 11711 RVA: 0x000CE7A8 File Offset: 0x000CC9A8
		internal void SetItemIndentCount(int index, int indentCount)
		{
			if (index < 0 || (this.VirtualMode && index >= this.VirtualListSize) || (!this.VirtualMode && index >= this.itemCount))
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (base.IsHandleCreated)
			{
				NativeMethods.LVITEM lvitem = default(NativeMethods.LVITEM);
				lvitem.mask = 16;
				lvitem.iItem = index;
				lvitem.iIndent = indentCount;
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.LVM_SETITEM, 0, ref lvitem);
			}
		}

		// Token: 0x06002DC0 RID: 11712 RVA: 0x000CE850 File Offset: 0x000CCA50
		internal void SetItemPosition(int index, int x, int y)
		{
			if (this.VirtualMode)
			{
				return;
			}
			if (index < 0 || index >= this.itemCount)
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			NativeMethods.POINT point = new NativeMethods.POINT();
			point.x = x;
			point.y = y;
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4145, index, point);
		}

		// Token: 0x06002DC1 RID: 11713 RVA: 0x000CE8D4 File Offset: 0x000CCAD4
		internal void SetItemState(int index, int state, int mask)
		{
			if (index < -1 || (this.VirtualMode && index >= this.VirtualListSize) || (!this.VirtualMode && index >= this.itemCount))
			{
				throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
				{
					"index",
					index.ToString(CultureInfo.CurrentCulture)
				}));
			}
			if (base.IsHandleCreated)
			{
				NativeMethods.LVITEM lvitem = default(NativeMethods.LVITEM);
				lvitem.mask = 8;
				lvitem.state = state;
				lvitem.stateMask = mask;
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4139, index, ref lvitem);
			}
		}

		// Token: 0x06002DC2 RID: 11714 RVA: 0x000CE97C File Offset: 0x000CCB7C
		internal void SetItemText(int itemIndex, int subItemIndex, string text)
		{
			NativeMethods.LVITEM lvitem = default(NativeMethods.LVITEM);
			this.SetItemText(itemIndex, subItemIndex, text, ref lvitem);
		}

		// Token: 0x06002DC3 RID: 11715 RVA: 0x000CE99C File Offset: 0x000CCB9C
		private void SetItemText(int itemIndex, int subItemIndex, string text, ref NativeMethods.LVITEM lvItem)
		{
			if (this.View == View.List && subItemIndex == 0)
			{
				int num = (int)((long)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4125, 0, 0));
				Graphics graphics = base.CreateGraphicsInternal();
				int num2 = 0;
				try
				{
					num2 = Size.Ceiling(graphics.MeasureString(text, this.Font)).Width;
				}
				finally
				{
					graphics.Dispose();
				}
				if (num2 > num)
				{
					this.SetColumnWidth(0, num2);
				}
			}
			lvItem.mask = 1;
			lvItem.iItem = itemIndex;
			lvItem.iSubItem = subItemIndex;
			lvItem.pszText = text;
			UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), NativeMethods.LVM_SETITEMTEXT, itemIndex, ref lvItem);
		}

		// Token: 0x06002DC4 RID: 11716 RVA: 0x000CEA58 File Offset: 0x000CCC58
		internal void SetSelectionMark(int itemIndex)
		{
			if (itemIndex < 0 || itemIndex >= this.Items.Count)
			{
				return;
			}
			base.SendMessage(4163, 0, itemIndex);
		}

		// Token: 0x06002DC5 RID: 11717 RVA: 0x000CEA7C File Offset: 0x000CCC7C
		private void SmallImageListRecreateHandle(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				IntPtr lparam = (this.SmallImageList == null) ? IntPtr.Zero : this.SmallImageList.Handle;
				base.SendMessage(4099, (IntPtr)1, lparam);
				this.ForceCheckBoxUpdate();
			}
		}

		// Token: 0x06002DC6 RID: 11718 RVA: 0x000CEAC8 File Offset: 0x000CCCC8
		public void Sort()
		{
			if (this.VirtualMode)
			{
				throw new InvalidOperationException(SR.GetString("ListViewSortNotAllowedInVirtualListView"));
			}
			this.ApplyUpdateCachedItems();
			if (base.IsHandleCreated && this.listItemSorter != null)
			{
				NativeMethods.ListViewCompareCallback pfnCompare = new NativeMethods.ListViewCompareCallback(this.CompareFunc);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4144, IntPtr.Zero, pfnCompare);
			}
		}

		// Token: 0x06002DC7 RID: 11719 RVA: 0x000CEB30 File Offset: 0x000CCD30
		private void StateImageListRecreateHandle(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				IntPtr lparam = IntPtr.Zero;
				if (this.StateImageList != null)
				{
					lparam = this.imageListState.Handle;
				}
				base.SendMessage(4099, (IntPtr)2, lparam);
			}
		}

		// Token: 0x06002DC8 RID: 11720 RVA: 0x000CEB74 File Offset: 0x000CCD74
		public override string ToString()
		{
			string text = base.ToString();
			if (this.listItemsArray != null)
			{
				text = text + ", Items.Count: " + this.listItemsArray.Count.ToString(CultureInfo.CurrentCulture);
				if (this.listItemsArray.Count > 0)
				{
					string text2 = this.listItemsArray[0].ToString();
					string str = (text2.Length > 40) ? text2.Substring(0, 40) : text2;
					text = text + ", Items[0]: " + str;
				}
			}
			else if (this.Items != null)
			{
				text = text + ", Items.Count: " + this.Items.Count.ToString(CultureInfo.CurrentCulture);
				if (this.Items.Count > 0 && !this.VirtualMode)
				{
					string text3 = (this.Items[0] == null) ? "null" : this.Items[0].ToString();
					string str2 = (text3.Length > 40) ? text3.Substring(0, 40) : text3;
					text = text + ", Items[0]: " + str2;
				}
			}
			return text;
		}

		// Token: 0x06002DC9 RID: 11721 RVA: 0x000CEC98 File Offset: 0x000CCE98
		internal void UpdateListViewItemsLocations()
		{
			if (!this.VirtualMode && base.IsHandleCreated && this.AutoArrange && (this.View == View.LargeIcon || this.View == View.SmallIcon))
			{
				try
				{
					this.BeginUpdate();
					base.SendMessage(4138, -1, 0);
				}
				finally
				{
					this.EndUpdate();
				}
			}
		}

		// Token: 0x06002DCA RID: 11722 RVA: 0x000CECFC File Offset: 0x000CCEFC
		private void UpdateColumnWidths(ColumnHeaderAutoResizeStyle headerAutoResize)
		{
			if (this.columnHeaders != null)
			{
				for (int i = 0; i < this.columnHeaders.Length; i++)
				{
					this.SetColumnWidth(i, headerAutoResize);
				}
			}
		}

		// Token: 0x06002DCB RID: 11723 RVA: 0x000CED2C File Offset: 0x000CCF2C
		protected void UpdateExtendedStyles()
		{
			if (base.IsHandleCreated)
			{
				int num = 0;
				int wparam = 68861;
				ItemActivation itemActivation = this.activation;
				if (itemActivation != ItemActivation.OneClick)
				{
					if (itemActivation == ItemActivation.TwoClick)
					{
						num |= 128;
					}
				}
				else
				{
					num |= 64;
				}
				if (this.AllowColumnReorder)
				{
					num |= 16;
				}
				if (this.CheckBoxes)
				{
					num |= 4;
				}
				if (this.DoubleBuffered)
				{
					num |= 65536;
				}
				if (this.FullRowSelect)
				{
					num |= 32;
				}
				if (this.GridLines)
				{
					num |= 1;
				}
				if (this.HoverSelection)
				{
					num |= 8;
				}
				if (this.HotTracking)
				{
					num |= 2048;
				}
				if (this.ShowItemToolTips)
				{
					num |= 1024;
				}
				base.SendMessage(4150, wparam, num);
				base.Invalidate();
			}
		}

		// Token: 0x06002DCC RID: 11724 RVA: 0x000CEDF0 File Offset: 0x000CCFF0
		internal void UpdateGroupNative(ListViewGroup group)
		{
			NativeMethods.LVGROUP lvgroup = new NativeMethods.LVGROUP();
			try
			{
				lvgroup = this.GetLVGROUP(group);
				int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4243, group.ID, lvgroup);
			}
			finally
			{
				this.DestroyLVGROUP(lvgroup);
			}
			base.Invalidate();
		}

		// Token: 0x06002DCD RID: 11725 RVA: 0x000CEE50 File Offset: 0x000CD050
		internal void UpdateGroupView()
		{
			if (base.IsHandleCreated && this.ComctlSupportsVisualStyles && !this.VirtualMode)
			{
				int num = (int)((long)base.SendMessage(4253, this.GroupsEnabled ? 1 : 0, 0));
			}
		}

		// Token: 0x06002DCE RID: 11726 RVA: 0x000CEE94 File Offset: 0x000CD094
		private void UpdateTileView()
		{
			NativeMethods.LVTILEVIEWINFO lvtileviewinfo = new NativeMethods.LVTILEVIEWINFO();
			lvtileviewinfo.dwMask = 2;
			lvtileviewinfo.cLines = ((this.columnHeaders != null) ? this.columnHeaders.Length : 0);
			lvtileviewinfo.dwMask |= 1;
			lvtileviewinfo.dwFlags = 3;
			lvtileviewinfo.sizeTile = new NativeMethods.SIZE(this.TileSize.Width, this.TileSize.Height);
			bool flag = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4258, 0, lvtileviewinfo);
		}

		// Token: 0x06002DCF RID: 11727 RVA: 0x000CEF1C File Offset: 0x000CD11C
		private void WmNmClick(ref Message m)
		{
			if (this.CheckBoxes)
			{
				Point p = Cursor.Position;
				p = base.PointToClientInternal(p);
				NativeMethods.LVHITTESTINFO lvhittestinfo = new NativeMethods.LVHITTESTINFO();
				lvhittestinfo.pt_x = p.X;
				lvhittestinfo.pt_y = p.Y;
				int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4114, 0, lvhittestinfo);
				if (num != -1 && (lvhittestinfo.flags & 8) != 0)
				{
					ListViewItem listViewItem = this.Items[num];
					if (listViewItem.Selected)
					{
						bool @checked = !listViewItem.Checked;
						if (!this.VirtualMode)
						{
							foreach (object obj in this.SelectedItems)
							{
								ListViewItem listViewItem2 = (ListViewItem)obj;
								if (listViewItem2 != listViewItem)
								{
									listViewItem2.Checked = @checked;
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x06002DD0 RID: 11728 RVA: 0x000CF014 File Offset: 0x000CD214
		private void WmNmDblClick(ref Message m)
		{
			if (this.CheckBoxes)
			{
				Point p = Cursor.Position;
				p = base.PointToClientInternal(p);
				NativeMethods.LVHITTESTINFO lvhittestinfo = new NativeMethods.LVHITTESTINFO();
				lvhittestinfo.pt_x = p.X;
				lvhittestinfo.pt_y = p.Y;
				int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4114, 0, lvhittestinfo);
				if (num != -1 && (lvhittestinfo.flags & 14) != 0)
				{
					ListViewItem listViewItem = this.Items[num];
					listViewItem.Checked = !listViewItem.Checked;
				}
			}
		}

		// Token: 0x06002DD1 RID: 11729 RVA: 0x000CF0A0 File Offset: 0x000CD2A0
		private void WmMouseDown(ref Message m, MouseButtons button, int clicks)
		{
			this.listViewState[524288] = false;
			this.listViewState[1048576] = true;
			this.FocusInternal();
			int x = NativeMethods.Util.SignedLOWORD(m.LParam);
			int y = NativeMethods.Util.SignedHIWORD(m.LParam);
			this.OnMouseDown(new MouseEventArgs(button, clicks, x, y, 0));
			if (!base.ValidationCancelled)
			{
				if (this.CheckBoxes)
				{
					ListViewHitTestInfo listViewHitTestInfo = this.HitTest(x, y);
					if (this.imageListState == null || this.imageListState.Images.Count >= 2)
					{
						if (AccessibilityImprovements.Level2 && listViewHitTestInfo.Item != null && listViewHitTestInfo.Location == ListViewHitTestLocations.StateImage)
						{
							listViewHitTestInfo.Item.Focused = true;
						}
						this.DefWndProc(ref m);
						return;
					}
					if (listViewHitTestInfo.Location != ListViewHitTestLocations.StateImage)
					{
						this.DefWndProc(ref m);
						return;
					}
				}
				else
				{
					this.DefWndProc(ref m);
				}
			}
		}

		// Token: 0x06002DD2 RID: 11730 RVA: 0x000CF180 File Offset: 0x000CD380
		private unsafe bool WmNotify(ref Message m)
		{
			NativeMethods.NMHDR* ptr = (NativeMethods.NMHDR*)((void*)m.LParam);
			if (ptr->code == -12 && this.OwnerDraw)
			{
				try
				{
					NativeMethods.NMCUSTOMDRAW* ptr2 = (NativeMethods.NMCUSTOMDRAW*)((void*)m.LParam);
					int dwDrawStage = ptr2->dwDrawStage;
					if (dwDrawStage == 1)
					{
						m.Result = (IntPtr)32;
						return true;
					}
					if (dwDrawStage != 65537)
					{
						return false;
					}
					Graphics graphics = Graphics.FromHdcInternal(ptr2->hdc);
					Rectangle bounds = Rectangle.FromLTRB(ptr2->rc.left, ptr2->rc.top, ptr2->rc.right, ptr2->rc.bottom);
					DrawListViewColumnHeaderEventArgs drawListViewColumnHeaderEventArgs = null;
					try
					{
						Color foreColor = ColorTranslator.FromWin32(SafeNativeMethods.GetTextColor(new HandleRef(this, ptr2->hdc)));
						Color backColor = ColorTranslator.FromWin32(SafeNativeMethods.GetBkColor(new HandleRef(this, ptr2->hdc)));
						Font listHeaderFont = this.GetListHeaderFont();
						drawListViewColumnHeaderEventArgs = new DrawListViewColumnHeaderEventArgs(graphics, bounds, (int)ptr2->dwItemSpec, this.columnHeaders[(int)ptr2->dwItemSpec], (ListViewItemStates)ptr2->uItemState, foreColor, backColor, listHeaderFont);
						this.OnDrawColumnHeader(drawListViewColumnHeaderEventArgs);
					}
					finally
					{
						graphics.Dispose();
					}
					if (drawListViewColumnHeaderEventArgs.DrawDefault)
					{
						m.Result = (IntPtr)0;
						return false;
					}
					m.Result = (IntPtr)4;
					return true;
				}
				catch (Exception ex)
				{
					m.Result = (IntPtr)0;
				}
			}
			if (ptr->code == -16 && this.listViewState[131072])
			{
				this.listViewState[131072] = false;
				this.OnColumnClick(new ColumnClickEventArgs(this.columnIndex));
			}
			if (ptr->code == -306 || ptr->code == -326)
			{
				this.listViewState[67108864] = true;
				this.listViewState1[2] = false;
				this.newWidthForColumnWidthChangingCancelled = -1;
				this.listViewState1[2] = false;
				NativeMethods.NMHEADER nmheader = (NativeMethods.NMHEADER)m.GetLParam(typeof(NativeMethods.NMHEADER));
				if (this.columnHeaders != null && this.columnHeaders.Length > nmheader.iItem)
				{
					this.columnHeaderClicked = this.columnHeaders[nmheader.iItem];
					this.columnHeaderClickedWidth = this.columnHeaderClicked.Width;
				}
				else
				{
					this.columnHeaderClickedWidth = -1;
					this.columnHeaderClicked = null;
				}
			}
			if (ptr->code == -300 || ptr->code == -320)
			{
				NativeMethods.NMHEADER nmheader2 = (NativeMethods.NMHEADER)m.GetLParam(typeof(NativeMethods.NMHEADER));
				if (this.columnHeaders != null && nmheader2.iItem < this.columnHeaders.Length && (this.listViewState[67108864] || this.listViewState[536870912]))
				{
					NativeMethods.HDITEM2 hditem = (NativeMethods.HDITEM2)UnsafeNativeMethods.PtrToStructure(nmheader2.pItem, typeof(NativeMethods.HDITEM2));
					int newWidth = ((hditem.mask & 1) != 0) ? hditem.cxy : -1;
					ColumnWidthChangingEventArgs columnWidthChangingEventArgs = new ColumnWidthChangingEventArgs(nmheader2.iItem, newWidth);
					this.OnColumnWidthChanging(columnWidthChangingEventArgs);
					m.Result = (IntPtr)(columnWidthChangingEventArgs.Cancel ? 1 : 0);
					if (columnWidthChangingEventArgs.Cancel)
					{
						hditem.cxy = columnWidthChangingEventArgs.NewWidth;
						if (this.listViewState[536870912])
						{
							this.listViewState[1073741824] = true;
						}
						this.listViewState1[2] = true;
						this.newWidthForColumnWidthChangingCancelled = columnWidthChangingEventArgs.NewWidth;
						return true;
					}
					return false;
				}
			}
			if ((ptr->code == -301 || ptr->code == -321) && !this.listViewState[67108864])
			{
				NativeMethods.NMHEADER nmheader3 = (NativeMethods.NMHEADER)m.GetLParam(typeof(NativeMethods.NMHEADER));
				if (this.columnHeaders != null && nmheader3.iItem < this.columnHeaders.Length)
				{
					int width = this.columnHeaders[nmheader3.iItem].Width;
					if (this.columnHeaderClicked == null || (this.columnHeaderClicked == this.columnHeaders[nmheader3.iItem] && this.columnHeaderClickedWidth != -1 && this.columnHeaderClickedWidth != width))
					{
						if (this.listViewState[536870912])
						{
							if (this.CompensateColumnHeaderResize(m, this.listViewState[1073741824]) == 0)
							{
								this.OnColumnWidthChanged(new ColumnWidthChangedEventArgs(nmheader3.iItem));
							}
						}
						else
						{
							this.OnColumnWidthChanged(new ColumnWidthChangedEventArgs(nmheader3.iItem));
						}
					}
				}
				this.columnHeaderClicked = null;
				this.columnHeaderClickedWidth = -1;
				ISite site = this.Site;
				if (site != null)
				{
					IComponentChangeService componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
					if (componentChangeService != null)
					{
						try
						{
							componentChangeService.OnComponentChanging(this, null);
						}
						catch (CheckoutException ex2)
						{
							if (ex2 == CheckoutException.Canceled)
							{
								return false;
							}
							throw ex2;
						}
					}
				}
			}
			if (ptr->code == -307 || ptr->code == -327)
			{
				this.listViewState[67108864] = false;
				if (this.listViewState1[2])
				{
					m.Result = (IntPtr)1;
					if (this.newWidthForColumnWidthChangingCancelled != -1)
					{
						NativeMethods.NMHEADER nmheader4 = (NativeMethods.NMHEADER)m.GetLParam(typeof(NativeMethods.NMHEADER));
						if (this.columnHeaders != null && this.columnHeaders.Length > nmheader4.iItem)
						{
							this.columnHeaders[nmheader4.iItem].Width = this.newWidthForColumnWidthChangingCancelled;
						}
					}
					this.listViewState1[2] = false;
					this.newWidthForColumnWidthChangingCancelled = -1;
					return true;
				}
				return false;
			}
			else
			{
				if (ptr->code == -311)
				{
					NativeMethods.NMHEADER nmheader5 = (NativeMethods.NMHEADER)m.GetLParam(typeof(NativeMethods.NMHEADER));
					if (nmheader5.pItem != IntPtr.Zero)
					{
						NativeMethods.HDITEM2 hditem2 = (NativeMethods.HDITEM2)UnsafeNativeMethods.PtrToStructure(nmheader5.pItem, typeof(NativeMethods.HDITEM2));
						if ((hditem2.mask & 128) == 128)
						{
							int displayIndex = this.Columns[nmheader5.iItem].DisplayIndex;
							int iOrder = hditem2.iOrder;
							if (displayIndex == iOrder)
							{
								return false;
							}
							if (iOrder < 0)
							{
								return false;
							}
							ColumnReorderedEventArgs columnReorderedEventArgs = new ColumnReorderedEventArgs(displayIndex, iOrder, this.Columns[nmheader5.iItem]);
							this.OnColumnReordered(columnReorderedEventArgs);
							if (columnReorderedEventArgs.Cancel)
							{
								m.Result = new IntPtr(1);
								return true;
							}
							int num = Math.Min(displayIndex, iOrder);
							int num2 = Math.Max(displayIndex, iOrder);
							bool flag = iOrder > displayIndex;
							ColumnHeader columnHeader = null;
							int[] array = new int[this.Columns.Count];
							for (int i = 0; i < this.Columns.Count; i++)
							{
								ColumnHeader columnHeader2 = this.Columns[i];
								if (columnHeader2.DisplayIndex == displayIndex)
								{
									columnHeader = columnHeader2;
								}
								else if (columnHeader2.DisplayIndex >= num && columnHeader2.DisplayIndex <= num2)
								{
									columnHeader2.DisplayIndexInternal -= (flag ? 1 : -1);
								}
								array[i] = columnHeader2.DisplayIndexInternal;
							}
							columnHeader.DisplayIndexInternal = iOrder;
							array[columnHeader.Index] = columnHeader.DisplayIndexInternal;
							this.SetDisplayIndices(array);
						}
					}
				}
				if (ptr->code == -305 || ptr->code == -325)
				{
					this.listViewState[536870912] = true;
					this.listViewState[1073741824] = false;
					bool flag2 = false;
					try
					{
						this.DefWndProc(ref m);
					}
					finally
					{
						this.listViewState[536870912] = false;
						flag2 = this.listViewState[1073741824];
						this.listViewState[1073741824] = false;
					}
					this.columnHeaderClicked = null;
					this.columnHeaderClickedWidth = -1;
					if (flag2)
					{
						if (this.newWidthForColumnWidthChangingCancelled != -1)
						{
							NativeMethods.NMHEADER nmheader6 = (NativeMethods.NMHEADER)m.GetLParam(typeof(NativeMethods.NMHEADER));
							if (this.columnHeaders != null && this.columnHeaders.Length > nmheader6.iItem)
							{
								this.columnHeaders[nmheader6.iItem].Width = this.newWidthForColumnWidthChangingCancelled;
							}
						}
						m.Result = (IntPtr)1;
					}
					else
					{
						int num3 = this.CompensateColumnHeaderResize(m, flag2);
						if (num3 != 0)
						{
							ColumnHeader columnHeader3 = this.columnHeaders[0];
							columnHeader3.Width += num3;
						}
					}
					return true;
				}
				return false;
			}
			bool result;
			return result;
		}

		// Token: 0x06002DD3 RID: 11731 RVA: 0x000CFA54 File Offset: 0x000CDC54
		private Font GetListHeaderFont()
		{
			IntPtr handle = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4127, 0, 0);
			IntPtr hfont = UnsafeNativeMethods.SendMessage(new HandleRef(this, handle), 49, 0, 0);
			IntSecurity.ObjectFromWin32Handle.Assert();
			return Font.FromHfont(hfont);
		}

		// Token: 0x06002DD4 RID: 11732 RVA: 0x000CFA9C File Offset: 0x000CDC9C
		private int GetIndexOfClickedItem(NativeMethods.LVHITTESTINFO lvhi)
		{
			Point p = Cursor.Position;
			p = base.PointToClientInternal(p);
			lvhi.pt_x = p.X;
			lvhi.pt_y = p.Y;
			return (int)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4114, 0, lvhi);
		}

		// Token: 0x06002DD5 RID: 11733 RVA: 0x000CFAEE File Offset: 0x000CDCEE
		internal void RecreateHandleInternal()
		{
			if (base.IsHandleCreated && this.StateImageList != null)
			{
				base.SendMessage(4099, 2, IntPtr.Zero);
			}
			base.RecreateHandle();
		}

		// Token: 0x06002DD6 RID: 11734 RVA: 0x000CFB18 File Offset: 0x000CDD18
		private unsafe void WmReflectNotify(ref Message m)
		{
			NativeMethods.NMHDR* ptr = (NativeMethods.NMHDR*)((void*)m.LParam);
			int code = ptr->code;
			if (code <= -155)
			{
				if (code == -176)
				{
					goto IL_158;
				}
				if (code != -175)
				{
					if (code != -155)
					{
						goto IL_655;
					}
					if (!this.CheckBoxes)
					{
						return;
					}
					NativeMethods.NMLVKEYDOWN nmlvkeydown = (NativeMethods.NMLVKEYDOWN)m.GetLParam(typeof(NativeMethods.NMLVKEYDOWN));
					if (nmlvkeydown.wVKey != 32)
					{
						return;
					}
					ListViewItem focusedItem = this.FocusedItem;
					if (focusedItem == null)
					{
						return;
					}
					bool @checked = !focusedItem.Checked;
					if (!this.VirtualMode)
					{
						using (IEnumerator enumerator = this.SelectedItems.GetEnumerator())
						{
							while (enumerator.MoveNext())
							{
								object obj = enumerator.Current;
								ListViewItem listViewItem = (ListViewItem)obj;
								if (listViewItem != focusedItem)
								{
									listViewItem.Checked = @checked;
								}
							}
							return;
						}
						goto IL_624;
					}
					return;
				}
			}
			else
			{
				switch (code)
				{
				case -114:
					this.OnItemActivate(EventArgs.Empty);
					return;
				case -113:
					goto IL_624;
				case -112:
				case -110:
				case -107:
				case -104:
				case -103:
				case -102:
					goto IL_655;
				case -111:
					if (!this.ItemCollectionChangedInMouseDown)
					{
						NativeMethods.NMLISTVIEW nmlv = (NativeMethods.NMLISTVIEW)m.GetLParam(typeof(NativeMethods.NMLISTVIEW));
						this.LvnBeginDrag(MouseButtons.Right, nmlv);
						return;
					}
					return;
				case -109:
					if (!this.ItemCollectionChangedInMouseDown)
					{
						NativeMethods.NMLISTVIEW nmlv2 = (NativeMethods.NMLISTVIEW)m.GetLParam(typeof(NativeMethods.NMLISTVIEW));
						this.LvnBeginDrag(MouseButtons.Left, nmlv2);
						return;
					}
					return;
				case -108:
				{
					NativeMethods.NMLISTVIEW nmlistview = (NativeMethods.NMLISTVIEW)m.GetLParam(typeof(NativeMethods.NMLISTVIEW));
					this.listViewState[131072] = true;
					this.columnIndex = nmlistview.iSubItem;
					return;
				}
				case -106:
					goto IL_158;
				case -105:
					break;
				case -101:
				{
					NativeMethods.NMLISTVIEW* ptr2 = (NativeMethods.NMLISTVIEW*)((void*)m.LParam);
					if ((ptr2->uChanged & 8) == 0)
					{
						return;
					}
					CheckState checkState = ((ptr2->uOldState & 61440) >> 12 == 1) ? CheckState.Unchecked : CheckState.Checked;
					CheckState checkState2 = ((ptr2->uNewState & 61440) >> 12 == 1) ? CheckState.Unchecked : CheckState.Checked;
					if (checkState2 != checkState)
					{
						ItemCheckedEventArgs e = new ItemCheckedEventArgs(this.Items[ptr2->iItem]);
						this.OnItemChecked(e);
						if (AccessibilityImprovements.Level1)
						{
							base.AccessibilityNotifyClients(AccessibleEvents.StateChange, ptr2->iItem);
							base.AccessibilityNotifyClients(AccessibleEvents.NameChange, ptr2->iItem);
						}
					}
					int num = ptr2->uOldState & 2;
					int num2 = ptr2->uNewState & 2;
					if (num2 == num)
					{
						return;
					}
					if (this.VirtualMode && ptr2->iItem == -1)
					{
						if (this.VirtualListSize > 0)
						{
							ListViewVirtualItemsSelectionRangeChangedEventArgs e2 = new ListViewVirtualItemsSelectionRangeChangedEventArgs(0, this.VirtualListSize - 1, num2 != 0);
							this.OnVirtualItemsSelectionRangeChanged(e2);
						}
					}
					else if (this.Items.Count > 0)
					{
						ListViewItemSelectionChangedEventArgs e3 = new ListViewItemSelectionChangedEventArgs(this.Items[ptr2->iItem], ptr2->iItem, num2 != 0);
						this.OnItemSelectionChanged(e3);
					}
					if (this.Items.Count == 0 || this.Items[this.Items.Count - 1] != null)
					{
						this.listViewState1[16] = false;
						this.OnSelectedIndexChanged(EventArgs.Empty);
						return;
					}
					this.listViewState1[16] = true;
					return;
				}
				case -100:
				{
					NativeMethods.NMLISTVIEW* ptr3 = (NativeMethods.NMLISTVIEW*)((void*)m.LParam);
					if ((ptr3->uChanged & 8) == 0)
					{
						return;
					}
					CheckState checkState3 = ((ptr3->uOldState & 61440) >> 12 == 1) ? CheckState.Unchecked : CheckState.Checked;
					CheckState checkState4 = ((ptr3->uNewState & 61440) >> 12 == 1) ? CheckState.Unchecked : CheckState.Checked;
					if (checkState3 != checkState4)
					{
						ItemCheckEventArgs itemCheckEventArgs = new ItemCheckEventArgs(ptr3->iItem, checkState4, checkState3);
						this.OnItemCheck(itemCheckEventArgs);
						m.Result = (IntPtr)((((itemCheckEventArgs.NewValue == CheckState.Unchecked) ? CheckState.Unchecked : CheckState.Checked) == checkState3) ? 1 : 0);
						return;
					}
					return;
				}
				default:
					if (code != -12)
					{
						switch (code)
						{
						case -6:
							goto IL_53F;
						case -5:
							break;
						case -4:
							goto IL_655;
						case -3:
							this.WmNmDblClick(ref m);
							goto IL_53F;
						case -2:
							this.WmNmClick(ref m);
							break;
						default:
							goto IL_655;
						}
						NativeMethods.LVHITTESTINFO lvhi = new NativeMethods.LVHITTESTINFO();
						int indexOfClickedItem = this.GetIndexOfClickedItem(lvhi);
						MouseButtons button = (ptr->code == -2) ? MouseButtons.Left : MouseButtons.Right;
						Point p = Cursor.Position;
						p = base.PointToClientInternal(p);
						if (!base.ValidationCancelled && indexOfClickedItem != -1)
						{
							this.OnClick(EventArgs.Empty);
							this.OnMouseClick(new MouseEventArgs(button, 1, p.X, p.Y, 0));
						}
						if (!this.listViewState[524288])
						{
							this.OnMouseUp(new MouseEventArgs(button, 1, p.X, p.Y, 0));
							this.listViewState[524288] = true;
							return;
						}
						return;
						IL_53F:
						NativeMethods.LVHITTESTINFO lvhi2 = new NativeMethods.LVHITTESTINFO();
						int indexOfClickedItem2 = this.GetIndexOfClickedItem(lvhi2);
						if (indexOfClickedItem2 != -1)
						{
							this.listViewState[262144] = true;
						}
						this.listViewState[524288] = false;
						base.CaptureInternal = true;
						return;
					}
					this.CustomDraw(ref m);
					return;
				}
			}
			NativeMethods.NMLVDISPINFO_NOTEXT nmlvdispinfo_NOTEXT = (NativeMethods.NMLVDISPINFO_NOTEXT)m.GetLParam(typeof(NativeMethods.NMLVDISPINFO_NOTEXT));
			LabelEditEventArgs labelEditEventArgs = new LabelEditEventArgs(nmlvdispinfo_NOTEXT.item.iItem);
			this.OnBeforeLabelEdit(labelEditEventArgs);
			m.Result = (IntPtr)(labelEditEventArgs.CancelEdit ? 1 : 0);
			this.listViewState[16384] = !labelEditEventArgs.CancelEdit;
			return;
			IL_158:
			this.listViewState[16384] = false;
			NativeMethods.NMLVDISPINFO nmlvdispinfo = (NativeMethods.NMLVDISPINFO)m.GetLParam(typeof(NativeMethods.NMLVDISPINFO));
			LabelEditEventArgs labelEditEventArgs2 = new LabelEditEventArgs(nmlvdispinfo.item.iItem, nmlvdispinfo.item.pszText);
			this.OnAfterLabelEdit(labelEditEventArgs2);
			m.Result = (IntPtr)(labelEditEventArgs2.CancelEdit ? 0 : 1);
			if (!labelEditEventArgs2.CancelEdit && nmlvdispinfo.item.pszText != null)
			{
				this.Items[nmlvdispinfo.item.iItem].Text = nmlvdispinfo.item.pszText;
				return;
			}
			return;
			IL_624:
			NativeMethods.NMLVCACHEHINT nmlvcachehint = (NativeMethods.NMLVCACHEHINT)m.GetLParam(typeof(NativeMethods.NMLVCACHEHINT));
			this.OnCacheVirtualItems(new CacheVirtualItemsEventArgs(nmlvcachehint.iFrom, nmlvcachehint.iTo));
			return;
			IL_655:
			if (ptr->code == NativeMethods.LVN_GETDISPINFO)
			{
				if (this.VirtualMode && m.LParam != IntPtr.Zero)
				{
					NativeMethods.NMLVDISPINFO_NOTEXT nmlvdispinfo_NOTEXT2 = (NativeMethods.NMLVDISPINFO_NOTEXT)m.GetLParam(typeof(NativeMethods.NMLVDISPINFO_NOTEXT));
					RetrieveVirtualItemEventArgs retrieveVirtualItemEventArgs = new RetrieveVirtualItemEventArgs(nmlvdispinfo_NOTEXT2.item.iItem);
					this.OnRetrieveVirtualItem(retrieveVirtualItemEventArgs);
					ListViewItem item = retrieveVirtualItemEventArgs.Item;
					if (item == null)
					{
						throw new InvalidOperationException(SR.GetString("ListViewVirtualItemRequired"));
					}
					item.SetItemIndex(this, nmlvdispinfo_NOTEXT2.item.iItem);
					if ((nmlvdispinfo_NOTEXT2.item.mask & 1) != 0)
					{
						string text;
						if (nmlvdispinfo_NOTEXT2.item.iSubItem == 0)
						{
							text = item.Text;
						}
						else
						{
							if (item.SubItems.Count <= nmlvdispinfo_NOTEXT2.item.iSubItem)
							{
								throw new InvalidOperationException(SR.GetString("ListViewVirtualModeCantAccessSubItem"));
							}
							text = item.SubItems[nmlvdispinfo_NOTEXT2.item.iSubItem].Text;
						}
						if (nmlvdispinfo_NOTEXT2.item.cchTextMax <= text.Length)
						{
							text = text.Substring(0, nmlvdispinfo_NOTEXT2.item.cchTextMax - 1);
						}
						if (Marshal.SystemDefaultCharSize == 1)
						{
							byte[] bytes = Encoding.Default.GetBytes(text + "\0");
							Marshal.Copy(bytes, 0, nmlvdispinfo_NOTEXT2.item.pszText, text.Length + 1);
						}
						else
						{
							char[] source = (text + "\0").ToCharArray();
							Marshal.Copy(source, 0, nmlvdispinfo_NOTEXT2.item.pszText, text.Length + 1);
						}
					}
					if ((nmlvdispinfo_NOTEXT2.item.mask & 2) != 0 && item.ImageIndex != -1)
					{
						nmlvdispinfo_NOTEXT2.item.iImage = item.ImageIndex;
					}
					if ((nmlvdispinfo_NOTEXT2.item.mask & 16) != 0)
					{
						nmlvdispinfo_NOTEXT2.item.iIndent = item.IndentCount;
					}
					if ((nmlvdispinfo_NOTEXT2.item.stateMask & 61440) != 0)
					{
						NativeMethods.NMLVDISPINFO_NOTEXT nmlvdispinfo_NOTEXT3 = nmlvdispinfo_NOTEXT2;
						nmlvdispinfo_NOTEXT3.item.state = (nmlvdispinfo_NOTEXT3.item.state | item.RawStateImageIndex);
					}
					Marshal.StructureToPtr(nmlvdispinfo_NOTEXT2, m.LParam, false);
					return;
				}
			}
			else if (ptr->code == -115)
			{
				if (this.VirtualMode && m.LParam != IntPtr.Zero)
				{
					NativeMethods.NMLVODSTATECHANGE nmlvodstatechange = (NativeMethods.NMLVODSTATECHANGE)m.GetLParam(typeof(NativeMethods.NMLVODSTATECHANGE));
					bool flag = (nmlvodstatechange.uNewState & 2) != (nmlvodstatechange.uOldState & 2);
					if (flag)
					{
						int num3 = nmlvodstatechange.iTo;
						if (!UnsafeNativeMethods.IsVista)
						{
							num3--;
						}
						ListViewVirtualItemsSelectionRangeChangedEventArgs e4 = new ListViewVirtualItemsSelectionRangeChangedEventArgs(nmlvodstatechange.iFrom, num3, (nmlvodstatechange.uNewState & 2) != 0);
						this.OnVirtualItemsSelectionRangeChanged(e4);
						return;
					}
				}
			}
			else if (ptr->code == NativeMethods.LVN_GETINFOTIP)
			{
				if (this.ShowItemToolTips && m.LParam != IntPtr.Zero)
				{
					NativeMethods.NMLVGETINFOTIP nmlvgetinfotip = (NativeMethods.NMLVGETINFOTIP)m.GetLParam(typeof(NativeMethods.NMLVGETINFOTIP));
					ListViewItem listViewItem2 = this.Items[nmlvgetinfotip.item];
					if (listViewItem2 != null && !string.IsNullOrEmpty(listViewItem2.ToolTipText))
					{
						UnsafeNativeMethods.SendMessage(new HandleRef(this, ptr->hwndFrom), 1048, 0, SystemInformation.MaxWindowTrackSize.Width);
						if (Marshal.SystemDefaultCharSize == 1)
						{
							byte[] bytes2 = Encoding.Default.GetBytes(listViewItem2.ToolTipText + "\0");
							Marshal.Copy(bytes2, 0, nmlvgetinfotip.lpszText, Math.Min(bytes2.Length, nmlvgetinfotip.cchTextMax));
						}
						else
						{
							char[] array = (listViewItem2.ToolTipText + "\0").ToCharArray();
							Marshal.Copy(array, 0, nmlvgetinfotip.lpszText, Math.Min(array.Length, nmlvgetinfotip.cchTextMax));
						}
						Marshal.StructureToPtr(nmlvgetinfotip, m.LParam, false);
						return;
					}
				}
			}
			else if (ptr->code == NativeMethods.LVN_ODFINDITEM && this.VirtualMode)
			{
				NativeMethods.NMLVFINDITEM nmlvfinditem = (NativeMethods.NMLVFINDITEM)m.GetLParam(typeof(NativeMethods.NMLVFINDITEM));
				if ((nmlvfinditem.lvfi.flags & 1) != 0)
				{
					m.Result = (IntPtr)(-1);
					return;
				}
				bool flag2 = (nmlvfinditem.lvfi.flags & 2) != 0 || (nmlvfinditem.lvfi.flags & 8) != 0;
				bool isPrefixSearch = (nmlvfinditem.lvfi.flags & 8) != 0;
				string text2 = string.Empty;
				if (flag2)
				{
					text2 = nmlvfinditem.lvfi.psz;
				}
				Point empty = Point.Empty;
				if ((nmlvfinditem.lvfi.flags & 64) != 0)
				{
					empty = new Point(nmlvfinditem.lvfi.ptX, nmlvfinditem.lvfi.ptY);
				}
				SearchDirectionHint direction = SearchDirectionHint.Down;
				if ((nmlvfinditem.lvfi.flags & 64) != 0)
				{
					direction = (SearchDirectionHint)nmlvfinditem.lvfi.vkDirection;
				}
				int iStart = nmlvfinditem.iStart;
				if (iStart >= this.VirtualListSize)
				{
				}
				SearchForVirtualItemEventArgs searchForVirtualItemEventArgs = new SearchForVirtualItemEventArgs(flag2, isPrefixSearch, false, text2, empty, direction, nmlvfinditem.iStart);
				this.OnSearchForVirtualItem(searchForVirtualItemEventArgs);
				if (searchForVirtualItemEventArgs.Index != -1)
				{
					m.Result = (IntPtr)searchForVirtualItemEventArgs.Index;
					return;
				}
				m.Result = (IntPtr)(-1);
			}
		}

		// Token: 0x06002DD7 RID: 11735 RVA: 0x000D06D8 File Offset: 0x000CE8D8
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
						graphics.DrawRectangle(new Pen(VisualStyleInformation.TextControlBorder), rect);
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

		// Token: 0x06002DD8 RID: 11736 RVA: 0x000D07A4 File Offset: 0x000CE9A4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 275)
			{
				if (msg <= 15)
				{
					if (msg != 7)
					{
						if (msg == 15)
						{
							base.WndProc(ref m);
							base.BeginInvoke(new MethodInvoker(this.CleanPreviousBackgroundImageFiles));
							return;
						}
					}
					else
					{
						base.WndProc(ref m);
						if (!base.RecreatingHandle && !this.ListViewHandleDestroyed && this.FocusedItem == null && this.Items.Count > 0)
						{
							this.Items[0].Focused = true;
							return;
						}
						return;
					}
				}
				else if (msg != 78)
				{
					if (msg == 275)
					{
						if ((int)((long)m.WParam) != 48 || !this.ComctlSupportsVisualStyles)
						{
							base.WndProc(ref m);
							return;
						}
						return;
					}
				}
				else if (this.WmNotify(ref m))
				{
					return;
				}
			}
			else if (msg <= 673)
			{
				switch (msg)
				{
				case 512:
					if (this.listViewState[1048576] && !this.listViewState[524288] && Control.MouseButtons == MouseButtons.None)
					{
						this.OnMouseUp(new MouseEventArgs(this.downButton, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
						this.listViewState[524288] = true;
					}
					base.CaptureInternal = false;
					base.WndProc(ref m);
					return;
				case 513:
					this.ItemCollectionChangedInMouseDown = false;
					this.WmMouseDown(ref m, MouseButtons.Left, 1);
					this.downButton = MouseButtons.Left;
					return;
				case 514:
				case 517:
				case 520:
				{
					NativeMethods.LVHITTESTINFO lvhi = new NativeMethods.LVHITTESTINFO();
					int indexOfClickedItem = this.GetIndexOfClickedItem(lvhi);
					if (!base.ValidationCancelled && this.listViewState[262144] && indexOfClickedItem != -1)
					{
						this.listViewState[262144] = false;
						this.OnDoubleClick(EventArgs.Empty);
						this.OnMouseDoubleClick(new MouseEventArgs(this.downButton, 2, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
					}
					if (!this.listViewState[524288])
					{
						this.OnMouseUp(new MouseEventArgs(this.downButton, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
						this.listViewState[1048576] = false;
					}
					this.ItemCollectionChangedInMouseDown = false;
					this.listViewState[524288] = true;
					base.CaptureInternal = false;
					return;
				}
				case 515:
					this.ItemCollectionChangedInMouseDown = false;
					base.CaptureInternal = true;
					this.WmMouseDown(ref m, MouseButtons.Left, 2);
					return;
				case 516:
					this.WmMouseDown(ref m, MouseButtons.Right, 1);
					this.downButton = MouseButtons.Right;
					return;
				case 518:
					this.WmMouseDown(ref m, MouseButtons.Right, 2);
					return;
				case 519:
					this.WmMouseDown(ref m, MouseButtons.Middle, 1);
					this.downButton = MouseButtons.Middle;
					return;
				case 521:
					this.WmMouseDown(ref m, MouseButtons.Middle, 2);
					return;
				default:
					if (msg == 673)
					{
						if (this.HoverSelection)
						{
							base.WndProc(ref m);
							return;
						}
						this.OnMouseHover(EventArgs.Empty);
						return;
					}
					break;
				}
			}
			else
			{
				if (msg == 675)
				{
					this.prevHoveredItem = null;
					base.WndProc(ref m);
					return;
				}
				if (msg == 791)
				{
					this.WmPrint(ref m);
					return;
				}
				if (msg == 8270)
				{
					this.WmReflectNotify(ref m);
					return;
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x06002DD9 RID: 11737 RVA: 0x000D0B09 File Offset: 0x000CED09
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			if (AccessibilityImprovements.Level3)
			{
				return new ListView.ListViewAccessibleObject(this);
			}
			return base.CreateAccessibilityInstance();
		}

		// Token: 0x0400129C RID: 4764
		private const int MASK_HITTESTFLAG = 247;

		// Token: 0x0400129D RID: 4765
		private static readonly object EVENT_CACHEVIRTUALITEMS = new object();

		// Token: 0x0400129E RID: 4766
		private static readonly object EVENT_COLUMNREORDERED = new object();

		// Token: 0x0400129F RID: 4767
		private static readonly object EVENT_COLUMNWIDTHCHANGED = new object();

		// Token: 0x040012A0 RID: 4768
		private static readonly object EVENT_COLUMNWIDTHCHANGING = new object();

		// Token: 0x040012A1 RID: 4769
		private static readonly object EVENT_DRAWCOLUMNHEADER = new object();

		// Token: 0x040012A2 RID: 4770
		private static readonly object EVENT_DRAWITEM = new object();

		// Token: 0x040012A3 RID: 4771
		private static readonly object EVENT_DRAWSUBITEM = new object();

		// Token: 0x040012A4 RID: 4772
		private static readonly object EVENT_ITEMSELECTIONCHANGED = new object();

		// Token: 0x040012A5 RID: 4773
		private static readonly object EVENT_RETRIEVEVIRTUALITEM = new object();

		// Token: 0x040012A6 RID: 4774
		private static readonly object EVENT_SEARCHFORVIRTUALITEM = new object();

		// Token: 0x040012A7 RID: 4775
		private static readonly object EVENT_SELECTEDINDEXCHANGED = new object();

		// Token: 0x040012A8 RID: 4776
		private static readonly object EVENT_VIRTUALITEMSSELECTIONRANGECHANGED = new object();

		// Token: 0x040012A9 RID: 4777
		private static readonly object EVENT_RIGHTTOLEFTLAYOUTCHANGED = new object();

		// Token: 0x040012AA RID: 4778
		private ItemActivation activation;

		// Token: 0x040012AB RID: 4779
		private ListViewAlignment alignStyle = ListViewAlignment.Top;

		// Token: 0x040012AC RID: 4780
		private BorderStyle borderStyle = BorderStyle.Fixed3D;

		// Token: 0x040012AD RID: 4781
		private ColumnHeaderStyle headerStyle = ColumnHeaderStyle.Clickable;

		// Token: 0x040012AE RID: 4782
		private SortOrder sorting;

		// Token: 0x040012AF RID: 4783
		private View viewStyle;

		// Token: 0x040012B0 RID: 4784
		private string toolTipCaption = string.Empty;

		// Token: 0x040012B1 RID: 4785
		private const int LISTVIEWSTATE_ownerDraw = 1;

		// Token: 0x040012B2 RID: 4786
		private const int LISTVIEWSTATE_allowColumnReorder = 2;

		// Token: 0x040012B3 RID: 4787
		private const int LISTVIEWSTATE_autoArrange = 4;

		// Token: 0x040012B4 RID: 4788
		private const int LISTVIEWSTATE_checkBoxes = 8;

		// Token: 0x040012B5 RID: 4789
		private const int LISTVIEWSTATE_fullRowSelect = 16;

		// Token: 0x040012B6 RID: 4790
		private const int LISTVIEWSTATE_gridLines = 32;

		// Token: 0x040012B7 RID: 4791
		private const int LISTVIEWSTATE_hideSelection = 64;

		// Token: 0x040012B8 RID: 4792
		private const int LISTVIEWSTATE_hotTracking = 128;

		// Token: 0x040012B9 RID: 4793
		private const int LISTVIEWSTATE_labelEdit = 256;

		// Token: 0x040012BA RID: 4794
		private const int LISTVIEWSTATE_labelWrap = 512;

		// Token: 0x040012BB RID: 4795
		private const int LISTVIEWSTATE_multiSelect = 1024;

		// Token: 0x040012BC RID: 4796
		private const int LISTVIEWSTATE_scrollable = 2048;

		// Token: 0x040012BD RID: 4797
		private const int LISTVIEWSTATE_hoverSelection = 4096;

		// Token: 0x040012BE RID: 4798
		private const int LISTVIEWSTATE_nonclickHdr = 8192;

		// Token: 0x040012BF RID: 4799
		private const int LISTVIEWSTATE_inLabelEdit = 16384;

		// Token: 0x040012C0 RID: 4800
		private const int LISTVIEWSTATE_showItemToolTips = 32768;

		// Token: 0x040012C1 RID: 4801
		private const int LISTVIEWSTATE_backgroundImageTiled = 65536;

		// Token: 0x040012C2 RID: 4802
		private const int LISTVIEWSTATE_columnClicked = 131072;

		// Token: 0x040012C3 RID: 4803
		private const int LISTVIEWSTATE_doubleclickFired = 262144;

		// Token: 0x040012C4 RID: 4804
		private const int LISTVIEWSTATE_mouseUpFired = 524288;

		// Token: 0x040012C5 RID: 4805
		private const int LISTVIEWSTATE_expectingMouseUp = 1048576;

		// Token: 0x040012C6 RID: 4806
		private const int LISTVIEWSTATE_comctlSupportsVisualStyles = 2097152;

		// Token: 0x040012C7 RID: 4807
		private const int LISTVIEWSTATE_comctlSupportsVisualStylesTested = 4194304;

		// Token: 0x040012C8 RID: 4808
		private const int LISTVIEWSTATE_showGroups = 8388608;

		// Token: 0x040012C9 RID: 4809
		private const int LISTVIEWSTATE_handleDestroyed = 16777216;

		// Token: 0x040012CA RID: 4810
		private const int LISTVIEWSTATE_virtualMode = 33554432;

		// Token: 0x040012CB RID: 4811
		private const int LISTVIEWSTATE_headerControlTracking = 67108864;

		// Token: 0x040012CC RID: 4812
		private const int LISTVIEWSTATE_itemCollectionChangedInMouseDown = 134217728;

		// Token: 0x040012CD RID: 4813
		private const int LISTVIEWSTATE_flipViewToLargeIconAndSmallIcon = 268435456;

		// Token: 0x040012CE RID: 4814
		private const int LISTVIEWSTATE_headerDividerDblClick = 536870912;

		// Token: 0x040012CF RID: 4815
		private const int LISTVIEWSTATE_columnResizeCancelled = 1073741824;

		// Token: 0x040012D0 RID: 4816
		private const int LISTVIEWSTATE1_insertingItemsNatively = 1;

		// Token: 0x040012D1 RID: 4817
		private const int LISTVIEWSTATE1_cancelledColumnWidthChanging = 2;

		// Token: 0x040012D2 RID: 4818
		private const int LISTVIEWSTATE1_disposingImageLists = 4;

		// Token: 0x040012D3 RID: 4819
		private const int LISTVIEWSTATE1_useCompatibleStateImageBehavior = 8;

		// Token: 0x040012D4 RID: 4820
		private const int LISTVIEWSTATE1_selectedIndexChangedSkipped = 16;

		// Token: 0x040012D5 RID: 4821
		private const int LVTOOLTIPTRACKING = 48;

		// Token: 0x040012D6 RID: 4822
		private const int MAXTILECOLUMNS = 20;

		// Token: 0x040012D7 RID: 4823
		private BitVector32 listViewState;

		// Token: 0x040012D8 RID: 4824
		private BitVector32 listViewState1;

		// Token: 0x040012D9 RID: 4825
		private Color odCacheForeColor = SystemColors.WindowText;

		// Token: 0x040012DA RID: 4826
		private Color odCacheBackColor = SystemColors.Window;

		// Token: 0x040012DB RID: 4827
		private Font odCacheFont;

		// Token: 0x040012DC RID: 4828
		private IntPtr odCacheFontHandle = IntPtr.Zero;

		// Token: 0x040012DD RID: 4829
		private Control.FontHandleWrapper odCacheFontHandleWrapper;

		// Token: 0x040012DE RID: 4830
		private ImageList imageListLarge;

		// Token: 0x040012DF RID: 4831
		private ImageList imageListSmall;

		// Token: 0x040012E0 RID: 4832
		private ImageList imageListState;

		// Token: 0x040012E1 RID: 4833
		private MouseButtons downButton;

		// Token: 0x040012E2 RID: 4834
		private int itemCount;

		// Token: 0x040012E3 RID: 4835
		private int columnIndex;

		// Token: 0x040012E4 RID: 4836
		private int topIndex;

		// Token: 0x040012E5 RID: 4837
		private bool hoveredAlready;

		// Token: 0x040012E6 RID: 4838
		private bool rightToLeftLayout;

		// Token: 0x040012E7 RID: 4839
		private int virtualListSize;

		// Token: 0x040012E8 RID: 4840
		private ListViewGroup defaultGroup;

		// Token: 0x040012E9 RID: 4841
		private Hashtable listItemsTable = new Hashtable();

		// Token: 0x040012EA RID: 4842
		private ArrayList listItemsArray = new ArrayList();

		// Token: 0x040012EB RID: 4843
		private Size tileSize = Size.Empty;

		// Token: 0x040012EC RID: 4844
		private static readonly int PropDelayedUpdateItems = PropertyStore.CreateKey();

		// Token: 0x040012ED RID: 4845
		private int updateCounter;

		// Token: 0x040012EE RID: 4846
		private ColumnHeader[] columnHeaders;

		// Token: 0x040012EF RID: 4847
		private ListView.ListViewItemCollection listItemCollection;

		// Token: 0x040012F0 RID: 4848
		private ListView.ColumnHeaderCollection columnHeaderCollection;

		// Token: 0x040012F1 RID: 4849
		private ListView.CheckedIndexCollection checkedIndexCollection;

		// Token: 0x040012F2 RID: 4850
		private ListView.CheckedListViewItemCollection checkedListViewItemCollection;

		// Token: 0x040012F3 RID: 4851
		private ListView.SelectedListViewItemCollection selectedListViewItemCollection;

		// Token: 0x040012F4 RID: 4852
		private ListView.SelectedIndexCollection selectedIndexCollection;

		// Token: 0x040012F5 RID: 4853
		private ListViewGroupCollection groups;

		// Token: 0x040012F6 RID: 4854
		private ListViewInsertionMark insertionMark;

		// Token: 0x040012F7 RID: 4855
		private LabelEditEventHandler onAfterLabelEdit;

		// Token: 0x040012F8 RID: 4856
		private LabelEditEventHandler onBeforeLabelEdit;

		// Token: 0x040012F9 RID: 4857
		private ColumnClickEventHandler onColumnClick;

		// Token: 0x040012FA RID: 4858
		private EventHandler onItemActivate;

		// Token: 0x040012FB RID: 4859
		private ItemCheckedEventHandler onItemChecked;

		// Token: 0x040012FC RID: 4860
		private ItemDragEventHandler onItemDrag;

		// Token: 0x040012FD RID: 4861
		private ItemCheckEventHandler onItemCheck;

		// Token: 0x040012FE RID: 4862
		private ListViewItemMouseHoverEventHandler onItemMouseHover;

		// Token: 0x040012FF RID: 4863
		private int nextID;

		// Token: 0x04001300 RID: 4864
		private List<ListViewItem> savedSelectedItems;

		// Token: 0x04001301 RID: 4865
		private List<ListViewItem> savedCheckedItems;

		// Token: 0x04001302 RID: 4866
		private IComparer listItemSorter;

		// Token: 0x04001303 RID: 4867
		private ListViewItem prevHoveredItem;

		// Token: 0x04001304 RID: 4868
		private string backgroundImageFileName = string.Empty;

		// Token: 0x04001305 RID: 4869
		private int bkImgFileNamesCount = -1;

		// Token: 0x04001306 RID: 4870
		private string[] bkImgFileNames;

		// Token: 0x04001307 RID: 4871
		private const int BKIMGARRAYSIZE = 8;

		// Token: 0x04001308 RID: 4872
		private ColumnHeader columnHeaderClicked;

		// Token: 0x04001309 RID: 4873
		private int columnHeaderClickedWidth;

		// Token: 0x0400130A RID: 4874
		private int newWidthForColumnWidthChangingCancelled = -1;

		// Token: 0x020006C6 RID: 1734
		internal class IconComparer : IComparer
		{
			// Token: 0x060069CA RID: 27082 RVA: 0x0018886B File Offset: 0x00186A6B
			public IconComparer(SortOrder currentSortOrder)
			{
				this.sortOrder = currentSortOrder;
			}

			// Token: 0x170016E0 RID: 5856
			// (set) Token: 0x060069CB RID: 27083 RVA: 0x0018887A File Offset: 0x00186A7A
			public SortOrder SortOrder
			{
				set
				{
					this.sortOrder = value;
				}
			}

			// Token: 0x060069CC RID: 27084 RVA: 0x00188884 File Offset: 0x00186A84
			public int Compare(object obj1, object obj2)
			{
				ListViewItem listViewItem = (ListViewItem)obj1;
				ListViewItem listViewItem2 = (ListViewItem)obj2;
				if (this.sortOrder == SortOrder.Ascending)
				{
					return string.Compare(listViewItem.Text, listViewItem2.Text, false, CultureInfo.CurrentCulture);
				}
				return string.Compare(listViewItem2.Text, listViewItem.Text, false, CultureInfo.CurrentCulture);
			}

			// Token: 0x04003B3B RID: 15163
			private SortOrder sortOrder;
		}

		// Token: 0x020006C7 RID: 1735
		[ListBindable(false)]
		public class CheckedIndexCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x060069CD RID: 27085 RVA: 0x001888D7 File Offset: 0x00186AD7
			public CheckedIndexCollection(ListView owner)
			{
				this.owner = owner;
			}

			// Token: 0x170016E1 RID: 5857
			// (get) Token: 0x060069CE RID: 27086 RVA: 0x001888E8 File Offset: 0x00186AE8
			[Browsable(false)]
			public int Count
			{
				get
				{
					if (!this.owner.CheckBoxes)
					{
						return 0;
					}
					int num = 0;
					foreach (object obj in this.owner.Items)
					{
						ListViewItem listViewItem = (ListViewItem)obj;
						if (listViewItem != null && listViewItem.Checked)
						{
							num++;
						}
					}
					return num;
				}
			}

			// Token: 0x170016E2 RID: 5858
			// (get) Token: 0x060069CF RID: 27087 RVA: 0x00188960 File Offset: 0x00186B60
			private int[] IndicesArray
			{
				get
				{
					int[] array = new int[this.Count];
					int num = 0;
					int num2 = 0;
					while (num2 < this.owner.Items.Count && num < array.Length)
					{
						if (this.owner.Items[num2].Checked)
						{
							array[num++] = num2;
						}
						num2++;
					}
					return array;
				}
			}

			// Token: 0x170016E3 RID: 5859
			public int this[int index]
			{
				get
				{
					if (index < 0)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					int count = this.owner.Items.Count;
					int num = 0;
					for (int i = 0; i < count; i++)
					{
						ListViewItem listViewItem = this.owner.Items[i];
						if (listViewItem.Checked)
						{
							if (num == index)
							{
								return i;
							}
							num++;
						}
					}
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
			}

			// Token: 0x170016E4 RID: 5860
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x170016E5 RID: 5861
			// (get) Token: 0x060069D3 RID: 27091 RVA: 0x00006C59 File Offset: 0x00004E59
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x170016E6 RID: 5862
			// (get) Token: 0x060069D4 RID: 27092 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170016E7 RID: 5863
			// (get) Token: 0x060069D5 RID: 27093 RVA: 0x00013062 File Offset: 0x00011262
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170016E8 RID: 5864
			// (get) Token: 0x060069D6 RID: 27094 RVA: 0x00013062 File Offset: 0x00011262
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060069D7 RID: 27095 RVA: 0x00188A85 File Offset: 0x00186C85
			public bool Contains(int checkedIndex)
			{
				return this.owner.Items[checkedIndex].Checked;
			}

			// Token: 0x060069D8 RID: 27096 RVA: 0x00188AA2 File Offset: 0x00186CA2
			bool IList.Contains(object checkedIndex)
			{
				return checkedIndex is int && this.Contains((int)checkedIndex);
			}

			// Token: 0x060069D9 RID: 27097 RVA: 0x00188ABC File Offset: 0x00186CBC
			public int IndexOf(int checkedIndex)
			{
				int[] indicesArray = this.IndicesArray;
				for (int i = 0; i < indicesArray.Length; i++)
				{
					if (indicesArray[i] == checkedIndex)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x060069DA RID: 27098 RVA: 0x00188AE7 File Offset: 0x00186CE7
			int IList.IndexOf(object checkedIndex)
			{
				if (checkedIndex is int)
				{
					return this.IndexOf((int)checkedIndex);
				}
				return -1;
			}

			// Token: 0x060069DB RID: 27099 RVA: 0x0000A547 File Offset: 0x00008747
			int IList.Add(object value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060069DC RID: 27100 RVA: 0x0000A547 File Offset: 0x00008747
			void IList.Clear()
			{
				throw new NotSupportedException();
			}

			// Token: 0x060069DD RID: 27101 RVA: 0x0000A547 File Offset: 0x00008747
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060069DE RID: 27102 RVA: 0x0000A547 File Offset: 0x00008747
			void IList.Remove(object value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060069DF RID: 27103 RVA: 0x0000A547 File Offset: 0x00008747
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060069E0 RID: 27104 RVA: 0x00188AFF File Offset: 0x00186CFF
			void ICollection.CopyTo(Array dest, int index)
			{
				if (this.Count > 0)
				{
					Array.Copy(this.IndicesArray, 0, dest, index, this.Count);
				}
			}

			// Token: 0x060069E1 RID: 27105 RVA: 0x00188B20 File Offset: 0x00186D20
			public IEnumerator GetEnumerator()
			{
				int[] indicesArray = this.IndicesArray;
				if (indicesArray != null)
				{
					return indicesArray.GetEnumerator();
				}
				return new int[0].GetEnumerator();
			}

			// Token: 0x04003B3C RID: 15164
			private ListView owner;
		}

		// Token: 0x020006C8 RID: 1736
		[ListBindable(false)]
		public class CheckedListViewItemCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x060069E2 RID: 27106 RVA: 0x00188B49 File Offset: 0x00186D49
			public CheckedListViewItemCollection(ListView owner)
			{
				this.owner = owner;
			}

			// Token: 0x170016E9 RID: 5865
			// (get) Token: 0x060069E3 RID: 27107 RVA: 0x00188B5F File Offset: 0x00186D5F
			[Browsable(false)]
			public int Count
			{
				get
				{
					if (this.owner.VirtualMode)
					{
						throw new InvalidOperationException(SR.GetString("ListViewCantAccessCheckedItemsCollectionWhenInVirtualMode"));
					}
					return this.owner.CheckedIndices.Count;
				}
			}

			// Token: 0x170016EA RID: 5866
			// (get) Token: 0x060069E4 RID: 27108 RVA: 0x00188B90 File Offset: 0x00186D90
			private ListViewItem[] ItemArray
			{
				get
				{
					ListViewItem[] array = new ListViewItem[this.Count];
					int num = 0;
					int num2 = 0;
					while (num2 < this.owner.Items.Count && num < array.Length)
					{
						if (this.owner.Items[num2].Checked)
						{
							array[num++] = this.owner.Items[num2];
						}
						num2++;
					}
					return array;
				}
			}

			// Token: 0x170016EB RID: 5867
			public ListViewItem this[int index]
			{
				get
				{
					if (this.owner.VirtualMode)
					{
						throw new InvalidOperationException(SR.GetString("ListViewCantAccessCheckedItemsCollectionWhenInVirtualMode"));
					}
					int index2 = this.owner.CheckedIndices[index];
					return this.owner.Items[index2];
				}
			}

			// Token: 0x170016EC RID: 5868
			object IList.this[int index]
			{
				get
				{
					if (this.owner.VirtualMode)
					{
						throw new InvalidOperationException(SR.GetString("ListViewCantAccessCheckedItemsCollectionWhenInVirtualMode"));
					}
					return this[index];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x170016ED RID: 5869
			public virtual ListViewItem this[string key]
			{
				get
				{
					if (this.owner.VirtualMode)
					{
						throw new InvalidOperationException(SR.GetString("ListViewCantAccessCheckedItemsCollectionWhenInVirtualMode"));
					}
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

			// Token: 0x170016EE RID: 5870
			// (get) Token: 0x060069E9 RID: 27113 RVA: 0x00006C59 File Offset: 0x00004E59
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x170016EF RID: 5871
			// (get) Token: 0x060069EA RID: 27114 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170016F0 RID: 5872
			// (get) Token: 0x060069EB RID: 27115 RVA: 0x00013062 File Offset: 0x00011262
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x170016F1 RID: 5873
			// (get) Token: 0x060069EC RID: 27116 RVA: 0x00013062 File Offset: 0x00011262
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x060069ED RID: 27117 RVA: 0x00188CC2 File Offset: 0x00186EC2
			public bool Contains(ListViewItem item)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessCheckedItemsCollectionWhenInVirtualMode"));
				}
				return item != null && item.ListView == this.owner && item.Checked;
			}

			// Token: 0x060069EE RID: 27118 RVA: 0x00188CFD File Offset: 0x00186EFD
			bool IList.Contains(object item)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessCheckedItemsCollectionWhenInVirtualMode"));
				}
				return item is ListViewItem && this.Contains((ListViewItem)item);
			}

			// Token: 0x060069EF RID: 27119 RVA: 0x00188D32 File Offset: 0x00186F32
			public virtual bool ContainsKey(string key)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessCheckedItemsCollectionWhenInVirtualMode"));
				}
				return this.IsValidIndex(this.IndexOfKey(key));
			}

			// Token: 0x060069F0 RID: 27120 RVA: 0x00188D60 File Offset: 0x00186F60
			public int IndexOf(ListViewItem item)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessCheckedItemsCollectionWhenInVirtualMode"));
				}
				ListViewItem[] itemArray = this.ItemArray;
				for (int i = 0; i < itemArray.Length; i++)
				{
					if (itemArray[i] == item)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x060069F1 RID: 27121 RVA: 0x00188DA8 File Offset: 0x00186FA8
			public virtual int IndexOfKey(string key)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessCheckedItemsCollectionWhenInVirtualMode"));
				}
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

			// Token: 0x060069F2 RID: 27122 RVA: 0x00188E42 File Offset: 0x00187042
			private bool IsValidIndex(int index)
			{
				return index >= 0 && index < this.Count;
			}

			// Token: 0x060069F3 RID: 27123 RVA: 0x00188E53 File Offset: 0x00187053
			int IList.IndexOf(object item)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessCheckedItemsCollectionWhenInVirtualMode"));
				}
				if (item is ListViewItem)
				{
					return this.IndexOf((ListViewItem)item);
				}
				return -1;
			}

			// Token: 0x060069F4 RID: 27124 RVA: 0x0000A547 File Offset: 0x00008747
			int IList.Add(object value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060069F5 RID: 27125 RVA: 0x0000A547 File Offset: 0x00008747
			void IList.Clear()
			{
				throw new NotSupportedException();
			}

			// Token: 0x060069F6 RID: 27126 RVA: 0x0000A547 File Offset: 0x00008747
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060069F7 RID: 27127 RVA: 0x0000A547 File Offset: 0x00008747
			void IList.Remove(object value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060069F8 RID: 27128 RVA: 0x0000A547 File Offset: 0x00008747
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			// Token: 0x060069F9 RID: 27129 RVA: 0x00188E88 File Offset: 0x00187088
			public void CopyTo(Array dest, int index)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessCheckedItemsCollectionWhenInVirtualMode"));
				}
				if (this.Count > 0)
				{
					Array.Copy(this.ItemArray, 0, dest, index, this.Count);
				}
			}

			// Token: 0x060069FA RID: 27130 RVA: 0x00188EC4 File Offset: 0x001870C4
			public IEnumerator GetEnumerator()
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessCheckedItemsCollectionWhenInVirtualMode"));
				}
				ListViewItem[] itemArray = this.ItemArray;
				if (itemArray != null)
				{
					return itemArray.GetEnumerator();
				}
				return new ListViewItem[0].GetEnumerator();
			}

			// Token: 0x04003B3D RID: 15165
			private ListView owner;

			// Token: 0x04003B3E RID: 15166
			private int lastAccessedIndex = -1;
		}

		// Token: 0x020006C9 RID: 1737
		[ListBindable(false)]
		public class SelectedIndexCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x060069FB RID: 27131 RVA: 0x00188F0A File Offset: 0x0018710A
			public SelectedIndexCollection(ListView owner)
			{
				this.owner = owner;
			}

			// Token: 0x170016F2 RID: 5874
			// (get) Token: 0x060069FC RID: 27132 RVA: 0x00188F1C File Offset: 0x0018711C
			[Browsable(false)]
			public int Count
			{
				get
				{
					if (this.owner.IsHandleCreated)
					{
						return (int)((long)this.owner.SendMessage(4146, 0, 0));
					}
					if (this.owner.savedSelectedItems != null)
					{
						return this.owner.savedSelectedItems.Count;
					}
					return 0;
				}
			}

			// Token: 0x170016F3 RID: 5875
			// (get) Token: 0x060069FD RID: 27133 RVA: 0x00188F70 File Offset: 0x00187170
			private int[] IndicesArray
			{
				get
				{
					int count = this.Count;
					int[] array = new int[count];
					if (this.owner.IsHandleCreated)
					{
						int wparam = -1;
						for (int i = 0; i < count; i++)
						{
							int num = (int)((long)this.owner.SendMessage(4108, wparam, 2));
							if (num <= -1)
							{
								throw new InvalidOperationException(SR.GetString("SelectedNotEqualActual"));
							}
							array[i] = num;
							wparam = num;
						}
					}
					else
					{
						for (int j = 0; j < count; j++)
						{
							array[j] = this.owner.savedSelectedItems[j].Index;
						}
					}
					return array;
				}
			}

			// Token: 0x170016F4 RID: 5876
			public int this[int index]
			{
				get
				{
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (this.owner.IsHandleCreated)
					{
						int num = -1;
						for (int i = 0; i <= index; i++)
						{
							num = (int)((long)this.owner.SendMessage(4108, num, 2));
						}
						return num;
					}
					return this.owner.savedSelectedItems[index].Index;
				}
			}

			// Token: 0x170016F5 RID: 5877
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x170016F6 RID: 5878
			// (get) Token: 0x06006A01 RID: 27137 RVA: 0x00006C59 File Offset: 0x00004E59
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x170016F7 RID: 5879
			// (get) Token: 0x06006A02 RID: 27138 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170016F8 RID: 5880
			// (get) Token: 0x06006A03 RID: 27139 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x170016F9 RID: 5881
			// (get) Token: 0x06006A04 RID: 27140 RVA: 0x00011A20 File Offset: 0x0000FC20
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06006A05 RID: 27141 RVA: 0x001890B6 File Offset: 0x001872B6
			public bool Contains(int selectedIndex)
			{
				return this.owner.Items[selectedIndex].Selected;
			}

			// Token: 0x06006A06 RID: 27142 RVA: 0x001890CE File Offset: 0x001872CE
			bool IList.Contains(object selectedIndex)
			{
				return selectedIndex is int && this.Contains((int)selectedIndex);
			}

			// Token: 0x06006A07 RID: 27143 RVA: 0x001890E8 File Offset: 0x001872E8
			public int IndexOf(int selectedIndex)
			{
				int[] indicesArray = this.IndicesArray;
				for (int i = 0; i < indicesArray.Length; i++)
				{
					if (indicesArray[i] == selectedIndex)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06006A08 RID: 27144 RVA: 0x00189113 File Offset: 0x00187313
			int IList.IndexOf(object selectedIndex)
			{
				if (selectedIndex is int)
				{
					return this.IndexOf((int)selectedIndex);
				}
				return -1;
			}

			// Token: 0x06006A09 RID: 27145 RVA: 0x0018912B File Offset: 0x0018732B
			int IList.Add(object value)
			{
				if (value is int)
				{
					return this.Add((int)value);
				}
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					"value",
					value.ToString()
				}));
			}

			// Token: 0x06006A0A RID: 27146 RVA: 0x00189168 File Offset: 0x00187368
			void IList.Clear()
			{
				this.Clear();
			}

			// Token: 0x06006A0B RID: 27147 RVA: 0x0000A547 File Offset: 0x00008747
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06006A0C RID: 27148 RVA: 0x00189170 File Offset: 0x00187370
			void IList.Remove(object value)
			{
				if (value is int)
				{
					this.Remove((int)value);
					return;
				}
				throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
				{
					"value",
					value.ToString()
				}));
			}

			// Token: 0x06006A0D RID: 27149 RVA: 0x0000A547 File Offset: 0x00008747
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06006A0E RID: 27150 RVA: 0x001891B0 File Offset: 0x001873B0
			public int Add(int itemIndex)
			{
				if (this.owner.VirtualMode)
				{
					if (itemIndex < 0 || itemIndex >= this.owner.VirtualListSize)
					{
						throw new ArgumentOutOfRangeException("itemIndex", SR.GetString("InvalidArgument", new object[]
						{
							"itemIndex",
							itemIndex.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (this.owner.IsHandleCreated)
					{
						this.owner.SetItemState(itemIndex, 2, 2);
						return this.Count;
					}
					return -1;
				}
				else
				{
					if (itemIndex < 0 || itemIndex >= this.owner.Items.Count)
					{
						throw new ArgumentOutOfRangeException("itemIndex", SR.GetString("InvalidArgument", new object[]
						{
							"itemIndex",
							itemIndex.ToString(CultureInfo.CurrentCulture)
						}));
					}
					this.owner.Items[itemIndex].Selected = true;
					return this.Count;
				}
			}

			// Token: 0x06006A0F RID: 27151 RVA: 0x00189298 File Offset: 0x00187498
			public void Clear()
			{
				if (!this.owner.VirtualMode)
				{
					this.owner.savedSelectedItems = null;
				}
				if (this.owner.IsHandleCreated)
				{
					this.owner.SetItemState(-1, 0, 2);
				}
			}

			// Token: 0x06006A10 RID: 27152 RVA: 0x001892CE File Offset: 0x001874CE
			public void CopyTo(Array dest, int index)
			{
				if (this.Count > 0)
				{
					Array.Copy(this.IndicesArray, 0, dest, index, this.Count);
				}
			}

			// Token: 0x06006A11 RID: 27153 RVA: 0x001892F0 File Offset: 0x001874F0
			public IEnumerator GetEnumerator()
			{
				int[] indicesArray = this.IndicesArray;
				if (indicesArray != null)
				{
					return indicesArray.GetEnumerator();
				}
				return new int[0].GetEnumerator();
			}

			// Token: 0x06006A12 RID: 27154 RVA: 0x0018931C File Offset: 0x0018751C
			public void Remove(int itemIndex)
			{
				if (this.owner.VirtualMode)
				{
					if (itemIndex < 0 || itemIndex >= this.owner.VirtualListSize)
					{
						throw new ArgumentOutOfRangeException("itemIndex", SR.GetString("InvalidArgument", new object[]
						{
							"itemIndex",
							itemIndex.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (this.owner.IsHandleCreated)
					{
						this.owner.SetItemState(itemIndex, 0, 2);
						return;
					}
				}
				else
				{
					if (itemIndex < 0 || itemIndex >= this.owner.Items.Count)
					{
						throw new ArgumentOutOfRangeException("itemIndex", SR.GetString("InvalidArgument", new object[]
						{
							"itemIndex",
							itemIndex.ToString(CultureInfo.CurrentCulture)
						}));
					}
					this.owner.Items[itemIndex].Selected = false;
				}
			}

			// Token: 0x04003B3F RID: 15167
			private ListView owner;
		}

		// Token: 0x020006CA RID: 1738
		[ListBindable(false)]
		public class SelectedListViewItemCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06006A13 RID: 27155 RVA: 0x001893F6 File Offset: 0x001875F6
			public SelectedListViewItemCollection(ListView owner)
			{
				this.owner = owner;
			}

			// Token: 0x170016FA RID: 5882
			// (get) Token: 0x06006A14 RID: 27156 RVA: 0x0018940C File Offset: 0x0018760C
			private ListViewItem[] SelectedItemArray
			{
				get
				{
					if (this.owner.IsHandleCreated)
					{
						int num = (int)((long)this.owner.SendMessage(4146, 0, 0));
						ListViewItem[] array = new ListViewItem[num];
						int wparam = -1;
						for (int i = 0; i < num; i++)
						{
							int num2 = (int)((long)this.owner.SendMessage(4108, wparam, 2));
							if (num2 <= -1)
							{
								throw new InvalidOperationException(SR.GetString("SelectedNotEqualActual"));
							}
							array[i] = this.owner.Items[num2];
							wparam = num2;
						}
						return array;
					}
					if (this.owner.savedSelectedItems != null)
					{
						ListViewItem[] array2 = new ListViewItem[this.owner.savedSelectedItems.Count];
						for (int j = 0; j < this.owner.savedSelectedItems.Count; j++)
						{
							array2[j] = this.owner.savedSelectedItems[j];
						}
						return array2;
					}
					return new ListViewItem[0];
				}
			}

			// Token: 0x170016FB RID: 5883
			// (get) Token: 0x06006A15 RID: 27157 RVA: 0x00189504 File Offset: 0x00187704
			[Browsable(false)]
			public int Count
			{
				get
				{
					if (this.owner.VirtualMode)
					{
						throw new InvalidOperationException(SR.GetString("ListViewCantAccessSelectedItemsCollectionWhenInVirtualMode"));
					}
					if (this.owner.IsHandleCreated)
					{
						return (int)((long)this.owner.SendMessage(4146, 0, 0));
					}
					if (this.owner.savedSelectedItems != null)
					{
						return this.owner.savedSelectedItems.Count;
					}
					return 0;
				}
			}

			// Token: 0x170016FC RID: 5884
			public ListViewItem this[int index]
			{
				get
				{
					if (this.owner.VirtualMode)
					{
						throw new InvalidOperationException(SR.GetString("ListViewCantAccessSelectedItemsCollectionWhenInVirtualMode"));
					}
					if (index < 0 || index >= this.Count)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (this.owner.IsHandleCreated)
					{
						int num = -1;
						for (int i = 0; i <= index; i++)
						{
							num = (int)((long)this.owner.SendMessage(4108, num, 2));
						}
						return this.owner.Items[num];
					}
					return this.owner.savedSelectedItems[index];
				}
			}

			// Token: 0x170016FD RID: 5885
			public virtual ListViewItem this[string key]
			{
				get
				{
					if (this.owner.VirtualMode)
					{
						throw new InvalidOperationException(SR.GetString("ListViewCantAccessSelectedItemsCollectionWhenInVirtualMode"));
					}
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

			// Token: 0x170016FE RID: 5886
			object IList.this[int index]
			{
				get
				{
					if (this.owner.VirtualMode)
					{
						throw new InvalidOperationException(SR.GetString("ListViewCantAccessSelectedItemsCollectionWhenInVirtualMode"));
					}
					return this[index];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x170016FF RID: 5887
			// (get) Token: 0x06006A1A RID: 27162 RVA: 0x00013062 File Offset: 0x00011262
			bool IList.IsFixedSize
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001700 RID: 5888
			// (get) Token: 0x06006A1B RID: 27163 RVA: 0x00013062 File Offset: 0x00011262
			public bool IsReadOnly
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001701 RID: 5889
			// (get) Token: 0x06006A1C RID: 27164 RVA: 0x00006C59 File Offset: 0x00004E59
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17001702 RID: 5890
			// (get) Token: 0x06006A1D RID: 27165 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool ICollection.IsSynchronized
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06006A1E RID: 27166 RVA: 0x0000A547 File Offset: 0x00008747
			int IList.Add(object value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06006A1F RID: 27167 RVA: 0x0000A547 File Offset: 0x00008747
			void IList.Insert(int index, object value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06006A20 RID: 27168 RVA: 0x001896A8 File Offset: 0x001878A8
			private bool IsValidIndex(int index)
			{
				return index >= 0 && index < this.Count;
			}

			// Token: 0x06006A21 RID: 27169 RVA: 0x0000A547 File Offset: 0x00008747
			void IList.Remove(object value)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06006A22 RID: 27170 RVA: 0x0000A547 File Offset: 0x00008747
			void IList.RemoveAt(int index)
			{
				throw new NotSupportedException();
			}

			// Token: 0x06006A23 RID: 27171 RVA: 0x001896BC File Offset: 0x001878BC
			public void Clear()
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessSelectedItemsCollectionWhenInVirtualMode"));
				}
				ListViewItem[] selectedItemArray = this.SelectedItemArray;
				for (int i = 0; i < selectedItemArray.Length; i++)
				{
					selectedItemArray[i].Selected = false;
				}
			}

			// Token: 0x06006A24 RID: 27172 RVA: 0x00189704 File Offset: 0x00187904
			public virtual bool ContainsKey(string key)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessSelectedItemsCollectionWhenInVirtualMode"));
				}
				return this.IsValidIndex(this.IndexOfKey(key));
			}

			// Token: 0x06006A25 RID: 27173 RVA: 0x00189730 File Offset: 0x00187930
			public bool Contains(ListViewItem item)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessSelectedItemsCollectionWhenInVirtualMode"));
				}
				return this.IndexOf(item) != -1;
			}

			// Token: 0x06006A26 RID: 27174 RVA: 0x0018975C File Offset: 0x0018795C
			bool IList.Contains(object item)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessSelectedItemsCollectionWhenInVirtualMode"));
				}
				return item is ListViewItem && this.Contains((ListViewItem)item);
			}

			// Token: 0x06006A27 RID: 27175 RVA: 0x00189791 File Offset: 0x00187991
			public void CopyTo(Array dest, int index)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessSelectedItemsCollectionWhenInVirtualMode"));
				}
				if (this.Count > 0)
				{
					Array.Copy(this.SelectedItemArray, 0, dest, index, this.Count);
				}
			}

			// Token: 0x06006A28 RID: 27176 RVA: 0x001897D0 File Offset: 0x001879D0
			public IEnumerator GetEnumerator()
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessSelectedItemsCollectionWhenInVirtualMode"));
				}
				ListViewItem[] selectedItemArray = this.SelectedItemArray;
				if (selectedItemArray != null)
				{
					return selectedItemArray.GetEnumerator();
				}
				return new ListViewItem[0].GetEnumerator();
			}

			// Token: 0x06006A29 RID: 27177 RVA: 0x00189818 File Offset: 0x00187A18
			public int IndexOf(ListViewItem item)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessSelectedItemsCollectionWhenInVirtualMode"));
				}
				ListViewItem[] selectedItemArray = this.SelectedItemArray;
				for (int i = 0; i < selectedItemArray.Length; i++)
				{
					if (selectedItemArray[i] == item)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06006A2A RID: 27178 RVA: 0x00189860 File Offset: 0x00187A60
			int IList.IndexOf(object item)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessSelectedItemsCollectionWhenInVirtualMode"));
				}
				if (item is ListViewItem)
				{
					return this.IndexOf((ListViewItem)item);
				}
				return -1;
			}

			// Token: 0x06006A2B RID: 27179 RVA: 0x00189898 File Offset: 0x00187A98
			public virtual int IndexOfKey(string key)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAccessSelectedItemsCollectionWhenInVirtualMode"));
				}
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

			// Token: 0x04003B40 RID: 15168
			private ListView owner;

			// Token: 0x04003B41 RID: 15169
			private int lastAccessedIndex = -1;
		}

		// Token: 0x020006CB RID: 1739
		[ListBindable(false)]
		public class ColumnHeaderCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06006A2C RID: 27180 RVA: 0x00189932 File Offset: 0x00187B32
			public ColumnHeaderCollection(ListView owner)
			{
				this.owner = owner;
			}

			// Token: 0x17001703 RID: 5891
			public virtual ColumnHeader this[int index]
			{
				get
				{
					if (this.owner.columnHeaders == null || index < 0 || index >= this.owner.columnHeaders.Length)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					return this.owner.columnHeaders[index];
				}
			}

			// Token: 0x17001704 RID: 5892
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					throw new NotSupportedException();
				}
			}

			// Token: 0x17001705 RID: 5893
			public virtual ColumnHeader this[string key]
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

			// Token: 0x17001706 RID: 5894
			// (get) Token: 0x06006A31 RID: 27185 RVA: 0x001899F1 File Offset: 0x00187BF1
			[Browsable(false)]
			public int Count
			{
				get
				{
					if (this.owner.columnHeaders != null)
					{
						return this.owner.columnHeaders.Length;
					}
					return 0;
				}
			}

			// Token: 0x17001707 RID: 5895
			// (get) Token: 0x06006A32 RID: 27186 RVA: 0x00006C59 File Offset: 0x00004E59
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x17001708 RID: 5896
			// (get) Token: 0x06006A33 RID: 27187 RVA: 0x00013062 File Offset: 0x00011262
			bool ICollection.IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17001709 RID: 5897
			// (get) Token: 0x06006A34 RID: 27188 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700170A RID: 5898
			// (get) Token: 0x06006A35 RID: 27189 RVA: 0x00011A20 File Offset: 0x0000FC20
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x06006A36 RID: 27190 RVA: 0x00189A10 File Offset: 0x00187C10
			public virtual void RemoveByKey(string key)
			{
				int index = this.IndexOfKey(key);
				if (this.IsValidIndex(index))
				{
					this.RemoveAt(index);
				}
			}

			// Token: 0x06006A37 RID: 27191 RVA: 0x00189A38 File Offset: 0x00187C38
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

			// Token: 0x06006A38 RID: 27192 RVA: 0x00189AB5 File Offset: 0x00187CB5
			private bool IsValidIndex(int index)
			{
				return index >= 0 && index < this.Count;
			}

			// Token: 0x06006A39 RID: 27193 RVA: 0x00189AC8 File Offset: 0x00187CC8
			public virtual ColumnHeader Add(string text, int width, HorizontalAlignment textAlign)
			{
				ColumnHeader columnHeader = new ColumnHeader();
				columnHeader.Text = text;
				columnHeader.Width = width;
				columnHeader.TextAlign = textAlign;
				return this.owner.InsertColumn(this.Count, columnHeader);
			}

			// Token: 0x06006A3A RID: 27194 RVA: 0x00189B04 File Offset: 0x00187D04
			public virtual int Add(ColumnHeader value)
			{
				int count = this.Count;
				this.owner.InsertColumn(count, value);
				return count;
			}

			// Token: 0x06006A3B RID: 27195 RVA: 0x00189B28 File Offset: 0x00187D28
			public virtual ColumnHeader Add(string text)
			{
				ColumnHeader columnHeader = new ColumnHeader();
				columnHeader.Text = text;
				return this.owner.InsertColumn(this.Count, columnHeader);
			}

			// Token: 0x06006A3C RID: 27196 RVA: 0x00189B54 File Offset: 0x00187D54
			public virtual ColumnHeader Add(string text, int width)
			{
				ColumnHeader columnHeader = new ColumnHeader();
				columnHeader.Text = text;
				columnHeader.Width = width;
				return this.owner.InsertColumn(this.Count, columnHeader);
			}

			// Token: 0x06006A3D RID: 27197 RVA: 0x00189B88 File Offset: 0x00187D88
			public virtual ColumnHeader Add(string key, string text)
			{
				ColumnHeader columnHeader = new ColumnHeader();
				columnHeader.Name = key;
				columnHeader.Text = text;
				return this.owner.InsertColumn(this.Count, columnHeader);
			}

			// Token: 0x06006A3E RID: 27198 RVA: 0x00189BBC File Offset: 0x00187DBC
			public virtual ColumnHeader Add(string key, string text, int width)
			{
				ColumnHeader columnHeader = new ColumnHeader();
				columnHeader.Name = key;
				columnHeader.Text = text;
				columnHeader.Width = width;
				return this.owner.InsertColumn(this.Count, columnHeader);
			}

			// Token: 0x06006A3F RID: 27199 RVA: 0x00189BF8 File Offset: 0x00187DF8
			public virtual ColumnHeader Add(string key, string text, int width, HorizontalAlignment textAlign, string imageKey)
			{
				ColumnHeader columnHeader = new ColumnHeader(imageKey);
				columnHeader.Name = key;
				columnHeader.Text = text;
				columnHeader.Width = width;
				columnHeader.TextAlign = textAlign;
				return this.owner.InsertColumn(this.Count, columnHeader);
			}

			// Token: 0x06006A40 RID: 27200 RVA: 0x00189C3C File Offset: 0x00187E3C
			public virtual ColumnHeader Add(string key, string text, int width, HorizontalAlignment textAlign, int imageIndex)
			{
				ColumnHeader columnHeader = new ColumnHeader(imageIndex);
				columnHeader.Name = key;
				columnHeader.Text = text;
				columnHeader.Width = width;
				columnHeader.TextAlign = textAlign;
				return this.owner.InsertColumn(this.Count, columnHeader);
			}

			// Token: 0x06006A41 RID: 27201 RVA: 0x00189C80 File Offset: 0x00187E80
			public virtual void AddRange(ColumnHeader[] values)
			{
				if (values == null)
				{
					throw new ArgumentNullException("values");
				}
				Hashtable hashtable = new Hashtable();
				int[] array = new int[values.Length];
				for (int i = 0; i < values.Length; i++)
				{
					if (values[i].DisplayIndex == -1)
					{
						values[i].DisplayIndexInternal = i;
					}
					if (!hashtable.ContainsKey(values[i].DisplayIndex) && values[i].DisplayIndex >= 0 && values[i].DisplayIndex < values.Length)
					{
						hashtable.Add(values[i].DisplayIndex, i);
					}
					array[i] = values[i].DisplayIndex;
					this.Add(values[i]);
				}
				if (hashtable.Count == values.Length)
				{
					this.owner.SetDisplayIndices(array);
				}
			}

			// Token: 0x06006A42 RID: 27202 RVA: 0x00189D3E File Offset: 0x00187F3E
			int IList.Add(object value)
			{
				if (value is ColumnHeader)
				{
					return this.Add((ColumnHeader)value);
				}
				throw new ArgumentException(SR.GetString("ColumnHeaderCollectionInvalidArgument"));
			}

			// Token: 0x06006A43 RID: 27203 RVA: 0x00189D64 File Offset: 0x00187F64
			public virtual void Clear()
			{
				if (this.owner.columnHeaders != null)
				{
					if (this.owner.View == View.Tile)
					{
						for (int i = this.owner.columnHeaders.Length - 1; i >= 0; i--)
						{
							int width = this.owner.columnHeaders[i].Width;
							this.owner.columnHeaders[i].OwnerListview = null;
						}
						this.owner.columnHeaders = null;
						if (this.owner.IsHandleCreated)
						{
							this.owner.RecreateHandleInternal();
							return;
						}
					}
					else
					{
						for (int j = this.owner.columnHeaders.Length - 1; j >= 0; j--)
						{
							int width2 = this.owner.columnHeaders[j].Width;
							if (this.owner.IsHandleCreated)
							{
								this.owner.SendMessage(4124, j, 0);
							}
							this.owner.columnHeaders[j].OwnerListview = null;
						}
						this.owner.columnHeaders = null;
					}
				}
			}

			// Token: 0x06006A44 RID: 27204 RVA: 0x00189E60 File Offset: 0x00188060
			public bool Contains(ColumnHeader value)
			{
				return this.IndexOf(value) != -1;
			}

			// Token: 0x06006A45 RID: 27205 RVA: 0x00189E6F File Offset: 0x0018806F
			bool IList.Contains(object value)
			{
				return value is ColumnHeader && this.Contains((ColumnHeader)value);
			}

			// Token: 0x06006A46 RID: 27206 RVA: 0x00189E87 File Offset: 0x00188087
			public virtual bool ContainsKey(string key)
			{
				return this.IsValidIndex(this.IndexOfKey(key));
			}

			// Token: 0x06006A47 RID: 27207 RVA: 0x00189E96 File Offset: 0x00188096
			void ICollection.CopyTo(Array dest, int index)
			{
				if (this.Count > 0)
				{
					Array.Copy(this.owner.columnHeaders, 0, dest, index, this.Count);
				}
			}

			// Token: 0x06006A48 RID: 27208 RVA: 0x00189EBC File Offset: 0x001880BC
			public int IndexOf(ColumnHeader value)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (this[i] == value)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06006A49 RID: 27209 RVA: 0x00189EE7 File Offset: 0x001880E7
			int IList.IndexOf(object value)
			{
				if (value is ColumnHeader)
				{
					return this.IndexOf((ColumnHeader)value);
				}
				return -1;
			}

			// Token: 0x06006A4A RID: 27210 RVA: 0x00189F00 File Offset: 0x00188100
			public void Insert(int index, ColumnHeader value)
			{
				if (index < 0 || index > this.Count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.owner.InsertColumn(index, value);
			}

			// Token: 0x06006A4B RID: 27211 RVA: 0x00189F5A File Offset: 0x0018815A
			void IList.Insert(int index, object value)
			{
				if (value is ColumnHeader)
				{
					this.Insert(index, (ColumnHeader)value);
				}
			}

			// Token: 0x06006A4C RID: 27212 RVA: 0x00189F74 File Offset: 0x00188174
			public void Insert(int index, string text, int width, HorizontalAlignment textAlign)
			{
				this.Insert(index, new ColumnHeader
				{
					Text = text,
					Width = width,
					TextAlign = textAlign
				});
			}

			// Token: 0x06006A4D RID: 27213 RVA: 0x00189FA8 File Offset: 0x001881A8
			public void Insert(int index, string text)
			{
				this.Insert(index, new ColumnHeader
				{
					Text = text
				});
			}

			// Token: 0x06006A4E RID: 27214 RVA: 0x00189FCC File Offset: 0x001881CC
			public void Insert(int index, string text, int width)
			{
				this.Insert(index, new ColumnHeader
				{
					Text = text,
					Width = width
				});
			}

			// Token: 0x06006A4F RID: 27215 RVA: 0x00189FF8 File Offset: 0x001881F8
			public void Insert(int index, string key, string text)
			{
				this.Insert(index, new ColumnHeader
				{
					Name = key,
					Text = text
				});
			}

			// Token: 0x06006A50 RID: 27216 RVA: 0x0018A024 File Offset: 0x00188224
			public void Insert(int index, string key, string text, int width)
			{
				this.Insert(index, new ColumnHeader
				{
					Name = key,
					Text = text,
					Width = width
				});
			}

			// Token: 0x06006A51 RID: 27217 RVA: 0x0018A058 File Offset: 0x00188258
			public void Insert(int index, string key, string text, int width, HorizontalAlignment textAlign, string imageKey)
			{
				this.Insert(index, new ColumnHeader(imageKey)
				{
					Name = key,
					Text = text,
					Width = width,
					TextAlign = textAlign
				});
			}

			// Token: 0x06006A52 RID: 27218 RVA: 0x0018A094 File Offset: 0x00188294
			public void Insert(int index, string key, string text, int width, HorizontalAlignment textAlign, int imageIndex)
			{
				this.Insert(index, new ColumnHeader(imageIndex)
				{
					Name = key,
					Text = text,
					Width = width,
					TextAlign = textAlign
				});
			}

			// Token: 0x06006A53 RID: 27219 RVA: 0x0018A0D0 File Offset: 0x001882D0
			public virtual void RemoveAt(int index)
			{
				if (index < 0 || index >= this.owner.columnHeaders.Length)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				int width = this.owner.columnHeaders[index].Width;
				if (this.owner.IsHandleCreated && this.owner.View != View.Tile && (int)((long)this.owner.SendMessage(4124, index, 0)) == 0)
				{
					throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				int[] array = new int[this.Count - 1];
				ColumnHeader columnHeader = this[index];
				for (int i = 0; i < this.Count; i++)
				{
					ColumnHeader columnHeader2 = this[i];
					if (i != index)
					{
						if (columnHeader2.DisplayIndex >= columnHeader.DisplayIndex)
						{
							ColumnHeader columnHeader3 = columnHeader2;
							int displayIndexInternal = columnHeader3.DisplayIndexInternal;
							columnHeader3.DisplayIndexInternal = displayIndexInternal - 1;
						}
						array[(i > index) ? (i - 1) : i] = columnHeader2.DisplayIndexInternal;
					}
				}
				columnHeader.DisplayIndexInternal = -1;
				this.owner.columnHeaders[index].OwnerListview = null;
				int num = this.owner.columnHeaders.Length;
				if (num == 1)
				{
					this.owner.columnHeaders = null;
				}
				else
				{
					ColumnHeader[] array2 = new ColumnHeader[--num];
					if (index > 0)
					{
						Array.Copy(this.owner.columnHeaders, 0, array2, 0, index);
					}
					if (index < num)
					{
						Array.Copy(this.owner.columnHeaders, index + 1, array2, index, num - index);
					}
					this.owner.columnHeaders = array2;
				}
				if (this.owner.IsHandleCreated && this.owner.View == View.Tile)
				{
					this.owner.RecreateHandleInternal();
				}
				this.owner.SetDisplayIndices(array);
			}

			// Token: 0x06006A54 RID: 27220 RVA: 0x0018A2CC File Offset: 0x001884CC
			public virtual void Remove(ColumnHeader column)
			{
				int num = this.IndexOf(column);
				if (num != -1)
				{
					this.RemoveAt(num);
				}
			}

			// Token: 0x06006A55 RID: 27221 RVA: 0x0018A2EC File Offset: 0x001884EC
			void IList.Remove(object value)
			{
				if (value is ColumnHeader)
				{
					this.Remove((ColumnHeader)value);
				}
			}

			// Token: 0x06006A56 RID: 27222 RVA: 0x0018A302 File Offset: 0x00188502
			public IEnumerator GetEnumerator()
			{
				if (this.owner.columnHeaders != null)
				{
					return this.owner.columnHeaders.GetEnumerator();
				}
				return new ColumnHeader[0].GetEnumerator();
			}

			// Token: 0x04003B42 RID: 15170
			private ListView owner;

			// Token: 0x04003B43 RID: 15171
			private int lastAccessedIndex = -1;
		}

		// Token: 0x020006CC RID: 1740
		[ListBindable(false)]
		public class ListViewItemCollection : IList, ICollection, IEnumerable
		{
			// Token: 0x06006A57 RID: 27223 RVA: 0x0018A32D File Offset: 0x0018852D
			public ListViewItemCollection(ListView owner)
			{
				this.innerList = new ListView.ListViewNativeItemCollection(owner);
			}

			// Token: 0x06006A58 RID: 27224 RVA: 0x0018A348 File Offset: 0x00188548
			internal ListViewItemCollection(ListView.ListViewItemCollection.IInnerList innerList)
			{
				this.innerList = innerList;
			}

			// Token: 0x1700170B RID: 5899
			// (get) Token: 0x06006A59 RID: 27225 RVA: 0x0018A35E File Offset: 0x0018855E
			private ListView.ListViewItemCollection.IInnerList InnerList
			{
				get
				{
					return this.innerList;
				}
			}

			// Token: 0x1700170C RID: 5900
			// (get) Token: 0x06006A5A RID: 27226 RVA: 0x0018A366 File Offset: 0x00188566
			[Browsable(false)]
			public int Count
			{
				get
				{
					return this.InnerList.Count;
				}
			}

			// Token: 0x1700170D RID: 5901
			// (get) Token: 0x06006A5B RID: 27227 RVA: 0x00006C59 File Offset: 0x00004E59
			object ICollection.SyncRoot
			{
				get
				{
					return this;
				}
			}

			// Token: 0x1700170E RID: 5902
			// (get) Token: 0x06006A5C RID: 27228 RVA: 0x00013062 File Offset: 0x00011262
			bool ICollection.IsSynchronized
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700170F RID: 5903
			// (get) Token: 0x06006A5D RID: 27229 RVA: 0x00011A20 File Offset: 0x0000FC20
			bool IList.IsFixedSize
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001710 RID: 5904
			// (get) Token: 0x06006A5E RID: 27230 RVA: 0x00011A20 File Offset: 0x0000FC20
			public bool IsReadOnly
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17001711 RID: 5905
			public virtual ListViewItem this[int index]
			{
				get
				{
					if (index < 0 || index >= this.InnerList.Count)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					return this.InnerList[index];
				}
				set
				{
					if (index < 0 || index >= this.InnerList.Count)
					{
						throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
					this.InnerList[index] = value;
				}
			}

			// Token: 0x17001712 RID: 5906
			object IList.this[int index]
			{
				get
				{
					return this[index];
				}
				set
				{
					if (value is ListViewItem)
					{
						this[index] = (ListViewItem)value;
						return;
					}
					if (value != null)
					{
						this[index] = new ListViewItem(value.ToString(), -1);
					}
				}
			}

			// Token: 0x17001713 RID: 5907
			public virtual ListViewItem this[string key]
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

			// Token: 0x06006A64 RID: 27236 RVA: 0x0018A49D File Offset: 0x0018869D
			public virtual ListViewItem Add(string text)
			{
				return this.Add(text, -1);
			}

			// Token: 0x06006A65 RID: 27237 RVA: 0x0018A4A7 File Offset: 0x001886A7
			int IList.Add(object item)
			{
				if (item is ListViewItem)
				{
					return this.IndexOf(this.Add((ListViewItem)item));
				}
				if (item != null)
				{
					return this.IndexOf(this.Add(item.ToString()));
				}
				return -1;
			}

			// Token: 0x06006A66 RID: 27238 RVA: 0x0018A4DC File Offset: 0x001886DC
			public virtual ListViewItem Add(string text, int imageIndex)
			{
				ListViewItem listViewItem = new ListViewItem(text, imageIndex);
				this.Add(listViewItem);
				return listViewItem;
			}

			// Token: 0x06006A67 RID: 27239 RVA: 0x0018A4FA File Offset: 0x001886FA
			public virtual ListViewItem Add(ListViewItem value)
			{
				this.InnerList.Add(value);
				return value;
			}

			// Token: 0x06006A68 RID: 27240 RVA: 0x0018A50C File Offset: 0x0018870C
			public virtual ListViewItem Add(string text, string imageKey)
			{
				ListViewItem listViewItem = new ListViewItem(text, imageKey);
				this.Add(listViewItem);
				return listViewItem;
			}

			// Token: 0x06006A69 RID: 27241 RVA: 0x0018A52C File Offset: 0x0018872C
			public virtual ListViewItem Add(string key, string text, string imageKey)
			{
				ListViewItem listViewItem = new ListViewItem(text, imageKey);
				listViewItem.Name = key;
				this.Add(listViewItem);
				return listViewItem;
			}

			// Token: 0x06006A6A RID: 27242 RVA: 0x0018A554 File Offset: 0x00188754
			public virtual ListViewItem Add(string key, string text, int imageIndex)
			{
				ListViewItem listViewItem = new ListViewItem(text, imageIndex);
				listViewItem.Name = key;
				this.Add(listViewItem);
				return listViewItem;
			}

			// Token: 0x06006A6B RID: 27243 RVA: 0x0018A579 File Offset: 0x00188779
			public void AddRange(ListViewItem[] items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				this.InnerList.AddRange(items);
			}

			// Token: 0x06006A6C RID: 27244 RVA: 0x0018A598 File Offset: 0x00188798
			public void AddRange(ListView.ListViewItemCollection items)
			{
				if (items == null)
				{
					throw new ArgumentNullException("items");
				}
				ListViewItem[] array = new ListViewItem[items.Count];
				items.CopyTo(array, 0);
				this.InnerList.AddRange(array);
			}

			// Token: 0x06006A6D RID: 27245 RVA: 0x0018A5D3 File Offset: 0x001887D3
			public virtual void Clear()
			{
				this.InnerList.Clear();
			}

			// Token: 0x06006A6E RID: 27246 RVA: 0x0018A5E0 File Offset: 0x001887E0
			public bool Contains(ListViewItem item)
			{
				return this.InnerList.Contains(item);
			}

			// Token: 0x06006A6F RID: 27247 RVA: 0x0018A5EE File Offset: 0x001887EE
			bool IList.Contains(object item)
			{
				return item is ListViewItem && this.Contains((ListViewItem)item);
			}

			// Token: 0x06006A70 RID: 27248 RVA: 0x0018A606 File Offset: 0x00188806
			public virtual bool ContainsKey(string key)
			{
				return this.IsValidIndex(this.IndexOfKey(key));
			}

			// Token: 0x06006A71 RID: 27249 RVA: 0x0018A615 File Offset: 0x00188815
			public void CopyTo(Array dest, int index)
			{
				this.InnerList.CopyTo(dest, index);
			}

			// Token: 0x06006A72 RID: 27250 RVA: 0x0018A624 File Offset: 0x00188824
			public ListViewItem[] Find(string key, bool searchAllSubItems)
			{
				ArrayList arrayList = this.FindInternal(key, searchAllSubItems, this, new ArrayList());
				ListViewItem[] array = new ListViewItem[arrayList.Count];
				arrayList.CopyTo(array, 0);
				return array;
			}

			// Token: 0x06006A73 RID: 27251 RVA: 0x0018A658 File Offset: 0x00188858
			private ArrayList FindInternal(string key, bool searchAllSubItems, ListView.ListViewItemCollection listViewItems, ArrayList foundItems)
			{
				if (listViewItems == null || foundItems == null)
				{
					return null;
				}
				for (int i = 0; i < listViewItems.Count; i++)
				{
					if (WindowsFormsUtils.SafeCompareStrings(listViewItems[i].Name, key, true))
					{
						foundItems.Add(listViewItems[i]);
					}
					else if (searchAllSubItems)
					{
						for (int j = 1; j < listViewItems[i].SubItems.Count; j++)
						{
							if (WindowsFormsUtils.SafeCompareStrings(listViewItems[i].SubItems[j].Name, key, true))
							{
								foundItems.Add(listViewItems[i]);
								break;
							}
						}
					}
				}
				return foundItems;
			}

			// Token: 0x06006A74 RID: 27252 RVA: 0x0018A6FA File Offset: 0x001888FA
			public IEnumerator GetEnumerator()
			{
				if (this.InnerList.OwnerIsVirtualListView && !this.InnerList.OwnerIsDesignMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantGetEnumeratorInVirtualMode"));
				}
				return this.InnerList.GetEnumerator();
			}

			// Token: 0x06006A75 RID: 27253 RVA: 0x0018A734 File Offset: 0x00188934
			public int IndexOf(ListViewItem item)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (this[i] == item)
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06006A76 RID: 27254 RVA: 0x0018A75F File Offset: 0x0018895F
			int IList.IndexOf(object item)
			{
				if (item is ListViewItem)
				{
					return this.IndexOf((ListViewItem)item);
				}
				return -1;
			}

			// Token: 0x06006A77 RID: 27255 RVA: 0x0018A778 File Offset: 0x00188978
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

			// Token: 0x06006A78 RID: 27256 RVA: 0x0018A7F5 File Offset: 0x001889F5
			private bool IsValidIndex(int index)
			{
				return index >= 0 && index < this.Count;
			}

			// Token: 0x06006A79 RID: 27257 RVA: 0x0018A808 File Offset: 0x00188A08
			public ListViewItem Insert(int index, ListViewItem item)
			{
				if (index < 0 || index > this.Count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.InnerList.Insert(index, item);
				return item;
			}

			// Token: 0x06006A7A RID: 27258 RVA: 0x0018A863 File Offset: 0x00188A63
			public ListViewItem Insert(int index, string text)
			{
				return this.Insert(index, new ListViewItem(text));
			}

			// Token: 0x06006A7B RID: 27259 RVA: 0x0018A872 File Offset: 0x00188A72
			public ListViewItem Insert(int index, string text, int imageIndex)
			{
				return this.Insert(index, new ListViewItem(text, imageIndex));
			}

			// Token: 0x06006A7C RID: 27260 RVA: 0x0018A882 File Offset: 0x00188A82
			void IList.Insert(int index, object item)
			{
				if (item is ListViewItem)
				{
					this.Insert(index, (ListViewItem)item);
					return;
				}
				if (item != null)
				{
					this.Insert(index, item.ToString());
				}
			}

			// Token: 0x06006A7D RID: 27261 RVA: 0x0018A8AC File Offset: 0x00188AAC
			public ListViewItem Insert(int index, string text, string imageKey)
			{
				return this.Insert(index, new ListViewItem(text, imageKey));
			}

			// Token: 0x06006A7E RID: 27262 RVA: 0x0018A8BC File Offset: 0x00188ABC
			public virtual ListViewItem Insert(int index, string key, string text, string imageKey)
			{
				return this.Insert(index, new ListViewItem(text, imageKey)
				{
					Name = key
				});
			}

			// Token: 0x06006A7F RID: 27263 RVA: 0x0018A8E4 File Offset: 0x00188AE4
			public virtual ListViewItem Insert(int index, string key, string text, int imageIndex)
			{
				return this.Insert(index, new ListViewItem(text, imageIndex)
				{
					Name = key
				});
			}

			// Token: 0x06006A80 RID: 27264 RVA: 0x0018A909 File Offset: 0x00188B09
			public virtual void Remove(ListViewItem item)
			{
				this.InnerList.Remove(item);
			}

			// Token: 0x06006A81 RID: 27265 RVA: 0x0018A918 File Offset: 0x00188B18
			public virtual void RemoveAt(int index)
			{
				if (index < 0 || index >= this.Count)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.InnerList.RemoveAt(index);
			}

			// Token: 0x06006A82 RID: 27266 RVA: 0x0018A970 File Offset: 0x00188B70
			public virtual void RemoveByKey(string key)
			{
				int index = this.IndexOfKey(key);
				if (this.IsValidIndex(index))
				{
					this.RemoveAt(index);
				}
			}

			// Token: 0x06006A83 RID: 27267 RVA: 0x0018A995 File Offset: 0x00188B95
			void IList.Remove(object item)
			{
				if (item == null || !(item is ListViewItem))
				{
					return;
				}
				this.Remove((ListViewItem)item);
			}

			// Token: 0x04003B44 RID: 15172
			private int lastAccessedIndex = -1;

			// Token: 0x04003B45 RID: 15173
			private ListView.ListViewItemCollection.IInnerList innerList;

			// Token: 0x020008C4 RID: 2244
			internal interface IInnerList
			{
				// Token: 0x17001938 RID: 6456
				// (get) Token: 0x060072F6 RID: 29430
				int Count { get; }

				// Token: 0x17001939 RID: 6457
				// (get) Token: 0x060072F7 RID: 29431
				bool OwnerIsVirtualListView { get; }

				// Token: 0x1700193A RID: 6458
				// (get) Token: 0x060072F8 RID: 29432
				bool OwnerIsDesignMode { get; }

				// Token: 0x1700193B RID: 6459
				ListViewItem this[int index]
				{
					get;
					set;
				}

				// Token: 0x060072FB RID: 29435
				ListViewItem Add(ListViewItem item);

				// Token: 0x060072FC RID: 29436
				void AddRange(ListViewItem[] items);

				// Token: 0x060072FD RID: 29437
				void Clear();

				// Token: 0x060072FE RID: 29438
				bool Contains(ListViewItem item);

				// Token: 0x060072FF RID: 29439
				void CopyTo(Array dest, int index);

				// Token: 0x06007300 RID: 29440
				IEnumerator GetEnumerator();

				// Token: 0x06007301 RID: 29441
				int IndexOf(ListViewItem item);

				// Token: 0x06007302 RID: 29442
				ListViewItem Insert(int index, ListViewItem item);

				// Token: 0x06007303 RID: 29443
				void Remove(ListViewItem item);

				// Token: 0x06007304 RID: 29444
				void RemoveAt(int index);
			}
		}

		// Token: 0x020006CD RID: 1741
		internal class ListViewNativeItemCollection : ListView.ListViewItemCollection.IInnerList
		{
			// Token: 0x06006A84 RID: 27268 RVA: 0x0018A9AF File Offset: 0x00188BAF
			public ListViewNativeItemCollection(ListView owner)
			{
				this.owner = owner;
			}

			// Token: 0x17001714 RID: 5908
			// (get) Token: 0x06006A85 RID: 27269 RVA: 0x0018A9BE File Offset: 0x00188BBE
			public int Count
			{
				get
				{
					this.owner.ApplyUpdateCachedItems();
					if (this.owner.VirtualMode)
					{
						return this.owner.VirtualListSize;
					}
					return this.owner.itemCount;
				}
			}

			// Token: 0x17001715 RID: 5909
			// (get) Token: 0x06006A86 RID: 27270 RVA: 0x0018A9EF File Offset: 0x00188BEF
			public bool OwnerIsVirtualListView
			{
				get
				{
					return this.owner.VirtualMode;
				}
			}

			// Token: 0x17001716 RID: 5910
			// (get) Token: 0x06006A87 RID: 27271 RVA: 0x0018A9FC File Offset: 0x00188BFC
			public bool OwnerIsDesignMode
			{
				get
				{
					return this.owner.DesignMode;
				}
			}

			// Token: 0x17001717 RID: 5911
			public ListViewItem this[int displayIndex]
			{
				get
				{
					this.owner.ApplyUpdateCachedItems();
					if (this.owner.VirtualMode)
					{
						RetrieveVirtualItemEventArgs retrieveVirtualItemEventArgs = new RetrieveVirtualItemEventArgs(displayIndex);
						this.owner.OnRetrieveVirtualItem(retrieveVirtualItemEventArgs);
						retrieveVirtualItemEventArgs.Item.SetItemIndex(this.owner, displayIndex);
						return retrieveVirtualItemEventArgs.Item;
					}
					if (displayIndex < 0 || displayIndex >= this.owner.itemCount)
					{
						throw new ArgumentOutOfRangeException("displayIndex", SR.GetString("InvalidArgument", new object[]
						{
							"displayIndex",
							displayIndex.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (this.owner.IsHandleCreated && !this.owner.ListViewHandleDestroyed)
					{
						return (ListViewItem)this.owner.listItemsTable[this.DisplayIndexToID(displayIndex)];
					}
					return (ListViewItem)this.owner.listItemsArray[displayIndex];
				}
				set
				{
					this.owner.ApplyUpdateCachedItems();
					if (this.owner.VirtualMode)
					{
						throw new InvalidOperationException(SR.GetString("ListViewCantModifyTheItemCollInAVirtualListView"));
					}
					if (displayIndex < 0 || displayIndex >= this.owner.itemCount)
					{
						throw new ArgumentOutOfRangeException("displayIndex", SR.GetString("InvalidArgument", new object[]
						{
							"displayIndex",
							displayIndex.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (this.owner.ExpectingMouseUp)
					{
						this.owner.ItemCollectionChangedInMouseDown = true;
					}
					this.RemoveAt(displayIndex);
					this.Insert(displayIndex, value);
				}
			}

			// Token: 0x06006A8A RID: 27274 RVA: 0x0018AB98 File Offset: 0x00188D98
			public ListViewItem Add(ListViewItem value)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAddItemsToAVirtualListView"));
				}
				bool @checked = value.Checked;
				this.owner.InsertItems(this.owner.itemCount, new ListViewItem[]
				{
					value
				}, true);
				if (this.owner.IsHandleCreated && !this.owner.CheckBoxes && @checked)
				{
					this.owner.UpdateSavedCheckedItems(value, true);
				}
				if (this.owner.ExpectingMouseUp)
				{
					this.owner.ItemCollectionChangedInMouseDown = true;
				}
				return value;
			}

			// Token: 0x06006A8B RID: 27275 RVA: 0x0018AC34 File Offset: 0x00188E34
			public void AddRange(ListViewItem[] values)
			{
				if (values == null)
				{
					throw new ArgumentNullException("values");
				}
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAddItemsToAVirtualListView"));
				}
				IComparer listItemSorter = this.owner.listItemSorter;
				this.owner.listItemSorter = null;
				bool[] array = null;
				if (this.owner.IsHandleCreated && !this.owner.CheckBoxes)
				{
					array = new bool[values.Length];
					for (int i = 0; i < values.Length; i++)
					{
						array[i] = values[i].Checked;
					}
				}
				try
				{
					this.owner.BeginUpdate();
					this.owner.InsertItems(this.owner.itemCount, values, true);
					if (this.owner.IsHandleCreated && !this.owner.CheckBoxes)
					{
						for (int j = 0; j < values.Length; j++)
						{
							if (array[j])
							{
								this.owner.UpdateSavedCheckedItems(values[j], true);
							}
						}
					}
				}
				finally
				{
					this.owner.listItemSorter = listItemSorter;
					this.owner.EndUpdate();
				}
				if (this.owner.ExpectingMouseUp)
				{
					this.owner.ItemCollectionChangedInMouseDown = true;
				}
				if (listItemSorter != null || (this.owner.Sorting != SortOrder.None && !this.owner.VirtualMode))
				{
					this.owner.Sort();
				}
			}

			// Token: 0x06006A8C RID: 27276 RVA: 0x0018AD90 File Offset: 0x00188F90
			private int DisplayIndexToID(int displayIndex)
			{
				if (this.owner.IsHandleCreated && !this.owner.ListViewHandleDestroyed)
				{
					NativeMethods.LVITEM lvitem = default(NativeMethods.LVITEM);
					lvitem.mask = 4;
					lvitem.iItem = displayIndex;
					UnsafeNativeMethods.SendMessage(new HandleRef(this.owner, this.owner.Handle), NativeMethods.LVM_GETITEM, 0, ref lvitem);
					return (int)lvitem.lParam;
				}
				return this[displayIndex].ID;
			}

			// Token: 0x06006A8D RID: 27277 RVA: 0x0018AE0C File Offset: 0x0018900C
			public void Clear()
			{
				if (this.owner.itemCount > 0)
				{
					this.owner.ApplyUpdateCachedItems();
					if (this.owner.IsHandleCreated && !this.owner.ListViewHandleDestroyed)
					{
						int count = this.owner.Items.Count;
						int num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this.owner, this.owner.Handle), 4108, -1, 2);
						for (int i = 0; i < count; i++)
						{
							ListViewItem listViewItem = this.owner.Items[i];
							if (listViewItem != null)
							{
								if (i == num)
								{
									listViewItem.StateSelected = true;
									num = (int)UnsafeNativeMethods.SendMessage(new HandleRef(this.owner, this.owner.Handle), 4108, num, 2);
								}
								else
								{
									listViewItem.StateSelected = false;
								}
								listViewItem.UnHost(i, false);
							}
						}
						UnsafeNativeMethods.SendMessage(new HandleRef(this.owner, this.owner.Handle), 4105, 0, 0);
						if (this.owner.View == View.SmallIcon)
						{
							if (this.owner.ComctlSupportsVisualStyles)
							{
								this.owner.FlipViewToLargeIconAndSmallIcon = true;
							}
							else
							{
								this.owner.View = View.LargeIcon;
								this.owner.View = View.SmallIcon;
							}
						}
					}
					else
					{
						int count2 = this.owner.Items.Count;
						for (int j = 0; j < count2; j++)
						{
							ListViewItem listViewItem2 = this.owner.Items[j];
							if (listViewItem2 != null)
							{
								listViewItem2.UnHost(j, true);
							}
						}
						this.owner.listItemsArray.Clear();
					}
					this.owner.listItemsTable.Clear();
					if (this.owner.IsHandleCreated && !this.owner.CheckBoxes)
					{
						this.owner.savedCheckedItems = null;
					}
					this.owner.itemCount = 0;
					if (this.owner.ExpectingMouseUp)
					{
						this.owner.ItemCollectionChangedInMouseDown = true;
					}
				}
			}

			// Token: 0x06006A8E RID: 27278 RVA: 0x0018B00C File Offset: 0x0018920C
			public bool Contains(ListViewItem item)
			{
				this.owner.ApplyUpdateCachedItems();
				if (this.owner.IsHandleCreated && !this.owner.ListViewHandleDestroyed)
				{
					return this.owner.listItemsTable[item.ID] == item;
				}
				return this.owner.listItemsArray.Contains(item);
			}

			// Token: 0x06006A8F RID: 27279 RVA: 0x0018B070 File Offset: 0x00189270
			public ListViewItem Insert(int index, ListViewItem item)
			{
				int num;
				if (this.owner.VirtualMode)
				{
					num = this.Count;
				}
				else
				{
					num = this.owner.itemCount;
				}
				if (index < 0 || index > num)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantAddItemsToAVirtualListView"));
				}
				if (index < num)
				{
					this.owner.ApplyUpdateCachedItems();
				}
				this.owner.InsertItems(index, new ListViewItem[]
				{
					item
				}, true);
				if (this.owner.IsHandleCreated && !this.owner.CheckBoxes && item.Checked)
				{
					this.owner.UpdateSavedCheckedItems(item, true);
				}
				if (this.owner.ExpectingMouseUp)
				{
					this.owner.ItemCollectionChangedInMouseDown = true;
				}
				return item;
			}

			// Token: 0x06006A90 RID: 27280 RVA: 0x0018B168 File Offset: 0x00189368
			public int IndexOf(ListViewItem item)
			{
				for (int i = 0; i < this.Count; i++)
				{
					if (item == this[i])
					{
						return i;
					}
				}
				return -1;
			}

			// Token: 0x06006A91 RID: 27281 RVA: 0x0018B194 File Offset: 0x00189394
			public void Remove(ListViewItem item)
			{
				int num = this.owner.VirtualMode ? (this.Count - 1) : this.IndexOf(item);
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantRemoveItemsFromAVirtualListView"));
				}
				if (num != -1)
				{
					this.RemoveAt(num);
				}
			}

			// Token: 0x06006A92 RID: 27282 RVA: 0x0018B1E8 File Offset: 0x001893E8
			public void RemoveAt(int index)
			{
				if (this.owner.VirtualMode)
				{
					throw new InvalidOperationException(SR.GetString("ListViewCantRemoveItemsFromAVirtualListView"));
				}
				if (index < 0 || index >= this.owner.itemCount)
				{
					throw new ArgumentOutOfRangeException("index", SR.GetString("InvalidArgument", new object[]
					{
						"index",
						index.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.owner.IsHandleCreated && !this.owner.CheckBoxes && this[index].Checked)
				{
					this.owner.UpdateSavedCheckedItems(this[index], false);
				}
				this.owner.ApplyUpdateCachedItems();
				int num = this.DisplayIndexToID(index);
				this[index].UnHost(true);
				if (this.owner.IsHandleCreated)
				{
					if ((int)((long)this.owner.SendMessage(4104, index, 0)) == 0)
					{
						throw new ArgumentException(SR.GetString("InvalidArgument", new object[]
						{
							"index",
							index.ToString(CultureInfo.CurrentCulture)
						}));
					}
				}
				else
				{
					this.owner.listItemsArray.RemoveAt(index);
				}
				this.owner.itemCount--;
				this.owner.listItemsTable.Remove(num);
				if (this.owner.ExpectingMouseUp)
				{
					this.owner.ItemCollectionChangedInMouseDown = true;
				}
			}

			// Token: 0x06006A93 RID: 27283 RVA: 0x0018B35C File Offset: 0x0018955C
			public void CopyTo(Array dest, int index)
			{
				if (this.owner.itemCount > 0)
				{
					for (int i = 0; i < this.Count; i++)
					{
						dest.SetValue(this[i], index++);
					}
				}
			}

			// Token: 0x06006A94 RID: 27284 RVA: 0x0018B39C File Offset: 0x0018959C
			public IEnumerator GetEnumerator()
			{
				ListViewItem[] array = new ListViewItem[this.owner.itemCount];
				this.CopyTo(array, 0);
				return array.GetEnumerator();
			}

			// Token: 0x04003B46 RID: 15174
			private ListView owner;
		}

		// Token: 0x020006CE RID: 1742
		internal class ListViewAccessibleObject : Control.ControlAccessibleObject
		{
			// Token: 0x06006A95 RID: 27285 RVA: 0x0009B963 File Offset: 0x00099B63
			internal ListViewAccessibleObject(ListView owner) : base(owner)
			{
			}

			// Token: 0x06006A96 RID: 27286 RVA: 0x0018B3C8 File Offset: 0x001895C8
			internal override bool IsIAccessibleExSupported()
			{
				return base.Owner != null || base.IsIAccessibleExSupported();
			}

			// Token: 0x06006A97 RID: 27287 RVA: 0x0018B3DC File Offset: 0x001895DC
			internal override object GetPropertyValue(int propertyID)
			{
				if (propertyID == 30026)
				{
					if (!base.IsOwnerControlDestroyed())
					{
						ListView listView = base.Owner as ListView;
						if (listView != null)
						{
							switch (listView.Sorting)
							{
							case SortOrder.None:
								return SR.GetString("NotSortedAccessibleStatus");
							case SortOrder.Ascending:
								return SR.GetString("SortedAscendingAccessibleStatus");
							case SortOrder.Descending:
								return SR.GetString("SortedDescendingAccessibleStatus");
							default:
								goto IL_61;
							}
						}
					}
					return string.Empty;
				}
				IL_61:
				return base.GetPropertyValue(propertyID);
			}
		}
	}
}

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
using System.Windows.Forms.VisualStyles;

namespace System.Windows.Forms
{
	// Token: 0x02000418 RID: 1048
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[DefaultProperty("Nodes")]
	[DefaultEvent("AfterSelect")]
	[Docking(DockingBehavior.Ask)]
	[Designer("System.Windows.Forms.Design.TreeViewDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	[SRDescription("DescriptionTreeView")]
	public class TreeView : Control
	{
		// Token: 0x170011E8 RID: 4584
		// (get) Token: 0x060048FD RID: 18685 RVA: 0x00133429 File Offset: 0x00131629
		private static Size? ScaledStateImageSize
		{
			get
			{
				if (!TreeView.isScalingInitialized)
				{
					if (DpiHelper.IsScalingRequired)
					{
						TreeView.scaledStateImageSize = new Size?(DpiHelper.LogicalToDeviceUnits(new Size(16, 16), 0));
					}
					TreeView.isScalingInitialized = true;
				}
				return TreeView.scaledStateImageSize;
			}
		}

		// Token: 0x170011E9 RID: 4585
		// (get) Token: 0x060048FE RID: 18686 RVA: 0x0013345D File Offset: 0x0013165D
		internal ImageList.Indexer ImageIndexer
		{
			get
			{
				if (this.imageIndexer == null)
				{
					this.imageIndexer = new ImageList.Indexer();
				}
				this.imageIndexer.ImageList = this.ImageList;
				return this.imageIndexer;
			}
		}

		// Token: 0x170011EA RID: 4586
		// (get) Token: 0x060048FF RID: 18687 RVA: 0x00133489 File Offset: 0x00131689
		internal ImageList.Indexer SelectedImageIndexer
		{
			get
			{
				if (this.selectedImageIndexer == null)
				{
					this.selectedImageIndexer = new ImageList.Indexer();
				}
				this.selectedImageIndexer.ImageList = this.ImageList;
				return this.selectedImageIndexer;
			}
		}

		// Token: 0x06004900 RID: 18688 RVA: 0x001334B8 File Offset: 0x001316B8
		public TreeView()
		{
			this.treeViewState = new BitVector32(117);
			this.root = new TreeNode(this);
			this.SelectedImageIndexer.Index = 0;
			this.ImageIndexer.Index = 0;
			base.SetStyle(ControlStyles.UserPaint, false);
			base.SetStyle(ControlStyles.StandardClick, false);
			base.SetStyle(ControlStyles.UseTextForAccessibility, false);
		}

		// Token: 0x170011EB RID: 4587
		// (get) Token: 0x06004901 RID: 18689 RVA: 0x00027F43 File Offset: 0x00026143
		// (set) Token: 0x06004902 RID: 18690 RVA: 0x00133552 File Offset: 0x00131752
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
					base.SendMessage(4381, 0, ColorTranslator.ToWin32(this.BackColor));
					base.SendMessage(4359, this.Indent, 0);
				}
			}
		}

		// Token: 0x170011EC RID: 4588
		// (get) Token: 0x06004903 RID: 18691 RVA: 0x00011A90 File Offset: 0x0000FC90
		// (set) Token: 0x06004904 RID: 18692 RVA: 0x00011A98 File Offset: 0x0000FC98
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

		// Token: 0x140003A0 RID: 928
		// (add) Token: 0x06004905 RID: 18693 RVA: 0x00011AA1 File Offset: 0x0000FCA1
		// (remove) Token: 0x06004906 RID: 18694 RVA: 0x00011AAA File Offset: 0x0000FCAA
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

		// Token: 0x170011ED RID: 4589
		// (get) Token: 0x06004907 RID: 18695 RVA: 0x00011AB3 File Offset: 0x0000FCB3
		// (set) Token: 0x06004908 RID: 18696 RVA: 0x00011ABB File Offset: 0x0000FCBB
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

		// Token: 0x140003A1 RID: 929
		// (add) Token: 0x06004909 RID: 18697 RVA: 0x00011AC4 File Offset: 0x0000FCC4
		// (remove) Token: 0x0600490A RID: 18698 RVA: 0x00011ACD File Offset: 0x0000FCCD
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

		// Token: 0x170011EE RID: 4590
		// (get) Token: 0x0600490B RID: 18699 RVA: 0x0013358E File Offset: 0x0013178E
		// (set) Token: 0x0600490C RID: 18700 RVA: 0x00133596 File Offset: 0x00131796
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

		// Token: 0x170011EF RID: 4591
		// (get) Token: 0x0600490D RID: 18701 RVA: 0x001335D4 File Offset: 0x001317D4
		// (set) Token: 0x0600490E RID: 18702 RVA: 0x001335E4 File Offset: 0x001317E4
		[SRCategory("CatAppearance")]
		[DefaultValue(false)]
		[SRDescription("TreeViewCheckBoxesDescr")]
		public bool CheckBoxes
		{
			get
			{
				return this.treeViewState[8];
			}
			set
			{
				if (this.CheckBoxes != value)
				{
					this.treeViewState[8] = value;
					if (base.IsHandleCreated)
					{
						if (this.CheckBoxes)
						{
							base.UpdateStyles();
							return;
						}
						this.UpdateCheckedState(this.root, false);
						base.RecreateHandle();
					}
				}
			}
		}

		// Token: 0x170011F0 RID: 4592
		// (get) Token: 0x0600490F RID: 18703 RVA: 0x00133634 File Offset: 0x00131834
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				createParams.ClassName = "SysTreeView32";
				if (base.IsHandleCreated)
				{
					int num = (int)((long)UnsafeNativeMethods.GetWindowLong(new HandleRef(this, base.Handle), -16));
					createParams.Style |= (num & 3145728);
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
				if (!this.Scrollable)
				{
					createParams.Style |= 8192;
				}
				if (!this.HideSelection)
				{
					createParams.Style |= 32;
				}
				if (this.LabelEdit)
				{
					createParams.Style |= 8;
				}
				if (this.ShowLines)
				{
					createParams.Style |= 2;
				}
				if (this.ShowPlusMinus)
				{
					createParams.Style |= 1;
				}
				if (this.ShowRootLines)
				{
					createParams.Style |= 4;
				}
				if (this.HotTracking)
				{
					createParams.Style |= 512;
				}
				if (this.FullRowSelect)
				{
					createParams.Style |= 4096;
				}
				if (this.setOddHeight)
				{
					createParams.Style |= 16384;
				}
				if (this.ShowNodeToolTips && base.IsHandleCreated && !base.DesignMode)
				{
					createParams.Style |= 2048;
				}
				if (this.CheckBoxes && base.IsHandleCreated)
				{
					createParams.Style |= 256;
				}
				if (this.RightToLeft == RightToLeft.Yes)
				{
					if (this.RightToLeftLayout)
					{
						createParams.ExStyle |= 4194304;
						createParams.ExStyle &= -28673;
					}
					else
					{
						createParams.Style |= 64;
					}
				}
				return createParams;
			}
		}

		// Token: 0x170011F1 RID: 4593
		// (get) Token: 0x06004910 RID: 18704 RVA: 0x000C9B1C File Offset: 0x000C7D1C
		protected override Size DefaultSize
		{
			get
			{
				return new Size(121, 97);
			}
		}

		// Token: 0x170011F2 RID: 4594
		// (get) Token: 0x06004911 RID: 18705 RVA: 0x000131D7 File Offset: 0x000113D7
		// (set) Token: 0x06004912 RID: 18706 RVA: 0x000131DF File Offset: 0x000113DF
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

		// Token: 0x170011F3 RID: 4595
		// (get) Token: 0x06004913 RID: 18707 RVA: 0x00013222 File Offset: 0x00011422
		// (set) Token: 0x06004914 RID: 18708 RVA: 0x00133828 File Offset: 0x00131A28
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
					base.SendMessage(4382, 0, ColorTranslator.ToWin32(this.ForeColor));
				}
			}
		}

		// Token: 0x170011F4 RID: 4596
		// (get) Token: 0x06004915 RID: 18709 RVA: 0x00133851 File Offset: 0x00131A51
		// (set) Token: 0x06004916 RID: 18710 RVA: 0x00133863 File Offset: 0x00131A63
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TreeViewFullRowSelectDescr")]
		public bool FullRowSelect
		{
			get
			{
				return this.treeViewState[512];
			}
			set
			{
				if (this.FullRowSelect != value)
				{
					this.treeViewState[512] = value;
					if (base.IsHandleCreated)
					{
						base.UpdateStyles();
					}
				}
			}
		}

		// Token: 0x170011F5 RID: 4597
		// (get) Token: 0x06004917 RID: 18711 RVA: 0x0013388D File Offset: 0x00131A8D
		// (set) Token: 0x06004918 RID: 18712 RVA: 0x0013389B File Offset: 0x00131A9B
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("TreeViewHideSelectionDescr")]
		public bool HideSelection
		{
			get
			{
				return this.treeViewState[1];
			}
			set
			{
				if (this.HideSelection != value)
				{
					this.treeViewState[1] = value;
					if (base.IsHandleCreated)
					{
						base.UpdateStyles();
					}
				}
			}
		}

		// Token: 0x170011F6 RID: 4598
		// (get) Token: 0x06004919 RID: 18713 RVA: 0x001338C1 File Offset: 0x00131AC1
		// (set) Token: 0x0600491A RID: 18714 RVA: 0x001338D3 File Offset: 0x00131AD3
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TreeViewHotTrackingDescr")]
		public bool HotTracking
		{
			get
			{
				return this.treeViewState[256];
			}
			set
			{
				if (this.HotTracking != value)
				{
					this.treeViewState[256] = value;
					if (base.IsHandleCreated)
					{
						base.UpdateStyles();
					}
				}
			}
		}

		// Token: 0x170011F7 RID: 4599
		// (get) Token: 0x0600491B RID: 18715 RVA: 0x00133900 File Offset: 0x00131B00
		// (set) Token: 0x0600491C RID: 18716 RVA: 0x00133958 File Offset: 0x00131B58
		[DefaultValue(-1)]
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[TypeConverter(typeof(NoneExcludedImageIndexConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SRDescription("TreeViewImageIndexDescr")]
		[RelatedImageList("ImageList")]
		public int ImageIndex
		{
			get
			{
				if (this.imageList == null)
				{
					return -1;
				}
				if (this.ImageIndexer.Index >= this.imageList.Images.Count)
				{
					return Math.Max(0, this.imageList.Images.Count - 1);
				}
				return this.ImageIndexer.Index;
			}
			set
			{
				if (value == -1)
				{
					value = 0;
				}
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("ImageIndex", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"ImageIndex",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.ImageIndexer.Index != value)
				{
					this.ImageIndexer.Index = value;
					if (base.IsHandleCreated)
					{
						base.RecreateHandle();
					}
				}
			}
		}

		// Token: 0x170011F8 RID: 4600
		// (get) Token: 0x0600491D RID: 18717 RVA: 0x001339DB File Offset: 0x00131BDB
		// (set) Token: 0x0600491E RID: 18718 RVA: 0x001339E8 File Offset: 0x00131BE8
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[TypeConverter(typeof(ImageKeyConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("TreeViewImageKeyDescr")]
		[RelatedImageList("ImageList")]
		public string ImageKey
		{
			get
			{
				return this.ImageIndexer.Key;
			}
			set
			{
				if (this.ImageIndexer.Key != value)
				{
					this.ImageIndexer.Key = value;
					if (string.IsNullOrEmpty(value) || value.Equals(SR.GetString("toStringNone")))
					{
						this.ImageIndex = ((this.ImageList != null) ? 0 : -1);
					}
					if (base.IsHandleCreated)
					{
						base.RecreateHandle();
					}
				}
			}
		}

		// Token: 0x170011F9 RID: 4601
		// (get) Token: 0x0600491F RID: 18719 RVA: 0x00133A4E File Offset: 0x00131C4E
		// (set) Token: 0x06004920 RID: 18720 RVA: 0x00133A58 File Offset: 0x00131C58
		[SRCategory("CatBehavior")]
		[DefaultValue(null)]
		[SRDescription("TreeViewImageListDescr")]
		[RefreshProperties(RefreshProperties.Repaint)]
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
					this.DetachImageListHandlers();
					this.imageList = value;
					this.AttachImageListHandlers();
					if (base.IsHandleCreated)
					{
						base.SendMessage(4361, 0, (value == null) ? IntPtr.Zero : value.Handle);
						if (this.StateImageList != null && this.StateImageList.Images.Count > 0)
						{
							this.SetStateImageList(this.internalStateImageList.Handle);
						}
					}
					this.UpdateCheckedState(this.root, true);
				}
			}
		}

		// Token: 0x06004921 RID: 18721 RVA: 0x00133AE0 File Offset: 0x00131CE0
		private void AttachImageListHandlers()
		{
			if (this.imageList != null)
			{
				this.imageList.RecreateHandle += this.ImageListRecreateHandle;
				this.imageList.Disposed += this.DetachImageList;
				this.imageList.ChangeHandle += this.ImageListChangedHandle;
			}
		}

		// Token: 0x06004922 RID: 18722 RVA: 0x00133B3C File Offset: 0x00131D3C
		private void DetachImageListHandlers()
		{
			if (this.imageList != null)
			{
				this.imageList.RecreateHandle -= this.ImageListRecreateHandle;
				this.imageList.Disposed -= this.DetachImageList;
				this.imageList.ChangeHandle -= this.ImageListChangedHandle;
			}
		}

		// Token: 0x06004923 RID: 18723 RVA: 0x00133B98 File Offset: 0x00131D98
		private void AttachStateImageListHandlers()
		{
			if (this.stateImageList != null)
			{
				this.stateImageList.RecreateHandle += this.StateImageListRecreateHandle;
				this.stateImageList.Disposed += this.DetachStateImageList;
				this.stateImageList.ChangeHandle += this.StateImageListChangedHandle;
			}
		}

		// Token: 0x06004924 RID: 18724 RVA: 0x00133BF4 File Offset: 0x00131DF4
		private void DetachStateImageListHandlers()
		{
			if (this.stateImageList != null)
			{
				this.stateImageList.RecreateHandle -= this.StateImageListRecreateHandle;
				this.stateImageList.Disposed -= this.DetachStateImageList;
				this.stateImageList.ChangeHandle -= this.StateImageListChangedHandle;
			}
		}

		// Token: 0x170011FA RID: 4602
		// (get) Token: 0x06004925 RID: 18725 RVA: 0x00133C4E File Offset: 0x00131E4E
		// (set) Token: 0x06004926 RID: 18726 RVA: 0x00133C58 File Offset: 0x00131E58
		[SRCategory("CatBehavior")]
		[DefaultValue(null)]
		[SRDescription("TreeViewStateImageListDescr")]
		public ImageList StateImageList
		{
			get
			{
				return this.stateImageList;
			}
			set
			{
				if (value != this.stateImageList)
				{
					this.DetachStateImageListHandlers();
					this.stateImageList = value;
					this.AttachStateImageListHandlers();
					if (base.IsHandleCreated)
					{
						this.UpdateNativeStateImageList();
						this.UpdateCheckedState(this.root, true);
						if ((value == null || this.stateImageList.Images.Count == 0) && this.CheckBoxes)
						{
							base.RecreateHandle();
							return;
						}
						this.RefreshNodes();
					}
				}
			}
		}

		// Token: 0x170011FB RID: 4603
		// (get) Token: 0x06004927 RID: 18727 RVA: 0x00133CC6 File Offset: 0x00131EC6
		// (set) Token: 0x06004928 RID: 18728 RVA: 0x00133CF8 File Offset: 0x00131EF8
		[Localizable(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewIndentDescr")]
		public int Indent
		{
			get
			{
				if (this.indent != -1)
				{
					return this.indent;
				}
				if (base.IsHandleCreated)
				{
					return (int)((long)base.SendMessage(4358, 0, 0));
				}
				return 19;
			}
			set
			{
				if (this.indent != value)
				{
					if (value < 0)
					{
						throw new ArgumentOutOfRangeException("Indent", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"Indent",
							value.ToString(CultureInfo.CurrentCulture),
							0.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (value > 32000)
					{
						throw new ArgumentOutOfRangeException("Indent", SR.GetString("InvalidHighBoundArgumentEx", new object[]
						{
							"Indent",
							value.ToString(CultureInfo.CurrentCulture),
							32000.ToString(CultureInfo.CurrentCulture)
						}));
					}
					this.indent = value;
					if (base.IsHandleCreated)
					{
						base.SendMessage(4359, value, 0);
						this.indent = (int)((long)base.SendMessage(4358, 0, 0));
					}
				}
			}
		}

		// Token: 0x170011FC RID: 4604
		// (get) Token: 0x06004929 RID: 18729 RVA: 0x00133DE0 File Offset: 0x00131FE0
		// (set) Token: 0x0600492A RID: 18730 RVA: 0x00133E44 File Offset: 0x00132044
		[SRCategory("CatAppearance")]
		[SRDescription("TreeViewItemHeightDescr")]
		public int ItemHeight
		{
			get
			{
				if (this.itemHeight != -1)
				{
					return this.itemHeight;
				}
				if (base.IsHandleCreated)
				{
					return (int)((long)base.SendMessage(4380, 0, 0));
				}
				if (this.CheckBoxes && this.DrawMode == TreeViewDrawMode.OwnerDrawAll)
				{
					return Math.Max(16, base.FontHeight + 3);
				}
				return base.FontHeight + 3;
			}
			set
			{
				if (this.itemHeight != value)
				{
					if (value < 1)
					{
						throw new ArgumentOutOfRangeException("ItemHeight", SR.GetString("InvalidLowBoundArgumentEx", new object[]
						{
							"ItemHeight",
							value.ToString(CultureInfo.CurrentCulture),
							1.ToString(CultureInfo.CurrentCulture)
						}));
					}
					if (value >= 32767)
					{
						throw new ArgumentOutOfRangeException("ItemHeight", SR.GetString("InvalidHighBoundArgument", new object[]
						{
							"ItemHeight",
							value.ToString(CultureInfo.CurrentCulture),
							short.MaxValue.ToString(CultureInfo.CurrentCulture)
						}));
					}
					this.itemHeight = value;
					if (base.IsHandleCreated)
					{
						if (this.itemHeight % 2 != 0)
						{
							this.setOddHeight = true;
							try
							{
								base.RecreateHandle();
							}
							finally
							{
								this.setOddHeight = false;
							}
						}
						base.SendMessage(4379, value, 0);
						this.itemHeight = (int)((long)base.SendMessage(4380, 0, 0));
					}
				}
			}
		}

		// Token: 0x170011FD RID: 4605
		// (get) Token: 0x0600492B RID: 18731 RVA: 0x00133F5C File Offset: 0x0013215C
		// (set) Token: 0x0600492C RID: 18732 RVA: 0x00133F6A File Offset: 0x0013216A
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TreeViewLabelEditDescr")]
		public bool LabelEdit
		{
			get
			{
				return this.treeViewState[2];
			}
			set
			{
				if (this.LabelEdit != value)
				{
					this.treeViewState[2] = value;
					if (base.IsHandleCreated)
					{
						base.UpdateStyles();
					}
				}
			}
		}

		// Token: 0x170011FE RID: 4606
		// (get) Token: 0x0600492D RID: 18733 RVA: 0x00133F90 File Offset: 0x00132190
		// (set) Token: 0x0600492E RID: 18734 RVA: 0x00133FC6 File Offset: 0x001321C6
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewLineColorDescr")]
		[DefaultValue(typeof(Color), "Black")]
		public Color LineColor
		{
			get
			{
				if (base.IsHandleCreated)
				{
					int win32Color = (int)((long)base.SendMessage(4393, 0, 0));
					return ColorTranslator.FromWin32(win32Color);
				}
				return this.lineColor;
			}
			set
			{
				if (this.lineColor != value)
				{
					this.lineColor = value;
					if (base.IsHandleCreated)
					{
						base.SendMessage(4392, 0, ColorTranslator.ToWin32(this.lineColor));
					}
				}
			}
		}

		// Token: 0x170011FF RID: 4607
		// (get) Token: 0x0600492F RID: 18735 RVA: 0x00133FFD File Offset: 0x001321FD
		[SRCategory("CatBehavior")]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		[Localizable(true)]
		[SRDescription("TreeViewNodesDescr")]
		[MergableProperty(false)]
		public TreeNodeCollection Nodes
		{
			get
			{
				if (this.nodes == null)
				{
					this.nodes = new TreeNodeCollection(this.root);
				}
				return this.nodes;
			}
		}

		// Token: 0x17001200 RID: 4608
		// (get) Token: 0x06004930 RID: 18736 RVA: 0x0013401E File Offset: 0x0013221E
		// (set) Token: 0x06004931 RID: 18737 RVA: 0x00134028 File Offset: 0x00132228
		[SRCategory("CatBehavior")]
		[DefaultValue(TreeViewDrawMode.Normal)]
		[SRDescription("TreeViewDrawModeDescr")]
		public TreeViewDrawMode DrawMode
		{
			get
			{
				return this.drawMode;
			}
			set
			{
				if (!ClientUtils.IsEnumValid(value, (int)value, 0, 2))
				{
					throw new InvalidEnumArgumentException("value", (int)value, typeof(TreeViewDrawMode));
				}
				if (this.drawMode != value)
				{
					this.drawMode = value;
					base.Invalidate();
					if (this.DrawMode == TreeViewDrawMode.OwnerDrawAll)
					{
						base.SetStyle(ControlStyles.ResizeRedraw, true);
					}
				}
			}
		}

		// Token: 0x17001201 RID: 4609
		// (get) Token: 0x06004932 RID: 18738 RVA: 0x00134083 File Offset: 0x00132283
		// (set) Token: 0x06004933 RID: 18739 RVA: 0x0013408B File Offset: 0x0013228B
		[SRCategory("CatBehavior")]
		[DefaultValue("\\")]
		[SRDescription("TreeViewPathSeparatorDescr")]
		public string PathSeparator
		{
			get
			{
				return this.pathSeparator;
			}
			set
			{
				this.pathSeparator = value;
			}
		}

		// Token: 0x17001202 RID: 4610
		// (get) Token: 0x06004934 RID: 18740 RVA: 0x00013656 File Offset: 0x00011856
		// (set) Token: 0x06004935 RID: 18741 RVA: 0x0001365E File Offset: 0x0001185E
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

		// Token: 0x140003A2 RID: 930
		// (add) Token: 0x06004936 RID: 18742 RVA: 0x00013667 File Offset: 0x00011867
		// (remove) Token: 0x06004937 RID: 18743 RVA: 0x00013670 File Offset: 0x00011870
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

		// Token: 0x17001203 RID: 4611
		// (get) Token: 0x06004938 RID: 18744 RVA: 0x00134094 File Offset: 0x00132294
		// (set) Token: 0x06004939 RID: 18745 RVA: 0x0013409C File Offset: 0x0013229C
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

		// Token: 0x17001204 RID: 4612
		// (get) Token: 0x0600493A RID: 18746 RVA: 0x001340F0 File Offset: 0x001322F0
		// (set) Token: 0x0600493B RID: 18747 RVA: 0x001340FE File Offset: 0x001322FE
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("TreeViewScrollableDescr")]
		public bool Scrollable
		{
			get
			{
				return this.treeViewState[4];
			}
			set
			{
				if (this.Scrollable != value)
				{
					this.treeViewState[4] = value;
					base.RecreateHandle();
				}
			}
		}

		// Token: 0x17001205 RID: 4613
		// (get) Token: 0x0600493C RID: 18748 RVA: 0x0013411C File Offset: 0x0013231C
		// (set) Token: 0x0600493D RID: 18749 RVA: 0x00134174 File Offset: 0x00132374
		[DefaultValue(-1)]
		[SRCategory("CatBehavior")]
		[TypeConverter(typeof(NoneExcludedImageIndexConverter))]
		[Localizable(true)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[SRDescription("TreeViewSelectedImageIndexDescr")]
		[RelatedImageList("ImageList")]
		public int SelectedImageIndex
		{
			get
			{
				if (this.imageList == null)
				{
					return -1;
				}
				if (this.SelectedImageIndexer.Index >= this.imageList.Images.Count)
				{
					return Math.Max(0, this.imageList.Images.Count - 1);
				}
				return this.SelectedImageIndexer.Index;
			}
			set
			{
				if (value == -1)
				{
					value = 0;
				}
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("SelectedImageIndex", SR.GetString("InvalidLowBoundArgumentEx", new object[]
					{
						"SelectedImageIndex",
						value.ToString(CultureInfo.CurrentCulture),
						0.ToString(CultureInfo.CurrentCulture)
					}));
				}
				if (this.SelectedImageIndexer.Index != value)
				{
					this.SelectedImageIndexer.Index = value;
					if (base.IsHandleCreated)
					{
						base.RecreateHandle();
					}
				}
			}
		}

		// Token: 0x17001206 RID: 4614
		// (get) Token: 0x0600493E RID: 18750 RVA: 0x001341F7 File Offset: 0x001323F7
		// (set) Token: 0x0600493F RID: 18751 RVA: 0x00134204 File Offset: 0x00132404
		[SRCategory("CatBehavior")]
		[Localizable(true)]
		[TypeConverter(typeof(ImageKeyConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[DefaultValue("")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[SRDescription("TreeViewSelectedImageKeyDescr")]
		[RelatedImageList("ImageList")]
		public string SelectedImageKey
		{
			get
			{
				return this.SelectedImageIndexer.Key;
			}
			set
			{
				if (this.SelectedImageIndexer.Key != value)
				{
					this.SelectedImageIndexer.Key = value;
					if (string.IsNullOrEmpty(value) || value.Equals(SR.GetString("toStringNone")))
					{
						this.SelectedImageIndex = ((this.ImageList != null) ? 0 : -1);
					}
					if (base.IsHandleCreated)
					{
						base.RecreateHandle();
					}
				}
			}
		}

		// Token: 0x17001207 RID: 4615
		// (get) Token: 0x06004940 RID: 18752 RVA: 0x0013426C File Offset: 0x0013246C
		// (set) Token: 0x06004941 RID: 18753 RVA: 0x001342C8 File Offset: 0x001324C8
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TreeViewSelectedNodeDescr")]
		public TreeNode SelectedNode
		{
			get
			{
				if (base.IsHandleCreated)
				{
					IntPtr intPtr = base.SendMessage(4362, 9, 0);
					if (intPtr == IntPtr.Zero)
					{
						return null;
					}
					return this.NodeFromHandle(intPtr);
				}
				else
				{
					if (this.selectedNode != null && this.selectedNode.TreeView == this)
					{
						return this.selectedNode;
					}
					return null;
				}
			}
			set
			{
				if (base.IsHandleCreated && (value == null || value.TreeView == this))
				{
					IntPtr lparam = (value == null) ? IntPtr.Zero : value.Handle;
					base.SendMessage(4363, 9, lparam);
					this.selectedNode = null;
					return;
				}
				this.selectedNode = value;
			}
		}

		// Token: 0x17001208 RID: 4616
		// (get) Token: 0x06004942 RID: 18754 RVA: 0x00134318 File Offset: 0x00132518
		// (set) Token: 0x06004943 RID: 18755 RVA: 0x00134327 File Offset: 0x00132527
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("TreeViewShowLinesDescr")]
		public bool ShowLines
		{
			get
			{
				return this.treeViewState[16];
			}
			set
			{
				if (this.ShowLines != value)
				{
					this.treeViewState[16] = value;
					if (base.IsHandleCreated)
					{
						base.UpdateStyles();
					}
				}
			}
		}

		// Token: 0x17001209 RID: 4617
		// (get) Token: 0x06004944 RID: 18756 RVA: 0x0013434E File Offset: 0x0013254E
		// (set) Token: 0x06004945 RID: 18757 RVA: 0x00134360 File Offset: 0x00132560
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TreeViewShowShowNodeToolTipsDescr")]
		public bool ShowNodeToolTips
		{
			get
			{
				return this.treeViewState[1024];
			}
			set
			{
				if (this.ShowNodeToolTips != value)
				{
					this.treeViewState[1024] = value;
					if (this.ShowNodeToolTips)
					{
						base.RecreateHandle();
					}
				}
			}
		}

		// Token: 0x1700120A RID: 4618
		// (get) Token: 0x06004946 RID: 18758 RVA: 0x0013438A File Offset: 0x0013258A
		// (set) Token: 0x06004947 RID: 18759 RVA: 0x00134399 File Offset: 0x00132599
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("TreeViewShowPlusMinusDescr")]
		public bool ShowPlusMinus
		{
			get
			{
				return this.treeViewState[32];
			}
			set
			{
				if (this.ShowPlusMinus != value)
				{
					this.treeViewState[32] = value;
					if (base.IsHandleCreated)
					{
						base.UpdateStyles();
					}
				}
			}
		}

		// Token: 0x1700120B RID: 4619
		// (get) Token: 0x06004948 RID: 18760 RVA: 0x001343C0 File Offset: 0x001325C0
		// (set) Token: 0x06004949 RID: 18761 RVA: 0x001343CF File Offset: 0x001325CF
		[SRCategory("CatBehavior")]
		[DefaultValue(true)]
		[SRDescription("TreeViewShowRootLinesDescr")]
		public bool ShowRootLines
		{
			get
			{
				return this.treeViewState[64];
			}
			set
			{
				if (this.ShowRootLines != value)
				{
					this.treeViewState[64] = value;
					if (base.IsHandleCreated)
					{
						base.UpdateStyles();
					}
				}
			}
		}

		// Token: 0x1700120C RID: 4620
		// (get) Token: 0x0600494A RID: 18762 RVA: 0x001343F6 File Offset: 0x001325F6
		// (set) Token: 0x0600494B RID: 18763 RVA: 0x00134408 File Offset: 0x00132608
		[SRCategory("CatBehavior")]
		[DefaultValue(false)]
		[SRDescription("TreeViewSortedDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Sorted
		{
			get
			{
				return this.treeViewState[128];
			}
			set
			{
				if (this.Sorted != value)
				{
					this.treeViewState[128] = value;
					if (this.Sorted && this.TreeViewNodeSorter == null && this.Nodes.Count >= 1)
					{
						this.RefreshNodes();
					}
				}
			}
		}

		// Token: 0x1700120D RID: 4621
		// (get) Token: 0x0600494C RID: 18764 RVA: 0x00134448 File Offset: 0x00132648
		// (set) Token: 0x0600494D RID: 18765 RVA: 0x00134450 File Offset: 0x00132650
		[SRCategory("CatBehavior")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TreeViewNodeSorterDescr")]
		public IComparer TreeViewNodeSorter
		{
			get
			{
				return this.treeViewNodeSorter;
			}
			set
			{
				if (this.treeViewNodeSorter != value)
				{
					this.treeViewNodeSorter = value;
					if (value != null)
					{
						this.Sort();
					}
				}
			}
		}

		// Token: 0x1700120E RID: 4622
		// (get) Token: 0x0600494E RID: 18766 RVA: 0x00013A28 File Offset: 0x00011C28
		// (set) Token: 0x0600494F RID: 18767 RVA: 0x00024185 File Offset: 0x00022385
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

		// Token: 0x140003A3 RID: 931
		// (add) Token: 0x06004950 RID: 18768 RVA: 0x00046771 File Offset: 0x00044971
		// (remove) Token: 0x06004951 RID: 18769 RVA: 0x0004677A File Offset: 0x0004497A
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

		// Token: 0x1700120F RID: 4623
		// (get) Token: 0x06004952 RID: 18770 RVA: 0x0013446C File Offset: 0x0013266C
		// (set) Token: 0x06004953 RID: 18771 RVA: 0x001344AC File Offset: 0x001326AC
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TreeViewTopNodeDescr")]
		public TreeNode TopNode
		{
			get
			{
				if (!base.IsHandleCreated)
				{
					return this.topNode;
				}
				IntPtr intPtr = base.SendMessage(4362, 5, 0);
				if (!(intPtr == IntPtr.Zero))
				{
					return this.NodeFromHandle(intPtr);
				}
				return null;
			}
			set
			{
				if (base.IsHandleCreated && (value == null || value.TreeView == this))
				{
					IntPtr lparam = (value == null) ? IntPtr.Zero : value.Handle;
					base.SendMessage(4363, 5, lparam);
					this.topNode = null;
					return;
				}
				this.topNode = value;
			}
		}

		// Token: 0x17001210 RID: 4624
		// (get) Token: 0x06004954 RID: 18772 RVA: 0x001344FB File Offset: 0x001326FB
		[SRCategory("CatAppearance")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("TreeViewVisibleCountDescr")]
		public int VisibleCount
		{
			get
			{
				if (base.IsHandleCreated)
				{
					return (int)((long)base.SendMessage(4368, 0, 0));
				}
				return 0;
			}
		}

		// Token: 0x140003A4 RID: 932
		// (add) Token: 0x06004955 RID: 18773 RVA: 0x0013451A File Offset: 0x0013271A
		// (remove) Token: 0x06004956 RID: 18774 RVA: 0x00134533 File Offset: 0x00132733
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewBeforeEditDescr")]
		public event NodeLabelEditEventHandler BeforeLabelEdit
		{
			add
			{
				this.onBeforeLabelEdit = (NodeLabelEditEventHandler)Delegate.Combine(this.onBeforeLabelEdit, value);
			}
			remove
			{
				this.onBeforeLabelEdit = (NodeLabelEditEventHandler)Delegate.Remove(this.onBeforeLabelEdit, value);
			}
		}

		// Token: 0x140003A5 RID: 933
		// (add) Token: 0x06004957 RID: 18775 RVA: 0x0013454C File Offset: 0x0013274C
		// (remove) Token: 0x06004958 RID: 18776 RVA: 0x00134565 File Offset: 0x00132765
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewAfterEditDescr")]
		public event NodeLabelEditEventHandler AfterLabelEdit
		{
			add
			{
				this.onAfterLabelEdit = (NodeLabelEditEventHandler)Delegate.Combine(this.onAfterLabelEdit, value);
			}
			remove
			{
				this.onAfterLabelEdit = (NodeLabelEditEventHandler)Delegate.Remove(this.onAfterLabelEdit, value);
			}
		}

		// Token: 0x140003A6 RID: 934
		// (add) Token: 0x06004959 RID: 18777 RVA: 0x0013457E File Offset: 0x0013277E
		// (remove) Token: 0x0600495A RID: 18778 RVA: 0x00134597 File Offset: 0x00132797
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewBeforeCheckDescr")]
		public event TreeViewCancelEventHandler BeforeCheck
		{
			add
			{
				this.onBeforeCheck = (TreeViewCancelEventHandler)Delegate.Combine(this.onBeforeCheck, value);
			}
			remove
			{
				this.onBeforeCheck = (TreeViewCancelEventHandler)Delegate.Remove(this.onBeforeCheck, value);
			}
		}

		// Token: 0x140003A7 RID: 935
		// (add) Token: 0x0600495B RID: 18779 RVA: 0x001345B0 File Offset: 0x001327B0
		// (remove) Token: 0x0600495C RID: 18780 RVA: 0x001345C9 File Offset: 0x001327C9
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewAfterCheckDescr")]
		public event TreeViewEventHandler AfterCheck
		{
			add
			{
				this.onAfterCheck = (TreeViewEventHandler)Delegate.Combine(this.onAfterCheck, value);
			}
			remove
			{
				this.onAfterCheck = (TreeViewEventHandler)Delegate.Remove(this.onAfterCheck, value);
			}
		}

		// Token: 0x140003A8 RID: 936
		// (add) Token: 0x0600495D RID: 18781 RVA: 0x001345E2 File Offset: 0x001327E2
		// (remove) Token: 0x0600495E RID: 18782 RVA: 0x001345FB File Offset: 0x001327FB
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewBeforeCollapseDescr")]
		public event TreeViewCancelEventHandler BeforeCollapse
		{
			add
			{
				this.onBeforeCollapse = (TreeViewCancelEventHandler)Delegate.Combine(this.onBeforeCollapse, value);
			}
			remove
			{
				this.onBeforeCollapse = (TreeViewCancelEventHandler)Delegate.Remove(this.onBeforeCollapse, value);
			}
		}

		// Token: 0x140003A9 RID: 937
		// (add) Token: 0x0600495F RID: 18783 RVA: 0x00134614 File Offset: 0x00132814
		// (remove) Token: 0x06004960 RID: 18784 RVA: 0x0013462D File Offset: 0x0013282D
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewAfterCollapseDescr")]
		public event TreeViewEventHandler AfterCollapse
		{
			add
			{
				this.onAfterCollapse = (TreeViewEventHandler)Delegate.Combine(this.onAfterCollapse, value);
			}
			remove
			{
				this.onAfterCollapse = (TreeViewEventHandler)Delegate.Remove(this.onAfterCollapse, value);
			}
		}

		// Token: 0x140003AA RID: 938
		// (add) Token: 0x06004961 RID: 18785 RVA: 0x00134646 File Offset: 0x00132846
		// (remove) Token: 0x06004962 RID: 18786 RVA: 0x0013465F File Offset: 0x0013285F
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewBeforeExpandDescr")]
		public event TreeViewCancelEventHandler BeforeExpand
		{
			add
			{
				this.onBeforeExpand = (TreeViewCancelEventHandler)Delegate.Combine(this.onBeforeExpand, value);
			}
			remove
			{
				this.onBeforeExpand = (TreeViewCancelEventHandler)Delegate.Remove(this.onBeforeExpand, value);
			}
		}

		// Token: 0x140003AB RID: 939
		// (add) Token: 0x06004963 RID: 18787 RVA: 0x00134678 File Offset: 0x00132878
		// (remove) Token: 0x06004964 RID: 18788 RVA: 0x00134691 File Offset: 0x00132891
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewAfterExpandDescr")]
		public event TreeViewEventHandler AfterExpand
		{
			add
			{
				this.onAfterExpand = (TreeViewEventHandler)Delegate.Combine(this.onAfterExpand, value);
			}
			remove
			{
				this.onAfterExpand = (TreeViewEventHandler)Delegate.Remove(this.onAfterExpand, value);
			}
		}

		// Token: 0x140003AC RID: 940
		// (add) Token: 0x06004965 RID: 18789 RVA: 0x001346AA File Offset: 0x001328AA
		// (remove) Token: 0x06004966 RID: 18790 RVA: 0x001346C3 File Offset: 0x001328C3
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewDrawNodeEventDescr")]
		public event DrawTreeNodeEventHandler DrawNode
		{
			add
			{
				this.onDrawNode = (DrawTreeNodeEventHandler)Delegate.Combine(this.onDrawNode, value);
			}
			remove
			{
				this.onDrawNode = (DrawTreeNodeEventHandler)Delegate.Remove(this.onDrawNode, value);
			}
		}

		// Token: 0x140003AD RID: 941
		// (add) Token: 0x06004967 RID: 18791 RVA: 0x001346DC File Offset: 0x001328DC
		// (remove) Token: 0x06004968 RID: 18792 RVA: 0x001346F5 File Offset: 0x001328F5
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

		// Token: 0x140003AE RID: 942
		// (add) Token: 0x06004969 RID: 18793 RVA: 0x0013470E File Offset: 0x0013290E
		// (remove) Token: 0x0600496A RID: 18794 RVA: 0x00134727 File Offset: 0x00132927
		[SRCategory("CatAction")]
		[SRDescription("TreeViewNodeMouseHoverDescr")]
		public event TreeNodeMouseHoverEventHandler NodeMouseHover
		{
			add
			{
				this.onNodeMouseHover = (TreeNodeMouseHoverEventHandler)Delegate.Combine(this.onNodeMouseHover, value);
			}
			remove
			{
				this.onNodeMouseHover = (TreeNodeMouseHoverEventHandler)Delegate.Remove(this.onNodeMouseHover, value);
			}
		}

		// Token: 0x140003AF RID: 943
		// (add) Token: 0x0600496B RID: 18795 RVA: 0x00134740 File Offset: 0x00132940
		// (remove) Token: 0x0600496C RID: 18796 RVA: 0x00134759 File Offset: 0x00132959
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewBeforeSelectDescr")]
		public event TreeViewCancelEventHandler BeforeSelect
		{
			add
			{
				this.onBeforeSelect = (TreeViewCancelEventHandler)Delegate.Combine(this.onBeforeSelect, value);
			}
			remove
			{
				this.onBeforeSelect = (TreeViewCancelEventHandler)Delegate.Remove(this.onBeforeSelect, value);
			}
		}

		// Token: 0x140003B0 RID: 944
		// (add) Token: 0x0600496D RID: 18797 RVA: 0x00134772 File Offset: 0x00132972
		// (remove) Token: 0x0600496E RID: 18798 RVA: 0x0013478B File Offset: 0x0013298B
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewAfterSelectDescr")]
		public event TreeViewEventHandler AfterSelect
		{
			add
			{
				this.onAfterSelect = (TreeViewEventHandler)Delegate.Combine(this.onAfterSelect, value);
			}
			remove
			{
				this.onAfterSelect = (TreeViewEventHandler)Delegate.Remove(this.onAfterSelect, value);
			}
		}

		// Token: 0x140003B1 RID: 945
		// (add) Token: 0x0600496F RID: 18799 RVA: 0x00013F87 File Offset: 0x00012187
		// (remove) Token: 0x06004970 RID: 18800 RVA: 0x00013F90 File Offset: 0x00012190
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

		// Token: 0x140003B2 RID: 946
		// (add) Token: 0x06004971 RID: 18801 RVA: 0x001347A4 File Offset: 0x001329A4
		// (remove) Token: 0x06004972 RID: 18802 RVA: 0x001347BD File Offset: 0x001329BD
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewNodeMouseClickDescr")]
		public event TreeNodeMouseClickEventHandler NodeMouseClick
		{
			add
			{
				this.onNodeMouseClick = (TreeNodeMouseClickEventHandler)Delegate.Combine(this.onNodeMouseClick, value);
			}
			remove
			{
				this.onNodeMouseClick = (TreeNodeMouseClickEventHandler)Delegate.Remove(this.onNodeMouseClick, value);
			}
		}

		// Token: 0x140003B3 RID: 947
		// (add) Token: 0x06004973 RID: 18803 RVA: 0x001347D6 File Offset: 0x001329D6
		// (remove) Token: 0x06004974 RID: 18804 RVA: 0x001347EF File Offset: 0x001329EF
		[SRCategory("CatBehavior")]
		[SRDescription("TreeViewNodeMouseDoubleClickDescr")]
		public event TreeNodeMouseClickEventHandler NodeMouseDoubleClick
		{
			add
			{
				this.onNodeMouseDoubleClick = (TreeNodeMouseClickEventHandler)Delegate.Combine(this.onNodeMouseDoubleClick, value);
			}
			remove
			{
				this.onNodeMouseDoubleClick = (TreeNodeMouseClickEventHandler)Delegate.Remove(this.onNodeMouseDoubleClick, value);
			}
		}

		// Token: 0x140003B4 RID: 948
		// (add) Token: 0x06004975 RID: 18805 RVA: 0x00134808 File Offset: 0x00132A08
		// (remove) Token: 0x06004976 RID: 18806 RVA: 0x00134821 File Offset: 0x00132A21
		[SRCategory("CatPropertyChanged")]
		[SRDescription("ControlOnRightToLeftLayoutChangedDescr")]
		public event EventHandler RightToLeftLayoutChanged
		{
			add
			{
				this.onRightToLeftLayoutChanged = (EventHandler)Delegate.Combine(this.onRightToLeftLayoutChanged, value);
			}
			remove
			{
				this.onRightToLeftLayoutChanged = (EventHandler)Delegate.Remove(this.onRightToLeftLayoutChanged, value);
			}
		}

		// Token: 0x06004977 RID: 18807 RVA: 0x00104241 File Offset: 0x00102441
		public void BeginUpdate()
		{
			base.BeginUpdateInternal();
		}

		// Token: 0x06004978 RID: 18808 RVA: 0x0013483A File Offset: 0x00132A3A
		public void CollapseAll()
		{
			this.root.Collapse();
		}

		// Token: 0x06004979 RID: 18809 RVA: 0x00134848 File Offset: 0x00132A48
		protected override void CreateHandle()
		{
			if (!base.RecreatingHandle)
			{
				IntPtr userCookie = UnsafeNativeMethods.ThemingScope.Activate();
				try
				{
					SafeNativeMethods.InitCommonControlsEx(new NativeMethods.INITCOMMONCONTROLSEX
					{
						dwICC = 2
					});
				}
				finally
				{
					UnsafeNativeMethods.ThemingScope.Deactivate(userCookie);
				}
			}
			base.CreateHandle();
		}

		// Token: 0x0600497A RID: 18810 RVA: 0x00134898 File Offset: 0x00132A98
		private void DetachImageList(object sender, EventArgs e)
		{
			this.ImageList = null;
		}

		// Token: 0x0600497B RID: 18811 RVA: 0x001348A1 File Offset: 0x00132AA1
		private void DetachStateImageList(object sender, EventArgs e)
		{
			this.internalStateImageList = null;
			this.StateImageList = null;
		}

		// Token: 0x0600497C RID: 18812 RVA: 0x001348B4 File Offset: 0x00132AB4
		protected override void Dispose(bool disposing)
		{
			if (disposing)
			{
				foreach (object obj in this.Nodes)
				{
					TreeNode treeNode = (TreeNode)obj;
					treeNode.ContextMenu = null;
				}
				lock (this)
				{
					this.DetachImageListHandlers();
					this.imageList = null;
					this.DetachStateImageListHandlers();
					this.stateImageList = null;
				}
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600497D RID: 18813 RVA: 0x00109FDC File Offset: 0x001081DC
		public void EndUpdate()
		{
			base.EndUpdateInternal();
		}

		// Token: 0x0600497E RID: 18814 RVA: 0x00134958 File Offset: 0x00132B58
		public void ExpandAll()
		{
			this.root.ExpandAll();
		}

		// Token: 0x0600497F RID: 18815 RVA: 0x00134968 File Offset: 0x00132B68
		internal void ForceScrollbarUpdate(bool delayed)
		{
			if (!base.IsUpdating() && base.IsHandleCreated)
			{
				base.SendMessage(11, 0, 0);
				if (delayed)
				{
					UnsafeNativeMethods.PostMessage(new HandleRef(this, base.Handle), 11, (IntPtr)1, IntPtr.Zero);
					return;
				}
				base.SendMessage(11, 1, 0);
			}
		}

		// Token: 0x06004980 RID: 18816 RVA: 0x001349C0 File Offset: 0x00132BC0
		internal void SetToolTip(ToolTip toolTip, string toolTipText)
		{
			if (toolTip != null)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(toolTip, toolTip.Handle), 1048, 0, SystemInformation.MaxWindowTrackSize.Width);
				UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4376, new HandleRef(toolTip, toolTip.Handle), 0);
				this.controlToolTipText = toolTipText;
			}
		}

		// Token: 0x06004981 RID: 18817 RVA: 0x00134A20 File Offset: 0x00132C20
		public TreeViewHitTestInfo HitTest(Point pt)
		{
			return this.HitTest(pt.X, pt.Y);
		}

		// Token: 0x06004982 RID: 18818 RVA: 0x00134A38 File Offset: 0x00132C38
		public TreeViewHitTestInfo HitTest(int x, int y)
		{
			NativeMethods.TV_HITTESTINFO tv_HITTESTINFO = new NativeMethods.TV_HITTESTINFO();
			tv_HITTESTINFO.pt_x = x;
			tv_HITTESTINFO.pt_y = y;
			IntPtr intPtr = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4369, 0, tv_HITTESTINFO);
			TreeNode hitNode = (intPtr == IntPtr.Zero) ? null : this.NodeFromHandle(intPtr);
			TreeViewHitTestLocations flags = (TreeViewHitTestLocations)tv_HITTESTINFO.flags;
			return new TreeViewHitTestInfo(hitNode, flags);
		}

		// Token: 0x06004983 RID: 18819 RVA: 0x00134A98 File Offset: 0x00132C98
		internal bool TreeViewBeforeCheck(TreeNode node, TreeViewAction actionTaken)
		{
			TreeViewCancelEventArgs treeViewCancelEventArgs = new TreeViewCancelEventArgs(node, false, actionTaken);
			this.OnBeforeCheck(treeViewCancelEventArgs);
			return treeViewCancelEventArgs.Cancel;
		}

		// Token: 0x06004984 RID: 18820 RVA: 0x00134ABB File Offset: 0x00132CBB
		internal void TreeViewAfterCheck(TreeNode node, TreeViewAction actionTaken)
		{
			this.OnAfterCheck(new TreeViewEventArgs(node, actionTaken));
		}

		// Token: 0x06004985 RID: 18821 RVA: 0x00134ACA File Offset: 0x00132CCA
		public int GetNodeCount(bool includeSubTrees)
		{
			return this.root.GetNodeCount(includeSubTrees);
		}

		// Token: 0x06004986 RID: 18822 RVA: 0x00134AD8 File Offset: 0x00132CD8
		public TreeNode GetNodeAt(Point pt)
		{
			return this.GetNodeAt(pt.X, pt.Y);
		}

		// Token: 0x06004987 RID: 18823 RVA: 0x00134AF0 File Offset: 0x00132CF0
		public TreeNode GetNodeAt(int x, int y)
		{
			NativeMethods.TV_HITTESTINFO tv_HITTESTINFO = new NativeMethods.TV_HITTESTINFO();
			tv_HITTESTINFO.pt_x = x;
			tv_HITTESTINFO.pt_y = y;
			IntPtr intPtr = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4369, 0, tv_HITTESTINFO);
			if (!(intPtr == IntPtr.Zero))
			{
				return this.NodeFromHandle(intPtr);
			}
			return null;
		}

		// Token: 0x06004988 RID: 18824 RVA: 0x00134B40 File Offset: 0x00132D40
		private void ImageListRecreateHandle(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				IntPtr lparam = (this.ImageList == null) ? IntPtr.Zero : this.ImageList.Handle;
				base.SendMessage(4361, 0, lparam);
			}
		}

		// Token: 0x06004989 RID: 18825 RVA: 0x00134B80 File Offset: 0x00132D80
		private void UpdateImagesRecursive(TreeNode node)
		{
			node.UpdateImage();
			foreach (object obj in node.Nodes)
			{
				TreeNode node2 = (TreeNode)obj;
				this.UpdateImagesRecursive(node2);
			}
		}

		// Token: 0x0600498A RID: 18826 RVA: 0x00134BE0 File Offset: 0x00132DE0
		private void ImageListChangedHandle(object sender, EventArgs e)
		{
			if (sender != null && sender == this.imageList && base.IsHandleCreated)
			{
				this.BeginUpdate();
				foreach (object obj in this.Nodes)
				{
					TreeNode node = (TreeNode)obj;
					this.UpdateImagesRecursive(node);
				}
				this.EndUpdate();
			}
		}

		// Token: 0x0600498B RID: 18827 RVA: 0x00134C5C File Offset: 0x00132E5C
		private void StateImageListRecreateHandle(object sender, EventArgs e)
		{
			if (base.IsHandleCreated)
			{
				IntPtr intPtr = IntPtr.Zero;
				if (this.internalStateImageList != null)
				{
					intPtr = this.internalStateImageList.Handle;
				}
				this.SetStateImageList(intPtr);
			}
		}

		// Token: 0x0600498C RID: 18828 RVA: 0x00134C94 File Offset: 0x00132E94
		private void StateImageListChangedHandle(object sender, EventArgs e)
		{
			if (sender != null && sender == this.stateImageList && base.IsHandleCreated)
			{
				if (this.stateImageList != null && this.stateImageList.Images.Count > 0)
				{
					Image[] array = new Image[this.stateImageList.Images.Count + 1];
					array[0] = this.stateImageList.Images[0];
					for (int i = 1; i <= this.stateImageList.Images.Count; i++)
					{
						array[i] = this.stateImageList.Images[i - 1];
					}
					if (this.internalStateImageList != null)
					{
						this.internalStateImageList.Images.Clear();
						this.internalStateImageList.Images.AddRange(array);
					}
					else
					{
						this.internalStateImageList = new ImageList();
						this.internalStateImageList.Images.AddRange(array);
					}
					if (this.internalStateImageList != null)
					{
						if (TreeView.ScaledStateImageSize != null)
						{
							this.internalStateImageList.ImageSize = TreeView.ScaledStateImageSize.Value;
						}
						this.SetStateImageList(this.internalStateImageList.Handle);
						return;
					}
				}
				else
				{
					this.UpdateCheckedState(this.root, true);
				}
			}
		}

		// Token: 0x0600498D RID: 18829 RVA: 0x00134DD4 File Offset: 0x00132FD4
		protected override bool IsInputKey(Keys keyData)
		{
			if (this.editNode != null && (keyData & Keys.Alt) == Keys.None)
			{
				Keys keys = keyData & Keys.KeyCode;
				if (keys == Keys.Return || keys == Keys.Escape || keys - Keys.Prior <= 3)
				{
					return true;
				}
			}
			return base.IsInputKey(keyData);
		}

		// Token: 0x0600498E RID: 18830 RVA: 0x00134E14 File Offset: 0x00133014
		internal TreeNode NodeFromHandle(IntPtr handle)
		{
			return (TreeNode)this.nodeTable[handle];
		}

		// Token: 0x0600498F RID: 18831 RVA: 0x00134E39 File Offset: 0x00133039
		protected virtual void OnDrawNode(DrawTreeNodeEventArgs e)
		{
			if (this.onDrawNode != null)
			{
				this.onDrawNode(this, e);
			}
		}

		// Token: 0x06004990 RID: 18832 RVA: 0x00134E50 File Offset: 0x00133050
		protected override void OnHandleCreated(EventArgs e)
		{
			TreeNode treeNode = this.selectedNode;
			this.selectedNode = null;
			base.OnHandleCreated(e);
			int num = (int)((long)base.SendMessage(8200, 0, 0));
			if (num < 5)
			{
				base.SendMessage(8199, 5, 0);
			}
			if (this.CheckBoxes)
			{
				int num2 = (int)UnsafeNativeMethods.GetWindowLong(new HandleRef(this, base.Handle), -16);
				num2 |= 256;
				UnsafeNativeMethods.SetWindowLong(new HandleRef(this, base.Handle), -16, new HandleRef(null, (IntPtr)num2));
			}
			if (this.ShowNodeToolTips && !base.DesignMode)
			{
				int num3 = (int)UnsafeNativeMethods.GetWindowLong(new HandleRef(this, base.Handle), -16);
				num3 |= 2048;
				UnsafeNativeMethods.SetWindowLong(new HandleRef(this, base.Handle), -16, new HandleRef(null, (IntPtr)num3));
			}
			Color color = this.BackColor;
			if (color != SystemColors.Window)
			{
				base.SendMessage(4381, 0, ColorTranslator.ToWin32(color));
			}
			color = this.ForeColor;
			if (color != SystemColors.WindowText)
			{
				base.SendMessage(4382, 0, ColorTranslator.ToWin32(color));
			}
			if (this.lineColor != Color.Empty)
			{
				base.SendMessage(4392, 0, ColorTranslator.ToWin32(this.lineColor));
			}
			if (this.imageList != null)
			{
				base.SendMessage(4361, 0, this.imageList.Handle);
			}
			if (this.stateImageList != null)
			{
				this.UpdateNativeStateImageList();
			}
			if (this.indent != -1)
			{
				base.SendMessage(4359, this.indent, 0);
			}
			if (this.itemHeight != -1)
			{
				base.SendMessage(4379, this.ItemHeight, 0);
			}
			try
			{
				this.treeViewState[32768] = true;
				int width = base.Width;
				int flags = 22;
				SafeNativeMethods.SetWindowPos(new HandleRef(this, base.Handle), NativeMethods.NullHandleRef, base.Left, base.Top, int.MaxValue, base.Height, flags);
				this.root.Realize(false);
				if (width != 0)
				{
					SafeNativeMethods.SetWindowPos(new HandleRef(this, base.Handle), NativeMethods.NullHandleRef, base.Left, base.Top, width, base.Height, flags);
				}
			}
			finally
			{
				this.treeViewState[32768] = false;
			}
			this.SelectedNode = treeNode;
		}

		// Token: 0x06004991 RID: 18833 RVA: 0x001350CC File Offset: 0x001332CC
		private void UpdateNativeStateImageList()
		{
			if (this.stateImageList != null && this.stateImageList.Images.Count > 0)
			{
				ImageList imageList = new ImageList();
				if (TreeView.ScaledStateImageSize != null)
				{
					imageList.ImageSize = TreeView.ScaledStateImageSize.Value;
				}
				Image[] array = new Image[this.stateImageList.Images.Count + 1];
				array[0] = this.stateImageList.Images[0];
				for (int i = 1; i <= this.stateImageList.Images.Count; i++)
				{
					array[i] = this.stateImageList.Images[i - 1];
				}
				imageList.Images.AddRange(array);
				base.SendMessage(4361, 2, imageList.Handle);
				if (this.internalStateImageList != null)
				{
					this.internalStateImageList.Dispose();
				}
				this.internalStateImageList = imageList;
			}
		}

		// Token: 0x06004992 RID: 18834 RVA: 0x001351B8 File Offset: 0x001333B8
		private void SetStateImageList(IntPtr handle)
		{
			IntPtr intPtr = base.SendMessage(4361, 2, handle);
			if (intPtr != IntPtr.Zero && intPtr != handle)
			{
				SafeNativeMethods.ImageList_Destroy_Native(new HandleRef(this, intPtr));
			}
		}

		// Token: 0x06004993 RID: 18835 RVA: 0x001351F8 File Offset: 0x001333F8
		private void DestroyNativeStateImageList(bool reset)
		{
			IntPtr intPtr = base.SendMessage(4360, 2, IntPtr.Zero);
			if (intPtr != IntPtr.Zero)
			{
				SafeNativeMethods.ImageList_Destroy_Native(new HandleRef(this, intPtr));
				if (reset)
				{
					base.SendMessage(4361, 2, IntPtr.Zero);
				}
			}
		}

		// Token: 0x06004994 RID: 18836 RVA: 0x00135246 File Offset: 0x00133446
		protected override void OnHandleDestroyed(EventArgs e)
		{
			this.selectedNode = this.SelectedNode;
			this.DestroyNativeStateImageList(true);
			if (this.internalStateImageList != null)
			{
				this.internalStateImageList.Dispose();
				this.internalStateImageList = null;
			}
			base.OnHandleDestroyed(e);
		}

		// Token: 0x06004995 RID: 18837 RVA: 0x0013527C File Offset: 0x0013347C
		protected override void OnMouseLeave(EventArgs e)
		{
			this.hoveredAlready = false;
			base.OnMouseLeave(e);
		}

		// Token: 0x06004996 RID: 18838 RVA: 0x0013528C File Offset: 0x0013348C
		protected override void OnMouseHover(EventArgs e)
		{
			NativeMethods.TV_HITTESTINFO tv_HITTESTINFO = new NativeMethods.TV_HITTESTINFO();
			Point p = Cursor.Position;
			p = base.PointToClientInternal(p);
			tv_HITTESTINFO.pt_x = p.X;
			tv_HITTESTINFO.pt_y = p.Y;
			IntPtr intPtr = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4369, 0, tv_HITTESTINFO);
			if (intPtr != IntPtr.Zero && (tv_HITTESTINFO.flags & 70) != 0)
			{
				TreeNode treeNode = this.NodeFromHandle(intPtr);
				if (treeNode != this.prevHoveredNode && treeNode != null)
				{
					this.OnNodeMouseHover(new TreeNodeMouseHoverEventArgs(treeNode));
					this.prevHoveredNode = treeNode;
				}
			}
			if (!this.hoveredAlready)
			{
				base.OnMouseHover(e);
				this.hoveredAlready = true;
			}
			base.ResetMouseEventArgs();
		}

		// Token: 0x06004997 RID: 18839 RVA: 0x0013533B File Offset: 0x0013353B
		protected virtual void OnBeforeLabelEdit(NodeLabelEditEventArgs e)
		{
			if (this.onBeforeLabelEdit != null)
			{
				this.onBeforeLabelEdit(this, e);
			}
		}

		// Token: 0x06004998 RID: 18840 RVA: 0x00135352 File Offset: 0x00133552
		protected virtual void OnAfterLabelEdit(NodeLabelEditEventArgs e)
		{
			if (this.onAfterLabelEdit != null)
			{
				this.onAfterLabelEdit(this, e);
			}
		}

		// Token: 0x06004999 RID: 18841 RVA: 0x00135369 File Offset: 0x00133569
		protected virtual void OnBeforeCheck(TreeViewCancelEventArgs e)
		{
			if (this.onBeforeCheck != null)
			{
				this.onBeforeCheck(this, e);
			}
		}

		// Token: 0x0600499A RID: 18842 RVA: 0x00135380 File Offset: 0x00133580
		protected virtual void OnAfterCheck(TreeViewEventArgs e)
		{
			if (this.onAfterCheck != null)
			{
				this.onAfterCheck(this, e);
			}
		}

		// Token: 0x0600499B RID: 18843 RVA: 0x00135397 File Offset: 0x00133597
		protected internal virtual void OnBeforeCollapse(TreeViewCancelEventArgs e)
		{
			if (this.onBeforeCollapse != null)
			{
				this.onBeforeCollapse(this, e);
			}
		}

		// Token: 0x0600499C RID: 18844 RVA: 0x001353AE File Offset: 0x001335AE
		protected internal virtual void OnAfterCollapse(TreeViewEventArgs e)
		{
			if (this.onAfterCollapse != null)
			{
				this.onAfterCollapse(this, e);
			}
		}

		// Token: 0x0600499D RID: 18845 RVA: 0x001353C5 File Offset: 0x001335C5
		protected virtual void OnBeforeExpand(TreeViewCancelEventArgs e)
		{
			if (this.onBeforeExpand != null)
			{
				this.onBeforeExpand(this, e);
			}
		}

		// Token: 0x0600499E RID: 18846 RVA: 0x001353DC File Offset: 0x001335DC
		protected virtual void OnAfterExpand(TreeViewEventArgs e)
		{
			if (this.onAfterExpand != null)
			{
				this.onAfterExpand(this, e);
			}
		}

		// Token: 0x0600499F RID: 18847 RVA: 0x001353F3 File Offset: 0x001335F3
		protected virtual void OnItemDrag(ItemDragEventArgs e)
		{
			if (this.onItemDrag != null)
			{
				this.onItemDrag(this, e);
			}
		}

		// Token: 0x060049A0 RID: 18848 RVA: 0x0013540A File Offset: 0x0013360A
		protected virtual void OnNodeMouseHover(TreeNodeMouseHoverEventArgs e)
		{
			if (this.onNodeMouseHover != null)
			{
				this.onNodeMouseHover(this, e);
			}
		}

		// Token: 0x060049A1 RID: 18849 RVA: 0x00135421 File Offset: 0x00133621
		protected virtual void OnBeforeSelect(TreeViewCancelEventArgs e)
		{
			if (this.onBeforeSelect != null)
			{
				this.onBeforeSelect(this, e);
			}
		}

		// Token: 0x060049A2 RID: 18850 RVA: 0x00135438 File Offset: 0x00133638
		protected virtual void OnAfterSelect(TreeViewEventArgs e)
		{
			if (this.onAfterSelect != null)
			{
				this.onAfterSelect(this, e);
			}
		}

		// Token: 0x060049A3 RID: 18851 RVA: 0x0013544F File Offset: 0x0013364F
		protected virtual void OnNodeMouseClick(TreeNodeMouseClickEventArgs e)
		{
			if (this.onNodeMouseClick != null)
			{
				this.onNodeMouseClick(this, e);
			}
		}

		// Token: 0x060049A4 RID: 18852 RVA: 0x00135466 File Offset: 0x00133666
		protected virtual void OnNodeMouseDoubleClick(TreeNodeMouseClickEventArgs e)
		{
			if (this.onNodeMouseDoubleClick != null)
			{
				this.onNodeMouseDoubleClick(this, e);
			}
		}

		// Token: 0x060049A5 RID: 18853 RVA: 0x00135480 File Offset: 0x00133680
		protected override void OnKeyDown(KeyEventArgs e)
		{
			base.OnKeyDown(e);
			if (e.Handled)
			{
				return;
			}
			if (this.CheckBoxes && (e.KeyData & Keys.KeyCode) == Keys.Space)
			{
				TreeNode treeNode = this.SelectedNode;
				if (treeNode != null)
				{
					if (!this.TreeViewBeforeCheck(treeNode, TreeViewAction.ByKeyboard))
					{
						treeNode.CheckedInternal = !treeNode.CheckedInternal;
						this.TreeViewAfterCheck(treeNode, TreeViewAction.ByKeyboard);
					}
					e.Handled = true;
					return;
				}
			}
		}

		// Token: 0x060049A6 RID: 18854 RVA: 0x001354EA File Offset: 0x001336EA
		protected override void OnKeyUp(KeyEventArgs e)
		{
			base.OnKeyUp(e);
			if (e.Handled)
			{
				return;
			}
			if ((e.KeyData & Keys.KeyCode) == Keys.Space)
			{
				e.Handled = true;
				return;
			}
		}

		// Token: 0x060049A7 RID: 18855 RVA: 0x00135514 File Offset: 0x00133714
		protected override void OnKeyPress(KeyPressEventArgs e)
		{
			base.OnKeyPress(e);
			if (e.Handled)
			{
				return;
			}
			if (e.KeyChar == ' ')
			{
				e.Handled = true;
			}
		}

		// Token: 0x060049A8 RID: 18856 RVA: 0x00135537 File Offset: 0x00133737
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
			if (this.onRightToLeftLayoutChanged != null)
			{
				this.onRightToLeftLayoutChanged(this, e);
			}
		}

		// Token: 0x060049A9 RID: 18857 RVA: 0x00135568 File Offset: 0x00133768
		private void RefreshNodes()
		{
			TreeNode[] dest = new TreeNode[this.Nodes.Count];
			this.Nodes.CopyTo(dest, 0);
			this.Nodes.Clear();
			this.Nodes.AddRange(dest);
		}

		// Token: 0x060049AA RID: 18858 RVA: 0x001355AA File Offset: 0x001337AA
		private void ResetIndent()
		{
			this.indent = -1;
			base.RecreateHandle();
		}

		// Token: 0x060049AB RID: 18859 RVA: 0x001355B9 File Offset: 0x001337B9
		private void ResetItemHeight()
		{
			this.itemHeight = -1;
			base.RecreateHandle();
		}

		// Token: 0x060049AC RID: 18860 RVA: 0x001355C8 File Offset: 0x001337C8
		private bool ShouldSerializeIndent()
		{
			return this.indent != -1;
		}

		// Token: 0x060049AD RID: 18861 RVA: 0x001355D6 File Offset: 0x001337D6
		private bool ShouldSerializeItemHeight()
		{
			return this.itemHeight != -1;
		}

		// Token: 0x060049AE RID: 18862 RVA: 0x001355E4 File Offset: 0x001337E4
		private bool ShouldSerializeSelectedImageIndex()
		{
			if (this.imageList != null)
			{
				return this.SelectedImageIndex != 0;
			}
			return this.SelectedImageIndex != -1;
		}

		// Token: 0x060049AF RID: 18863 RVA: 0x00135604 File Offset: 0x00133804
		private bool ShouldSerializeImageIndex()
		{
			if (this.imageList != null)
			{
				return this.ImageIndex != 0;
			}
			return this.ImageIndex != -1;
		}

		// Token: 0x060049B0 RID: 18864 RVA: 0x00135624 File Offset: 0x00133824
		public void Sort()
		{
			this.Sorted = true;
			this.RefreshNodes();
		}

		// Token: 0x060049B1 RID: 18865 RVA: 0x00135634 File Offset: 0x00133834
		public override string ToString()
		{
			string text = base.ToString();
			if (this.Nodes != null)
			{
				text = text + ", Nodes.Count: " + this.Nodes.Count.ToString(CultureInfo.CurrentCulture);
				if (this.Nodes.Count > 0)
				{
					text = text + ", Nodes[0]: " + this.Nodes[0].ToString();
				}
			}
			return text;
		}

		// Token: 0x060049B2 RID: 18866 RVA: 0x001356A0 File Offset: 0x001338A0
		private unsafe void TvnBeginDrag(MouseButtons buttons, NativeMethods.NMTREEVIEW* nmtv)
		{
			NativeMethods.TV_ITEM itemNew = nmtv->itemNew;
			if (itemNew.hItem == IntPtr.Zero)
			{
				return;
			}
			TreeNode item = this.NodeFromHandle(itemNew.hItem);
			this.OnItemDrag(new ItemDragEventArgs(buttons, item));
		}

		// Token: 0x060049B3 RID: 18867 RVA: 0x001356E4 File Offset: 0x001338E4
		private unsafe IntPtr TvnExpanding(NativeMethods.NMTREEVIEW* nmtv)
		{
			NativeMethods.TV_ITEM itemNew = nmtv->itemNew;
			if (itemNew.hItem == IntPtr.Zero)
			{
				return IntPtr.Zero;
			}
			TreeViewCancelEventArgs treeViewCancelEventArgs;
			if ((itemNew.state & 32) == 0)
			{
				treeViewCancelEventArgs = new TreeViewCancelEventArgs(this.NodeFromHandle(itemNew.hItem), false, TreeViewAction.Expand);
				this.OnBeforeExpand(treeViewCancelEventArgs);
			}
			else
			{
				treeViewCancelEventArgs = new TreeViewCancelEventArgs(this.NodeFromHandle(itemNew.hItem), false, TreeViewAction.Collapse);
				this.OnBeforeCollapse(treeViewCancelEventArgs);
			}
			return (IntPtr)(treeViewCancelEventArgs.Cancel ? 1 : 0);
		}

		// Token: 0x060049B4 RID: 18868 RVA: 0x00135768 File Offset: 0x00133968
		private unsafe void TvnExpanded(NativeMethods.NMTREEVIEW* nmtv)
		{
			NativeMethods.TV_ITEM itemNew = nmtv->itemNew;
			if (itemNew.hItem == IntPtr.Zero)
			{
				return;
			}
			TreeNode node = this.NodeFromHandle(itemNew.hItem);
			TreeViewEventArgs e;
			if ((itemNew.state & 32) == 0)
			{
				e = new TreeViewEventArgs(node, TreeViewAction.Collapse);
				this.OnAfterCollapse(e);
				return;
			}
			e = new TreeViewEventArgs(node, TreeViewAction.Expand);
			this.OnAfterExpand(e);
		}

		// Token: 0x060049B5 RID: 18869 RVA: 0x001357C8 File Offset: 0x001339C8
		private unsafe IntPtr TvnSelecting(NativeMethods.NMTREEVIEW* nmtv)
		{
			if (this.treeViewState[65536])
			{
				return (IntPtr)1;
			}
			if (nmtv->itemNew.hItem == IntPtr.Zero)
			{
				return IntPtr.Zero;
			}
			TreeNode node = this.NodeFromHandle(nmtv->itemNew.hItem);
			TreeViewAction action = TreeViewAction.Unknown;
			int action2 = nmtv->action;
			if (action2 != 1)
			{
				if (action2 == 2)
				{
					action = TreeViewAction.ByKeyboard;
				}
			}
			else
			{
				action = TreeViewAction.ByMouse;
			}
			TreeViewCancelEventArgs treeViewCancelEventArgs = new TreeViewCancelEventArgs(node, false, action);
			this.OnBeforeSelect(treeViewCancelEventArgs);
			return (IntPtr)(treeViewCancelEventArgs.Cancel ? 1 : 0);
		}

		// Token: 0x060049B6 RID: 18870 RVA: 0x00135858 File Offset: 0x00133A58
		private unsafe void TvnSelected(NativeMethods.NMTREEVIEW* nmtv)
		{
			if (this.nodesCollectionClear)
			{
				return;
			}
			if (nmtv->itemNew.hItem != IntPtr.Zero)
			{
				TreeViewAction action = TreeViewAction.Unknown;
				int action2 = nmtv->action;
				if (action2 != 1)
				{
					if (action2 == 2)
					{
						action = TreeViewAction.ByKeyboard;
					}
				}
				else
				{
					action = TreeViewAction.ByMouse;
				}
				this.OnAfterSelect(new TreeViewEventArgs(this.NodeFromHandle(nmtv->itemNew.hItem), action));
			}
			NativeMethods.RECT rect = default(NativeMethods.RECT);
			*(IntPtr*)(&rect.left) = nmtv->itemOld.hItem;
			if (nmtv->itemOld.hItem != IntPtr.Zero && (int)((long)UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4356, 1, ref rect)) != 0)
			{
				SafeNativeMethods.InvalidateRect(new HandleRef(this, base.Handle), ref rect, true);
			}
		}

		// Token: 0x060049B7 RID: 18871 RVA: 0x00135924 File Offset: 0x00133B24
		private IntPtr TvnBeginLabelEdit(NativeMethods.NMTVDISPINFO nmtvdi)
		{
			if (nmtvdi.item.hItem == IntPtr.Zero)
			{
				return IntPtr.Zero;
			}
			TreeNode node = this.NodeFromHandle(nmtvdi.item.hItem);
			NodeLabelEditEventArgs nodeLabelEditEventArgs = new NodeLabelEditEventArgs(node);
			this.OnBeforeLabelEdit(nodeLabelEditEventArgs);
			if (!nodeLabelEditEventArgs.CancelEdit)
			{
				this.editNode = node;
			}
			return (IntPtr)(nodeLabelEditEventArgs.CancelEdit ? 1 : 0);
		}

		// Token: 0x060049B8 RID: 18872 RVA: 0x00135990 File Offset: 0x00133B90
		private IntPtr TvnEndLabelEdit(NativeMethods.NMTVDISPINFO nmtvdi)
		{
			this.editNode = null;
			if (nmtvdi.item.hItem == IntPtr.Zero)
			{
				return (IntPtr)1;
			}
			TreeNode treeNode = this.NodeFromHandle(nmtvdi.item.hItem);
			string text = (nmtvdi.item.pszText == IntPtr.Zero) ? null : Marshal.PtrToStringAuto(nmtvdi.item.pszText);
			NodeLabelEditEventArgs nodeLabelEditEventArgs = new NodeLabelEditEventArgs(treeNode, text);
			this.OnAfterLabelEdit(nodeLabelEditEventArgs);
			if (text != null && !nodeLabelEditEventArgs.CancelEdit && treeNode != null)
			{
				treeNode.text = text;
				if (this.Scrollable)
				{
					this.ForceScrollbarUpdate(true);
				}
			}
			return (IntPtr)(nodeLabelEditEventArgs.CancelEdit ? 0 : 1);
		}

		// Token: 0x060049B9 RID: 18873 RVA: 0x00135A43 File Offset: 0x00133C43
		internal override void UpdateStylesCore()
		{
			base.UpdateStylesCore();
			if (base.IsHandleCreated && this.CheckBoxes && this.StateImageList != null && this.internalStateImageList != null)
			{
				this.SetStateImageList(this.internalStateImageList.Handle);
			}
		}

		// Token: 0x060049BA RID: 18874 RVA: 0x00135A7C File Offset: 0x00133C7C
		private void UpdateCheckedState(TreeNode node, bool update)
		{
			if (update)
			{
				node.CheckedInternal = node.CheckedInternal;
				for (int i = node.Nodes.Count - 1; i >= 0; i--)
				{
					this.UpdateCheckedState(node.Nodes[i], update);
				}
				return;
			}
			node.CheckedInternal = false;
			for (int j = node.Nodes.Count - 1; j >= 0; j--)
			{
				this.UpdateCheckedState(node.Nodes[j], update);
			}
		}

		// Token: 0x060049BB RID: 18875 RVA: 0x00135AF8 File Offset: 0x00133CF8
		private void WmMouseDown(ref Message m, MouseButtons button, int clicks)
		{
			base.SendMessage(4363, 8, null);
			this.OnMouseDown(new MouseEventArgs(button, clicks, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
			if (!base.ValidationCancelled)
			{
				this.DefWndProc(ref m);
			}
		}

		// Token: 0x060049BC RID: 18876 RVA: 0x00135B48 File Offset: 0x00133D48
		private void CustomDraw(ref Message m)
		{
			NativeMethods.NMTVCUSTOMDRAW nmtvcustomdraw = (NativeMethods.NMTVCUSTOMDRAW)m.GetLParam(typeof(NativeMethods.NMTVCUSTOMDRAW));
			int dwDrawStage = nmtvcustomdraw.nmcd.dwDrawStage;
			if (dwDrawStage != 1)
			{
				if (dwDrawStage != 65537)
				{
					if (dwDrawStage == 65538)
					{
						if (this.drawMode == TreeViewDrawMode.OwnerDrawText)
						{
							TreeNode treeNode = this.NodeFromHandle(nmtvcustomdraw.nmcd.dwItemSpec);
							if (treeNode == null)
							{
								return;
							}
							Graphics graphics = Graphics.FromHdcInternal(nmtvcustomdraw.nmcd.hdc);
							try
							{
								Rectangle bounds = treeNode.Bounds;
								Size size = TextRenderer.MeasureText(treeNode.Text, treeNode.TreeView.Font);
								Point location = new Point(bounds.X - 1, bounds.Y);
								bounds = new Rectangle(location, new Size(size.Width, bounds.Height));
								DrawTreeNodeEventArgs drawTreeNodeEventArgs = new DrawTreeNodeEventArgs(graphics, treeNode, bounds, (TreeNodeStates)nmtvcustomdraw.nmcd.uItemState);
								this.OnDrawNode(drawTreeNodeEventArgs);
								if (drawTreeNodeEventArgs.DrawDefault)
								{
									TreeNodeStates state = drawTreeNodeEventArgs.State;
									Font font = (treeNode.NodeFont != null) ? treeNode.NodeFont : treeNode.TreeView.Font;
									Color foreColor = ((state & TreeNodeStates.Selected) == TreeNodeStates.Selected && treeNode.TreeView.Focused) ? SystemColors.HighlightText : ((treeNode.ForeColor != Color.Empty) ? treeNode.ForeColor : treeNode.TreeView.ForeColor);
									if ((state & TreeNodeStates.Selected) == TreeNodeStates.Selected)
									{
										graphics.FillRectangle(SystemBrushes.Highlight, bounds);
										ControlPaint.DrawFocusRectangle(graphics, bounds, foreColor, SystemColors.Highlight);
										TextRenderer.DrawText(graphics, drawTreeNodeEventArgs.Node.Text, font, bounds, foreColor, TextFormatFlags.Default);
									}
									else
									{
										using (Brush brush = new SolidBrush(this.BackColor))
										{
											graphics.FillRectangle(brush, bounds);
										}
										TextRenderer.DrawText(graphics, drawTreeNodeEventArgs.Node.Text, font, bounds, foreColor, TextFormatFlags.Default);
									}
								}
							}
							finally
							{
								graphics.Dispose();
							}
							m.Result = (IntPtr)32;
							return;
						}
					}
				}
				else
				{
					TreeNode treeNode = this.NodeFromHandle(nmtvcustomdraw.nmcd.dwItemSpec);
					if (treeNode == null)
					{
						m.Result = (IntPtr)4;
						return;
					}
					int uItemState = nmtvcustomdraw.nmcd.uItemState;
					if (this.drawMode == TreeViewDrawMode.OwnerDrawText)
					{
						nmtvcustomdraw.clrText = nmtvcustomdraw.clrTextBk;
						Marshal.StructureToPtr(nmtvcustomdraw, m.LParam, false);
						m.Result = (IntPtr)18;
						return;
					}
					if (this.drawMode == TreeViewDrawMode.OwnerDrawAll)
					{
						Graphics graphics2 = Graphics.FromHdcInternal(nmtvcustomdraw.nmcd.hdc);
						DrawTreeNodeEventArgs drawTreeNodeEventArgs2;
						try
						{
							Rectangle rowBounds = treeNode.RowBounds;
							NativeMethods.SCROLLINFO scrollinfo = new NativeMethods.SCROLLINFO();
							scrollinfo.cbSize = Marshal.SizeOf(typeof(NativeMethods.SCROLLINFO));
							scrollinfo.fMask = 4;
							if (UnsafeNativeMethods.GetScrollInfo(new HandleRef(this, base.Handle), 0, scrollinfo))
							{
								int nPos = scrollinfo.nPos;
								if (nPos > 0)
								{
									rowBounds.X -= nPos;
									rowBounds.Width += nPos;
								}
							}
							drawTreeNodeEventArgs2 = new DrawTreeNodeEventArgs(graphics2, treeNode, rowBounds, (TreeNodeStates)uItemState);
							this.OnDrawNode(drawTreeNodeEventArgs2);
						}
						finally
						{
							graphics2.Dispose();
						}
						if (!drawTreeNodeEventArgs2.DrawDefault)
						{
							m.Result = (IntPtr)4;
							return;
						}
					}
					OwnerDrawPropertyBag itemRenderStyles = this.GetItemRenderStyles(treeNode, uItemState);
					bool flag = false;
					Color foreColor2 = itemRenderStyles.ForeColor;
					Color backColor = itemRenderStyles.BackColor;
					if (itemRenderStyles != null && !foreColor2.IsEmpty)
					{
						nmtvcustomdraw.clrText = ColorTranslator.ToWin32(foreColor2);
						flag = true;
					}
					if (itemRenderStyles != null && !backColor.IsEmpty)
					{
						nmtvcustomdraw.clrTextBk = ColorTranslator.ToWin32(backColor);
						flag = true;
					}
					if (flag)
					{
						Marshal.StructureToPtr(nmtvcustomdraw, m.LParam, false);
					}
					if (itemRenderStyles != null && itemRenderStyles.Font != null)
					{
						SafeNativeMethods.SelectObject(new HandleRef(nmtvcustomdraw.nmcd, nmtvcustomdraw.nmcd.hdc), new HandleRef(itemRenderStyles, itemRenderStyles.FontHandle));
						m.Result = (IntPtr)2;
						return;
					}
				}
				m.Result = (IntPtr)0;
				return;
			}
			m.Result = (IntPtr)32;
		}

		// Token: 0x060049BD RID: 18877 RVA: 0x00135F84 File Offset: 0x00134184
		protected OwnerDrawPropertyBag GetItemRenderStyles(TreeNode node, int state)
		{
			OwnerDrawPropertyBag ownerDrawPropertyBag = new OwnerDrawPropertyBag();
			if (node == null || node.propBag == null)
			{
				return ownerDrawPropertyBag;
			}
			if ((state & 71) == 0)
			{
				ownerDrawPropertyBag.ForeColor = node.propBag.ForeColor;
				ownerDrawPropertyBag.BackColor = node.propBag.BackColor;
			}
			ownerDrawPropertyBag.Font = node.propBag.Font;
			return ownerDrawPropertyBag;
		}

		// Token: 0x060049BE RID: 18878 RVA: 0x00135FE0 File Offset: 0x001341E0
		private unsafe bool WmShowToolTip(ref Message m)
		{
			NativeMethods.NMHDR* ptr = (NativeMethods.NMHDR*)((void*)m.LParam);
			IntPtr hwndFrom = ptr->hwndFrom;
			NativeMethods.TV_HITTESTINFO tv_HITTESTINFO = new NativeMethods.TV_HITTESTINFO();
			Point p = Cursor.Position;
			p = base.PointToClientInternal(p);
			tv_HITTESTINFO.pt_x = p.X;
			tv_HITTESTINFO.pt_y = p.Y;
			IntPtr intPtr = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4369, 0, tv_HITTESTINFO);
			if (intPtr != IntPtr.Zero && (tv_HITTESTINFO.flags & 70) != 0)
			{
				TreeNode treeNode = this.NodeFromHandle(intPtr);
				if (treeNode != null && !this.ShowNodeToolTips)
				{
					Rectangle bounds = treeNode.Bounds;
					bounds.Location = base.PointToScreen(bounds.Location);
					UnsafeNativeMethods.SendMessage(new HandleRef(this, hwndFrom), 1055, 1, ref bounds);
					SafeNativeMethods.SetWindowPos(new HandleRef(this, hwndFrom), NativeMethods.HWND_TOPMOST, bounds.Left, bounds.Top, 0, 0, 21);
					return true;
				}
			}
			return false;
		}

		// Token: 0x060049BF RID: 18879 RVA: 0x001360D0 File Offset: 0x001342D0
		private void WmNeedText(ref Message m)
		{
			NativeMethods.TOOLTIPTEXT tooltiptext = (NativeMethods.TOOLTIPTEXT)m.GetLParam(typeof(NativeMethods.TOOLTIPTEXT));
			string lpszText = this.controlToolTipText;
			NativeMethods.TV_HITTESTINFO tv_HITTESTINFO = new NativeMethods.TV_HITTESTINFO();
			Point p = Cursor.Position;
			p = base.PointToClientInternal(p);
			tv_HITTESTINFO.pt_x = p.X;
			tv_HITTESTINFO.pt_y = p.Y;
			IntPtr intPtr = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4369, 0, tv_HITTESTINFO);
			if (intPtr != IntPtr.Zero && (tv_HITTESTINFO.flags & 70) != 0)
			{
				TreeNode treeNode = this.NodeFromHandle(intPtr);
				if (this.ShowNodeToolTips && treeNode != null && !string.IsNullOrEmpty(treeNode.ToolTipText))
				{
					lpszText = treeNode.ToolTipText;
				}
				else if (treeNode != null && treeNode.Bounds.Right > base.Bounds.Right)
				{
					lpszText = treeNode.Text;
				}
				else
				{
					lpszText = null;
				}
			}
			tooltiptext.lpszText = lpszText;
			tooltiptext.hinst = IntPtr.Zero;
			if (this.RightToLeft == RightToLeft.Yes)
			{
				tooltiptext.uFlags |= 4;
			}
			Marshal.StructureToPtr(tooltiptext, m.LParam, false);
		}

		// Token: 0x060049C0 RID: 18880 RVA: 0x001361F0 File Offset: 0x001343F0
		private unsafe void WmNotify(ref Message m)
		{
			NativeMethods.NMHDR* ptr = (NativeMethods.NMHDR*)((void*)m.LParam);
			if (ptr->code == -12)
			{
				this.CustomDraw(ref m);
				return;
			}
			NativeMethods.NMTREEVIEW* ptr2 = (NativeMethods.NMTREEVIEW*)((void*)m.LParam);
			int code = ptr2->nmhdr.code;
			if (code <= -401)
			{
				switch (code)
				{
				case -460:
					goto IL_12E;
				case -459:
					goto IL_10C;
				case -458:
				case -453:
				case -452:
					return;
				case -457:
					goto IL_FF;
				case -456:
					goto IL_F2;
				case -455:
					goto IL_D4;
				case -454:
					break;
				case -451:
					goto IL_EA;
				case -450:
					goto IL_DC;
				default:
					switch (code)
					{
					case -411:
						goto IL_12E;
					case -410:
						goto IL_10C;
					case -409:
					case -404:
					case -403:
						return;
					case -408:
						goto IL_FF;
					case -407:
						goto IL_F2;
					case -406:
						goto IL_D4;
					case -405:
						break;
					case -402:
						goto IL_EA;
					case -401:
						goto IL_DC;
					default:
						return;
					}
					break;
				}
				m.Result = this.TvnExpanding(ptr2);
				return;
				IL_D4:
				this.TvnExpanded(ptr2);
				return;
				IL_DC:
				m.Result = this.TvnSelecting(ptr2);
				return;
				IL_EA:
				this.TvnSelected(ptr2);
				return;
				IL_F2:
				this.TvnBeginDrag(MouseButtons.Left, ptr2);
				return;
				IL_FF:
				this.TvnBeginDrag(MouseButtons.Right, ptr2);
				return;
				IL_10C:
				m.Result = this.TvnBeginLabelEdit((NativeMethods.NMTVDISPINFO)m.GetLParam(typeof(NativeMethods.NMTVDISPINFO)));
				return;
				IL_12E:
				m.Result = this.TvnEndLabelEdit((NativeMethods.NMTVDISPINFO)m.GetLParam(typeof(NativeMethods.NMTVDISPINFO)));
				return;
			}
			if (code != -5 && code != -2)
			{
				return;
			}
			MouseButtons button = MouseButtons.Left;
			NativeMethods.TV_HITTESTINFO tv_HITTESTINFO = new NativeMethods.TV_HITTESTINFO();
			Point p = Cursor.Position;
			p = base.PointToClientInternal(p);
			tv_HITTESTINFO.pt_x = p.X;
			tv_HITTESTINFO.pt_y = p.Y;
			IntPtr intPtr = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4369, 0, tv_HITTESTINFO);
			if (ptr2->nmhdr.code != -2 || (tv_HITTESTINFO.flags & 70) != 0)
			{
				button = ((ptr2->nmhdr.code == -2) ? MouseButtons.Left : MouseButtons.Right);
			}
			if ((ptr2->nmhdr.code != -2 || (tv_HITTESTINFO.flags & 70) != 0 || this.FullRowSelect) && intPtr != IntPtr.Zero && !base.ValidationCancelled)
			{
				this.OnNodeMouseClick(new TreeNodeMouseClickEventArgs(this.NodeFromHandle(intPtr), button, 1, p.X, p.Y));
				this.OnClick(new MouseEventArgs(button, 1, p.X, p.Y, 0));
				this.OnMouseClick(new MouseEventArgs(button, 1, p.X, p.Y, 0));
			}
			if (ptr2->nmhdr.code == -5)
			{
				TreeNode treeNode = this.NodeFromHandle(intPtr);
				if (treeNode != null && (treeNode.ContextMenu != null || treeNode.ContextMenuStrip != null))
				{
					this.ShowContextMenu(treeNode);
				}
				else
				{
					this.treeViewState[8192] = true;
					base.SendMessage(123, base.Handle, SafeNativeMethods.GetMessagePos());
				}
				m.Result = (IntPtr)1;
			}
			if (!this.treeViewState[4096] && (ptr2->nmhdr.code != -2 || (tv_HITTESTINFO.flags & 70) != 0))
			{
				this.OnMouseUp(new MouseEventArgs(button, 1, p.X, p.Y, 0));
				this.treeViewState[4096] = true;
			}
		}

		// Token: 0x060049C1 RID: 18881 RVA: 0x0013652C File Offset: 0x0013472C
		private void ShowContextMenu(TreeNode treeNode)
		{
			if (treeNode.ContextMenu != null || treeNode.ContextMenuStrip != null)
			{
				ContextMenu contextMenu = treeNode.ContextMenu;
				ContextMenuStrip contextMenuStrip = treeNode.ContextMenuStrip;
				if (contextMenu != null)
				{
					NativeMethods.POINT point = new NativeMethods.POINT();
					UnsafeNativeMethods.GetCursorPos(point);
					UnsafeNativeMethods.SetForegroundWindow(new HandleRef(this, base.Handle));
					contextMenu.OnPopup(EventArgs.Empty);
					SafeNativeMethods.TrackPopupMenuEx(new HandleRef(contextMenu, contextMenu.Handle), 64, point.x, point.y, new HandleRef(this, base.Handle), null);
					UnsafeNativeMethods.PostMessage(new HandleRef(this, base.Handle), 0, IntPtr.Zero, IntPtr.Zero);
					return;
				}
				if (contextMenuStrip != null)
				{
					UnsafeNativeMethods.PostMessage(new HandleRef(this, base.Handle), 4363, 8, treeNode.Handle);
					contextMenuStrip.ShowInternal(this, base.PointToClient(Control.MousePosition), false);
					contextMenuStrip.Closing += this.ContextMenuStripClosing;
				}
			}
		}

		// Token: 0x060049C2 RID: 18882 RVA: 0x00136618 File Offset: 0x00134818
		private void ContextMenuStripClosing(object sender, ToolStripDropDownClosingEventArgs e)
		{
			ContextMenuStrip contextMenuStrip = sender as ContextMenuStrip;
			contextMenuStrip.Closing -= this.ContextMenuStripClosing;
			base.SendMessage(4363, 8, null);
		}

		// Token: 0x060049C3 RID: 18883 RVA: 0x0013664C File Offset: 0x0013484C
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

		// Token: 0x060049C4 RID: 18884 RVA: 0x00136718 File Offset: 0x00134918
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg <= 131)
			{
				if (msg <= 21)
				{
					if (msg != 5)
					{
						if (msg != 7)
						{
							if (msg != 21)
							{
								goto IL_878;
							}
							base.SendMessage(4359, this.Indent, 0);
							base.WndProc(ref m);
							return;
						}
						else
						{
							if (this.treeViewState[16384])
							{
								this.treeViewState[16384] = false;
								base.WmImeSetFocus();
								this.DefWndProc(ref m);
								base.InvokeGotFocus(this, EventArgs.Empty);
								return;
							}
							base.WndProc(ref m);
							return;
						}
					}
				}
				else if (msg <= 78)
				{
					if (msg - 70 > 1)
					{
						if (msg != 78)
						{
							goto IL_878;
						}
						NativeMethods.NMHDR nmhdr = (NativeMethods.NMHDR)m.GetLParam(typeof(NativeMethods.NMHDR));
						int code = nmhdr.code;
						if (code != -530)
						{
							if (code != -521)
							{
								if (code != -520)
								{
									base.WndProc(ref m);
									return;
								}
							}
							else
							{
								if (this.WmShowToolTip(ref m))
								{
									m.Result = (IntPtr)1;
									return;
								}
								base.WndProc(ref m);
								return;
							}
						}
						UnsafeNativeMethods.SendMessage(new HandleRef(nmhdr, nmhdr.hwndFrom), 1048, 0, SystemInformation.MaxWindowTrackSize.Width);
						this.WmNeedText(ref m);
						m.Result = (IntPtr)1;
						return;
					}
				}
				else if (msg != 123)
				{
					if (msg != 131)
					{
						goto IL_878;
					}
				}
				else
				{
					if (this.treeViewState[8192])
					{
						this.treeViewState[8192] = false;
						base.WndProc(ref m);
						return;
					}
					TreeNode treeNode = this.SelectedNode;
					if (treeNode == null || (treeNode.ContextMenu == null && treeNode.ContextMenuStrip == null))
					{
						base.WndProc(ref m);
						return;
					}
					Point point = new Point(treeNode.Bounds.X, treeNode.Bounds.Y + treeNode.Bounds.Height / 2);
					if (!base.ClientRectangle.Contains(point))
					{
						return;
					}
					if (treeNode.ContextMenu != null)
					{
						treeNode.ContextMenu.Show(this, point);
						return;
					}
					if (treeNode.ContextMenuStrip != null)
					{
						bool isKeyboardActivated = (int)((long)m.LParam) == -1;
						treeNode.ContextMenuStrip.ShowInternal(this, point, isKeyboardActivated);
						return;
					}
					return;
				}
				if (this.treeViewState[32768])
				{
					this.DefWndProc(ref m);
					return;
				}
				base.WndProc(ref m);
				return;
			}
			else if (msg <= 675)
			{
				if (msg != 276)
				{
					switch (msg)
					{
					case 513:
					{
						try
						{
							this.treeViewState[65536] = true;
							this.FocusInternal();
						}
						finally
						{
							this.treeViewState[65536] = false;
						}
						this.treeViewState[4096] = false;
						NativeMethods.TV_HITTESTINFO tv_HITTESTINFO = new NativeMethods.TV_HITTESTINFO();
						tv_HITTESTINFO.pt_x = NativeMethods.Util.SignedLOWORD(m.LParam);
						tv_HITTESTINFO.pt_y = NativeMethods.Util.SignedHIWORD(m.LParam);
						this.hNodeMouseDown = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4369, 0, tv_HITTESTINFO);
						if ((tv_HITTESTINFO.flags & 64) != 0)
						{
							this.OnMouseDown(new MouseEventArgs(MouseButtons.Left, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
							if (!base.ValidationCancelled && this.CheckBoxes)
							{
								TreeNode treeNode2 = this.NodeFromHandle(this.hNodeMouseDown);
								if (!this.TreeViewBeforeCheck(treeNode2, TreeViewAction.ByMouse) && treeNode2 != null)
								{
									treeNode2.CheckedInternal = !treeNode2.CheckedInternal;
									this.TreeViewAfterCheck(treeNode2, TreeViewAction.ByMouse);
								}
							}
							m.Result = IntPtr.Zero;
						}
						else
						{
							this.WmMouseDown(ref m, MouseButtons.Left, 1);
						}
						this.downButton = MouseButtons.Left;
						return;
					}
					case 514:
					case 517:
					{
						NativeMethods.TV_HITTESTINFO tv_HITTESTINFO2 = new NativeMethods.TV_HITTESTINFO();
						tv_HITTESTINFO2.pt_x = NativeMethods.Util.SignedLOWORD(m.LParam);
						tv_HITTESTINFO2.pt_y = NativeMethods.Util.SignedHIWORD(m.LParam);
						IntPtr intPtr = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4369, 0, tv_HITTESTINFO2);
						if (intPtr != IntPtr.Zero)
						{
							if (!base.ValidationCancelled && (!this.treeViewState[2048] & !this.treeViewState[4096]))
							{
								if (intPtr == this.hNodeMouseDown)
								{
									this.OnNodeMouseClick(new TreeNodeMouseClickEventArgs(this.NodeFromHandle(intPtr), this.downButton, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam)));
								}
								this.OnClick(new MouseEventArgs(this.downButton, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
								this.OnMouseClick(new MouseEventArgs(this.downButton, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
							}
							if (this.treeViewState[2048])
							{
								this.treeViewState[2048] = false;
								if (!base.ValidationCancelled)
								{
									this.OnNodeMouseDoubleClick(new TreeNodeMouseClickEventArgs(this.NodeFromHandle(intPtr), this.downButton, 2, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam)));
									this.OnDoubleClick(new MouseEventArgs(this.downButton, 2, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
									this.OnMouseDoubleClick(new MouseEventArgs(this.downButton, 2, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
								}
							}
						}
						if (!this.treeViewState[4096])
						{
							this.OnMouseUp(new MouseEventArgs(this.downButton, 1, NativeMethods.Util.SignedLOWORD(m.LParam), NativeMethods.Util.SignedHIWORD(m.LParam), 0));
						}
						this.treeViewState[2048] = false;
						this.treeViewState[4096] = false;
						base.CaptureInternal = false;
						this.hNodeMouseDown = IntPtr.Zero;
						return;
					}
					case 515:
						this.WmMouseDown(ref m, MouseButtons.Left, 2);
						this.treeViewState[2048] = true;
						this.treeViewState[4096] = false;
						base.CaptureInternal = true;
						return;
					case 516:
					{
						this.treeViewState[4096] = false;
						NativeMethods.TV_HITTESTINFO tv_HITTESTINFO3 = new NativeMethods.TV_HITTESTINFO();
						tv_HITTESTINFO3.pt_x = NativeMethods.Util.SignedLOWORD(m.LParam);
						tv_HITTESTINFO3.pt_y = NativeMethods.Util.SignedHIWORD(m.LParam);
						this.hNodeMouseDown = UnsafeNativeMethods.SendMessage(new HandleRef(this, base.Handle), 4369, 0, tv_HITTESTINFO3);
						this.WmMouseDown(ref m, MouseButtons.Right, 1);
						this.downButton = MouseButtons.Right;
						return;
					}
					case 518:
						this.WmMouseDown(ref m, MouseButtons.Right, 2);
						this.treeViewState[2048] = true;
						this.treeViewState[4096] = false;
						base.CaptureInternal = true;
						return;
					case 519:
						this.treeViewState[4096] = false;
						this.WmMouseDown(ref m, MouseButtons.Middle, 1);
						this.downButton = MouseButtons.Middle;
						return;
					case 520:
						break;
					case 521:
						this.treeViewState[4096] = false;
						this.WmMouseDown(ref m, MouseButtons.Middle, 2);
						return;
					default:
						if (msg == 675)
						{
							this.prevHoveredNode = null;
							base.WndProc(ref m);
							return;
						}
						break;
					}
				}
				else
				{
					base.WndProc(ref m);
					if (this.DrawMode == TreeViewDrawMode.OwnerDrawAll)
					{
						base.Invalidate();
						return;
					}
					return;
				}
			}
			else
			{
				if (msg <= 4365)
				{
					if (msg == 791)
					{
						this.WmPrint(ref m);
						return;
					}
					if (msg != 4365)
					{
						goto IL_878;
					}
				}
				else if (msg != 4415)
				{
					if (msg != 8270)
					{
						goto IL_878;
					}
					this.WmNotify(ref m);
					return;
				}
				base.WndProc(ref m);
				if (!this.CheckBoxes)
				{
					return;
				}
				NativeMethods.TV_ITEM tv_ITEM = (NativeMethods.TV_ITEM)m.GetLParam(typeof(NativeMethods.TV_ITEM));
				if (tv_ITEM.hItem != IntPtr.Zero)
				{
					NativeMethods.TV_ITEM tv_ITEM2 = default(NativeMethods.TV_ITEM);
					tv_ITEM2.mask = 24;
					tv_ITEM2.hItem = tv_ITEM.hItem;
					tv_ITEM2.stateMask = 61440;
					UnsafeNativeMethods.SendMessage(new HandleRef(null, base.Handle), NativeMethods.TVM_GETITEM, 0, ref tv_ITEM2);
					TreeNode treeNode3 = this.NodeFromHandle(tv_ITEM.hItem);
					treeNode3.CheckedStateInternal = (tv_ITEM2.state >> 12 > 1);
					return;
				}
				return;
			}
			IL_878:
			base.WndProc(ref m);
		}

		// Token: 0x04002756 RID: 10070
		private const int MaxIndent = 32000;

		// Token: 0x04002757 RID: 10071
		private const string backSlash = "\\";

		// Token: 0x04002758 RID: 10072
		private const int DefaultTreeViewIndent = 19;

		// Token: 0x04002759 RID: 10073
		private DrawTreeNodeEventHandler onDrawNode;

		// Token: 0x0400275A RID: 10074
		private NodeLabelEditEventHandler onBeforeLabelEdit;

		// Token: 0x0400275B RID: 10075
		private NodeLabelEditEventHandler onAfterLabelEdit;

		// Token: 0x0400275C RID: 10076
		private TreeViewCancelEventHandler onBeforeCheck;

		// Token: 0x0400275D RID: 10077
		private TreeViewEventHandler onAfterCheck;

		// Token: 0x0400275E RID: 10078
		private TreeViewCancelEventHandler onBeforeCollapse;

		// Token: 0x0400275F RID: 10079
		private TreeViewEventHandler onAfterCollapse;

		// Token: 0x04002760 RID: 10080
		private TreeViewCancelEventHandler onBeforeExpand;

		// Token: 0x04002761 RID: 10081
		private TreeViewEventHandler onAfterExpand;

		// Token: 0x04002762 RID: 10082
		private TreeViewCancelEventHandler onBeforeSelect;

		// Token: 0x04002763 RID: 10083
		private TreeViewEventHandler onAfterSelect;

		// Token: 0x04002764 RID: 10084
		private ItemDragEventHandler onItemDrag;

		// Token: 0x04002765 RID: 10085
		private TreeNodeMouseHoverEventHandler onNodeMouseHover;

		// Token: 0x04002766 RID: 10086
		private EventHandler onRightToLeftLayoutChanged;

		// Token: 0x04002767 RID: 10087
		internal TreeNode selectedNode;

		// Token: 0x04002768 RID: 10088
		private ImageList.Indexer imageIndexer;

		// Token: 0x04002769 RID: 10089
		private ImageList.Indexer selectedImageIndexer;

		// Token: 0x0400276A RID: 10090
		private bool setOddHeight;

		// Token: 0x0400276B RID: 10091
		private TreeNode prevHoveredNode;

		// Token: 0x0400276C RID: 10092
		private bool hoveredAlready;

		// Token: 0x0400276D RID: 10093
		private bool rightToLeftLayout;

		// Token: 0x0400276E RID: 10094
		private IntPtr hNodeMouseDown = IntPtr.Zero;

		// Token: 0x0400276F RID: 10095
		private const int TREEVIEWSTATE_hideSelection = 1;

		// Token: 0x04002770 RID: 10096
		private const int TREEVIEWSTATE_labelEdit = 2;

		// Token: 0x04002771 RID: 10097
		private const int TREEVIEWSTATE_scrollable = 4;

		// Token: 0x04002772 RID: 10098
		private const int TREEVIEWSTATE_checkBoxes = 8;

		// Token: 0x04002773 RID: 10099
		private const int TREEVIEWSTATE_showLines = 16;

		// Token: 0x04002774 RID: 10100
		private const int TREEVIEWSTATE_showPlusMinus = 32;

		// Token: 0x04002775 RID: 10101
		private const int TREEVIEWSTATE_showRootLines = 64;

		// Token: 0x04002776 RID: 10102
		private const int TREEVIEWSTATE_sorted = 128;

		// Token: 0x04002777 RID: 10103
		private const int TREEVIEWSTATE_hotTracking = 256;

		// Token: 0x04002778 RID: 10104
		private const int TREEVIEWSTATE_fullRowSelect = 512;

		// Token: 0x04002779 RID: 10105
		private const int TREEVIEWSTATE_showNodeToolTips = 1024;

		// Token: 0x0400277A RID: 10106
		private const int TREEVIEWSTATE_doubleclickFired = 2048;

		// Token: 0x0400277B RID: 10107
		private const int TREEVIEWSTATE_mouseUpFired = 4096;

		// Token: 0x0400277C RID: 10108
		private const int TREEVIEWSTATE_showTreeViewContextMenu = 8192;

		// Token: 0x0400277D RID: 10109
		private const int TREEVIEWSTATE_lastControlValidated = 16384;

		// Token: 0x0400277E RID: 10110
		private const int TREEVIEWSTATE_stopResizeWindowMsgs = 32768;

		// Token: 0x0400277F RID: 10111
		private const int TREEVIEWSTATE_ignoreSelects = 65536;

		// Token: 0x04002780 RID: 10112
		private BitVector32 treeViewState;

		// Token: 0x04002781 RID: 10113
		private static bool isScalingInitialized;

		// Token: 0x04002782 RID: 10114
		private static Size? scaledStateImageSize;

		// Token: 0x04002783 RID: 10115
		private ImageList imageList;

		// Token: 0x04002784 RID: 10116
		private int indent = -1;

		// Token: 0x04002785 RID: 10117
		private int itemHeight = -1;

		// Token: 0x04002786 RID: 10118
		private string pathSeparator = "\\";

		// Token: 0x04002787 RID: 10119
		private BorderStyle borderStyle = BorderStyle.Fixed3D;

		// Token: 0x04002788 RID: 10120
		internal TreeNodeCollection nodes;

		// Token: 0x04002789 RID: 10121
		internal TreeNode editNode;

		// Token: 0x0400278A RID: 10122
		internal TreeNode root;

		// Token: 0x0400278B RID: 10123
		internal Hashtable nodeTable = new Hashtable();

		// Token: 0x0400278C RID: 10124
		internal bool nodesCollectionClear;

		// Token: 0x0400278D RID: 10125
		private MouseButtons downButton;

		// Token: 0x0400278E RID: 10126
		private TreeViewDrawMode drawMode;

		// Token: 0x0400278F RID: 10127
		private ImageList internalStateImageList;

		// Token: 0x04002790 RID: 10128
		private TreeNode topNode;

		// Token: 0x04002791 RID: 10129
		private ImageList stateImageList;

		// Token: 0x04002792 RID: 10130
		private Color lineColor;

		// Token: 0x04002793 RID: 10131
		private string controlToolTipText;

		// Token: 0x04002794 RID: 10132
		private IComparer treeViewNodeSorter;

		// Token: 0x04002795 RID: 10133
		private TreeNodeMouseClickEventHandler onNodeMouseClick;

		// Token: 0x04002796 RID: 10134
		private TreeNodeMouseClickEventHandler onNodeMouseDoubleClick;
	}
}

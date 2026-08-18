using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace System.Windows.Forms
{
	// Token: 0x02000410 RID: 1040
	[TypeConverter(typeof(TreeNodeConverter))]
	[DefaultProperty("Text")]
	[Serializable]
	public class TreeNode : MarshalByRefObject, ICloneable, ISerializable
	{
		// Token: 0x170011B3 RID: 4531
		// (get) Token: 0x06004858 RID: 18520 RVA: 0x00130779 File Offset: 0x0012E979
		internal TreeNode.TreeNodeImageIndexer ImageIndexer
		{
			get
			{
				if (this.imageIndexer == null)
				{
					this.imageIndexer = new TreeNode.TreeNodeImageIndexer(this, TreeNode.TreeNodeImageIndexer.ImageListType.Default);
				}
				return this.imageIndexer;
			}
		}

		// Token: 0x170011B4 RID: 4532
		// (get) Token: 0x06004859 RID: 18521 RVA: 0x00130796 File Offset: 0x0012E996
		internal TreeNode.TreeNodeImageIndexer SelectedImageIndexer
		{
			get
			{
				if (this.selectedImageIndexer == null)
				{
					this.selectedImageIndexer = new TreeNode.TreeNodeImageIndexer(this, TreeNode.TreeNodeImageIndexer.ImageListType.Default);
				}
				return this.selectedImageIndexer;
			}
		}

		// Token: 0x170011B5 RID: 4533
		// (get) Token: 0x0600485A RID: 18522 RVA: 0x001307B3 File Offset: 0x0012E9B3
		internal TreeNode.TreeNodeImageIndexer StateImageIndexer
		{
			get
			{
				if (this.stateImageIndexer == null)
				{
					this.stateImageIndexer = new TreeNode.TreeNodeImageIndexer(this, TreeNode.TreeNodeImageIndexer.ImageListType.State);
				}
				return this.stateImageIndexer;
			}
		}

		// Token: 0x0600485B RID: 18523 RVA: 0x001307D0 File Offset: 0x0012E9D0
		public TreeNode()
		{
			this.treeNodeState = default(BitVector32);
		}

		// Token: 0x0600485C RID: 18524 RVA: 0x001307EF File Offset: 0x0012E9EF
		internal TreeNode(TreeView treeView) : this()
		{
			this.treeView = treeView;
		}

		// Token: 0x0600485D RID: 18525 RVA: 0x001307FE File Offset: 0x0012E9FE
		public TreeNode(string text) : this()
		{
			this.text = text;
		}

		// Token: 0x0600485E RID: 18526 RVA: 0x0013080D File Offset: 0x0012EA0D
		public TreeNode(string text, TreeNode[] children) : this()
		{
			this.text = text;
			this.Nodes.AddRange(children);
		}

		// Token: 0x0600485F RID: 18527 RVA: 0x00130828 File Offset: 0x0012EA28
		public TreeNode(string text, int imageIndex, int selectedImageIndex) : this()
		{
			this.text = text;
			this.ImageIndexer.Index = imageIndex;
			this.SelectedImageIndexer.Index = selectedImageIndex;
		}

		// Token: 0x06004860 RID: 18528 RVA: 0x0013084F File Offset: 0x0012EA4F
		public TreeNode(string text, int imageIndex, int selectedImageIndex, TreeNode[] children) : this()
		{
			this.text = text;
			this.ImageIndexer.Index = imageIndex;
			this.SelectedImageIndexer.Index = selectedImageIndex;
			this.Nodes.AddRange(children);
		}

		// Token: 0x06004861 RID: 18529 RVA: 0x00130883 File Offset: 0x0012EA83
		protected TreeNode(SerializationInfo serializationInfo, StreamingContext context) : this()
		{
			this.Deserialize(serializationInfo, context);
		}

		// Token: 0x170011B6 RID: 4534
		// (get) Token: 0x06004862 RID: 18530 RVA: 0x00130893 File Offset: 0x0012EA93
		// (set) Token: 0x06004863 RID: 18531 RVA: 0x001308B0 File Offset: 0x0012EAB0
		[SRCategory("CatAppearance")]
		[SRDescription("TreeNodeBackColorDescr")]
		public Color BackColor
		{
			get
			{
				if (this.propBag == null)
				{
					return Color.Empty;
				}
				return this.propBag.BackColor;
			}
			set
			{
				Color backColor = this.BackColor;
				if (value.IsEmpty)
				{
					if (this.propBag != null)
					{
						this.propBag.BackColor = Color.Empty;
						this.RemovePropBagIfEmpty();
					}
					if (!backColor.IsEmpty)
					{
						this.InvalidateHostTree();
					}
					return;
				}
				if (this.propBag == null)
				{
					this.propBag = new OwnerDrawPropertyBag();
				}
				this.propBag.BackColor = value;
				if (!value.Equals(backColor))
				{
					this.InvalidateHostTree();
				}
			}
		}

		// Token: 0x170011B7 RID: 4535
		// (get) Token: 0x06004864 RID: 18532 RVA: 0x00130938 File Offset: 0x0012EB38
		[Browsable(false)]
		public unsafe Rectangle Bounds
		{
			get
			{
				TreeView treeView = this.TreeView;
				if (treeView == null || treeView.IsDisposed)
				{
					return Rectangle.Empty;
				}
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				*(IntPtr*)(&rect.left) = this.Handle;
				if ((int)UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), 4356, 1, ref rect) == 0)
				{
					return Rectangle.Empty;
				}
				return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
			}
		}

		// Token: 0x170011B8 RID: 4536
		// (get) Token: 0x06004865 RID: 18533 RVA: 0x001309B8 File Offset: 0x0012EBB8
		internal unsafe Rectangle RowBounds
		{
			get
			{
				TreeView treeView = this.TreeView;
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				*(IntPtr*)(&rect.left) = this.Handle;
				if (treeView == null || treeView.IsDisposed)
				{
					return Rectangle.Empty;
				}
				if ((int)UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), 4356, 0, ref rect) == 0)
				{
					return Rectangle.Empty;
				}
				return Rectangle.FromLTRB(rect.left, rect.top, rect.right, rect.bottom);
			}
		}

		// Token: 0x170011B9 RID: 4537
		// (get) Token: 0x06004866 RID: 18534 RVA: 0x00130A37 File Offset: 0x0012EC37
		// (set) Token: 0x06004867 RID: 18535 RVA: 0x00130A45 File Offset: 0x0012EC45
		internal bool CheckedStateInternal
		{
			get
			{
				return this.treeNodeState[1];
			}
			set
			{
				this.treeNodeState[1] = value;
			}
		}

		// Token: 0x170011BA RID: 4538
		// (get) Token: 0x06004868 RID: 18536 RVA: 0x00130A54 File Offset: 0x0012EC54
		// (set) Token: 0x06004869 RID: 18537 RVA: 0x00130A5C File Offset: 0x0012EC5C
		internal bool CheckedInternal
		{
			get
			{
				return this.CheckedStateInternal;
			}
			set
			{
				this.CheckedStateInternal = value;
				if (this.handle == IntPtr.Zero)
				{
					return;
				}
				TreeView treeView = this.TreeView;
				if (treeView == null || !treeView.IsHandleCreated || treeView.IsDisposed)
				{
					return;
				}
				NativeMethods.TV_ITEM tv_ITEM = default(NativeMethods.TV_ITEM);
				tv_ITEM.mask = 24;
				tv_ITEM.hItem = this.handle;
				tv_ITEM.stateMask = 61440;
				tv_ITEM.state |= (value ? 8192 : 4096);
				UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), NativeMethods.TVM_SETITEM, 0, ref tv_ITEM);
			}
		}

		// Token: 0x170011BB RID: 4539
		// (get) Token: 0x0600486A RID: 18538 RVA: 0x00130AFC File Offset: 0x0012ECFC
		// (set) Token: 0x0600486B RID: 18539 RVA: 0x00130B04 File Offset: 0x0012ED04
		[SRCategory("CatBehavior")]
		[SRDescription("TreeNodeCheckedDescr")]
		[DefaultValue(false)]
		public bool Checked
		{
			get
			{
				return this.CheckedInternal;
			}
			set
			{
				TreeView treeView = this.TreeView;
				if (treeView != null)
				{
					if (!treeView.TreeViewBeforeCheck(this, TreeViewAction.Unknown))
					{
						this.CheckedInternal = value;
						treeView.TreeViewAfterCheck(this, TreeViewAction.Unknown);
						return;
					}
				}
				else
				{
					this.CheckedInternal = value;
				}
			}
		}

		// Token: 0x170011BC RID: 4540
		// (get) Token: 0x0600486C RID: 18540 RVA: 0x00130B3E File Offset: 0x0012ED3E
		// (set) Token: 0x0600486D RID: 18541 RVA: 0x00130B46 File Offset: 0x0012ED46
		[SRCategory("CatBehavior")]
		[DefaultValue(null)]
		[SRDescription("ControlContextMenuDescr")]
		public virtual ContextMenu ContextMenu
		{
			get
			{
				return this.contextMenu;
			}
			set
			{
				this.contextMenu = value;
			}
		}

		// Token: 0x170011BD RID: 4541
		// (get) Token: 0x0600486E RID: 18542 RVA: 0x00130B4F File Offset: 0x0012ED4F
		// (set) Token: 0x0600486F RID: 18543 RVA: 0x00130B57 File Offset: 0x0012ED57
		[SRCategory("CatBehavior")]
		[DefaultValue(null)]
		[SRDescription("ControlContextMenuDescr")]
		public virtual ContextMenuStrip ContextMenuStrip
		{
			get
			{
				return this.contextMenuStrip;
			}
			set
			{
				this.contextMenuStrip = value;
			}
		}

		// Token: 0x170011BE RID: 4542
		// (get) Token: 0x06004870 RID: 18544 RVA: 0x00130B60 File Offset: 0x0012ED60
		[Browsable(false)]
		public TreeNode FirstNode
		{
			get
			{
				if (this.childCount == 0)
				{
					return null;
				}
				return this.children[0];
			}
		}

		// Token: 0x170011BF RID: 4543
		// (get) Token: 0x06004871 RID: 18545 RVA: 0x00130B74 File Offset: 0x0012ED74
		private TreeNode FirstVisibleParent
		{
			get
			{
				TreeNode treeNode = this;
				while (treeNode != null && treeNode.Bounds.IsEmpty)
				{
					treeNode = treeNode.Parent;
				}
				return treeNode;
			}
		}

		// Token: 0x170011C0 RID: 4544
		// (get) Token: 0x06004872 RID: 18546 RVA: 0x00130BA0 File Offset: 0x0012EDA0
		// (set) Token: 0x06004873 RID: 18547 RVA: 0x00130BBC File Offset: 0x0012EDBC
		[SRCategory("CatAppearance")]
		[SRDescription("TreeNodeForeColorDescr")]
		public Color ForeColor
		{
			get
			{
				if (this.propBag == null)
				{
					return Color.Empty;
				}
				return this.propBag.ForeColor;
			}
			set
			{
				Color foreColor = this.ForeColor;
				if (value.IsEmpty)
				{
					if (this.propBag != null)
					{
						this.propBag.ForeColor = Color.Empty;
						this.RemovePropBagIfEmpty();
					}
					if (!foreColor.IsEmpty)
					{
						this.InvalidateHostTree();
					}
					return;
				}
				if (this.propBag == null)
				{
					this.propBag = new OwnerDrawPropertyBag();
				}
				this.propBag.ForeColor = value;
				if (!value.Equals(foreColor))
				{
					this.InvalidateHostTree();
				}
			}
		}

		// Token: 0x170011C1 RID: 4545
		// (get) Token: 0x06004874 RID: 18548 RVA: 0x00130C44 File Offset: 0x0012EE44
		[Browsable(false)]
		public string FullPath
		{
			get
			{
				TreeView treeView = this.TreeView;
				if (treeView != null)
				{
					StringBuilder stringBuilder = new StringBuilder();
					this.GetFullPath(stringBuilder, treeView.PathSeparator);
					return stringBuilder.ToString();
				}
				throw new InvalidOperationException(SR.GetString("TreeNodeNoParent"));
			}
		}

		// Token: 0x170011C2 RID: 4546
		// (get) Token: 0x06004875 RID: 18549 RVA: 0x00130C84 File Offset: 0x0012EE84
		[Browsable(false)]
		public IntPtr Handle
		{
			get
			{
				if (this.handle == IntPtr.Zero)
				{
					this.TreeView.CreateControl();
				}
				return this.handle;
			}
		}

		// Token: 0x170011C3 RID: 4547
		// (get) Token: 0x06004876 RID: 18550 RVA: 0x00130CA9 File Offset: 0x0012EEA9
		// (set) Token: 0x06004877 RID: 18551 RVA: 0x00130CB6 File Offset: 0x0012EEB6
		[Localizable(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("TreeNodeImageIndexDescr")]
		[TypeConverter(typeof(TreeViewImageIndexConverter))]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RefreshProperties(RefreshProperties.Repaint)]
		[DefaultValue(-1)]
		[RelatedImageList("TreeView.ImageList")]
		public int ImageIndex
		{
			get
			{
				return this.ImageIndexer.Index;
			}
			set
			{
				this.ImageIndexer.Index = value;
				this.UpdateNode(2);
			}
		}

		// Token: 0x170011C4 RID: 4548
		// (get) Token: 0x06004878 RID: 18552 RVA: 0x00130CCB File Offset: 0x0012EECB
		// (set) Token: 0x06004879 RID: 18553 RVA: 0x00130CD8 File Offset: 0x0012EED8
		[Localizable(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("TreeNodeImageKeyDescr")]
		[TypeConverter(typeof(TreeViewImageKeyConverter))]
		[DefaultValue("")]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RefreshProperties(RefreshProperties.Repaint)]
		[RelatedImageList("TreeView.ImageList")]
		public string ImageKey
		{
			get
			{
				return this.ImageIndexer.Key;
			}
			set
			{
				this.ImageIndexer.Key = value;
				this.UpdateNode(2);
			}
		}

		// Token: 0x170011C5 RID: 4549
		// (get) Token: 0x0600487A RID: 18554 RVA: 0x00130CED File Offset: 0x0012EEED
		[SRCategory("CatBehavior")]
		[SRDescription("TreeNodeIndexDescr")]
		public int Index
		{
			get
			{
				return this.index;
			}
		}

		// Token: 0x170011C6 RID: 4550
		// (get) Token: 0x0600487B RID: 18555 RVA: 0x00130CF8 File Offset: 0x0012EEF8
		[Browsable(false)]
		public bool IsEditing
		{
			get
			{
				TreeView treeView = this.TreeView;
				return treeView != null && treeView.editNode == this;
			}
		}

		// Token: 0x170011C7 RID: 4551
		// (get) Token: 0x0600487C RID: 18556 RVA: 0x00130D1A File Offset: 0x0012EF1A
		[Browsable(false)]
		public bool IsExpanded
		{
			get
			{
				if (this.handle == IntPtr.Zero)
				{
					return this.expandOnRealization;
				}
				return (this.State & 32) != 0;
			}
		}

		// Token: 0x170011C8 RID: 4552
		// (get) Token: 0x0600487D RID: 18557 RVA: 0x00130D41 File Offset: 0x0012EF41
		[Browsable(false)]
		public bool IsSelected
		{
			get
			{
				return !(this.handle == IntPtr.Zero) && (this.State & 2) != 0;
			}
		}

		// Token: 0x170011C9 RID: 4553
		// (get) Token: 0x0600487E RID: 18558 RVA: 0x00130D64 File Offset: 0x0012EF64
		[Browsable(false)]
		public unsafe bool IsVisible
		{
			get
			{
				if (this.handle == IntPtr.Zero)
				{
					return false;
				}
				TreeView treeView = this.TreeView;
				if (treeView.IsDisposed)
				{
					return false;
				}
				NativeMethods.RECT rect = default(NativeMethods.RECT);
				*(IntPtr*)(&rect.left) = this.Handle;
				bool flag = (int)UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), 4356, 1, ref rect) != 0;
				if (flag)
				{
					Size clientSize = treeView.ClientSize;
					flag = (rect.bottom > 0 && rect.right > 0 && rect.top < clientSize.Height && rect.left < clientSize.Width);
				}
				return flag;
			}
		}

		// Token: 0x170011CA RID: 4554
		// (get) Token: 0x0600487F RID: 18559 RVA: 0x00130E0E File Offset: 0x0012F00E
		[Browsable(false)]
		public TreeNode LastNode
		{
			get
			{
				if (this.childCount == 0)
				{
					return null;
				}
				return this.children[this.childCount - 1];
			}
		}

		// Token: 0x170011CB RID: 4555
		// (get) Token: 0x06004880 RID: 18560 RVA: 0x00130E29 File Offset: 0x0012F029
		[Browsable(false)]
		public int Level
		{
			get
			{
				if (this.Parent == null)
				{
					return 0;
				}
				return this.Parent.Level + 1;
			}
		}

		// Token: 0x170011CC RID: 4556
		// (get) Token: 0x06004881 RID: 18561 RVA: 0x00130E42 File Offset: 0x0012F042
		[Browsable(false)]
		public TreeNode NextNode
		{
			get
			{
				if (this.index + 1 < this.parent.Nodes.Count)
				{
					return this.parent.Nodes[this.index + 1];
				}
				return null;
			}
		}

		// Token: 0x170011CD RID: 4557
		// (get) Token: 0x06004882 RID: 18562 RVA: 0x00130E78 File Offset: 0x0012F078
		[Browsable(false)]
		public TreeNode NextVisibleNode
		{
			get
			{
				TreeView treeView = this.TreeView;
				if (treeView == null || treeView.IsDisposed)
				{
					return null;
				}
				TreeNode firstVisibleParent = this.FirstVisibleParent;
				if (firstVisibleParent != null)
				{
					IntPtr value = UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), 4362, 6, firstVisibleParent.Handle);
					if (value != IntPtr.Zero)
					{
						return treeView.NodeFromHandle(value);
					}
				}
				return null;
			}
		}

		// Token: 0x170011CE RID: 4558
		// (get) Token: 0x06004883 RID: 18563 RVA: 0x00130ED7 File Offset: 0x0012F0D7
		// (set) Token: 0x06004884 RID: 18564 RVA: 0x00130EF0 File Offset: 0x0012F0F0
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("TreeNodeNodeFontDescr")]
		[DefaultValue(null)]
		public Font NodeFont
		{
			get
			{
				if (this.propBag == null)
				{
					return null;
				}
				return this.propBag.Font;
			}
			set
			{
				Font nodeFont = this.NodeFont;
				if (value == null)
				{
					if (this.propBag != null)
					{
						this.propBag.Font = null;
						this.RemovePropBagIfEmpty();
					}
					if (nodeFont != null)
					{
						this.InvalidateHostTree();
					}
					return;
				}
				if (this.propBag == null)
				{
					this.propBag = new OwnerDrawPropertyBag();
				}
				this.propBag.Font = value;
				if (!value.Equals(nodeFont))
				{
					this.InvalidateHostTree();
				}
			}
		}

		// Token: 0x170011CF RID: 4559
		// (get) Token: 0x06004885 RID: 18565 RVA: 0x00130F59 File Offset: 0x0012F159
		[ListBindable(false)]
		[Browsable(false)]
		public TreeNodeCollection Nodes
		{
			get
			{
				if (this.nodes == null)
				{
					this.nodes = new TreeNodeCollection(this);
				}
				return this.nodes;
			}
		}

		// Token: 0x170011D0 RID: 4560
		// (get) Token: 0x06004886 RID: 18566 RVA: 0x00130F78 File Offset: 0x0012F178
		[Browsable(false)]
		public TreeNode Parent
		{
			get
			{
				TreeView treeView = this.TreeView;
				if (treeView != null && this.parent == treeView.root)
				{
					return null;
				}
				return this.parent;
			}
		}

		// Token: 0x170011D1 RID: 4561
		// (get) Token: 0x06004887 RID: 18567 RVA: 0x00130FA8 File Offset: 0x0012F1A8
		[Browsable(false)]
		public TreeNode PrevNode
		{
			get
			{
				int num = this.index;
				int fixedIndex = this.parent.Nodes.FixedIndex;
				if (fixedIndex > 0)
				{
					num = fixedIndex;
				}
				if (num > 0 && num <= this.parent.Nodes.Count)
				{
					return this.parent.Nodes[num - 1];
				}
				return null;
			}
		}

		// Token: 0x170011D2 RID: 4562
		// (get) Token: 0x06004888 RID: 18568 RVA: 0x00131000 File Offset: 0x0012F200
		[Browsable(false)]
		public TreeNode PrevVisibleNode
		{
			get
			{
				TreeNode firstVisibleParent = this.FirstVisibleParent;
				TreeView treeView = this.TreeView;
				if (firstVisibleParent != null)
				{
					if (treeView == null || treeView.IsDisposed)
					{
						return null;
					}
					IntPtr value = UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), 4362, 7, firstVisibleParent.Handle);
					if (value != IntPtr.Zero)
					{
						return treeView.NodeFromHandle(value);
					}
				}
				return null;
			}
		}

		// Token: 0x170011D3 RID: 4563
		// (get) Token: 0x06004889 RID: 18569 RVA: 0x0013105F File Offset: 0x0012F25F
		// (set) Token: 0x0600488A RID: 18570 RVA: 0x0013106C File Offset: 0x0012F26C
		[Localizable(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("TreeNodeSelectedImageIndexDescr")]
		[TypeConverter(typeof(TreeViewImageIndexConverter))]
		[DefaultValue(-1)]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RelatedImageList("TreeView.ImageList")]
		public int SelectedImageIndex
		{
			get
			{
				return this.SelectedImageIndexer.Index;
			}
			set
			{
				this.SelectedImageIndexer.Index = value;
				this.UpdateNode(32);
			}
		}

		// Token: 0x170011D4 RID: 4564
		// (get) Token: 0x0600488B RID: 18571 RVA: 0x00131082 File Offset: 0x0012F282
		// (set) Token: 0x0600488C RID: 18572 RVA: 0x0013108F File Offset: 0x0012F28F
		[Localizable(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("TreeNodeSelectedImageKeyDescr")]
		[TypeConverter(typeof(TreeViewImageKeyConverter))]
		[DefaultValue("")]
		[RefreshProperties(RefreshProperties.Repaint)]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RelatedImageList("TreeView.ImageList")]
		public string SelectedImageKey
		{
			get
			{
				return this.SelectedImageIndexer.Key;
			}
			set
			{
				this.SelectedImageIndexer.Key = value;
				this.UpdateNode(32);
			}
		}

		// Token: 0x170011D5 RID: 4565
		// (get) Token: 0x0600488D RID: 18573 RVA: 0x001310A8 File Offset: 0x0012F2A8
		internal int State
		{
			get
			{
				if (this.handle == IntPtr.Zero)
				{
					return 0;
				}
				TreeView treeView = this.TreeView;
				if (treeView == null || treeView.IsDisposed)
				{
					return 0;
				}
				NativeMethods.TV_ITEM tv_ITEM = default(NativeMethods.TV_ITEM);
				tv_ITEM.hItem = this.Handle;
				tv_ITEM.mask = 24;
				tv_ITEM.stateMask = 34;
				UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), NativeMethods.TVM_GETITEM, 0, ref tv_ITEM);
				return tv_ITEM.state;
			}
		}

		// Token: 0x170011D6 RID: 4566
		// (get) Token: 0x0600488E RID: 18574 RVA: 0x00131124 File Offset: 0x0012F324
		// (set) Token: 0x0600488F RID: 18575 RVA: 0x00131131 File Offset: 0x0012F331
		[Localizable(true)]
		[SRCategory("CatBehavior")]
		[SRDescription("TreeNodeStateImageKeyDescr")]
		[TypeConverter(typeof(ImageKeyConverter))]
		[DefaultValue("")]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RefreshProperties(RefreshProperties.Repaint)]
		[RelatedImageList("TreeView.StateImageList")]
		public string StateImageKey
		{
			get
			{
				return this.StateImageIndexer.Key;
			}
			set
			{
				if (this.StateImageIndexer.Key != value)
				{
					this.StateImageIndexer.Key = value;
					if (this.treeView != null && !this.treeView.CheckBoxes)
					{
						this.UpdateNode(8);
					}
				}
			}
		}

		// Token: 0x170011D7 RID: 4567
		// (get) Token: 0x06004890 RID: 18576 RVA: 0x0013116E File Offset: 0x0012F36E
		// (set) Token: 0x06004891 RID: 18577 RVA: 0x00131194 File Offset: 0x0012F394
		[Localizable(true)]
		[TypeConverter(typeof(NoneExcludedImageIndexConverter))]
		[DefaultValue(-1)]
		[SRCategory("CatBehavior")]
		[SRDescription("TreeNodeStateImageIndexDescr")]
		[Editor("System.Windows.Forms.Design.ImageIndexEditor, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a", typeof(UITypeEditor))]
		[RefreshProperties(RefreshProperties.Repaint)]
		[RelatedImageList("TreeView.StateImageList")]
		public int StateImageIndex
		{
			get
			{
				if (this.treeView != null && this.treeView.StateImageList != null)
				{
					return this.StateImageIndexer.Index;
				}
				return -1;
			}
			set
			{
				if (value < -1 || value > 14)
				{
					throw new ArgumentOutOfRangeException("StateImageIndex", SR.GetString("InvalidArgument", new object[]
					{
						"StateImageIndex",
						value.ToString(CultureInfo.CurrentCulture)
					}));
				}
				this.StateImageIndexer.Index = value;
				if (this.treeView != null && !this.treeView.CheckBoxes)
				{
					this.UpdateNode(8);
				}
			}
		}

		// Token: 0x170011D8 RID: 4568
		// (get) Token: 0x06004892 RID: 18578 RVA: 0x00131204 File Offset: 0x0012F404
		// (set) Token: 0x06004893 RID: 18579 RVA: 0x0013120C File Offset: 0x0012F40C
		[SRCategory("CatData")]
		[Localizable(false)]
		[Bindable(true)]
		[SRDescription("ControlTagDescr")]
		[DefaultValue(null)]
		[TypeConverter(typeof(StringConverter))]
		public object Tag
		{
			get
			{
				return this.userData;
			}
			set
			{
				this.userData = value;
			}
		}

		// Token: 0x170011D9 RID: 4569
		// (get) Token: 0x06004894 RID: 18580 RVA: 0x00131215 File Offset: 0x0012F415
		// (set) Token: 0x06004895 RID: 18581 RVA: 0x0013122B File Offset: 0x0012F42B
		[Localizable(true)]
		[SRCategory("CatAppearance")]
		[SRDescription("TreeNodeTextDescr")]
		public string Text
		{
			get
			{
				if (this.text != null)
				{
					return this.text;
				}
				return "";
			}
			set
			{
				this.text = value;
				this.UpdateNode(1);
			}
		}

		// Token: 0x170011DA RID: 4570
		// (get) Token: 0x06004896 RID: 18582 RVA: 0x0013123B File Offset: 0x0012F43B
		// (set) Token: 0x06004897 RID: 18583 RVA: 0x00131243 File Offset: 0x0012F443
		[Localizable(false)]
		[SRCategory("CatAppearance")]
		[SRDescription("TreeNodeToolTipTextDescr")]
		[DefaultValue("")]
		public string ToolTipText
		{
			get
			{
				return this.toolTipText;
			}
			set
			{
				this.toolTipText = value;
			}
		}

		// Token: 0x170011DB RID: 4571
		// (get) Token: 0x06004898 RID: 18584 RVA: 0x0013124C File Offset: 0x0012F44C
		// (set) Token: 0x06004899 RID: 18585 RVA: 0x00131262 File Offset: 0x0012F462
		[SRCategory("CatAppearance")]
		[SRDescription("TreeNodeNodeNameDescr")]
		public string Name
		{
			get
			{
				if (this.name != null)
				{
					return this.name;
				}
				return "";
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x170011DC RID: 4572
		// (get) Token: 0x0600489A RID: 18586 RVA: 0x0013126B File Offset: 0x0012F46B
		[Browsable(false)]
		public TreeView TreeView
		{
			get
			{
				if (this.treeView == null)
				{
					this.treeView = this.FindTreeView();
				}
				return this.treeView;
			}
		}

		// Token: 0x0600489B RID: 18587 RVA: 0x00131288 File Offset: 0x0012F488
		internal int AddSorted(TreeNode node)
		{
			int result = 0;
			string @string = node.Text;
			TreeView treeView = this.TreeView;
			if (this.childCount > 0)
			{
				if (treeView.TreeViewNodeSorter == null)
				{
					CompareInfo compareInfo = Application.CurrentCulture.CompareInfo;
					if (compareInfo.Compare(this.children[this.childCount - 1].Text, @string) <= 0)
					{
						result = this.childCount;
					}
					else
					{
						int i = 0;
						int num = this.childCount;
						while (i < num)
						{
							int num2 = (i + num) / 2;
							if (compareInfo.Compare(this.children[num2].Text, @string) <= 0)
							{
								i = num2 + 1;
							}
							else
							{
								num = num2;
							}
						}
						result = i;
					}
				}
				else
				{
					IComparer treeViewNodeSorter = treeView.TreeViewNodeSorter;
					int i = 0;
					int num = this.childCount;
					while (i < num)
					{
						int num2 = (i + num) / 2;
						if (treeViewNodeSorter.Compare(this.children[num2], node) <= 0)
						{
							i = num2 + 1;
						}
						else
						{
							num = num2;
						}
					}
					result = i;
				}
			}
			node.SortChildren(treeView);
			this.InsertNodeAt(result, node);
			return result;
		}

		// Token: 0x0600489C RID: 18588 RVA: 0x00131377 File Offset: 0x0012F577
		public static TreeNode FromHandle(TreeView tree, IntPtr handle)
		{
			IntSecurity.ControlFromHandleOrLocation.Demand();
			return tree.NodeFromHandle(handle);
		}

		// Token: 0x0600489D RID: 18589 RVA: 0x0013138C File Offset: 0x0012F58C
		private void SortChildren(TreeView parentTreeView)
		{
			if (this.childCount > 0)
			{
				TreeNode[] array = new TreeNode[this.childCount];
				if (parentTreeView == null || parentTreeView.TreeViewNodeSorter == null)
				{
					CompareInfo compareInfo = Application.CurrentCulture.CompareInfo;
					for (int i = 0; i < this.childCount; i++)
					{
						int num = -1;
						for (int j = 0; j < this.childCount; j++)
						{
							if (this.children[j] != null)
							{
								if (num == -1)
								{
									num = j;
								}
								else if (compareInfo.Compare(this.children[j].Text, this.children[num].Text) <= 0)
								{
									num = j;
								}
							}
						}
						array[i] = this.children[num];
						this.children[num] = null;
						array[i].index = i;
						array[i].SortChildren(parentTreeView);
					}
					this.children = array;
					return;
				}
				IComparer treeViewNodeSorter = parentTreeView.TreeViewNodeSorter;
				for (int k = 0; k < this.childCount; k++)
				{
					int num2 = -1;
					for (int l = 0; l < this.childCount; l++)
					{
						if (this.children[l] != null)
						{
							if (num2 == -1)
							{
								num2 = l;
							}
							else if (treeViewNodeSorter.Compare(this.children[l], this.children[num2]) <= 0)
							{
								num2 = l;
							}
						}
					}
					array[k] = this.children[num2];
					this.children[num2] = null;
					array[k].index = k;
					array[k].SortChildren(parentTreeView);
				}
				this.children = array;
			}
		}

		// Token: 0x0600489E RID: 18590 RVA: 0x00131504 File Offset: 0x0012F704
		public void BeginEdit()
		{
			if (this.handle != IntPtr.Zero)
			{
				TreeView treeView = this.TreeView;
				if (!treeView.LabelEdit)
				{
					throw new InvalidOperationException(SR.GetString("TreeNodeBeginEditFailed"));
				}
				if (!treeView.Focused)
				{
					treeView.FocusInternal();
				}
				UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), NativeMethods.TVM_EDITLABEL, 0, this.handle);
			}
		}

		// Token: 0x0600489F RID: 18591 RVA: 0x00131570 File Offset: 0x0012F770
		internal void Clear()
		{
			bool flag = false;
			TreeView treeView = this.TreeView;
			try
			{
				if (treeView != null)
				{
					treeView.nodesCollectionClear = true;
					if (treeView != null && this.childCount > 200)
					{
						flag = true;
						treeView.BeginUpdate();
					}
				}
				while (this.childCount > 0)
				{
					this.children[this.childCount - 1].Remove(true);
				}
				this.children = null;
				if (treeView != null && flag)
				{
					treeView.EndUpdate();
				}
			}
			finally
			{
				if (treeView != null)
				{
					treeView.nodesCollectionClear = false;
				}
				this.nodesCleared = true;
			}
		}

		// Token: 0x060048A0 RID: 18592 RVA: 0x00131604 File Offset: 0x0012F804
		public virtual object Clone()
		{
			Type type = base.GetType();
			TreeNode treeNode;
			if (type == typeof(TreeNode))
			{
				treeNode = new TreeNode(this.text, this.ImageIndexer.Index, this.SelectedImageIndexer.Index);
			}
			else
			{
				treeNode = (TreeNode)Activator.CreateInstance(type);
			}
			treeNode.Text = this.text;
			treeNode.Name = this.name;
			treeNode.ImageIndexer.Index = this.ImageIndexer.Index;
			treeNode.SelectedImageIndexer.Index = this.SelectedImageIndexer.Index;
			treeNode.StateImageIndexer.Index = this.StateImageIndexer.Index;
			treeNode.ToolTipText = this.toolTipText;
			treeNode.ContextMenu = this.contextMenu;
			treeNode.ContextMenuStrip = this.contextMenuStrip;
			if (!string.IsNullOrEmpty(this.ImageIndexer.Key))
			{
				treeNode.ImageIndexer.Key = this.ImageIndexer.Key;
			}
			if (!string.IsNullOrEmpty(this.SelectedImageIndexer.Key))
			{
				treeNode.SelectedImageIndexer.Key = this.SelectedImageIndexer.Key;
			}
			if (!string.IsNullOrEmpty(this.StateImageIndexer.Key))
			{
				treeNode.StateImageIndexer.Key = this.StateImageIndexer.Key;
			}
			if (this.childCount > 0)
			{
				treeNode.children = new TreeNode[this.childCount];
				for (int i = 0; i < this.childCount; i++)
				{
					treeNode.Nodes.Add((TreeNode)this.children[i].Clone());
				}
			}
			if (this.propBag != null)
			{
				treeNode.propBag = OwnerDrawPropertyBag.Copy(this.propBag);
			}
			treeNode.Checked = this.Checked;
			treeNode.Tag = this.Tag;
			return treeNode;
		}

		// Token: 0x060048A1 RID: 18593 RVA: 0x001317D0 File Offset: 0x0012F9D0
		private void CollapseInternal(bool ignoreChildren)
		{
			TreeView treeView = this.TreeView;
			bool flag = false;
			this.collapseOnRealization = false;
			this.expandOnRealization = false;
			if (treeView == null || !treeView.IsHandleCreated)
			{
				this.collapseOnRealization = true;
				return;
			}
			if (ignoreChildren)
			{
				this.DoCollapse(treeView);
			}
			else
			{
				if (!ignoreChildren && this.childCount > 0)
				{
					for (int i = 0; i < this.childCount; i++)
					{
						if (treeView.SelectedNode == this.children[i])
						{
							flag = true;
						}
						this.children[i].DoCollapse(treeView);
						this.children[i].Collapse();
					}
				}
				this.DoCollapse(treeView);
			}
			if (flag)
			{
				treeView.SelectedNode = this;
			}
			treeView.Invalidate();
			this.collapseOnRealization = false;
		}

		// Token: 0x060048A2 RID: 18594 RVA: 0x0013187B File Offset: 0x0012FA7B
		public void Collapse(bool ignoreChildren)
		{
			this.CollapseInternal(ignoreChildren);
		}

		// Token: 0x060048A3 RID: 18595 RVA: 0x00131884 File Offset: 0x0012FA84
		public void Collapse()
		{
			this.CollapseInternal(false);
		}

		// Token: 0x060048A4 RID: 18596 RVA: 0x00131890 File Offset: 0x0012FA90
		private void DoCollapse(TreeView tv)
		{
			if ((this.State & 32) != 0)
			{
				TreeViewCancelEventArgs treeViewCancelEventArgs = new TreeViewCancelEventArgs(this, false, TreeViewAction.Collapse);
				tv.OnBeforeCollapse(treeViewCancelEventArgs);
				if (!treeViewCancelEventArgs.Cancel)
				{
					UnsafeNativeMethods.SendMessage(new HandleRef(tv, tv.Handle), 4354, 1, this.Handle);
					tv.OnAfterCollapse(new TreeViewEventArgs(this));
				}
			}
		}

		// Token: 0x060048A5 RID: 18597 RVA: 0x001318EC File Offset: 0x0012FAEC
		protected virtual void Deserialize(SerializationInfo serializationInfo, StreamingContext context)
		{
			int num = 0;
			int num2 = -1;
			string text = null;
			int num3 = -1;
			string text2 = null;
			int num4 = -1;
			string text3 = null;
			foreach (SerializationEntry serializationEntry in serializationInfo)
			{
				string text4 = serializationEntry.Name;
				uint num5 = <PrivateImplementationDetails>.ComputeStringHash(text4);
				if (num5 <= 1606954993U)
				{
					if (num5 <= 759659912U)
					{
						if (num5 != 266367750U)
						{
							if (num5 != 717129186U)
							{
								if (num5 == 759659912U)
								{
									if (text4 == "SelectedImageKey")
									{
										text2 = serializationInfo.GetString(serializationEntry.Name);
									}
								}
							}
							else if (text4 == "UserData")
							{
								this.userData = serializationEntry.Value;
							}
						}
						else if (text4 == "Name")
						{
							this.Name = serializationInfo.GetString(serializationEntry.Name);
						}
					}
					else if (num5 != 1011358670U)
					{
						if (num5 != 1041509726U)
						{
							if (num5 == 1606954993U)
							{
								if (text4 == "ImageKey")
								{
									text = serializationInfo.GetString(serializationEntry.Name);
								}
							}
						}
						else if (text4 == "Text")
						{
							this.Text = serializationInfo.GetString(serializationEntry.Name);
						}
					}
					else if (text4 == "PropBag")
					{
						this.propBag = (OwnerDrawPropertyBag)serializationInfo.GetValue(serializationEntry.Name, typeof(OwnerDrawPropertyBag));
					}
				}
				else if (num5 <= 2569126364U)
				{
					if (num5 != 2041341998U)
					{
						if (num5 != 2143661137U)
						{
							if (num5 == 2569126364U)
							{
								if (text4 == "ChildCount")
								{
									num = serializationInfo.GetInt32(serializationEntry.Name);
								}
							}
						}
						else if (text4 == "StateImageIndex")
						{
							num4 = serializationInfo.GetInt32(serializationEntry.Name);
						}
					}
					else if (text4 == "ImageIndex")
					{
						num2 = serializationInfo.GetInt32(serializationEntry.Name);
					}
				}
				else if (num5 <= 3441588130U)
				{
					if (num5 != 2606303591U)
					{
						if (num5 == 3441588130U)
						{
							if (text4 == "StateImageKey")
							{
								text3 = serializationInfo.GetString(serializationEntry.Name);
							}
						}
					}
					else if (text4 == "ToolTipText")
					{
						this.ToolTipText = serializationInfo.GetString(serializationEntry.Name);
					}
				}
				else if (num5 != 3693047415U)
				{
					if (num5 == 3931153718U)
					{
						if (text4 == "IsChecked")
						{
							this.CheckedStateInternal = serializationInfo.GetBoolean(serializationEntry.Name);
						}
					}
				}
				else if (text4 == "SelectedImageIndex")
				{
					num3 = serializationInfo.GetInt32(serializationEntry.Name);
				}
			}
			if (text != null)
			{
				this.ImageKey = text;
			}
			else if (num2 != -1)
			{
				this.ImageIndex = num2;
			}
			if (text2 != null)
			{
				this.SelectedImageKey = text2;
			}
			else if (num3 != -1)
			{
				this.SelectedImageIndex = num3;
			}
			if (text3 != null)
			{
				this.StateImageKey = text3;
			}
			else if (num4 != -1)
			{
				this.StateImageIndex = num4;
			}
			if (num > 0)
			{
				TreeNode[] array = new TreeNode[num];
				for (int i = 0; i < num; i++)
				{
					array[i] = (TreeNode)serializationInfo.GetValue("children" + i.ToString(), typeof(TreeNode));
				}
				this.Nodes.AddRange(array);
			}
		}

		// Token: 0x060048A6 RID: 18598 RVA: 0x00131CD8 File Offset: 0x0012FED8
		public void EndEdit(bool cancel)
		{
			TreeView treeView = this.TreeView;
			if (treeView == null || treeView.IsDisposed)
			{
				return;
			}
			UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), 4374, cancel ? 1 : 0, 0);
		}

		// Token: 0x060048A7 RID: 18599 RVA: 0x00131D18 File Offset: 0x0012FF18
		internal void EnsureCapacity(int num)
		{
			int num2 = num;
			if (num2 < 4)
			{
				num2 = 4;
			}
			if (this.children == null)
			{
				this.children = new TreeNode[num2];
				return;
			}
			if (this.childCount + num > this.children.Length)
			{
				int num3 = this.childCount + num;
				if (num == 1)
				{
					num3 = this.childCount * 2;
				}
				TreeNode[] destinationArray = new TreeNode[num3];
				Array.Copy(this.children, 0, destinationArray, 0, this.childCount);
				this.children = destinationArray;
			}
		}

		// Token: 0x060048A8 RID: 18600 RVA: 0x00131D8C File Offset: 0x0012FF8C
		private void EnsureStateImageValue()
		{
			if (this.treeView == null)
			{
				return;
			}
			if (this.treeView.CheckBoxes && this.treeView.StateImageList != null)
			{
				if (!string.IsNullOrEmpty(this.StateImageKey))
				{
					this.StateImageIndex = (this.Checked ? 1 : 0);
					this.StateImageKey = this.treeView.StateImageList.Images.Keys[this.StateImageIndex];
					return;
				}
				this.StateImageIndex = (this.Checked ? 1 : 0);
			}
		}

		// Token: 0x060048A9 RID: 18601 RVA: 0x00131E14 File Offset: 0x00130014
		public void EnsureVisible()
		{
			TreeView treeView = this.TreeView;
			if (treeView == null || treeView.IsDisposed)
			{
				return;
			}
			UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), 4372, 0, this.Handle);
		}

		// Token: 0x060048AA RID: 18602 RVA: 0x00131E54 File Offset: 0x00130054
		public void Expand()
		{
			TreeView treeView = this.TreeView;
			if (treeView == null || !treeView.IsHandleCreated)
			{
				this.expandOnRealization = true;
				return;
			}
			this.ResetExpandedState(treeView);
			if (!this.IsExpanded)
			{
				UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), 4354, 2, this.Handle);
			}
			this.expandOnRealization = false;
		}

		// Token: 0x060048AB RID: 18603 RVA: 0x00131EB0 File Offset: 0x001300B0
		public void ExpandAll()
		{
			this.Expand();
			for (int i = 0; i < this.childCount; i++)
			{
				this.children[i].ExpandAll();
			}
		}

		// Token: 0x060048AC RID: 18604 RVA: 0x00131EE4 File Offset: 0x001300E4
		internal TreeView FindTreeView()
		{
			TreeNode treeNode = this;
			while (treeNode.parent != null)
			{
				treeNode = treeNode.parent;
			}
			return treeNode.treeView;
		}

		// Token: 0x060048AD RID: 18605 RVA: 0x00131F0A File Offset: 0x0013010A
		private void GetFullPath(StringBuilder path, string pathSeparator)
		{
			if (this.parent != null)
			{
				this.parent.GetFullPath(path, pathSeparator);
				if (this.parent.parent != null)
				{
					path.Append(pathSeparator);
				}
				path.Append(this.text);
			}
		}

		// Token: 0x060048AE RID: 18606 RVA: 0x00131F44 File Offset: 0x00130144
		public int GetNodeCount(bool includeSubTrees)
		{
			int num = this.childCount;
			if (includeSubTrees)
			{
				for (int i = 0; i < this.childCount; i++)
				{
					num += this.children[i].GetNodeCount(true);
				}
			}
			return num;
		}

		// Token: 0x060048AF RID: 18607 RVA: 0x00131F80 File Offset: 0x00130180
		internal void InsertNodeAt(int index, TreeNode node)
		{
			this.EnsureCapacity(1);
			node.parent = this;
			node.index = index;
			for (int i = this.childCount; i > index; i--)
			{
				(this.children[i] = this.children[i - 1]).index = i;
			}
			this.children[index] = node;
			this.childCount++;
			node.Realize(false);
			if (this.TreeView != null && node == this.TreeView.selectedNode)
			{
				this.TreeView.SelectedNode = node;
			}
		}

		// Token: 0x060048B0 RID: 18608 RVA: 0x0013200E File Offset: 0x0013020E
		private void InvalidateHostTree()
		{
			if (this.treeView != null && this.treeView.IsHandleCreated)
			{
				this.treeView.Invalidate();
			}
		}

		// Token: 0x060048B1 RID: 18609 RVA: 0x00132030 File Offset: 0x00130230
		internal void Realize(bool insertFirst)
		{
			TreeView treeView = this.TreeView;
			if (treeView == null || !treeView.IsHandleCreated || treeView.IsDisposed)
			{
				return;
			}
			if (this.parent != null)
			{
				if (treeView.InvokeRequired)
				{
					throw new InvalidOperationException(SR.GetString("InvalidCrossThreadControlCall"));
				}
				NativeMethods.TV_INSERTSTRUCT tv_INSERTSTRUCT = default(NativeMethods.TV_INSERTSTRUCT);
				tv_INSERTSTRUCT.item_mask = TreeNode.insertMask;
				tv_INSERTSTRUCT.hParent = this.parent.handle;
				TreeNode prevNode = this.PrevNode;
				if (insertFirst || prevNode == null)
				{
					tv_INSERTSTRUCT.hInsertAfter = (IntPtr)(-65535);
				}
				else
				{
					tv_INSERTSTRUCT.hInsertAfter = prevNode.handle;
				}
				tv_INSERTSTRUCT.item_pszText = Marshal.StringToHGlobalAuto(this.text);
				tv_INSERTSTRUCT.item_iImage = ((this.ImageIndexer.ActualIndex == -1) ? treeView.ImageIndexer.ActualIndex : this.ImageIndexer.ActualIndex);
				tv_INSERTSTRUCT.item_iSelectedImage = ((this.SelectedImageIndexer.ActualIndex == -1) ? treeView.SelectedImageIndexer.ActualIndex : this.SelectedImageIndexer.ActualIndex);
				tv_INSERTSTRUCT.item_mask = 1;
				tv_INSERTSTRUCT.item_stateMask = 0;
				tv_INSERTSTRUCT.item_state = 0;
				if (treeView.CheckBoxes)
				{
					tv_INSERTSTRUCT.item_mask |= 8;
					tv_INSERTSTRUCT.item_stateMask |= 61440;
					tv_INSERTSTRUCT.item_state |= (this.CheckedInternal ? 8192 : 4096);
				}
				else if (treeView.StateImageList != null && this.StateImageIndexer.ActualIndex >= 0)
				{
					tv_INSERTSTRUCT.item_mask |= 8;
					tv_INSERTSTRUCT.item_stateMask = 61440;
					tv_INSERTSTRUCT.item_state = this.StateImageIndexer.ActualIndex + 1 << 12;
				}
				if (tv_INSERTSTRUCT.item_iImage >= 0)
				{
					tv_INSERTSTRUCT.item_mask |= 2;
				}
				if (tv_INSERTSTRUCT.item_iSelectedImage >= 0)
				{
					tv_INSERTSTRUCT.item_mask |= 32;
				}
				bool flag = false;
				IntPtr value = UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), 4367, 0, 0);
				if (value != IntPtr.Zero)
				{
					flag = true;
					UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), 4374, 0, 0);
				}
				this.handle = UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), NativeMethods.TVM_INSERTITEM, 0, ref tv_INSERTSTRUCT);
				treeView.nodeTable[this.handle] = this;
				this.UpdateNode(4);
				Marshal.FreeHGlobal(tv_INSERTSTRUCT.item_pszText);
				if (flag)
				{
					UnsafeNativeMethods.PostMessage(new HandleRef(treeView, treeView.Handle), NativeMethods.TVM_EDITLABEL, IntPtr.Zero, this.handle);
				}
				SafeNativeMethods.InvalidateRect(new HandleRef(treeView, treeView.Handle), null, false);
				if (this.parent.nodesCleared && (insertFirst || prevNode == null) && !treeView.Scrollable)
				{
					UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), 11, 1, 0);
					this.nodesCleared = false;
				}
			}
			for (int i = this.childCount - 1; i >= 0; i--)
			{
				this.children[i].Realize(true);
			}
			if (this.expandOnRealization)
			{
				this.Expand();
			}
			if (this.collapseOnRealization)
			{
				this.Collapse();
			}
		}

		// Token: 0x060048B2 RID: 18610 RVA: 0x00132347 File Offset: 0x00130547
		public void Remove()
		{
			this.Remove(true);
		}

		// Token: 0x060048B3 RID: 18611 RVA: 0x00132350 File Offset: 0x00130550
		internal void Remove(bool notify)
		{
			bool isExpanded = this.IsExpanded;
			for (int i = 0; i < this.childCount; i++)
			{
				this.children[i].Remove(false);
			}
			if (notify && this.parent != null)
			{
				for (int j = this.index; j < this.parent.childCount - 1; j++)
				{
					(this.parent.children[j] = this.parent.children[j + 1]).index = j;
				}
				this.parent.children[this.parent.childCount - 1] = null;
				this.parent.childCount--;
				this.parent = null;
			}
			this.expandOnRealization = isExpanded;
			TreeView treeView = this.TreeView;
			if (treeView == null || treeView.IsDisposed)
			{
				return;
			}
			if (this.handle != IntPtr.Zero)
			{
				if (notify && treeView.IsHandleCreated)
				{
					UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), 4353, 0, this.handle);
				}
				this.treeView.nodeTable.Remove(this.handle);
				this.handle = IntPtr.Zero;
			}
			this.treeView = null;
		}

		// Token: 0x060048B4 RID: 18612 RVA: 0x0013248B File Offset: 0x0013068B
		private void RemovePropBagIfEmpty()
		{
			if (this.propBag == null)
			{
				return;
			}
			if (this.propBag.IsEmpty())
			{
				this.propBag = null;
			}
		}

		// Token: 0x060048B5 RID: 18613 RVA: 0x001324AC File Offset: 0x001306AC
		private void ResetExpandedState(TreeView tv)
		{
			NativeMethods.TV_ITEM tv_ITEM = default(NativeMethods.TV_ITEM);
			tv_ITEM.mask = 24;
			tv_ITEM.hItem = this.handle;
			tv_ITEM.stateMask = 64;
			tv_ITEM.state = 0;
			UnsafeNativeMethods.SendMessage(new HandleRef(tv, tv.Handle), NativeMethods.TVM_SETITEM, 0, ref tv_ITEM);
		}

		// Token: 0x060048B6 RID: 18614 RVA: 0x00132502 File Offset: 0x00130702
		private bool ShouldSerializeBackColor()
		{
			return this.BackColor != Color.Empty;
		}

		// Token: 0x060048B7 RID: 18615 RVA: 0x00132514 File Offset: 0x00130714
		private bool ShouldSerializeForeColor()
		{
			return this.ForeColor != Color.Empty;
		}

		// Token: 0x060048B8 RID: 18616 RVA: 0x00132528 File Offset: 0x00130728
		[SecurityPermission(SecurityAction.Demand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		protected virtual void Serialize(SerializationInfo si, StreamingContext context)
		{
			if (this.propBag != null)
			{
				si.AddValue("PropBag", this.propBag, typeof(OwnerDrawPropertyBag));
			}
			si.AddValue("Text", this.text);
			si.AddValue("ToolTipText", this.toolTipText);
			si.AddValue("Name", this.Name);
			si.AddValue("IsChecked", this.treeNodeState[1]);
			si.AddValue("ImageIndex", this.ImageIndexer.Index);
			si.AddValue("ImageKey", this.ImageIndexer.Key);
			si.AddValue("SelectedImageIndex", this.SelectedImageIndexer.Index);
			si.AddValue("SelectedImageKey", this.SelectedImageIndexer.Key);
			if (this.treeView != null && this.treeView.StateImageList != null)
			{
				si.AddValue("StateImageIndex", this.StateImageIndexer.Index);
			}
			if (this.treeView != null && this.treeView.StateImageList != null)
			{
				si.AddValue("StateImageKey", this.StateImageIndexer.Key);
			}
			si.AddValue("ChildCount", this.childCount);
			if (this.childCount > 0)
			{
				for (int i = 0; i < this.childCount; i++)
				{
					si.AddValue("children" + i.ToString(), this.children[i], typeof(TreeNode));
				}
			}
			if (this.userData != null && this.userData.GetType().IsSerializable)
			{
				si.AddValue("UserData", this.userData, this.userData.GetType());
			}
		}

		// Token: 0x060048B9 RID: 18617 RVA: 0x001326DA File Offset: 0x001308DA
		public void Toggle()
		{
			if (this.IsExpanded)
			{
				this.Collapse();
				return;
			}
			this.Expand();
		}

		// Token: 0x060048BA RID: 18618 RVA: 0x001326F1 File Offset: 0x001308F1
		public override string ToString()
		{
			return "TreeNode: " + ((this.text == null) ? "" : this.text);
		}

		// Token: 0x060048BB RID: 18619 RVA: 0x00132714 File Offset: 0x00130914
		private void UpdateNode(int mask)
		{
			if (this.handle == IntPtr.Zero)
			{
				return;
			}
			TreeView treeView = this.TreeView;
			NativeMethods.TV_ITEM tv_ITEM = default(NativeMethods.TV_ITEM);
			tv_ITEM.mask = (16 | mask);
			tv_ITEM.hItem = this.handle;
			if ((mask & 1) != 0)
			{
				tv_ITEM.pszText = Marshal.StringToHGlobalAuto(this.text);
			}
			if ((mask & 2) != 0)
			{
				tv_ITEM.iImage = ((this.ImageIndexer.ActualIndex == -1) ? treeView.ImageIndexer.ActualIndex : this.ImageIndexer.ActualIndex);
			}
			if ((mask & 32) != 0)
			{
				tv_ITEM.iSelectedImage = ((this.SelectedImageIndexer.ActualIndex == -1) ? treeView.SelectedImageIndexer.ActualIndex : this.SelectedImageIndexer.ActualIndex);
			}
			if ((mask & 8) != 0)
			{
				tv_ITEM.stateMask = 61440;
				if (this.StateImageIndexer.ActualIndex != -1)
				{
					tv_ITEM.state = this.StateImageIndexer.ActualIndex + 1 << 12;
				}
			}
			if ((mask & 4) != 0)
			{
				tv_ITEM.lParam = this.handle;
			}
			UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), NativeMethods.TVM_SETITEM, 0, ref tv_ITEM);
			if ((mask & 1) != 0)
			{
				Marshal.FreeHGlobal(tv_ITEM.pszText);
				if (treeView.Scrollable)
				{
					treeView.ForceScrollbarUpdate(false);
				}
			}
		}

		// Token: 0x060048BC RID: 18620 RVA: 0x00132858 File Offset: 0x00130A58
		internal void UpdateImage()
		{
			TreeView treeView = this.TreeView;
			if (treeView.IsDisposed)
			{
				return;
			}
			NativeMethods.TV_ITEM tv_ITEM = default(NativeMethods.TV_ITEM);
			tv_ITEM.mask = 18;
			tv_ITEM.hItem = this.Handle;
			tv_ITEM.iImage = Math.Max(0, (this.ImageIndexer.ActualIndex >= treeView.ImageList.Images.Count) ? (treeView.ImageList.Images.Count - 1) : this.ImageIndexer.ActualIndex);
			UnsafeNativeMethods.SendMessage(new HandleRef(treeView, treeView.Handle), NativeMethods.TVM_SETITEM, 0, ref tv_ITEM);
		}

		// Token: 0x060048BD RID: 18621 RVA: 0x001328F6 File Offset: 0x00130AF6
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo si, StreamingContext context)
		{
			this.Serialize(si, context);
		}

		// Token: 0x0400272B RID: 10027
		private const int SHIFTVAL = 12;

		// Token: 0x0400272C RID: 10028
		private const int CHECKED = 8192;

		// Token: 0x0400272D RID: 10029
		private const int UNCHECKED = 4096;

		// Token: 0x0400272E RID: 10030
		private const int ALLOWEDIMAGES = 14;

		// Token: 0x0400272F RID: 10031
		internal const int MAX_TREENODES_OPS = 200;

		// Token: 0x04002730 RID: 10032
		internal OwnerDrawPropertyBag propBag;

		// Token: 0x04002731 RID: 10033
		internal IntPtr handle;

		// Token: 0x04002732 RID: 10034
		internal string text;

		// Token: 0x04002733 RID: 10035
		internal string name;

		// Token: 0x04002734 RID: 10036
		private const int TREENODESTATE_isChecked = 1;

		// Token: 0x04002735 RID: 10037
		private BitVector32 treeNodeState;

		// Token: 0x04002736 RID: 10038
		private TreeNode.TreeNodeImageIndexer imageIndexer;

		// Token: 0x04002737 RID: 10039
		private TreeNode.TreeNodeImageIndexer selectedImageIndexer;

		// Token: 0x04002738 RID: 10040
		private TreeNode.TreeNodeImageIndexer stateImageIndexer;

		// Token: 0x04002739 RID: 10041
		private string toolTipText = "";

		// Token: 0x0400273A RID: 10042
		private ContextMenu contextMenu;

		// Token: 0x0400273B RID: 10043
		private ContextMenuStrip contextMenuStrip;

		// Token: 0x0400273C RID: 10044
		internal bool nodesCleared;

		// Token: 0x0400273D RID: 10045
		internal int index;

		// Token: 0x0400273E RID: 10046
		internal int childCount;

		// Token: 0x0400273F RID: 10047
		internal TreeNode[] children;

		// Token: 0x04002740 RID: 10048
		internal TreeNode parent;

		// Token: 0x04002741 RID: 10049
		internal TreeView treeView;

		// Token: 0x04002742 RID: 10050
		private bool expandOnRealization;

		// Token: 0x04002743 RID: 10051
		private bool collapseOnRealization;

		// Token: 0x04002744 RID: 10052
		private TreeNodeCollection nodes;

		// Token: 0x04002745 RID: 10053
		private object userData;

		// Token: 0x04002746 RID: 10054
		private static readonly int insertMask = 35;

		// Token: 0x02000826 RID: 2086
		internal class TreeNodeImageIndexer : ImageList.Indexer
		{
			// Token: 0x0600700A RID: 28682 RVA: 0x0019B47D File Offset: 0x0019967D
			public TreeNodeImageIndexer(TreeNode node, TreeNode.TreeNodeImageIndexer.ImageListType imageListType)
			{
				this.owner = node;
				this.imageListType = imageListType;
			}

			// Token: 0x1700187D RID: 6269
			// (get) Token: 0x0600700B RID: 28683 RVA: 0x0019B493 File Offset: 0x00199693
			// (set) Token: 0x0600700C RID: 28684 RVA: 0x000072B6 File Offset: 0x000054B6
			public override ImageList ImageList
			{
				get
				{
					if (this.owner.TreeView == null)
					{
						return null;
					}
					if (this.imageListType == TreeNode.TreeNodeImageIndexer.ImageListType.State)
					{
						return this.owner.TreeView.StateImageList;
					}
					return this.owner.TreeView.ImageList;
				}
				set
				{
				}
			}

			// Token: 0x0400433F RID: 17215
			private TreeNode owner;

			// Token: 0x04004340 RID: 17216
			private TreeNode.TreeNodeImageIndexer.ImageListType imageListType;

			// Token: 0x020008D1 RID: 2257
			public enum ImageListType
			{
				// Token: 0x04004564 RID: 17764
				Default,
				// Token: 0x04004565 RID: 17765
				State
			}
		}
	}
}

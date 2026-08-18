using System;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x0200037E RID: 894
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[SRDescription("DescriptionStatusStrip")]
	public class StatusStrip : ToolStrip
	{
		// Token: 0x06003A5C RID: 14940 RVA: 0x00101714 File Offset: 0x000FF914
		public StatusStrip()
		{
			base.SuspendLayout();
			this.CanOverflow = false;
			this.LayoutStyle = ToolStripLayoutStyle.Table;
			base.RenderMode = ToolStripRenderMode.System;
			this.GripStyle = ToolStripGripStyle.Hidden;
			base.SetStyle(ControlStyles.ResizeRedraw, true);
			this.Stretch = true;
			this.state[StatusStrip.stateSizingGrip] = true;
			base.ResumeLayout(true);
		}

		// Token: 0x17000DED RID: 3565
		// (get) Token: 0x06003A5D RID: 14941 RVA: 0x000D8F84 File Offset: 0x000D7184
		// (set) Token: 0x06003A5E RID: 14942 RVA: 0x000D8F8C File Offset: 0x000D718C
		[DefaultValue(false)]
		[SRDescription("ToolStripCanOverflowDescr")]
		[SRCategory("CatLayout")]
		[Browsable(false)]
		public new bool CanOverflow
		{
			get
			{
				return base.CanOverflow;
			}
			set
			{
				base.CanOverflow = value;
			}
		}

		// Token: 0x17000DEE RID: 3566
		// (get) Token: 0x06003A5F RID: 14943 RVA: 0x00011A20 File Offset: 0x0000FC20
		protected override bool DefaultShowItemToolTips
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000DEF RID: 3567
		// (get) Token: 0x06003A60 RID: 14944 RVA: 0x00101771 File Offset: 0x000FF971
		protected override Size DefaultSize
		{
			get
			{
				return new Size(200, 22);
			}
		}

		// Token: 0x17000DF0 RID: 3568
		// (get) Token: 0x06003A61 RID: 14945 RVA: 0x00101780 File Offset: 0x000FF980
		protected override Padding DefaultPadding
		{
			get
			{
				if (base.Orientation != Orientation.Horizontal)
				{
					return new Padding(1, 3, 1, this.DefaultSize.Height);
				}
				if (this.RightToLeft == RightToLeft.No)
				{
					return new Padding(1, 0, 14, 0);
				}
				return new Padding(14, 0, 1, 0);
			}
		}

		// Token: 0x17000DF1 RID: 3569
		// (get) Token: 0x06003A62 RID: 14946 RVA: 0x0001627D File Offset: 0x0001447D
		protected override DockStyle DefaultDock
		{
			get
			{
				return DockStyle.Bottom;
			}
		}

		// Token: 0x17000DF2 RID: 3570
		// (get) Token: 0x06003A63 RID: 14947 RVA: 0x001017C9 File Offset: 0x000FF9C9
		// (set) Token: 0x06003A64 RID: 14948 RVA: 0x001017D1 File Offset: 0x000FF9D1
		[DefaultValue(DockStyle.Bottom)]
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

		// Token: 0x17000DF3 RID: 3571
		// (get) Token: 0x06003A65 RID: 14949 RVA: 0x000D904D File Offset: 0x000D724D
		// (set) Token: 0x06003A66 RID: 14950 RVA: 0x000D9055 File Offset: 0x000D7255
		[DefaultValue(ToolStripGripStyle.Hidden)]
		public new ToolStripGripStyle GripStyle
		{
			get
			{
				return base.GripStyle;
			}
			set
			{
				base.GripStyle = value;
			}
		}

		// Token: 0x17000DF4 RID: 3572
		// (get) Token: 0x06003A67 RID: 14951 RVA: 0x001017DA File Offset: 0x000FF9DA
		// (set) Token: 0x06003A68 RID: 14952 RVA: 0x001017E2 File Offset: 0x000FF9E2
		[DefaultValue(ToolStripLayoutStyle.Table)]
		public new ToolStripLayoutStyle LayoutStyle
		{
			get
			{
				return base.LayoutStyle;
			}
			set
			{
				base.LayoutStyle = value;
			}
		}

		// Token: 0x17000DF5 RID: 3573
		// (get) Token: 0x06003A69 RID: 14953 RVA: 0x00013656 File Offset: 0x00011856
		// (set) Token: 0x06003A6A RID: 14954 RVA: 0x0001365E File Offset: 0x0001185E
		[Browsable(false)]
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

		// Token: 0x140002D0 RID: 720
		// (add) Token: 0x06003A6B RID: 14955 RVA: 0x00013667 File Offset: 0x00011867
		// (remove) Token: 0x06003A6C RID: 14956 RVA: 0x00013670 File Offset: 0x00011870
		[Browsable(false)]
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

		// Token: 0x17000DF6 RID: 3574
		// (get) Token: 0x06003A6D RID: 14957 RVA: 0x001017EB File Offset: 0x000FF9EB
		private Control RTLGrip
		{
			get
			{
				if (this.rtlLayoutGrip == null)
				{
					this.rtlLayoutGrip = new StatusStrip.RightToLeftLayoutGrip();
				}
				return this.rtlLayoutGrip;
			}
		}

		// Token: 0x17000DF7 RID: 3575
		// (get) Token: 0x06003A6E RID: 14958 RVA: 0x000D90AA File Offset: 0x000D72AA
		// (set) Token: 0x06003A6F RID: 14959 RVA: 0x000D90B2 File Offset: 0x000D72B2
		[DefaultValue(false)]
		[SRDescription("ToolStripShowItemToolTipsDescr")]
		[SRCategory("CatBehavior")]
		public new bool ShowItemToolTips
		{
			get
			{
				return base.ShowItemToolTips;
			}
			set
			{
				base.ShowItemToolTips = value;
			}
		}

		// Token: 0x17000DF8 RID: 3576
		// (get) Token: 0x06003A70 RID: 14960 RVA: 0x00101808 File Offset: 0x000FFA08
		private bool ShowSizingGrip
		{
			get
			{
				if (this.SizingGrip && base.IsHandleCreated)
				{
					if (base.DesignMode)
					{
						return true;
					}
					HandleRef rootHWnd = WindowsFormsUtils.GetRootHWnd(this);
					if (rootHWnd.Handle != IntPtr.Zero)
					{
						return !UnsafeNativeMethods.IsZoomed(rootHWnd);
					}
				}
				return false;
			}
		}

		// Token: 0x17000DF9 RID: 3577
		// (get) Token: 0x06003A71 RID: 14961 RVA: 0x00101854 File Offset: 0x000FFA54
		// (set) Token: 0x06003A72 RID: 14962 RVA: 0x00101866 File Offset: 0x000FFA66
		[SRCategory("CatAppearance")]
		[DefaultValue(true)]
		[SRDescription("StatusStripSizingGripDescr")]
		public bool SizingGrip
		{
			get
			{
				return this.state[StatusStrip.stateSizingGrip];
			}
			set
			{
				if (value != this.state[StatusStrip.stateSizingGrip])
				{
					this.state[StatusStrip.stateSizingGrip] = value;
					this.EnsureRightToLeftGrip();
					base.Invalidate(true);
				}
			}
		}

		// Token: 0x17000DFA RID: 3578
		// (get) Token: 0x06003A73 RID: 14963 RVA: 0x0010189C File Offset: 0x000FFA9C
		[Browsable(false)]
		public Rectangle SizeGripBounds
		{
			get
			{
				if (!this.SizingGrip)
				{
					return Rectangle.Empty;
				}
				Size size = base.Size;
				int num = Math.Min(this.DefaultSize.Height, size.Height);
				if (this.RightToLeft == RightToLeft.Yes)
				{
					return new Rectangle(0, size.Height - num, 12, num);
				}
				return new Rectangle(size.Width - 12, size.Height - num, 12, num);
			}
		}

		// Token: 0x17000DFB RID: 3579
		// (get) Token: 0x06003A74 RID: 14964 RVA: 0x000D90BB File Offset: 0x000D72BB
		// (set) Token: 0x06003A75 RID: 14965 RVA: 0x000D90C3 File Offset: 0x000D72C3
		[DefaultValue(true)]
		[SRCategory("CatLayout")]
		[SRDescription("ToolStripStretchDescr")]
		public new bool Stretch
		{
			get
			{
				return base.Stretch;
			}
			set
			{
				base.Stretch = value;
			}
		}

		// Token: 0x17000DFC RID: 3580
		// (get) Token: 0x06003A76 RID: 14966 RVA: 0x00101910 File Offset: 0x000FFB10
		private TableLayoutSettings TableLayoutSettings
		{
			get
			{
				return base.LayoutSettings as TableLayoutSettings;
			}
		}

		// Token: 0x06003A77 RID: 14967 RVA: 0x0010191D File Offset: 0x000FFB1D
		protected override AccessibleObject CreateAccessibilityInstance()
		{
			return new StatusStrip.StatusStripAccessibleObject(this);
		}

		// Token: 0x06003A78 RID: 14968 RVA: 0x00101925 File Offset: 0x000FFB25
		protected internal override ToolStripItem CreateDefaultItem(string text, Image image, EventHandler onClick)
		{
			return new ToolStripStatusLabel(text, image, onClick);
		}

		// Token: 0x06003A79 RID: 14969 RVA: 0x0010192F File Offset: 0x000FFB2F
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.rtlLayoutGrip != null)
			{
				this.rtlLayoutGrip.Dispose();
				this.rtlLayoutGrip = null;
			}
			base.Dispose(disposing);
		}

		// Token: 0x06003A7A RID: 14970 RVA: 0x00101958 File Offset: 0x000FFB58
		private void EnsureRightToLeftGrip()
		{
			if (this.SizingGrip && this.RightToLeft == RightToLeft.Yes)
			{
				this.RTLGrip.Bounds = this.SizeGripBounds;
				if (!base.Controls.Contains(this.RTLGrip))
				{
					WindowsFormsUtils.ReadOnlyControlCollection readOnlyControlCollection = base.Controls as WindowsFormsUtils.ReadOnlyControlCollection;
					if (readOnlyControlCollection != null)
					{
						readOnlyControlCollection.AddInternal(this.RTLGrip);
						return;
					}
				}
			}
			else if (this.rtlLayoutGrip != null && base.Controls.Contains(this.rtlLayoutGrip))
			{
				WindowsFormsUtils.ReadOnlyControlCollection readOnlyControlCollection2 = base.Controls as WindowsFormsUtils.ReadOnlyControlCollection;
				if (readOnlyControlCollection2 != null)
				{
					readOnlyControlCollection2.RemoveInternal(this.rtlLayoutGrip);
				}
				this.rtlLayoutGrip.Dispose();
				this.rtlLayoutGrip = null;
			}
		}

		// Token: 0x06003A7B RID: 14971 RVA: 0x00101A00 File Offset: 0x000FFC00
		internal override Size GetPreferredSizeCore(Size proposedSize)
		{
			if (this.LayoutStyle != ToolStripLayoutStyle.Table)
			{
				return base.GetPreferredSizeCore(proposedSize);
			}
			if (proposedSize.Width == 1)
			{
				proposedSize.Width = int.MaxValue;
			}
			if (proposedSize.Height == 1)
			{
				proposedSize.Height = int.MaxValue;
			}
			if (base.Orientation == Orientation.Horizontal)
			{
				return ToolStrip.GetPreferredSizeHorizontal(this, proposedSize) + this.Padding.Size;
			}
			return ToolStrip.GetPreferredSizeVertical(this, proposedSize) + this.Padding.Size;
		}

		// Token: 0x06003A7C RID: 14972 RVA: 0x00101A87 File Offset: 0x000FFC87
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			base.OnPaintBackground(e);
			if (this.ShowSizingGrip)
			{
				base.Renderer.DrawStatusStripSizingGrip(new ToolStripRenderEventArgs(e.Graphics, this));
			}
		}

		// Token: 0x06003A7D RID: 14973 RVA: 0x00101AB0 File Offset: 0x000FFCB0
		protected override void OnLayout(LayoutEventArgs levent)
		{
			this.state[StatusStrip.stateCalledSpringTableLayout] = false;
			bool flag = false;
			ToolStripItem toolStripItem = levent.AffectedComponent as ToolStripItem;
			int count = this.DisplayedItems.Count;
			if (toolStripItem != null)
			{
				flag = this.DisplayedItems.Contains(toolStripItem);
			}
			if (this.LayoutStyle == ToolStripLayoutStyle.Table)
			{
				this.OnSpringTableLayoutCore();
			}
			base.OnLayout(levent);
			if ((count != this.DisplayedItems.Count || (toolStripItem != null && flag != this.DisplayedItems.Contains(toolStripItem))) && this.LayoutStyle == ToolStripLayoutStyle.Table)
			{
				this.OnSpringTableLayoutCore();
				base.OnLayout(levent);
			}
			this.EnsureRightToLeftGrip();
		}

		// Token: 0x17000DFD RID: 3581
		// (get) Token: 0x06003A7E RID: 14974 RVA: 0x00028D57 File Offset: 0x00026F57
		internal override bool SupportsUiaProviders
		{
			get
			{
				return AccessibilityImprovements.Level3 && !base.DesignMode;
			}
		}

		// Token: 0x06003A7F RID: 14975 RVA: 0x00101B4C File Offset: 0x000FFD4C
		protected override void SetDisplayedItems()
		{
			if (this.state[StatusStrip.stateCalledSpringTableLayout])
			{
				bool flag = base.Orientation == Orientation.Horizontal && this.RightToLeft == RightToLeft.Yes;
				Point location = this.DisplayRectangle.Location;
				location.X += base.ClientSize.Width + 1;
				location.Y += base.ClientSize.Height + 1;
				bool flag2 = false;
				Rectangle rectangle = Rectangle.Empty;
				ToolStripItem toolStripItem = null;
				for (int i = 0; i < this.Items.Count; i++)
				{
					ToolStripItem toolStripItem2 = this.Items[i];
					if (flag2 || ((IArrangedElement)toolStripItem2).ParticipatesInLayout)
					{
						if (flag2 || (this.SizingGrip && toolStripItem2.Bounds.IntersectsWith(this.SizeGripBounds)))
						{
							base.SetItemLocation(toolStripItem2, location);
							toolStripItem2.SetPlacement(ToolStripItemPlacement.None);
						}
					}
					else if (toolStripItem != null && rectangle.IntersectsWith(toolStripItem2.Bounds))
					{
						base.SetItemLocation(toolStripItem2, location);
						toolStripItem2.SetPlacement(ToolStripItemPlacement.None);
					}
					else if (toolStripItem2.Bounds.Width == 1)
					{
						ToolStripStatusLabel toolStripStatusLabel = toolStripItem2 as ToolStripStatusLabel;
						if (toolStripStatusLabel != null && toolStripStatusLabel.Spring)
						{
							base.SetItemLocation(toolStripItem2, location);
							toolStripItem2.SetPlacement(ToolStripItemPlacement.None);
						}
					}
					if (toolStripItem2.Bounds.Location != location)
					{
						toolStripItem = toolStripItem2;
						rectangle = toolStripItem.Bounds;
					}
					else if (((IArrangedElement)toolStripItem2).ParticipatesInLayout)
					{
						flag2 = true;
					}
				}
			}
			base.SetDisplayedItems();
		}

		// Token: 0x06003A80 RID: 14976 RVA: 0x00101CE9 File Offset: 0x000FFEE9
		internal override void ResetRenderMode()
		{
			base.RenderMode = ToolStripRenderMode.System;
		}

		// Token: 0x06003A81 RID: 14977 RVA: 0x00101CF2 File Offset: 0x000FFEF2
		internal override bool ShouldSerializeRenderMode()
		{
			return base.RenderMode != ToolStripRenderMode.System && base.RenderMode > ToolStripRenderMode.Custom;
		}

		// Token: 0x06003A82 RID: 14978 RVA: 0x00101D08 File Offset: 0x000FFF08
		protected virtual void OnSpringTableLayoutCore()
		{
			if (this.LayoutStyle == ToolStripLayoutStyle.Table)
			{
				this.state[StatusStrip.stateCalledSpringTableLayout] = true;
				base.SuspendLayout();
				if (this.lastOrientation != base.Orientation)
				{
					TableLayoutSettings tableLayoutSettings = this.TableLayoutSettings;
					tableLayoutSettings.RowCount = 0;
					tableLayoutSettings.ColumnCount = 0;
					tableLayoutSettings.ColumnStyles.Clear();
					tableLayoutSettings.RowStyles.Clear();
				}
				this.lastOrientation = base.Orientation;
				if (base.Orientation == Orientation.Horizontal)
				{
					this.TableLayoutSettings.GrowStyle = TableLayoutPanelGrowStyle.AddColumns;
					int count = this.TableLayoutSettings.ColumnStyles.Count;
					for (int i = 0; i < this.DisplayedItems.Count; i++)
					{
						if (i >= count)
						{
							this.TableLayoutSettings.ColumnStyles.Add(new ColumnStyle());
						}
						ToolStripStatusLabel toolStripStatusLabel = this.DisplayedItems[i] as ToolStripStatusLabel;
						bool flag = toolStripStatusLabel != null && toolStripStatusLabel.Spring;
						this.DisplayedItems[i].Anchor = (flag ? (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right) : (AnchorStyles.Top | AnchorStyles.Bottom));
						ColumnStyle columnStyle = this.TableLayoutSettings.ColumnStyles[i];
						columnStyle.Width = 100f;
						columnStyle.SizeType = (flag ? SizeType.Percent : SizeType.AutoSize);
					}
					if (this.TableLayoutSettings.RowStyles.Count > 1 || this.TableLayoutSettings.RowStyles.Count == 0)
					{
						this.TableLayoutSettings.RowStyles.Clear();
						this.TableLayoutSettings.RowStyles.Add(new RowStyle());
					}
					this.TableLayoutSettings.RowCount = 1;
					this.TableLayoutSettings.RowStyles[0].SizeType = SizeType.Absolute;
					this.TableLayoutSettings.RowStyles[0].Height = (float)Math.Max(0, this.DisplayRectangle.Height);
					this.TableLayoutSettings.ColumnCount = this.DisplayedItems.Count + 1;
					for (int j = this.DisplayedItems.Count; j < this.TableLayoutSettings.ColumnStyles.Count; j++)
					{
						this.TableLayoutSettings.ColumnStyles[j].SizeType = SizeType.AutoSize;
					}
				}
				else
				{
					this.TableLayoutSettings.GrowStyle = TableLayoutPanelGrowStyle.AddRows;
					int count2 = this.TableLayoutSettings.RowStyles.Count;
					for (int k = 0; k < this.DisplayedItems.Count; k++)
					{
						if (k >= count2)
						{
							this.TableLayoutSettings.RowStyles.Add(new RowStyle());
						}
						ToolStripStatusLabel toolStripStatusLabel2 = this.DisplayedItems[k] as ToolStripStatusLabel;
						bool flag2 = toolStripStatusLabel2 != null && toolStripStatusLabel2.Spring;
						this.DisplayedItems[k].Anchor = (flag2 ? (AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right) : (AnchorStyles.Left | AnchorStyles.Right));
						RowStyle rowStyle = this.TableLayoutSettings.RowStyles[k];
						rowStyle.Height = 100f;
						rowStyle.SizeType = (flag2 ? SizeType.Percent : SizeType.AutoSize);
					}
					this.TableLayoutSettings.ColumnCount = 1;
					if (this.TableLayoutSettings.ColumnStyles.Count > 1 || this.TableLayoutSettings.ColumnStyles.Count == 0)
					{
						this.TableLayoutSettings.ColumnStyles.Clear();
						this.TableLayoutSettings.ColumnStyles.Add(new ColumnStyle());
					}
					this.TableLayoutSettings.ColumnCount = 1;
					this.TableLayoutSettings.ColumnStyles[0].SizeType = SizeType.Absolute;
					this.TableLayoutSettings.ColumnStyles[0].Width = (float)Math.Max(0, this.DisplayRectangle.Width);
					this.TableLayoutSettings.RowCount = this.DisplayedItems.Count + 1;
					for (int l = this.DisplayedItems.Count; l < this.TableLayoutSettings.RowStyles.Count; l++)
					{
						this.TableLayoutSettings.RowStyles[l].SizeType = SizeType.AutoSize;
					}
				}
				base.ResumeLayout(false);
			}
		}

		// Token: 0x06003A83 RID: 14979 RVA: 0x0010210C File Offset: 0x0010030C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			if (m.Msg == 132 && this.SizingGrip)
			{
				Rectangle sizeGripBounds = this.SizeGripBounds;
				int x = NativeMethods.Util.LOWORD(m.LParam);
				int y = NativeMethods.Util.HIWORD(m.LParam);
				if (sizeGripBounds.Contains(base.PointToClient(new Point(x, y))))
				{
					HandleRef rootHWnd = WindowsFormsUtils.GetRootHWnd(this);
					if (rootHWnd.Handle != IntPtr.Zero && !UnsafeNativeMethods.IsZoomed(rootHWnd))
					{
						NativeMethods.RECT rect = default(NativeMethods.RECT);
						UnsafeNativeMethods.GetClientRect(rootHWnd, ref rect);
						NativeMethods.POINT point;
						if (this.RightToLeft == RightToLeft.Yes)
						{
							point = new NativeMethods.POINT(this.SizeGripBounds.Left, this.SizeGripBounds.Bottom);
						}
						else
						{
							point = new NativeMethods.POINT(this.SizeGripBounds.Right, this.SizeGripBounds.Bottom);
						}
						UnsafeNativeMethods.MapWindowPoints(new HandleRef(this, base.Handle), rootHWnd, point, 1);
						int num = Math.Abs(rect.bottom - point.y);
						int num2 = Math.Abs(rect.right - point.x);
						if (this.RightToLeft != RightToLeft.Yes && num2 + num < 2)
						{
							m.Result = (IntPtr)17;
							return;
						}
					}
				}
			}
			base.WndProc(ref m);
		}

		// Token: 0x0400230C RID: 8972
		private const AnchorStyles AllAnchor = AnchorStyles.Top | AnchorStyles.Bottom | AnchorStyles.Left | AnchorStyles.Right;

		// Token: 0x0400230D RID: 8973
		private const AnchorStyles HorizontalAnchor = AnchorStyles.Left | AnchorStyles.Right;

		// Token: 0x0400230E RID: 8974
		private const AnchorStyles VerticalAnchor = AnchorStyles.Top | AnchorStyles.Bottom;

		// Token: 0x0400230F RID: 8975
		private BitVector32 state;

		// Token: 0x04002310 RID: 8976
		private static readonly int stateSizingGrip = BitVector32.CreateMask();

		// Token: 0x04002311 RID: 8977
		private static readonly int stateCalledSpringTableLayout = BitVector32.CreateMask(StatusStrip.stateSizingGrip);

		// Token: 0x04002312 RID: 8978
		private const int gripWidth = 12;

		// Token: 0x04002313 RID: 8979
		private StatusStrip.RightToLeftLayoutGrip rtlLayoutGrip;

		// Token: 0x04002314 RID: 8980
		private Orientation lastOrientation;

		// Token: 0x020007EC RID: 2028
		private class RightToLeftLayoutGrip : Control
		{
			// Token: 0x06006E2E RID: 28206 RVA: 0x001943ED File Offset: 0x001925ED
			public RightToLeftLayoutGrip()
			{
				base.SetStyle(ControlStyles.SupportsTransparentBackColor, true);
				this.BackColor = Color.Transparent;
			}

			// Token: 0x17001811 RID: 6161
			// (get) Token: 0x06006E2F RID: 28207 RVA: 0x0019440C File Offset: 0x0019260C
			protected override CreateParams CreateParams
			{
				[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
				get
				{
					CreateParams createParams = base.CreateParams;
					createParams.ExStyle |= 4194304;
					return createParams;
				}
			}

			// Token: 0x06006E30 RID: 28208 RVA: 0x00194434 File Offset: 0x00192634
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			protected override void WndProc(ref Message m)
			{
				if (m.Msg == 132)
				{
					int x = NativeMethods.Util.LOWORD(m.LParam);
					int y = NativeMethods.Util.HIWORD(m.LParam);
					if (base.ClientRectangle.Contains(base.PointToClient(new Point(x, y))))
					{
						m.Result = (IntPtr)16;
						return;
					}
				}
				base.WndProc(ref m);
			}
		}

		// Token: 0x020007ED RID: 2029
		[ComVisible(true)]
		internal class StatusStripAccessibleObject : ToolStrip.ToolStripAccessibleObject
		{
			// Token: 0x06006E31 RID: 28209 RVA: 0x0018CF24 File Offset: 0x0018B124
			public StatusStripAccessibleObject(StatusStrip owner) : base(owner)
			{
			}

			// Token: 0x17001812 RID: 6162
			// (get) Token: 0x06006E32 RID: 28210 RVA: 0x00194498 File Offset: 0x00192698
			public override AccessibleRole Role
			{
				get
				{
					if (base.IsOwnerControlDestroyed())
					{
						return AccessibleRole.StatusBar;
					}
					AccessibleRole accessibleRole = base.Owner.AccessibleRole;
					if (accessibleRole != AccessibleRole.Default)
					{
						return accessibleRole;
					}
					return AccessibleRole.StatusBar;
				}
			}

			// Token: 0x06006E33 RID: 28211 RVA: 0x001944C4 File Offset: 0x001926C4
			internal override object GetPropertyValue(int propertyID)
			{
				if (AccessibilityImprovements.Level3 && propertyID == 30003)
				{
					return 50017;
				}
				return base.GetPropertyValue(propertyID);
			}

			// Token: 0x06006E34 RID: 28212 RVA: 0x001944E8 File Offset: 0x001926E8
			internal override UnsafeNativeMethods.IRawElementProviderFragment FragmentNavigate(UnsafeNativeMethods.NavigateDirection direction)
			{
				if (base.IsOwnerControlDestroyed())
				{
					return null;
				}
				StatusStrip statusStrip = base.Owner as StatusStrip;
				if (statusStrip == null || statusStrip.Items.Count == 0)
				{
					if (base.Owner.ToolStripControlHost != null && (direction == UnsafeNativeMethods.NavigateDirection.Parent || direction == UnsafeNativeMethods.NavigateDirection.PreviousSibling || direction == UnsafeNativeMethods.NavigateDirection.NextSibling))
					{
						return base.Owner.ToolStripControlHost.AccessibilityObject.FragmentNavigate(direction);
					}
					return null;
				}
				else
				{
					if (direction == UnsafeNativeMethods.NavigateDirection.FirstChild)
					{
						for (int i = 0; i < this.GetChildCount(); i++)
						{
							AccessibleObject child = this.GetChild(i);
							if (child != null && !(child is Control.ControlAccessibleObject))
							{
								return child;
							}
						}
						return null;
					}
					if (direction != UnsafeNativeMethods.NavigateDirection.LastChild)
					{
						return base.FragmentNavigate(direction);
					}
					for (int j = this.GetChildCount() - 1; j >= 0; j--)
					{
						AccessibleObject child2 = this.GetChild(j);
						if (child2 != null && !(child2 is Control.ControlAccessibleObject))
						{
							return child2;
						}
					}
					return null;
				}
			}

			// Token: 0x06006E35 RID: 28213 RVA: 0x001798B4 File Offset: 0x00177AB4
			internal override UnsafeNativeMethods.IRawElementProviderFragment ElementProviderFromPoint(double x, double y)
			{
				return this.HitTest((int)x, (int)y);
			}

			// Token: 0x06006E36 RID: 28214 RVA: 0x000F17D2 File Offset: 0x000EF9D2
			internal override UnsafeNativeMethods.IRawElementProviderFragment GetFocus()
			{
				return this.GetFocused();
			}
		}
	}
}

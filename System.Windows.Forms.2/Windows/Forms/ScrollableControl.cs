using System;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Windows.Forms.Layout;

namespace System.Windows.Forms
{
	// Token: 0x02000356 RID: 854
	[ComVisible(true)]
	[ClassInterface(ClassInterfaceType.AutoDispatch)]
	[Designer("System.Windows.Forms.Design.ScrollableControlDesigner, System.Design, Version=4.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a")]
	public class ScrollableControl : Control, IArrangedElement, IComponent, IDisposable
	{
		// Token: 0x060037B5 RID: 14261 RVA: 0x000F82E0 File Offset: 0x000F64E0
		public ScrollableControl()
		{
			base.SetStyle(ControlStyles.ContainerControl, true);
			base.SetStyle(ControlStyles.AllPaintingInWmPaint, false);
			this.SetScrollState(1, false);
		}

		// Token: 0x17000D38 RID: 3384
		// (get) Token: 0x060037B6 RID: 14262 RVA: 0x000F8346 File Offset: 0x000F6546
		// (set) Token: 0x060037B7 RID: 14263 RVA: 0x000F834F File Offset: 0x000F654F
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[DefaultValue(false)]
		[SRDescription("FormAutoScrollDescr")]
		public virtual bool AutoScroll
		{
			get
			{
				return this.GetScrollState(1);
			}
			set
			{
				if (value)
				{
					this.UpdateFullDrag();
				}
				this.SetScrollState(1, value);
				LayoutTransaction.DoLayout(this, this, PropertyNames.AutoScroll);
			}
		}

		// Token: 0x17000D39 RID: 3385
		// (get) Token: 0x060037B8 RID: 14264 RVA: 0x000F836E File Offset: 0x000F656E
		// (set) Token: 0x060037B9 RID: 14265 RVA: 0x000F8378 File Offset: 0x000F6578
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[SRDescription("FormAutoScrollMarginDescr")]
		public Size AutoScrollMargin
		{
			get
			{
				return this.requestedScrollMargin;
			}
			set
			{
				if (value.Width < 0 || value.Height < 0)
				{
					throw new ArgumentOutOfRangeException("AutoScrollMargin", SR.GetString("InvalidArgument", new object[]
					{
						"AutoScrollMargin",
						value.ToString()
					}));
				}
				this.SetAutoScrollMargin(value.Width, value.Height);
			}
		}

		// Token: 0x17000D3A RID: 3386
		// (get) Token: 0x060037BA RID: 14266 RVA: 0x000F83E0 File Offset: 0x000F65E0
		// (set) Token: 0x060037BB RID: 14267 RVA: 0x000F8407 File Offset: 0x000F6607
		[SRCategory("CatLayout")]
		[Browsable(false)]
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[SRDescription("FormAutoScrollPositionDescr")]
		public Point AutoScrollPosition
		{
			get
			{
				Rectangle displayRectInternal = this.GetDisplayRectInternal();
				return new Point(displayRectInternal.X, displayRectInternal.Y);
			}
			set
			{
				if (base.Created)
				{
					this.SetDisplayRectLocation(-value.X, -value.Y);
					this.SyncScrollbars(true);
				}
				this.scrollPosition = value;
			}
		}

		// Token: 0x17000D3B RID: 3387
		// (get) Token: 0x060037BC RID: 14268 RVA: 0x000F8435 File Offset: 0x000F6635
		// (set) Token: 0x060037BD RID: 14269 RVA: 0x000F843D File Offset: 0x000F663D
		[SRCategory("CatLayout")]
		[Localizable(true)]
		[SRDescription("FormAutoScrollMinSizeDescr")]
		public Size AutoScrollMinSize
		{
			get
			{
				return this.userAutoScrollMinSize;
			}
			set
			{
				if (value != this.userAutoScrollMinSize)
				{
					this.userAutoScrollMinSize = value;
					this.AutoScroll = true;
					base.PerformLayout();
				}
			}
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x060037BE RID: 14270 RVA: 0x000F8464 File Offset: 0x000F6664
		protected override CreateParams CreateParams
		{
			[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
			get
			{
				CreateParams createParams = base.CreateParams;
				if (this.HScroll || this.HorizontalScroll.Visible)
				{
					createParams.Style |= 1048576;
				}
				else
				{
					createParams.Style &= -1048577;
				}
				if (this.VScroll || this.VerticalScroll.Visible)
				{
					createParams.Style |= 2097152;
				}
				else
				{
					createParams.Style &= -2097153;
				}
				return createParams;
			}
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x060037BF RID: 14271 RVA: 0x000F84F0 File Offset: 0x000F66F0
		public override Rectangle DisplayRectangle
		{
			get
			{
				Rectangle clientRectangle = base.ClientRectangle;
				if (!this.displayRect.IsEmpty)
				{
					clientRectangle.X = this.displayRect.X;
					clientRectangle.Y = this.displayRect.Y;
					if (this.HScroll)
					{
						clientRectangle.Width = this.displayRect.Width;
					}
					if (this.VScroll)
					{
						clientRectangle.Height = this.displayRect.Height;
					}
				}
				return LayoutUtils.DeflateRect(clientRectangle, base.Padding);
			}
		}

		// Token: 0x17000D3E RID: 3390
		// (get) Token: 0x060037C0 RID: 14272 RVA: 0x000F8578 File Offset: 0x000F6778
		Rectangle IArrangedElement.DisplayRectangle
		{
			get
			{
				Rectangle displayRectangle = this.DisplayRectangle;
				if (this.AutoScrollMinSize.Width != 0 && this.AutoScrollMinSize.Height != 0)
				{
					displayRectangle.Width = Math.Max(displayRectangle.Width, this.AutoScrollMinSize.Width);
					displayRectangle.Height = Math.Max(displayRectangle.Height, this.AutoScrollMinSize.Height);
				}
				return displayRectangle;
			}
		}

		// Token: 0x17000D3F RID: 3391
		// (get) Token: 0x060037C1 RID: 14273 RVA: 0x000F85EF File Offset: 0x000F67EF
		// (set) Token: 0x060037C2 RID: 14274 RVA: 0x000F85F8 File Offset: 0x000F67F8
		protected bool HScroll
		{
			get
			{
				return this.GetScrollState(2);
			}
			set
			{
				this.SetScrollState(2, value);
			}
		}

		// Token: 0x17000D40 RID: 3392
		// (get) Token: 0x060037C3 RID: 14275 RVA: 0x000F8602 File Offset: 0x000F6802
		[SRCategory("CatLayout")]
		[SRDescription("ScrollableControlHorizontalScrollDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public HScrollProperties HorizontalScroll
		{
			get
			{
				if (this.horizontalScroll == null)
				{
					this.horizontalScroll = new HScrollProperties(this);
				}
				return this.horizontalScroll;
			}
		}

		// Token: 0x17000D41 RID: 3393
		// (get) Token: 0x060037C4 RID: 14276 RVA: 0x000F861E File Offset: 0x000F681E
		// (set) Token: 0x060037C5 RID: 14277 RVA: 0x000F8627 File Offset: 0x000F6827
		protected bool VScroll
		{
			get
			{
				return this.GetScrollState(4);
			}
			set
			{
				this.SetScrollState(4, value);
			}
		}

		// Token: 0x17000D42 RID: 3394
		// (get) Token: 0x060037C6 RID: 14278 RVA: 0x000F8631 File Offset: 0x000F6831
		[SRCategory("CatLayout")]
		[SRDescription("ScrollableControlVerticalScrollDescr")]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Always)]
		public VScrollProperties VerticalScroll
		{
			get
			{
				if (this.verticalScroll == null)
				{
					this.verticalScroll = new VScrollProperties(this);
				}
				return this.verticalScroll;
			}
		}

		// Token: 0x17000D43 RID: 3395
		// (get) Token: 0x060037C7 RID: 14279 RVA: 0x000F864D File Offset: 0x000F684D
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
		[Browsable(false)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public ScrollableControl.DockPaddingEdges DockPadding
		{
			get
			{
				if (this.dockPadding == null)
				{
					this.dockPadding = new ScrollableControl.DockPaddingEdges(this);
				}
				return this.dockPadding;
			}
		}

		// Token: 0x060037C8 RID: 14280 RVA: 0x000F866C File Offset: 0x000F686C
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected virtual void AdjustFormScrollbars(bool displayScrollbars)
		{
			bool flag = false;
			Rectangle displayRectInternal = this.GetDisplayRectInternal();
			if (!displayScrollbars && (this.HScroll || this.VScroll))
			{
				flag = this.SetVisibleScrollbars(false, false);
			}
			if (!displayScrollbars)
			{
				Rectangle clientRectangle = base.ClientRectangle;
				displayRectInternal.Width = clientRectangle.Width;
				displayRectInternal.Height = clientRectangle.Height;
			}
			else
			{
				flag |= this.ApplyScrollbarChanges(displayRectInternal);
			}
			if (flag)
			{
				LayoutTransaction.DoLayout(this, this, PropertyNames.DisplayRectangle);
			}
		}

		// Token: 0x060037C9 RID: 14281 RVA: 0x000F86E0 File Offset: 0x000F68E0
		private bool ApplyScrollbarChanges(Rectangle display)
		{
			bool flag = false;
			bool flag2 = false;
			bool flag3 = false;
			Rectangle clientRectangle = base.ClientRectangle;
			Rectangle rectangle = clientRectangle;
			Rectangle rectangle2 = rectangle;
			if (this.HScroll)
			{
				rectangle.Height += SystemInformation.HorizontalScrollBarHeight;
			}
			else
			{
				rectangle2.Height -= SystemInformation.HorizontalScrollBarHeight;
			}
			if (this.VScroll)
			{
				rectangle.Width += SystemInformation.VerticalScrollBarWidth;
			}
			else
			{
				rectangle2.Width -= SystemInformation.VerticalScrollBarWidth;
			}
			int num = rectangle2.Width;
			int num2 = rectangle2.Height;
			if (base.Controls.Count != 0)
			{
				this.scrollMargin = this.requestedScrollMargin;
				if (this.dockPadding != null)
				{
					this.scrollMargin.Height = this.scrollMargin.Height + base.Padding.Bottom;
					this.scrollMargin.Width = this.scrollMargin.Width + base.Padding.Right;
				}
				for (int i = 0; i < base.Controls.Count; i++)
				{
					Control control = base.Controls[i];
					if (control != null && control.GetState(2))
					{
						DockStyle dock = control.Dock;
						if (dock != DockStyle.Bottom)
						{
							if (dock == DockStyle.Right)
							{
								this.scrollMargin.Width = this.scrollMargin.Width + control.Size.Width;
							}
						}
						else
						{
							this.scrollMargin.Height = this.scrollMargin.Height + control.Size.Height;
						}
					}
				}
			}
			if (!this.userAutoScrollMinSize.IsEmpty)
			{
				num = this.userAutoScrollMinSize.Width + this.scrollMargin.Width;
				num2 = this.userAutoScrollMinSize.Height + this.scrollMargin.Height;
				flag2 = true;
				flag3 = true;
			}
			bool flag4 = this.LayoutEngine == DefaultLayout.Instance;
			if (!flag4 && CommonProperties.HasLayoutBounds(this))
			{
				Size layoutBounds = CommonProperties.GetLayoutBounds(this);
				if (layoutBounds.Width > num)
				{
					flag2 = true;
					num = layoutBounds.Width;
				}
				if (layoutBounds.Height > num2)
				{
					flag3 = true;
					num2 = layoutBounds.Height;
				}
			}
			else if (base.Controls.Count != 0)
			{
				for (int j = 0; j < base.Controls.Count; j++)
				{
					bool flag5 = true;
					bool flag6 = true;
					Control control2 = base.Controls[j];
					if (control2 != null && control2.GetState(2))
					{
						if (flag4)
						{
							Control control3 = control2;
							switch (control3.Dock)
							{
							case DockStyle.Top:
								flag5 = false;
								break;
							case DockStyle.Bottom:
							case DockStyle.Right:
							case DockStyle.Fill:
								flag5 = false;
								flag6 = false;
								break;
							case DockStyle.Left:
								flag6 = false;
								break;
							default:
							{
								AnchorStyles anchor = control3.Anchor;
								if ((anchor & AnchorStyles.Right) == AnchorStyles.Right)
								{
									flag5 = false;
								}
								if ((anchor & AnchorStyles.Left) != AnchorStyles.Left)
								{
									flag5 = false;
								}
								if ((anchor & AnchorStyles.Bottom) == AnchorStyles.Bottom)
								{
									flag6 = false;
								}
								if ((anchor & AnchorStyles.Top) != AnchorStyles.Top)
								{
									flag6 = false;
								}
								break;
							}
							}
						}
						if (flag5 || flag6)
						{
							Rectangle bounds = control2.Bounds;
							int num3 = -display.X + bounds.X + bounds.Width + this.scrollMargin.Width;
							int num4 = -display.Y + bounds.Y + bounds.Height + this.scrollMargin.Height;
							if (!flag4)
							{
								num3 += control2.Margin.Right;
								num4 += control2.Margin.Bottom;
							}
							if (num3 > num && flag5)
							{
								flag2 = true;
								num = num3;
							}
							if (num4 > num2 && flag6)
							{
								flag3 = true;
								num2 = num4;
							}
						}
					}
				}
			}
			if (num <= rectangle.Width)
			{
				flag2 = false;
			}
			if (num2 <= rectangle.Height)
			{
				flag3 = false;
			}
			Rectangle rectangle3 = rectangle;
			if (flag2)
			{
				rectangle3.Height -= SystemInformation.HorizontalScrollBarHeight;
			}
			if (flag3)
			{
				rectangle3.Width -= SystemInformation.VerticalScrollBarWidth;
			}
			if (flag2 && num2 > rectangle3.Height)
			{
				flag3 = true;
			}
			if (flag3 && num > rectangle3.Width)
			{
				flag2 = true;
			}
			if (!flag2)
			{
				num = rectangle3.Width;
			}
			if (!flag3)
			{
				num2 = rectangle3.Height;
			}
			flag = (this.SetVisibleScrollbars(flag2, flag3) || flag);
			if (this.HScroll || this.VScroll)
			{
				flag = (this.SetDisplayRectangleSize(num, num2) || flag);
			}
			else
			{
				this.SetDisplayRectangleSize(num, num2);
			}
			this.SyncScrollbars(true);
			return flag;
		}

		// Token: 0x060037CA RID: 14282 RVA: 0x000F8B44 File Offset: 0x000F6D44
		private Rectangle GetDisplayRectInternal()
		{
			if (this.displayRect.IsEmpty)
			{
				this.displayRect = base.ClientRectangle;
			}
			if (!this.AutoScroll && this.HorizontalScroll.visible)
			{
				this.displayRect = new Rectangle(this.displayRect.X, this.displayRect.Y, this.HorizontalScroll.Maximum, this.displayRect.Height);
			}
			if (!this.AutoScroll && this.VerticalScroll.visible)
			{
				this.displayRect = new Rectangle(this.displayRect.X, this.displayRect.Y, this.displayRect.Width, this.VerticalScroll.Maximum);
			}
			return this.displayRect;
		}

		// Token: 0x060037CB RID: 14283 RVA: 0x000F8C08 File Offset: 0x000F6E08
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected bool GetScrollState(int bit)
		{
			return (bit & this.scrollState) == bit;
		}

		// Token: 0x060037CC RID: 14284 RVA: 0x000F8C15 File Offset: 0x000F6E15
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnLayout(LayoutEventArgs levent)
		{
			if (levent.AffectedControl != null && this.AutoScroll)
			{
				base.OnLayout(levent);
			}
			this.AdjustFormScrollbars(this.AutoScroll);
			base.OnLayout(levent);
		}

		// Token: 0x060037CD RID: 14285 RVA: 0x000F8C44 File Offset: 0x000F6E44
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnMouseWheel(MouseEventArgs e)
		{
			if (this.VScroll)
			{
				Rectangle clientRectangle = base.ClientRectangle;
				int num = -this.displayRect.Y;
				int val = -(clientRectangle.Height - this.displayRect.Height);
				num = Math.Max(num - e.Delta, 0);
				num = Math.Min(num, val);
				this.SetDisplayRectLocation(this.displayRect.X, -num);
				this.SyncScrollbars(this.AutoScroll);
				if (e is HandledMouseEventArgs)
				{
					((HandledMouseEventArgs)e).Handled = true;
				}
			}
			else if (this.HScroll)
			{
				Rectangle clientRectangle2 = base.ClientRectangle;
				int num2 = -this.displayRect.X;
				int val2 = -(clientRectangle2.Width - this.displayRect.Width);
				num2 = Math.Max(num2 - e.Delta, 0);
				num2 = Math.Min(num2, val2);
				this.SetDisplayRectLocation(-num2, this.displayRect.Y);
				this.SyncScrollbars(this.AutoScroll);
				if (e is HandledMouseEventArgs)
				{
					((HandledMouseEventArgs)e).Handled = true;
				}
			}
			base.OnMouseWheel(e);
		}

		// Token: 0x060037CE RID: 14286 RVA: 0x000F8D5E File Offset: 0x000F6F5E
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnRightToLeftChanged(EventArgs e)
		{
			base.OnRightToLeftChanged(e);
			this.resetRTLHScrollValue = true;
			LayoutTransaction.DoLayout(this, this, PropertyNames.RightToLeft);
		}

		// Token: 0x060037CF RID: 14287 RVA: 0x000F8D7C File Offset: 0x000F6F7C
		protected override void OnPaintBackground(PaintEventArgs e)
		{
			if ((this.HScroll || this.VScroll) && this.BackgroundImage != null && (this.BackgroundImageLayout == ImageLayout.Zoom || this.BackgroundImageLayout == ImageLayout.Stretch || this.BackgroundImageLayout == ImageLayout.Center))
			{
				if (ControlPaint.IsImageTransparent(this.BackgroundImage))
				{
					base.PaintTransparentBackground(e, this.displayRect);
				}
				ControlPaint.DrawBackgroundImage(e.Graphics, this.BackgroundImage, this.BackColor, this.BackgroundImageLayout, this.displayRect, this.displayRect, this.displayRect.Location);
				return;
			}
			base.OnPaintBackground(e);
		}

		// Token: 0x060037D0 RID: 14288 RVA: 0x000F8E14 File Offset: 0x000F7014
		protected override void OnPaddingChanged(EventArgs e)
		{
			EventHandler eventHandler = (EventHandler)base.Events[Control.EventPaddingChanged];
			if (eventHandler != null)
			{
				eventHandler(this, e);
			}
		}

		// Token: 0x060037D1 RID: 14289 RVA: 0x000F8E42 File Offset: 0x000F7042
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		protected override void OnVisibleChanged(EventArgs e)
		{
			if (base.Visible)
			{
				LayoutTransaction.DoLayout(this, this, PropertyNames.Visible);
			}
			base.OnVisibleChanged(e);
		}

		// Token: 0x060037D2 RID: 14290 RVA: 0x000F8E5F File Offset: 0x000F705F
		internal void ScaleDockPadding(float dx, float dy)
		{
			if (this.dockPadding != null)
			{
				this.dockPadding.Scale(dx, dy);
			}
		}

		// Token: 0x060037D3 RID: 14291 RVA: 0x000F8E76 File Offset: 0x000F7076
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected override void ScaleCore(float dx, float dy)
		{
			this.ScaleDockPadding(dx, dy);
			base.ScaleCore(dx, dy);
		}

		// Token: 0x060037D4 RID: 14292 RVA: 0x000F8E88 File Offset: 0x000F7088
		protected override void ScaleControl(SizeF factor, BoundsSpecified specified)
		{
			this.ScaleDockPadding(factor.Width, factor.Height);
			base.ScaleControl(factor, specified);
		}

		// Token: 0x060037D5 RID: 14293 RVA: 0x000F8EA8 File Offset: 0x000F70A8
		internal void SetDisplayFromScrollProps(int x, int y)
		{
			Rectangle displayRectInternal = this.GetDisplayRectInternal();
			this.ApplyScrollbarChanges(displayRectInternal);
			this.SetDisplayRectLocation(x, y);
		}

		// Token: 0x060037D6 RID: 14294 RVA: 0x000F8ECC File Offset: 0x000F70CC
		protected void SetDisplayRectLocation(int x, int y)
		{
			int num = 0;
			int num2 = 0;
			Rectangle clientRectangle = base.ClientRectangle;
			Rectangle rectangle = this.displayRect;
			int num3 = Math.Min(clientRectangle.Width - rectangle.Width, 0);
			int num4 = Math.Min(clientRectangle.Height - rectangle.Height, 0);
			if (x > 0)
			{
				x = 0;
			}
			if (y > 0)
			{
				y = 0;
			}
			if (x < num3)
			{
				x = num3;
			}
			if (y < num4)
			{
				y = num4;
			}
			if (rectangle.X != x)
			{
				num = x - rectangle.X;
			}
			if (rectangle.Y != y)
			{
				num2 = y - rectangle.Y;
			}
			this.displayRect.X = x;
			this.displayRect.Y = y;
			if (num != 0 || (num2 != 0 && base.IsHandleCreated))
			{
				Rectangle clientRectangle2 = base.ClientRectangle;
				NativeMethods.RECT rect = NativeMethods.RECT.FromXYWH(clientRectangle2.X, clientRectangle2.Y, clientRectangle2.Width, clientRectangle2.Height);
				NativeMethods.RECT rect2 = NativeMethods.RECT.FromXYWH(clientRectangle2.X, clientRectangle2.Y, clientRectangle2.Width, clientRectangle2.Height);
				SafeNativeMethods.ScrollWindowEx(new HandleRef(this, base.Handle), num, num2, null, ref rect, NativeMethods.NullHandleRef, ref rect2, 7);
			}
			for (int i = 0; i < base.Controls.Count; i++)
			{
				Control control = base.Controls[i];
				if (control != null && control.IsHandleCreated)
				{
					control.UpdateBounds();
				}
			}
		}

		// Token: 0x060037D7 RID: 14295 RVA: 0x000F9034 File Offset: 0x000F7234
		public void ScrollControlIntoView(Control activeControl)
		{
			Rectangle clientRectangle = base.ClientRectangle;
			if (base.IsDescendant(activeControl) && this.AutoScroll && (this.HScroll || this.VScroll) && activeControl != null && clientRectangle.Width > 0 && clientRectangle.Height > 0)
			{
				Point point = this.ScrollToControl(activeControl);
				this.SetScrollState(8, false);
				this.SetDisplayRectLocation(point.X, point.Y);
				this.SyncScrollbars(true);
			}
		}

		// Token: 0x060037D8 RID: 14296 RVA: 0x000F90AC File Offset: 0x000F72AC
		protected virtual Point ScrollToControl(Control activeControl)
		{
			Rectangle clientRectangle = base.ClientRectangle;
			int num = this.displayRect.X;
			int num2 = this.displayRect.Y;
			int width = this.scrollMargin.Width;
			int height = this.scrollMargin.Height;
			Rectangle r = activeControl.Bounds;
			if (activeControl.ParentInternal != this)
			{
				r = base.RectangleToClient(activeControl.ParentInternal.RectangleToScreen(r));
			}
			if (r.X < width)
			{
				num = this.displayRect.X + width - r.X;
			}
			else if (r.X + r.Width + width > clientRectangle.Width)
			{
				num = clientRectangle.Width - (r.X + r.Width + width - this.displayRect.X);
				if (r.X + num - this.displayRect.X < width)
				{
					num = this.displayRect.X + width - r.X;
				}
			}
			if (r.Y < height)
			{
				num2 = this.displayRect.Y + height - r.Y;
			}
			else if (r.Y + r.Height + height > clientRectangle.Height)
			{
				num2 = clientRectangle.Height - (r.Y + r.Height + height - this.displayRect.Y);
				if (r.Y + num2 - this.displayRect.Y < height)
				{
					num2 = this.displayRect.Y + height - r.Y;
				}
			}
			num += activeControl.AutoScrollOffset.X;
			num2 += activeControl.AutoScrollOffset.Y;
			return new Point(num, num2);
		}

		// Token: 0x060037D9 RID: 14297 RVA: 0x000F9268 File Offset: 0x000F7468
		private int ScrollThumbPosition(int fnBar)
		{
			NativeMethods.SCROLLINFO scrollinfo = new NativeMethods.SCROLLINFO();
			scrollinfo.fMask = 16;
			SafeNativeMethods.GetScrollInfo(new HandleRef(this, base.Handle), fnBar, scrollinfo);
			return scrollinfo.nTrackPos;
		}

		// Token: 0x14000298 RID: 664
		// (add) Token: 0x060037DA RID: 14298 RVA: 0x000F929D File Offset: 0x000F749D
		// (remove) Token: 0x060037DB RID: 14299 RVA: 0x000F92B0 File Offset: 0x000F74B0
		[SRCategory("CatAction")]
		[SRDescription("ScrollBarOnScrollDescr")]
		public event ScrollEventHandler Scroll
		{
			add
			{
				base.Events.AddHandler(ScrollableControl.EVENT_SCROLL, value);
			}
			remove
			{
				base.Events.RemoveHandler(ScrollableControl.EVENT_SCROLL, value);
			}
		}

		// Token: 0x060037DC RID: 14300 RVA: 0x000F92C4 File Offset: 0x000F74C4
		protected virtual void OnScroll(ScrollEventArgs se)
		{
			ScrollEventHandler scrollEventHandler = (ScrollEventHandler)base.Events[ScrollableControl.EVENT_SCROLL];
			if (scrollEventHandler != null)
			{
				scrollEventHandler(this, se);
			}
		}

		// Token: 0x060037DD RID: 14301 RVA: 0x000F92F2 File Offset: 0x000F74F2
		private void ResetAutoScrollMargin()
		{
			this.AutoScrollMargin = Size.Empty;
		}

		// Token: 0x060037DE RID: 14302 RVA: 0x000F92FF File Offset: 0x000F74FF
		private void ResetAutoScrollMinSize()
		{
			this.AutoScrollMinSize = Size.Empty;
		}

		// Token: 0x060037DF RID: 14303 RVA: 0x000F930C File Offset: 0x000F750C
		private void ResetScrollProperties(ScrollProperties scrollProperties)
		{
			scrollProperties.visible = false;
			scrollProperties.value = 0;
		}

		// Token: 0x060037E0 RID: 14304 RVA: 0x000F931C File Offset: 0x000F751C
		public void SetAutoScrollMargin(int x, int y)
		{
			if (x < 0)
			{
				x = 0;
			}
			if (y < 0)
			{
				y = 0;
			}
			if (x != this.requestedScrollMargin.Width || y != this.requestedScrollMargin.Height)
			{
				this.requestedScrollMargin = new Size(x, y);
				if (this.AutoScroll)
				{
					base.PerformLayout();
				}
			}
		}

		// Token: 0x060037E1 RID: 14305 RVA: 0x000F9370 File Offset: 0x000F7570
		private bool SetVisibleScrollbars(bool horiz, bool vert)
		{
			bool flag = false;
			if ((!horiz && this.HScroll) || (horiz && !this.HScroll) || (!vert && this.VScroll) || (vert && !this.VScroll))
			{
				flag = true;
			}
			if (horiz && !this.HScroll && this.RightToLeft == RightToLeft.Yes)
			{
				this.resetRTLHScrollValue = true;
			}
			if (flag)
			{
				int x = this.displayRect.X;
				int y = this.displayRect.Y;
				if (!horiz)
				{
					x = 0;
				}
				if (!vert)
				{
					y = 0;
				}
				this.SetDisplayRectLocation(x, y);
				this.SetScrollState(8, false);
				this.HScroll = horiz;
				this.VScroll = vert;
				if (horiz)
				{
					this.HorizontalScroll.visible = true;
				}
				else
				{
					this.ResetScrollProperties(this.HorizontalScroll);
				}
				if (vert)
				{
					this.VerticalScroll.visible = true;
				}
				else
				{
					this.ResetScrollProperties(this.VerticalScroll);
				}
				base.UpdateStyles();
			}
			return flag;
		}

		// Token: 0x060037E2 RID: 14306 RVA: 0x000F9450 File Offset: 0x000F7650
		private bool SetDisplayRectangleSize(int width, int height)
		{
			bool result = false;
			if (this.displayRect.Width != width || this.displayRect.Height != height)
			{
				this.displayRect.Width = width;
				this.displayRect.Height = height;
				result = true;
			}
			int num = base.ClientRectangle.Width - width;
			int num2 = base.ClientRectangle.Height - height;
			if (num > 0)
			{
				num = 0;
			}
			if (num2 > 0)
			{
				num2 = 0;
			}
			int num3 = this.displayRect.X;
			int num4 = this.displayRect.Y;
			if (!this.HScroll)
			{
				num3 = 0;
			}
			if (!this.VScroll)
			{
				num4 = 0;
			}
			if (num3 < num)
			{
				num3 = num;
			}
			if (num4 < num2)
			{
				num4 = num2;
			}
			this.SetDisplayRectLocation(num3, num4);
			return result;
		}

		// Token: 0x060037E3 RID: 14307 RVA: 0x000F950B File Offset: 0x000F770B
		protected void SetScrollState(int bit, bool value)
		{
			if (value)
			{
				this.scrollState |= bit;
				return;
			}
			this.scrollState &= ~bit;
		}

		// Token: 0x060037E4 RID: 14308 RVA: 0x000F9530 File Offset: 0x000F7730
		private bool ShouldSerializeAutoScrollPosition()
		{
			if (this.AutoScroll)
			{
				Point autoScrollPosition = this.AutoScrollPosition;
				if (autoScrollPosition.X != 0 || autoScrollPosition.Y != 0)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060037E5 RID: 14309 RVA: 0x000F9564 File Offset: 0x000F7764
		private bool ShouldSerializeAutoScrollMargin()
		{
			return !this.AutoScrollMargin.Equals(new Size(0, 0));
		}

		// Token: 0x060037E6 RID: 14310 RVA: 0x000F9594 File Offset: 0x000F7794
		private bool ShouldSerializeAutoScrollMinSize()
		{
			return !this.AutoScrollMinSize.Equals(new Size(0, 0));
		}

		// Token: 0x060037E7 RID: 14311 RVA: 0x000F95C4 File Offset: 0x000F77C4
		private void SyncScrollbars(bool autoScroll)
		{
			Rectangle rectangle = this.displayRect;
			if (autoScroll)
			{
				if (!base.IsHandleCreated)
				{
					return;
				}
				if (this.HScroll)
				{
					if (!this.HorizontalScroll.maximumSetExternally)
					{
						this.HorizontalScroll.maximum = rectangle.Width - 1;
					}
					if (!this.HorizontalScroll.largeChangeSetExternally)
					{
						this.HorizontalScroll.largeChange = base.ClientRectangle.Width;
					}
					if (!this.HorizontalScroll.smallChangeSetExternally)
					{
						this.HorizontalScroll.smallChange = 5;
					}
					if (this.resetRTLHScrollValue && !base.IsMirrored)
					{
						this.resetRTLHScrollValue = false;
						base.BeginInvoke(new EventHandler(this.OnSetScrollPosition));
					}
					else if (-rectangle.X >= this.HorizontalScroll.minimum && -rectangle.X < this.HorizontalScroll.maximum)
					{
						this.HorizontalScroll.value = -rectangle.X;
					}
					this.HorizontalScroll.UpdateScrollInfo();
				}
				if (this.VScroll)
				{
					if (!this.VerticalScroll.maximumSetExternally)
					{
						this.VerticalScroll.maximum = rectangle.Height - 1;
					}
					if (!this.VerticalScroll.largeChangeSetExternally)
					{
						this.VerticalScroll.largeChange = base.ClientRectangle.Height;
					}
					if (!this.VerticalScroll.smallChangeSetExternally)
					{
						this.VerticalScroll.smallChange = 5;
					}
					if (-rectangle.Y >= this.VerticalScroll.minimum && -rectangle.Y < this.VerticalScroll.maximum)
					{
						this.VerticalScroll.value = -rectangle.Y;
					}
					this.VerticalScroll.UpdateScrollInfo();
					return;
				}
			}
			else
			{
				if (this.HorizontalScroll.Visible)
				{
					this.HorizontalScroll.Value = -rectangle.X;
				}
				else
				{
					this.ResetScrollProperties(this.HorizontalScroll);
				}
				if (this.VerticalScroll.Visible)
				{
					this.VerticalScroll.Value = -rectangle.Y;
					return;
				}
				this.ResetScrollProperties(this.VerticalScroll);
			}
		}

		// Token: 0x060037E8 RID: 14312 RVA: 0x000F97D5 File Offset: 0x000F79D5
		private void OnSetScrollPosition(object sender, EventArgs e)
		{
			if (!base.IsMirrored)
			{
				base.SendMessage(276, NativeMethods.Util.MAKELPARAM((this.RightToLeft == RightToLeft.Yes) ? 7 : 6, 0), 0);
			}
		}

		// Token: 0x060037E9 RID: 14313 RVA: 0x000F97FF File Offset: 0x000F79FF
		private void UpdateFullDrag()
		{
			this.SetScrollState(16, SystemInformation.DragFullWindows);
		}

		// Token: 0x060037EA RID: 14314 RVA: 0x000F9810 File Offset: 0x000F7A10
		private void WmVScroll(ref Message m)
		{
			if (m.LParam != IntPtr.Zero)
			{
				base.WndProc(ref m);
				return;
			}
			Rectangle clientRectangle = base.ClientRectangle;
			bool flag = NativeMethods.Util.LOWORD(m.WParam) != 5;
			int num = -this.displayRect.Y;
			int oldValue = num;
			int num2 = -(clientRectangle.Height - this.displayRect.Height);
			if (!this.AutoScroll)
			{
				num2 = this.VerticalScroll.Maximum;
			}
			switch (NativeMethods.Util.LOWORD(m.WParam))
			{
			case 0:
				if (num > 0)
				{
					num -= this.VerticalScroll.SmallChange;
				}
				else
				{
					num = 0;
				}
				break;
			case 1:
				if (num < num2 - this.VerticalScroll.SmallChange)
				{
					num += this.VerticalScroll.SmallChange;
				}
				else
				{
					num = num2;
				}
				break;
			case 2:
				if (num > this.VerticalScroll.LargeChange)
				{
					num -= this.VerticalScroll.LargeChange;
				}
				else
				{
					num = 0;
				}
				break;
			case 3:
				if (num < num2 - this.VerticalScroll.LargeChange)
				{
					num += this.VerticalScroll.LargeChange;
				}
				else
				{
					num = num2;
				}
				break;
			case 4:
			case 5:
				num = this.ScrollThumbPosition(1);
				break;
			case 6:
				num = 0;
				break;
			case 7:
				num = num2;
				break;
			}
			if (this.GetScrollState(16) || flag)
			{
				this.SetScrollState(8, true);
				this.SetDisplayRectLocation(this.displayRect.X, -num);
				this.SyncScrollbars(this.AutoScroll);
			}
			this.WmOnScroll(ref m, oldValue, num, ScrollOrientation.VerticalScroll);
		}

		// Token: 0x060037EB RID: 14315 RVA: 0x000F999C File Offset: 0x000F7B9C
		private void WmHScroll(ref Message m)
		{
			if (m.LParam != IntPtr.Zero)
			{
				base.WndProc(ref m);
				return;
			}
			Rectangle clientRectangle = base.ClientRectangle;
			int num = -this.displayRect.X;
			int oldValue = num;
			int num2 = -(clientRectangle.Width - this.displayRect.Width);
			if (!this.AutoScroll)
			{
				num2 = this.HorizontalScroll.Maximum;
			}
			switch (NativeMethods.Util.LOWORD(m.WParam))
			{
			case 0:
				if (num > this.HorizontalScroll.SmallChange)
				{
					num -= this.HorizontalScroll.SmallChange;
				}
				else
				{
					num = 0;
				}
				break;
			case 1:
				if (num < num2 - this.HorizontalScroll.SmallChange)
				{
					num += this.HorizontalScroll.SmallChange;
				}
				else
				{
					num = num2;
				}
				break;
			case 2:
				if (num > this.HorizontalScroll.LargeChange)
				{
					num -= this.HorizontalScroll.LargeChange;
				}
				else
				{
					num = 0;
				}
				break;
			case 3:
				if (num < num2 - this.HorizontalScroll.LargeChange)
				{
					num += this.HorizontalScroll.LargeChange;
				}
				else
				{
					num = num2;
				}
				break;
			case 4:
			case 5:
				num = this.ScrollThumbPosition(0);
				break;
			case 6:
				num = 0;
				break;
			case 7:
				num = num2;
				break;
			}
			if (this.GetScrollState(16) || NativeMethods.Util.LOWORD(m.WParam) != 5)
			{
				this.SetScrollState(8, true);
				this.SetDisplayRectLocation(-num, this.displayRect.Y);
				this.SyncScrollbars(this.AutoScroll);
			}
			this.WmOnScroll(ref m, oldValue, num, ScrollOrientation.HorizontalScroll);
		}

		// Token: 0x060037EC RID: 14316 RVA: 0x000F9B24 File Offset: 0x000F7D24
		private void WmOnScroll(ref Message m, int oldValue, int value, ScrollOrientation scrollOrientation)
		{
			ScrollEventType scrollEventType = (ScrollEventType)NativeMethods.Util.LOWORD(m.WParam);
			if (scrollEventType != ScrollEventType.EndScroll)
			{
				ScrollEventArgs se = new ScrollEventArgs(scrollEventType, oldValue, value, scrollOrientation);
				this.OnScroll(se);
			}
		}

		// Token: 0x060037ED RID: 14317 RVA: 0x000F9B53 File Offset: 0x000F7D53
		private void WmSettingChange(ref Message m)
		{
			base.WndProc(ref m);
			this.UpdateFullDrag();
		}

		// Token: 0x060037EE RID: 14318 RVA: 0x000F9B64 File Offset: 0x000F7D64
		[EditorBrowsable(EditorBrowsableState.Advanced)]
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.UnmanagedCode)]
		protected override void WndProc(ref Message m)
		{
			int msg = m.Msg;
			if (msg == 26)
			{
				this.WmSettingChange(ref m);
				return;
			}
			if (msg == 276)
			{
				this.WmHScroll(ref m);
				return;
			}
			if (msg == 277)
			{
				this.WmVScroll(ref m);
				return;
			}
			base.WndProc(ref m);
		}

		// Token: 0x04002176 RID: 8566
		internal static readonly TraceSwitch AutoScrolling;

		// Token: 0x04002177 RID: 8567
		protected const int ScrollStateAutoScrolling = 1;

		// Token: 0x04002178 RID: 8568
		protected const int ScrollStateHScrollVisible = 2;

		// Token: 0x04002179 RID: 8569
		protected const int ScrollStateVScrollVisible = 4;

		// Token: 0x0400217A RID: 8570
		protected const int ScrollStateUserHasScrolled = 8;

		// Token: 0x0400217B RID: 8571
		protected const int ScrollStateFullDrag = 16;

		// Token: 0x0400217C RID: 8572
		private Size userAutoScrollMinSize = Size.Empty;

		// Token: 0x0400217D RID: 8573
		private Rectangle displayRect = Rectangle.Empty;

		// Token: 0x0400217E RID: 8574
		private Size scrollMargin = Size.Empty;

		// Token: 0x0400217F RID: 8575
		private Size requestedScrollMargin = Size.Empty;

		// Token: 0x04002180 RID: 8576
		internal Point scrollPosition = Point.Empty;

		// Token: 0x04002181 RID: 8577
		private ScrollableControl.DockPaddingEdges dockPadding;

		// Token: 0x04002182 RID: 8578
		private int scrollState;

		// Token: 0x04002183 RID: 8579
		private VScrollProperties verticalScroll;

		// Token: 0x04002184 RID: 8580
		private HScrollProperties horizontalScroll;

		// Token: 0x04002185 RID: 8581
		private static readonly object EVENT_SCROLL = new object();

		// Token: 0x04002186 RID: 8582
		private bool resetRTLHScrollValue;

		// Token: 0x020007DF RID: 2015
		[TypeConverter(typeof(ScrollableControl.DockPaddingEdgesConverter))]
		public class DockPaddingEdges : ICloneable
		{
			// Token: 0x06006DD6 RID: 28118 RVA: 0x0019319A File Offset: 0x0019139A
			internal DockPaddingEdges(ScrollableControl owner)
			{
				this.owner = owner;
			}

			// Token: 0x06006DD7 RID: 28119 RVA: 0x001931A9 File Offset: 0x001913A9
			internal DockPaddingEdges(int left, int right, int top, int bottom)
			{
				this.left = left;
				this.right = right;
				this.top = top;
				this.bottom = bottom;
			}

			// Token: 0x17001801 RID: 6145
			// (get) Token: 0x06006DD8 RID: 28120 RVA: 0x001931D0 File Offset: 0x001913D0
			// (set) Token: 0x06006DD9 RID: 28121 RVA: 0x0019329B File Offset: 0x0019149B
			[RefreshProperties(RefreshProperties.All)]
			[SRDescription("PaddingAllDescr")]
			public int All
			{
				get
				{
					if (this.owner == null)
					{
						if (this.left == this.right && this.top == this.bottom && this.left == this.top)
						{
							return this.left;
						}
						return 0;
					}
					else
					{
						if (this.owner.Padding.All == -1 && (this.owner.Padding.Left != -1 || this.owner.Padding.Top != -1 || this.owner.Padding.Right != -1 || this.owner.Padding.Bottom != -1))
						{
							return 0;
						}
						return this.owner.Padding.All;
					}
				}
				set
				{
					if (this.owner == null)
					{
						this.left = value;
						this.top = value;
						this.right = value;
						this.bottom = value;
						return;
					}
					this.owner.Padding = new Padding(value);
				}
			}

			// Token: 0x17001802 RID: 6146
			// (get) Token: 0x06006DDA RID: 28122 RVA: 0x001932D4 File Offset: 0x001914D4
			// (set) Token: 0x06006DDB RID: 28123 RVA: 0x00193304 File Offset: 0x00191504
			[RefreshProperties(RefreshProperties.All)]
			[SRDescription("PaddingBottomDescr")]
			public int Bottom
			{
				get
				{
					if (this.owner == null)
					{
						return this.bottom;
					}
					return this.owner.Padding.Bottom;
				}
				set
				{
					if (this.owner == null)
					{
						this.bottom = value;
						return;
					}
					Padding padding = this.owner.Padding;
					padding.Bottom = value;
					this.owner.Padding = padding;
				}
			}

			// Token: 0x17001803 RID: 6147
			// (get) Token: 0x06006DDC RID: 28124 RVA: 0x00193344 File Offset: 0x00191544
			// (set) Token: 0x06006DDD RID: 28125 RVA: 0x00193374 File Offset: 0x00191574
			[RefreshProperties(RefreshProperties.All)]
			[SRDescription("PaddingLeftDescr")]
			public int Left
			{
				get
				{
					if (this.owner == null)
					{
						return this.left;
					}
					return this.owner.Padding.Left;
				}
				set
				{
					if (this.owner == null)
					{
						this.left = value;
						return;
					}
					Padding padding = this.owner.Padding;
					padding.Left = value;
					this.owner.Padding = padding;
				}
			}

			// Token: 0x17001804 RID: 6148
			// (get) Token: 0x06006DDE RID: 28126 RVA: 0x001933B4 File Offset: 0x001915B4
			// (set) Token: 0x06006DDF RID: 28127 RVA: 0x001933E4 File Offset: 0x001915E4
			[RefreshProperties(RefreshProperties.All)]
			[SRDescription("PaddingRightDescr")]
			public int Right
			{
				get
				{
					if (this.owner == null)
					{
						return this.right;
					}
					return this.owner.Padding.Right;
				}
				set
				{
					if (this.owner == null)
					{
						this.right = value;
						return;
					}
					Padding padding = this.owner.Padding;
					padding.Right = value;
					this.owner.Padding = padding;
				}
			}

			// Token: 0x17001805 RID: 6149
			// (get) Token: 0x06006DE0 RID: 28128 RVA: 0x00193424 File Offset: 0x00191624
			// (set) Token: 0x06006DE1 RID: 28129 RVA: 0x00193454 File Offset: 0x00191654
			[RefreshProperties(RefreshProperties.All)]
			[SRDescription("PaddingTopDescr")]
			public int Top
			{
				get
				{
					if (this.owner == null)
					{
						return this.bottom;
					}
					return this.owner.Padding.Top;
				}
				set
				{
					if (this.owner == null)
					{
						this.top = value;
						return;
					}
					Padding padding = this.owner.Padding;
					padding.Top = value;
					this.owner.Padding = padding;
				}
			}

			// Token: 0x06006DE2 RID: 28130 RVA: 0x00193494 File Offset: 0x00191694
			public override bool Equals(object other)
			{
				ScrollableControl.DockPaddingEdges dockPaddingEdges = other as ScrollableControl.DockPaddingEdges;
				return dockPaddingEdges != null && this.owner.Padding.Equals(dockPaddingEdges.owner.Padding);
			}

			// Token: 0x06006DE3 RID: 28131 RVA: 0x0014D6AD File Offset: 0x0014B8AD
			public override int GetHashCode()
			{
				return base.GetHashCode();
			}

			// Token: 0x06006DE4 RID: 28132 RVA: 0x001934D6 File Offset: 0x001916D6
			private void ResetAll()
			{
				this.All = 0;
			}

			// Token: 0x06006DE5 RID: 28133 RVA: 0x001934DF File Offset: 0x001916DF
			private void ResetBottom()
			{
				this.Bottom = 0;
			}

			// Token: 0x06006DE6 RID: 28134 RVA: 0x001934E8 File Offset: 0x001916E8
			private void ResetLeft()
			{
				this.Left = 0;
			}

			// Token: 0x06006DE7 RID: 28135 RVA: 0x001934F1 File Offset: 0x001916F1
			private void ResetRight()
			{
				this.Right = 0;
			}

			// Token: 0x06006DE8 RID: 28136 RVA: 0x001934FA File Offset: 0x001916FA
			private void ResetTop()
			{
				this.Top = 0;
			}

			// Token: 0x06006DE9 RID: 28137 RVA: 0x00193504 File Offset: 0x00191704
			internal void Scale(float dx, float dy)
			{
				this.owner.Padding.Scale(dx, dy);
			}

			// Token: 0x06006DEA RID: 28138 RVA: 0x000F1AC4 File Offset: 0x000EFCC4
			public override string ToString()
			{
				return "";
			}

			// Token: 0x06006DEB RID: 28139 RVA: 0x00193528 File Offset: 0x00191728
			object ICloneable.Clone()
			{
				return new ScrollableControl.DockPaddingEdges(this.Left, this.Right, this.Top, this.Bottom);
			}

			// Token: 0x040042BB RID: 17083
			private ScrollableControl owner;

			// Token: 0x040042BC RID: 17084
			private int left;

			// Token: 0x040042BD RID: 17085
			private int right;

			// Token: 0x040042BE RID: 17086
			private int top;

			// Token: 0x040042BF RID: 17087
			private int bottom;
		}

		// Token: 0x020007E0 RID: 2016
		public class DockPaddingEdgesConverter : TypeConverter
		{
			// Token: 0x06006DEC RID: 28140 RVA: 0x00193554 File Offset: 0x00191754
			public override PropertyDescriptorCollection GetProperties(ITypeDescriptorContext context, object value, Attribute[] attributes)
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(typeof(ScrollableControl.DockPaddingEdges), attributes);
				return properties.Sort(new string[]
				{
					"All",
					"Left",
					"Top",
					"Right",
					"Bottom"
				});
			}

			// Token: 0x06006DED RID: 28141 RVA: 0x00013062 File Offset: 0x00011262
			public override bool GetPropertiesSupported(ITypeDescriptorContext context)
			{
				return true;
			}
		}
	}
}

using System;
using System.Collections;
using System.Design;
using System.Drawing;
using System.Windows.Forms.Design.Behavior;

namespace System.Windows.Forms.Design
{
	// Token: 0x0200034C RID: 844
	internal sealed class ToolStripAdornerWindowService : IDisposable
	{
		// Token: 0x0600215F RID: 8543 RVA: 0x000CB998 File Offset: 0x000C9B98
		internal ToolStripAdornerWindowService(IServiceProvider serviceProvider, Control windowFrame)
		{
			this.serviceProvider = serviceProvider;
			this.toolStripAdornerWindow = new ToolStripAdornerWindowService.ToolStripAdornerWindow(windowFrame);
			this.bs = (BehaviorService)serviceProvider.GetService(typeof(BehaviorService));
			int adornerWindowIndex = this.bs.AdornerWindowIndex;
			this.os = (IOverlayService)serviceProvider.GetService(typeof(IOverlayService));
			if (this.os != null)
			{
				this.os.InsertOverlay(this.toolStripAdornerWindow, adornerWindowIndex);
			}
			this.dropDownAdorner = new Adorner();
			int count = this.bs.Adorners.Count;
			if (count > 1)
			{
				this.bs.Adorners.Insert(count - 1, this.dropDownAdorner);
			}
		}

		// Token: 0x1700070C RID: 1804
		// (get) Token: 0x06002160 RID: 8544 RVA: 0x000CBA53 File Offset: 0x000C9C53
		internal Control ToolStripAdornerWindowControl
		{
			get
			{
				return this.toolStripAdornerWindow;
			}
		}

		// Token: 0x1700070D RID: 1805
		// (get) Token: 0x06002161 RID: 8545 RVA: 0x000CBA5B File Offset: 0x000C9C5B
		public Graphics ToolStripAdornerWindowGraphics
		{
			get
			{
				return this.toolStripAdornerWindow.CreateGraphics();
			}
		}

		// Token: 0x1700070E RID: 1806
		// (get) Token: 0x06002162 RID: 8546 RVA: 0x000CBA68 File Offset: 0x000C9C68
		internal Adorner DropDownAdorner
		{
			get
			{
				return this.dropDownAdorner;
			}
		}

		// Token: 0x06002163 RID: 8547 RVA: 0x000CBA70 File Offset: 0x000C9C70
		public void Dispose()
		{
			if (this.os != null)
			{
				this.os.RemoveOverlay(this.toolStripAdornerWindow);
			}
			this.toolStripAdornerWindow.Dispose();
			if (this.bs != null)
			{
				this.bs.Adorners.Remove(this.dropDownAdorner);
				this.bs = null;
			}
			if (this.dropDownAdorner != null)
			{
				this.dropDownAdorner.Glyphs.Clear();
				this.dropDownAdorner = null;
			}
		}

		// Token: 0x06002164 RID: 8548 RVA: 0x000CBAE8 File Offset: 0x000C9CE8
		public Point AdornerWindowPointToScreen(Point p)
		{
			NativeMethods.POINT point = new NativeMethods.POINT(p.X, p.Y);
			NativeMethods.MapWindowPoints(this.toolStripAdornerWindow.Handle, IntPtr.Zero, point, 1);
			return new Point(point.x, point.y);
		}

		// Token: 0x06002165 RID: 8549 RVA: 0x000CBB34 File Offset: 0x000C9D34
		public Point AdornerWindowToScreen()
		{
			Point p = new Point(0, 0);
			return this.AdornerWindowPointToScreen(p);
		}

		// Token: 0x06002166 RID: 8550 RVA: 0x000CBB54 File Offset: 0x000C9D54
		public Point ControlToAdornerWindow(Control c)
		{
			if (c.Parent == null)
			{
				return Point.Empty;
			}
			NativeMethods.POINT point = new NativeMethods.POINT();
			point.x = c.Left;
			point.y = c.Top;
			NativeMethods.MapWindowPoints(c.Parent.Handle, this.toolStripAdornerWindow.Handle, point, 1);
			return new Point(point.x, point.y);
		}

		// Token: 0x06002167 RID: 8551 RVA: 0x000CBBBC File Offset: 0x000C9DBC
		public void Invalidate()
		{
			this.toolStripAdornerWindow.InvalidateAdornerWindow();
		}

		// Token: 0x06002168 RID: 8552 RVA: 0x000CBBC9 File Offset: 0x000C9DC9
		public void Invalidate(Rectangle rect)
		{
			this.toolStripAdornerWindow.InvalidateAdornerWindow(rect);
		}

		// Token: 0x06002169 RID: 8553 RVA: 0x000CBBD7 File Offset: 0x000C9DD7
		public void Invalidate(Region r)
		{
			this.toolStripAdornerWindow.InvalidateAdornerWindow(r);
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x0600216A RID: 8554 RVA: 0x000CBBE5 File Offset: 0x000C9DE5
		// (set) Token: 0x0600216B RID: 8555 RVA: 0x000CBBED File Offset: 0x000C9DED
		internal ArrayList DropDowns
		{
			get
			{
				return this.dropDownCollection;
			}
			set
			{
				if (this.dropDownCollection == null)
				{
					this.dropDownCollection = new ArrayList();
				}
			}
		}

		// Token: 0x0600216C RID: 8556 RVA: 0x000CBC02 File Offset: 0x000C9E02
		internal void ProcessPaintMessage(Rectangle paintRect)
		{
			this.toolStripAdornerWindow.Invalidate(paintRect);
		}

		// Token: 0x04001937 RID: 6455
		private IServiceProvider serviceProvider;

		// Token: 0x04001938 RID: 6456
		private ToolStripAdornerWindowService.ToolStripAdornerWindow toolStripAdornerWindow;

		// Token: 0x04001939 RID: 6457
		private BehaviorService bs;

		// Token: 0x0400193A RID: 6458
		private Adorner dropDownAdorner;

		// Token: 0x0400193B RID: 6459
		private ArrayList dropDownCollection;

		// Token: 0x0400193C RID: 6460
		private IOverlayService os;

		// Token: 0x02000595 RID: 1429
		private class ToolStripAdornerWindow : Control
		{
			// Token: 0x0600331F RID: 13087 RVA: 0x00116650 File Offset: 0x00114850
			internal ToolStripAdornerWindow(Control designerFrame)
			{
				this.designerFrame = designerFrame;
				this.Dock = DockStyle.Fill;
				this.AllowDrop = true;
				this.Text = "ToolStripAdornerWindow";
				base.SetStyle(ControlStyles.Opaque, true);
			}

			// Token: 0x170009FE RID: 2558
			// (get) Token: 0x06003320 RID: 13088 RVA: 0x00116680 File Offset: 0x00114880
			protected override CreateParams CreateParams
			{
				get
				{
					CreateParams createParams = base.CreateParams;
					createParams.Style &= -100663297;
					createParams.ExStyle |= 32;
					return createParams;
				}
			}

			// Token: 0x06003321 RID: 13089 RVA: 0x001166B6 File Offset: 0x001148B6
			protected override void OnHandleCreated(EventArgs e)
			{
				base.OnHandleCreated(e);
			}

			// Token: 0x06003322 RID: 13090 RVA: 0x001166BF File Offset: 0x001148BF
			protected override void OnHandleDestroyed(EventArgs e)
			{
				base.OnHandleDestroyed(e);
			}

			// Token: 0x06003323 RID: 13091 RVA: 0x001166C8 File Offset: 0x001148C8
			protected override void Dispose(bool disposing)
			{
				if (disposing && this.designerFrame != null)
				{
					this.designerFrame = null;
				}
				base.Dispose(disposing);
			}

			// Token: 0x170009FF RID: 2559
			// (get) Token: 0x06003324 RID: 13092 RVA: 0x001166E3 File Offset: 0x001148E3
			private bool DesignerFrameValid
			{
				get
				{
					return this.designerFrame != null && !this.designerFrame.IsDisposed && this.designerFrame.IsHandleCreated;
				}
			}

			// Token: 0x06003325 RID: 13093 RVA: 0x0011670A File Offset: 0x0011490A
			internal void InvalidateAdornerWindow()
			{
				if (this.DesignerFrameValid)
				{
					this.designerFrame.Invalidate(true);
					this.designerFrame.Update();
				}
			}

			// Token: 0x06003326 RID: 13094 RVA: 0x0011672B File Offset: 0x0011492B
			internal void InvalidateAdornerWindow(Region region)
			{
				if (this.DesignerFrameValid)
				{
					this.designerFrame.Invalidate(region, true);
					this.designerFrame.Update();
				}
			}

			// Token: 0x06003327 RID: 13095 RVA: 0x0011674D File Offset: 0x0011494D
			internal void InvalidateAdornerWindow(Rectangle rectangle)
			{
				if (this.DesignerFrameValid)
				{
					this.designerFrame.Invalidate(rectangle, true);
					this.designerFrame.Update();
				}
			}

			// Token: 0x06003328 RID: 13096 RVA: 0x00116770 File Offset: 0x00114970
			protected override void WndProc(ref Message m)
			{
				int msg = m.Msg;
				if (msg == 132)
				{
					m.Result = (IntPtr)(-1);
					return;
				}
				base.WndProc(ref m);
			}

			// Token: 0x0400222B RID: 8747
			private Control designerFrame;
		}
	}
}

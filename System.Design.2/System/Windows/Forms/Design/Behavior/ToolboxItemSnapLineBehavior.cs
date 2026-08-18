using System;
using System.Collections;
using System.ComponentModel.Design;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x02000393 RID: 915
	internal class ToolboxItemSnapLineBehavior : Behavior
	{
		// Token: 0x06002551 RID: 9553 RVA: 0x000E9A80 File Offset: 0x000E7C80
		public ToolboxItemSnapLineBehavior(IServiceProvider serviceProvider, BehaviorService behaviorService)
		{
			this.serviceProvider = serviceProvider;
			this.behaviorService = behaviorService;
			this.designer = null;
			this.isPushed = false;
			this.lastRectangle = Rectangle.Empty;
			this.lastOffset = Point.Empty;
			this.statusCommandUI = new StatusCommandUI(serviceProvider);
			this.targetAllowsDragBox = true;
			this.targetAllowsSnapLines = true;
		}

		// Token: 0x06002552 RID: 9554 RVA: 0x000E9ADF File Offset: 0x000E7CDF
		public ToolboxItemSnapLineBehavior(IServiceProvider serviceProvider, BehaviorService behaviorService, ControlDesigner controlDesigner) : this(serviceProvider, behaviorService)
		{
			this.designer = controlDesigner;
			if (controlDesigner != null && !controlDesigner.ParticipatesWithSnapLines)
			{
				this.targetAllowsSnapLines = false;
			}
		}

		// Token: 0x06002553 RID: 9555 RVA: 0x000E9B02 File Offset: 0x000E7D02
		public ToolboxItemSnapLineBehavior(IServiceProvider serviceProvider, BehaviorService behaviorService, ControlDesigner controlDesigner, bool allowDragBox) : this(serviceProvider, behaviorService, controlDesigner)
		{
			this.designer = controlDesigner;
			this.targetAllowsDragBox = allowDragBox;
		}

		// Token: 0x170007DF RID: 2015
		// (get) Token: 0x06002554 RID: 9556 RVA: 0x000E9B1C File Offset: 0x000E7D1C
		// (set) Token: 0x06002555 RID: 9557 RVA: 0x000E9B24 File Offset: 0x000E7D24
		public bool IsPushed
		{
			get
			{
				return this.isPushed;
			}
			set
			{
				this.isPushed = value;
				if (this.isPushed)
				{
					if (this.dragManager == null)
					{
						this.dragManager = new DragAssistanceManager(this.serviceProvider);
						return;
					}
				}
				else
				{
					if (!this.lastRectangle.IsEmpty)
					{
						this.behaviorService.Invalidate(this.lastRectangle);
					}
					this.lastOffset = Point.Empty;
					this.lastRectangle = Rectangle.Empty;
					if (this.dragManager != null)
					{
						this.dragManager.OnMouseUp();
						this.dragManager = null;
					}
				}
			}
		}

		// Token: 0x06002556 RID: 9558 RVA: 0x000E9BA8 File Offset: 0x000E7DA8
		private ToolboxSnapDragDropEventArgs CreateToolboxSnapArgs(DragEventArgs e, Point mouseLoc)
		{
			ToolboxSnapDragDropEventArgs.SnapDirection snapDirection = ToolboxSnapDragDropEventArgs.SnapDirection.None;
			Point empty = Point.Empty;
			bool flag = false;
			bool flag2 = false;
			if (this.dragManager != null)
			{
				DragAssistanceManager.Line[] recentLines = this.dragManager.GetRecentLines();
				foreach (DragAssistanceManager.Line line in recentLines)
				{
					if (line.LineType == DragAssistanceManager.LineType.Standard)
					{
						if (!flag && line.x1 == line.x2)
						{
							if (line.x1 == this.lastRectangle.Left)
							{
								snapDirection |= ToolboxSnapDragDropEventArgs.SnapDirection.Left;
								empty.X = this.lastRectangle.Left - mouseLoc.X;
							}
							else
							{
								snapDirection |= ToolboxSnapDragDropEventArgs.SnapDirection.Right;
								empty.X = this.lastRectangle.Right - mouseLoc.X;
							}
							flag = true;
						}
						else if (!flag2 && line.y1 == line.y2)
						{
							if (line.y1 == this.lastRectangle.Top)
							{
								snapDirection |= ToolboxSnapDragDropEventArgs.SnapDirection.Top;
								empty.Y = this.lastRectangle.Top - mouseLoc.Y;
							}
							else if (line.y1 == this.lastRectangle.Bottom)
							{
								snapDirection |= ToolboxSnapDragDropEventArgs.SnapDirection.Bottom;
								empty.Y = this.lastRectangle.Bottom - mouseLoc.Y;
							}
							flag2 = true;
						}
					}
					else if (line.LineType == DragAssistanceManager.LineType.Margin || line.LineType == DragAssistanceManager.LineType.Padding)
					{
						if (!flag2 && line.x1 == line.x2)
						{
							if (Math.Max(line.y1, line.y2) <= this.lastRectangle.Top)
							{
								snapDirection |= ToolboxSnapDragDropEventArgs.SnapDirection.Top;
								empty.Y = this.lastRectangle.Top - mouseLoc.Y;
							}
							else
							{
								snapDirection |= ToolboxSnapDragDropEventArgs.SnapDirection.Bottom;
								empty.Y = this.lastRectangle.Bottom - mouseLoc.Y;
							}
							flag2 = true;
						}
						else if (!flag && line.y1 == line.y2)
						{
							if (Math.Max(line.x1, line.x2) <= this.lastRectangle.Left)
							{
								snapDirection |= ToolboxSnapDragDropEventArgs.SnapDirection.Left;
								empty.X = this.lastRectangle.Left - mouseLoc.X;
							}
							else
							{
								snapDirection |= ToolboxSnapDragDropEventArgs.SnapDirection.Right;
								empty.X = this.lastRectangle.Right - mouseLoc.X;
							}
							flag = true;
						}
					}
					if (flag && flag2)
					{
						break;
					}
				}
			}
			if (!flag)
			{
				snapDirection |= ToolboxSnapDragDropEventArgs.SnapDirection.Left;
				empty.X = this.lastRectangle.Left - mouseLoc.X;
			}
			if (!flag2)
			{
				snapDirection |= ToolboxSnapDragDropEventArgs.SnapDirection.Top;
				empty.Y = this.lastRectangle.Top - mouseLoc.Y;
			}
			return new ToolboxSnapDragDropEventArgs(snapDirection, empty, e);
		}

		// Token: 0x06002557 RID: 9559 RVA: 0x000E9E4C File Offset: 0x000E804C
		private SnapLine[] GenerateNewToolSnapLines(Rectangle r)
		{
			return new SnapLine[]
			{
				new SnapLine(SnapLineType.Left, r.Left),
				new SnapLine(SnapLineType.Right, r.Right),
				new SnapLine(SnapLineType.Bottom, r.Bottom),
				new SnapLine(SnapLineType.Top, r.Top),
				new SnapLine(SnapLineType.Horizontal, r.Top - 4, "Margin.Top", SnapLinePriority.Always),
				new SnapLine(SnapLineType.Horizontal, r.Bottom + 3, "Margin.Bottom", SnapLinePriority.Always),
				new SnapLine(SnapLineType.Vertical, r.Left - 4, "Margin.Left", SnapLinePriority.Always),
				new SnapLine(SnapLineType.Vertical, r.Right + 3, "Margin.Right", SnapLinePriority.Always)
			};
		}

		// Token: 0x06002558 RID: 9560 RVA: 0x000E9F00 File Offset: 0x000E8100
		public override void OnDragDrop(Glyph g, DragEventArgs e)
		{
			this.behaviorService.PopBehavior(this);
			try
			{
				Point point = this.behaviorService.AdornerWindowToScreen();
				ToolboxSnapDragDropEventArgs e2 = this.CreateToolboxSnapArgs(e, new Point(e.X - point.X, e.Y - point.Y));
				base.OnDragDrop(g, e2);
			}
			finally
			{
				this.IsPushed = false;
			}
		}

		// Token: 0x06002559 RID: 9561 RVA: 0x000E9F74 File Offset: 0x000E8174
		public void OnBeginDrag()
		{
			Adorner adorner = null;
			SelectionManager selectionManager = (SelectionManager)this.serviceProvider.GetService(typeof(SelectionManager));
			if (selectionManager != null)
			{
				adorner = selectionManager.BodyGlyphAdorner;
			}
			ArrayList arrayList = new ArrayList();
			foreach (object obj in adorner.Glyphs)
			{
				ControlBodyGlyph controlBodyGlyph = (ControlBodyGlyph)obj;
				Control control = controlBodyGlyph.RelatedComponent as Control;
				if (control != null && !control.AllowDrop)
				{
					arrayList.Add(controlBodyGlyph);
				}
			}
			foreach (object obj2 in arrayList)
			{
				Glyph value = (Glyph)obj2;
				adorner.Glyphs.Remove(value);
			}
		}

		// Token: 0x0600255A RID: 9562 RVA: 0x000EA06C File Offset: 0x000E826C
		public override bool OnMouseMove(Glyph g, MouseButtons button, Point mouseLoc)
		{
			bool flag = Control.ModifierKeys == Keys.Alt;
			if (flag && this.dragManager != null)
			{
				this.dragManager.EraseSnapLines();
			}
			bool result = base.OnMouseMove(g, button, mouseLoc);
			Rectangle rectangle = new Rectangle(mouseLoc.X - DesignerUtils.BOXIMAGESIZE / 2, mouseLoc.Y - DesignerUtils.BOXIMAGESIZE / 2, DesignerUtils.BOXIMAGESIZE, DesignerUtils.BOXIMAGESIZE);
			if (rectangle != this.lastRectangle)
			{
				if (this.dragManager != null && this.targetAllowsSnapLines && !flag)
				{
					this.lastOffset = this.dragManager.OnMouseMove(rectangle, this.GenerateNewToolSnapLines(rectangle));
					rectangle.Offset(this.lastOffset.X, this.lastOffset.Y);
				}
				if (!this.lastRectangle.IsEmpty)
				{
					using (Region region = new Region(this.lastRectangle))
					{
						region.Exclude(rectangle);
						this.behaviorService.Invalidate(region);
					}
				}
				if (this.targetAllowsDragBox)
				{
					using (Graphics adornerWindowGraphics = this.behaviorService.AdornerWindowGraphics)
					{
						adornerWindowGraphics.DrawImage(DesignerUtils.BoxImage, rectangle.Location);
					}
				}
				IDesignerHost designerHost = (IDesignerHost)this.serviceProvider.GetService(typeof(IDesignerHost));
				if (designerHost != null)
				{
					Control control = designerHost.RootComponent as Control;
					if (control != null)
					{
						Point point = this.behaviorService.MapAdornerWindowPoint(control.Handle, new Point(0, 0));
						Rectangle statusInformation = new Rectangle(rectangle.X - point.X, rectangle.Y - point.Y, 0, 0);
						if (this.statusCommandUI != null)
						{
							this.statusCommandUI.SetStatusInformation(statusInformation);
						}
					}
				}
				if (this.dragManager != null && this.targetAllowsSnapLines && !flag)
				{
					this.dragManager.RenderSnapLinesInternal();
				}
				this.lastRectangle = rectangle;
			}
			return result;
		}

		// Token: 0x04001B39 RID: 6969
		private IServiceProvider serviceProvider;

		// Token: 0x04001B3A RID: 6970
		private BehaviorService behaviorService;

		// Token: 0x04001B3B RID: 6971
		private ControlDesigner designer;

		// Token: 0x04001B3C RID: 6972
		private bool isPushed;

		// Token: 0x04001B3D RID: 6973
		private Rectangle lastRectangle;

		// Token: 0x04001B3E RID: 6974
		private Point lastOffset;

		// Token: 0x04001B3F RID: 6975
		private DragAssistanceManager dragManager;

		// Token: 0x04001B40 RID: 6976
		private bool targetAllowsSnapLines;

		// Token: 0x04001B41 RID: 6977
		private StatusCommandUI statusCommandUI;

		// Token: 0x04001B42 RID: 6978
		private bool targetAllowsDragBox;
	}
}

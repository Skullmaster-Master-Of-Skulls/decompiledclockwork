using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Drawing;
using System.Runtime.InteropServices;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x02000389 RID: 905
	internal class ResizeBehavior : Behavior
	{
		// Token: 0x0600250E RID: 9486 RVA: 0x000E6728 File Offset: 0x000E4928
		internal ResizeBehavior(IServiceProvider serviceProvider)
		{
			this.serviceProvider = serviceProvider;
			this.dragging = false;
			this.pushedBehavior = false;
			this.lastSnapOffset = Point.Empty;
			this.didSnap = false;
			this.statusCommandUI = new StatusCommandUI(serviceProvider);
		}

		// Token: 0x170007CD RID: 1997
		// (get) Token: 0x0600250F RID: 9487 RVA: 0x000E6779 File Offset: 0x000E4979
		private BehaviorService BehaviorService
		{
			get
			{
				if (this.behaviorService == null)
				{
					this.behaviorService = (BehaviorService)this.serviceProvider.GetService(typeof(BehaviorService));
				}
				return this.behaviorService;
			}
		}

		// Token: 0x170007CE RID: 1998
		// (get) Token: 0x06002510 RID: 9488 RVA: 0x000E67A9 File Offset: 0x000E49A9
		public override Cursor Cursor
		{
			get
			{
				return this.cursor;
			}
		}

		// Token: 0x06002511 RID: 9489 RVA: 0x000E67B4 File Offset: 0x000E49B4
		private Rectangle AdjustToGrid(Rectangle controlBounds, SelectionRules rules)
		{
			Rectangle result = controlBounds;
			if ((rules & SelectionRules.RightSizeable) != SelectionRules.None)
			{
				int num = controlBounds.Right % this.parentGridSize.Width;
				if (num > this.parentGridSize.Width / 2)
				{
					result.Width += this.parentGridSize.Width - num;
				}
				else
				{
					result.Width -= num;
				}
			}
			else if ((rules & SelectionRules.LeftSizeable) != SelectionRules.None)
			{
				int num2 = controlBounds.Left % this.parentGridSize.Width;
				if (num2 > this.parentGridSize.Width / 2)
				{
					result.X += this.parentGridSize.Width - num2;
					result.Width -= this.parentGridSize.Width - num2;
				}
				else
				{
					result.X -= num2;
					result.Width += num2;
				}
			}
			if ((rules & SelectionRules.BottomSizeable) != SelectionRules.None)
			{
				int num3 = controlBounds.Bottom % this.parentGridSize.Height;
				if (num3 > this.parentGridSize.Height / 2)
				{
					result.Height += this.parentGridSize.Height - num3;
				}
				else
				{
					result.Height -= num3;
				}
			}
			else if ((rules & SelectionRules.TopSizeable) != SelectionRules.None)
			{
				int num4 = controlBounds.Top % this.parentGridSize.Height;
				if (num4 > this.parentGridSize.Height / 2)
				{
					result.Y += this.parentGridSize.Height - num4;
					result.Height -= this.parentGridSize.Height - num4;
				}
				else
				{
					result.Y -= num4;
					result.Height += num4;
				}
			}
			result.Width = Math.Max(result.Width, this.parentGridSize.Width);
			result.Height = Math.Max(result.Height, this.parentGridSize.Height);
			return result;
		}

		// Token: 0x06002512 RID: 9490 RVA: 0x000E69C0 File Offset: 0x000E4BC0
		private SnapLine[] GenerateSnapLines(SelectionRules rules, Point loc)
		{
			ArrayList arrayList = new ArrayList(2);
			if ((rules & SelectionRules.BottomSizeable) != SelectionRules.None)
			{
				arrayList.Add(new SnapLine(SnapLineType.Bottom, loc.Y - 1));
				if (this.primaryControl != null)
				{
					arrayList.Add(new SnapLine(SnapLineType.Horizontal, loc.Y + this.primaryControl.Margin.Bottom, "Margin.Bottom", SnapLinePriority.Always));
				}
			}
			else if ((rules & SelectionRules.TopSizeable) != SelectionRules.None)
			{
				arrayList.Add(new SnapLine(SnapLineType.Top, loc.Y));
				if (this.primaryControl != null)
				{
					arrayList.Add(new SnapLine(SnapLineType.Horizontal, loc.Y - this.primaryControl.Margin.Top, "Margin.Top", SnapLinePriority.Always));
				}
			}
			if ((rules & SelectionRules.RightSizeable) != SelectionRules.None)
			{
				arrayList.Add(new SnapLine(SnapLineType.Right, loc.X - 1));
				if (this.primaryControl != null)
				{
					arrayList.Add(new SnapLine(SnapLineType.Vertical, loc.X + this.primaryControl.Margin.Right, "Margin.Right", SnapLinePriority.Always));
				}
			}
			else if ((rules & SelectionRules.LeftSizeable) != SelectionRules.None)
			{
				arrayList.Add(new SnapLine(SnapLineType.Left, loc.X));
				if (this.primaryControl != null)
				{
					arrayList.Add(new SnapLine(SnapLineType.Vertical, loc.X - this.primaryControl.Margin.Left, "Margin.Left", SnapLinePriority.Always));
				}
			}
			SnapLine[] array = new SnapLine[arrayList.Count];
			arrayList.CopyTo(array);
			return array;
		}

		// Token: 0x06002513 RID: 9491 RVA: 0x000E6B2C File Offset: 0x000E4D2C
		private void InitiateResize()
		{
			bool useSnapLines = this.BehaviorService.UseSnapLines;
			ArrayList arrayList = new ArrayList();
			IDesignerHost designerHost = this.serviceProvider.GetService(typeof(IDesignerHost)) as IDesignerHost;
			for (int i = 0; i < this.resizeComponents.Length; i++)
			{
				this.resizeComponents[i].resizeBounds = ((Control)this.resizeComponents[i].resizeControl).Bounds;
				if (useSnapLines)
				{
					arrayList.Add(this.resizeComponents[i].resizeControl);
				}
				if (designerHost != null)
				{
					ControlDesigner controlDesigner = designerHost.GetDesigner(this.resizeComponents[i].resizeControl as Component) as ControlDesigner;
					if (controlDesigner != null)
					{
						this.resizeComponents[i].resizeRules = controlDesigner.SelectionRules;
					}
					else
					{
						this.resizeComponents[i].resizeRules = SelectionRules.None;
					}
				}
			}
			this.BehaviorService.EnableAllAdorners(false);
			IDesignerHost designerHost2 = (IDesignerHost)this.serviceProvider.GetService(typeof(IDesignerHost));
			if (designerHost2 != null)
			{
				string @string;
				if (this.resizeComponents.Length == 1)
				{
					string text = TypeDescriptor.GetComponentName(this.resizeComponents[0].resizeControl);
					if (text == null || text.Length == 0)
					{
						text = this.resizeComponents[0].resizeControl.GetType().Name;
					}
					@string = SR.GetString("BehaviorServiceResizeControl", new object[]
					{
						text
					});
				}
				else
				{
					@string = SR.GetString("BehaviorServiceResizeControls", new object[]
					{
						this.resizeComponents.Length
					});
				}
				this.resizeTransaction = designerHost2.CreateTransaction(@string);
			}
			this.initialResize = true;
			if (useSnapLines)
			{
				this.dragManager = new DragAssistanceManager(this.serviceProvider, arrayList, true);
			}
			else if (this.resizeComponents.Length != 0)
			{
				Control control = this.resizeComponents[0].resizeControl as Control;
				if (control != null && control.Parent != null)
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(control.Parent)["SnapToGrid"];
					if (propertyDescriptor != null && (bool)propertyDescriptor.GetValue(control.Parent))
					{
						PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(control.Parent)["GridSize"];
						if (propertyDescriptor2 != null)
						{
							this.parentGridSize = (Size)propertyDescriptor2.GetValue(control.Parent);
							this.parentLocation = this.behaviorService.ControlToAdornerWindow(control);
							this.parentLocation.X = this.parentLocation.X - control.Location.X;
							this.parentLocation.Y = this.parentLocation.Y - control.Location.Y;
						}
					}
				}
			}
			this.captureLost = false;
		}

		// Token: 0x06002514 RID: 9492 RVA: 0x000E6E0C File Offset: 0x000E500C
		public override bool OnMouseDown(Glyph g, MouseButtons button, Point mouseLoc)
		{
			if (button != MouseButtons.Left)
			{
				return this.pushedBehavior;
			}
			this.targetResizeRules = SelectionRules.None;
			SelectionGlyphBase selectionGlyphBase = g as SelectionGlyphBase;
			if (selectionGlyphBase != null)
			{
				this.targetResizeRules = selectionGlyphBase.SelectionRules;
				this.cursor = selectionGlyphBase.HitTestCursor;
			}
			if (this.targetResizeRules == SelectionRules.None)
			{
				return false;
			}
			ISelectionService selectionService = (ISelectionService)this.serviceProvider.GetService(typeof(ISelectionService));
			if (selectionService == null)
			{
				return false;
			}
			this.initialPoint = mouseLoc;
			this.lastMouseLoc = mouseLoc;
			this.primaryControl = (selectionService.PrimarySelection as Control);
			ArrayList arrayList = new ArrayList();
			foreach (object obj in selectionService.GetSelectedComponents())
			{
				if (obj is Control)
				{
					PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(obj)["Locked"];
					if (propertyDescriptor == null || !(bool)propertyDescriptor.GetValue(obj))
					{
						arrayList.Add(obj);
					}
				}
			}
			if (arrayList.Count == 0)
			{
				return false;
			}
			this.resizeComponents = new ResizeBehavior.ResizeComponent[arrayList.Count];
			for (int i = 0; i < arrayList.Count; i++)
			{
				this.resizeComponents[i].resizeControl = arrayList[i];
			}
			this.pushedBehavior = true;
			this.BehaviorService.PushCaptureBehavior(this);
			return false;
		}

		// Token: 0x06002515 RID: 9493 RVA: 0x000E6F7C File Offset: 0x000E517C
		public override void OnLoseCapture(Glyph g, EventArgs e)
		{
			this.captureLost = true;
			if (this.pushedBehavior)
			{
				this.pushedBehavior = false;
				if (this.BehaviorService != null)
				{
					if (this.dragging)
					{
						this.dragging = false;
						int num = 0;
						while (!this.captureLost && num < this.resizeComponents.Length)
						{
							Control c = this.resizeComponents[num].resizeControl as Control;
							Rectangle rectangle = this.BehaviorService.ControlRectInAdornerWindow(c);
							if (!rectangle.IsEmpty)
							{
								using (Graphics adornerWindowGraphics = this.BehaviorService.AdornerWindowGraphics)
								{
									adornerWindowGraphics.SetClip(rectangle);
									using (Region region = new Region(rectangle))
									{
										region.Exclude(Rectangle.Inflate(rectangle, -2, -2));
										this.BehaviorService.Invalidate(region);
									}
									adornerWindowGraphics.ResetClip();
								}
							}
							num++;
						}
						this.BehaviorService.EnableAllAdorners(true);
					}
					this.BehaviorService.PopBehavior(this);
					if (this.lastResizeRegion != null)
					{
						this.BehaviorService.Invalidate(this.lastResizeRegion);
						this.lastResizeRegion.Dispose();
						this.lastResizeRegion = null;
					}
				}
			}
			if (this.resizeTransaction != null)
			{
				DesignerTransaction designerTransaction = this.resizeTransaction;
				this.resizeTransaction = null;
				using (designerTransaction)
				{
					designerTransaction.Cancel();
				}
			}
		}

		// Token: 0x06002516 RID: 9494 RVA: 0x000E7104 File Offset: 0x000E5304
		internal static int AdjustPixelsForIntegralHeight(Control control, int pixelsMoved)
		{
			PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(control)["IntegralHeight"];
			if (propertyDescriptor != null)
			{
				object value = propertyDescriptor.GetValue(control);
				if (value is bool && (bool)value)
				{
					PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(control)["ItemHeight"];
					if (propertyDescriptor2 != null)
					{
						if (pixelsMoved >= 0)
						{
							return pixelsMoved - pixelsMoved % (int)propertyDescriptor2.GetValue(control);
						}
						int num = (int)propertyDescriptor2.GetValue(control);
						return pixelsMoved - (num - Math.Abs(pixelsMoved) % num);
					}
				}
			}
			return pixelsMoved;
		}

		// Token: 0x06002517 RID: 9495 RVA: 0x000E7184 File Offset: 0x000E5384
		public override bool OnMouseMove(Glyph g, MouseButtons button, Point mouseLoc)
		{
			if (!this.pushedBehavior)
			{
				return false;
			}
			bool flag = Control.ModifierKeys == Keys.Alt;
			if (flag && this.dragManager != null)
			{
				this.dragManager.EraseSnapLines();
			}
			if (!flag && mouseLoc.Equals(this.lastMouseLoc))
			{
				return true;
			}
			if (this.lastMouseAbs != null)
			{
				NativeMethods.POINT point = new NativeMethods.POINT(mouseLoc.X, mouseLoc.Y);
				UnsafeNativeMethods.ClientToScreen(new HandleRef(this, this.behaviorService.AdornerWindowControl.Handle), point);
				if (point.x == this.lastMouseAbs.x && point.y == this.lastMouseAbs.y)
				{
					return true;
				}
			}
			if (!this.dragging)
			{
				if (Math.Abs(this.initialPoint.X - mouseLoc.X) <= DesignerUtils.MinDragSize.Width / 2 && Math.Abs(this.initialPoint.Y - mouseLoc.Y) <= DesignerUtils.MinDragSize.Height / 2)
				{
					return false;
				}
				this.InitiateResize();
				this.dragging = true;
			}
			if (this.resizeComponents == null || this.resizeComponents.Length == 0)
			{
				return false;
			}
			PropertyDescriptor propertyDescriptor = null;
			PropertyDescriptor propertyDescriptor2 = null;
			PropertyDescriptor propertyDescriptor3 = null;
			PropertyDescriptor propertyDescriptor4 = null;
			if (this.initialResize)
			{
				propertyDescriptor = TypeDescriptor.GetProperties(this.resizeComponents[0].resizeControl)["Width"];
				propertyDescriptor2 = TypeDescriptor.GetProperties(this.resizeComponents[0].resizeControl)["Height"];
				propertyDescriptor3 = TypeDescriptor.GetProperties(this.resizeComponents[0].resizeControl)["Top"];
				propertyDescriptor4 = TypeDescriptor.GetProperties(this.resizeComponents[0].resizeControl)["Left"];
				if (propertyDescriptor != null && !typeof(int).IsAssignableFrom(propertyDescriptor.PropertyType))
				{
					propertyDescriptor = null;
				}
				if (propertyDescriptor2 != null && !typeof(int).IsAssignableFrom(propertyDescriptor2.PropertyType))
				{
					propertyDescriptor2 = null;
				}
				if (propertyDescriptor3 != null && !typeof(int).IsAssignableFrom(propertyDescriptor3.PropertyType))
				{
					propertyDescriptor3 = null;
				}
				if (propertyDescriptor4 != null && !typeof(int).IsAssignableFrom(propertyDescriptor4.PropertyType))
				{
					propertyDescriptor4 = null;
				}
			}
			Control control = this.resizeComponents[0].resizeControl as Control;
			this.lastMouseLoc = mouseLoc;
			this.lastMouseAbs = new NativeMethods.POINT(mouseLoc.X, mouseLoc.Y);
			UnsafeNativeMethods.ClientToScreen(new HandleRef(this, this.behaviorService.AdornerWindowControl.Handle), this.lastMouseAbs);
			int num = Math.Max(control.MinimumSize.Height, 10);
			int num2 = Math.Max(control.MinimumSize.Width, 10);
			if (this.dragManager != null)
			{
				bool flag2 = true;
				bool shouldSnapHorizontally = true;
				if (((this.targetResizeRules & SelectionRules.BottomSizeable) != SelectionRules.None || (this.targetResizeRules & SelectionRules.TopSizeable) != SelectionRules.None) && control.Height == num)
				{
					flag2 = false;
				}
				else if (((this.targetResizeRules & SelectionRules.RightSizeable) != SelectionRules.None || (this.targetResizeRules & SelectionRules.LeftSizeable) != SelectionRules.None) && control.Width == num2)
				{
					flag2 = false;
				}
				PropertyDescriptor propertyDescriptor5 = TypeDescriptor.GetProperties(control)["IntegralHeight"];
				if (propertyDescriptor5 != null)
				{
					object value = propertyDescriptor5.GetValue(control);
					if (value is bool && (bool)value)
					{
						shouldSnapHorizontally = false;
					}
				}
				if (!flag && flag2)
				{
					this.lastSnapOffset = this.dragManager.OnMouseMove(control, this.GenerateSnapLines(this.targetResizeRules, mouseLoc), ref this.didSnap, shouldSnapHorizontally);
				}
				else
				{
					this.dragManager.OnMouseMove(new Rectangle(-100, -100, 0, 0));
				}
				mouseLoc.X += this.lastSnapOffset.X;
				mouseLoc.Y += this.lastSnapOffset.Y;
			}
			Rectangle rectangle = new Rectangle(this.resizeComponents[0].resizeBounds.X, this.resizeComponents[0].resizeBounds.Y, this.resizeComponents[0].resizeBounds.Width, this.resizeComponents[0].resizeBounds.Height);
			if (this.didSnap && control.Parent != null)
			{
				rectangle.Location = this.behaviorService.MapAdornerWindowPoint(control.Parent.Handle, rectangle.Location);
				if (control.Parent.IsMirrored)
				{
					rectangle.Offset(-rectangle.Width, 0);
				}
			}
			Rectangle rectangle2 = Rectangle.Empty;
			Rectangle dragRect = Rectangle.Empty;
			bool flag3 = true;
			Color backColor = (control.Parent != null) ? control.Parent.BackColor : Color.Empty;
			for (int i = 0; i < this.resizeComponents.Length; i++)
			{
				Control control2 = this.resizeComponents[i].resizeControl as Control;
				Rectangle rectangle3 = control2.Bounds;
				Rectangle rectangle4 = rectangle3;
				Rectangle resizeBounds = this.resizeComponents[i].resizeBounds;
				Rectangle rect = this.BehaviorService.ControlRectInAdornerWindow(control2);
				bool flag4 = true;
				UnsafeNativeMethods.SendMessage(control2.Handle, 11, false, 0);
				try
				{
					bool flag5 = false;
					if (control2.Parent != null && control2.Parent.IsMirrored)
					{
						flag5 = true;
					}
					BoundsSpecified boundsSpecified = BoundsSpecified.None;
					SelectionRules resizeRules = this.resizeComponents[i].resizeRules;
					if ((this.targetResizeRules & SelectionRules.BottomSizeable) != SelectionRules.None && (resizeRules & SelectionRules.BottomSizeable) != SelectionRules.None)
					{
						int num3;
						if (this.didSnap)
						{
							num3 = mouseLoc.Y - rectangle.Bottom;
						}
						else
						{
							num3 = ResizeBehavior.AdjustPixelsForIntegralHeight(control2, mouseLoc.Y - this.initialPoint.Y);
						}
						rectangle3.Height = Math.Max(num, resizeBounds.Height + num3);
						boundsSpecified |= BoundsSpecified.Height;
					}
					if ((this.targetResizeRules & SelectionRules.TopSizeable) != SelectionRules.None && (resizeRules & SelectionRules.TopSizeable) != SelectionRules.None)
					{
						int num4;
						if (this.didSnap)
						{
							num4 = rectangle.Y - mouseLoc.Y;
						}
						else
						{
							num4 = ResizeBehavior.AdjustPixelsForIntegralHeight(control2, this.initialPoint.Y - mouseLoc.Y);
						}
						boundsSpecified |= BoundsSpecified.Height;
						rectangle3.Height = Math.Max(num, resizeBounds.Height + num4);
						if (rectangle3.Height != num || (rectangle3.Height == num && rectangle4.Height != num))
						{
							boundsSpecified |= BoundsSpecified.Y;
							rectangle3.Y = Math.Min(resizeBounds.Bottom - num, resizeBounds.Y - num4);
						}
					}
					if (((this.targetResizeRules & SelectionRules.RightSizeable) != SelectionRules.None && (resizeRules & SelectionRules.RightSizeable) != SelectionRules.None && !flag5) || ((this.targetResizeRules & SelectionRules.LeftSizeable) != SelectionRules.None && (resizeRules & SelectionRules.LeftSizeable) > SelectionRules.None && flag5))
					{
						boundsSpecified |= BoundsSpecified.Width;
						int num5 = this.initialPoint.X;
						if (this.didSnap)
						{
							num5 = ((!flag5) ? rectangle.Right : rectangle.Left);
						}
						rectangle3.Width = Math.Max(num2, resizeBounds.Width + ((!flag5) ? (mouseLoc.X - num5) : (num5 - mouseLoc.X)));
					}
					if (((this.targetResizeRules & SelectionRules.RightSizeable) != SelectionRules.None && (resizeRules & SelectionRules.RightSizeable) > SelectionRules.None && flag5) || ((this.targetResizeRules & SelectionRules.LeftSizeable) != SelectionRules.None && (resizeRules & SelectionRules.LeftSizeable) != SelectionRules.None && !flag5))
					{
						boundsSpecified |= BoundsSpecified.Width;
						int num6 = this.initialPoint.X;
						if (this.didSnap)
						{
							num6 = ((!flag5) ? rectangle.Left : rectangle.Right);
						}
						int num7 = (!flag5) ? (num6 - mouseLoc.X) : (mouseLoc.X - num6);
						rectangle3.Width = Math.Max(num2, resizeBounds.Width + num7);
						if (rectangle3.Width != num2 || (rectangle3.Width == num2 && rectangle4.Width != num2))
						{
							boundsSpecified |= BoundsSpecified.X;
							rectangle3.X = Math.Min(resizeBounds.Right - num2, resizeBounds.X - num7);
						}
					}
					if (!this.parentGridSize.IsEmpty)
					{
						rectangle3 = this.AdjustToGrid(rectangle3, this.targetResizeRules);
					}
					if ((boundsSpecified & BoundsSpecified.Width) == BoundsSpecified.Width && this.dragging && this.initialResize && propertyDescriptor != null)
					{
						propertyDescriptor.SetValue(this.resizeComponents[i].resizeControl, rectangle3.Width);
					}
					if ((boundsSpecified & BoundsSpecified.Height) == BoundsSpecified.Height && this.dragging && this.initialResize && propertyDescriptor2 != null)
					{
						propertyDescriptor2.SetValue(this.resizeComponents[i].resizeControl, rectangle3.Height);
					}
					if ((boundsSpecified & BoundsSpecified.X) == BoundsSpecified.X && this.dragging && this.initialResize && propertyDescriptor4 != null)
					{
						propertyDescriptor4.SetValue(this.resizeComponents[i].resizeControl, rectangle3.X);
					}
					if ((boundsSpecified & BoundsSpecified.Y) == BoundsSpecified.Y && this.dragging && this.initialResize && propertyDescriptor3 != null)
					{
						propertyDescriptor3.SetValue(this.resizeComponents[i].resizeControl, rectangle3.Y);
					}
					if (this.dragging)
					{
						control2.SetBounds(rectangle3.X, rectangle3.Y, rectangle3.Width, rectangle3.Height, boundsSpecified);
						rectangle2 = this.BehaviorService.ControlRectInAdornerWindow(control2);
						if (control2.Equals(control))
						{
							dragRect = rectangle2;
						}
						if (control2.Bounds == rectangle4)
						{
							flag4 = false;
						}
						if (control2.Bounds != rectangle3)
						{
							flag3 = false;
						}
					}
					if (control2 == this.primaryControl && this.statusCommandUI != null)
					{
						this.statusCommandUI.SetStatusInformation(control2);
					}
				}
				finally
				{
					UnsafeNativeMethods.SendMessage(control2.Handle, 11, true, 0);
					if (flag4)
					{
						Control parent = control2.Parent;
						if (parent != null)
						{
							control2.Invalidate(true);
							parent.Invalidate(rectangle4, true);
							parent.Update();
						}
						else
						{
							control2.Refresh();
						}
					}
					if (!rectangle2.IsEmpty)
					{
						using (Region region = new Region(rectangle2))
						{
							region.Exclude(Rectangle.Inflate(rectangle2, -2, -2));
							if (flag4)
							{
								using (Region region2 = new Region(rect))
								{
									region2.Exclude(Rectangle.Inflate(rect, -2, -2));
									this.BehaviorService.Invalidate(region2);
								}
							}
							if (!this.captureLost)
							{
								using (Graphics adornerWindowGraphics = this.BehaviorService.AdornerWindowGraphics)
								{
									if (this.lastResizeRegion != null && !this.lastResizeRegion.Equals(region, adornerWindowGraphics))
									{
										this.lastResizeRegion.Exclude(region);
										this.BehaviorService.Invalidate(this.lastResizeRegion);
										this.lastResizeRegion.Dispose();
										this.lastResizeRegion = null;
									}
									DesignerUtils.DrawResizeBorder(adornerWindowGraphics, region, backColor);
								}
								if (this.lastResizeRegion == null)
								{
									this.lastResizeRegion = region.Clone();
								}
							}
						}
					}
				}
			}
			if (flag3 && !flag && this.dragManager != null)
			{
				this.dragManager.RenderSnapLinesInternal(dragRect);
			}
			this.initialResize = false;
			return true;
		}

		// Token: 0x06002518 RID: 9496 RVA: 0x000E7CF4 File Offset: 0x000E5EF4
		public override bool OnMouseUp(Glyph g, MouseButtons button)
		{
			try
			{
				if (this.dragging)
				{
					if (this.dragManager != null)
					{
						this.dragManager.OnMouseUp();
						this.dragManager = null;
						this.lastSnapOffset = Point.Empty;
						this.didSnap = false;
					}
					if (this.resizeComponents != null && this.resizeComponents.Length != 0)
					{
						PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(this.resizeComponents[0].resizeControl)["Width"];
						PropertyDescriptor propertyDescriptor2 = TypeDescriptor.GetProperties(this.resizeComponents[0].resizeControl)["Height"];
						PropertyDescriptor propertyDescriptor3 = TypeDescriptor.GetProperties(this.resizeComponents[0].resizeControl)["Top"];
						PropertyDescriptor propertyDescriptor4 = TypeDescriptor.GetProperties(this.resizeComponents[0].resizeControl)["Left"];
						for (int i = 0; i < this.resizeComponents.Length; i++)
						{
							if (propertyDescriptor != null && ((Control)this.resizeComponents[i].resizeControl).Width != this.resizeComponents[i].resizeBounds.Width)
							{
								propertyDescriptor.SetValue(this.resizeComponents[i].resizeControl, ((Control)this.resizeComponents[i].resizeControl).Width);
							}
							if (propertyDescriptor2 != null && ((Control)this.resizeComponents[i].resizeControl).Height != this.resizeComponents[i].resizeBounds.Height)
							{
								propertyDescriptor2.SetValue(this.resizeComponents[i].resizeControl, ((Control)this.resizeComponents[i].resizeControl).Height);
							}
							if (propertyDescriptor3 != null && ((Control)this.resizeComponents[i].resizeControl).Top != this.resizeComponents[i].resizeBounds.Y)
							{
								propertyDescriptor3.SetValue(this.resizeComponents[i].resizeControl, ((Control)this.resizeComponents[i].resizeControl).Top);
							}
							if (propertyDescriptor4 != null && ((Control)this.resizeComponents[i].resizeControl).Left != this.resizeComponents[i].resizeBounds.X)
							{
								propertyDescriptor4.SetValue(this.resizeComponents[i].resizeControl, ((Control)this.resizeComponents[i].resizeControl).Left);
							}
							if (this.resizeComponents[i].resizeControl == this.primaryControl && this.statusCommandUI != null)
							{
								this.statusCommandUI.SetStatusInformation(this.primaryControl);
							}
						}
					}
				}
				if (this.resizeTransaction != null)
				{
					DesignerTransaction designerTransaction = this.resizeTransaction;
					this.resizeTransaction = null;
					using (designerTransaction)
					{
						designerTransaction.Commit();
					}
				}
			}
			finally
			{
				this.OnLoseCapture(g, EventArgs.Empty);
			}
			return false;
		}

		// Token: 0x04001ADB RID: 6875
		private ResizeBehavior.ResizeComponent[] resizeComponents;

		// Token: 0x04001ADC RID: 6876
		private IServiceProvider serviceProvider;

		// Token: 0x04001ADD RID: 6877
		private BehaviorService behaviorService;

		// Token: 0x04001ADE RID: 6878
		private SelectionRules targetResizeRules;

		// Token: 0x04001ADF RID: 6879
		private Point initialPoint;

		// Token: 0x04001AE0 RID: 6880
		private bool dragging;

		// Token: 0x04001AE1 RID: 6881
		private bool pushedBehavior;

		// Token: 0x04001AE2 RID: 6882
		private bool initialResize;

		// Token: 0x04001AE3 RID: 6883
		private DesignerTransaction resizeTransaction;

		// Token: 0x04001AE4 RID: 6884
		private const int MINSIZE = 10;

		// Token: 0x04001AE5 RID: 6885
		private const int borderSize = 2;

		// Token: 0x04001AE6 RID: 6886
		private DragAssistanceManager dragManager;

		// Token: 0x04001AE7 RID: 6887
		private Point lastMouseLoc;

		// Token: 0x04001AE8 RID: 6888
		private Point parentLocation;

		// Token: 0x04001AE9 RID: 6889
		private Size parentGridSize;

		// Token: 0x04001AEA RID: 6890
		private NativeMethods.POINT lastMouseAbs;

		// Token: 0x04001AEB RID: 6891
		private Point lastSnapOffset;

		// Token: 0x04001AEC RID: 6892
		private bool didSnap;

		// Token: 0x04001AED RID: 6893
		private Control primaryControl;

		// Token: 0x04001AEE RID: 6894
		private Cursor cursor = Cursors.Default;

		// Token: 0x04001AEF RID: 6895
		private StatusCommandUI statusCommandUI;

		// Token: 0x04001AF0 RID: 6896
		private Region lastResizeRegion;

		// Token: 0x04001AF1 RID: 6897
		private bool captureLost;

		// Token: 0x020005A9 RID: 1449
		private struct ResizeComponent
		{
			// Token: 0x040022A7 RID: 8871
			public object resizeControl;

			// Token: 0x040022A8 RID: 8872
			public Rectangle resizeBounds;

			// Token: 0x040022A9 RID: 8873
			public SelectionRules resizeRules;
		}
	}
}

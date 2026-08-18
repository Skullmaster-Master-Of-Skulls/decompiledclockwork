using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;

namespace System.Windows.Forms.Design.Behavior
{
	// Token: 0x0200037D RID: 893
	internal sealed class DragAssistanceManager
	{
		// Token: 0x060024B6 RID: 9398 RVA: 0x000E2AF8 File Offset: 0x000E0CF8
		internal DragAssistanceManager(IServiceProvider serviceProvider) : this(serviceProvider, null, null, null, false, false)
		{
		}

		// Token: 0x060024B7 RID: 9399 RVA: 0x000E2B06 File Offset: 0x000E0D06
		internal DragAssistanceManager(IServiceProvider serviceProvider, ArrayList dragComponents) : this(serviceProvider, null, dragComponents, null, false, false)
		{
		}

		// Token: 0x060024B8 RID: 9400 RVA: 0x000E2B14 File Offset: 0x000E0D14
		internal DragAssistanceManager(IServiceProvider serviceProvider, ArrayList dragComponents, bool resizing) : this(serviceProvider, null, dragComponents, null, resizing, false)
		{
		}

		// Token: 0x060024B9 RID: 9401 RVA: 0x000E2B22 File Offset: 0x000E0D22
		internal DragAssistanceManager(IServiceProvider serviceProvider, Graphics graphics, ArrayList dragComponents, Image backgroundImage, bool ctrlDrag) : this(serviceProvider, graphics, dragComponents, backgroundImage, false, ctrlDrag)
		{
		}

		// Token: 0x060024BA RID: 9402 RVA: 0x000E2B34 File Offset: 0x000E0D34
		internal DragAssistanceManager(IServiceProvider serviceProvider, Graphics graphics, ArrayList dragComponents, Image backgroundImage, bool resizing, bool ctrlDrag)
		{
			this.serviceProvider = serviceProvider;
			this.behaviorService = (serviceProvider.GetService(typeof(BehaviorService)) as BehaviorService);
			IDesignerHost designerHost = serviceProvider.GetService(typeof(IDesignerHost)) as IDesignerHost;
			IUIService iuiservice = serviceProvider.GetService(typeof(IUIService)) as IUIService;
			if (designerHost == null || this.behaviorService == null)
			{
				return;
			}
			if (graphics == null)
			{
				this.graphics = this.behaviorService.AdornerWindowGraphics;
			}
			else
			{
				this.graphics = graphics;
			}
			if (iuiservice != null)
			{
				if (iuiservice.Styles["VsColorSnaplines"] is Color)
				{
					this.edgePen = new Pen((Color)iuiservice.Styles["VsColorSnaplines"]);
					this.disposeEdgePen = true;
				}
				if (iuiservice.Styles["VsColorSnaplinesTextBaseline"] is Color)
				{
					this.baselinePen.Dispose();
					this.baselinePen = new Pen((Color)iuiservice.Styles["VsColorSnaplinesTextBaseline"]);
				}
			}
			this.backgroundImage = backgroundImage;
			this.rootComponentHandle = ((designerHost.RootComponent is Control) ? ((Control)designerHost.RootComponent).Handle : IntPtr.Zero);
			this.resizing = resizing;
			this.ctrlDrag = ctrlDrag;
			this.Initialize(dragComponents, designerHost);
		}

		// Token: 0x060024BB RID: 9403 RVA: 0x000E2D1C File Offset: 0x000E0F1C
		private void AddSnapLines(ControlDesigner controlDesigner, ArrayList horizontalList, ArrayList verticalList, bool isTarget, bool validTarget)
		{
			IList snapLines = controlDesigner.SnapLines;
			Rectangle clientRectangle = controlDesigner.Control.ClientRectangle;
			Rectangle bounds = controlDesigner.Control.Bounds;
			bounds.Location = (clientRectangle.Location = this.behaviorService.ControlToAdornerWindow(controlDesigner.Control));
			int left = bounds.Left;
			int top = bounds.Top;
			Point offsetToClientArea = controlDesigner.GetOffsetToClientArea();
			clientRectangle.X += offsetToClientArea.X;
			clientRectangle.Y += offsetToClientArea.Y;
			foreach (object obj in snapLines)
			{
				SnapLine snapLine = (SnapLine)obj;
				if (isTarget)
				{
					if (snapLine.Filter != null && snapLine.Filter.StartsWith("Padding"))
					{
						continue;
					}
					if (validTarget && !this.targetSnapLineTypes.Contains(snapLine.SnapLineType))
					{
						this.targetSnapLineTypes.Add(snapLine.SnapLineType);
					}
				}
				else
				{
					if (validTarget && !this.targetSnapLineTypes.Contains(snapLine.SnapLineType))
					{
						continue;
					}
					if (snapLine.Filter != null && snapLine.Filter.StartsWith("Padding"))
					{
						this.snapLineToBounds.Add(snapLine, clientRectangle);
					}
					else
					{
						this.snapLineToBounds.Add(snapLine, bounds);
					}
				}
				if (snapLine.IsHorizontal)
				{
					snapLine.AdjustOffset(top);
					horizontalList.Add(snapLine);
				}
				else
				{
					snapLine.AdjustOffset(left);
					verticalList.Add(snapLine);
				}
			}
		}

		// Token: 0x060024BC RID: 9404 RVA: 0x000E2EF8 File Offset: 0x000E10F8
		private int BuildDistanceArray(ArrayList snapLines, ArrayList targetSnapLines, int[] distances, Rectangle dragBounds)
		{
			int num = 4369;
			int num2 = 0;
			for (int i = 0; i < snapLines.Count; i++)
			{
				SnapLine snapLine = (SnapLine)snapLines[i];
				if (DragAssistanceManager.IsMarginOrPaddingSnapLine(snapLine) && !this.ValidateMarginOrPaddingLine(snapLine, dragBounds))
				{
					distances[i] = 4369;
				}
				else
				{
					int num3 = 4369;
					for (int j = 0; j < targetSnapLines.Count; j++)
					{
						SnapLine snapLine2 = (SnapLine)targetSnapLines[j];
						if (SnapLine.ShouldSnap(snapLine, snapLine2))
						{
							int num4 = snapLine2.Offset - snapLine.Offset;
							if (Math.Abs(num4) < Math.Abs(num3))
							{
								num3 = num4;
							}
						}
					}
					distances[i] = num3;
					int priority = (int)((SnapLine)snapLines[i]).Priority;
					if (Math.Abs(num3) < Math.Abs(num) || (Math.Abs(num3) == Math.Abs(num) && priority > num2))
					{
						num = num3;
						if (priority != 4)
						{
							num2 = priority;
						}
					}
				}
			}
			return num;
		}

		// Token: 0x060024BD RID: 9405 RVA: 0x000E2FF4 File Offset: 0x000E11F4
		private DragAssistanceManager.Line[] EraseOldSnapLines(DragAssistanceManager.Line[] lines, ArrayList tempLines)
		{
			Rectangle empty = Rectangle.Empty;
			if (lines != null)
			{
				foreach (DragAssistanceManager.Line line in lines)
				{
					bool flag = false;
					if (tempLines != null)
					{
						for (int j = 0; j < tempLines.Count; j++)
						{
							if (line.LineType == ((DragAssistanceManager.Line)tempLines[j]).LineType)
							{
								DragAssistanceManager.Line[] diffs = DragAssistanceManager.Line.GetDiffs(line, (DragAssistanceManager.Line)tempLines[j]);
								if (diffs != null)
								{
									for (int k = 0; k < diffs.Length; k++)
									{
										empty = new Rectangle(diffs[k].x1, diffs[k].y1, diffs[k].x2 - diffs[k].x1, diffs[k].y2 - diffs[k].y1);
										empty.Inflate(1, 1);
										if (this.backgroundImage != null)
										{
											this.graphics.DrawImage(this.backgroundImage, empty, empty, GraphicsUnit.Pixel);
										}
										else
										{
											this.behaviorService.Invalidate(empty);
										}
									}
									flag = true;
									break;
								}
							}
						}
					}
					if (!flag)
					{
						empty = new Rectangle(line.x1, line.y1, line.x2 - line.x1, line.y2 - line.y1);
						empty.Inflate(1, 1);
						if (this.backgroundImage != null)
						{
							this.graphics.DrawImage(this.backgroundImage, empty, empty, GraphicsUnit.Pixel);
						}
						else
						{
							this.behaviorService.Invalidate(empty);
						}
					}
				}
			}
			if (tempLines != null)
			{
				lines = new DragAssistanceManager.Line[tempLines.Count];
				tempLines.CopyTo(lines);
			}
			else
			{
				lines = new DragAssistanceManager.Line[0];
			}
			return lines;
		}

		// Token: 0x060024BE RID: 9406 RVA: 0x000E319A File Offset: 0x000E139A
		internal void EraseSnapLines()
		{
			this.EraseOldSnapLines(this.vertLines, null);
			this.EraseOldSnapLines(this.horzLines, null);
		}

		// Token: 0x060024BF RID: 9407 RVA: 0x000E31B8 File Offset: 0x000E13B8
		internal DragAssistanceManager.Line[] GetRecentLines()
		{
			if (this.recentLines != null)
			{
				return this.recentLines;
			}
			return new DragAssistanceManager.Line[0];
		}

		// Token: 0x060024C0 RID: 9408 RVA: 0x000E31D0 File Offset: 0x000E13D0
		private void IdentifyAndStoreValidLines(ArrayList snapLines, int[] distances, Rectangle dragBounds, int smallestDistance)
		{
			int num = 1;
			for (int i = 0; i < distances.Length; i++)
			{
				if (distances[i] == smallestDistance)
				{
					int priority = (int)((SnapLine)snapLines[i]).Priority;
					if (priority > num && priority != 4)
					{
						num = priority;
					}
				}
			}
			for (int j = 0; j < distances.Length; j++)
			{
				if (distances[j] == smallestDistance && (((SnapLine)snapLines[j]).Priority == (SnapLinePriority)num || ((SnapLine)snapLines[j]).Priority == SnapLinePriority.Always))
				{
					this.StoreSnapLine((SnapLine)snapLines[j], dragBounds);
				}
			}
		}

		// Token: 0x060024C1 RID: 9409 RVA: 0x000E3260 File Offset: 0x000E1460
		private bool AddChildCompSnaplines(IComponent comp, ArrayList dragComponents, Rectangle clipBounds, Control targetControl)
		{
			Control control = comp as Control;
			if (control == null || (dragComponents != null && dragComponents.Contains(comp) && !this.ctrlDrag) || DragAssistanceManager.IsChildOfParent(control, targetControl) || !clipBounds.IntersectsWith(control.Bounds) || control.Parent == null || !control.Visible)
			{
				return false;
			}
			Control control2 = control;
			if (!control2.Equals(targetControl))
			{
				IDesignerHost designerHost = this.serviceProvider.GetService(typeof(IDesignerHost)) as IDesignerHost;
				if (designerHost != null)
				{
					ControlDesigner controlDesigner = designerHost.GetDesigner(control2) as ControlDesigner;
					if (controlDesigner != null)
					{
						return controlDesigner.ControlSupportsSnaplines;
					}
				}
			}
			return true;
		}

		// Token: 0x060024C2 RID: 9410 RVA: 0x000E32F8 File Offset: 0x000E14F8
		private bool AddControlSnaplinesWhenResizing(ControlDesigner designer, Control control, Control targetControl)
		{
			return !this.resizing || !(designer is ParentControlDesigner) || !control.AutoSize || targetControl == null || targetControl.Parent == null || !targetControl.Parent.Equals(control);
		}

		// Token: 0x060024C3 RID: 9411 RVA: 0x000E3330 File Offset: 0x000E1530
		private void Initialize(ArrayList dragComponents, IDesignerHost host)
		{
			Control control = null;
			if (dragComponents != null && dragComponents.Count > 0)
			{
				control = (dragComponents[0] as Control);
			}
			Control control2 = host.RootComponent as Control;
			Rectangle clipBounds = new Rectangle(0, 0, control2.ClientRectangle.Width, control2.ClientRectangle.Height);
			clipBounds.Inflate(-1, -1);
			if (control != null)
			{
				this.dragOffset = this.behaviorService.ControlToAdornerWindow(control);
			}
			else
			{
				this.dragOffset = this.behaviorService.MapAdornerWindowPoint(control2.Handle, Point.Empty);
				if (control2.Parent != null && control2.Parent.IsMirrored)
				{
					this.dragOffset.Offset(-control2.Width, 0);
				}
			}
			if (control != null)
			{
				ControlDesigner controlDesigner = host.GetDesigner(control) as ControlDesigner;
				bool flag = false;
				if (controlDesigner == null)
				{
					controlDesigner = (TypeDescriptor.CreateDesigner(control, typeof(IDesigner)) as ControlDesigner);
					if (controlDesigner != null)
					{
						controlDesigner.ForceVisible = false;
						controlDesigner.Initialize(control);
						flag = true;
					}
				}
				this.AddSnapLines(controlDesigner, this.targetHorizontalSnapLines, this.targetVerticalSnapLines, true, control != null);
				if (flag)
				{
					controlDesigner.Dispose();
				}
			}
			foreach (object obj in host.Container.Components)
			{
				IComponent component = (IComponent)obj;
				if (this.AddChildCompSnaplines(component, dragComponents, clipBounds, control))
				{
					ControlDesigner controlDesigner2 = host.GetDesigner(component) as ControlDesigner;
					if (controlDesigner2 != null)
					{
						if (this.AddControlSnaplinesWhenResizing(controlDesigner2, component as Control, control))
						{
							this.AddSnapLines(controlDesigner2, this.horizontalSnapLines, this.verticalSnapLines, false, control != null);
						}
						int num = controlDesigner2.NumberOfInternalControlDesigners();
						for (int i = 0; i < num; i++)
						{
							ControlDesigner controlDesigner3 = controlDesigner2.InternalControlDesigner(i);
							if (controlDesigner3 != null && this.AddChildCompSnaplines(controlDesigner3.Component, dragComponents, clipBounds, control) && this.AddControlSnaplinesWhenResizing(controlDesigner3, controlDesigner3.Component as Control, control))
							{
								this.AddSnapLines(controlDesigner3, this.horizontalSnapLines, this.verticalSnapLines, false, control != null);
							}
						}
					}
				}
			}
			this.verticalDistances = new int[this.verticalSnapLines.Count];
			this.horizontalDistances = new int[this.horizontalSnapLines.Count];
		}

		// Token: 0x060024C4 RID: 9412 RVA: 0x000E3598 File Offset: 0x000E1798
		private static bool IsChildOfParent(Control child, Control parent)
		{
			if (child == null || parent == null)
			{
				return false;
			}
			for (Control parent2 = child.Parent; parent2 != null; parent2 = parent2.Parent)
			{
				if (parent2.Equals(parent))
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060024C5 RID: 9413 RVA: 0x000E35CC File Offset: 0x000E17CC
		private static bool IsMarginOrPaddingSnapLine(SnapLine snapLine)
		{
			return snapLine.Filter != null && (snapLine.Filter.StartsWith("Margin") || snapLine.Filter.StartsWith("Padding"));
		}

		// Token: 0x060024C6 RID: 9414 RVA: 0x000E35FC File Offset: 0x000E17FC
		internal Point OffsetToNearestSnapLocation(Control targetControl, IList targetSnaplines, Point directionOffset)
		{
			this.targetHorizontalSnapLines.Clear();
			this.targetVerticalSnapLines.Clear();
			foreach (object obj in targetSnaplines)
			{
				SnapLine snapLine = (SnapLine)obj;
				if (snapLine.IsHorizontal)
				{
					this.targetHorizontalSnapLines.Add(snapLine);
				}
				else
				{
					this.targetVerticalSnapLines.Add(snapLine);
				}
			}
			return this.OffsetToNearestSnapLocation(targetControl, directionOffset);
		}

		// Token: 0x060024C7 RID: 9415 RVA: 0x000E368C File Offset: 0x000E188C
		internal Point OffsetToNearestSnapLocation(Control targetControl, Point directionOffset)
		{
			Point empty = Point.Empty;
			Rectangle dragBounds = new Rectangle(this.behaviorService.ControlToAdornerWindow(targetControl), targetControl.Size);
			if (directionOffset.X != 0)
			{
				this.BuildDistanceArray(this.verticalSnapLines, this.targetVerticalSnapLines, this.verticalDistances, dragBounds);
				int min = (directionOffset.X < 0) ? 0 : dragBounds.X;
				int max = (directionOffset.X < 0) ? dragBounds.Right : int.MaxValue;
				empty.X = DragAssistanceManager.FindSmallestValidDistance(this.verticalSnapLines, this.verticalDistances, min, max, directionOffset.X);
				if (empty.X != 0)
				{
					this.IdentifyAndStoreValidLines(this.verticalSnapLines, this.verticalDistances, dragBounds, empty.X);
					if (directionOffset.X < 0)
					{
						empty.X *= -1;
					}
				}
			}
			if (directionOffset.Y != 0)
			{
				this.BuildDistanceArray(this.horizontalSnapLines, this.targetHorizontalSnapLines, this.horizontalDistances, dragBounds);
				int min2 = (directionOffset.Y < 0) ? 0 : dragBounds.Y;
				int max2 = (directionOffset.Y < 0) ? dragBounds.Bottom : int.MaxValue;
				empty.Y = DragAssistanceManager.FindSmallestValidDistance(this.horizontalSnapLines, this.horizontalDistances, min2, max2, directionOffset.Y);
				if (empty.Y != 0)
				{
					this.IdentifyAndStoreValidLines(this.horizontalSnapLines, this.horizontalDistances, dragBounds, empty.Y);
					if (directionOffset.Y < 0)
					{
						empty.Y *= -1;
					}
				}
			}
			if (!empty.IsEmpty)
			{
				this.cachedDragRect = dragBounds;
				this.cachedDragRect.Offset(empty.X, empty.Y);
				if (empty.X != 0)
				{
					this.vertLines = new DragAssistanceManager.Line[this.tempVertLines.Count];
					this.tempVertLines.CopyTo(this.vertLines);
				}
				if (empty.Y != 0)
				{
					this.horzLines = new DragAssistanceManager.Line[this.tempHorzLines.Count];
					this.tempHorzLines.CopyTo(this.horzLines);
				}
			}
			return empty;
		}

		// Token: 0x060024C8 RID: 9416 RVA: 0x000E38AC File Offset: 0x000E1AAC
		private static int FindSmallestValidDistance(ArrayList snapLines, int[] distances, int min, int max, int direction)
		{
			int num = 0;
			int num2;
			do
			{
				num2 = DragAssistanceManager.SmallestDistanceIndex(distances, direction, out num);
				if (num2 == 4369)
				{
					return 0;
				}
			}
			while (!DragAssistanceManager.IsWithinValidRange(((SnapLine)snapLines[num2]).Offset, min, max));
			distances[num2] = num;
			return num;
		}

		// Token: 0x060024C9 RID: 9417 RVA: 0x000E38F1 File Offset: 0x000E1AF1
		private static bool IsWithinValidRange(int offset, int min, int max)
		{
			return offset > min && offset < max;
		}

		// Token: 0x060024CA RID: 9418 RVA: 0x000E3900 File Offset: 0x000E1B00
		private static int SmallestDistanceIndex(int[] distances, int direction, out int distanceValue)
		{
			distanceValue = 4369;
			int num = 4369;
			if (distances.Length == 0)
			{
				return num;
			}
			for (int i = 0; i < distances.Length; i++)
			{
				if (distances[i] == 0 || (distances[i] > 0 && direction > 0) || (distances[i] < 0 && direction < 0))
				{
					distances[i] = 4369;
				}
				if (Math.Abs(distances[i]) < distanceValue)
				{
					distanceValue = Math.Abs(distances[i]);
					num = i;
				}
			}
			if (num < distances.Length)
			{
				distances[num] = 4369;
			}
			return num;
		}

		// Token: 0x060024CB RID: 9419 RVA: 0x000E3978 File Offset: 0x000E1B78
		private void RenderSnapLines(DragAssistanceManager.Line[] lines, Rectangle dragRect)
		{
			for (int i = 0; i < lines.Length; i++)
			{
				Pen pen;
				if (lines[i].LineType == DragAssistanceManager.LineType.Margin || lines[i].LineType == DragAssistanceManager.LineType.Padding)
				{
					pen = this.edgePen;
					if (lines[i].x1 == lines[i].x2)
					{
						int num = Math.Max(dragRect.Top, lines[i].OriginalBounds.Top);
						num += (Math.Min(dragRect.Bottom, lines[i].OriginalBounds.Bottom) - num) / 2;
						lines[i].y1 = (lines[i].y2 = num);
						if (lines[i].LineType == DragAssistanceManager.LineType.Margin)
						{
							lines[i].x1 = Math.Min(dragRect.Right, lines[i].OriginalBounds.Right);
							lines[i].x2 = Math.Max(dragRect.Left, lines[i].OriginalBounds.Left);
						}
						else if (lines[i].PaddingLineType == DragAssistanceManager.PaddingLineType.PaddingLeft)
						{
							lines[i].x1 = lines[i].OriginalBounds.Left;
							lines[i].x2 = dragRect.Left;
						}
						else
						{
							lines[i].x1 = dragRect.Right;
							lines[i].x2 = lines[i].OriginalBounds.Right;
						}
						lines[i].x2--;
					}
					else
					{
						int num2 = Math.Max(dragRect.Left, lines[i].OriginalBounds.Left);
						num2 += (Math.Min(dragRect.Right, lines[i].OriginalBounds.Right) - num2) / 2;
						lines[i].x1 = (lines[i].x2 = num2);
						if (lines[i].LineType == DragAssistanceManager.LineType.Margin)
						{
							lines[i].y1 = Math.Min(dragRect.Bottom, lines[i].OriginalBounds.Bottom);
							lines[i].y2 = Math.Max(dragRect.Top, lines[i].OriginalBounds.Top);
						}
						else if (lines[i].PaddingLineType == DragAssistanceManager.PaddingLineType.PaddingTop)
						{
							lines[i].y1 = lines[i].OriginalBounds.Top;
							lines[i].y2 = dragRect.Top;
						}
						else
						{
							lines[i].y1 = dragRect.Bottom;
							lines[i].y2 = lines[i].OriginalBounds.Bottom;
						}
						lines[i].y2--;
					}
				}
				else if (lines[i].LineType == DragAssistanceManager.LineType.Baseline)
				{
					pen = this.baselinePen;
					lines[i].x2--;
				}
				else
				{
					pen = this.edgePen;
					if (lines[i].x1 == lines[i].x2)
					{
						lines[i].y2--;
					}
					else
					{
						lines[i].x2--;
					}
				}
				this.graphics.DrawLine(pen, lines[i].x1, lines[i].y1, lines[i].x2, lines[i].y2);
			}
		}

		// Token: 0x060024CC RID: 9420 RVA: 0x000E3C94 File Offset: 0x000E1E94
		private static void CombineSnaplines(DragAssistanceManager.Line snapLine, ArrayList currentLines)
		{
			bool flag = false;
			for (int i = 0; i < currentLines.Count; i++)
			{
				DragAssistanceManager.Line l = (DragAssistanceManager.Line)currentLines[i];
				DragAssistanceManager.Line line = DragAssistanceManager.Line.Overlap(snapLine, l);
				if (line != null)
				{
					currentLines[i] = line;
					flag = true;
				}
			}
			if (!flag)
			{
				currentLines.Add(snapLine);
			}
		}

		// Token: 0x060024CD RID: 9421 RVA: 0x000E3CE4 File Offset: 0x000E1EE4
		private void StoreSnapLine(SnapLine snapLine, Rectangle dragBounds)
		{
			Rectangle originalBounds = (Rectangle)this.snapLineToBounds[snapLine];
			DragAssistanceManager.LineType lineType = DragAssistanceManager.LineType.Standard;
			if (DragAssistanceManager.IsMarginOrPaddingSnapLine(snapLine))
			{
				lineType = (snapLine.Filter.StartsWith("Margin") ? DragAssistanceManager.LineType.Margin : DragAssistanceManager.LineType.Padding);
			}
			else if (snapLine.SnapLineType == SnapLineType.Baseline)
			{
				lineType = DragAssistanceManager.LineType.Baseline;
			}
			DragAssistanceManager.Line line;
			if (snapLine.IsVertical)
			{
				line = new DragAssistanceManager.Line(snapLine.Offset, Math.Min(dragBounds.Top + ((this.snapPointY != 4369) ? this.snapPointY : 0), originalBounds.Top), snapLine.Offset, Math.Max(dragBounds.Bottom + ((this.snapPointY != 4369) ? this.snapPointY : 0), originalBounds.Bottom));
				line.LineType = lineType;
				DragAssistanceManager.CombineSnaplines(line, this.tempVertLines);
			}
			else
			{
				line = new DragAssistanceManager.Line(Math.Min(dragBounds.Left + ((this.snapPointX != 4369) ? this.snapPointX : 0), originalBounds.Left), snapLine.Offset, Math.Max(dragBounds.Right + ((this.snapPointX != 4369) ? this.snapPointX : 0), originalBounds.Right), snapLine.Offset);
				line.LineType = lineType;
				DragAssistanceManager.CombineSnaplines(line, this.tempHorzLines);
			}
			if (DragAssistanceManager.IsMarginOrPaddingSnapLine(snapLine))
			{
				line.OriginalBounds = originalBounds;
				if (line.LineType == DragAssistanceManager.LineType.Padding)
				{
					string filter = snapLine.Filter;
					if (filter == "Padding.Right")
					{
						line.PaddingLineType = DragAssistanceManager.PaddingLineType.PaddingRight;
						return;
					}
					if (filter == "Padding.Left")
					{
						line.PaddingLineType = DragAssistanceManager.PaddingLineType.PaddingLeft;
						return;
					}
					if (filter == "Padding.Top")
					{
						line.PaddingLineType = DragAssistanceManager.PaddingLineType.PaddingTop;
						return;
					}
					if (!(filter == "Padding.Bottom"))
					{
						return;
					}
					line.PaddingLineType = DragAssistanceManager.PaddingLineType.PaddingBottom;
				}
			}
		}

		// Token: 0x060024CE RID: 9422 RVA: 0x000E3EA4 File Offset: 0x000E20A4
		private bool ValidateMarginOrPaddingLine(SnapLine snapLine, Rectangle dragBounds)
		{
			Rectangle rectangle = (Rectangle)this.snapLineToBounds[snapLine];
			if (snapLine.IsVertical)
			{
				if (rectangle.Top < dragBounds.Top)
				{
					if (rectangle.Top + rectangle.Height < dragBounds.Top)
					{
						return false;
					}
				}
				else if (dragBounds.Top + dragBounds.Height < rectangle.Top)
				{
					return false;
				}
			}
			else if (rectangle.Left < dragBounds.Left)
			{
				if (rectangle.Left + rectangle.Width < dragBounds.Left)
				{
					return false;
				}
			}
			else if (dragBounds.Left + dragBounds.Width < rectangle.Left)
			{
				return false;
			}
			return true;
		}

		// Token: 0x060024CF RID: 9423 RVA: 0x000E3F54 File Offset: 0x000E2154
		internal Point OnMouseMove(Rectangle dragBounds, SnapLine[] snapLines)
		{
			bool flag = false;
			return this.OnMouseMove(dragBounds, snapLines, ref flag, true);
		}

		// Token: 0x060024D0 RID: 9424 RVA: 0x000E3F70 File Offset: 0x000E2170
		internal Point OnMouseMove(Rectangle dragBounds, SnapLine[] snapLines, ref bool didSnap, bool shouldSnapHorizontally)
		{
			if (snapLines == null || snapLines.Length == 0)
			{
				return Point.Empty;
			}
			this.targetHorizontalSnapLines.Clear();
			this.targetVerticalSnapLines.Clear();
			foreach (SnapLine snapLine in snapLines)
			{
				if (snapLine.IsHorizontal)
				{
					this.targetHorizontalSnapLines.Add(snapLine);
				}
				else
				{
					this.targetVerticalSnapLines.Add(snapLine);
				}
			}
			return this.OnMouseMove(dragBounds, false, ref didSnap, shouldSnapHorizontally);
		}

		// Token: 0x060024D1 RID: 9425 RVA: 0x000E3FE4 File Offset: 0x000E21E4
		internal Point OnMouseMove(Rectangle dragBounds)
		{
			bool flag = false;
			return this.OnMouseMove(dragBounds, true, ref flag, true);
		}

		// Token: 0x060024D2 RID: 9426 RVA: 0x000E4000 File Offset: 0x000E2200
		internal Point OnMouseMove(Control targetControl, SnapLine[] snapLines, ref bool didSnap, bool shouldSnapHorizontally)
		{
			Rectangle dragBounds = new Rectangle(this.behaviorService.ControlToAdornerWindow(targetControl), targetControl.Size);
			didSnap = false;
			return this.OnMouseMove(dragBounds, snapLines, ref didSnap, shouldSnapHorizontally);
		}

		// Token: 0x060024D3 RID: 9427 RVA: 0x000E4034 File Offset: 0x000E2234
		private Point OnMouseMove(Rectangle dragBounds, bool offsetSnapLines, ref bool didSnap, bool shouldSnapHorizontally)
		{
			this.tempVertLines.Clear();
			this.tempHorzLines.Clear();
			this.dragOffset = new Point(dragBounds.X - this.dragOffset.X, dragBounds.Y - this.dragOffset.Y);
			if (offsetSnapLines)
			{
				for (int i = 0; i < this.targetHorizontalSnapLines.Count; i++)
				{
					((SnapLine)this.targetHorizontalSnapLines[i]).AdjustOffset(this.dragOffset.Y);
				}
				for (int j = 0; j < this.targetVerticalSnapLines.Count; j++)
				{
					((SnapLine)this.targetVerticalSnapLines[j]).AdjustOffset(this.dragOffset.X);
				}
			}
			int num = this.BuildDistanceArray(this.verticalSnapLines, this.targetVerticalSnapLines, this.verticalDistances, dragBounds);
			int num2 = 4369;
			if (shouldSnapHorizontally)
			{
				num2 = this.BuildDistanceArray(this.horizontalSnapLines, this.targetHorizontalSnapLines, this.horizontalDistances, dragBounds);
			}
			this.snapPointX = ((Math.Abs(num) <= 8) ? (-num) : 4369);
			this.snapPointY = ((Math.Abs(num2) <= 8) ? (-num2) : 4369);
			didSnap = false;
			if (this.snapPointX != 4369)
			{
				this.IdentifyAndStoreValidLines(this.verticalSnapLines, this.verticalDistances, dragBounds, num);
				didSnap = true;
			}
			if (this.snapPointY != 4369)
			{
				this.IdentifyAndStoreValidLines(this.horizontalSnapLines, this.horizontalDistances, dragBounds, num2);
				didSnap = true;
			}
			Point result = new Point((this.snapPointX != 4369) ? this.snapPointX : 0, (this.snapPointY != 4369) ? this.snapPointY : 0);
			Rectangle rectangle = new Rectangle(dragBounds.Left + result.X, dragBounds.Top + result.Y, dragBounds.Width, dragBounds.Height);
			this.vertLines = this.EraseOldSnapLines(this.vertLines, this.tempVertLines);
			this.horzLines = this.EraseOldSnapLines(this.horzLines, this.tempHorzLines);
			this.cachedDragRect = rectangle;
			this.dragOffset = dragBounds.Location;
			return result;
		}

		// Token: 0x060024D4 RID: 9428 RVA: 0x000E4267 File Offset: 0x000E2467
		internal void RenderSnapLinesInternal(Rectangle dragRect)
		{
			this.cachedDragRect = dragRect;
			this.RenderSnapLinesInternal();
		}

		// Token: 0x060024D5 RID: 9429 RVA: 0x000E4278 File Offset: 0x000E2478
		internal void RenderSnapLinesInternal()
		{
			this.RenderSnapLines(this.vertLines, this.cachedDragRect);
			this.RenderSnapLines(this.horzLines, this.cachedDragRect);
			this.recentLines = new DragAssistanceManager.Line[this.vertLines.Length + this.horzLines.Length];
			this.vertLines.CopyTo(this.recentLines, 0);
			this.horzLines.CopyTo(this.recentLines, this.vertLines.Length);
		}

		// Token: 0x060024D6 RID: 9430 RVA: 0x000E42F0 File Offset: 0x000E24F0
		internal void OnMouseUp()
		{
			if (this.behaviorService != null)
			{
				DragAssistanceManager.Line[] array = this.GetRecentLines();
				string[] array2 = new string[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					array2[i] = array[i].ToString();
				}
				this.behaviorService.RecentSnapLines = array2;
			}
			this.EraseSnapLines();
			this.graphics.Dispose();
			if (this.disposeEdgePen && this.edgePen != null)
			{
				this.edgePen.Dispose();
			}
			if (this.baselinePen != null)
			{
				this.baselinePen.Dispose();
			}
			if (this.backgroundImage != null)
			{
				this.backgroundImage.Dispose();
			}
		}

		// Token: 0x04001A8D RID: 6797
		private BehaviorService behaviorService;

		// Token: 0x04001A8E RID: 6798
		private IServiceProvider serviceProvider;

		// Token: 0x04001A8F RID: 6799
		private Graphics graphics;

		// Token: 0x04001A90 RID: 6800
		private IntPtr rootComponentHandle;

		// Token: 0x04001A91 RID: 6801
		private Point dragOffset;

		// Token: 0x04001A92 RID: 6802
		private Rectangle cachedDragRect;

		// Token: 0x04001A93 RID: 6803
		private Pen edgePen = SystemPens.Highlight;

		// Token: 0x04001A94 RID: 6804
		private bool disposeEdgePen;

		// Token: 0x04001A95 RID: 6805
		private Pen baselinePen = new Pen(Color.Fuchsia);

		// Token: 0x04001A96 RID: 6806
		private ArrayList verticalSnapLines = new ArrayList();

		// Token: 0x04001A97 RID: 6807
		private ArrayList horizontalSnapLines = new ArrayList();

		// Token: 0x04001A98 RID: 6808
		private ArrayList targetVerticalSnapLines = new ArrayList();

		// Token: 0x04001A99 RID: 6809
		private ArrayList targetHorizontalSnapLines = new ArrayList();

		// Token: 0x04001A9A RID: 6810
		private ArrayList targetSnapLineTypes = new ArrayList();

		// Token: 0x04001A9B RID: 6811
		private int[] verticalDistances;

		// Token: 0x04001A9C RID: 6812
		private int[] horizontalDistances;

		// Token: 0x04001A9D RID: 6813
		private ArrayList tempVertLines = new ArrayList();

		// Token: 0x04001A9E RID: 6814
		private ArrayList tempHorzLines = new ArrayList();

		// Token: 0x04001A9F RID: 6815
		private DragAssistanceManager.Line[] vertLines = new DragAssistanceManager.Line[0];

		// Token: 0x04001AA0 RID: 6816
		private DragAssistanceManager.Line[] horzLines = new DragAssistanceManager.Line[0];

		// Token: 0x04001AA1 RID: 6817
		private Hashtable snapLineToBounds = new Hashtable();

		// Token: 0x04001AA2 RID: 6818
		private DragAssistanceManager.Line[] recentLines;

		// Token: 0x04001AA3 RID: 6819
		private Image backgroundImage;

		// Token: 0x04001AA4 RID: 6820
		private const int snapDistance = 8;

		// Token: 0x04001AA5 RID: 6821
		private int snapPointX;

		// Token: 0x04001AA6 RID: 6822
		private int snapPointY;

		// Token: 0x04001AA7 RID: 6823
		private const int INVALID_VALUE = 4369;

		// Token: 0x04001AA8 RID: 6824
		private bool resizing;

		// Token: 0x04001AA9 RID: 6825
		private bool ctrlDrag;

		// Token: 0x020005A4 RID: 1444
		internal class Line
		{
			// Token: 0x17000A13 RID: 2579
			// (get) Token: 0x060033B0 RID: 13232 RVA: 0x0011AEB9 File Offset: 0x001190B9
			// (set) Token: 0x060033B1 RID: 13233 RVA: 0x0011AEC1 File Offset: 0x001190C1
			public DragAssistanceManager.LineType LineType
			{
				get
				{
					return this.lineType;
				}
				set
				{
					this.lineType = value;
				}
			}

			// Token: 0x17000A14 RID: 2580
			// (get) Token: 0x060033B2 RID: 13234 RVA: 0x0011AECA File Offset: 0x001190CA
			// (set) Token: 0x060033B3 RID: 13235 RVA: 0x0011AED2 File Offset: 0x001190D2
			public Rectangle OriginalBounds
			{
				get
				{
					return this.originalBounds;
				}
				set
				{
					this.originalBounds = value;
				}
			}

			// Token: 0x17000A15 RID: 2581
			// (get) Token: 0x060033B4 RID: 13236 RVA: 0x0011AEDB File Offset: 0x001190DB
			// (set) Token: 0x060033B5 RID: 13237 RVA: 0x0011AEE3 File Offset: 0x001190E3
			public DragAssistanceManager.PaddingLineType PaddingLineType
			{
				get
				{
					return this.paddingLineType;
				}
				set
				{
					this.paddingLineType = value;
				}
			}

			// Token: 0x060033B6 RID: 13238 RVA: 0x0011AEEC File Offset: 0x001190EC
			public Line(int x1, int y1, int x2, int y2)
			{
				this.x1 = x1;
				this.y1 = y1;
				this.x2 = x2;
				this.y2 = y2;
				this.lineType = DragAssistanceManager.LineType.Standard;
			}

			// Token: 0x060033B7 RID: 13239 RVA: 0x0011AF18 File Offset: 0x00119118
			private Line(int x1, int y1, int x2, int y2, DragAssistanceManager.LineType type)
			{
				this.x1 = x1;
				this.y1 = y1;
				this.x2 = x2;
				this.y2 = y2;
				this.lineType = type;
			}

			// Token: 0x060033B8 RID: 13240 RVA: 0x0011AF48 File Offset: 0x00119148
			public static DragAssistanceManager.Line[] GetDiffs(DragAssistanceManager.Line l1, DragAssistanceManager.Line l2)
			{
				if (l1.x1 == l1.x2 && l1.x1 == l2.x1)
				{
					return new DragAssistanceManager.Line[]
					{
						new DragAssistanceManager.Line(l1.x1, Math.Min(l1.y1, l2.y1), l1.x1, Math.Max(l1.y1, l2.y1)),
						new DragAssistanceManager.Line(l1.x1, Math.Min(l1.y2, l2.y2), l1.x1, Math.Max(l1.y2, l2.y2))
					};
				}
				if (l1.y1 == l1.y2 && l1.y1 == l2.y1)
				{
					return new DragAssistanceManager.Line[]
					{
						new DragAssistanceManager.Line(Math.Min(l1.x1, l2.x1), l1.y1, Math.Max(l1.x1, l2.x1), l1.y1),
						new DragAssistanceManager.Line(Math.Min(l1.x2, l2.x2), l1.y1, Math.Max(l1.x2, l2.x2), l1.y1)
					};
				}
				return null;
			}

			// Token: 0x060033B9 RID: 13241 RVA: 0x0011B07C File Offset: 0x0011927C
			public static DragAssistanceManager.Line Overlap(DragAssistanceManager.Line l1, DragAssistanceManager.Line l2)
			{
				if (l1.LineType != l2.LineType)
				{
					return null;
				}
				if (l1.LineType != DragAssistanceManager.LineType.Standard && l1.LineType != DragAssistanceManager.LineType.Baseline)
				{
					return null;
				}
				if (l1.x1 == l1.x2 && l2.x1 == l2.x2 && l1.x1 == l2.x1)
				{
					return new DragAssistanceManager.Line(l1.x1, Math.Min(l1.y1, l2.y1), l1.x2, Math.Max(l1.y2, l2.y2), l1.LineType);
				}
				if (l1.y1 == l1.y2 && l2.y1 == l2.y2 && l1.y1 == l2.y2)
				{
					return new DragAssistanceManager.Line(Math.Min(l1.x1, l2.x1), l1.y1, Math.Max(l1.x2, l2.x2), l1.y2, l1.LineType);
				}
				return null;
			}

			// Token: 0x060033BA RID: 13242 RVA: 0x0011B178 File Offset: 0x00119378
			public override string ToString()
			{
				return string.Concat(new string[]
				{
					"Line, type = ",
					this.lineType.ToString(),
					", dims =(",
					this.x1.ToString(),
					", ",
					this.y1.ToString(),
					")->(",
					this.x2.ToString(),
					", ",
					this.y2.ToString(),
					")"
				});
			}

			// Token: 0x0400228B RID: 8843
			public int x1;

			// Token: 0x0400228C RID: 8844
			public int y1;

			// Token: 0x0400228D RID: 8845
			public int x2;

			// Token: 0x0400228E RID: 8846
			public int y2;

			// Token: 0x0400228F RID: 8847
			private DragAssistanceManager.LineType lineType;

			// Token: 0x04002290 RID: 8848
			private DragAssistanceManager.PaddingLineType paddingLineType;

			// Token: 0x04002291 RID: 8849
			private Rectangle originalBounds;
		}

		// Token: 0x020005A5 RID: 1445
		internal enum LineType
		{
			// Token: 0x04002293 RID: 8851
			Standard,
			// Token: 0x04002294 RID: 8852
			Margin,
			// Token: 0x04002295 RID: 8853
			Padding,
			// Token: 0x04002296 RID: 8854
			Baseline
		}

		// Token: 0x020005A6 RID: 1446
		internal enum PaddingLineType
		{
			// Token: 0x04002298 RID: 8856
			None,
			// Token: 0x04002299 RID: 8857
			PaddingRight,
			// Token: 0x0400229A RID: 8858
			PaddingLeft,
			// Token: 0x0400229B RID: 8859
			PaddingTop,
			// Token: 0x0400229C RID: 8860
			PaddingBottom
		}
	}
}

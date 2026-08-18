using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Telerik.Charting.Styles
{
	// Token: 0x0200176D RID: 5997
	[ToolboxItem(false)]
	internal class RadShapeEditorControl : UserControl
	{
		// Token: 0x170046F5 RID: 18165
		// (get) Token: 0x0600E9F9 RID: 59897 RVA: 0x003534F3 File Offset: 0x003516F3
		public List<ShapePoint> Points
		{
			get
			{
				return this.points;
			}
		}

		// Token: 0x170046F6 RID: 18166
		// (get) Token: 0x0600E9FA RID: 59898 RVA: 0x003534FB File Offset: 0x003516FB
		// (set) Token: 0x0600E9FB RID: 59899 RVA: 0x00353503 File Offset: 0x00351703
		public Rectangle Dimension
		{
			get
			{
				return this.dimension;
			}
			set
			{
				this.dimension = value;
			}
		}

		// Token: 0x0600E9FC RID: 59900 RVA: 0x0035350C File Offset: 0x0035170C
		public RadShapeEditorControl()
		{
			this.dimensionPoints = new ShapePoint[4];
			for (int i = 0; i < 4; i++)
			{
				this.dimensionPoints[i] = new ShapePoint();
			}
			this.InitializeComponent();
			base.SetStyle(ControlStyles.ContainerControl | ControlStyles.UserPaint | ControlStyles.Opaque | ControlStyles.ResizeRedraw | ControlStyles.AllPaintingInWmPaint | ControlStyles.OptimizedDoubleBuffer, true);
			this.contextMenuPoint.RenderMode = ToolStripRenderMode.Professional;
			((ToolStripProfessionalRenderer)this.contextMenuPoint.Renderer).ColorTable.UseSystemColors = true;
			this.contextMenuLine.RenderMode = ToolStripRenderMode.Professional;
			((ToolStripProfessionalRenderer)this.contextMenuLine.Renderer).ColorTable.UseSystemColors = true;
			this.menuItemAddPoint.Click += this.menuItemAddPoint_Click;
			this.menuItemConvert.Click += this.menuItemConvert_Click;
			this.menuItemRemovePoint.Click += this.menuItemRemovePoint_Click;
			this.menuItemRemoveLine.Click += this.menuItemRemoveLine_Click;
			this.menuItemAnchorLeft.Click += this.menuItemAnchorLeft_Click;
			this.menuItemAnchorRight.Click += this.menuItemAnchorRight_Click;
			this.menuItemAnchorTop.Click += this.menuItemAnchorTop_Click;
			this.menuItemAnchorBottom.Click += this.menuItemAnchorBottom_Click;
			this.menuItemConvertLine.Click += this.menuItemConvert_Click;
			this.menuItemLeftTopCorner.Click += this.menuItemLeftTopCorner_Click;
			this.menuItemLeftBottomCorner.Click += this.menuItemLeftBottomCorner_Click;
			this.menuItemRightTopCorner.Click += this.menuItemRightTopCorner_Click;
			this.menuItemRightBottomCorner.Click += this.menuItemRightBottomCorner_Click;
			this.menuItemLocked.Click += this.menuItemLocked_Click;
		}

		// Token: 0x0600E9FD RID: 59901 RVA: 0x00353704 File Offset: 0x00351904
		protected override void OnPaint(PaintEventArgs e)
		{
			using (HatchBrush hatchBrush = new HatchBrush(HatchStyle.DottedGrid, Color.LightGray, Color.White))
			{
				e.Graphics.FillRectangle(hatchBrush, base.ClientRectangle);
			}
			Rectangle clientRectangle = base.ClientRectangle;
			clientRectangle.Width--;
			clientRectangle.Height--;
			e.Graphics.DrawRectangle(SystemPens.ControlDark, clientRectangle);
			using (HatchBrush hatchBrush2 = new HatchBrush(HatchStyle.DottedGrid, Color.DarkGray, Color.LightGray))
			{
				e.Graphics.FillRectangle(hatchBrush2, this.Dimension);
			}
			Rectangle rect = this.dimension;
			int num = rect.Left + rect.Width / 2;
			int num2 = rect.Top + rect.Height / 2;
			e.Graphics.DrawRectangle(Pens.Black, rect);
			e.Graphics.FillRectangle(Brushes.Black, num - 4, rect.Y - 4, 8, 4);
			e.Graphics.FillRectangle(Brushes.Black, num - 4, rect.Bottom, 8, 4);
			e.Graphics.FillRectangle(Brushes.Black, rect.X - 4, num2 - 4, 4, 8);
			e.Graphics.FillRectangle(Brushes.Black, rect.Right, num2 - 4, 4, 8);
			e.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
			using (Brush brush = new SolidBrush(Color.Red))
			{
				using (Pen pen = new Pen(Color.Black, 2f))
				{
					using (Pen pen2 = new Pen(Color.Blue, 2f))
					{
						using (Brush brush2 = new SolidBrush(Color.Blue))
						{
							using (Pen pen3 = new Pen(Color.Green))
							{
								using (Brush brush3 = new SolidBrush(Color.Green))
								{
									for (int i = 0; i < this.points.Count; i++)
									{
										ShapePoint shapePoint = this.points[i];
										ShapePoint shapePoint2 = (i < this.points.Count - 1) ? this.points[i + 1] : this.points[0];
										Pen pen4 = (this.point == shapePoint) ? pen2 : pen;
										if (shapePoint.Bezier)
										{
											e.Graphics.DrawBezier(pen4, shapePoint.GetPoint(), shapePoint.ControlPoint1.GetPoint(), shapePoint.ControlPoint2.GetPoint(), shapePoint2.GetPoint());
											e.Graphics.DrawLine(pen3, shapePoint.GetPoint(), shapePoint.ControlPoint1.GetPoint());
											e.Graphics.DrawLine(pen3, shapePoint2.GetPoint(), shapePoint.ControlPoint2.GetPoint());
											e.Graphics.FillEllipse((shapePoint.ControlPoint1 == this.point || shapePoint.ControlPoint1.Selected) ? brush2 : brush3, shapePoint.ControlPoint1.GetBounds(8));
											e.Graphics.FillEllipse((shapePoint.ControlPoint2 == this.point || shapePoint.ControlPoint2.Selected) ? brush2 : brush3, shapePoint.ControlPoint2.GetBounds(8));
										}
										else
										{
											e.Graphics.DrawLine((shapePoint == this.point) ? pen2 : pen, shapePoint.GetPoint(), shapePoint2.GetPoint());
										}
										e.Graphics.FillEllipse((shapePoint == this.point || shapePoint.Selected) ? brush2 : brush, shapePoint.GetBounds(8));
										e.Graphics.FillEllipse((shapePoint2 == this.point || shapePoint2.Selected) ? brush2 : brush, shapePoint2.GetBounds(8));
									}
									if (this.mouseDown)
									{
										rect = new Rectangle((this.downPoint.X < this.curPoint.X) ? this.downPoint.X : this.curPoint.X, (this.downPoint.Y < this.curPoint.Y) ? this.downPoint.Y : this.curPoint.Y, Math.Abs(this.downPoint.X - this.curPoint.X), Math.Abs(this.downPoint.Y - this.curPoint.Y));
										pen2.Width = 1f;
										e.Graphics.DrawRectangle(pen2, rect);
									}
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x0600E9FE RID: 59902 RVA: 0x00353C80 File Offset: 0x00351E80
		private void RadShapeEditorControl_Load(object sender, EventArgs e)
		{
			if (this.points.Count == 0)
			{
				this.Dimension = new Rectangle(base.ClientRectangle.X + 20, base.ClientRectangle.Y + 20, base.ClientRectangle.Width - 40, base.ClientRectangle.Height - 40);
				this.points.Add(new ShapePoint(this.Dimension.X, this.Dimension.Y));
				this.points.Add(new ShapePoint(this.Dimension.Right, this.Dimension.Y));
				this.points.Add(new ShapePoint(this.Dimension.Right, this.Dimension.Bottom));
				this.points.Add(new ShapePoint(this.Dimension.X, this.Dimension.Bottom));
			}
			if (this.propertyGrid != null)
			{
				this.propertyGrid.PropertyValueChanged += this.propertyGrid_PropertyValueChanged;
			}
		}

		// Token: 0x0600E9FF RID: 59903 RVA: 0x00353DC4 File Offset: 0x00351FC4
		private void RadShapeEditorControl_MouseDown(object sender, MouseEventArgs e)
		{
			this.mouseDown = true;
			this.downPoint = new Point(e.X, e.Y);
			this.curPoint = this.downPoint;
			this.point = null;
			foreach (ShapePoint shapePoint in this.points)
			{
				if (shapePoint.IsVisible(e.X, e.Y, 8))
				{
					this.pointType = RadShapeEditorControl.PointTypes.Point;
					this.point = shapePoint;
					break;
				}
				if (shapePoint.Bezier)
				{
					if (shapePoint.ControlPoint1.IsVisible(e.X, e.Y, 8))
					{
						this.pointType = RadShapeEditorControl.PointTypes.ControlPoint;
						this.point = shapePoint.ControlPoint1;
						break;
					}
					if (shapePoint.ControlPoint2.IsVisible(e.X, e.Y, 8))
					{
						this.pointType = RadShapeEditorControl.PointTypes.ControlPoint;
						this.point = shapePoint.ControlPoint2;
						break;
					}
				}
			}
			if (this.point == null)
			{
				for (int i = 0; i < this.points.Count; i++)
				{
					ShapePoint shapePoint2 = this.points[i];
					ShapePoint nextPoint = (i < this.points.Count - 1) ? this.points[i + 1] : this.points[0];
					if (shapePoint2.IsVisible(nextPoint, new Point(e.X, e.Y), 3))
					{
						this.pointType = RadShapeEditorControl.PointTypes.Line;
						this.point = shapePoint2;
						break;
					}
				}
			}
			if (this.point != null)
			{
				this.propertyGrid.SelectedObject = this.point;
			}
			else
			{
				this.propertyGrid.SelectedObject = null;
			}
			this.Refresh();
			if (e.Button == MouseButtons.Left && (this.point == null || !this.point.Selected))
			{
				foreach (ShapePoint shapePoint3 in this.points)
				{
					shapePoint3.Selected = false;
					shapePoint3.ControlPoint1.Selected = false;
					shapePoint3.ControlPoint2.Selected = false;
				}
			}
			if (this.point != null)
			{
				this.mouseDown = false;
			}
			if (e.Button == MouseButtons.Right && this.point != null)
			{
				if (this.pointType == RadShapeEditorControl.PointTypes.Point && this.point is ShapePoint)
				{
					this.menuItemAnchorLeft.Checked = ((this.point.Anchor & AnchorStyles.Left) != AnchorStyles.None);
					this.menuItemAnchorRight.Checked = ((this.point.Anchor & AnchorStyles.Right) != AnchorStyles.None);
					this.menuItemAnchorTop.Checked = ((this.point.Anchor & AnchorStyles.Top) != AnchorStyles.None);
					this.menuItemAnchorBottom.Checked = ((this.point.Anchor & AnchorStyles.Bottom) != AnchorStyles.None);
					if (this.points.Count <= 2)
					{
						this.menuItemRemoveLine.Enabled = false;
						this.menuItemRemovePoint.Enabled = false;
					}
					this.menuItemLocked.Checked = this.point.Locked;
					if ((this.point as ShapePoint).Bezier)
					{
						this.contextMenuPoint.Items[1].Text = "Convert to Line";
					}
					else
					{
						this.contextMenuPoint.Items[1].Text = "Convert to Bezier Curve";
					}
					this.contextMenuPoint.Show(base.PointToScreen(new Point(e.X, e.Y)));
					return;
				}
				if (this.pointType == RadShapeEditorControl.PointTypes.Line)
				{
					if ((this.point as ShapePoint).Bezier)
					{
						this.contextMenuLine.Items[1].Text = "Convert to Line";
					}
					else
					{
						this.contextMenuLine.Items[1].Text = "Convert to Bezier Curve";
					}
					this.contextMenuLine.Show(base.PointToScreen(new Point(e.X, e.Y)));
				}
			}
		}

		// Token: 0x0600EA00 RID: 59904 RVA: 0x003541E0 File Offset: 0x003523E0
		private void RadShapeEditorControl_MouseMove(object sender, MouseEventArgs e)
		{
			this.oldCurPoint = this.curPoint;
			this.curPoint = new Point(e.X, e.Y);
			if (e.Button == MouseButtons.Left && this.oldCurPoint != this.curPoint)
			{
				if (this.point != null && this.pointType != RadShapeEditorControl.PointTypes.Line && !this.point.Locked)
				{
					this.point.X = (float)((e.X < 0) ? 0 : e.X);
					this.point.Y = (float)((e.Y < 0) ? 0 : e.Y);
					this.propertyGrid.Refresh();
				}
				if (!this.mouseDown)
				{
					int num = this.curPoint.X - this.oldCurPoint.X;
					int num2 = this.curPoint.Y - this.oldCurPoint.Y;
					foreach (ShapePoint shapePoint in this.points)
					{
						if (shapePoint != this.point && shapePoint.Selected && !shapePoint.Locked)
						{
							shapePoint.X += (float)num;
							shapePoint.Y += (float)num2;
						}
						if (shapePoint.Bezier)
						{
							if (shapePoint.ControlPoint1.Selected && !shapePoint.ControlPoint1.Locked)
							{
								shapePoint.ControlPoint1.X += (float)num;
								shapePoint.ControlPoint1.Y += (float)num2;
							}
							if (shapePoint.ControlPoint2.Selected && !shapePoint.ControlPoint2.Locked)
							{
								shapePoint.ControlPoint2.X += (float)num;
								shapePoint.ControlPoint2.Y += (float)num2;
							}
						}
					}
				}
				this.Refresh();
			}
		}

		// Token: 0x0600EA01 RID: 59905 RVA: 0x003543E8 File Offset: 0x003525E8
		private void RadShapeEditorControl_MouseUp(object sender, MouseEventArgs e)
		{
			if (this.mouseDown)
			{
				this.mouseDown = false;
				Rectangle rectangle = new Rectangle((this.downPoint.X < this.curPoint.X) ? this.downPoint.X : this.curPoint.X, (this.downPoint.Y < this.curPoint.Y) ? this.downPoint.Y : this.curPoint.Y, Math.Abs(this.downPoint.X - this.curPoint.X), Math.Abs(this.downPoint.Y - this.curPoint.Y));
				foreach (ShapePoint shapePoint in this.points)
				{
					shapePoint.Selected = (this.point == null && rectangle.Contains(shapePoint.GetPoint()));
					if (shapePoint.Bezier)
					{
						shapePoint.ControlPoint1.Selected = (this.point == null && rectangle.Contains(shapePoint.ControlPoint1.GetPoint()));
						shapePoint.ControlPoint2.Selected = (this.point == null && rectangle.Contains(shapePoint.ControlPoint2.GetPoint()));
					}
					else
					{
						shapePoint.ControlPoint1.Selected = false;
						shapePoint.ControlPoint2.Selected = false;
					}
				}
				this.Refresh();
			}
		}

		// Token: 0x0600EA02 RID: 59906 RVA: 0x00354584 File Offset: 0x00352784
		private void propertyGrid_PropertyValueChanged(object s, PropertyValueChangedEventArgs e)
		{
			if (this.propertyGrid.SelectedObject is ShapePoint)
			{
				int index = this.points.IndexOf(this.point as ShapePoint);
				this.points[index] = (this.propertyGrid.SelectedObject as ShapePoint);
				this.point = this.points[index];
			}
			this.Refresh();
		}

		// Token: 0x0600EA03 RID: 59907 RVA: 0x003545EE File Offset: 0x003527EE
		private void menuItemAnchorLeft_Click(object sender, EventArgs e)
		{
			this.point.Anchor ^= AnchorStyles.Left;
			this.propertyGrid.Refresh();
		}

		// Token: 0x0600EA04 RID: 59908 RVA: 0x0035460E File Offset: 0x0035280E
		private void menuItemAnchorRight_Click(object sender, EventArgs e)
		{
			this.point.Anchor ^= AnchorStyles.Right;
			this.propertyGrid.Refresh();
		}

		// Token: 0x0600EA05 RID: 59909 RVA: 0x0035462E File Offset: 0x0035282E
		private void menuItemAnchorTop_Click(object sender, EventArgs e)
		{
			this.point.Anchor ^= AnchorStyles.Top;
			this.propertyGrid.Refresh();
		}

		// Token: 0x0600EA06 RID: 59910 RVA: 0x0035464E File Offset: 0x0035284E
		private void menuItemAnchorBottom_Click(object sender, EventArgs e)
		{
			this.point.Anchor ^= AnchorStyles.Bottom;
			this.propertyGrid.Refresh();
		}

		// Token: 0x0600EA07 RID: 59911 RVA: 0x0035466E File Offset: 0x0035286E
		private void menuItemRemoveLine_Click(object sender, EventArgs e)
		{
			if (this.points.Count > 2)
			{
				this.points.Remove(this.point as ShapePoint);
				this.point = null;
				this.propertyGrid.SelectedObject = null;
				this.Refresh();
			}
		}

		// Token: 0x0600EA08 RID: 59912 RVA: 0x003546AE File Offset: 0x003528AE
		private void menuItemRemovePoint_Click(object sender, EventArgs e)
		{
			if (this.points.Count > 2)
			{
				this.points.Remove(this.point as ShapePoint);
				this.point = null;
				this.propertyGrid.SelectedObject = null;
				this.Refresh();
			}
		}

		// Token: 0x0600EA09 RID: 59913 RVA: 0x003546F0 File Offset: 0x003528F0
		private void menuItemConvert_Click(object sender, EventArgs e)
		{
			(this.point as ShapePoint).Bezier = !(this.point as ShapePoint).Bezier;
			if ((this.point as ShapePoint).Bezier)
			{
				int num = this.points.IndexOf(this.point as ShapePoint) + 1;
				if (num >= this.points.Count)
				{
					num = 0;
				}
				ShapePoint nextPoint = this.points[num];
				(this.point as ShapePoint).CreateBezier(nextPoint);
			}
			this.propertyGrid.Refresh();
			this.Refresh();
		}

		// Token: 0x0600EA0A RID: 59914 RVA: 0x0035478C File Offset: 0x0035298C
		private void menuItemAddPoint_Click(object sender, EventArgs e)
		{
			int num = this.points.IndexOf(this.point as ShapePoint) + 1;
			if (num >= this.points.Count)
			{
				num = 0;
			}
			this.points.Insert(num, new ShapePoint(this.downPoint.X, this.downPoint.Y));
			this.point = this.points[num];
			this.propertyGrid.SelectedObject = this.point;
			this.Refresh();
		}

		// Token: 0x0600EA0B RID: 59915 RVA: 0x00354814 File Offset: 0x00352A14
		private void menuItemLeftTopCorner_Click(object sender, EventArgs e)
		{
			this.point.X = (float)this.dimension.X;
			this.point.Y = (float)this.dimension.Y;
			this.point.Anchor = (AnchorStyles.Top | AnchorStyles.Left);
			this.point.Locked = true;
			this.Refresh();
		}

		// Token: 0x0600EA0C RID: 59916 RVA: 0x00354870 File Offset: 0x00352A70
		private void menuItemRightTopCorner_Click(object sender, EventArgs e)
		{
			this.point.X = (float)this.dimension.Right;
			this.point.Y = (float)this.dimension.Y;
			this.point.Anchor = (AnchorStyles.Top | AnchorStyles.Right);
			this.point.Locked = true;
			this.Refresh();
		}

		// Token: 0x0600EA0D RID: 59917 RVA: 0x003548CC File Offset: 0x00352ACC
		private void menuItemLeftBottomCorner_Click(object sender, EventArgs e)
		{
			this.point.X = (float)this.dimension.X;
			this.point.Y = (float)this.dimension.Bottom;
			this.point.Anchor = (AnchorStyles.Bottom | AnchorStyles.Left);
			this.point.Locked = true;
			this.Refresh();
		}

		// Token: 0x0600EA0E RID: 59918 RVA: 0x00354928 File Offset: 0x00352B28
		private void menuItemRightBottomCorner_Click(object sender, EventArgs e)
		{
			this.point.X = (float)this.dimension.Right;
			this.point.Y = (float)this.dimension.Bottom;
			this.point.Anchor = (AnchorStyles.Bottom | AnchorStyles.Right);
			this.point.Locked = true;
			this.Refresh();
		}

		// Token: 0x0600EA0F RID: 59919 RVA: 0x00354982 File Offset: 0x00352B82
		private void menuItemLocked_Click(object sender, EventArgs e)
		{
			this.point.Locked = !this.point.Locked;
		}

		// Token: 0x0600EA10 RID: 59920 RVA: 0x003549A0 File Offset: 0x00352BA0
		public CustomShape GetShape()
		{
			CustomShape customShape = new CustomShape();
			customShape.Dimension = this.Dimension;
			foreach (ShapePoint item in this.Points)
			{
				customShape.Points.Add(item);
			}
			return customShape;
		}

		// Token: 0x0600EA11 RID: 59921 RVA: 0x00354A0C File Offset: 0x00352C0C
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600EA12 RID: 59922 RVA: 0x00354A2C File Offset: 0x00352C2C
		private void InitializeComponent()
		{
			this.components = new Container();
			this.contextMenuPoint = new ContextMenuStrip(this.components);
			this.menuItemRemovePoint = new ToolStripMenuItem();
			this.menuItemConvert = new ToolStripMenuItem();
			this.anchorStylesToolStripMenuItem = new ToolStripMenuItem();
			this.menuItemAnchorLeft = new ToolStripMenuItem();
			this.menuItemAnchorRight = new ToolStripMenuItem();
			this.menuItemAnchorTop = new ToolStripMenuItem();
			this.menuItemAnchorBottom = new ToolStripMenuItem();
			this.contextMenuLine = new ContextMenuStrip(this.components);
			this.menuItemRemoveLine = new ToolStripMenuItem();
			this.menuItemConvertLine = new ToolStripMenuItem();
			this.menuItemAddPoint = new ToolStripMenuItem();
			this.snapToToolStripMenuItem = new ToolStripMenuItem();
			this.menuItemLeftTopCorner = new ToolStripMenuItem();
			this.menuItemRightTopCorner = new ToolStripMenuItem();
			this.menuItemLeftBottomCorner = new ToolStripMenuItem();
			this.menuItemRightBottomCorner = new ToolStripMenuItem();
			this.menuItemLocked = new ToolStripMenuItem();
			this.contextMenuPoint.SuspendLayout();
			this.contextMenuLine.SuspendLayout();
			base.SuspendLayout();
			this.contextMenuPoint.Items.AddRange(new ToolStripItem[]
			{
				this.menuItemRemovePoint,
				this.menuItemConvert,
				this.anchorStylesToolStripMenuItem,
				this.snapToToolStripMenuItem,
				this.menuItemLocked
			});
			this.contextMenuPoint.Name = "contextMenuPoint";
			this.contextMenuPoint.Size = new Size(202, 136);
			this.menuItemRemovePoint.Name = "menuItemRemovePoint";
			this.menuItemRemovePoint.Size = new Size(201, 22);
			this.menuItemRemovePoint.Text = "Remove Point";
			this.menuItemConvert.Name = "menuItemConvert";
			this.menuItemConvert.Size = new Size(201, 22);
			this.menuItemConvert.Text = "Convert to Bezier Curve";
			this.anchorStylesToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.menuItemAnchorLeft,
				this.menuItemAnchorRight,
				this.menuItemAnchorTop,
				this.menuItemAnchorBottom
			});
			this.anchorStylesToolStripMenuItem.Name = "anchorStylesToolStripMenuItem";
			this.anchorStylesToolStripMenuItem.Size = new Size(201, 22);
			this.anchorStylesToolStripMenuItem.Text = "Anchor styles";
			this.menuItemAnchorLeft.Name = "menuItemAnchorLeft";
			this.menuItemAnchorLeft.Size = new Size(152, 22);
			this.menuItemAnchorLeft.Text = "Left";
			this.menuItemAnchorRight.Name = "menuItemAnchorRight";
			this.menuItemAnchorRight.Size = new Size(152, 22);
			this.menuItemAnchorRight.Text = "Right";
			this.menuItemAnchorTop.Name = "menuItemAnchorTop";
			this.menuItemAnchorTop.Size = new Size(152, 22);
			this.menuItemAnchorTop.Text = "Top";
			this.menuItemAnchorBottom.Name = "menuItemAnchorBottom";
			this.menuItemAnchorBottom.Size = new Size(152, 22);
			this.menuItemAnchorBottom.Text = "Bottom";
			this.contextMenuLine.Items.AddRange(new ToolStripItem[]
			{
				this.menuItemRemoveLine,
				this.menuItemConvertLine,
				this.menuItemAddPoint
			});
			this.contextMenuLine.Name = "contextMenuLine";
			this.contextMenuLine.Size = new Size(202, 70);
			this.menuItemRemoveLine.Name = "menuItemRemoveLine";
			this.menuItemRemoveLine.Size = new Size(201, 22);
			this.menuItemRemoveLine.Text = "Remove Line";
			this.menuItemConvertLine.Name = "menuItemConvertLine";
			this.menuItemConvertLine.Size = new Size(201, 22);
			this.menuItemConvertLine.Text = "Convert to Bezier Curve";
			this.menuItemAddPoint.Name = "menuItemAddPoint";
			this.menuItemAddPoint.Size = new Size(201, 22);
			this.menuItemAddPoint.Text = "Add Point";
			this.snapToToolStripMenuItem.DropDownItems.AddRange(new ToolStripItem[]
			{
				this.menuItemLeftTopCorner,
				this.menuItemRightTopCorner,
				this.menuItemLeftBottomCorner,
				this.menuItemRightBottomCorner
			});
			this.snapToToolStripMenuItem.Name = "snapToToolStripMenuItem";
			this.snapToToolStripMenuItem.Size = new Size(201, 22);
			this.snapToToolStripMenuItem.Text = "Snap to";
			this.menuItemLeftTopCorner.Name = "menuItemLeftTopCorner";
			this.menuItemLeftTopCorner.Size = new Size(177, 22);
			this.menuItemLeftTopCorner.Text = "LeftTop Corner";
			this.menuItemRightTopCorner.Name = "menuItemRightTopCorner";
			this.menuItemRightTopCorner.Size = new Size(177, 22);
			this.menuItemRightTopCorner.Text = "RightTop Corner";
			this.menuItemLeftBottomCorner.Name = "menuItemLeftBottomCorner";
			this.menuItemLeftBottomCorner.Size = new Size(177, 22);
			this.menuItemLeftBottomCorner.Text = "LeftBottom Corner";
			this.menuItemRightBottomCorner.Name = "menuItemRightBottomCorner";
			this.menuItemRightBottomCorner.Size = new Size(177, 22);
			this.menuItemRightBottomCorner.Text = "RightBottomCorner";
			this.menuItemLocked.Name = "menuItemLocked";
			this.menuItemLocked.Size = new Size(201, 22);
			this.menuItemLocked.Text = "Locked";
			base.AutoScaleDimensions = new SizeF(6f, 13f);
			base.AutoScaleMode = AutoScaleMode.Font;
			base.Name = "RadShapeEditorControl";
			base.Load += this.RadShapeEditorControl_Load;
			base.MouseDown += this.RadShapeEditorControl_MouseDown;
			base.MouseMove += this.RadShapeEditorControl_MouseMove;
			base.MouseUp += this.RadShapeEditorControl_MouseUp;
			this.contextMenuPoint.ResumeLayout(false);
			this.contextMenuLine.ResumeLayout(false);
			base.ResumeLayout(false);
		}

		// Token: 0x04004347 RID: 17223
		public PropertyGrid propertyGrid;

		// Token: 0x04004348 RID: 17224
		private ShapePoint[] dimensionPoints;

		// Token: 0x04004349 RID: 17225
		private ShapePointBase point;

		// Token: 0x0400434A RID: 17226
		private RadShapeEditorControl.PointTypes pointType;

		// Token: 0x0400434B RID: 17227
		private Point downPoint;

		// Token: 0x0400434C RID: 17228
		private Point curPoint;

		// Token: 0x0400434D RID: 17229
		private Point oldCurPoint;

		// Token: 0x0400434E RID: 17230
		private bool mouseDown;

		// Token: 0x0400434F RID: 17231
		private List<ShapePoint> points = new List<ShapePoint>();

		// Token: 0x04004350 RID: 17232
		private Rectangle dimension = new Rectangle(10, 10, 100, 100);

		// Token: 0x04004351 RID: 17233
		private IContainer components;

		// Token: 0x04004352 RID: 17234
		private ContextMenuStrip contextMenuPoint;

		// Token: 0x04004353 RID: 17235
		private ContextMenuStrip contextMenuLine;

		// Token: 0x04004354 RID: 17236
		private ToolStripMenuItem menuItemRemovePoint;

		// Token: 0x04004355 RID: 17237
		private ToolStripMenuItem menuItemConvert;

		// Token: 0x04004356 RID: 17238
		private ToolStripMenuItem anchorStylesToolStripMenuItem;

		// Token: 0x04004357 RID: 17239
		private ToolStripMenuItem menuItemAnchorLeft;

		// Token: 0x04004358 RID: 17240
		private ToolStripMenuItem menuItemAnchorRight;

		// Token: 0x04004359 RID: 17241
		private ToolStripMenuItem menuItemAnchorTop;

		// Token: 0x0400435A RID: 17242
		private ToolStripMenuItem menuItemAnchorBottom;

		// Token: 0x0400435B RID: 17243
		private ToolStripMenuItem menuItemRemoveLine;

		// Token: 0x0400435C RID: 17244
		private ToolStripMenuItem menuItemAddPoint;

		// Token: 0x0400435D RID: 17245
		private ToolStripMenuItem menuItemConvertLine;

		// Token: 0x0400435E RID: 17246
		private ToolStripMenuItem snapToToolStripMenuItem;

		// Token: 0x0400435F RID: 17247
		private ToolStripMenuItem menuItemLeftTopCorner;

		// Token: 0x04004360 RID: 17248
		private ToolStripMenuItem menuItemRightTopCorner;

		// Token: 0x04004361 RID: 17249
		private ToolStripMenuItem menuItemLeftBottomCorner;

		// Token: 0x04004362 RID: 17250
		private ToolStripMenuItem menuItemRightBottomCorner;

		// Token: 0x04004363 RID: 17251
		private ToolStripMenuItem menuItemLocked;

		// Token: 0x0200176E RID: 5998
		private enum PointTypes
		{
			// Token: 0x04004365 RID: 17253
			Point,
			// Token: 0x04004366 RID: 17254
			ControlPoint,
			// Token: 0x04004367 RID: 17255
			Line
		}
	}
}

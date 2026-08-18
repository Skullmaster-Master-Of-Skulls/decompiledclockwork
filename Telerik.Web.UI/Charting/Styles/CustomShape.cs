using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Telerik.Charting.Styles
{
	// Token: 0x0200176A RID: 5994
	[Designer(typeof(CustomShapeDesigner))]
	[ToolboxItem(false)]
	internal class CustomShape : ElementShape
	{
		// Token: 0x0600E9DD RID: 59869 RVA: 0x00352BD7 File Offset: 0x00350DD7
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public CustomShape()
		{
			this.points = new List<ShapePoint>();
		}

		// Token: 0x0600E9DE RID: 59870 RVA: 0x00352BF5 File Offset: 0x00350DF5
		public CustomShape(IContainer container)
		{
			container.Add(this);
		}

		// Token: 0x170046EF RID: 18159
		// (get) Token: 0x0600E9DF RID: 59871 RVA: 0x00352C0F File Offset: 0x00350E0F
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public List<ShapePoint> Points
		{
			get
			{
				return this.points;
			}
		}

		// Token: 0x170046F0 RID: 18160
		// (get) Token: 0x0600E9E0 RID: 59872 RVA: 0x00352C17 File Offset: 0x00350E17
		// (set) Token: 0x0600E9E1 RID: 59873 RVA: 0x00352C1F File Offset: 0x00350E1F
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

		// Token: 0x0600E9E2 RID: 59874 RVA: 0x00352C28 File Offset: 0x00350E28
		internal override GraphicsPath CreatePath(Rectangle bounds)
		{
			GraphicsPath graphicsPath = new GraphicsPath();
			for (int i = 0; i < this.points.Count; i++)
			{
				ShapePoint shapePoint = this.points[i];
				ShapePoint shapePoint2 = (i < this.points.Count - 1) ? this.points[i + 1] : this.points[0];
				Point point = shapePoint.GetPoint(this.dimension, bounds);
				Point point2 = shapePoint2.GetPoint(this.dimension, bounds);
				if (shapePoint.Bezier)
				{
					graphicsPath.AddBezier(point, shapePoint.ControlPoint1.GetPoint(this.dimension, bounds), shapePoint.ControlPoint2.GetPoint(this.dimension, bounds), point2);
				}
				else
				{
					graphicsPath.AddLine(point, point2);
				}
			}
			graphicsPath.CloseAllFigures();
			return graphicsPath;
		}

		// Token: 0x0600E9E3 RID: 59875 RVA: 0x00352CF8 File Offset: 0x00350EF8
		internal override string SerializeProperties()
		{
			string text = string.Format("{0},{1},{2},{3}:", new object[]
			{
				this.dimension.X,
				this.dimension.Y,
				this.dimension.Width,
				this.dimension.Height
			});
			foreach (ShapePoint shapePoint in this.points)
			{
				text += string.Format("{0},{1},{2},{3},{4},{5},{6},{7}:", new object[]
				{
					(int)shapePoint.X,
					(int)shapePoint.Y,
					shapePoint.Bezier,
					(int)shapePoint.ControlPoint1.X,
					(int)shapePoint.ControlPoint1.Y,
					(int)shapePoint.ControlPoint2.X,
					(int)shapePoint.ControlPoint2.Y,
					(int)shapePoint.Anchor
				});
			}
			return text;
		}

		// Token: 0x0600E9E4 RID: 59876 RVA: 0x00352E58 File Offset: 0x00351058
		internal override void DeserializeProperties(string propertiesString)
		{
			string[] array = propertiesString.Split(new char[]
			{
				':'
			});
			string[] array2 = array[0].Split(new char[]
			{
				','
			});
			this.dimension = new Rectangle(int.Parse(array2[0]), int.Parse(array2[1]), int.Parse(array2[2]), int.Parse(array2[3]));
			for (int i = 1; i < array.Length; i++)
			{
				string[] array3 = array[i].Split(new char[]
				{
					','
				});
				if (array3.Length > 2)
				{
					ShapePoint shapePoint = new ShapePoint(int.Parse(array3[0]), int.Parse(array3[1]));
					shapePoint.Bezier = bool.Parse(array3[2]);
					shapePoint.ControlPoint1.X = (float)int.Parse(array3[3]);
					shapePoint.ControlPoint1.Y = (float)int.Parse(array3[4]);
					shapePoint.ControlPoint2.X = (float)int.Parse(array3[5]);
					shapePoint.ControlPoint2.Y = (float)int.Parse(array3[6]);
					shapePoint.Anchor = (AnchorStyles)int.Parse(array3[7]);
					this.points.Add(shapePoint);
				}
			}
		}

		// Token: 0x04004343 RID: 17219
		private List<ShapePoint> points = new List<ShapePoint>();

		// Token: 0x04004344 RID: 17220
		private Rectangle dimension;
	}
}

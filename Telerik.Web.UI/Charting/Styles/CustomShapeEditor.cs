using System;
using System.ComponentModel;
using System.Drawing;
using System.Drawing.Design;
using System.Drawing.Drawing2D;
using System.Windows.Forms;

namespace Telerik.Charting.Styles
{
	// Token: 0x0200176C RID: 5996
	internal class CustomShapeEditor : UITypeEditor
	{
		// Token: 0x0600E9F4 RID: 59892 RVA: 0x003533D7 File Offset: 0x003515D7
		public override UITypeEditorEditStyle GetEditStyle(ITypeDescriptorContext context)
		{
			return UITypeEditorEditStyle.Modal;
		}

		// Token: 0x0600E9F5 RID: 59893 RVA: 0x003533DC File Offset: 0x003515DC
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider provider, object value)
		{
			CustomShape customShape = new CustomShape();
			customShape.DeserializeProperties(value.ToString());
			CustomShapeEditorForm customShapeEditorForm = new CustomShapeEditorForm();
			if (customShape.Points.Count != 0)
			{
				customShapeEditorForm.EditorControl.Dimension = customShape.Dimension;
				customShapeEditorForm.EditorControl.Points.AddRange(customShape.Points);
			}
			if (customShapeEditorForm.ShowDialog() == DialogResult.OK)
			{
				customShape = new CustomShape();
				customShape.Points.AddRange(customShapeEditorForm.EditorControl.Points);
				customShape.Dimension = customShapeEditorForm.EditorControl.Dimension;
				CustomFigure customFigure = new CustomFigure();
				customFigure.Name = "";
				customFigure.Description = customShape.SerializeProperties();
				return customShape.SerializeProperties();
			}
			return value;
		}

		// Token: 0x0600E9F6 RID: 59894 RVA: 0x00353490 File Offset: 0x00351690
		public override void PaintValue(PaintValueEventArgs e)
		{
			CustomShape customShape = e.Value as CustomShape;
			if (customShape != null)
			{
				using (GraphicsPath graphicsPath = customShape.CreatePath(e.Bounds))
				{
					e.Graphics.DrawPath(Pens.Black, graphicsPath);
				}
			}
		}

		// Token: 0x0600E9F7 RID: 59895 RVA: 0x003534E8 File Offset: 0x003516E8
		public override bool GetPaintValueSupported(ITypeDescriptorContext context)
		{
			return true;
		}
	}
}

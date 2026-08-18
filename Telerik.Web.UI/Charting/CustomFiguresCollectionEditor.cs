using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;
using System.Windows.Forms;
using Telerik.Charting.Styles;

namespace Telerik.Charting
{
	// Token: 0x02001755 RID: 5973
	internal class CustomFiguresCollectionEditor : CollectionEditor
	{
		// Token: 0x0600E8EC RID: 59628 RVA: 0x00344EE0 File Offset: 0x003430E0
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public CustomFiguresCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x0600E8ED RID: 59629 RVA: 0x00344EEC File Offset: 0x003430EC
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider isp, object value)
		{
			this.chartComponent = (IChartComponent)context.Instance;
			this.customFiguresCollection = new CustomFiguresCollection();
			CustomFiguresCollection customFiguresCollection = value as CustomFiguresCollection;
			if (customFiguresCollection != null)
			{
				foreach (CustomFigure item in customFiguresCollection)
				{
					this.customFiguresCollection.Add(item);
				}
			}
			this.cancel = false;
			object result = base.EditValue(context, isp, value);
			if (!this.cancel)
			{
				return result;
			}
			return this.UndoChanges();
		}

		// Token: 0x0600E8EE RID: 59630 RVA: 0x00344F80 File Offset: 0x00343180
		private CustomFiguresCollection UndoChanges()
		{
			this.chartComponent.Chart.Figures.Figures.Clear();
			this.chartComponent.Chart.Figures.Figures.AddRange(DefaultFigures.FiguresList);
			this.chartComponent.Chart.CustomFigures.Clear();
			foreach (CustomFigure customFigure in this.customFiguresCollection)
			{
				this.chartComponent.Chart.Figures.Add(customFigure.Name);
				this.chartComponent.Chart.CustomFigures.Add(customFigure);
			}
			return this.chartComponent.Chart.CustomFigures;
		}

		// Token: 0x0600E8EF RID: 59631 RVA: 0x00345058 File Offset: 0x00343258
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		protected override void CancelChanges()
		{
			base.CancelChanges();
			this.cancel = true;
		}

		// Token: 0x0600E8F0 RID: 59632 RVA: 0x00345068 File Offset: 0x00343268
		protected override object CreateInstance(Type itemType)
		{
			CustomShapeEditorForm customShapeEditorForm = new CustomShapeEditorForm();
			CustomFigure customFigure = new CustomFigure();
			if (customShapeEditorForm.ShowDialog() == DialogResult.OK)
			{
				CustomShape customShape = new CustomShape();
				customShape.Points.AddRange(customShapeEditorForm.EditorControl.Points);
				customShape.Dimension = customShapeEditorForm.EditorControl.Dimension;
				int num = 1;
				bool flag;
				do
				{
					flag = true;
					foreach (CustomFigure customFigure2 in this.chartComponent.Chart.CustomFigures)
					{
						if (object.Equals(customFigure2.Name, "Figure" + num))
						{
							flag = false;
							num++;
							break;
						}
					}
				}
				while (!flag);
				customFigure.Name = "Figure" + num;
				customFigure.Description = customShape.SerializeProperties();
				this.chartComponent.Chart.CustomFigures.Add(customFigure);
				return customFigure;
			}
			customFigure.Name = "";
			customFigure.Description = "";
			return customFigure;
		}

		// Token: 0x04004300 RID: 17152
		private IChartComponent chartComponent;

		// Token: 0x04004301 RID: 17153
		private CustomFiguresCollection customFiguresCollection;

		// Token: 0x04004302 RID: 17154
		private bool cancel;
	}
}

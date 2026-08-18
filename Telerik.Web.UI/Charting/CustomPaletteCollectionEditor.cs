using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Charting
{
	// Token: 0x0200174C RID: 5964
	internal class CustomPaletteCollectionEditor : CollectionEditor
	{
		// Token: 0x0600E8C9 RID: 59593 RVA: 0x0034453E File Offset: 0x0034273E
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public CustomPaletteCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x0600E8CA RID: 59594 RVA: 0x00344547 File Offset: 0x00342747
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider isp, object value)
		{
			this.chartComponent = (IChartComponent)context.Instance;
			return base.EditValue(context, isp, value);
		}

		// Token: 0x0600E8CB RID: 59595 RVA: 0x00344564 File Offset: 0x00342764
		protected override object CreateInstance(Type itemType)
		{
			int num = 1;
			bool flag;
			do
			{
				flag = true;
				foreach (Palette palette in this.chartComponent.Chart.CustomPalettes)
				{
					if (object.Equals(palette.Name, "Palette" + num))
					{
						flag = false;
						num++;
						break;
					}
				}
			}
			while (!flag);
			return new Palette("Palette" + num);
		}

		// Token: 0x040042F3 RID: 17139
		private IChartComponent chartComponent;
	}
}

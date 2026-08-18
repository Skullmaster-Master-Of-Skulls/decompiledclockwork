using System;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics.CodeAnalysis;

namespace Telerik.Charting
{
	// Token: 0x02001759 RID: 5977
	internal class PaletteItemsCollectionEditor : CollectionEditor
	{
		// Token: 0x0600E8F9 RID: 59641 RVA: 0x003454A0 File Offset: 0x003436A0
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public PaletteItemsCollectionEditor(Type type) : base(type)
		{
		}

		// Token: 0x0600E8FA RID: 59642 RVA: 0x003454A9 File Offset: 0x003436A9
		[SuppressMessage("Microsoft.Security", "CA2116:AptcaMethodsShouldOnlyCallAptcaMethods")]
		public override object EditValue(ITypeDescriptorContext context, IServiceProvider isp, object value)
		{
			this._palette = (Palette)context.Instance;
			return base.EditValue(context, isp, value);
		}

		// Token: 0x0600E8FB RID: 59643 RVA: 0x003454C8 File Offset: 0x003436C8
		protected override object CreateInstance(Type itemType)
		{
			int num = 1;
			bool flag;
			do
			{
				flag = true;
				foreach (PaletteItem paletteItem in this._palette.Items)
				{
					if (object.Equals(paletteItem.Name, "PaletteItem " + num))
					{
						flag = false;
						num++;
						break;
					}
				}
			}
			while (!flag);
			return new PaletteItem
			{
				Name = "PaletteItem " + num
			};
		}

		// Token: 0x04004305 RID: 17157
		private Palette _palette;
	}
}

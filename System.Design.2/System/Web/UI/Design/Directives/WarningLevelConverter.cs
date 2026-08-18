using System;
using System.ComponentModel;

namespace System.Web.UI.Design.Directives
{
	// Token: 0x0200018E RID: 398
	internal class WarningLevelConverter : TypeConverter
	{
		// Token: 0x06000E7E RID: 3710 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool GetStandardValuesSupported(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06000E7F RID: 3711 RVA: 0x00003B0F File Offset: 0x00001D0F
		public override bool GetStandardValuesExclusive(ITypeDescriptorContext context)
		{
			return true;
		}

		// Token: 0x06000E80 RID: 3712 RVA: 0x00054600 File Offset: 0x00052800
		public override TypeConverter.StandardValuesCollection GetStandardValues(ITypeDescriptorContext context)
		{
			string[] values = new string[]
			{
				"0",
				"1",
				"2",
				"3",
				"4"
			};
			return new TypeConverter.StandardValuesCollection(values);
		}
	}
}

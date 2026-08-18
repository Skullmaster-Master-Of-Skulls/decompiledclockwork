using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x020000D7 RID: 215
	internal sealed class DataTableTypeConverter : ReferenceConverter
	{
		// Token: 0x06000E18 RID: 3608 RVA: 0x0007579C File Offset: 0x00074B9C
		public DataTableTypeConverter() : base(typeof(DataTable))
		{
		}

		// Token: 0x06000E19 RID: 3609 RVA: 0x000757BC File Offset: 0x00074BBC
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}
	}
}

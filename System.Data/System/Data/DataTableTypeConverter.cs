using System;
using System.ComponentModel;

namespace System.Data
{
	// Token: 0x020000A5 RID: 165
	internal sealed class DataTableTypeConverter : ReferenceConverter
	{
		// Token: 0x06000B0A RID: 2826 RVA: 0x0020B878 File Offset: 0x0020AC78
		public DataTableTypeConverter() : base(typeof(DataTable))
		{
		}

		// Token: 0x06000B0B RID: 2827 RVA: 0x0020B898 File Offset: 0x0020AC98
		public override bool GetPropertiesSupported(ITypeDescriptorContext context)
		{
			return false;
		}
	}
}

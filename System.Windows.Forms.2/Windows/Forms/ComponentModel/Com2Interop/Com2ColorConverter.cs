using System;
using System.Drawing;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x02000493 RID: 1171
	internal class Com2ColorConverter : Com2DataTypeToManagedDataTypeConverter
	{
		// Token: 0x17001344 RID: 4932
		// (get) Token: 0x06004E63 RID: 20067 RVA: 0x00142D43 File Offset: 0x00140F43
		public override Type ManagedType
		{
			get
			{
				return typeof(Color);
			}
		}

		// Token: 0x06004E64 RID: 20068 RVA: 0x00142D50 File Offset: 0x00140F50
		public override object ConvertNativeToManaged(object nativeValue, Com2PropertyDescriptor pd)
		{
			int oleColor = 0;
			if (nativeValue is uint)
			{
				oleColor = (int)((uint)nativeValue);
			}
			else if (nativeValue is int)
			{
				oleColor = (int)nativeValue;
			}
			return ColorTranslator.FromOle(oleColor);
		}

		// Token: 0x06004E65 RID: 20069 RVA: 0x00142D8C File Offset: 0x00140F8C
		public override object ConvertManagedToNative(object managedValue, Com2PropertyDescriptor pd, ref bool cancelSet)
		{
			cancelSet = false;
			if (managedValue == null)
			{
				managedValue = Color.Black;
			}
			if (managedValue is Color)
			{
				return ColorTranslator.ToOle((Color)managedValue);
			}
			return 0;
		}
	}
}

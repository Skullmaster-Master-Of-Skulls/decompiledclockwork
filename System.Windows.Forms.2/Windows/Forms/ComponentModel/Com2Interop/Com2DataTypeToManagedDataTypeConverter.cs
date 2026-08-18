using System;

namespace System.Windows.Forms.ComponentModel.Com2Interop
{
	// Token: 0x02000495 RID: 1173
	internal abstract class Com2DataTypeToManagedDataTypeConverter
	{
		// Token: 0x17001345 RID: 4933
		// (get) Token: 0x06004E6A RID: 20074 RVA: 0x00011A20 File Offset: 0x0000FC20
		public virtual bool AllowExpand
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17001346 RID: 4934
		// (get) Token: 0x06004E6B RID: 20075
		public abstract Type ManagedType { get; }

		// Token: 0x06004E6C RID: 20076
		public abstract object ConvertNativeToManaged(object nativeValue, Com2PropertyDescriptor pd);

		// Token: 0x06004E6D RID: 20077
		public abstract object ConvertManagedToNative(object managedValue, Com2PropertyDescriptor pd, ref bool cancelSet);
	}
}

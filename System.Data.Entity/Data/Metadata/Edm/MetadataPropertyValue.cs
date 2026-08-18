using System;
using System.Reflection;
using System.Runtime.CompilerServices;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001E6 RID: 486
	internal sealed class MetadataPropertyValue
	{
		// Token: 0x060020B2 RID: 8370 RVA: 0x00072500 File Offset: 0x00070700
		internal MetadataPropertyValue(PropertyInfo propertyInfo, MetadataItem item)
		{
			this._propertyInfo = propertyInfo;
			this._item = item;
		}

		// Token: 0x060020B3 RID: 8371 RVA: 0x00072516 File Offset: 0x00070716
		[MethodImpl(MethodImplOptions.NoInlining | MethodImplOptions.NoOptimization)]
		internal object GetValue()
		{
			return this._propertyInfo.GetValue(this._item, new object[0]);
		}

		// Token: 0x04000E57 RID: 3671
		private PropertyInfo _propertyInfo;

		// Token: 0x04000E58 RID: 3672
		private MetadataItem _item;
	}
}

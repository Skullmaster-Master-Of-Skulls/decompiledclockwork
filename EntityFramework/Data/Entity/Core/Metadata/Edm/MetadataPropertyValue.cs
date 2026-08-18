using System;
using System.Reflection;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004F0 RID: 1264
	internal sealed class MetadataPropertyValue
	{
		// Token: 0x06002F0A RID: 12042 RVA: 0x000E0BE2 File Offset: 0x000DEDE2
		internal MetadataPropertyValue(PropertyInfo propertyInfo, MetadataItem item)
		{
			this._propertyInfo = propertyInfo;
			this._item = item;
		}

		// Token: 0x06002F0B RID: 12043 RVA: 0x000E0BF8 File Offset: 0x000DEDF8
		internal object GetValue()
		{
			return this._propertyInfo.GetValue(this._item, new object[0]);
		}

		// Token: 0x040011DA RID: 4570
		private readonly PropertyInfo _propertyInfo;

		// Token: 0x040011DB RID: 4571
		private readonly MetadataItem _item;
	}
}

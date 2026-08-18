using System;

namespace System.Data.Design
{
	// Token: 0x0200022C RID: 556
	[AttributeUsage(AttributeTargets.Property)]
	internal sealed class DataSourceXmlSubItemAttribute : DataSourceXmlSerializationAttribute
	{
		// Token: 0x060014A2 RID: 5282 RVA: 0x0007697C File Offset: 0x00074B7C
		internal DataSourceXmlSubItemAttribute()
		{
		}

		// Token: 0x060014A3 RID: 5283 RVA: 0x00076984 File Offset: 0x00074B84
		internal DataSourceXmlSubItemAttribute(Type itemType)
		{
			base.ItemType = itemType;
		}
	}
}

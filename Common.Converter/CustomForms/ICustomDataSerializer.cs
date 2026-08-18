using System;
using TechnoPro.Common.Public.Entities.CustomForms.Data;
using TechnoPro.Common.Public.Entities.CustomForms.Data.DataHolders;

namespace TechnoPro.Common.Converter.CustomForms
{
	// Token: 0x02000005 RID: 5
	public interface ICustomDataSerializer<T> where T : CustomDataHolder
	{
		// Token: 0x06000011 RID: 17
		CustomDataSerialized Serialize(T dataObj);

		// Token: 0x06000012 RID: 18
		T DeSerialize(CustomDataSerialized serializedData);

		// Token: 0x06000013 RID: 19
		bool IsValueEmptyForStorage(T dataObj);
	}
}

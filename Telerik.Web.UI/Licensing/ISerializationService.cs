using System;

namespace Telerik.Licensing
{
	// Token: 0x02000416 RID: 1046
	internal interface ISerializationService
	{
		// Token: 0x060025E0 RID: 9696
		string Serialize<T>(T obj);

		// Token: 0x060025E1 RID: 9697
		T Deserialize<T>(string serializedObj);

		// Token: 0x060025E2 RID: 9698
		string SerializeToJson<T>(T obj);

		// Token: 0x060025E3 RID: 9699
		T DeserializeFromJson<T>(string serializedObj);
	}
}

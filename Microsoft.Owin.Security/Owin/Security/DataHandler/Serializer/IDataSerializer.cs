using System;

namespace Microsoft.Owin.Security.DataHandler.Serializer
{
	// Token: 0x02000011 RID: 17
	public interface IDataSerializer<TModel>
	{
		// Token: 0x06000026 RID: 38
		byte[] Serialize(TModel model);

		// Token: 0x06000027 RID: 39
		TModel Deserialize(byte[] data);
	}
}

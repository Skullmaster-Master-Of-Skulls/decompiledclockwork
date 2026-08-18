using System;

namespace System.Web.UI
{
	// Token: 0x020002B5 RID: 693
	public interface IStateFormatter
	{
		// Token: 0x06001FC8 RID: 8136
		object Deserialize(string serializedState);

		// Token: 0x06001FC9 RID: 8137
		string Serialize(object state);
	}
}

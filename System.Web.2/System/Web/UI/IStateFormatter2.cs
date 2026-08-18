using System;
using System.Web.Security.Cryptography;

namespace System.Web.UI
{
	// Token: 0x02000235 RID: 565
	internal interface IStateFormatter2 : IStateFormatter
	{
		// Token: 0x06001AA7 RID: 6823
		object Deserialize(string serializedState, Purpose purpose);

		// Token: 0x06001AA8 RID: 6824
		string Serialize(object state, Purpose purpose);
	}
}

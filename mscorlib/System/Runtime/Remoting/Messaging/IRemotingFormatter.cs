using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000704 RID: 1796
	[ComVisible(true)]
	public interface IRemotingFormatter : IFormatter
	{
		// Token: 0x06003FED RID: 16365
		object Deserialize(Stream serializationStream, HeaderHandler handler);

		// Token: 0x06003FEE RID: 16366
		void Serialize(Stream serializationStream, object graph, Header[] headers);
	}
}

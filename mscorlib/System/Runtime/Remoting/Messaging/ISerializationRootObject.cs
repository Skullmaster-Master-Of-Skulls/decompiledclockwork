using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200071B RID: 1819
	internal interface ISerializationRootObject
	{
		// Token: 0x06004102 RID: 16642
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
		void RootSetObjectData(SerializationInfo info, StreamingContext ctx);
	}
}

using System;
using System.Runtime.Remoting.Proxies;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200076D RID: 1901
	internal class RemotingSurrogate : ISerializationSurrogate
	{
		// Token: 0x060043E2 RID: 17378 RVA: 0x000E80D4 File Offset: 0x000E70D4
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual void GetObjectData(object obj, SerializationInfo info, StreamingContext context)
		{
			if (obj == null)
			{
				throw new ArgumentNullException("obj");
			}
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			if (RemotingServices.IsTransparentProxy(obj))
			{
				RealProxy realProxy = RemotingServices.GetRealProxy(obj);
				realProxy.GetObjectData(info, context);
				return;
			}
			RemotingServices.GetObjectData(obj, info, context);
		}

		// Token: 0x060043E3 RID: 17379 RVA: 0x000E811D File Offset: 0x000E711D
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_PopulateData"));
		}
	}
}

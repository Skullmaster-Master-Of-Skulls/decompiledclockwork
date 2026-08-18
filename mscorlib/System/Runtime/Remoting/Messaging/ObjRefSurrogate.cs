using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x0200076E RID: 1902
	internal class ObjRefSurrogate : ISerializationSurrogate
	{
		// Token: 0x060043E5 RID: 17381 RVA: 0x000E8136 File Offset: 0x000E7136
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
			((ObjRef)obj).GetObjectData(info, context);
			info.AddValue("fIsMarshalled", 0);
		}

		// Token: 0x060043E6 RID: 17382 RVA: 0x000E816D File Offset: 0x000E716D
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public virtual object SetObjectData(object obj, SerializationInfo info, StreamingContext context, ISurrogateSelector selector)
		{
			throw new NotSupportedException(Environment.GetResourceString("NotSupported_PopulateData"));
		}
	}
}

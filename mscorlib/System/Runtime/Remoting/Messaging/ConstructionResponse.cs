using System;
using System.Collections;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Activation;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Runtime.Remoting.Messaging
{
	// Token: 0x02000721 RID: 1825
	[CLSCompliant(false)]
	[ComVisible(true)]
	[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[SecurityPermission(SecurityAction.InheritanceDemand, Flags = SecurityPermissionFlag.Infrastructure)]
	[Serializable]
	public class ConstructionResponse : MethodResponse, IConstructionReturnMessage, IMethodReturnMessage, IMethodMessage, IMessage
	{
		// Token: 0x0600416E RID: 16750 RVA: 0x000DEE9C File Offset: 0x000DDE9C
		public ConstructionResponse(Header[] h, IMethodCallMessage mcm) : base(h, mcm)
		{
		}

		// Token: 0x0600416F RID: 16751 RVA: 0x000DEEA6 File Offset: 0x000DDEA6
		internal ConstructionResponse(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000B5E RID: 2910
		// (get) Token: 0x06004170 RID: 16752 RVA: 0x000DEEB0 File Offset: 0x000DDEB0
		public override IDictionary Properties
		{
			get
			{
				IDictionary externalProperties;
				lock (this)
				{
					if (this.InternalProperties == null)
					{
						this.InternalProperties = new Hashtable();
					}
					if (this.ExternalProperties == null)
					{
						this.ExternalProperties = new CRMDictionary(this, this.InternalProperties);
					}
					externalProperties = this.ExternalProperties;
				}
				return externalProperties;
			}
		}
	}
}

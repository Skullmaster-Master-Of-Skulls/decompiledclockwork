using System;
using System.Runtime.InteropServices;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Security.Principal
{
	// Token: 0x02000949 RID: 2377
	[ComVisible(false)]
	[Serializable]
	public sealed class IdentityNotMappedException : SystemException
	{
		// Token: 0x060055CA RID: 21962 RVA: 0x001375E0 File Offset: 0x001365E0
		public IdentityNotMappedException() : base(Environment.GetResourceString("IdentityReference_IdentityNotMapped"))
		{
		}

		// Token: 0x060055CB RID: 21963 RVA: 0x001375F2 File Offset: 0x001365F2
		public IdentityNotMappedException(string message) : base(message)
		{
		}

		// Token: 0x060055CC RID: 21964 RVA: 0x001375FB File Offset: 0x001365FB
		public IdentityNotMappedException(string message, Exception inner) : base(message, inner)
		{
		}

		// Token: 0x060055CD RID: 21965 RVA: 0x00137605 File Offset: 0x00136605
		internal IdentityNotMappedException(string message, IdentityReferenceCollection unmappedIdentities) : this(message)
		{
			this.unmappedIdentities = unmappedIdentities;
		}

		// Token: 0x060055CE RID: 21966 RVA: 0x00137615 File Offset: 0x00136615
		internal IdentityNotMappedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060055CF RID: 21967 RVA: 0x0013761F File Offset: 0x0013661F
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x17000EE3 RID: 3811
		// (get) Token: 0x060055D0 RID: 21968 RVA: 0x00137629 File Offset: 0x00136629
		public IdentityReferenceCollection UnmappedIdentities
		{
			get
			{
				if (this.unmappedIdentities == null)
				{
					this.unmappedIdentities = new IdentityReferenceCollection();
				}
				return this.unmappedIdentities;
			}
		}

		// Token: 0x04002CDA RID: 11482
		private IdentityReferenceCollection unmappedIdentities;
	}
}

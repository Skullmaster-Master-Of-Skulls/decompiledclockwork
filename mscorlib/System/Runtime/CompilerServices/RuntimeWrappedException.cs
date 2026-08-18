using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Runtime.CompilerServices
{
	// Token: 0x0200060B RID: 1547
	[Serializable]
	public sealed class RuntimeWrappedException : Exception
	{
		// Token: 0x0600380C RID: 14348 RVA: 0x000BBE11 File Offset: 0x000BAE11
		private RuntimeWrappedException(object thrownObject) : base(Environment.GetResourceString("RuntimeWrappedException"))
		{
			base.SetErrorCode(-2146233026);
			this.m_wrappedException = thrownObject;
		}

		// Token: 0x17000973 RID: 2419
		// (get) Token: 0x0600380D RID: 14349 RVA: 0x000BBE35 File Offset: 0x000BAE35
		public object WrappedException
		{
			get
			{
				return this.m_wrappedException;
			}
		}

		// Token: 0x0600380E RID: 14350 RVA: 0x000BBE3D File Offset: 0x000BAE3D
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("WrappedException", this.m_wrappedException, typeof(object));
		}

		// Token: 0x0600380F RID: 14351 RVA: 0x000BBE70 File Offset: 0x000BAE70
		internal RuntimeWrappedException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.m_wrappedException = info.GetValue("WrappedException", typeof(object));
		}

		// Token: 0x04001D0A RID: 7434
		private object m_wrappedException;
	}
}

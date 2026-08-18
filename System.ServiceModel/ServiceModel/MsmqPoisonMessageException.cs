using System;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;

namespace System.ServiceModel
{
	// Token: 0x020000B0 RID: 176
	[Serializable]
	public class MsmqPoisonMessageException : PoisonMessageException
	{
		// Token: 0x060002FB RID: 763 RVA: 0x00011DAF File Offset: 0x0000FFAF
		public MsmqPoisonMessageException()
		{
		}

		// Token: 0x060002FC RID: 764 RVA: 0x00011DB7 File Offset: 0x0000FFB7
		public MsmqPoisonMessageException(string message) : base(message)
		{
		}

		// Token: 0x060002FD RID: 765 RVA: 0x00011DC0 File Offset: 0x0000FFC0
		public MsmqPoisonMessageException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060002FE RID: 766 RVA: 0x00011DCA File Offset: 0x0000FFCA
		public MsmqPoisonMessageException(long messageLookupId) : this(messageLookupId, null)
		{
		}

		// Token: 0x060002FF RID: 767 RVA: 0x00011DD4 File Offset: 0x0000FFD4
		public MsmqPoisonMessageException(long messageLookupId, Exception innerException) : base(SR.GetString("MsmqPoisonMessage"), innerException)
		{
			this.messageLookupId = messageLookupId;
		}

		// Token: 0x170000B3 RID: 179
		// (get) Token: 0x06000300 RID: 768 RVA: 0x00011DEE File Offset: 0x0000FFEE
		public long MessageLookupId
		{
			get
			{
				return this.messageLookupId;
			}
		}

		// Token: 0x06000301 RID: 769 RVA: 0x00011DF6 File Offset: 0x0000FFF6
		protected MsmqPoisonMessageException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.messageLookupId = (long)info.GetValue("messageLookupId", typeof(long));
		}

		// Token: 0x06000302 RID: 770 RVA: 0x00011E20 File Offset: 0x00010020
		[SecurityCritical]
		[SecurityPermission(SecurityAction.LinkDemand, SerializationFormatter = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("messageLookupId", this.messageLookupId);
		}

		// Token: 0x04000957 RID: 2391
		private long messageLookupId;
	}
}

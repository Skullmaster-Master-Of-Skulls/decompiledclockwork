using System;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;

namespace System.ServiceModel.Security
{
	// Token: 0x020002D9 RID: 729
	[__DynamicallyInvokable]
	[Serializable]
	public class MessageSecurityException : CommunicationException
	{
		// Token: 0x060017D6 RID: 6102 RVA: 0x0005AE5C File Offset: 0x0005905C
		public MessageSecurityException()
		{
		}

		// Token: 0x060017D7 RID: 6103 RVA: 0x0005AE64 File Offset: 0x00059064
		[__DynamicallyInvokable]
		public MessageSecurityException(string message) : base(message)
		{
		}

		// Token: 0x060017D8 RID: 6104 RVA: 0x0005AE6D File Offset: 0x0005906D
		[__DynamicallyInvokable]
		public MessageSecurityException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060017D9 RID: 6105 RVA: 0x0005AE77 File Offset: 0x00059077
		protected MessageSecurityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x060017DA RID: 6106 RVA: 0x0005AE81 File Offset: 0x00059081
		internal MessageSecurityException(string message, Exception innerException, MessageFault fault) : base(message, innerException)
		{
			this.fault = fault;
		}

		// Token: 0x060017DB RID: 6107 RVA: 0x0005AE92 File Offset: 0x00059092
		internal MessageSecurityException(string message, bool isReplay) : base(message)
		{
			this.isReplay = isReplay;
		}

		// Token: 0x17000573 RID: 1395
		// (get) Token: 0x060017DC RID: 6108 RVA: 0x0005AEA2 File Offset: 0x000590A2
		internal bool ReplayDetected
		{
			get
			{
				return this.isReplay;
			}
		}

		// Token: 0x17000574 RID: 1396
		// (get) Token: 0x060017DD RID: 6109 RVA: 0x0005AEAA File Offset: 0x000590AA
		internal MessageFault Fault
		{
			get
			{
				return this.fault;
			}
		}

		// Token: 0x04001C3D RID: 7229
		private MessageFault fault;

		// Token: 0x04001C3E RID: 7230
		private bool isReplay;
	}
}

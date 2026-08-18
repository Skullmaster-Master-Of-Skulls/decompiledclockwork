using System;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;

namespace System.ServiceModel
{
	// Token: 0x0200010C RID: 268
	[__DynamicallyInvokable]
	[Serializable]
	public class MessageHeaderException : ProtocolException
	{
		// Token: 0x06000624 RID: 1572 RVA: 0x0001B3D7 File Offset: 0x000195D7
		[__DynamicallyInvokable]
		public MessageHeaderException(string message) : this(message, null, null)
		{
		}

		// Token: 0x06000625 RID: 1573 RVA: 0x0001B3E2 File Offset: 0x000195E2
		[__DynamicallyInvokable]
		public MessageHeaderException(string message, bool isDuplicate) : this(message, null, null)
		{
		}

		// Token: 0x06000626 RID: 1574 RVA: 0x0001B3ED File Offset: 0x000195ED
		[__DynamicallyInvokable]
		public MessageHeaderException(string message, Exception innerException) : this(message, null, null, innerException)
		{
		}

		// Token: 0x06000627 RID: 1575 RVA: 0x0001B3F9 File Offset: 0x000195F9
		[__DynamicallyInvokable]
		public MessageHeaderException(string message, string headerName, string ns) : this(message, headerName, ns, null)
		{
		}

		// Token: 0x06000628 RID: 1576 RVA: 0x0001B405 File Offset: 0x00019605
		[__DynamicallyInvokable]
		public MessageHeaderException(string message, string headerName, string ns, bool isDuplicate) : this(message, headerName, ns, isDuplicate, null)
		{
		}

		// Token: 0x06000629 RID: 1577 RVA: 0x0001B413 File Offset: 0x00019613
		[__DynamicallyInvokable]
		public MessageHeaderException(string message, string headerName, string ns, Exception innerException) : this(message, headerName, ns, false, innerException)
		{
		}

		// Token: 0x0600062A RID: 1578 RVA: 0x0001B421 File Offset: 0x00019621
		[__DynamicallyInvokable]
		public MessageHeaderException(string message, string headerName, string ns, bool isDuplicate, Exception innerException) : base(message, innerException)
		{
			this.headerName = headerName;
			this.headerNamespace = ns;
			this.isDuplicate = isDuplicate;
		}

		// Token: 0x17000185 RID: 389
		// (get) Token: 0x0600062B RID: 1579 RVA: 0x0001B442 File Offset: 0x00019642
		[__DynamicallyInvokable]
		public string HeaderName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.headerName;
			}
		}

		// Token: 0x17000186 RID: 390
		// (get) Token: 0x0600062C RID: 1580 RVA: 0x0001B44A File Offset: 0x0001964A
		[__DynamicallyInvokable]
		public string HeaderNamespace
		{
			[__DynamicallyInvokable]
			get
			{
				return this.headerNamespace;
			}
		}

		// Token: 0x17000187 RID: 391
		// (get) Token: 0x0600062D RID: 1581 RVA: 0x0001B452 File Offset: 0x00019652
		[__DynamicallyInvokable]
		public bool IsDuplicate
		{
			[__DynamicallyInvokable]
			get
			{
				return this.isDuplicate;
			}
		}

		// Token: 0x0600062E RID: 1582 RVA: 0x0001B45C File Offset: 0x0001965C
		internal Message ProvideFault(MessageVersion messageVersion)
		{
			WSAddressing10ProblemHeaderQNameFault wsaddressing10ProblemHeaderQNameFault = new WSAddressing10ProblemHeaderQNameFault(this);
			Message message = System.ServiceModel.Channels.Message.CreateMessage(messageVersion, wsaddressing10ProblemHeaderQNameFault, AddressingVersion.WSAddressing10.FaultAction);
			wsaddressing10ProblemHeaderQNameFault.AddHeaders(message.Headers);
			return message;
		}

		// Token: 0x0600062F RID: 1583 RVA: 0x0001B48F File Offset: 0x0001968F
		public MessageHeaderException()
		{
		}

		// Token: 0x06000630 RID: 1584 RVA: 0x0001B497 File Offset: 0x00019697
		protected MessageHeaderException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x04000A64 RID: 2660
		[NonSerialized]
		private string headerName;

		// Token: 0x04000A65 RID: 2661
		[NonSerialized]
		private string headerNamespace;

		// Token: 0x04000A66 RID: 2662
		[NonSerialized]
		private bool isDuplicate;
	}
}

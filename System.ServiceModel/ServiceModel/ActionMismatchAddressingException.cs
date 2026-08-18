using System;
using System.Runtime.Serialization;
using System.ServiceModel.Channels;

namespace System.ServiceModel
{
	// Token: 0x02000025 RID: 37
	[Serializable]
	internal class ActionMismatchAddressingException : ProtocolException
	{
		// Token: 0x06000165 RID: 357 RVA: 0x00008925 File Offset: 0x00006B25
		public ActionMismatchAddressingException(string message, string soapActionHeader, string httpActionHeader) : base(message)
		{
			this.httpActionHeader = httpActionHeader;
			this.soapActionHeader = soapActionHeader;
		}

		// Token: 0x06000166 RID: 358 RVA: 0x0000893C File Offset: 0x00006B3C
		protected ActionMismatchAddressingException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x06000167 RID: 359 RVA: 0x00008946 File Offset: 0x00006B46
		public string HttpActionHeader
		{
			get
			{
				return this.httpActionHeader;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000168 RID: 360 RVA: 0x0000894E File Offset: 0x00006B4E
		public string SoapActionHeader
		{
			get
			{
				return this.soapActionHeader;
			}
		}

		// Token: 0x06000169 RID: 361 RVA: 0x00008958 File Offset: 0x00006B58
		internal Message ProvideFault(MessageVersion messageVersion)
		{
			WSAddressing10ProblemHeaderQNameFault wsaddressing10ProblemHeaderQNameFault = new WSAddressing10ProblemHeaderQNameFault(this);
			Message message = System.ServiceModel.Channels.Message.CreateMessage(messageVersion, wsaddressing10ProblemHeaderQNameFault, messageVersion.Addressing.FaultAction);
			wsaddressing10ProblemHeaderQNameFault.AddHeaders(message.Headers);
			return message;
		}

		// Token: 0x04000185 RID: 389
		private string httpActionHeader;

		// Token: 0x04000186 RID: 390
		private string soapActionHeader;
	}
}

using System;
using System.Runtime.Serialization;
using Renci.SshNet.Messages.Transport;

namespace Renci.SshNet.Common
{
	// Token: 0x02000102 RID: 258
	[Serializable]
	public class SshConnectionException : SshException
	{
		// Token: 0x170002BC RID: 700
		// (get) Token: 0x06000AF7 RID: 2807 RVA: 0x00024DE0 File Offset: 0x00022FE0
		// (set) Token: 0x06000AF8 RID: 2808 RVA: 0x00024DE8 File Offset: 0x00022FE8
		public DisconnectReason DisconnectReason { get; private set; }

		// Token: 0x06000AF9 RID: 2809 RVA: 0x0002417D File Offset: 0x0002237D
		public SshConnectionException()
		{
		}

		// Token: 0x06000AFA RID: 2810 RVA: 0x00024DF1 File Offset: 0x00022FF1
		public SshConnectionException(string message) : base(message)
		{
			this.DisconnectReason = DisconnectReason.None;
		}

		// Token: 0x06000AFB RID: 2811 RVA: 0x00024E01 File Offset: 0x00023001
		public SshConnectionException(string message, DisconnectReason disconnectReasonCode) : base(message)
		{
			this.DisconnectReason = disconnectReasonCode;
		}

		// Token: 0x06000AFC RID: 2812 RVA: 0x00024E11 File Offset: 0x00023011
		public SshConnectionException(string message, DisconnectReason disconnectReasonCode, Exception inner) : base(message, inner)
		{
			this.DisconnectReason = disconnectReasonCode;
		}

		// Token: 0x06000AFD RID: 2813 RVA: 0x00024198 File Offset: 0x00022398
		protected SshConnectionException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
		}
	}
}

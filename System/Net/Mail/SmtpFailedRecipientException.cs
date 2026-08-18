using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net.Mail
{
	// Token: 0x020006CD RID: 1741
	[Serializable]
	public class SmtpFailedRecipientException : SmtpException, ISerializable
	{
		// Token: 0x060035D2 RID: 13778 RVA: 0x000E5C04 File Offset: 0x000E4C04
		public SmtpFailedRecipientException()
		{
		}

		// Token: 0x060035D3 RID: 13779 RVA: 0x000E5C0C File Offset: 0x000E4C0C
		public SmtpFailedRecipientException(string message) : base(message)
		{
		}

		// Token: 0x060035D4 RID: 13780 RVA: 0x000E5C15 File Offset: 0x000E4C15
		public SmtpFailedRecipientException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060035D5 RID: 13781 RVA: 0x000E5C1F File Offset: 0x000E4C1F
		protected SmtpFailedRecipientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.failedRecipient = info.GetString("failedRecipient");
		}

		// Token: 0x060035D6 RID: 13782 RVA: 0x000E5C3A File Offset: 0x000E4C3A
		public SmtpFailedRecipientException(SmtpStatusCode statusCode, string failedRecipient) : base(statusCode)
		{
			this.failedRecipient = failedRecipient;
		}

		// Token: 0x060035D7 RID: 13783 RVA: 0x000E5C4A File Offset: 0x000E4C4A
		public SmtpFailedRecipientException(SmtpStatusCode statusCode, string failedRecipient, string serverResponse) : base(statusCode, serverResponse, true)
		{
			this.failedRecipient = failedRecipient;
		}

		// Token: 0x060035D8 RID: 13784 RVA: 0x000E5C5C File Offset: 0x000E4C5C
		public SmtpFailedRecipientException(string message, string failedRecipient, Exception innerException) : base(message, innerException)
		{
			this.failedRecipient = failedRecipient;
		}

		// Token: 0x17000C7F RID: 3199
		// (get) Token: 0x060035D9 RID: 13785 RVA: 0x000E5C6D File Offset: 0x000E4C6D
		public string FailedRecipient
		{
			get
			{
				return this.failedRecipient;
			}
		}

		// Token: 0x060035DA RID: 13786 RVA: 0x000E5C75 File Offset: 0x000E4C75
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x060035DB RID: 13787 RVA: 0x000E5C7F File Offset: 0x000E4C7F
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
			serializationInfo.AddValue("failedRecipient", this.failedRecipient, typeof(string));
		}

		// Token: 0x04003119 RID: 12569
		private string failedRecipient;

		// Token: 0x0400311A RID: 12570
		internal bool fatal;
	}
}

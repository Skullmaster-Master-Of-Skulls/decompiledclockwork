using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net.Mail
{
	// Token: 0x0200028B RID: 651
	[Serializable]
	public class SmtpFailedRecipientException : SmtpException, ISerializable
	{
		// Token: 0x06001861 RID: 6241 RVA: 0x0007C213 File Offset: 0x0007A413
		public SmtpFailedRecipientException()
		{
		}

		// Token: 0x06001862 RID: 6242 RVA: 0x0007C21B File Offset: 0x0007A41B
		public SmtpFailedRecipientException(string message) : base(message)
		{
		}

		// Token: 0x06001863 RID: 6243 RVA: 0x0007C224 File Offset: 0x0007A424
		public SmtpFailedRecipientException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x06001864 RID: 6244 RVA: 0x0007C22E File Offset: 0x0007A42E
		protected SmtpFailedRecipientException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.failedRecipient = info.GetString("failedRecipient");
		}

		// Token: 0x06001865 RID: 6245 RVA: 0x0007C249 File Offset: 0x0007A449
		public SmtpFailedRecipientException(SmtpStatusCode statusCode, string failedRecipient) : base(statusCode)
		{
			this.failedRecipient = failedRecipient;
		}

		// Token: 0x06001866 RID: 6246 RVA: 0x0007C259 File Offset: 0x0007A459
		public SmtpFailedRecipientException(SmtpStatusCode statusCode, string failedRecipient, string serverResponse) : base(statusCode, serverResponse, true)
		{
			this.failedRecipient = failedRecipient;
		}

		// Token: 0x06001867 RID: 6247 RVA: 0x0007C26B File Offset: 0x0007A46B
		public SmtpFailedRecipientException(string message, string failedRecipient, Exception innerException) : base(message, innerException)
		{
			this.failedRecipient = failedRecipient;
		}

		// Token: 0x17000510 RID: 1296
		// (get) Token: 0x06001868 RID: 6248 RVA: 0x0007C27C File Offset: 0x0007A47C
		public string FailedRecipient
		{
			get
			{
				return this.failedRecipient;
			}
		}

		// Token: 0x06001869 RID: 6249 RVA: 0x0007C284 File Offset: 0x0007A484
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x0600186A RID: 6250 RVA: 0x0007C28E File Offset: 0x0007A48E
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
			serializationInfo.AddValue("failedRecipient", this.failedRecipient, typeof(string));
		}

		// Token: 0x0400185D RID: 6237
		private string failedRecipient;

		// Token: 0x0400185E RID: 6238
		internal bool fatal;
	}
}

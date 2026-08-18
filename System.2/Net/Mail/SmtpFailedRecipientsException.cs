using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net.Mail
{
	// Token: 0x0200028C RID: 652
	[Serializable]
	public class SmtpFailedRecipientsException : SmtpFailedRecipientException, ISerializable
	{
		// Token: 0x0600186B RID: 6251 RVA: 0x0007C2B3 File Offset: 0x0007A4B3
		public SmtpFailedRecipientsException()
		{
			this.innerExceptions = new SmtpFailedRecipientException[0];
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x0007C2C7 File Offset: 0x0007A4C7
		public SmtpFailedRecipientsException(string message) : base(message)
		{
			this.innerExceptions = new SmtpFailedRecipientException[0];
		}

		// Token: 0x0600186D RID: 6253 RVA: 0x0007C2DC File Offset: 0x0007A4DC
		public SmtpFailedRecipientsException(string message, Exception innerException) : base(message, innerException)
		{
			SmtpFailedRecipientException ex = innerException as SmtpFailedRecipientException;
			SmtpFailedRecipientException[] array;
			if (ex != null)
			{
				(array = new SmtpFailedRecipientException[1])[0] = ex;
			}
			else
			{
				array = new SmtpFailedRecipientException[0];
			}
			this.innerExceptions = array;
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x0007C313 File Offset: 0x0007A513
		protected SmtpFailedRecipientsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.innerExceptions = (SmtpFailedRecipientException[])info.GetValue("innerExceptions", typeof(SmtpFailedRecipientException[]));
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x0007C340 File Offset: 0x0007A540
		public SmtpFailedRecipientsException(string message, SmtpFailedRecipientException[] innerExceptions) : base(message, (innerExceptions != null && innerExceptions.Length != 0) ? innerExceptions[0].FailedRecipient : null, (innerExceptions != null && innerExceptions.Length != 0) ? innerExceptions[0] : null)
		{
			if (innerExceptions == null)
			{
				throw new ArgumentNullException("innerExceptions");
			}
			this.innerExceptions = ((innerExceptions == null) ? new SmtpFailedRecipientException[0] : innerExceptions);
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x0007C394 File Offset: 0x0007A594
		internal SmtpFailedRecipientsException(ArrayList innerExceptions, bool allFailed) : base(allFailed ? SR.GetString("SmtpAllRecipientsFailed") : SR.GetString("SmtpRecipientFailed"), (innerExceptions != null && innerExceptions.Count > 0) ? ((SmtpFailedRecipientException)innerExceptions[0]).FailedRecipient : null, (innerExceptions != null && innerExceptions.Count > 0) ? ((SmtpFailedRecipientException)innerExceptions[0]) : null)
		{
			if (innerExceptions == null)
			{
				throw new ArgumentNullException("innerExceptions");
			}
			this.innerExceptions = new SmtpFailedRecipientException[innerExceptions.Count];
			int num = 0;
			foreach (object obj in innerExceptions)
			{
				SmtpFailedRecipientException ex = (SmtpFailedRecipientException)obj;
				this.innerExceptions[num++] = ex;
			}
		}

		// Token: 0x17000511 RID: 1297
		// (get) Token: 0x06001871 RID: 6257 RVA: 0x0007C46C File Offset: 0x0007A66C
		public SmtpFailedRecipientException[] InnerExceptions
		{
			get
			{
				return this.innerExceptions;
			}
		}

		// Token: 0x06001872 RID: 6258 RVA: 0x0007C474 File Offset: 0x0007A674
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x06001873 RID: 6259 RVA: 0x0007C47E File Offset: 0x0007A67E
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
			serializationInfo.AddValue("innerExceptions", this.innerExceptions, typeof(SmtpFailedRecipientException[]));
		}

		// Token: 0x0400185F RID: 6239
		private SmtpFailedRecipientException[] innerExceptions;
	}
}

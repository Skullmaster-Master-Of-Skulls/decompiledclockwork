using System;
using System.Collections;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net.Mail
{
	// Token: 0x020006CE RID: 1742
	[Serializable]
	public class SmtpFailedRecipientsException : SmtpFailedRecipientException, ISerializable
	{
		// Token: 0x060035DC RID: 13788 RVA: 0x000E5CA4 File Offset: 0x000E4CA4
		public SmtpFailedRecipientsException()
		{
			this.innerExceptions = new SmtpFailedRecipientException[0];
		}

		// Token: 0x060035DD RID: 13789 RVA: 0x000E5CB8 File Offset: 0x000E4CB8
		public SmtpFailedRecipientsException(string message) : base(message)
		{
			this.innerExceptions = new SmtpFailedRecipientException[0];
		}

		// Token: 0x060035DE RID: 13790 RVA: 0x000E5CD0 File Offset: 0x000E4CD0
		public SmtpFailedRecipientsException(string message, Exception innerException) : base(message, innerException)
		{
			SmtpFailedRecipientException ex = innerException as SmtpFailedRecipientException;
			this.innerExceptions = ((ex == null) ? new SmtpFailedRecipientException[0] : new SmtpFailedRecipientException[]
			{
				ex
			});
		}

		// Token: 0x060035DF RID: 13791 RVA: 0x000E5D09 File Offset: 0x000E4D09
		protected SmtpFailedRecipientsException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.innerExceptions = (SmtpFailedRecipientException[])info.GetValue("innerExceptions", typeof(SmtpFailedRecipientException[]));
		}

		// Token: 0x060035E0 RID: 13792 RVA: 0x000E5D34 File Offset: 0x000E4D34
		public SmtpFailedRecipientsException(string message, SmtpFailedRecipientException[] innerExceptions) : base(message, (innerExceptions != null && innerExceptions.Length > 0) ? innerExceptions[0].FailedRecipient : null, (innerExceptions != null && innerExceptions.Length > 0) ? innerExceptions[0] : null)
		{
			if (innerExceptions == null)
			{
				throw new ArgumentNullException("innerExceptions");
			}
			this.innerExceptions = ((innerExceptions == null) ? new SmtpFailedRecipientException[0] : innerExceptions);
		}

		// Token: 0x060035E1 RID: 13793 RVA: 0x000E5D8C File Offset: 0x000E4D8C
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

		// Token: 0x17000C80 RID: 3200
		// (get) Token: 0x060035E2 RID: 13794 RVA: 0x000E5E64 File Offset: 0x000E4E64
		public SmtpFailedRecipientException[] InnerExceptions
		{
			get
			{
				return this.innerExceptions;
			}
		}

		// Token: 0x060035E3 RID: 13795 RVA: 0x000E5E6C File Offset: 0x000E4E6C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x060035E4 RID: 13796 RVA: 0x000E5E76 File Offset: 0x000E4E76
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
			serializationInfo.AddValue("innerExceptions", this.innerExceptions, typeof(SmtpFailedRecipientException[]));
		}

		// Token: 0x0400311B RID: 12571
		private SmtpFailedRecipientException[] innerExceptions;
	}
}

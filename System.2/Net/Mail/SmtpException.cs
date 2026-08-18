using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net.Mail
{
	// Token: 0x0200028A RID: 650
	[Serializable]
	public class SmtpException : Exception, ISerializable
	{
		// Token: 0x06001853 RID: 6227 RVA: 0x0007BEED File Offset: 0x0007A0ED
		private static string GetMessageForStatus(SmtpStatusCode statusCode, string serverResponse)
		{
			return SmtpException.GetMessageForStatus(statusCode) + " " + SR.GetString("MailServerResponse", new object[]
			{
				serverResponse
			});
		}

		// Token: 0x06001854 RID: 6228 RVA: 0x0007BF14 File Offset: 0x0007A114
		private static string GetMessageForStatus(SmtpStatusCode statusCode)
		{
			if (statusCode <= SmtpStatusCode.UserNotLocalWillForward)
			{
				if (statusCode <= SmtpStatusCode.ServiceReady)
				{
					if (statusCode == SmtpStatusCode.SystemStatus)
					{
						return SR.GetString("SmtpSystemStatus");
					}
					if (statusCode == SmtpStatusCode.HelpMessage)
					{
						return SR.GetString("SmtpHelpMessage");
					}
					if (statusCode == SmtpStatusCode.ServiceReady)
					{
						return SR.GetString("SmtpServiceReady");
					}
				}
				else
				{
					if (statusCode == SmtpStatusCode.ServiceClosingTransmissionChannel)
					{
						return SR.GetString("SmtpServiceClosingTransmissionChannel");
					}
					if (statusCode == SmtpStatusCode.Ok)
					{
						return SR.GetString("SmtpOK");
					}
					if (statusCode == SmtpStatusCode.UserNotLocalWillForward)
					{
						return SR.GetString("SmtpUserNotLocalWillForward");
					}
				}
			}
			else if (statusCode <= SmtpStatusCode.ClientNotPermitted)
			{
				if (statusCode == SmtpStatusCode.StartMailInput)
				{
					return SR.GetString("SmtpStartMailInput");
				}
				if (statusCode == SmtpStatusCode.ServiceNotAvailable)
				{
					return SR.GetString("SmtpServiceNotAvailable");
				}
				switch (statusCode)
				{
				case SmtpStatusCode.MailboxBusy:
					return SR.GetString("SmtpMailboxBusy");
				case SmtpStatusCode.LocalErrorInProcessing:
					return SR.GetString("SmtpLocalErrorInProcessing");
				case SmtpStatusCode.InsufficientStorage:
					return SR.GetString("SmtpInsufficientStorage");
				case SmtpStatusCode.ClientNotPermitted:
					return SR.GetString("SmtpClientNotPermitted");
				}
			}
			else
			{
				switch (statusCode)
				{
				case SmtpStatusCode.CommandUnrecognized:
					break;
				case SmtpStatusCode.SyntaxError:
					return SR.GetString("SmtpSyntaxError");
				case SmtpStatusCode.CommandNotImplemented:
					return SR.GetString("SmtpCommandNotImplemented");
				case SmtpStatusCode.BadCommandSequence:
					return SR.GetString("SmtpBadCommandSequence");
				case SmtpStatusCode.CommandParameterNotImplemented:
					return SR.GetString("SmtpCommandParameterNotImplemented");
				default:
					if (statusCode == SmtpStatusCode.MustIssueStartTlsFirst)
					{
						return SR.GetString("SmtpMustIssueStartTlsFirst");
					}
					switch (statusCode)
					{
					case SmtpStatusCode.MailboxUnavailable:
						return SR.GetString("SmtpMailboxUnavailable");
					case SmtpStatusCode.UserNotLocalTryAlternatePath:
						return SR.GetString("SmtpUserNotLocalTryAlternatePath");
					case SmtpStatusCode.ExceededStorageAllocation:
						return SR.GetString("SmtpExceededStorageAllocation");
					case SmtpStatusCode.MailboxNameNotAllowed:
						return SR.GetString("SmtpMailboxNameNotAllowed");
					case SmtpStatusCode.TransactionFailed:
						return SR.GetString("SmtpTransactionFailed");
					}
					break;
				}
			}
			return SR.GetString("SmtpCommandUnrecognized");
		}

		// Token: 0x06001855 RID: 6229 RVA: 0x0007C104 File Offset: 0x0007A304
		public SmtpException(SmtpStatusCode statusCode) : base(SmtpException.GetMessageForStatus(statusCode))
		{
			this.statusCode = statusCode;
		}

		// Token: 0x06001856 RID: 6230 RVA: 0x0007C120 File Offset: 0x0007A320
		public SmtpException(SmtpStatusCode statusCode, string message) : base(message)
		{
			this.statusCode = statusCode;
		}

		// Token: 0x06001857 RID: 6231 RVA: 0x0007C137 File Offset: 0x0007A337
		public SmtpException() : this(SmtpStatusCode.GeneralFailure)
		{
		}

		// Token: 0x06001858 RID: 6232 RVA: 0x0007C140 File Offset: 0x0007A340
		public SmtpException(string message) : base(message)
		{
		}

		// Token: 0x06001859 RID: 6233 RVA: 0x0007C150 File Offset: 0x0007A350
		public SmtpException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x0600185A RID: 6234 RVA: 0x0007C161 File Offset: 0x0007A361
		protected SmtpException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
			this.statusCode = (SmtpStatusCode)serializationInfo.GetInt32("Status");
		}

		// Token: 0x0600185B RID: 6235 RVA: 0x0007C183 File Offset: 0x0007A383
		internal SmtpException(SmtpStatusCode statusCode, string serverMessage, bool serverResponse) : base(SmtpException.GetMessageForStatus(statusCode, serverMessage))
		{
			this.statusCode = statusCode;
		}

		// Token: 0x0600185C RID: 6236 RVA: 0x0007C1A0 File Offset: 0x0007A3A0
		internal SmtpException(string message, string serverResponse) : base(message + " " + SR.GetString("MailServerResponse", new object[]
		{
			serverResponse
		}))
		{
		}

		// Token: 0x0600185D RID: 6237 RVA: 0x0007C1CE File Offset: 0x0007A3CE
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x0600185E RID: 6238 RVA: 0x0007C1D8 File Offset: 0x0007A3D8
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
			serializationInfo.AddValue("Status", (int)this.statusCode, typeof(int));
		}

		// Token: 0x1700050F RID: 1295
		// (get) Token: 0x0600185F RID: 6239 RVA: 0x0007C202 File Offset: 0x0007A402
		// (set) Token: 0x06001860 RID: 6240 RVA: 0x0007C20A File Offset: 0x0007A40A
		public SmtpStatusCode StatusCode
		{
			get
			{
				return this.statusCode;
			}
			set
			{
				this.statusCode = value;
			}
		}

		// Token: 0x0400185C RID: 6236
		private SmtpStatusCode statusCode = SmtpStatusCode.GeneralFailure;
	}
}

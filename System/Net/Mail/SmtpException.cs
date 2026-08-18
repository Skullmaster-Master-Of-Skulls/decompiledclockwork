using System;
using System.Runtime.Serialization;
using System.Security.Permissions;

namespace System.Net.Mail
{
	// Token: 0x020006CC RID: 1740
	[Serializable]
	public class SmtpException : Exception, ISerializable
	{
		// Token: 0x060035C4 RID: 13764 RVA: 0x000E58C4 File Offset: 0x000E48C4
		private static string GetMessageForStatus(SmtpStatusCode statusCode, string serverResponse)
		{
			return SmtpException.GetMessageForStatus(statusCode) + " " + SR.GetString("MailServerResponse", new object[]
			{
				serverResponse
			});
		}

		// Token: 0x060035C5 RID: 13765 RVA: 0x000E58F8 File Offset: 0x000E48F8
		private static string GetMessageForStatus(SmtpStatusCode statusCode)
		{
			if (statusCode <= SmtpStatusCode.StartMailInput)
			{
				if (statusCode <= SmtpStatusCode.HelpMessage)
				{
					if (statusCode == SmtpStatusCode.SystemStatus)
					{
						return SR.GetString("SmtpSystemStatus");
					}
					if (statusCode == SmtpStatusCode.HelpMessage)
					{
						return SR.GetString("SmtpHelpMessage");
					}
				}
				else
				{
					switch (statusCode)
					{
					case SmtpStatusCode.ServiceReady:
						return SR.GetString("SmtpServiceReady");
					case SmtpStatusCode.ServiceClosingTransmissionChannel:
						return SR.GetString("SmtpServiceClosingTransmissionChannel");
					default:
						switch (statusCode)
						{
						case SmtpStatusCode.Ok:
							return SR.GetString("SmtpOK");
						case SmtpStatusCode.UserNotLocalWillForward:
							return SR.GetString("SmtpUserNotLocalWillForward");
						default:
							if (statusCode == SmtpStatusCode.StartMailInput)
							{
								return SR.GetString("SmtpStartMailInput");
							}
							break;
						}
						break;
					}
				}
			}
			else if (statusCode <= SmtpStatusCode.ClientNotPermitted)
			{
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

		// Token: 0x060035C6 RID: 13766 RVA: 0x000E5AE6 File Offset: 0x000E4AE6
		public SmtpException(SmtpStatusCode statusCode) : base(SmtpException.GetMessageForStatus(statusCode))
		{
			this.statusCode = statusCode;
		}

		// Token: 0x060035C7 RID: 13767 RVA: 0x000E5B02 File Offset: 0x000E4B02
		public SmtpException(SmtpStatusCode statusCode, string message) : base(message)
		{
			this.statusCode = statusCode;
		}

		// Token: 0x060035C8 RID: 13768 RVA: 0x000E5B19 File Offset: 0x000E4B19
		public SmtpException() : this(SmtpStatusCode.GeneralFailure)
		{
		}

		// Token: 0x060035C9 RID: 13769 RVA: 0x000E5B22 File Offset: 0x000E4B22
		public SmtpException(string message) : base(message)
		{
		}

		// Token: 0x060035CA RID: 13770 RVA: 0x000E5B32 File Offset: 0x000E4B32
		public SmtpException(string message, Exception innerException) : base(message, innerException)
		{
		}

		// Token: 0x060035CB RID: 13771 RVA: 0x000E5B43 File Offset: 0x000E4B43
		protected SmtpException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
			this.statusCode = (SmtpStatusCode)serializationInfo.GetInt32("Status");
		}

		// Token: 0x060035CC RID: 13772 RVA: 0x000E5B65 File Offset: 0x000E4B65
		internal SmtpException(SmtpStatusCode statusCode, string serverMessage, bool serverResponse) : base(SmtpException.GetMessageForStatus(statusCode, serverMessage))
		{
			this.statusCode = statusCode;
		}

		// Token: 0x060035CD RID: 13773 RVA: 0x000E5B84 File Offset: 0x000E4B84
		internal SmtpException(string message, string serverResponse) : base(message + " " + SR.GetString("MailServerResponse", new object[]
		{
			serverResponse
		}))
		{
		}

		// Token: 0x060035CE RID: 13774 RVA: 0x000E5BBF File Offset: 0x000E4BBF
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		void ISerializable.GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			this.GetObjectData(serializationInfo, streamingContext);
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x000E5BC9 File Offset: 0x000E4BC9
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo serializationInfo, StreamingContext streamingContext)
		{
			base.GetObjectData(serializationInfo, streamingContext);
			serializationInfo.AddValue("Status", (int)this.statusCode, typeof(int));
		}

		// Token: 0x17000C7E RID: 3198
		// (get) Token: 0x060035D0 RID: 13776 RVA: 0x000E5BF3 File Offset: 0x000E4BF3
		// (set) Token: 0x060035D1 RID: 13777 RVA: 0x000E5BFB File Offset: 0x000E4BFB
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

		// Token: 0x04003118 RID: 12568
		private SmtpStatusCode statusCode = SmtpStatusCode.GeneralFailure;
	}
}

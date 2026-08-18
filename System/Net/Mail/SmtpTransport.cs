using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace System.Net.Mail
{
	// Token: 0x020006DC RID: 1756
	internal class SmtpTransport
	{
		// Token: 0x06003621 RID: 13857 RVA: 0x000E7193 File Offset: 0x000E6193
		internal SmtpTransport(SmtpClient client) : this(client, SmtpAuthenticationManager.GetModules())
		{
		}

		// Token: 0x06003622 RID: 13858 RVA: 0x000E71A1 File Offset: 0x000E61A1
		internal SmtpTransport(SmtpClient client, ISmtpAuthenticationModule[] authenticationModules)
		{
			this.client = client;
			if (authenticationModules == null)
			{
				throw new ArgumentNullException("authenticationModules");
			}
			this.authenticationModules = authenticationModules;
		}

		// Token: 0x17000C88 RID: 3208
		// (get) Token: 0x06003623 RID: 13859 RVA: 0x000E71DB File Offset: 0x000E61DB
		// (set) Token: 0x06003624 RID: 13860 RVA: 0x000E71E3 File Offset: 0x000E61E3
		internal ICredentialsByHost Credentials
		{
			get
			{
				return this.credentials;
			}
			set
			{
				this.credentials = value;
			}
		}

		// Token: 0x17000C89 RID: 3209
		// (get) Token: 0x06003625 RID: 13861 RVA: 0x000E71EC File Offset: 0x000E61EC
		// (set) Token: 0x06003626 RID: 13862 RVA: 0x000E71F4 File Offset: 0x000E61F4
		internal bool IdentityRequired
		{
			get
			{
				return this.m_IdentityRequired;
			}
			set
			{
				this.m_IdentityRequired = value;
			}
		}

		// Token: 0x17000C8A RID: 3210
		// (get) Token: 0x06003627 RID: 13863 RVA: 0x000E71FD File Offset: 0x000E61FD
		internal bool IsConnected
		{
			get
			{
				return this.connection != null && this.connection.IsConnected;
			}
		}

		// Token: 0x17000C8B RID: 3211
		// (get) Token: 0x06003628 RID: 13864 RVA: 0x000E7214 File Offset: 0x000E6214
		// (set) Token: 0x06003629 RID: 13865 RVA: 0x000E721C File Offset: 0x000E621C
		internal int Timeout
		{
			get
			{
				return this.timeout;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.timeout = value;
			}
		}

		// Token: 0x17000C8C RID: 3212
		// (get) Token: 0x0600362A RID: 13866 RVA: 0x000E7234 File Offset: 0x000E6234
		// (set) Token: 0x0600362B RID: 13867 RVA: 0x000E723C File Offset: 0x000E623C
		internal bool EnableSsl
		{
			get
			{
				return this.enableSsl;
			}
			set
			{
				this.enableSsl = value;
			}
		}

		// Token: 0x17000C8D RID: 3213
		// (get) Token: 0x0600362C RID: 13868 RVA: 0x000E7245 File Offset: 0x000E6245
		internal X509CertificateCollection ClientCertificates
		{
			get
			{
				if (this.clientCertificates == null)
				{
					this.clientCertificates = new X509CertificateCollection();
				}
				return this.clientCertificates;
			}
		}

		// Token: 0x0600362D RID: 13869 RVA: 0x000E7260 File Offset: 0x000E6260
		internal void GetConnection(string host, int port)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			if (port < 0 || port > 65535)
			{
				throw new ArgumentOutOfRangeException("port");
			}
			this.connection = new SmtpConnection(this, this.client, this.credentials, this.authenticationModules);
			this.connection.Timeout = this.timeout;
			if (Logging.On)
			{
				Logging.Associate(Logging.Web, this, this.connection);
			}
			if (this.EnableSsl)
			{
				this.connection.EnableSsl = true;
				this.connection.ClientCertificates = this.ClientCertificates;
			}
			this.connection.GetConnection(host, port);
		}

		// Token: 0x0600362E RID: 13870 RVA: 0x000E730C File Offset: 0x000E630C
		internal IAsyncResult BeginGetConnection(string host, int port, ContextAwareResult outerResult, AsyncCallback callback, object state)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			if (port < 0 || port > 65535)
			{
				throw new ArgumentOutOfRangeException("port");
			}
			IAsyncResult result = null;
			try
			{
				this.connection = new SmtpConnection(this, this.client, this.credentials, this.authenticationModules);
				this.connection.Timeout = this.timeout;
				if (Logging.On)
				{
					Logging.Associate(Logging.Web, this, this.connection);
				}
				if (this.EnableSsl)
				{
					this.connection.EnableSsl = true;
					this.connection.ClientCertificates = this.ClientCertificates;
				}
				result = this.connection.BeginGetConnection(host, port, outerResult, callback, state);
			}
			catch (Exception innerException)
			{
				throw new SmtpException(SR.GetString("MailHostNotFound"), innerException);
			}
			catch
			{
				throw new SmtpException(SR.GetString("MailHostNotFound"), new Exception(SR.GetString("net_nonClsCompliantException")));
			}
			return result;
		}

		// Token: 0x0600362F RID: 13871 RVA: 0x000E7410 File Offset: 0x000E6410
		internal void EndGetConnection(IAsyncResult result)
		{
			this.connection.EndGetConnection(result);
		}

		// Token: 0x06003630 RID: 13872 RVA: 0x000E7420 File Offset: 0x000E6420
		internal IAsyncResult BeginSendMail(MailAddress sender, MailAddressCollection recipients, string deliveryNotify, AsyncCallback callback, object state)
		{
			if (sender == null)
			{
				throw new ArgumentNullException("sender");
			}
			if (recipients == null)
			{
				throw new ArgumentNullException("recipients");
			}
			SendMailAsyncResult sendMailAsyncResult = new SendMailAsyncResult(this.connection, sender.SmtpAddress, recipients, this.connection.DSNEnabled ? deliveryNotify : null, callback, state);
			sendMailAsyncResult.Send();
			return sendMailAsyncResult;
		}

		// Token: 0x06003631 RID: 13873 RVA: 0x000E7478 File Offset: 0x000E6478
		internal void ReleaseConnection()
		{
			if (this.connection != null)
			{
				this.connection.ReleaseConnection();
			}
		}

		// Token: 0x06003632 RID: 13874 RVA: 0x000E748D File Offset: 0x000E648D
		internal void Abort()
		{
			if (this.connection != null)
			{
				this.connection.Abort();
			}
		}

		// Token: 0x06003633 RID: 13875 RVA: 0x000E74A4 File Offset: 0x000E64A4
		internal MailWriter EndSendMail(IAsyncResult result)
		{
			return SendMailAsyncResult.End(result);
		}

		// Token: 0x06003634 RID: 13876 RVA: 0x000E74BC File Offset: 0x000E64BC
		internal MailWriter SendMail(MailAddress sender, MailAddressCollection recipients, string deliveryNotify, out SmtpFailedRecipientException exception)
		{
			if (sender == null)
			{
				throw new ArgumentNullException("sender");
			}
			if (recipients == null)
			{
				throw new ArgumentNullException("recipients");
			}
			MailCommand.Send(this.connection, SmtpCommands.Mail, sender.SmtpAddress);
			this.failedRecipientExceptions.Clear();
			exception = null;
			foreach (MailAddress mailAddress in recipients)
			{
				string serverResponse;
				if (!RecipientCommand.Send(this.connection, this.connection.DSNEnabled ? (mailAddress.SmtpAddress + deliveryNotify) : mailAddress.SmtpAddress, out serverResponse))
				{
					this.failedRecipientExceptions.Add(new SmtpFailedRecipientException(this.connection.Reader.StatusCode, mailAddress.SmtpAddress, serverResponse));
				}
			}
			if (this.failedRecipientExceptions.Count > 0)
			{
				if (this.failedRecipientExceptions.Count == 1)
				{
					exception = (SmtpFailedRecipientException)this.failedRecipientExceptions[0];
				}
				else
				{
					exception = new SmtpFailedRecipientsException(this.failedRecipientExceptions, this.failedRecipientExceptions.Count == recipients.Count);
				}
				if (this.failedRecipientExceptions.Count == recipients.Count)
				{
					exception.fatal = true;
					throw exception;
				}
			}
			DataCommand.Send(this.connection);
			return new MailWriter(this.connection.GetClosableStream());
		}

		// Token: 0x04003163 RID: 12643
		internal const int DefaultPort = 25;

		// Token: 0x04003164 RID: 12644
		private ISmtpAuthenticationModule[] authenticationModules;

		// Token: 0x04003165 RID: 12645
		private SmtpConnection connection;

		// Token: 0x04003166 RID: 12646
		private SmtpClient client;

		// Token: 0x04003167 RID: 12647
		private ICredentialsByHost credentials;

		// Token: 0x04003168 RID: 12648
		private int timeout = 100000;

		// Token: 0x04003169 RID: 12649
		private ArrayList failedRecipientExceptions = new ArrayList();

		// Token: 0x0400316A RID: 12650
		private bool m_IdentityRequired;

		// Token: 0x0400316B RID: 12651
		private bool enableSsl;

		// Token: 0x0400316C RID: 12652
		private X509CertificateCollection clientCertificates;
	}
}

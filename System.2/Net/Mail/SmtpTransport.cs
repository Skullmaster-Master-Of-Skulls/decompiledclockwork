using System;
using System.Collections;
using System.Security.Cryptography.X509Certificates;

namespace System.Net.Mail
{
	// Token: 0x02000298 RID: 664
	internal class SmtpTransport
	{
		// Token: 0x060018A9 RID: 6313 RVA: 0x0007D406 File Offset: 0x0007B606
		internal SmtpTransport(SmtpClient client) : this(client, SmtpAuthenticationManager.GetModules())
		{
		}

		// Token: 0x060018AA RID: 6314 RVA: 0x0007D414 File Offset: 0x0007B614
		internal SmtpTransport(SmtpClient client, ISmtpAuthenticationModule[] authenticationModules)
		{
			this.client = client;
			if (authenticationModules == null)
			{
				throw new ArgumentNullException("authenticationModules");
			}
			this.authenticationModules = authenticationModules;
		}

		// Token: 0x17000519 RID: 1305
		// (get) Token: 0x060018AB RID: 6315 RVA: 0x0007D44E File Offset: 0x0007B64E
		// (set) Token: 0x060018AC RID: 6316 RVA: 0x0007D456 File Offset: 0x0007B656
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

		// Token: 0x1700051A RID: 1306
		// (get) Token: 0x060018AD RID: 6317 RVA: 0x0007D45F File Offset: 0x0007B65F
		// (set) Token: 0x060018AE RID: 6318 RVA: 0x0007D467 File Offset: 0x0007B667
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

		// Token: 0x1700051B RID: 1307
		// (get) Token: 0x060018AF RID: 6319 RVA: 0x0007D470 File Offset: 0x0007B670
		internal bool IsConnected
		{
			get
			{
				return this.connection != null && this.connection.IsConnected;
			}
		}

		// Token: 0x1700051C RID: 1308
		// (get) Token: 0x060018B0 RID: 6320 RVA: 0x0007D487 File Offset: 0x0007B687
		// (set) Token: 0x060018B1 RID: 6321 RVA: 0x0007D48F File Offset: 0x0007B68F
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

		// Token: 0x1700051D RID: 1309
		// (get) Token: 0x060018B2 RID: 6322 RVA: 0x0007D4A7 File Offset: 0x0007B6A7
		// (set) Token: 0x060018B3 RID: 6323 RVA: 0x0007D4AF File Offset: 0x0007B6AF
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

		// Token: 0x1700051E RID: 1310
		// (get) Token: 0x060018B4 RID: 6324 RVA: 0x0007D4B8 File Offset: 0x0007B6B8
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

		// Token: 0x1700051F RID: 1311
		// (get) Token: 0x060018B5 RID: 6325 RVA: 0x0007D4D3 File Offset: 0x0007B6D3
		internal bool ServerSupportsEai
		{
			get
			{
				return this.connection != null && this.connection.ServerSupportsEai;
			}
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x0007D4EC File Offset: 0x0007B6EC
		private void UpdateServicePoint(ServicePoint servicePoint)
		{
			if (this.lastUsedServicePoint == null)
			{
				this.lastUsedServicePoint = servicePoint;
				return;
			}
			if (this.lastUsedServicePoint.Host != servicePoint.Host || this.lastUsedServicePoint.Port != servicePoint.Port)
			{
				ConnectionPoolManager.CleanupConnectionPool(servicePoint, "");
				this.lastUsedServicePoint = servicePoint;
			}
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x0007D548 File Offset: 0x0007B748
		internal void GetConnection(ServicePoint servicePoint)
		{
			this.UpdateServicePoint(servicePoint);
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
			this.connection.GetConnection(servicePoint);
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x0007D5D4 File Offset: 0x0007B7D4
		internal IAsyncResult BeginGetConnection(ServicePoint servicePoint, ContextAwareResult outerResult, AsyncCallback callback, object state)
		{
			IAsyncResult result = null;
			try
			{
				this.UpdateServicePoint(servicePoint);
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
				result = this.connection.BeginGetConnection(servicePoint, outerResult, callback, state);
			}
			catch (Exception innerException)
			{
				throw new SmtpException(SR.GetString("MailHostNotFound"), innerException);
			}
			return result;
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x0007D68C File Offset: 0x0007B88C
		internal void EndGetConnection(IAsyncResult result)
		{
			try
			{
				this.connection.EndGetConnection(result);
			}
			finally
			{
			}
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x0007D6B8 File Offset: 0x0007B8B8
		internal IAsyncResult BeginSendMail(MailAddress sender, MailAddressCollection recipients, string deliveryNotify, bool allowUnicode, AsyncCallback callback, object state)
		{
			if (sender == null)
			{
				throw new ArgumentNullException("sender");
			}
			if (recipients == null)
			{
				throw new ArgumentNullException("recipients");
			}
			SendMailAsyncResult sendMailAsyncResult = new SendMailAsyncResult(this.connection, sender, recipients, allowUnicode, this.connection.DSNEnabled ? deliveryNotify : null, callback, state);
			sendMailAsyncResult.Send();
			return sendMailAsyncResult;
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x0007D70D File Offset: 0x0007B90D
		internal void ReleaseConnection()
		{
			if (this.connection != null)
			{
				this.connection.ReleaseConnection();
			}
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x0007D722 File Offset: 0x0007B922
		internal void Abort()
		{
			if (this.connection != null)
			{
				this.connection.Abort();
			}
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x0007D737 File Offset: 0x0007B937
		internal MailWriter EndSendMail(IAsyncResult result)
		{
			return SendMailAsyncResult.End(result);
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x0007D740 File Offset: 0x0007B940
		internal MailWriter SendMail(MailAddress sender, MailAddressCollection recipients, string deliveryNotify, bool allowUnicode, out SmtpFailedRecipientException exception)
		{
			if (sender == null)
			{
				throw new ArgumentNullException("sender");
			}
			if (recipients == null)
			{
				throw new ArgumentNullException("recipients");
			}
			MailCommand.Send(this.connection, SmtpCommands.Mail, sender, allowUnicode);
			this.failedRecipientExceptions.Clear();
			exception = null;
			foreach (MailAddress mailAddress in recipients)
			{
				string smtpAddress = mailAddress.GetSmtpAddress(allowUnicode);
				string to = smtpAddress + (this.connection.DSNEnabled ? deliveryNotify : string.Empty);
				string serverResponse;
				if (!RecipientCommand.Send(this.connection, to, out serverResponse))
				{
					this.failedRecipientExceptions.Add(new SmtpFailedRecipientException(this.connection.Reader.StatusCode, smtpAddress, serverResponse));
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

		// Token: 0x060018BF RID: 6335 RVA: 0x0007D8A8 File Offset: 0x0007BAA8
		internal void CloseIdleConnections(ServicePoint servicePoint)
		{
			ConnectionPoolManager.CleanupConnectionPool(servicePoint, "");
		}

		// Token: 0x04001897 RID: 6295
		internal const int DefaultPort = 25;

		// Token: 0x04001898 RID: 6296
		private ISmtpAuthenticationModule[] authenticationModules;

		// Token: 0x04001899 RID: 6297
		private SmtpConnection connection;

		// Token: 0x0400189A RID: 6298
		private SmtpClient client;

		// Token: 0x0400189B RID: 6299
		private ICredentialsByHost credentials;

		// Token: 0x0400189C RID: 6300
		private int timeout = 100000;

		// Token: 0x0400189D RID: 6301
		private ArrayList failedRecipientExceptions = new ArrayList();

		// Token: 0x0400189E RID: 6302
		private bool m_IdentityRequired;

		// Token: 0x0400189F RID: 6303
		private bool enableSsl;

		// Token: 0x040018A0 RID: 6304
		private X509CertificateCollection clientCertificates;

		// Token: 0x040018A1 RID: 6305
		private ServicePoint lastUsedServicePoint;
	}
}

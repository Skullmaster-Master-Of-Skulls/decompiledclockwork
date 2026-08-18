using System;
using System.Collections;
using System.Collections.ObjectModel;
using System.IO;
using System.Net.Mime;

namespace System.Net.Mail
{
	// Token: 0x02000299 RID: 665
	internal class SendMailAsyncResult : LazyAsyncResult
	{
		// Token: 0x060018C0 RID: 6336 RVA: 0x0007D8B5 File Offset: 0x0007BAB5
		internal SendMailAsyncResult(SmtpConnection connection, MailAddress from, MailAddressCollection toCollection, bool allowUnicode, string deliveryNotify, AsyncCallback callback, object state) : base(null, state, callback)
		{
			this.toCollection = toCollection;
			this.connection = connection;
			this.from = from;
			this.deliveryNotify = deliveryNotify;
			this.allowUnicode = allowUnicode;
		}

		// Token: 0x060018C1 RID: 6337 RVA: 0x0007D8F2 File Offset: 0x0007BAF2
		internal void Send()
		{
			this.SendMailFrom();
		}

		// Token: 0x060018C2 RID: 6338 RVA: 0x0007D8FC File Offset: 0x0007BAFC
		internal static MailWriter End(IAsyncResult result)
		{
			SendMailAsyncResult sendMailAsyncResult = (SendMailAsyncResult)result;
			object obj = sendMailAsyncResult.InternalWaitForCompletion();
			if (obj is Exception && (!(obj is SmtpFailedRecipientException) || ((SmtpFailedRecipientException)obj).fatal))
			{
				throw (Exception)obj;
			}
			return new MailWriter(sendMailAsyncResult.stream);
		}

		// Token: 0x060018C3 RID: 6339 RVA: 0x0007D948 File Offset: 0x0007BB48
		private void SendMailFrom()
		{
			IAsyncResult asyncResult = MailCommand.BeginSend(this.connection, SmtpCommands.Mail, this.from, this.allowUnicode, SendMailAsyncResult.sendMailFromCompleted, this);
			if (!asyncResult.CompletedSynchronously)
			{
				return;
			}
			MailCommand.EndSend(asyncResult);
			this.SendToCollection();
		}

		// Token: 0x060018C4 RID: 6340 RVA: 0x0007D990 File Offset: 0x0007BB90
		private static void SendMailFromCompleted(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				SendMailAsyncResult sendMailAsyncResult = (SendMailAsyncResult)result.AsyncState;
				try
				{
					MailCommand.EndSend(result);
					sendMailAsyncResult.SendToCollection();
				}
				catch (Exception result2)
				{
					sendMailAsyncResult.InvokeCallback(result2);
				}
			}
		}

		// Token: 0x060018C5 RID: 6341 RVA: 0x0007D9DC File Offset: 0x0007BBDC
		private void SendToCollection()
		{
			while (this.toIndex < this.toCollection.Count)
			{
				SmtpConnection conn = this.connection;
				Collection<MailAddress> collection = this.toCollection;
				int num = this.toIndex;
				this.toIndex = num + 1;
				MultiAsyncResult multiAsyncResult = (MultiAsyncResult)RecipientCommand.BeginSend(conn, collection[num].GetSmtpAddress(this.allowUnicode) + this.deliveryNotify, SendMailAsyncResult.sendToCollectionCompleted, this);
				if (!multiAsyncResult.CompletedSynchronously)
				{
					return;
				}
				string serverResponse;
				if (!RecipientCommand.EndSend(multiAsyncResult, out serverResponse))
				{
					this.failedRecipientExceptions.Add(new SmtpFailedRecipientException(this.connection.Reader.StatusCode, this.toCollection[this.toIndex - 1].GetSmtpAddress(this.allowUnicode), serverResponse));
				}
			}
			this.SendData();
		}

		// Token: 0x060018C6 RID: 6342 RVA: 0x0007DAA8 File Offset: 0x0007BCA8
		private static void SendToCollectionCompleted(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				SendMailAsyncResult sendMailAsyncResult = (SendMailAsyncResult)result.AsyncState;
				try
				{
					string serverResponse;
					if (!RecipientCommand.EndSend(result, out serverResponse))
					{
						sendMailAsyncResult.failedRecipientExceptions.Add(new SmtpFailedRecipientException(sendMailAsyncResult.connection.Reader.StatusCode, sendMailAsyncResult.toCollection[sendMailAsyncResult.toIndex - 1].GetSmtpAddress(sendMailAsyncResult.allowUnicode), serverResponse));
						if (sendMailAsyncResult.failedRecipientExceptions.Count == sendMailAsyncResult.toCollection.Count)
						{
							SmtpFailedRecipientException ex;
							if (sendMailAsyncResult.toCollection.Count == 1)
							{
								ex = (SmtpFailedRecipientException)sendMailAsyncResult.failedRecipientExceptions[0];
							}
							else
							{
								ex = new SmtpFailedRecipientsException(sendMailAsyncResult.failedRecipientExceptions, true);
							}
							ex.fatal = true;
							sendMailAsyncResult.InvokeCallback(ex);
							return;
						}
					}
					sendMailAsyncResult.SendToCollection();
				}
				catch (Exception result2)
				{
					sendMailAsyncResult.InvokeCallback(result2);
				}
			}
		}

		// Token: 0x060018C7 RID: 6343 RVA: 0x0007DB94 File Offset: 0x0007BD94
		private void SendData()
		{
			IAsyncResult asyncResult = DataCommand.BeginSend(this.connection, SendMailAsyncResult.sendDataCompleted, this);
			if (!asyncResult.CompletedSynchronously)
			{
				return;
			}
			DataCommand.EndSend(asyncResult);
			this.stream = this.connection.GetClosableStream();
			if (this.failedRecipientExceptions.Count > 1)
			{
				base.InvokeCallback(new SmtpFailedRecipientsException(this.failedRecipientExceptions, this.failedRecipientExceptions.Count == this.toCollection.Count));
				return;
			}
			if (this.failedRecipientExceptions.Count == 1)
			{
				base.InvokeCallback(this.failedRecipientExceptions[0]);
				return;
			}
			base.InvokeCallback();
		}

		// Token: 0x060018C8 RID: 6344 RVA: 0x0007DC34 File Offset: 0x0007BE34
		private static void SendDataCompleted(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				SendMailAsyncResult sendMailAsyncResult = (SendMailAsyncResult)result.AsyncState;
				try
				{
					DataCommand.EndSend(result);
					sendMailAsyncResult.stream = sendMailAsyncResult.connection.GetClosableStream();
					if (sendMailAsyncResult.failedRecipientExceptions.Count > 1)
					{
						sendMailAsyncResult.InvokeCallback(new SmtpFailedRecipientsException(sendMailAsyncResult.failedRecipientExceptions, sendMailAsyncResult.failedRecipientExceptions.Count == sendMailAsyncResult.toCollection.Count));
					}
					else if (sendMailAsyncResult.failedRecipientExceptions.Count == 1)
					{
						sendMailAsyncResult.InvokeCallback(sendMailAsyncResult.failedRecipientExceptions[0]);
					}
					else
					{
						sendMailAsyncResult.InvokeCallback();
					}
				}
				catch (Exception result2)
				{
					sendMailAsyncResult.InvokeCallback(result2);
				}
			}
		}

		// Token: 0x060018C9 RID: 6345 RVA: 0x0007DCEC File Offset: 0x0007BEEC
		internal SmtpFailedRecipientException GetFailedRecipientException()
		{
			if (this.failedRecipientExceptions.Count == 1)
			{
				return (SmtpFailedRecipientException)this.failedRecipientExceptions[0];
			}
			if (this.failedRecipientExceptions.Count > 1)
			{
				return new SmtpFailedRecipientsException(this.failedRecipientExceptions, false);
			}
			return null;
		}

		// Token: 0x040018A2 RID: 6306
		private SmtpConnection connection;

		// Token: 0x040018A3 RID: 6307
		private MailAddress from;

		// Token: 0x040018A4 RID: 6308
		private string deliveryNotify;

		// Token: 0x040018A5 RID: 6309
		private static AsyncCallback sendMailFromCompleted = new AsyncCallback(SendMailAsyncResult.SendMailFromCompleted);

		// Token: 0x040018A6 RID: 6310
		private static AsyncCallback sendToCollectionCompleted = new AsyncCallback(SendMailAsyncResult.SendToCollectionCompleted);

		// Token: 0x040018A7 RID: 6311
		private static AsyncCallback sendDataCompleted = new AsyncCallback(SendMailAsyncResult.SendDataCompleted);

		// Token: 0x040018A8 RID: 6312
		private ArrayList failedRecipientExceptions = new ArrayList();

		// Token: 0x040018A9 RID: 6313
		private Stream stream;

		// Token: 0x040018AA RID: 6314
		private MailAddressCollection toCollection;

		// Token: 0x040018AB RID: 6315
		private int toIndex;

		// Token: 0x040018AC RID: 6316
		private bool allowUnicode;
	}
}

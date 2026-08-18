using System;
using System.Collections;
using System.IO;
using System.Net.Mime;

namespace System.Net.Mail
{
	// Token: 0x020006DD RID: 1757
	internal class SendMailAsyncResult : LazyAsyncResult
	{
		// Token: 0x06003635 RID: 13877 RVA: 0x000E7624 File Offset: 0x000E6624
		internal SendMailAsyncResult(SmtpConnection connection, string from, MailAddressCollection toCollection, string deliveryNotify, AsyncCallback callback, object state) : base(null, state, callback)
		{
			this.toCollection = toCollection;
			this.connection = connection;
			this.from = from;
			this.deliveryNotify = deliveryNotify;
		}

		// Token: 0x06003636 RID: 13878 RVA: 0x000E7659 File Offset: 0x000E6659
		internal void Send()
		{
			this.SendMailFrom();
		}

		// Token: 0x06003637 RID: 13879 RVA: 0x000E7664 File Offset: 0x000E6664
		internal static MailWriter End(IAsyncResult result)
		{
			SendMailAsyncResult sendMailAsyncResult = (SendMailAsyncResult)result;
			object obj = sendMailAsyncResult.InternalWaitForCompletion();
			if (obj is Exception)
			{
				throw (Exception)obj;
			}
			return new MailWriter(sendMailAsyncResult.stream);
		}

		// Token: 0x06003638 RID: 13880 RVA: 0x000E769C File Offset: 0x000E669C
		private void SendMailFrom()
		{
			IAsyncResult asyncResult = MailCommand.BeginSend(this.connection, SmtpCommands.Mail, this.from, SendMailAsyncResult.sendMailFromCompleted, this);
			if (!asyncResult.CompletedSynchronously)
			{
				return;
			}
			MailCommand.EndSend(asyncResult);
			this.SendTo();
		}

		// Token: 0x06003639 RID: 13881 RVA: 0x000E76DC File Offset: 0x000E66DC
		private static void SendMailFromCompleted(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				SendMailAsyncResult sendMailAsyncResult = (SendMailAsyncResult)result.AsyncState;
				try
				{
					MailCommand.EndSend(result);
					sendMailAsyncResult.SendTo();
				}
				catch (Exception result2)
				{
					sendMailAsyncResult.InvokeCallback(result2);
				}
				catch
				{
					sendMailAsyncResult.InvokeCallback(new Exception(SR.GetString("net_nonClsCompliantException")));
				}
			}
		}

		// Token: 0x0600363A RID: 13882 RVA: 0x000E774C File Offset: 0x000E674C
		private void SendTo()
		{
			if (this.to == null)
			{
				if (this.SendToCollection())
				{
					this.SendData();
				}
				return;
			}
			IAsyncResult asyncResult = RecipientCommand.BeginSend(this.connection, (this.deliveryNotify != null) ? (this.to + this.deliveryNotify) : this.to, SendMailAsyncResult.sendToCompleted, this);
			if (!asyncResult.CompletedSynchronously)
			{
				return;
			}
			string serverResponse;
			if (!RecipientCommand.EndSend(asyncResult, out serverResponse))
			{
				throw new SmtpFailedRecipientException(this.connection.Reader.StatusCode, this.to, serverResponse);
			}
			this.SendData();
		}

		// Token: 0x0600363B RID: 13883 RVA: 0x000E77DC File Offset: 0x000E67DC
		private static void SendToCompleted(IAsyncResult result)
		{
			if (!result.CompletedSynchronously)
			{
				SendMailAsyncResult sendMailAsyncResult = (SendMailAsyncResult)result.AsyncState;
				try
				{
					string serverResponse;
					if (RecipientCommand.EndSend(result, out serverResponse))
					{
						sendMailAsyncResult.SendData();
					}
					else
					{
						sendMailAsyncResult.InvokeCallback(new SmtpFailedRecipientException(sendMailAsyncResult.connection.Reader.StatusCode, sendMailAsyncResult.to, serverResponse));
					}
				}
				catch (Exception result2)
				{
					sendMailAsyncResult.InvokeCallback(result2);
				}
				catch
				{
					sendMailAsyncResult.InvokeCallback(new Exception(SR.GetString("net_nonClsCompliantException")));
				}
			}
		}

		// Token: 0x0600363C RID: 13884 RVA: 0x000E7874 File Offset: 0x000E6874
		private bool SendToCollection()
		{
			while (this.toIndex < this.toCollection.Count)
			{
				MultiAsyncResult multiAsyncResult = (MultiAsyncResult)RecipientCommand.BeginSend(this.connection, this.toCollection[this.toIndex++].SmtpAddress + this.deliveryNotify, SendMailAsyncResult.sendToCollectionCompleted, this);
				if (!multiAsyncResult.CompletedSynchronously)
				{
					return false;
				}
				string serverResponse;
				if (!RecipientCommand.EndSend(multiAsyncResult, out serverResponse))
				{
					this.failedRecipientExceptions.Add(new SmtpFailedRecipientException(this.connection.Reader.StatusCode, this.toCollection[this.toIndex - 1].SmtpAddress, serverResponse));
				}
			}
			return true;
		}

		// Token: 0x0600363D RID: 13885 RVA: 0x000E7930 File Offset: 0x000E6930
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
						sendMailAsyncResult.failedRecipientExceptions.Add(new SmtpFailedRecipientException(sendMailAsyncResult.connection.Reader.StatusCode, sendMailAsyncResult.toCollection[sendMailAsyncResult.toIndex - 1].SmtpAddress, serverResponse));
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
					if (sendMailAsyncResult.SendToCollection())
					{
						sendMailAsyncResult.SendData();
					}
				}
				catch (Exception result2)
				{
					sendMailAsyncResult.InvokeCallback(result2);
				}
				catch
				{
					sendMailAsyncResult.InvokeCallback(new Exception(SR.GetString("net_nonClsCompliantException")));
				}
			}
		}

		// Token: 0x0600363E RID: 13886 RVA: 0x000E7A44 File Offset: 0x000E6A44
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

		// Token: 0x0600363F RID: 13887 RVA: 0x000E7AE4 File Offset: 0x000E6AE4
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
				catch
				{
					sendMailAsyncResult.InvokeCallback(new Exception(SR.GetString("net_nonClsCompliantException")));
				}
			}
		}

		// Token: 0x0400316D RID: 12653
		private SmtpConnection connection;

		// Token: 0x0400316E RID: 12654
		private string from;

		// Token: 0x0400316F RID: 12655
		private string deliveryNotify;

		// Token: 0x04003170 RID: 12656
		private static AsyncCallback sendMailFromCompleted = new AsyncCallback(SendMailAsyncResult.SendMailFromCompleted);

		// Token: 0x04003171 RID: 12657
		private static AsyncCallback sendToCompleted = new AsyncCallback(SendMailAsyncResult.SendToCompleted);

		// Token: 0x04003172 RID: 12658
		private static AsyncCallback sendToCollectionCompleted = new AsyncCallback(SendMailAsyncResult.SendToCollectionCompleted);

		// Token: 0x04003173 RID: 12659
		private static AsyncCallback sendDataCompleted = new AsyncCallback(SendMailAsyncResult.SendDataCompleted);

		// Token: 0x04003174 RID: 12660
		private ArrayList failedRecipientExceptions = new ArrayList();

		// Token: 0x04003175 RID: 12661
		private Stream stream;

		// Token: 0x04003176 RID: 12662
		private string to;

		// Token: 0x04003177 RID: 12663
		private MailAddressCollection toCollection;

		// Token: 0x04003178 RID: 12664
		private int toIndex;
	}
}

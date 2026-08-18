using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using ClockWorkLogger;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Client.Services.Proxies;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.ClientManager.ClientCaching;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.Public;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.Mailing
{
	// Token: 0x0200003D RID: 61
	public class MailingClientManager : IMailingAsync, IMailing, IService, IDisposable
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x0600022E RID: 558 RVA: 0x0000A79A File Offset: 0x0000899A
		// (set) Token: 0x0600022F RID: 559 RVA: 0x0000A7A2 File Offset: 0x000089A2
		private IMailingAsync mailingAsync { get; set; }

		// Token: 0x06000230 RID: 560 RVA: 0x0000A7AB File Offset: 0x000089AB
		public MailingClientManager()
		{
			this.mailingAsync = ClientServiceFactory.GetAsyncClientInstance<IMailingAsync>();
		}

		// Token: 0x06000231 RID: 561 RVA: 0x0000A7C8 File Offset: 0x000089C8
		public SendEmailsResp SendEmails(SendEmailsReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<SendEmailsReq>(Request);
			ClientCache clientCache = ObjectFactory.Resolve<ClientCache>();
			bool isClockWorkServerEnable = clientCache.IsClockWorkServerEnable;
			bool flag = false;
			SendEmailsResp result;
			try
			{
				IMailing clientInstance = ClientServiceFactory.GetClientInstance<IMailing>(isClockWorkServerEnable, out flag, false);
				result = clientInstance.SendEmails(Request);
			}
			catch (Exception ex)
			{
				CWLogger.Logger.Error("MailingClientManager.SendEmailsFromServer Failed:UsedServerInstance={0}:Error={1}", flag.ToString(), ex.ToString());
				result = this.SendEmailsSynchronously(Request, false);
			}
			return result;
		}

		// Token: 0x06000232 RID: 562 RVA: 0x0000A848 File Offset: 0x00008A48
		public IAsyncResult BeginSendEmails(SendEmailsReq req, AsyncCallback callback, object asyncState)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<SendEmailsReq>(req);
			return this.mailingAsync.BeginSendEmails(req, callback, asyncState);
		}

		// Token: 0x06000233 RID: 563 RVA: 0x0000A874 File Offset: 0x00008A74
		public SendEmailsResp EndSendEmails(IAsyncResult result)
		{
			return this.mailingAsync.EndSendEmails(result);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x0000A892 File Offset: 0x00008A92
		public void Close()
		{
			this.Dispose();
		}

		// Token: 0x06000235 RID: 565 RVA: 0x0000A89C File Offset: 0x00008A9C
		public TPMailResultDTO SendEmail(string to, string from, string subject, string bodytext, string bodyhtml = null, string cc = null, string bcc = null, string attachments = null)
		{
			List<TPMailAttachmentDTO> attachments2 = null;
			bool flag = !string.IsNullOrEmpty(attachments);
			if (flag)
			{
				string[] source = attachments.Split(new char[]
				{
					','
				}, StringSplitOptions.RemoveEmptyEntries);
				attachments2 = (from filename in source
				select filename.Trim() into fn
				where File.Exists(fn)
				let data = File.ReadAllBytes(fn)
				select new TPMailAttachmentDTO
				{
					FileBytes = data,
					FileNameForDisplay = Path.GetFileName(fn)
				}).ToList<TPMailAttachmentDTO>();
			}
			SendEmailsReq sendEmailsReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<SendEmailsReq>();
			sendEmailsReq.MailMessages = new List<TPMailMessageDTO>
			{
				new TPMailMessageDTO
				{
					To = new List<TPMailAddressDTO>
					{
						new TPMailAddressDTO
						{
							EmailAddress = to
						}
					},
					From = new TPMailAddressDTO
					{
						EmailAddress = from
					},
					Subject = subject,
					Body = bodytext,
					BodyHtml = bodyhtml,
					Cc = new List<TPMailAddressDTO>
					{
						new TPMailAddressDTO
						{
							EmailAddress = cc
						}
					},
					Bcc = new List<TPMailAddressDTO>
					{
						new TPMailAddressDTO
						{
							EmailAddress = bcc
						}
					},
					Attachments = attachments2
				}
			};
			SendEmailsResp sendEmailsResp = this.SendEmails(sendEmailsReq);
			TPMailResultDTO result;
			if (sendEmailsResp == null)
			{
				(result = new TPMailResultDTO()).Status = eTPMailResultStatusDTO.Failed;
			}
			else
			{
				result = sendEmailsResp.SendEmailResult;
			}
			return result;
		}

		// Token: 0x06000236 RID: 566 RVA: 0x0000AA50 File Offset: 0x00008C50
		public GetDefaultFromAddressResp GetDefaultFromAddress(GetDefaultFromAddressReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<GetDefaultFromAddressReq>(Request);
			return ClientServiceFactory.GetClientInstance<IMailing>().GetDefaultFromAddress(Request);
		}

		// Token: 0x06000237 RID: 567 RVA: 0x0000AA7C File Offset: 0x00008C7C
		public SendEmailWithOverrideSettingsResp SendEmailWithOverrideSettings(SendEmailWithOverrideSettingsReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<SendEmailWithOverrideSettingsReq>(Request);
			return ClientServiceFactory.GetClientInstance<IMailing>().SendEmailWithOverrideSettings(Request);
		}

		// Token: 0x06000238 RID: 568 RVA: 0x0000AAA8 File Offset: 0x00008CA8
		public SendEmailsReturnResultResp SendEmailsReturnResult(SendEmailsReturnResultReq Request)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<SendEmailsReturnResultReq>(Request);
			return ClientServiceFactory.GetClientInstance<IMailing>().SendEmailsReturnResult(Request);
		}

		// Token: 0x06000239 RID: 569 RVA: 0x0000AAD4 File Offset: 0x00008CD4
		~MailingClientManager()
		{
			this.Dispose(false);
		}

		// Token: 0x0600023A RID: 570 RVA: 0x0000AB08 File Offset: 0x00008D08
		protected virtual void Dispose(bool disposing)
		{
			bool flag = !this.disposed;
			if (flag)
			{
				if (disposing)
				{
				}
				bool flag2 = this.mailingAsync != null;
				if (flag2)
				{
					this.mailingAsync.Close();
				}
				this.mailingAsync = null;
				this.disposed = true;
			}
		}

		// Token: 0x0600023B RID: 571 RVA: 0x0000AB54 File Offset: 0x00008D54
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x0600023C RID: 572 RVA: 0x0000AB68 File Offset: 0x00008D68
		private SendEmailsResp SendEmailsSynchronously(SendEmailsReq Request, bool sendFromServer)
		{
			ObjectFactory.Resolve<IRequestBuilderClientManager>().UpdateRequest<SendEmailsReq>(Request);
			return ClientServiceFactory.GetClientInstance<IMailing>(sendFromServer, false).SendEmails(Request);
		}

		// Token: 0x0400000F RID: 15
		private bool disposed = false;
	}
}

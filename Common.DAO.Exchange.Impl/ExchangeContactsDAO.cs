using System;
using System.Collections.Generic;
using System.Linq;
using ClockWorkLogger;
using Microsoft.Exchange.WebServices.Data;
using TechnoPro.Common.DAO.AppointmentSync;
using TechnoPro.Common.DAO.Exchange.Impl.Adapters;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.AppointmentSync;

namespace TechnoPro.Common.DAO.Exchange.Impl
{
	// Token: 0x02000004 RID: 4
	public class ExchangeContactsDAO : ISyncContactsDAO, IBaseOperationContext<SyncOperationContext>
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x06000030 RID: 48 RVA: 0x00004AD6 File Offset: 0x00002CD6
		// (set) Token: 0x06000031 RID: 49 RVA: 0x00004ADE File Offset: 0x00002CDE
		private ExchangeService ExchangeService { get; set; }

		// Token: 0x06000032 RID: 50 RVA: 0x00004AE7 File Offset: 0x00002CE7
		public ExchangeContactsDAO(SyncOperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000009 RID: 9
		// (get) Token: 0x06000033 RID: 51 RVA: 0x00004AFC File Offset: 0x00002CFC
		// (set) Token: 0x06000034 RID: 52 RVA: 0x00004B14 File Offset: 0x00002D14
		public SyncOperationContext OpContext
		{
			get
			{
				return this._opContext;
			}
			set
			{
				this._opContext = value;
				bool flag = value != null;
				if (flag)
				{
					this.ExchangeService = value.GetExchangeService();
				}
			}
		}

		// Token: 0x06000035 RID: 53 RVA: 0x00004B40 File Offset: 0x00002D40
		public bool IsValidEmailAddress(string address)
		{
			NameResolutionCollection nameResolutionCollection = this.ExchangeService.ResolveName(address);
			bool flag = nameResolutionCollection.Any((NameResolution nm) => nm.Mailbox.RoutingType == "SMTP" && !string.IsNullOrEmpty(nm.Mailbox.Address) && nm.Mailbox.Address.Equals(address, StringComparison.OrdinalIgnoreCase));
			bool flag2 = !flag;
			if (flag2)
			{
				bool flag3 = nameResolutionCollection.Count > 0;
				if (flag3)
				{
					NameResolution nameResolution = nameResolutionCollection.FirstOrDefault((NameResolution nm) => nm.Mailbox.RoutingType == "SMTP" && !string.IsNullOrEmpty(nm.Mailbox.Address));
					bool flag4 = nameResolution != null;
					if (flag4)
					{
						CWLogger.Logger.Debug("OutlookDAO::IsValidEmailAddress:: Address '{0}' is wrong, suggested address is '{1}'", address, nameResolution.Mailbox.Address);
					}
				}
				else
				{
					CWLogger.Logger.Debug("OutlookDAO::IsValidEmailAddress:: Address '{0}' is wrong.", address);
				}
			}
			return flag;
		}

		// Token: 0x06000036 RID: 54 RVA: 0x00004C14 File Offset: 0x00002E14
		public string ResolveEmailAddress(string address)
		{
			string result;
			try
			{
				NameResolutionCollection nameResolutionCollection = this.ExchangeService.ResolveName(address);
				foreach (NameResolution nameResolution in nameResolutionCollection)
				{
					bool flag = nameResolution.Mailbox.RoutingType == "SMTP";
					if (flag)
					{
						return nameResolution.Mailbox.Address;
					}
				}
				result = address;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("ExchangeContactsDAO::ResolveEmailAddress: {0}", ex.ToString()), ex);
				result = address;
			}
			return result;
		}

		// Token: 0x06000037 RID: 55 RVA: 0x00004CCC File Offset: 0x00002ECC
		public string GetPrimarySmtpAddress(string email)
		{
			string result;
			try
			{
				NameResolution nameResolution = this.ExchangeService.ResolveName("smtp:" + email).Where(delegate(NameResolution resolution)
				{
					MailboxType? mailboxType = resolution.Mailbox.MailboxType;
					MailboxType mailboxType2 = MailboxType.Mailbox;
					return mailboxType.GetValueOrDefault() == mailboxType2 & mailboxType != null;
				}).FirstOrDefault<NameResolution>();
				string text = (nameResolution != null) ? nameResolution.Mailbox.Address : email;
				CWLogger.Logger.Debug("ExchangeContactsDAO::GetPrimarySmtpAddress: email='{0}', primary='{1}'", email, text);
				result = text;
			}
			catch (Exception ex)
			{
				CWLogger.Logger.ErrorException(string.Format("ExchangeContactsDAO::GetPrimaryStmpAddress: {0}", ex.ToString()), ex);
				result = email;
			}
			return result;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00004D78 File Offset: 0x00002F78
		public IList<ExternalAttendee> GetGroupMembers(string email)
		{
			ExpandGroupResults expandGroupResults = this.ExchangeService.ExpandGroup(email);
			return (from m in expandGroupResults.Members
			select new ExternalAttendee
			{
				Username = m.Address,
				Name = m.Name,
				MailboxType = eMailboxType.Group
			}).ToList<ExternalAttendee>();
		}

		// Token: 0x04000013 RID: 19
		private SyncOperationContext _opContext;
	}
}

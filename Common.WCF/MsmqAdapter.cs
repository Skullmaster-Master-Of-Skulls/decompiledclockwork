using System;
using System.Messaging;
using System.ServiceModel;
using ClockWorkLogger;
using TechnoPro.Common.WinServices;

namespace TechnoPro.Common.WCF
{
	// Token: 0x0200000A RID: 10
	public static class MsmqAdapter
	{
		// Token: 0x06000043 RID: 67 RVA: 0x00002F40 File Offset: 0x00001140
		static MsmqAdapter()
		{
			MsmqServiceStatus messagingQueueServiceStatus = MessaggingQueueAdapter.GetMessagingQueueServiceStatus();
			MsmqAdapter.isMSMQInstalled = (messagingQueueServiceStatus > MsmqServiceStatus.NotInstalled);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002F64 File Offset: 0x00001164
		public static void VerifyQueue(this string queue, bool transactional = false)
		{
			bool flag = MsmqAdapter.isMSMQInstalled && !MessageQueue.Exists(queue);
			if (flag)
			{
				MessageQueue messageQueue = transactional ? MessageQueue.Create(queue, true) : MessageQueue.Create(queue);
				CWLogger.Logger.Trace("WCF::VerifyQueue: Queue '{0}' has been created", queue);
				bool flag2 = false;
				try
				{
					messageQueue.SetPermissions("NETWORK SERVICE", MessageQueueAccessRights.DeleteMessage | MessageQueueAccessRights.PeekMessage | MessageQueueAccessRights.WriteMessage | MessageQueueAccessRights.DeleteJournalMessage | MessageQueueAccessRights.GetQueueProperties | MessageQueueAccessRights.GetQueuePermissions);
					CWLogger.Logger.Trace("WCF::VerifyQueue: Setting permissions to 'NETWORK SERVICE' in '{0}' ", queue);
				}
				catch (Exception ex)
				{
					CWLogger.Logger.ErrorException(string.Format("MsmqAdapter::VerifyQueue: Settting 'NETWORK SERVICE' permissions failed.\n{0}", ex.ToString()), ex);
					messageQueue.SetPermissions("Everyone", MessageQueueAccessRights.DeleteMessage | MessageQueueAccessRights.PeekMessage | MessageQueueAccessRights.WriteMessage | MessageQueueAccessRights.DeleteJournalMessage | MessageQueueAccessRights.GetQueueProperties | MessageQueueAccessRights.GetQueuePermissions);
					flag2 = true;
				}
				try
				{
					messageQueue.SetPermissions("IIS_IUSRS", MessageQueueAccessRights.DeleteMessage | MessageQueueAccessRights.PeekMessage | MessageQueueAccessRights.WriteMessage | MessageQueueAccessRights.DeleteJournalMessage | MessageQueueAccessRights.GetQueueProperties | MessageQueueAccessRights.GetQueuePermissions);
					CWLogger.Logger.Trace("WCF::VerifyQueue: Setting permissions to 'IIS_IUSRS' in '{0}' ", queue);
				}
				catch (Exception ex2)
				{
					CWLogger.Logger.ErrorException(string.Format("MsmqAdapter::VerifyQueue: Settting 'IIS_IUSRS' permissions failed.\n{0}", ex2.ToString()), ex2);
					bool flag3 = !flag2;
					if (flag3)
					{
						messageQueue.SetPermissions("Everyone", MessageQueueAccessRights.DeleteMessage | MessageQueueAccessRights.PeekMessage | MessageQueueAccessRights.WriteMessage | MessageQueueAccessRights.DeleteJournalMessage | MessageQueueAccessRights.GetQueueProperties | MessageQueueAccessRights.GetQueuePermissions);
					}
				}
				try
				{
					messageQueue.SetPermissions("Administrators", MessageQueueAccessRights.FullControl);
					CWLogger.Logger.Trace("WCF::VerifyQueue: Setting permissions to 'Administrators' in '{0}' ", queue);
				}
				catch (Exception ex3)
				{
					CWLogger.Logger.ErrorException(string.Format("MsmqAdapter::VerifyQueue: Settting 'Administrators' permissions failed.\n{0}", ex3.ToString()), ex3);
				}
			}
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000030E8 File Offset: 0x000012E8
		public static void VerifyQueue(this EndpointAddress address, bool transactional = false)
		{
			string queueFromUri = MsmqAdapter.GetQueueFromUri(address.Uri);
			queueFromUri.VerifyQueue(transactional);
		}

		// Token: 0x06000046 RID: 70 RVA: 0x0000310C File Offset: 0x0000130C
		private static string GetQueueFromUri(Uri uri)
		{
			return string.Format(".\\private$\\{0}", uri.AbsolutePath.Substring(9));
		}

		// Token: 0x04000011 RID: 17
		private static bool isMSMQInstalled = false;
	}
}

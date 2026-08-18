using System;
using System.Transactions;
using OracleInternal.Common;

namespace OracleInternal.MTS
{
	// Token: 0x02000131 RID: 305
	internal class MTSProxyPool
	{
		// Token: 0x06000C82 RID: 3202 RVA: 0x0008BBE8 File Offset: 0x00089DE8
		internal static MTSTxnRM GetRM(bool bIsCCP, string easyConnectName, string serviceName, string pdbName, Transaction txn)
		{
			if (ProviderConfig.m_bTraceLevelPrivate_NoTrace)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					"Getting RM for Server ID = " + easyConnectName + " Txn Local ID = " + txn.TransactionInformation.LocalIdentifier
				});
			}
			string text = string.Empty;
			if (string.IsNullOrEmpty(serviceName))
			{
				text = string.Format("{0}.{1}", easyConnectName, string.Empty);
			}
			else
			{
				text = string.Format("{0}.{1}", easyConnectName, string.IsNullOrEmpty(serviceName) ? string.Empty : serviceName);
			}
			MTSTxnRM mtstxnRM = null;
			MTSTxnRM result;
			try
			{
				MTSProxy mtsproxy = null;
				if ((mtsproxy = MTSProxyPool.s_dbProxies[text]) == null)
				{
					lock (MTSProxyPool.s_lock)
					{
						if ((mtsproxy = MTSProxyPool.s_dbProxies[text]) == null)
						{
							if (ProviderConfig.m_bTraceLevelPrivate_NoTrace)
							{
								Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[]
								{
									"Creating MTSProxy for Server ID = " + text + " Txn Local ID = " + txn.TransactionInformation.LocalIdentifier
								});
							}
							mtsproxy = new MTSProxy(text);
							MTSProxyPool.s_dbProxies[text] = mtsproxy;
						}
						else
						{
							mtsproxy = MTSProxyPool.s_dbProxies[text];
						}
					}
				}
				mtstxnRM = mtsproxy.GetRM(bIsCCP, serviceName, pdbName, txn);
				result = mtstxnRM;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate_NoTrace)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[]
					{
						string.Concat(new string[]
						{
							"Getting TxnRM = ",
							(mtstxnRM != null) ? mtstxnRM.m_RMGuid.ToString() : "null",
							" for Server ID = ",
							text,
							" Txn Local ID = ",
							txn.TransactionInformation.LocalIdentifier
						})
					});
				}
			}
			return result;
		}

		// Token: 0x06000C83 RID: 3203 RVA: 0x0008BDBC File Offset: 0x00089FBC
		internal static void ReleaseRM(bool bIsCCP, string dataSource, Transaction txn)
		{
			if (ProviderConfig.m_bTraceLevelPrivate_NoTrace)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[0]);
			}
			try
			{
				MTSProxy mtsproxy;
				if ((mtsproxy = MTSProxyPool.s_dbProxies[dataSource]) != null)
				{
					mtsproxy.RemoveRM(bIsCCP, txn.TransactionInformation.LocalIdentifier);
				}
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate_NoTrace)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[0]);
				}
			}
		}

		// Token: 0x04000D9E RID: 3486
		private static MTSProxyPool.MTSProxies s_dbProxies = new MTSProxyPool.MTSProxies();

		// Token: 0x04000D9F RID: 3487
		private static object s_lock = new object();

		// Token: 0x02000132 RID: 306
		private class MTSTxnRMs : SyncDictionary<string, MTSTxnRM>
		{
		}

		// Token: 0x02000133 RID: 307
		private class MTSProxies : SyncDictionary<string, MTSProxy>
		{
		}
	}
}

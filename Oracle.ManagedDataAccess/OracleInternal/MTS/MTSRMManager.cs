using System;
using System.Configuration;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Transactions;
using Oracle.ManagedDataAccess.Client;
using OracleInternal.Common;
using OracleInternal.ConnectionPool;
using OracleInternal.ServiceObjects;

namespace OracleInternal.MTS
{
	// Token: 0x02000129 RID: 297
	internal class MTSRMManager
	{
		// Token: 0x06000C6A RID: 3178 RVA: 0x0008AE08 File Offset: 0x00089008
		static MTSRMManager()
		{
			if (ConfigBaseClass.m_recoveryServicePort <= 0)
			{
				ConfigBaseClass.m_recoveryServicePort = 2030;
			}
			if (ConfigBaseClass.m_dtcTxnTimeout <= 0U)
			{
				ConfigBaseClass.m_dtcTxnTimeout = 120U;
			}
			IPAddress ipaddress;
			if (ConfigBaseClass.m_recoveryServiceHost.Length > TransXID.MAXRRECOHOSTNAME_LEN && IPAddress.TryParse(ConfigBaseClass.m_recoveryServiceHost, out ipaddress) && ipaddress.AddressFamily == AddressFamily.InterNetworkV6)
			{
				ConfigBaseClass.m_recoveryServiceHost = ipaddress.ToString();
				if (ConfigBaseClass.m_recoveryServiceHost.Length > TransXID.MAXRRECOHOSTNAME_LEN)
				{
					ConfigBaseClass.m_recoveryServiceHost = MTSRMManager.CompressedIPv6(ipaddress);
				}
			}
			if ((DTCDebugConfig.s_DTCDbgEvt & DTCDebugEvent.MTSConfig) == DTCDebugEvent.MTSConfig)
			{
				Console.WriteLine("DTC_DEBUG_EVENT = " + DTCDebugConfig.s_DTCDbgEvt);
				Console.WriteLine("MTS Config value: Host = " + ConfigBaseClass.m_recoveryServiceHost);
				Console.WriteLine("MTS Config value: Port = " + ConfigBaseClass.m_recoveryServicePort);
				Console.WriteLine("MTS Config value: Transaction Time out = " + ConfigBaseClass.m_dtcTxnTimeout);
				Console.WriteLine("MTS Config value: Is Use DTC DLL = " + ConfigBaseClass.m_dtcUseDTCDLL);
			}
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
				{
					"DTC_DEBUG_EVENT = " + DTCDebugConfig.s_DTCDbgEvt
				});
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
				{
					"MTS Config value: Host = " + ConfigBaseClass.m_recoveryServiceHost
				});
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
				{
					"MTS Config value: Port = " + ConfigBaseClass.m_recoveryServicePort
				});
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
				{
					"MTS Config value: Transaction Time out = " + ConfigBaseClass.m_dtcTxnTimeout
				});
				Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
				{
					"MTS Config value: Is Use DTC DLL = " + ConfigBaseClass.m_dtcUseDTCDLL
				});
			}
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0008AFE8 File Offset: 0x000891E8
		private static string CompressedIPv6(IPAddress address)
		{
			byte[] addressBytes = address.GetAddressBytes();
			StringBuilder stringBuilder = new StringBuilder();
			byte[] array = new byte[2];
			for (int i = 0; i < addressBytes.Length; i += 2)
			{
				array[0] = addressBytes[i + 1];
				array[1] = addressBytes[i];
				ushort num = BitConverter.ToUInt16(array, 0);
				if (stringBuilder.Length > 0)
				{
					stringBuilder.Append(":");
				}
				if (num != 0)
				{
					stringBuilder.AppendFormat("{0:x}", num);
				}
				else
				{
					stringBuilder.Append("0");
				}
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0008B074 File Offset: 0x00089274
		private static Exception HandleException(Exception ex)
		{
			OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268439552, ex, null);
			if (!(ex.GetType() == typeof(OracleException)))
			{
				return ex;
			}
			if (((OracleException)ex).Errors.Count <= 0)
			{
				return ex;
			}
			if (((OracleException)ex).Errors[0].Number == 161)
			{
				return new ConfigurationErrorsException(ex.Message, new OracleException(ResourceStringConstants.MTS_INVALID_CONFIG_VALUES, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.MTS_INVALID_CONFIG_VALUES, new string[0])));
			}
			return ex;
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0008B10C File Offset: 0x0008930C
		private static void CheckPromotionRule(OracleConnectionImpl connImpl)
		{
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0008B110 File Offset: 0x00089310
		internal static void EnlistPromotedTransaction(OracleConnectionImpl connImpl, Transaction txn, MTSTxnRM txnRM, MTSTxnBranch txnBranch, Guid sysTxnXID)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"Local TxnID =  ",
						txn.TransactionInformation.LocalIdentifier,
						"\t TxnID = ",
						txn.TransactionInformation.DistributedIdentifier,
						" using Conn ID = ",
						connImpl.m_endUserSessionId,
						" to DBInst = ",
						connImpl.m_instanceName
					})
				});
			}
			try
			{
				MTSRMManager.CheckPromotionRule(connImpl);
				if (txnBranch.m_bNew)
				{
					txnBranch.Set(txn.TransactionInformation.LocalIdentifier, sysTxnXID, txn.IsolationLevel);
				}
				txnBranch.PromoteDistributedTransaction(connImpl);
				txnRM.AddBranch(connImpl, txnBranch, sysTxnXID);
			}
			catch (Exception ex)
			{
				throw MTSRMManager.HandleException(ex);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[]
					{
						string.Concat(new object[]
						{
							"Local TxnID =  ",
							txn.TransactionInformation.LocalIdentifier,
							"\t TxnID = ",
							txnBranch.TxnID,
							" using Conn ID = ",
							connImpl.m_endUserSessionId,
							" to DBInst = ",
							connImpl.m_instanceName
						})
					});
				}
			}
		}

		// Token: 0x06000C6F RID: 3183 RVA: 0x0008B288 File Offset: 0x00089488
		internal static void CCPEnlistDistributedTxnToSysTxn(OracleConnectionImpl connImpl, Transaction txn, MTSTxnRM txnRM, MTSTxnBranch txnBranch)
		{
			if (ProviderConfig.m_bTraceLevelPrivate_NoTrace)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"Local TxnID =  ",
						txn.TransactionInformation.LocalIdentifier,
						"\t TxnID = ",
						txn.TransactionInformation.DistributedIdentifier,
						" using Conn ID = ",
						connImpl.m_endUserSessionId,
						" to DBInst = ",
						connImpl.m_instanceName
					})
				});
			}
			try
			{
				TransactionInterop.GetTransmitterPropagationToken(txn);
				if (txnBranch.m_bNew)
				{
					txnBranch.Set(txn.TransactionInformation.LocalIdentifier, txn.TransactionInformation.DistributedIdentifier, txn.IsolationLevel);
				}
				txnBranch.StartDistributedTransaction(connImpl);
				txnRM.AddBranch(connImpl, txnBranch, txn.TransactionInformation.DistributedIdentifier);
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
					{
						string.Concat(new object[]
						{
							"Distributed Txn Start Succeeded: Local TxnID =  ",
							txn.TransactionInformation.LocalIdentifier,
							"\t TxnID = ",
							txn.TransactionInformation.DistributedIdentifier,
							" using Conn ID = ",
							connImpl.m_endUserSessionId,
							" to DBInst = ",
							connImpl.m_instanceName
						})
					});
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
						{
							string.Concat(new object[]
							{
								"Releasing Connection with Conn ID = ",
								connImpl.m_endUserSessionId,
								" to DBInst = ",
								connImpl.m_instanceName,
								"\t TxnID = ",
								txnBranch.TxnID
							})
						});
					}
				}
				OracleConnectionDispenser<OraclePoolManager, OraclePool, OracleConnectionImpl>.PutFromDTC(connImpl);
				txnRM.EnlistToSysTransaction();
			}
			catch (Exception ex)
			{
				throw MTSRMManager.HandleException(ex);
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[]
					{
						string.Concat(new object[]
						{
							"Local TxnID =  ",
							txn.TransactionInformation.LocalIdentifier,
							"\t TxnID = ",
							txnBranch.TxnID,
							" using Conn ID = ",
							connImpl.m_endUserSessionId,
							" to DBInst = ",
							connImpl.m_instanceName
						})
					});
				}
			}
		}

		// Token: 0x06000C70 RID: 3184 RVA: 0x0008B534 File Offset: 0x00089734
		internal static void CCPEnlistTransaction(OracleConnectionImpl connImpl, Transaction transaction, CriteriaCtx criteriaCtx)
		{
			if (ProviderConfig.m_bTraceLevelPrivate_NoTrace)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"Local TxnID =  ",
						transaction.TransactionInformation.LocalIdentifier,
						" using conn ID = ",
						connImpl.m_endUserSessionId,
						" to DBInst = ",
						connImpl.m_instanceName
					})
				});
			}
			if (transaction.IsolationLevel != IsolationLevel.Serializable && transaction.IsolationLevel != IsolationLevel.ReadCommitted)
			{
				throw new ArgumentException(OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_INVALID_ISO_LEVEL, new string[0]), "isolationLevel");
			}
			MTSTxnRM rm = OracleConnectionDispenser<OraclePoolManager, OraclePool, OracleConnectionImpl>.GetRM(connImpl.m_cs, criteriaCtx, transaction, connImpl);
			MTSTxnBranch txnBranch = rm.GetTxnBranch(connImpl.m_cs, connImpl.m_instanceName);
			if (txnBranch == null)
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
					{
						string.Format("Max branch limit reach ({0}).  Local Txn ID = {1}", rm.m_branchNum, transaction.TransactionInformation.LocalIdentifier, rm.m_txnAffInstanceName)
					});
				}
				throw new OracleException(ResourceStringConstants.CON_MTS_ENLIST_FAIL, string.Empty, string.Empty, OracleStringResourceManager.GetErrorMesg(ResourceStringConstants.CON_MTS_ENLIST_FAIL, new string[0]));
			}
			MTSRMManager.CCPEnlistTransaction(connImpl, transaction, rm, txnBranch);
			if (ProviderConfig.m_bTraceLevelPrivate_NoTrace)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[]
				{
					string.Format("[GetTxnBranch] (2) (txnid={0}) (affinity={1}) (rmid={2}) (rmtxid={3}) (brid={4}) (brtxnid={5})", new object[]
					{
						transaction.TransactionInformation.LocalIdentifier,
						rm.m_txnAffInstanceName,
						rm.GetHashCode(),
						rm.m_txnLocalID,
						txnBranch.GetHashCode(),
						txnBranch.m_txnLocalID
					})
				});
			}
		}

		// Token: 0x06000C71 RID: 3185 RVA: 0x0008B6EC File Offset: 0x000898EC
		internal static void CCPEnlistTransaction(OracleConnectionImpl connImpl, Transaction transaction, MTSTxnRM txnRM, MTSTxnBranch txnBranch)
		{
			if (ProviderConfig.m_bTraceLevelPrivate)
			{
				Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4352, new string[]
				{
					string.Concat(new object[]
					{
						"Local TxnID =  ",
						transaction.TransactionInformation.LocalIdentifier,
						" using conn ID = ",
						connImpl.m_endUserSessionId,
						" to DBInst = ",
						connImpl.m_instanceName
					})
				});
			}
			try
			{
				connImpl.m_localTxnId = transaction.TransactionInformation.LocalIdentifier;
				if ((connImpl.m_cs.m_promotableTransaction == PromotableTransaction.Local || (connImpl.m_cs.m_promotableTransaction == PromotableTransaction.Promotable && connImpl.IsSupportPromotableTransaction)) && transaction.EnlistPromotableSinglePhase(PSPEManager.Create(connImpl, transaction, txnRM, txnBranch)))
				{
					if (ProviderConfig.m_bTraceLevelPrivate)
					{
						Trace.Write(OracleTraceLevel.Private, OracleTraceTag.MTS, new string[]
						{
							string.Concat(new object[]
							{
								"Register PSPE Local Txn:\tLocal Txn ID = ",
								transaction.TransactionInformation.LocalIdentifier,
								" using Conn ID = ",
								connImpl.m_endUserSessionId,
								" to DBInst = ",
								connImpl.m_instanceName
							})
						});
					}
				}
				else
				{
					MTSRMManager.CCPEnlistDistributedTxnToSysTxn(connImpl, transaction, txnRM, txnBranch);
				}
			}
			catch (Exception ex)
			{
				OracleException.HandleError(OracleTraceLevel.Private, (OracleTraceTag)268439552, ex, null);
				connImpl.m_localTxnId = string.Empty;
				throw;
			}
			finally
			{
				if (ProviderConfig.m_bTraceLevelPrivate)
				{
					Trace.Write(OracleTraceLevel.Private, (OracleTraceTag)4608, new string[]
					{
						string.Concat(new object[]
						{
							" Local TxnID : ",
							transaction.TransactionInformation.LocalIdentifier,
							" using conn ID = ",
							connImpl.m_endUserSessionId,
							" to DBInst = ",
							connImpl.m_instanceName
						})
					});
				}
			}
		}
	}
}

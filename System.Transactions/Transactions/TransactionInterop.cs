using System;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Transactions.Diagnostics;
using System.Transactions.Oletx;

namespace System.Transactions
{
	// Token: 0x02000066 RID: 102
	[PermissionSet(SecurityAction.LinkDemand, Name = "FullTrust")]
	public static class TransactionInterop
	{
		// Token: 0x060002CC RID: 716 RVA: 0x000336D4 File Offset: 0x00032AD4
		internal static OletxTransaction ConvertToOletxTransaction(Transaction transaction)
		{
			if (null == transaction)
			{
				throw new ArgumentNullException("transaction");
			}
			if (transaction.Disposed)
			{
				throw new ObjectDisposedException("Transaction");
			}
			if (transaction.complete)
			{
				throw TransactionException.CreateTransactionCompletedException(SR.GetString("TraceSourceLtm"));
			}
			return transaction.Promote();
		}

		// Token: 0x060002CD RID: 717 RVA: 0x00033734 File Offset: 0x00032B34
		public static byte[] GetExportCookie(Transaction transaction, byte[] whereabouts)
		{
			if (!TransactionManager._platformValidated)
			{
				TransactionManager.ValidatePlatform();
			}
			byte[] array = null;
			if (null == transaction)
			{
				throw new ArgumentNullException("transaction");
			}
			if (whereabouts == null)
			{
				throw new ArgumentNullException("whereabouts");
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "TransactionInterop.GetExportCookie");
			}
			byte[] array2 = new byte[whereabouts.Length];
			Array.Copy(whereabouts, array2, whereabouts.Length);
			whereabouts = array2;
			int num = 0;
			uint num2 = 0U;
			CoTaskMemHandle coTaskMemHandle = null;
			OletxTransaction oletxTransaction = TransactionInterop.ConvertToOletxTransaction(transaction);
			try
			{
				oletxTransaction.realOletxTransaction.TransactionShim.Export(Convert.ToUInt32(whereabouts.Length), whereabouts, out num, out num2, out coTaskMemHandle);
				array = new byte[num2];
				Marshal.Copy(coTaskMemHandle.DangerousGetHandle(), array, 0, Convert.ToInt32(num2));
			}
			catch (COMException ex)
			{
				OletxTransactionManager.ProxyException(ex);
				throw TransactionManagerCommunicationException.Create(SR.GetString("TraceSourceOletx"), ex);
			}
			finally
			{
				if (coTaskMemHandle != null)
				{
					coTaskMemHandle.Close();
				}
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "TransactionInterop.GetExportCookie");
			}
			return array;
		}

		// Token: 0x060002CE RID: 718 RVA: 0x00033874 File Offset: 0x00032C74
		public static Transaction GetTransactionFromExportCookie(byte[] cookie)
		{
			if (!TransactionManager._platformValidated)
			{
				TransactionManager.ValidatePlatform();
			}
			if (cookie == null)
			{
				throw new ArgumentNullException("cookie");
			}
			if (cookie.Length < 32)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument"), "cookie");
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "TransactionInterop.GetTransactionFromExportCookie");
			}
			byte[] array = new byte[cookie.Length];
			Array.Copy(cookie, array, cookie.Length);
			cookie = array;
			ITransactionShim transactionShim = null;
			Guid empty = Guid.Empty;
			OletxTransactionIsolationLevel oletxIsoLevel = OletxTransactionIsolationLevel.ISOLATIONLEVEL_SERIALIZABLE;
			OutcomeEnlistment outcomeEnlistment = null;
			byte[] array2 = new byte[16];
			for (int i = 0; i < array2.Length; i++)
			{
				array2[i] = cookie[i + 16];
			}
			Guid transactionIdentifier = new Guid(array2);
			Transaction transaction = TransactionManager.FindPromotedTransaction(transactionIdentifier);
			if (null != transaction)
			{
				if (DiagnosticTrace.Verbose)
				{
					MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "TransactionInterop.GetTransactionFromExportCookie");
				}
				return transaction;
			}
			OletxTransactionManager distributedTransactionManager = TransactionManager.DistributedTransactionManager;
			distributedTransactionManager.dtcTransactionManagerLock.AcquireReaderLock(-1);
			try
			{
				outcomeEnlistment = new OutcomeEnlistment();
				IntPtr intPtr = IntPtr.Zero;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					intPtr = HandleTable.AllocHandle(outcomeEnlistment);
					distributedTransactionManager.DtcTransactionManager.ProxyShimFactory.Import(Convert.ToUInt32(cookie.Length), cookie, intPtr, out empty, out oletxIsoLevel, out transactionShim);
				}
				finally
				{
					if (transactionShim == null && intPtr != IntPtr.Zero)
					{
						HandleTable.FreeHandle(intPtr);
					}
				}
			}
			catch (COMException ex)
			{
				OletxTransactionManager.ProxyException(ex);
				throw TransactionManagerCommunicationException.Create(SR.GetString("TraceSourceOletx"), ex);
			}
			finally
			{
				distributedTransactionManager.dtcTransactionManagerLock.ReleaseReaderLock();
			}
			RealOletxTransaction realOletxTransaction = new RealOletxTransaction(distributedTransactionManager, transactionShim, outcomeEnlistment, empty, oletxIsoLevel, false);
			OletxTransaction oletx = new OletxTransaction(realOletxTransaction);
			transaction = TransactionManager.FindOrCreatePromotedTransaction(transactionIdentifier, oletx);
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "TransactionInterop.GetTransactionFromExportCookie");
			}
			return transaction;
		}

		// Token: 0x060002CF RID: 719 RVA: 0x00033A84 File Offset: 0x00032E84
		public static byte[] GetTransmitterPropagationToken(Transaction transaction)
		{
			if (!TransactionManager._platformValidated)
			{
				TransactionManager.ValidatePlatform();
			}
			if (null == transaction)
			{
				throw new ArgumentNullException("transaction");
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "TransactionInterop.GetTransmitterPropagationToken");
			}
			OletxTransaction oletxTx = TransactionInterop.ConvertToOletxTransaction(transaction);
			byte[] transmitterPropagationToken = TransactionInterop.GetTransmitterPropagationToken(oletxTx);
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "TransactionInterop.GetTransmitterPropagationToken");
			}
			return transmitterPropagationToken;
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x00033B04 File Offset: 0x00032F04
		internal static byte[] GetTransmitterPropagationToken(OletxTransaction oletxTx)
		{
			byte[] array = null;
			CoTaskMemHandle coTaskMemHandle = null;
			uint num = 0U;
			try
			{
				oletxTx.realOletxTransaction.TransactionShim.GetPropagationToken(out num, out coTaskMemHandle);
				array = new byte[num];
				Marshal.Copy(coTaskMemHandle.DangerousGetHandle(), array, 0, Convert.ToInt32(num));
			}
			catch (COMException comException)
			{
				OletxTransactionManager.ProxyException(comException);
				throw;
			}
			finally
			{
				if (coTaskMemHandle != null)
				{
					coTaskMemHandle.Close();
				}
			}
			return array;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x00033B94 File Offset: 0x00032F94
		public static Transaction GetTransactionFromTransmitterPropagationToken(byte[] propagationToken)
		{
			if (!TransactionManager._platformValidated)
			{
				TransactionManager.ValidatePlatform();
			}
			if (propagationToken == null)
			{
				throw new ArgumentNullException("propagationToken");
			}
			if (propagationToken.Length < 24)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument"), "propagationToken");
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "TransactionInterop.GetTransactionFromTransmitterPropagationToken");
			}
			byte[] array = new byte[16];
			for (int i = 0; i < array.Length; i++)
			{
				array[i] = propagationToken[i + 8];
			}
			Guid transactionIdentifier = new Guid(array);
			Transaction transaction = TransactionManager.FindPromotedTransaction(transactionIdentifier);
			if (null != transaction)
			{
				if (DiagnosticTrace.Verbose)
				{
					MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "TransactionInterop.GetTransactionFromTransmitterPropagationToken");
				}
				return transaction;
			}
			OletxTransaction oletxTransactionFromTransmitterPropigationToken = TransactionInterop.GetOletxTransactionFromTransmitterPropigationToken(propagationToken);
			Transaction result = TransactionManager.FindOrCreatePromotedTransaction(transactionIdentifier, oletxTransactionFromTransmitterPropigationToken);
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "TransactionInterop.GetTransactionFromTransmitterPropagationToken");
			}
			return result;
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x00033C84 File Offset: 0x00033084
		internal static OletxTransaction GetOletxTransactionFromTransmitterPropigationToken(byte[] propagationToken)
		{
			ITransactionShim transactionShim = null;
			if (propagationToken == null)
			{
				throw new ArgumentNullException("propagationToken");
			}
			if (propagationToken.Length < 24)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument"), "propagationToken");
			}
			byte[] array = new byte[propagationToken.Length];
			Array.Copy(propagationToken, array, propagationToken.Length);
			propagationToken = array;
			OletxTransactionManager distributedTransactionManager = TransactionManager.DistributedTransactionManager;
			distributedTransactionManager.dtcTransactionManagerLock.AcquireReaderLock(-1);
			OutcomeEnlistment outcomeEnlistment;
			Guid identifier;
			OletxTransactionIsolationLevel oletxIsoLevel;
			try
			{
				outcomeEnlistment = new OutcomeEnlistment();
				IntPtr intPtr = IntPtr.Zero;
				RuntimeHelpers.PrepareConstrainedRegions();
				try
				{
					intPtr = HandleTable.AllocHandle(outcomeEnlistment);
					distributedTransactionManager.DtcTransactionManager.ProxyShimFactory.ReceiveTransaction(Convert.ToUInt32(propagationToken.Length), propagationToken, intPtr, out identifier, out oletxIsoLevel, out transactionShim);
				}
				finally
				{
					if (transactionShim == null && intPtr != IntPtr.Zero)
					{
						HandleTable.FreeHandle(intPtr);
					}
				}
			}
			catch (COMException ex)
			{
				OletxTransactionManager.ProxyException(ex);
				throw TransactionManagerCommunicationException.Create(SR.GetString("TraceSourceOletx"), ex);
			}
			finally
			{
				distributedTransactionManager.dtcTransactionManagerLock.ReleaseReaderLock();
			}
			RealOletxTransaction realOletxTransaction = new RealOletxTransaction(distributedTransactionManager, transactionShim, outcomeEnlistment, identifier, oletxIsoLevel, false);
			return new OletxTransaction(realOletxTransaction);
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x00033DD4 File Offset: 0x000331D4
		public static IDtcTransaction GetDtcTransaction(Transaction transaction)
		{
			if (!TransactionManager._platformValidated)
			{
				TransactionManager.ValidatePlatform();
			}
			if (null == transaction)
			{
				throw new ArgumentNullException("transaction");
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "TransactionInterop.GetDtcTransaction");
			}
			IDtcTransaction result = null;
			OletxTransaction oletxTransaction = TransactionInterop.ConvertToOletxTransaction(transaction);
			try
			{
				oletxTransaction.realOletxTransaction.TransactionShim.GetITransactionNative(out result);
			}
			catch (COMException comException)
			{
				OletxTransactionManager.ProxyException(comException);
				throw;
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "TransactionInterop.GetDtcTransaction");
			}
			return result;
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x00033E84 File Offset: 0x00033284
		public static Transaction GetTransactionFromDtcTransaction(IDtcTransaction transactionNative)
		{
			if (!TransactionManager._platformValidated)
			{
				TransactionManager.ValidatePlatform();
			}
			bool flag = false;
			ITransactionShim transactionShim = null;
			Guid empty = Guid.Empty;
			OletxTransactionIsolationLevel oletxIsoLevel = OletxTransactionIsolationLevel.ISOLATIONLEVEL_SERIALIZABLE;
			OutcomeEnlistment outcomeEnlistment = null;
			if (transactionNative == null)
			{
				throw new ArgumentNullException("transactionNative");
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "TransactionInterop.GetTransactionFromDtc");
			}
			ITransactionNativeInternal transactionNativeInternal = transactionNative as ITransactionNativeInternal;
			if (transactionNativeInternal == null)
			{
				throw new ArgumentException(SR.GetString("InvalidArgument"), "transactionNative");
			}
			OletxXactTransInfo oletxXactTransInfo;
			try
			{
				transactionNativeInternal.GetTransactionInfo(out oletxXactTransInfo);
			}
			catch (COMException ex)
			{
				if (NativeMethods.XACT_E_NOTRANSACTION != ex.ErrorCode)
				{
					throw;
				}
				flag = true;
				oletxXactTransInfo.uow = Guid.Empty;
			}
			OletxTransactionManager distributedTransactionManager = TransactionManager.DistributedTransactionManager;
			Transaction transaction;
			if (!flag)
			{
				transaction = TransactionManager.FindPromotedTransaction(oletxXactTransInfo.uow);
				if (null != transaction)
				{
					if (DiagnosticTrace.Verbose)
					{
						MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "TransactionInterop.GetTransactionFromDtcTransaction");
					}
					return transaction;
				}
				distributedTransactionManager.dtcTransactionManagerLock.AcquireReaderLock(-1);
				try
				{
					outcomeEnlistment = new OutcomeEnlistment();
					IntPtr intPtr = IntPtr.Zero;
					RuntimeHelpers.PrepareConstrainedRegions();
					try
					{
						intPtr = HandleTable.AllocHandle(outcomeEnlistment);
						distributedTransactionManager.DtcTransactionManager.ProxyShimFactory.CreateTransactionShim(transactionNative, intPtr, out empty, out oletxIsoLevel, out transactionShim);
					}
					finally
					{
						if (transactionShim == null && intPtr != IntPtr.Zero)
						{
							HandleTable.FreeHandle(intPtr);
						}
					}
				}
				catch (COMException comException)
				{
					OletxTransactionManager.ProxyException(comException);
					throw;
				}
				finally
				{
					distributedTransactionManager.dtcTransactionManagerLock.ReleaseReaderLock();
				}
				RealOletxTransaction realOletxTransaction = new RealOletxTransaction(distributedTransactionManager, transactionShim, outcomeEnlistment, empty, oletxIsoLevel, false);
				OletxTransaction oletxTransaction = new OletxTransaction(realOletxTransaction);
				transaction = TransactionManager.FindOrCreatePromotedTransaction(oletxXactTransInfo.uow, oletxTransaction);
			}
			else
			{
				RealOletxTransaction realOletxTransaction = new RealOletxTransaction(distributedTransactionManager, null, null, empty, OletxTransactionIsolationLevel.ISOLATIONLEVEL_SERIALIZABLE, false);
				OletxTransaction oletxTransaction = new OletxTransaction(realOletxTransaction);
				transaction = new Transaction(oletxTransaction);
				TransactionManager.FireDistributedTransactionStarted(transaction);
				oletxTransaction.savedLtmPromotedTransaction = transaction;
				InternalTransaction.DistributedTransactionOutcome(transaction.internalTransaction, TransactionStatus.InDoubt);
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "TransactionInterop.GetTransactionFromDtc");
			}
			return transaction;
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x000340D4 File Offset: 0x000334D4
		public static byte[] GetWhereabouts()
		{
			if (!TransactionManager._platformValidated)
			{
				TransactionManager.ValidatePlatform();
			}
			byte[] result = null;
			if (DiagnosticTrace.Verbose)
			{
				MethodEnteredTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "TransactionInterop.GetWhereabouts");
			}
			OletxTransactionManager distributedTransactionManager = TransactionManager.DistributedTransactionManager;
			if (distributedTransactionManager == null)
			{
				throw new ArgumentException(SR.GetString("ArgumentWrongType"), "transactionManager");
			}
			distributedTransactionManager.dtcTransactionManagerLock.AcquireReaderLock(-1);
			try
			{
				result = distributedTransactionManager.DtcTransactionManager.Whereabouts;
			}
			finally
			{
				distributedTransactionManager.dtcTransactionManagerLock.ReleaseReaderLock();
			}
			if (DiagnosticTrace.Verbose)
			{
				MethodExitedTraceRecord.Trace(SR.GetString("TraceSourceOletx"), "TransactionInterop.GetWhereabouts");
			}
			return result;
		}
	}
}

using System;
using System.EnterpriseServices;

namespace System.Web.Util
{
	// Token: 0x02000228 RID: 552
	public class Transactions
	{
		// Token: 0x06001A4C RID: 6732 RVA: 0x000525F0 File Offset: 0x000507F0
		public static void InvokeTransacted(TransactedCallback callback, TransactionOption mode)
		{
			bool flag = false;
			Transactions.InvokeTransacted(callback, mode, ref flag);
		}

		// Token: 0x06001A4D RID: 6733 RVA: 0x00052608 File Offset: 0x00050808
		public static void InvokeTransacted(TransactedCallback callback, TransactionOption mode, ref bool transactionAborted)
		{
			HttpRuntime.CheckAspNetHostingPermission(AspNetHostingPermissionLevel.Medium, "Transaction_not_supported_in_low_trust");
			bool flag = false;
			if (Environment.OSVersion.Platform != PlatformID.Win32NT || Environment.OSVersion.Version.Major <= 4)
			{
				throw new PlatformNotSupportedException(SR.GetString("RequiresNT"));
			}
			if (mode == TransactionOption.Disabled)
			{
				flag = true;
			}
			if (flag)
			{
				callback();
				transactionAborted = false;
				return;
			}
			Transactions.TransactedInvocation transactedInvocation = new Transactions.TransactedInvocation(callback);
			TransactedExecCallback callback2 = new TransactedExecCallback(transactedInvocation.ExecuteTransactedCode);
			PerfCounters.IncrementCounter(AppPerfCounter.TRANSACTIONS_PENDING);
			int num;
			try
			{
				num = UnsafeNativeMethods.TransactManagedCallback(callback2, (int)mode);
			}
			finally
			{
				PerfCounters.DecrementCounter(AppPerfCounter.TRANSACTIONS_PENDING);
			}
			if (transactedInvocation.Error != null)
			{
				throw new HttpException(null, transactedInvocation.Error);
			}
			PerfCounters.IncrementCounter(AppPerfCounter.TRANSACTIONS_TOTAL);
			if (num == 1)
			{
				PerfCounters.IncrementCounter(AppPerfCounter.TRANSACTIONS_COMMITTED);
				transactionAborted = false;
				return;
			}
			if (num == 0)
			{
				PerfCounters.IncrementCounter(AppPerfCounter.TRANSACTIONS_ABORTED);
				transactionAborted = true;
				return;
			}
			throw new HttpException(SR.GetString("Cannot_execute_transacted_code"));
		}

		// Token: 0x0200094E RID: 2382
		internal class Utils
		{
			// Token: 0x060069A0 RID: 27040 RVA: 0x000030B5 File Offset: 0x000012B5
			private Utils()
			{
			}

			// Token: 0x17001D27 RID: 7463
			// (get) Token: 0x060069A1 RID: 27041 RVA: 0x001779E0 File Offset: 0x00175BE0
			internal static bool IsInTransaction
			{
				get
				{
					bool result = false;
					try
					{
						result = ContextUtil.IsInTransaction;
					}
					catch
					{
					}
					return result;
				}
			}

			// Token: 0x17001D28 RID: 7464
			// (get) Token: 0x060069A2 RID: 27042 RVA: 0x00177A0C File Offset: 0x00175C0C
			internal static bool AbortPending
			{
				get
				{
					bool result = false;
					try
					{
						if (ContextUtil.MyTransactionVote == TransactionVote.Abort)
						{
							result = true;
						}
					}
					catch
					{
					}
					return result;
				}
			}
		}

		// Token: 0x0200094F RID: 2383
		internal class TransactedInvocation
		{
			// Token: 0x060069A3 RID: 27043 RVA: 0x00177A3C File Offset: 0x00175C3C
			internal TransactedInvocation(TransactedCallback callback)
			{
				this._callback = callback;
			}

			// Token: 0x060069A4 RID: 27044 RVA: 0x00177A4C File Offset: 0x00175C4C
			internal int ExecuteTransactedCode()
			{
				TransactedExecState result = TransactedExecState.CommitPending;
				try
				{
					this._callback();
					if (Transactions.Utils.AbortPending)
					{
						result = TransactedExecState.AbortPending;
					}
				}
				catch (Exception error)
				{
					this._error = error;
					result = TransactedExecState.Error;
				}
				return (int)result;
			}

			// Token: 0x17001D29 RID: 7465
			// (get) Token: 0x060069A5 RID: 27045 RVA: 0x00177A90 File Offset: 0x00175C90
			internal Exception Error
			{
				get
				{
					return this._error;
				}
			}

			// Token: 0x040037D8 RID: 14296
			private TransactedCallback _callback;

			// Token: 0x040037D9 RID: 14297
			private Exception _error;
		}
	}
}

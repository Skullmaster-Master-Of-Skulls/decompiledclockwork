using System;
using System.Collections.Generic;
using System.Threading;
using System.Transactions;

namespace System.ServiceModel.Transactions
{
	// Token: 0x020001B3 RID: 435
	internal abstract class TransactionCache<T, S>
	{
		// Token: 0x06000E42 RID: 3650 RVA: 0x00033254 File Offset: 0x00031454
		protected void AddEntry(Transaction transaction, T key, S value)
		{
			this.key = key;
			if (TransactionCache<T, S>.Add(key, value))
			{
				transaction.TransactionCompleted += this.OnTransactionCompleted;
			}
		}

		// Token: 0x06000E43 RID: 3651 RVA: 0x00033278 File Offset: 0x00031478
		private void OnTransactionCompleted(object sender, TransactionEventArgs e)
		{
			TransactionCache<T, S>.Remove(this.key);
		}

		// Token: 0x06000E44 RID: 3652 RVA: 0x00033288 File Offset: 0x00031488
		private static bool Add(T key, S value)
		{
			bool flag = false;
			try
			{
				try
				{
				}
				finally
				{
					TransactionCache<T, S>.cacheLock.AcquireWriterLock(-1);
					flag = true;
				}
				if (!TransactionCache<T, S>.cache.ContainsKey(key))
				{
					TransactionCache<T, S>.cache.Add(key, value);
					return true;
				}
			}
			finally
			{
				if (flag)
				{
					TransactionCache<T, S>.cacheLock.ReleaseWriterLock();
				}
			}
			return false;
		}

		// Token: 0x06000E45 RID: 3653 RVA: 0x000332F4 File Offset: 0x000314F4
		private static void Remove(T key)
		{
			bool flag = false;
			try
			{
				try
				{
				}
				finally
				{
					TransactionCache<T, S>.cacheLock.AcquireWriterLock(-1);
					flag = true;
				}
				if (!TransactionCache<T, S>.cache.Remove(key))
				{
					DiagnosticUtility.FailFast("TransactionCache: key must be present in transaction cache");
				}
			}
			finally
			{
				if (flag)
				{
					TransactionCache<T, S>.cacheLock.ReleaseWriterLock();
				}
			}
		}

		// Token: 0x06000E46 RID: 3654 RVA: 0x0003335C File Offset: 0x0003155C
		public static bool Find(T key, out S value)
		{
			bool flag = false;
			try
			{
				try
				{
				}
				finally
				{
					TransactionCache<T, S>.cacheLock.AcquireReaderLock(-1);
					flag = true;
				}
				if (TransactionCache<T, S>.cache.TryGetValue(key, out value))
				{
					return true;
				}
			}
			finally
			{
				if (flag)
				{
					TransactionCache<T, S>.cacheLock.ReleaseReaderLock();
				}
			}
			return false;
		}

		// Token: 0x04001741 RID: 5953
		private static Dictionary<T, S> cache = new Dictionary<T, S>();

		// Token: 0x04001742 RID: 5954
		private static ReaderWriterLock cacheLock = new ReaderWriterLock();

		// Token: 0x04001743 RID: 5955
		private T key;
	}
}

using System;
using System.Threading;

namespace log4net.Util
{
	// Token: 0x02000114 RID: 276
	public sealed class ReaderWriterLock
	{
		// Token: 0x06000811 RID: 2065 RVA: 0x00018F9C File Offset: 0x0001719C
		public ReaderWriterLock()
		{
			this.m_lock = new ReaderWriterLockSlim(LockRecursionPolicy.SupportsRecursion);
		}

		// Token: 0x06000812 RID: 2066 RVA: 0x00018FB0 File Offset: 0x000171B0
		public void AcquireReaderLock()
		{
			try
			{
			}
			finally
			{
				this.m_lock.EnterReadLock();
			}
		}

		// Token: 0x06000813 RID: 2067 RVA: 0x00018FDC File Offset: 0x000171DC
		public void ReleaseReaderLock()
		{
			this.m_lock.ExitReadLock();
		}

		// Token: 0x06000814 RID: 2068 RVA: 0x00018FEC File Offset: 0x000171EC
		public void AcquireWriterLock()
		{
			try
			{
			}
			finally
			{
				this.m_lock.EnterWriteLock();
			}
		}

		// Token: 0x06000815 RID: 2069 RVA: 0x00019018 File Offset: 0x00017218
		public void ReleaseWriterLock()
		{
			this.m_lock.ExitWriteLock();
		}

		// Token: 0x040002F0 RID: 752
		private ReaderWriterLockSlim m_lock;
	}
}

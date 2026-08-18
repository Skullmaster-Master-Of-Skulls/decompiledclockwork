using System;
using System.Threading;

namespace System.ServiceModel.Channels
{
	// Token: 0x02000828 RID: 2088
	internal struct LockHelper : IDisposable
	{
		// Token: 0x06004E04 RID: 19972 RVA: 0x0011D204 File Offset: 0x0011B404
		private LockHelper(ReaderWriterLockSlim readerWriterLock, bool isReaderLock)
		{
			this.readerWriterLock = readerWriterLock;
			this.isReaderLock = isReaderLock;
			if (isReaderLock)
			{
				this.readerWriterLock.EnterReadLock();
			}
			else
			{
				this.readerWriterLock.EnterWriteLock();
			}
			this.isLockHeld = true;
		}

		// Token: 0x06004E05 RID: 19973 RVA: 0x0011D236 File Offset: 0x0011B436
		public void Dispose()
		{
			if (this.isLockHeld)
			{
				this.isLockHeld = false;
				if (this.isReaderLock)
				{
					this.readerWriterLock.ExitReadLock();
					return;
				}
				this.readerWriterLock.ExitWriteLock();
			}
		}

		// Token: 0x06004E06 RID: 19974 RVA: 0x0011D266 File Offset: 0x0011B466
		internal static IDisposable TakeWriterLock(ReaderWriterLockSlim readerWriterLock)
		{
			return new LockHelper(readerWriterLock, false);
		}

		// Token: 0x06004E07 RID: 19975 RVA: 0x0011D274 File Offset: 0x0011B474
		internal static IDisposable TakeReaderLock(ReaderWriterLockSlim readerWriterLock)
		{
			return new LockHelper(readerWriterLock, true);
		}

		// Token: 0x040030C2 RID: 12482
		private ReaderWriterLockSlim readerWriterLock;

		// Token: 0x040030C3 RID: 12483
		private bool isReaderLock;

		// Token: 0x040030C4 RID: 12484
		private bool isLockHeld;
	}
}

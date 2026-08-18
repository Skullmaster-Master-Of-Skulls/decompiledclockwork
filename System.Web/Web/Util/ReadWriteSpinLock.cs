using System;
using System.Threading;

namespace System.Web.Util
{
	// Token: 0x0200077C RID: 1916
	internal struct ReadWriteSpinLock
	{
		// Token: 0x06005C92 RID: 23698 RVA: 0x0017316E File Offset: 0x0017216E
		private static bool WriterWaiting(int bits)
		{
			return (bits & 1073741824) != 0;
		}

		// Token: 0x06005C93 RID: 23699 RVA: 0x0017317D File Offset: 0x0017217D
		private static int WriteLockCount(int bits)
		{
			return (bits & 1073676288) >> 16;
		}

		// Token: 0x06005C94 RID: 23700 RVA: 0x00173189 File Offset: 0x00172189
		private static int ReadLockCount(int bits)
		{
			return bits & 65535;
		}

		// Token: 0x06005C95 RID: 23701 RVA: 0x00173192 File Offset: 0x00172192
		private static bool NoWriters(int bits)
		{
			return (bits & 1073676288) == 0;
		}

		// Token: 0x06005C96 RID: 23702 RVA: 0x0017319E File Offset: 0x0017219E
		private static bool NoWritersOrWaitingWriters(int bits)
		{
			return (bits & 2147418112) == 0;
		}

		// Token: 0x06005C97 RID: 23703 RVA: 0x001731AA File Offset: 0x001721AA
		private static bool NoLocks(int bits)
		{
			return (bits & -1073741825) == 0;
		}

		// Token: 0x06005C98 RID: 23704 RVA: 0x001731B6 File Offset: 0x001721B6
		private bool WriterWaiting()
		{
			return ReadWriteSpinLock.WriterWaiting(this._bits);
		}

		// Token: 0x06005C99 RID: 23705 RVA: 0x001731C3 File Offset: 0x001721C3
		private int WriteLockCount()
		{
			return ReadWriteSpinLock.WriteLockCount(this._bits);
		}

		// Token: 0x06005C9A RID: 23706 RVA: 0x001731D0 File Offset: 0x001721D0
		private int ReadLockCount()
		{
			return ReadWriteSpinLock.ReadLockCount(this._bits);
		}

		// Token: 0x06005C9B RID: 23707 RVA: 0x001731DD File Offset: 0x001721DD
		private bool NoWriters()
		{
			return ReadWriteSpinLock.NoWriters(this._bits);
		}

		// Token: 0x06005C9C RID: 23708 RVA: 0x001731EA File Offset: 0x001721EA
		private bool NoWritersOrWaitingWriters()
		{
			return ReadWriteSpinLock.NoWritersOrWaitingWriters(this._bits);
		}

		// Token: 0x06005C9D RID: 23709 RVA: 0x001731F7 File Offset: 0x001721F7
		private bool NoLocks()
		{
			return ReadWriteSpinLock.NoLocks(this._bits);
		}

		// Token: 0x06005C9E RID: 23710 RVA: 0x00173204 File Offset: 0x00172204
		private int CreateNewBits(bool writerWaiting, int writeCount, int readCount)
		{
			int num = writeCount << 16 | readCount;
			if (writerWaiting)
			{
				num |= 1073741824;
			}
			return num;
		}

		// Token: 0x06005C9F RID: 23711 RVA: 0x00173224 File Offset: 0x00172224
		internal void AcquireReaderLock()
		{
			int hashCode = Thread.CurrentThread.GetHashCode();
			if (this._TryAcquireReaderLock(hashCode))
			{
				return;
			}
			this._Spin(true, hashCode);
		}

		// Token: 0x06005CA0 RID: 23712 RVA: 0x00173250 File Offset: 0x00172250
		internal void AcquireWriterLock()
		{
			int hashCode = Thread.CurrentThread.GetHashCode();
			if (this._TryAcquireWriterLock(hashCode))
			{
				return;
			}
			this._Spin(false, hashCode);
		}

		// Token: 0x06005CA1 RID: 23713 RVA: 0x0017327A File Offset: 0x0017227A
		internal void ReleaseReaderLock()
		{
			Interlocked.Decrement(ref this._bits);
		}

		// Token: 0x06005CA2 RID: 23714 RVA: 0x00173288 File Offset: 0x00172288
		private void AlterWriteCountHoldingWriterLock(int oldBits, int delta)
		{
			int readCount = ReadWriteSpinLock.ReadLockCount(oldBits);
			int num = ReadWriteSpinLock.WriteLockCount(oldBits);
			int writeCount = num + delta;
			for (;;)
			{
				int value = this.CreateNewBits(ReadWriteSpinLock.WriterWaiting(oldBits), writeCount, readCount);
				int num2 = Interlocked.CompareExchange(ref this._bits, value, oldBits);
				if (num2 == oldBits)
				{
					break;
				}
				oldBits = num2;
			}
		}

		// Token: 0x06005CA3 RID: 23715 RVA: 0x001732D0 File Offset: 0x001722D0
		internal void ReleaseWriterLock()
		{
			int bits = this._bits;
			int num = ReadWriteSpinLock.WriteLockCount(bits);
			if (num == 1)
			{
				this._id = 0;
			}
			this.AlterWriteCountHoldingWriterLock(bits, -1);
		}

		// Token: 0x06005CA4 RID: 23716 RVA: 0x00173300 File Offset: 0x00172300
		private bool _TryAcquireWriterLock(int threadId)
		{
			int id = this._id;
			int num = this._bits;
			if (id == threadId)
			{
				this.AlterWriteCountHoldingWriterLock(num, 1);
				return true;
			}
			if (id == 0 && ReadWriteSpinLock.NoLocks(num))
			{
				int value = this.CreateNewBits(false, 1, 0);
				int num2 = Interlocked.CompareExchange(ref this._bits, value, num);
				if (num2 == num)
				{
					id = this._id;
					this._id = threadId;
					return true;
				}
				num = num2;
			}
			if (!ReadWriteSpinLock.WriterWaiting(num))
			{
				for (;;)
				{
					int value = num | 1073741824;
					int num2 = Interlocked.CompareExchange(ref this._bits, value, num);
					if (num2 == num)
					{
						break;
					}
					num = num2;
				}
			}
			return false;
		}

		// Token: 0x06005CA5 RID: 23717 RVA: 0x0017338C File Offset: 0x0017238C
		private bool _TryAcquireReaderLock(int threadId)
		{
			int bits = this._bits;
			int id = this._id;
			if (id == 0)
			{
				if (!ReadWriteSpinLock.NoWriters(bits))
				{
					return false;
				}
			}
			else if (id != threadId)
			{
				return false;
			}
			return Interlocked.CompareExchange(ref this._bits, bits + 1, bits) == bits;
		}

		// Token: 0x06005CA6 RID: 23718 RVA: 0x001733D0 File Offset: 0x001723D0
		private void _Spin(bool isReaderLock, int threadId)
		{
			int num = 0;
			double num2 = ReadWriteSpinLock.s_backOffFactors[Math.Abs(threadId) % 13];
			int num3 = (int)(4000.0 * num2);
			num3 = Math.Min(10000, num3);
			num3 = Math.Max(num3, 100);
			DateTime utcNow = DateTime.UtcNow;
			bool flag = ReadWriteSpinLock.s_disableBusyWaiting;
			for (;;)
			{
				if (isReaderLock)
				{
					if (this._TryAcquireReaderLock(threadId))
					{
						break;
					}
				}
				else if (this._TryAcquireWriterLock(threadId))
				{
					return;
				}
				if (flag)
				{
					Thread.Sleep(num);
					num ^= 1;
				}
				else
				{
					int num4 = num3;
					for (;;)
					{
						if (isReaderLock)
						{
							if (this.NoWritersOrWaitingWriters())
							{
								break;
							}
						}
						else if (this.NoLocks())
						{
							break;
						}
						if (--num4 < 0)
						{
							Thread.Sleep(num);
							num3 /= 2;
							num3 = Math.Max(num3, 100);
							num4 = num3;
							num ^= 1;
						}
						else
						{
							Thread.SpinWait(10);
						}
					}
				}
			}
		}

		// Token: 0x04003176 RID: 12662
		private const int BACK_OFF_FACTORS_LENGTH = 13;

		// Token: 0x04003177 RID: 12663
		private const int WRITER_WAITING_MASK = 1073741824;

		// Token: 0x04003178 RID: 12664
		private const int WRITE_COUNT_MASK = 1073676288;

		// Token: 0x04003179 RID: 12665
		private const int READ_COUNT_MASK = 65535;

		// Token: 0x0400317A RID: 12666
		private const int WRITER_WAITING_SHIFT = 30;

		// Token: 0x0400317B RID: 12667
		private const int WRITE_COUNT_SHIFT = 16;

		// Token: 0x0400317C RID: 12668
		private int _bits;

		// Token: 0x0400317D RID: 12669
		private int _id;

		// Token: 0x0400317E RID: 12670
		private static bool s_disableBusyWaiting = SystemInfo.GetNumProcessCPUs() == 1;

		// Token: 0x0400317F RID: 12671
		private static readonly double[] s_backOffFactors = new double[]
		{
			1.02,
			0.965,
			0.89,
			1.065,
			1.025,
			1.115,
			0.94,
			0.995,
			1.05,
			1.08,
			0.915,
			0.98,
			1.01
		};
	}
}

using System;
using System.Threading;

namespace System.Web.Util
{
	// Token: 0x02000218 RID: 536
	internal struct ReadWriteSpinLock
	{
		// Token: 0x060019E1 RID: 6625 RVA: 0x00050E44 File Offset: 0x0004F044
		private static bool WriterWaiting(int bits)
		{
			return (bits & 1073741824) != 0;
		}

		// Token: 0x060019E2 RID: 6626 RVA: 0x00050E50 File Offset: 0x0004F050
		private static int WriteLockCount(int bits)
		{
			return (bits & 1073676288) >> 16;
		}

		// Token: 0x060019E3 RID: 6627 RVA: 0x00050E5C File Offset: 0x0004F05C
		private static int ReadLockCount(int bits)
		{
			return bits & 65535;
		}

		// Token: 0x060019E4 RID: 6628 RVA: 0x00050E65 File Offset: 0x0004F065
		private static bool NoWriters(int bits)
		{
			return (bits & 1073676288) == 0;
		}

		// Token: 0x060019E5 RID: 6629 RVA: 0x00050E71 File Offset: 0x0004F071
		private static bool NoWritersOrWaitingWriters(int bits)
		{
			return (bits & 2147418112) == 0;
		}

		// Token: 0x060019E6 RID: 6630 RVA: 0x00050E7D File Offset: 0x0004F07D
		private static bool NoLocks(int bits)
		{
			return (bits & -1073741825) == 0;
		}

		// Token: 0x060019E7 RID: 6631 RVA: 0x00050E89 File Offset: 0x0004F089
		private bool WriterWaiting()
		{
			return ReadWriteSpinLock.WriterWaiting(this._bits);
		}

		// Token: 0x060019E8 RID: 6632 RVA: 0x00050E96 File Offset: 0x0004F096
		private int WriteLockCount()
		{
			return ReadWriteSpinLock.WriteLockCount(this._bits);
		}

		// Token: 0x060019E9 RID: 6633 RVA: 0x00050EA3 File Offset: 0x0004F0A3
		private int ReadLockCount()
		{
			return ReadWriteSpinLock.ReadLockCount(this._bits);
		}

		// Token: 0x060019EA RID: 6634 RVA: 0x00050EB0 File Offset: 0x0004F0B0
		private bool NoWriters()
		{
			return ReadWriteSpinLock.NoWriters(this._bits);
		}

		// Token: 0x060019EB RID: 6635 RVA: 0x00050EBD File Offset: 0x0004F0BD
		private bool NoWritersOrWaitingWriters()
		{
			return ReadWriteSpinLock.NoWritersOrWaitingWriters(this._bits);
		}

		// Token: 0x060019EC RID: 6636 RVA: 0x00050ECA File Offset: 0x0004F0CA
		private bool NoLocks()
		{
			return ReadWriteSpinLock.NoLocks(this._bits);
		}

		// Token: 0x060019ED RID: 6637 RVA: 0x00050ED8 File Offset: 0x0004F0D8
		private int CreateNewBits(bool writerWaiting, int writeCount, int readCount)
		{
			int num = writeCount << 16 | readCount;
			if (writerWaiting)
			{
				num |= 1073741824;
			}
			return num;
		}

		// Token: 0x060019EE RID: 6638 RVA: 0x00050EF8 File Offset: 0x0004F0F8
		internal void AcquireReaderLock()
		{
			int hashCode = Thread.CurrentThread.GetHashCode();
			if (this._TryAcquireReaderLock(hashCode))
			{
				return;
			}
			this._Spin(true, hashCode);
		}

		// Token: 0x060019EF RID: 6639 RVA: 0x00050F24 File Offset: 0x0004F124
		internal void AcquireWriterLock()
		{
			int hashCode = Thread.CurrentThread.GetHashCode();
			if (this._TryAcquireWriterLock(hashCode))
			{
				return;
			}
			this._Spin(false, hashCode);
		}

		// Token: 0x060019F0 RID: 6640 RVA: 0x00050F50 File Offset: 0x0004F150
		internal void ReleaseReaderLock()
		{
			int num = Interlocked.Decrement(ref this._bits);
		}

		// Token: 0x060019F1 RID: 6641 RVA: 0x00050F6C File Offset: 0x0004F16C
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

		// Token: 0x060019F2 RID: 6642 RVA: 0x00050FB4 File Offset: 0x0004F1B4
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

		// Token: 0x060019F3 RID: 6643 RVA: 0x00050FE4 File Offset: 0x0004F1E4
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

		// Token: 0x060019F4 RID: 6644 RVA: 0x00051070 File Offset: 0x0004F270
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

		// Token: 0x060019F5 RID: 6645 RVA: 0x000510B4 File Offset: 0x0004F2B4
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

		// Token: 0x040017F1 RID: 6129
		private int _bits;

		// Token: 0x040017F2 RID: 6130
		private int _id;

		// Token: 0x040017F3 RID: 6131
		private static bool s_disableBusyWaiting = SystemInfo.GetNumProcessCPUs() == 1;

		// Token: 0x040017F4 RID: 6132
		private const int BACK_OFF_FACTORS_LENGTH = 13;

		// Token: 0x040017F5 RID: 6133
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

		// Token: 0x040017F6 RID: 6134
		private const int WRITER_WAITING_MASK = 1073741824;

		// Token: 0x040017F7 RID: 6135
		private const int WRITE_COUNT_MASK = 1073676288;

		// Token: 0x040017F8 RID: 6136
		private const int READ_COUNT_MASK = 65535;

		// Token: 0x040017F9 RID: 6137
		private const int WRITER_WAITING_SHIFT = 30;

		// Token: 0x040017FA RID: 6138
		private const int WRITE_COUNT_SHIFT = 16;
	}
}

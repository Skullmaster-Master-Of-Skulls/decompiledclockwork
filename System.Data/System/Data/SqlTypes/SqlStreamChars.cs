using System;
using System.IO;

namespace System.Data.SqlTypes
{
	// Token: 0x020002C1 RID: 705
	internal abstract class SqlStreamChars : INullable, IDisposable
	{
		// Token: 0x17000549 RID: 1353
		// (get) Token: 0x0600237D RID: 9085
		public abstract bool IsNull { get; }

		// Token: 0x1700054A RID: 1354
		// (get) Token: 0x0600237E RID: 9086
		public abstract bool CanRead { get; }

		// Token: 0x1700054B RID: 1355
		// (get) Token: 0x0600237F RID: 9087
		public abstract bool CanSeek { get; }

		// Token: 0x1700054C RID: 1356
		// (get) Token: 0x06002380 RID: 9088
		public abstract bool CanWrite { get; }

		// Token: 0x1700054D RID: 1357
		// (get) Token: 0x06002381 RID: 9089
		public abstract long Length { get; }

		// Token: 0x1700054E RID: 1358
		// (get) Token: 0x06002382 RID: 9090
		// (set) Token: 0x06002383 RID: 9091
		public abstract long Position { get; set; }

		// Token: 0x06002384 RID: 9092
		public abstract int Read(char[] buffer, int offset, int count);

		// Token: 0x06002385 RID: 9093
		public abstract void Write(char[] buffer, int offset, int count);

		// Token: 0x06002386 RID: 9094
		public abstract long Seek(long offset, SeekOrigin origin);

		// Token: 0x06002387 RID: 9095
		public abstract void SetLength(long value);

		// Token: 0x06002388 RID: 9096
		public abstract void Flush();

		// Token: 0x06002389 RID: 9097 RVA: 0x00291038 File Offset: 0x00290438
		public virtual void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x0600238A RID: 9098 RVA: 0x00291058 File Offset: 0x00290458
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x0600238B RID: 9099 RVA: 0x00291078 File Offset: 0x00290478
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x0600238C RID: 9100 RVA: 0x00291088 File Offset: 0x00290488
		public virtual int ReadChar()
		{
			char[] array = new char[1];
			if (this.Read(array, 0, 1) == 0)
			{
				return -1;
			}
			return (int)array[0];
		}

		// Token: 0x0600238D RID: 9101 RVA: 0x002910B8 File Offset: 0x002904B8
		public virtual void WriteChar(char value)
		{
			this.Write(new char[]
			{
				value
			}, 0, 1);
		}

		// Token: 0x1700054F RID: 1359
		// (get) Token: 0x0600238E RID: 9102 RVA: 0x002910E8 File Offset: 0x002904E8
		public static SqlStreamChars Null
		{
			get
			{
				return new SqlStreamChars.NullSqlStreamChars();
			}
		}

		// Token: 0x020002C2 RID: 706
		private class NullSqlStreamChars : SqlStreamChars
		{
			// Token: 0x06002390 RID: 9104 RVA: 0x00291128 File Offset: 0x00290528
			internal NullSqlStreamChars()
			{
			}

			// Token: 0x17000550 RID: 1360
			// (get) Token: 0x06002391 RID: 9105 RVA: 0x00291148 File Offset: 0x00290548
			public override bool IsNull
			{
				get
				{
					return true;
				}
			}

			// Token: 0x17000551 RID: 1361
			// (get) Token: 0x06002392 RID: 9106 RVA: 0x00291158 File Offset: 0x00290558
			public override bool CanRead
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000552 RID: 1362
			// (get) Token: 0x06002393 RID: 9107 RVA: 0x00291168 File Offset: 0x00290568
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000553 RID: 1363
			// (get) Token: 0x06002394 RID: 9108 RVA: 0x00291178 File Offset: 0x00290578
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x17000554 RID: 1364
			// (get) Token: 0x06002395 RID: 9109 RVA: 0x00291188 File Offset: 0x00290588
			public override long Length
			{
				get
				{
					throw new SqlNullValueException();
				}
			}

			// Token: 0x17000555 RID: 1365
			// (get) Token: 0x06002396 RID: 9110 RVA: 0x002911A8 File Offset: 0x002905A8
			// (set) Token: 0x06002397 RID: 9111 RVA: 0x002911C8 File Offset: 0x002905C8
			public override long Position
			{
				get
				{
					throw new SqlNullValueException();
				}
				set
				{
					throw new SqlNullValueException();
				}
			}

			// Token: 0x06002398 RID: 9112 RVA: 0x002911E8 File Offset: 0x002905E8
			public override int Read(char[] buffer, int offset, int count)
			{
				throw new SqlNullValueException();
			}

			// Token: 0x06002399 RID: 9113 RVA: 0x00291208 File Offset: 0x00290608
			public override void Write(char[] buffer, int offset, int count)
			{
				throw new SqlNullValueException();
			}

			// Token: 0x0600239A RID: 9114 RVA: 0x00291228 File Offset: 0x00290628
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new SqlNullValueException();
			}

			// Token: 0x0600239B RID: 9115 RVA: 0x00291248 File Offset: 0x00290648
			public override void SetLength(long value)
			{
				throw new SqlNullValueException();
			}

			// Token: 0x0600239C RID: 9116 RVA: 0x00291268 File Offset: 0x00290668
			public override void Flush()
			{
				throw new SqlNullValueException();
			}

			// Token: 0x0600239D RID: 9117 RVA: 0x00291288 File Offset: 0x00290688
			public override void Close()
			{
			}
		}
	}
}

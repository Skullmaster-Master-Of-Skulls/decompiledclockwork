using System;
using System.IO;

namespace System.Data.SqlTypes
{
	// Token: 0x02000158 RID: 344
	internal abstract class SqlStreamChars : INullable, IDisposable
	{
		// Token: 0x1700030A RID: 778
		// (get) Token: 0x06001483 RID: 5251
		public abstract bool IsNull { get; }

		// Token: 0x1700030B RID: 779
		// (get) Token: 0x06001484 RID: 5252
		public abstract bool CanRead { get; }

		// Token: 0x1700030C RID: 780
		// (get) Token: 0x06001485 RID: 5253
		public abstract bool CanSeek { get; }

		// Token: 0x1700030D RID: 781
		// (get) Token: 0x06001486 RID: 5254
		public abstract bool CanWrite { get; }

		// Token: 0x1700030E RID: 782
		// (get) Token: 0x06001487 RID: 5255
		public abstract long Length { get; }

		// Token: 0x1700030F RID: 783
		// (get) Token: 0x06001488 RID: 5256
		// (set) Token: 0x06001489 RID: 5257
		public abstract long Position { get; set; }

		// Token: 0x0600148A RID: 5258
		public abstract int Read(char[] buffer, int offset, int count);

		// Token: 0x0600148B RID: 5259
		public abstract void Write(char[] buffer, int offset, int count);

		// Token: 0x0600148C RID: 5260
		public abstract long Seek(long offset, SeekOrigin origin);

		// Token: 0x0600148D RID: 5261
		public abstract void SetLength(long value);

		// Token: 0x0600148E RID: 5262
		public abstract void Flush();

		// Token: 0x0600148F RID: 5263 RVA: 0x0009DE34 File Offset: 0x0009D234
		public virtual void Close()
		{
			this.Dispose(true);
		}

		// Token: 0x06001490 RID: 5264 RVA: 0x0009DE48 File Offset: 0x0009D248
		void IDisposable.Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06001491 RID: 5265 RVA: 0x0009DE5C File Offset: 0x0009D25C
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06001492 RID: 5266 RVA: 0x0009DE6C File Offset: 0x0009D26C
		public virtual int ReadChar()
		{
			char[] array = new char[1];
			if (this.Read(array, 0, 1) == 0)
			{
				return -1;
			}
			return (int)array[0];
		}

		// Token: 0x06001493 RID: 5267 RVA: 0x0009DE94 File Offset: 0x0009D294
		public virtual void WriteChar(char value)
		{
			this.Write(new char[]
			{
				value
			}, 0, 1);
		}

		// Token: 0x17000310 RID: 784
		// (get) Token: 0x06001494 RID: 5268 RVA: 0x0009DEB8 File Offset: 0x0009D2B8
		public static SqlStreamChars Null
		{
			get
			{
				return new SqlStreamChars.NullSqlStreamChars();
			}
		}

		// Token: 0x02000372 RID: 882
		private class NullSqlStreamChars : SqlStreamChars
		{
			// Token: 0x06003456 RID: 13398 RVA: 0x00140C78 File Offset: 0x00140078
			internal NullSqlStreamChars()
			{
			}

			// Token: 0x1700084A RID: 2122
			// (get) Token: 0x06003457 RID: 13399 RVA: 0x00140C8C File Offset: 0x0014008C
			public override bool IsNull
			{
				get
				{
					return true;
				}
			}

			// Token: 0x1700084B RID: 2123
			// (get) Token: 0x06003458 RID: 13400 RVA: 0x00140C9C File Offset: 0x0014009C
			public override bool CanRead
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700084C RID: 2124
			// (get) Token: 0x06003459 RID: 13401 RVA: 0x00140CAC File Offset: 0x001400AC
			public override bool CanSeek
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700084D RID: 2125
			// (get) Token: 0x0600345A RID: 13402 RVA: 0x00140CBC File Offset: 0x001400BC
			public override bool CanWrite
			{
				get
				{
					return false;
				}
			}

			// Token: 0x1700084E RID: 2126
			// (get) Token: 0x0600345B RID: 13403 RVA: 0x00140CCC File Offset: 0x001400CC
			public override long Length
			{
				get
				{
					throw new SqlNullValueException();
				}
			}

			// Token: 0x1700084F RID: 2127
			// (get) Token: 0x0600345C RID: 13404 RVA: 0x00140CE0 File Offset: 0x001400E0
			// (set) Token: 0x0600345D RID: 13405 RVA: 0x00140CF4 File Offset: 0x001400F4
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

			// Token: 0x0600345E RID: 13406 RVA: 0x00140D08 File Offset: 0x00140108
			public override int Read(char[] buffer, int offset, int count)
			{
				throw new SqlNullValueException();
			}

			// Token: 0x0600345F RID: 13407 RVA: 0x00140D1C File Offset: 0x0014011C
			public override void Write(char[] buffer, int offset, int count)
			{
				throw new SqlNullValueException();
			}

			// Token: 0x06003460 RID: 13408 RVA: 0x00140D30 File Offset: 0x00140130
			public override long Seek(long offset, SeekOrigin origin)
			{
				throw new SqlNullValueException();
			}

			// Token: 0x06003461 RID: 13409 RVA: 0x00140D44 File Offset: 0x00140144
			public override void SetLength(long value)
			{
				throw new SqlNullValueException();
			}

			// Token: 0x06003462 RID: 13410 RVA: 0x00140D58 File Offset: 0x00140158
			public override void Flush()
			{
				throw new SqlNullValueException();
			}

			// Token: 0x06003463 RID: 13411 RVA: 0x00140D6C File Offset: 0x0014016C
			public override void Close()
			{
			}
		}
	}
}

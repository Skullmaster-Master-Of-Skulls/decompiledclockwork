using System;
using System.IO;

namespace System.Windows.Forms
{
	// Token: 0x02000226 RID: 550
	internal class DataStreamFromComStream : Stream
	{
		// Token: 0x060023BC RID: 9148 RVA: 0x000AAA2A File Offset: 0x000A8C2A
		public DataStreamFromComStream(UnsafeNativeMethods.IStream comStream)
		{
			this.comStream = comStream;
		}

		// Token: 0x17000822 RID: 2082
		// (get) Token: 0x060023BD RID: 9149 RVA: 0x000AAA39 File Offset: 0x000A8C39
		// (set) Token: 0x060023BE RID: 9150 RVA: 0x000AAA44 File Offset: 0x000A8C44
		public override long Position
		{
			get
			{
				return this.Seek(0L, SeekOrigin.Current);
			}
			set
			{
				this.Seek(value, SeekOrigin.Begin);
			}
		}

		// Token: 0x17000823 RID: 2083
		// (get) Token: 0x060023BF RID: 9151 RVA: 0x00013062 File Offset: 0x00011262
		public override bool CanWrite
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000824 RID: 2084
		// (get) Token: 0x060023C0 RID: 9152 RVA: 0x00013062 File Offset: 0x00011262
		public override bool CanSeek
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000825 RID: 2085
		// (get) Token: 0x060023C1 RID: 9153 RVA: 0x00013062 File Offset: 0x00011262
		public override bool CanRead
		{
			get
			{
				return true;
			}
		}

		// Token: 0x17000826 RID: 2086
		// (get) Token: 0x060023C2 RID: 9154 RVA: 0x000AAA50 File Offset: 0x000A8C50
		public override long Length
		{
			get
			{
				long position = this.Position;
				long num = this.Seek(0L, SeekOrigin.End);
				this.Position = position;
				return num - position;
			}
		}

		// Token: 0x060023C3 RID: 9155 RVA: 0x000AAA78 File Offset: 0x000A8C78
		private unsafe int _Read(void* handle, int bytes)
		{
			return this.comStream.Read((IntPtr)handle, bytes);
		}

		// Token: 0x060023C4 RID: 9156 RVA: 0x000AAA8C File Offset: 0x000A8C8C
		private unsafe int _Write(void* handle, int bytes)
		{
			return this.comStream.Write((IntPtr)handle, bytes);
		}

		// Token: 0x060023C5 RID: 9157 RVA: 0x000072B6 File Offset: 0x000054B6
		public override void Flush()
		{
		}

		// Token: 0x060023C6 RID: 9158 RVA: 0x000AAAA0 File Offset: 0x000A8CA0
		public unsafe override int Read(byte[] buffer, int index, int count)
		{
			int result = 0;
			if (count > 0 && index >= 0 && count + index <= buffer.Length)
			{
				fixed (byte[] array = buffer)
				{
					byte* ptr;
					if (buffer == null || array.Length == 0)
					{
						ptr = null;
					}
					else
					{
						ptr = &array[0];
					}
					result = this._Read((void*)(ptr + index), count);
				}
			}
			return result;
		}

		// Token: 0x060023C7 RID: 9159 RVA: 0x000AAAE5 File Offset: 0x000A8CE5
		public override void SetLength(long value)
		{
			this.comStream.SetSize(value);
		}

		// Token: 0x060023C8 RID: 9160 RVA: 0x000AAAF3 File Offset: 0x000A8CF3
		public override long Seek(long offset, SeekOrigin origin)
		{
			return this.comStream.Seek(offset, (int)origin);
		}

		// Token: 0x060023C9 RID: 9161 RVA: 0x000AAB04 File Offset: 0x000A8D04
		public unsafe override void Write(byte[] buffer, int index, int count)
		{
			int num = 0;
			if (count > 0 && index >= 0 && count + index <= buffer.Length)
			{
				try
				{
					try
					{
						fixed (byte[] array = buffer)
						{
							byte* ptr;
							if (buffer == null || array.Length == 0)
							{
								ptr = null;
							}
							else
							{
								ptr = &array[0];
							}
							num = this._Write((void*)(ptr + index), count);
						}
					}
					finally
					{
						byte[] array = null;
					}
				}
				catch
				{
				}
			}
			if (num < count)
			{
				throw new IOException(SR.GetString("DataStreamWrite"));
			}
		}

		// Token: 0x060023CA RID: 9162 RVA: 0x000AAB80 File Offset: 0x000A8D80
		protected override void Dispose(bool disposing)
		{
			try
			{
				if (disposing && this.comStream != null)
				{
					try
					{
						this.comStream.Commit(0);
					}
					catch (Exception)
					{
					}
				}
				this.comStream = null;
			}
			finally
			{
				base.Dispose(disposing);
			}
		}

		// Token: 0x060023CB RID: 9163 RVA: 0x000AABD8 File Offset: 0x000A8DD8
		~DataStreamFromComStream()
		{
			this.Dispose(false);
		}

		// Token: 0x04000EB7 RID: 3767
		private UnsafeNativeMethods.IStream comStream;
	}
}

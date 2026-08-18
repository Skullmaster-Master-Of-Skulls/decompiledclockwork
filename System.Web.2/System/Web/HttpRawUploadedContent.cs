using System;
using System.CodeDom.Compiler;
using System.IO;
using System.Security.Permissions;

namespace System.Web
{
	// Token: 0x020000A1 RID: 161
	internal class HttpRawUploadedContent : IDisposable
	{
		// Token: 0x06000A35 RID: 2613 RVA: 0x000176CC File Offset: 0x000158CC
		internal HttpRawUploadedContent(int fileThreshold, int expectedLength)
		{
			this._fileThreshold = fileThreshold;
			this._expectedLength = expectedLength;
			if (this._expectedLength >= 0 && this._expectedLength < this._fileThreshold)
			{
				this._data = new byte[this._expectedLength];
				return;
			}
			this._data = new byte[this._fileThreshold];
		}

		// Token: 0x06000A36 RID: 2614 RVA: 0x00017727 File Offset: 0x00015927
		public void Dispose()
		{
			if (this._file != null)
			{
				this._file.Dispose();
			}
		}

		// Token: 0x06000A37 RID: 2615 RVA: 0x0001773C File Offset: 0x0001593C
		internal void AddBytes(byte[] data, int offset, int length)
		{
			if (this._completed)
			{
				throw new InvalidOperationException();
			}
			if (length <= 0)
			{
				return;
			}
			if (this._file == null)
			{
				if (this._length + length <= this._data.Length)
				{
					Array.Copy(data, offset, this._data, this._length, length);
					this._length += length;
					return;
				}
				if (this._length + length <= this._fileThreshold)
				{
					byte[] array = new byte[this._fileThreshold];
					if (this._length > 0)
					{
						Array.Copy(this._data, 0, array, 0, this._length);
					}
					Array.Copy(data, offset, array, this._length, length);
					this._data = array;
					this._length += length;
					return;
				}
				this._file = new HttpRawUploadedContent.TempFile();
				this._file.AddBytes(this._data, 0, this._length);
			}
			this._file.AddBytes(data, offset, length);
			this._length += length;
		}

		// Token: 0x06000A38 RID: 2616 RVA: 0x00017839 File Offset: 0x00015A39
		internal void DoneAddingBytes()
		{
			if (this._data == null)
			{
				this._data = new byte[0];
			}
			if (this._file != null)
			{
				this._file.DoneAddingBytes();
			}
			this._completed = true;
		}

		// Token: 0x170003F2 RID: 1010
		// (get) Token: 0x06000A39 RID: 2617 RVA: 0x00017869 File Offset: 0x00015A69
		internal int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x170003F3 RID: 1011
		internal byte this[int index]
		{
			get
			{
				if (!this._completed)
				{
					throw new InvalidOperationException();
				}
				if (this._file == null)
				{
					return this._data[index];
				}
				if (index >= this._chunkOffset && index < this._chunkOffset + this._chunkLength)
				{
					return this._data[index - this._chunkOffset];
				}
				if (index < 0 || index >= this._length)
				{
					throw new ArgumentOutOfRangeException("index");
				}
				this._chunkLength = this._file.GetBytes(index, this._data.Length, this._data, 0);
				this._chunkOffset = index;
				return this._data[0];
			}
		}

		// Token: 0x06000A3B RID: 2619 RVA: 0x00017914 File Offset: 0x00015B14
		internal void CopyBytes(int offset, byte[] buffer, int bufferOffset, int length)
		{
			if (!this._completed)
			{
				throw new InvalidOperationException();
			}
			if (this._file == null)
			{
				Array.Copy(this._data, offset, buffer, bufferOffset, length);
				return;
			}
			if (offset >= this._chunkOffset && offset + length < this._chunkOffset + this._chunkLength)
			{
				Array.Copy(this._data, offset - this._chunkOffset, buffer, bufferOffset, length);
				return;
			}
			if (length <= this._data.Length)
			{
				this._chunkLength = this._file.GetBytes(offset, this._data.Length, this._data, 0);
				this._chunkOffset = offset;
				Array.Copy(this._data, offset - this._chunkOffset, buffer, bufferOffset, length);
				return;
			}
			this._file.GetBytes(offset, length, buffer, bufferOffset);
		}

		// Token: 0x06000A3C RID: 2620 RVA: 0x000179DC File Offset: 0x00015BDC
		internal void WriteBytes(int offset, int length, Stream stream)
		{
			if (!this._completed)
			{
				throw new InvalidOperationException();
			}
			if (this._file != null)
			{
				int num = offset;
				int i = length;
				byte[] buffer = new byte[(i > this._fileThreshold) ? this._fileThreshold : i];
				while (i > 0)
				{
					int length2 = (i > this._fileThreshold) ? this._fileThreshold : i;
					int bytes = this._file.GetBytes(num, length2, buffer, 0);
					if (bytes == 0)
					{
						return;
					}
					stream.Write(buffer, 0, bytes);
					num += bytes;
					i -= bytes;
				}
				return;
			}
			stream.Write(this._data, offset, length);
		}

		// Token: 0x06000A3D RID: 2621 RVA: 0x00017A6C File Offset: 0x00015C6C
		internal byte[] GetAsByteArray()
		{
			if (this._file == null && this._length == this._data.Length)
			{
				return this._data;
			}
			return this.GetAsByteArray(0, this._length);
		}

		// Token: 0x06000A3E RID: 2622 RVA: 0x00017A9C File Offset: 0x00015C9C
		internal byte[] GetAsByteArray(int offset, int length)
		{
			if (!this._completed)
			{
				throw new InvalidOperationException();
			}
			if (length == 0)
			{
				return new byte[0];
			}
			byte[] array = new byte[length];
			this.CopyBytes(offset, array, 0, length);
			return array;
		}

		// Token: 0x040003BA RID: 954
		private int _fileThreshold;

		// Token: 0x040003BB RID: 955
		private int _expectedLength;

		// Token: 0x040003BC RID: 956
		private bool _completed;

		// Token: 0x040003BD RID: 957
		private int _length;

		// Token: 0x040003BE RID: 958
		private byte[] _data;

		// Token: 0x040003BF RID: 959
		private HttpRawUploadedContent.TempFile _file;

		// Token: 0x040003C0 RID: 960
		private int _chunkOffset;

		// Token: 0x040003C1 RID: 961
		private int _chunkLength;

		// Token: 0x020008E2 RID: 2274
		private class TempFile : IDisposable
		{
			// Token: 0x06006848 RID: 26696 RVA: 0x001724C8 File Offset: 0x001706C8
			internal TempFile()
			{
				using (new ApplicationImpersonationContext())
				{
					string text = Path.Combine(HttpRuntime.CodegenDirInternal, "uploads");
					new FileIOPermission(FileIOPermissionAccess.AllAccess, text).Assert();
					if (!Directory.Exists(text))
					{
						try
						{
							Directory.CreateDirectory(text);
						}
						catch
						{
						}
					}
					this._tempFiles = new TempFileCollection(text, false);
					this._filename = this._tempFiles.AddExtension("post", false);
					this._filestream = new FileStream(this._filename, FileMode.Create, FileAccess.ReadWrite, FileShare.None, 4096, FileOptions.DeleteOnClose);
				}
			}

			// Token: 0x06006849 RID: 26697 RVA: 0x0017257C File Offset: 0x0017077C
			public void Dispose()
			{
				using (new ApplicationImpersonationContext())
				{
					try
					{
						if (this._filestream != null)
						{
							this._filestream.Close();
						}
						this._tempFiles.Delete();
						((IDisposable)this._tempFiles).Dispose();
					}
					catch
					{
					}
				}
			}

			// Token: 0x0600684A RID: 26698 RVA: 0x001725E4 File Offset: 0x001707E4
			internal void AddBytes(byte[] data, int offset, int length)
			{
				if (this._filestream == null)
				{
					throw new InvalidOperationException();
				}
				this._filestream.Write(data, offset, length);
			}

			// Token: 0x0600684B RID: 26699 RVA: 0x00172602 File Offset: 0x00170802
			internal void DoneAddingBytes()
			{
				if (this._filestream == null)
				{
					throw new InvalidOperationException();
				}
				this._filestream.Flush();
				this._filestream.Seek(0L, SeekOrigin.Begin);
			}

			// Token: 0x0600684C RID: 26700 RVA: 0x0017262C File Offset: 0x0017082C
			internal int GetBytes(int offset, int length, byte[] buffer, int bufferOffset)
			{
				if (this._filestream == null)
				{
					throw new InvalidOperationException();
				}
				this._filestream.Seek((long)offset, SeekOrigin.Begin);
				return this._filestream.Read(buffer, bufferOffset, length);
			}

			// Token: 0x04003649 RID: 13897
			private TempFileCollection _tempFiles;

			// Token: 0x0400364A RID: 13898
			private string _filename;

			// Token: 0x0400364B RID: 13899
			private Stream _filestream;
		}
	}
}

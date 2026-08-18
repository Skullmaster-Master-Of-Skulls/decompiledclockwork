using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Security;
using System.Threading;

namespace System.Reflection.Internal
{
	// Token: 0x02000082 RID: 130
	internal sealed class StreamMemoryBlockProvider : MemoryBlockProvider
	{
		// Token: 0x06000331 RID: 817 RVA: 0x00007F04 File Offset: 0x00006104
		public StreamMemoryBlockProvider(Stream stream, long imageStart, int imageSize, bool isFileStream, bool leaveOpen)
		{
			this._stream = stream;
			this._streamGuard = new object();
			this._imageStart = imageStart;
			this._imageSize = imageSize;
			this._leaveOpen = leaveOpen;
			this._isFileStream = isFileStream;
			this._useMemoryMap = (isFileStream && MemoryMapLightUp.IsAvailable);
		}

		// Token: 0x06000332 RID: 818 RVA: 0x00007F59 File Offset: 0x00006159
		protected override void Dispose(bool disposing)
		{
			if (!this._leaveOpen)
			{
				Stream stream = Interlocked.Exchange<Stream>(ref this._stream, null);
				if (stream != null)
				{
					stream.Dispose();
				}
			}
			IDisposable disposable = Interlocked.Exchange<IDisposable>(ref this._lazyMemoryMap, null);
			if (disposable == null)
			{
				return;
			}
			disposable.Dispose();
		}

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x06000333 RID: 819 RVA: 0x00007F90 File Offset: 0x00006190
		public override int Size
		{
			get
			{
				return this._imageSize;
			}
		}

		// Token: 0x06000334 RID: 820 RVA: 0x00007F98 File Offset: 0x00006198
		[SecuritySafeCritical]
		internal static NativeHeapMemoryBlock ReadMemoryBlockNoLock(Stream stream, bool isFileStream, long start, int size)
		{
			NativeHeapMemoryBlock nativeHeapMemoryBlock = new NativeHeapMemoryBlock(size);
			bool flag = true;
			try
			{
				stream.Seek(start, SeekOrigin.Begin);
				stream.CopyTo(nativeHeapMemoryBlock.Pointer, size);
				flag = false;
			}
			finally
			{
				if (flag)
				{
					nativeHeapMemoryBlock.Dispose();
				}
			}
			return nativeHeapMemoryBlock;
		}

		// Token: 0x06000335 RID: 821 RVA: 0x00007FE4 File Offset: 0x000061E4
		protected override AbstractMemoryBlock GetMemoryBlockImpl(int start, int size)
		{
			long start2 = this._imageStart + (long)start;
			if (this._useMemoryMap && size > 16384)
			{
				MemoryMappedFileBlock result;
				if (this.TryCreateMemoryMappedFileBlock(start2, size, out result))
				{
					return result;
				}
				this._useMemoryMap = false;
			}
			object streamGuard = this._streamGuard;
			AbstractMemoryBlock result2;
			lock (streamGuard)
			{
				result2 = StreamMemoryBlockProvider.ReadMemoryBlockNoLock(this._stream, this._isFileStream, start2, size);
			}
			return result2;
		}

		// Token: 0x06000336 RID: 822 RVA: 0x00008064 File Offset: 0x00006264
		public override Stream GetStream(out StreamConstraints constraints)
		{
			constraints = new StreamConstraints(this._streamGuard, this._imageStart, this._imageSize);
			return this._stream;
		}

		// Token: 0x06000337 RID: 823 RVA: 0x0000808C File Offset: 0x0000628C
		[SecuritySafeCritical]
		private bool TryCreateMemoryMappedFileBlock(long start, int size, out MemoryMappedFileBlock block)
		{
			if (this._lazyMemoryMap == null)
			{
				object streamGuard = this._streamGuard;
				IDisposable disposable;
				lock (streamGuard)
				{
					disposable = MemoryMapLightUp.CreateMemoryMap(this._stream);
				}
				if (disposable == null)
				{
					block = null;
					return false;
				}
				if (Interlocked.CompareExchange<IDisposable>(ref this._lazyMemoryMap, disposable, null) != null)
				{
					disposable.Dispose();
				}
			}
			IDisposable disposable2 = MemoryMapLightUp.CreateViewAccessor(this._lazyMemoryMap, start, size);
			if (disposable2 == null)
			{
				block = null;
				return false;
			}
			SafeBuffer safeBuffer;
			long offset;
			if (!MemoryMapLightUp.TryGetSafeBufferAndPointerOffset(disposable2, out safeBuffer, out offset))
			{
				block = null;
				return false;
			}
			block = new MemoryMappedFileBlock(disposable2, safeBuffer, offset, size);
			return true;
		}

		// Token: 0x04000488 RID: 1160
		internal const int MemoryMapThreshold = 16384;

		// Token: 0x04000489 RID: 1161
		private Stream _stream;

		// Token: 0x0400048A RID: 1162
		private readonly object _streamGuard;

		// Token: 0x0400048B RID: 1163
		private readonly bool _leaveOpen;

		// Token: 0x0400048C RID: 1164
		private bool _useMemoryMap;

		// Token: 0x0400048D RID: 1165
		private readonly bool _isFileStream;

		// Token: 0x0400048E RID: 1166
		private readonly long _imageStart;

		// Token: 0x0400048F RID: 1167
		private readonly int _imageSize;

		// Token: 0x04000490 RID: 1168
		private IDisposable _lazyMemoryMap;
	}
}

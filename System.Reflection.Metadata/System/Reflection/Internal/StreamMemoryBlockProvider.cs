using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;

namespace System.Reflection.Internal
{
	// Token: 0x0200015B RID: 347
	internal sealed class StreamMemoryBlockProvider : MemoryBlockProvider
	{
		// Token: 0x06000AD0 RID: 2768 RVA: 0x0001EBB8 File Offset: 0x0001CDB8
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

		// Token: 0x06000AD1 RID: 2769 RVA: 0x0001EC0D File Offset: 0x0001CE0D
		protected override void Dispose(bool disposing)
		{
			if (!this._leaveOpen && this._stream != null)
			{
				this._stream.Dispose();
				this._stream = null;
			}
			if (this._lazyMemoryMap != null)
			{
				this._lazyMemoryMap.Dispose();
				this._lazyMemoryMap = null;
			}
		}

		// Token: 0x170002B2 RID: 690
		// (get) Token: 0x06000AD2 RID: 2770 RVA: 0x0001EC4B File Offset: 0x0001CE4B
		public override int Size
		{
			get
			{
				return this._imageSize;
			}
		}

		// Token: 0x06000AD3 RID: 2771 RVA: 0x0001EC54 File Offset: 0x0001CE54
		internal static NativeHeapMemoryBlock ReadMemoryBlockNoLock(Stream stream, bool isFileStream, long start, int size)
		{
			NativeHeapMemoryBlock nativeHeapMemoryBlock = new NativeHeapMemoryBlock(size);
			bool flag = true;
			try
			{
				stream.Seek(start, SeekOrigin.Begin);
				if (!isFileStream || !FileStreamReadLightUp.TryReadFile(stream, nativeHeapMemoryBlock.Pointer, start, size))
				{
					stream.CopyTo(nativeHeapMemoryBlock.Pointer, size);
				}
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

		// Token: 0x06000AD4 RID: 2772 RVA: 0x0001ECB4 File Offset: 0x0001CEB4
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

		// Token: 0x06000AD5 RID: 2773 RVA: 0x0001ED34 File Offset: 0x0001CF34
		public override Stream GetStream(out StreamConstraints constraints)
		{
			constraints = new StreamConstraints(this._streamGuard, this._imageStart, this._imageSize);
			return this._stream;
		}

		// Token: 0x06000AD6 RID: 2774 RVA: 0x0001ED5C File Offset: 0x0001CF5C
		private unsafe bool TryCreateMemoryMappedFileBlock(long start, int size, out MemoryMappedFileBlock block)
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
			byte* ptr = MemoryMapLightUp.AcquirePointer(disposable2, out safeBuffer);
			if (ptr == null)
			{
				block = null;
				return false;
			}
			block = new MemoryMappedFileBlock(disposable2, safeBuffer, ptr, size);
			return true;
		}

		// Token: 0x04000900 RID: 2304
		internal const int MemoryMapThreshold = 16384;

		// Token: 0x04000901 RID: 2305
		private Stream _stream;

		// Token: 0x04000902 RID: 2306
		private readonly object _streamGuard;

		// Token: 0x04000903 RID: 2307
		private readonly bool _leaveOpen;

		// Token: 0x04000904 RID: 2308
		private bool _useMemoryMap;

		// Token: 0x04000905 RID: 2309
		private readonly bool _isFileStream;

		// Token: 0x04000906 RID: 2310
		private readonly long _imageStart;

		// Token: 0x04000907 RID: 2311
		private readonly int _imageSize;

		// Token: 0x04000908 RID: 2312
		private IDisposable _lazyMemoryMap;
	}
}

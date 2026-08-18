using System;
using System.Collections;
using System.IO;
using System.Text;
using System.Threading;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000CA RID: 202
	public sealed class HttpWriter : TextWriter
	{
		// Token: 0x06000DA4 RID: 3492 RVA: 0x000260CC File Offset: 0x000242CC
		internal HttpWriter(HttpResponse response) : base(null)
		{
			this._response = response;
			this._stream = new HttpResponseStream(this);
			this._buffers = new ArrayList();
			this._lastBuffer = null;
			this._charBuffer = null;
			this._charBufferLength = 0;
			this._charBufferFree = 0;
			this.UpdateResponseBuffering();
		}

		// Token: 0x170004D8 RID: 1240
		// (get) Token: 0x06000DA5 RID: 3493 RVA: 0x00026120 File Offset: 0x00024320
		internal ArrayList SubstElements
		{
			get
			{
				if (this._substElements == null)
				{
					this._substElements = new ArrayList();
					this._response.Context.Request.SetDynamicCompression(false);
				}
				return this._substElements;
			}
		}

		// Token: 0x170004D9 RID: 1241
		// (get) Token: 0x06000DA6 RID: 3494 RVA: 0x00026151 File Offset: 0x00024351
		internal bool IgnoringFurtherWrites
		{
			get
			{
				return this._ignoringFurtherWrites;
			}
		}

		// Token: 0x06000DA7 RID: 3495 RVA: 0x00026159 File Offset: 0x00024359
		internal void IgnoreFurtherWrites()
		{
			this._ignoringFurtherWrites = true;
		}

		// Token: 0x06000DA8 RID: 3496 RVA: 0x00026162 File Offset: 0x00024362
		internal void UpdateResponseBuffering()
		{
			this._responseBufferingOn = this._response.BufferOutput;
		}

		// Token: 0x06000DA9 RID: 3497 RVA: 0x00026178 File Offset: 0x00024378
		internal void UpdateResponseEncoding()
		{
			if (this._responseEncodingUpdated && this._charBufferLength != this._charBufferFree)
			{
				this.FlushCharBuffer(true);
			}
			this._responseEncoding = this._response.ContentEncoding;
			this._responseEncoder = this._response.ContentEncoder;
			this._responseCodePage = this._responseEncoding.CodePage;
			this._responseCodePageIsAsciiCompat = CodePageUtils.IsAsciiCompatibleCodePage(this._responseCodePage);
			this._responseEncodingUpdated = true;
		}

		// Token: 0x170004DA RID: 1242
		// (get) Token: 0x06000DAA RID: 3498 RVA: 0x000261ED File Offset: 0x000243ED
		public override Encoding Encoding
		{
			get
			{
				if (!this._responseEncodingUpdated)
				{
					this.UpdateResponseEncoding();
				}
				return this._responseEncoding;
			}
		}

		// Token: 0x170004DB RID: 1243
		// (get) Token: 0x06000DAB RID: 3499 RVA: 0x00026203 File Offset: 0x00024403
		internal Encoder Encoder
		{
			get
			{
				if (!this._responseEncodingUpdated)
				{
					this.UpdateResponseEncoding();
				}
				return this._responseEncoder;
			}
		}

		// Token: 0x06000DAC RID: 3500 RVA: 0x00026219 File Offset: 0x00024419
		private HttpBaseMemoryResponseBufferElement CreateNewMemoryBufferElement()
		{
			return new HttpResponseUnmanagedBufferElement();
		}

		// Token: 0x06000DAD RID: 3501 RVA: 0x00026220 File Offset: 0x00024420
		internal void DisposeIntegratedBuffers()
		{
			if (this._buffers != null)
			{
				int count = this._buffers.Count;
				for (int i = 0; i < count; i++)
				{
					HttpBaseMemoryResponseBufferElement httpBaseMemoryResponseBufferElement = this._buffers[i] as HttpBaseMemoryResponseBufferElement;
					if (httpBaseMemoryResponseBufferElement != null)
					{
						httpBaseMemoryResponseBufferElement.Recycle();
					}
				}
				this._buffers = null;
			}
			this.ClearBuffers();
		}

		// Token: 0x06000DAE RID: 3502 RVA: 0x00026275 File Offset: 0x00024475
		internal void RecycleBuffers()
		{
			if (this._charBuffer != null)
			{
				this.AllocatorProvider.CharBufferAllocator.ReuseBuffer(this._charBuffer);
				this._charBuffer = null;
			}
			this.RecycleBufferElements();
		}

		// Token: 0x06000DAF RID: 3503 RVA: 0x000262A2 File Offset: 0x000244A2
		internal static void ReleaseAllPooledBuffers()
		{
			if (HttpWriter.s_DefaultAllocator != null)
			{
				HttpWriter.s_DefaultAllocator.TrimMemory();
			}
		}

		// Token: 0x06000DB0 RID: 3504 RVA: 0x000262B5 File Offset: 0x000244B5
		internal void ClearSubstitutionBlocks()
		{
			this._substElements = null;
		}

		// Token: 0x170004DC RID: 1244
		// (get) Token: 0x06000DB1 RID: 3505 RVA: 0x000262C0 File Offset: 0x000244C0
		// (set) Token: 0x06000DB2 RID: 3506 RVA: 0x00026319 File Offset: 0x00024519
		internal IAllocatorProvider AllocatorProvider
		{
			private get
			{
				if (this._allocator == null)
				{
					if (HttpWriter.s_DefaultAllocator == null)
					{
						IBufferAllocator allocator = new CharBufferAllocator(1024, 64);
						Interlocked.CompareExchange<IAllocatorProvider>(ref HttpWriter.s_DefaultAllocator, new AllocatorProvider
						{
							CharBufferAllocator = new BufferAllocatorWrapper<char>(allocator)
						}, null);
					}
					this._allocator = HttpWriter.s_DefaultAllocator;
				}
				return this._allocator;
			}
			set
			{
				this._allocator = value;
			}
		}

		// Token: 0x06000DB3 RID: 3507 RVA: 0x00026324 File Offset: 0x00024524
		private void RecycleBufferElements()
		{
			if (this._buffers != null)
			{
				int count = this._buffers.Count;
				for (int i = 0; i < count; i++)
				{
					HttpBaseMemoryResponseBufferElement httpBaseMemoryResponseBufferElement = this._buffers[i] as HttpBaseMemoryResponseBufferElement;
					if (httpBaseMemoryResponseBufferElement != null)
					{
						httpBaseMemoryResponseBufferElement.Recycle();
					}
				}
				this._buffers = null;
			}
		}

		// Token: 0x06000DB4 RID: 3508 RVA: 0x00026373 File Offset: 0x00024573
		private void ClearCharBuffer()
		{
			this._charBufferFree = this._charBufferLength;
		}

		// Token: 0x170004DD RID: 1245
		// (get) Token: 0x06000DB5 RID: 3509 RVA: 0x00026381 File Offset: 0x00024581
		private char[] CharBuffer
		{
			get
			{
				if (this._charBuffer == null)
				{
					this._charBuffer = this.AllocatorProvider.CharBufferAllocator.GetBuffer();
					this._charBufferLength = this._charBuffer.Length;
					this._charBufferFree = this._charBufferLength;
				}
				return this._charBuffer;
			}
		}

		// Token: 0x06000DB6 RID: 3510 RVA: 0x000263C4 File Offset: 0x000245C4
		private void FlushCharBuffer(bool flushEncoder)
		{
			int num = this._charBufferLength - this._charBufferFree;
			if (!this._responseEncodingUpdated)
			{
				this.UpdateResponseEncoding();
			}
			this._responseEncodingUsed = true;
			int maxByteCount = this._responseEncoding.GetMaxByteCount(num);
			if (maxByteCount <= 128 || !this._responseBufferingOn)
			{
				byte[] array = new byte[maxByteCount];
				int bytes = this._responseEncoder.GetBytes(this.CharBuffer, 0, num, array, 0, flushEncoder);
				this.BufferData(array, 0, bytes, false);
			}
			else
			{
				int num2 = (this._lastBuffer != null) ? this._lastBuffer.FreeBytes : 0;
				if (num2 < maxByteCount)
				{
					this._lastBuffer = this.CreateNewMemoryBufferElement();
					this._buffers.Add(this._lastBuffer);
					num2 = this._lastBuffer.FreeBytes;
				}
				this._lastBuffer.AppendEncodedChars(this.CharBuffer, 0, num, this._responseEncoder, flushEncoder);
			}
			this._charBufferFree = this._charBufferLength;
		}

		// Token: 0x06000DB7 RID: 3511 RVA: 0x000264AC File Offset: 0x000246AC
		private void BufferData(byte[] data, int offset, int size, bool needToCopyData)
		{
			if (this._lastBuffer != null)
			{
				int num = this._lastBuffer.Append(data, offset, size);
				size -= num;
				offset += num;
			}
			else if (!needToCopyData && offset == 0 && !this._responseBufferingOn)
			{
				this._buffers.Add(new HttpResponseBufferElement(data, size));
				return;
			}
			while (size > 0)
			{
				this._lastBuffer = this.CreateNewMemoryBufferElement();
				this._buffers.Add(this._lastBuffer);
				int num = this._lastBuffer.Append(data, offset, size);
				offset += num;
				size -= num;
			}
		}

		// Token: 0x06000DB8 RID: 3512 RVA: 0x0002653C File Offset: 0x0002473C
		private void BufferResource(IntPtr data, int offset, int size)
		{
			if (size > 4096 || !this._responseBufferingOn)
			{
				this._lastBuffer = null;
				this._buffers.Add(new HttpResourceResponseElement(data, offset, size));
				return;
			}
			if (this._lastBuffer != null)
			{
				int num = this._lastBuffer.Append(data, offset, size);
				size -= num;
				offset += num;
			}
			while (size > 0)
			{
				this._lastBuffer = this.CreateNewMemoryBufferElement();
				this._buffers.Add(this._lastBuffer);
				int num = this._lastBuffer.Append(data, offset, size);
				offset += num;
				size -= num;
			}
		}

		// Token: 0x06000DB9 RID: 3513 RVA: 0x000265D3 File Offset: 0x000247D3
		internal void WriteFromStream(byte[] data, int offset, int size)
		{
			if (this._charBufferLength != this._charBufferFree)
			{
				this.FlushCharBuffer(true);
			}
			this.BufferData(data, offset, size, true);
			if (!this._responseBufferingOn)
			{
				this._response.Flush();
			}
		}

		// Token: 0x06000DBA RID: 3514 RVA: 0x00026608 File Offset: 0x00024808
		internal void WriteUTF8ResourceString(IntPtr pv, int offset, int size, bool asciiOnly)
		{
			if (!this._responseEncodingUpdated)
			{
				this.UpdateResponseEncoding();
			}
			if (this._responseCodePage == 65001 || (asciiOnly && this._responseCodePageIsAsciiCompat))
			{
				this._responseEncodingUsed = true;
				if (this._charBufferLength != this._charBufferFree)
				{
					this.FlushCharBuffer(true);
				}
				this.BufferResource(pv, offset, size);
				if (!this._responseBufferingOn)
				{
					this._response.Flush();
					return;
				}
			}
			else
			{
				this.Write(StringResourceManager.ResourceToString(pv, offset, size));
			}
		}

		// Token: 0x06000DBB RID: 3515 RVA: 0x00026684 File Offset: 0x00024884
		internal void TransmitFile(string filename, long offset, long size, bool isImpersonating, bool supportsLongTransmitFile)
		{
			if (this._charBufferLength != this._charBufferFree)
			{
				this.FlushCharBuffer(true);
			}
			this._lastBuffer = null;
			this._buffers.Add(new HttpFileResponseElement(filename, offset, size, isImpersonating, supportsLongTransmitFile));
			if (!this._responseBufferingOn)
			{
				this._response.Flush();
			}
		}

		// Token: 0x06000DBC RID: 3516 RVA: 0x000266D8 File Offset: 0x000248D8
		internal void WriteFile(string filename, long offset, long size)
		{
			if (this._charBufferLength != this._charBufferFree)
			{
				this.FlushCharBuffer(true);
			}
			this._lastBuffer = null;
			this._buffers.Add(new HttpFileResponseElement(filename, offset, size));
			if (!this._responseBufferingOn)
			{
				this._response.Flush();
			}
		}

		// Token: 0x06000DBD RID: 3517 RVA: 0x00026728 File Offset: 0x00024928
		internal void WriteSubstBlock(HttpResponseSubstitutionCallback callback, IIS7WorkerRequest iis7WorkerRequest)
		{
			if (this._charBufferLength != this._charBufferFree)
			{
				this.FlushCharBuffer(true);
			}
			this._lastBuffer = null;
			IHttpResponseElement value = new HttpSubstBlockResponseElement(callback, this.Encoding, this.Encoder, iis7WorkerRequest);
			this._buffers.Add(value);
			if (iis7WorkerRequest != null)
			{
				this.SubstElements.Add(value);
			}
			if (!this._responseBufferingOn)
			{
				this._response.Flush();
			}
		}

		// Token: 0x170004DE RID: 1246
		// (get) Token: 0x06000DBE RID: 3518 RVA: 0x00026795 File Offset: 0x00024995
		// (set) Token: 0x06000DBF RID: 3519 RVA: 0x0002679D File Offset: 0x0002499D
		internal bool HasBeenClearedRecently
		{
			get
			{
				return this._hasBeenClearedRecently;
			}
			set
			{
				this._hasBeenClearedRecently = value;
			}
		}

		// Token: 0x06000DC0 RID: 3520 RVA: 0x000267A6 File Offset: 0x000249A6
		internal int GetResponseBufferCountAfterFlush()
		{
			if (this._charBufferLength != this._charBufferFree)
			{
				this.FlushCharBuffer(true);
			}
			this._lastBuffer = null;
			return this._buffers.Count;
		}

		// Token: 0x06000DC1 RID: 3521 RVA: 0x000267D0 File Offset: 0x000249D0
		internal void MoveResponseBufferRangeForward(int srcIndex, int srcCount, int dstIndex)
		{
			if (srcCount > 0)
			{
				object[] array = new object[srcIndex - dstIndex];
				this._buffers.CopyTo(dstIndex, array, 0, array.Length);
				for (int i = 0; i < srcCount; i++)
				{
					this._buffers[dstIndex + i] = this._buffers[srcIndex + i];
				}
				for (int j = 0; j < array.Length; j++)
				{
					this._buffers[dstIndex + srcCount + j] = array[j];
				}
			}
			HttpBaseMemoryResponseBufferElement httpBaseMemoryResponseBufferElement = this._buffers[this._buffers.Count - 1] as HttpBaseMemoryResponseBufferElement;
			if (httpBaseMemoryResponseBufferElement != null && httpBaseMemoryResponseBufferElement.FreeBytes > 0)
			{
				this._lastBuffer = httpBaseMemoryResponseBufferElement;
				return;
			}
			this._lastBuffer = null;
		}

		// Token: 0x06000DC2 RID: 3522 RVA: 0x00026880 File Offset: 0x00024A80
		internal void ClearBuffers()
		{
			this.ClearCharBuffer();
			if (this._substElements != null)
			{
				this._response.Context.Request.SetDynamicCompression(true);
			}
			this.RecycleBufferElements();
			this._buffers = new ArrayList();
			this._lastBuffer = null;
			this._hasBeenClearedRecently = true;
		}

		// Token: 0x06000DC3 RID: 3523 RVA: 0x000268D0 File Offset: 0x00024AD0
		internal long GetBufferedLength()
		{
			if (this._charBufferLength != this._charBufferFree)
			{
				this.FlushCharBuffer(true);
			}
			long num = 0L;
			if (this._buffers != null)
			{
				int count = this._buffers.Count;
				for (int i = 0; i < count; i++)
				{
					num += ((IHttpResponseElement)this._buffers[i]).GetSize();
				}
			}
			return num;
		}

		// Token: 0x170004DF RID: 1247
		// (get) Token: 0x06000DC4 RID: 3524 RVA: 0x0002692F File Offset: 0x00024B2F
		internal bool ResponseEncodingUsed
		{
			get
			{
				return this._responseEncodingUsed;
			}
		}

		// Token: 0x06000DC5 RID: 3525 RVA: 0x00026938 File Offset: 0x00024B38
		internal ArrayList GetIntegratedSnapshot(out bool hasSubstBlocks, IIS7WorkerRequest wr)
		{
			ArrayList snapshot = this.GetSnapshot(out hasSubstBlocks);
			ArrayList bufferedResponseChunks = wr.GetBufferedResponseChunks(true, this._substElements, ref hasSubstBlocks);
			ArrayList arrayList;
			if (bufferedResponseChunks != null)
			{
				for (int i = 0; i < snapshot.Count; i++)
				{
					bufferedResponseChunks.Add(snapshot[i]);
				}
				arrayList = bufferedResponseChunks;
			}
			else
			{
				arrayList = snapshot;
			}
			if (this._substElements != null && this._substElements.Count > 0)
			{
				int num = 0;
				for (int j = 0; j < arrayList.Count; j++)
				{
					if (arrayList[j] is HttpSubstBlockResponseElement)
					{
						num++;
						if (num == this._substElements.Count)
						{
							break;
						}
					}
				}
				if (num != this._substElements.Count)
				{
					throw new InvalidOperationException(SR.GetString("Substitution_blocks_cannot_be_modified"));
				}
				this._response.Context.Request.SetDynamicCompression(true);
			}
			return arrayList;
		}

		// Token: 0x06000DC6 RID: 3526 RVA: 0x00026A10 File Offset: 0x00024C10
		internal ArrayList GetSnapshot(out bool hasSubstBlocks)
		{
			if (this._charBufferLength != this._charBufferFree)
			{
				this.FlushCharBuffer(true);
			}
			this._lastBuffer = null;
			hasSubstBlocks = false;
			ArrayList arrayList = new ArrayList();
			int count = this._buffers.Count;
			for (int i = 0; i < count; i++)
			{
				object obj = this._buffers[i];
				HttpBaseMemoryResponseBufferElement httpBaseMemoryResponseBufferElement = obj as HttpBaseMemoryResponseBufferElement;
				if (httpBaseMemoryResponseBufferElement != null)
				{
					if (httpBaseMemoryResponseBufferElement.FreeBytes > 4096)
					{
						obj = httpBaseMemoryResponseBufferElement.Clone();
					}
					else
					{
						httpBaseMemoryResponseBufferElement.DisableRecycling();
					}
				}
				else if (obj is HttpSubstBlockResponseElement)
				{
					hasSubstBlocks = true;
				}
				arrayList.Add(obj);
			}
			return arrayList;
		}

		// Token: 0x06000DC7 RID: 3527 RVA: 0x00026AA8 File Offset: 0x00024CA8
		internal void UseSnapshot(ArrayList buffers)
		{
			this.ClearBuffers();
			int count = buffers.Count;
			for (int i = 0; i < count; i++)
			{
				object obj = buffers[i];
				HttpSubstBlockResponseElement httpSubstBlockResponseElement = obj as HttpSubstBlockResponseElement;
				if (httpSubstBlockResponseElement != null)
				{
					this._buffers.Add(httpSubstBlockResponseElement.Substitute(this.Encoding));
				}
				else
				{
					this._buffers.Add(obj);
				}
			}
		}

		// Token: 0x06000DC8 RID: 3528 RVA: 0x00026B07 File Offset: 0x00024D07
		internal Stream GetCurrentFilter()
		{
			if (this._installedFilter != null)
			{
				return this._installedFilter;
			}
			if (this._filterSink == null)
			{
				this._filterSink = new HttpResponseStreamFilterSink(this);
			}
			return this._filterSink;
		}

		// Token: 0x170004E0 RID: 1248
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x00026B32 File Offset: 0x00024D32
		internal bool FilterInstalled
		{
			get
			{
				return this._installedFilter != null;
			}
		}

		// Token: 0x06000DCA RID: 3530 RVA: 0x00026B3D File Offset: 0x00024D3D
		internal void InstallFilter(Stream filter)
		{
			if (this._filterSink == null)
			{
				throw new HttpException(SR.GetString("Invalid_response_filter"));
			}
			this._installedFilter = filter;
		}

		// Token: 0x06000DCB RID: 3531 RVA: 0x00026B60 File Offset: 0x00024D60
		internal void Filter(bool finalFiltering)
		{
			if (this._installedFilter == null)
			{
				return;
			}
			if (this._charBufferLength != this._charBufferFree)
			{
				this.FlushCharBuffer(true);
			}
			this._lastBuffer = null;
			if (this._buffers.Count == 0 && !finalFiltering)
			{
				return;
			}
			ArrayList buffers = this._buffers;
			this._buffers = new ArrayList();
			this._filterSink.Filtering = true;
			try
			{
				int count = buffers.Count;
				for (int i = 0; i < count; i++)
				{
					IHttpResponseElement httpResponseElement = (IHttpResponseElement)buffers[i];
					long size = httpResponseElement.GetSize();
					if (size > 0L)
					{
						this._installedFilter.Write(httpResponseElement.GetBytes(), 0, Convert.ToInt32(size));
					}
				}
				this._installedFilter.Flush();
			}
			finally
			{
				try
				{
					if (finalFiltering)
					{
						this._installedFilter.Close();
					}
				}
				finally
				{
					this._filterSink.Filtering = false;
				}
			}
		}

		// Token: 0x06000DCC RID: 3532 RVA: 0x00026C50 File Offset: 0x00024E50
		internal void FilterIntegrated(bool finalFiltering, IIS7WorkerRequest wr)
		{
			if (this._installedFilter == null)
			{
				return;
			}
			if (this._charBufferLength != this._charBufferFree)
			{
				this.FlushCharBuffer(true);
			}
			this._lastBuffer = null;
			ArrayList buffers = this._buffers;
			this._buffers = new ArrayList();
			bool flag = false;
			ArrayList bufferedResponseChunks = wr.GetBufferedResponseChunks(false, null, ref flag);
			this._filterSink.Filtering = true;
			try
			{
				if (bufferedResponseChunks != null)
				{
					for (int i = 0; i < bufferedResponseChunks.Count; i++)
					{
						IHttpResponseElement httpResponseElement = (IHttpResponseElement)bufferedResponseChunks[i];
						long size = httpResponseElement.GetSize();
						if (size > 0L)
						{
							this._installedFilter.Write(httpResponseElement.GetBytes(), 0, Convert.ToInt32(size));
						}
					}
					wr.ClearResponse(true, false);
				}
				if (buffers != null)
				{
					for (int j = 0; j < buffers.Count; j++)
					{
						IHttpResponseElement httpResponseElement2 = (IHttpResponseElement)buffers[j];
						long size2 = httpResponseElement2.GetSize();
						if (size2 > 0L)
						{
							this._installedFilter.Write(httpResponseElement2.GetBytes(), 0, Convert.ToInt32(size2));
						}
					}
				}
				this._installedFilter.Flush();
			}
			finally
			{
				try
				{
					if (finalFiltering)
					{
						this._installedFilter.Close();
					}
				}
				finally
				{
					this._filterSink.Filtering = false;
				}
			}
		}

		// Token: 0x06000DCD RID: 3533 RVA: 0x00026D9C File Offset: 0x00024F9C
		internal void Send(HttpWorkerRequest wr)
		{
			if (this._charBufferLength != this._charBufferFree)
			{
				this.FlushCharBuffer(true);
			}
			int count = this._buffers.Count;
			if (count > 0)
			{
				for (int i = 0; i < count; i++)
				{
					((IHttpResponseElement)this._buffers[i]).Send(wr);
				}
			}
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00006164 File Offset: 0x00004364
		public override void Close()
		{
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00006164 File Offset: 0x00004364
		public override void Flush()
		{
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x00026DF4 File Offset: 0x00024FF4
		public override void Write(char ch)
		{
			if (this._ignoringFurtherWrites)
			{
				return;
			}
			char[] charBuffer = this.CharBuffer;
			if (this._charBufferFree == 0)
			{
				this.FlushCharBuffer(false);
			}
			charBuffer[this._charBufferLength - this._charBufferFree] = ch;
			this._charBufferFree--;
			if (!this._responseBufferingOn)
			{
				this._response.Flush();
			}
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x00026E54 File Offset: 0x00025054
		public override void Write(char[] buffer, int index, int count)
		{
			if (this._ignoringFurtherWrites)
			{
				return;
			}
			if (buffer == null)
			{
				throw new ArgumentNullException("buffer");
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (buffer.Length - index < count)
			{
				throw new ArgumentException(SR.GetString("InvalidOffsetOrCount", new object[]
				{
					"index",
					"count"
				}));
			}
			if (count == 0)
			{
				return;
			}
			char[] charBuffer = this.CharBuffer;
			while (count > 0)
			{
				if (this._charBufferFree == 0)
				{
					this.FlushCharBuffer(false);
				}
				int num = (count < this._charBufferFree) ? count : this._charBufferFree;
				Array.Copy(buffer, index, charBuffer, this._charBufferLength - this._charBufferFree, num);
				this._charBufferFree -= num;
				index += num;
				count -= num;
			}
			if (!this._responseBufferingOn)
			{
				this._response.Flush();
			}
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x00026F38 File Offset: 0x00025138
		public override void Write(string s)
		{
			if (this._ignoringFurtherWrites)
			{
				return;
			}
			if (s == null)
			{
				return;
			}
			char[] charBuffer = this.CharBuffer;
			if (s.Length != 0)
			{
				if (s.Length < this._charBufferFree)
				{
					StringUtil.UnsafeStringCopy(s, 0, charBuffer, this._charBufferLength - this._charBufferFree, s.Length);
					this._charBufferFree -= s.Length;
				}
				else
				{
					int i = s.Length;
					int num = 0;
					while (i > 0)
					{
						if (this._charBufferFree == 0)
						{
							this.FlushCharBuffer(false);
						}
						int num2 = (i < this._charBufferFree) ? i : this._charBufferFree;
						StringUtil.UnsafeStringCopy(s, num, charBuffer, this._charBufferLength - this._charBufferFree, num2);
						this._charBufferFree -= num2;
						num += num2;
						i -= num2;
					}
				}
			}
			if (!this._responseBufferingOn)
			{
				this._response.Flush();
			}
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00027014 File Offset: 0x00025214
		public void WriteString(string s, int index, int count)
		{
			if (s == null)
			{
				return;
			}
			if (index < 0)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (count < 0)
			{
				throw new ArgumentOutOfRangeException("count");
			}
			if (index + count > s.Length)
			{
				throw new ArgumentOutOfRangeException("index");
			}
			if (this._ignoringFurtherWrites)
			{
				return;
			}
			char[] charBuffer = this.CharBuffer;
			if (count != 0)
			{
				if (count < this._charBufferFree)
				{
					StringUtil.UnsafeStringCopy(s, index, charBuffer, this._charBufferLength - this._charBufferFree, count);
					this._charBufferFree -= count;
				}
				else
				{
					while (count > 0)
					{
						if (this._charBufferFree == 0)
						{
							this.FlushCharBuffer(false);
						}
						int num = (count < this._charBufferFree) ? count : this._charBufferFree;
						StringUtil.UnsafeStringCopy(s, index, charBuffer, this._charBufferLength - this._charBufferFree, num);
						this._charBufferFree -= num;
						index += num;
						count -= num;
					}
				}
			}
			if (!this._responseBufferingOn)
			{
				this._response.Flush();
			}
		}

		// Token: 0x06000DD4 RID: 3540 RVA: 0x00027105 File Offset: 0x00025305
		public override void Write(object obj)
		{
			if (this._ignoringFurtherWrites)
			{
				return;
			}
			if (obj != null)
			{
				this.Write(obj.ToString());
			}
		}

		// Token: 0x06000DD5 RID: 3541 RVA: 0x0002711F File Offset: 0x0002531F
		public void WriteBytes(byte[] buffer, int index, int count)
		{
			if (this._ignoringFurtherWrites)
			{
				return;
			}
			this.WriteFromStream(buffer, index, count);
		}

		// Token: 0x06000DD6 RID: 3542 RVA: 0x00027134 File Offset: 0x00025334
		public override void WriteLine()
		{
			if (this._ignoringFurtherWrites)
			{
				return;
			}
			char[] charBuffer = this.CharBuffer;
			if (this._charBufferFree < 2)
			{
				this.FlushCharBuffer(false);
			}
			int num = this._charBufferLength - this._charBufferFree;
			charBuffer[num] = '\r';
			charBuffer[num + 1] = '\n';
			this._charBufferFree -= 2;
			if (!this._responseBufferingOn)
			{
				this._response.Flush();
			}
		}

		// Token: 0x170004E1 RID: 1249
		// (get) Token: 0x06000DD7 RID: 3543 RVA: 0x0002719C File Offset: 0x0002539C
		public Stream OutputStream
		{
			get
			{
				return this._stream;
			}
		}

		// Token: 0x0400050A RID: 1290
		private HttpResponse _response;

		// Token: 0x0400050B RID: 1291
		private HttpResponseStream _stream;

		// Token: 0x0400050C RID: 1292
		private HttpResponseStreamFilterSink _filterSink;

		// Token: 0x0400050D RID: 1293
		private Stream _installedFilter;

		// Token: 0x0400050E RID: 1294
		private HttpBaseMemoryResponseBufferElement _lastBuffer;

		// Token: 0x0400050F RID: 1295
		private ArrayList _buffers;

		// Token: 0x04000510 RID: 1296
		private char[] _charBuffer;

		// Token: 0x04000511 RID: 1297
		private int _charBufferLength;

		// Token: 0x04000512 RID: 1298
		private int _charBufferFree;

		// Token: 0x04000513 RID: 1299
		private ArrayList _substElements;

		// Token: 0x04000514 RID: 1300
		private static IAllocatorProvider s_DefaultAllocator;

		// Token: 0x04000515 RID: 1301
		private IAllocatorProvider _allocator;

		// Token: 0x04000516 RID: 1302
		private bool _responseBufferingOn;

		// Token: 0x04000517 RID: 1303
		private Encoding _responseEncoding;

		// Token: 0x04000518 RID: 1304
		private bool _responseEncodingUsed;

		// Token: 0x04000519 RID: 1305
		private bool _responseEncodingUpdated;

		// Token: 0x0400051A RID: 1306
		private Encoder _responseEncoder;

		// Token: 0x0400051B RID: 1307
		private int _responseCodePage;

		// Token: 0x0400051C RID: 1308
		private bool _responseCodePageIsAsciiCompat;

		// Token: 0x0400051D RID: 1309
		private bool _ignoringFurtherWrites;

		// Token: 0x0400051E RID: 1310
		private bool _hasBeenClearedRecently;
	}
}

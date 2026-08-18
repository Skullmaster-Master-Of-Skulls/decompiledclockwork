using System;
using System.Runtime.CompilerServices;
using System.Text;
using System.Web.Hosting;
using System.Web.Util;

namespace System.Web
{
	// Token: 0x020000C7 RID: 199
	internal sealed class HttpSubstBlockResponseElement : IHttpResponseElement
	{
		// Token: 0x170004D1 RID: 1233
		// (get) Token: 0x06000D87 RID: 3463 RVA: 0x00025D92 File Offset: 0x00023F92
		internal HttpResponseSubstitutionCallback Callback
		{
			get
			{
				return this._callback;
			}
		}

		// Token: 0x06000D88 RID: 3464 RVA: 0x00025D9C File Offset: 0x00023F9C
		internal HttpSubstBlockResponseElement(HttpResponseSubstitutionCallback callback, Encoding encoding, Encoder encoder, IIS7WorkerRequest iis7WorkerRequest)
		{
			this._callback = callback;
			if (iis7WorkerRequest == null)
			{
				this._firstSubstitution = this.Substitute(encoding);
				return;
			}
			this._isIIS7WorkerRequest = true;
			string text = this._callback(HttpContext.Current);
			if (text == null)
			{
				throw new ArgumentNullException("substitutionString");
			}
			this.CreateFirstSubstData(text, iis7WorkerRequest, encoder);
		}

		// Token: 0x06000D89 RID: 3465 RVA: 0x00025DF8 File Offset: 0x00023FF8
		internal HttpSubstBlockResponseElement(HttpResponseSubstitutionCallback callback)
		{
			this._callback = callback;
		}

		// Token: 0x06000D8A RID: 3466 RVA: 0x00025E08 File Offset: 0x00024008
		private unsafe void CreateFirstSubstData(string s, IIS7WorkerRequest iis7WorkerRequest, Encoder encoder)
		{
			int firstSubstDataSize = 0;
			int length = s.Length;
			IntPtr intPtr;
			if (length > 0)
			{
				fixed (string text = s)
				{
					char* ptr = text;
					if (ptr != null)
					{
						ptr += RuntimeHelpers.OffsetToStringData / 2;
					}
					int byteCount = encoder.GetByteCount(ptr, length, true);
					intPtr = iis7WorkerRequest.AllocateRequestMemory(byteCount);
					if (intPtr != IntPtr.Zero)
					{
						firstSubstDataSize = encoder.GetBytes(ptr, length, (byte*)((void*)intPtr), byteCount, true);
					}
				}
			}
			else
			{
				intPtr = iis7WorkerRequest.AllocateRequestMemory(1);
			}
			if (intPtr == IntPtr.Zero)
			{
				throw new OutOfMemoryException();
			}
			this._firstSubstData = intPtr;
			this._firstSubstDataSize = firstSubstDataSize;
		}

		// Token: 0x06000D8B RID: 3467 RVA: 0x00025E98 File Offset: 0x00024098
		internal IHttpResponseElement Substitute(Encoding e)
		{
			string s = this._callback(HttpContext.Current);
			byte[] bytes = e.GetBytes(s);
			return new HttpResponseBufferElement(bytes, bytes.Length);
		}

		// Token: 0x06000D8C RID: 3468 RVA: 0x00025EC7 File Offset: 0x000240C7
		internal bool PointerEquals(IntPtr ptr)
		{
			return this._firstSubstData == ptr;
		}

		// Token: 0x06000D8D RID: 3469 RVA: 0x00025ED5 File Offset: 0x000240D5
		long IHttpResponseElement.GetSize()
		{
			if (this._isIIS7WorkerRequest)
			{
				return (long)this._firstSubstDataSize;
			}
			return this._firstSubstitution.GetSize();
		}

		// Token: 0x06000D8E RID: 3470 RVA: 0x00025EF4 File Offset: 0x000240F4
		byte[] IHttpResponseElement.GetBytes()
		{
			if (!this._isIIS7WorkerRequest)
			{
				return this._firstSubstitution.GetBytes();
			}
			if (this._firstSubstDataSize > 0)
			{
				byte[] array = new byte[this._firstSubstDataSize];
				Misc.CopyMemory(this._firstSubstData, 0, array, 0, this._firstSubstDataSize);
				return array;
			}
			if (!(this._firstSubstData == IntPtr.Zero))
			{
				return new byte[0];
			}
			return null;
		}

		// Token: 0x06000D8F RID: 3471 RVA: 0x00025F5C File Offset: 0x0002415C
		void IHttpResponseElement.Send(HttpWorkerRequest wr)
		{
			if (this._isIIS7WorkerRequest)
			{
				IIS7WorkerRequest iis7WorkerRequest = wr as IIS7WorkerRequest;
				if (iis7WorkerRequest != null)
				{
					iis7WorkerRequest.SendResponseFromIISAllocatedRequestMemory(this._firstSubstData, this._firstSubstDataSize);
					return;
				}
			}
			else
			{
				this._firstSubstitution.Send(wr);
			}
		}

		// Token: 0x04000503 RID: 1283
		private HttpResponseSubstitutionCallback _callback;

		// Token: 0x04000504 RID: 1284
		private IHttpResponseElement _firstSubstitution;

		// Token: 0x04000505 RID: 1285
		private IntPtr _firstSubstData;

		// Token: 0x04000506 RID: 1286
		private int _firstSubstDataSize;

		// Token: 0x04000507 RID: 1287
		private bool _isIIS7WorkerRequest;
	}
}

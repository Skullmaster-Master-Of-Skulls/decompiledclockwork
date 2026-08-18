using System;
using System.Diagnostics;
using System.IdentityModel.Diagnostics;
using System.IdentityModel.Tokens;
using System.IO;
using System.IO.Compression;

namespace System.IdentityModel
{
	// Token: 0x02000034 RID: 52
	public sealed class DeflateCookieTransform : CookieTransform
	{
		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000198 RID: 408 RVA: 0x00007F1D File Offset: 0x0000611D
		// (set) Token: 0x06000199 RID: 409 RVA: 0x00007F25 File Offset: 0x00006125
		public int MaxDecompressedSize
		{
			get
			{
				return this._maxDecompressedSize;
			}
			set
			{
				this._maxDecompressedSize = value;
			}
		}

		// Token: 0x0600019A RID: 410 RVA: 0x00007F30 File Offset: 0x00006130
		public override byte[] Decode(byte[] encoded)
		{
			if (encoded == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("encoded");
			}
			if (encoded.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("encoded", SR.GetString("ID6045"));
			}
			MemoryStream stream = new MemoryStream(encoded);
			byte[] result;
			using (DeflateStream deflateStream = new DeflateStream(stream, CompressionMode.Decompress, false))
			{
				using (MemoryStream memoryStream = new MemoryStream())
				{
					byte[] array = new byte[1024];
					for (;;)
					{
						int num = deflateStream.Read(array, 0, array.Length);
						memoryStream.Write(array, 0, num);
						if (memoryStream.Length > (long)this.MaxDecompressedSize)
						{
							break;
						}
						if (num <= 0)
						{
							goto Block_8;
						}
					}
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new SecurityTokenException(SR.GetString("ID1068", new object[]
					{
						this.MaxDecompressedSize
					})));
					Block_8:
					result = memoryStream.ToArray();
				}
			}
			return result;
		}

		// Token: 0x0600019B RID: 411 RVA: 0x00008024 File Offset: 0x00006224
		public override byte[] Encode(byte[] value)
		{
			if (value == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("value");
			}
			if (value.Length == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("value", SR.GetString("ID6044"));
			}
			byte[] result;
			using (MemoryStream memoryStream = new MemoryStream())
			{
				using (DeflateStream deflateStream = new DeflateStream(memoryStream, CompressionMode.Compress, true))
				{
					deflateStream.Write(value, 0, value.Length);
				}
				byte[] array = memoryStream.ToArray();
				if (DiagnosticUtility.ShouldTrace(TraceEventType.Information))
				{
					TraceUtility.TraceEvent(TraceEventType.Information, 786438, SR.GetString("TraceDeflateCookieEncode"), new DeflateCookieTraceRecord(value.Length, array.Length), null, null);
				}
				result = array;
			}
			return result;
		}

		// Token: 0x0400012E RID: 302
		private int _maxDecompressedSize = 1048576;
	}
}

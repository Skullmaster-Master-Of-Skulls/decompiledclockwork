using System;
using System.IO;

namespace System.Web
{
	// Token: 0x020000C6 RID: 198
	internal sealed class HttpFileResponseElement : IHttpResponseElement
	{
		// Token: 0x06000D7F RID: 3455 RVA: 0x00025BAE File Offset: 0x00023DAE
		internal HttpFileResponseElement(string filename, long offset, long size, bool isImpersonating, bool supportsLongTransmitFile) : this(filename, offset, size, isImpersonating, true, supportsLongTransmitFile)
		{
		}

		// Token: 0x06000D80 RID: 3456 RVA: 0x00025BBE File Offset: 0x00023DBE
		internal HttpFileResponseElement(string filename, long offset, long size) : this(filename, offset, size, false, false, false)
		{
		}

		// Token: 0x06000D81 RID: 3457 RVA: 0x00025BCC File Offset: 0x00023DCC
		private HttpFileResponseElement(string filename, long offset, long size, bool isImpersonating, bool useTransmitFile, bool supportsLongTransmitFile)
		{
			if ((!supportsLongTransmitFile && size > 2147483647L) || size < 0L)
			{
				throw new ArgumentOutOfRangeException("size", size, SR.GetString("Invalid_size"));
			}
			if ((!supportsLongTransmitFile && offset > 2147483647L) || offset < 0L)
			{
				throw new ArgumentOutOfRangeException("offset", offset, SR.GetString("Invalid_size"));
			}
			this._filename = filename;
			this._offset = offset;
			this._size = size;
			this._isImpersonating = isImpersonating;
			this._useTransmitFile = useTransmitFile;
		}

		// Token: 0x170004CF RID: 1231
		// (get) Token: 0x06000D82 RID: 3458 RVA: 0x00025C5E File Offset: 0x00023E5E
		internal string FileName
		{
			get
			{
				return this._filename;
			}
		}

		// Token: 0x170004D0 RID: 1232
		// (get) Token: 0x06000D83 RID: 3459 RVA: 0x00025C66 File Offset: 0x00023E66
		internal long Offset
		{
			get
			{
				return this._offset;
			}
		}

		// Token: 0x06000D84 RID: 3460 RVA: 0x00025C6E File Offset: 0x00023E6E
		long IHttpResponseElement.GetSize()
		{
			return this._size;
		}

		// Token: 0x06000D85 RID: 3461 RVA: 0x00025C78 File Offset: 0x00023E78
		byte[] IHttpResponseElement.GetBytes()
		{
			if (this._size == 0L)
			{
				return null;
			}
			byte[] array = null;
			FileStream fileStream = null;
			try
			{
				fileStream = new FileStream(this._filename, FileMode.Open, FileAccess.Read, FileShare.Read);
				long length = fileStream.Length;
				if (this._offset < 0L || this._size > length - this._offset)
				{
					throw new HttpException(SR.GetString("Invalid_range"));
				}
				if (this._offset > 0L)
				{
					fileStream.Seek(this._offset, SeekOrigin.Begin);
				}
				int num = (int)this._size;
				array = new byte[num];
				int num2 = 0;
				do
				{
					int num3 = fileStream.Read(array, num2, num);
					if (num3 == 0)
					{
						break;
					}
					num2 += num3;
					num -= num3;
				}
				while (num > 0);
			}
			finally
			{
				if (fileStream != null)
				{
					fileStream.Close();
				}
			}
			return array;
		}

		// Token: 0x06000D86 RID: 3462 RVA: 0x00025D3C File Offset: 0x00023F3C
		void IHttpResponseElement.Send(HttpWorkerRequest wr)
		{
			if (this._size > 0L)
			{
				if (this._useTransmitFile)
				{
					wr.TransmitFile(this._filename, this._offset, this._size, this._isImpersonating);
					return;
				}
				wr.SendResponseFromFile(this._filename, this._offset, this._size);
			}
		}

		// Token: 0x040004FE RID: 1278
		private string _filename;

		// Token: 0x040004FF RID: 1279
		private long _offset;

		// Token: 0x04000500 RID: 1280
		private long _size;

		// Token: 0x04000501 RID: 1281
		private bool _isImpersonating;

		// Token: 0x04000502 RID: 1282
		private bool _useTransmitFile;
	}
}

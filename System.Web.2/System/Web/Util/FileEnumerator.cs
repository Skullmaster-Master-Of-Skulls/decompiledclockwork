using System;
using System.Collections;
using System.IO;

namespace System.Web.Util
{
	// Token: 0x020001FE RID: 510
	internal class FileEnumerator : FileData, IEnumerable, IEnumerator, IDisposable
	{
		// Token: 0x06001912 RID: 6418 RVA: 0x0004DAD8 File Offset: 0x0004BCD8
		internal static FileEnumerator Create(string path)
		{
			return new FileEnumerator(path);
		}

		// Token: 0x06001913 RID: 6419 RVA: 0x0004DAE0 File Offset: 0x0004BCE0
		private FileEnumerator(string path)
		{
			this._path = Path.GetFullPath(path);
		}

		// Token: 0x06001914 RID: 6420 RVA: 0x0004DB00 File Offset: 0x0004BD00
		~FileEnumerator()
		{
			((IDisposable)this).Dispose();
		}

		// Token: 0x06001915 RID: 6421 RVA: 0x0004DB2C File Offset: 0x0004BD2C
		private bool SkipCurrent()
		{
			return this._wfd.cFileName == "." || this._wfd.cFileName == "..";
		}

		// Token: 0x06001916 RID: 6422 RVA: 0x00004335 File Offset: 0x00002535
		IEnumerator IEnumerable.GetEnumerator()
		{
			return this;
		}

		// Token: 0x06001917 RID: 6423 RVA: 0x0004DB60 File Offset: 0x0004BD60
		bool IEnumerator.MoveNext()
		{
			for (;;)
			{
				if (this._hFindFile == UnsafeNativeMethods.INVALID_HANDLE_VALUE)
				{
					this._hFindFile = UnsafeNativeMethods.FindFirstFile(this._path + "\\*.*", out this._wfd);
					if (this._hFindFile == UnsafeNativeMethods.INVALID_HANDLE_VALUE)
					{
						break;
					}
				}
				else if (!UnsafeNativeMethods.FindNextFile(this._hFindFile, out this._wfd))
				{
					return false;
				}
				if (!this.SkipCurrent())
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x17000755 RID: 1877
		// (get) Token: 0x06001918 RID: 6424 RVA: 0x00004335 File Offset: 0x00002535
		object IEnumerator.Current
		{
			get
			{
				return this;
			}
		}

		// Token: 0x06001919 RID: 6425 RVA: 0x0004DBD4 File Offset: 0x0004BDD4
		void IEnumerator.Reset()
		{
			throw new InvalidOperationException();
		}

		// Token: 0x0600191A RID: 6426 RVA: 0x0004DBDB File Offset: 0x0004BDDB
		void IDisposable.Dispose()
		{
			if (this._hFindFile != UnsafeNativeMethods.INVALID_HANDLE_VALUE)
			{
				UnsafeNativeMethods.FindClose(this._hFindFile);
				this._hFindFile = UnsafeNativeMethods.INVALID_HANDLE_VALUE;
			}
			GC.SuppressFinalize(this);
		}

		// Token: 0x040017AC RID: 6060
		private IntPtr _hFindFile = UnsafeNativeMethods.INVALID_HANDLE_VALUE;
	}
}

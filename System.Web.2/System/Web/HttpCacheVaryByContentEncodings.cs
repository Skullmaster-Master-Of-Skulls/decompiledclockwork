using System;

namespace System.Web
{
	// Token: 0x0200008C RID: 140
	public sealed class HttpCacheVaryByContentEncodings
	{
		// Token: 0x0600089F RID: 2207 RVA: 0x000131C6 File Offset: 0x000113C6
		public HttpCacheVaryByContentEncodings()
		{
			this.Reset();
		}

		// Token: 0x060008A0 RID: 2208 RVA: 0x000131D4 File Offset: 0x000113D4
		internal void Reset()
		{
			this._isModified = false;
			this._contentEncodings = null;
		}

		// Token: 0x060008A1 RID: 2209 RVA: 0x000131E4 File Offset: 0x000113E4
		public void SetContentEncodings(string[] contentEncodings)
		{
			this.Reset();
			if (contentEncodings != null)
			{
				this._isModified = true;
				this._contentEncodings = new string[contentEncodings.Length];
				for (int i = 0; i < contentEncodings.Length; i++)
				{
					this._contentEncodings[i] = contentEncodings[i];
				}
			}
		}

		// Token: 0x060008A2 RID: 2210 RVA: 0x00013228 File Offset: 0x00011428
		internal bool IsCacheableEncoding(string coding)
		{
			if (this._contentEncodings == null)
			{
				return true;
			}
			if (coding == null)
			{
				return true;
			}
			for (int i = 0; i < this._contentEncodings.Length; i++)
			{
				if (this._contentEncodings[i] == coding)
				{
					return true;
				}
			}
			return false;
		}

		// Token: 0x060008A3 RID: 2211 RVA: 0x0001326A File Offset: 0x0001146A
		internal bool IsModified()
		{
			return this._isModified;
		}

		// Token: 0x060008A4 RID: 2212 RVA: 0x00013274 File Offset: 0x00011474
		public string[] GetContentEncodings()
		{
			if (this._contentEncodings != null)
			{
				string[] array = new string[this._contentEncodings.Length];
				this._contentEncodings.CopyTo(array, 0);
				return array;
			}
			return null;
		}

		// Token: 0x17000364 RID: 868
		public bool this[string contentEncoding]
		{
			get
			{
				if (string.IsNullOrEmpty(contentEncoding))
				{
					throw new ArgumentNullException(SR.GetString("Parameter_NullOrEmpty", new object[]
					{
						"contentEncoding"
					}));
				}
				if (this._contentEncodings == null)
				{
					return false;
				}
				for (int i = 0; i < this._contentEncodings.Length; i++)
				{
					if (this._contentEncodings[i] == contentEncoding)
					{
						return true;
					}
				}
				return false;
			}
			set
			{
				if (string.IsNullOrEmpty(contentEncoding))
				{
					throw new ArgumentNullException(SR.GetString("Parameter_NullOrEmpty", new object[]
					{
						"contentEncoding"
					}));
				}
				if (!value)
				{
					return;
				}
				this._isModified = true;
				if (this._contentEncodings != null)
				{
					string[] array = new string[this._contentEncodings.Length + 1];
					for (int i = 0; i < this._contentEncodings.Length; i++)
					{
						array[i] = this._contentEncodings[i];
					}
					array[array.Length - 1] = contentEncoding;
					this._contentEncodings = array;
					return;
				}
				this._contentEncodings = new string[1];
				this._contentEncodings[0] = contentEncoding;
			}
		}

		// Token: 0x0400031F RID: 799
		private string[] _contentEncodings;

		// Token: 0x04000320 RID: 800
		private bool _isModified;
	}
}

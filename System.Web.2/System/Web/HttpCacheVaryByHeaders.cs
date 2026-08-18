using System;
using System.Text;

namespace System.Web
{
	// Token: 0x0200008B RID: 139
	public sealed class HttpCacheVaryByHeaders
	{
		// Token: 0x0600088D RID: 2189 RVA: 0x00012EC4 File Offset: 0x000110C4
		public HttpCacheVaryByHeaders()
		{
			this.Reset();
		}

		// Token: 0x0600088E RID: 2190 RVA: 0x00012ED2 File Offset: 0x000110D2
		internal void Reset()
		{
			this._isModified = false;
			this._varyStar = false;
			this._headers = null;
		}

		// Token: 0x0600088F RID: 2191 RVA: 0x00012EEC File Offset: 0x000110EC
		public void SetHeaders(string[] headers)
		{
			if (headers == null)
			{
				this._isModified = false;
				this._varyStar = false;
				this._headers = null;
				return;
			}
			this._isModified = true;
			if (headers[0].Equals("*"))
			{
				this._varyStar = true;
				this._headers = null;
				return;
			}
			this._varyStar = false;
			this._headers = new HttpDictionary();
			int i = 0;
			int num = headers.Length;
			while (i < num)
			{
				this._headers.SetValue(headers[i], headers[i]);
				i++;
			}
		}

		// Token: 0x06000890 RID: 2192 RVA: 0x00012F6A File Offset: 0x0001116A
		internal bool IsModified()
		{
			return this._isModified;
		}

		// Token: 0x06000891 RID: 2193 RVA: 0x00012F74 File Offset: 0x00011174
		internal string ToHeaderString()
		{
			if (this._varyStar)
			{
				return "*";
			}
			if (this._headers != null)
			{
				StringBuilder stringBuilder = new StringBuilder();
				int i = 0;
				int size = this._headers.Size;
				while (i < size)
				{
					object value = this._headers.GetValue(i);
					if (value != null)
					{
						HttpCachePolicy.AppendValueToHeader(stringBuilder, (string)value);
					}
					i++;
				}
				if (stringBuilder.Length > 0)
				{
					return stringBuilder.ToString();
				}
			}
			return null;
		}

		// Token: 0x06000892 RID: 2194 RVA: 0x00012FE4 File Offset: 0x000111E4
		public string[] GetHeaders()
		{
			string[] array = null;
			if (this._varyStar)
			{
				return new string[]
				{
					"*"
				};
			}
			if (this._headers != null)
			{
				int size = this._headers.Size;
				int num = 0;
				for (int i = 0; i < size; i++)
				{
					object value = this._headers.GetValue(i);
					if (value != null)
					{
						num++;
					}
				}
				if (num > 0)
				{
					array = new string[num];
					int num2 = 0;
					for (int i = 0; i < size; i++)
					{
						object value = this._headers.GetValue(i);
						if (value != null)
						{
							array[num2] = (string)value;
							num2++;
						}
					}
				}
			}
			return array;
		}

		// Token: 0x06000893 RID: 2195 RVA: 0x0001307F File Offset: 0x0001127F
		public void VaryByUnspecifiedParameters()
		{
			this._isModified = true;
			this._varyStar = true;
			this._headers = null;
		}

		// Token: 0x06000894 RID: 2196 RVA: 0x00013096 File Offset: 0x00011296
		internal bool GetVaryByUnspecifiedParameters()
		{
			return this._varyStar;
		}

		// Token: 0x1700035F RID: 863
		// (get) Token: 0x06000895 RID: 2197 RVA: 0x0001309E File Offset: 0x0001129E
		// (set) Token: 0x06000896 RID: 2198 RVA: 0x000130AB File Offset: 0x000112AB
		public bool AcceptTypes
		{
			get
			{
				return this["Accept"];
			}
			set
			{
				this._isModified = true;
				this["Accept"] = value;
			}
		}

		// Token: 0x17000360 RID: 864
		// (get) Token: 0x06000897 RID: 2199 RVA: 0x000130C0 File Offset: 0x000112C0
		// (set) Token: 0x06000898 RID: 2200 RVA: 0x000130CD File Offset: 0x000112CD
		public bool UserLanguage
		{
			get
			{
				return this["Accept-Language"];
			}
			set
			{
				this._isModified = true;
				this["Accept-Language"] = value;
			}
		}

		// Token: 0x17000361 RID: 865
		// (get) Token: 0x06000899 RID: 2201 RVA: 0x000130E2 File Offset: 0x000112E2
		// (set) Token: 0x0600089A RID: 2202 RVA: 0x000130EF File Offset: 0x000112EF
		public bool UserAgent
		{
			get
			{
				return this["User-Agent"];
			}
			set
			{
				this._isModified = true;
				this["User-Agent"] = value;
			}
		}

		// Token: 0x17000362 RID: 866
		// (get) Token: 0x0600089B RID: 2203 RVA: 0x00013104 File Offset: 0x00011304
		// (set) Token: 0x0600089C RID: 2204 RVA: 0x00013111 File Offset: 0x00011311
		public bool UserCharSet
		{
			get
			{
				return this["Accept-Charset"];
			}
			set
			{
				this._isModified = true;
				this["Accept-Charset"] = value;
			}
		}

		// Token: 0x17000363 RID: 867
		public bool this[string header]
		{
			get
			{
				if (header == null)
				{
					throw new ArgumentNullException("header");
				}
				if (header.Equals("*"))
				{
					return this._varyStar;
				}
				return this._headers != null && this._headers.GetValue(header) != null;
			}
			set
			{
				if (header == null)
				{
					throw new ArgumentNullException("header");
				}
				if (!value)
				{
					return;
				}
				this._isModified = true;
				if (header.Equals("*"))
				{
					this.VaryByUnspecifiedParameters();
					return;
				}
				if (!this._varyStar)
				{
					if (this._headers == null)
					{
						this._headers = new HttpDictionary();
					}
					this._headers.SetValue(header, header);
				}
			}
		}

		// Token: 0x0400031C RID: 796
		private bool _isModified;

		// Token: 0x0400031D RID: 797
		private bool _varyStar;

		// Token: 0x0400031E RID: 798
		private HttpDictionary _headers;
	}
}

using System;

namespace System.Web
{
	// Token: 0x02000081 RID: 129
	public sealed class HttpCacheVaryByParams
	{
		// Token: 0x0600080B RID: 2059 RVA: 0x00011241 File Offset: 0x0000F441
		public HttpCacheVaryByParams()
		{
			this.Reset();
		}

		// Token: 0x0600080C RID: 2060 RVA: 0x0001124F File Offset: 0x0000F44F
		internal void Reset()
		{
			this._isModified = false;
			this._paramsStar = false;
			this._parameters = null;
			this._ignoreParams = -1;
		}

		// Token: 0x0600080D RID: 2061 RVA: 0x00011270 File Offset: 0x0000F470
		public void SetParams(string[] parameters)
		{
			this.Reset();
			if (parameters != null)
			{
				this._isModified = true;
				if (parameters[0].Length == 0)
				{
					this.IgnoreParams = true;
					return;
				}
				if (parameters[0].Equals("*"))
				{
					this._paramsStar = true;
					return;
				}
				this._parameters = new HttpDictionary();
				int i = 0;
				int num = parameters.Length;
				while (i < num)
				{
					this._parameters.SetValue(parameters[i], parameters[i]);
					i++;
				}
			}
		}

		// Token: 0x0600080E RID: 2062 RVA: 0x000112E2 File Offset: 0x0000F4E2
		internal bool IsModified()
		{
			return this._isModified;
		}

		// Token: 0x0600080F RID: 2063 RVA: 0x000112EA File Offset: 0x0000F4EA
		internal bool AcceptsParams()
		{
			return this._ignoreParams == 1 || this._paramsStar || this._parameters != null;
		}

		// Token: 0x06000810 RID: 2064 RVA: 0x00011308 File Offset: 0x0000F508
		public string[] GetParams()
		{
			string[] array = null;
			if (this._ignoreParams == 1)
			{
				array = new string[]
				{
					string.Empty
				};
			}
			else if (this._paramsStar)
			{
				array = new string[]
				{
					"*"
				};
			}
			else if (this._parameters != null)
			{
				int size = this._parameters.Size;
				int num = 0;
				for (int i = 0; i < size; i++)
				{
					object value = this._parameters.GetValue(i);
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
						object value = this._parameters.GetValue(i);
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

		// Token: 0x1700032C RID: 812
		public bool this[string header]
		{
			get
			{
				if (header == null)
				{
					throw new ArgumentNullException("header");
				}
				if (header.Length == 0)
				{
					return this._ignoreParams == 1;
				}
				return this._paramsStar || (this._parameters != null && this._parameters.GetValue(header) != null);
			}
			set
			{
				if (header == null)
				{
					throw new ArgumentNullException("header");
				}
				if (header.Length == 0)
				{
					this.IgnoreParams = value;
					return;
				}
				if (value)
				{
					this._isModified = true;
					this._ignoreParams = 0;
					if (header.Equals("*"))
					{
						this._paramsStar = true;
						this._parameters = null;
						return;
					}
					if (!this._paramsStar)
					{
						if (this._parameters == null)
						{
							this._parameters = new HttpDictionary();
						}
						this._parameters.SetValue(header, header);
					}
				}
			}
		}

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06000813 RID: 2067 RVA: 0x00011494 File Offset: 0x0000F694
		// (set) Token: 0x06000814 RID: 2068 RVA: 0x0001149F File Offset: 0x0000F69F
		public bool IgnoreParams
		{
			get
			{
				return this._ignoreParams == 1;
			}
			set
			{
				if (this._paramsStar || this._parameters != null)
				{
					return;
				}
				if (this._ignoreParams == -1 || this._ignoreParams == 1)
				{
					this._ignoreParams = (value ? 1 : 0);
					this._isModified = true;
				}
			}
		}

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x06000815 RID: 2069 RVA: 0x000114D8 File Offset: 0x0000F6D8
		internal bool IsVaryByStar
		{
			get
			{
				return this._paramsStar;
			}
		}

		// Token: 0x040002A6 RID: 678
		private HttpDictionary _parameters;

		// Token: 0x040002A7 RID: 679
		private int _ignoreParams;

		// Token: 0x040002A8 RID: 680
		private bool _isModified;

		// Token: 0x040002A9 RID: 681
		private bool _paramsStar;
	}
}

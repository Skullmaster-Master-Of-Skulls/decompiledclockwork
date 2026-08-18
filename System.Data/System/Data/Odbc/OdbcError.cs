using System;

namespace System.Data.Odbc
{
	// Token: 0x020001EB RID: 491
	[Serializable]
	public sealed class OdbcError
	{
		// Token: 0x06001B6F RID: 7023 RVA: 0x00263698 File Offset: 0x00262A98
		internal OdbcError(string source, string message, string state, int nativeerror)
		{
			this._source = source;
			this._message = message;
			this._state = state;
			this._nativeerror = nativeerror;
		}

		// Token: 0x170003A9 RID: 937
		// (get) Token: 0x06001B70 RID: 7024 RVA: 0x002636C8 File Offset: 0x00262AC8
		public string Message
		{
			get
			{
				if (this._message == null)
				{
					return string.Empty;
				}
				return this._message;
			}
		}

		// Token: 0x170003AA RID: 938
		// (get) Token: 0x06001B71 RID: 7025 RVA: 0x002636F8 File Offset: 0x00262AF8
		public string SQLState
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x170003AB RID: 939
		// (get) Token: 0x06001B72 RID: 7026 RVA: 0x00263718 File Offset: 0x00262B18
		public int NativeError
		{
			get
			{
				return this._nativeerror;
			}
		}

		// Token: 0x170003AC RID: 940
		// (get) Token: 0x06001B73 RID: 7027 RVA: 0x00263738 File Offset: 0x00262B38
		public string Source
		{
			get
			{
				if (this._source == null)
				{
					return string.Empty;
				}
				return this._source;
			}
		}

		// Token: 0x06001B74 RID: 7028 RVA: 0x00263768 File Offset: 0x00262B68
		internal void SetSource(string Source)
		{
			this._source = Source;
		}

		// Token: 0x06001B75 RID: 7029 RVA: 0x00263788 File Offset: 0x00262B88
		public override string ToString()
		{
			return this.Message;
		}

		// Token: 0x04001017 RID: 4119
		internal string _message;

		// Token: 0x04001018 RID: 4120
		internal string _state;

		// Token: 0x04001019 RID: 4121
		internal int _nativeerror;

		// Token: 0x0400101A RID: 4122
		internal string _source;
	}
}

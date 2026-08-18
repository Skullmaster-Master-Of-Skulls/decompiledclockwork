using System;

namespace System.Data.Odbc
{
	// Token: 0x0200029B RID: 667
	[Serializable]
	public sealed class OdbcError
	{
		// Token: 0x060028CF RID: 10447 RVA: 0x0011095C File Offset: 0x0010FD5C
		internal OdbcError(string source, string message, string state, int nativeerror)
		{
			this._source = source;
			this._message = message;
			this._state = state;
			this._nativeerror = nativeerror;
		}

		// Token: 0x170006A5 RID: 1701
		// (get) Token: 0x060028D0 RID: 10448 RVA: 0x0011098C File Offset: 0x0010FD8C
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

		// Token: 0x170006A6 RID: 1702
		// (get) Token: 0x060028D1 RID: 10449 RVA: 0x001109B0 File Offset: 0x0010FDB0
		public string SQLState
		{
			get
			{
				return this._state;
			}
		}

		// Token: 0x170006A7 RID: 1703
		// (get) Token: 0x060028D2 RID: 10450 RVA: 0x001109C4 File Offset: 0x0010FDC4
		public int NativeError
		{
			get
			{
				return this._nativeerror;
			}
		}

		// Token: 0x170006A8 RID: 1704
		// (get) Token: 0x060028D3 RID: 10451 RVA: 0x001109D8 File Offset: 0x0010FDD8
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

		// Token: 0x060028D4 RID: 10452 RVA: 0x001109FC File Offset: 0x0010FDFC
		internal void SetSource(string Source)
		{
			this._source = Source;
		}

		// Token: 0x060028D5 RID: 10453 RVA: 0x00110A10 File Offset: 0x0010FE10
		public override string ToString()
		{
			return this.Message;
		}

		// Token: 0x04001AA3 RID: 6819
		internal string _message;

		// Token: 0x04001AA4 RID: 6820
		internal string _state;

		// Token: 0x04001AA5 RID: 6821
		internal int _nativeerror;

		// Token: 0x04001AA6 RID: 6822
		internal string _source;
	}
}

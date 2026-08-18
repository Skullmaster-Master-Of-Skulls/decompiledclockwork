using System;
using System.Text;

namespace System.Data.Odbc
{
	// Token: 0x020002A2 RID: 674
	public sealed class OdbcInfoMessageEventArgs : EventArgs
	{
		// Token: 0x060028FE RID: 10494 RVA: 0x00111184 File Offset: 0x00110584
		internal OdbcInfoMessageEventArgs(OdbcErrorCollection errors)
		{
			this._errors = errors;
		}

		// Token: 0x170006B1 RID: 1713
		// (get) Token: 0x060028FF RID: 10495 RVA: 0x001111A0 File Offset: 0x001105A0
		public OdbcErrorCollection Errors
		{
			get
			{
				return this._errors;
			}
		}

		// Token: 0x170006B2 RID: 1714
		// (get) Token: 0x06002900 RID: 10496 RVA: 0x001111B4 File Offset: 0x001105B4
		public string Message
		{
			get
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object obj in this.Errors)
				{
					OdbcError odbcError = (OdbcError)obj;
					if (0 < stringBuilder.Length)
					{
						stringBuilder.Append(Environment.NewLine);
					}
					stringBuilder.Append(odbcError.Message);
				}
				return stringBuilder.ToString();
			}
		}

		// Token: 0x06002901 RID: 10497 RVA: 0x00111240 File Offset: 0x00110640
		public override string ToString()
		{
			return this.Message;
		}

		// Token: 0x04001AAD RID: 6829
		private OdbcErrorCollection _errors;
	}
}

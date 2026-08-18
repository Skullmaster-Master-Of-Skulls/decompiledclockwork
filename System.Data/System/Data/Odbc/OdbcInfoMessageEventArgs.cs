using System;
using System.Text;

namespace System.Data.Odbc
{
	// Token: 0x020001F1 RID: 497
	public sealed class OdbcInfoMessageEventArgs : EventArgs
	{
		// Token: 0x06001B97 RID: 7063 RVA: 0x00263D28 File Offset: 0x00263128
		internal OdbcInfoMessageEventArgs(OdbcErrorCollection errors)
		{
			this._errors = errors;
		}

		// Token: 0x170003B3 RID: 947
		// (get) Token: 0x06001B98 RID: 7064 RVA: 0x00263D48 File Offset: 0x00263148
		public OdbcErrorCollection Errors
		{
			get
			{
				return this._errors;
			}
		}

		// Token: 0x170003B4 RID: 948
		// (get) Token: 0x06001B99 RID: 7065 RVA: 0x00263D68 File Offset: 0x00263168
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

		// Token: 0x06001B9A RID: 7066 RVA: 0x00263DF8 File Offset: 0x002631F8
		public override string ToString()
		{
			return this.Message;
		}

		// Token: 0x0400101F RID: 4127
		private OdbcErrorCollection _errors;
	}
}

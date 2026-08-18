using System;
using System.Data.SqlClient;

namespace System.Web.DataAccess
{
	// Token: 0x020001B0 RID: 432
	internal sealed class SqlConnectionHolder
	{
		// Token: 0x1700069C RID: 1692
		// (get) Token: 0x06001665 RID: 5733 RVA: 0x00047378 File Offset: 0x00045578
		internal SqlConnection Connection
		{
			get
			{
				return this._Connection;
			}
		}

		// Token: 0x06001666 RID: 5734 RVA: 0x00047380 File Offset: 0x00045580
		internal SqlConnectionHolder(string connectionString)
		{
			try
			{
				this._Connection = new SqlConnection(connectionString);
			}
			catch (ArgumentException innerException)
			{
				throw new ArgumentException(SR.GetString("SqlError_Connection_String"), "connectionString", innerException);
			}
		}

		// Token: 0x06001667 RID: 5735 RVA: 0x000473C8 File Offset: 0x000455C8
		internal void Open(HttpContext context, bool revertImpersonate)
		{
			if (this._Opened)
			{
				return;
			}
			if (revertImpersonate)
			{
				using (new ApplicationImpersonationContext())
				{
					this.Connection.Open();
					goto IL_34;
				}
			}
			this.Connection.Open();
			IL_34:
			this._Opened = true;
		}

		// Token: 0x06001668 RID: 5736 RVA: 0x00047420 File Offset: 0x00045620
		internal void Close()
		{
			if (!this._Opened)
			{
				return;
			}
			this.Connection.Close();
			this._Opened = false;
		}

		// Token: 0x0400169F RID: 5791
		internal SqlConnection _Connection;

		// Token: 0x040016A0 RID: 5792
		private bool _Opened;
	}
}

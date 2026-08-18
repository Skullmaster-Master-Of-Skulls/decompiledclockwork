using System;
using System.Data.SqlClient;

namespace System.Web.DataAccess
{
	// Token: 0x0200027B RID: 635
	internal sealed class SqlConnectionHolder
	{
		// Token: 0x17000720 RID: 1824
		// (get) Token: 0x060020E0 RID: 8416 RVA: 0x0008F3B0 File Offset: 0x0008E3B0
		internal SqlConnection Connection
		{
			get
			{
				return this._Connection;
			}
		}

		// Token: 0x060020E1 RID: 8417 RVA: 0x0008F3B8 File Offset: 0x0008E3B8
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

		// Token: 0x060020E2 RID: 8418 RVA: 0x0008F400 File Offset: 0x0008E400
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

		// Token: 0x060020E3 RID: 8419 RVA: 0x0008F458 File Offset: 0x0008E458
		internal void Close()
		{
			if (!this._Opened)
			{
				return;
			}
			this.Connection.Close();
			this._Opened = false;
		}

		// Token: 0x04001AD6 RID: 6870
		internal SqlConnection _Connection;

		// Token: 0x04001AD7 RID: 6871
		private bool _Opened;
	}
}

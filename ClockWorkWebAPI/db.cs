using System;
using System.Collections.Specialized;
using System.Data;
using System.Data.SqlClient;
using Databases;
using EncryptionClassLibrary;
using TechnoPro.Common.Configuration;

namespace ClockWorkWebAPI
{
	// Token: 0x02000014 RID: 20
	public class db
	{
		// Token: 0x17000061 RID: 97
		// (get) Token: 0x06000153 RID: 339 RVA: 0x0000A280 File Offset: 0x00008480
		public static db DB
		{
			get
			{
				bool flag = db.staticDb == null;
				if (flag)
				{
					db.staticDb = new db(ClockWorkConfigurationManager.GetConnectionStringByNameUsingProtection("clockwork"));
				}
				return db.staticDb;
			}
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x06000154 RID: 340 RVA: 0x0000A2BC File Offset: 0x000084BC
		public SqlDataAdapter Da
		{
			get
			{
				bool flag = this.da == null;
				if (flag)
				{
					this.SetupDbConnection();
				}
				return this.da;
			}
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x06000155 RID: 341 RVA: 0x0000A2E8 File Offset: 0x000084E8
		public SqlConnection Conn
		{
			get
			{
				bool flag = this.conn == null;
				if (flag)
				{
					this.SetupDbConnection();
				}
				return this.conn;
			}
		}

		// Token: 0x06000156 RID: 342 RVA: 0x0000A314 File Offset: 0x00008514
		private void SetupDbConnection()
		{
			this.conn = new SqlConnection(this.connectionString);
			this.da = new SqlDataAdapter("", this.conn);
			this.da.SelectCommand = new SqlCommand("", this.conn);
		}

		// Token: 0x17000064 RID: 100
		// (get) Token: 0x06000157 RID: 343 RVA: 0x0000A368 File Offset: 0x00008568
		public IEncryption TripleDES
		{
			get
			{
				DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
				return clockWork.Encryption;
			}
		}

		// Token: 0x06000158 RID: 344 RVA: 0x0000A388 File Offset: 0x00008588
		public db(string connectionString)
		{
			this.connectionString = connectionString;
			DatabaseLayer clockWork = DatabaseLayerFactory.ClockWork;
			this.conn = null;
			this.da = null;
			this.tripleDES = clockWork.Encryption;
		}

		// Token: 0x06000159 RID: 345 RVA: 0x0000A3D4 File Offset: 0x000085D4
		public int Fill(DataTable t, string sql, NameObjectPairCollection args)
		{
			this.da.SelectCommand.CommandText = sql;
			this.da.SelectCommand.Parameters.Clear();
			foreach (object obj in args)
			{
				NameObjectPair nameObjectPair = (NameObjectPair)obj;
				this.da.SelectCommand.Parameters.AddWithValue(nameObjectPair.Name, nameObjectPair.Value);
			}
			return this.da.Fill(t);
		}

		// Token: 0x0600015A RID: 346 RVA: 0x0000A480 File Offset: 0x00008680
		public void BeginTransaction()
		{
			bool flag = this.conn == null;
			if (flag)
			{
				this.SetupDbConnection();
			}
			this.conn.Open();
			this.transaction = this.conn.BeginTransaction();
			this.cmd = new SqlCommand();
			this.cmd.Connection = this.conn;
			this.cmd.Transaction = this.transaction;
		}

		// Token: 0x0600015B RID: 347 RVA: 0x0000A4F0 File Offset: 0x000086F0
		public Exception CommitTransaction()
		{
			Exception result;
			try
			{
				this.transaction.Commit();
				result = null;
			}
			catch (Exception ex)
			{
				result = ex;
				this.transaction.Rollback();
			}
			finally
			{
				this.conn.Close();
				this.transaction = null;
				this.cmd = null;
			}
			return result;
		}

		// Token: 0x0600015C RID: 348 RVA: 0x0000A564 File Offset: 0x00008764
		public Exception ExecuteTransactionQuery(string sql, NameObjectPairCollection args, out int result)
		{
			Exception result2;
			try
			{
				this.cmd.CommandText = sql;
				this.cmd.Parameters.Clear();
				foreach (object obj in args)
				{
					NameObjectPair nameObjectPair = (NameObjectPair)obj;
					this.cmd.Parameters.AddWithValue(nameObjectPair.Name, nameObjectPair.Value);
				}
				result = this.cmd.ExecuteNonQuery();
				result2 = null;
			}
			catch (Exception ex)
			{
				result = 0;
				this.transaction.Rollback();
				this.conn.Close();
				this.transaction = null;
				this.cmd = null;
				result2 = ex;
			}
			finally
			{
			}
			return result2;
		}

		// Token: 0x0600015D RID: 349 RVA: 0x0000A65C File Offset: 0x0000885C
		public Exception ExecuteTransactionQueryScalar(string sql, NameObjectPairCollection args, out int result)
		{
			Exception result2;
			try
			{
				this.cmd.CommandText = sql;
				this.cmd.Parameters.Clear();
				foreach (object obj in args)
				{
					NameObjectPair nameObjectPair = (NameObjectPair)obj;
					this.cmd.Parameters.AddWithValue(nameObjectPair.Name, nameObjectPair.Value);
				}
				object value = this.cmd.ExecuteScalar();
				result = Convert.ToInt32(value);
				result2 = null;
			}
			catch (Exception ex)
			{
				result = 0;
				this.transaction.Rollback();
				this.conn.Close();
				this.transaction = null;
				this.cmd = null;
				result2 = ex;
			}
			finally
			{
			}
			return result2;
		}

		// Token: 0x0600015E RID: 350 RVA: 0x0000A75C File Offset: 0x0000895C
		public void RollBackTransaction()
		{
			try
			{
				this.transaction.Rollback();
			}
			catch
			{
			}
			finally
			{
				this.conn.Close();
				this.transaction = null;
				this.cmd = null;
			}
		}

		// Token: 0x0600015F RID: 351 RVA: 0x0000A7BC File Offset: 0x000089BC
		public override string ToString()
		{
			return "Not implemented yet (tostring)";
		}

		// Token: 0x0400005E RID: 94
		private static db staticDb;

		// Token: 0x0400005F RID: 95
		private string connectionString;

		// Token: 0x04000060 RID: 96
		private SqlConnection conn;

		// Token: 0x04000061 RID: 97
		private SqlDataAdapter da;

		// Token: 0x04000062 RID: 98
		private IEncryption tripleDES;

		// Token: 0x04000063 RID: 99
		private NameValueCollection appSettings;

		// Token: 0x04000064 RID: 100
		private SqlTransaction transaction = null;

		// Token: 0x04000065 RID: 101
		private SqlCommand cmd = null;
	}
}

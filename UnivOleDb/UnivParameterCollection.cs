using System;
using System.Data.OleDb;
using System.Data.SqlClient;

namespace UnivOleDb22
{
	// Token: 0x02000008 RID: 8
	public class UnivParameterCollection
	{
		// Token: 0x06000062 RID: 98 RVA: 0x000047CE File Offset: 0x000037CE
		public UnivParameterCollection(UnivConnection univConnection, UnivCommand univCommand, object parameterCollection)
		{
			this.myUnivConnection = univConnection;
			this.myUnivCommand = univCommand;
			this.myUnivParameterCollection = parameterCollection;
		}

		// Token: 0x06000063 RID: 99 RVA: 0x000047F0 File Offset: 0x000037F0
		public void MakeAddParameterCommandTextChanges(string parameterName, object parameterValue)
		{
			dbName dbName = this.myUnivConnection.GetDbName();
			dbName dbName2 = dbName;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 != dbName.MSSQL)
				{
				}
			}
			else
			{
				bool flag = parameterValue is DateTime;
				if (!flag)
				{
					bool flag2 = parameterValue is string;
					if (flag2)
					{
					}
				}
			}
		}

		// Token: 0x06000064 RID: 100 RVA: 0x0000483C File Offset: 0x0000383C
		public void Add(string parameterName, object parameterValue)
		{
			dbName dbName = this.myUnivConnection.GetDbName();
			dbName dbName2 = dbName;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 == dbName.MSSQL)
				{
					SqlParameterCollection sqlParameterCollection = (SqlParameterCollection)this.myUnivParameterCollection;
					sqlParameterCollection.Add(parameterName, parameterValue);
				}
			}
			else
			{
				OleDbParameterCollection oleDbParameterCollection = (OleDbParameterCollection)this.myUnivParameterCollection;
				bool flag = parameterValue == null;
				if (flag)
				{
					oleDbParameterCollection.Add(parameterName, parameterValue);
				}
				else
				{
					bool flag2 = parameterValue is DateTime;
					if (flag2)
					{
						OleDbParameter oleDbParameter = oleDbParameterCollection.Add(parameterName, OleDbType.Date);
						oleDbParameter.Value = parameterValue;
					}
					else
					{
						bool flag3 = parameterValue is string;
						if (flag3)
						{
							OleDbParameter oleDbParameter2 = oleDbParameterCollection.Add(parameterName, OleDbType.VarChar);
							oleDbParameter2.Value = parameterValue;
						}
						else
						{
							oleDbParameterCollection.Add(parameterName, parameterValue);
						}
					}
				}
			}
		}

		// Token: 0x06000065 RID: 101 RVA: 0x00004900 File Offset: 0x00003900
		public object AddUsingSourceColumn(string parameterName, string sourceColumn)
		{
			dbName dbName = this.myUnivConnection.GetDbName();
			dbName dbName2 = dbName;
			object result;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 != dbName.MSSQL)
				{
					result = null;
				}
				else
				{
					SqlParameterCollection sqlParameterCollection = (SqlParameterCollection)this.myUnivParameterCollection;
					SqlParameter sqlParameter = sqlParameterCollection.Add(parameterName, DBNull.Value);
					sqlParameter.SourceColumn = sourceColumn;
					result = sqlParameter;
				}
			}
			else
			{
				OleDbParameterCollection oleDbParameterCollection = (OleDbParameterCollection)this.myUnivParameterCollection;
				OleDbParameter oleDbParameter = oleDbParameterCollection.Add(parameterName, DBNull.Value);
				oleDbParameter.SourceColumn = sourceColumn;
				result = oleDbParameter;
			}
			return result;
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00004980 File Offset: 0x00003980
		public int Count
		{
			get
			{
				dbName dbName = this.myUnivConnection.GetDbName();
				dbName dbName2 = dbName;
				int result;
				if (dbName2 != dbName.MSAccess)
				{
					if (dbName2 != dbName.MSSQL)
					{
						result = -1;
					}
					else
					{
						SqlParameterCollection sqlParameterCollection = (SqlParameterCollection)this.myUnivParameterCollection;
						result = sqlParameterCollection.Count;
					}
				}
				else
				{
					OleDbParameterCollection oleDbParameterCollection = (OleDbParameterCollection)this.myUnivParameterCollection;
					result = oleDbParameterCollection.Count;
				}
				return result;
			}
		}

		// Token: 0x06000067 RID: 103 RVA: 0x000049DC File Offset: 0x000039DC
		public object Value(string parameterName)
		{
			dbName dbName = this.myUnivConnection.GetDbName();
			dbName dbName2 = dbName;
			object result;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 != dbName.MSSQL)
				{
					result = null;
				}
				else
				{
					SqlParameterCollection sqlParameterCollection = (SqlParameterCollection)this.myUnivParameterCollection;
					result = sqlParameterCollection[parameterName].Value;
				}
			}
			else
			{
				OleDbParameterCollection oleDbParameterCollection = (OleDbParameterCollection)this.myUnivParameterCollection;
				result = oleDbParameterCollection[parameterName].Value;
			}
			return result;
		}

		// Token: 0x06000068 RID: 104 RVA: 0x00004A44 File Offset: 0x00003A44
		public string ParameterName(int parameterIndex)
		{
			return (string)this.GetParameterNameValue(parameterIndex, true);
		}

		// Token: 0x06000069 RID: 105 RVA: 0x00004A64 File Offset: 0x00003A64
		public object Value(int parameterIndex)
		{
			return this.GetParameterNameValue(parameterIndex, false);
		}

		// Token: 0x0600006A RID: 106 RVA: 0x00004A80 File Offset: 0x00003A80
		private object GetParameterNameValue(int parameterIndex, bool getName)
		{
			dbName dbName = this.myUnivConnection.GetDbName();
			dbName dbName2 = dbName;
			object result;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 != dbName.MSSQL)
				{
					result = null;
				}
				else
				{
					SqlParameterCollection sqlParameterCollection = (SqlParameterCollection)this.myUnivParameterCollection;
					if (getName)
					{
						result = sqlParameterCollection[parameterIndex].ParameterName;
					}
					else
					{
						result = sqlParameterCollection[parameterIndex].Value;
					}
				}
			}
			else
			{
				OleDbParameterCollection oleDbParameterCollection = (OleDbParameterCollection)this.myUnivParameterCollection;
				if (getName)
				{
					result = oleDbParameterCollection[parameterIndex].ParameterName;
				}
				else
				{
					result = oleDbParameterCollection[parameterIndex].Value;
				}
			}
			return result;
		}

		// Token: 0x0600006B RID: 107 RVA: 0x00004B18 File Offset: 0x00003B18
		public bool Contains(string parameterName)
		{
			dbName dbName = this.myUnivConnection.GetDbName();
			dbName dbName2 = dbName;
			bool result;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 != dbName.MSSQL)
				{
					result = false;
				}
				else
				{
					SqlParameterCollection sqlParameterCollection = (SqlParameterCollection)this.myUnivParameterCollection;
					result = sqlParameterCollection.Contains(parameterName);
				}
			}
			else
			{
				OleDbParameterCollection oleDbParameterCollection = (OleDbParameterCollection)this.myUnivParameterCollection;
				result = oleDbParameterCollection.Contains(parameterName);
			}
			return result;
		}

		// Token: 0x0600006C RID: 108 RVA: 0x00004B74 File Offset: 0x00003B74
		public void SetValue(string parameterName, object val)
		{
			dbName dbName = this.myUnivConnection.GetDbName();
			dbName dbName2 = dbName;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 == dbName.MSSQL)
				{
					SqlParameterCollection sqlParameterCollection = (SqlParameterCollection)this.myUnivParameterCollection;
					sqlParameterCollection[parameterName].Value = val;
				}
			}
			else
			{
				OleDbParameterCollection oleDbParameterCollection = (OleDbParameterCollection)this.myUnivParameterCollection;
				oleDbParameterCollection[parameterName].Value = val;
			}
		}

		// Token: 0x0600006D RID: 109 RVA: 0x00004BD4 File Offset: 0x00003BD4
		public void Clear()
		{
			dbName dbName = this.myUnivConnection.GetDbName();
			dbName dbName2 = dbName;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 == dbName.MSSQL)
				{
					SqlParameterCollection sqlParameterCollection = (SqlParameterCollection)this.myUnivParameterCollection;
					sqlParameterCollection.Clear();
				}
			}
			else
			{
				OleDbParameterCollection oleDbParameterCollection = (OleDbParameterCollection)this.myUnivParameterCollection;
				oleDbParameterCollection.Clear();
			}
		}

		// Token: 0x0600006E RID: 110 RVA: 0x00004C28 File Offset: 0x00003C28
		public void Clear(string parameterName)
		{
			dbName dbName = this.myUnivConnection.GetDbName();
			dbName dbName2 = dbName;
			if (dbName2 != dbName.MSAccess)
			{
				if (dbName2 == dbName.MSSQL)
				{
					SqlParameterCollection sqlParameterCollection = (SqlParameterCollection)this.myUnivParameterCollection;
					SqlParameter sqlParameter = sqlParameterCollection[parameterName];
					bool flag = sqlParameter != null;
					if (flag)
					{
						sqlParameterCollection.Remove(sqlParameter);
					}
				}
			}
			else
			{
				OleDbParameterCollection oleDbParameterCollection = (OleDbParameterCollection)this.myUnivParameterCollection;
				OleDbParameter oleDbParameter = oleDbParameterCollection[parameterName];
				bool flag2 = oleDbParameter != null;
				if (flag2)
				{
					oleDbParameterCollection.Remove(oleDbParameter);
				}
			}
		}

		// Token: 0x04000025 RID: 37
		private UnivConnection myUnivConnection;

		// Token: 0x04000026 RID: 38
		private object myUnivParameterCollection;

		// Token: 0x04000027 RID: 39
		private UnivCommand myUnivCommand;
	}
}

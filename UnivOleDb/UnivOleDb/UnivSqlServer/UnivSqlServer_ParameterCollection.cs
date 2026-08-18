using System;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;

namespace UnivOleDb.UnivSqlServer
{
	// Token: 0x0200001A RID: 26
	[Serializable]
	public class UnivSqlServer_ParameterCollection : UnivParameterCollection
	{
		// Token: 0x06000170 RID: 368 RVA: 0x00007A51 File Offset: 0x00006A51
		public UnivSqlServer_ParameterCollection(UnivSqlServer_Connection univConnection, UnivSqlServer_Command univCommand, SqlParameterCollection parameterCollection)
		{
			this.myUnivConnection = univConnection;
			this.myUnivCommand = univCommand;
			this.myParameterCollection = parameterCollection;
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x06000171 RID: 369 RVA: 0x00007A70 File Offset: 0x00006A70
		public object ParameterCollection
		{
			get
			{
				return this.myParameterCollection;
			}
		}

		// Token: 0x06000172 RID: 370 RVA: 0x00006527 File Offset: 0x00005527
		public void MakeAddParameterCommandTextChanges(string parameterName, object parameterValue)
		{
		}

		// Token: 0x06000173 RID: 371 RVA: 0x00007A88 File Offset: 0x00006A88
		public object AddNull(string parameterName)
		{
			SqlParameter sqlParameter = new SqlParameter();
			sqlParameter.ParameterName = parameterName;
			sqlParameter.IsNullable = true;
			sqlParameter.Value = DBNull.Value;
			return this.myParameterCollection.Add(sqlParameter);
		}

		// Token: 0x06000174 RID: 372 RVA: 0x00007AC8 File Offset: 0x00006AC8
		public object Add(string parameterName, object parameterValue)
		{
			bool flag = parameterValue == null || parameterValue == DBNull.Value;
			object result;
			if (flag)
			{
				result = this.AddNull(parameterName);
			}
			else
			{
				result = this.myParameterCollection.Add(parameterName, parameterValue);
			}
			return result;
		}

		// Token: 0x06000175 RID: 373 RVA: 0x00007B04 File Offset: 0x00006B04
		public object Add(string parameterName, Type type, int size, object parameterValue)
		{
			SqlDbType dbtype = this.GetDBType(type);
			SqlParameter sqlParameter = this.myParameterCollection.Add(parameterName, dbtype, size);
			sqlParameter.Value = parameterValue;
			return sqlParameter;
		}

		// Token: 0x06000176 RID: 374 RVA: 0x00007B38 File Offset: 0x00006B38
		public object AddNull(string parameterName, DbType dbType)
		{
			SqlParameter sqlParameter = new SqlParameter();
			sqlParameter.ParameterName = parameterName;
			sqlParameter.IsNullable = true;
			sqlParameter.Value = DBNull.Value;
			sqlParameter.DbType = dbType;
			return this.myParameterCollection.Add(sqlParameter);
		}

		// Token: 0x06000177 RID: 375 RVA: 0x00007B80 File Offset: 0x00006B80
		private SqlDbType GetDBType(Type theType)
		{
			bool flag = theType == typeof(DateTime);
			SqlDbType result;
			if (flag)
			{
				result = SqlDbType.DateTime;
			}
			else
			{
				byte[] array = new byte[1];
				Type type = array.GetType();
				bool flag2 = theType == type;
				if (flag2)
				{
					result = SqlDbType.Binary;
				}
				else
				{
					SqlParameter sqlParameter = new SqlParameter();
					TypeConverter converter = TypeDescriptor.GetConverter(sqlParameter.DbType);
					bool flag3 = converter.CanConvertFrom(theType);
					if (flag3)
					{
						sqlParameter.DbType = (DbType)converter.ConvertFrom(theType.Name);
					}
					else
					{
						try
						{
							sqlParameter.DbType = (DbType)converter.ConvertFrom(theType.Name);
						}
						catch (Exception ex)
						{
						}
					}
					result = sqlParameter.SqlDbType;
				}
			}
			return result;
		}

		// Token: 0x06000178 RID: 376 RVA: 0x00007C50 File Offset: 0x00006C50
		public object AddUsingSourceColumn(string parameterName, string sourceColumn)
		{
			SqlParameter sqlParameter = this.myParameterCollection.Add(parameterName, DBNull.Value);
			sqlParameter.SourceColumn = sourceColumn;
			return sqlParameter;
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000179 RID: 377 RVA: 0x00007C80 File Offset: 0x00006C80
		public int Count
		{
			get
			{
				return this.myParameterCollection.Count;
			}
		}

		// Token: 0x0600017A RID: 378 RVA: 0x00007CA0 File Offset: 0x00006CA0
		public object Value(string parameterName)
		{
			return this.myParameterCollection[parameterName];
		}

		// Token: 0x17000048 RID: 72
		public object this[string parameterName]
		{
			get
			{
				return this.myParameterCollection[parameterName];
			}
		}

		// Token: 0x17000049 RID: 73
		public object this[int index]
		{
			get
			{
				return this.myParameterCollection[index];
			}
		}

		// Token: 0x0600017D RID: 381 RVA: 0x00007D00 File Offset: 0x00006D00
		public string ParameterName(int parameterIndex)
		{
			return (string)this.GetParameterNameValue(parameterIndex, true);
		}

		// Token: 0x0600017E RID: 382 RVA: 0x00007D20 File Offset: 0x00006D20
		public DbType ParameterDbType(int parameterIndex)
		{
			return this.myParameterCollection[parameterIndex].DbType;
		}

		// Token: 0x0600017F RID: 383 RVA: 0x00007D44 File Offset: 0x00006D44
		public object Value(int parameterIndex)
		{
			return this.GetParameterNameValue(parameterIndex, false);
		}

		// Token: 0x06000180 RID: 384 RVA: 0x00007D60 File Offset: 0x00006D60
		public object GetParameterNameValue(int parameterIndex, bool getName)
		{
			object result;
			if (getName)
			{
				result = this.myParameterCollection[parameterIndex].ParameterName;
			}
			else
			{
				result = this.myParameterCollection[parameterIndex].Value;
			}
			return result;
		}

		// Token: 0x06000181 RID: 385 RVA: 0x00007DA0 File Offset: 0x00006DA0
		public bool Contains(string parameterName)
		{
			return this.myParameterCollection.Contains(parameterName);
		}

		// Token: 0x06000182 RID: 386 RVA: 0x00007DBE File Offset: 0x00006DBE
		public void SetValue(string parameterName, object val)
		{
			this.myParameterCollection[parameterName].Value = val;
		}

		// Token: 0x06000183 RID: 387 RVA: 0x00007DD4 File Offset: 0x00006DD4
		public void Clear()
		{
			this.myParameterCollection.Clear();
		}

		// Token: 0x06000184 RID: 388 RVA: 0x00007DE4 File Offset: 0x00006DE4
		public void Clear(string parameterName)
		{
			SqlParameter sqlParameter = this.myParameterCollection[parameterName];
			bool flag = sqlParameter != null;
			if (flag)
			{
				this.myParameterCollection.Remove(sqlParameter);
			}
		}

		// Token: 0x0400004E RID: 78
		private UnivSqlServer_Command myUnivCommand;

		// Token: 0x0400004F RID: 79
		private UnivSqlServer_Connection myUnivConnection;

		// Token: 0x04000050 RID: 80
		private SqlParameterCollection myParameterCollection;
	}
}

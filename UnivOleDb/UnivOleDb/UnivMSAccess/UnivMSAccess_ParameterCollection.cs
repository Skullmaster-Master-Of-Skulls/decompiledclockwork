using System;
using System.Data;
using System.Data.OleDb;

namespace UnivOleDb.UnivMSAccess
{
	// Token: 0x02000026 RID: 38
	public class UnivMSAccess_ParameterCollection : UnivParameterCollection
	{
		// Token: 0x060001F0 RID: 496 RVA: 0x0000915D File Offset: 0x0000815D
		public UnivMSAccess_ParameterCollection(UnivMSAccess_Connection univConnection, UnivMSAccess_Command univCommand, OleDbParameterCollection parameterCollection)
		{
			this.myUnivConnection = univConnection;
			this.myUnivCommand = univCommand;
			this.myParameterCollection = parameterCollection;
		}

		// Token: 0x17000062 RID: 98
		// (get) Token: 0x060001F1 RID: 497 RVA: 0x0000917C File Offset: 0x0000817C
		public object ParameterCollection
		{
			get
			{
				return this.myParameterCollection;
			}
		}

		// Token: 0x060001F2 RID: 498 RVA: 0x00006527 File Offset: 0x00005527
		public void MakeAddParameterCommandTextChanges(string parameterName, object parameterValue)
		{
		}

		// Token: 0x060001F3 RID: 499 RVA: 0x00009194 File Offset: 0x00008194
		public DbType ParameterDbType(int parameterIndex)
		{
			return this.myParameterCollection[parameterIndex].DbType;
		}

		// Token: 0x060001F4 RID: 500 RVA: 0x000091B8 File Offset: 0x000081B8
		public object AddNull(string parameterName)
		{
			OleDbParameter oleDbParameter = new OleDbParameter();
			oleDbParameter.ParameterName = parameterName;
			oleDbParameter.IsNullable = true;
			oleDbParameter.Value = DBNull.Value;
			return this.myParameterCollection.Add(oleDbParameter);
		}

		// Token: 0x060001F5 RID: 501 RVA: 0x000091F8 File Offset: 0x000081F8
		public object AddNull(string parameterName, DbType dbType)
		{
			OleDbParameter oleDbParameter = new OleDbParameter();
			oleDbParameter.ParameterName = parameterName;
			oleDbParameter.IsNullable = true;
			oleDbParameter.Value = DBNull.Value;
			oleDbParameter.DbType = dbType;
			return this.myParameterCollection.Add(oleDbParameter);
		}

		// Token: 0x060001F6 RID: 502 RVA: 0x00009240 File Offset: 0x00008240
		public object Add(string parameterName, object parameterValue)
		{
			bool flag = parameterValue != null && parameterValue is DateTime;
			object result;
			if (flag)
			{
				OleDbParameter oleDbParameter = new OleDbParameter(parameterName, OleDbType.Date);
				oleDbParameter.Value = parameterValue;
				result = this.myParameterCollection.Add(oleDbParameter);
			}
			else
			{
				result = this.myParameterCollection.AddWithValue(parameterName, parameterValue);
			}
			return result;
		}

		// Token: 0x060001F7 RID: 503 RVA: 0x00009294 File Offset: 0x00008294
		public object Add(string parameterName, Type type, int size, object parameterValue)
		{
			return this.myParameterCollection.AddWithValue(parameterName, parameterValue);
		}

		// Token: 0x060001F8 RID: 504 RVA: 0x000092B4 File Offset: 0x000082B4
		public object AddUsingSourceColumn(string parameterName, string sourceColumn)
		{
			OleDbParameter oleDbParameter = this.myParameterCollection.AddWithValue(parameterName, DBNull.Value);
			oleDbParameter.SourceColumn = sourceColumn;
			return oleDbParameter;
		}

		// Token: 0x17000063 RID: 99
		// (get) Token: 0x060001F9 RID: 505 RVA: 0x000092E4 File Offset: 0x000082E4
		public int Count
		{
			get
			{
				return this.myParameterCollection.Count;
			}
		}

		// Token: 0x060001FA RID: 506 RVA: 0x00009304 File Offset: 0x00008304
		public object Value(string parameterName)
		{
			return this.myParameterCollection[parameterName];
		}

		// Token: 0x17000064 RID: 100
		public object this[string parameterName]
		{
			get
			{
				return this.myParameterCollection[parameterName];
			}
		}

		// Token: 0x17000065 RID: 101
		public object this[int index]
		{
			get
			{
				return this.myParameterCollection[index];
			}
		}

		// Token: 0x060001FD RID: 509 RVA: 0x00009364 File Offset: 0x00008364
		public string ParameterName(int parameterIndex)
		{
			return (string)this.GetParameterNameValue(parameterIndex, true);
		}

		// Token: 0x060001FE RID: 510 RVA: 0x00009384 File Offset: 0x00008384
		public object Value(int parameterIndex)
		{
			return this.GetParameterNameValue(parameterIndex, false);
		}

		// Token: 0x060001FF RID: 511 RVA: 0x000093A0 File Offset: 0x000083A0
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

		// Token: 0x06000200 RID: 512 RVA: 0x000093E0 File Offset: 0x000083E0
		public bool Contains(string parameterName)
		{
			return this.myParameterCollection.Contains(parameterName);
		}

		// Token: 0x06000201 RID: 513 RVA: 0x000093FE File Offset: 0x000083FE
		public void SetValue(string parameterName, object val)
		{
			this.myParameterCollection[parameterName].Value = val;
		}

		// Token: 0x06000202 RID: 514 RVA: 0x00009414 File Offset: 0x00008414
		public void Clear()
		{
			this.myParameterCollection.Clear();
		}

		// Token: 0x06000203 RID: 515 RVA: 0x00009424 File Offset: 0x00008424
		public void Clear(string parameterName)
		{
			OleDbParameter oleDbParameter = this.myParameterCollection[parameterName];
			bool flag = oleDbParameter != null;
			if (flag)
			{
				this.myParameterCollection.Remove(oleDbParameter);
			}
		}

		// Token: 0x0400006E RID: 110
		private UnivMSAccess_Command myUnivCommand;

		// Token: 0x0400006F RID: 111
		private UnivMSAccess_Connection myUnivConnection;

		// Token: 0x04000070 RID: 112
		private OleDbParameterCollection myParameterCollection;
	}
}

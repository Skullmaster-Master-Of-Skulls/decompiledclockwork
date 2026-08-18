using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;

namespace System.Data
{
	// Token: 0x020000E9 RID: 233
	internal sealed class DataExpression : IFilter
	{
		// Token: 0x06000F58 RID: 3928 RVA: 0x0007C37C File Offset: 0x0007B77C
		internal DataExpression(DataTable table, string expression) : this(table, expression, null)
		{
		}

		// Token: 0x06000F59 RID: 3929 RVA: 0x0007C394 File Offset: 0x0007B794
		internal DataExpression(DataTable table, string expression, Type type)
		{
			ExpressionParser expressionParser = new ExpressionParser(table);
			expressionParser.LoadExpression(expression);
			this.originalExpression = expression;
			this.expr = null;
			if (expression != null)
			{
				this._storageType = DataStorage.GetStorageType(type);
				if (this._storageType == StorageType.BigInteger)
				{
					throw ExprException.UnsupportedDataType(type);
				}
				this._dataType = type;
				this.expr = expressionParser.Parse();
				this.parsed = true;
				if (this.expr != null && table != null)
				{
					this.Bind(table);
					return;
				}
				this.bound = false;
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000F5A RID: 3930 RVA: 0x0007C424 File Offset: 0x0007B824
		internal string Expression
		{
			get
			{
				if (this.originalExpression == null)
				{
					return "";
				}
				return this.originalExpression;
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000F5B RID: 3931 RVA: 0x0007C448 File Offset: 0x0007B848
		internal ExpressionNode ExpressionNode
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000F5C RID: 3932 RVA: 0x0007C45C File Offset: 0x0007B85C
		internal bool HasValue
		{
			get
			{
				return this.expr != null;
			}
		}

		// Token: 0x06000F5D RID: 3933 RVA: 0x0007C474 File Offset: 0x0007B874
		internal void Bind(DataTable table)
		{
			this.table = table;
			if (table == null)
			{
				return;
			}
			if (this.expr != null)
			{
				List<DataColumn> list = new List<DataColumn>();
				this.expr.Bind(table, list);
				this.expr = this.expr.Optimize();
				this.table = table;
				this.bound = true;
				this.dependency = list.ToArray();
			}
		}

		// Token: 0x06000F5E RID: 3934 RVA: 0x0007C4D4 File Offset: 0x0007B8D4
		internal bool DependsOn(DataColumn column)
		{
			return this.expr != null && this.expr.DependsOn(column);
		}

		// Token: 0x06000F5F RID: 3935 RVA: 0x0007C4F8 File Offset: 0x0007B8F8
		internal object Evaluate()
		{
			return this.Evaluate(null, DataRowVersion.Default);
		}

		// Token: 0x06000F60 RID: 3936 RVA: 0x0007C514 File Offset: 0x0007B914
		internal object Evaluate(DataRow row, DataRowVersion version)
		{
			if (!this.bound)
			{
				this.Bind(this.table);
			}
			object obj;
			if (this.expr != null)
			{
				obj = this.expr.Eval(row, version);
				if (obj == DBNull.Value && StorageType.Uri >= this._storageType)
				{
					return obj;
				}
				try
				{
					if (StorageType.Object != this._storageType)
					{
						obj = SqlConvert.ChangeType2(obj, this._storageType, this._dataType, this.table.FormatProvider);
					}
					return obj;
				}
				catch (Exception ex)
				{
					if (!ADP.IsCatchableExceptionType(ex))
					{
						throw;
					}
					ExceptionBuilder.TraceExceptionForCapture(ex);
					throw ExprException.DatavalueConvertion(obj, this._dataType, ex);
				}
			}
			obj = null;
			return obj;
		}

		// Token: 0x06000F61 RID: 3937 RVA: 0x0007C5C8 File Offset: 0x0007B9C8
		internal object Evaluate(DataRow[] rows)
		{
			return this.Evaluate(rows, DataRowVersion.Default);
		}

		// Token: 0x06000F62 RID: 3938 RVA: 0x0007C5E4 File Offset: 0x0007B9E4
		internal object Evaluate(DataRow[] rows, DataRowVersion version)
		{
			if (!this.bound)
			{
				this.Bind(this.table);
			}
			if (this.expr != null)
			{
				List<int> list = new List<int>();
				foreach (DataRow dataRow in rows)
				{
					if (dataRow.RowState != DataRowState.Deleted && (version != DataRowVersion.Original || dataRow.oldRecord != -1))
					{
						list.Add(dataRow.GetRecordFromVersion(version));
					}
				}
				int[] recordNos = list.ToArray();
				return this.expr.Eval(recordNos);
			}
			return DBNull.Value;
		}

		// Token: 0x06000F63 RID: 3939 RVA: 0x0007C66C File Offset: 0x0007BA6C
		public bool Invoke(DataRow row, DataRowVersion version)
		{
			if (this.expr == null)
			{
				return true;
			}
			if (row == null)
			{
				throw ExprException.InvokeArgument();
			}
			object value = this.expr.Eval(row, version);
			bool result;
			try
			{
				result = DataExpression.ToBoolean(value);
			}
			catch (EvaluateException)
			{
				throw ExprException.FilterConvertion(this.Expression);
			}
			return result;
		}

		// Token: 0x06000F64 RID: 3940 RVA: 0x0007C6D0 File Offset: 0x0007BAD0
		internal DataColumn[] GetDependency()
		{
			return this.dependency;
		}

		// Token: 0x06000F65 RID: 3941 RVA: 0x0007C6E4 File Offset: 0x0007BAE4
		internal bool IsTableAggregate()
		{
			return this.expr != null && this.expr.IsTableConstant();
		}

		// Token: 0x06000F66 RID: 3942 RVA: 0x0007C708 File Offset: 0x0007BB08
		internal static bool IsUnknown(object value)
		{
			return DataStorage.IsObjectNull(value);
		}

		// Token: 0x06000F67 RID: 3943 RVA: 0x0007C71C File Offset: 0x0007BB1C
		internal bool HasLocalAggregate()
		{
			return this.expr != null && this.expr.HasLocalAggregate();
		}

		// Token: 0x06000F68 RID: 3944 RVA: 0x0007C740 File Offset: 0x0007BB40
		internal bool HasRemoteAggregate()
		{
			return this.expr != null && this.expr.HasRemoteAggregate();
		}

		// Token: 0x06000F69 RID: 3945 RVA: 0x0007C764 File Offset: 0x0007BB64
		internal static bool ToBoolean(object value)
		{
			if (DataExpression.IsUnknown(value))
			{
				return false;
			}
			if (value is bool)
			{
				return (bool)value;
			}
			if (value is SqlBoolean)
			{
				return ((SqlBoolean)value).IsTrue;
			}
			if (value is string)
			{
				try
				{
					return bool.Parse((string)value);
				}
				catch (Exception ex)
				{
					if (!ADP.IsCatchableExceptionType(ex))
					{
						throw;
					}
					ExceptionBuilder.TraceExceptionForCapture(ex);
					throw ExprException.DatavalueConvertion(value, typeof(bool), ex);
				}
			}
			throw ExprException.DatavalueConvertion(value, typeof(bool), null);
		}

		// Token: 0x04000494 RID: 1172
		internal string originalExpression;

		// Token: 0x04000495 RID: 1173
		private bool parsed;

		// Token: 0x04000496 RID: 1174
		private bool bound;

		// Token: 0x04000497 RID: 1175
		private ExpressionNode expr;

		// Token: 0x04000498 RID: 1176
		private DataTable table;

		// Token: 0x04000499 RID: 1177
		private readonly StorageType _storageType;

		// Token: 0x0400049A RID: 1178
		private readonly Type _dataType;

		// Token: 0x0400049B RID: 1179
		private DataColumn[] dependency = DataTable.zeroColumns;
	}
}

using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.SqlTypes;

namespace System.Data
{
	// Token: 0x020001A7 RID: 423
	internal sealed class DataExpression : IFilter
	{
		// Token: 0x06001887 RID: 6279 RVA: 0x002541C8 File Offset: 0x002535C8
		internal DataExpression(DataTable table, string expression) : this(table, expression, null)
		{
		}

		// Token: 0x06001888 RID: 6280 RVA: 0x002541E8 File Offset: 0x002535E8
		internal DataExpression(DataTable table, string expression, Type type)
		{
			ExpressionParser expressionParser = new ExpressionParser(table);
			expressionParser.LoadExpression(expression);
			this.originalExpression = expression;
			this.expr = null;
			if (expression != null)
			{
				this._storageType = DataStorage.GetStorageType(type);
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

		// Token: 0x1700032D RID: 813
		// (get) Token: 0x06001889 RID: 6281 RVA: 0x00254268 File Offset: 0x00253668
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

		// Token: 0x1700032E RID: 814
		// (get) Token: 0x0600188A RID: 6282 RVA: 0x00254298 File Offset: 0x00253698
		internal ExpressionNode ExpressionNode
		{
			get
			{
				return this.expr;
			}
		}

		// Token: 0x1700032F RID: 815
		// (get) Token: 0x0600188B RID: 6283 RVA: 0x002542B8 File Offset: 0x002536B8
		internal bool HasValue
		{
			get
			{
				return null != this.expr;
			}
		}

		// Token: 0x0600188C RID: 6284 RVA: 0x002542D8 File Offset: 0x002536D8
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

		// Token: 0x0600188D RID: 6285 RVA: 0x00254338 File Offset: 0x00253738
		internal bool DependsOn(DataColumn column)
		{
			return this.expr != null && this.expr.DependsOn(column);
		}

		// Token: 0x0600188E RID: 6286 RVA: 0x00254368 File Offset: 0x00253768
		internal object Evaluate()
		{
			return this.Evaluate(null, DataRowVersion.Default);
		}

		// Token: 0x0600188F RID: 6287 RVA: 0x00254388 File Offset: 0x00253788
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

		// Token: 0x06001890 RID: 6288 RVA: 0x00254448 File Offset: 0x00253848
		internal object Evaluate(DataRow[] rows)
		{
			return this.Evaluate(rows, DataRowVersion.Default);
		}

		// Token: 0x06001891 RID: 6289 RVA: 0x00254468 File Offset: 0x00253868
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

		// Token: 0x06001892 RID: 6290 RVA: 0x002544F8 File Offset: 0x002538F8
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

		// Token: 0x06001893 RID: 6291 RVA: 0x00254568 File Offset: 0x00253968
		internal DataColumn[] GetDependency()
		{
			return this.dependency;
		}

		// Token: 0x06001894 RID: 6292 RVA: 0x00254588 File Offset: 0x00253988
		internal bool IsTableAggregate()
		{
			return this.expr != null && this.expr.IsTableConstant();
		}

		// Token: 0x06001895 RID: 6293 RVA: 0x002545B8 File Offset: 0x002539B8
		internal static bool IsUnknown(object value)
		{
			return DataStorage.IsObjectNull(value);
		}

		// Token: 0x06001896 RID: 6294 RVA: 0x002545D8 File Offset: 0x002539D8
		internal bool HasLocalAggregate()
		{
			return this.expr != null && this.expr.HasLocalAggregate();
		}

		// Token: 0x06001897 RID: 6295 RVA: 0x00254608 File Offset: 0x00253A08
		internal bool HasRemoteAggregate()
		{
			return this.expr != null && this.expr.HasRemoteAggregate();
		}

		// Token: 0x06001898 RID: 6296 RVA: 0x00254638 File Offset: 0x00253A38
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

		// Token: 0x04000D63 RID: 3427
		internal string originalExpression;

		// Token: 0x04000D64 RID: 3428
		private bool parsed;

		// Token: 0x04000D65 RID: 3429
		private bool bound;

		// Token: 0x04000D66 RID: 3430
		private ExpressionNode expr;

		// Token: 0x04000D67 RID: 3431
		private DataTable table;

		// Token: 0x04000D68 RID: 3432
		private readonly StorageType _storageType;

		// Token: 0x04000D69 RID: 3433
		private readonly Type _dataType;

		// Token: 0x04000D6A RID: 3434
		private DataColumn[] dependency = DataTable.zeroColumns;
	}
}

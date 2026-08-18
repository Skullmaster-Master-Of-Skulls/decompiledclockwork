using System;
using System.ComponentModel.Design.Data;
using System.Globalization;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design.WebControls
{
	// Token: 0x0200011C RID: 284
	internal sealed class SqlDataSourceFilterClause
	{
		// Token: 0x06000A63 RID: 2659 RVA: 0x00042B6C File Offset: 0x00040D6C
		public SqlDataSourceFilterClause(DesignerDataConnection designerDataConnection, DesignerDataTableBase designerDataTable, DesignerDataColumn designerDataColumn, string operatorFormat, bool isBinary, string value, Parameter parameter)
		{
			this._designerDataConnection = designerDataConnection;
			this._designerDataTable = designerDataTable;
			this._designerDataColumn = designerDataColumn;
			this._isBinary = isBinary;
			this._operatorFormat = operatorFormat;
			this._value = value;
			this._parameter = parameter;
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000A64 RID: 2660 RVA: 0x00042BA9 File Offset: 0x00040DA9
		public DesignerDataColumn DesignerDataColumn
		{
			get
			{
				return this._designerDataColumn;
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000A65 RID: 2661 RVA: 0x00042BB1 File Offset: 0x00040DB1
		public bool IsBinary
		{
			get
			{
				return this._isBinary;
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000A66 RID: 2662 RVA: 0x00042BB9 File Offset: 0x00040DB9
		public string OperatorFormat
		{
			get
			{
				return this._operatorFormat;
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000A67 RID: 2663 RVA: 0x00042BC1 File Offset: 0x00040DC1
		public Parameter Parameter
		{
			get
			{
				return this._parameter;
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000A68 RID: 2664 RVA: 0x00042BC9 File Offset: 0x00040DC9
		public string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x06000A69 RID: 2665 RVA: 0x00042BD4 File Offset: 0x00040DD4
		public override string ToString()
		{
			SqlDataSourceColumnData sqlDataSourceColumnData = new SqlDataSourceColumnData(this._designerDataConnection, this._designerDataColumn);
			if (this._isBinary)
			{
				return string.Format(CultureInfo.InvariantCulture, this._operatorFormat, new object[]
				{
					sqlDataSourceColumnData.EscapedName,
					this._value
				});
			}
			return string.Format(CultureInfo.InvariantCulture, this._operatorFormat, new object[]
			{
				sqlDataSourceColumnData.EscapedName
			});
		}

		// Token: 0x0400063F RID: 1599
		private DesignerDataColumn _designerDataColumn;

		// Token: 0x04000640 RID: 1600
		private DesignerDataTableBase _designerDataTable;

		// Token: 0x04000641 RID: 1601
		private DesignerDataConnection _designerDataConnection;

		// Token: 0x04000642 RID: 1602
		private bool _isBinary;

		// Token: 0x04000643 RID: 1603
		private string _operatorFormat;

		// Token: 0x04000644 RID: 1604
		private string _value;

		// Token: 0x04000645 RID: 1605
		private Parameter _parameter;
	}
}

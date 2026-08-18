using System;
using System.Data;

namespace System.ComponentModel.Design.Data
{
	// Token: 0x020001FB RID: 507
	public sealed class DesignerDataColumn
	{
		// Token: 0x06001328 RID: 4904 RVA: 0x0006F1A4 File Offset: 0x0006D3A4
		public DesignerDataColumn(string name, DbType dataType) : this(name, dataType, null, false, false, false, -1, -1, -1)
		{
		}

		// Token: 0x06001329 RID: 4905 RVA: 0x0006F1C0 File Offset: 0x0006D3C0
		public DesignerDataColumn(string name, DbType dataType, object defaultValue) : this(name, dataType, defaultValue, false, false, false, -1, -1, -1)
		{
		}

		// Token: 0x0600132A RID: 4906 RVA: 0x0006F1DC File Offset: 0x0006D3DC
		public DesignerDataColumn(string name, DbType dataType, object defaultValue, bool identity, bool nullable, bool primaryKey, int precision, int scale, int length)
		{
			this._dataType = dataType;
			this._defaultValue = defaultValue;
			this._identity = identity;
			this._length = length;
			this._name = name;
			this._nullable = nullable;
			this._precision = precision;
			this._primaryKey = primaryKey;
			this._scale = scale;
		}

		// Token: 0x17000434 RID: 1076
		// (get) Token: 0x0600132B RID: 4907 RVA: 0x0006F234 File Offset: 0x0006D434
		public DbType DataType
		{
			get
			{
				return this._dataType;
			}
		}

		// Token: 0x17000435 RID: 1077
		// (get) Token: 0x0600132C RID: 4908 RVA: 0x0006F23C File Offset: 0x0006D43C
		public object DefaultValue
		{
			get
			{
				return this._defaultValue;
			}
		}

		// Token: 0x17000436 RID: 1078
		// (get) Token: 0x0600132D RID: 4909 RVA: 0x0006F244 File Offset: 0x0006D444
		public bool Identity
		{
			get
			{
				return this._identity;
			}
		}

		// Token: 0x17000437 RID: 1079
		// (get) Token: 0x0600132E RID: 4910 RVA: 0x0006F24C File Offset: 0x0006D44C
		public int Length
		{
			get
			{
				return this._length;
			}
		}

		// Token: 0x17000438 RID: 1080
		// (get) Token: 0x0600132F RID: 4911 RVA: 0x0006F254 File Offset: 0x0006D454
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000439 RID: 1081
		// (get) Token: 0x06001330 RID: 4912 RVA: 0x0006F25C File Offset: 0x0006D45C
		public bool Nullable
		{
			get
			{
				return this._nullable;
			}
		}

		// Token: 0x1700043A RID: 1082
		// (get) Token: 0x06001331 RID: 4913 RVA: 0x0006F264 File Offset: 0x0006D464
		public int Precision
		{
			get
			{
				return this._precision;
			}
		}

		// Token: 0x1700043B RID: 1083
		// (get) Token: 0x06001332 RID: 4914 RVA: 0x0006F26C File Offset: 0x0006D46C
		public bool PrimaryKey
		{
			get
			{
				return this._primaryKey;
			}
		}

		// Token: 0x1700043C RID: 1084
		// (get) Token: 0x06001333 RID: 4915 RVA: 0x0006F274 File Offset: 0x0006D474
		public int Scale
		{
			get
			{
				return this._scale;
			}
		}

		// Token: 0x04000A57 RID: 2647
		private DbType _dataType;

		// Token: 0x04000A58 RID: 2648
		private object _defaultValue;

		// Token: 0x04000A59 RID: 2649
		private bool _identity;

		// Token: 0x04000A5A RID: 2650
		private int _length;

		// Token: 0x04000A5B RID: 2651
		private string _name;

		// Token: 0x04000A5C RID: 2652
		private bool _nullable;

		// Token: 0x04000A5D RID: 2653
		private int _precision;

		// Token: 0x04000A5E RID: 2654
		private bool _primaryKey;

		// Token: 0x04000A5F RID: 2655
		private int _scale;
	}
}

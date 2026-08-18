using System;
using System.Data;

namespace System.ComponentModel.Design.Data
{
	// Token: 0x020001FD RID: 509
	public sealed class DesignerDataParameter
	{
		// Token: 0x0600133A RID: 4922 RVA: 0x0006F2CD File Offset: 0x0006D4CD
		public DesignerDataParameter(string name, DbType dataType, ParameterDirection direction)
		{
			this._dataType = dataType;
			this._direction = direction;
			this._name = name;
		}

		// Token: 0x17000441 RID: 1089
		// (get) Token: 0x0600133B RID: 4923 RVA: 0x0006F2EA File Offset: 0x0006D4EA
		public DbType DataType
		{
			get
			{
				return this._dataType;
			}
		}

		// Token: 0x17000442 RID: 1090
		// (get) Token: 0x0600133C RID: 4924 RVA: 0x0006F2F2 File Offset: 0x0006D4F2
		public ParameterDirection Direction
		{
			get
			{
				return this._direction;
			}
		}

		// Token: 0x17000443 RID: 1091
		// (get) Token: 0x0600133D RID: 4925 RVA: 0x0006F2FA File Offset: 0x0006D4FA
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x04000A64 RID: 2660
		private DbType _dataType;

		// Token: 0x04000A65 RID: 2661
		private ParameterDirection _direction;

		// Token: 0x04000A66 RID: 2662
		private string _name;
	}
}

using System;
using System.Data.OleDb;

namespace Spire.DataExport.Access
{
	// Token: 0x020001E9 RID: 489
	public struct ParameterData
	{
		// Token: 0x06000ED6 RID: 3798 RVA: 0x000A3BA4 File Offset: 0x000A2BA4
		public ParameterData(string Name, OleDbType Type, int Size, string ColumnName)
		{
			this.Name = Name;
			this.Type = Type;
			this.Size = Size;
			this.ColumnName = ColumnName;
		}

		// Token: 0x04000B42 RID: 2882
		private bool \u2593\u0099\u0080\u00A4;

		// Token: 0x04000B43 RID: 2883
		public string Name;

		// Token: 0x04000B44 RID: 2884
		private float[] \u2460\u009A\u009F\u0096;

		// Token: 0x04000B45 RID: 2885
		public OleDbType Type;

		// Token: 0x04000B46 RID: 2886
		private bool \u25D9\u0096\u00AD\u0089;

		// Token: 0x04000B47 RID: 2887
		private bool[] \u2609\u009B\u009F\u00AF;

		// Token: 0x04000B48 RID: 2888
		private bool \u2593\u00A2\u0089\u0094;

		// Token: 0x04000B49 RID: 2889
		public int Size;

		// Token: 0x04000B4A RID: 2890
		public string ColumnName;
	}
}

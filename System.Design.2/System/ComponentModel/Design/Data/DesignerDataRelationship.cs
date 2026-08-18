using System;
using System.Collections;

namespace System.ComponentModel.Design.Data
{
	// Token: 0x020001FE RID: 510
	public sealed class DesignerDataRelationship
	{
		// Token: 0x0600133E RID: 4926 RVA: 0x0006F302 File Offset: 0x0006D502
		public DesignerDataRelationship(string name, ICollection parentColumns, DesignerDataTable childTable, ICollection childColumns)
		{
			this._childColumns = childColumns;
			this._childTable = childTable;
			this._name = name;
			this._parentColumns = parentColumns;
		}

		// Token: 0x17000444 RID: 1092
		// (get) Token: 0x0600133F RID: 4927 RVA: 0x0006F327 File Offset: 0x0006D527
		public ICollection ChildColumns
		{
			get
			{
				return this._childColumns;
			}
		}

		// Token: 0x17000445 RID: 1093
		// (get) Token: 0x06001340 RID: 4928 RVA: 0x0006F32F File Offset: 0x0006D52F
		public DesignerDataTable ChildTable
		{
			get
			{
				return this._childTable;
			}
		}

		// Token: 0x17000446 RID: 1094
		// (get) Token: 0x06001341 RID: 4929 RVA: 0x0006F337 File Offset: 0x0006D537
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000447 RID: 1095
		// (get) Token: 0x06001342 RID: 4930 RVA: 0x0006F33F File Offset: 0x0006D53F
		public ICollection ParentColumns
		{
			get
			{
				return this._parentColumns;
			}
		}

		// Token: 0x04000A67 RID: 2663
		private ICollection _childColumns;

		// Token: 0x04000A68 RID: 2664
		private DesignerDataTable _childTable;

		// Token: 0x04000A69 RID: 2665
		private string _name;

		// Token: 0x04000A6A RID: 2666
		private ICollection _parentColumns;
	}
}

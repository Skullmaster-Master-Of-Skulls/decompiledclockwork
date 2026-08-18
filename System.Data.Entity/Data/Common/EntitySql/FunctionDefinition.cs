using System;
using System.Data.Common.CommandTrees;

namespace System.Data.Common.EntitySql
{
	// Token: 0x02000359 RID: 857
	public sealed class FunctionDefinition
	{
		// Token: 0x060031C4 RID: 12740 RVA: 0x000C3D56 File Offset: 0x000C1F56
		internal FunctionDefinition(string name, DbLambda lambda, int startPosition, int endPosition)
		{
			this._name = name;
			this._lambda = lambda;
			this._startPosition = startPosition;
			this._endPosition = endPosition;
		}

		// Token: 0x17000996 RID: 2454
		// (get) Token: 0x060031C5 RID: 12741 RVA: 0x000C3D7B File Offset: 0x000C1F7B
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000997 RID: 2455
		// (get) Token: 0x060031C6 RID: 12742 RVA: 0x000C3D83 File Offset: 0x000C1F83
		public DbLambda Lambda
		{
			get
			{
				return this._lambda;
			}
		}

		// Token: 0x17000998 RID: 2456
		// (get) Token: 0x060031C7 RID: 12743 RVA: 0x000C3D8B File Offset: 0x000C1F8B
		public int StartPosition
		{
			get
			{
				return this._startPosition;
			}
		}

		// Token: 0x17000999 RID: 2457
		// (get) Token: 0x060031C8 RID: 12744 RVA: 0x000C3D93 File Offset: 0x000C1F93
		public int EndPosition
		{
			get
			{
				return this._endPosition;
			}
		}

		// Token: 0x0400159E RID: 5534
		private readonly string _name;

		// Token: 0x0400159F RID: 5535
		private readonly DbLambda _lambda;

		// Token: 0x040015A0 RID: 5536
		private readonly int _startPosition;

		// Token: 0x040015A1 RID: 5537
		private readonly int _endPosition;
	}
}

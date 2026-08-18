using System;
using System.Data.Entity.Core.Common.CommandTrees;

namespace System.Data.Entity.Core.Common.EntitySql
{
	// Token: 0x02000250 RID: 592
	public sealed class FunctionDefinition
	{
		// Token: 0x060014BC RID: 5308 RVA: 0x00062981 File Offset: 0x00060B81
		internal FunctionDefinition(string name, DbLambda lambda, int startPosition, int endPosition)
		{
			this._name = name;
			this._lambda = lambda;
			this._startPosition = startPosition;
			this._endPosition = endPosition;
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x060014BD RID: 5309 RVA: 0x000629A6 File Offset: 0x00060BA6
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x060014BE RID: 5310 RVA: 0x000629AE File Offset: 0x00060BAE
		public DbLambda Lambda
		{
			get
			{
				return this._lambda;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x060014BF RID: 5311 RVA: 0x000629B6 File Offset: 0x00060BB6
		public int StartPosition
		{
			get
			{
				return this._startPosition;
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x060014C0 RID: 5312 RVA: 0x000629BE File Offset: 0x00060BBE
		public int EndPosition
		{
			get
			{
				return this._endPosition;
			}
		}

		// Token: 0x0400071B RID: 1819
		private readonly string _name;

		// Token: 0x0400071C RID: 1820
		private readonly DbLambda _lambda;

		// Token: 0x0400071D RID: 1821
		private readonly int _startPosition;

		// Token: 0x0400071E RID: 1822
		private readonly int _endPosition;
	}
}

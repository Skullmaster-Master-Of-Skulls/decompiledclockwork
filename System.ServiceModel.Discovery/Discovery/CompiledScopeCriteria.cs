using System;

namespace System.ServiceModel.Discovery
{
	// Token: 0x0200000C RID: 12
	internal class CompiledScopeCriteria
	{
		// Token: 0x060000A8 RID: 168 RVA: 0x0000384F File Offset: 0x00001A4F
		public CompiledScopeCriteria(string compiledScope, CompiledScopeCriteriaMatchBy matchBy)
		{
			this.compiledScope = compiledScope;
			this.matchBy = matchBy;
		}

		// Token: 0x17000018 RID: 24
		// (get) Token: 0x060000A9 RID: 169 RVA: 0x00003865 File Offset: 0x00001A65
		public string CompiledScope
		{
			get
			{
				return this.compiledScope;
			}
		}

		// Token: 0x17000019 RID: 25
		// (get) Token: 0x060000AA RID: 170 RVA: 0x0000386D File Offset: 0x00001A6D
		public CompiledScopeCriteriaMatchBy MatchBy
		{
			get
			{
				return this.matchBy;
			}
		}

		// Token: 0x0400002F RID: 47
		private string compiledScope;

		// Token: 0x04000030 RID: 48
		private CompiledScopeCriteriaMatchBy matchBy;
	}
}

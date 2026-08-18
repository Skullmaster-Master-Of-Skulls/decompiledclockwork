using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Entity.Core.Common.CommandTrees.Internal;
using System.Data.Entity.Core.Metadata.Edm;
using System.Linq;

namespace System.Data.Entity.Core.Common.CommandTrees
{
	// Token: 0x0200010F RID: 271
	public abstract class DbModificationCommandTree : DbCommandTree
	{
		// Token: 0x0600071B RID: 1819 RVA: 0x00026D56 File Offset: 0x00024F56
		internal DbModificationCommandTree()
		{
		}

		// Token: 0x0600071C RID: 1820 RVA: 0x00026D5E File Offset: 0x00024F5E
		internal DbModificationCommandTree(MetadataWorkspace metadata, DataSpace dataSpace, DbExpressionBinding target) : base(metadata, dataSpace, true)
		{
			this._target = target;
		}

		// Token: 0x17000088 RID: 136
		// (get) Token: 0x0600071D RID: 1821 RVA: 0x00026D70 File Offset: 0x00024F70
		public DbExpressionBinding Target
		{
			get
			{
				return this._target;
			}
		}

		// Token: 0x17000089 RID: 137
		// (get) Token: 0x0600071E RID: 1822
		internal abstract bool HasReader { get; }

		// Token: 0x0600071F RID: 1823 RVA: 0x00026D8B File Offset: 0x00024F8B
		internal override IEnumerable<KeyValuePair<string, TypeUsage>> GetParameters()
		{
			if (this._parameters == null)
			{
				this._parameters = ParameterRetriever.GetParameters(this);
			}
			return from p in this._parameters
			select new KeyValuePair<string, TypeUsage>(p.ParameterName, p.ResultType);
		}

		// Token: 0x06000720 RID: 1824 RVA: 0x00026DC9 File Offset: 0x00024FC9
		internal override void DumpStructure(ExpressionDumper dumper)
		{
			if (this.Target != null)
			{
				dumper.Dump(this.Target, "Target");
			}
		}

		// Token: 0x04000205 RID: 517
		private readonly DbExpressionBinding _target;

		// Token: 0x04000206 RID: 518
		private ReadOnlyCollection<DbParameterReferenceExpression> _parameters;
	}
}

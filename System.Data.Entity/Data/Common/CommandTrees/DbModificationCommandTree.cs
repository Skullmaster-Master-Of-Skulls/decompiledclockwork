using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Data.Common.CommandTrees.Internal;
using System.Data.Metadata.Edm;
using System.Linq;

namespace System.Data.Common.CommandTrees
{
	// Token: 0x020003EA RID: 1002
	public abstract class DbModificationCommandTree : DbCommandTree
	{
		// Token: 0x060035CB RID: 13771 RVA: 0x000CFD79 File Offset: 0x000CDF79
		internal DbModificationCommandTree(MetadataWorkspace metadata, DataSpace dataSpace, DbExpressionBinding target) : base(metadata, dataSpace)
		{
			EntityUtil.CheckArgumentNull<DbExpressionBinding>(target, "target");
			this._target = target;
		}

		// Token: 0x17000A46 RID: 2630
		// (get) Token: 0x060035CC RID: 13772 RVA: 0x000CFD96 File Offset: 0x000CDF96
		public DbExpressionBinding Target
		{
			get
			{
				return this._target;
			}
		}

		// Token: 0x17000A47 RID: 2631
		// (get) Token: 0x060035CD RID: 13773
		internal abstract bool HasReader { get; }

		// Token: 0x060035CE RID: 13774 RVA: 0x000CFD9E File Offset: 0x000CDF9E
		internal override IEnumerable<KeyValuePair<string, TypeUsage>> GetParameters()
		{
			if (this._parameters == null)
			{
				this._parameters = ParameterRetriever.GetParameters(this);
			}
			return from p in this._parameters
			select new KeyValuePair<string, TypeUsage>(p.ParameterName, p.ResultType);
		}

		// Token: 0x060035CF RID: 13775 RVA: 0x000CFDDE File Offset: 0x000CDFDE
		internal override void DumpStructure(ExpressionDumper dumper)
		{
			if (this.Target != null)
			{
				dumper.Dump(this.Target, "Target");
			}
		}

		// Token: 0x040017AD RID: 6061
		private readonly DbExpressionBinding _target;

		// Token: 0x040017AE RID: 6062
		private ReadOnlyCollection<DbParameterReferenceExpression> _parameters;
	}
}

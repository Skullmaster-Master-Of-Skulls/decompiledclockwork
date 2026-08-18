using System;
using System.Collections.Generic;
using System.Linq;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000016 RID: 22
	internal abstract class DataModelValidationRuleSet
	{
		// Token: 0x060000BA RID: 186 RVA: 0x00004DAA File Offset: 0x00002FAA
		protected void AddRule(DataModelValidationRule rule)
		{
			this._rules.Add(rule);
		}

		// Token: 0x060000BB RID: 187 RVA: 0x00004DB8 File Offset: 0x00002FB8
		protected void RemoveRule(DataModelValidationRule rule)
		{
			this._rules.Remove(rule);
		}

		// Token: 0x060000BC RID: 188 RVA: 0x00004DE4 File Offset: 0x00002FE4
		internal IEnumerable<DataModelValidationRule> GetRules(MetadataItem itemToValidate)
		{
			return from r in this._rules
			where r.ValidatedType.IsInstanceOfType(itemToValidate)
			select r;
		}

		// Token: 0x04000025 RID: 37
		private readonly List<DataModelValidationRule> _rules = new List<DataModelValidationRule>();
	}
}

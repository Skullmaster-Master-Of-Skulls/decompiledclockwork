using System;
using System.Collections.Generic;
using System.Data.Entity.Edm;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x02000025 RID: 37
	internal sealed class EdmModelValidationVisitor : EdmModelVisitor
	{
		// Token: 0x0600016C RID: 364 RVA: 0x0000806B File Offset: 0x0000626B
		internal EdmModelValidationVisitor(EdmModelValidationContext context, EdmModelRuleSet ruleSet)
		{
			this._context = context;
			this._ruleSet = ruleSet;
		}

		// Token: 0x0600016D RID: 365 RVA: 0x0000808C File Offset: 0x0000628C
		protected internal override void VisitMetadataItem(MetadataItem item)
		{
			if (this._visitedItems.Add(item))
			{
				this.EvaluateItem(item);
			}
		}

		// Token: 0x0600016E RID: 366 RVA: 0x000080A4 File Offset: 0x000062A4
		private void EvaluateItem(MetadataItem item)
		{
			foreach (DataModelValidationRule dataModelValidationRule in this._ruleSet.GetRules(item))
			{
				dataModelValidationRule.Evaluate(this._context, item);
			}
		}

		// Token: 0x0600016F RID: 367 RVA: 0x00008100 File Offset: 0x00006300
		internal void Visit(EdmModel model)
		{
			this.EvaluateItem(model);
			this.VisitEdmModel(model);
		}

		// Token: 0x040000AA RID: 170
		private readonly EdmModelValidationContext _context;

		// Token: 0x040000AB RID: 171
		private readonly EdmModelRuleSet _ruleSet;

		// Token: 0x040000AC RID: 172
		private readonly HashSet<MetadataItem> _visitedItems = new HashSet<MetadataItem>();
	}
}

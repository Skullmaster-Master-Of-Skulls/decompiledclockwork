using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.Public.Entities.Adapters;

namespace TechnoPro.Common.Public.Entities.AlertTrigger
{
	// Token: 0x0200059E RID: 1438
	public class AlertTriggerCollection
	{
		// Token: 0x06002EBA RID: 11962 RVA: 0x0000D55A File Offset: 0x0000B75A
		public AlertTriggerCollection()
		{
		}

		// Token: 0x06002EBB RID: 11963 RVA: 0x000336A8 File Offset: 0x000318A8
		public AlertTriggerCollection(IEnumerable<IAlertTriggerDefinition> triggers)
		{
			var source = (from g in triggers
			select new
			{
				TriggerType = g.GetAlertTriggerType(),
				Trigger = g
			}).ToList();
			this.Items = (from p in source
			group p by p.TriggerType).ToDictionary(g => g.Key, g => (from m in g
			select m.Trigger).ToList<IAlertTriggerDefinition>());
		}

		// Token: 0x170013A3 RID: 5027
		// (get) Token: 0x06002EBC RID: 11964 RVA: 0x00033757 File Offset: 0x00031957
		// (set) Token: 0x06002EBD RID: 11965 RVA: 0x0003375F File Offset: 0x0003195F
		public IDictionary<eAlertTriggerType, IList<IAlertTriggerDefinition>> Items { get; set; }
	}
}

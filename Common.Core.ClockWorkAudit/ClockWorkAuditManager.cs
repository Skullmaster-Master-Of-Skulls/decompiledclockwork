using System;
using System.Collections.Generic;
using System.Linq;
using TechnoPro.Common.ICore.ClockWorkAudit;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.ClockWorkAudit;

namespace TechnoPro.Common.Core.ClockWorkAudit
{
	// Token: 0x02000002 RID: 2
	public class ClockWorkAuditManager : IClockWorkAuditManager, IOperationContext, IBaseOperationContext<OperationContext>
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public ClockWorkAuditManager()
		{
		}

		// Token: 0x06000002 RID: 2 RVA: 0x0000205A File Offset: 0x0000025A
		public ClockWorkAuditManager(OperationContext opContext)
		{
			this.OpContext = opContext;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x06000003 RID: 3 RVA: 0x0000206C File Offset: 0x0000026C
		// (set) Token: 0x06000004 RID: 4 RVA: 0x00002074 File Offset: 0x00000274
		public OperationContext OpContext { get; set; }

		// Token: 0x06000005 RID: 5 RVA: 0x00002080 File Offset: 0x00000280
		public IList<AuditResult> ExecuteFullAudit()
		{
			List<KeyValuePair<eClockWorkAuditType, ClockWorkAuditTypeAttribute>> source = (from m in ((eClockWorkAuditType[])Enum.GetValues(typeof(eClockWorkAuditType))).ToDictionary((eClockWorkAuditType g) => g, (eClockWorkAuditType h) => h.GetAttribute<ClockWorkAuditTypeAttribute>())
			where !m.Value.IsDisabled
			select m).ToList<KeyValuePair<eClockWorkAuditType, ClockWorkAuditTypeAttribute>>();
			return (from audit in source
			select this.ExecuteAudit(audit.Key, audit.Value)).ToList<AuditResult>();
		}

		// Token: 0x06000006 RID: 6 RVA: 0x0000212C File Offset: 0x0000032C
		public AuditResult ExecuteAudit(eClockWorkAuditType AuditType)
		{
			return this.ExecuteAudit(AuditType, AuditType.GetAttribute<ClockWorkAuditTypeAttribute>());
		}

		// Token: 0x06000007 RID: 7 RVA: 0x00002150 File Offset: 0x00000350
		private AuditResult ExecuteAudit(eClockWorkAuditType AuditType, ClockWorkAuditTypeAttribute AuditTypeAttribute)
		{
			AuditResult result;
			try
			{
				string typeName = string.Format("TechnoPro.Common.Core.ClockWorkAudit.AuditHandlers.{0}", AuditType);
				Type type = Type.GetType(typeName);
				IClockWorkAuditHandler clockWorkAuditHandler = (IClockWorkAuditHandler)Activator.CreateInstance(type, new object[]
				{
					this.OpContext
				});
				result = clockWorkAuditHandler.ExecuteAudit();
			}
			catch (Exception ex)
			{
				result = new AuditResult(AuditType)
				{
					Checks = new List<AuditCheck>
					{
						new AuditCheck("Basic audit handler execution failed", eAuditStatus.Failed, new string[]
						{
							"err={0}",
							ex.ToString()
						})
					}
				};
			}
			return result;
		}
	}
}

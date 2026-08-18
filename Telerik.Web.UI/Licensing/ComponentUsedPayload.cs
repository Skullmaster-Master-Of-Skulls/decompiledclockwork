using System;

namespace Telerik.Licensing
{
	// Token: 0x02000439 RID: 1081
	internal class ComponentUsedPayload : ProductUsedPayload
	{
		// Token: 0x060026C7 RID: 9927 RVA: 0x0007E8C0 File Offset: 0x0007CAC0
		public ComponentUsedPayload(Type type, string machineId, string sessionId) : base(type, machineId, sessionId)
		{
			this.ComponentType = type.FullName;
			base.Type = "ComponentUsed";
		}

		// Token: 0x17000C75 RID: 3189
		// (get) Token: 0x060026C8 RID: 9928 RVA: 0x0007E8E2 File Offset: 0x0007CAE2
		// (set) Token: 0x060026C9 RID: 9929 RVA: 0x0007E8EA File Offset: 0x0007CAEA
		public string ComponentType { get; set; }

		// Token: 0x040009F8 RID: 2552
		private const string EventType = "ComponentUsed";
	}
}

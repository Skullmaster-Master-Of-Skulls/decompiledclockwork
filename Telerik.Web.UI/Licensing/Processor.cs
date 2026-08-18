using System;

namespace Telerik.Licensing
{
	// Token: 0x02000404 RID: 1028
	internal class Processor : Device
	{
		// Token: 0x0600259C RID: 9628 RVA: 0x0007CCBC File Offset: 0x0007AEBC
		public Processor() : base("Win32_Processor")
		{
		}

		// Token: 0x0600259D RID: 9629 RVA: 0x0007CD02 File Offset: 0x0007AF02
		public override string[] GetWmiProperties()
		{
			return this._wmiProperties;
		}

		// Token: 0x0600259E RID: 9630 RVA: 0x0007CD0A File Offset: 0x0007AF0A
		public static string GetId()
		{
			return Device.GetId(typeof(Processor));
		}

		// Token: 0x04000991 RID: 2449
		private const string WmiClass = "Win32_Processor";

		// Token: 0x04000992 RID: 2450
		private readonly string[] _wmiProperties = new string[]
		{
			"UniqueId",
			"ProcessorId",
			"Name",
			"Manufacturer"
		};
	}
}

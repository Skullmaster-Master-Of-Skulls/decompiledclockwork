using System;

namespace Telerik.Licensing
{
	// Token: 0x02000402 RID: 1026
	internal class Bios : Device
	{
		// Token: 0x06002591 RID: 9617 RVA: 0x0007CA9C File Offset: 0x0007AC9C
		public Bios() : base("Win32_BIOS")
		{
		}

		// Token: 0x06002592 RID: 9618 RVA: 0x0007CADA File Offset: 0x0007ACDA
		public override string[] GetWmiProperties()
		{
			return this._wmiProperties;
		}

		// Token: 0x06002593 RID: 9619 RVA: 0x0007CAE2 File Offset: 0x0007ACE2
		public static string GetId()
		{
			return Device.GetId(typeof(Bios));
		}

		// Token: 0x0400098B RID: 2443
		private const string WmiClass = "Win32_BIOS";

		// Token: 0x0400098C RID: 2444
		private readonly string[] _wmiProperties = new string[]
		{
			"Manufacturer",
			"SerialNumber",
			"Name"
		};
	}
}

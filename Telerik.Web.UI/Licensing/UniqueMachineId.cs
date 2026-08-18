using System;

namespace Telerik.Licensing
{
	// Token: 0x02000408 RID: 1032
	internal class UniqueMachineId
	{
		// Token: 0x060025B1 RID: 9649 RVA: 0x0007CEFD File Offset: 0x0007B0FD
		public UniqueMachineId(IHashingService service)
		{
			this._hashService = service;
			this.Id = this.ReadKey();
		}

		// Token: 0x17000C3A RID: 3130
		// (get) Token: 0x060025B2 RID: 9650 RVA: 0x0007CF18 File Offset: 0x0007B118
		// (set) Token: 0x060025B3 RID: 9651 RVA: 0x0007CF20 File Offset: 0x0007B120
		public string Id { get; private set; }

		// Token: 0x060025B4 RID: 9652 RVA: 0x0007CF29 File Offset: 0x0007B129
		public static string GetIdWithDefaultHash()
		{
			return new UniqueMachineId(HashService.GetInstance()).Id;
		}

		// Token: 0x060025B5 RID: 9653 RVA: 0x0007CF3A File Offset: 0x0007B13A
		public override string ToString()
		{
			return this.Id;
		}

		// Token: 0x060025B6 RID: 9654 RVA: 0x0007CF44 File Offset: 0x0007B144
		private string ReadKey()
		{
			string result;
			try
			{
				result = this._hashService.Sha256(Bios.GetId() + Hdd.GetId() + Processor.GetId());
			}
			catch
			{
				result = "ZGVmYXVsdF9pZA==";
			}
			return result;
		}

		// Token: 0x040009A2 RID: 2466
		private const string DefaultId = "ZGVmYXVsdF9pZA==";

		// Token: 0x040009A3 RID: 2467
		private readonly IHashingService _hashService;
	}
}

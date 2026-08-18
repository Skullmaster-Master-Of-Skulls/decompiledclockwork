using System;
using System.Web.Security;

namespace System.Web.Helpers.AntiXsrf
{
	// Token: 0x02000023 RID: 35
	internal sealed class MachineKey45CryptoSystem : ICryptoSystem
	{
		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000103 RID: 259 RVA: 0x0000450A File Offset: 0x0000270A
		public static MachineKey45CryptoSystem Instance
		{
			get
			{
				return MachineKey45CryptoSystem._singletonInstance;
			}
		}

		// Token: 0x06000104 RID: 260 RVA: 0x00004511 File Offset: 0x00002711
		private static MachineKey45CryptoSystem GetSingletonInstance()
		{
			return new MachineKey45CryptoSystem();
		}

		// Token: 0x06000105 RID: 261 RVA: 0x00004518 File Offset: 0x00002718
		public string Protect(byte[] data)
		{
			byte[] input = MachineKey.Protect(data, MachineKey45CryptoSystem._purposes);
			return HttpServerUtility.UrlTokenEncode(input);
		}

		// Token: 0x06000106 RID: 262 RVA: 0x00004538 File Offset: 0x00002738
		public byte[] Unprotect(string protectedData)
		{
			byte[] protectedData2 = HttpServerUtility.UrlTokenDecode(protectedData);
			return MachineKey.Unprotect(protectedData2, MachineKey45CryptoSystem._purposes);
		}

		// Token: 0x04000055 RID: 85
		private static readonly string[] _purposes = new string[]
		{
			"System.Web.Helpers.AntiXsrf.AntiForgeryToken.v1"
		};

		// Token: 0x04000056 RID: 86
		private static readonly MachineKey45CryptoSystem _singletonInstance = MachineKey45CryptoSystem.GetSingletonInstance();
	}
}

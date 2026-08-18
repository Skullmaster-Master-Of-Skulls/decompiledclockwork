using System;

namespace System.IdentityModel.Claims
{
	// Token: 0x020001DD RID: 477
	public static class Rights
	{
		// Token: 0x17000430 RID: 1072
		// (get) Token: 0x06000FCA RID: 4042 RVA: 0x00044C52 File Offset: 0x00042E52
		public static string Identity
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/right/identity";
			}
		}

		// Token: 0x17000431 RID: 1073
		// (get) Token: 0x06000FCB RID: 4043 RVA: 0x00044C59 File Offset: 0x00042E59
		public static string PossessProperty
		{
			get
			{
				return "http://schemas.xmlsoap.org/ws/2005/05/identity/right/possessproperty";
			}
		}

		// Token: 0x04000DC7 RID: 3527
		private const string rightNamespace = "http://schemas.xmlsoap.org/ws/2005/05/identity/right";

		// Token: 0x04000DC8 RID: 3528
		private const string identity = "http://schemas.xmlsoap.org/ws/2005/05/identity/right/identity";

		// Token: 0x04000DC9 RID: 3529
		private const string possessProperty = "http://schemas.xmlsoap.org/ws/2005/05/identity/right/possessproperty";
	}
}

using System;
using System.Security.Cryptography.X509Certificates;

namespace System.ServiceModel.Security
{
	// Token: 0x0200034C RID: 844
	internal static class StoreLocationHelper
	{
		// Token: 0x06001EAB RID: 7851 RVA: 0x00071964 File Offset: 0x0006FB64
		internal static bool IsDefined(StoreLocation value)
		{
			return value == StoreLocation.CurrentUser || value == StoreLocation.LocalMachine;
		}
	}
}

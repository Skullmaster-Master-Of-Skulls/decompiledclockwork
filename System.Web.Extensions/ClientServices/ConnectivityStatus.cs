using System;
using System.IO;
using System.Windows.Forms;

namespace System.Web.ClientServices
{
	// Token: 0x0200010C RID: 268
	public static class ConnectivityStatus
	{
		// Token: 0x17000530 RID: 1328
		// (get) Token: 0x06000DF5 RID: 3573 RVA: 0x000310C1 File Offset: 0x0002F2C1
		// (set) Token: 0x06000DF6 RID: 3574 RVA: 0x000310D4 File Offset: 0x0002F2D4
		public static bool IsOffline
		{
			get
			{
				if (!ConnectivityStatus._IsOfflineFetched)
				{
					ConnectivityStatus.FetchIsOffline();
				}
				return ConnectivityStatus._IsOffline;
			}
			set
			{
				if (ConnectivityStatus.IsOffline != value)
				{
					ConnectivityStatus._IsOffline = value;
					ConnectivityStatus.StoreIsOffline();
				}
			}
		}

		// Token: 0x06000DF7 RID: 3575 RVA: 0x000310EC File Offset: 0x0002F2EC
		private static void FetchIsOffline()
		{
			string path = Path.Combine(Application.UserAppDataPath, "AppIsOffline");
			ConnectivityStatus._IsOffline = File.Exists(path);
			ConnectivityStatus._IsOfflineFetched = true;
		}

		// Token: 0x06000DF8 RID: 3576 RVA: 0x0003111C File Offset: 0x0002F31C
		private static void StoreIsOffline()
		{
			string path = Path.Combine(Application.UserAppDataPath, "AppIsOffline");
			if (!ConnectivityStatus._IsOffline)
			{
				File.Delete(path);
				return;
			}
			using (FileStream fileStream = File.Create(path))
			{
				fileStream.Write(new byte[0], 0, 0);
			}
		}

		// Token: 0x040003ED RID: 1005
		private static bool _IsOffline;

		// Token: 0x040003EE RID: 1006
		private static bool _IsOfflineFetched;
	}
}

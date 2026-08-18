using System;
using System.Web.UI;

namespace Telerik.Web.UI.PersistenceFramework
{
	// Token: 0x02000193 RID: 403
	public interface IStatePersister
	{
		// Token: 0x06000DB7 RID: 3511
		void SaveState(Control c, string key);

		// Token: 0x06000DB8 RID: 3512
		void LoadState(Control c, string key);

		// Token: 0x06000DB9 RID: 3513
		void LoadState(Control c, RadControlState state);

		// Token: 0x06000DBA RID: 3514
		void ReadSettings(Control c);

		// Token: 0x06000DBB RID: 3515
		void ApplySettings(Control c);
	}
}

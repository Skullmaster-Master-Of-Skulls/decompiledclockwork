using System;

namespace Telerik.Web.UI
{
	// Token: 0x020018A0 RID: 6304
	public interface IRadFilterCommandEvent
	{
		// Token: 0x17004979 RID: 18809
		// (get) Token: 0x0600F3DA RID: 62426
		// (set) Token: 0x0600F3DB RID: 62427
		bool Canceled { get; set; }

		// Token: 0x0600F3DC RID: 62428
		void ExecuteCommand(object source);
	}
}

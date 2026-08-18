using System;

namespace Telerik.Web.UI
{
	// Token: 0x0200120A RID: 4618
	public interface ITreeListCommandEvent
	{
		// Token: 0x17003DA4 RID: 15780
		// (get) Token: 0x0600BF04 RID: 48900
		// (set) Token: 0x0600BF05 RID: 48901
		bool Canceled { get; set; }

		// Token: 0x0600BF06 RID: 48902
		void ExecuteCommand(object source);
	}
}

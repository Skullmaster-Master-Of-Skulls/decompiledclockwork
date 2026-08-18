using System;
using System.Runtime.InteropServices;

namespace System.ComponentModel.Design
{
	// Token: 0x020005F2 RID: 1522
	[ComVisible(true)]
	public interface IMenuCommandService
	{
		// Token: 0x17000D7B RID: 3451
		// (get) Token: 0x0600383E RID: 14398
		DesignerVerbCollection Verbs { get; }

		// Token: 0x0600383F RID: 14399
		void AddCommand(MenuCommand command);

		// Token: 0x06003840 RID: 14400
		void AddVerb(DesignerVerb verb);

		// Token: 0x06003841 RID: 14401
		MenuCommand FindCommand(CommandID commandID);

		// Token: 0x06003842 RID: 14402
		bool GlobalInvoke(CommandID commandID);

		// Token: 0x06003843 RID: 14403
		void RemoveCommand(MenuCommand command);

		// Token: 0x06003844 RID: 14404
		void RemoveVerb(DesignerVerb verb);

		// Token: 0x06003845 RID: 14405
		void ShowContextMenu(CommandID menuID, int x, int y);
	}
}

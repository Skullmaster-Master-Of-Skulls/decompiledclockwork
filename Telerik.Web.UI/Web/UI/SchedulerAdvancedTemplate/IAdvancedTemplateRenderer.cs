using System;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.SchedulerAdvancedTemplate
{
	// Token: 0x0200080E RID: 2062
	internal interface IAdvancedTemplateRenderer
	{
		// Token: 0x170018AA RID: 6314
		// (get) Token: 0x06004B79 RID: 19321
		IAdvancedTemplateView View { get; }

		// Token: 0x170018AB RID: 6315
		// (get) Token: 0x06004B7A RID: 19322
		// (set) Token: 0x06004B7B RID: 19323
		Panel TitleBarOuterPanel { get; set; }

		// Token: 0x170018AC RID: 6316
		// (get) Token: 0x06004B7C RID: 19324
		// (set) Token: 0x06004B7D RID: 19325
		Panel TitleBarInnerPanel { get; set; }

		// Token: 0x170018AD RID: 6317
		// (get) Token: 0x06004B7E RID: 19326
		// (set) Token: 0x06004B7F RID: 19327
		Panel OptionsPanel { get; set; }

		// Token: 0x170018AE RID: 6318
		// (get) Token: 0x06004B80 RID: 19328
		// (set) Token: 0x06004B81 RID: 19329
		Panel OptionsPanelScroll { get; set; }

		// Token: 0x170018AF RID: 6319
		// (get) Token: 0x06004B82 RID: 19330
		// (set) Token: 0x06004B83 RID: 19331
		Panel BasicControlsPanel { get; set; }

		// Token: 0x170018B0 RID: 6320
		// (get) Token: 0x06004B84 RID: 19332
		// (set) Token: 0x06004B85 RID: 19333
		Panel AdvancedControlsPanel { get; set; }

		// Token: 0x170018B1 RID: 6321
		// (get) Token: 0x06004B86 RID: 19334
		// (set) Token: 0x06004B87 RID: 19335
		Panel ButtonsPanel { get; set; }

		// Token: 0x06004B88 RID: 19336
		void CreateLayout(Control container);

		// Token: 0x06004B89 RID: 19337
		void CreateControls(Control container);

		// Token: 0x06004B8A RID: 19338
		void CreateTitle(string title);

		// Token: 0x06004B8B RID: 19339
		void CreateInsertButtons();

		// Token: 0x06004B8C RID: 19340
		void CreateEditButtons();
	}
}

using System;

namespace System.Web.UI
{
	// Token: 0x0200005C RID: 92
	internal interface IScriptManagerInternal
	{
		// Token: 0x170000CC RID: 204
		// (get) Token: 0x06000337 RID: 823
		string AsyncPostBackSourceElementID { get; }

		// Token: 0x170000CD RID: 205
		// (get) Token: 0x06000338 RID: 824
		bool SupportsPartialRendering { get; }

		// Token: 0x170000CE RID: 206
		// (get) Token: 0x06000339 RID: 825
		bool IsInAsyncPostBack { get; }

		// Token: 0x0600033A RID: 826
		void RegisterAsyncPostBackControl(Control control);

		// Token: 0x0600033B RID: 827
		void RegisterExtenderControl<TExtenderControl>(TExtenderControl extenderControl, Control targetControl) where TExtenderControl : Control, IExtenderControl;

		// Token: 0x0600033C RID: 828
		void RegisterPostBackControl(Control control);

		// Token: 0x0600033D RID: 829
		void RegisterProxy(ScriptManagerProxy proxy);

		// Token: 0x0600033E RID: 830
		void RegisterScriptControl<TScriptControl>(TScriptControl scriptControl) where TScriptControl : Control, IScriptControl;

		// Token: 0x0600033F RID: 831
		void RegisterScriptDescriptors(IExtenderControl extenderControl);

		// Token: 0x06000340 RID: 832
		void RegisterScriptDescriptors(IScriptControl scriptControl);

		// Token: 0x06000341 RID: 833
		void RegisterUpdatePanel(UpdatePanel updatePanel);

		// Token: 0x06000342 RID: 834
		void UnregisterUpdatePanel(UpdatePanel updatePanel);
	}
}

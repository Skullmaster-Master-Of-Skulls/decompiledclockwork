using System;
using System.Collections;
using System.Collections.Generic;
using System.Web.UI.HtmlControls;

namespace System.Web.UI
{
	// Token: 0x0200005A RID: 90
	internal interface IPage
	{
		// Token: 0x170000BF RID: 191
		// (get) Token: 0x06000316 RID: 790
		string AppRelativeVirtualPath { get; }

		// Token: 0x170000C0 RID: 192
		// (get) Token: 0x06000317 RID: 791
		IDictionary<string, string> HiddenFieldsToRender { get; }

		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000318 RID: 792
		IClientScriptManager ClientScript { get; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000319 RID: 793
		bool EnableEventValidation { get; }

		// Token: 0x170000C3 RID: 195
		// (get) Token: 0x0600031A RID: 794
		IHtmlForm Form { get; }

		// Token: 0x170000C4 RID: 196
		// (get) Token: 0x0600031B RID: 795
		HtmlHead Header { get; }

		// Token: 0x170000C5 RID: 197
		// (get) Token: 0x0600031C RID: 796
		bool IsPostBack { get; }

		// Token: 0x170000C6 RID: 198
		// (get) Token: 0x0600031D RID: 797
		bool IsValid { get; }

		// Token: 0x170000C7 RID: 199
		// (get) Token: 0x0600031E RID: 798
		IDictionary Items { get; }

		// Token: 0x170000C8 RID: 200
		// (get) Token: 0x0600031F RID: 799
		HttpRequestBase Request { get; }

		// Token: 0x170000C9 RID: 201
		// (get) Token: 0x06000320 RID: 800
		HttpResponseInternalBase Response { get; }

		// Token: 0x170000CA RID: 202
		// (get) Token: 0x06000321 RID: 801
		HttpServerUtilityBase Server { get; }

		// Token: 0x170000CB RID: 203
		// (get) Token: 0x06000322 RID: 802
		// (set) Token: 0x06000323 RID: 803
		string Title { get; set; }

		// Token: 0x14000001 RID: 1
		// (add) Token: 0x06000324 RID: 804
		// (remove) Token: 0x06000325 RID: 805
		event EventHandler Error;

		// Token: 0x14000002 RID: 2
		// (add) Token: 0x06000326 RID: 806
		// (remove) Token: 0x06000327 RID: 807
		event EventHandler InitComplete;

		// Token: 0x14000003 RID: 3
		// (add) Token: 0x06000328 RID: 808
		// (remove) Token: 0x06000329 RID: 809
		event EventHandler LoadComplete;

		// Token: 0x14000004 RID: 4
		// (add) Token: 0x0600032A RID: 810
		// (remove) Token: 0x0600032B RID: 811
		event EventHandler PreRender;

		// Token: 0x14000005 RID: 5
		// (add) Token: 0x0600032C RID: 812
		// (remove) Token: 0x0600032D RID: 813
		event EventHandler PreRenderComplete;

		// Token: 0x0600032E RID: 814
		void RegisterRequiresViewStateEncryption();

		// Token: 0x0600032F RID: 815
		void SetFocus(Control control);

		// Token: 0x06000330 RID: 816
		void SetFocus(string clientID);

		// Token: 0x06000331 RID: 817
		void SetPostFormRenderDelegate(RenderMethod renderMethod);

		// Token: 0x06000332 RID: 818
		void SetRenderMethodDelegate(RenderMethod renderMethod);

		// Token: 0x06000333 RID: 819
		void Validate(string validationGroup);

		// Token: 0x06000334 RID: 820
		void VerifyRenderingInServerForm(Control control);
	}
}

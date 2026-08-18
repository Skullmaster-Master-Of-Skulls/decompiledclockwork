using System;

namespace System.Web.UI
{
	// Token: 0x020002B4 RID: 692
	internal interface IScriptManager
	{
		// Token: 0x170008CD RID: 2253
		// (get) Token: 0x06001FB7 RID: 8119
		bool SupportsPartialRendering { get; }

		// Token: 0x170008CE RID: 2254
		// (get) Token: 0x06001FB8 RID: 8120
		bool IsInAsyncPostBack { get; }

		// Token: 0x170008CF RID: 2255
		// (get) Token: 0x06001FB9 RID: 8121
		bool EnableCdn { get; }

		// Token: 0x170008D0 RID: 2256
		// (get) Token: 0x06001FBA RID: 8122
		bool EnableCdnFallback { get; }

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06001FBB RID: 8123
		bool IsDebuggingEnabled { get; }

		// Token: 0x170008D2 RID: 2258
		// (get) Token: 0x06001FBC RID: 8124
		bool IsSecureConnection { get; }

		// Token: 0x06001FBD RID: 8125
		void RegisterArrayDeclaration(Control control, string arrayName, string arrayValue);

		// Token: 0x06001FBE RID: 8126
		void RegisterClientScriptBlock(Control control, Type type, string key, string script, bool addScriptTags);

		// Token: 0x06001FBF RID: 8127
		void RegisterClientScriptInclude(Control control, Type type, string key, string url);

		// Token: 0x06001FC0 RID: 8128
		void RegisterClientScriptResource(Control control, Type type, string resourceName);

		// Token: 0x06001FC1 RID: 8129
		void RegisterDispose(Control control, string disposeScript);

		// Token: 0x06001FC2 RID: 8130
		void RegisterExpandoAttribute(Control control, string controlId, string attributeName, string attributeValue, bool encode);

		// Token: 0x06001FC3 RID: 8131
		void RegisterHiddenField(Control control, string hiddenFieldName, string hiddenFieldValue);

		// Token: 0x06001FC4 RID: 8132
		void RegisterOnSubmitStatement(Control control, Type type, string key, string script);

		// Token: 0x06001FC5 RID: 8133
		void RegisterPostBackControl(Control control);

		// Token: 0x06001FC6 RID: 8134
		void RegisterStartupScript(Control control, Type type, string key, string script, bool addScriptTags);

		// Token: 0x06001FC7 RID: 8135
		void SetFocusInternal(string clientID);
	}
}

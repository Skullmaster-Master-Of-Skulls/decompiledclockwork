using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Web.UI
{
	// Token: 0x02000052 RID: 82
	internal interface IClientScriptManager
	{
		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x06000301 RID: 769
		Dictionary<Assembly, Dictionary<string, object>> RegisteredResourcesToSuppress { get; }

		// Token: 0x06000302 RID: 770
		string GetPostBackEventReference(PostBackOptions options);

		// Token: 0x06000303 RID: 771
		string GetWebResourceUrl(Type type, string resourceName);

		// Token: 0x06000304 RID: 772
		void RegisterClientScriptBlock(Type type, string key, string script);

		// Token: 0x06000305 RID: 773
		void RegisterClientScriptInclude(Type type, string key, string url);

		// Token: 0x06000306 RID: 774
		void RegisterClientScriptBlock(Type type, string key, string script, bool addScriptTags);

		// Token: 0x06000307 RID: 775
		void RegisterStartupScript(Type type, string key, string script, bool addScriptTags);
	}
}

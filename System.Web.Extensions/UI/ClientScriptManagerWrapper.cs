using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Web.UI
{
	// Token: 0x02000046 RID: 70
	internal sealed class ClientScriptManagerWrapper : IClientScriptManager
	{
		// Token: 0x060002BA RID: 698 RVA: 0x0001152F File Offset: 0x0000F72F
		internal ClientScriptManagerWrapper(ClientScriptManager clientScriptManager)
		{
			this._clientScriptManager = clientScriptManager;
		}

		// Token: 0x170000A1 RID: 161
		// (get) Token: 0x060002BB RID: 699 RVA: 0x0001153E File Offset: 0x0000F73E
		Dictionary<Assembly, Dictionary<string, object>> IClientScriptManager.RegisteredResourcesToSuppress
		{
			get
			{
				return this._clientScriptManager.RegisteredResourcesToSuppress;
			}
		}

		// Token: 0x060002BC RID: 700 RVA: 0x0001154B File Offset: 0x0000F74B
		string IClientScriptManager.GetPostBackEventReference(PostBackOptions options)
		{
			return this._clientScriptManager.GetPostBackEventReference(options);
		}

		// Token: 0x060002BD RID: 701 RVA: 0x00011559 File Offset: 0x0000F759
		string IClientScriptManager.GetWebResourceUrl(Type type, string resourceName)
		{
			return this._clientScriptManager.GetWebResourceUrl(type, resourceName);
		}

		// Token: 0x060002BE RID: 702 RVA: 0x00011568 File Offset: 0x0000F768
		void IClientScriptManager.RegisterClientScriptBlock(Type type, string key, string script)
		{
			this._clientScriptManager.RegisterClientScriptBlock(type, key, script);
		}

		// Token: 0x060002BF RID: 703 RVA: 0x00011578 File Offset: 0x0000F778
		void IClientScriptManager.RegisterClientScriptInclude(Type type, string key, string url)
		{
			this._clientScriptManager.RegisterClientScriptInclude(type, key, url);
		}

		// Token: 0x060002C0 RID: 704 RVA: 0x00011588 File Offset: 0x0000F788
		void IClientScriptManager.RegisterClientScriptBlock(Type type, string key, string script, bool addScriptTags)
		{
			this._clientScriptManager.RegisterClientScriptBlock(type, key, script, addScriptTags);
		}

		// Token: 0x060002C1 RID: 705 RVA: 0x0001159A File Offset: 0x0000F79A
		void IClientScriptManager.RegisterStartupScript(Type type, string key, string script, bool addScriptTags)
		{
			this._clientScriptManager.RegisterStartupScript(type, key, script, addScriptTags);
		}

		// Token: 0x04000109 RID: 265
		private readonly ClientScriptManager _clientScriptManager;
	}
}

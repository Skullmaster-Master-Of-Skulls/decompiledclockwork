using System;
using System.Reflection;

namespace System.Web.UI.WebControls
{
	// Token: 0x0200050E RID: 1294
	internal static class ValidatorCompatibilityHelper
	{
		// Token: 0x0600411A RID: 16666 RVA: 0x000D50DC File Offset: 0x000D32DC
		public static void RegisterArrayDeclaration(Control control, string arrayName, string arrayValue)
		{
			Type scriptManagerType = control.Page.ScriptManagerType;
			scriptManagerType.InvokeMember("RegisterArrayDeclaration", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, null, null, new object[]
			{
				control,
				arrayName,
				arrayValue
			});
		}

		// Token: 0x0600411B RID: 16667 RVA: 0x000D511C File Offset: 0x000D331C
		public static void RegisterClientScriptResource(Control control, string resourceName)
		{
			Type scriptManagerType = control.Page.ScriptManagerType;
			scriptManagerType.InvokeMember("RegisterNamedClientScriptResource", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, null, null, new object[]
			{
				control,
				resourceName
			});
		}

		// Token: 0x0600411C RID: 16668 RVA: 0x000D5158 File Offset: 0x000D3358
		public static void RegisterClientScriptResource(Control control, Type type, string resourceName)
		{
			Type scriptManagerType = control.Page.ScriptManagerType;
			scriptManagerType.InvokeMember("RegisterClientScriptResource", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, null, null, new object[]
			{
				control,
				type,
				resourceName
			});
		}

		// Token: 0x0600411D RID: 16669 RVA: 0x000D5198 File Offset: 0x000D3398
		public static void RegisterExpandoAttribute(Control control, string controlId, string attributeName, string attributeValue, bool encode)
		{
			Type scriptManagerType = control.Page.ScriptManagerType;
			scriptManagerType.InvokeMember("RegisterExpandoAttribute", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, null, null, new object[]
			{
				control,
				controlId,
				attributeName,
				attributeValue,
				encode
			});
		}

		// Token: 0x0600411E RID: 16670 RVA: 0x000D51E4 File Offset: 0x000D33E4
		public static void RegisterOnSubmitStatement(Control control, Type type, string key, string script)
		{
			Type scriptManagerType = control.Page.ScriptManagerType;
			scriptManagerType.InvokeMember("RegisterOnSubmitStatement", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, null, null, new object[]
			{
				control,
				type,
				key,
				script
			});
		}

		// Token: 0x0600411F RID: 16671 RVA: 0x000D5228 File Offset: 0x000D3428
		public static void RegisterStartupScript(Control control, Type type, string key, string script, bool addScriptTags)
		{
			Type scriptManagerType = control.Page.ScriptManagerType;
			scriptManagerType.InvokeMember("RegisterStartupScript", BindingFlags.Static | BindingFlags.Public | BindingFlags.InvokeMethod, null, null, new object[]
			{
				control,
				type,
				key,
				script,
				addScriptTags
			});
		}
	}
}

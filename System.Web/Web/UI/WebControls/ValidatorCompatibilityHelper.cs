using System;
using System.Reflection;

namespace System.Web.UI.WebControls
{
	// Token: 0x02000680 RID: 1664
	internal static class ValidatorCompatibilityHelper
	{
		// Token: 0x060051D4 RID: 20948 RVA: 0x0014AF70 File Offset: 0x00149F70
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

		// Token: 0x060051D5 RID: 20949 RVA: 0x0014AFB0 File Offset: 0x00149FB0
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

		// Token: 0x060051D6 RID: 20950 RVA: 0x0014AFF0 File Offset: 0x00149FF0
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

		// Token: 0x060051D7 RID: 20951 RVA: 0x0014B040 File Offset: 0x0014A040
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

		// Token: 0x060051D8 RID: 20952 RVA: 0x0014B084 File Offset: 0x0014A084
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

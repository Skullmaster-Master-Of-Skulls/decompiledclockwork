using System;
using System.Reflection;
using System.Security;
using System.Security.Permissions;

namespace System.Xml
{
	// Token: 0x020000D8 RID: 216
	internal static class BinaryCompatibility
	{
		// Token: 0x170001EB RID: 491
		// (get) Token: 0x06000AA4 RID: 2724 RVA: 0x000256DD File Offset: 0x000238DD
		internal static bool TargetsAtLeast_Desktop_V4_5_2
		{
			get
			{
				return BinaryCompatibility._targetsAtLeast_Desktop_V4_5_2;
			}
		}

		// Token: 0x06000AA5 RID: 2725 RVA: 0x000256E4 File Offset: 0x000238E4
		[SecuritySafeCritical]
		[ReflectionPermission(SecurityAction.Assert, Unrestricted = true)]
		private static bool RunningOnCheck(string propertyName)
		{
			Type type;
			try
			{
				type = typeof(object).GetTypeInfo().Assembly.GetType("System.Runtime.Versioning.BinaryCompatibility", false);
			}
			catch (TypeLoadException)
			{
				return false;
			}
			if (type == null)
			{
				return false;
			}
			PropertyInfo property = type.GetProperty(propertyName, BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			return !(property == null) && (bool)property.GetValue(null);
		}

		// Token: 0x0400036A RID: 874
		private static bool _targetsAtLeast_Desktop_V4_5_2 = BinaryCompatibility.RunningOnCheck("TargetsAtLeast_Desktop_V4_5_2");
	}
}

using System;
using System.Reflection;

namespace Google.Apis.Util
{
	// Token: 0x0200000E RID: 14
	public static class UriPatcher
	{
		// Token: 0x0600002F RID: 47 RVA: 0x00002324 File Offset: 0x00000524
		public static void PatchUriQuirks()
		{
			Type uriParser = typeof(Uri).GetTypeInfo().Assembly.GetType("System.UriParser");
			if (uriParser == null)
			{
				return;
			}
			if (new Uri("http://example.com/%2f").AbsolutePath == "//" || new Uri("https://example.com/%2f").AbsolutePath == "//")
			{
				MethodInfo setUpdatableFlagsMethod = uriParser.GetMethod("SetUpdatableFlags", BindingFlags.Instance | BindingFlags.NonPublic);
				if (setUpdatableFlagsMethod != null)
				{
					Action<string> action = delegate(string fieldName)
					{
						FieldInfo field2 = uriParser.GetField(fieldName, BindingFlags.Static | BindingFlags.NonPublic);
						if (field2 == null)
						{
							return;
						}
						object value = field2.GetValue(null);
						if (value == null)
						{
							return;
						}
						setUpdatableFlagsMethod.Invoke(value, new object[]
						{
							0
						});
					};
					action("HttpUri");
					action("HttpsUri");
				}
			}
			if (Uri.EscapeDataString("*") == "*")
			{
				FieldInfo field = uriParser.GetField("s_QuirksVersion", BindingFlags.Static | BindingFlags.NonPublic);
				if (field != null && (int)field.GetValue(null) <= 2)
				{
					field.SetValue(null, 3);
				}
			}
		}
	}
}

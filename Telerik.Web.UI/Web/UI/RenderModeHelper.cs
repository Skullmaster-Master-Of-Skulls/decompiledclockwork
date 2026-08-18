using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;

namespace Telerik.Web.UI
{
	// Token: 0x02000A1A RID: 2586
	internal static class RenderModeHelper
	{
		// Token: 0x060061EF RID: 25071 RVA: 0x00171D88 File Offset: 0x0016FF88
		static RenderModeHelper()
		{
			foreach (object obj in Enum.GetValues(typeof(RenderMode)))
			{
				RenderMode renderMode = (RenderMode)obj;
				RenderModeHelper._cache.Add(renderMode, RenderModeHelper.ExtractEnumValue(renderMode));
			}
		}

		// Token: 0x060061F0 RID: 25072 RVA: 0x00171E00 File Offset: 0x00170000
		internal static string ExtractEnumValue(RenderMode enumerationValue)
		{
			Type type = enumerationValue.GetType();
			if (!type.IsEnum)
			{
				throw new ArgumentException("EnumerationValue must be of RenderingMode type", "enumerationValue");
			}
			MemberInfo[] member = type.GetMember(enumerationValue.ToString());
			if (member != null && member.Length > 0)
			{
				object[] customAttributes = member[0].GetCustomAttributes(typeof(DefaultValueAttribute), false);
				if (customAttributes != null && customAttributes.Length > 0)
				{
					return (string)((DefaultValueAttribute)customAttributes[0]).Value;
				}
			}
			return enumerationValue.ToString();
		}

		// Token: 0x060061F1 RID: 25073 RVA: 0x00171E87 File Offset: 0x00170087
		public static string GetRenderingModeString(RenderMode enumerationValue)
		{
			return RenderModeHelper._cache[enumerationValue];
		}

		// Token: 0x040017FE RID: 6142
		public const string Auto = "Auto";

		// Token: 0x040017FF RID: 6143
		public const string Classic = "Classic";

		// Token: 0x04001800 RID: 6144
		public const string Lite = "Lite";

		// Token: 0x04001801 RID: 6145
		public const string Native = "Native";

		// Token: 0x04001802 RID: 6146
		public const string Mobile = "Mobile";

		// Token: 0x04001803 RID: 6147
		public const string RenderingModeSimpleObsoleteMessage = "RenderingMode.Simple is obsolete, please use RenderingMode.Native instead";

		// Token: 0x04001804 RID: 6148
		private static Dictionary<RenderMode, string> _cache = new Dictionary<RenderMode, string>(4);
	}
}

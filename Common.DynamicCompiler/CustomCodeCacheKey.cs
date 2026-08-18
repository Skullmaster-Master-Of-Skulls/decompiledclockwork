using System;
using System.Linq;

namespace TechnoPro.Common.DynamicCompiler
{
	// Token: 0x02000005 RID: 5
	public class CustomCodeCacheKey
	{
		// Token: 0x1700000E RID: 14
		// (get) Token: 0x06000027 RID: 39 RVA: 0x00002BBC File Offset: 0x00000DBC
		// (set) Token: 0x06000028 RID: 40 RVA: 0x00002BC4 File Offset: 0x00000DC4
		public string CompilerType { get; set; }

		// Token: 0x1700000F RID: 15
		// (get) Token: 0x06000029 RID: 41 RVA: 0x00002BCD File Offset: 0x00000DCD
		// (set) Token: 0x0600002A RID: 42 RVA: 0x00002BD5 File Offset: 0x00000DD5
		public CustomCSharpCode Code { get; set; }

		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002B RID: 43 RVA: 0x00002BDE File Offset: 0x00000DDE
		// (set) Token: 0x0600002C RID: 44 RVA: 0x00002BE6 File Offset: 0x00000DE6
		public string CompilerTypeSecondary { get; set; }

		// Token: 0x0600002D RID: 45 RVA: 0x00002BF0 File Offset: 0x00000DF0
		public static bool operator ==(CustomCodeCacheKey a, CustomCodeCacheKey b)
		{
			bool flag = a == null && b == null;
			bool result;
			if (flag)
			{
				result = true;
			}
			else
			{
				bool flag2 = a == null || b == null;
				if (flag2)
				{
					result = false;
				}
				else
				{
					bool flag3 = (a.CompilerType ?? "") != (b.CompilerType ?? "");
					if (flag3)
					{
						result = false;
					}
					else
					{
						bool flag4 = a.CompilerTypeSecondary != b.CompilerTypeSecondary;
						if (flag4)
						{
							result = false;
						}
						else
						{
							string a2 = (a.Code == null || a.Code.Code == null) ? "" : a.Code.Code;
							string b2 = (b.Code == null || b.Code.Code == null) ? "" : b.Code.Code;
							bool flag5 = a2 != b2;
							if (flag5)
							{
								result = false;
							}
							else
							{
								string a3 = (a.Code == null || a.Code.Imports == null) ? "" : string.Join(",", a.Code.Imports.ToArray<string>());
								string b3 = (b.Code == null || b.Code.Imports == null) ? "" : string.Join(",", b.Code.Imports.ToArray<string>());
								result = (a3 == b3);
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00002D70 File Offset: 0x00000F70
		public static bool operator !=(CustomCodeCacheKey a, CustomCodeCacheKey b)
		{
			return !(a == b);
		}
	}
}

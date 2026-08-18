using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Web.UI.PageLayout.Utils
{
	// Token: 0x02000648 RID: 1608
	public class CssUtils
	{
		// Token: 0x06003ABF RID: 15039 RVA: 0x000BF71C File Offset: 0x000BD91C
		public static string AddClassName(string collection, string className)
		{
			return collection;
		}

		// Token: 0x06003AC0 RID: 15040 RVA: 0x000BF71F File Offset: 0x000BD91F
		public static string[] AddClassName(string[] collection, string className, bool addUnique = true)
		{
			return collection;
		}

		// Token: 0x06003AC1 RID: 15041 RVA: 0x000BF722 File Offset: 0x000BD922
		public static List<string> AddClassName(List<string> collection, string className, bool addUnique = true)
		{
			if (!addUnique || collection.IndexOf(className) > -1)
			{
				collection.Add(className);
			}
			return collection;
		}

		// Token: 0x06003AC2 RID: 15042 RVA: 0x000BF739 File Offset: 0x000BD939
		public static string RemoveDuplicateClassNames(string input)
		{
			return StringUtils.RemoveDuplicates(input);
		}

		// Token: 0x06003AC3 RID: 15043 RVA: 0x000BF741 File Offset: 0x000BD941
		public static string[] RemoveDuplicateClassNames(string[] input)
		{
			return StringUtils.RemoveDuplicates(input);
		}

		// Token: 0x06003AC4 RID: 15044 RVA: 0x000BF749 File Offset: 0x000BD949
		public static List<string> RemoveDuplicateClassNames(List<string> input)
		{
			return StringUtils.RemoveDuplicates(input);
		}

		// Token: 0x06003AC5 RID: 15045 RVA: 0x000BF75C File Offset: 0x000BD95C
		public static string NormalizeClassNames(string[] classNames)
		{
			string[] value = (from className in classNames
			where !string.IsNullOrEmpty(className)
			select className).Distinct<string>().ToArray<string>();
			return string.Join(" ", value);
		}

		// Token: 0x06003AC6 RID: 15046 RVA: 0x000BF7B0 File Offset: 0x000BD9B0
		public static string NormalizeClassNames(List<string> classNames)
		{
			List<string> values = (from className in classNames
			where !string.IsNullOrEmpty(className)
			select className).Distinct<string>().ToList<string>();
			return string.Join(" ", values);
		}

		// Token: 0x06003AC7 RID: 15047 RVA: 0x000BF7F6 File Offset: 0x000BD9F6
		public static string NormalizeClassNames(string classNames)
		{
			return CssUtils.NormalizeClassNames(classNames.Split(null, StringSplitOptions.RemoveEmptyEntries));
		}
	}
}

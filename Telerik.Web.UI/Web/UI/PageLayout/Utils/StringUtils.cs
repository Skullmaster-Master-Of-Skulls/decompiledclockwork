using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;

namespace Telerik.Web.UI.PageLayout.Utils
{
	// Token: 0x02000649 RID: 1609
	internal class StringUtils
	{
		// Token: 0x06003ACB RID: 15051 RVA: 0x000BF80D File Offset: 0x000BDA0D
		public static string CollapseInnerSpaces(string input)
		{
			return StringUtils._multipleSpaces.Replace(input, " ");
		}

		// Token: 0x06003ACC RID: 15052 RVA: 0x000BF81F File Offset: 0x000BDA1F
		public static string TrimAndCollapseInnerSpaces(string input)
		{
			return StringUtils._multipleSpaces.Replace(input.Trim(), " ");
		}

		// Token: 0x06003ACD RID: 15053 RVA: 0x000BF836 File Offset: 0x000BDA36
		public static string RemoveDuplicates(string input)
		{
			return input.Split(null, StringSplitOptions.RemoveEmptyEntries).Distinct<string>().ToString();
		}

		// Token: 0x06003ACE RID: 15054 RVA: 0x000BF84A File Offset: 0x000BDA4A
		public static string[] RemoveDuplicates(string[] input)
		{
			return input.Distinct<string>().ToArray<string>();
		}

		// Token: 0x06003ACF RID: 15055 RVA: 0x000BF857 File Offset: 0x000BDA57
		public static List<string> RemoveDuplicates(List<string> input)
		{
			return input.Distinct<string>().ToList<string>();
		}

		// Token: 0x06003AD0 RID: 15056 RVA: 0x000BF864 File Offset: 0x000BDA64
		public static string RemoveEmptyMembers(string input)
		{
			return StringUtils._multipleSpaces.Replace(input, " ");
		}

		// Token: 0x06003AD1 RID: 15057 RVA: 0x000BF881 File Offset: 0x000BDA81
		public static string[] RemoveEmptyMembers(string[] input)
		{
			return (from className in input
			where !string.IsNullOrEmpty(className)
			select className).ToArray<string>();
		}

		// Token: 0x06003AD2 RID: 15058 RVA: 0x000BF8B6 File Offset: 0x000BDAB6
		public static List<string> RemoveEmptyMembers(List<string> input)
		{
			return (from className in input
			where !string.IsNullOrEmpty(className)
			select className).ToList<string>();
		}

		// Token: 0x04001000 RID: 4096
		private static readonly Regex _multipleSpaces = new Regex(" {2,}", RegexOptions.Compiled);
	}
}

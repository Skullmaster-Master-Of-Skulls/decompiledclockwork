using System;
using System.Collections.Generic;
using System.Linq;
using Antlr.Runtime.Tree;

namespace WebGrease.Css.Extensions
{
	// Token: 0x02000186 RID: 390
	public static class CommonTreeExtensions
	{
		// Token: 0x06001466 RID: 5222 RVA: 0x00077D48 File Offset: 0x00075F48
		public static IEnumerable<CommonTree> Children(this CommonTree commonTree, string childFilterText = null)
		{
			if (commonTree != null && commonTree.Children != null)
			{
				if (!string.IsNullOrWhiteSpace(childFilterText))
				{
					foreach (CommonTree child in from _ in commonTree.Children.OfType<CommonTree>()
					where _.Text == childFilterText
					select _)
					{
						yield return child;
					}
				}
				else
				{
					foreach (CommonTree child2 in commonTree.Children.OfType<CommonTree>())
					{
						yield return child2;
					}
				}
			}
			yield break;
		}

		// Token: 0x06001467 RID: 5223 RVA: 0x00077F5C File Offset: 0x0007615C
		public static IEnumerable<CommonTree> GrandChildren(this CommonTree commonTree, string childFilterText)
		{
			if (commonTree != null && commonTree.Children != null)
			{
				foreach (CommonTree granchChild in commonTree.Children(childFilterText).SelectMany((CommonTree _) => _.Children(null)))
				{
					yield return granchChild;
				}
			}
			yield break;
		}

		// Token: 0x06001468 RID: 5224 RVA: 0x00077F80 File Offset: 0x00076180
		public static string TextOrDefault(this CommonTree commonTree, string defaultText = null)
		{
			if (commonTree == null)
			{
				return defaultText;
			}
			return commonTree.ToString();
		}

		// Token: 0x06001469 RID: 5225 RVA: 0x00077F8D File Offset: 0x0007618D
		public static string FirstChildText(this CommonTree commonTree)
		{
			return commonTree.FirstChildTextOrDefault(null);
		}

		// Token: 0x0600146A RID: 5226 RVA: 0x00077F98 File Offset: 0x00076198
		public static string FirstChildTextOrDefault(this CommonTree commonTree, string defaultText = null)
		{
			if (commonTree != null)
			{
				CommonTree commonTree2 = commonTree.Children(null).FirstOrDefault<CommonTree>();
				if (commonTree2 != null)
				{
					return commonTree2.TextOrDefault(defaultText);
				}
			}
			return defaultText;
		}

		// Token: 0x0600146B RID: 5227 RVA: 0x00077FC1 File Offset: 0x000761C1
		public static string FirstChildText(this IEnumerable<CommonTree> commonTree)
		{
			return commonTree.FirstChildTextOrDefault(null);
		}

		// Token: 0x0600146C RID: 5228 RVA: 0x00077FCC File Offset: 0x000761CC
		public static string FirstChildTextOrDefault(this IEnumerable<CommonTree> commonTree, string defaultText = null)
		{
			if (commonTree != null)
			{
				CommonTree commonTree2 = commonTree.FirstOrDefault<CommonTree>();
				if (commonTree2 != null)
				{
					CommonTree commonTree3 = commonTree2.Children(null).FirstOrDefault<CommonTree>();
					if (commonTree3 != null)
					{
						return commonTree3.TextOrDefault(defaultText);
					}
				}
			}
			return defaultText;
		}
	}
}

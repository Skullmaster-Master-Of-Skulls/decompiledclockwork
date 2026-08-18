using System;
using System.Collections.Generic;
using System.Linq;

namespace Telerik.Web.Data.Expressions
{
	// Token: 0x02001BBE RID: 7102
	internal static class MemberAccessTokenizer
	{
		// Token: 0x0601128F RID: 70287 RVA: 0x003C8C38 File Offset: 0x003C6E38
		public static IEnumerable<IMemberAccessToken> GetTokens(string memberPath)
		{
			string[] members = memberPath.Split(new char[]
			{
				'.',
				'['
			}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string member in members)
			{
				IndexerToken indexerToken;
				if (MemberAccessTokenizer.TryParseIndexerToken(member, out indexerToken))
				{
					yield return indexerToken;
				}
				else
				{
					yield return new PropertyToken(member);
				}
			}
			yield break;
		}

		// Token: 0x06011290 RID: 70288 RVA: 0x003C8C60 File Offset: 0x003C6E60
		private static bool TryParseIndexerToken(string member, out IndexerToken token)
		{
			token = null;
			if (!MemberAccessTokenizer.IsValidIndexer(member))
			{
				return false;
			}
			List<object> list = new List<object>();
			list.AddRange(from a in MemberAccessTokenizer.ExtractIndexerArguments(member)
			select MemberAccessTokenizer.ConvertIndexerArgument(a));
			token = new IndexerToken(list);
			return true;
		}

		// Token: 0x06011291 RID: 70289 RVA: 0x003C8CB7 File Offset: 0x003C6EB7
		private static bool IsValidIndexer(string member)
		{
			return member.EndsWith("]", StringComparison.Ordinal);
		}

		// Token: 0x06011292 RID: 70290 RVA: 0x003C8E6C File Offset: 0x003C706C
		private static IEnumerable<string> ExtractIndexerArguments(string member)
		{
			string args = member.TrimEnd(new char[]
			{
				']'
			});
			foreach (string arg in args.Split(new char[]
			{
				','
			}))
			{
				yield return arg;
			}
			yield break;
		}

		// Token: 0x06011293 RID: 70291 RVA: 0x003C8E8C File Offset: 0x003C708C
		private static object ConvertIndexerArgument(string argument)
		{
			int num;
			if (int.TryParse(argument, out num))
			{
				return num;
			}
			if (argument.StartsWith("\"", StringComparison.Ordinal))
			{
				return argument.Trim(new char[]
				{
					'"'
				});
			}
			if (!argument.StartsWith("'", StringComparison.Ordinal))
			{
				return argument;
			}
			string text = argument.Trim(new char[]
			{
				'\''
			});
			if (text.Length == 1)
			{
				return text[0];
			}
			return text;
		}
	}
}

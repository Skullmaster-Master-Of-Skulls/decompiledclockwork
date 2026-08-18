using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;

namespace AutoMapper
{
	// Token: 0x02000048 RID: 72
	public class PrePostfixName : ISourceToDestinationNameMapper
	{
		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060002F9 RID: 761 RVA: 0x000077B7 File Offset: 0x000059B7
		public ICollection<string> Prefixes { get; } = new Collection<string>();

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060002FA RID: 762 RVA: 0x000077BF File Offset: 0x000059BF
		public ICollection<string> Postfixes { get; } = new Collection<string>();

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060002FB RID: 763 RVA: 0x000077C7 File Offset: 0x000059C7
		public ICollection<string> DestinationPrefixes { get; } = new Collection<string>();

		// Token: 0x170000BA RID: 186
		// (get) Token: 0x060002FC RID: 764 RVA: 0x000077CF File Offset: 0x000059CF
		public ICollection<string> DestinationPostfixes { get; } = new Collection<string>();

		// Token: 0x060002FD RID: 765 RVA: 0x000077D8 File Offset: 0x000059D8
		public PrePostfixName AddStrings(Func<PrePostfixName, ICollection<string>> getStringsFunc, params string[] names)
		{
			ICollection<string> collection = getStringsFunc(this);
			foreach (string item in names)
			{
				collection.Add(item);
			}
			return this;
		}

		// Token: 0x060002FE RID: 766 RVA: 0x0000780C File Offset: 0x00005A0C
		public MemberInfo GetMatchingMemberInfo(IGetTypeInfoMembers getTypeInfoMembers, TypeDetails typeInfo, Type destType, string nameToSearch)
		{
			IEnumerable<string> source = this.PossibleNames(nameToSearch, this.DestinationPrefixes, this.DestinationPostfixes);
			IEnumerable<<>f__AnonymousType4<MemberInfo, IEnumerable<string>>> possibleDestNames = from mi in getTypeInfoMembers.GetMemberInfos(typeInfo)
			select new
			{
				mi = mi,
				possibles = this.PossibleNames(mi.Name, this.Prefixes, this.Postfixes)
			};
			var <>f__AnonymousType = (from sourceName in source
			from destName in possibleDestNames
			select new
			{
				sourceName,
				destName
			}).FirstOrDefault(pair => pair.destName.possibles.Any((string p) => string.Compare(p, pair.sourceName, StringComparison.OrdinalIgnoreCase) == 0));
			if (<>f__AnonymousType == null)
			{
				return null;
			}
			return <>f__AnonymousType.destName.mi;
		}

		// Token: 0x060002FF RID: 767 RVA: 0x000078B5 File Offset: 0x00005AB5
		private IEnumerable<string> PossibleNames(string memberName, IEnumerable<string> prefixes, IEnumerable<string> postfixes)
		{
			if (string.IsNullOrEmpty(memberName))
			{
				yield break;
			}
			yield return memberName;
			Func<string, bool> <>9__0;
			Func<string, bool> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = ((string prefix) => memberName.StartsWith(prefix, StringComparison.Ordinal)));
			}
			IEnumerable<string> source = prefixes.Where(predicate);
			Func<string, string> <>9__1;
			Func<string, string> selector;
			if ((selector = <>9__1) == null)
			{
				selector = (<>9__1 = ((string prefix) => memberName.Substring(prefix.Length)));
			}
			foreach (string withoutPrefix in source.Select(selector))
			{
				yield return withoutPrefix;
				foreach (string text in this.PostFixes(postfixes, withoutPrefix))
				{
					yield return text;
				}
				IEnumerator<string> enumerator2 = null;
				withoutPrefix = null;
			}
			IEnumerator<string> enumerator = null;
			foreach (string text2 in this.PostFixes(postfixes, memberName))
			{
				yield return text2;
			}
			enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x06000300 RID: 768 RVA: 0x000078DC File Offset: 0x00005ADC
		private IEnumerable<string> PostFixes(IEnumerable<string> postfixes, string name)
		{
			return from postfix in postfixes
			where name.EndsWith(postfix, StringComparison.Ordinal)
			select name.Remove(name.Length - postfix.Length);
		}
	}
}

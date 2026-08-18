using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Reflection;
using AutoMapper.Internal;

namespace AutoMapper
{
	// Token: 0x02000049 RID: 73
	public class ReplaceName : ISourceToDestinationNameMapper
	{
		// Token: 0x170000BB RID: 187
		// (get) Token: 0x06000303 RID: 771 RVA: 0x0000796D File Offset: 0x00005B6D
		private ICollection<MemberNameReplacer> MemberNameReplacers { get; }

		// Token: 0x06000304 RID: 772 RVA: 0x00007975 File Offset: 0x00005B75
		public ReplaceName()
		{
			this.MemberNameReplacers = new Collection<MemberNameReplacer>();
		}

		// Token: 0x06000305 RID: 773 RVA: 0x00007988 File Offset: 0x00005B88
		public ReplaceName AddReplace(string original, string newValue)
		{
			this.MemberNameReplacers.Add(new MemberNameReplacer(original, newValue));
			return this;
		}

		// Token: 0x06000306 RID: 774 RVA: 0x000079A0 File Offset: 0x00005BA0
		public MemberInfo GetMatchingMemberInfo(IGetTypeInfoMembers getTypeInfoMembers, TypeDetails typeInfo, Type destType, string nameToSearch)
		{
			IEnumerable<string> source = this.PossibleNames(nameToSearch);
			IEnumerable<<>f__AnonymousType4<MemberInfo, IEnumerable<string>>> possibleDestNames = from mi in getTypeInfoMembers.GetMemberInfos(typeInfo)
			select new
			{
				mi = mi,
				possibles = this.PossibleNames(mi.Name)
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

		// Token: 0x06000307 RID: 775 RVA: 0x00007A40 File Offset: 0x00005C40
		private IEnumerable<string> PossibleNames(string nameToSearch)
		{
			IEnumerable<string> first = from r in this.MemberNameReplacers
			select nameToSearch.Replace(r.OriginalValue, r.NewValue);
			string[] array = new string[2];
			array[0] = this.MemberNameReplacers.Aggregate(nameToSearch, (string s, MemberNameReplacer r) => s.Replace(r.OriginalValue, r.NewValue));
			array[1] = nameToSearch;
			return first.Concat(array).ToList<string>();
		}
	}
}

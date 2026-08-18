using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text.RegularExpressions;
using AutoMapper.Internal;

namespace AutoMapper
{
	// Token: 0x02000050 RID: 80
	public class NameSplitMember : IChildMemberConfiguration
	{
		// Token: 0x170000C1 RID: 193
		// (get) Token: 0x06000320 RID: 800 RVA: 0x00007CD4 File Offset: 0x00005ED4
		// (set) Token: 0x06000321 RID: 801 RVA: 0x00007CDC File Offset: 0x00005EDC
		public INamingConvention SourceMemberNamingConvention { get; set; }

		// Token: 0x170000C2 RID: 194
		// (get) Token: 0x06000322 RID: 802 RVA: 0x00007CE5 File Offset: 0x00005EE5
		// (set) Token: 0x06000323 RID: 803 RVA: 0x00007CED File Offset: 0x00005EED
		public INamingConvention DestinationMemberNamingConvention { get; set; }

		// Token: 0x06000324 RID: 804 RVA: 0x00007CF6 File Offset: 0x00005EF6
		public NameSplitMember()
		{
			this.SourceMemberNamingConvention = new PascalCaseNamingConvention();
			this.DestinationMemberNamingConvention = new PascalCaseNamingConvention();
		}

		// Token: 0x06000325 RID: 805 RVA: 0x00007D14 File Offset: 0x00005F14
		public bool MapDestinationPropertyToSource(IProfileConfiguration options, TypeDetails sourceType, Type destType, string nameToSearch, LinkedList<IValueResolver> resolvers, IMemberConfiguration parent)
		{
			string[] array = (from Match m in this.DestinationMemberNamingConvention.SplittingExpression.Matches(nameToSearch)
			select this.SourceMemberNamingConvention.ReplaceValue(m)).ToArray<string>();
			MemberInfo memberInfo = null;
			for (int i = 1; i <= array.Length; i++)
			{
				NameSplitMember.NameSnippet nameSnippet = this.CreateNameSnippet(array, i);
				memberInfo = parent.NameMapper.GetMatchingMemberInfo(sourceType, destType, nameSnippet.First);
				if (memberInfo != null)
				{
					resolvers.AddLast(memberInfo.ToMemberGetter());
					if (parent.MapDestinationPropertyToSource(options, TypeMapFactory.GetTypeInfo(memberInfo.GetMemberType(), options), destType, nameSnippet.Second, resolvers))
					{
						break;
					}
					resolvers.RemoveLast();
				}
			}
			return memberInfo != null;
		}

		// Token: 0x06000326 RID: 806 RVA: 0x00007DC4 File Offset: 0x00005FC4
		private NameSplitMember.NameSnippet CreateNameSnippet(IEnumerable<string> matches, int i)
		{
			string first = string.Join(this.SourceMemberNamingConvention.SeparatorCharacter, (from s in matches.Take(i)
			select this.SourceMemberNamingConvention.SplittingExpression.Replace(s, new MatchEvaluator(this.SourceMemberNamingConvention.ReplaceValue))).ToArray<string>());
			string second = string.Join(this.SourceMemberNamingConvention.SeparatorCharacter, (from s in matches.Skip(i)
			select this.SourceMemberNamingConvention.SplittingExpression.Replace(s, new MatchEvaluator(this.SourceMemberNamingConvention.ReplaceValue))).ToArray<string>());
			return new NameSplitMember.NameSnippet
			{
				First = first,
				Second = second
			};
		}

		// Token: 0x0200011F RID: 287
		private class NameSnippet
		{
			// Token: 0x17000101 RID: 257
			// (get) Token: 0x060006F7 RID: 1783 RVA: 0x0001705B File Offset: 0x0001525B
			// (set) Token: 0x060006F8 RID: 1784 RVA: 0x00017063 File Offset: 0x00015263
			public string First { get; set; }

			// Token: 0x17000102 RID: 258
			// (get) Token: 0x060006F9 RID: 1785 RVA: 0x0001706C File Offset: 0x0001526C
			// (set) Token: 0x060006FA RID: 1786 RVA: 0x00017074 File Offset: 0x00015274
			public string Second { get; set; }
		}
	}
}

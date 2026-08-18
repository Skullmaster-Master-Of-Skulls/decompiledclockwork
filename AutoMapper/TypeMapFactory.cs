using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using AutoMapper.Internal;

namespace AutoMapper
{
	// Token: 0x0200003E RID: 62
	public class TypeMapFactory : ITypeMapFactory
	{
		// Token: 0x060002CF RID: 719 RVA: 0x0000709C File Offset: 0x0000529C
		public static TypeDetails GetTypeInfo(Type type, IProfileConfiguration profileConfiguration)
		{
			return TypeMapFactory._typeInfos.GetOrAdd(type, (Type t) => new TypeDetails(type, profileConfiguration.ShouldMapProperty, profileConfiguration.ShouldMapField, profileConfiguration.SourceExtensionMethods));
		}

		// Token: 0x060002D0 RID: 720 RVA: 0x000070DC File Offset: 0x000052DC
		public TypeMap CreateTypeMap(Type sourceType, Type destinationType, IProfileConfiguration options, MemberList memberList)
		{
			TypeDetails typeInfo = TypeMapFactory.GetTypeInfo(sourceType, options);
			TypeDetails typeInfo2 = TypeMapFactory.GetTypeInfo(destinationType, options);
			TypeMap typeMap = new TypeMap(typeInfo, typeInfo2, memberList, options.ProfileName);
			foreach (MemberInfo memberInfo in typeInfo2.PublicWriteAccessors)
			{
				LinkedList<IValueResolver> linkedList = new LinkedList<IValueResolver>();
				if (this.MapDestinationPropertyToSource(options, typeInfo, memberInfo.GetMemberType(), memberInfo.Name, linkedList))
				{
					IMemberAccessor destProperty = memberInfo.ToMemberAccessor();
					typeMap.AddPropertyMap(destProperty, linkedList);
				}
			}
			if (!destinationType.IsAbstract() && destinationType.IsClass())
			{
				foreach (ConstructorInfo destCtor in from ci in typeInfo2.Constructors
				orderby ci.GetParameters().Length descending
				select ci)
				{
					if (this.MapDestinationCtorToSource(typeMap, destCtor, typeInfo, options))
					{
						break;
					}
				}
			}
			return typeMap;
		}

		// Token: 0x060002D1 RID: 721 RVA: 0x000071F8 File Offset: 0x000053F8
		private bool MapDestinationPropertyToSource(IProfileConfiguration options, TypeDetails sourceTypeInfo, Type destType, string destMemberInfo, LinkedList<IValueResolver> members)
		{
			return options.MemberConfigurations.Any((IMemberConfiguration _) => _.MapDestinationPropertyToSource(options, sourceTypeInfo, destType, destMemberInfo, members));
		}

		// Token: 0x060002D2 RID: 722 RVA: 0x0000724C File Offset: 0x0000544C
		private bool MapDestinationCtorToSource(TypeMap typeMap, ConstructorInfo destCtor, TypeDetails sourceTypeInfo, IProfileConfiguration options)
		{
			List<ConstructorParameterMap> list = new List<ConstructorParameterMap>();
			ParameterInfo[] parameters = destCtor.GetParameters();
			if (parameters.Length == 0 || !options.ConstructorMappingEnabled)
			{
				return false;
			}
			foreach (ParameterInfo parameterInfo in parameters)
			{
				LinkedList<IValueResolver> linkedList = new LinkedList<IValueResolver>();
				bool flag = this.MapDestinationPropertyToSource(options, sourceTypeInfo, parameterInfo.GetType(), parameterInfo.Name, linkedList);
				if (!flag && parameterInfo.HasDefaultValue)
				{
					flag = true;
				}
				ConstructorParameterMap item = new ConstructorParameterMap(parameterInfo, linkedList.ToArray<IValueResolver>(), flag);
				list.Add(item);
			}
			typeMap.AddConstructorMap(destCtor, list);
			return true;
		}

		// Token: 0x060002D3 RID: 723 RVA: 0x000072DF File Offset: 0x000054DF
		public TypeDetails GetTypeInfo(Type type, IMappingOptions mappingOptions)
		{
			return this.GetTypeInfo(type, mappingOptions.ShouldMapProperty, mappingOptions.ShouldMapField, mappingOptions.SourceExtensionMethods);
		}

		// Token: 0x060002D4 RID: 724 RVA: 0x000072FC File Offset: 0x000054FC
		private TypeDetails GetTypeInfo(Type type, Func<PropertyInfo, bool> shouldMapProperty, Func<FieldInfo, bool> shouldMapField, IEnumerable<MethodInfo> extensionMethodsToSearch)
		{
			return TypeMapFactory._typeInfos.GetOrAdd(type, (Type t) => new TypeDetails(type, shouldMapProperty, shouldMapField, extensionMethodsToSearch));
		}

		// Token: 0x060002D5 RID: 725 RVA: 0x00007348 File Offset: 0x00005548
		private static MemberInfo FindTypeMember(IEnumerable<MemberInfo> modelProperties, IEnumerable<MethodInfo> getMethods, IEnumerable<MethodInfo> getExtensionMethods, string nameToSearch, IMappingOptions mappingOptions)
		{
			MemberInfo memberInfo = modelProperties.FirstOrDefault((MemberInfo prop) => TypeMapFactory.NameMatches(prop.Name, nameToSearch, mappingOptions));
			if (memberInfo != null)
			{
				return memberInfo;
			}
			MethodInfo methodInfo = getMethods.FirstOrDefault((MethodInfo m) => TypeMapFactory.NameMatches(m.Name, nameToSearch, mappingOptions));
			if (methodInfo != null)
			{
				return methodInfo;
			}
			methodInfo = getExtensionMethods.FirstOrDefault((MethodInfo m) => TypeMapFactory.NameMatches(m.Name, nameToSearch, mappingOptions));
			if (methodInfo != null)
			{
				return methodInfo;
			}
			return null;
		}

		// Token: 0x060002D6 RID: 726 RVA: 0x000073C8 File Offset: 0x000055C8
		private static bool NameMatches(string memberName, string nameToMatch, IMappingOptions mappingOptions)
		{
			IEnumerable<string> source = TypeMapFactory.PossibleNames(memberName, mappingOptions.Aliases, mappingOptions.MemberNameReplacers, mappingOptions.Prefixes, mappingOptions.Postfixes);
			IEnumerable<string> possibleDestNames = TypeMapFactory.PossibleNames(nameToMatch, mappingOptions.Aliases, mappingOptions.MemberNameReplacers, mappingOptions.DestinationPrefixes, mappingOptions.DestinationPostfixes);
			return (from sourceName in source
			from destName in possibleDestNames
			select new
			{
				sourceName,
				destName
			}).Any(pair => string.Compare(pair.sourceName, pair.destName, StringComparison.OrdinalIgnoreCase) == 0);
		}

		// Token: 0x060002D7 RID: 727 RVA: 0x00007471 File Offset: 0x00005671
		private static IEnumerable<string> PossibleNames(string memberName, IEnumerable<AliasedMember> aliases, IEnumerable<MemberNameReplacer> memberNameReplacers, IEnumerable<string> prefixes, IEnumerable<string> postfixes)
		{
			if (string.IsNullOrEmpty(memberName))
			{
				yield break;
			}
			yield return memberName;
			Func<AliasedMember, bool> <>9__0;
			Func<AliasedMember, bool> predicate;
			if ((predicate = <>9__0) == null)
			{
				predicate = (<>9__0 = ((AliasedMember alias) => string.Equals(memberName, alias.Member, StringComparison.Ordinal)));
			}
			foreach (AliasedMember aliasedMember in aliases.Where(predicate))
			{
				yield return aliasedMember.Alias;
			}
			IEnumerator<AliasedMember> enumerator = null;
			yield break;
			yield break;
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00007488 File Offset: 0x00005688
		private TypeMapFactory.NameSnippet CreateNameSnippet(IEnumerable<string> matches, int i, IMappingOptions mappingOptions)
		{
			return new TypeMapFactory.NameSnippet
			{
				First = string.Join("", matches.Take(i).ToArray<string>()),
				Second = string.Join("", matches.Skip(i).ToArray<string>())
			};
		}

		// Token: 0x0400008E RID: 142
		private static readonly ConcurrentDictionary<Type, TypeDetails> _typeInfos = new ConcurrentDictionary<Type, TypeDetails>();

		// Token: 0x02000104 RID: 260
		private class NameSnippet
		{
			// Token: 0x170000FB RID: 251
			// (get) Token: 0x0600069D RID: 1693 RVA: 0x00016658 File Offset: 0x00014858
			// (set) Token: 0x0600069E RID: 1694 RVA: 0x00016660 File Offset: 0x00014860
			public string First { get; set; }

			// Token: 0x170000FC RID: 252
			// (get) Token: 0x0600069F RID: 1695 RVA: 0x00016669 File Offset: 0x00014869
			// (set) Token: 0x060006A0 RID: 1696 RVA: 0x00016671 File Offset: 0x00014871
			public string Second { get; set; }
		}
	}
}

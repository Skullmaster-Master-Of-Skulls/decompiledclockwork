using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace AutoMapper
{
	// Token: 0x0200004A RID: 74
	public class SourceToDestinationNameMapperAttributesMember : ISourceToDestinationNameMapper
	{
		// Token: 0x06000309 RID: 777 RVA: 0x00007AD4 File Offset: 0x00005CD4
		public MemberInfo GetMatchingMemberInfo(IGetTypeInfoMembers getTypeInfoMembers, TypeDetails typeInfo, Type destType, string nameToSearch)
		{
			SourceToDestinationNameMapperAttributesMember.Cache.GetOrAdd(typeInfo, (TypeDetails ti) => getTypeInfoMembers.GetMemberInfos(ti).ToDictionary((MemberInfo mi) => mi, (MemberInfo mi) => mi.GetCustomAttributes(typeof(SourceToDestinationMapperAttribute), true).OfType<SourceToDestinationMapperAttribute>()));
			return SourceToDestinationNameMapperAttributesMember.Cache[typeInfo].FirstOrDefault((KeyValuePair<MemberInfo, IEnumerable<SourceToDestinationMapperAttribute>> kp) => kp.Value.Any((SourceToDestinationMapperAttribute _) => _.IsMatch(typeInfo, kp.Key, destType, nameToSearch))).Key;
		}

		// Token: 0x0400009B RID: 155
		private static readonly ConcurrentDictionary<TypeDetails, Dictionary<MemberInfo, IEnumerable<SourceToDestinationMapperAttribute>>> Cache = new ConcurrentDictionary<TypeDetails, Dictionary<MemberInfo, IEnumerable<SourceToDestinationMapperAttribute>>>();
	}
}

using System;
using System.Linq;
using System.Reflection;

namespace AutoMapper.QueryableExtensions.Impl
{
	// Token: 0x02000069 RID: 105
	internal static class QueryMapperHelper
	{
		// Token: 0x0600039A RID: 922 RVA: 0x00008FAC File Offset: 0x000071AC
		public static PropertyMap GetPropertyMap(this IConfigurationProvider config, MemberInfo sourceMemberInfo, Type destinationMemberType)
		{
			PropertyMap propertyMap = config.CheckIfMapExists(sourceMemberInfo.DeclaringType, destinationMemberType).GetPropertyMaps().FirstOrDefault((PropertyMap pm) => pm.CanResolveValue() && pm.SourceMember != null && pm.SourceMember.Name == sourceMemberInfo.Name);
			if (propertyMap == null)
			{
				throw new InvalidOperationException(string.Format("Missing property map from {0} to {1} for {2} property. Create using Mapper.CreateMap<{3}, {4}>.", new object[]
				{
					sourceMemberInfo.DeclaringType.Name,
					destinationMemberType.Name,
					sourceMemberInfo.Name,
					sourceMemberInfo.DeclaringType.Name,
					destinationMemberType.Name
				}));
			}
			return propertyMap;
		}

		// Token: 0x0600039B RID: 923 RVA: 0x0000904F File Offset: 0x0000724F
		public static TypeMap CheckIfMapExists(this IConfigurationProvider config, Type sourceType, Type destinationType)
		{
			TypeMap typeMap = config.FindTypeMapFor(sourceType, destinationType);
			if (typeMap == null)
			{
				throw QueryMapperHelper.MissingMapException(sourceType, destinationType);
			}
			return typeMap;
		}

		// Token: 0x0600039C RID: 924 RVA: 0x00009064 File Offset: 0x00007264
		public static Exception MissingMapException(Type sourceType, Type destinationType)
		{
			string name = sourceType.Name;
			string name2 = destinationType.Name;
			throw new InvalidOperationException(string.Format("Missing map from {0} to {1}. Create using Mapper.CreateMap<{2}, {3}>.", new object[]
			{
				name,
				name2,
				name,
				name2
			}));
		}
	}
}

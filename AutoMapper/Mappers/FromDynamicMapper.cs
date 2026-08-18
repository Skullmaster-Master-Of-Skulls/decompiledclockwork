using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper.Internal;

namespace AutoMapper.Mappers
{
	// Token: 0x02000078 RID: 120
	public class FromDynamicMapper : DynamicMapper
	{
		// Token: 0x060003EE RID: 1006 RVA: 0x00010782 File Offset: 0x0000E982
		public override bool IsMatch(TypePair context)
		{
			return context.SourceType.IsDynamic() && !context.DestinationType.IsDynamic();
		}

		// Token: 0x060003EF RID: 1007 RVA: 0x000107A1 File Offset: 0x0000E9A1
		protected override IEnumerable<MemberInfo> MembersToMap(object source, object destination)
		{
			return new TypeDetails(destination.GetType()).PublicWriteAccessors;
		}

		// Token: 0x060003F0 RID: 1008 RVA: 0x000107B3 File Offset: 0x0000E9B3
		protected override object GetSourceMember(MemberInfo member, object target)
		{
			return base.GetDynamically(member, target);
		}

		// Token: 0x060003F1 RID: 1009 RVA: 0x000107BD File Offset: 0x0000E9BD
		protected override void SetDestinationMember(MemberInfo member, object target, object value)
		{
			member.SetMemberValue(target, value);
		}
	}
}

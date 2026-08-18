using System;
using System.Collections.Generic;
using System.Reflection;
using AutoMapper.Internal;

namespace AutoMapper.Mappers
{
	// Token: 0x02000079 RID: 121
	public class ToDynamicMapper : DynamicMapper
	{
		// Token: 0x060003F3 RID: 1011 RVA: 0x000107CF File Offset: 0x0000E9CF
		public override bool IsMatch(TypePair context)
		{
			return context.DestinationType.IsDynamic() && !context.SourceType.IsDynamic();
		}

		// Token: 0x060003F4 RID: 1012 RVA: 0x000107EE File Offset: 0x0000E9EE
		protected override IEnumerable<MemberInfo> MembersToMap(object source, object destination)
		{
			return new TypeDetails(source.GetType()).PublicReadAccessors;
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00010800 File Offset: 0x0000EA00
		protected override object GetSourceMember(MemberInfo member, object target)
		{
			return member.GetMemberValue(target);
		}

		// Token: 0x060003F6 RID: 1014 RVA: 0x00010809 File Offset: 0x0000EA09
		protected override void SetDestinationMember(MemberInfo member, object target, object value)
		{
			base.SetDynamically(member, target, value);
		}
	}
}

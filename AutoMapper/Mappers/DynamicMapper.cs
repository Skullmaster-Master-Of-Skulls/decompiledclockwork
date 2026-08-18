using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using AutoMapper.Internal;
using Microsoft.CSharp.RuntimeBinder;

namespace AutoMapper.Mappers
{
	// Token: 0x02000077 RID: 119
	public abstract class DynamicMapper : IObjectMapper
	{
		// Token: 0x060003E6 RID: 998
		public abstract bool IsMatch(TypePair context);

		// Token: 0x060003E7 RID: 999 RVA: 0x00010660 File Offset: 0x0000E860
		public object Map(ResolutionContext context)
		{
			object sourceValue = context.SourceValue;
			object obj = context.Engine.CreateObject(context);
			foreach (MemberInfo member in this.MembersToMap(sourceValue, obj))
			{
				object sourceMember;
				try
				{
					sourceMember = this.GetSourceMember(member, sourceValue);
				}
				catch (RuntimeBinderException)
				{
					continue;
				}
				object value = ReflectionHelper.Map(context, member, sourceMember);
				this.SetDestinationMember(member, obj, value);
			}
			return obj;
		}

		// Token: 0x060003E8 RID: 1000
		protected abstract IEnumerable<MemberInfo> MembersToMap(object source, object destination);

		// Token: 0x060003E9 RID: 1001
		protected abstract object GetSourceMember(MemberInfo member, object target);

		// Token: 0x060003EA RID: 1002
		protected abstract void SetDestinationMember(MemberInfo member, object target, object value);

		// Token: 0x060003EB RID: 1003 RVA: 0x000106F0 File Offset: 0x0000E8F0
		protected object GetDynamically(MemberInfo member, object target)
		{
			CallSite<Func<CallSite, object, object>> callSite = CallSite<Func<CallSite, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.GetMember(CSharpBinderFlags.None, member.Name, member.GetMemberType(), new CSharpArgumentInfo[]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
			}));
			return callSite.Target(callSite, target);
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00010734 File Offset: 0x0000E934
		protected void SetDynamically(MemberInfo member, object target, object value)
		{
			CallSite<Func<CallSite, object, object, object>> callSite = CallSite<Func<CallSite, object, object, object>>.Create(Microsoft.CSharp.RuntimeBinder.Binder.SetMember(CSharpBinderFlags.None, member.Name, member.GetMemberType(), new CSharpArgumentInfo[]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null),
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
			}));
			callSite.Target(callSite, target, value);
		}
	}
}

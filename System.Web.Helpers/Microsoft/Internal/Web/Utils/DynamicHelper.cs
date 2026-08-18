using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using Microsoft.CSharp.RuntimeBinder;

namespace Microsoft.Internal.Web.Utils
{
	// Token: 0x0200000C RID: 12
	internal static class DynamicHelper
	{
		// Token: 0x0600006F RID: 111 RVA: 0x00003798 File Offset: 0x00001998
		public static bool TryGetMemberValue(object obj, string memberName, out object result)
		{
			try
			{
				result = DynamicHelper.GetMemberValue(obj, memberName);
				return true;
			}
			catch (RuntimeBinderException)
			{
			}
			catch (RuntimeBinderInternalCompilerException)
			{
			}
			result = null;
			return false;
		}

		// Token: 0x06000070 RID: 112 RVA: 0x000037DC File Offset: 0x000019DC
		public static bool TryGetMemberValue(object obj, GetMemberBinder binder, out object result)
		{
			bool result2;
			try
			{
				if (typeof(Binder).Assembly.Equals(binder.GetType().Assembly))
				{
					result = DynamicHelper.GetMemberValue(obj, binder);
				}
				else
				{
					result = DynamicHelper.GetMemberValue(obj, binder.Name);
				}
				result2 = true;
			}
			catch
			{
				result = null;
				result2 = false;
			}
			return result2;
		}

		// Token: 0x06000071 RID: 113 RVA: 0x00003840 File Offset: 0x00001A40
		public static object GetMemberValue(object obj, string memberName)
		{
			CallSite<Func<CallSite, object, object>> memberAccessCallSite = DynamicHelper.GetMemberAccessCallSite(memberName);
			return memberAccessCallSite.Target(memberAccessCallSite, obj);
		}

		// Token: 0x06000072 RID: 114 RVA: 0x00003864 File Offset: 0x00001A64
		public static object GetMemberValue(object obj, GetMemberBinder binder)
		{
			CallSite<Func<CallSite, object, object>> memberAccessCallSite = DynamicHelper.GetMemberAccessCallSite(binder);
			return memberAccessCallSite.Target(memberAccessCallSite, obj);
		}

		// Token: 0x06000073 RID: 115 RVA: 0x00003888 File Offset: 0x00001A88
		public static CallSite<Func<CallSite, object, object>> GetMemberAccessCallSite(string memberName)
		{
			CallSiteBinder member = Binder.GetMember(CSharpBinderFlags.None, memberName, typeof(DynamicHelper), new CSharpArgumentInfo[]
			{
				CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, null)
			});
			return DynamicHelper.GetMemberAccessCallSite(member);
		}

		// Token: 0x06000074 RID: 116 RVA: 0x000038BF File Offset: 0x00001ABF
		public static CallSite<Func<CallSite, object, object>> GetMemberAccessCallSite(CallSiteBinder binder)
		{
			return CallSite<Func<CallSite, object, object>>.Create(binder);
		}

		// Token: 0x06000075 RID: 117 RVA: 0x000038C8 File Offset: 0x00001AC8
		public static IEnumerable<string> GetMemberNames(object obj)
		{
			IDynamicMetaObjectProvider dynamicMetaObjectProvider = obj as IDynamicMetaObjectProvider;
			Expression parameter = Expression.Parameter(typeof(object));
			return dynamicMetaObjectProvider.GetMetaObject(parameter).GetDynamicMemberNames();
		}
	}
}

using System;
using System.Reflection;
using System.Security.Permissions;
using System.Threading;

namespace System.Web.Util
{
	// Token: 0x020001D6 RID: 470
	[SecurityPermission(SecurityAction.LinkDemand, Unrestricted = true)]
	internal static class ExecutionContextUtil
	{
		// Token: 0x06001788 RID: 6024 RVA: 0x00049B4C File Offset: 0x00047D4C
		[ReflectionPermission(SecurityAction.Assert, MemberAccess = true)]
		private static ExecutionContext GetDummyDefaultEC()
		{
			PropertyInfo property = typeof(ExecutionContext).GetProperty("PreAllocatedDefault", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
			if (property == null)
			{
				throw new Exception(SR.GetString("Type_doesnt_have_property", new object[]
				{
					typeof(ExecutionContext).FullName,
					"PreAllocatedDefault"
				}));
			}
			return (ExecutionContext)property.GetValue(null, null);
		}

		// Token: 0x06001789 RID: 6025 RVA: 0x00049BB6 File Offset: 0x00047DB6
		internal static void RunInNullExecutionContext(Action callback)
		{
			ExecutionContext.Run(ExecutionContextUtil.s_dummyDefaultEC, ExecutionContextUtil.s_actionToActionObjShunt, callback);
		}

		// Token: 0x04001718 RID: 5912
		private static readonly ContextCallback s_actionToActionObjShunt = delegate(object obj)
		{
			((Action)obj)();
		};

		// Token: 0x04001719 RID: 5913
		private static readonly ExecutionContext s_dummyDefaultEC = ExecutionContextUtil.GetDummyDefaultEC();
	}
}

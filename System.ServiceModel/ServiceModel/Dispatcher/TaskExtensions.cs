using System;
using System.Linq;
using System.Reflection;
using System.Runtime;

namespace System.ServiceModel.Dispatcher
{
	// Token: 0x020005AA RID: 1450
	internal static class TaskExtensions
	{
		// Token: 0x17000D67 RID: 3431
		// (get) Token: 0x0600389A RID: 14490 RVA: 0x000DA3E8 File Offset: 0x000D85E8
		public static MethodInfo TaskAsAsyncResultMethodInfo
		{
			get
			{
				if (TaskExtensions.taskAsAsyncResultMethodInfo == null)
				{
					TaskExtensions.taskAsAsyncResultMethodInfo = (from m in typeof(TaskExtensions).GetMethods()
					where m.IsGenericMethod && m.Name == "AsAsyncResult"
					select m).First<MethodInfo>();
				}
				return TaskExtensions.taskAsAsyncResultMethodInfo;
			}
		}

		// Token: 0x0600389B RID: 14491 RVA: 0x000DA444 File Offset: 0x000D8644
		public static MethodInfo MakeGenericMethod(Type genericArgument)
		{
			return TaskExtensions.TaskAsAsyncResultMethodInfo.MakeGenericMethod(new Type[]
			{
				genericArgument
			});
		}

		// Token: 0x040029A3 RID: 10659
		private const string TaskAsAsyncResultMethodName = "AsAsyncResult";

		// Token: 0x040029A4 RID: 10660
		private static MethodInfo taskAsAsyncResultMethodInfo;
	}
}

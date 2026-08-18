using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.Web.Mvc
{
	// Token: 0x02000056 RID: 86
	internal static class ActionDescriptorHelper
	{
		// Token: 0x06000231 RID: 561 RVA: 0x00007C54 File Offset: 0x00005E54
		public static ICollection<ActionSelector> GetSelectors(MethodInfo methodInfo)
		{
			ActionMethodSelectorAttribute[] array = (ActionMethodSelectorAttribute[])methodInfo.GetCustomAttributes(typeof(ActionMethodSelectorAttribute), true);
			return Array.ConvertAll<ActionMethodSelectorAttribute, ActionSelector>(array, (ActionMethodSelectorAttribute attr) => (ControllerContext controllerContext) => attr.IsValidForRequest(controllerContext, methodInfo));
		}

		// Token: 0x06000232 RID: 562 RVA: 0x00007CF8 File Offset: 0x00005EF8
		public static ICollection<ActionNameSelector> GetNameSelectors(MethodInfo methodInfo)
		{
			ActionNameSelectorAttribute[] array = (ActionNameSelectorAttribute[])methodInfo.GetCustomAttributes(typeof(ActionNameSelectorAttribute), true);
			return Array.ConvertAll<ActionNameSelectorAttribute, ActionNameSelector>(array, (ActionNameSelectorAttribute attr) => (ControllerContext controllerContext, string actionName) => attr.IsValidName(controllerContext, actionName, methodInfo));
		}

		// Token: 0x06000233 RID: 563 RVA: 0x00007D42 File Offset: 0x00005F42
		public static bool IsDefined(MemberInfo methodInfo, Type attributeType, bool inherit)
		{
			return methodInfo.IsDefined(attributeType, inherit);
		}

		// Token: 0x06000234 RID: 564 RVA: 0x00007D4C File Offset: 0x00005F4C
		public static object[] GetCustomAttributes(MemberInfo methodInfo, bool inherit)
		{
			return methodInfo.GetCustomAttributes(inherit);
		}

		// Token: 0x06000235 RID: 565 RVA: 0x00007D55 File Offset: 0x00005F55
		public static object[] GetCustomAttributes(MemberInfo methodInfo, Type attributeType, bool inherit)
		{
			return methodInfo.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06000236 RID: 566 RVA: 0x00007D60 File Offset: 0x00005F60
		public static ParameterDescriptor[] GetParameters(ActionDescriptor actionDescriptor, MethodInfo methodInfo, ref ParameterDescriptor[] parametersCache)
		{
			ParameterDescriptor[] array = ActionDescriptorHelper.LazilyFetchParametersCollection(actionDescriptor, methodInfo, ref parametersCache);
			return (ParameterDescriptor[])array.Clone();
		}

		// Token: 0x06000237 RID: 567 RVA: 0x00007DA0 File Offset: 0x00005FA0
		private static ParameterDescriptor[] LazilyFetchParametersCollection(ActionDescriptor actionDescriptor, MethodInfo methodInfo, ref ParameterDescriptor[] parametersCache)
		{
			return DescriptorUtil.LazilyFetchOrCreateDescriptors<ParameterInfo, ParameterDescriptor, ActionDescriptorHelper.CreateDescriptorState>(ref parametersCache, (ActionDescriptorHelper.CreateDescriptorState state) => state.MethodInfo.GetParameters(), (ParameterInfo parameterInfo, ActionDescriptorHelper.CreateDescriptorState state) => new ReflectedParameterDescriptor(parameterInfo, state.ActionDescriptor), new ActionDescriptorHelper.CreateDescriptorState
			{
				ActionDescriptor = actionDescriptor,
				MethodInfo = methodInfo
			});
		}

		// Token: 0x02000057 RID: 87
		private struct CreateDescriptorState
		{
			// Token: 0x0400006C RID: 108
			internal ActionDescriptor ActionDescriptor;

			// Token: 0x0400006D RID: 109
			internal MethodInfo MethodInfo;
		}
	}
}

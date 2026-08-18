using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace System.Web.Mvc.Async
{
	// Token: 0x020000E8 RID: 232
	internal sealed class AsyncActionMethodSelector : ActionMethodSelectorBase
	{
		// Token: 0x060005EF RID: 1519 RVA: 0x00010178 File Offset: 0x0000E378
		public AsyncActionMethodSelector(Type controllerType, bool allowLegacyAsyncActions = true)
		{
			this._allowLegacyAsyncActions = allowLegacyAsyncActions;
			base.Initialize(controllerType);
		}

		// Token: 0x060005F0 RID: 1520 RVA: 0x00010190 File Offset: 0x0000E390
		public ActionDescriptorCreator FindAction(ControllerContext controllerContext, string actionName)
		{
			MethodInfo methodInfo = base.FindActionMethod(controllerContext, actionName);
			if (methodInfo == null)
			{
				return null;
			}
			return this.GetActionDescriptorDelegate(methodInfo);
		}

		// Token: 0x170001CB RID: 459
		// (get) Token: 0x060005F1 RID: 1521 RVA: 0x000101B8 File Offset: 0x0000E3B8
		internal bool AllowLegacyAsyncActions
		{
			get
			{
				return this._allowLegacyAsyncActions;
			}
		}

		// Token: 0x060005F2 RID: 1522 RVA: 0x00010208 File Offset: 0x0000E408
		internal ActionDescriptorCreator GetActionDescriptorDelegate(MethodInfo entryMethod)
		{
			AsyncActionMethodSelector.<>c__DisplayClass4 CS$<>8__locals1 = new AsyncActionMethodSelector.<>c__DisplayClass4();
			CS$<>8__locals1.entryMethod = entryMethod;
			if (CS$<>8__locals1.entryMethod.ReturnType != null && typeof(Task).IsAssignableFrom(CS$<>8__locals1.entryMethod.ReturnType))
			{
				return (string actionName, ControllerDescriptor controllerDescriptor) => new TaskAsyncActionDescriptor(CS$<>8__locals1.entryMethod, actionName, controllerDescriptor);
			}
			if (!this.IsAsyncSuffixedMethod(CS$<>8__locals1.entryMethod))
			{
				return (string actionName, ControllerDescriptor controllerDescriptor) => new ReflectedActionDescriptor(CS$<>8__locals1.entryMethod, actionName, controllerDescriptor);
			}
			string methodName = CS$<>8__locals1.entryMethod.Name.Substring(0, CS$<>8__locals1.entryMethod.Name.Length - "Async".Length) + "Completed";
			MethodInfo completionMethod = this.GetMethodByName(methodName);
			if (completionMethod != null)
			{
				return (string actionName, ControllerDescriptor controllerDescriptor) => new ReflectedAsyncActionDescriptor(CS$<>8__locals1.entryMethod, completionMethod, actionName, controllerDescriptor);
			}
			throw Error.AsyncActionMethodSelector_CouldNotFindMethod(methodName, base.ControllerType);
		}

		// Token: 0x060005F3 RID: 1523 RVA: 0x0001030C File Offset: 0x0000E50C
		protected override bool IsValidActionMethod(MethodInfo methodInfo)
		{
			return this.IsValidActionMethod(methodInfo, true);
		}

		// Token: 0x060005F4 RID: 1524 RVA: 0x00010316 File Offset: 0x0000E516
		private bool IsValidActionMethod(MethodInfo methodInfo, bool stripInfrastructureMethods)
		{
			return !methodInfo.IsSpecialName && !methodInfo.GetBaseDefinition().DeclaringType.IsAssignableFrom(typeof(AsyncController)) && (!stripInfrastructureMethods || !this.IsCompletedSuffixedMethod(methodInfo));
		}

		// Token: 0x060005F5 RID: 1525 RVA: 0x00010350 File Offset: 0x0000E550
		protected override string GetCanonicalMethodName(MethodInfo methodInfo)
		{
			string name = methodInfo.Name;
			if (!this.IsAsyncSuffixedMethod(methodInfo))
			{
				return name;
			}
			return name.Substring(0, name.Length - "Async".Length);
		}

		// Token: 0x060005F6 RID: 1526 RVA: 0x00010387 File Offset: 0x0000E587
		private bool IsAsyncSuffixedMethod(MethodInfo methodInfo)
		{
			return this._allowLegacyAsyncActions && methodInfo.Name.EndsWith("Async", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060005F7 RID: 1527 RVA: 0x000103A4 File Offset: 0x0000E5A4
		private bool IsCompletedSuffixedMethod(MethodInfo methodInfo)
		{
			return this._allowLegacyAsyncActions && methodInfo.Name.EndsWith("Completed", StringComparison.OrdinalIgnoreCase);
		}

		// Token: 0x060005F8 RID: 1528 RVA: 0x000103CC File Offset: 0x0000E5CC
		private MethodInfo GetMethodByName(string methodName)
		{
			List<MethodInfo> list = (from MethodInfo methodInfo in base.ControllerType.GetMember(methodName, MemberTypes.Method, BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod)
			where this.IsValidActionMethod(methodInfo, false)
			select methodInfo).ToList<MethodInfo>();
			switch (list.Count)
			{
			case 0:
				return null;
			case 1:
				return list[0];
			default:
				throw base.CreateAmbiguousMethodMatchException(list, methodName);
			}
		}

		// Token: 0x040001A6 RID: 422
		private readonly bool _allowLegacyAsyncActions;
	}
}

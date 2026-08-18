using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x0200002B RID: 43
	internal abstract class ActionMethodSelectorBase
	{
		// Token: 0x060000D1 RID: 209 RVA: 0x00004C94 File Offset: 0x00002E94
		protected void Initialize(Type controllerType)
		{
			this.ControllerType = controllerType;
			MethodInfo[] methods = this.ControllerType.GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod);
			this.ActionMethods = Array.FindAll<MethodInfo>(methods, new Predicate<MethodInfo>(this.IsValidActionMethod));
			this.StandardRouteMethods = new HashSet<MethodInfo>(this.ActionMethods);
		}

		// Token: 0x17000035 RID: 53
		// (get) Token: 0x060000D2 RID: 210 RVA: 0x00004CE3 File Offset: 0x00002EE3
		// (set) Token: 0x060000D3 RID: 211 RVA: 0x00004CEB File Offset: 0x00002EEB
		public Type ControllerType { get; private set; }

		// Token: 0x17000036 RID: 54
		// (get) Token: 0x060000D4 RID: 212 RVA: 0x00004CF4 File Offset: 0x00002EF4
		// (set) Token: 0x060000D5 RID: 213 RVA: 0x00004CFC File Offset: 0x00002EFC
		public MethodInfo[] ActionMethods { get; private set; }

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x060000D6 RID: 214 RVA: 0x00004D05 File Offset: 0x00002F05
		// (set) Token: 0x060000D7 RID: 215 RVA: 0x00004D0D File Offset: 0x00002F0D
		public HashSet<MethodInfo> StandardRouteMethods { get; private set; }

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x060000D8 RID: 216 RVA: 0x00004D16 File Offset: 0x00002F16
		public MethodInfo[] AliasedMethods
		{
			get
			{
				return this.StandardRouteCache.AliasedMethods;
			}
		}

		// Token: 0x17000039 RID: 57
		// (get) Token: 0x060000D9 RID: 217 RVA: 0x00004D23 File Offset: 0x00002F23
		public ILookup<string, MethodInfo> NonAliasedMethods
		{
			get
			{
				return this.StandardRouteCache.NonAliasedMethods;
			}
		}

		// Token: 0x1700003A RID: 58
		// (get) Token: 0x060000DA RID: 218 RVA: 0x00004D30 File Offset: 0x00002F30
		private ActionMethodSelectorBase.StandardRouteActionMethodCache StandardRouteCache
		{
			get
			{
				if (this._standardRouteCache == null)
				{
					this._standardRouteCache = this.CreateStandardRouteCache();
				}
				return this._standardRouteCache;
			}
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00004D4C File Offset: 0x00002F4C
		protected AmbiguousMatchException CreateAmbiguousActionMatchException(IEnumerable<MethodInfo> ambiguousMethods, string actionName)
		{
			string text = ActionMethodSelectorBase.CreateAmbiguousMatchList(ambiguousMethods);
			string message = string.Format(CultureInfo.CurrentCulture, MvcResources.ActionMethodSelector_AmbiguousMatch, new object[]
			{
				actionName,
				this.ControllerType.Name,
				text
			});
			return new AmbiguousMatchException(message);
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00004D94 File Offset: 0x00002F94
		protected AmbiguousMatchException CreateAmbiguousMethodMatchException(IEnumerable<MethodInfo> ambiguousMethods, string methodName)
		{
			string text = ActionMethodSelectorBase.CreateAmbiguousMatchList(ambiguousMethods);
			string message = string.Format(CultureInfo.CurrentCulture, MvcResources.AsyncActionMethodSelector_AmbiguousMethodMatch, new object[]
			{
				methodName,
				this.ControllerType.Name,
				text
			});
			return new AmbiguousMatchException(message);
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00004DDC File Offset: 0x00002FDC
		protected static string CreateAmbiguousMatchList(IEnumerable<MethodInfo> ambiguousMethods)
		{
			StringBuilder stringBuilder = new StringBuilder();
			foreach (MethodInfo methodInfo in ambiguousMethods)
			{
				string text = Convert.ToString(methodInfo, CultureInfo.CurrentCulture);
				string fullName = methodInfo.DeclaringType.FullName;
				stringBuilder.AppendLine();
				stringBuilder.AppendFormat(CultureInfo.CurrentCulture, MvcResources.ActionMethodSelector_AmbiguousMatchType, new object[]
				{
					text,
					fullName
				});
			}
			return stringBuilder.ToString();
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00004E74 File Offset: 0x00003074
		private static bool IsMethodDecoratedWithAliasingAttribute(MethodInfo methodInfo)
		{
			return methodInfo.IsDefined(typeof(ActionNameSelectorAttribute), true);
		}

		// Token: 0x060000DF RID: 223
		protected abstract bool IsValidActionMethod(MethodInfo methodInfo);

		// Token: 0x060000E0 RID: 224 RVA: 0x00004E88 File Offset: 0x00003088
		protected virtual string GetCanonicalMethodName(MethodInfo methodInfo)
		{
			return methodInfo.Name;
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x00004EA0 File Offset: 0x000030A0
		private ActionMethodSelectorBase.StandardRouteActionMethodCache CreateStandardRouteCache()
		{
			ActionMethodSelectorBase.StandardRouteActionMethodCache standardRouteActionMethodCache = new ActionMethodSelectorBase.StandardRouteActionMethodCache();
			standardRouteActionMethodCache.AliasedMethods = this.StandardRouteMethods.Where(new Func<MethodInfo, bool>(ActionMethodSelectorBase.IsMethodDecoratedWithAliasingAttribute)).ToArray<MethodInfo>();
			standardRouteActionMethodCache.NonAliasedMethods = this.StandardRouteMethods.Except(standardRouteActionMethodCache.AliasedMethods).ToLookup(new Func<MethodInfo, string>(this.GetCanonicalMethodName), StringComparer.OrdinalIgnoreCase);
			return standardRouteActionMethodCache;
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00004F04 File Offset: 0x00003104
		protected List<MethodInfo> FindActionMethods(ControllerContext controllerContext, string actionName)
		{
			List<MethodInfo> list = new List<MethodInfo>();
			ActionMethodSelectorBase.StandardRouteActionMethodCache standardRouteCache = this.StandardRouteCache;
			for (int i = 0; i < standardRouteCache.AliasedMethods.Length; i++)
			{
				MethodInfo methodInfo = standardRouteCache.AliasedMethods[i];
				if (ActionMethodSelectorBase.IsMatchingAliasedMethod(methodInfo, controllerContext, actionName))
				{
					list.Add(methodInfo);
				}
			}
			list.AddRange(standardRouteCache.NonAliasedMethods[actionName]);
			ActionMethodSelectorBase.RunSelectionFilters(controllerContext, list);
			return list;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x00004F68 File Offset: 0x00003168
		protected static bool IsMatchingAliasedMethod(MethodInfo method, ControllerContext controllerContext, string actionName)
		{
			ReadOnlyCollection<ActionNameSelectorAttribute> actionNameSelectorAttributes = ReflectedAttributeCache.GetActionNameSelectorAttributes(method);
			int count = actionNameSelectorAttributes.Count;
			for (int i = 0; i < count; i++)
			{
				if (!actionNameSelectorAttributes[i].IsValidName(controllerContext, actionName, method))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x00004FA4 File Offset: 0x000031A4
		protected static bool IsValidMethodSelector(ReadOnlyCollection<ActionMethodSelectorAttribute> attributes, ControllerContext controllerContext, MethodInfo method)
		{
			int count = attributes.Count;
			for (int i = 0; i < count; i++)
			{
				if (!attributes[i].IsValidForRequest(controllerContext, method))
				{
					return false;
				}
			}
			return true;
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00004FD8 File Offset: 0x000031D8
		protected static void RunSelectionFilters(ControllerContext controllerContext, List<MethodInfo> methodInfos)
		{
			bool flag = false;
			for (int i = methodInfos.Count - 1; i >= 0; i--)
			{
				MethodInfo methodInfo = methodInfos[i];
				ReadOnlyCollection<ActionMethodSelectorAttribute> actionMethodSelectorAttributesCollection = ReflectedAttributeCache.GetActionMethodSelectorAttributesCollection(methodInfo);
				if (actionMethodSelectorAttributesCollection.Count == 0)
				{
					if (flag)
					{
						methodInfos.RemoveAt(i);
					}
				}
				else if (ActionMethodSelectorBase.IsValidMethodSelector(actionMethodSelectorAttributesCollection, controllerContext, methodInfo))
				{
					if (!flag)
					{
						if (i + 1 < methodInfos.Count)
						{
							methodInfos.RemoveFrom(i + 1);
						}
						flag = true;
					}
				}
				else
				{
					methodInfos.RemoveAt(i);
				}
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x0000504C File Offset: 0x0000324C
		public string GetActionName(MethodInfo methodInfo)
		{
			object[] customAttributes = methodInfo.GetCustomAttributes(typeof(ActionNameAttribute), true);
			if (customAttributes.Length > 0)
			{
				ActionNameAttribute actionNameAttribute = customAttributes[0] as ActionNameAttribute;
				if (actionNameAttribute != null)
				{
					return actionNameAttribute.Name;
				}
			}
			return this.GetCanonicalMethodName(methodInfo);
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x0000508C File Offset: 0x0000328C
		public MethodInfo FindActionMethod(ControllerContext controllerContext, string actionName)
		{
			if (controllerContext == null)
			{
				throw Error.ArgumentNull("controllerContext");
			}
			if (actionName == null)
			{
				throw Error.ArgumentNull("actionName");
			}
			List<MethodInfo> list = this.FindActionMethods(controllerContext, actionName);
			switch (list.Count)
			{
			case 0:
				return null;
			case 1:
				return list[0];
			default:
				throw this.CreateAmbiguousActionMatchException(list, actionName);
			}
		}

		// Token: 0x04000036 RID: 54
		private ActionMethodSelectorBase.StandardRouteActionMethodCache _standardRouteCache;

		// Token: 0x0200002C RID: 44
		private class StandardRouteActionMethodCache
		{
			// Token: 0x1700003B RID: 59
			// (get) Token: 0x060000E9 RID: 233 RVA: 0x000050EF File Offset: 0x000032EF
			// (set) Token: 0x060000EA RID: 234 RVA: 0x000050F7 File Offset: 0x000032F7
			public MethodInfo[] AliasedMethods { get; set; }

			// Token: 0x1700003C RID: 60
			// (get) Token: 0x060000EB RID: 235 RVA: 0x00005100 File Offset: 0x00003300
			// (set) Token: 0x060000EC RID: 236 RVA: 0x00005108 File Offset: 0x00003308
			public ILookup<string, MethodInfo> NonAliasedMethods { get; set; }
		}
	}
}

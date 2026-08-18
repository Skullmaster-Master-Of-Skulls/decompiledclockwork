using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Reflection;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x0200005A RID: 90
	public abstract class ActionDescriptor : ICustomAttributeProvider, IUniquelyIdentifiable
	{
		// Token: 0x170000FA RID: 250
		// (get) Token: 0x06000240 RID: 576
		public abstract string ActionName { get; }

		// Token: 0x170000FB RID: 251
		// (get) Token: 0x06000241 RID: 577
		public abstract ControllerDescriptor ControllerDescriptor { get; }

		// Token: 0x170000FC RID: 252
		// (get) Token: 0x06000242 RID: 578 RVA: 0x00007E0E File Offset: 0x0000600E
		// (set) Token: 0x06000243 RID: 579 RVA: 0x00007E29 File Offset: 0x00006029
		internal ActionMethodDispatcherCache DispatcherCache
		{
			get
			{
				if (this._instanceDispatcherCache == null)
				{
					this._instanceDispatcherCache = ActionDescriptor._staticDispatcherCache;
				}
				return this._instanceDispatcherCache;
			}
			set
			{
				this._instanceDispatcherCache = value;
			}
		}

		// Token: 0x170000FD RID: 253
		// (get) Token: 0x06000244 RID: 580 RVA: 0x00007E32 File Offset: 0x00006032
		public virtual string UniqueId
		{
			get
			{
				if (this._uniqueId == null)
				{
					this._uniqueId = this.CreateUniqueId();
				}
				return this._uniqueId;
			}
		}

		// Token: 0x06000245 RID: 581 RVA: 0x00007E4E File Offset: 0x0000604E
		private string CreateUniqueId()
		{
			return DescriptorUtil.CreateUniqueId(base.GetType(), this.ControllerDescriptor, this.ActionName);
		}

		// Token: 0x06000246 RID: 582
		public abstract object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters);

		// Token: 0x06000247 RID: 583 RVA: 0x00007E68 File Offset: 0x00006068
		internal static object ExtractParameterFromDictionary(ParameterInfo parameterInfo, IDictionary<string, object> parameters, MethodInfo methodInfo)
		{
			object obj;
			if (!parameters.TryGetValue(parameterInfo.Name, out obj))
			{
				string message = string.Format(CultureInfo.CurrentCulture, MvcResources.ReflectedActionDescriptor_ParameterNotInDictionary, new object[]
				{
					parameterInfo.Name,
					parameterInfo.ParameterType,
					methodInfo,
					methodInfo.DeclaringType
				});
				throw new ArgumentException(message, "parameters");
			}
			if (obj == null && !TypeHelpers.TypeAllowsNullValue(parameterInfo.ParameterType))
			{
				string message2 = string.Format(CultureInfo.CurrentCulture, MvcResources.ReflectedActionDescriptor_ParameterCannotBeNull, new object[]
				{
					parameterInfo.Name,
					parameterInfo.ParameterType,
					methodInfo,
					methodInfo.DeclaringType
				});
				throw new ArgumentException(message2, "parameters");
			}
			if (obj != null && !parameterInfo.ParameterType.IsInstanceOfType(obj))
			{
				string message3 = string.Format(CultureInfo.CurrentCulture, MvcResources.ReflectedActionDescriptor_ParameterValueHasWrongType, new object[]
				{
					parameterInfo.Name,
					methodInfo,
					methodInfo.DeclaringType,
					obj.GetType(),
					parameterInfo.ParameterType
				});
				throw new ArgumentException(message3, "parameters");
			}
			return obj;
		}

		// Token: 0x06000248 RID: 584 RVA: 0x00007F8C File Offset: 0x0000618C
		internal static object ExtractParameterOrDefaultFromDictionary(ParameterInfo parameterInfo, IDictionary<string, object> parameters)
		{
			Type parameterType = parameterInfo.ParameterType;
			object obj;
			parameters.TryGetValue(parameterInfo.Name, out obj);
			if (parameterType.IsInstanceOfType(obj))
			{
				return obj;
			}
			object result;
			if (ParameterInfoUtil.TryGetDefaultValue(parameterInfo, out result))
			{
				return result;
			}
			return TypeHelpers.GetDefaultValue(parameterType);
		}

		// Token: 0x06000249 RID: 585 RVA: 0x00007FCC File Offset: 0x000061CC
		public virtual object[] GetCustomAttributes(bool inherit)
		{
			return this.GetCustomAttributes(typeof(object), inherit);
		}

		// Token: 0x0600024A RID: 586 RVA: 0x00007FDF File Offset: 0x000061DF
		public virtual object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return (object[])Array.CreateInstance(attributeType, 0);
		}

		// Token: 0x0600024B RID: 587 RVA: 0x00008001 File Offset: 0x00006201
		public virtual IEnumerable<FilterAttribute> GetFilterAttributes(bool useCache)
		{
			return this.GetCustomAttributes(typeof(FilterAttribute), true).Cast<FilterAttribute>();
		}

		// Token: 0x0600024C RID: 588 RVA: 0x00008019 File Offset: 0x00006219
		[Obsolete("Please call System.Web.Mvc.FilterProviders.Providers.GetFilters() now.", true)]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public virtual FilterInfo GetFilters()
		{
			return new FilterInfo();
		}

		// Token: 0x0600024D RID: 589
		public abstract ParameterDescriptor[] GetParameters();

		// Token: 0x0600024E RID: 590 RVA: 0x00008020 File Offset: 0x00006220
		public virtual ICollection<ActionSelector> GetSelectors()
		{
			return ActionDescriptor._emptySelectors;
		}

		// Token: 0x0600024F RID: 591 RVA: 0x00008027 File Offset: 0x00006227
		internal virtual ICollection<ActionNameSelector> GetNameSelectors()
		{
			return ActionDescriptor._emptyNameSelectors;
		}

		// Token: 0x06000250 RID: 592 RVA: 0x0000802E File Offset: 0x0000622E
		public virtual bool IsDefined(Type attributeType, bool inherit)
		{
			if (attributeType == null)
			{
				throw new ArgumentNullException("attributeType");
			}
			return false;
		}

		// Token: 0x06000251 RID: 593 RVA: 0x00008048 File Offset: 0x00006248
		internal static string VerifyActionMethodIsCallable(MethodInfo methodInfo)
		{
			if (methodInfo.IsStatic)
			{
				return string.Format(CultureInfo.CurrentCulture, MvcResources.ReflectedActionDescriptor_CannotCallStaticMethod, new object[]
				{
					methodInfo,
					methodInfo.ReflectedType.FullName
				});
			}
			if (!typeof(ControllerBase).IsAssignableFrom(methodInfo.ReflectedType))
			{
				return string.Format(CultureInfo.CurrentCulture, MvcResources.ReflectedActionDescriptor_CannotCallInstanceMethodOnNonControllerType, new object[]
				{
					methodInfo,
					methodInfo.ReflectedType.FullName
				});
			}
			if (methodInfo.ContainsGenericParameters)
			{
				return string.Format(CultureInfo.CurrentCulture, MvcResources.ReflectedActionDescriptor_CannotCallOpenGenericMethods, new object[]
				{
					methodInfo,
					methodInfo.ReflectedType.FullName
				});
			}
			ParameterInfo[] parameters = methodInfo.GetParameters();
			foreach (ParameterInfo parameterInfo in parameters)
			{
				if (parameterInfo.IsOut || parameterInfo.ParameterType.IsByRef)
				{
					return string.Format(CultureInfo.CurrentCulture, MvcResources.ReflectedActionDescriptor_CannotCallMethodsWithOutOrRefParameters, new object[]
					{
						methodInfo,
						methodInfo.ReflectedType.FullName,
						parameterInfo
					});
				}
			}
			return null;
		}

		// Token: 0x0400006E RID: 110
		private static readonly ActionMethodDispatcherCache _staticDispatcherCache = new ActionMethodDispatcherCache();

		// Token: 0x0400006F RID: 111
		private static readonly ActionSelector[] _emptySelectors = new ActionSelector[0];

		// Token: 0x04000070 RID: 112
		private static readonly ActionNameSelector[] _emptyNameSelectors = new ActionNameSelector[0];

		// Token: 0x04000071 RID: 113
		private string _uniqueId;

		// Token: 0x04000072 RID: 114
		private ActionMethodDispatcherCache _instanceDispatcherCache;
	}
}

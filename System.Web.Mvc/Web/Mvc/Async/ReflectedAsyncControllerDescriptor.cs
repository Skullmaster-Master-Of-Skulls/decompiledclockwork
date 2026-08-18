using System;
using System.Collections.Generic;

namespace System.Web.Mvc.Async
{
	// Token: 0x02000121 RID: 289
	public class ReflectedAsyncControllerDescriptor : ControllerDescriptor
	{
		// Token: 0x0600079E RID: 1950 RVA: 0x00014AF8 File Offset: 0x00012CF8
		public ReflectedAsyncControllerDescriptor(Type controllerType)
		{
			if (controllerType == null)
			{
				throw new ArgumentNullException("controllerType");
			}
			this._controllerType = controllerType;
			bool allowLegacyAsyncActions = ReflectedAsyncControllerDescriptor.AllowLegacyAsyncActions(this._controllerType);
			this._selector = new AsyncActionMethodSelector(this._controllerType, allowLegacyAsyncActions);
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x0600079F RID: 1951 RVA: 0x00014B44 File Offset: 0x00012D44
		public sealed override Type ControllerType
		{
			get
			{
				return this._controllerType;
			}
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060007A0 RID: 1952 RVA: 0x00014B4C File Offset: 0x00012D4C
		internal AsyncActionMethodSelector Selector
		{
			get
			{
				return this._selector;
			}
		}

		// Token: 0x060007A1 RID: 1953 RVA: 0x00014B54 File Offset: 0x00012D54
		private static bool AllowLegacyAsyncActions(Type controllerType)
		{
			return typeof(AsyncController).IsAssignableFrom(controllerType) || (!typeof(Controller).IsAssignableFrom(controllerType) && typeof(IAsyncController).IsAssignableFrom(controllerType));
		}

		// Token: 0x060007A2 RID: 1954 RVA: 0x00014B94 File Offset: 0x00012D94
		public override ActionDescriptor FindAction(ControllerContext controllerContext, string actionName)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			if (string.IsNullOrEmpty(actionName))
			{
				throw Error.ParameterCannotBeNullOrEmpty("actionName");
			}
			ActionDescriptorCreator actionDescriptorCreator = this._selector.FindAction(controllerContext, actionName);
			if (actionDescriptorCreator == null)
			{
				return null;
			}
			return actionDescriptorCreator(actionName, this);
		}

		// Token: 0x060007A3 RID: 1955 RVA: 0x00014BDD File Offset: 0x00012DDD
		public override ActionDescriptor[] GetCanonicalActions()
		{
			return ReflectedAsyncControllerDescriptor._emptyCanonicalActions;
		}

		// Token: 0x060007A4 RID: 1956 RVA: 0x00014BE4 File Offset: 0x00012DE4
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.ControllerType.GetCustomAttributes(inherit);
		}

		// Token: 0x060007A5 RID: 1957 RVA: 0x00014BF2 File Offset: 0x00012DF2
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.ControllerType.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x060007A6 RID: 1958 RVA: 0x00014C01 File Offset: 0x00012E01
		public override IEnumerable<FilterAttribute> GetFilterAttributes(bool useCache)
		{
			if (useCache && base.GetType() == typeof(ReflectedAsyncControllerDescriptor))
			{
				return ReflectedAttributeCache.GetTypeFilterAttributes(this.ControllerType);
			}
			return base.GetFilterAttributes(useCache);
		}

		// Token: 0x060007A7 RID: 1959 RVA: 0x00014C30 File Offset: 0x00012E30
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.ControllerType.IsDefined(attributeType, inherit);
		}

		// Token: 0x0400021D RID: 541
		internal static readonly Func<Type, ControllerDescriptor> DefaultDescriptorFactory = (Type type) => new ReflectedAsyncControllerDescriptor(type);

		// Token: 0x0400021E RID: 542
		private static readonly ActionDescriptor[] _emptyCanonicalActions = new ActionDescriptor[0];

		// Token: 0x0400021F RID: 543
		private readonly Type _controllerType;

		// Token: 0x04000220 RID: 544
		private readonly AsyncActionMethodSelector _selector;
	}
}

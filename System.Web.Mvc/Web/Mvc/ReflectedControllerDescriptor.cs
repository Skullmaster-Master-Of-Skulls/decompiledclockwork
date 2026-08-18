using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x020001A8 RID: 424
	public class ReflectedControllerDescriptor : ControllerDescriptor
	{
		// Token: 0x06000BCB RID: 3019 RVA: 0x0001EDEA File Offset: 0x0001CFEA
		public ReflectedControllerDescriptor(Type controllerType)
		{
			if (controllerType == null)
			{
				throw new ArgumentNullException("controllerType");
			}
			this._controllerType = controllerType;
			this._selector = new ActionMethodSelector(this._controllerType);
		}

		// Token: 0x170002BD RID: 701
		// (get) Token: 0x06000BCC RID: 3020 RVA: 0x0001EE1E File Offset: 0x0001D01E
		public sealed override Type ControllerType
		{
			get
			{
				return this._controllerType;
			}
		}

		// Token: 0x06000BCD RID: 3021 RVA: 0x0001EE28 File Offset: 0x0001D028
		public override ActionDescriptor FindAction(ControllerContext controllerContext, string actionName)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			if (string.IsNullOrEmpty(actionName))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "actionName");
			}
			MethodInfo methodInfo = this._selector.FindActionMethod(controllerContext, actionName);
			if (methodInfo == null)
			{
				return null;
			}
			return new ReflectedActionDescriptor(methodInfo, actionName, this);
		}

		// Token: 0x06000BCE RID: 3022 RVA: 0x0001EE7C File Offset: 0x0001D07C
		private MethodInfo[] GetAllActionMethodsFromSelector()
		{
			return this._selector.StandardRouteMethods.ToArray<MethodInfo>();
		}

		// Token: 0x06000BCF RID: 3023 RVA: 0x0001EE90 File Offset: 0x0001D090
		public override ActionDescriptor[] GetCanonicalActions()
		{
			ActionDescriptor[] array = this.LazilyFetchCanonicalActionsCollection();
			return (ActionDescriptor[])array.Clone();
		}

		// Token: 0x06000BD0 RID: 3024 RVA: 0x0001EEAF File Offset: 0x0001D0AF
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.ControllerType.GetCustomAttributes(inherit);
		}

		// Token: 0x06000BD1 RID: 3025 RVA: 0x0001EEBD File Offset: 0x0001D0BD
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.ControllerType.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06000BD2 RID: 3026 RVA: 0x0001EECC File Offset: 0x0001D0CC
		public override IEnumerable<FilterAttribute> GetFilterAttributes(bool useCache)
		{
			if (useCache && base.GetType() == typeof(ReflectedControllerDescriptor))
			{
				return ReflectedAttributeCache.GetTypeFilterAttributes(this.ControllerType);
			}
			return base.GetFilterAttributes(useCache);
		}

		// Token: 0x06000BD3 RID: 3027 RVA: 0x0001EEFB File Offset: 0x0001D0FB
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.ControllerType.IsDefined(attributeType, inherit);
		}

		// Token: 0x06000BD4 RID: 3028 RVA: 0x0001EF24 File Offset: 0x0001D124
		private ActionDescriptor[] LazilyFetchCanonicalActionsCollection()
		{
			return DescriptorUtil.LazilyFetchOrCreateDescriptors<MethodInfo, ActionDescriptor, ReflectedControllerDescriptor>(ref this._canonicalActionsCache, (ReflectedControllerDescriptor state) => state.GetAllActionMethodsFromSelector(), (MethodInfo methodInfo, ReflectedControllerDescriptor state) => ReflectedActionDescriptor.TryCreateDescriptor(methodInfo, methodInfo.Name, state), this);
		}

		// Token: 0x04000325 RID: 805
		private readonly Type _controllerType;

		// Token: 0x04000326 RID: 806
		private readonly ActionMethodSelector _selector;

		// Token: 0x04000327 RID: 807
		private ActionDescriptor[] _canonicalActionsCache;
	}
}

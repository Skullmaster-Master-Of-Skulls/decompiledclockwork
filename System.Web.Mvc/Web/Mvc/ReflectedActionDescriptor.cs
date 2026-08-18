using System;
using System.Collections.Generic;
using System.Reflection;
using System.Text;
using System.Web.Mvc.Properties;

namespace System.Web.Mvc
{
	// Token: 0x020001AA RID: 426
	public class ReflectedActionDescriptor : ActionDescriptor, IMethodInfoActionDescriptor
	{
		// Token: 0x06000BDB RID: 3035 RVA: 0x0001EF77 File Offset: 0x0001D177
		public ReflectedActionDescriptor(MethodInfo methodInfo, string actionName, ControllerDescriptor controllerDescriptor) : this(methodInfo, actionName, controllerDescriptor, true)
		{
		}

		// Token: 0x06000BDC RID: 3036 RVA: 0x0001EF84 File Offset: 0x0001D184
		internal ReflectedActionDescriptor(MethodInfo methodInfo, string actionName, ControllerDescriptor controllerDescriptor, bool validateMethod)
		{
			if (methodInfo == null)
			{
				throw new ArgumentNullException("methodInfo");
			}
			if (string.IsNullOrEmpty(actionName))
			{
				throw new ArgumentException(MvcResources.Common_NullOrEmpty, "actionName");
			}
			if (controllerDescriptor == null)
			{
				throw new ArgumentNullException("controllerDescriptor");
			}
			if (validateMethod)
			{
				string text = ActionDescriptor.VerifyActionMethodIsCallable(methodInfo);
				if (text != null)
				{
					throw new ArgumentException(text, "methodInfo");
				}
			}
			this.MethodInfo = methodInfo;
			this._actionName = actionName;
			this._controllerDescriptor = controllerDescriptor;
		}

		// Token: 0x170002BE RID: 702
		// (get) Token: 0x06000BDD RID: 3037 RVA: 0x0001F000 File Offset: 0x0001D200
		public override string ActionName
		{
			get
			{
				return this._actionName;
			}
		}

		// Token: 0x170002BF RID: 703
		// (get) Token: 0x06000BDE RID: 3038 RVA: 0x0001F008 File Offset: 0x0001D208
		public override ControllerDescriptor ControllerDescriptor
		{
			get
			{
				return this._controllerDescriptor;
			}
		}

		// Token: 0x170002C0 RID: 704
		// (get) Token: 0x06000BDF RID: 3039 RVA: 0x0001F010 File Offset: 0x0001D210
		// (set) Token: 0x06000BE0 RID: 3040 RVA: 0x0001F018 File Offset: 0x0001D218
		public MethodInfo MethodInfo { get; private set; }

		// Token: 0x170002C1 RID: 705
		// (get) Token: 0x06000BE1 RID: 3041 RVA: 0x0001F021 File Offset: 0x0001D221
		public override string UniqueId
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

		// Token: 0x06000BE2 RID: 3042 RVA: 0x0001F040 File Offset: 0x0001D240
		private string CreateUniqueId()
		{
			StringBuilder stringBuilder = new StringBuilder(base.UniqueId);
			DescriptorUtil.AppendUniqueId(stringBuilder, this.MethodInfo);
			return stringBuilder.ToString();
		}

		// Token: 0x06000BE3 RID: 3043 RVA: 0x0001F06C File Offset: 0x0001D26C
		public override object Execute(ControllerContext controllerContext, IDictionary<string, object> parameters)
		{
			if (controllerContext == null)
			{
				throw new ArgumentNullException("controllerContext");
			}
			if (parameters == null)
			{
				throw new ArgumentNullException("parameters");
			}
			ParameterInfo[] parameters2 = this.MethodInfo.GetParameters();
			object[] array = new object[parameters2.Length];
			for (int i = 0; i < parameters2.Length; i++)
			{
				ParameterInfo parameterInfo = parameters2[i];
				object obj = ActionDescriptor.ExtractParameterFromDictionary(parameterInfo, parameters, this.MethodInfo);
				array[i] = obj;
			}
			ActionMethodDispatcher dispatcher = base.DispatcherCache.GetDispatcher(this.MethodInfo);
			return dispatcher.Execute(controllerContext.Controller, array);
		}

		// Token: 0x06000BE4 RID: 3044 RVA: 0x0001F0F5 File Offset: 0x0001D2F5
		public override object[] GetCustomAttributes(bool inherit)
		{
			return ActionDescriptorHelper.GetCustomAttributes(this.MethodInfo, inherit);
		}

		// Token: 0x06000BE5 RID: 3045 RVA: 0x0001F103 File Offset: 0x0001D303
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return ActionDescriptorHelper.GetCustomAttributes(this.MethodInfo, attributeType, inherit);
		}

		// Token: 0x06000BE6 RID: 3046 RVA: 0x0001F112 File Offset: 0x0001D312
		public override IEnumerable<FilterAttribute> GetFilterAttributes(bool useCache)
		{
			if (useCache && base.GetType() == typeof(ReflectedActionDescriptor))
			{
				return ReflectedAttributeCache.GetMethodFilterAttributes(this.MethodInfo);
			}
			return base.GetFilterAttributes(useCache);
		}

		// Token: 0x06000BE7 RID: 3047 RVA: 0x0001F141 File Offset: 0x0001D341
		public override ParameterDescriptor[] GetParameters()
		{
			return ActionDescriptorHelper.GetParameters(this, this.MethodInfo, ref this._parametersCache);
		}

		// Token: 0x06000BE8 RID: 3048 RVA: 0x0001F155 File Offset: 0x0001D355
		public override ICollection<ActionSelector> GetSelectors()
		{
			return ActionDescriptorHelper.GetSelectors(this.MethodInfo);
		}

		// Token: 0x06000BE9 RID: 3049 RVA: 0x0001F162 File Offset: 0x0001D362
		internal override ICollection<ActionNameSelector> GetNameSelectors()
		{
			return ActionDescriptorHelper.GetNameSelectors(this.MethodInfo);
		}

		// Token: 0x06000BEA RID: 3050 RVA: 0x0001F16F File Offset: 0x0001D36F
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return ActionDescriptorHelper.IsDefined(this.MethodInfo, attributeType, inherit);
		}

		// Token: 0x06000BEB RID: 3051 RVA: 0x0001F180 File Offset: 0x0001D380
		internal static ReflectedActionDescriptor TryCreateDescriptor(MethodInfo methodInfo, string name, ControllerDescriptor controllerDescriptor)
		{
			ReflectedActionDescriptor result = new ReflectedActionDescriptor(methodInfo, name, controllerDescriptor, false);
			string text = ActionDescriptor.VerifyActionMethodIsCallable(methodInfo);
			if (text != null)
			{
				return null;
			}
			return result;
		}

		// Token: 0x0400032A RID: 810
		private readonly string _actionName;

		// Token: 0x0400032B RID: 811
		private readonly ControllerDescriptor _controllerDescriptor;

		// Token: 0x0400032C RID: 812
		private string _uniqueId;

		// Token: 0x0400032D RID: 813
		private ParameterDescriptor[] _parametersCache;
	}
}

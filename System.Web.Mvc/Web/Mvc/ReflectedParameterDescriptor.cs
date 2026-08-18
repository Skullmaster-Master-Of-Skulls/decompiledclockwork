using System;
using System.Reflection;

namespace System.Web.Mvc
{
	// Token: 0x020001B5 RID: 437
	public class ReflectedParameterDescriptor : ParameterDescriptor
	{
		// Token: 0x06000C52 RID: 3154 RVA: 0x00020A75 File Offset: 0x0001EC75
		public ReflectedParameterDescriptor(ParameterInfo parameterInfo, ActionDescriptor actionDescriptor)
		{
			if (parameterInfo == null)
			{
				throw new ArgumentNullException("parameterInfo");
			}
			if (actionDescriptor == null)
			{
				throw new ArgumentNullException("actionDescriptor");
			}
			this.ParameterInfo = parameterInfo;
			this._actionDescriptor = actionDescriptor;
			this._bindingInfo = new ReflectedParameterBindingInfo(parameterInfo);
		}

		// Token: 0x170002D8 RID: 728
		// (get) Token: 0x06000C53 RID: 3155 RVA: 0x00020AB3 File Offset: 0x0001ECB3
		public override ActionDescriptor ActionDescriptor
		{
			get
			{
				return this._actionDescriptor;
			}
		}

		// Token: 0x170002D9 RID: 729
		// (get) Token: 0x06000C54 RID: 3156 RVA: 0x00020ABB File Offset: 0x0001ECBB
		public override ParameterBindingInfo BindingInfo
		{
			get
			{
				return this._bindingInfo;
			}
		}

		// Token: 0x170002DA RID: 730
		// (get) Token: 0x06000C55 RID: 3157 RVA: 0x00020AC4 File Offset: 0x0001ECC4
		public override object DefaultValue
		{
			get
			{
				object result;
				if (ParameterInfoUtil.TryGetDefaultValue(this.ParameterInfo, out result))
				{
					return result;
				}
				return base.DefaultValue;
			}
		}

		// Token: 0x170002DB RID: 731
		// (get) Token: 0x06000C56 RID: 3158 RVA: 0x00020AE8 File Offset: 0x0001ECE8
		// (set) Token: 0x06000C57 RID: 3159 RVA: 0x00020AF0 File Offset: 0x0001ECF0
		public ParameterInfo ParameterInfo { get; private set; }

		// Token: 0x170002DC RID: 732
		// (get) Token: 0x06000C58 RID: 3160 RVA: 0x00020AF9 File Offset: 0x0001ECF9
		public override string ParameterName
		{
			get
			{
				return this.ParameterInfo.Name;
			}
		}

		// Token: 0x170002DD RID: 733
		// (get) Token: 0x06000C59 RID: 3161 RVA: 0x00020B06 File Offset: 0x0001ED06
		public override Type ParameterType
		{
			get
			{
				return this.ParameterInfo.ParameterType;
			}
		}

		// Token: 0x06000C5A RID: 3162 RVA: 0x00020B13 File Offset: 0x0001ED13
		public override object[] GetCustomAttributes(bool inherit)
		{
			return this.ParameterInfo.GetCustomAttributes(inherit);
		}

		// Token: 0x06000C5B RID: 3163 RVA: 0x00020B21 File Offset: 0x0001ED21
		public override object[] GetCustomAttributes(Type attributeType, bool inherit)
		{
			return this.ParameterInfo.GetCustomAttributes(attributeType, inherit);
		}

		// Token: 0x06000C5C RID: 3164 RVA: 0x00020B30 File Offset: 0x0001ED30
		public override bool IsDefined(Type attributeType, bool inherit)
		{
			return this.ParameterInfo.IsDefined(attributeType, inherit);
		}

		// Token: 0x04000350 RID: 848
		private readonly ActionDescriptor _actionDescriptor;

		// Token: 0x04000351 RID: 849
		private readonly ReflectedParameterBindingInfo _bindingInfo;
	}
}

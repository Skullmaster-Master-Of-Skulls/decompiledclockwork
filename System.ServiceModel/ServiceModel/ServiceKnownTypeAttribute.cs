using System;

namespace System.ServiceModel
{
	// Token: 0x020000E1 RID: 225
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Method | AttributeTargets.Interface, Inherited = true, AllowMultiple = true)]
	[__DynamicallyInvokable]
	public sealed class ServiceKnownTypeAttribute : Attribute
	{
		// Token: 0x06000477 RID: 1143 RVA: 0x0001662F File Offset: 0x0001482F
		private ServiceKnownTypeAttribute()
		{
		}

		// Token: 0x06000478 RID: 1144 RVA: 0x00016637 File Offset: 0x00014837
		[__DynamicallyInvokable]
		public ServiceKnownTypeAttribute(Type type)
		{
			this.type = type;
		}

		// Token: 0x06000479 RID: 1145 RVA: 0x00016646 File Offset: 0x00014846
		[__DynamicallyInvokable]
		public ServiceKnownTypeAttribute(string methodName)
		{
			this.methodName = methodName;
		}

		// Token: 0x0600047A RID: 1146 RVA: 0x00016655 File Offset: 0x00014855
		[__DynamicallyInvokable]
		public ServiceKnownTypeAttribute(string methodName, Type declaringType)
		{
			this.methodName = methodName;
			this.declaringType = declaringType;
		}

		// Token: 0x17000131 RID: 305
		// (get) Token: 0x0600047B RID: 1147 RVA: 0x0001666B File Offset: 0x0001486B
		[__DynamicallyInvokable]
		public Type DeclaringType
		{
			[__DynamicallyInvokable]
			get
			{
				return this.declaringType;
			}
		}

		// Token: 0x17000132 RID: 306
		// (get) Token: 0x0600047C RID: 1148 RVA: 0x00016673 File Offset: 0x00014873
		[__DynamicallyInvokable]
		public string MethodName
		{
			[__DynamicallyInvokable]
			get
			{
				return this.methodName;
			}
		}

		// Token: 0x17000133 RID: 307
		// (get) Token: 0x0600047D RID: 1149 RVA: 0x0001667B File Offset: 0x0001487B
		[__DynamicallyInvokable]
		public Type Type
		{
			[__DynamicallyInvokable]
			get
			{
				return this.type;
			}
		}

		// Token: 0x040009FB RID: 2555
		private Type declaringType;

		// Token: 0x040009FC RID: 2556
		private string methodName;

		// Token: 0x040009FD RID: 2557
		private Type type;
	}
}

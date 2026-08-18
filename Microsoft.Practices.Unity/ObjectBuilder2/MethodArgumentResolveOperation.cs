using System;
using System.Globalization;
using System.Reflection;
using Microsoft.Practices.Unity.Properties;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x0200005C RID: 92
	public class MethodArgumentResolveOperation : BuildOperation
	{
		// Token: 0x0600018C RID: 396 RVA: 0x00005978 File Offset: 0x00003B78
		public MethodArgumentResolveOperation(Type typeBeingConstructed, string methodSignature, string parameterName) : base(typeBeingConstructed)
		{
			this.methodSignature = methodSignature;
			this.parameterName = parameterName;
		}

		// Token: 0x0600018D RID: 397 RVA: 0x00005990 File Offset: 0x00003B90
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, Resources.MethodArgumentResolveOperation, new object[]
			{
				this.parameterName,
				base.TypeBeingConstructed.GetTypeInfo().Name,
				this.methodSignature
			});
		}

		// Token: 0x17000037 RID: 55
		// (get) Token: 0x0600018E RID: 398 RVA: 0x000059D9 File Offset: 0x00003BD9
		public string MethodSignature
		{
			get
			{
				return this.methodSignature;
			}
		}

		// Token: 0x17000038 RID: 56
		// (get) Token: 0x0600018F RID: 399 RVA: 0x000059E1 File Offset: 0x00003BE1
		public string ParameterName
		{
			get
			{
				return this.parameterName;
			}
		}

		// Token: 0x0400005B RID: 91
		private readonly string methodSignature;

		// Token: 0x0400005C RID: 92
		private readonly string parameterName;
	}
}

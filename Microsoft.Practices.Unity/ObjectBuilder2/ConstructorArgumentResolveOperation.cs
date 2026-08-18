using System;
using System.Globalization;
using Microsoft.Practices.Unity.Properties;

namespace Microsoft.Practices.ObjectBuilder2
{
	// Token: 0x02000054 RID: 84
	public class ConstructorArgumentResolveOperation : BuildOperation
	{
		// Token: 0x06000157 RID: 343 RVA: 0x0000481B File Offset: 0x00002A1B
		public ConstructorArgumentResolveOperation(Type typeBeingConstructed, string constructorSignature, string parameterName) : base(typeBeingConstructed)
		{
			this.constructorSignature = constructorSignature;
			this.parameterName = parameterName;
		}

		// Token: 0x06000158 RID: 344 RVA: 0x00004834 File Offset: 0x00002A34
		public override string ToString()
		{
			return string.Format(CultureInfo.CurrentCulture, Resources.ConstructorArgumentResolveOperation, new object[]
			{
				this.parameterName,
				this.constructorSignature
			});
		}

		// Token: 0x17000031 RID: 49
		// (get) Token: 0x06000159 RID: 345 RVA: 0x0000486A File Offset: 0x00002A6A
		public string ConstructorSignature
		{
			get
			{
				return this.constructorSignature;
			}
		}

		// Token: 0x17000032 RID: 50
		// (get) Token: 0x0600015A RID: 346 RVA: 0x00004872 File Offset: 0x00002A72
		public string ParameterName
		{
			get
			{
				return this.parameterName;
			}
		}

		// Token: 0x04000046 RID: 70
		private readonly string constructorSignature;

		// Token: 0x04000047 RID: 71
		private readonly string parameterName;
	}
}

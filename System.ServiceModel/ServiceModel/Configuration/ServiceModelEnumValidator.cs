using System;
using System.ComponentModel;
using System.Configuration;
using System.Reflection;

namespace System.ServiceModel.Configuration
{
	// Token: 0x020006CE RID: 1742
	internal class ServiceModelEnumValidator : ConfigurationValidatorBase
	{
		// Token: 0x06004359 RID: 17241 RVA: 0x000FE88A File Offset: 0x000FCA8A
		public ServiceModelEnumValidator(Type enumHelperType)
		{
			this.enumHelperType = enumHelperType;
			this.isDefined = this.enumHelperType.GetMethod("IsDefined", BindingFlags.Static | BindingFlags.Public | BindingFlags.NonPublic);
		}

		// Token: 0x0600435A RID: 17242 RVA: 0x000FE8B1 File Offset: 0x000FCAB1
		public override bool CanValidate(Type type)
		{
			return this.isDefined != null;
		}

		// Token: 0x0600435B RID: 17243 RVA: 0x000FE8C0 File Offset: 0x000FCAC0
		public override void Validate(object value)
		{
			if (!(bool)this.isDefined.Invoke(null, new object[]
			{
				value
			}))
			{
				ParameterInfo[] parameters = this.isDefined.GetParameters();
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new InvalidEnumArgumentException("value", (int)value, parameters[0].ParameterType));
			}
		}

		// Token: 0x04002D14 RID: 11540
		private Type enumHelperType;

		// Token: 0x04002D15 RID: 11541
		private MethodInfo isDefined;
	}
}

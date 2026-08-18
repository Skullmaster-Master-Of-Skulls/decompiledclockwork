using System;
using System.Reflection;

namespace System.Configuration
{
	// Token: 0x02000014 RID: 20
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class CallbackValidatorAttribute : ConfigurationValidatorAttribute
	{
		// Token: 0x17000027 RID: 39
		// (get) Token: 0x060000AE RID: 174 RVA: 0x000074F0 File Offset: 0x000056F0
		public override ConfigurationValidatorBase ValidatorInstance
		{
			get
			{
				if (this._callbackMethod == null)
				{
					if (this._type == null)
					{
						throw new ArgumentNullException("Type");
					}
					if (!string.IsNullOrEmpty(this._callbackMethodName))
					{
						MethodInfo method = this._type.GetMethod(this._callbackMethodName, BindingFlags.Static | BindingFlags.Public);
						if (method != null)
						{
							ParameterInfo[] parameters = method.GetParameters();
							if (parameters.Length == 1 && parameters[0].ParameterType == typeof(object))
							{
								this._callbackMethod = (ValidatorCallback)TypeUtil.CreateDelegateRestricted(this._declaringType, typeof(ValidatorCallback), method);
							}
						}
					}
				}
				if (this._callbackMethod == null)
				{
					throw new ArgumentException(SR.GetString("Validator_method_not_found", new object[]
					{
						this._callbackMethodName
					}));
				}
				return new CallbackValidator(this._callbackMethod);
			}
		}

		// Token: 0x17000028 RID: 40
		// (get) Token: 0x060000B0 RID: 176 RVA: 0x000075D7 File Offset: 0x000057D7
		// (set) Token: 0x060000B1 RID: 177 RVA: 0x000075DF File Offset: 0x000057DF
		public Type Type
		{
			get
			{
				return this._type;
			}
			set
			{
				this._type = value;
				this._callbackMethod = null;
			}
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x060000B2 RID: 178 RVA: 0x000075EF File Offset: 0x000057EF
		// (set) Token: 0x060000B3 RID: 179 RVA: 0x000075F7 File Offset: 0x000057F7
		public string CallbackMethodName
		{
			get
			{
				return this._callbackMethodName;
			}
			set
			{
				this._callbackMethodName = value;
				this._callbackMethod = null;
			}
		}

		// Token: 0x0400012E RID: 302
		private Type _type;

		// Token: 0x0400012F RID: 303
		private string _callbackMethodName = string.Empty;

		// Token: 0x04000130 RID: 304
		private ValidatorCallback _callbackMethod;
	}
}

using System;

namespace System.Configuration
{
	// Token: 0x02000013 RID: 19
	public sealed class CallbackValidator : ConfigurationValidatorBase
	{
		// Token: 0x060000AA RID: 170 RVA: 0x0000747B File Offset: 0x0000567B
		public CallbackValidator(Type type, ValidatorCallback callback) : this(callback)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this._type = type;
		}

		// Token: 0x060000AB RID: 171 RVA: 0x0000749F File Offset: 0x0000569F
		internal CallbackValidator(ValidatorCallback callback)
		{
			if (callback == null)
			{
				throw new ArgumentNullException("callback");
			}
			this._type = null;
			this._callback = callback;
		}

		// Token: 0x060000AC RID: 172 RVA: 0x000074C3 File Offset: 0x000056C3
		public override bool CanValidate(Type type)
		{
			return type == this._type || this._type == null;
		}

		// Token: 0x060000AD RID: 173 RVA: 0x000074E1 File Offset: 0x000056E1
		public override void Validate(object value)
		{
			this._callback(value);
		}

		// Token: 0x0400012C RID: 300
		private Type _type;

		// Token: 0x0400012D RID: 301
		private ValidatorCallback _callback;
	}
}

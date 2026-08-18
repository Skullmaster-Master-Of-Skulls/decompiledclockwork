using System;
using System.Configuration;

namespace System.Net
{
	// Token: 0x02000223 RID: 547
	internal sealed class TimeoutValidator : ConfigurationValidatorBase
	{
		// Token: 0x06001423 RID: 5155 RVA: 0x0006AAE4 File Offset: 0x00068CE4
		internal TimeoutValidator(bool zeroValid)
		{
			this._zeroValid = zeroValid;
		}

		// Token: 0x06001424 RID: 5156 RVA: 0x0006AAF3 File Offset: 0x00068CF3
		public override bool CanValidate(Type type)
		{
			return type == typeof(int) || type == typeof(long);
		}

		// Token: 0x06001425 RID: 5157 RVA: 0x0006AB1C File Offset: 0x00068D1C
		public override void Validate(object value)
		{
			if (value == null)
			{
				return;
			}
			int num = (int)value;
			if (this._zeroValid && num == 0)
			{
				return;
			}
			if (num <= 0 && num != -1)
			{
				throw new ConfigurationErrorsException(SR.GetString("net_io_timeout_use_gt_zero"));
			}
		}

		// Token: 0x0400161F RID: 5663
		private bool _zeroValid;
	}
}

using System;
using System.Configuration;

namespace System.Net
{
	// Token: 0x02000669 RID: 1641
	internal sealed class TimeoutValidator : ConfigurationValidatorBase
	{
		// Token: 0x060032C8 RID: 13000 RVA: 0x000D756A File Offset: 0x000D656A
		internal TimeoutValidator(bool zeroValid)
		{
			this._zeroValid = zeroValid;
		}

		// Token: 0x060032C9 RID: 13001 RVA: 0x000D7579 File Offset: 0x000D6579
		public override bool CanValidate(Type type)
		{
			return type == typeof(int) || type == typeof(long);
		}

		// Token: 0x060032CA RID: 13002 RVA: 0x000D7598 File Offset: 0x000D6598
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

		// Token: 0x04002F74 RID: 12148
		private bool _zeroValid;
	}
}

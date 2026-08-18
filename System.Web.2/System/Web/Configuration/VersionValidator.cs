using System;
using System.Configuration;

namespace System.Web.Configuration
{
	// Token: 0x0200076E RID: 1902
	internal sealed class VersionValidator : ConfigurationValidatorBase
	{
		// Token: 0x06005BA0 RID: 23456 RVA: 0x0013D8E2 File Offset: 0x0013BAE2
		public VersionValidator(Version minimumVersion)
		{
			this._minimumVersion = minimumVersion;
		}

		// Token: 0x06005BA1 RID: 23457 RVA: 0x0013D8F1 File Offset: 0x0013BAF1
		public override bool CanValidate(Type type)
		{
			return typeof(Version).Equals(type);
		}

		// Token: 0x06005BA2 RID: 23458 RVA: 0x0013D903 File Offset: 0x0013BB03
		public override void Validate(object value)
		{
			if (value == null)
			{
				throw new ArgumentNullException("value");
			}
			if ((Version)value < this._minimumVersion)
			{
				throw new ArgumentOutOfRangeException("value", SR.GetString("Config_control_rendering_compatibility_version_is_less_than_minimum_version"));
			}
		}

		// Token: 0x04003046 RID: 12358
		private readonly Version _minimumVersion;
	}
}

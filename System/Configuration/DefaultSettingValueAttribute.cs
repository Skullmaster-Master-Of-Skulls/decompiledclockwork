using System;

namespace System.Configuration
{
	// Token: 0x02000707 RID: 1799
	[AttributeUsage(AttributeTargets.Property)]
	public sealed class DefaultSettingValueAttribute : Attribute
	{
		// Token: 0x0600375D RID: 14173 RVA: 0x000EB4CE File Offset: 0x000EA4CE
		public DefaultSettingValueAttribute(string value)
		{
			this._value = value;
		}

		// Token: 0x17000CD3 RID: 3283
		// (get) Token: 0x0600375E RID: 14174 RVA: 0x000EB4DD File Offset: 0x000EA4DD
		public string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x040031CA RID: 12746
		private readonly string _value;
	}
}

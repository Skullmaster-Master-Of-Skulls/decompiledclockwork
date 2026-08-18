using System;

namespace System.Configuration
{
	// Token: 0x02000067 RID: 103
	internal sealed class InvalidPropValue
	{
		// Token: 0x060003EE RID: 1006 RVA: 0x000141F8 File Offset: 0x000123F8
		internal InvalidPropValue(string value, ConfigurationException error)
		{
			this._value = value;
			this._error = error;
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x060003EF RID: 1007 RVA: 0x0001420E File Offset: 0x0001240E
		internal ConfigurationException Error
		{
			get
			{
				return this._error;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x060003F0 RID: 1008 RVA: 0x00014216 File Offset: 0x00012416
		internal string Value
		{
			get
			{
				return this._value;
			}
		}

		// Token: 0x0400028D RID: 653
		private string _value;

		// Token: 0x0400028E RID: 654
		private ConfigurationException _error;
	}
}

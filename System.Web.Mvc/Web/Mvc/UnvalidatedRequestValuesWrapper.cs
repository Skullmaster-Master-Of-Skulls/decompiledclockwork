using System;
using System.Collections.Specialized;

namespace System.Web.Mvc
{
	// Token: 0x020000A4 RID: 164
	internal sealed class UnvalidatedRequestValuesWrapper : IUnvalidatedRequestValues
	{
		// Token: 0x0600047D RID: 1149 RVA: 0x0000D0B9 File Offset: 0x0000B2B9
		public UnvalidatedRequestValuesWrapper(UnvalidatedRequestValuesBase unvalidatedValues)
		{
			this._unvalidatedValues = unvalidatedValues;
		}

		// Token: 0x17000183 RID: 387
		// (get) Token: 0x0600047E RID: 1150 RVA: 0x0000D0C8 File Offset: 0x0000B2C8
		public NameValueCollection Form
		{
			get
			{
				return this._unvalidatedValues.Form;
			}
		}

		// Token: 0x17000184 RID: 388
		// (get) Token: 0x0600047F RID: 1151 RVA: 0x0000D0D5 File Offset: 0x0000B2D5
		public NameValueCollection QueryString
		{
			get
			{
				return this._unvalidatedValues.QueryString;
			}
		}

		// Token: 0x17000185 RID: 389
		public string this[string key]
		{
			get
			{
				return this._unvalidatedValues[key];
			}
		}

		// Token: 0x04000142 RID: 322
		private readonly UnvalidatedRequestValuesBase _unvalidatedValues;
	}
}

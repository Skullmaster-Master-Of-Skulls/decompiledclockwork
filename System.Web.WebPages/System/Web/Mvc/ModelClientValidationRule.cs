using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;

namespace System.Web.Mvc
{
	// Token: 0x02000045 RID: 69
	[TypeForwardedFrom("System.Web.Mvc, Version=3.0.0.0, Culture=neutral, PublicKeyToken=31bf3856ad364e35")]
	public class ModelClientValidationRule
	{
		// Token: 0x17000069 RID: 105
		// (get) Token: 0x060001DB RID: 475 RVA: 0x00007AC6 File Offset: 0x00005CC6
		// (set) Token: 0x060001DC RID: 476 RVA: 0x00007ACE File Offset: 0x00005CCE
		public string ErrorMessage { get; set; }

		// Token: 0x1700006A RID: 106
		// (get) Token: 0x060001DD RID: 477 RVA: 0x00007AD7 File Offset: 0x00005CD7
		public IDictionary<string, object> ValidationParameters
		{
			get
			{
				return this._validationParameters;
			}
		}

		// Token: 0x1700006B RID: 107
		// (get) Token: 0x060001DE RID: 478 RVA: 0x00007ADF File Offset: 0x00005CDF
		// (set) Token: 0x060001DF RID: 479 RVA: 0x00007AF0 File Offset: 0x00005CF0
		public string ValidationType
		{
			get
			{
				return this._validationType ?? string.Empty;
			}
			set
			{
				this._validationType = value;
			}
		}

		// Token: 0x0400009C RID: 156
		private readonly Dictionary<string, object> _validationParameters = new Dictionary<string, object>();

		// Token: 0x0400009D RID: 157
		private string _validationType;
	}
}

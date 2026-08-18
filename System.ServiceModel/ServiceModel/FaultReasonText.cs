using System;
using System.Globalization;

namespace System.ServiceModel
{
	// Token: 0x02000126 RID: 294
	[__DynamicallyInvokable]
	public class FaultReasonText
	{
		// Token: 0x060007E1 RID: 2017 RVA: 0x00020E0D File Offset: 0x0001F00D
		[__DynamicallyInvokable]
		public FaultReasonText(string text)
		{
			if (text == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("text"));
			}
			this.text = text;
			this.xmlLang = CultureInfo.CurrentCulture.Name;
		}

		// Token: 0x060007E2 RID: 2018 RVA: 0x00020E44 File Offset: 0x0001F044
		[__DynamicallyInvokable]
		public FaultReasonText(string text, string xmlLang)
		{
			if (text == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("text"));
			}
			if (xmlLang == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("xmlLang"));
			}
			this.text = text;
			this.xmlLang = xmlLang;
		}

		// Token: 0x060007E3 RID: 2019 RVA: 0x00020E98 File Offset: 0x0001F098
		public FaultReasonText(string text, CultureInfo cultureInfo)
		{
			if (text == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("text"));
			}
			if (cultureInfo == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("cultureInfo"));
			}
			this.text = text;
			this.xmlLang = cultureInfo.Name;
		}

		// Token: 0x060007E4 RID: 2020 RVA: 0x00020EEE File Offset: 0x0001F0EE
		[__DynamicallyInvokable]
		public bool Matches(CultureInfo cultureInfo)
		{
			if (cultureInfo == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("cultureInfo"));
			}
			return this.xmlLang == cultureInfo.Name;
		}

		// Token: 0x17000200 RID: 512
		// (get) Token: 0x060007E5 RID: 2021 RVA: 0x00020F19 File Offset: 0x0001F119
		[__DynamicallyInvokable]
		public string XmlLang
		{
			[__DynamicallyInvokable]
			get
			{
				return this.xmlLang;
			}
		}

		// Token: 0x17000201 RID: 513
		// (get) Token: 0x060007E6 RID: 2022 RVA: 0x00020F21 File Offset: 0x0001F121
		[__DynamicallyInvokable]
		public string Text
		{
			[__DynamicallyInvokable]
			get
			{
				return this.text;
			}
		}

		// Token: 0x04000AF5 RID: 2805
		private string xmlLang;

		// Token: 0x04000AF6 RID: 2806
		private string text;
	}
}

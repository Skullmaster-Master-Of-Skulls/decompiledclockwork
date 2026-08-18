using System;
using System.Collections.Generic;
using System.Globalization;

namespace System.ServiceModel
{
	// Token: 0x02000125 RID: 293
	[__DynamicallyInvokable]
	public class FaultReason
	{
		// Token: 0x060007D6 RID: 2006 RVA: 0x00020B50 File Offset: 0x0001ED50
		[__DynamicallyInvokable]
		public FaultReason(FaultReasonText translation)
		{
			if (translation == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgumentNull("translation");
			}
			this.Init(translation);
		}

		// Token: 0x060007D7 RID: 2007 RVA: 0x00020B72 File Offset: 0x0001ED72
		[__DynamicallyInvokable]
		public FaultReason(string text)
		{
			this.Init(new FaultReasonText(text));
		}

		// Token: 0x060007D8 RID: 2008 RVA: 0x00020B86 File Offset: 0x0001ED86
		internal FaultReason(string text, string xmlLang)
		{
			this.Init(new FaultReasonText(text, xmlLang));
		}

		// Token: 0x060007D9 RID: 2009 RVA: 0x00020B9B File Offset: 0x0001ED9B
		internal FaultReason(string text, CultureInfo cultureInfo)
		{
			this.Init(new FaultReasonText(text, cultureInfo));
		}

		// Token: 0x060007DA RID: 2010 RVA: 0x00020BB0 File Offset: 0x0001EDB0
		[__DynamicallyInvokable]
		public FaultReason(IEnumerable<FaultReasonText> translations)
		{
			if (translations == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("translations"));
			}
			int num = 0;
			foreach (FaultReasonText faultReasonText in translations)
			{
				num++;
			}
			if (num == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("AtLeastOneFaultReasonMustBeSpecified"), "translations"));
			}
			FaultReasonText[] array = new FaultReasonText[num];
			int num2 = 0;
			foreach (FaultReasonText faultReasonText2 in translations)
			{
				if (faultReasonText2 == null)
				{
					throw DiagnosticUtility.ExceptionUtility.ThrowHelperArgument("translations", SR.GetString("NoNullTranslations"));
				}
				array[num2++] = faultReasonText2;
			}
			this.Init(array);
		}

		// Token: 0x060007DB RID: 2011 RVA: 0x00020CA8 File Offset: 0x0001EEA8
		private void Init(FaultReasonText translation)
		{
			this.Init(new FaultReasonText[]
			{
				translation
			});
		}

		// Token: 0x060007DC RID: 2012 RVA: 0x00020CBA File Offset: 0x0001EEBA
		private void Init(FaultReasonText[] translations)
		{
			this.translations = new SynchronizedReadOnlyCollection<FaultReasonText>(new object(), Array.AsReadOnly<FaultReasonText>(translations));
		}

		// Token: 0x060007DD RID: 2013 RVA: 0x00020CD2 File Offset: 0x0001EED2
		[__DynamicallyInvokable]
		public FaultReasonText GetMatchingTranslation()
		{
			return this.GetMatchingTranslation(CultureInfo.CurrentCulture);
		}

		// Token: 0x060007DE RID: 2014 RVA: 0x00020CE0 File Offset: 0x0001EEE0
		[__DynamicallyInvokable]
		public FaultReasonText GetMatchingTranslation(CultureInfo cultureInfo)
		{
			if (cultureInfo == null)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentNullException("cultureInfo"));
			}
			if (this.translations.Count == 1)
			{
				return this.translations[0];
			}
			for (int i = 0; i < this.translations.Count; i++)
			{
				if (this.translations[i].Matches(cultureInfo))
				{
					return this.translations[i];
				}
			}
			if (this.translations.Count == 0)
			{
				throw DiagnosticUtility.ExceptionUtility.ThrowHelperError(new ArgumentException(SR.GetString("NoMatchingTranslationFoundForFaultText")));
			}
			string text = cultureInfo.Name;
			int j;
			for (;;)
			{
				int num = text.LastIndexOf('-');
				if (num == -1)
				{
					goto IL_EC;
				}
				text = text.Substring(0, num);
				for (j = 0; j < this.translations.Count; j++)
				{
					if (this.translations[j].XmlLang == text)
					{
						goto Block_7;
					}
				}
			}
			Block_7:
			return this.translations[j];
			IL_EC:
			return this.translations[0];
		}

		// Token: 0x170001FF RID: 511
		// (get) Token: 0x060007DF RID: 2015 RVA: 0x00020DE5 File Offset: 0x0001EFE5
		public SynchronizedReadOnlyCollection<FaultReasonText> Translations
		{
			get
			{
				return this.translations;
			}
		}

		// Token: 0x060007E0 RID: 2016 RVA: 0x00020DED File Offset: 0x0001EFED
		[__DynamicallyInvokable]
		public override string ToString()
		{
			if (this.translations.Count == 0)
			{
				return string.Empty;
			}
			return this.GetMatchingTranslation().Text;
		}

		// Token: 0x04000AF4 RID: 2804
		private SynchronizedReadOnlyCollection<FaultReasonText> translations;
	}
}

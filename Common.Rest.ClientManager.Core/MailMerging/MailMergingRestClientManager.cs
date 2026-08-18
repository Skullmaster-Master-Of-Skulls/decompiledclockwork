using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.MailMerging;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.MailMergeEntities.Output;
using TechnoPro.Common.Unity.IoC;
using TechnoPro.Common.Web.Security.Proxy;

namespace TechnoPro.Common.Rest.ClientManager.Core.MailMerging
{
	// Token: 0x02000031 RID: 49
	public class MailMergingRestClientManager : BearerTokenRestProxy<IMailMergingClientManager>, IMailMergingClientManager, IWebService
	{
		// Token: 0x060001C6 RID: 454 RVA: 0x0000676B File Offset: 0x0000496B
		public MailMergingRestClientManager(string serviceAddress, string token = null) : base(serviceAddress, token)
		{
		}

		// Token: 0x060001C7 RID: 455 RVA: 0x00006775 File Offset: 0x00004975
		public MailMergingRestClientManager(string serviceAddress, string serviceAddressSuffix, string token = null) : base(serviceAddress, serviceAddressSuffix, token)
		{
		}

		// Token: 0x060001C8 RID: 456 RVA: 0x00006780 File Offset: 0x00004980
		public IList<string> MailMergeText(MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary, string Template, eMailMergeDocumentOutputFormat OutputFormat)
		{
			MailMergeTextReq mailMergeTextReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeTextReq>();
			mailMergeTextReq.ContextWithCustomDictionary = ContextWithCustomDictionary;
			mailMergeTextReq.Template = Template;
			mailMergeTextReq.MailMergeDocumentOutputFormat = OutputFormat;
			BaseReportMessageReq baseReportMessageReq = mailMergeTextReq;
			ApplicationContext applicationContext = mailMergeTextReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return base.Post<MailMergeTextReq, IList<string>>(mailMergeTextReq, "mailmerging/text");
		}

		// Token: 0x060001C9 RID: 457 RVA: 0x000067D1 File Offset: 0x000049D1
		public string GetMailMergeCodeDefinitionsForDisplay()
		{
			return base.Get<string>("mailmerging/codedefinitionsfordisplay", true);
		}

		// Token: 0x060001CA RID: 458 RVA: 0x000067E0 File Offset: 0x000049E0
		public IList<MailMergeCodeDTO> LookupCodeValues(MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary, IList<string> CodesNoTags)
		{
			LookupCodeValuesReq lookupCodeValuesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LookupCodeValuesReq>();
			lookupCodeValuesReq.ContextWithCustomDictionary = ContextWithCustomDictionary;
			lookupCodeValuesReq.CodesNoTags = CodesNoTags;
			BaseReportMessageReq baseReportMessageReq = lookupCodeValuesReq;
			ApplicationContext applicationContext = lookupCodeValuesReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return base.Post<LookupCodeValuesReq, IList<MailMergeCodeDTO>>(lookupCodeValuesReq, "mailmerge/lookupcodevalues");
		}

		// Token: 0x060001CB RID: 459 RVA: 0x0000682C File Offset: 0x00004A2C
		public IList<string> TestAllMailMergeCodes(MailMergeContextDTO StartingContext, string TemplateHeaderText, IList<string> CustomMailMergeCodes)
		{
			TestAllMailMergeCodesReq testAllMailMergeCodesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<TestAllMailMergeCodesReq>();
			testAllMailMergeCodesReq.StartingContext = StartingContext;
			testAllMailMergeCodesReq.TemplateHeaderText = TemplateHeaderText;
			testAllMailMergeCodesReq.CustomMailMergeCodes = CustomMailMergeCodes;
			BaseReportMessageReq baseReportMessageReq = testAllMailMergeCodesReq;
			ApplicationContext applicationContext = testAllMailMergeCodesReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return base.Post<TestAllMailMergeCodesReq, IList<string>>(testAllMailMergeCodesReq, "mailmerge/testallmailmergecodes");
		}

		// Token: 0x060001CC RID: 460 RVA: 0x00006880 File Offset: 0x00004A80
		public IList<string> TestAllMailMergeCodes(string StartingContextString, string TemplateHeaderText, IList<string> CustomMailMergeCodes)
		{
			TestAllMailMergeCodesReq testAllMailMergeCodesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<TestAllMailMergeCodesReq>();
			testAllMailMergeCodesReq.StartingContextString = StartingContextString;
			testAllMailMergeCodesReq.TemplateHeaderText = TemplateHeaderText;
			testAllMailMergeCodesReq.CustomMailMergeCodes = CustomMailMergeCodes;
			BaseReportMessageReq baseReportMessageReq = testAllMailMergeCodesReq;
			ApplicationContext applicationContext = testAllMailMergeCodesReq.ApplicationContext;
			baseReportMessageReq.BinPath = ((applicationContext != null) ? applicationContext.ExecutingPath : null);
			return base.Post<TestAllMailMergeCodesReq, IList<string>>(testAllMailMergeCodesReq, "mailmerge/testallmailmergecodes");
		}
	}
}

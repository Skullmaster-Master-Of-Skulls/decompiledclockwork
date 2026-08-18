using System;
using System.Collections.Generic;
using TechnoPro.ClockWorkServer.Client.ServiceFactory;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.Common.ClientManager.ICore;
using TechnoPro.Common.ClientManager.ICore.MailMerging;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.MailMergeEntities.Output;
using TechnoPro.Common.Unity.IoC;

namespace TechnoPro.Common.ClientManager.Core.MailMerging
{
	// Token: 0x0200003A RID: 58
	public class MailMergingClientManager : IMailMergingClientManager, IWebService
	{
		// Token: 0x06000210 RID: 528 RVA: 0x00009C70 File Offset: 0x00007E70
		public IList<string> MailMergeText(MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary, string Template, eMailMergeDocumentOutputFormat OutputFormat)
		{
			MailMergeTextReq mailMergeTextReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<MailMergeTextReq>();
			mailMergeTextReq.ContextWithCustomDictionary = ContextWithCustomDictionary;
			mailMergeTextReq.Template = Template;
			mailMergeTextReq.MailMergeDocumentOutputFormat = OutputFormat;
			mailMergeTextReq.BinPath = ((mailMergeTextReq.ApplicationContext != null) ? mailMergeTextReq.ApplicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IMailMerging>().MailMergeText(mailMergeTextReq).MergedTexts;
		}

		// Token: 0x06000211 RID: 529 RVA: 0x00009CD4 File Offset: 0x00007ED4
		public string GetMailMergeCodeDefinitionsForDisplay()
		{
			GetMailMergeCodeDefinitionsForDisplayReq getMailMergeCodeDefinitionsForDisplayReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<GetMailMergeCodeDefinitionsForDisplayReq>();
			getMailMergeCodeDefinitionsForDisplayReq.BinPath = ((getMailMergeCodeDefinitionsForDisplayReq.ApplicationContext != null) ? getMailMergeCodeDefinitionsForDisplayReq.ApplicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IMailMerging>().GetMailMergeCodeDefinitionsForDisplay(getMailMergeCodeDefinitionsForDisplayReq).DisplayString;
		}

		// Token: 0x06000212 RID: 530 RVA: 0x00009D20 File Offset: 0x00007F20
		public IList<MailMergeCodeDTO> LookupCodeValues(MailMergeContextWithCustomDictionaryDTO ContextWithCustomDictionary, IList<string> CodesNoTags)
		{
			LookupCodeValuesReq lookupCodeValuesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<LookupCodeValuesReq>();
			lookupCodeValuesReq.ContextWithCustomDictionary = ContextWithCustomDictionary;
			lookupCodeValuesReq.CodesNoTags = CodesNoTags;
			lookupCodeValuesReq.BinPath = ((lookupCodeValuesReq.ApplicationContext != null) ? lookupCodeValuesReq.ApplicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IMailMerging>().LookupCodeValues(lookupCodeValuesReq).Codes;
		}

		// Token: 0x06000213 RID: 531 RVA: 0x00009D7C File Offset: 0x00007F7C
		public IList<string> TestAllMailMergeCodes(MailMergeContextDTO StartingContext, string TemplateHeaderText, IList<string> CustomMailMergeCodes)
		{
			TestAllMailMergeCodesReq testAllMailMergeCodesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<TestAllMailMergeCodesReq>();
			testAllMailMergeCodesReq.StartingContext = StartingContext;
			testAllMailMergeCodesReq.TemplateHeaderText = TemplateHeaderText;
			testAllMailMergeCodesReq.CustomMailMergeCodes = CustomMailMergeCodes;
			testAllMailMergeCodesReq.BinPath = ((testAllMailMergeCodesReq.ApplicationContext != null) ? testAllMailMergeCodesReq.ApplicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IMailMerging>().TestAllMailMergeCodes(testAllMailMergeCodesReq).Text;
		}

		// Token: 0x06000214 RID: 532 RVA: 0x00009DE0 File Offset: 0x00007FE0
		public IList<string> TestAllMailMergeCodes(string StartingContextString, string TemplateHeaderText, IList<string> CustomMailMergeCodes)
		{
			TestAllMailMergeCodesReq testAllMailMergeCodesReq = ObjectFactory.Resolve<IRequestBuilderClientManager>().CreateRequest<TestAllMailMergeCodesReq>();
			testAllMailMergeCodesReq.StartingContextString = StartingContextString;
			testAllMailMergeCodesReq.TemplateHeaderText = TemplateHeaderText;
			testAllMailMergeCodesReq.CustomMailMergeCodes = CustomMailMergeCodes;
			testAllMailMergeCodesReq.BinPath = ((testAllMailMergeCodesReq.ApplicationContext != null) ? testAllMailMergeCodesReq.ApplicationContext.ExecutingPath : null);
			return ClientServiceFactory.GetClientInstance<IMailMerging>().TestAllMailMergeCodes(testAllMailMergeCodesReq).Text;
		}
	}
}

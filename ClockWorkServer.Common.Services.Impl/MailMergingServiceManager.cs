using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using TechnoPro.ClockWorkServer.Common.Services.Impl.Adapters;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.Common.Core.MailMerging;
using TechnoPro.Common.Core.Mappers.MailMergeEntities;
using TechnoPro.Common.ICore.MailMerging;
using TechnoPro.Common.Public;
using TechnoPro.Common.Public.Entities.MailMergeEntities;

namespace TechnoPro.ClockWorkServer.Common.Services.Impl
{
	// Token: 0x0200006A RID: 106
	public class MailMergingServiceManager : IMailMerging, IService
	{
		// Token: 0x060003E8 RID: 1000 RVA: 0x00012A4C File Offset: 0x00010C4C
		public LookupCodeValuesResp LookupCodeValues(LookupCodeValuesReq Request)
		{
			IMailMergingManager mailMergingManager = new MailMergingManager(Request.GetOperationContext());
			IEnumerable<string> source = from g in Request.CodesNoTags
			select (g.StartsWith("#<") && g.EndsWith(">#")) ? g : ("#<" + g + ">#");
			IList<MailMergeCode> codes = mailMergingManager.ExtractCodes(string.Join(Environment.NewLine, source.ToArray<string>()));
			IList<MailMergeCode> list = mailMergingManager.LookupCodeValues(Request.ContextWithCustomDictionary.ToDomainObject(), codes);
			LookupCodeValuesResp lookupCodeValuesResp = new LookupCodeValuesResp();
			IList<MailMergeCodeDTO> codes2;
			if (list == null)
			{
				codes2 = null;
			}
			else
			{
				codes2 = list.ToList<MailMergeCode>().ConvertAll<MailMergeCodeDTO>((MailMergeCode f) => f.ToDTO());
			}
			lookupCodeValuesResp.Codes = codes2;
			return lookupCodeValuesResp;
		}

		// Token: 0x060003E9 RID: 1001 RVA: 0x00012B00 File Offset: 0x00010D00
		public OutputTextResp OutputText(OutputTextReq Request)
		{
			IMailMergingManager mailMergingManager = new MailMergingManager(Request.GetOperationContext());
			List<MailMergeCodeDTO> list = Request.Codes.ToList<MailMergeCodeDTO>();
			IList<string> mergedTexts = mailMergingManager.OutputText(list.ConvertAll<MailMergeCode>((MailMergeCodeDTO f) => f.ToDomainObject()), Request.Template, Request.OutputFormat);
			return new OutputTextResp
			{
				MergedTexts = mergedTexts
			};
		}

		// Token: 0x060003EA RID: 1002 RVA: 0x00012B70 File Offset: 0x00010D70
		public ExtractCodesResp ExtractCodes(ExtractCodesReq Request)
		{
			IMailMergingManager mailMergingManager = new MailMergingManager(Request.GetOperationContext());
			IList<MailMergeCode> source = mailMergingManager.ExtractCodes(Request.Template);
			ExtractCodesResp extractCodesResp = new ExtractCodesResp();
			extractCodesResp.Codes = source.ToList<MailMergeCode>().ConvertAll<MailMergeCodeDTO>((MailMergeCode f) => f.ToDTO());
			return extractCodesResp;
		}

		// Token: 0x060003EB RID: 1003 RVA: 0x00012BD4 File Offset: 0x00010DD4
		public MailMergeTextResp MailMergeText(MailMergeTextReq Request)
		{
			IMailMergingManager mailMergingManager = new MailMergingManager(Request.GetOperationContext());
			IList<string> mergedTexts = mailMergingManager.MailMerge(Request.ContextWithCustomDictionary.ToDomainObject(), Request.Template, Request.MailMergeDocumentOutputFormat);
			return new MailMergeTextResp
			{
				MergedTexts = mergedTexts
			};
		}

		// Token: 0x060003EC RID: 1004 RVA: 0x00012C20 File Offset: 0x00010E20
		public GetMailMergeCodeDefinitionsForDisplayResp GetMailMergeCodeDefinitionsForDisplay(GetMailMergeCodeDefinitionsForDisplayReq Request)
		{
			IMailMergingManager mailMergingManager = new MailMergingManager(Request.GetOperationContext());
			string mailMergeCodeDefinitionsForDisplay = mailMergingManager.GetMailMergeCodeDefinitionsForDisplay();
			return new GetMailMergeCodeDefinitionsForDisplayResp
			{
				DisplayString = mailMergeCodeDefinitionsForDisplay
			};
		}

		// Token: 0x060003ED RID: 1005 RVA: 0x00012C54 File Offset: 0x00010E54
		public TestAllMailMergeCodesResp TestAllMailMergeCodes(TestAllMailMergeCodesReq Request)
		{
			IMailMergingManager mailMergingManager = new MailMergingManager(Request.GetOperationContext());
			IList<MailMergeCode> list;
			IList<string> text = (Request.StartingContext == null) ? mailMergingManager.TestAllMailMergeCodes(Request.StartingContextString, Request.TemplateHeaderText, Request.CustomMailMergeCodes, out list) : mailMergingManager.TestAllMailMergeCodes((Request.StartingContext == null) ? null : Request.StartingContext.ToDomainObject(), Request.TemplateHeaderText, Request.CustomMailMergeCodes, out list);
			bool flag = list != null;
			if (flag)
			{
				List<MailMergeCodeDTO> items = list.ToList<MailMergeCode>().ConvertAll<MailMergeCodeDTO>((MailMergeCode g) => g.ToDTO());
				IList<byte[]> bytes = SerializerGeneric.Serialize<MailMergeCodeDTO>(items);
				IList<MailMergeCodeDTO> list2 = SerializerGeneric.Deserialize<MailMergeCodeDTO>(bytes);
				bool flag2 = list2 == null || list2.Count != list.Count;
				if (flag2)
				{
					throw new SerializationException("Common.Services.Impl.MailMergingServiceManager:TestAllMailMergeCodes:Failed serialization test for codesWithValues because counts are different");
				}
			}
			return new TestAllMailMergeCodesResp
			{
				Text = text
			};
		}
	}
}

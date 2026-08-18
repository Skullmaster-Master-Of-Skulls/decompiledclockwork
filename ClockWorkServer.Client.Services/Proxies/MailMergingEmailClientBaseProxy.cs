using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000E9 RID: 233
	internal class MailMergingEmailClientBaseProxy : ClientBase<IMailMergingEmail>, IMailMergingEmail, IService
	{
		// Token: 0x0600090B RID: 2315 RVA: 0x00017638 File Offset: 0x00015838
		public MailMergingEmailClientBaseProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x0600090C RID: 2316 RVA: 0x00017643 File Offset: 0x00015843
		public MailMergingEmailClientBaseProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x0600090D RID: 2317 RVA: 0x00017650 File Offset: 0x00015850
		public MailMergeEmailFromTemplateResp MailMergeFromTemplate(MailMergeEmailFromTemplateReq Request)
		{
			return base.Channel.MailMergeFromTemplate(Request);
		}

		// Token: 0x0600090E RID: 2318 RVA: 0x00017670 File Offset: 0x00015870
		public MailMergeEmailFromTemplateInWebSettingsResp MailMergeFromTemplateInWebSettings(MailMergeEmailFromTemplateInWebSettingsReq Request)
		{
			return base.Channel.MailMergeFromTemplateInWebSettings(Request);
		}

		// Token: 0x0600090F RID: 2319 RVA: 0x00017690 File Offset: 0x00015890
		public MailMergeEmailFromTemplateXmlResp MailMergeFromTemplateXml(MailMergeEmailFromTemplateXmlReq Request)
		{
			return base.Channel.MailMergeFromTemplateXml(Request);
		}

		// Token: 0x06000910 RID: 2320 RVA: 0x000176B0 File Offset: 0x000158B0
		public MailMergeAccommodationLetterCoursesEmailResp MailMergeAccommodationLetterCoursesEmail(MailMergeAccommodationLetterCoursesEmailReq Request)
		{
			return base.Channel.MailMergeAccommodationLetterCoursesEmail(Request);
		}

		// Token: 0x06000911 RID: 2321 RVA: 0x000176D0 File Offset: 0x000158D0
		public MailMergeAccommodationSingleLetterEmailResp MailMergeAccommodationSingleLetterEmail(MailMergeAccommodationSingleLetterEmailReq Request)
		{
			return base.Channel.MailMergeAccommodationSingleLetterEmail(Request);
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x000176F0 File Offset: 0x000158F0
		public MailMergeMultipleEmailsFromTemplateIdResp MailMergeMultipleEmailsFromTemplateId(MailMergeMultipleEmailsFromTemplateIdReq Request)
		{
			return base.Channel.MailMergeMultipleEmailsFromTemplateId(Request);
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00017710 File Offset: 0x00015910
		public MailMergeMultipleEmailsFromTemplateInWebSettingsResp MailMergeMultipleEmailsFromTemplateInWebSettings(MailMergeMultipleEmailsFromTemplateInWebSettingsReq Request)
		{
			return base.Channel.MailMergeMultipleEmailsFromTemplateInWebSettings(Request);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00017730 File Offset: 0x00015930
		public MailMergeMultipleEmailsFromTemplateXmlResp MailMergeMultipleEmailsFromTemplateXml(MailMergeMultipleEmailsFromTemplateXmlReq Request)
		{
			return base.Channel.MailMergeMultipleEmailsFromTemplateXml(Request);
		}
	}
}

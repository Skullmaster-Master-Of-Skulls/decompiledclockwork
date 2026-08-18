using System;
using System.ServiceModel;
using System.ServiceModel.Channels;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.Common.Public;

namespace TechnoPro.ClockWorkServer.Client.Services.Proxies
{
	// Token: 0x020000E8 RID: 232
	public class MailMergingEmailReusableClientProxy : WCFTokenBasedReusableClientProxy<IMailMergingEmail>, IMailMergingEmail, IService
	{
		// Token: 0x06000901 RID: 2305 RVA: 0x0001745E File Offset: 0x0001565E
		public MailMergingEmailReusableClientProxy(string endpoint) : base(endpoint)
		{
		}

		// Token: 0x06000902 RID: 2306 RVA: 0x00017469 File Offset: 0x00015669
		public MailMergingEmailReusableClientProxy(Binding binding, EndpointAddress endpointAddress) : base(binding, endpointAddress)
		{
		}

		// Token: 0x06000903 RID: 2307 RVA: 0x00017478 File Offset: 0x00015678
		public MailMergeEmailFromTemplateResp MailMergeFromTemplate(MailMergeEmailFromTemplateReq Request)
		{
			return this.WrapServiceMethod<MailMergeEmailFromTemplateResp>(() => this.Proxy.MailMergeFromTemplate(Request));
		}

		// Token: 0x06000904 RID: 2308 RVA: 0x000174B0 File Offset: 0x000156B0
		public MailMergeEmailFromTemplateInWebSettingsResp MailMergeFromTemplateInWebSettings(MailMergeEmailFromTemplateInWebSettingsReq Request)
		{
			return this.WrapServiceMethod<MailMergeEmailFromTemplateInWebSettingsResp>(() => this.Proxy.MailMergeFromTemplateInWebSettings(Request));
		}

		// Token: 0x06000905 RID: 2309 RVA: 0x000174E8 File Offset: 0x000156E8
		public MailMergeEmailFromTemplateXmlResp MailMergeFromTemplateXml(MailMergeEmailFromTemplateXmlReq Request)
		{
			return this.WrapServiceMethod<MailMergeEmailFromTemplateXmlResp>(() => this.Proxy.MailMergeFromTemplateXml(Request));
		}

		// Token: 0x06000906 RID: 2310 RVA: 0x00017520 File Offset: 0x00015720
		public MailMergeAccommodationLetterCoursesEmailResp MailMergeAccommodationLetterCoursesEmail(MailMergeAccommodationLetterCoursesEmailReq Request)
		{
			return this.WrapServiceMethod<MailMergeAccommodationLetterCoursesEmailResp>(() => this.Proxy.MailMergeAccommodationLetterCoursesEmail(Request));
		}

		// Token: 0x06000907 RID: 2311 RVA: 0x00017558 File Offset: 0x00015758
		public MailMergeAccommodationSingleLetterEmailResp MailMergeAccommodationSingleLetterEmail(MailMergeAccommodationSingleLetterEmailReq Request)
		{
			return this.WrapServiceMethod<MailMergeAccommodationSingleLetterEmailResp>(() => this.Proxy.MailMergeAccommodationSingleLetterEmail(Request));
		}

		// Token: 0x06000908 RID: 2312 RVA: 0x00017590 File Offset: 0x00015790
		public MailMergeMultipleEmailsFromTemplateIdResp MailMergeMultipleEmailsFromTemplateId(MailMergeMultipleEmailsFromTemplateIdReq Request)
		{
			return this.WrapServiceMethod<MailMergeMultipleEmailsFromTemplateIdResp>(() => this.Proxy.MailMergeMultipleEmailsFromTemplateId(Request));
		}

		// Token: 0x06000909 RID: 2313 RVA: 0x000175C8 File Offset: 0x000157C8
		public MailMergeMultipleEmailsFromTemplateInWebSettingsResp MailMergeMultipleEmailsFromTemplateInWebSettings(MailMergeMultipleEmailsFromTemplateInWebSettingsReq Request)
		{
			return this.WrapServiceMethod<MailMergeMultipleEmailsFromTemplateInWebSettingsResp>(() => this.Proxy.MailMergeMultipleEmailsFromTemplateInWebSettings(Request));
		}

		// Token: 0x0600090A RID: 2314 RVA: 0x00017600 File Offset: 0x00015800
		public MailMergeMultipleEmailsFromTemplateXmlResp MailMergeMultipleEmailsFromTemplateXml(MailMergeMultipleEmailsFromTemplateXmlReq Request)
		{
			return this.WrapServiceMethod<MailMergeMultipleEmailsFromTemplateXmlResp>(() => this.Proxy.MailMergeMultipleEmailsFromTemplateXml(Request));
		}
	}
}

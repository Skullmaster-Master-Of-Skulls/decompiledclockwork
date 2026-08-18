using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan
{
	// Token: 0x020001B4 RID: 436
	[DataContract(Namespace = "http://tpro.ca")]
	[Serializable]
	public class TPMailMessageDTO
	{
		// Token: 0x060009E5 RID: 2533 RVA: 0x00004714 File Offset: 0x00002914
		public TPMailMessageDTO()
		{
			this.UniqueId = Guid.NewGuid().ToString();
		}

		// Token: 0x170001E3 RID: 483
		// (get) Token: 0x060009E6 RID: 2534 RVA: 0x00004743 File Offset: 0x00002943
		// (set) Token: 0x060009E7 RID: 2535 RVA: 0x0000474B File Offset: 0x0000294B
		[DataMember]
		public List<TPMailAddressDTO> To { get; set; }

		// Token: 0x170001E4 RID: 484
		// (get) Token: 0x060009E8 RID: 2536 RVA: 0x00004754 File Offset: 0x00002954
		// (set) Token: 0x060009E9 RID: 2537 RVA: 0x0000475C File Offset: 0x0000295C
		[DataMember]
		public List<TPMailAddressDTO> Cc { get; set; }

		// Token: 0x170001E5 RID: 485
		// (get) Token: 0x060009EA RID: 2538 RVA: 0x00004765 File Offset: 0x00002965
		// (set) Token: 0x060009EB RID: 2539 RVA: 0x0000476D File Offset: 0x0000296D
		[DataMember]
		public List<TPMailAddressDTO> Bcc { get; set; }

		// Token: 0x170001E6 RID: 486
		// (get) Token: 0x060009EC RID: 2540 RVA: 0x00004776 File Offset: 0x00002976
		// (set) Token: 0x060009ED RID: 2541 RVA: 0x0000477E File Offset: 0x0000297E
		[DataMember]
		public TPMailAddressDTO From { get; set; }

		// Token: 0x170001E7 RID: 487
		// (get) Token: 0x060009EE RID: 2542 RVA: 0x00004787 File Offset: 0x00002987
		// (set) Token: 0x060009EF RID: 2543 RVA: 0x0000478F File Offset: 0x0000298F
		[DataMember]
		public string Subject { get; set; }

		// Token: 0x170001E8 RID: 488
		// (get) Token: 0x060009F0 RID: 2544 RVA: 0x00004798 File Offset: 0x00002998
		// (set) Token: 0x060009F1 RID: 2545 RVA: 0x000047A0 File Offset: 0x000029A0
		[DataMember]
		public string Body { get; set; }

		// Token: 0x170001E9 RID: 489
		// (get) Token: 0x060009F2 RID: 2546 RVA: 0x000047A9 File Offset: 0x000029A9
		// (set) Token: 0x060009F3 RID: 2547 RVA: 0x000047B1 File Offset: 0x000029B1
		[DataMember]
		public eEmailBodyType BodyType { get; set; }

		// Token: 0x170001EA RID: 490
		// (get) Token: 0x060009F4 RID: 2548 RVA: 0x000047BA File Offset: 0x000029BA
		// (set) Token: 0x060009F5 RID: 2549 RVA: 0x000047C2 File Offset: 0x000029C2
		[DataMember]
		[Obsolete("Only Body and BodyType will be used, unless BodyType is missing then will use Body or BodyHtml.  Don't use BodyHtml anymore.")]
		public string BodyHtml { get; set; }

		// Token: 0x170001EB RID: 491
		// (get) Token: 0x060009F6 RID: 2550 RVA: 0x000047CB File Offset: 0x000029CB
		// (set) Token: 0x060009F7 RID: 2551 RVA: 0x000047D3 File Offset: 0x000029D3
		[DataMember]
		public List<TPMailAttachmentDTO> Attachments { get; set; }

		// Token: 0x170001EC RID: 492
		// (get) Token: 0x060009F8 RID: 2552 RVA: 0x000047DC File Offset: 0x000029DC
		// (set) Token: 0x060009F9 RID: 2553 RVA: 0x000047E4 File Offset: 0x000029E4
		[DataMember]
		public eTPMessageDeliveryMethodDTO DeliveryMethod { get; set; }

		// Token: 0x170001ED RID: 493
		// (get) Token: 0x060009FA RID: 2554 RVA: 0x000047ED File Offset: 0x000029ED
		// (set) Token: 0x060009FB RID: 2555 RVA: 0x000047F5 File Offset: 0x000029F5
		[DataMember]
		public eTPMessagePriorityDTO Priority { get; set; }

		// Token: 0x170001EE RID: 494
		// (get) Token: 0x060009FC RID: 2556 RVA: 0x000047FE File Offset: 0x000029FE
		// (set) Token: 0x060009FD RID: 2557 RVA: 0x00004806 File Offset: 0x00002A06
		[DataMember]
		public bool IsActive { get; set; }

		// Token: 0x170001EF RID: 495
		// (get) Token: 0x060009FE RID: 2558 RVA: 0x0000480F File Offset: 0x00002A0F
		// (set) Token: 0x060009FF RID: 2559 RVA: 0x00004817 File Offset: 0x00002A17
		[DataMember]
		public string ErrorMessage { get; set; }

		// Token: 0x170001F0 RID: 496
		// (get) Token: 0x06000A00 RID: 2560 RVA: 0x00004820 File Offset: 0x00002A20
		// (set) Token: 0x06000A01 RID: 2561 RVA: 0x00004828 File Offset: 0x00002A28
		[DataMember]
		public string ErrorMessageHtml { get; set; }

		// Token: 0x170001F1 RID: 497
		// (get) Token: 0x06000A02 RID: 2562 RVA: 0x00004831 File Offset: 0x00002A31
		// (set) Token: 0x06000A03 RID: 2563 RVA: 0x00004839 File Offset: 0x00002A39
		[DataMember]
		public bool WasSent { get; set; }

		// Token: 0x170001F2 RID: 498
		// (get) Token: 0x06000A04 RID: 2564 RVA: 0x00004842 File Offset: 0x00002A42
		// (set) Token: 0x06000A05 RID: 2565 RVA: 0x0000484A File Offset: 0x00002A4A
		[DataMember]
		public string UniqueId { get; set; }

		// Token: 0x06000A06 RID: 2566 RVA: 0x00004854 File Offset: 0x00002A54
		public string GetPlainTextBody()
		{
			eEmailBodyType bodyType = this.BodyType;
			eEmailBodyType eEmailBodyType = bodyType;
			string result;
			if (eEmailBodyType != eEmailBodyType.PlainText)
			{
				if (eEmailBodyType != eEmailBodyType.Html)
				{
					result = this.Body;
				}
				else
				{
					result = null;
				}
			}
			else
			{
				result = this.Body;
			}
			return result;
		}

		// Token: 0x06000A07 RID: 2567 RVA: 0x00004890 File Offset: 0x00002A90
		public string GetHtmlBody()
		{
			eEmailBodyType bodyType = this.BodyType;
			eEmailBodyType eEmailBodyType = bodyType;
			string result;
			if (eEmailBodyType != eEmailBodyType.PlainText)
			{
				if (eEmailBodyType != eEmailBodyType.Html)
				{
					result = (string.IsNullOrEmpty(this.BodyHtml) ? this.Body : this.BodyHtml);
				}
				else
				{
					result = this.Body;
				}
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}

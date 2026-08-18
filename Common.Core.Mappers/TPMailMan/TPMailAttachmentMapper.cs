using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.Core.Mappers.TPMailMan
{
	// Token: 0x02000030 RID: 48
	public static class TPMailAttachmentMapper
	{
		// Token: 0x060000CA RID: 202 RVA: 0x0000620C File Offset: 0x0000440C
		static TPMailAttachmentMapper()
		{
			Mapper.CreateMap<TPMailAttachmentDTO, TPMailAttachment>().ForMember((TPMailAttachment pb) => pb.Id, delegate(IMemberConfigurationExpression<TPMailAttachmentDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<TPMailAttachment, TPMailAttachmentDTO>();
		}

		// Token: 0x060000CB RID: 203 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060000CC RID: 204 RVA: 0x0000627C File Offset: 0x0000447C
		public static TPMailAttachment ToDomainObject(this TPMailAttachmentDTO tPMailAttachmentDTO)
		{
			return Mapper.Map<TPMailAttachmentDTO, TPMailAttachment>(tPMailAttachmentDTO);
		}

		// Token: 0x060000CD RID: 205 RVA: 0x00006294 File Offset: 0x00004494
		public static TPMailAttachmentDTO ToDTO(this TPMailAttachment tPMailAttachment)
		{
			return Mapper.Map<TPMailAttachment, TPMailAttachmentDTO>(tPMailAttachment);
		}
	}
}

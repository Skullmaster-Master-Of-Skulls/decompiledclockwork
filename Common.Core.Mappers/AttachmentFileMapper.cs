using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.Public.Entities.DropBox;

namespace TechnoPro.Common.Core.Mappers
{
	// Token: 0x02000004 RID: 4
	public static class AttachmentFileMapper
	{
		// Token: 0x0600000D RID: 13 RVA: 0x000025FC File Offset: 0x000007FC
		static AttachmentFileMapper()
		{
			AttachmentInfoMapper.CreateMap();
			Mapper.CreateMap<AttachmentFile, DropBox_Attachment>().ForMember((DropBox_Attachment pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<AttachmentFile> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<DropBox_Attachment, AttachmentFile>();
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600000F RID: 15 RVA: 0x00002680 File Offset: 0x00000880
		public static AttachmentFile ToDTO(this DropBox_Attachment att)
		{
			return Mapper.Map<DropBox_Attachment, AttachmentFile>(att);
		}

		// Token: 0x06000010 RID: 16 RVA: 0x00002698 File Offset: 0x00000898
		public static DropBox_Attachment ToDomaninObject(this AttachmentFile att)
		{
			return Mapper.Map<AttachmentFile, DropBox_Attachment>(att);
		}
	}
}

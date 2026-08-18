using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.Public.Entities.DropBox;

namespace TechnoPro.Common.Core.Mappers
{
	// Token: 0x02000006 RID: 6
	public static class AttachmentInfoMapper
	{
		// Token: 0x06000017 RID: 23 RVA: 0x000027D8 File Offset: 0x000009D8
		static AttachmentInfoMapper()
		{
			DropBoxUserMapper.CreateMap();
			Mapper.CreateMap<DropBox_AttachmentInfo, AttachmentInfo>().ForMember((AttachmentInfo att) => att.From, delegate(IMemberConfigurationExpression<DropBox_AttachmentInfo> m)
			{
				m.MapFrom<DropBox_User>((DropBox_AttachmentInfo dbAtt) => dbAtt.From);
			});
			Mapper.CreateMap<AttachmentInfo, DropBox_AttachmentInfo>().ForMember((DropBox_AttachmentInfo attDTO) => attDTO.From, delegate(IMemberConfigurationExpression<AttachmentInfo> m)
			{
				m.MapFrom<IM_User>((AttachmentInfo att) => att.From);
			});
		}

		// Token: 0x06000018 RID: 24 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000019 RID: 25 RVA: 0x00002894 File Offset: 0x00000A94
		public static AttachmentInfo ToDTO(this DropBox_AttachmentInfo attInfo)
		{
			return Mapper.Map<DropBox_AttachmentInfo, AttachmentInfo>(attInfo);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000028AC File Offset: 0x00000AAC
		public static IList<AttachmentInfo> ToDTO(this IList<DropBox_AttachmentInfo> list)
		{
			IList<AttachmentInfo> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<AttachmentInfo>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000028F0 File Offset: 0x00000AF0
		public static DropBox_AttachmentInfo ToDomainObject(this AttachmentInfo attInfo)
		{
			return Mapper.Map<AttachmentInfo, DropBox_AttachmentInfo>(attInfo);
		}
	}
}

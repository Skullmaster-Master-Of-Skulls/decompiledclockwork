using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core.Mappers.Files
{
	// Token: 0x02000110 RID: 272
	public static class FileTypeMapper
	{
		// Token: 0x060004A9 RID: 1193 RVA: 0x00016C58 File Offset: 0x00014E58
		static FileTypeMapper()
		{
			Mapper.CreateMap<FileTypeDTO, FileType>().ForMember((FileType pb) => pb.Id, delegate(IMemberConfigurationExpression<FileTypeDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<FileType, FileTypeDTO>();
		}

		// Token: 0x060004AA RID: 1194 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060004AB RID: 1195 RVA: 0x00016CC8 File Offset: 0x00014EC8
		public static FileType ToDomainObject(this FileTypeDTO fileTypeDTO)
		{
			return Mapper.Map<FileTypeDTO, FileType>(fileTypeDTO);
		}

		// Token: 0x060004AC RID: 1196 RVA: 0x00016CE0 File Offset: 0x00014EE0
		public static FileTypeDTO ToDTO(this FileType fileTypeDTO)
		{
			return Mapper.Map<FileType, FileTypeDTO>(fileTypeDTO);
		}
	}
}

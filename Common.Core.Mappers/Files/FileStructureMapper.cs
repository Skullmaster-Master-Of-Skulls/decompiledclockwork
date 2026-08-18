using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core.Mappers.Files
{
	// Token: 0x0200010F RID: 271
	public static class FileStructureMapper
	{
		// Token: 0x060004A5 RID: 1189 RVA: 0x00016BA4 File Offset: 0x00014DA4
		static FileStructureMapper()
		{
			FileTypeMapper.CreateMap();
			Mapper.CreateMap<FileStructureDTO, FileStructure>().ForMember((FileStructure pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<FileStructureDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<FileStructure, FileStructureDTO>();
		}

		// Token: 0x060004A6 RID: 1190 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060004A7 RID: 1191 RVA: 0x00016C28 File Offset: 0x00014E28
		public static FileStructure ToDomainObject(this FileStructureDTO fileStructureDTO)
		{
			return Mapper.Map<FileStructureDTO, FileStructure>(fileStructureDTO);
		}

		// Token: 0x060004A8 RID: 1192 RVA: 0x00016C40 File Offset: 0x00014E40
		public static FileStructureDTO ToDTO(this FileStructure fileStructure)
		{
			return Mapper.Map<FileStructure, FileStructureDTO>(fileStructure);
		}
	}
}

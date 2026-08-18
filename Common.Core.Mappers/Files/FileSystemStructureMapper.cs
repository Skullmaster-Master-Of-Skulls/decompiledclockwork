using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core.Mappers.Files
{
	// Token: 0x02000111 RID: 273
	public static class FileSystemStructureMapper
	{
		// Token: 0x060004AD RID: 1197 RVA: 0x00016CF8 File Offset: 0x00014EF8
		static FileSystemStructureMapper()
		{
			FileStructureMapper.CreateMap();
			Mapper.CreateMap<FileSystemStructureDTO, FileSystemStructure>().ForMember((FileSystemStructure pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<FileSystemStructureDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<FileSystemStructure, FileSystemStructureDTO>();
		}

		// Token: 0x060004AE RID: 1198 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060004AF RID: 1199 RVA: 0x00016D7C File Offset: 0x00014F7C
		public static FileSystemStructure ToDomainObject(this FileSystemStructureDTO fileStructureDTO)
		{
			return Mapper.Map<FileSystemStructureDTO, FileSystemStructure>(fileStructureDTO);
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x00016D94 File Offset: 0x00014F94
		public static FileSystemStructureDTO ToDTO(this FileSystemStructure fileStructure)
		{
			return Mapper.Map<FileSystemStructure, FileSystemStructureDTO>(fileStructure);
		}

		// Token: 0x060004B1 RID: 1201 RVA: 0x00016DAC File Offset: 0x00014FAC
		public static IList<FileSystemStructure> ToDomainObject(this IList<FileSystemStructureDTO> list)
		{
			IList<FileSystemStructure> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<FileSystemStructure>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060004B2 RID: 1202 RVA: 0x00016DF0 File Offset: 0x00014FF0
		public static IList<FileSystemStructureDTO> ToDTO(this IList<FileSystemStructure> list)
		{
			IList<FileSystemStructureDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<FileSystemStructureDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}

using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Files;
using TechnoPro.Common.Public.Entities.Files;

namespace TechnoPro.Common.Core.Mappers.Files
{
	// Token: 0x0200010A RID: 266
	public static class BinaryFileMapper
	{
		// Token: 0x0600048D RID: 1165 RVA: 0x000164B8 File Offset: 0x000146B8
		static BinaryFileMapper()
		{
			Mapper.CreateMap<BinaryFileDTO, BinaryFile>().ForMember((BinaryFile pb) => pb.Id, delegate(IMemberConfigurationExpression<BinaryFileDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<BinaryFile, BinaryFileDTO>();
		}

		// Token: 0x0600048E RID: 1166 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600048F RID: 1167 RVA: 0x00016528 File Offset: 0x00014728
		public static BinaryFile ToDomainObject(this BinaryFileDTO binaryFileDTO)
		{
			return Mapper.Map<BinaryFileDTO, BinaryFile>(binaryFileDTO);
		}

		// Token: 0x06000490 RID: 1168 RVA: 0x00016540 File Offset: 0x00014740
		public static BinaryFileDTO ToDTO(this BinaryFile binaryFile)
		{
			return Mapper.Map<BinaryFile, BinaryFileDTO>(binaryFile);
		}
	}
}

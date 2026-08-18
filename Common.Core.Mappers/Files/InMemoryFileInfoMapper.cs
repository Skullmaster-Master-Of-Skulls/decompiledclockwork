using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage;
using TechnoPro.Common.Public.Entities.FileStorage;

namespace TechnoPro.Common.Core.Mappers.Files
{
	// Token: 0x0200010E RID: 270
	public static class InMemoryFileInfoMapper
	{
		// Token: 0x060004A1 RID: 1185 RVA: 0x00016A1C File Offset: 0x00014C1C
		static InMemoryFileInfoMapper()
		{
			BasicFileInfoMapper.CreateMap();
			Mapper.CreateMap<InMemoryFileDTO, InMemoryFile>().ForMember((InMemoryFile bo) => bo.FileIdentifier, delegate(IMemberConfigurationExpression<InMemoryFileDTO> m)
			{
				m.MapFrom<FileIdentifierDTO>((InMemoryFileDTO dto) => dto.FileIdentifier);
			}).ForMember((InMemoryFile bo) => bo.FileData, delegate(IMemberConfigurationExpression<InMemoryFileDTO> m)
			{
				m.MapFrom<byte[]>((InMemoryFileDTO dto) => dto.FileData);
			});
			Mapper.CreateMap<InMemoryFile, InMemoryFileDTO>().ForMember((InMemoryFileDTO dto) => dto.FileIdentifier, delegate(IMemberConfigurationExpression<InMemoryFile> m)
			{
				m.MapFrom<FileIdentifier>((InMemoryFile bo) => bo.FileIdentifier);
			}).ForMember((InMemoryFileDTO dto) => dto.FileData, delegate(IMemberConfigurationExpression<InMemoryFile> m)
			{
				m.MapFrom<byte[]>((InMemoryFile bo) => bo.FileData);
			});
		}

		// Token: 0x060004A2 RID: 1186 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060004A3 RID: 1187 RVA: 0x00016B74 File Offset: 0x00014D74
		public static InMemoryFile ToDomainObject(this InMemoryFileDTO binaryFileDTO)
		{
			return Mapper.Map<InMemoryFileDTO, InMemoryFile>(binaryFileDTO);
		}

		// Token: 0x060004A4 RID: 1188 RVA: 0x00016B8C File Offset: 0x00014D8C
		public static InMemoryFileDTO ToDTO(this InMemoryFile binaryFile)
		{
			return Mapper.Map<InMemoryFile, InMemoryFileDTO>(binaryFile);
		}
	}
}

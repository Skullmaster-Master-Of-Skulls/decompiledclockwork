using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage;
using TechnoPro.Common.Public.Entities.FileStorage;

namespace TechnoPro.Common.Core.Mappers.Files
{
	// Token: 0x0200010B RID: 267
	public static class FileIdentifierMapper
	{
		// Token: 0x06000491 RID: 1169 RVA: 0x00016558 File Offset: 0x00014758
		static FileIdentifierMapper()
		{
			Mapper.CreateMap<FileIdentifier, FileIdentifierDTO>();
			Mapper.CreateMap<FileIdentifier, FileIdentifierMessageDTO>();
			Mapper.CreateMap<FileIdentifierDTO, FileIdentifier>();
			Mapper.CreateMap<FileIdentifierMessageDTO, FileIdentifier>();
		}

		// Token: 0x06000492 RID: 1170 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000493 RID: 1171 RVA: 0x00016574 File Offset: 0x00014774
		public static FileIdentifier ToDomaintObject(this FileIdentifierDTO dto)
		{
			return Mapper.Map<FileIdentifierDTO, FileIdentifier>(dto);
		}

		// Token: 0x06000494 RID: 1172 RVA: 0x0001658C File Offset: 0x0001478C
		public static FileIdentifier ToDomaintObject(this FileIdentifierMessageDTO dto)
		{
			return Mapper.Map<FileIdentifierMessageDTO, FileIdentifier>(dto);
		}

		// Token: 0x06000495 RID: 1173 RVA: 0x000165A4 File Offset: 0x000147A4
		public static FileIdentifierDTO ToDTO(this FileIdentifier bo)
		{
			return Mapper.Map<FileIdentifier, FileIdentifierDTO>(bo);
		}

		// Token: 0x06000496 RID: 1174 RVA: 0x000165BC File Offset: 0x000147BC
		public static FileIdentifierMessageDTO ToMessageDTO(this FileIdentifier bo)
		{
			return Mapper.Map<FileIdentifier, FileIdentifierMessageDTO>(bo);
		}
	}
}

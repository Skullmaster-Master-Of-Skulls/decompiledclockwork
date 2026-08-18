using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage;
using TechnoPro.Common.Public.Entities.FileStorage;

namespace TechnoPro.Common.Core.Mappers.Files
{
	// Token: 0x0200010C RID: 268
	public static class BasicFileInfoMapper
	{
		// Token: 0x06000497 RID: 1175 RVA: 0x000165D4 File Offset: 0x000147D4
		static BasicFileInfoMapper()
		{
			FileIdentifierMapper.CreateMap();
			Mapper.CreateMap<BasicFileInfoDTO, BasicFileInfo>().ForMember((BasicFileInfo bo) => bo.FileIdentifier, delegate(IMemberConfigurationExpression<BasicFileInfoDTO> m)
			{
				m.MapFrom<FileIdentifierDTO>((BasicFileInfoDTO dto) => dto.FileIdentifier);
			});
			Mapper.CreateMap<BasicFileInfoMessageDTO, BasicFileInfo>().ForMember((BasicFileInfo bo) => bo.FileIdentifier, delegate(IMemberConfigurationExpression<BasicFileInfoMessageDTO> m)
			{
				m.MapFrom<FileIdentifierMessageDTO>((BasicFileInfoMessageDTO dto) => dto.FileIdentifier);
			});
			Mapper.CreateMap<BasicFileInfo, BasicFileInfoDTO>().ForMember((BasicFileInfoDTO bo) => bo.FileIdentifier, delegate(IMemberConfigurationExpression<BasicFileInfo> m)
			{
				m.MapFrom<FileIdentifier>((BasicFileInfo dto) => dto.FileIdentifier);
			});
			Mapper.CreateMap<BasicFileInfo, BasicFileInfoMessageDTO>().ForMember((BasicFileInfoMessageDTO bo) => bo.FileIdentifier, delegate(IMemberConfigurationExpression<BasicFileInfo> m)
			{
				m.MapFrom<FileIdentifier>((BasicFileInfo dto) => dto.FileIdentifier);
			});
		}

		// Token: 0x06000498 RID: 1176 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000499 RID: 1177 RVA: 0x00016738 File Offset: 0x00014938
		public static BasicFileInfo ToDomainObject(this BasicFileInfoDTO dto)
		{
			return Mapper.Map<BasicFileInfoDTO, BasicFileInfo>(dto);
		}

		// Token: 0x0600049A RID: 1178 RVA: 0x00016750 File Offset: 0x00014950
		public static BasicFileInfo ToDomainObject(this BasicFileInfoMessageDTO dto)
		{
			return Mapper.Map<BasicFileInfoMessageDTO, BasicFileInfo>(dto);
		}

		// Token: 0x0600049B RID: 1179 RVA: 0x00016768 File Offset: 0x00014968
		public static BasicFileInfoDTO ToDTO(this BasicFileInfo bo)
		{
			return Mapper.Map<BasicFileInfo, BasicFileInfoDTO>(bo);
		}

		// Token: 0x0600049C RID: 1180 RVA: 0x00016780 File Offset: 0x00014980
		public static BasicFileInfoMessageDTO ToMessageDTO(this BasicFileInfo bo)
		{
			return Mapper.Map<BasicFileInfo, BasicFileInfoMessageDTO>(bo);
		}
	}
}

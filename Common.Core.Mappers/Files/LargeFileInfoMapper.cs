using System;
using System.IO;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.FileStorage;
using TechnoPro.Common.Public.Entities.FileStorage;

namespace TechnoPro.Common.Core.Mappers.Files
{
	// Token: 0x0200010D RID: 269
	public static class LargeFileInfoMapper
	{
		// Token: 0x0600049D RID: 1181 RVA: 0x00016798 File Offset: 0x00014998
		static LargeFileInfoMapper()
		{
			BasicFileInfoMapper.CreateMap();
			Mapper.CreateMap<StreamingFileDTO, StreamingFile>().ForMember((StreamingFile bo) => bo.FileIdentifier, delegate(IMemberConfigurationExpression<StreamingFileDTO> m)
			{
				m.MapFrom<FileIdentifierMessageDTO>((StreamingFileDTO dto) => dto.FileIdentifier);
			}).ForMember((StreamingFile bo) => bo.FileByteStream, delegate(IMemberConfigurationExpression<StreamingFileDTO> m)
			{
				m.MapFrom<Stream>((StreamingFileDTO dto) => dto.FileByteStream);
			});
			Mapper.CreateMap<StreamingFile, StreamingFileDTO>().ForMember((StreamingFileDTO dto) => dto.FileIdentifier, delegate(IMemberConfigurationExpression<StreamingFile> m)
			{
				m.MapFrom<FileIdentifier>((StreamingFile bo) => bo.FileIdentifier);
			}).ForMember((StreamingFileDTO dto) => (object)dto.WhoAmI, delegate(IMemberConfigurationExpression<StreamingFile> m)
			{
				m.Ignore();
			}).ForMember((StreamingFileDTO dto) => dto.SessionId, delegate(IMemberConfigurationExpression<StreamingFile> m)
			{
				m.Ignore();
			}).ForMember((StreamingFileDTO dto) => dto.ApplicationContext, delegate(IMemberConfigurationExpression<StreamingFile> m)
			{
				m.Ignore();
			}).ForMember((StreamingFileDTO dto) => dto.FileByteStream, delegate(IMemberConfigurationExpression<StreamingFile> m)
			{
				m.MapFrom<Stream>((StreamingFile bo) => bo.FileByteStream);
			});
		}

		// Token: 0x0600049E RID: 1182 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600049F RID: 1183 RVA: 0x000169EC File Offset: 0x00014BEC
		public static StreamingFile ToDomainObject(this StreamingFileDTO binaryFileDTO)
		{
			return Mapper.Map<StreamingFileDTO, StreamingFile>(binaryFileDTO);
		}

		// Token: 0x060004A0 RID: 1184 RVA: 0x00016A04 File Offset: 0x00014C04
		public static StreamingFileDTO ToDTO(this StreamingFile binaryFile)
		{
			return Mapper.Map<StreamingFile, StreamingFileDTO>(binaryFile);
		}
	}
}

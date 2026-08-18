using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.MailMergeEntities;
using TechnoPro.Common.Public.Entities.MailMergeEntities;

namespace TechnoPro.Common.Core.Mappers.MailMergeEntities
{
	// Token: 0x020000C3 RID: 195
	public static class MailMergeContextWithCustomDictionaryMapper
	{
		// Token: 0x06000340 RID: 832 RVA: 0x00010E8C File Offset: 0x0000F08C
		static MailMergeContextWithCustomDictionaryMapper()
		{
			MailMergeContextMapper.CreateMap();
			MailMergeCustomDictionaryMapper.CreateMap();
			MailMergeValueFormatMapper.CreateMap();
			Mapper.CreateMap<MailMergeContextWithCustomDictionaryDTO, MailMergeContextWithCustomDictionary>().ForMember((MailMergeContextWithCustomDictionary pb) => pb.Context, delegate(IMemberConfigurationExpression<MailMergeContextWithCustomDictionaryDTO> m)
			{
				m.MapFrom<MailMergeContext>((MailMergeContextWithCustomDictionaryDTO pbdto) => (pbdto.Context == null) ? null : pbdto.Context.ToDomainObject());
			}).ForMember((MailMergeContextWithCustomDictionary pb) => pb.CustomDictionary, delegate(IMemberConfigurationExpression<MailMergeContextWithCustomDictionaryDTO> m)
			{
				m.MapFrom<MailMergeCustomDictionary>((MailMergeContextWithCustomDictionaryDTO pbdto) => (pbdto.CustomDictionary == null) ? null : pbdto.CustomDictionary.ToDomainObject());
			});
			Mapper.CreateMap<MailMergeContextWithCustomDictionary, MailMergeContextWithCustomDictionaryDTO>().ForMember((MailMergeContextWithCustomDictionaryDTO pb) => pb.Context, delegate(IMemberConfigurationExpression<MailMergeContextWithCustomDictionary> m)
			{
				m.MapFrom<MailMergeContextDTO>((MailMergeContextWithCustomDictionary pbdto) => (pbdto.Context == null) ? null : pbdto.Context.ToDTO());
			}).ForMember((MailMergeContextWithCustomDictionaryDTO pb) => pb.CustomDictionary, delegate(IMemberConfigurationExpression<MailMergeContextWithCustomDictionary> m)
			{
				m.MapFrom<MailMergeCustomDictionaryDTO>((MailMergeContextWithCustomDictionary pbdto) => (pbdto.CustomDictionary == null) ? null : pbdto.CustomDictionary.ToDTO());
			});
		}

		// Token: 0x06000341 RID: 833 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000342 RID: 834 RVA: 0x00010FF0 File Offset: 0x0000F1F0
		public static MailMergeContextWithCustomDictionary ToDomainObject(this MailMergeContextWithCustomDictionaryDTO mailMergeCodeDTO)
		{
			return Mapper.Map<MailMergeContextWithCustomDictionaryDTO, MailMergeContextWithCustomDictionary>(mailMergeCodeDTO);
		}

		// Token: 0x06000343 RID: 835 RVA: 0x00011008 File Offset: 0x0000F208
		public static MailMergeContextWithCustomDictionaryDTO ToDTO(this MailMergeContextWithCustomDictionary mailMergeCode)
		{
			return Mapper.Map<MailMergeContextWithCustomDictionary, MailMergeContextWithCustomDictionaryDTO>(mailMergeCode);
		}
	}
}

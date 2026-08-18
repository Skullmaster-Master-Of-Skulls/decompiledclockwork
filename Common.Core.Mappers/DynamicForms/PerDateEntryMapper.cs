using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.Common.Public.Entities.DynamicForms;

namespace TechnoPro.Common.Core.Mappers.DynamicForms
{
	// Token: 0x02000124 RID: 292
	public static class PerDateEntryMapper
	{
		// Token: 0x06000503 RID: 1283 RVA: 0x00018438 File Offset: 0x00016638
		static PerDateEntryMapper()
		{
			PersonBaseMapper.CreateMap();
			Mapper.CreateMap<PerDateEntry, PerDateEntryDTO>();
			Mapper.CreateMap<PerDateEntryDTO, PerDateEntry>().ForMember((PerDateEntry pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<PerDateEntryDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<PerDateEntry, PerDateEntryWithChildEntries>().ForMember((PerDateEntryWithChildEntries pb) => pb.ChildEntries, delegate(IMemberConfigurationExpression<PerDateEntry> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<PerDateEntryWithChildEntries, PerDateEntry>().ForMember((PerDateEntry pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<PerDateEntryWithChildEntries> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000504 RID: 1284 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000505 RID: 1285 RVA: 0x00018578 File Offset: 0x00016778
		public static PerDateEntry ToDomainObject(this PerDateEntryDTO dto)
		{
			return Mapper.Map<PerDateEntryDTO, PerDateEntry>(dto);
		}

		// Token: 0x06000506 RID: 1286 RVA: 0x00018590 File Offset: 0x00016790
		public static PerDateEntryDTO ToDTO(this PerDateEntry item)
		{
			return Mapper.Map<PerDateEntry, PerDateEntryDTO>(item);
		}

		// Token: 0x06000507 RID: 1287 RVA: 0x000185A8 File Offset: 0x000167A8
		public static PerDateEntry ToPerDateEntry(this PerDateEntryWithChildEntries sub)
		{
			return Mapper.Map<PerDateEntryWithChildEntries, PerDateEntry>(sub);
		}

		// Token: 0x06000508 RID: 1288 RVA: 0x000185C0 File Offset: 0x000167C0
		public static PerDateEntryWithChildEntries ToPerDateEntryWithChildEntries(this PerDateEntry item)
		{
			return Mapper.Map<PerDateEntry, PerDateEntryWithChildEntries>(item);
		}
	}
}

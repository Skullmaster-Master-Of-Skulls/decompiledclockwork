using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms;
using TechnoPro.ClockWorkServer.Contracts.DTO.LookupCourses;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.Startup;
using TechnoPro.ClockWorkServer.Contracts.DTO.UserSettingsPermissions;
using TechnoPro.Common.Core.Mappers.DynamicForms;
using TechnoPro.Common.Core.Mappers.LookupCourses;
using TechnoPro.Common.Core.Mappers.UserSettingsPermissions;
using TechnoPro.Common.Public.Entities.DynamicForms;
using TechnoPro.Common.Public.Entities.LookupCourses;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Startup;
using TechnoPro.Common.Public.Entities.UserSettingsPermissions;

namespace TechnoPro.Common.Core.Mappers.Startup
{
	// Token: 0x02000067 RID: 103
	public static class ClockWorkClientStartupMapper
	{
		// Token: 0x060001A4 RID: 420 RVA: 0x0000ABEC File Offset: 0x00008DEC
		static ClockWorkClientStartupMapper()
		{
			Mapper.CreateMap<ClockWorkClientStartup, ClockWorkClientStartupDTO>().ForMember((ClockWorkClientStartupDTO pb) => pb.Rooms, delegate(IMemberConfigurationExpression<ClockWorkClientStartup> m)
			{
				m.MapFrom<IEnumerable<PersonBaseDTO>>((ClockWorkClientStartup pbdto) => (pbdto.Rooms == null) ? null : (from g in pbdto.Rooms
				select g.ToDTO()));
			}).ForMember((ClockWorkClientStartupDTO pb) => pb.Sessions, delegate(IMemberConfigurationExpression<ClockWorkClientStartup> m)
			{
				m.MapFrom<IEnumerable<AcademicTermDTO>>((ClockWorkClientStartup pbdto) => (pbdto.Sessions == null) ? null : (from g in pbdto.Sessions
				select g.ToDTO()));
			}).ForMember((ClockWorkClientStartupDTO pb) => pb.Screens, delegate(IMemberConfigurationExpression<ClockWorkClientStartup> m)
			{
				m.MapFrom<IEnumerable<DynamicFormWithExtendedInfoDTO>>((ClockWorkClientStartup pbdto) => (pbdto.Screens == null) ? null : (from g in pbdto.Screens
				select g.ToDTO()));
			}).ForMember((ClockWorkClientStartupDTO pb) => pb.UserPermissionIsAllowedSet, delegate(IMemberConfigurationExpression<ClockWorkClientStartup> m)
			{
				m.MapFrom<UserPermissionIsAllowedSetDTO>((ClockWorkClientStartup pbdto) => (pbdto.UserPermissionIsAllowedSet == null) ? null : pbdto.UserPermissionIsAllowedSet.ToDTO());
			});
			Mapper.CreateMap<ClockWorkClientStartupDTO, ClockWorkClientStartup>().ForMember((ClockWorkClientStartup pb) => pb.Rooms, delegate(IMemberConfigurationExpression<ClockWorkClientStartupDTO> m)
			{
				m.MapFrom<IEnumerable<PersonBase>>((ClockWorkClientStartupDTO pbdto) => (pbdto.Rooms == null) ? null : (from g in pbdto.Rooms
				select g.ToDomainObject()));
			}).ForMember((ClockWorkClientStartup pb) => pb.Sessions, delegate(IMemberConfigurationExpression<ClockWorkClientStartupDTO> m)
			{
				m.MapFrom<IEnumerable<AcademicTerm>>((ClockWorkClientStartupDTO pbdto) => (pbdto.Sessions == null) ? null : (from g in pbdto.Sessions
				select g.ToDomainObject()));
			}).ForMember((ClockWorkClientStartup pb) => pb.Screens, delegate(IMemberConfigurationExpression<ClockWorkClientStartupDTO> m)
			{
				m.MapFrom<IEnumerable<DynamicFormWithExtendedInfo>>((ClockWorkClientStartupDTO pbdto) => (pbdto.Screens == null) ? null : (from g in pbdto.Screens
				select g.ToDomainObject()));
			}).ForMember((ClockWorkClientStartup pb) => pb.UserPermissionIsAllowedSet, delegate(IMemberConfigurationExpression<ClockWorkClientStartupDTO> m)
			{
				m.MapFrom<UserPermissionIsAllowedSet>((ClockWorkClientStartupDTO pbdto) => (pbdto.UserPermissionIsAllowedSet == null) ? null : pbdto.UserPermissionIsAllowedSet.ToDomainObject());
			});
		}

		// Token: 0x060001A5 RID: 421 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060001A6 RID: 422 RVA: 0x0000AE78 File Offset: 0x00009078
		public static ClockWorkClientStartup ToDomainObject(this ClockWorkClientStartupDTO dto)
		{
			return Mapper.Map<ClockWorkClientStartupDTO, ClockWorkClientStartup>(dto);
		}

		// Token: 0x060001A7 RID: 423 RVA: 0x0000AE90 File Offset: 0x00009090
		public static ClockWorkClientStartupDTO ToDTO(this ClockWorkClientStartup item)
		{
			return Mapper.Map<ClockWorkClientStartup, ClockWorkClientStartupDTO>(item);
		}
	}
}

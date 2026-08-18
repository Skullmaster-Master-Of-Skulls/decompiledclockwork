using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers.AlternativeFormat
{
	// Token: 0x02000223 RID: 547
	public static class StudentMediaRequestMapper
	{
		// Token: 0x0600095D RID: 2397 RVA: 0x00029B64 File Offset: 0x00027D64
		static StudentMediaRequestMapper()
		{
			PersonBaseMapper.CreateMap();
			MediaContentRequestedInfoMapper.CreateMap();
			CampusMapper.CreateMap();
			Mapper.CreateMap<StudentMediaRequest, StudentMediaRequestDTO>().ForMember((StudentMediaRequestDTO dto) => (object)dto.StudentMediaRequestId, delegate(IMemberConfigurationExpression<StudentMediaRequest> m)
			{
				m.MapFrom<int>((StudentMediaRequest bo) => bo.StudentMediaRequestId);
			}).ForMember((StudentMediaRequestDTO dto) => dto.RequestMadeFromStudent, delegate(IMemberConfigurationExpression<StudentMediaRequest> m)
			{
				m.MapFrom<PersonBaseDTO>((StudentMediaRequest bo) => bo.RequestMadeFromStudent.ToDTO());
			}).ForMember((StudentMediaRequestDTO dto) => (object)dto.CreatedDatetime, delegate(IMemberConfigurationExpression<StudentMediaRequest> m)
			{
				m.MapFrom<DateTime>((StudentMediaRequest bo) => bo.CreatedDatetime);
			}).ForMember((StudentMediaRequestDTO dto) => dto.ContentRequestedList, delegate(IMemberConfigurationExpression<StudentMediaRequest> m)
			{
				m.MapFrom<IEnumerable<MediaContentRequestedInfoDTO>>((StudentMediaRequest bo) => from i in bo.ContentRequestedList
				select i.ToDTO());
			}).ForMember((StudentMediaRequestDTO dto) => (object)dto.CompletedDateTime, delegate(IMemberConfigurationExpression<StudentMediaRequest> m)
			{
				m.MapFrom<DateTime?>((StudentMediaRequest bo) => bo.CompletedDateTime);
			}).ForMember((StudentMediaRequestDTO dto) => dto.Campus, delegate(IMemberConfigurationExpression<StudentMediaRequest> m)
			{
				m.MapFrom<SchoolCampusDTO>((StudentMediaRequest bo) => bo.Campus.ToDTO());
			});
			Mapper.CreateMap<StudentMediaRequestDTO, StudentMediaRequest>().ForMember((StudentMediaRequest bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<StudentMediaRequestDTO> m)
			{
				m.Ignore();
			}).ForMember((StudentMediaRequest bo) => (object)bo.StudentMediaRequestId, delegate(IMemberConfigurationExpression<StudentMediaRequestDTO> m)
			{
				m.MapFrom<int>((StudentMediaRequestDTO dto) => dto.StudentMediaRequestId);
			}).ForMember((StudentMediaRequest bo) => bo.RequestMadeFromStudent, delegate(IMemberConfigurationExpression<StudentMediaRequestDTO> m)
			{
				m.MapFrom<PersonBase>((StudentMediaRequestDTO dto) => dto.RequestMadeFromStudent.ToDomainObject());
			}).ForMember((StudentMediaRequest bo) => (object)bo.CreatedDatetime, delegate(IMemberConfigurationExpression<StudentMediaRequestDTO> m)
			{
				m.MapFrom<DateTime>((StudentMediaRequestDTO dto) => dto.CreatedDatetime);
			}).ForMember((StudentMediaRequest bo) => bo.ContentRequestedList, delegate(IMemberConfigurationExpression<StudentMediaRequestDTO> m)
			{
				m.MapFrom<IEnumerable<MediaContentRequestedInfo>>((StudentMediaRequestDTO dto) => from i in dto.ContentRequestedList
				select i.ToDomainObject());
			}).ForMember((StudentMediaRequest bo) => (object)bo.CompletedDateTime, delegate(IMemberConfigurationExpression<StudentMediaRequestDTO> m)
			{
				m.MapFrom<DateTime?>((StudentMediaRequestDTO dto) => dto.CompletedDateTime);
			}).ForMember((StudentMediaRequest bo) => bo.Campus, delegate(IMemberConfigurationExpression<StudentMediaRequestDTO> m)
			{
				m.MapFrom<SchoolCampus>((StudentMediaRequestDTO dto) => dto.Campus.ToDomainObject());
			});
		}

		// Token: 0x0600095E RID: 2398 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600095F RID: 2399 RVA: 0x00029FF4 File Offset: 0x000281F4
		public static StudentMediaRequest ToDomainObject(this StudentMediaRequestDTO studentMediaRequestDTO)
		{
			return Mapper.Map<StudentMediaRequestDTO, StudentMediaRequest>(studentMediaRequestDTO);
		}

		// Token: 0x06000960 RID: 2400 RVA: 0x0002A00C File Offset: 0x0002820C
		public static IList<StudentMediaRequest> ToDomainObject(this IList<StudentMediaRequestDTO> list)
		{
			IList<StudentMediaRequest> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<StudentMediaRequest>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x06000961 RID: 2401 RVA: 0x0002A050 File Offset: 0x00028250
		public static StudentMediaRequestDTO ToDTO(this StudentMediaRequest studentMediaRequest)
		{
			return Mapper.Map<StudentMediaRequest, StudentMediaRequestDTO>(studentMediaRequest);
		}

		// Token: 0x06000962 RID: 2402 RVA: 0x0002A068 File Offset: 0x00028268
		public static IList<StudentMediaRequestDTO> ToDTO(this IList<StudentMediaRequest> list)
		{
			IList<StudentMediaRequestDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<StudentMediaRequestDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}

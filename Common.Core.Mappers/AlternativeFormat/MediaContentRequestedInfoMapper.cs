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
	// Token: 0x02000217 RID: 535
	public static class MediaContentRequestedInfoMapper
	{
		// Token: 0x06000911 RID: 2321 RVA: 0x00026F7C File Offset: 0x0002517C
		static MediaContentRequestedInfoMapper()
		{
			ProofOfPurchaseMapper.CreateMap();
			MediaContentDetailMapper.CreateMap();
			CampusMapper.CreateMap();
			Mapper.CreateMap<MediaContentRequestedInfo, MediaContentRequestedInfoDTO>().ForMember((MediaContentRequestedInfoDTO dto) => (object)dto.MediaContentRequestedInfoID, delegate(IMemberConfigurationExpression<MediaContentRequestedInfo> m)
			{
				m.MapFrom<int>((MediaContentRequestedInfo bo) => bo.MediaContentRequestedInfoID);
			}).ForMember((MediaContentRequestedInfoDTO dto) => dto.ProofOfPurchase, delegate(IMemberConfigurationExpression<MediaContentRequestedInfo> m)
			{
				m.MapFrom<ProofOfPurchaseInfo>((MediaContentRequestedInfo bo) => bo.ProofOfPurchase);
			}).ForMember((MediaContentRequestedInfoDTO dto) => (object)dto.ProofOfPurchaseId, delegate(IMemberConfigurationExpression<MediaContentRequestedInfo> m)
			{
				m.MapFrom<int>((MediaContentRequestedInfo bo) => bo.ProofOfPurchaseId);
			}).ForMember((MediaContentRequestedInfoDTO dto) => (object)dto.RequestStatus, delegate(IMemberConfigurationExpression<MediaContentRequestedInfo> m)
			{
				m.MapFrom<MediaRequestStatus>((MediaContentRequestedInfo bo) => bo.RequestStatus);
			}).ForMember((MediaContentRequestedInfoDTO dto) => (object)dto.IsApproved, delegate(IMemberConfigurationExpression<MediaContentRequestedInfo> m)
			{
				m.MapFrom<bool>((MediaContentRequestedInfo bo) => bo.IsApproved);
			}).ForMember((MediaContentRequestedInfoDTO dto) => (object)dto.IsCompleted, delegate(IMemberConfigurationExpression<MediaContentRequestedInfo> m)
			{
				m.MapFrom<bool>((MediaContentRequestedInfo bo) => bo.IsCompleted);
			}).ForMember((MediaContentRequestedInfoDTO dto) => (object)dto.IsCancelled, delegate(IMemberConfigurationExpression<MediaContentRequestedInfo> m)
			{
				m.MapFrom<bool>((MediaContentRequestedInfo bo) => bo.IsCancelled);
			}).ForMember((MediaContentRequestedInfoDTO dto) => (object)dto.AvailableStartTime, delegate(IMemberConfigurationExpression<MediaContentRequestedInfo> m)
			{
				m.MapFrom<DateTime?>((MediaContentRequestedInfo bo) => bo.AvailableStartTime);
			}).ForMember((MediaContentRequestedInfoDTO dto) => (object)dto.AvailableEndTime, delegate(IMemberConfigurationExpression<MediaContentRequestedInfo> m)
			{
				m.MapFrom<DateTime?>((MediaContentRequestedInfo bo) => bo.AvailableEndTime);
			}).ForMember((MediaContentRequestedInfoDTO dto) => dto.ContentDetailRequested, delegate(IMemberConfigurationExpression<MediaContentRequestedInfo> m)
			{
				m.MapFrom<MediaContentDetailDTO>((MediaContentRequestedInfo bo) => bo.ContentDetailRequested.ToDTO());
			}).ForMember((MediaContentRequestedInfoDTO dto) => (object)dto.MediaJobId, delegate(IMemberConfigurationExpression<MediaContentRequestedInfo> m)
			{
				m.MapFrom<int>((MediaContentRequestedInfo bo) => bo.MediaJobId);
			}).ForMember((MediaContentRequestedInfoDTO dto) => dto.MediaJobTitle, delegate(IMemberConfigurationExpression<MediaContentRequestedInfo> m)
			{
				m.MapFrom<string>((MediaContentRequestedInfo bo) => bo.MediaJobTitle);
			}).ForMember((MediaContentRequestedInfoDTO dto) => (object)dto.StudentRequestId, delegate(IMemberConfigurationExpression<MediaContentRequestedInfo> m)
			{
				m.MapFrom<int>((MediaContentRequestedInfo bo) => bo.StudentRequestId);
			}).ForMember((MediaContentRequestedInfoDTO dto) => dto.RequestMadeFromStudent, delegate(IMemberConfigurationExpression<MediaContentRequestedInfo> m)
			{
				m.MapFrom<PersonBaseDTO>((MediaContentRequestedInfo bo) => bo.RequestMadeFromStudent.ToDTO());
			}).ForMember((MediaContentRequestedInfoDTO dto) => dto.Campus, delegate(IMemberConfigurationExpression<MediaContentRequestedInfo> m)
			{
				m.MapFrom<SchoolCampusDTO>((MediaContentRequestedInfo bo) => bo.Campus.ToDTO());
			}).ForMember((MediaContentRequestedInfoDTO dto) => (object)dto.CreatedDatetime, delegate(IMemberConfigurationExpression<MediaContentRequestedInfo> m)
			{
				m.MapFrom<DateTime>((MediaContentRequestedInfo bo) => bo.CreatedDatetime);
			}).ForMember((MediaContentRequestedInfoDTO dto) => (object)dto.CompletedDateTime, delegate(IMemberConfigurationExpression<MediaContentRequestedInfo> m)
			{
				m.MapFrom<DateTime?>((MediaContentRequestedInfo bo) => bo.CompletedDateTime);
			});
			Mapper.CreateMap<MediaContentRequestedInfoDTO, MediaContentRequestedInfo>().ForMember((MediaContentRequestedInfo bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoDTO> m)
			{
				m.Ignore();
			}).ForMember((MediaContentRequestedInfo bo) => (object)bo.MediaContentRequestedInfoID, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoDTO> m)
			{
				m.MapFrom<int>((MediaContentRequestedInfoDTO dto) => dto.MediaContentRequestedInfoID);
			}).ForMember((MediaContentRequestedInfo bo) => bo.ProofOfPurchase, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoDTO> m)
			{
				m.MapFrom<ProofOfPurchaseInfoDTO>((MediaContentRequestedInfoDTO dto) => dto.ProofOfPurchase);
			}).ForMember((MediaContentRequestedInfo bo) => (object)bo.ProofOfPurchaseId, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoDTO> m)
			{
				m.MapFrom<int>((MediaContentRequestedInfoDTO dto) => dto.ProofOfPurchaseId);
			}).ForMember((MediaContentRequestedInfo bo) => (object)bo.RequestStatus, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoDTO> m)
			{
				m.MapFrom<MediaRequestStatus>((MediaContentRequestedInfoDTO dto) => dto.RequestStatus);
			}).ForMember((MediaContentRequestedInfo bo) => (object)bo.IsApproved, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoDTO> m)
			{
				m.MapFrom<bool>((MediaContentRequestedInfoDTO dto) => dto.IsApproved);
			}).ForMember((MediaContentRequestedInfo bo) => (object)bo.IsCompleted, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoDTO> m)
			{
				m.MapFrom<bool>((MediaContentRequestedInfoDTO dto) => dto.IsCompleted);
			}).ForMember((MediaContentRequestedInfo bo) => (object)bo.AvailableStartTime, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoDTO> m)
			{
				m.MapFrom<DateTime?>((MediaContentRequestedInfoDTO dto) => dto.AvailableStartTime);
			}).ForMember((MediaContentRequestedInfo bo) => (object)bo.AvailableEndTime, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoDTO> m)
			{
				m.MapFrom<DateTime?>((MediaContentRequestedInfoDTO dto) => dto.AvailableEndTime);
			}).ForMember((MediaContentRequestedInfo bo) => bo.ContentDetailRequested, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoDTO> m)
			{
				m.MapFrom<MediaContentDetail>((MediaContentRequestedInfoDTO dto) => dto.ContentDetailRequested.ToDomainObject());
			}).ForMember((MediaContentRequestedInfo bo) => (object)bo.MediaJobId, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoDTO> m)
			{
				m.MapFrom<int>((MediaContentRequestedInfoDTO dto) => dto.MediaJobId);
			}).ForMember((MediaContentRequestedInfo bo) => bo.MediaJobTitle, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoDTO> m)
			{
				m.MapFrom<string>((MediaContentRequestedInfoDTO dto) => dto.MediaJobTitle);
			}).ForMember((MediaContentRequestedInfo bo) => (object)bo.StudentRequestId, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoDTO> m)
			{
				m.MapFrom<int>((MediaContentRequestedInfoDTO dto) => dto.StudentRequestId);
			}).ForMember((MediaContentRequestedInfo bo) => bo.RequestMadeFromStudent, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoDTO> m)
			{
				m.MapFrom<PersonBase>((MediaContentRequestedInfoDTO dto) => dto.RequestMadeFromStudent.ToDomainObject());
			}).ForMember((MediaContentRequestedInfo bo) => bo.Campus, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoDTO> m)
			{
				m.MapFrom<SchoolCampus>((MediaContentRequestedInfoDTO dto) => dto.Campus.ToDomainObject());
			}).ForMember((MediaContentRequestedInfo bo) => (object)bo.CreatedDatetime, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoDTO> m)
			{
				m.MapFrom<DateTime>((MediaContentRequestedInfoDTO dto) => dto.CreatedDatetime);
			}).ForMember((MediaContentRequestedInfo bo) => (object)bo.CompletedDateTime, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoDTO> m)
			{
				m.MapFrom<DateTime?>((MediaContentRequestedInfoDTO dto) => dto.CompletedDateTime);
			});
			Mapper.CreateMap<MediaContentRequestedInfoExtended, MediaContentRequestedInfoExtendedDTO>().ForMember((MediaContentRequestedInfoExtendedDTO dto) => (object)dto.MediaContentRequestedInfoID, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtended> m)
			{
				m.MapFrom<int>((MediaContentRequestedInfoExtended bo) => bo.MediaContentRequestedInfoID);
			}).ForMember((MediaContentRequestedInfoExtendedDTO dto) => dto.ProofOfPurchase, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtended> m)
			{
				m.MapFrom<ProofOfPurchaseInfo>((MediaContentRequestedInfoExtended bo) => bo.ProofOfPurchase);
			}).ForMember((MediaContentRequestedInfoExtendedDTO dto) => (object)dto.ProofOfPurchaseId, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtended> m)
			{
				m.MapFrom<int>((MediaContentRequestedInfoExtended bo) => bo.ProofOfPurchaseId);
			}).ForMember((MediaContentRequestedInfoExtendedDTO dto) => (object)dto.RequestStatus, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtended> m)
			{
				m.MapFrom<MediaRequestStatus>((MediaContentRequestedInfoExtended bo) => bo.RequestStatus);
			}).ForMember((MediaContentRequestedInfoExtendedDTO dto) => (object)dto.IsApproved, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtended> m)
			{
				m.MapFrom<bool>((MediaContentRequestedInfoExtended bo) => bo.IsApproved);
			}).ForMember((MediaContentRequestedInfoExtendedDTO dto) => (object)dto.IsCompleted, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtended> m)
			{
				m.MapFrom<bool>((MediaContentRequestedInfoExtended bo) => bo.IsCompleted);
			}).ForMember((MediaContentRequestedInfoExtendedDTO dto) => (object)dto.IsCancelled, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtended> m)
			{
				m.MapFrom<bool>((MediaContentRequestedInfoExtended bo) => bo.IsCancelled);
			}).ForMember((MediaContentRequestedInfoExtendedDTO dto) => (object)dto.AvailableStartTime, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtended> m)
			{
				m.MapFrom<DateTime?>((MediaContentRequestedInfoExtended bo) => bo.AvailableStartTime);
			}).ForMember((MediaContentRequestedInfoExtendedDTO dto) => (object)dto.AvailableEndTime, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtended> m)
			{
				m.MapFrom<DateTime?>((MediaContentRequestedInfoExtended bo) => bo.AvailableEndTime);
			}).ForMember((MediaContentRequestedInfoExtendedDTO dto) => dto.ContentDetailRequested, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtended> m)
			{
				m.MapFrom<MediaContentDetailDTO>((MediaContentRequestedInfoExtended bo) => bo.ContentDetailRequested.ToDTO());
			}).ForMember((MediaContentRequestedInfoExtendedDTO dto) => (object)dto.MediaJobId, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtended> m)
			{
				m.MapFrom<int>((MediaContentRequestedInfoExtended bo) => bo.MediaJobId);
			}).ForMember((MediaContentRequestedInfoExtendedDTO dto) => dto.MediaJobTitle, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtended> m)
			{
				m.MapFrom<string>((MediaContentRequestedInfoExtended bo) => bo.MediaJobTitle);
			}).ForMember((MediaContentRequestedInfoExtendedDTO dto) => (object)dto.StudentRequestId, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtended> m)
			{
				m.MapFrom<int>((MediaContentRequestedInfoExtended bo) => bo.StudentRequestId);
			}).ForMember((MediaContentRequestedInfoExtendedDTO dto) => dto.RequestMadeFromStudent, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtended> m)
			{
				m.MapFrom<PersonBaseDTO>((MediaContentRequestedInfoExtended bo) => bo.RequestMadeFromStudent.ToDTO());
			}).ForMember((MediaContentRequestedInfoExtendedDTO dto) => dto.Campus, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtended> m)
			{
				m.MapFrom<SchoolCampusDTO>((MediaContentRequestedInfoExtended bo) => bo.Campus.ToDTO());
			}).ForMember((MediaContentRequestedInfoExtendedDTO dto) => (object)dto.CreatedDatetime, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtended> m)
			{
				m.MapFrom<DateTime>((MediaContentRequestedInfoExtended bo) => bo.CreatedDatetime);
			}).ForMember((MediaContentRequestedInfoExtendedDTO dto) => (object)dto.CompletedDateTime, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtended> m)
			{
				m.MapFrom<DateTime?>((MediaContentRequestedInfoExtended bo) => bo.CompletedDateTime);
			}).ForMember((MediaContentRequestedInfoExtendedDTO dto) => (object)dto.FileSize, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtended> m)
			{
				m.MapFrom<int>((MediaContentRequestedInfoExtended bo) => bo.FileSize);
			});
			Mapper.CreateMap<MediaContentRequestedInfoExtendedDTO, MediaContentRequestedInfoExtended>().ForMember((MediaContentRequestedInfoExtended bo) => (object)bo.Id, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtendedDTO> m)
			{
				m.Ignore();
			}).ForMember((MediaContentRequestedInfoExtended bo) => (object)bo.MediaContentRequestedInfoID, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtendedDTO> m)
			{
				m.MapFrom<int>((MediaContentRequestedInfoExtendedDTO dto) => dto.MediaContentRequestedInfoID);
			}).ForMember((MediaContentRequestedInfoExtended bo) => bo.ProofOfPurchase, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtendedDTO> m)
			{
				m.MapFrom<ProofOfPurchaseInfoDTO>((MediaContentRequestedInfoExtendedDTO dto) => dto.ProofOfPurchase);
			}).ForMember((MediaContentRequestedInfoExtended bo) => (object)bo.ProofOfPurchaseId, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtendedDTO> m)
			{
				m.MapFrom<int>((MediaContentRequestedInfoExtendedDTO dto) => dto.ProofOfPurchaseId);
			}).ForMember((MediaContentRequestedInfoExtended bo) => (object)bo.RequestStatus, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtendedDTO> m)
			{
				m.MapFrom<MediaRequestStatus>((MediaContentRequestedInfoExtendedDTO dto) => dto.RequestStatus);
			}).ForMember((MediaContentRequestedInfoExtended bo) => (object)bo.IsApproved, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtendedDTO> m)
			{
				m.MapFrom<bool>((MediaContentRequestedInfoExtendedDTO dto) => dto.IsApproved);
			}).ForMember((MediaContentRequestedInfoExtended bo) => (object)bo.IsCompleted, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtendedDTO> m)
			{
				m.MapFrom<bool>((MediaContentRequestedInfoExtendedDTO dto) => dto.IsCompleted);
			}).ForMember((MediaContentRequestedInfoExtended bo) => (object)bo.AvailableStartTime, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtendedDTO> m)
			{
				m.MapFrom<DateTime?>((MediaContentRequestedInfoExtendedDTO dto) => dto.AvailableStartTime);
			}).ForMember((MediaContentRequestedInfoExtended bo) => (object)bo.AvailableEndTime, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtendedDTO> m)
			{
				m.MapFrom<DateTime?>((MediaContentRequestedInfoExtendedDTO dto) => dto.AvailableEndTime);
			}).ForMember((MediaContentRequestedInfoExtended bo) => bo.ContentDetailRequested, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtendedDTO> m)
			{
				m.MapFrom<MediaContentDetail>((MediaContentRequestedInfoExtendedDTO dto) => dto.ContentDetailRequested.ToDomainObject());
			}).ForMember((MediaContentRequestedInfoExtended bo) => (object)bo.MediaJobId, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtendedDTO> m)
			{
				m.MapFrom<int>((MediaContentRequestedInfoExtendedDTO dto) => dto.MediaJobId);
			}).ForMember((MediaContentRequestedInfoExtended bo) => bo.MediaJobTitle, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtendedDTO> m)
			{
				m.MapFrom<string>((MediaContentRequestedInfoExtendedDTO dto) => dto.MediaJobTitle);
			}).ForMember((MediaContentRequestedInfoExtended bo) => (object)bo.StudentRequestId, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtendedDTO> m)
			{
				m.MapFrom<int>((MediaContentRequestedInfoExtendedDTO dto) => dto.StudentRequestId);
			}).ForMember((MediaContentRequestedInfoExtended bo) => bo.RequestMadeFromStudent, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtendedDTO> m)
			{
				m.MapFrom<PersonBase>((MediaContentRequestedInfoExtendedDTO dto) => dto.RequestMadeFromStudent.ToDomainObject());
			}).ForMember((MediaContentRequestedInfoExtended bo) => bo.Campus, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtendedDTO> m)
			{
				m.MapFrom<SchoolCampus>((MediaContentRequestedInfoExtendedDTO dto) => dto.Campus.ToDomainObject());
			}).ForMember((MediaContentRequestedInfoExtended bo) => (object)bo.CreatedDatetime, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtendedDTO> m)
			{
				m.MapFrom<DateTime>((MediaContentRequestedInfoExtendedDTO dto) => dto.CreatedDatetime);
			}).ForMember((MediaContentRequestedInfoExtended bo) => (object)bo.CompletedDateTime, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtendedDTO> m)
			{
				m.MapFrom<DateTime?>((MediaContentRequestedInfoExtendedDTO dto) => dto.CompletedDateTime);
			}).ForMember((MediaContentRequestedInfoExtended bo) => (object)bo.FileSize, delegate(IMemberConfigurationExpression<MediaContentRequestedInfoExtendedDTO> m)
			{
				m.MapFrom<int>((MediaContentRequestedInfoExtendedDTO dto) => dto.FileSize);
			});
		}

		// Token: 0x06000912 RID: 2322 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000913 RID: 2323 RVA: 0x00028800 File Offset: 0x00026A00
		public static MediaContentRequestedInfo ToDomainObject(this MediaContentRequestedInfoDTO mediaContentRequestInfoDTO)
		{
			return Mapper.Map<MediaContentRequestedInfoDTO, MediaContentRequestedInfo>(mediaContentRequestInfoDTO);
		}

		// Token: 0x06000914 RID: 2324 RVA: 0x00028818 File Offset: 0x00026A18
		public static MediaContentRequestedInfoExtended ToDomainObject(this MediaContentRequestedInfoExtendedDTO mediaContentRequestInfoDTO)
		{
			return Mapper.Map<MediaContentRequestedInfoExtendedDTO, MediaContentRequestedInfoExtended>(mediaContentRequestInfoDTO);
		}

		// Token: 0x06000915 RID: 2325 RVA: 0x00028830 File Offset: 0x00026A30
		public static IList<MediaContentRequestedInfo> ToDomainObject(this IList<MediaContentRequestedInfoDTO> list)
		{
			return (from g in list
			select g.ToDomainObject()).ToList<MediaContentRequestedInfo>();
		}

		// Token: 0x06000916 RID: 2326 RVA: 0x0002886C File Offset: 0x00026A6C
		public static IList<MediaContentRequestedInfoExtended> ToDomainObject(this IList<MediaContentRequestedInfoExtendedDTO> list)
		{
			return (from g in list
			select g.ToDomainObject()).ToList<MediaContentRequestedInfoExtended>();
		}

		// Token: 0x06000917 RID: 2327 RVA: 0x000288A8 File Offset: 0x00026AA8
		public static MediaContentRequestedInfoDTO ToDTO(this MediaContentRequestedInfo mediaContentRequestInfo)
		{
			return Mapper.Map<MediaContentRequestedInfo, MediaContentRequestedInfoDTO>(mediaContentRequestInfo);
		}

		// Token: 0x06000918 RID: 2328 RVA: 0x000288C0 File Offset: 0x00026AC0
		public static MediaContentRequestedInfoExtendedDTO ToDTO(this MediaContentRequestedInfoExtended mediaContentRequestInfo)
		{
			return Mapper.Map<MediaContentRequestedInfoExtended, MediaContentRequestedInfoExtendedDTO>(mediaContentRequestInfo);
		}

		// Token: 0x06000919 RID: 2329 RVA: 0x000288D8 File Offset: 0x00026AD8
		public static IList<MediaContentRequestedInfoDTO> ToDTO(this IList<MediaContentRequestedInfo> list)
		{
			return (from g in list
			select g.ToDTO()).ToList<MediaContentRequestedInfoDTO>();
		}

		// Token: 0x0600091A RID: 2330 RVA: 0x00028914 File Offset: 0x00026B14
		public static IList<MediaContentRequestedInfoExtendedDTO> ToDTO(this IList<MediaContentRequestedInfoExtended> list)
		{
			return (from g in list
			select g.ToDTO()).ToList<MediaContentRequestedInfoExtendedDTO>();
		}
	}
}

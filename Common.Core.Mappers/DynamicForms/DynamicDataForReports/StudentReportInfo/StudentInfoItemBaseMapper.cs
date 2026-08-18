using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.DynamicForms.DynamicDataForReports.StudentReportInfo;
using TechnoPro.Common.Public.Entities.DynamicForms.DynamicDataForReports.StudentReportInfo;

namespace TechnoPro.Common.Core.Mappers.DynamicForms.DynamicDataForReports.StudentReportInfo
{
	// Token: 0x02000128 RID: 296
	public static class StudentInfoItemBaseMapper
	{
		// Token: 0x06000515 RID: 1301 RVA: 0x00018744 File Offset: 0x00016944
		static StudentInfoItemBaseMapper()
		{
			DynamicFieldMapper.CreateMap();
			Mapper.CreateMap<StudentInfoItemBaseDTO, StudentInfoItemBase>().Include<StudentInfoAccExpiryItemDTO, StudentInfoAccExpiryItem>().Include<StudentInfoAgeItemDTO, StudentInfoAgeItem>().Include<StudentInfoAssignedAdvisorItemDTO, StudentInfoAssignedAdvisorItem>().Include<StudentInfoEmailItemDTO, StudentInfoEmailItem>().ForMember((StudentInfoItemBase pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<StudentInfoItemBaseDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<StudentInfoItemBase, StudentInfoItemBaseDTO>().Include<StudentInfoAccExpiryItem, StudentInfoAccExpiryItemDTO>().Include<StudentInfoAgeItem, StudentInfoAgeItemDTO>().Include<StudentInfoAssignedAdvisorItem, StudentInfoAssignedAdvisorItemDTO>().Include<StudentInfoEmailItem, StudentInfoEmailItemDTO>();
			Mapper.CreateMap<StudentInfoAccExpiryItemDTO, StudentInfoAccExpiryItem>().ForMember((StudentInfoAccExpiryItem pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<StudentInfoAccExpiryItemDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<StudentInfoAccExpiryItem, StudentInfoAccExpiryItemDTO>();
			Mapper.CreateMap<StudentInfoAgeItemDTO, StudentInfoAgeItem>().ForMember((StudentInfoAgeItem pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<StudentInfoAgeItemDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<StudentInfoAgeItem, StudentInfoAgeItemDTO>();
			Mapper.CreateMap<StudentInfoAssignedAdvisorItemDTO, StudentInfoAssignedAdvisorItem>().ForMember((StudentInfoAssignedAdvisorItem pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<StudentInfoAssignedAdvisorItemDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<StudentInfoAssignedAdvisorItem, StudentInfoAssignedAdvisorItemDTO>();
			Mapper.CreateMap<StudentInfoEmailItemDTO, StudentInfoEmailItem>().ForMember((StudentInfoEmailItem pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<StudentInfoEmailItemDTO> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<StudentInfoEmailItem, StudentInfoEmailItemDTO>();
		}

		// Token: 0x06000516 RID: 1302 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000517 RID: 1303 RVA: 0x000189A8 File Offset: 0x00016BA8
		public static StudentInfoItemBase ToDomainObject(this StudentInfoItemBaseDTO dto)
		{
			Type type = dto.GetType();
			bool flag = type == typeof(StudentInfoAccExpiryItemDTO);
			StudentInfoItemBase result;
			if (flag)
			{
				result = (StudentInfoAccExpiryItem)Mapper.Map(dto, type, typeof(StudentInfoAccExpiryItem));
			}
			else
			{
				bool flag2 = type == typeof(StudentInfoAgeItemDTO);
				if (flag2)
				{
					result = (StudentInfoAgeItem)Mapper.Map(dto, type, typeof(StudentInfoAgeItem));
				}
				else
				{
					bool flag3 = type == typeof(StudentInfoAssignedAdvisorItemDTO);
					if (flag3)
					{
						result = (StudentInfoAssignedAdvisorItem)Mapper.Map(dto, type, typeof(StudentInfoAssignedAdvisorItem));
					}
					else
					{
						bool flag4 = type == typeof(StudentInfoEmailItemDTO);
						if (flag4)
						{
							result = (StudentInfoEmailItem)Mapper.Map(dto, type, typeof(StudentInfoEmailItem));
						}
						else
						{
							result = null;
						}
					}
				}
			}
			return result;
		}

		// Token: 0x06000518 RID: 1304 RVA: 0x00018A80 File Offset: 0x00016C80
		public static StudentInfoItemBaseDTO ToDTO(this StudentInfoItemBase item)
		{
			Type type = item.GetType();
			bool flag = type == typeof(StudentInfoAccExpiryItem);
			StudentInfoItemBaseDTO result;
			if (flag)
			{
				result = (StudentInfoAccExpiryItemDTO)Mapper.Map(item, type, typeof(StudentInfoAccExpiryItemDTO));
			}
			else
			{
				bool flag2 = type == typeof(StudentInfoAgeItem);
				if (flag2)
				{
					result = (StudentInfoAgeItemDTO)Mapper.Map(item, type, typeof(StudentInfoAgeItemDTO));
				}
				else
				{
					bool flag3 = type == typeof(StudentInfoAssignedAdvisorItem);
					if (flag3)
					{
						result = (StudentInfoAssignedAdvisorItemDTO)Mapper.Map(item, type, typeof(StudentInfoAssignedAdvisorItemDTO));
					}
					else
					{
						bool flag4 = type == typeof(StudentInfoEmailItem);
						if (flag4)
						{
							result = (StudentInfoEmailItemDTO)Mapper.Map(item, type, typeof(StudentInfoEmailItemDTO));
						}
						else
						{
							result = null;
						}
					}
				}
			}
			return result;
		}
	}
}

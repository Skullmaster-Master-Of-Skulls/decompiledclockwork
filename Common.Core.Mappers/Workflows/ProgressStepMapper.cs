using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Workflows;
using TechnoPro.Common.Public.Entities.Workflows;

namespace TechnoPro.Common.Core.Mappers.Workflows
{
	// Token: 0x0200000F RID: 15
	public static class ProgressStepMapper
	{
		// Token: 0x06000040 RID: 64 RVA: 0x00003774 File Offset: 0x00001974
		static ProgressStepMapper()
		{
			Mapper.CreateMap<ProgressStep, ProgressStepDTO>();
			Mapper.CreateMap<ProgressStepDTO, ProgressStep>().ForMember((ProgressStep pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<ProgressStepDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000042 RID: 66 RVA: 0x000037F0 File Offset: 0x000019F0
		public static ProgressStep ToDomainObject(this ProgressStepDTO dto)
		{
			return Mapper.Map<ProgressStepDTO, ProgressStep>(dto);
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00003808 File Offset: 0x00001A08
		public static ProgressStepDTO ToDTO(this ProgressStep item)
		{
			return Mapper.Map<ProgressStep, ProgressStepDTO>(item);
		}
	}
}

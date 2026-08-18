using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.Workflows;
using TechnoPro.Common.Public.Entities.Workflows;

namespace TechnoPro.Common.Core.Mappers.Workflows
{
	// Token: 0x02000010 RID: 16
	public static class WorkflowMapper
	{
		// Token: 0x06000044 RID: 68 RVA: 0x00003820 File Offset: 0x00001A20
		static WorkflowMapper()
		{
			ProgressStepMapper.CreateMap();
			Mapper.CreateMap<Workflow, WorkflowDTO>().ForMember((WorkflowDTO pb) => pb.ProgressSteps, delegate(IMemberConfigurationExpression<Workflow> m)
			{
				m.MapFrom<List<ProgressStepDTO>>((Workflow pbdto) => (pbdto.ProgressSteps == null) ? null : (from g in pbdto.ProgressSteps
				select g.ToDTO()).ToList<ProgressStepDTO>());
			});
			Mapper.CreateMap<WorkflowDTO, Workflow>().ForMember((Workflow pb) => pb.ProgressSteps, delegate(IMemberConfigurationExpression<WorkflowDTO> m)
			{
				m.MapFrom<List<ProgressStep>>((WorkflowDTO pbdto) => (pbdto.ProgressSteps == null) ? null : (from g in pbdto.ProgressSteps
				select g.ToDomainObject()).ToList<ProgressStep>());
			});
		}

		// Token: 0x06000045 RID: 69 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000038DC File Offset: 0x00001ADC
		public static Workflow ToDomainObject(this WorkflowDTO dto)
		{
			return Mapper.Map<WorkflowDTO, Workflow>(dto);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000038F4 File Offset: 0x00001AF4
		public static WorkflowDTO ToDTO(this Workflow item)
		{
			return Mapper.Map<Workflow, WorkflowDTO>(item);
		}
	}
}

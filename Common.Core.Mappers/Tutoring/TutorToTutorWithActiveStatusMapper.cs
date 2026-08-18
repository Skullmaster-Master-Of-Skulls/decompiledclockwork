using System;
using AutoMapper;
using TechnoPro.Common.Public.Entities.Tutoring;

namespace TechnoPro.Common.Core.Mappers.Tutoring
{
	// Token: 0x0200002D RID: 45
	public static class TutorToTutorWithActiveStatusMapper
	{
		// Token: 0x060000BE RID: 190 RVA: 0x00005EF8 File Offset: 0x000040F8
		static TutorToTutorWithActiveStatusMapper()
		{
			Mapper.CreateMap<Tutor, TutorWithActiveStatus>().ForMember((TutorWithActiveStatus pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<Tutor> m)
			{
				m.Ignore();
			});
			Mapper.CreateMap<TutorWithActiveStatus, Tutor>().ForMember((Tutor pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<TutorWithActiveStatus> m)
			{
				m.Ignore();
			}).ForMember((Tutor pb) => (object)pb.IsActivated, delegate(IMemberConfigurationExpression<TutorWithActiveStatus> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x060000BF RID: 191 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x00006034 File Offset: 0x00004234
		public static TutorWithActiveStatus ToTutorWithActiveStatus(this Tutor dto)
		{
			return Mapper.Map<Tutor, TutorWithActiveStatus>(dto);
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000604C File Offset: 0x0000424C
		public static Tutor ToTutor(this TutorWithActiveStatus item)
		{
			return Mapper.Map<TutorWithActiveStatus, Tutor>(item);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.ClockWorkServer.Contracts.DTO.Reports;
using TechnoPro.Common.Public.Entities.People;
using TechnoPro.Common.Public.Entities.Reports;

namespace TechnoPro.Common.Core.Mappers.Reports
{
	// Token: 0x0200008F RID: 143
	public static class ReportMapper
	{
		// Token: 0x0600026A RID: 618 RVA: 0x0000D7D4 File Offset: 0x0000B9D4
		static ReportMapper()
		{
			ReportParameterMapper.CreateMap();
			ReportFunctionMapper.CreateMap();
			ReportGroupMapper.CreateMap();
			ReportParametersLegacyMapper.CreateMap();
			ReportParameterFormMapper.CreateMap();
			PersonBaseMapper.CreateMap();
			FormattedReportMapper.CreateMap();
			ReportOptionsMapper.CreateMap();
			Mapper.CreateMap<ReportDTO, Report>().ForMember((Report pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<ReportDTO> m)
			{
				m.Ignore();
			}).ForMember((Report pb) => pb.WhoLastExecuted, delegate(IMemberConfigurationExpression<ReportDTO> m)
			{
				m.MapFrom<PersonBase>((ReportDTO pbdto) => (pbdto.WhoLastExecuted == null) ? null : pbdto.WhoLastExecuted.ToDomainObject());
			}).ForMember((Report pb) => pb.WhoCreated, delegate(IMemberConfigurationExpression<ReportDTO> m)
			{
				m.MapFrom<PersonBase>((ReportDTO pbdto) => (pbdto.WhoCreated == null) ? null : pbdto.WhoCreated.ToDomainObject());
			}).ForMember((Report pb) => pb.WhoLastModified, delegate(IMemberConfigurationExpression<ReportDTO> m)
			{
				m.MapFrom<PersonBase>((ReportDTO pbdto) => (pbdto.WhoLastModified == null) ? null : pbdto.WhoLastModified.ToDomainObject());
			}).ForMember((Report pb) => pb.LegacyParameters, delegate(IMemberConfigurationExpression<ReportDTO> m)
			{
				m.MapFrom<ReportParametersLegacy>((ReportDTO pbdto) => (pbdto.LegacyParameters == null) ? null : pbdto.LegacyParameters.ToDomainObject());
			}).ForMember((Report pb) => pb.ReportParameters, delegate(IMemberConfigurationExpression<ReportDTO> m)
			{
				m.MapFrom<List<ReportParameter>>((ReportDTO pbdto) => (pbdto.ReportParameters == null) ? null : pbdto.ReportParameters.ToList<ReportParameterDTO>().ConvertAll<ReportParameter>((ReportParameterDTO g) => g.ToDomainObject()));
			}).ForMember((Report pb) => pb.Functions, delegate(IMemberConfigurationExpression<ReportDTO> m)
			{
				m.MapFrom<List<ReportFunction>>((ReportDTO pbdto) => (pbdto.Functions == null) ? null : pbdto.Functions.ToList<ReportFunctionDTO>().ConvertAll<ReportFunction>((ReportFunctionDTO g) => g.ToDomainObject()));
			}).ForMember((Report pb) => pb.FormattedReports, delegate(IMemberConfigurationExpression<ReportDTO> m)
			{
				m.MapFrom<List<FormattedReport>>((ReportDTO pbdto) => (pbdto.FormattedReports == null) ? null : pbdto.FormattedReports.ToList<FormattedReportDTO>().ConvertAll<FormattedReport>((FormattedReportDTO g) => g.ToDomainObject()));
			}).ForMember((Report pb) => pb.ReportOptions, delegate(IMemberConfigurationExpression<ReportDTO> m)
			{
				m.MapFrom<ReportOptions>((ReportDTO pbdto) => (pbdto.ReportOptions == null) ? null : pbdto.ReportOptions.ToDomainObject());
			});
			Mapper.CreateMap<Report, ReportDTO>().ForMember((ReportDTO pb) => pb.WhoLastExecuted, delegate(IMemberConfigurationExpression<Report> m)
			{
				m.MapFrom<PersonBaseDTO>((Report pbdto) => (pbdto.WhoLastExecuted == null) ? null : pbdto.WhoLastExecuted.ToDTO());
			}).ForMember((ReportDTO pb) => pb.WhoCreated, delegate(IMemberConfigurationExpression<Report> m)
			{
				m.MapFrom<PersonBaseDTO>((Report pbdto) => (pbdto.WhoCreated == null) ? null : pbdto.WhoCreated.ToDTO());
			}).ForMember((ReportDTO pb) => pb.WhoLastModified, delegate(IMemberConfigurationExpression<Report> m)
			{
				m.MapFrom<PersonBaseDTO>((Report pbdto) => (pbdto.WhoLastModified == null) ? null : pbdto.WhoLastModified.ToDTO());
			}).ForMember((ReportDTO pb) => pb.LegacyParameters, delegate(IMemberConfigurationExpression<Report> m)
			{
				m.MapFrom<ReportParametersLegacyDTO>((Report pbdto) => (pbdto.LegacyParameters == null) ? null : pbdto.LegacyParameters.ToDTO());
			}).ForMember((ReportDTO pb) => pb.ReportParameters, delegate(IMemberConfigurationExpression<Report> m)
			{
				m.MapFrom<List<ReportParameterDTO>>((Report pbdto) => (pbdto.ReportParameters == null) ? null : pbdto.ReportParameters.ToList<ReportParameter>().ConvertAll<ReportParameterDTO>((ReportParameter g) => g.ToDTO()));
			}).ForMember((ReportDTO pb) => pb.Functions, delegate(IMemberConfigurationExpression<Report> m)
			{
				m.MapFrom<List<ReportFunctionDTO>>((Report pbdto) => (pbdto.Functions == null) ? null : pbdto.Functions.ToList<ReportFunction>().ConvertAll<ReportFunctionDTO>((ReportFunction g) => g.ToDTO()));
			}).ForMember((ReportDTO pb) => pb.FormattedReports, delegate(IMemberConfigurationExpression<Report> m)
			{
				m.MapFrom<List<FormattedReportDTO>>((Report pbdto) => (pbdto.FormattedReports == null) ? null : pbdto.FormattedReports.ToList<FormattedReport>().ConvertAll<FormattedReportDTO>((FormattedReport g) => g.ToDTO()));
			}).ForMember((ReportDTO pb) => pb.ReportOptions, delegate(IMemberConfigurationExpression<Report> m)
			{
				m.MapFrom<ReportOptionsDTO>((Report pbdto) => (pbdto.ReportOptions == null) ? null : pbdto.ReportOptions.ToDTO());
			});
		}

		// Token: 0x0600026B RID: 619 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600026C RID: 620 RVA: 0x0000DD60 File Offset: 0x0000BF60
		public static Report ToDomainObject(this ReportDTO dto)
		{
			return Mapper.Map<ReportDTO, Report>(dto);
		}

		// Token: 0x0600026D RID: 621 RVA: 0x0000DD78 File Offset: 0x0000BF78
		public static ReportDTO ToDTO(this Report item)
		{
			return Mapper.Map<Report, ReportDTO>(item);
		}
	}
}

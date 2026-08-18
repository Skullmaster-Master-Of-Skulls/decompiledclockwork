using System;
using System.Data;
using System.Data.Common;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;

namespace TechnoPro.Common.Core.Mappers
{
	// Token: 0x0200000A RID: 10
	public static class DbParameterMapper
	{
		// Token: 0x06000029 RID: 41 RVA: 0x00002B5C File Offset: 0x00000D5C
		static DbParameterMapper()
		{
			Mapper.CreateMap<CWDbParameter, DbParameter>().ForMember((DbParameter pb) => (object)pb.IsNullable, delegate(IMemberConfigurationExpression<CWDbParameter> m)
			{
				m.Ignore();
			}).ForMember((DbParameter pb) => (object)pb.Precision, delegate(IMemberConfigurationExpression<CWDbParameter> m)
			{
				m.Ignore();
			}).ForMember((DbParameter pb) => (object)pb.Scale, delegate(IMemberConfigurationExpression<CWDbParameter> m)
			{
				m.Ignore();
			}).ForMember((DbParameter pb) => (object)pb.Size, delegate(IMemberConfigurationExpression<CWDbParameter> m)
			{
				m.Ignore();
			}).ForMember((DbParameter pb) => pb.SourceColumn, delegate(IMemberConfigurationExpression<CWDbParameter> m)
			{
				m.Ignore();
			}).ForMember((DbParameter pb) => (object)pb.SourceColumnNullMapping, delegate(IMemberConfigurationExpression<CWDbParameter> m)
			{
				m.Ignore();
			}).ForMember((DbParameter pb) => (object)pb.SourceVersion, delegate(IMemberConfigurationExpression<CWDbParameter> m)
			{
				m.Ignore();
			}).ForMember((DbParameter dbp) => (object)dbp.DbType, delegate(IMemberConfigurationExpression<CWDbParameter> m)
			{
				m.MapFrom<DbType>((CWDbParameter cwp) => (DbType)cwp.DbType);
			}).ForMember((DbParameter dbp) => (object)dbp.Direction, delegate(IMemberConfigurationExpression<CWDbParameter> m)
			{
				m.MapFrom<ParameterDirection>((CWDbParameter cwp) => ParameterDirection.Input);
			});
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600002B RID: 43 RVA: 0x00002EA8 File Offset: 0x000010A8
		public static DbParameter ToDomainObject(this CWDbParameter cwDBParameter)
		{
			return Mapper.Map<CWDbParameter, DbParameter>(cwDBParameter);
		}

		// Token: 0x0600002C RID: 44 RVA: 0x00002EC0 File Offset: 0x000010C0
		public static DbParameter[] ToDomainObject(this CWDbParameter[] list)
		{
			DbParameter[] result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToArray<DbParameter>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}

using System;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DataContracts;
using TechnoPro.Common.Public.Entities;

namespace TechnoPro.Common.Core.Mappers
{
	// Token: 0x02000009 RID: 9
	public static class MailingInfoMapper
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00002ABC File Offset: 0x00000CBC
		static MailingInfoMapper()
		{
			Mapper.CreateMap<MailingInfo, DataMailingInfo>();
			Mapper.CreateMap<DataMailingInfo, MailingInfo>().ForMember((MailingInfo pb) => pb.Id, delegate(IMemberConfigurationExpression<DataMailingInfo> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000026 RID: 38 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002B2C File Offset: 0x00000D2C
		public static MailingInfo ToDomainObject(this DataMailingInfo dataMailingInfo)
		{
			return Mapper.Map<DataMailingInfo, MailingInfo>(dataMailingInfo);
		}

		// Token: 0x06000028 RID: 40 RVA: 0x00002B44 File Offset: 0x00000D44
		public static DataMailingInfo ToDTO(this MailingInfo mailingInfo)
		{
			return Mapper.Map<MailingInfo, DataMailingInfo>(mailingInfo);
		}
	}
}

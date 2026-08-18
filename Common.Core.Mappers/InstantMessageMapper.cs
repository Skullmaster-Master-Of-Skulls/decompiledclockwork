using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts;
using TechnoPro.Common.Public.Entities.DropBox;

namespace TechnoPro.Common.Core.Mappers
{
	// Token: 0x0200000B RID: 11
	public static class InstantMessageMapper
	{
		// Token: 0x0600002D RID: 45 RVA: 0x00002F04 File Offset: 0x00001104
		static InstantMessageMapper()
		{
			Mapper.CreateMap<DropBox_IM, InstantMessage>().ForMember((InstantMessage im) => im.From, delegate(IMemberConfigurationExpression<DropBox_IM> m)
			{
				m.MapFrom<DropBox_User>((DropBox_IM dbIM) => dbIM.From);
			}).ForMember((InstantMessage im) => (object)im.Code, delegate(IMemberConfigurationExpression<DropBox_IM> m)
			{
				m.MapFrom<MessageCode>((DropBox_IM dpIM) => MessageCode.REGULAR_MESSAGE);
			}).ForMember((InstantMessage im) => (object)im.Type, delegate(IMemberConfigurationExpression<DropBox_IM> m)
			{
				m.MapFrom<MessageType>((DropBox_IM dpIM) => MessageType.Private);
			});
			Mapper.CreateMap<InstantMessage, DropBox_IM>().ForMember((DropBox_IM dbIM) => (object)dbIM.Id, delegate(IMemberConfigurationExpression<InstantMessage> m)
			{
				m.Ignore();
			}).ForMember((DropBox_IM dbIM) => dbIM.From, delegate(IMemberConfigurationExpression<InstantMessage> m)
			{
				m.MapFrom<IM_User>((InstantMessage im) => im.From);
			}).ForMember((DropBox_IM dbIM) => (object)dbIM.WasRead, delegate(IMemberConfigurationExpression<InstantMessage> m)
			{
				m.UseValue<bool>(false);
			});
		}

		// Token: 0x0600002E RID: 46 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x0600002F RID: 47 RVA: 0x00003134 File Offset: 0x00001334
		public static InstantMessage ToDTO(this DropBox_IM dpIM)
		{
			return Mapper.Map<DropBox_IM, InstantMessage>(dpIM);
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000314C File Offset: 0x0000134C
		public static IList<InstantMessage> ToDTO(this IList<DropBox_IM> list)
		{
			IList<InstantMessage> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<InstantMessage>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}

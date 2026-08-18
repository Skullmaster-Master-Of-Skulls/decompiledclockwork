using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.TPMailMan;
using TechnoPro.Common.Public.Entities.TPMailMan;

namespace TechnoPro.Common.Core.Mappers.TPMailMan
{
	// Token: 0x02000031 RID: 49
	public static class TPMailMessageMapper
	{
		// Token: 0x060000CE RID: 206 RVA: 0x000062AC File Offset: 0x000044AC
		static TPMailMessageMapper()
		{
			TPMailAddressMapper.CreateMap();
			TPMailAttachmentMapper.CreateMap();
			eTPMessageDeliveryMethodMapper.CreateMap();
			eTPMessagePriorityMapper.CreateMap();
			Mapper.CreateMap<TPMailMessageDTO, TPMailMessage>().ForMember((TPMailMessage msgdto) => msgdto.Id, delegate(IMemberConfigurationExpression<TPMailMessageDTO> m)
			{
				m.Ignore();
			}).ForMember((TPMailMessage msg) => msg.From, delegate(IMemberConfigurationExpression<TPMailMessageDTO> m)
			{
				m.MapFrom<TPMailAddress>((TPMailMessageDTO msgdto) => (msgdto == null || msgdto.From == null) ? null : msgdto.From.ToDomainObject());
			}).ForMember((TPMailMessage msg) => msg.To, delegate(IMemberConfigurationExpression<TPMailMessageDTO> m)
			{
				ParameterExpression parameterExpression2;
				m.MapFrom<List<TPMailAddress>>(Expression.Lambda<Func<TPMailMessageDTO, List<TPMailAddress>>>(Expression.Condition(Expression.OrElse(Expression.Equal(parameterExpression2, Expression.Constant(null, typeof(object))), Expression.Equal(Expression.Property(parameterExpression2, methodof(TPMailMessageDTO.get_To())), Expression.Constant(null, typeof(object)))), Expression.New(typeof(List<TPMailAddress>)), Expression.Call(Expression.Property(parameterExpression2, methodof(TPMailMessageDTO.get_To())), methodof(List<TPMailAddressDTO>.ConvertAll(Converter<T, !!0>)), new Expression[]
				{
					(TPMailAddressDTO mg) => mg.ToDomainObject()
				})), new ParameterExpression[]
				{
					parameterExpression2
				}));
			}).ForMember((TPMailMessage msg) => msg.Bcc, delegate(IMemberConfigurationExpression<TPMailMessageDTO> m)
			{
				ParameterExpression parameterExpression2;
				m.MapFrom<List<TPMailAddress>>(Expression.Lambda<Func<TPMailMessageDTO, List<TPMailAddress>>>(Expression.Condition(Expression.OrElse(Expression.Equal(parameterExpression2, Expression.Constant(null, typeof(object))), Expression.Equal(Expression.Property(parameterExpression2, methodof(TPMailMessageDTO.get_Bcc())), Expression.Constant(null, typeof(object)))), Expression.New(typeof(List<TPMailAddress>)), Expression.Call(Expression.Property(parameterExpression2, methodof(TPMailMessageDTO.get_Bcc())), methodof(List<TPMailAddressDTO>.ConvertAll(Converter<T, !!0>)), new Expression[]
				{
					(TPMailAddressDTO mg) => mg.ToDomainObject()
				})), new ParameterExpression[]
				{
					parameterExpression2
				}));
			}).ForMember((TPMailMessage msg) => msg.Cc, delegate(IMemberConfigurationExpression<TPMailMessageDTO> m)
			{
				ParameterExpression parameterExpression2;
				m.MapFrom<List<TPMailAddress>>(Expression.Lambda<Func<TPMailMessageDTO, List<TPMailAddress>>>(Expression.Condition(Expression.OrElse(Expression.Equal(parameterExpression2, Expression.Constant(null, typeof(object))), Expression.Equal(Expression.Property(parameterExpression2, methodof(TPMailMessageDTO.get_Cc())), Expression.Constant(null, typeof(object)))), Expression.New(typeof(List<TPMailAddress>)), Expression.Call(Expression.Property(parameterExpression2, methodof(TPMailMessageDTO.get_Cc())), methodof(List<TPMailAddressDTO>.ConvertAll(Converter<T, !!0>)), new Expression[]
				{
					(TPMailAddressDTO mg) => mg.ToDomainObject()
				})), new ParameterExpression[]
				{
					parameterExpression2
				}));
			}).ForMember((TPMailMessage msg) => msg.Attachments, delegate(IMemberConfigurationExpression<TPMailMessageDTO> m)
			{
				ParameterExpression parameterExpression2;
				m.MapFrom<List<TPMailAttachment>>(Expression.Lambda<Func<TPMailMessageDTO, List<TPMailAttachment>>>(Expression.Condition(Expression.OrElse(Expression.Equal(parameterExpression2, Expression.Constant(null, typeof(object))), Expression.Equal(Expression.Property(parameterExpression2, methodof(TPMailMessageDTO.get_Attachments())), Expression.Constant(null, typeof(object)))), Expression.New(typeof(List<TPMailAttachment>)), Expression.Call(null, methodof(IEnumerable<!!0>.ToList()), new Expression[]
				{
					Expression.Call(null, methodof(IEnumerable<!!0>.Select(Func<!!0, !!1>)), new Expression[]
					{
						Expression.Property(parameterExpression2, methodof(TPMailMessageDTO.get_Attachments())),
						(TPMailAttachmentDTO g) => g.ToDomainObject()
					})
				})), new ParameterExpression[]
				{
					parameterExpression2
				}));
			});
			Mapper.CreateMap<TPMailMessage, TPMailMessageDTO>().ForMember((TPMailMessageDTO msg) => msg.From, delegate(IMemberConfigurationExpression<TPMailMessage> m)
			{
				m.MapFrom<TPMailAddressDTO>((TPMailMessage msgdto) => (msgdto == null || msgdto.From == null) ? null : msgdto.From.ToDTO());
			}).ForMember((TPMailMessageDTO msg) => msg.To, delegate(IMemberConfigurationExpression<TPMailMessage> m)
			{
				ParameterExpression parameterExpression2;
				m.MapFrom<List<TPMailAddressDTO>>(Expression.Lambda<Func<TPMailMessage, List<TPMailAddressDTO>>>(Expression.Condition(Expression.OrElse(Expression.Equal(parameterExpression2, Expression.Constant(null, typeof(object))), Expression.Equal(Expression.Property(parameterExpression2, methodof(TPMailMessage.get_To())), Expression.Constant(null, typeof(object)))), Expression.New(typeof(List<TPMailAddressDTO>)), Expression.Call(Expression.Property(parameterExpression2, methodof(TPMailMessage.get_To())), methodof(List<TPMailAddress>.ConvertAll(Converter<T, !!0>)), new Expression[]
				{
					(TPMailAddress mg) => mg.ToDTO()
				})), new ParameterExpression[]
				{
					parameterExpression2
				}));
			}).ForMember((TPMailMessageDTO msg) => msg.Bcc, delegate(IMemberConfigurationExpression<TPMailMessage> m)
			{
				ParameterExpression parameterExpression2;
				m.MapFrom<List<TPMailAddressDTO>>(Expression.Lambda<Func<TPMailMessage, List<TPMailAddressDTO>>>(Expression.Condition(Expression.OrElse(Expression.Equal(parameterExpression2, Expression.Constant(null, typeof(object))), Expression.Equal(Expression.Property(parameterExpression2, methodof(TPMailMessage.get_Bcc())), Expression.Constant(null, typeof(object)))), Expression.New(typeof(List<TPMailAddressDTO>)), Expression.Call(Expression.Property(parameterExpression2, methodof(TPMailMessage.get_Bcc())), methodof(List<TPMailAddress>.ConvertAll(Converter<T, !!0>)), new Expression[]
				{
					(TPMailAddress mg) => mg.ToDTO()
				})), new ParameterExpression[]
				{
					parameterExpression2
				}));
			}).ForMember((TPMailMessageDTO msg) => msg.Cc, delegate(IMemberConfigurationExpression<TPMailMessage> m)
			{
				ParameterExpression parameterExpression2;
				m.MapFrom<List<TPMailAddressDTO>>(Expression.Lambda<Func<TPMailMessage, List<TPMailAddressDTO>>>(Expression.Condition(Expression.OrElse(Expression.Equal(parameterExpression2, Expression.Constant(null, typeof(object))), Expression.Equal(Expression.Property(parameterExpression2, methodof(TPMailMessage.get_Cc())), Expression.Constant(null, typeof(object)))), Expression.New(typeof(List<TPMailAddressDTO>)), Expression.Call(Expression.Property(parameterExpression2, methodof(TPMailMessage.get_Cc())), methodof(List<TPMailAddress>.ConvertAll(Converter<T, !!0>)), new Expression[]
				{
					(TPMailAddress mg) => mg.ToDTO()
				})), new ParameterExpression[]
				{
					parameterExpression2
				}));
			}).ForMember((TPMailMessageDTO msg) => msg.Attachments, delegate(IMemberConfigurationExpression<TPMailMessage> m)
			{
				ParameterExpression parameterExpression2;
				m.MapFrom<List<TPMailAttachmentDTO>>(Expression.Lambda<Func<TPMailMessage, List<TPMailAttachmentDTO>>>(Expression.Condition(Expression.OrElse(Expression.Equal(parameterExpression2, Expression.Constant(null, typeof(object))), Expression.Equal(Expression.Property(parameterExpression2, methodof(TPMailMessage.get_Attachments())), Expression.Constant(null, typeof(object)))), Expression.New(typeof(List<TPMailAttachmentDTO>)), Expression.Call(null, methodof(IEnumerable<!!0>.ToList()), new Expression[]
				{
					Expression.Call(null, methodof(IEnumerable<!!0>.Select(Func<!!0, !!1>)), new Expression[]
					{
						Expression.Property(parameterExpression2, methodof(TPMailMessage.get_Attachments())),
						(TPMailAttachment g) => g.ToDTO()
					})
				})), new ParameterExpression[]
				{
					parameterExpression2
				}));
			});
		}

		// Token: 0x060000CF RID: 207 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x00006640 File Offset: 0x00004840
		public static TPMailMessage ToDomainObject(this TPMailMessageDTO tPMailMessageDTO)
		{
			return Mapper.Map<TPMailMessageDTO, TPMailMessage>(tPMailMessageDTO);
		}

		// Token: 0x060000D1 RID: 209 RVA: 0x00006658 File Offset: 0x00004858
		public static TPMailMessageDTO ToDTO(this TPMailMessage tPMailMessage)
		{
			return Mapper.Map<TPMailMessage, TPMailMessageDTO>(tPMailMessage);
		}

		// Token: 0x060000D2 RID: 210 RVA: 0x00006670 File Offset: 0x00004870
		public static IList<TPMailMessage> ToDomainObject(this IList<TPMailMessageDTO> list)
		{
			IList<TPMailMessage> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<TPMailMessage>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x060000D3 RID: 211 RVA: 0x000066B4 File Offset: 0x000048B4
		public static IList<TPMailMessageDTO> ToDTO(this IList<TPMailMessage> list)
		{
			IList<TPMailMessageDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<TPMailMessageDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}

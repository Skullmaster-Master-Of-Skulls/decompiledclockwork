using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.People;
using TechnoPro.Common.Core.Mappers.PersonBase;
using TechnoPro.Common.Public.Entities.People;

namespace TechnoPro.Common.Core.Mappers
{
	// Token: 0x0200000D RID: 13
	public static class PersonBaseMapper
	{
		// Token: 0x06000037 RID: 55 RVA: 0x000033CC File Offset: 0x000015CC
		static PersonBaseMapper()
		{
			GroupMapper.CreateMap();
			Mapper.CreateMap<PersonBaseDTO, PersonBase>().ForMember((PersonBase pb) => (object)pb.Id, delegate(IMemberConfigurationExpression<PersonBaseDTO> m)
			{
				m.Ignore();
			}).ForMember((PersonBase pb) => (object)pb.CoreGroup, delegate(IMemberConfigurationExpression<PersonBaseDTO> m)
			{
				m.MapFrom<eCoreGroup>((PersonBaseDTO pbdto) => (eCoreGroup)pbdto.CoreGroup);
			}).ForMember((PersonBase pb) => pb.Groups, delegate(IMemberConfigurationExpression<PersonBaseDTO> m)
			{
				ParameterExpression parameterExpression2;
				m.MapFrom<List<Group>>(Expression.Lambda<Func<PersonBaseDTO, List<Group>>>(Expression.Condition(Expression.Equal(Expression.Property(parameterExpression2, methodof(PersonBaseDTO.get_Groups())), Expression.Constant(null, typeof(object))), Expression.New(typeof(List<Group>)), Expression.Call(Expression.Property(parameterExpression2, methodof(PersonBaseDTO.get_Groups())), methodof(List<GroupDTO>.ConvertAll(Converter<T, !!0>)), new Expression[]
				{
					(GroupDTO g) => g.ToDomainObject()
				})), new ParameterExpression[]
				{
					parameterExpression2
				}));
			});
			Mapper.CreateMap<PersonBase, PersonBaseDTO>().ForMember((PersonBaseDTO pb) => pb.Tag, delegate(IMemberConfigurationExpression<PersonBase> m)
			{
				m.Ignore();
			}).ForMember((PersonBaseDTO pb) => (object)pb.CoreGroup, delegate(IMemberConfigurationExpression<PersonBase> m)
			{
				m.MapFrom<eCoreGroupDTO>((PersonBase pbdto) => (eCoreGroupDTO)pbdto.CoreGroup);
			}).ForMember((PersonBaseDTO pb) => pb.Groups, delegate(IMemberConfigurationExpression<PersonBase> m)
			{
				ParameterExpression parameterExpression2;
				m.MapFrom<List<GroupDTO>>(Expression.Lambda<Func<PersonBase, List<GroupDTO>>>(Expression.Condition(Expression.Equal(Expression.Property(parameterExpression2, methodof(PersonBase.get_Groups())), Expression.Constant(null, typeof(object))), Expression.New(typeof(List<GroupDTO>)), Expression.Call(Expression.Property(parameterExpression2, methodof(PersonBase.get_Groups())), methodof(List<Group>.ConvertAll(Converter<T, !!0>)), new Expression[]
				{
					(Group g) => g.ToDTO()
				})), new ParameterExpression[]
				{
					parameterExpression2
				}));
			});
		}

		// Token: 0x06000038 RID: 56 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000039 RID: 57 RVA: 0x000035F4 File Offset: 0x000017F4
		public static PersonBase ToDomainObject(this PersonBaseDTO personBaseDTO)
		{
			return Mapper.Map<PersonBaseDTO, PersonBase>(personBaseDTO);
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000360C File Offset: 0x0000180C
		public static PersonBaseDTO ToDTO(this PersonBase personBase)
		{
			return Mapper.Map<PersonBase, PersonBaseDTO>(personBase);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003624 File Offset: 0x00001824
		public static IList<PersonBase> ToDomainObject(this IList<PersonBaseDTO> list)
		{
			IList<PersonBase> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<PersonBase>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00003668 File Offset: 0x00001868
		public static IList<PersonBaseDTO> ToDTO(this IList<PersonBase> list)
		{
			IList<PersonBaseDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<PersonBaseDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}

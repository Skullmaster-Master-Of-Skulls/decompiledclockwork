using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat.BookSearch;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat.BookSearch;

namespace TechnoPro.Common.Core.Mappers.AlternativeFormat.BookSearch
{
	// Token: 0x02000225 RID: 549
	public static class EBookSearchResultMapper
	{
		// Token: 0x06000967 RID: 2407 RVA: 0x0002A0EC File Offset: 0x000282EC
		static EBookSearchResultMapper()
		{
			Mapper.CreateMap<EBookSearchResult, EBookSearchResultDTO>();
			Mapper.CreateMap<EBookSearchResultDTO, EBookSearchResult>();
			Mapper.CreateMap<EBookSearchResultDTO, MediaContentDTO>().ForMember((MediaContentDTO dto) => dto.Identifier, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				ParameterExpression parameterExpression2;
				m.MapFrom<MediaContentIdentifierDTO>(Expression.Lambda<Func<EBookSearchResultDTO, MediaContentIdentifierDTO>>(Expression.MemberInit(Expression.New(typeof(MediaContentIdentifierDTO)), new MemberBinding[]
				{
					Expression.Bind(methodof(MediaContentIdentifierDTO.set_ExternalId(string)), Expression.Property(parameterExpression2, methodof(EBookSearchResultDTO.get_Id()))),
					Expression.Bind(methodof(MediaContentIdentifierDTO.set_ExternalSourceProvider(string)), Expression.Call(Expression.Property(parameterExpression2, methodof(EBookSearchResultDTO.get_SearchEngine())), methodof(object.ToString()), Array.Empty<Expression>())),
					Expression.Bind(methodof(MediaContentIdentifierDTO.set_ISBN(string)), Expression.Property(parameterExpression2, methodof(EBookSearchResultDTO.get_ISBN()))),
					Expression.Bind(methodof(MediaContentIdentifierDTO.set_MediaContentId(int)), Expression.Constant(0, typeof(int))),
					Expression.Bind(methodof(MediaContentIdentifierDTO.set_MediaContentUniqueId(Guid?)), Expression.Constant(null, typeof(Guid?)))
				}), new ParameterExpression[]
				{
					parameterExpression2
				}));
			}).ForMember((MediaContentDTO dto) => dto.ShortTitle, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.MapFrom<string>((EBookSearchResultDTO dto) => dto.Title ?? string.Empty);
			}).ForMember((MediaContentDTO dto) => dto.Authors, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.MapFrom<List<string>>((EBookSearchResultDTO dto) => (dto.Authors != null) ? (from s in dto.Authors
				where !string.IsNullOrWhiteSpace(s)
				select s).ToList<string>() : null);
			}).ForMember((MediaContentDTO dto) => dto.Edition, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.UseValue<string>(string.Empty);
			}).ForMember((MediaContentDTO dto) => (object)dto.ContentCategory, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.UseValue<string>(eMediaContentCategory.AlternateTextBook.ToString());
			}).ForMember((MediaContentDTO dto) => dto.Publisher, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				ParameterExpression parameterExpression2;
				m.MapFrom<MediaPublisherDTO>(Expression.Lambda<Func<EBookSearchResultDTO, MediaPublisherDTO>>(Expression.MemberInit(Expression.New(typeof(MediaPublisherDTO)), new MemberBinding[]
				{
					Expression.Bind(methodof(MediaPublisherDTO.set_Name(string)), Expression.Property(parameterExpression2, methodof(EBookSearchResultDTO.get_Publisher())))
				}), new ParameterExpression[]
				{
					parameterExpression2
				}));
			}).ForMember((MediaContentDTO dto) => (object)dto.PublishedDate, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.MapFrom<DateTime?>((EBookSearchResultDTO dto) => dto.PublisherDate);
			}).ForMember((MediaContentDTO dto) => dto.WebSite, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.MapFrom<string>((EBookSearchResultDTO dto) => dto.Url ?? string.Empty);
			}).ForMember((MediaContentDTO dto) => (object)dto.ProofOfPurchaseRequired, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.UseValue<bool>(true);
			}).ForMember((MediaContentDTO dto) => dto.ThumbnailImageUrl, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.MapFrom<string>((EBookSearchResultDTO dto) => dto.ThumbnailUrl ?? string.Empty);
			}).ForMember((MediaContentDTO dto) => (object)dto.IsThumbnailAvailable, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.MapFrom<bool>((EBookSearchResultDTO dto) => !string.IsNullOrEmpty(dto.ThumbnailUrl));
			}).ForMember((MediaContentDTO dto) => dto.Summary, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.MapFrom<string>((EBookSearchResultDTO dto) => dto.Summary);
			}).ForMember((MediaContentDTO dto) => dto.CourseIdList, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.Ignore();
			}).ForMember((MediaContentDTO dto) => (object)dto.DateCreated, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.UseValue<DateTime>(DateTime.Today);
			}).ForMember((MediaContentDTO dto) => dto.ExternalId, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.Ignore();
			}).ForMember((MediaContentDTO dto) => dto.ExternalSourceProvider, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.Ignore();
			}).ForMember((MediaContentDTO dto) => dto.ISBN, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.Ignore();
			}).ForMember((MediaContentDTO dto) => (object)dto.MediaContentDataID, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.Ignore();
			}).ForMember((MediaContentDTO dto) => (object)dto.MediaContentUniqueId, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.Ignore();
			}).ForMember((MediaContentDTO dto) => (object)dto.IsActive, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.UseValue<bool>(true);
			}).ForMember((MediaContentDTO dto) => dto.Length, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				ParameterExpression parameterExpression2;
				m.MapFrom<string>(Expression.Lambda<Func<EBookSearchResultDTO, string>>(Expression.Call(null, methodof(string + string), new Expression[]
				{
					Expression.Call(Expression.Property(parameterExpression2, methodof(EBookSearchResultDTO.get_PageCount())), methodof(int.ToString()), Array.Empty<Expression>()),
					Expression.Constant(" pages", typeof(string))
				}), new ParameterExpression[]
				{
					parameterExpression2
				}));
			}).ForMember((MediaContentDTO dto) => dto.WhoEntered, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.Ignore();
			}).ForMember((MediaContentDTO dto) => dto.LongTitle, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.MapFrom<string>((EBookSearchResultDTO dto) => dto.Title ?? string.Empty);
			}).ForMember((MediaContentDTO dto) => dto.Notes, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000968 RID: 2408 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000969 RID: 2409 RVA: 0x0002A8D4 File Offset: 0x00028AD4
		public static MediaContentDTO ToMediaContentDTO(this EBookSearchResultDTO dto)
		{
			return Mapper.Map<EBookSearchResultDTO, MediaContentDTO>(dto);
		}

		// Token: 0x0600096A RID: 2410 RVA: 0x0002A8EC File Offset: 0x00028AEC
		public static EBookSearchResult ToDomainObject(this EBookSearchResultDTO dto)
		{
			return Mapper.Map<EBookSearchResultDTO, EBookSearchResult>(dto);
		}

		// Token: 0x0600096B RID: 2411 RVA: 0x0002A904 File Offset: 0x00028B04
		public static EBookSearchResultDTO ToDTO(this EBookSearchResult bo)
		{
			return Mapper.Map<EBookSearchResult, EBookSearchResultDTO>(bo);
		}

		// Token: 0x0600096C RID: 2412 RVA: 0x0002A91C File Offset: 0x00028B1C
		public static IList<EBookSearchResult> ToDomainObject(this IList<EBookSearchResultDTO> list)
		{
			IList<EBookSearchResult> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDomainObject()).ToList<EBookSearchResult>();
			}
			else
			{
				result = null;
			}
			return result;
		}

		// Token: 0x0600096D RID: 2413 RVA: 0x0002A960 File Offset: 0x00028B60
		public static IList<EBookSearchResultDTO> ToDTO(this IList<EBookSearchResult> list)
		{
			IList<EBookSearchResultDTO> result;
			if (list != null)
			{
				result = (from i in list
				select i.ToDTO()).ToList<EBookSearchResultDTO>();
			}
			else
			{
				result = null;
			}
			return result;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using AutoMapper;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat.BookSearch;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.UI.Web.Entity.AlternateFormat;

namespace TechnoPro.Common.UI.Web.Mappers.AlternateFormat
{
	// Token: 0x02000004 RID: 4
	public static class MediaContentWebViewMapper
	{
		// Token: 0x06000007 RID: 7 RVA: 0x000021E4 File Offset: 0x000003E4
		static MediaContentWebViewMapper()
		{
			Mapper.CreateMap<MediaContentDTO, MediaContentWebView>().ForMember((MediaContentWebView wView) => wView.Id, delegate(IMemberConfigurationExpression<MediaContentDTO> m)
			{
				m.Ignore();
			}).ForMember((MediaContentWebView wView) => wView.Identifier, delegate(IMemberConfigurationExpression<MediaContentDTO> m)
			{
				m.MapFrom<MediaContentIdentifierDTO>((MediaContentDTO dto) => dto.Identifier);
			}).ForMember((MediaContentWebView wView) => wView.ShortTitle, delegate(IMemberConfigurationExpression<MediaContentDTO> m)
			{
				m.MapFrom<string>((MediaContentDTO dto) => dto.ShortTitle ?? string.Empty);
			}).ForMember((MediaContentWebView wView) => wView.Authors, delegate(IMemberConfigurationExpression<MediaContentDTO> m)
			{
				m.MapFrom<string>((MediaContentDTO dto) => (dto.Authors != null) ? dto.Authors.CommaSeparatedValues<string>() : string.Empty);
			}).ForMember((MediaContentWebView wView) => wView.Edition, delegate(IMemberConfigurationExpression<MediaContentDTO> m)
			{
				m.MapFrom<string>((MediaContentDTO dto) => dto.Edition ?? string.Empty);
			}).ForMember((MediaContentWebView wView) => wView.ISBN, delegate(IMemberConfigurationExpression<MediaContentDTO> m)
			{
				m.MapFrom<string>((MediaContentDTO dto) => dto.ISBN ?? string.Empty);
			}).ForMember((MediaContentWebView wView) => wView.Courses, delegate(IMemberConfigurationExpression<MediaContentDTO> m)
			{
				m.MapFrom<string>((MediaContentDTO dto) => dto.CourseDescriptions ?? string.Empty);
			}).ForMember((MediaContentWebView wView) => wView.MediaContentCategory, delegate(IMemberConfigurationExpression<MediaContentDTO> m)
			{
				ParameterExpression parameterExpression2;
				m.MapFrom<string>(Expression.Lambda<Func<MediaContentDTO, string>>(Expression.Call(Expression.Property(parameterExpression2, methodof(MediaContentDTO.get_ContentCategory())), methodof(object.ToString()), Array.Empty<Expression>()), new ParameterExpression[]
				{
					parameterExpression2
				}));
			}).ForMember((MediaContentWebView wView) => wView.Publisher, delegate(IMemberConfigurationExpression<MediaContentDTO> m)
			{
				m.MapFrom<string>((MediaContentDTO dto) => (dto.Publisher != null) ? (dto.Publisher.Name ?? string.Empty) : string.Empty);
			}).ForMember((MediaContentWebView wView) => wView.PublisherEmail, delegate(IMemberConfigurationExpression<MediaContentDTO> m)
			{
				m.MapFrom<string>((MediaContentDTO dto) => (dto.Publisher != null) ? (dto.Publisher.Email ?? string.Empty) : string.Empty);
			}).ForMember((MediaContentWebView wView) => wView.PublisherWebsite, delegate(IMemberConfigurationExpression<MediaContentDTO> m)
			{
				m.MapFrom<string>((MediaContentDTO dto) => (dto.Publisher != null) ? (dto.Publisher.Website ?? string.Empty) : string.Empty);
			}).ForMember((MediaContentWebView wView) => wView.PublisherPhone, delegate(IMemberConfigurationExpression<MediaContentDTO> m)
			{
				m.MapFrom<string>((MediaContentDTO dto) => (dto.Publisher != null) ? (dto.Publisher.Phone ?? string.Empty) : string.Empty);
			}).ForMember((MediaContentWebView wView) => wView.PublisherFax, delegate(IMemberConfigurationExpression<MediaContentDTO> m)
			{
				m.MapFrom<string>((MediaContentDTO dto) => (dto.Publisher != null) ? (dto.Publisher.Fax ?? string.Empty) : string.Empty);
			}).ForMember((MediaContentWebView wView) => wView.PublisherAddress, delegate(IMemberConfigurationExpression<MediaContentDTO> m)
			{
				m.MapFrom<string>((MediaContentDTO dto) => (dto.Publisher != null) ? (dto.Publisher.Address ?? string.Empty) : string.Empty);
			}).ForMember((MediaContentWebView wView) => (object)wView.PublishedDate, delegate(IMemberConfigurationExpression<MediaContentDTO> m)
			{
				m.MapFrom<DateTime?>((MediaContentDTO dto) => dto.PublishedDate);
			}).ForMember((MediaContentWebView wView) => wView.Website, delegate(IMemberConfigurationExpression<MediaContentDTO> m)
			{
				m.MapFrom<string>((MediaContentDTO dto) => dto.WebSite ?? string.Empty);
			}).ForMember((MediaContentWebView wView) => (object)wView.ProofOfPurchaseRequired, delegate(IMemberConfigurationExpression<MediaContentDTO> m)
			{
				m.MapFrom<bool>((MediaContentDTO dto) => dto.ProofOfPurchaseRequired);
			}).ForMember((MediaContentWebView wView) => wView.ThumbnailUrl, delegate(IMemberConfigurationExpression<MediaContentDTO> m)
			{
				m.MapFrom<string>((MediaContentDTO dto) => dto.ThumbnailImageUrl);
			}).ForMember((MediaContentWebView wView) => (object)wView.IsThumbnailAvailable, delegate(IMemberConfigurationExpression<MediaContentDTO> m)
			{
				m.MapFrom<bool>((MediaContentDTO dto) => dto.IsThumbnailAvailable);
			}).ForMember((MediaContentWebView wView) => wView.Summary, delegate(IMemberConfigurationExpression<MediaContentDTO> m)
			{
				m.MapFrom<string>((MediaContentDTO dto) => dto.Summary);
			});
			Mapper.CreateMap<EBookSearchResultDTO, MediaContentWebView>().ForMember((MediaContentWebView wView) => wView.Identifier, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.Ignore();
			}).ForMember((MediaContentWebView wView) => wView.Id, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
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
			}).ForMember((MediaContentWebView wView) => wView.ShortTitle, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.MapFrom<string>((EBookSearchResultDTO dto) => dto.Title ?? string.Empty);
			}).ForMember((MediaContentWebView wView) => wView.Authors, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.MapFrom<string>((EBookSearchResultDTO dto) => (dto.Authors != null) ? dto.Authors.CommaSeparatedValues<string>() : string.Empty);
			}).ForMember((MediaContentWebView wView) => wView.Edition, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.UseValue<string>(string.Empty);
			}).ForMember((MediaContentWebView wView) => wView.ISBN, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.MapFrom<string>((EBookSearchResultDTO dto) => dto.ISBN ?? string.Empty);
			}).ForMember((MediaContentWebView wView) => wView.Courses, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.UseValue<string>(string.Empty);
			}).ForMember((MediaContentWebView wView) => wView.MediaContentCategory, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.UseValue<string>(eMediaContentCategory.AlternateTextBook.ToString());
			}).ForMember((MediaContentWebView wView) => wView.Publisher, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.MapFrom<string>((EBookSearchResultDTO dto) => dto.Publisher ?? string.Empty);
			}).ForMember((MediaContentWebView wView) => wView.PublisherEmail, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.UseValue<string>(string.Empty);
			}).ForMember((MediaContentWebView wView) => wView.PublisherWebsite, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.UseValue<string>(string.Empty);
			}).ForMember((MediaContentWebView wView) => wView.PublisherPhone, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.UseValue<string>(string.Empty);
			}).ForMember((MediaContentWebView wView) => wView.PublisherFax, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.UseValue<string>(string.Empty);
			}).ForMember((MediaContentWebView wView) => wView.PublisherAddress, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.UseValue<string>(string.Empty);
			}).ForMember((MediaContentWebView wView) => (object)wView.PublishedDate, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.MapFrom<DateTime?>((EBookSearchResultDTO dto) => dto.PublisherDate);
			}).ForMember((MediaContentWebView wView) => wView.Website, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.MapFrom<string>((EBookSearchResultDTO dto) => dto.Url ?? string.Empty);
			}).ForMember((MediaContentWebView wView) => (object)wView.ProofOfPurchaseRequired, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.UseValue<bool>(true);
			}).ForMember((MediaContentWebView wView) => wView.ThumbnailUrl, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.MapFrom<string>((EBookSearchResultDTO dto) => dto.ThumbnailUrl ?? string.Empty);
			}).ForMember((MediaContentWebView wView) => (object)wView.IsThumbnailAvailable, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.MapFrom<bool>((EBookSearchResultDTO dto) => !string.IsNullOrEmpty(dto.ThumbnailUrl));
			}).ForMember((MediaContentWebView wView) => wView.Summary, delegate(IMemberConfigurationExpression<EBookSearchResultDTO> m)
			{
				m.MapFrom<string>((EBookSearchResultDTO dto) => dto.Summary);
			});
			Mapper.CreateMap<MediaContentWebView, BasicMediaContentDTO>().ForMember((BasicMediaContentDTO dto) => dto.Identifier, delegate(IMemberConfigurationExpression<MediaContentWebView> m)
			{
				m.MapFrom<MediaContentIdentifierDTO>((MediaContentWebView v) => v.Identifier);
			}).ForMember((BasicMediaContentDTO dto) => dto.ShortTitle, delegate(IMemberConfigurationExpression<MediaContentWebView> m)
			{
				m.MapFrom<string>((MediaContentWebView v) => v.ShortTitle);
			}).ForMember((BasicMediaContentDTO dto) => dto.Summary, delegate(IMemberConfigurationExpression<MediaContentWebView> m)
			{
				m.MapFrom<string>((MediaContentWebView v) => v.Summary);
			}).ForMember((BasicMediaContentDTO dto) => dto.Authors, delegate(IMemberConfigurationExpression<MediaContentWebView> m)
			{
				m.MapFrom<IList<string>>((MediaContentWebView v) => v.Authors.SplitValues('\n'));
			}).ForMember((BasicMediaContentDTO dto) => dto.Edition, delegate(IMemberConfigurationExpression<MediaContentWebView> m)
			{
				m.MapFrom<string>((MediaContentWebView v) => v.Edition);
			}).ForMember((BasicMediaContentDTO dto) => (object)dto.PublishedDate, delegate(IMemberConfigurationExpression<MediaContentWebView> m)
			{
				m.MapFrom<DateTime?>((MediaContentWebView v) => v.PublishedDate);
			}).ForMember((BasicMediaContentDTO dto) => dto.Publisher, delegate(IMemberConfigurationExpression<MediaContentWebView> m)
			{
				ParameterExpression parameterExpression2;
				m.MapFrom<MediaPublisherDTO>(Expression.Lambda<Func<MediaContentWebView, MediaPublisherDTO>>(Expression.MemberInit(Expression.New(typeof(MediaPublisherDTO)), new MemberBinding[]
				{
					Expression.Bind(methodof(MediaPublisherDTO.set_PublisherId(int)), Expression.Property(parameterExpression2, methodof(MediaContentWebView.get_PublisherId()))),
					Expression.Bind(methodof(MediaPublisherDTO.set_Name(string)), Expression.Property(parameterExpression2, methodof(MediaContentWebView.get_Publisher()))),
					Expression.Bind(methodof(MediaPublisherDTO.set_Address(string)), Expression.Property(parameterExpression2, methodof(MediaContentWebView.get_PublisherAddress()))),
					Expression.Bind(methodof(MediaPublisherDTO.set_Email(string)), Expression.Property(parameterExpression2, methodof(MediaContentWebView.get_PublisherEmail()))),
					Expression.Bind(methodof(MediaPublisherDTO.set_Fax(string)), Expression.Property(parameterExpression2, methodof(MediaContentWebView.get_PublisherFax()))),
					Expression.Bind(methodof(MediaPublisherDTO.set_Phone(string)), Expression.Property(parameterExpression2, methodof(MediaContentWebView.get_PublisherPhone()))),
					Expression.Bind(methodof(MediaPublisherDTO.set_Website(string)), Expression.Property(parameterExpression2, methodof(MediaContentWebView.get_PublisherWebsite())))
				}), new ParameterExpression[]
				{
					parameterExpression2
				}));
			}).ForMember((BasicMediaContentDTO dto) => (object)dto.ProofOfPurchaseRequired, delegate(IMemberConfigurationExpression<MediaContentWebView> m)
			{
				m.MapFrom<bool>((MediaContentWebView v) => v.ProofOfPurchaseRequired);
			}).ForMember((BasicMediaContentDTO dto) => dto.WebSite, delegate(IMemberConfigurationExpression<MediaContentWebView> m)
			{
				m.MapFrom<string>((MediaContentWebView v) => v.Website);
			}).ForMember((BasicMediaContentDTO dto) => dto.ThumbnailImageUrl, delegate(IMemberConfigurationExpression<MediaContentWebView> m)
			{
				m.MapFrom<string>((MediaContentWebView v) => v.ThumbnailUrl);
			}).ForMember((BasicMediaContentDTO dto) => (object)dto.MediaContentUniqueId, delegate(IMemberConfigurationExpression<MediaContentWebView> m)
			{
				m.Ignore();
			}).ForMember((BasicMediaContentDTO dto) => (object)dto.MediaContentDataID, delegate(IMemberConfigurationExpression<MediaContentWebView> m)
			{
				m.Ignore();
			}).ForMember((BasicMediaContentDTO dto) => dto.ExternalId, delegate(IMemberConfigurationExpression<MediaContentWebView> m)
			{
				m.Ignore();
			}).ForMember((BasicMediaContentDTO dto) => dto.ExternalSourceProvider, delegate(IMemberConfigurationExpression<MediaContentWebView> m)
			{
				m.Ignore();
			});
		}

		// Token: 0x06000008 RID: 8 RVA: 0x00002050 File Offset: 0x00000250
		public static void CreateMap()
		{
		}

		// Token: 0x06000009 RID: 9 RVA: 0x00003317 File Offset: 0x00001517
		public static MediaContentWebView ToWebView(this MediaContentDTO dto)
		{
			return Mapper.Map<MediaContentDTO, MediaContentWebView>(dto);
		}

		// Token: 0x0600000A RID: 10 RVA: 0x0000331F File Offset: 0x0000151F
		public static MediaContentWebView ToWebView(this EBookSearchResultDTO dto)
		{
			return Mapper.Map<EBookSearchResultDTO, MediaContentWebView>(dto);
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00003327 File Offset: 0x00001527
		public static BasicMediaContentDTO ToBasicDTO(this MediaContentWebView view)
		{
			return Mapper.Map<MediaContentWebView, BasicMediaContentDTO>(view);
		}
	}
}

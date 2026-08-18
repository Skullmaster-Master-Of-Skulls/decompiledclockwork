using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using AutoMapper;
using TechnoPro.Common.Public.Entities;
using TechnoPro.Common.Public.Entities.AlternativeFormat;
using TechnoPro.Common.Public.Entities.AlternativeFormat.BookSearch;

namespace TechnoPro.Common.Core.Mappers.AlternativeFormat.BookSearch
{
	// Token: 0x02000227 RID: 551
	public static class BookSearchToMediaContentConverter
	{
		// Token: 0x06000970 RID: 2416 RVA: 0x0002A9E4 File Offset: 0x00028BE4
		static BookSearchToMediaContentConverter()
		{
			Mapper.CreateMap<EBookSearchResult, MediaContent>().ForMember((MediaContent mc) => mc.Identifier, delegate(IMemberConfigurationExpression<EBookSearchResult> m)
			{
				ParameterExpression parameterExpression2;
				m.MapFrom<MediaContentIdentifier>(Expression.Lambda<Func<EBookSearchResult, MediaContentIdentifier>>(Expression.MemberInit(Expression.New(typeof(MediaContentIdentifier)), new MemberBinding[]
				{
					Expression.Bind(methodof(MediaContentIdentifier.set_ExternalId(string)), Expression.Property(parameterExpression2, methodof(BusinessBase<string>.get_Id()))),
					Expression.Bind(methodof(MediaContentIdentifier.set_ISBN(string)), Expression.Property(parameterExpression2, methodof(EBookSearchResult.get_ISBN()))),
					Expression.Bind(methodof(MediaContentIdentifier.set_ExternalSourceProvider(string)), Expression.Call(Expression.Property(parameterExpression2, methodof(EBookSearchResult.get_SearchEngine())), methodof(object.ToString()), Array.Empty<Expression>()))
				}), new ParameterExpression[]
				{
					parameterExpression2
				}));
			}).ForMember((MediaContent mc) => mc.ExternalId, delegate(IMemberConfigurationExpression<EBookSearchResult> m)
			{
				m.Ignore();
			}).ForMember((MediaContent mc) => mc.ExternalSourceProvider, delegate(IMemberConfigurationExpression<EBookSearchResult> m)
			{
				m.Ignore();
			}).ForMember((MediaContent mc) => mc.ISBN, delegate(IMemberConfigurationExpression<EBookSearchResult> m)
			{
				m.Ignore();
			}).ForMember((MediaContent mc) => mc.ShortTitle, delegate(IMemberConfigurationExpression<EBookSearchResult> m)
			{
				m.MapFrom<string>((EBookSearchResult bs) => bs.Title);
			}).ForMember((MediaContent mc) => mc.LongTitle, delegate(IMemberConfigurationExpression<EBookSearchResult> m)
			{
				m.MapFrom<string>((EBookSearchResult bs) => bs.Title);
			}).ForMember((MediaContent mc) => (object)mc.ContentCategory, delegate(IMemberConfigurationExpression<EBookSearchResult> m)
			{
				m.UseValue<eMediaContentCategory>(eMediaContentCategory.AlternateTextBook);
			}).ForMember((MediaContent mc) => mc.Edition, delegate(IMemberConfigurationExpression<EBookSearchResult> m)
			{
				m.Ignore();
			}).ForMember((MediaContent mc) => mc.Length, delegate(IMemberConfigurationExpression<EBookSearchResult> m)
			{
				m.MapFrom<int>((EBookSearchResult bs) => bs.PageCount);
			}).ForMember((MediaContent mc) => (object)mc.PublishedDate, delegate(IMemberConfigurationExpression<EBookSearchResult> m)
			{
				m.MapFrom<DateTime?>((EBookSearchResult bs) => bs.PublisherDate);
			}).ForMember((MediaContent mc) => mc.Authors, delegate(IMemberConfigurationExpression<EBookSearchResult> m)
			{
				m.MapFrom<List<string>>((EBookSearchResult bs) => (bs.Authors != null) ? (from s in bs.Authors
				where !string.IsNullOrWhiteSpace(s)
				select s).ToList<string>() : null);
			}).ForMember((MediaContent mc) => mc.CourseIdList, delegate(IMemberConfigurationExpression<EBookSearchResult> m)
			{
				m.Ignore();
			}).ForMember((MediaContent mc) => mc.Summary, delegate(IMemberConfigurationExpression<EBookSearchResult> m)
			{
				m.MapFrom<string>((EBookSearchResult bs) => bs.Summary);
			}).ForMember((MediaContent mc) => mc.WebSite, delegate(IMemberConfigurationExpression<EBookSearchResult> m)
			{
				m.MapFrom<string>((EBookSearchResult bs) => bs.Url);
			}).ForMember((MediaContent mc) => (object)mc.DateCreated, delegate(IMemberConfigurationExpression<EBookSearchResult> m)
			{
				m.UseValue<DateTime>(DateTime.Today);
			}).ForMember((MediaContent mc) => (object)mc.IsActive, delegate(IMemberConfigurationExpression<EBookSearchResult> m)
			{
				m.UseValue<bool>(true);
			}).ForMember((MediaContent mc) => mc.Notes, delegate(IMemberConfigurationExpression<EBookSearchResult> m)
			{
				m.Ignore();
			}).ForMember((MediaContent mc) => (object)mc.ProofOfPurchaseRequired, delegate(IMemberConfigurationExpression<EBookSearchResult> m)
			{
				m.UseValue<bool>(true);
			}).ForMember((MediaContent mc) => mc.Publisher, delegate(IMemberConfigurationExpression<EBookSearchResult> m)
			{
				m.ResolveUsing<PublisherResolver>();
			}).ForMember((MediaContent mc) => mc.WhoEntered, delegate(IMemberConfigurationExpression<EBookSearchResult> m)
			{
				m.Ignore();
			}).ForMember((MediaContent mc) => (object)mc.MediaContentDataID, delegate(IMemberConfigurationExpression<EBookSearchResult> m)
			{
				m.Ignore();
			}).ForMember((MediaContent mc) => (object)mc.MediaContentUniqueId, delegate(IMemberConfigurationExpression<EBookSearchResult> m)
			{
				m.Ignore();
			}).ForMember((MediaContent mc) => (object)mc.IsThumbnailAvailable, delegate(IMemberConfigurationExpression<EBookSearchResult> m)
			{
				m.Ignore();
			}).ForMember((MediaContent mc) => mc.ThumbnailImageUrl, delegate(IMemberConfigurationExpression<EBookSearchResult> m)
			{
				m.MapFrom<string>((EBookSearchResult bs) => bs.ThumbnailUrl);
			});
		}

		// Token: 0x06000971 RID: 2417 RVA: 0x000020C3 File Offset: 0x000002C3
		public static void CreateMap()
		{
		}

		// Token: 0x06000972 RID: 2418 RVA: 0x0002B1C0 File Offset: 0x000293C0
		public static MediaContent ConvertToMediaContent(this EBookSearchResult bookSearchResult)
		{
			return Mapper.Map<EBookSearchResult, MediaContent>(bookSearchResult);
		}
	}
}

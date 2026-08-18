using System;
using System.Collections.Generic;
using System.Linq;
using AutoMapper;
using TechnoPro.Common.Public.Entities.AlternativeFormat.BookSearch;

namespace TechnoPro.Common.Core.GoogleBooks.Mappers
{
	// Token: 0x02000004 RID: 4
	public static class GoogleBookItemToBookSearchResultMapper
	{
		// Token: 0x06000010 RID: 16 RVA: 0x00002664 File Offset: 0x00000864
		static GoogleBookItemToBookSearchResultMapper()
		{
			Mapper.CreateMap<GoogleBookSearchProvider.Item, EBookSearchResult>().ForMember((EBookSearchResult r) => r.Id, delegate(IMemberConfigurationExpression<GoogleBookSearchProvider.Item> m)
			{
				m.MapFrom<string>((GoogleBookSearchProvider.Item b) => b.id);
			}).ForMember((EBookSearchResult r) => r.Title, delegate(IMemberConfigurationExpression<GoogleBookSearchProvider.Item> m)
			{
				m.MapFrom<string>((GoogleBookSearchProvider.Item b) => (b.volumeInfo != null) ? b.volumeInfo.title : null);
			}).ForMember((EBookSearchResult r) => r.Authors, delegate(IMemberConfigurationExpression<GoogleBookSearchProvider.Item> m)
			{
				m.MapFrom<List<string>>((GoogleBookSearchProvider.Item b) => (b.volumeInfo != null && b.volumeInfo.authors != null) ? (from s in b.volumeInfo.authors
				where !string.IsNullOrWhiteSpace(s)
				select s).ToList<string>() : null);
			}).ForMember((EBookSearchResult r) => r.ISBN, delegate(IMemberConfigurationExpression<GoogleBookSearchProvider.Item> m)
			{
				m.MapFrom<string>((GoogleBookSearchProvider.Item b) => (b.volumeInfo != null && b.volumeInfo.industryIdentifiers != null) ? ((from i in b.volumeInfo.industryIdentifiers
				where i != null && i.type.Equals("ISBN_13")
				select i.identifier).FirstOrDefault<string>() ?? ((from i in b.volumeInfo.industryIdentifiers
				where i != null && i.type.Equals("ISBN_10")
				select i.identifier).FirstOrDefault<string>() ?? string.Empty)) : string.Empty);
			}).ForMember((EBookSearchResult r) => r.Language, delegate(IMemberConfigurationExpression<GoogleBookSearchProvider.Item> m)
			{
				m.MapFrom<string>((GoogleBookSearchProvider.Item b) => (b.volumeInfo != null) ? b.volumeInfo.language : null);
			}).ForMember((EBookSearchResult r) => (object)r.PageCount, delegate(IMemberConfigurationExpression<GoogleBookSearchProvider.Item> m)
			{
				m.MapFrom<int>((GoogleBookSearchProvider.Item b) => (b.volumeInfo != null) ? b.volumeInfo.pageCount : 0);
			}).ForMember((EBookSearchResult r) => r.Publisher, delegate(IMemberConfigurationExpression<GoogleBookSearchProvider.Item> m)
			{
				m.MapFrom<string>((GoogleBookSearchProvider.Item b) => (b.volumeInfo != null) ? b.volumeInfo.publisher : null);
			}).ForMember((EBookSearchResult r) => (object)r.PublisherDate, delegate(IMemberConfigurationExpression<GoogleBookSearchProvider.Item> m)
			{
				m.ResolveUsing<PublishedDateResolver>();
			}).ForMember((EBookSearchResult r) => (object)r.SearchEngine, delegate(IMemberConfigurationExpression<GoogleBookSearchProvider.Item> m)
			{
				m.UseValue<eBookSearchProviderName>(eBookSearchProviderName.Google);
			}).ForMember((EBookSearchResult r) => r.Summary, delegate(IMemberConfigurationExpression<GoogleBookSearchProvider.Item> m)
			{
				m.MapFrom<string>((GoogleBookSearchProvider.Item b) => (b.volumeInfo != null) ? b.volumeInfo.description : null);
			}).ForMember((EBookSearchResult r) => r.ThumbnailUrl, delegate(IMemberConfigurationExpression<GoogleBookSearchProvider.Item> m)
			{
				m.MapFrom<string>((GoogleBookSearchProvider.Item b) => (b.volumeInfo != null && b.volumeInfo.imageLinks != null) ? b.volumeInfo.imageLinks.ThumbnailUrl() : null);
			}).ForMember((EBookSearchResult r) => r.CoverImageUrl, delegate(IMemberConfigurationExpression<GoogleBookSearchProvider.Item> m)
			{
				m.MapFrom<string>((GoogleBookSearchProvider.Item b) => (b.volumeInfo != null && b.volumeInfo.imageLinks != null) ? b.volumeInfo.imageLinks.CoverImageUrl() : null);
			}).ForMember((EBookSearchResult r) => r.Url, delegate(IMemberConfigurationExpression<GoogleBookSearchProvider.Item> m)
			{
				m.MapFrom<string>((GoogleBookSearchProvider.Item b) => (b.volumeInfo != null) ? ((!string.IsNullOrEmpty(b.volumeInfo.previewLink)) ? b.volumeInfo.previewLink : b.volumeInfo.infoLink) : null);
			});
		}

		// Token: 0x06000011 RID: 17 RVA: 0x00002AA0 File Offset: 0x00000CA0
		public static void CreateMap()
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002AA4 File Offset: 0x00000CA4
		internal static EBookSearchResult ToBookSearchResult(this GoogleBookSearchProvider.Item item)
		{
			return Mapper.Map<GoogleBookSearchProvider.Item, EBookSearchResult>(item);
		}
	}
}

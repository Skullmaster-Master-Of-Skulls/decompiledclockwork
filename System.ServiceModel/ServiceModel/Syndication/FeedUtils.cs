using System;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Xml;

namespace System.ServiceModel.Syndication
{
	// Token: 0x0200018C RID: 396
	internal static class FeedUtils
	{
		// Token: 0x06000C65 RID: 3173 RVA: 0x0002CAE0 File Offset: 0x0002ACE0
		public static string AddLineInfo(XmlReader reader, string error)
		{
			IXmlLineInfo xmlLineInfo = reader as IXmlLineInfo;
			if (xmlLineInfo != null && xmlLineInfo.HasLineInfo())
			{
				error = string.Format(CultureInfo.InvariantCulture, "{0} {1}", new object[]
				{
					SR.GetString("ErrorInLine", new object[]
					{
						xmlLineInfo.LineNumber,
						xmlLineInfo.LinePosition
					}),
					SR.GetString(error)
				});
			}
			return error;
		}

		// Token: 0x06000C66 RID: 3174 RVA: 0x0002CB50 File Offset: 0x0002AD50
		internal static Collection<SyndicationCategory> CloneCategories(Collection<SyndicationCategory> categories)
		{
			if (categories == null)
			{
				return null;
			}
			Collection<SyndicationCategory> collection = new NullNotAllowedCollection<SyndicationCategory>();
			for (int i = 0; i < categories.Count; i++)
			{
				collection.Add(categories[i].Clone());
			}
			return collection;
		}

		// Token: 0x06000C67 RID: 3175 RVA: 0x0002CB8C File Offset: 0x0002AD8C
		internal static Collection<SyndicationLink> CloneLinks(Collection<SyndicationLink> links)
		{
			if (links == null)
			{
				return null;
			}
			Collection<SyndicationLink> collection = new NullNotAllowedCollection<SyndicationLink>();
			for (int i = 0; i < links.Count; i++)
			{
				collection.Add(links[i].Clone());
			}
			return collection;
		}

		// Token: 0x06000C68 RID: 3176 RVA: 0x0002CBC8 File Offset: 0x0002ADC8
		internal static Collection<SyndicationPerson> ClonePersons(Collection<SyndicationPerson> persons)
		{
			if (persons == null)
			{
				return null;
			}
			Collection<SyndicationPerson> collection = new NullNotAllowedCollection<SyndicationPerson>();
			for (int i = 0; i < persons.Count; i++)
			{
				collection.Add(persons[i].Clone());
			}
			return collection;
		}

		// Token: 0x06000C69 RID: 3177 RVA: 0x0002CC04 File Offset: 0x0002AE04
		internal static TextSyndicationContent CloneTextContent(TextSyndicationContent content)
		{
			if (content == null)
			{
				return null;
			}
			return (TextSyndicationContent)content.Clone();
		}

		// Token: 0x06000C6A RID: 3178 RVA: 0x0002CC18 File Offset: 0x0002AE18
		internal static Uri CombineXmlBase(Uri rootBase, string newBase)
		{
			if (string.IsNullOrEmpty(newBase))
			{
				return rootBase;
			}
			Uri uri = new Uri(newBase, UriKind.RelativeOrAbsolute);
			if (rootBase == null || uri.IsAbsoluteUri)
			{
				return uri;
			}
			return new Uri(rootBase, newBase);
		}

		// Token: 0x06000C6B RID: 3179 RVA: 0x0002CC54 File Offset: 0x0002AE54
		internal static Uri GetBaseUriToWrite(Uri rootBase, Uri currentBase)
		{
			Uri result;
			if (rootBase == currentBase || currentBase == null)
			{
				result = null;
			}
			else if (rootBase == null)
			{
				result = currentBase;
			}
			else if (rootBase.IsAbsoluteUri && currentBase.IsAbsoluteUri && rootBase.IsBaseOf(currentBase))
			{
				result = rootBase.MakeRelativeUri(currentBase);
			}
			else
			{
				result = currentBase;
			}
			return result;
		}

		// Token: 0x06000C6C RID: 3180 RVA: 0x0002CCAA File Offset: 0x0002AEAA
		internal static string GetUriString(Uri uri)
		{
			if (uri == null)
			{
				return null;
			}
			if (uri.IsAbsoluteUri)
			{
				return uri.AbsoluteUri;
			}
			return uri.ToString();
		}

		// Token: 0x06000C6D RID: 3181 RVA: 0x0002CCCC File Offset: 0x0002AECC
		internal static bool IsXmlns(string name, string ns)
		{
			return name == "xmlns" || ns == "http://www.w3.org/2000/xmlns/";
		}

		// Token: 0x06000C6E RID: 3182 RVA: 0x0002CCE8 File Offset: 0x0002AEE8
		internal static bool IsXmlSchemaType(string name, string ns)
		{
			return name == "type" && ns == "http://www.w3.org/2001/XMLSchema-instance";
		}
	}
}

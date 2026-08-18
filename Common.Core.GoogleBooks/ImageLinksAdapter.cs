using System;

namespace TechnoPro.Common.Core.GoogleBooks
{
	// Token: 0x02000003 RID: 3
	internal static class ImageLinksAdapter
	{
		// Token: 0x0600000E RID: 14 RVA: 0x0000251C File Offset: 0x0000071C
		public static string ThumbnailUrl(this GoogleBookSearchProvider.ImageLinks imgLinks)
		{
			bool flag = !string.IsNullOrEmpty(imgLinks.thumbnail);
			string result;
			if (flag)
			{
				result = imgLinks.thumbnail;
			}
			else
			{
				bool flag2 = !string.IsNullOrEmpty(imgLinks.smallThumbnail);
				if (flag2)
				{
					result = imgLinks.smallThumbnail;
				}
				else
				{
					bool flag3 = !string.IsNullOrEmpty(imgLinks.small);
					if (flag3)
					{
						result = imgLinks.small;
					}
					else
					{
						bool flag4 = !string.IsNullOrEmpty(imgLinks.medium);
						if (flag4)
						{
							result = imgLinks.medium;
						}
						else
						{
							bool flag5 = !string.IsNullOrEmpty(imgLinks.large);
							if (flag5)
							{
								result = imgLinks.large;
							}
							else
							{
								result = string.Empty;
							}
						}
					}
				}
			}
			return result;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000025C0 File Offset: 0x000007C0
		public static string CoverImageUrl(this GoogleBookSearchProvider.ImageLinks imgLinks)
		{
			bool flag = !string.IsNullOrEmpty(imgLinks.large);
			string result;
			if (flag)
			{
				result = imgLinks.large;
			}
			else
			{
				bool flag2 = !string.IsNullOrEmpty(imgLinks.medium);
				if (flag2)
				{
					result = imgLinks.medium;
				}
				else
				{
					bool flag3 = !string.IsNullOrEmpty(imgLinks.small);
					if (flag3)
					{
						result = imgLinks.small;
					}
					else
					{
						bool flag4 = !string.IsNullOrEmpty(imgLinks.thumbnail);
						if (flag4)
						{
							result = imgLinks.thumbnail;
						}
						else
						{
							bool flag5 = !string.IsNullOrEmpty(imgLinks.smallThumbnail);
							if (flag5)
							{
								result = imgLinks.smallThumbnail;
							}
							else
							{
								result = string.Empty;
							}
						}
					}
				}
			}
			return result;
		}
	}
}

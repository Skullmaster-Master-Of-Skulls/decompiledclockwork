using System;
using System.Linq;
using System.Text;
using TechnoPro.ClockWorkServer.Contracts.DTO.AlternativeFormat;
using TechnoPro.Common.IDisplay;
using TechnoPro.Common.Public.Adapters;
using TechnoPro.Common.Public.Entities.AlternativeFormat.Adapters;

namespace TechnoPro.Common.Display.AlternateFormat
{
	// Token: 0x02000004 RID: 4
	public class MediaContentDisplayString : ClockWorkBaseDisplayString<MediaContentDTO>
	{
		// Token: 0x06000008 RID: 8 RVA: 0x0000220C File Offset: 0x0000040C
		protected override string GetHtmlDisplayString(MediaContentDTO mc, DisplayParameters parameters = null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			bool flag = parameters != null && parameters.Contains("InnerTag");
			stringBuilder.Append(flag ? "<div class='lightblueinnersummarybox'>" : "<div class='lightbluesummarybox'>");
			if (mc != null)
			{
				if (parameters == null || parameters.DisplayPropertyList.Contains("MediaContentUniqueId"))
				{
					stringBuilder.AppendFormat("<b>Media Content Id: </b><i>{0}</i><br />", mc.MediaContentUniqueId);
				}
				if ((parameters == null || parameters.DisplayPropertyList.Contains("ShortTitle")) && !string.IsNullOrEmpty(mc.ShortTitle))
				{
					stringBuilder.AppendFormat("<b>Short Title: </b><i>{0}</i><br />", mc.ShortTitle);
				}
				if ((parameters == null || parameters.DisplayPropertyList.Contains("LongTitle")) && !string.IsNullOrEmpty(mc.LongTitle))
				{
					stringBuilder.AppendFormat("<b>Long Title: </b><i>{0}</i><br />", mc.LongTitle);
				}
				if ((parameters == null || parameters.DisplayPropertyList.Contains("Authors")) && mc.Authors != null && mc.Authors.Count > 0)
				{
					stringBuilder.AppendFormat("<b>Authors:</b>", Array.Empty<object>());
					stringBuilder.Append("<ul>");
					foreach (string text in mc.Authors)
					{
						stringBuilder.AppendFormat("<li>{0}</li>", text ?? string.Empty);
					}
					stringBuilder.Append("</ul>");
				}
				if ((parameters == null || parameters.DisplayPropertyList.Contains("Edition")) && !string.IsNullOrEmpty(mc.Edition))
				{
					stringBuilder.AppendFormat("<b>Edition: </b><i>{0}</i><br />", mc.Edition);
				}
				if ((parameters == null || parameters.DisplayPropertyList.Contains("Summary")) && !string.IsNullOrEmpty(mc.Summary))
				{
					stringBuilder.AppendFormat("<b>Summary: </b><i>{0}</i><br />", mc.Summary);
				}
				if ((parameters == null || parameters.DisplayPropertyList.Contains("Publisher")) && mc.Publisher != null && !string.IsNullOrEmpty(mc.Publisher.Name))
				{
					stringBuilder.AppendFormat("<b>Publisher: </b><i>{0}</i><br />", mc.Publisher.Name);
				}
				if ((parameters == null || parameters.DisplayPropertyList.Contains("PublishedDate")) && mc.PublishedDate != null)
				{
					stringBuilder.AppendFormat("<b>Published Date: </b><i>{0}</i><br />", mc.PublishedDate.Value.ToString("MMMM, yyyy"));
				}
				if ((parameters == null || parameters.DisplayPropertyList.Contains("ISBN")) && !string.IsNullOrEmpty(mc.ISBN))
				{
					stringBuilder.AppendFormat("<b>ISBN: </b><i>{0}</i><br />", mc.ISBN.DisplayISBNFormat());
				}
				if ((parameters == null || parameters.DisplayPropertyList.Contains("Length")) && !string.IsNullOrEmpty(mc.Length))
				{
					stringBuilder.AppendFormat("<b>Length: </b><i>{0}</i><br />", mc.Length);
				}
				if ((parameters == null || parameters.DisplayPropertyList.Contains("WebSite")) && !string.IsNullOrEmpty(mc.WebSite))
				{
					stringBuilder.AppendFormat("<b>WebSite: </b><i>{0}</i><br />", mc.WebSite);
				}
				if ((parameters == null || parameters.DisplayPropertyList.Contains("Courses")) && !string.IsNullOrEmpty(mc.CourseDescriptions))
				{
					stringBuilder.AppendFormat("<b>Courses:</b>", Array.Empty<object>());
					stringBuilder.Append("<ul>");
					foreach (string text2 in mc.CourseDescriptions.SplitValues())
					{
						if (text2 != null)
						{
							stringBuilder.AppendFormat("<li>{0}</li>", text2);
						}
					}
					stringBuilder.Append("</ul>");
				}
				if ((parameters == null || parameters.DisplayPropertyList.Contains("AvailableFormats")) && !string.IsNullOrEmpty(mc.AvailableFormats))
				{
					stringBuilder.AppendFormat("<b>Formats:</b>", Array.Empty<object>());
					stringBuilder.Append("<ul>");
					foreach (string text3 in mc.AvailableFormats.SplitValues())
					{
						if (text3 != null)
						{
							stringBuilder.AppendFormat("<li>{0}</li>", text3);
						}
					}
					stringBuilder.Append("</ul>");
				}
				if ((parameters == null || parameters.DisplayPropertyList.Contains("Notes")) && !string.IsNullOrEmpty(mc.Notes))
				{
					stringBuilder.AppendFormat("<b>Notes: </b><i>{0}</i><br />", mc.Notes);
				}
				if (parameters == null || parameters.DisplayPropertyList.Contains("ContentCategory"))
				{
					stringBuilder.AppendFormat("<b>Content Category: </b><i>{0}</i><br />", mc.ContentCategory);
				}
				if ((parameters == null || parameters.DisplayPropertyList.Contains("WhoEntered")) && mc.WhoEntered != null)
				{
					if (!string.IsNullOrEmpty(mc.WhoEntered.FirstName) && !string.IsNullOrEmpty(mc.WhoEntered.LastName))
					{
						stringBuilder.AppendFormat("<b>Who create it: </b><i>{0} {1}</i><br />", mc.WhoEntered.FirstName, mc.WhoEntered.LastName);
					}
					else if (!string.IsNullOrEmpty(mc.WhoEntered.FirstName) || !string.IsNullOrEmpty(mc.WhoEntered.LastName))
					{
						stringBuilder.AppendFormat("<b>Who create it: </b><i>{0}</i><br />", string.IsNullOrEmpty(mc.WhoEntered.LastName) ? mc.WhoEntered.FirstName : mc.WhoEntered.LastName);
					}
				}
			}
			else
			{
				stringBuilder.Append("<i>No media content</i>");
			}
			stringBuilder.Append("</div>");
			return stringBuilder.ToString();
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000021FC File Offset: 0x000003FC
		protected override string GetPlainTextDisplayString(MediaContentDTO mc, DisplayParameters parameters = null)
		{
			return string.Empty;
		}
	}
}

using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Telerik.Web.UI.Common.SerializeJS;

namespace Telerik.Web.UI
{
	// Token: 0x02000927 RID: 2343
	internal class TimelineItemActionConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x0600590C RID: 22796 RVA: 0x0010F92A File Offset: 0x0010DB2A
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600590D RID: 22797 RVA: 0x0010F934 File Offset: 0x0010DB34
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			TimelineItemAction timelineItemAction = (TimelineItemAction)obj;
			new JavaScriptSerializerMarkers();
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (!string.IsNullOrEmpty(timelineItemAction.Text))
			{
				dictionary.Add("text", timelineItemAction.Text);
			}
			if (!string.IsNullOrEmpty(timelineItemAction.Url))
			{
				dictionary.Add("url", timelineItemAction.Url);
			}
			if (timelineItemAction.Attributes.Count > 0)
			{
				dictionary.Add("attributes", timelineItemAction.Attributes);
			}
			return dictionary;
		}

		// Token: 0x17001D5B RID: 7515
		// (get) Token: 0x0600590E RID: 22798 RVA: 0x0010F9B0 File Offset: 0x0010DBB0
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(TimelineItemAction)
				};
			}
		}
	}
}

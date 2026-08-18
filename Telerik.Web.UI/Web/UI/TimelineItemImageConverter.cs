using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using Telerik.Web.UI.Common.SerializeJS;

namespace Telerik.Web.UI
{
	// Token: 0x02000926 RID: 2342
	internal class TimelineItemImageConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06005908 RID: 22792 RVA: 0x0010F898 File Offset: 0x0010DA98
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06005909 RID: 22793 RVA: 0x0010F8A0 File Offset: 0x0010DAA0
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			TimelineItemImage timelineItemImage = (TimelineItemImage)obj;
			new JavaScriptSerializerMarkers();
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			if (!string.IsNullOrEmpty(timelineItemImage.Src))
			{
				dictionary.Add("src", timelineItemImage.Src);
			}
			if (timelineItemImage.Attributes.Count > 0)
			{
				dictionary.Add("attributes", timelineItemImage.Attributes);
			}
			return dictionary;
		}

		// Token: 0x17001D5A RID: 7514
		// (get) Token: 0x0600590A RID: 22794 RVA: 0x0010F900 File Offset: 0x0010DB00
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(TimelineItemImage)
				};
			}
		}
	}
}

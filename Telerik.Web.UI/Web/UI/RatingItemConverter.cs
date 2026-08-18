using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x020019D0 RID: 6608
	internal class RatingItemConverter : JavaScriptConverter
	{
		// Token: 0x0600FF72 RID: 65394 RVA: 0x003953B9 File Offset: 0x003935B9
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600FF73 RID: 65395 RVA: 0x003953C0 File Offset: 0x003935C0
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			RadRatingItem radRatingItem = obj as RadRatingItem;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("value", radRatingItem.Value);
			string toolTip = radRatingItem.ToolTip;
			if (!string.IsNullOrEmpty(toolTip))
			{
				dictionary.Add("tooltip", toolTip);
			}
			string imageUrl = radRatingItem.ImageUrl;
			if (!string.IsNullOrEmpty(imageUrl))
			{
				dictionary.Add("imageUrl", imageUrl);
			}
			string hoveredImageUrl = radRatingItem.HoveredImageUrl;
			if (!string.IsNullOrEmpty(hoveredImageUrl))
			{
				dictionary.Add("hoveredImageUrl", hoveredImageUrl);
			}
			string selectedImageUrl = radRatingItem.SelectedImageUrl;
			if (!string.IsNullOrEmpty(selectedImageUrl))
			{
				dictionary.Add("selectedImageUrl", selectedImageUrl);
			}
			string hoveredSelectedImageUrl = radRatingItem.HoveredSelectedImageUrl;
			if (!string.IsNullOrEmpty(hoveredSelectedImageUrl))
			{
				dictionary.Add("hoveredSelectedImageUrl", hoveredSelectedImageUrl);
			}
			return dictionary;
		}

		// Token: 0x17004D15 RID: 19733
		// (get) Token: 0x0600FF74 RID: 65396 RVA: 0x00395550 File Offset: 0x00393750
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RadRatingItem);
				yield break;
			}
		}
	}
}

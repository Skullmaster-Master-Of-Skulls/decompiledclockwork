using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000625 RID: 1573
	internal class NavigationNodeJavaScriptConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x06003953 RID: 14675 RVA: 0x000BC165 File Offset: 0x000BA365
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06003954 RID: 14676 RVA: 0x000BC16C File Offset: 0x000BA36C
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			NavigationNode navigationNode = (NavigationNode)obj;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			ExplicitJavaScriptConverter.AddProperty(dictionary, "text", navigationNode.Text, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "enabled", navigationNode.Enabled, true);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "visible", navigationNode.Visible, true);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "selected", navigationNode.Selected, false);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "navigateUrl", navigationNode.NavigateUrl, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "target", navigationNode.Target, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "imageUrl", navigationNode.ImageUrl, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "selectedImageUrl", navigationNode.SelectedImageUrl, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "hoveredImageUrl", navigationNode.HoveredImageUrl, string.Empty);
			ExplicitJavaScriptConverter.AddProperty(dictionary, "disabledImageUrl", navigationNode.DisabledImageUrl, string.Empty);
			if (navigationNode.ContentTemplate != null)
			{
				dictionary.Add("hasContentTemplate", true);
			}
			if (navigationNode.TemplateData != null)
			{
				foreach (KeyValuePair<string, object> keyValuePair in navigationNode.TemplateData)
				{
					dictionary.Add(keyValuePair.Key, keyValuePair.Value);
				}
			}
			if (navigationNode.Nodes.Count > 0)
			{
				dictionary.Add("nodes", navigationNode.Nodes);
			}
			return dictionary;
		}

		// Token: 0x170012E0 RID: 4832
		// (get) Token: 0x06003955 RID: 14677 RVA: 0x000BC3D4 File Offset: 0x000BA5D4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(NavigationNode);
				yield break;
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001AF3 RID: 6899
	internal class AttributeCollectionConverter : JavaScriptConverter
	{
		// Token: 0x06010B2B RID: 68395 RVA: 0x003B7ADE File Offset: 0x003B5CDE
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06010B2C RID: 68396 RVA: 0x003B7AE8 File Offset: 0x003B5CE8
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			AttributeCollection attributeCollection = obj as AttributeCollection;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			foreach (object obj2 in attributeCollection.Keys)
			{
				string text = (string)obj2;
				if (!HtmlAttributes.IsHtmlAttribute(text))
				{
					dictionary.Add(text, attributeCollection[text]);
				}
			}
			return dictionary;
		}

		// Token: 0x17005142 RID: 20802
		// (get) Token: 0x06010B2D RID: 68397 RVA: 0x003B7C30 File Offset: 0x003B5E30
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(AttributeCollection);
				yield break;
			}
		}
	}
}

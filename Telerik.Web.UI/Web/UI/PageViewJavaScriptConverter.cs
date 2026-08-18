using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x02001AD1 RID: 6865
	internal class PageViewJavaScriptConverter : JavaScriptConverter
	{
		// Token: 0x060109F7 RID: 68087 RVA: 0x003B5715 File Offset: 0x003B3915
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x060109F8 RID: 68088 RVA: 0x003B571C File Offset: 0x003B391C
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			RadPageView radPageView = obj as RadPageView;
			IDictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary.Add("id", radPageView.ClientID);
			if (!string.IsNullOrEmpty(radPageView.DefaultButton))
			{
				Control control = radPageView.FindControl(radPageView.DefaultButton);
				if (control == null)
				{
					throw new Exception(string.Format("Unable to find default button with ID={0} for PageView with ID={1}", radPageView.DefaultButton, radPageView.ID));
				}
				dictionary.Add("defaultButton", control.ClientID);
			}
			if (!string.IsNullOrEmpty(radPageView.ContentUrl))
			{
				dictionary.Add("contentUrl", radPageView.ResolveUrl(radPageView.ContentUrl));
			}
			return dictionary;
		}

		// Token: 0x170050D1 RID: 20689
		// (get) Token: 0x060109F9 RID: 68089 RVA: 0x003B5884 File Offset: 0x003B3A84
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(RadPageView);
				yield break;
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02000B06 RID: 2822
	internal class ODataSettingsConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060069AE RID: 27054 RVA: 0x0018D240 File Offset: 0x0018B440
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			ODataSettings odataSettings = obj as ODataSettings;
			if (odataSettings == null)
			{
				throw new InvalidOperationException("Can serialize only ODataSettings objects.");
			}
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			foreach (ODataEntityType odataEntityType in odataSettings.Entities)
			{
				ExplicitJavaScriptConverter.AddProperty(dictionary2, "Name", odataEntityType.Name, string.Empty);
				ExplicitJavaScriptConverter.AddProperty(dictionary2, "DataValueField", odataEntityType.DataValueField, string.Empty);
				ExplicitJavaScriptConverter.AddProperty(dictionary2, "DatTextField", odataEntityType.DataTextField, string.Empty);
				ExplicitJavaScriptConverter.AddProperty(dictionary2, "NavigationProperty", odataEntityType.NavigationProperty, string.Empty);
			}
			dictionary.Add("Entities", dictionary2);
			dictionary.Add("ResponseType", odataSettings.ResponseType);
			return dictionary;
		}

		// Token: 0x1700229F RID: 8863
		// (get) Token: 0x060069AF RID: 27055 RVA: 0x0018D32C File Offset: 0x0018B52C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ODataSettings)
				};
			}
		}
	}
}

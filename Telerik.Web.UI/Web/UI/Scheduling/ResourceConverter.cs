using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Scheduling
{
	// Token: 0x020011C5 RID: 4549
	internal class ResourceConverter : EditorConverterBase
	{
		// Token: 0x0600BBFB RID: 48123 RVA: 0x0029A862 File Offset: 0x00298A62
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600BBFC RID: 48124 RVA: 0x0029A86C File Offset: 0x00298A6C
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Resource resource = obj as Resource;
			if (resource == null)
			{
				throw new ArgumentException("Can serialize only Resource objects.");
			}
			IDictionary<string, object> dictionary = base.Serialize(obj, serializer);
			dictionary["internalKey"] = LosSerializer.Serialize(dictionary["key"]);
			SchedulerAttributeCollectionConverter schedulerAttributeCollectionConverter = new SchedulerAttributeCollectionConverter();
			IDictionary<string, object> dictionary2 = schedulerAttributeCollectionConverter.Serialize(resource.Attributes, serializer);
			if (dictionary2.Count > 0)
			{
				dictionary.Add("attributes", dictionary2);
			}
			return dictionary;
		}

		// Token: 0x17003CD8 RID: 15576
		// (get) Token: 0x0600BBFD RID: 48125 RVA: 0x0029A8E4 File Offset: 0x00298AE4
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Resource)
				};
			}
		}
	}
}

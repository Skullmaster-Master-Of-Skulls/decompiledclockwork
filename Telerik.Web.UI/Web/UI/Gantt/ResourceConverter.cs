using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000302 RID: 770
	internal class ResourceConverter : JavaScriptConverter
	{
		// Token: 0x06001A45 RID: 6725 RVA: 0x00055639 File Offset: 0x00053839
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A46 RID: 6726 RVA: 0x00055640 File Offset: 0x00053840
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Resource resource = obj as Resource;
			if (resource == null)
			{
				throw new NotSupportedException("Only instances of IResource can be serialized.");
			}
			return resource.GetSerializationData();
		}

		// Token: 0x170008D6 RID: 2262
		// (get) Token: 0x06001A47 RID: 6727 RVA: 0x00055668 File Offset: 0x00053868
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

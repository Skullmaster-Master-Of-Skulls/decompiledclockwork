using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000300 RID: 768
	internal class DependencyConverter : JavaScriptConverter
	{
		// Token: 0x06001A3D RID: 6717 RVA: 0x0005549A File Offset: 0x0005369A
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A3E RID: 6718 RVA: 0x000554A4 File Offset: 0x000536A4
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dependency dependency = obj as Dependency;
			if (dependency == null)
			{
				throw new NotSupportedException("Only instances of IDependency can be serialized.");
			}
			return dependency.GetSerializationData();
		}

		// Token: 0x170008D4 RID: 2260
		// (get) Token: 0x06001A3F RID: 6719 RVA: 0x000554CC File Offset: 0x000536CC
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Dependency)
				};
			}
		}
	}
}

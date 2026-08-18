using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x020002FD RID: 765
	internal class AssignmentConverter : JavaScriptConverter
	{
		// Token: 0x06001A31 RID: 6705 RVA: 0x000551EF File Offset: 0x000533EF
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A32 RID: 6706 RVA: 0x000551F8 File Offset: 0x000533F8
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Assignment assignment = obj as Assignment;
			if (assignment == null)
			{
				throw new NotSupportedException("Only instances of IAssignment can be serialized.");
			}
			return assignment.GetSerializationData();
		}

		// Token: 0x170008D1 RID: 2257
		// (get) Token: 0x06001A33 RID: 6707 RVA: 0x00055220 File Offset: 0x00053420
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Assignment)
				};
			}
		}
	}
}

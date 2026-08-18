using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x0200030B RID: 779
	internal class TaskConverter : JavaScriptConverter
	{
		// Token: 0x06001A6A RID: 6762 RVA: 0x00056502 File Offset: 0x00054702
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06001A6B RID: 6763 RVA: 0x0005650C File Offset: 0x0005470C
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Task task = obj as Task;
			if (task == null)
			{
				throw new NotSupportedException("Only instances of ITask can be serialized.");
			}
			return task.GetSerializationData();
		}

		// Token: 0x170008DF RID: 2271
		// (get) Token: 0x06001A6C RID: 6764 RVA: 0x00056534 File Offset: 0x00054734
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Task)
				};
			}
		}
	}
}

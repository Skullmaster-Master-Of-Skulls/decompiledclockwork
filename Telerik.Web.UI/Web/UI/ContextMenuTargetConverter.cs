using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x02001B3C RID: 6972
	internal class ContextMenuTargetConverter : JavaScriptConverter
	{
		// Token: 0x06010DC4 RID: 69060 RVA: 0x003BD960 File Offset: 0x003BBB60
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06010DC5 RID: 69061 RVA: 0x003BD968 File Offset: 0x003BBB68
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Type type = obj.GetType();
			if (type == typeof(ContextMenuControlTarget))
			{
				dictionary["id"] = ((ContextMenuControlTarget)obj).ControlID;
			}
			if (type == typeof(ContextMenuElementTarget))
			{
				dictionary["id"] = ((ContextMenuElementTarget)obj).ElementID;
			}
			if (type == typeof(ContextMenuTagNameTarget))
			{
				dictionary["tagName"] = ((ContextMenuTagNameTarget)obj).TagName;
			}
			dictionary["type"] = ((ContextMenuTarget)obj).Type;
			return dictionary;
		}

		// Token: 0x17005231 RID: 21041
		// (get) Token: 0x06010DC6 RID: 69062 RVA: 0x003BDA18 File Offset: 0x003BBC18
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(ContextMenuControlTarget),
					typeof(ContextMenuDocumentTarget),
					typeof(ContextMenuElementTarget),
					typeof(ContextMenuTagNameTarget)
				};
			}
		}
	}
}

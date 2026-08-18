using System;
using System.Collections.Generic;
using System.Drawing;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Common
{
	// Token: 0x020019DC RID: 6620
	internal class JavaScriptColorConverter : JavaScriptConverter
	{
		// Token: 0x0601004D RID: 65613 RVA: 0x00397A29 File Offset: 0x00395C29
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new Exception("The method or operation is not implemented.");
		}

		// Token: 0x0601004E RID: 65614 RVA: 0x00397A38 File Offset: 0x00395C38
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Color c = (Color)obj;
			dictionary["hex"] = ColorTranslator.ToHtml(c);
			return dictionary;
		}

		// Token: 0x17004D58 RID: 19800
		// (get) Token: 0x0601004F RID: 65615 RVA: 0x00397A64 File Offset: 0x00395C64
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(Color)
				};
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.AsyncUpload
{
	// Token: 0x0200006C RID: 108
	public class FileFilterConverter : JavaScriptConverter
	{
		// Token: 0x06000471 RID: 1137 RVA: 0x0000B847 File Offset: 0x00009A47
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06000472 RID: 1138 RVA: 0x0000B850 File Offset: 0x00009A50
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			FileFilterCollection fileFilterCollection = obj as FileFilterCollection;
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Stack<Dictionary<string, object>> stack = new Stack<Dictionary<string, object>>();
			foreach (object obj2 in fileFilterCollection)
			{
				FileFilter fileFilter = (FileFilter)obj2;
				stack.Push(new Dictionary<string, object>
				{
					{
						"Description",
						fileFilter.Description
					},
					{
						"Extensions",
						FileFilter.GetFilter(fileFilter.Extensions, false)
					}
				});
			}
			dictionary["values"] = stack;
			return dictionary;
		}

		// Token: 0x170001A9 RID: 425
		// (get) Token: 0x06000473 RID: 1139 RVA: 0x0000B900 File Offset: 0x00009B00
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(FileFilterCollection)
				};
			}
		}
	}
}

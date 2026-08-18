using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics.CodeAnalysis;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Scheduler.OData
{
	// Token: 0x02000E61 RID: 3681
	internal class ODataResourceConverter : JavaScriptConverter
	{
		// Token: 0x17002C29 RID: 11305
		// (get) Token: 0x06008BB2 RID: 35762 RVA: 0x001FC09C File Offset: 0x001FA29C
		// (set) Token: 0x06008BB3 RID: 35763 RVA: 0x001FC0A4 File Offset: 0x001FA2A4
		public ODataResourceType ResourceType { get; set; }

		// Token: 0x06008BB4 RID: 35764 RVA: 0x001FC0AD File Offset: 0x001FA2AD
		public ODataResourceConverter(ODataResourceType resourceType)
		{
			this.ResourceType = resourceType;
		}

		// Token: 0x06008BB5 RID: 35765 RVA: 0x001FC0BC File Offset: 0x001FA2BC
		private string TryGetValue(string key, IDictionary<string, object> dict)
		{
			object obj = "";
			if (!string.IsNullOrEmpty(key) && dict.TryGetValue(key, out obj))
			{
				return obj.ToString();
			}
			return "";
		}

		// Token: 0x06008BB6 RID: 35766 RVA: 0x001FC0EE File Offset: 0x001FA2EE
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x06008BB7 RID: 35767 RVA: 0x001FC0F8 File Offset: 0x001FA2F8
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			List<ResourceData> list = new List<ResourceData>();
			if (dictionary.ContainsKey("d"))
			{
				object obj = dictionary["d"];
				ArrayList arrayList = obj as ArrayList;
				if (arrayList == null && (obj as IDictionary<string, object>).ContainsKey("results"))
				{
					arrayList = ((obj as IDictionary<string, object>)["results"] as ArrayList);
					if (arrayList == null)
					{
						return list;
					}
				}
				foreach (object obj2 in arrayList)
				{
					IDictionary<string, object> dict = (IDictionary<string, object>)obj2;
					list.Add(new ResourceData
					{
						Available = true,
						Key = this.TryGetValue(this.ResourceType.KeyField, dict),
						Text = this.TryGetValue(this.ResourceType.TextField, dict),
						Type = this.ResourceType.Name
					});
				}
				return list;
			}
			return list;
		}

		// Token: 0x17002C2A RID: 11306
		// (get) Token: 0x06008BB8 RID: 35768 RVA: 0x001FC2D0 File Offset: 0x001FA4D0
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				yield return typeof(List<ResourceData>);
				yield break;
			}
		}

		// Token: 0x0400271E RID: 10014
		public const string OData_Version1_Key = "d";

		// Token: 0x0400271F RID: 10015
		public const string OData_Version2_Key = "results";
	}
}

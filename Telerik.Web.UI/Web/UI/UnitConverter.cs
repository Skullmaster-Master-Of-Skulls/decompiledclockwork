using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02000FB7 RID: 4023
	internal class UnitConverter : JavaScriptConverter
	{
		// Token: 0x06009B3C RID: 39740 RVA: 0x00228C94 File Offset: 0x00226E94
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			if (dictionary == null)
			{
				throw new ArgumentNullException("dictionary");
			}
			if (!object.ReferenceEquals(type, typeof(Unit)) || dictionary.Count <= 0)
			{
				return null;
			}
			if ((bool)dictionary["IsEmpty"])
			{
				return Unit.Empty;
			}
			double value = Convert.ToDouble(dictionary["Value"]);
			UnitType type2 = (UnitType)Enum.Parse(typeof(UnitType), dictionary["Type"].ToString());
			Unit unit = new Unit(value, type2);
			return unit;
		}

		// Token: 0x06009B3D RID: 39741 RVA: 0x00228D30 File Offset: 0x00226F30
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Unit unit = (Unit)obj;
			return new Dictionary<string, object>
			{
				{
					"Type",
					unit.Type
				},
				{
					"Value",
					unit.Value
				},
				{
					"IsEmpty",
					unit.IsEmpty
				}
			};
		}

		// Token: 0x17003123 RID: 12579
		// (get) Token: 0x06009B3E RID: 39742 RVA: 0x00228D90 File Offset: 0x00226F90
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new ReadOnlyCollection<Type>(new List<Type>(new Type[]
				{
					typeof(Unit)
				}));
			}
		}
	}
}

using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.PersistenceFramework
{
	// Token: 0x02000499 RID: 1177
	internal class UnitConverter : JavaScriptConverter
	{
		// Token: 0x060029DD RID: 10717 RVA: 0x00086C58 File Offset: 0x00084E58
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			if (dictionary == null)
			{
				throw new PersistenceFrameworkArgumentNullException("dictionary");
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

		// Token: 0x060029DE RID: 10718 RVA: 0x00086CF4 File Offset: 0x00084EF4
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

		// Token: 0x17000D8F RID: 3471
		// (get) Token: 0x060029DF RID: 10719 RVA: 0x00086D54 File Offset: 0x00084F54
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

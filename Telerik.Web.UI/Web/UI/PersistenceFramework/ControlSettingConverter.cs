using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Drawing;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.PersistenceFramework
{
	// Token: 0x02000485 RID: 1157
	internal class ControlSettingConverter : JavaScriptConverter
	{
		// Token: 0x06002949 RID: 10569 RVA: 0x00085460 File Offset: 0x00083660
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			if (dictionary == null)
			{
				throw new PersistenceFrameworkArgumentNullException("dictionary");
			}
			if (object.ReferenceEquals(type, typeof(ControlSetting)) && dictionary.Count > 0)
			{
				ControlSetting controlSetting = default(ControlSetting);
				controlSetting.Name = (string)dictionary["Name"];
				Type targetType;
				if ((string)dictionary["Type"] == typeof(Unit).FullName)
				{
					targetType = typeof(Unit);
				}
				else if ((string)dictionary["Type"] == typeof(List<Unit>).FullName)
				{
					targetType = typeof(List<Unit>);
				}
				else if ((string)dictionary["Type"] == typeof(Unit[]).FullName)
				{
					targetType = typeof(Unit[]);
				}
				else if ((string)dictionary["Type"] == typeof(Color).FullName)
				{
					targetType = typeof(Color);
				}
				else
				{
					targetType = Type.GetType((string)dictionary["Type"]);
				}
				controlSetting.Value = serializer.Deserialize((string)dictionary["Value"], targetType);
				return controlSetting;
			}
			return null;
		}

		// Token: 0x0600294A RID: 10570 RVA: 0x000855C8 File Offset: 0x000837C8
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			ControlSetting controlSetting = (ControlSetting)obj;
			if (!object.Equals(controlSetting, null))
			{
				return new Dictionary<string, object>
				{
					{
						"Name",
						controlSetting.Name
					},
					{
						"Type",
						controlSetting.Value.GetType().FullName
					},
					{
						"Value",
						serializer.Serialize(controlSetting.Value)
					}
				};
			}
			return new Dictionary<string, object>();
		}

		// Token: 0x17000D5D RID: 3421
		// (get) Token: 0x0600294B RID: 10571 RVA: 0x00085640 File Offset: 0x00083840
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new ReadOnlyCollection<Type>(new List<Type>(new Type[]
				{
					typeof(ControlSetting)
				}));
			}
		}
	}
}

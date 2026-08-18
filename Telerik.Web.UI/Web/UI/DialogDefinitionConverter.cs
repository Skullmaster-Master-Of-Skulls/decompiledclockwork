using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI
{
	// Token: 0x02001031 RID: 4145
	internal class DialogDefinitionConverter : JavaScriptConverter
	{
		// Token: 0x17003392 RID: 13202
		// (get) Token: 0x0600A354 RID: 41812 RVA: 0x00245558 File Offset: 0x00243758
		private bool SerializeParameters
		{
			get
			{
				return this._serializeParameters;
			}
		}

		// Token: 0x0600A355 RID: 41813 RVA: 0x00245560 File Offset: 0x00243760
		public DialogDefinitionConverter(bool serializeParameters)
		{
			this._serializeParameters = serializeParameters;
		}

		// Token: 0x0600A356 RID: 41814 RVA: 0x0024556F File Offset: 0x0024376F
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600A357 RID: 41815 RVA: 0x00245578 File Offset: 0x00243778
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Type typeFromHandle = typeof(DialogDefinition);
			foreach (PropertyInfo propertyInfo in typeFromHandle.GetProperties(BindingFlags.Instance | BindingFlags.Public))
			{
				if (propertyInfo.GetCustomAttributes(typeof(ScriptIgnoreAttribute), true).Length <= 0 && (!(propertyInfo.Name == "SerializedParameters") || this.SerializeParameters))
				{
					object obj2 = propertyInfo.GetValue(obj, null);
					if (obj2 != null)
					{
						DefaultValueAttribute[] array = (DefaultValueAttribute[])propertyInfo.GetCustomAttributes(typeof(DefaultValueAttribute), true);
						if (array.Length <= 0 || !obj2.Equals(array[0].Value))
						{
							ClientPropertyNameAttribute[] array2 = (ClientPropertyNameAttribute[])propertyInfo.GetCustomAttributes(typeof(ClientPropertyNameAttribute), true);
							if (propertyInfo.PropertyType == typeof(Unit))
							{
								obj2 = obj2.ToString();
							}
							dictionary[(array2.Length == 0) ? propertyInfo.Name : array2[0].PropertyName] = obj2;
						}
					}
				}
			}
			return dictionary;
		}

		// Token: 0x17003393 RID: 13203
		// (get) Token: 0x0600A358 RID: 41816 RVA: 0x0024568C File Offset: 0x0024388C
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(DialogDefinition)
				};
			}
		}

		// Token: 0x04002D6B RID: 11627
		private readonly bool _serializeParameters;
	}
}

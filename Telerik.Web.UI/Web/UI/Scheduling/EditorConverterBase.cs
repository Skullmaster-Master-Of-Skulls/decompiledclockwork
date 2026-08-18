using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace Telerik.Web.UI.Scheduling
{
	// Token: 0x020011C3 RID: 4547
	internal abstract class EditorConverterBase : JavaScriptConverter
	{
		// Token: 0x0600BBED RID: 48109 RVA: 0x0029A207 File Offset: 0x00298407
		protected virtual bool ShouldSkipProperty(PropertyInfo property, object value, object obj)
		{
			return value == null || this.HasScriptIgnoreAttribute(property) || this.IsDefaultValue(property, value);
		}

		// Token: 0x0600BBEE RID: 48110 RVA: 0x0029A220 File Offset: 0x00298420
		protected virtual object GetValue(PropertyInfo property, object obj)
		{
			object value = property.GetValue(obj, null);
			if (property.PropertyType == typeof(Unit))
			{
				return value.ToString();
			}
			if (!(property.PropertyType == typeof(System.Web.UI.AttributeCollection)))
			{
				return value;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			System.Web.UI.AttributeCollection attributeCollection = value as System.Web.UI.AttributeCollection;
			if (attributeCollection != null)
			{
				foreach (object obj2 in attributeCollection.Keys)
				{
					string text = (string)obj2;
					dictionary[text.ToLowerInvariant()] = attributeCollection[text];
				}
			}
			if (dictionary.Count != 0)
			{
				return dictionary;
			}
			return null;
		}

		// Token: 0x0600BBEF RID: 48111 RVA: 0x0029A2E8 File Offset: 0x002984E8
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			Type type = obj.GetType();
			foreach (PropertyInfo property in type.GetProperties(BindingFlags.Instance | BindingFlags.Public))
			{
				object value = this.GetValue(property, obj);
				if (!this.ShouldSkipProperty(property, value, obj))
				{
					dictionary[this.GetPropertyName(property)] = value;
				}
			}
			return dictionary;
		}

		// Token: 0x0600BBF0 RID: 48112 RVA: 0x0029A348 File Offset: 0x00298548
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600BBF1 RID: 48113 RVA: 0x0029A350 File Offset: 0x00298550
		protected bool IsDefaultValue(PropertyInfo property, object value)
		{
			DefaultValueAttribute[] array = (DefaultValueAttribute[])property.GetCustomAttributes(typeof(DefaultValueAttribute), true);
			return array.Length > 0 && value.Equals(array[0].Value);
		}

		// Token: 0x0600BBF2 RID: 48114 RVA: 0x0029A38A File Offset: 0x0029858A
		protected bool HasScriptIgnoreAttribute(PropertyInfo property)
		{
			return property.GetCustomAttributes(typeof(ScriptIgnoreAttribute), true).Length > 0;
		}

		// Token: 0x0600BBF3 RID: 48115 RVA: 0x0029A3A4 File Offset: 0x002985A4
		protected virtual string GetPropertyName(PropertyInfo property)
		{
			ClientPropertyNameAttribute[] array = (ClientPropertyNameAttribute[])property.GetCustomAttributes(typeof(ClientPropertyNameAttribute), true);
			string result;
			if (array.Length == 0)
			{
				result = property.Name[0].ToString().ToLowerInvariant() + property.Name.Substring(1);
			}
			else
			{
				result = array[0].PropertyName;
			}
			return result;
		}
	}
}

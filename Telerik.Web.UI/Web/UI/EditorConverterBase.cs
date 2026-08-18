using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Web.UI.WebControls;
using Telerik.Web.UI.Editor;

namespace Telerik.Web.UI
{
	// Token: 0x020002BB RID: 699
	internal abstract class EditorConverterBase : JavaScriptConverter
	{
		// Token: 0x0600186A RID: 6250 RVA: 0x00050725 File Offset: 0x0004E925
		protected virtual bool ShouldSkipProperty(PropertyInfo _property, object value, object obj)
		{
			return value == null || this.HasScriptIgnoreAttribute(_property) || this.IsDefaultValue(_property, value);
		}

		// Token: 0x0600186B RID: 6251 RVA: 0x00050740 File Offset: 0x0004E940
		protected virtual object GetValue(PropertyInfo _property, object obj)
		{
			object value = _property.GetValue(obj, null);
			if (_property.PropertyType == typeof(Unit))
			{
				return value.ToString();
			}
			if (!(_property.PropertyType == typeof(Telerik.Web.UI.Editor.AttributeCollection)))
			{
				return value;
			}
			Dictionary<string, string> dictionary = new Dictionary<string, string>();
			Telerik.Web.UI.Editor.AttributeCollection attributeCollection = (Telerik.Web.UI.Editor.AttributeCollection)value;
			foreach (object obj2 in attributeCollection.Keys)
			{
				string text = (string)obj2;
				dictionary[text.ToLowerInvariant()] = attributeCollection[text];
			}
			if (dictionary.Count != 0)
			{
				return dictionary;
			}
			return null;
		}

		// Token: 0x0600186C RID: 6252 RVA: 0x00050804 File Offset: 0x0004EA04
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

		// Token: 0x0600186D RID: 6253 RVA: 0x00050864 File Offset: 0x0004EA64
		public override object Deserialize(IDictionary<string, object> dictionary, Type type, JavaScriptSerializer serializer)
		{
			throw new NotImplementedException();
		}

		// Token: 0x0600186E RID: 6254 RVA: 0x0005086C File Offset: 0x0004EA6C
		protected bool IsDefaultValue(PropertyInfo _property, object value)
		{
			DefaultValueAttribute[] array = (DefaultValueAttribute[])_property.GetCustomAttributes(typeof(DefaultValueAttribute), true);
			return array.Length > 0 && value.Equals(array[0].Value);
		}

		// Token: 0x0600186F RID: 6255 RVA: 0x000508A6 File Offset: 0x0004EAA6
		protected bool HasScriptIgnoreAttribute(PropertyInfo _property)
		{
			return _property.GetCustomAttributes(typeof(ScriptIgnoreAttribute), true).Length > 0;
		}

		// Token: 0x06001870 RID: 6256 RVA: 0x000508C0 File Offset: 0x0004EAC0
		protected virtual string GetPropertyName(PropertyInfo _property)
		{
			ClientPropertyNameAttribute[] array = (ClientPropertyNameAttribute[])_property.GetCustomAttributes(typeof(ClientPropertyNameAttribute), true);
			string result = string.Empty;
			if (array.Length == 0)
			{
				result = _property.Name[0].ToString().ToLowerInvariant() + _property.Name.Substring(1);
			}
			else
			{
				result = array[0].PropertyName;
			}
			return result;
		}
	}
}

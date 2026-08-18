using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Reflection;
using System.Text;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI
{
	// Token: 0x020008FB RID: 2299
	internal class TileListBindingConverter : ExplicitJavaScriptConverter
	{
		// Token: 0x060056C6 RID: 22214 RVA: 0x00109A28 File Offset: 0x00107C28
		public override IDictionary<string, object> Serialize(object obj, JavaScriptSerializer serializer)
		{
			CommonTileBinding commonTileBinding = obj as CommonTileBinding;
			if (commonTileBinding != null)
			{
				return this.SerializeTileBinding(commonTileBinding);
			}
			ContentTemplateTileBinding contentTemplateTileBinding = obj as ContentTemplateTileBinding;
			if (contentTemplateTileBinding != null)
			{
				return this.SerializeTileBinding(contentTemplateTileBinding);
			}
			IconTileBinding iconTileBinding = obj as IconTileBinding;
			if (iconTileBinding != null)
			{
				return this.SerializeTileBinding(iconTileBinding);
			}
			ImageAndTextTileBinding imageAndTextTileBinding = obj as ImageAndTextTileBinding;
			if (imageAndTextTileBinding != null)
			{
				return this.SerializeTileBinding(imageAndTextTileBinding);
			}
			ImageTileBinding imageTileBinding = obj as ImageTileBinding;
			if (imageTileBinding != null)
			{
				return this.SerializeTileBinding(imageTileBinding);
			}
			LiveTileBinding liveTileBinding = obj as LiveTileBinding;
			if (liveTileBinding != null)
			{
				return this.SerializeTileBinding(liveTileBinding);
			}
			TextTileBinding textTileBinding = obj as TextTileBinding;
			if (textTileBinding != null)
			{
				return this.SerializeTileBinding(textTileBinding);
			}
			new Dictionary<string, object>();
			TileListBinding tileListBinding = obj as TileListBinding;
			if (tileListBinding != null)
			{
				return this.SerializeTileListBinding(tileListBinding);
			}
			throw new InvalidOperationException(this.GetInvalidSupportedTypeMessage());
		}

		// Token: 0x060056C7 RID: 22215 RVA: 0x00109AE4 File Offset: 0x00107CE4
		private string GetInvalidSupportedTypeMessage()
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append("Can serialize only ");
			foreach (Type type in this.SupportedTypes)
			{
				stringBuilder.Append(type.Name);
				stringBuilder.Append(", ");
			}
			stringBuilder.Append(" objects.");
			return stringBuilder.ToString();
		}

		// Token: 0x060056C8 RID: 22216 RVA: 0x00109B68 File Offset: 0x00107D68
		internal IDictionary<string, object> SerializeTileListBinding(TileListBinding tileListBinding)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			PropertyInfo[] properties = tileListBinding.GetType().GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				string name = propertyInfo.Name;
				object value = propertyInfo.GetValue(tileListBinding, null);
				if (!this.IsITemplateProperty(tileListBinding, name) && value != null)
				{
					if (value.GetType().Name == "String")
					{
						ExplicitJavaScriptConverter.AddProperty(dictionary, StringHelpers.ToCamelCase(name), value, this.GetDefaultValue(tileListBinding, name));
					}
					else
					{
						this.AddTileBindingProperty(dictionary, StringHelpers.ToCamelCase(name), value);
					}
				}
			}
			return dictionary;
		}

		// Token: 0x060056C9 RID: 22217 RVA: 0x00109C02 File Offset: 0x00107E02
		private void AddTileBindingProperty(Dictionary<string, object> state, string propertyName, object obj)
		{
			if (this.SerializeTileBinding(obj).Count > 0)
			{
				ExplicitJavaScriptConverter.AddProperty(state, propertyName, obj, null);
			}
		}

		// Token: 0x060056CA RID: 22218 RVA: 0x00109C1C File Offset: 0x00107E1C
		public IDictionary<string, object> SerializeTileBinding(object obj)
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			PropertyInfo[] properties = obj.GetType().GetProperties();
			foreach (PropertyInfo propertyInfo in properties)
			{
				string name = propertyInfo.Name;
				object value = propertyInfo.GetValue(obj, null);
				if (value != null && !this.IsITemplateProperty(obj, name))
				{
					ExplicitJavaScriptConverter.AddProperty(dictionary, StringHelpers.ToCamelCase(name), value, this.GetDefaultValue(obj, name));
				}
			}
			return dictionary;
		}

		// Token: 0x060056CB RID: 22219 RVA: 0x00109C8D File Offset: 0x00107E8D
		private bool IsITemplateProperty(object obj, string propertyName)
		{
			return TypeDescriptor.GetProperties(obj)[propertyName].PropertyType.Name == "ITemplate";
		}

		// Token: 0x060056CC RID: 22220 RVA: 0x00109CB0 File Offset: 0x00107EB0
		private object GetDefaultValue(object obj, string propertyName)
		{
			object result = null;
			foreach (object obj2 in TypeDescriptor.GetProperties(obj)[propertyName].Attributes)
			{
				DefaultValueAttribute defaultValueAttribute = obj2 as DefaultValueAttribute;
				if (defaultValueAttribute != null)
				{
					result = defaultValueAttribute.Value;
					break;
				}
			}
			return result;
		}

		// Token: 0x17001CB5 RID: 7349
		// (get) Token: 0x060056CD RID: 22221 RVA: 0x00109D24 File Offset: 0x00107F24
		public override IEnumerable<Type> SupportedTypes
		{
			get
			{
				return new Type[]
				{
					typeof(TileListBinding),
					typeof(CommonTileBinding),
					typeof(ContentTemplateTileBinding),
					typeof(IconTileBinding),
					typeof(ImageAndTextTileBinding),
					typeof(ImageTileBinding),
					typeof(LiveTileBinding),
					typeof(TextTileBinding)
				};
			}
		}
	}
}

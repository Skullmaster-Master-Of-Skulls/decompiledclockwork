using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Reflection;
using System.Web.UI;

namespace AjaxControlToolkit
{
	// Token: 0x02000098 RID: 152
	public class ComponentDescriber
	{
		// Token: 0x060004C0 RID: 1216 RVA: 0x0000CDFC File Offset: 0x0000AFFC
		public static void DescribeComponent(object instance, IScriptComponentDescriptor descriptor, IUrlResolutionService urlResolver, IControlResolver controlResolver)
		{
			if (instance == null)
			{
				throw new ArgumentNullException("instance");
			}
			if (descriptor == null)
			{
				throw new ArgumentNullException("descriptor");
			}
			if (urlResolver == null)
			{
				urlResolver = (instance as IUrlResolutionService);
			}
			if (controlResolver == null)
			{
				controlResolver = (instance as IControlResolver);
			}
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(instance);
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				ExtenderControlPropertyAttribute extenderControlPropertyAttribute = null;
				ExtenderControlEventAttribute extenderControlEventAttribute = null;
				ClientPropertyNameAttribute clientPropertyNameAttribute = null;
				IDReferencePropertyAttribute idreferencePropertyAttribute = null;
				UrlPropertyAttribute urlPropertyAttribute = null;
				ElementReferenceAttribute elementReferenceAttribute = null;
				ComponentReferenceAttribute componentReferenceAttribute = null;
				foreach (object obj2 in propertyDescriptor.Attributes)
				{
					Attribute attribute = (Attribute)obj2;
					Type type = attribute.GetType();
					if (type == typeof(ExtenderControlPropertyAttribute))
					{
						extenderControlPropertyAttribute = (attribute as ExtenderControlPropertyAttribute);
					}
					else if (type == typeof(ExtenderControlEventAttribute))
					{
						extenderControlEventAttribute = (attribute as ExtenderControlEventAttribute);
					}
					else if (type == typeof(ClientPropertyNameAttribute))
					{
						clientPropertyNameAttribute = (attribute as ClientPropertyNameAttribute);
					}
					else if (type == typeof(IDReferencePropertyAttribute))
					{
						idreferencePropertyAttribute = (attribute as IDReferencePropertyAttribute);
					}
					else if (type == typeof(UrlPropertyAttribute))
					{
						urlPropertyAttribute = (attribute as UrlPropertyAttribute);
					}
					else if (type == typeof(ElementReferenceAttribute))
					{
						elementReferenceAttribute = (attribute as ElementReferenceAttribute);
					}
					else if (type == typeof(ComponentReferenceAttribute))
					{
						componentReferenceAttribute = (attribute as ComponentReferenceAttribute);
					}
				}
				string name = propertyDescriptor.Name;
				if ((extenderControlPropertyAttribute != null && extenderControlPropertyAttribute.IsScriptProperty) || (extenderControlEventAttribute != null && extenderControlEventAttribute.IsScriptEvent))
				{
					if (clientPropertyNameAttribute != null && !string.IsNullOrEmpty(clientPropertyNameAttribute.PropertyName))
					{
						name = clientPropertyNameAttribute.PropertyName;
					}
					bool flag = propertyDescriptor.ShouldSerializeValue(instance) || propertyDescriptor.IsReadOnly;
					if (flag)
					{
						Control control = null;
						object obj3 = propertyDescriptor.GetValue(instance);
						if (obj3 != null)
						{
							if (extenderControlEventAttribute != null && propertyDescriptor.PropertyType != typeof(string))
							{
								throw new InvalidOperationException("ExtenderControlEventAttribute can only be applied to a property with a PropertyType of System.String.");
							}
							if (!propertyDescriptor.PropertyType.IsPrimitive && !propertyDescriptor.PropertyType.IsEnum)
							{
								Converter<object, string> converter = null;
								if (!ComponentDescriber._customConverters.TryGetValue(propertyDescriptor.PropertyType, out converter))
								{
									foreach (KeyValuePair<Type, Converter<object, string>> keyValuePair in ComponentDescriber._customConverters)
									{
										if (propertyDescriptor.PropertyType.IsSubclassOf(keyValuePair.Key))
										{
											converter = keyValuePair.Value;
											break;
										}
									}
								}
								if (converter != null)
								{
									obj3 = converter(obj3);
								}
								else if (extenderControlPropertyAttribute == null || !extenderControlPropertyAttribute.UseJsonSerialization)
								{
									TypeConverter converter2 = propertyDescriptor.Converter;
									if (obj3.GetType() == typeof(DateTime))
									{
										obj3 = ((DateTime)obj3).ToString("s", CultureInfo.InvariantCulture);
									}
									else
									{
										obj3 = converter2.ConvertToString(null, CultureInfo.InvariantCulture, obj3);
									}
								}
							}
							if (idreferencePropertyAttribute != null && controlResolver != null)
							{
								control = controlResolver.ResolveControl((string)obj3);
							}
							if (urlPropertyAttribute != null && urlResolver != null)
							{
								obj3 = urlResolver.ResolveClientUrl((string)obj3);
							}
							if (extenderControlEventAttribute != null)
							{
								descriptor.AddEvent(name, (string)obj3);
							}
							else if (elementReferenceAttribute != null)
							{
								if (control == null && controlResolver != null)
								{
									control = controlResolver.ResolveControl((string)obj3);
								}
								if (control != null)
								{
									obj3 = control.ClientID;
								}
								descriptor.AddElementProperty(name, (string)obj3);
							}
							else if (componentReferenceAttribute != null)
							{
								if (control == null && controlResolver != null)
								{
									control = controlResolver.ResolveControl((string)obj3);
								}
								if (control != null)
								{
									ExtenderControlBase extenderControlBase = control as ExtenderControlBase;
									if (extenderControlBase != null && extenderControlBase.BehaviorID.Length > 0)
									{
										obj3 = extenderControlBase.BehaviorID;
									}
									else
									{
										obj3 = control.ClientID;
									}
								}
								descriptor.AddComponentProperty(name, (string)obj3);
							}
							else
							{
								if (control != null)
								{
									obj3 = control.ClientID;
								}
								descriptor.AddProperty(name, obj3);
							}
						}
					}
				}
			}
			MethodInfo[] methods = instance.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
			int i = 0;
			while (i < methods.Length)
			{
				MethodInfo element = methods[i];
				ExtenderControlMethodAttribute extenderControlMethodAttribute = (ExtenderControlMethodAttribute)Attribute.GetCustomAttribute(element, typeof(ExtenderControlMethodAttribute));
				if (extenderControlMethodAttribute != null && extenderControlMethodAttribute.IsScriptMethod)
				{
					Control control2 = instance as Control;
					if (control2 != null)
					{
						control2.Page.ClientScript.GetCallbackEventReference(control2, null, null, null);
						descriptor.AddProperty("_callbackTarget", control2.UniqueID);
						return;
					}
					break;
				}
				else
				{
					i++;
				}
			}
		}

		// Token: 0x040002A8 RID: 680
		private static Dictionary<Type, Converter<object, string>> _customConverters = new Dictionary<Type, Converter<object, string>>();
	}
}

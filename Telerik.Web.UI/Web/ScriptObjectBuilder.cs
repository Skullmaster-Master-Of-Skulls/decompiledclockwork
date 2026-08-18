using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Diagnostics.CodeAnalysis;
using System.Drawing;
using System.Globalization;
using System.Reflection;
using System.Web.Script.Serialization;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using Telerik.Web.UI;

namespace Telerik.Web
{
	// Token: 0x020001D1 RID: 465
	public static class ScriptObjectBuilder
	{
		// Token: 0x060010D0 RID: 4304 RVA: 0x0003D58C File Offset: 0x0003B78C
		[SuppressMessage("Microsoft.Design", "CA1062:ValidateArgumentsOfPublicMethods", Justification = "controlResolver is checked against null before being used")]
		[SuppressMessage("Microsoft.Maintainability", "CA1502:AvoidExcessiveComplexity", Justification = "Cyclomatic complexity issues not currently being addressed")]
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily", Justification = "value is assigned/reassigned numerous times - code below favors clarity")]
		[SuppressMessage("Microsoft.Globalization", "CA1303:DoNotPassLiteralsAsLocalizedParameters", Justification = "Assembly is not localized")]
		public static void DescribeComponent(object instance, IScriptDescriptor descriptor, IUrlResolutionService urlResolver, IControlResolver controlResolver)
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
				ClientControlEventAttribute clientControlEventAttribute = null;
				string name = propertyDescriptor.Name;
				name = propertyDescriptor.Name[0].ToString(CultureInfo.InvariantCulture).ToLower(CultureInfo.InvariantCulture) + propertyDescriptor.Name.Substring(1);
				ClientControlPropertyAttribute clientControlPropertyAttribute = (ClientControlPropertyAttribute)propertyDescriptor.Attributes[typeof(ClientControlPropertyAttribute)];
				if (clientControlPropertyAttribute == null || !clientControlPropertyAttribute.IsScriptProperty)
				{
					clientControlEventAttribute = (ClientControlEventAttribute)propertyDescriptor.Attributes[typeof(ClientControlEventAttribute)];
					if (clientControlEventAttribute == null || !clientControlEventAttribute.IsScriptEvent)
					{
						continue;
					}
				}
				ClientPropertyNameAttribute clientPropertyNameAttribute = (ClientPropertyNameAttribute)propertyDescriptor.Attributes[typeof(ClientPropertyNameAttribute)];
				if (!string.IsNullOrEmpty(clientPropertyNameAttribute.PropertyName))
				{
					name = clientPropertyNameAttribute.PropertyName;
				}
				bool flag = propertyDescriptor.ShouldSerializeValue(instance) || propertyDescriptor.IsReadOnly;
				if (flag)
				{
					Control control = null;
					object obj2 = propertyDescriptor.GetValue(instance);
					if (obj2 != null)
					{
						if (clientControlEventAttribute != null && propertyDescriptor.PropertyType != typeof(string))
						{
							throw new InvalidOperationException("ClientControlEventAttribute can only be applied to a property with a PropertyType of System.String.");
						}
						if (!propertyDescriptor.PropertyType.IsPrimitive && !propertyDescriptor.PropertyType.IsEnum)
						{
							if (propertyDescriptor.PropertyType == typeof(Color))
							{
								obj2 = ColorTranslator.ToHtml((Color)obj2);
							}
							else if (propertyDescriptor.PropertyType == typeof(DateTime))
							{
								obj2 = ((DateTime)obj2).ToString("yyyy-MM-dd-HH-mm-ss");
							}
							else if (propertyDescriptor.PropertyType == typeof(TimeSpan))
							{
								TimeSpan timeSpan = (TimeSpan)obj2;
								obj2 = string.Format("{0}-{1}-{2}-{3}-{4}", new object[]
								{
									timeSpan.Days,
									timeSpan.Hours,
									timeSpan.Minutes,
									timeSpan.Seconds,
									timeSpan.Milliseconds
								});
							}
							else
							{
								TypeConverter converter = propertyDescriptor.Converter;
								obj2 = converter.ConvertToString(null, CultureInfo.InvariantCulture, obj2);
							}
						}
						if (propertyDescriptor.Attributes[typeof(IDReferencePropertyAttribute)] != null && controlResolver != null)
						{
							control = controlResolver.ResolveControl((string)obj2);
						}
						if (propertyDescriptor.Attributes[typeof(UrlPropertyAttribute)] != null && urlResolver != null)
						{
							obj2 = urlResolver.ResolveClientUrl((string)obj2);
						}
						if (clientControlEventAttribute != null)
						{
							descriptor.AddEvent(name, (string)obj2);
						}
						else if (propertyDescriptor.Attributes[typeof(ElementReferenceAttribute)] != null)
						{
							if (control == null && controlResolver != null)
							{
								control = controlResolver.ResolveControl((string)obj2);
							}
							if (control != null)
							{
								obj2 = control.ClientID;
							}
							descriptor.AddElementProperty(name, (string)obj2);
						}
						else if (propertyDescriptor.Attributes[typeof(ComponentReferenceAttribute)] != null)
						{
							if (control == null && controlResolver != null)
							{
								control = controlResolver.ResolveControl((string)obj2);
							}
							if (control != null)
							{
								obj2 = control.ClientID;
							}
							descriptor.AddComponentProperty(name, (string)obj2);
						}
						else
						{
							if (control != null)
							{
								obj2 = control.ClientID;
							}
							descriptor.AddProperty(name, obj2);
						}
					}
				}
			}
			MethodInfo[] methods = instance.GetType().GetMethods(BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
			int i = 0;
			while (i < methods.Length)
			{
				MethodInfo element = methods[i];
				ClientControlMethodAttribute clientControlMethodAttribute = (ClientControlMethodAttribute)Attribute.GetCustomAttribute(element, typeof(ClientControlMethodAttribute));
				if (clientControlMethodAttribute != null && clientControlMethodAttribute.IsScriptMethod)
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

		// Token: 0x060010D1 RID: 4305 RVA: 0x0003DA00 File Offset: 0x0003BC00
		public static IEnumerable<ScriptReference> GetScriptReferences(Type type)
		{
			return ScriptObjectBuilder.GetScriptReferences(type, false);
		}

		// Token: 0x060010D2 RID: 4306 RVA: 0x0003DA0C File Offset: 0x0003BC0C
		public static IEnumerable<ScriptReference> GetScriptReferences(Type type, bool ignoreStartingTypeReferences)
		{
			List<ScriptObjectBuilder.ResourceEntry> scriptReferencesInternal = ScriptObjectBuilder.GetScriptReferencesInternal((ignoreStartingTypeReferences && null != type) ? type.BaseType : type, new Stack<Type>());
			return ScriptObjectBuilder.ScriptReferencesFromResourceEntries(scriptReferencesInternal);
		}

		// Token: 0x060010D3 RID: 4307 RVA: 0x0003DA3F File Offset: 0x0003BC3F
		public static IEnumerable<string> GetCssReferences(Control control)
		{
			return ScriptObjectBuilder.GetCssReferences(control, control.GetType(), new Stack<Type>());
		}

		// Token: 0x060010D4 RID: 4308 RVA: 0x0003DA54 File Offset: 0x0003BC54
		public static void RegisterCssReferences(Control control)
		{
			foreach (string text in ScriptObjectBuilder.GetCssReferences(control))
			{
				if (control.Page.Header == null)
				{
					throw new Exception("The control " + control.ID + " requires a <head runat=\"server\"> declaration.");
				}
				bool flag = false;
				foreach (object obj in control.Page.Header.Controls)
				{
					Control control2 = (Control)obj;
					HtmlLink htmlLink = control2 as HtmlLink;
					flag = (htmlLink != null && htmlLink.Href == text);
					if (flag)
					{
						break;
					}
				}
				if (!flag)
				{
					HtmlLink htmlLink2 = new HtmlLink();
					htmlLink2.Href = text;
					htmlLink2.Attributes.Add("type", "text/css");
					htmlLink2.Attributes.Add("rel", "stylesheet");
					control.Page.Header.Controls.Add(htmlLink2);
				}
			}
		}

		// Token: 0x060010D5 RID: 4309 RVA: 0x0003DB94 File Offset: 0x0003BD94
		[SuppressMessage("Microsoft.Design", "CA1031:DoNotCatchGeneralExceptionTypes", Justification = "Deliberate attempt to catch and pass-on all exceptions")]
		public static string ExecuteCallbackMethod(Control control, string callbackArgument)
		{
			Type type = control.GetType();
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			Dictionary<string, object> dictionary = javaScriptSerializer.DeserializeObject(callbackArgument) as Dictionary<string, object>;
			string text = (string)dictionary["name"];
			object[] array = (object[])dictionary["args"];
			string clientState = (string)dictionary["state"];
			IClientStateManager clientStateManager = control as IClientStateManager;
			if (clientStateManager != null && clientStateManager.SupportsClientState)
			{
				clientStateManager.LoadClientState(clientState);
			}
			object value = null;
			string text2 = null;
			try
			{
				MethodInfo method = type.GetMethod(text, BindingFlags.Instance | BindingFlags.Static | BindingFlags.Public);
				if (method == null)
				{
					throw new MissingMethodException(type.FullName, text);
				}
				ParameterInfo[] parameters = method.GetParameters();
				ClientControlMethodAttribute clientControlMethodAttribute = (ClientControlMethodAttribute)Attribute.GetCustomAttribute(method, typeof(ClientControlMethodAttribute));
				if (clientControlMethodAttribute == null || !clientControlMethodAttribute.IsScriptMethod || array.Length != parameters.Length)
				{
					throw new MissingMethodException(type.FullName, text);
				}
				object[] array2 = new object[array.Length];
				for (int i = 0; i < array2.Length; i++)
				{
					if (array[i] != null)
					{
						array2[i] = Convert.ChangeType(array[i], parameters[i].ParameterType, CultureInfo.InvariantCulture);
					}
				}
				value = method.Invoke(control, array2);
			}
			catch (Exception innerException)
			{
				if (innerException is TargetInvocationException)
				{
					innerException = innerException.InnerException;
				}
				text2 = innerException.GetType().FullName + ":" + innerException.Message;
			}
			Dictionary<string, object> dictionary2 = new Dictionary<string, object>();
			if (text2 == null)
			{
				dictionary2["result"] = value;
				if (clientStateManager != null && clientStateManager.SupportsClientState)
				{
					dictionary2["state"] = clientStateManager.SaveClientState();
				}
			}
			else
			{
				dictionary2["error"] = text2;
			}
			return javaScriptSerializer.Serialize(dictionary2);
		}

		// Token: 0x060010D6 RID: 4310 RVA: 0x0003DD64 File Offset: 0x0003BF64
		private static IEnumerable<ScriptReference> ScriptReferencesFromResourceEntries(IList<ScriptObjectBuilder.ResourceEntry> entries)
		{
			IList<ScriptReference> list = new List<ScriptReference>(entries.Count);
			foreach (ScriptObjectBuilder.ResourceEntry resourceEntry in entries)
			{
				list.Add(resourceEntry.ToScriptReference());
			}
			return list;
		}

		// Token: 0x060010D7 RID: 4311 RVA: 0x0003DE08 File Offset: 0x0003C008
		private static List<ScriptObjectBuilder.ResourceEntry> GetScriptReferencesInternal(Type type, Stack<Type> typeReferenceStack)
		{
			if (typeReferenceStack.Contains(type))
			{
				throw new InvalidOperationException("Circular reference detected.");
			}
			List<ScriptObjectBuilder.ResourceEntry> list;
			if (ScriptObjectBuilder._cache.TryGetValue(type, out list))
			{
				return list;
			}
			typeReferenceStack.Push(type);
			List<ScriptObjectBuilder.ResourceEntry> result;
			try
			{
				lock (ScriptObjectBuilder._sync)
				{
					if (!ScriptObjectBuilder._cache.TryGetValue(type, out list))
					{
						list = new List<ScriptObjectBuilder.ResourceEntry>();
						List<RequiredScriptAttribute> list2 = new List<RequiredScriptAttribute>();
						foreach (RequiredScriptAttribute item in type.GetCustomAttributes(typeof(RequiredScriptAttribute), true))
						{
							list2.Add(item);
						}
						list2.Sort((RequiredScriptAttribute left, RequiredScriptAttribute right) => left.LoadOrder.CompareTo(right.LoadOrder));
						foreach (RequiredScriptAttribute requiredScriptAttribute in list2)
						{
							if (requiredScriptAttribute.ExtenderType != null)
							{
								list.AddRange(ScriptObjectBuilder.GetScriptReferencesInternal(requiredScriptAttribute.ExtenderType, typeReferenceStack));
							}
						}
						int num = 0;
						List<ScriptObjectBuilder.ResourceEntry> list3 = new List<ScriptObjectBuilder.ResourceEntry>();
						Type type2 = type;
						while (type2 != null && type2 != typeof(object))
						{
							object[] customAttributes2 = Attribute.GetCustomAttributes(type2, typeof(ClientScriptResourceAttribute), false);
							num -= customAttributes2.Length;
							foreach (ClientScriptResourceAttribute clientScriptResourceAttribute in customAttributes2)
							{
								ScriptObjectBuilder.ResourceEntry item2 = new ScriptObjectBuilder.ResourceEntry(clientScriptResourceAttribute.ResourcePath, type2, num + clientScriptResourceAttribute.LoadOrder);
								if (!list.Contains(item2) && !list3.Contains(item2))
								{
									list3.Add(item2);
								}
							}
							type2 = type2.BaseType;
						}
						list3.Sort((ScriptObjectBuilder.ResourceEntry l, ScriptObjectBuilder.ResourceEntry r) => l.Order.CompareTo(r.Order));
						list.AddRange(list3);
						ScriptObjectBuilder._cache.Add(type, list);
					}
					result = list;
				}
			}
			finally
			{
				typeReferenceStack.Pop();
			}
			return result;
		}

		// Token: 0x060010D8 RID: 4312 RVA: 0x0003E0B4 File Offset: 0x0003C2B4
		private static IEnumerable<string> GetCssReferences(Control control, Type type, Stack<Type> typeReferenceStack)
		{
			if (typeReferenceStack.Contains(type))
			{
				throw new InvalidOperationException("Circular reference detected.");
			}
			IList<string> list;
			if (ScriptObjectBuilder._cssCache.TryGetValue(type, out list))
			{
				return list;
			}
			typeReferenceStack.Push(type);
			IEnumerable<string> result;
			try
			{
				lock (ScriptObjectBuilder._sync)
				{
					if (ScriptObjectBuilder._cssCache.TryGetValue(type, out list))
					{
						result = list;
					}
					else
					{
						List<string> list2 = new List<string>();
						List<RequiredScriptAttribute> list3 = new List<RequiredScriptAttribute>();
						foreach (RequiredScriptAttribute item in type.GetCustomAttributes(typeof(RequiredScriptAttribute), true))
						{
							list3.Add(item);
						}
						list3.Sort((RequiredScriptAttribute left, RequiredScriptAttribute right) => left.LoadOrder.CompareTo(right.LoadOrder));
						foreach (RequiredScriptAttribute requiredScriptAttribute in list3)
						{
							if (requiredScriptAttribute.ExtenderType != null)
							{
								list2.AddRange(ScriptObjectBuilder.GetCssReferences(control, requiredScriptAttribute.ExtenderType, typeReferenceStack));
							}
						}
						List<ScriptObjectBuilder.ResourceEntry> list4 = new List<ScriptObjectBuilder.ResourceEntry>();
						int num = 0;
						Type type2 = type;
						while (type2 != null && type2 != typeof(object))
						{
							object[] customAttributes2 = Attribute.GetCustomAttributes(type2, typeof(ClientCssResourceAttribute), false);
							num -= customAttributes2.Length;
							foreach (ClientCssResourceAttribute clientCssResourceAttribute in customAttributes2)
							{
								list4.Add(new ScriptObjectBuilder.ResourceEntry(clientCssResourceAttribute.ResourcePath, type2, num + clientCssResourceAttribute.LoadOrder));
							}
							type2 = type2.BaseType;
						}
						list4.Sort((ScriptObjectBuilder.ResourceEntry l, ScriptObjectBuilder.ResourceEntry r) => l.Order.CompareTo(r.Order));
						foreach (ScriptObjectBuilder.ResourceEntry resourceEntry in list4)
						{
							list2.Add(control.Page.ClientScript.GetWebResourceUrl(resourceEntry.ComponentType, resourceEntry.ResourcePath));
						}
						Dictionary<string, object> dictionary = new Dictionary<string, object>(StringComparer.InvariantCultureIgnoreCase);
						List<string> list5 = new List<string>();
						foreach (string text in list2)
						{
							if (!dictionary.ContainsKey(text))
							{
								dictionary.Add(text, null);
								list5.Add(text);
							}
						}
						list = new ReadOnlyCollection<string>(list5);
						ScriptObjectBuilder._cssCache.Add(type, list);
						result = list;
					}
				}
			}
			finally
			{
				typeReferenceStack.Pop();
			}
			return result;
		}

		// Token: 0x040004C7 RID: 1223
		private const string CSS_LINK = "<link rel=\"stylesheet\" type=\"text/css\" href=\"{0}\" />";

		// Token: 0x040004C8 RID: 1224
		private static readonly Dictionary<Type, List<ScriptObjectBuilder.ResourceEntry>> _cache = new Dictionary<Type, List<ScriptObjectBuilder.ResourceEntry>>();

		// Token: 0x040004C9 RID: 1225
		private static readonly Dictionary<Type, IList<string>> _cssCache = new Dictionary<Type, IList<string>>();

		// Token: 0x040004CA RID: 1226
		private static readonly object _sync = new object();

		// Token: 0x020001D2 RID: 466
		private struct ResourceEntry
		{
			// Token: 0x170005A9 RID: 1449
			// (get) Token: 0x060010DE RID: 4318 RVA: 0x0003E400 File Offset: 0x0003C600
			// (set) Token: 0x060010DF RID: 4319 RVA: 0x0003E408 File Offset: 0x0003C608
			public string ResourcePath
			{
				get
				{
					return this._resourcePath;
				}
				set
				{
					this._resourcePath = value;
				}
			}

			// Token: 0x170005AA RID: 1450
			// (get) Token: 0x060010E0 RID: 4320 RVA: 0x0003E411 File Offset: 0x0003C611
			// (set) Token: 0x060010E1 RID: 4321 RVA: 0x0003E419 File Offset: 0x0003C619
			public Type ComponentType
			{
				get
				{
					return this._componentType;
				}
				set
				{
					this._componentType = value;
				}
			}

			// Token: 0x170005AB RID: 1451
			// (get) Token: 0x060010E2 RID: 4322 RVA: 0x0003E422 File Offset: 0x0003C622
			// (set) Token: 0x060010E3 RID: 4323 RVA: 0x0003E42A File Offset: 0x0003C62A
			public int Order
			{
				get
				{
					return this._order;
				}
				set
				{
					this._order = value;
				}
			}

			// Token: 0x170005AC RID: 1452
			// (get) Token: 0x060010E4 RID: 4324 RVA: 0x0003E434 File Offset: 0x0003C634
			private string RefKey
			{
				get
				{
					return string.Format(CultureInfo.CurrentCulture, "{0}#{1}", new object[]
					{
						(this.ComponentType == null) ? "" : this.ComponentType.Assembly.FullName,
						this.ResourcePath
					});
				}
			}

			// Token: 0x060010E5 RID: 4325 RVA: 0x0003E48B File Offset: 0x0003C68B
			public ResourceEntry(string path, Type componentType, int order)
			{
				this._resourcePath = path;
				this._componentType = componentType;
				this._order = order;
			}

			// Token: 0x060010E6 RID: 4326 RVA: 0x0003E4A4 File Offset: 0x0003C6A4
			public ScriptReference ToScriptReference()
			{
				return new ScriptReference
				{
					Assembly = this.ComponentType.Assembly.FullName,
					Name = this.ResourcePath
				};
			}

			// Token: 0x060010E7 RID: 4327 RVA: 0x0003E4DC File Offset: 0x0003C6DC
			public override bool Equals(object obj)
			{
				ScriptObjectBuilder.ResourceEntry resourceEntry = (ScriptObjectBuilder.ResourceEntry)obj;
				return string.Compare(this.RefKey, resourceEntry.RefKey, true, CultureInfo.CurrentCulture) == 0;
			}

			// Token: 0x060010E8 RID: 4328 RVA: 0x0003E50B File Offset: 0x0003C70B
			public static bool operator ==(ScriptObjectBuilder.ResourceEntry obj1, ScriptObjectBuilder.ResourceEntry obj2)
			{
				return obj1.Equals(obj2);
			}

			// Token: 0x060010E9 RID: 4329 RVA: 0x0003E520 File Offset: 0x0003C720
			public static bool operator !=(ScriptObjectBuilder.ResourceEntry obj1, ScriptObjectBuilder.ResourceEntry obj2)
			{
				return !obj1.Equals(obj2);
			}

			// Token: 0x060010EA RID: 4330 RVA: 0x0003E538 File Offset: 0x0003C738
			public override int GetHashCode()
			{
				return this.RefKey.GetHashCode();
			}

			// Token: 0x040004CF RID: 1231
			private string _resourcePath;

			// Token: 0x040004D0 RID: 1232
			private Type _componentType;

			// Token: 0x040004D1 RID: 1233
			private int _order;
		}
	}
}

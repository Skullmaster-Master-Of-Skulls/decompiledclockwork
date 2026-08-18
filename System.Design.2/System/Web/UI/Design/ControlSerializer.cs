using System;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Drawing;
using System.Globalization;
using System.IO;
using System.Reflection;
using System.Text;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;

namespace System.Web.UI.Design
{
	// Token: 0x0200001E RID: 30
	internal static class ControlSerializer
	{
		// Token: 0x060000D3 RID: 211 RVA: 0x00006E54 File Offset: 0x00005054
		private static bool CanSerializeAsInnerDefaultString(string filter, string name, Type type, ObjectPersistData persistData, PersistenceMode mode, DataBindingCollection dataBindings, ExpressionBindingCollection expressions)
		{
			if (type == typeof(string) && filter.Length == 0 && (mode == PersistenceMode.InnerDefaultProperty || mode == PersistenceMode.EncodedInnerDefaultProperty) && (dataBindings == null || dataBindings[name] == null) && (expressions == null || expressions[name] == null))
			{
				if (persistData == null)
				{
					return true;
				}
				ICollection propertyAllFilters = persistData.GetPropertyAllFilters(name);
				if (propertyAllFilters.Count == 0)
				{
					return true;
				}
				if (propertyAllFilters.Count == 1)
				{
					foreach (object obj in propertyAllFilters)
					{
						PropertyEntry propertyEntry = (PropertyEntry)obj;
						if (propertyEntry.Filter.Length == 0 && propertyEntry is ComplexPropertyEntry)
						{
							return true;
						}
					}
					return false;
				}
			}
			return false;
		}

		// Token: 0x060000D4 RID: 212 RVA: 0x00006F2C File Offset: 0x0000512C
		private static string ConvertObjectModelToPersistName(string objectModelName)
		{
			return objectModelName.Replace('.', '-');
		}

		// Token: 0x060000D5 RID: 213 RVA: 0x00006F38 File Offset: 0x00005138
		private static string ConvertPersistToObjectModelName(string persistName)
		{
			return persistName.Replace('-', '.');
		}

		// Token: 0x060000D6 RID: 214 RVA: 0x00006F44 File Offset: 0x00005144
		private static IDictionary GetExpandos(string filter, string name, ObjectPersistData persistData)
		{
			IDictionary result = null;
			if (persistData != null)
			{
				BuilderPropertyEntry builderPropertyEntry = persistData.GetFilteredProperty(filter, name) as BuilderPropertyEntry;
				if (builderPropertyEntry != null)
				{
					ObjectPersistData objectPersistData = builderPropertyEntry.Builder.GetObjectPersistData();
					result = objectPersistData.GetFilteredProperties(ControlBuilder.DesignerFilter);
				}
			}
			return result;
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00006F80 File Offset: 0x00005180
		internal static ArrayList GetControlPersistedAttributes(Control control, IDesignerHost host)
		{
			ObjectPersistData persistData = null;
			if (((IControlBuilderAccessor)control).ControlBuilder != null)
			{
				persistData = ((IControlBuilderAccessor)control).ControlBuilder.GetObjectPersistData();
			}
			return ControlSerializer.SerializeAttributes(control, host, string.Empty, persistData, ControlSerializer.GetCurrentFilter(host), true);
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00006FBC File Offset: 0x000051BC
		internal static ArrayList GetControlPersistedAttribute(Control control, PropertyDescriptor propDesc, IDesignerHost host)
		{
			ObjectPersistData persistData = null;
			if (((IControlBuilderAccessor)control).ControlBuilder != null)
			{
				persistData = ((IControlBuilderAccessor)control).ControlBuilder.GetObjectPersistData();
			}
			string prefix = string.Empty;
			ArrayList arrayList = new ArrayList();
			if (propDesc.SerializationVisibility == DesignerSerializationVisibility.Content)
			{
				object value = propDesc.GetValue(control);
				prefix = propDesc.Name;
				ControlSerializer.SerializeAttributesRecursive(value, host, prefix, persistData, ControlSerializer.GetCurrentFilter(host), arrayList, null, null, true);
			}
			else
			{
				DataBindingCollection dataBindings = ((IDataBindingsAccessor)control).DataBindings;
				ExpressionBindingCollection expressions = ((IExpressionsAccessor)control).Expressions;
				ControlSerializer.SerializeAttribute(control, propDesc, dataBindings, expressions, host, prefix, persistData, ControlSerializer.GetCurrentFilter(host), arrayList, true);
			}
			return arrayList;
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00007048 File Offset: 0x00005248
		private static object GetPropertyDefaultValue(PropertyDescriptor propDesc, string name, ObjectPersistData defaultPropertyEntries, string filter, IDesignerHost host)
		{
			if (filter.Length > 0 && defaultPropertyEntries != null)
			{
				string name2 = ControlSerializer.ConvertPersistToObjectModelName(name);
				ServiceContainer serviceContainer = new ServiceContainer();
				if (host != null)
				{
					IFilterResolutionService filterResolutionService = (IFilterResolutionService)host.GetService(typeof(IFilterResolutionService));
					if (filterResolutionService != null)
					{
						serviceContainer.AddService(typeof(IFilterResolutionService), filterResolutionService);
					}
					IThemeResolutionService themeResolutionService = (IThemeResolutionService)host.GetService(typeof(IThemeResolutionService));
					if (themeResolutionService != null)
					{
						serviceContainer.AddService(typeof(IThemeResolutionService), themeResolutionService);
					}
				}
				PropertyEntry filteredProperty = defaultPropertyEntries.GetFilteredProperty(string.Empty, name2);
				if (filteredProperty is SimplePropertyEntry)
				{
					return ((SimplePropertyEntry)filteredProperty).Value;
				}
				if (filteredProperty is BoundPropertyEntry)
				{
					string text = ((BoundPropertyEntry)filteredProperty).Expression.Trim();
					string text2 = ((BoundPropertyEntry)filteredProperty).ExpressionPrefix.Trim();
					if (text2.Length > 0)
					{
						text = text2 + ":" + text;
					}
					return text;
				}
				if (filteredProperty is ComplexPropertyEntry)
				{
					ControlBuilder builder = ((ComplexPropertyEntry)filteredProperty).Builder;
					builder.SetServiceProvider(serviceContainer);
					object result = null;
					try
					{
						result = builder.BuildObject();
					}
					finally
					{
						builder.SetServiceProvider(null);
					}
					return result;
				}
			}
			DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)propDesc.Attributes[typeof(DefaultValueAttribute)];
			if (defaultValueAttribute != null)
			{
				return defaultValueAttribute.Value;
			}
			return null;
		}

		// Token: 0x060000DA RID: 218 RVA: 0x000071B8 File Offset: 0x000053B8
		private static string GetDirectives(IDesignerHost designerHost)
		{
			string result = string.Empty;
			WebFormsReferenceManager webFormsReferenceManager = null;
			if (designerHost.RootComponent != null)
			{
				WebFormsRootDesigner webFormsRootDesigner = designerHost.GetDesigner(designerHost.RootComponent) as WebFormsRootDesigner;
				if (webFormsRootDesigner != null)
				{
					webFormsReferenceManager = webFormsRootDesigner.ReferenceManager;
				}
			}
			if (webFormsReferenceManager == null)
			{
				IWebFormReferenceManager webFormReferenceManager = (IWebFormReferenceManager)designerHost.GetService(typeof(IWebFormReferenceManager));
				if (webFormReferenceManager != null)
				{
					result = webFormReferenceManager.GetRegisterDirectives();
				}
			}
			else
			{
				StringBuilder stringBuilder = new StringBuilder();
				foreach (object obj in webFormsReferenceManager.GetRegisterDirectives())
				{
					string value = (string)obj;
					stringBuilder.Append(value);
				}
				result = stringBuilder.ToString();
			}
			return result;
		}

		// Token: 0x060000DB RID: 219 RVA: 0x00003930 File Offset: 0x00001B30
		private static string GetCurrentFilter(IDesignerHost host)
		{
			return string.Empty;
		}

		// Token: 0x060000DC RID: 220 RVA: 0x00007280 File Offset: 0x00005480
		private static string GetPersistValue(PropertyDescriptor propDesc, Type propType, object propValue, ControlSerializer.BindingType bindingType, bool topLevelInDesigner)
		{
			string text = string.Empty;
			if (bindingType == ControlSerializer.BindingType.Data)
			{
				text = "<%# " + propValue.ToString() + " %>";
			}
			else if (bindingType == ControlSerializer.BindingType.Expression)
			{
				text = "<%$ " + propValue.ToString() + " %>";
			}
			else if (propType.IsEnum)
			{
				text = Enum.Format(propType, propValue, "G");
			}
			else if (propType == typeof(string))
			{
				if (propValue != null)
				{
					text = propValue.ToString();
					if (!topLevelInDesigner)
					{
						text = HttpUtility.HtmlEncode(text);
					}
				}
			}
			else
			{
				TypeConverter converter;
				if (propDesc != null)
				{
					converter = propDesc.Converter;
				}
				else
				{
					converter = TypeDescriptor.GetConverter(propValue);
				}
				if (converter != null)
				{
					text = converter.ConvertToInvariantString(null, propValue);
				}
				else
				{
					text = propValue.ToString();
				}
				if (!topLevelInDesigner)
				{
					text = HttpUtility.HtmlEncode(text);
				}
			}
			return text;
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00007344 File Offset: 0x00005544
		private static bool GetShouldSerializeValue(object obj, string name, out bool useResult)
		{
			useResult = false;
			Type type = obj.GetType();
			BindingFlags bindingFlags = BindingFlags.IgnoreCase | BindingFlags.Instance | BindingFlags.Public;
			PropertyInfo property = type.GetProperty(name, bindingFlags);
			bindingFlags |= BindingFlags.NonPublic;
			MethodInfo method = property.DeclaringType.GetMethod("ShouldSerialize" + name, bindingFlags);
			if (method != null)
			{
				useResult = true;
				object obj2 = method.Invoke(obj, new object[0]);
				return (bool)obj2;
			}
			return true;
		}

		// Token: 0x060000DE RID: 222 RVA: 0x000073A8 File Offset: 0x000055A8
		private static string GetTagName(Type type, IDesignerHost host)
		{
			string text = string.Empty;
			string text2 = string.Empty;
			WebFormsReferenceManager webFormsReferenceManager = null;
			if (host.RootComponent != null)
			{
				WebFormsRootDesigner webFormsRootDesigner = host.GetDesigner(host.RootComponent) as WebFormsRootDesigner;
				if (webFormsRootDesigner != null)
				{
					webFormsReferenceManager = webFormsRootDesigner.ReferenceManager;
				}
			}
			if (webFormsReferenceManager == null)
			{
				IWebFormReferenceManager webFormReferenceManager = (IWebFormReferenceManager)host.GetService(typeof(IWebFormReferenceManager));
				if (webFormReferenceManager != null)
				{
					text2 = webFormReferenceManager.GetTagPrefix(type);
				}
			}
			else
			{
				text2 = webFormsReferenceManager.GetTagPrefix(type);
			}
			if (string.IsNullOrEmpty(text2))
			{
				text2 = webFormsReferenceManager.RegisterTagPrefix(type);
			}
			if (text2 != null && text2.Length != 0)
			{
				text = text2 + ":" + type.Name;
			}
			if (text.Length == 0)
			{
				text = type.FullName;
			}
			return text;
		}

		// Token: 0x060000DF RID: 223 RVA: 0x00007458 File Offset: 0x00005658
		internal static Control DeserializeControlInternal(string text, IDesignerHost host, bool applyTheme)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			if (text == null || text.Length == 0)
			{
				throw new ArgumentNullException("text");
			}
			string directives = ControlSerializer.GetDirectives(host);
			if (directives != null && directives.Length > 0)
			{
				text = directives + text;
			}
			DesignTimeParseData designTimeParseData = new DesignTimeParseData(host, text, ControlSerializer.GetCurrentFilter(host));
			designTimeParseData.ShouldApplyTheme = applyTheme;
			designTimeParseData.DataBindingHandler = GlobalDataBindingHandler.Handler;
			Control result = null;
			Type typeFromHandle = typeof(LicenseManager);
			lock (typeFromHandle)
			{
				LicenseContext currentContext = LicenseManager.CurrentContext;
				bool flag2 = false;
				try
				{
					if (!ControlSerializer.licenseManagerLockHeld)
					{
						LicenseManager.CurrentContext = new ControlSerializer.WebFormsDesigntimeLicenseContext(host);
						LicenseManager.LockContext(ControlSerializer.licenseManagerLock);
						ControlSerializer.licenseManagerLockHeld = true;
						flag2 = true;
					}
					result = DesignTimeTemplateParser.ParseControl(designTimeParseData);
				}
				catch (TargetInvocationException ex)
				{
					throw ex.InnerException;
				}
				finally
				{
					if (flag2)
					{
						LicenseManager.UnlockContext(ControlSerializer.licenseManagerLock);
						LicenseManager.CurrentContext = currentContext;
						ControlSerializer.licenseManagerLockHeld = false;
					}
				}
			}
			return result;
		}

		// Token: 0x060000E0 RID: 224 RVA: 0x00007570 File Offset: 0x00005770
		public static Control DeserializeControl(string text, IDesignerHost host)
		{
			return ControlSerializer.DeserializeControlInternal(text, host, false);
		}

		// Token: 0x060000E1 RID: 225 RVA: 0x0000757A File Offset: 0x0000577A
		public static Control[] DeserializeControls(string text, IDesignerHost host)
		{
			return ControlSerializer.DeserializeControlsInternal(text, host, null);
		}

		// Token: 0x060000E2 RID: 226 RVA: 0x00007584 File Offset: 0x00005784
		internal static Control[] DeserializeControlsInternal(string text, IDesignerHost host, List<Triplet> userControlRegisterEntries)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			if (text == null || text.Length == 0)
			{
				throw new ArgumentNullException("text");
			}
			string directives = ControlSerializer.GetDirectives(host);
			if (directives != null && directives.Length > 0)
			{
				text = directives + text;
			}
			DesignTimeParseData designTimeParseData = new DesignTimeParseData(host, text, ControlSerializer.GetCurrentFilter(host));
			designTimeParseData.DataBindingHandler = GlobalDataBindingHandler.Handler;
			Control[] result = null;
			Type typeFromHandle = typeof(LicenseManager);
			lock (typeFromHandle)
			{
				LicenseContext currentContext = LicenseManager.CurrentContext;
				try
				{
					LicenseManager.CurrentContext = new ControlSerializer.WebFormsDesigntimeLicenseContext(host);
					LicenseManager.LockContext(ControlSerializer.licenseManagerLock);
					result = DesignTimeTemplateParser.ParseControls(designTimeParseData);
				}
				catch (TargetInvocationException ex)
				{
					throw ex.InnerException;
				}
				finally
				{
					LicenseManager.UnlockContext(ControlSerializer.licenseManagerLock);
					LicenseManager.CurrentContext = currentContext;
				}
			}
			if (userControlRegisterEntries != null && designTimeParseData.UserControlRegisterEntries != null)
			{
				foreach (object obj in designTimeParseData.UserControlRegisterEntries)
				{
					Triplet item = (Triplet)obj;
					userControlRegisterEntries.Add(item);
				}
			}
			return result;
		}

		// Token: 0x060000E3 RID: 227 RVA: 0x000076D4 File Offset: 0x000058D4
		public static ITemplate DeserializeTemplate(string text, IDesignerHost host)
		{
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			if (text == null || text.Length == 0)
			{
				return null;
			}
			string parseText = text;
			string directives = ControlSerializer.GetDirectives(host);
			if (directives != null && directives.Length > 0)
			{
				parseText = directives + text;
			}
			DesignTimeParseData designTimeParseData = new DesignTimeParseData(host, parseText);
			designTimeParseData.DataBindingHandler = GlobalDataBindingHandler.Handler;
			ITemplate template = null;
			Type typeFromHandle = typeof(LicenseManager);
			lock (typeFromHandle)
			{
				LicenseContext currentContext = LicenseManager.CurrentContext;
				try
				{
					LicenseManager.CurrentContext = new ControlSerializer.WebFormsDesigntimeLicenseContext(host);
					LicenseManager.LockContext(ControlSerializer.licenseManagerLock);
					template = DesignTimeTemplateParser.ParseTemplate(designTimeParseData);
				}
				catch (TargetInvocationException ex)
				{
					throw ex.InnerException;
				}
				finally
				{
					LicenseManager.UnlockContext(ControlSerializer.licenseManagerLock);
					LicenseManager.CurrentContext = currentContext;
				}
			}
			if (template != null && template is TemplateBuilder)
			{
				((TemplateBuilder)template).Text = text;
			}
			return template;
		}

		// Token: 0x060000E4 RID: 228 RVA: 0x000077D4 File Offset: 0x000059D4
		private static void SerializeAttribute(object obj, PropertyDescriptor propDesc, DataBindingCollection dataBindings, ExpressionBindingCollection expressions, IDesignerHost host, string prefix, ObjectPersistData persistData, string filter, ArrayList attributes, bool topLevelInDesigner)
		{
			DesignOnlyAttribute designOnlyAttribute = (DesignOnlyAttribute)propDesc.Attributes[typeof(DesignOnlyAttribute)];
			if (designOnlyAttribute != null && designOnlyAttribute.IsDesignOnly)
			{
				return;
			}
			string name = propDesc.Name;
			Type propertyType = propDesc.PropertyType;
			PersistenceMode mode = ((PersistenceModeAttribute)propDesc.Attributes[typeof(PersistenceModeAttribute)]).Mode;
			bool flag = dataBindings != null && dataBindings[name] != null;
			bool flag2 = expressions != null && expressions[name] != null;
			if (!flag && !flag2 && propDesc.SerializationVisibility == DesignerSerializationVisibility.Hidden)
			{
				return;
			}
			if (mode != PersistenceMode.Attribute && (!flag || !flag2 || propertyType != typeof(string)) && (mode == PersistenceMode.InnerProperty || propertyType != typeof(string)))
			{
				return;
			}
			string text = string.Empty;
			if (prefix.Length > 0)
			{
				text = prefix + "-" + name;
			}
			else
			{
				text = name;
			}
			if (propDesc.SerializationVisibility == DesignerSerializationVisibility.Content)
			{
				object value = propDesc.GetValue(obj);
				ControlSerializer.SerializeAttributesRecursive(value, host, text, persistData, filter, attributes, dataBindings, expressions, topLevelInDesigner);
				return;
			}
			IAttributeAccessor attributeAccessor = obj as IAttributeAccessor;
			if (propDesc.IsReadOnly && (attributeAccessor == null || attributeAccessor.GetAttribute(text) == null))
			{
				return;
			}
			string text2 = ControlSerializer.ConvertPersistToObjectModelName(text);
			if (!FilterableAttribute.IsPropertyFilterable(propDesc))
			{
				filter = string.Empty;
			}
			if (ControlSerializer.CanSerializeAsInnerDefaultString(filter, text2, propertyType, persistData, mode, dataBindings, expressions))
			{
				if (topLevelInDesigner)
				{
					attributes.Add(new Triplet(filter, text, null));
				}
				return;
			}
			object obj2 = null;
			object obj3 = propDesc.GetValue(obj);
			ControlSerializer.BindingType bindingType = ControlSerializer.BindingType.None;
			if (dataBindings != null)
			{
				DataBinding dataBinding = dataBindings[text2];
				if (dataBinding != null)
				{
					obj3 = dataBinding.Expression;
					bindingType = ControlSerializer.BindingType.Data;
				}
			}
			if (bindingType == ControlSerializer.BindingType.None)
			{
				if (expressions != null)
				{
					ExpressionBinding expressionBinding = expressions[text2];
					if (expressionBinding != null && !expressionBinding.Generated)
					{
						obj3 = expressionBinding.ExpressionPrefix + ":" + expressionBinding.Expression;
						bindingType = ControlSerializer.BindingType.Expression;
					}
				}
				else if (persistData != null)
				{
					BoundPropertyEntry boundPropertyEntry = persistData.GetFilteredProperty(filter, name) as BoundPropertyEntry;
					if (boundPropertyEntry != null && !boundPropertyEntry.Generated)
					{
						obj2 = ControlSerializer.GetPropertyDefaultValue(propDesc, text, persistData, filter, host);
						if (object.Equals(obj3, obj2))
						{
							obj3 = boundPropertyEntry.ExpressionPrefix + ":" + boundPropertyEntry.Expression;
							bindingType = ControlSerializer.BindingType.Expression;
						}
					}
				}
			}
			bool flag5;
			if (filter.Length == 0)
			{
				bool flag3 = false;
				bool flag4 = false;
				if (bindingType == ControlSerializer.BindingType.None)
				{
					flag4 = ControlSerializer.GetShouldSerializeValue(obj, name, out flag3);
				}
				if (flag3)
				{
					flag5 = flag4;
				}
				else
				{
					obj2 = ControlSerializer.GetPropertyDefaultValue(propDesc, text, persistData, filter, host);
					flag5 = !object.Equals(obj3, obj2);
				}
			}
			else
			{
				obj2 = ControlSerializer.GetPropertyDefaultValue(propDesc, text, persistData, filter, host);
				flag5 = !object.Equals(obj3, obj2);
			}
			if (flag5)
			{
				string text3 = ControlSerializer.GetPersistValue(propDesc, propertyType, obj3, bindingType, topLevelInDesigner);
				if (topLevelInDesigner && obj2 != null && (text3 == null || text3.Length == 0) && ControlSerializer.ShouldPersistBlankValue(obj2, propertyType))
				{
					text3 = string.Empty;
				}
				if (text3 != null && (!propertyType.IsArray || text3.Length > 0))
				{
					attributes.Add(new Triplet(filter, text, text3));
				}
				else if (topLevelInDesigner)
				{
					attributes.Add(new Triplet(filter, text, null));
				}
			}
			else if (topLevelInDesigner)
			{
				attributes.Add(new Triplet(filter, text, null));
			}
			if (persistData != null)
			{
				ICollection propertyAllFilters = persistData.GetPropertyAllFilters(text2);
				foreach (object obj4 in propertyAllFilters)
				{
					PropertyEntry propertyEntry = (PropertyEntry)obj4;
					if (string.Compare(propertyEntry.Filter, filter, StringComparison.OrdinalIgnoreCase) != 0)
					{
						string z = string.Empty;
						if (propertyEntry is SimplePropertyEntry)
						{
							SimplePropertyEntry simplePropertyEntry = (SimplePropertyEntry)propertyEntry;
							if (simplePropertyEntry.UseSetAttribute)
							{
								z = simplePropertyEntry.Value.ToString();
							}
							else
							{
								z = ControlSerializer.GetPersistValue(propDesc, propertyEntry.Type, simplePropertyEntry.Value, ControlSerializer.BindingType.None, topLevelInDesigner);
							}
						}
						else if (propertyEntry is BoundPropertyEntry)
						{
							BoundPropertyEntry boundPropertyEntry2 = (BoundPropertyEntry)propertyEntry;
							if (boundPropertyEntry2.Generated)
							{
								continue;
							}
							string text4 = boundPropertyEntry2.Expression.Trim();
							bindingType = ControlSerializer.BindingType.Data;
							string expressionPrefix = boundPropertyEntry2.ExpressionPrefix;
							if (expressionPrefix.Length > 0)
							{
								text4 = expressionPrefix + ":" + text4;
								bindingType = ControlSerializer.BindingType.Expression;
							}
							z = ControlSerializer.GetPersistValue(propDesc, propertyEntry.Type, text4, bindingType, topLevelInDesigner);
						}
						else if (propertyEntry is ComplexPropertyEntry)
						{
							ComplexPropertyEntry complexPropertyEntry = (ComplexPropertyEntry)propertyEntry;
							object obj5 = complexPropertyEntry.Builder.BuildObject();
							z = (string)obj5;
						}
						attributes.Add(new Triplet(propertyEntry.Filter, text, z));
					}
				}
			}
		}

		// Token: 0x060000E5 RID: 229 RVA: 0x00007C94 File Offset: 0x00005E94
		private static void SerializeAttributes(object obj, IDesignerHost host, string prefix, ObjectPersistData persistData, TextWriter writer, string filter)
		{
			ArrayList arrayList = ControlSerializer.SerializeAttributes(obj, host, prefix, persistData, filter, false);
			foreach (object obj2 in arrayList)
			{
				Triplet triplet = (Triplet)obj2;
				ControlSerializer.WriteAttribute(writer, triplet.First.ToString(), triplet.Second.ToString(), triplet.Third.ToString());
			}
		}

		// Token: 0x060000E6 RID: 230 RVA: 0x00007D18 File Offset: 0x00005F18
		private static ArrayList SerializeAttributes(object obj, IDesignerHost host, string prefix, ObjectPersistData persistData, string filter, bool topLevelInDesigner)
		{
			ArrayList arrayList = new ArrayList();
			ControlSerializer.SerializeAttributesRecursive(obj, host, prefix, persistData, filter, arrayList, null, null, topLevelInDesigner);
			if (persistData != null)
			{
				foreach (object obj2 in persistData.AllPropertyEntries)
				{
					PropertyEntry propertyEntry = (PropertyEntry)obj2;
					BoundPropertyEntry boundPropertyEntry = propertyEntry as BoundPropertyEntry;
					if (boundPropertyEntry != null && !boundPropertyEntry.Generated)
					{
						string[] array = boundPropertyEntry.Name.Split(new char[]
						{
							'.'
						});
						if (array.Length > 1)
						{
							object component = obj;
							foreach (string name in array)
							{
								PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)[name];
								if (propertyDescriptor == null)
								{
									break;
								}
								PersistenceModeAttribute persistenceModeAttribute = propertyDescriptor.Attributes[typeof(PersistenceModeAttribute)] as PersistenceModeAttribute;
								if (persistenceModeAttribute != PersistenceModeAttribute.Attribute)
								{
									string propValue = string.IsNullOrEmpty(boundPropertyEntry.ExpressionPrefix) ? boundPropertyEntry.Expression : (boundPropertyEntry.ExpressionPrefix + ":" + boundPropertyEntry.Expression);
									string persistValue = ControlSerializer.GetPersistValue(TypeDescriptor.GetProperties(boundPropertyEntry.PropertyInfo.DeclaringType)[boundPropertyEntry.PropertyInfo.Name], boundPropertyEntry.Type, propValue, string.IsNullOrEmpty(boundPropertyEntry.ExpressionPrefix) ? ControlSerializer.BindingType.Data : ControlSerializer.BindingType.Expression, topLevelInDesigner);
									arrayList.Add(new Triplet(boundPropertyEntry.Filter, ControlSerializer.ConvertObjectModelToPersistName(boundPropertyEntry.Name), persistValue));
									break;
								}
								component = propertyDescriptor.GetValue(component);
							}
						}
					}
				}
			}
			if (obj is Control)
			{
				AttributeCollection attributeCollection = null;
				if (obj is WebControl)
				{
					attributeCollection = ((WebControl)obj).Attributes;
				}
				else if (obj is HtmlControl)
				{
					attributeCollection = ((HtmlControl)obj).Attributes;
				}
				else if (obj is UserControl)
				{
					attributeCollection = ((UserControl)obj).Attributes;
				}
				if (attributeCollection != null)
				{
					foreach (object obj3 in attributeCollection.Keys)
					{
						string text = (string)obj3;
						string text2 = attributeCollection[text];
						bool flag = false;
						if (text2 != null)
						{
							bool flag2 = false;
							string propName = ControlSerializer.ConvertPersistToObjectModelName(text);
							object obj4;
							PropertyDescriptor complexProperty = ControlDesigner.GetComplexProperty(obj, propName, out obj4);
							if (complexProperty != null && !complexProperty.IsReadOnly)
							{
								flag2 = true;
							}
							if (!flag2)
							{
								if (filter.Length == 0)
								{
									flag = true;
								}
								else
								{
									PropertyEntry propertyEntry2 = null;
									if (persistData != null)
									{
										propertyEntry2 = persistData.GetFilteredProperty(string.Empty, text);
									}
									if (propertyEntry2 is SimplePropertyEntry)
									{
										flag = !text2.Equals(((SimplePropertyEntry)propertyEntry2).PersistedValue);
									}
									else if (propertyEntry2 is BoundPropertyEntry)
									{
										string text3 = ((BoundPropertyEntry)propertyEntry2).Expression;
										string expressionPrefix = ((BoundPropertyEntry)propertyEntry2).ExpressionPrefix;
										if (expressionPrefix.Length > 0)
										{
											text3 = expressionPrefix + ":" + text3;
										}
										flag = !text2.Equals(text3);
									}
									else if (propertyEntry2 == null)
									{
										flag = true;
									}
								}
							}
							if (flag)
							{
								arrayList.Add(new Triplet(filter, text, text2));
							}
						}
					}
				}
			}
			if (obj.GetType().Equals(typeof(UserControl)) && persistData != null)
			{
				PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
				foreach (object obj5 in persistData.AllPropertyEntries)
				{
					PropertyEntry propertyEntry3 = (PropertyEntry)obj5;
					BoundPropertyEntry boundPropertyEntry2 = propertyEntry3 as BoundPropertyEntry;
					if (boundPropertyEntry2 != null && !boundPropertyEntry2.Generated && boundPropertyEntry2.UseSetAttribute && string.Equals(propertyEntry3.Filter, filter, StringComparison.OrdinalIgnoreCase) && properties.Find(boundPropertyEntry2.Name, false) == null)
					{
						string text4 = boundPropertyEntry2.Expression.Trim();
						string expressionPrefix2 = boundPropertyEntry2.ExpressionPrefix;
						ControlSerializer.BindingType bindingType = ControlSerializer.BindingType.Data;
						if (!string.IsNullOrEmpty(expressionPrefix2))
						{
							text4 = expressionPrefix2 + ":" + text4;
							bindingType = ControlSerializer.BindingType.Expression;
						}
						string persistValue2 = ControlSerializer.GetPersistValue(null, null, text4, bindingType, topLevelInDesigner);
						arrayList.Add(new Triplet(boundPropertyEntry2.Filter, ControlSerializer.ConvertObjectModelToPersistName(boundPropertyEntry2.Name), persistValue2));
					}
				}
			}
			if (persistData != null)
			{
				if (!string.IsNullOrEmpty(persistData.ResourceKey))
				{
					arrayList.Add(new Triplet("meta", "resourceKey", persistData.ResourceKey));
				}
				if (!persistData.Localize)
				{
					arrayList.Add(new Triplet("meta", "localize", "false"));
				}
				foreach (object obj6 in persistData.AllPropertyEntries)
				{
					PropertyEntry propertyEntry4 = (PropertyEntry)obj6;
					if (string.Compare(propertyEntry4.Filter, filter, StringComparison.OrdinalIgnoreCase) != 0)
					{
						string empty = string.Empty;
						if (propertyEntry4 is SimplePropertyEntry)
						{
							SimplePropertyEntry simplePropertyEntry = (SimplePropertyEntry)propertyEntry4;
							if (simplePropertyEntry.UseSetAttribute)
							{
								arrayList.Add(new Triplet(propertyEntry4.Filter, ControlSerializer.ConvertObjectModelToPersistName(propertyEntry4.Name), simplePropertyEntry.Value.ToString()));
							}
						}
						else if (propertyEntry4 is BoundPropertyEntry)
						{
							BoundPropertyEntry boundPropertyEntry3 = (BoundPropertyEntry)propertyEntry4;
							if (boundPropertyEntry3.UseSetAttribute)
							{
								string text5 = ((BoundPropertyEntry)propertyEntry4).Expression;
								string expressionPrefix3 = ((BoundPropertyEntry)propertyEntry4).ExpressionPrefix;
								if (expressionPrefix3.Length > 0)
								{
									text5 = expressionPrefix3 + ":" + text5;
								}
								arrayList.Add(new Triplet(propertyEntry4.Filter, ControlSerializer.ConvertObjectModelToPersistName(propertyEntry4.Name), text5));
							}
						}
					}
				}
			}
			if (obj is Control && persistData != null && host.GetDesigner((Control)obj) == null)
			{
				foreach (object obj7 in persistData.EventEntries)
				{
					EventEntry eventEntry = (EventEntry)obj7;
					arrayList.Add(new Triplet(string.Empty, "On" + eventEntry.Name, eventEntry.HandlerMethodName));
				}
			}
			return arrayList;
		}

		// Token: 0x060000E7 RID: 231 RVA: 0x000083D4 File Offset: 0x000065D4
		private static void SerializeAttributesRecursive(object obj, IDesignerHost host, string prefix, ObjectPersistData persistData, string filter, ArrayList attributes, DataBindingCollection dataBindings, ExpressionBindingCollection expressions, bool topLevelInDesigner)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
			if (obj is IDataBindingsAccessor)
			{
				dataBindings = ((IDataBindingsAccessor)obj).DataBindings;
			}
			if (obj is Control)
			{
				try
				{
					ControlCollection controls = ((Control)obj).Controls;
				}
				catch (Exception ex)
				{
					IComponentDesignerDebugService componentDesignerDebugService = host.GetService(typeof(IComponentDesignerDebugService)) as IComponentDesignerDebugService;
					if (componentDesignerDebugService != null)
					{
						componentDesignerDebugService.Fail(ex.Message);
					}
				}
			}
			if (obj is IExpressionsAccessor)
			{
				expressions = ((IExpressionsAccessor)obj).Expressions;
			}
			for (int i = 0; i < properties.Count; i++)
			{
				try
				{
					ControlSerializer.SerializeAttribute(obj, properties[i], dataBindings, expressions, host, prefix, persistData, filter, attributes, topLevelInDesigner);
				}
				catch (Exception ex2)
				{
					if (host != null)
					{
						IComponentDesignerDebugService componentDesignerDebugService2 = host.GetService(typeof(IComponentDesignerDebugService)) as IComponentDesignerDebugService;
						if (componentDesignerDebugService2 != null)
						{
							componentDesignerDebugService2.Fail(ex2.Message);
						}
					}
				}
			}
		}

		// Token: 0x060000E8 RID: 232 RVA: 0x000084D0 File Offset: 0x000066D0
		private static void SerializeCollectionProperty(object obj, IDesignerHost host, PropertyDescriptor propDesc, ObjectPersistData persistData, PersistenceMode persistenceMode, TextWriter writer, string filter)
		{
			string name = propDesc.Name;
			bool flag = false;
			ICollection collection = propDesc.GetValue(obj) as ICollection;
			int num = 0;
			if (collection != null)
			{
				num = collection.Count;
			}
			int num2 = 0;
			ObjectPersistData objectPersistData = null;
			if (persistData != null)
			{
				ComplexPropertyEntry complexPropertyEntry = persistData.GetFilteredProperty(string.Empty, name) as ComplexPropertyEntry;
				if (complexPropertyEntry != null)
				{
					objectPersistData = complexPropertyEntry.Builder.GetObjectPersistData();
					num2 = objectPersistData.CollectionItems.Count;
				}
			}
			if (filter.Length == 0)
			{
				flag = true;
			}
			else if (persistData != null)
			{
				if (persistData.GetFilteredProperty(filter, name) is ComplexPropertyEntry)
				{
					flag = true;
				}
				else if (num2 != num)
				{
					flag = true;
				}
				else if (objectPersistData != null)
				{
					IEnumerator enumerator = collection.GetEnumerator();
					IEnumerator enumerator2 = objectPersistData.CollectionItems.GetEnumerator();
					while (enumerator.MoveNext())
					{
						enumerator2.MoveNext();
						ComplexPropertyEntry complexPropertyEntry2 = (ComplexPropertyEntry)enumerator2.Current;
						if (enumerator.Current.GetType() != complexPropertyEntry2.Builder.ControlType)
						{
							flag = true;
							break;
						}
					}
				}
			}
			bool flag2 = false;
			ArrayList arrayList = new ArrayList();
			if (num > 0)
			{
				StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
				IDictionary dictionary = new Hashtable(ControlSerializer.ReferenceKeyComparer.Default);
				if (objectPersistData != null)
				{
					foreach (object obj2 in objectPersistData.CollectionItems)
					{
						ComplexPropertyEntry complexPropertyEntry3 = (ComplexPropertyEntry)obj2;
						ObjectPersistData objectPersistData2 = complexPropertyEntry3.Builder.GetObjectPersistData();
						if (objectPersistData2 != null)
						{
							objectPersistData2.AddToObjectControlBuilderTable(dictionary);
						}
					}
				}
				if (!flag)
				{
					flag2 = true;
					foreach (object obj3 in collection)
					{
						string tagName = ControlSerializer.GetTagName(obj3.GetType(), host);
						ObjectPersistData persistData2 = null;
						ControlBuilder controlBuilder = (ControlBuilder)dictionary[obj3];
						if (controlBuilder != null)
						{
							persistData2 = controlBuilder.GetObjectPersistData();
						}
						stringWriter.Write('<');
						stringWriter.Write(tagName);
						ControlSerializer.SerializeAttributes(obj3, host, string.Empty, persistData2, stringWriter, filter);
						stringWriter.Write('>');
						ControlSerializer.SerializeInnerProperties(obj3, host, persistData2, stringWriter, filter);
						stringWriter.Write("</");
						stringWriter.Write(tagName);
						stringWriter.WriteLine('>');
					}
					IDictionary expandos = ControlSerializer.GetExpandos(filter, name, objectPersistData);
					arrayList.Add(new Triplet(string.Empty, stringWriter, expandos));
				}
				else
				{
					foreach (object obj4 in collection)
					{
						string tagName2 = ControlSerializer.GetTagName(obj4.GetType(), host);
						if (obj4 is Control)
						{
							ControlSerializer.SerializeControl((Control)obj4, host, stringWriter, string.Empty);
						}
						else
						{
							stringWriter.Write('<');
							stringWriter.Write(tagName2);
							ObjectPersistData objectPersistData3 = null;
							ControlBuilder controlBuilder2 = (ControlBuilder)dictionary[obj4];
							if (controlBuilder2 != null)
							{
								objectPersistData3 = controlBuilder2.GetObjectPersistData();
							}
							if (filter.Length == 0 && objectPersistData3 != null)
							{
								ControlSerializer.SerializeAttributes(obj4, host, string.Empty, objectPersistData3, stringWriter, string.Empty);
								stringWriter.Write('>');
								ControlSerializer.SerializeInnerProperties(obj4, host, objectPersistData3, stringWriter, string.Empty);
							}
							else
							{
								ControlSerializer.SerializeAttributes(obj4, host, string.Empty, null, stringWriter, string.Empty);
								stringWriter.Write('>');
								ControlSerializer.SerializeInnerProperties(obj4, host, null, stringWriter, string.Empty);
							}
							stringWriter.Write("</");
							stringWriter.Write(tagName2);
							stringWriter.WriteLine('>');
						}
					}
					IDictionary expandos2 = ControlSerializer.GetExpandos(filter, name, persistData);
					arrayList.Add(new Triplet(filter, stringWriter, expandos2));
				}
			}
			else if (num2 > 0)
			{
				IDictionary expandos3 = ControlSerializer.GetExpandos(filter, name, persistData);
				arrayList.Add(new Triplet(filter, new StringWriter(CultureInfo.InvariantCulture), expandos3));
			}
			if (persistData != null)
			{
				ICollection propertyAllFilters = persistData.GetPropertyAllFilters(name);
				foreach (object obj5 in propertyAllFilters)
				{
					ComplexPropertyEntry complexPropertyEntry4 = (ComplexPropertyEntry)obj5;
					StringWriter stringWriter2 = new StringWriter(CultureInfo.InvariantCulture);
					if (string.Compare(complexPropertyEntry4.Filter, filter, StringComparison.OrdinalIgnoreCase) != 0 && (!flag2 || complexPropertyEntry4.Filter.Length > 0))
					{
						ObjectPersistData objectPersistData4 = complexPropertyEntry4.Builder.GetObjectPersistData();
						IEnumerator enumerator7 = objectPersistData4.CollectionItems.GetEnumerator();
						foreach (object obj6 in objectPersistData4.CollectionItems)
						{
							ComplexPropertyEntry complexPropertyEntry5 = (ComplexPropertyEntry)obj6;
							object obj7 = complexPropertyEntry5.Builder.BuildObject();
							if (obj7 is Control)
							{
								ControlSerializer.SerializeControl((Control)obj7, host, stringWriter2, string.Empty);
							}
							else
							{
								string tagName3 = ControlSerializer.GetTagName(obj7.GetType(), host);
								ObjectPersistData objectPersistData5 = complexPropertyEntry5.Builder.GetObjectPersistData();
								stringWriter2.Write('<');
								stringWriter2.Write(tagName3);
								ControlSerializer.SerializeAttributes(obj7, host, string.Empty, objectPersistData5, stringWriter2, string.Empty);
								stringWriter2.Write('>');
								ControlSerializer.SerializeInnerProperties(obj7, host, objectPersistData5, stringWriter2, string.Empty);
								stringWriter2.Write("</");
								stringWriter2.Write(tagName3);
								stringWriter2.WriteLine('>');
							}
						}
						IDictionary expandos4 = ControlSerializer.GetExpandos(complexPropertyEntry4.Filter, name, persistData);
						arrayList.Add(new Triplet(complexPropertyEntry4.Filter, stringWriter2, expandos4));
					}
				}
			}
			foreach (object obj8 in arrayList)
			{
				Triplet triplet = (Triplet)obj8;
				string text = triplet.First.ToString();
				IDictionary dictionary2 = (IDictionary)triplet.Third;
				if (arrayList.Count == 1 && text.Length == 0 && persistenceMode != PersistenceMode.InnerProperty && (dictionary2 == null || dictionary2.Count == 0))
				{
					writer.Write(triplet.Second.ToString());
				}
				else
				{
					string text2 = triplet.Second.ToString().Trim();
					if (text2.Length > 0)
					{
						ControlSerializer.WriteInnerPropertyBeginTag(writer, text, name, dictionary2, true);
						writer.WriteLine(text2);
						ControlSerializer.WriteInnerPropertyEndTag(writer, text, name);
					}
				}
			}
		}

		// Token: 0x060000E9 RID: 233 RVA: 0x00008BCC File Offset: 0x00006DCC
		private static void SerializeComplexProperty(object obj, IDesignerHost host, PropertyDescriptor propDesc, ObjectPersistData persistData, TextWriter writer, string filter)
		{
			string name = propDesc.Name;
			object value = propDesc.GetValue(obj);
			ObjectPersistData persistData2 = null;
			if (persistData != null)
			{
				ComplexPropertyEntry complexPropertyEntry = persistData.GetFilteredProperty(string.Empty, name) as ComplexPropertyEntry;
				if (complexPropertyEntry != null)
				{
					persistData2 = complexPropertyEntry.Builder.GetObjectPersistData();
				}
			}
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			ControlSerializer.SerializeInnerProperties(value, host, persistData2, stringWriter, filter);
			string text = stringWriter.ToString();
			ArrayList arrayList = ControlSerializer.SerializeAttributes(value, host, string.Empty, persistData2, filter, false);
			StringWriter stringWriter2 = new StringWriter(CultureInfo.InvariantCulture);
			bool flag = true;
			foreach (object obj2 in arrayList)
			{
				Triplet triplet = (Triplet)obj2;
				string text2 = triplet.First.ToString();
				if (text2 != ControlBuilder.DesignerFilter)
				{
					flag = false;
				}
				ControlSerializer.WriteAttribute(stringWriter2, text2, triplet.Second.ToString(), triplet.Third.ToString());
			}
			string text3 = string.Empty;
			if (!flag || text.Length > 0)
			{
				text3 = stringWriter2.ToString();
			}
			if (text3.Length + text.Length > 0)
			{
				writer.WriteLine();
				writer.Write('<');
				writer.Write(name);
				writer.Write(text3);
				writer.Write('>');
				writer.Write(text);
				ControlSerializer.WriteInnerPropertyEndTag(writer, string.Empty, name);
			}
			if (persistData != null)
			{
				ICollection propertyAllFilters = persistData.GetPropertyAllFilters(name);
				foreach (object obj3 in propertyAllFilters)
				{
					ComplexPropertyEntry complexPropertyEntry2 = (ComplexPropertyEntry)obj3;
					if (complexPropertyEntry2.Filter.Length > 0)
					{
						object obj4 = complexPropertyEntry2.Builder.BuildObject();
						writer.WriteLine();
						writer.Write('<');
						writer.Write(complexPropertyEntry2.Filter);
						writer.Write(':');
						writer.Write(name);
						ControlSerializer.SerializeAttributes(obj4, host, string.Empty, null, writer, string.Empty);
						writer.Write('>');
						ControlSerializer.SerializeInnerProperties(obj4, host, null, writer, string.Empty);
						ControlSerializer.WriteInnerPropertyEndTag(writer, complexPropertyEntry2.Filter, name);
					}
				}
			}
		}

		// Token: 0x060000EA RID: 234 RVA: 0x00008E2C File Offset: 0x0000702C
		public static string SerializeControl(Control control)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			ControlSerializer.SerializeControl(control, stringWriter);
			return stringWriter.ToString();
		}

		// Token: 0x060000EB RID: 235 RVA: 0x00008E54 File Offset: 0x00007054
		public static string SerializeControl(Control control, IDesignerHost host)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			ControlSerializer.SerializeControl(control, host, stringWriter);
			return stringWriter.ToString();
		}

		// Token: 0x060000EC RID: 236 RVA: 0x00008E7C File Offset: 0x0000707C
		public static void SerializeControl(Control control, TextWriter writer)
		{
			ISite site = control.Site;
			if (site == null)
			{
				IComponent page = control.Page;
				if (page != null)
				{
					site = page.Site;
				}
			}
			IDesignerHost host = null;
			if (site != null)
			{
				host = (IDesignerHost)site.GetService(typeof(IDesignerHost));
			}
			ControlSerializer.SerializeControl(control, host, writer);
		}

		// Token: 0x060000ED RID: 237 RVA: 0x00008EC7 File Offset: 0x000070C7
		public static void SerializeControl(Control control, IDesignerHost host, TextWriter writer)
		{
			ControlSerializer.SerializeControl(control, host, writer, ControlSerializer.GetCurrentFilter(host));
		}

		// Token: 0x060000EE RID: 238 RVA: 0x00008ED8 File Offset: 0x000070D8
		private static void SerializeControl(Control control, IDesignerHost host, TextWriter writer, string filter)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (host == null)
			{
				throw new ArgumentNullException("host");
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (control is LiteralControl)
			{
				writer.Write(((LiteralControl)control).Text);
				return;
			}
			if (control is DesignerDataBoundLiteralControl)
			{
				DataBindingCollection dataBindings = ((IDataBindingsAccessor)control).DataBindings;
				DataBinding dataBinding = dataBindings["Text"];
				if (dataBinding != null)
				{
					writer.Write("<%# ");
					writer.Write(dataBinding.Expression);
					writer.Write(" %>");
					return;
				}
			}
			else if (control is UserControl)
			{
				IUserControlDesignerAccessor userControlDesignerAccessor = (IUserControlDesignerAccessor)control;
				string tagName = userControlDesignerAccessor.TagName;
				if (tagName.Length > 0)
				{
					writer.Write('<');
					writer.Write(tagName);
					writer.Write(" runat=\"server\"");
					ObjectPersistData persistData = null;
					if (((IControlBuilderAccessor)control).ControlBuilder != null)
					{
						persistData = ((IControlBuilderAccessor)control).ControlBuilder.GetObjectPersistData();
					}
					ControlSerializer.SerializeAttributes(control, host, string.Empty, persistData, writer, filter);
					writer.Write('>');
					string innerText = userControlDesignerAccessor.InnerText;
					if (innerText != null && innerText.Length > 0)
					{
						writer.Write(userControlDesignerAccessor.InnerText);
					}
					writer.Write("</");
					writer.Write(tagName);
					writer.WriteLine('>');
					return;
				}
			}
			else
			{
				HtmlControl htmlControl = control as HtmlControl;
				string tagName2;
				if (htmlControl != null)
				{
					tagName2 = htmlControl.TagName;
				}
				else
				{
					tagName2 = ControlSerializer.GetTagName(control.GetType(), host);
				}
				writer.Write('<');
				writer.Write(tagName2);
				writer.Write(" runat=\"server\"");
				ObjectPersistData persistData2 = null;
				if (((IControlBuilderAccessor)control).ControlBuilder != null)
				{
					persistData2 = ((IControlBuilderAccessor)control).ControlBuilder.GetObjectPersistData();
				}
				ControlSerializer.SerializeAttributes(control, host, string.Empty, persistData2, writer, filter);
				writer.Write('>');
				ControlSerializer.SerializeInnerContents(control, host, persistData2, writer, filter);
				writer.Write("</");
				writer.Write(tagName2);
				writer.WriteLine('>');
			}
		}

		// Token: 0x060000EF RID: 239 RVA: 0x000090BC File Offset: 0x000072BC
		public static string SerializeInnerContents(Control control, IDesignerHost host)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			ObjectPersistData persistData = null;
			if (((IControlBuilderAccessor)control).ControlBuilder != null)
			{
				persistData = ((IControlBuilderAccessor)control).ControlBuilder.GetObjectPersistData();
			}
			ControlSerializer.SerializeInnerContents(control, host, persistData, stringWriter, ControlSerializer.GetCurrentFilter(host));
			return stringWriter.ToString();
		}

		// Token: 0x060000F0 RID: 240 RVA: 0x00009104 File Offset: 0x00007304
		internal static void SerializeInnerContents(Control control, IDesignerHost host, ObjectPersistData persistData, TextWriter writer, string filter)
		{
			PersistChildrenAttribute persistChildrenAttribute = (PersistChildrenAttribute)TypeDescriptor.GetAttributes(control)[typeof(PersistChildrenAttribute)];
			ParseChildrenAttribute parseChildrenAttribute = (ParseChildrenAttribute)TypeDescriptor.GetAttributes(control)[typeof(ParseChildrenAttribute)];
			if (persistChildrenAttribute.Persist || (!parseChildrenAttribute.ChildrenAsProperties && control.HasControls()))
			{
				for (int i = 0; i < control.Controls.Count; i++)
				{
					ControlSerializer.SerializeControl(control.Controls[i], host, writer, string.Empty);
				}
				return;
			}
			ControlSerializer.SerializeInnerProperties(control, host, persistData, writer, filter);
		}

		// Token: 0x060000F1 RID: 241 RVA: 0x0000919C File Offset: 0x0000739C
		public static string SerializeInnerProperties(object obj, IDesignerHost host)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			ControlSerializer.SerializeInnerProperties(obj, host, stringWriter);
			return stringWriter.ToString();
		}

		// Token: 0x060000F2 RID: 242 RVA: 0x000091C4 File Offset: 0x000073C4
		internal static void SerializeInnerProperties(object obj, IDesignerHost host, TextWriter writer)
		{
			ObjectPersistData persistData = null;
			IControlBuilderAccessor controlBuilderAccessor = (IControlBuilderAccessor)obj;
			if (controlBuilderAccessor.ControlBuilder != null)
			{
				persistData = controlBuilderAccessor.ControlBuilder.GetObjectPersistData();
			}
			ControlSerializer.SerializeInnerProperties(obj, host, persistData, writer, ControlSerializer.GetCurrentFilter(host));
		}

		// Token: 0x060000F3 RID: 243 RVA: 0x00009200 File Offset: 0x00007400
		private static void SerializeInnerProperties(object obj, IDesignerHost host, ObjectPersistData persistData, TextWriter writer, string filter)
		{
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
			if (obj is Control)
			{
				try
				{
					ControlCollection controls = ((Control)obj).Controls;
				}
				catch (Exception ex)
				{
					IComponentDesignerDebugService componentDesignerDebugService = host.GetService(typeof(IComponentDesignerDebugService)) as IComponentDesignerDebugService;
					if (componentDesignerDebugService != null)
					{
						componentDesignerDebugService.Fail(ex.Message);
					}
				}
			}
			for (int i = 0; i < properties.Count; i++)
			{
				try
				{
					if (!FilterableAttribute.IsPropertyFilterable(properties[i]))
					{
						string empty = string.Empty;
					}
					if (properties[i].SerializationVisibility != DesignerSerializationVisibility.Hidden)
					{
						PersistenceModeAttribute persistenceModeAttribute = (PersistenceModeAttribute)properties[i].Attributes[typeof(PersistenceModeAttribute)];
						if (persistenceModeAttribute.Mode != PersistenceMode.Attribute)
						{
							DesignOnlyAttribute designOnlyAttribute = (DesignOnlyAttribute)properties[i].Attributes[typeof(DesignOnlyAttribute)];
							if (designOnlyAttribute == null || !designOnlyAttribute.IsDesignOnly)
							{
								string name = properties[i].Name;
								if (properties[i].PropertyType == typeof(string))
								{
									ControlSerializer.SerializeStringProperty(obj, host, properties[i], persistData, persistenceModeAttribute.Mode, writer, filter);
								}
								else if (typeof(ICollection).IsAssignableFrom(properties[i].PropertyType))
								{
									ControlSerializer.SerializeCollectionProperty(obj, host, properties[i], persistData, persistenceModeAttribute.Mode, writer, filter);
								}
								else if (typeof(ITemplate).IsAssignableFrom(properties[i].PropertyType))
								{
									ControlSerializer.SerializeTemplateProperty(obj, host, properties[i], persistData, writer, filter);
								}
								else
								{
									ControlSerializer.SerializeComplexProperty(obj, host, properties[i], persistData, writer, filter);
								}
							}
						}
					}
				}
				catch (Exception ex2)
				{
					if (host != null)
					{
						IComponentDesignerDebugService componentDesignerDebugService2 = host.GetService(typeof(IComponentDesignerDebugService)) as IComponentDesignerDebugService;
						if (componentDesignerDebugService2 != null)
						{
							componentDesignerDebugService2.Fail(ex2.Message);
						}
					}
				}
			}
		}

		// Token: 0x060000F4 RID: 244 RVA: 0x00009434 File Offset: 0x00007634
		private static void SerializeStringProperty(object obj, IDesignerHost host, PropertyDescriptor propDesc, ObjectPersistData persistData, PersistenceMode persistenceMode, TextWriter writer, string filter)
		{
			string name = propDesc.Name;
			DataBindingCollection dataBindingCollection = null;
			if (obj is IDataBindingsAccessor)
			{
				dataBindingCollection = ((IDataBindingsAccessor)obj).DataBindings;
			}
			ExpressionBindingCollection expressionBindingCollection = null;
			if (obj is IExpressionsAccessor)
			{
				expressionBindingCollection = ((IExpressionsAccessor)obj).Expressions;
			}
			if (persistenceMode != PersistenceMode.InnerProperty && !ControlSerializer.CanSerializeAsInnerDefaultString(filter, name, propDesc.PropertyType, persistData, persistenceMode, dataBindingCollection, expressionBindingCollection))
			{
				return;
			}
			ArrayList arrayList = new ArrayList();
			if (dataBindingCollection == null || dataBindingCollection[name] == null || expressionBindingCollection == null || expressionBindingCollection[name] == null)
			{
				string y = string.Empty;
				object value = propDesc.GetValue(obj);
				if (value != null)
				{
					y = value.ToString();
				}
				bool flag2;
				if (filter.Length == 0)
				{
					bool flag;
					bool shouldSerializeValue = ControlSerializer.GetShouldSerializeValue(obj, name, out flag);
					if (flag)
					{
						flag2 = shouldSerializeValue;
					}
					else
					{
						object propertyDefaultValue = ControlSerializer.GetPropertyDefaultValue(propDesc, name, persistData, filter, host);
						flag2 = !object.Equals(value, propertyDefaultValue);
					}
				}
				else
				{
					object propertyDefaultValue2 = ControlSerializer.GetPropertyDefaultValue(propDesc, name, persistData, filter, host);
					flag2 = !object.Equals(value, propertyDefaultValue2);
				}
				if (flag2)
				{
					IDictionary expandos = ControlSerializer.GetExpandos(filter, name, persistData);
					arrayList.Add(new Triplet(filter, y, expandos));
				}
			}
			if (persistData != null)
			{
				ICollection propertyAllFilters = persistData.GetPropertyAllFilters(name);
				foreach (object obj2 in propertyAllFilters)
				{
					PropertyEntry propertyEntry = (PropertyEntry)obj2;
					if (string.Compare(propertyEntry.Filter, filter, StringComparison.OrdinalIgnoreCase) != 0 && propertyEntry is ComplexPropertyEntry)
					{
						ComplexPropertyEntry complexPropertyEntry = (ComplexPropertyEntry)propertyEntry;
						object obj3 = complexPropertyEntry.Builder.BuildObject();
						string y2 = obj3.ToString();
						IDictionary expandos2 = ControlSerializer.GetExpandos(propertyEntry.Filter, name, persistData);
						arrayList.Add(new Triplet(propertyEntry.Filter, y2, expandos2));
					}
				}
			}
			foreach (object obj4 in arrayList)
			{
				Triplet triplet = (Triplet)obj4;
				bool flag3 = false;
				IDictionary dictionary = triplet.Third as IDictionary;
				if (arrayList.Count == 1 && triplet.First.ToString().Length == 0 && (dictionary == null || dictionary.Count == 0))
				{
					if (persistenceMode == PersistenceMode.InnerDefaultProperty)
					{
						writer.Write(triplet.Second.ToString());
						flag3 = true;
					}
					else if (persistenceMode == PersistenceMode.EncodedInnerDefaultProperty)
					{
						HttpUtility.HtmlEncode(triplet.Second.ToString(), writer);
						flag3 = true;
					}
				}
				if (!flag3)
				{
					string filter2 = triplet.First.ToString();
					ControlSerializer.WriteInnerPropertyBeginTag(writer, filter2, name, dictionary, true);
					writer.Write(triplet.Second.ToString());
					ControlSerializer.WriteInnerPropertyEndTag(writer, filter2, name);
				}
			}
		}

		// Token: 0x060000F5 RID: 245 RVA: 0x000096F8 File Offset: 0x000078F8
		private static void SerializeTemplateProperty(object obj, IDesignerHost host, PropertyDescriptor propDesc, ObjectPersistData persistData, TextWriter writer, string filter)
		{
			string name = propDesc.Name;
			string text = string.Empty;
			ITemplate template = (ITemplate)propDesc.GetValue(obj);
			if (template != null)
			{
				text = ControlSerializer.SerializeTemplate(template, host);
				string a = string.Empty;
				if (filter.Length > 0 && persistData != null)
				{
					TemplatePropertyEntry templatePropertyEntry = persistData.GetFilteredProperty(string.Empty, name) as TemplatePropertyEntry;
					if (templatePropertyEntry != null)
					{
						a = ControlSerializer.SerializeTemplate(templatePropertyEntry.Builder as ITemplate, host);
					}
				}
				IDictionary expandos = ControlSerializer.GetExpandos(filter, name, persistData);
				if ((template != null && expandos != null && expandos.Count > 0) || !string.Equals(a, text))
				{
					ControlSerializer.WriteInnerPropertyBeginTag(writer, filter, name, expandos, false);
					if (text.Length > 0 && !text.StartsWith("\r\n", StringComparison.Ordinal))
					{
						writer.WriteLine();
					}
					writer.Write(text);
					if (text.Length > 0 && !text.EndsWith("\r\n", StringComparison.Ordinal))
					{
						writer.WriteLine();
					}
					ControlSerializer.WriteInnerPropertyEndTag(writer, filter, name);
				}
			}
			if (persistData != null)
			{
				ICollection propertyAllFilters = persistData.GetPropertyAllFilters(name);
				foreach (object obj2 in propertyAllFilters)
				{
					TemplatePropertyEntry templatePropertyEntry2 = (TemplatePropertyEntry)obj2;
					if (string.Compare(templatePropertyEntry2.Filter, filter, StringComparison.OrdinalIgnoreCase) != 0)
					{
						IDictionary expandos2 = ControlSerializer.GetExpandos(templatePropertyEntry2.Filter, name, persistData);
						ControlSerializer.WriteInnerPropertyBeginTag(writer, templatePropertyEntry2.Filter, name, expandos2, false);
						string text2 = ControlSerializer.SerializeTemplate((ITemplate)templatePropertyEntry2.Builder, host);
						if (text2 != null)
						{
							if (!text2.StartsWith("\r\n", StringComparison.Ordinal))
							{
								writer.WriteLine();
							}
							writer.Write(text2);
							if (!text2.EndsWith("\r\n", StringComparison.Ordinal))
							{
								writer.WriteLine();
							}
							ControlSerializer.WriteInnerPropertyEndTag(writer, templatePropertyEntry2.Filter, name);
						}
					}
				}
			}
		}

		// Token: 0x060000F6 RID: 246 RVA: 0x000098D8 File Offset: 0x00007AD8
		public static string SerializeTemplate(ITemplate template, IDesignerHost host)
		{
			StringWriter stringWriter = new StringWriter(CultureInfo.InvariantCulture);
			ControlSerializer.SerializeTemplate(template, stringWriter, host);
			return stringWriter.ToString();
		}

		// Token: 0x060000F7 RID: 247 RVA: 0x00009900 File Offset: 0x00007B00
		public static void SerializeTemplate(ITemplate template, TextWriter writer, IDesignerHost host)
		{
			if (template == null)
			{
				return;
			}
			if (writer == null)
			{
				throw new ArgumentNullException("writer");
			}
			if (template is TemplateBuilder)
			{
				writer.Write(((TemplateBuilder)template).Text);
			}
			else
			{
				Control control = new Control();
				StringBuilder stringBuilder = new StringBuilder();
				try
				{
					template.InstantiateIn(control);
					foreach (object obj in control.Controls)
					{
						Control control2 = (Control)obj;
						stringBuilder.Append(ControlSerializer.SerializeControl(control2, host));
					}
					writer.Write(stringBuilder.ToString());
				}
				catch (Exception ex)
				{
				}
			}
			writer.Flush();
		}

		// Token: 0x060000F8 RID: 248 RVA: 0x000099C8 File Offset: 0x00007BC8
		private static bool ShouldPersistBlankValue(object defValue, Type type)
		{
			if (type == typeof(string))
			{
				return !defValue.Equals("");
			}
			if (type == typeof(Color))
			{
				return !((Color)defValue).IsEmpty;
			}
			if (type == typeof(FontUnit))
			{
				return !((FontUnit)defValue).IsEmpty;
			}
			return type == typeof(Unit) && !defValue.Equals(Unit.Empty);
		}

		// Token: 0x060000F9 RID: 249 RVA: 0x00009A68 File Offset: 0x00007C68
		private static void WriteAttribute(TextWriter writer, string filter, string name, string value)
		{
			writer.Write(" ");
			if (filter != null && filter.Length > 0)
			{
				writer.Write(filter);
				writer.Write(':');
			}
			writer.Write(name);
			if (value.IndexOf('"') > -1)
			{
				writer.Write("='");
				writer.Write(value);
				writer.Write("'");
				return;
			}
			writer.Write("=\"");
			writer.Write(value);
			writer.Write("\"");
		}

		// Token: 0x060000FA RID: 250 RVA: 0x00009AE8 File Offset: 0x00007CE8
		private static void WriteInnerPropertyBeginTag(TextWriter writer, string filter, string name, IDictionary expandos, bool requiresNewLine)
		{
			writer.Write('<');
			if (filter != null && filter.Length > 0)
			{
				writer.Write(filter);
				writer.Write(':');
			}
			writer.Write(name);
			if (expandos != null)
			{
				foreach (object obj in expandos)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					SimplePropertyEntry simplePropertyEntry = dictionaryEntry.Value as SimplePropertyEntry;
					if (simplePropertyEntry != null)
					{
						ControlSerializer.WriteAttribute(writer, ControlBuilder.DesignerFilter, dictionaryEntry.Key.ToString(), simplePropertyEntry.Value.ToString());
					}
				}
			}
			if (requiresNewLine)
			{
				writer.WriteLine('>');
				return;
			}
			writer.Write('>');
		}

		// Token: 0x060000FB RID: 251 RVA: 0x00009BAC File Offset: 0x00007DAC
		private static void WriteInnerPropertyEndTag(TextWriter writer, string filter, string name)
		{
			writer.Write("</");
			if (filter != null && filter.Length > 0)
			{
				writer.Write(filter);
				writer.Write(':');
			}
			writer.Write(name);
			writer.WriteLine('>');
		}

		// Token: 0x040000E3 RID: 227
		private static readonly object licenseManagerLock = new object();

		// Token: 0x040000E4 RID: 228
		private static bool licenseManagerLockHeld = false;

		// Token: 0x040000E5 RID: 229
		private const char PERSIST_CHAR = '-';

		// Token: 0x040000E6 RID: 230
		private const char OM_CHAR = '.';

		// Token: 0x040000E7 RID: 231
		private const char FILTER_SEPARATOR_CHAR = ':';

		// Token: 0x020003A6 RID: 934
		private sealed class WebFormsDesigntimeLicenseContext : DesigntimeLicenseContext
		{
			// Token: 0x060025DF RID: 9695 RVA: 0x000EBFBC File Offset: 0x000EA1BC
			public WebFormsDesigntimeLicenseContext(IServiceProvider provider)
			{
				this.provider = provider;
			}

			// Token: 0x060025E0 RID: 9696 RVA: 0x000EBFCB File Offset: 0x000EA1CB
			public override object GetService(Type serviceClass)
			{
				if (this.provider != null)
				{
					return this.provider.GetService(serviceClass);
				}
				return null;
			}

			// Token: 0x04001B86 RID: 7046
			private IServiceProvider provider;
		}

		// Token: 0x020003A7 RID: 935
		private enum BindingType
		{
			// Token: 0x04001B88 RID: 7048
			None,
			// Token: 0x04001B89 RID: 7049
			Data,
			// Token: 0x04001B8A RID: 7050
			Expression
		}

		// Token: 0x020003A8 RID: 936
		private class ReferenceKeyComparer : IEqualityComparer
		{
			// Token: 0x060025E1 RID: 9697 RVA: 0x000EBFE3 File Offset: 0x000EA1E3
			bool IEqualityComparer.Equals(object x, object y)
			{
				return x == y;
			}

			// Token: 0x060025E2 RID: 9698 RVA: 0x000EBFE9 File Offset: 0x000EA1E9
			int IEqualityComparer.GetHashCode(object obj)
			{
				return obj.GetHashCode();
			}

			// Token: 0x04001B8B RID: 7051
			internal static readonly ControlSerializer.ReferenceKeyComparer Default = new ControlSerializer.ReferenceKeyComparer();
		}
	}
}

using System;
using System.Collections;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Design;
using System.Text;
using System.Web.Compilation;

namespace System.Web.UI.Design
{
	// Token: 0x0200001A RID: 26
	internal static class ControlLocalizer
	{
		// Token: 0x060000BD RID: 189 RVA: 0x00006100 File Offset: 0x00004300
		private static bool IsPropertyLocalizable(PropertyDescriptor propertyDescriptor)
		{
			DesignerSerializationVisibilityAttribute designerSerializationVisibilityAttribute = (DesignerSerializationVisibilityAttribute)propertyDescriptor.Attributes[typeof(DesignerSerializationVisibilityAttribute)];
			if (designerSerializationVisibilityAttribute != null && designerSerializationVisibilityAttribute.Visibility == DesignerSerializationVisibility.Hidden)
			{
				return false;
			}
			LocalizableAttribute localizableAttribute = (LocalizableAttribute)propertyDescriptor.Attributes[typeof(LocalizableAttribute)];
			return localizableAttribute != null && localizableAttribute.IsLocalizable;
		}

		// Token: 0x060000BE RID: 190 RVA: 0x0000615C File Offset: 0x0000435C
		public static string LocalizeControl(Control control, IDesignTimeResourceWriter resourceWriter, out string newInnerContent)
		{
			if (control == null)
			{
				throw new ArgumentNullException("control");
			}
			if (resourceWriter == null)
			{
				throw new ArgumentNullException("resourceWriter");
			}
			if (control.Site == null)
			{
				throw new InvalidOperationException();
			}
			IDesignerHost designerHost = (IDesignerHost)control.Site.GetService(typeof(IDesignerHost));
			IDesignerHost designerHost2 = new ControlLocalizer.LocalizationDesignerHost(designerHost);
			ControlDesigner controlDesigner = designerHost.GetDesigner(control) as ControlDesigner;
			Control control2 = controlDesigner.CreateClonedControl(designerHost2, false);
			((IControlDesignerAccessor)control2).SetOwnerControl(control);
			bool flag = ControlLocalizer.ShouldLocalizeInnerContents(control.Site, control);
			string result = ControlLocalizer.LocalizeControl(control2, designerHost2, resourceWriter, flag);
			if (flag)
			{
				newInnerContent = ControlSerializer.SerializeInnerContents(control2, designerHost2);
			}
			else
			{
				newInnerContent = null;
			}
			return result;
		}

		// Token: 0x060000BF RID: 191 RVA: 0x00006200 File Offset: 0x00004400
		private static string LocalizeControl(Control control, IServiceProvider serviceProvider, IDesignTimeResourceWriter resourceWriter, bool shouldLocalizeInnerContent)
		{
			ResourceExpressionEditor resourceExpressionEditor = (ResourceExpressionEditor)ExpressionEditor.GetExpressionEditor("resources", serviceProvider);
			ControlBuilder controlBuilder = ((IControlBuilderAccessor)control).ControlBuilder;
			ObjectPersistData objectPersistData = controlBuilder.GetObjectPersistData();
			string resourceKey = controlBuilder.GetResourceKey();
			string text = ControlLocalizer.LocalizeObject(serviceProvider, control, objectPersistData, resourceExpressionEditor, resourceWriter, resourceKey, string.Empty, control, string.Empty, shouldLocalizeInnerContent, false, false);
			if (!string.Equals(resourceKey, text, StringComparison.OrdinalIgnoreCase))
			{
				controlBuilder.SetResourceKey(text);
			}
			if (objectPersistData != null)
			{
				foreach (object obj in objectPersistData.AllPropertyEntries)
				{
					PropertyEntry propertyEntry = (PropertyEntry)obj;
					BoundPropertyEntry boundPropertyEntry = propertyEntry as BoundPropertyEntry;
					if (boundPropertyEntry != null && !boundPropertyEntry.Generated)
					{
						string[] array = boundPropertyEntry.Name.Split(new char[]
						{
							'.'
						});
						if (array.Length > 1)
						{
							object component = control;
							string[] array2 = array;
							int i = 0;
							while (i < array2.Length)
							{
								string name = array2[i];
								PropertyDescriptor propertyDescriptor = TypeDescriptor.GetProperties(component)[name];
								if (propertyDescriptor == null)
								{
									break;
								}
								PersistenceModeAttribute persistenceModeAttribute = propertyDescriptor.Attributes[typeof(PersistenceModeAttribute)] as PersistenceModeAttribute;
								if (persistenceModeAttribute != PersistenceModeAttribute.Attribute)
								{
									if (!string.Equals(boundPropertyEntry.ExpressionPrefix, "resources", StringComparison.OrdinalIgnoreCase))
									{
										break;
									}
									ResourceExpressionFields resourceExpressionFields = boundPropertyEntry.ParsedExpressionData as ResourceExpressionFields;
									if (resourceExpressionFields != null && string.IsNullOrEmpty(resourceExpressionFields.ClassKey))
									{
										object obj2 = resourceExpressionEditor.EvaluateExpression(boundPropertyEntry.Expression, boundPropertyEntry.ParsedExpressionData, boundPropertyEntry.PropertyInfo.PropertyType, serviceProvider);
										if (obj2 == null)
										{
											object component2;
											PropertyDescriptor complexProperty = ControlDesigner.GetComplexProperty(control, boundPropertyEntry.Name, out component2);
											obj2 = complexProperty.GetValue(component2);
										}
										resourceWriter.AddResource(resourceExpressionFields.ResourceKey, obj2);
										break;
									}
									break;
								}
								else
								{
									component = propertyDescriptor.GetValue(component);
									i++;
								}
							}
						}
					}
				}
			}
			return text;
		}

		// Token: 0x060000C0 RID: 192 RVA: 0x0000640C File Offset: 0x0000460C
		private static bool ShouldLocalizeInnerContents(IServiceProvider serviceProvider, object obj)
		{
			Control control = obj as Control;
			if (control == null)
			{
				return false;
			}
			IDesignerHost designerHost = (IDesignerHost)serviceProvider.GetService(typeof(IDesignerHost));
			if (designerHost == null)
			{
				return false;
			}
			ControlDesigner controlDesigner = designerHost.GetDesigner(control) as ControlDesigner;
			return controlDesigner == null || controlDesigner.ReadOnlyInternal;
		}

		// Token: 0x060000C1 RID: 193 RVA: 0x0000645C File Offset: 0x0000465C
		private static bool ParseChildren(Type controlType)
		{
			object[] customAttributes = controlType.GetCustomAttributes(typeof(ParseChildrenAttribute), true);
			return customAttributes != null && customAttributes.Length != 0 && ((ParseChildrenAttribute)customAttributes[0]).ChildrenAsProperties;
		}

		// Token: 0x060000C2 RID: 194 RVA: 0x00006494 File Offset: 0x00004694
		private static string LocalizeObject(IServiceProvider serviceProvider, object obj, ObjectPersistData persistData, ResourceExpressionEditor resEditor, IDesignTimeResourceWriter resourceWriter, string resourceKey, string objectModelName, object topLevelObject, string filter, bool shouldLocalizeInnerContent, bool isComplexProperty, bool implicitlyLocalizeComplexProperty)
		{
			bool flag;
			if (isComplexProperty)
			{
				flag = implicitlyLocalizeComplexProperty;
			}
			else
			{
				flag = (persistData == null || persistData.Localize);
			}
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(obj);
			for (int i = 0; i < properties.Count; i++)
			{
				try
				{
					PropertyDescriptor propertyDescriptor = properties[i];
					if (string.Equals(propertyDescriptor.Name, "Controls", StringComparison.Ordinal))
					{
						Control control = obj as Control;
						if (control != null && shouldLocalizeInnerContent)
						{
							if (!ControlLocalizer.ParseChildren(control.GetType()))
							{
								ControlCollection controls = control.Controls;
								foreach (object obj2 in controls)
								{
									Control control2 = (Control)obj2;
									IControlBuilderAccessor controlBuilderAccessor = control2;
									ControlBuilder controlBuilder = controlBuilderAccessor.ControlBuilder;
									if (controlBuilder != null)
									{
										string resourceKey2 = controlBuilder.GetResourceKey();
										string text = ControlLocalizer.LocalizeObject(serviceProvider, control2, controlBuilder.GetObjectPersistData(), resEditor, resourceWriter, resourceKey2, string.Empty, control2, string.Empty, true, false, false);
										if (!string.Equals(resourceKey2, text, StringComparison.OrdinalIgnoreCase))
										{
											controlBuilder.SetResourceKey(text);
										}
									}
								}
							}
							goto IL_7BC;
						}
					}
					PersistenceModeAttribute persistenceModeAttribute = (PersistenceModeAttribute)propertyDescriptor.Attributes[typeof(PersistenceModeAttribute)];
					string text2 = (objectModelName.Length > 0) ? (objectModelName + "." + propertyDescriptor.Name) : propertyDescriptor.Name;
					if (persistenceModeAttribute.Mode == PersistenceMode.Attribute && propertyDescriptor.SerializationVisibility == DesignerSerializationVisibility.Content)
					{
						resourceKey = ControlLocalizer.LocalizeObject(serviceProvider, propertyDescriptor.GetValue(obj), persistData, resEditor, resourceWriter, resourceKey, text2, topLevelObject, filter, true, true, flag);
					}
					else if (persistenceModeAttribute.Mode == PersistenceMode.Attribute || propertyDescriptor.PropertyType == typeof(string))
					{
						bool flag2 = false;
						bool flag3 = false;
						object obj3 = null;
						string text3 = string.Empty;
						if (persistData != null)
						{
							PropertyEntry filteredProperty = persistData.GetFilteredProperty(string.Empty, text2);
							if (filteredProperty is BoundPropertyEntry)
							{
								BoundPropertyEntry boundPropertyEntry = (BoundPropertyEntry)filteredProperty;
								if (!boundPropertyEntry.Generated)
								{
									if (string.Equals(boundPropertyEntry.ExpressionPrefix, "resources", StringComparison.OrdinalIgnoreCase))
									{
										ResourceExpressionFields resourceExpressionFields = boundPropertyEntry.ParsedExpressionData as ResourceExpressionFields;
										if (resourceExpressionFields != null && string.IsNullOrEmpty(resourceExpressionFields.ClassKey))
										{
											text3 = resourceExpressionFields.ResourceKey;
											obj3 = resEditor.EvaluateExpression(boundPropertyEntry.Expression, boundPropertyEntry.ParsedExpressionData, propertyDescriptor.PropertyType, serviceProvider);
											if (obj3 != null)
											{
												flag3 = true;
											}
											flag2 = true;
										}
									}
								}
								else
								{
									flag2 = true;
								}
							}
							else
							{
								flag2 = (flag && ControlLocalizer.IsPropertyLocalizable(propertyDescriptor));
							}
						}
						else
						{
							flag2 = (flag && ControlLocalizer.IsPropertyLocalizable(propertyDescriptor));
						}
						if (flag2)
						{
							if (!flag3)
							{
								obj3 = propertyDescriptor.GetValue(obj);
							}
							if (text3.Length == 0)
							{
								if (string.IsNullOrEmpty(resourceKey))
								{
									resourceKey = resourceWriter.CreateResourceKey(null, topLevelObject);
								}
								text3 = resourceKey + "." + text2;
								if (filter.Length != 0)
								{
									text3 = filter + ":" + text3;
								}
							}
							resourceWriter.AddResource(text3, obj3);
						}
						if (persistData != null)
						{
							ICollection propertyAllFilters = persistData.GetPropertyAllFilters(text2);
							foreach (object obj4 in propertyAllFilters)
							{
								PropertyEntry propertyEntry = (PropertyEntry)obj4;
								if (propertyEntry.Filter.Length > 0)
								{
									if (propertyEntry is SimplePropertyEntry)
									{
										if (flag && ControlLocalizer.IsPropertyLocalizable(propertyDescriptor))
										{
											if (text3.Length == 0)
											{
												if (string.IsNullOrEmpty(resourceKey))
												{
													resourceKey = resourceWriter.CreateResourceKey(null, topLevelObject);
												}
												text3 = resourceKey + "." + text2;
											}
											string name = propertyEntry.Filter + ":" + text3;
											resourceWriter.AddResource(name, ((SimplePropertyEntry)propertyEntry).Value);
										}
									}
									else if (!(propertyEntry is ComplexPropertyEntry) && propertyEntry is BoundPropertyEntry)
									{
										BoundPropertyEntry boundPropertyEntry2 = (BoundPropertyEntry)propertyEntry;
										if (!boundPropertyEntry2.Generated && string.Equals(boundPropertyEntry2.ExpressionPrefix, "resources", StringComparison.OrdinalIgnoreCase))
										{
											ResourceExpressionFields resourceExpressionFields2 = boundPropertyEntry2.ParsedExpressionData as ResourceExpressionFields;
											if (resourceExpressionFields2 != null && string.IsNullOrEmpty(resourceExpressionFields2.ClassKey))
											{
												object obj5 = resEditor.EvaluateExpression(boundPropertyEntry2.Expression, boundPropertyEntry2.ParsedExpressionData, propertyEntry.PropertyInfo.PropertyType, serviceProvider);
												if (obj5 == null)
												{
													obj5 = string.Empty;
												}
												resourceWriter.AddResource(resourceExpressionFields2.ResourceKey, obj5);
											}
										}
									}
								}
							}
						}
					}
					else if (shouldLocalizeInnerContent)
					{
						if (typeof(ICollection).IsAssignableFrom(propertyDescriptor.PropertyType))
						{
							if (persistData != null)
							{
								ICollection propertyAllFilters2 = persistData.GetPropertyAllFilters(propertyDescriptor.Name);
								foreach (object obj6 in propertyAllFilters2)
								{
									ComplexPropertyEntry complexPropertyEntry = (ComplexPropertyEntry)obj6;
									ObjectPersistData objectPersistData = complexPropertyEntry.Builder.GetObjectPersistData();
									foreach (object obj7 in objectPersistData.CollectionItems)
									{
										ComplexPropertyEntry complexPropertyEntry2 = (ComplexPropertyEntry)obj7;
										ControlBuilder builder = complexPropertyEntry2.Builder;
										object obj8 = builder.BuildObject();
										string resourceKey3 = builder.GetResourceKey();
										string text4 = ControlLocalizer.LocalizeObject(serviceProvider, obj8, builder.GetObjectPersistData(), resEditor, resourceWriter, resourceKey3, string.Empty, obj8, string.Empty, true, false, false);
										if (!string.Equals(resourceKey3, text4, StringComparison.OrdinalIgnoreCase))
										{
											builder.SetResourceKey(text4);
										}
									}
								}
							}
						}
						else if (typeof(ITemplate).IsAssignableFrom(propertyDescriptor.PropertyType))
						{
							if (persistData != null)
							{
								ICollection propertyAllFilters3 = persistData.GetPropertyAllFilters(propertyDescriptor.Name);
								foreach (object obj9 in propertyAllFilters3)
								{
									TemplatePropertyEntry templatePropertyEntry = (TemplatePropertyEntry)obj9;
									TemplateBuilder templateBuilder = (TemplateBuilder)templatePropertyEntry.Builder;
									IDesignerHost designerHost = (IDesignerHost)serviceProvider.GetService(typeof(IDesignerHost));
									Control[] array = ControlParser.ParseControls(designerHost, templateBuilder.Text);
									for (int j = 0; j < array.Length; j++)
									{
										if (!(array[j] is LiteralControl) && !(array[j] is DesignerDataBoundLiteralControl))
										{
											ControlLocalizer.LocalizeControl(array[j], serviceProvider, resourceWriter, true);
										}
									}
									StringBuilder stringBuilder = new StringBuilder();
									for (int k = 0; k < array.Length; k++)
									{
										if (array[k] is LiteralControl)
										{
											stringBuilder.Append(((LiteralControl)array[k]).Text);
										}
										else
										{
											stringBuilder.Append(ControlPersister.PersistControl(array[k], designerHost));
										}
									}
									templateBuilder.Text = stringBuilder.ToString();
								}
							}
						}
						else if (persistData != null)
						{
							object obj10 = propertyDescriptor.GetValue(obj);
							ObjectPersistData persistData2 = null;
							ComplexPropertyEntry complexPropertyEntry3 = (ComplexPropertyEntry)persistData.GetFilteredProperty(string.Empty, propertyDescriptor.Name);
							if (complexPropertyEntry3 != null)
							{
								persistData2 = complexPropertyEntry3.Builder.GetObjectPersistData();
							}
							resourceKey = ControlLocalizer.LocalizeObject(serviceProvider, obj10, persistData2, resEditor, resourceWriter, resourceKey, text2, topLevelObject, string.Empty, true, true, flag);
							ICollection propertyAllFilters4 = persistData.GetPropertyAllFilters(propertyDescriptor.Name);
							foreach (object obj11 in propertyAllFilters4)
							{
								ComplexPropertyEntry complexPropertyEntry4 = (ComplexPropertyEntry)obj11;
								if (complexPropertyEntry4.Filter.Length > 0)
								{
									ControlBuilder builder2 = complexPropertyEntry4.Builder;
									persistData2 = builder2.GetObjectPersistData();
									obj10 = builder2.BuildObject();
									resourceKey = ControlLocalizer.LocalizeObject(serviceProvider, obj10, persistData2, resEditor, resourceWriter, resourceKey, text2, topLevelObject, complexPropertyEntry4.Filter, true, true, flag);
								}
							}
						}
					}
				}
				catch (Exception ex)
				{
					if (serviceProvider != null)
					{
						IComponentDesignerDebugService componentDesignerDebugService = serviceProvider.GetService(typeof(IComponentDesignerDebugService)) as IComponentDesignerDebugService;
						if (componentDesignerDebugService != null)
						{
							componentDesignerDebugService.Fail(ex.Message);
						}
					}
				}
				IL_7BC:;
			}
			return resourceKey;
		}

		// Token: 0x040000D8 RID: 216
		private const string LocalizationResourceExpressionPrefix = "resources";

		// Token: 0x040000D9 RID: 217
		private const char filterDelimiter = ':';

		// Token: 0x040000DA RID: 218
		private const char objectDelimiter = '.';

		// Token: 0x040000DB RID: 219
		private const char OMDelimiter = '.';

		// Token: 0x020003A5 RID: 933
		private sealed class LocalizationDesignerHost : IDesignerHost, IServiceContainer, IServiceProvider
		{
			// Token: 0x060025BB RID: 9659 RVA: 0x000EBD6A File Offset: 0x000E9F6A
			internal LocalizationDesignerHost(IDesignerHost parentHost)
			{
				this._parentHost = parentHost;
			}

			// Token: 0x170007F1 RID: 2033
			// (get) Token: 0x060025BC RID: 9660 RVA: 0x000EBD79 File Offset: 0x000E9F79
			IContainer IDesignerHost.Container
			{
				get
				{
					return this._parentHost.Container;
				}
			}

			// Token: 0x170007F2 RID: 2034
			// (get) Token: 0x060025BD RID: 9661 RVA: 0x000EBD86 File Offset: 0x000E9F86
			bool IDesignerHost.InTransaction
			{
				get
				{
					return this._parentHost.InTransaction;
				}
			}

			// Token: 0x170007F3 RID: 2035
			// (get) Token: 0x060025BE RID: 9662 RVA: 0x000EBD93 File Offset: 0x000E9F93
			bool IDesignerHost.Loading
			{
				get
				{
					return this._parentHost.Loading;
				}
			}

			// Token: 0x170007F4 RID: 2036
			// (get) Token: 0x060025BF RID: 9663 RVA: 0x000EBDA0 File Offset: 0x000E9FA0
			string IDesignerHost.TransactionDescription
			{
				get
				{
					return this._parentHost.TransactionDescription;
				}
			}

			// Token: 0x170007F5 RID: 2037
			// (get) Token: 0x060025C0 RID: 9664 RVA: 0x000EBDAD File Offset: 0x000E9FAD
			IComponent IDesignerHost.RootComponent
			{
				get
				{
					return this._parentHost.RootComponent;
				}
			}

			// Token: 0x170007F6 RID: 2038
			// (get) Token: 0x060025C1 RID: 9665 RVA: 0x000EBDBA File Offset: 0x000E9FBA
			string IDesignerHost.RootComponentClassName
			{
				get
				{
					return this._parentHost.RootComponentClassName;
				}
			}

			// Token: 0x14000059 RID: 89
			// (add) Token: 0x060025C2 RID: 9666 RVA: 0x000EBDC7 File Offset: 0x000E9FC7
			// (remove) Token: 0x060025C3 RID: 9667 RVA: 0x000EBDD5 File Offset: 0x000E9FD5
			event EventHandler IDesignerHost.Activated
			{
				add
				{
					this._parentHost.Activated += value;
				}
				remove
				{
					this._parentHost.Activated -= value;
				}
			}

			// Token: 0x1400005A RID: 90
			// (add) Token: 0x060025C4 RID: 9668 RVA: 0x000EBDE3 File Offset: 0x000E9FE3
			// (remove) Token: 0x060025C5 RID: 9669 RVA: 0x000EBDF1 File Offset: 0x000E9FF1
			event EventHandler IDesignerHost.Deactivated
			{
				add
				{
					this._parentHost.Deactivated += value;
				}
				remove
				{
					this._parentHost.Deactivated -= value;
				}
			}

			// Token: 0x1400005B RID: 91
			// (add) Token: 0x060025C6 RID: 9670 RVA: 0x000EBDFF File Offset: 0x000E9FFF
			// (remove) Token: 0x060025C7 RID: 9671 RVA: 0x000EBE0D File Offset: 0x000EA00D
			event EventHandler IDesignerHost.LoadComplete
			{
				add
				{
					this._parentHost.LoadComplete += value;
				}
				remove
				{
					this._parentHost.LoadComplete -= value;
				}
			}

			// Token: 0x1400005C RID: 92
			// (add) Token: 0x060025C8 RID: 9672 RVA: 0x000EBE1B File Offset: 0x000EA01B
			// (remove) Token: 0x060025C9 RID: 9673 RVA: 0x000EBE29 File Offset: 0x000EA029
			event DesignerTransactionCloseEventHandler IDesignerHost.TransactionClosed
			{
				add
				{
					this._parentHost.TransactionClosed += value;
				}
				remove
				{
					this._parentHost.TransactionClosed -= value;
				}
			}

			// Token: 0x1400005D RID: 93
			// (add) Token: 0x060025CA RID: 9674 RVA: 0x000EBE37 File Offset: 0x000EA037
			// (remove) Token: 0x060025CB RID: 9675 RVA: 0x000EBE45 File Offset: 0x000EA045
			event DesignerTransactionCloseEventHandler IDesignerHost.TransactionClosing
			{
				add
				{
					this._parentHost.TransactionClosing += value;
				}
				remove
				{
					this._parentHost.TransactionClosing -= value;
				}
			}

			// Token: 0x1400005E RID: 94
			// (add) Token: 0x060025CC RID: 9676 RVA: 0x000EBE53 File Offset: 0x000EA053
			// (remove) Token: 0x060025CD RID: 9677 RVA: 0x000EBE61 File Offset: 0x000EA061
			event EventHandler IDesignerHost.TransactionOpened
			{
				add
				{
					this._parentHost.TransactionOpened += value;
				}
				remove
				{
					this._parentHost.TransactionOpened -= value;
				}
			}

			// Token: 0x1400005F RID: 95
			// (add) Token: 0x060025CE RID: 9678 RVA: 0x000EBE6F File Offset: 0x000EA06F
			// (remove) Token: 0x060025CF RID: 9679 RVA: 0x000EBE7D File Offset: 0x000EA07D
			event EventHandler IDesignerHost.TransactionOpening
			{
				add
				{
					this._parentHost.TransactionOpening += value;
				}
				remove
				{
					this._parentHost.TransactionOpening -= value;
				}
			}

			// Token: 0x060025D0 RID: 9680 RVA: 0x00003937 File Offset: 0x00001B37
			void IDesignerHost.Activate()
			{
			}

			// Token: 0x060025D1 RID: 9681 RVA: 0x000EBE8B File Offset: 0x000EA08B
			IComponent IDesignerHost.CreateComponent(Type componentType)
			{
				return this._parentHost.CreateComponent(componentType);
			}

			// Token: 0x060025D2 RID: 9682 RVA: 0x000EBE99 File Offset: 0x000EA099
			IComponent IDesignerHost.CreateComponent(Type componentType, string name)
			{
				return this._parentHost.CreateComponent(componentType, name);
			}

			// Token: 0x060025D3 RID: 9683 RVA: 0x000EBEA8 File Offset: 0x000EA0A8
			DesignerTransaction IDesignerHost.CreateTransaction()
			{
				return this._parentHost.CreateTransaction();
			}

			// Token: 0x060025D4 RID: 9684 RVA: 0x000EBEB5 File Offset: 0x000EA0B5
			DesignerTransaction IDesignerHost.CreateTransaction(string description)
			{
				return this._parentHost.CreateTransaction(description);
			}

			// Token: 0x060025D5 RID: 9685 RVA: 0x000EBEC3 File Offset: 0x000EA0C3
			void IDesignerHost.DestroyComponent(IComponent component)
			{
				this._parentHost.DestroyComponent(component);
			}

			// Token: 0x060025D6 RID: 9686 RVA: 0x000EBED1 File Offset: 0x000EA0D1
			Type IDesignerHost.GetType(string typeName)
			{
				return this._parentHost.GetType(typeName);
			}

			// Token: 0x060025D7 RID: 9687 RVA: 0x000EBEDF File Offset: 0x000EA0DF
			IDesigner IDesignerHost.GetDesigner(IComponent component)
			{
				return this._parentHost.GetDesigner(component);
			}

			// Token: 0x060025D8 RID: 9688 RVA: 0x000EBEED File Offset: 0x000EA0ED
			void IServiceContainer.RemoveService(Type serviceType, bool promote)
			{
				this._parentHost.RemoveService(serviceType, promote);
			}

			// Token: 0x060025D9 RID: 9689 RVA: 0x000EBEFC File Offset: 0x000EA0FC
			void IServiceContainer.RemoveService(Type serviceType)
			{
				this._parentHost.RemoveService(serviceType);
			}

			// Token: 0x060025DA RID: 9690 RVA: 0x000EBF0A File Offset: 0x000EA10A
			void IServiceContainer.AddService(Type serviceType, ServiceCreatorCallback callback, bool promote)
			{
				this._parentHost.AddService(serviceType, callback, promote);
			}

			// Token: 0x060025DB RID: 9691 RVA: 0x000EBF1A File Offset: 0x000EA11A
			void IServiceContainer.AddService(Type serviceType, ServiceCreatorCallback callback)
			{
				this._parentHost.AddService(serviceType, callback);
			}

			// Token: 0x060025DC RID: 9692 RVA: 0x000EBF29 File Offset: 0x000EA129
			void IServiceContainer.AddService(Type serviceType, object serviceInstance, bool promote)
			{
				this._parentHost.AddService(serviceType, serviceInstance, promote);
			}

			// Token: 0x060025DD RID: 9693 RVA: 0x000EBF39 File Offset: 0x000EA139
			void IServiceContainer.AddService(Type serviceType, object serviceInstance)
			{
				this._parentHost.AddService(serviceType, serviceInstance);
			}

			// Token: 0x060025DE RID: 9694 RVA: 0x000EBF48 File Offset: 0x000EA148
			object IServiceProvider.GetService(Type serviceType)
			{
				if (serviceType == typeof(IFilterResolutionService))
				{
					if (this._localizationFilterService == null)
					{
						IFilterResolutionService filterResolutionService = (IFilterResolutionService)this._parentHost.GetService(typeof(IFilterResolutionService));
						if (filterResolutionService == null)
						{
							throw new InvalidOperationException(SR.GetString("ControlLocalizer_RequiresFilterService"));
						}
						this._localizationFilterService = new ControlLocalizer.LocalizationDesignerHost.LocalizationFilterResolutionService(filterResolutionService);
					}
					return this._localizationFilterService;
				}
				return this._parentHost.GetService(serviceType);
			}

			// Token: 0x04001B84 RID: 7044
			private IDesignerHost _parentHost;

			// Token: 0x04001B85 RID: 7045
			private ControlLocalizer.LocalizationDesignerHost.LocalizationFilterResolutionService _localizationFilterService;

			// Token: 0x020005BD RID: 1469
			private sealed class LocalizationFilterResolutionService : IFilterResolutionService
			{
				// Token: 0x060033DE RID: 13278 RVA: 0x0011B778 File Offset: 0x00119978
				internal LocalizationFilterResolutionService(IFilterResolutionService realFilterService)
				{
					this._realFilterService = realFilterService;
				}

				// Token: 0x060033DF RID: 13279 RVA: 0x0011B787 File Offset: 0x00119987
				int IFilterResolutionService.CompareFilters(string filter1, string filter2)
				{
					return this._realFilterService.CompareFilters(filter1, filter2);
				}

				// Token: 0x060033E0 RID: 13280 RVA: 0x0011B796 File Offset: 0x00119996
				bool IFilterResolutionService.EvaluateFilter(string filterName)
				{
					return filterName == null || filterName.Length == 0 || string.Equals(filterName, "default", StringComparison.OrdinalIgnoreCase);
				}

				// Token: 0x040022C1 RID: 8897
				private IFilterResolutionService _realFilterService;
			}
		}
	}
}

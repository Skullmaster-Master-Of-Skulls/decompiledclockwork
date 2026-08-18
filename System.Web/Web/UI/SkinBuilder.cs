using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x02000463 RID: 1123
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class SkinBuilder : ControlBuilder
	{
		// Token: 0x06003522 RID: 13602 RVA: 0x000E5EE3 File Offset: 0x000E4EE3
		public SkinBuilder(ThemeProvider provider, Control control, ControlBuilder skinBuilder, string themePath)
		{
			this._provider = provider;
			this._control = control;
			this._skinBuilder = skinBuilder;
			this._themePath = themePath;
		}

		// Token: 0x06003523 RID: 13603 RVA: 0x000E5F08 File Offset: 0x000E4F08
		private void ApplyTemplateProperties(Control control)
		{
			object[] array = new object[1];
			ICollection filteredPropertyEntrySet = base.GetFilteredPropertyEntrySet(this._skinBuilder.TemplatePropertyEntries);
			foreach (object obj in filteredPropertyEntrySet)
			{
				TemplatePropertyEntry templatePropertyEntry = (TemplatePropertyEntry)obj;
				try
				{
					if (FastPropertyAccessor.GetProperty(control, templatePropertyEntry.Name, base.InDesigner) == null)
					{
						ControlBuilder builder = templatePropertyEntry.Builder;
						builder.SetServiceProvider(base.ServiceProvider);
						try
						{
							object obj2 = builder.BuildObject(true);
							array[0] = obj2;
						}
						finally
						{
							builder.SetServiceProvider(null);
						}
						MethodInfo setMethod = templatePropertyEntry.PropertyInfo.GetSetMethod();
						Util.InvokeMethod(setMethod, control, array);
					}
				}
				catch (Exception)
				{
				}
				catch
				{
				}
			}
		}

		// Token: 0x06003524 RID: 13604 RVA: 0x000E6000 File Offset: 0x000E5000
		private void ApplyComplexProperties(Control control)
		{
			ICollection filteredPropertyEntrySet = base.GetFilteredPropertyEntrySet(this._skinBuilder.ComplexPropertyEntries);
			foreach (object obj in filteredPropertyEntrySet)
			{
				ComplexPropertyEntry complexPropertyEntry = (ComplexPropertyEntry)obj;
				ControlBuilder builder = complexPropertyEntry.Builder;
				if (builder != null)
				{
					string name = complexPropertyEntry.Name;
					if (complexPropertyEntry.ReadOnly)
					{
						object property = FastPropertyAccessor.GetProperty(control, name, base.InDesigner);
						if (property == null)
						{
							continue;
						}
						complexPropertyEntry.Builder.SetServiceProvider(base.ServiceProvider);
						try
						{
							complexPropertyEntry.Builder.InitObject(property);
							continue;
						}
						finally
						{
							complexPropertyEntry.Builder.SetServiceProvider(null);
						}
					}
					object obj2 = complexPropertyEntry.Builder.BuildObject(true);
					object target;
					string text;
					PropertyDescriptor mappedPropertyDescriptor = PropertyMapper.GetMappedPropertyDescriptor(control, PropertyMapper.MapNameToPropertyName(name), out target, out text, base.InDesigner);
					if (mappedPropertyDescriptor != null)
					{
						string text2 = obj2 as string;
						if (obj2 != null && mappedPropertyDescriptor.Attributes[typeof(UrlPropertyAttribute)] != null && UrlPath.IsRelativeUrl(text2))
						{
							obj2 = this._themePath + text2;
						}
					}
					FastPropertyAccessor.SetProperty(target, name, obj2, base.InDesigner);
				}
			}
		}

		// Token: 0x06003525 RID: 13605 RVA: 0x000E6154 File Offset: 0x000E5154
		private void ApplySimpleProperties(Control control)
		{
			ICollection filteredPropertyEntrySet = base.GetFilteredPropertyEntrySet(this._skinBuilder.SimplePropertyEntries);
			foreach (object obj in filteredPropertyEntrySet)
			{
				SimplePropertyEntry simplePropertyEntry = (SimplePropertyEntry)obj;
				try
				{
					if (simplePropertyEntry.UseSetAttribute)
					{
						base.SetSimpleProperty(simplePropertyEntry, control);
					}
					else
					{
						string mappedName = PropertyMapper.MapNameToPropertyName(simplePropertyEntry.Name);
						object component;
						string text;
						PropertyDescriptor mappedPropertyDescriptor = PropertyMapper.GetMappedPropertyDescriptor(control, mappedName, out component, out text, base.InDesigner);
						if (mappedPropertyDescriptor != null)
						{
							DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)mappedPropertyDescriptor.Attributes[typeof(DefaultValueAttribute)];
							object value = mappedPropertyDescriptor.GetValue(component);
							if (defaultValueAttribute == null || object.Equals(defaultValueAttribute.Value, value))
							{
								object obj2 = simplePropertyEntry.Value;
								string text2 = obj2 as string;
								if (obj2 != null && mappedPropertyDescriptor.Attributes[typeof(UrlPropertyAttribute)] != null && UrlPath.IsRelativeUrl(text2))
								{
									obj2 = this._themePath + text2;
								}
								base.SetSimpleProperty(simplePropertyEntry, control);
							}
						}
					}
				}
				catch (Exception)
				{
				}
				catch
				{
				}
			}
		}

		// Token: 0x06003526 RID: 13606 RVA: 0x000E62A8 File Offset: 0x000E52A8
		private void ApplyBoundProperties(Control control)
		{
			DataBindingCollection dataBindingCollection = null;
			IAttributeAccessor attributeAccessor = null;
			ICollection filteredPropertyEntrySet = base.GetFilteredPropertyEntrySet(this._skinBuilder.BoundPropertyEntries);
			foreach (object obj in filteredPropertyEntrySet)
			{
				BoundPropertyEntry entry = (BoundPropertyEntry)obj;
				this.InitBoundProperty(control, entry, ref dataBindingCollection, ref attributeAccessor);
			}
		}

		// Token: 0x06003527 RID: 13607 RVA: 0x000E6320 File Offset: 0x000E5320
		private void InitBoundProperty(Control control, BoundPropertyEntry entry, ref DataBindingCollection dataBindings, ref IAttributeAccessor attributeAccessor)
		{
			string expressionPrefix = entry.ExpressionPrefix;
			if (expressionPrefix.Length == 0)
			{
				if (dataBindings == null && control != null)
				{
					dataBindings = ((IDataBindingsAccessor)control).DataBindings;
				}
				dataBindings.Add(new DataBinding(entry.Name, entry.Type, entry.Expression.Trim()));
				return;
			}
			throw new InvalidOperationException(SR.GetString("ControlBuilder_ExpressionsNotAllowedInThemes"));
		}

		// Token: 0x06003528 RID: 13608 RVA: 0x000E637E File Offset: 0x000E537E
		public Control ApplyTheme()
		{
			if (this._skinBuilder != null)
			{
				this.ApplySimpleProperties(this._control);
				this.ApplyComplexProperties(this._control);
				this.ApplyBoundProperties(this._control);
				this.ApplyTemplateProperties(this._control);
			}
			return this._control;
		}

		// Token: 0x0400251E RID: 9502
		private ThemeProvider _provider;

		// Token: 0x0400251F RID: 9503
		private Control _control;

		// Token: 0x04002520 RID: 9504
		private ControlBuilder _skinBuilder;

		// Token: 0x04002521 RID: 9505
		private string _themePath;

		// Token: 0x04002522 RID: 9506
		internal static readonly object[] EmptyParams = new object[0];
	}
}

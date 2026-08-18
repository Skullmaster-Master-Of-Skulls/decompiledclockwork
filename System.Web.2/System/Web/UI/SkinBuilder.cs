using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x020002FD RID: 765
	public sealed class SkinBuilder : ControlBuilder
	{
		// Token: 0x06002354 RID: 9044 RVA: 0x000731B8 File Offset: 0x000713B8
		public SkinBuilder(ThemeProvider provider, Control control, ControlBuilder skinBuilder, string themePath)
		{
			this._provider = provider;
			this._control = control;
			this._skinBuilder = skinBuilder;
			this._themePath = themePath;
		}

		// Token: 0x06002355 RID: 9045 RVA: 0x000731E0 File Offset: 0x000713E0
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
				catch (Exception ex)
				{
				}
				catch
				{
				}
			}
		}

		// Token: 0x06002356 RID: 9046 RVA: 0x000732D4 File Offset: 0x000714D4
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

		// Token: 0x06002357 RID: 9047 RVA: 0x00073428 File Offset: 0x00071628
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
				catch (Exception ex)
				{
				}
				catch
				{
				}
			}
		}

		// Token: 0x06002358 RID: 9048 RVA: 0x00073578 File Offset: 0x00071778
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

		// Token: 0x06002359 RID: 9049 RVA: 0x000735EC File Offset: 0x000717EC
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

		// Token: 0x0600235A RID: 9050 RVA: 0x0007364A File Offset: 0x0007184A
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

		// Token: 0x04001CB4 RID: 7348
		private ThemeProvider _provider;

		// Token: 0x04001CB5 RID: 7349
		private Control _control;

		// Token: 0x04001CB6 RID: 7350
		private ControlBuilder _skinBuilder;

		// Token: 0x04001CB7 RID: 7351
		private string _themePath;

		// Token: 0x04001CB8 RID: 7352
		internal static readonly object[] EmptyParams = new object[0];
	}
}

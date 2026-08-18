using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x0200024B RID: 587
	public sealed class BindableTemplateBuilder : TemplateBuilder, IBindableTemplate, ITemplate
	{
		// Token: 0x06001B12 RID: 6930 RVA: 0x00055030 File Offset: 0x00053230
		private IOrderedDictionary ExtractTemplateValuesMethod(Control container)
		{
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			if (this != null)
			{
				this.ExtractTemplateValuesRecursive(this.SubBuilders, orderedDictionary, container);
			}
			return orderedDictionary;
		}

		// Token: 0x06001B13 RID: 6931 RVA: 0x00055058 File Offset: 0x00053258
		private void ExtractTemplateValuesRecursive(ArrayList subBuilders, OrderedDictionary table, Control container)
		{
			foreach (object obj in subBuilders)
			{
				ControlBuilder controlBuilder = obj as ControlBuilder;
				if (controlBuilder != null)
				{
					ICollection collection;
					if (!controlBuilder.HasFilteredBoundEntries)
					{
						collection = controlBuilder.BoundPropertyEntries;
					}
					else
					{
						ServiceContainer serviceContainer = new ServiceContainer();
						serviceContainer.AddService(typeof(IFilterResolutionService), controlBuilder.TemplateControl);
						try
						{
							controlBuilder.SetServiceProvider(serviceContainer);
							collection = controlBuilder.GetFilteredPropertyEntrySet(controlBuilder.BoundPropertyEntries);
						}
						finally
						{
							controlBuilder.SetServiceProvider(null);
						}
					}
					string strA = null;
					Control control = null;
					foreach (object obj2 in collection)
					{
						BoundPropertyEntry boundPropertyEntry = (BoundPropertyEntry)obj2;
						if (boundPropertyEntry.TwoWayBound)
						{
							bool flag = string.Compare(strA, boundPropertyEntry.ControlID, StringComparison.Ordinal) != 0;
							strA = boundPropertyEntry.ControlID;
							if (flag)
							{
								control = container.FindControl(boundPropertyEntry.ControlID);
								if (control == null || !boundPropertyEntry.ControlType.IsInstanceOfType(control))
								{
									continue;
								}
							}
							string propName;
							object target = PropertyMapper.LocatePropertyObject(control, boundPropertyEntry.Name, out propName, base.InDesigner);
							table[boundPropertyEntry.FieldName] = FastPropertyAccessor.GetProperty(target, propName, base.InDesigner);
						}
					}
					this.ExtractTemplateValuesRecursive(controlBuilder.SubBuilders, table, container);
				}
			}
		}

		// Token: 0x06001B14 RID: 6932 RVA: 0x0005521C File Offset: 0x0005341C
		public IOrderedDictionary ExtractValues(Control container)
		{
			if (this._extractTemplateValuesMethod != null && !base.InDesigner)
			{
				return this._extractTemplateValuesMethod(container);
			}
			return new OrderedDictionary();
		}

		// Token: 0x06001B15 RID: 6933 RVA: 0x00055240 File Offset: 0x00053440
		public override void OnAppendToParentBuilder(ControlBuilder parentBuilder)
		{
			base.OnAppendToParentBuilder(parentBuilder);
			if (base.HasTwoWayBoundProperties)
			{
				this._extractTemplateValuesMethod = new ExtractTemplateValuesMethod(this.ExtractTemplateValuesMethod);
			}
		}

		// Token: 0x04001880 RID: 6272
		private ExtractTemplateValuesMethod _extractTemplateValuesMethod;
	}
}

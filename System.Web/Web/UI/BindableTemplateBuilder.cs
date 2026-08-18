using System;
using System.Collections;
using System.Collections.Specialized;
using System.ComponentModel.Design;
using System.Security.Permissions;
using System.Web.Util;

namespace System.Web.UI
{
	// Token: 0x0200039A RID: 922
	[AspNetHostingPermission(SecurityAction.LinkDemand, Level = AspNetHostingPermissionLevel.Minimal)]
	public sealed class BindableTemplateBuilder : TemplateBuilder, IBindableTemplate, ITemplate
	{
		// Token: 0x06002D09 RID: 11529 RVA: 0x000CA518 File Offset: 0x000C9518
		private IOrderedDictionary ExtractTemplateValuesMethod(Control container)
		{
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			if (this != null)
			{
				this.ExtractTemplateValuesRecursive(this.SubBuilders, orderedDictionary, container);
			}
			return orderedDictionary;
		}

		// Token: 0x06002D0A RID: 11530 RVA: 0x000CA540 File Offset: 0x000C9540
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

		// Token: 0x06002D0B RID: 11531 RVA: 0x000CA704 File Offset: 0x000C9704
		public IOrderedDictionary ExtractValues(Control container)
		{
			if (this._extractTemplateValuesMethod != null && !base.InDesigner)
			{
				return this._extractTemplateValuesMethod(container);
			}
			return new OrderedDictionary();
		}

		// Token: 0x06002D0C RID: 11532 RVA: 0x000CA728 File Offset: 0x000C9728
		public override void OnAppendToParentBuilder(ControlBuilder parentBuilder)
		{
			base.OnAppendToParentBuilder(parentBuilder);
			if (base.HasTwoWayBoundProperties)
			{
				this._extractTemplateValuesMethod = new ExtractTemplateValuesMethod(this.ExtractTemplateValuesMethod);
			}
		}

		// Token: 0x040020D5 RID: 8405
		private ExtractTemplateValuesMethod _extractTemplateValuesMethod;
	}
}

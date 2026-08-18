using System;
using System.Collections;
using System.ComponentModel;
using System.Web.UI;

namespace Telerik.Web.UI
{
	// Token: 0x020011B0 RID: 4528
	internal class NestedState
	{
		// Token: 0x0600B9E8 RID: 47592 RVA: 0x0029412C File Offset: 0x0029232C
		internal static object[] SaveViewState(object component)
		{
			PropertyDescriptorCollection nestedStateManagerProperties = NestedState.GetNestedStateManagerProperties(component);
			object[] array = new object[nestedStateManagerProperties.Count];
			for (int i = 0; i < nestedStateManagerProperties.Count; i++)
			{
				PropertyDescriptor propertyDescriptor = nestedStateManagerProperties[i];
				IStateManager stateManager = (IStateManager)propertyDescriptor.GetValue(component);
				if (stateManager != null)
				{
					array[i] = stateManager.SaveViewState();
				}
			}
			return array;
		}

		// Token: 0x0600B9E9 RID: 47593 RVA: 0x00294184 File Offset: 0x00292384
		internal static void LoadViewState(object component, object[] nestedViewState)
		{
			PropertyDescriptorCollection nestedStateManagerProperties = NestedState.GetNestedStateManagerProperties(component);
			for (int i = 0; i < nestedStateManagerProperties.Count; i++)
			{
				PropertyDescriptor propertyDescriptor = nestedStateManagerProperties[i];
				IStateManager stateManager = (IStateManager)propertyDescriptor.GetValue(component);
				if (stateManager != null)
				{
					object state = nestedViewState[i];
					stateManager.LoadViewState(state);
				}
			}
		}

		// Token: 0x0600B9EA RID: 47594 RVA: 0x002941D0 File Offset: 0x002923D0
		internal static void TrackViewState(object component)
		{
			PropertyDescriptorCollection nestedStateManagerProperties = NestedState.GetNestedStateManagerProperties(component);
			for (int i = 0; i < nestedStateManagerProperties.Count; i++)
			{
				PropertyDescriptor propertyDescriptor = nestedStateManagerProperties[i];
				IStateManager stateManager = (IStateManager)propertyDescriptor.GetValue(component);
				if (stateManager != null)
				{
					stateManager.TrackViewState();
				}
			}
		}

		// Token: 0x0600B9EB RID: 47595 RVA: 0x00294214 File Offset: 0x00292414
		private static PropertyDescriptorCollection GetNestedStateManagerProperties(object component)
		{
			ArrayList arrayList = new ArrayList();
			PropertyDescriptorCollection properties = TypeDescriptor.GetProperties(component);
			foreach (object obj in properties)
			{
				PropertyDescriptor propertyDescriptor = (PropertyDescriptor)obj;
				foreach (object obj2 in propertyDescriptor.Attributes)
				{
					Attribute attribute = (Attribute)obj2;
					if (attribute is NestedStateManagerAttribute)
					{
						arrayList.Add(propertyDescriptor);
					}
				}
			}
			PropertyDescriptor[] properties2 = (PropertyDescriptor[])arrayList.ToArray(typeof(PropertyDescriptor));
			return new PropertyDescriptorCollection(properties2);
		}
	}
}

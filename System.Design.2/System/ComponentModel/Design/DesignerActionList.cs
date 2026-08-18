using System;
using System.Collections.Generic;
using System.Reflection;

namespace System.ComponentModel.Design
{
	// Token: 0x0200019D RID: 413
	public class DesignerActionList
	{
		// Token: 0x06000F32 RID: 3890 RVA: 0x000576CD File Offset: 0x000558CD
		public DesignerActionList(IComponent component)
		{
			this._component = component;
		}

		// Token: 0x17000399 RID: 921
		// (get) Token: 0x06000F33 RID: 3891 RVA: 0x000576DC File Offset: 0x000558DC
		// (set) Token: 0x06000F34 RID: 3892 RVA: 0x000576E4 File Offset: 0x000558E4
		public virtual bool AutoShow
		{
			get
			{
				return this._autoShow;
			}
			set
			{
				if (this._autoShow != value)
				{
					this._autoShow = value;
				}
			}
		}

		// Token: 0x1700039A RID: 922
		// (get) Token: 0x06000F35 RID: 3893 RVA: 0x000576F6 File Offset: 0x000558F6
		public IComponent Component
		{
			get
			{
				return this._component;
			}
		}

		// Token: 0x06000F36 RID: 3894 RVA: 0x000576FE File Offset: 0x000558FE
		public object GetService(Type serviceType)
		{
			if (this._component != null && this._component.Site != null)
			{
				return this._component.Site.GetService(serviceType);
			}
			return null;
		}

		// Token: 0x06000F37 RID: 3895 RVA: 0x00057728 File Offset: 0x00055928
		private object GetCustomAttribute(MemberInfo info, Type attributeType)
		{
			object[] customAttributes = info.GetCustomAttributes(attributeType, true);
			if (customAttributes.Length != 0)
			{
				return customAttributes[0];
			}
			return null;
		}

		// Token: 0x06000F38 RID: 3896 RVA: 0x00057748 File Offset: 0x00055948
		private void GetMemberDisplayProperties(MemberInfo info, out string displayName, out string description, out string category)
		{
			string text;
			category = (text = "");
			description = (text = text);
			displayName = text;
			DescriptionAttribute descriptionAttribute = this.GetCustomAttribute(info, typeof(DescriptionAttribute)) as DescriptionAttribute;
			if (descriptionAttribute != null)
			{
				description = descriptionAttribute.Description;
			}
			DisplayNameAttribute displayNameAttribute = this.GetCustomAttribute(info, typeof(DisplayNameAttribute)) as DisplayNameAttribute;
			if (displayNameAttribute != null)
			{
				displayName = displayNameAttribute.DisplayName;
			}
			CategoryAttribute categoryAttribute = this.GetCustomAttribute(info, typeof(CategoryAttribute)) as CategoryAttribute;
			if (displayNameAttribute != null)
			{
				category = categoryAttribute.Category;
			}
			if (string.IsNullOrEmpty(displayName))
			{
				displayName = info.Name;
			}
		}

		// Token: 0x06000F39 RID: 3897 RVA: 0x000577E0 File Offset: 0x000559E0
		public virtual DesignerActionItemCollection GetSortedActionItems()
		{
			SortedList<string, DesignerActionItem> sortedList = new SortedList<string, DesignerActionItem>();
			IList<MethodInfo> list = Array.AsReadOnly<MethodInfo>(typeof(DesignerActionList).GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod));
			IList<PropertyInfo> list2 = Array.AsReadOnly<PropertyInfo>(typeof(DesignerActionList).GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod));
			MethodInfo[] methods = base.GetType().GetMethods(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod);
			foreach (MethodInfo methodInfo in methods)
			{
				if (!list.Contains(methodInfo) && methodInfo.GetParameters().Length == 0 && !methodInfo.IsSpecialName)
				{
					string text;
					string description;
					string category;
					this.GetMemberDisplayProperties(methodInfo, out text, out description, out category);
					sortedList.Add(methodInfo.Name, new DesignerActionMethodItem(this, methodInfo.Name, text, category, description));
				}
			}
			PropertyInfo[] properties = base.GetType().GetProperties(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.InvokeMethod);
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (!list2.Contains(propertyInfo))
				{
					string text;
					string description;
					string category;
					this.GetMemberDisplayProperties(propertyInfo, out text, out description, out category);
					sortedList.Add(text, new DesignerActionPropertyItem(propertyInfo.Name, text, category, description));
				}
			}
			DesignerActionItemCollection designerActionItemCollection = new DesignerActionItemCollection();
			foreach (DesignerActionItem value in sortedList.Values)
			{
				designerActionItemCollection.Add(value);
			}
			return designerActionItemCollection;
		}

		// Token: 0x040008E9 RID: 2281
		private bool _autoShow;

		// Token: 0x040008EA RID: 2282
		private IComponent _component;
	}
}

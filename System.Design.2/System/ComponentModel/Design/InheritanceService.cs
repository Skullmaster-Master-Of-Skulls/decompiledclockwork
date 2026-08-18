using System;
using System.Collections;
using System.ComponentModel.Design.Serialization;
using System.Diagnostics;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Windows.Forms.Design;

namespace System.ComponentModel.Design
{
	// Token: 0x020001B4 RID: 436
	public class InheritanceService : IInheritanceService, IDisposable
	{
		// Token: 0x06000FD4 RID: 4052 RVA: 0x00059D8C File Offset: 0x00057F8C
		public InheritanceService()
		{
			this.inheritedComponents = new Hashtable();
		}

		// Token: 0x06000FD5 RID: 4053 RVA: 0x00059D9F File Offset: 0x00057F9F
		public void Dispose()
		{
			this.Dispose(true);
		}

		// Token: 0x06000FD6 RID: 4054 RVA: 0x00059DA8 File Offset: 0x00057FA8
		protected virtual void Dispose(bool disposing)
		{
			if (disposing && this.inheritedComponents != null)
			{
				this.inheritedComponents.Clear();
				this.inheritedComponents = null;
			}
		}

		// Token: 0x06000FD7 RID: 4055 RVA: 0x00059DC7 File Offset: 0x00057FC7
		public void AddInheritedComponents(IComponent component, IContainer container)
		{
			this.AddInheritedComponents(component.GetType(), component, container);
		}

		// Token: 0x06000FD8 RID: 4056 RVA: 0x00059DD8 File Offset: 0x00057FD8
		protected virtual void AddInheritedComponents(Type type, IComponent component, IContainer container)
		{
			if (type == null || !typeof(IComponent).IsAssignableFrom(type))
			{
				return;
			}
			ISite site = component.Site;
			IComponentChangeService componentChangeService = null;
			INameCreationService nameCreationService = null;
			if (site != null)
			{
				nameCreationService = (INameCreationService)site.GetService(typeof(INameCreationService));
				componentChangeService = (IComponentChangeService)site.GetService(typeof(IComponentChangeService));
				if (componentChangeService != null)
				{
					componentChangeService.ComponentAdding += this.OnComponentAdding;
				}
			}
			try
			{
				while (type != typeof(object))
				{
					Type reflectionType = TypeDescriptor.GetReflectionType(type);
					foreach (FieldInfo fieldInfo in reflectionType.GetFields(BindingFlags.DeclaredOnly | BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic))
					{
						string name = fieldInfo.Name;
						Type reflectionTypeFromTypeHelper = InheritanceService.GetReflectionTypeFromTypeHelper(fieldInfo.FieldType);
						if (InheritanceService.GetReflectionTypeFromTypeHelper(typeof(IComponent)).IsAssignableFrom(reflectionTypeFromTypeHelper))
						{
							object value = fieldInfo.GetValue(component);
							if (value != null)
							{
								MemberInfo memberInfo = fieldInfo;
								object[] customAttributes = fieldInfo.GetCustomAttributes(typeof(AccessedThroughPropertyAttribute), false);
								if (customAttributes != null && customAttributes.Length != 0)
								{
									AccessedThroughPropertyAttribute accessedThroughPropertyAttribute = (AccessedThroughPropertyAttribute)customAttributes[0];
									PropertyInfo property = reflectionType.GetProperty(accessedThroughPropertyAttribute.PropertyName, BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic);
									if (property != null && property.PropertyType == fieldInfo.FieldType)
									{
										if (!property.CanRead)
										{
											goto IL_235;
										}
										memberInfo = property.GetGetMethod(true);
										name = accessedThroughPropertyAttribute.PropertyName;
									}
								}
								bool flag = this.IgnoreInheritedMember(memberInfo, component);
								bool flag2 = false;
								if (flag)
								{
									flag2 = true;
								}
								else if (memberInfo is FieldInfo)
								{
									FieldInfo fieldInfo2 = (FieldInfo)memberInfo;
									flag2 = (fieldInfo2.IsPrivate | fieldInfo2.IsAssembly);
								}
								else if (memberInfo is MethodInfo)
								{
									MethodInfo methodInfo = (MethodInfo)memberInfo;
									flag2 = (methodInfo.IsPrivate | methodInfo.IsAssembly);
								}
								InheritanceAttribute value2;
								if (flag2)
								{
									value2 = InheritanceAttribute.InheritedReadOnly;
								}
								else
								{
									value2 = InheritanceAttribute.Inherited;
								}
								bool flag3 = this.inheritedComponents[value] == null;
								this.inheritedComponents[value] = value2;
								if (!flag && flag3)
								{
									try
									{
										this.addingComponent = (IComponent)value;
										this.addingAttribute = value2;
										if (nameCreationService == null || nameCreationService.IsValidName(name))
										{
											try
											{
												container.Add((IComponent)value, name);
											}
											catch
											{
											}
										}
									}
									finally
									{
										this.addingComponent = null;
										this.addingAttribute = null;
									}
								}
							}
						}
						IL_235:;
					}
					type = type.BaseType;
				}
			}
			finally
			{
				if (componentChangeService != null)
				{
					componentChangeService.ComponentAdding -= this.OnComponentAdding;
				}
			}
		}

		// Token: 0x06000FD9 RID: 4057 RVA: 0x0005A0AC File Offset: 0x000582AC
		protected virtual bool IgnoreInheritedMember(MemberInfo member, IComponent component)
		{
			if (member is FieldInfo)
			{
				FieldInfo fieldInfo = (FieldInfo)member;
				return fieldInfo.IsPrivate || fieldInfo.IsAssembly;
			}
			if (member is MethodInfo)
			{
				MethodInfo methodInfo = (MethodInfo)member;
				return methodInfo.IsPrivate || methodInfo.IsAssembly;
			}
			return true;
		}

		// Token: 0x06000FDA RID: 4058 RVA: 0x0005A0FC File Offset: 0x000582FC
		public InheritanceAttribute GetInheritanceAttribute(IComponent component)
		{
			InheritanceAttribute inheritanceAttribute = (InheritanceAttribute)this.inheritedComponents[component];
			if (inheritanceAttribute == null)
			{
				inheritanceAttribute = InheritanceAttribute.Default;
			}
			return inheritanceAttribute;
		}

		// Token: 0x06000FDB RID: 4059 RVA: 0x0005A128 File Offset: 0x00058328
		private void OnComponentAdding(object sender, ComponentEventArgs ce)
		{
			if (this.addingComponent != null && this.addingComponent != ce.Component)
			{
				this.inheritedComponents[ce.Component] = InheritanceAttribute.InheritedReadOnly;
				INestedContainer nestedContainer = sender as INestedContainer;
				if (nestedContainer != null && nestedContainer.Owner == this.addingComponent)
				{
					this.inheritedComponents[ce.Component] = this.addingAttribute;
				}
			}
		}

		// Token: 0x06000FDC RID: 4060 RVA: 0x0005A190 File Offset: 0x00058390
		private static Type GetReflectionTypeFromTypeHelper(Type type)
		{
			if (type != null)
			{
				TypeDescriptionProvider targetFrameworkProviderForType = InheritanceService.GetTargetFrameworkProviderForType(type);
				if (targetFrameworkProviderForType != null && targetFrameworkProviderForType.IsSupportedType(type))
				{
					return targetFrameworkProviderForType.GetReflectionType(type);
				}
			}
			return type;
		}

		// Token: 0x06000FDD RID: 4061 RVA: 0x0005A1C4 File Offset: 0x000583C4
		private static TypeDescriptionProvider GetTargetFrameworkProviderForType(Type type)
		{
			IDesignerSerializationManager manager = DocumentDesigner.manager;
			if (manager != null)
			{
				TypeDescriptionProviderService typeDescriptionProviderService = manager.GetService(typeof(TypeDescriptionProviderService)) as TypeDescriptionProviderService;
				if (typeDescriptionProviderService != null)
				{
					return typeDescriptionProviderService.GetProvider(type);
				}
			}
			return null;
		}

		// Token: 0x0400093A RID: 2362
		private static TraceSwitch InheritanceServiceSwitch = new TraceSwitch("InheritanceService", "InheritanceService : Debug inheritance scan.");

		// Token: 0x0400093B RID: 2363
		private Hashtable inheritedComponents;

		// Token: 0x0400093C RID: 2364
		private IComponent addingComponent;

		// Token: 0x0400093D RID: 2365
		private InheritanceAttribute addingAttribute;
	}
}

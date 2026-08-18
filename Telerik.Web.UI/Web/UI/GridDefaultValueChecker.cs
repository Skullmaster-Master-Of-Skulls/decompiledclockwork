using System;
using System.Collections;
using System.ComponentModel;
using System.Reflection;

namespace Telerik.Web.UI
{
	// Token: 0x020010CB RID: 4299
	internal class GridDefaultValueChecker
	{
		// Token: 0x0600AF78 RID: 44920 RVA: 0x0026087C File Offset: 0x0025EA7C
		internal GridDefaultValueChecker(object ToCheck)
		{
			this._objectToCheck = ToCheck;
		}

		// Token: 0x170038A6 RID: 14502
		// (get) Token: 0x0600AF79 RID: 44921 RVA: 0x00260896 File Offset: 0x0025EA96
		internal bool IsDefault
		{
			get
			{
				return this._objectToCheck != null && this.CheckIsDefault(this._objectToCheck);
			}
		}

		// Token: 0x0600AF7A RID: 44922 RVA: 0x002608B0 File Offset: 0x0025EAB0
		public bool CheckIsDefault(object ToCheck)
		{
			if (this.VisitedObjects.Contains(ToCheck))
			{
				return true;
			}
			this.VisitedObjects.Add(ToCheck);
			PropertyInfo[] properties = ToCheck.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
			if (properties.Length == 0)
			{
				return true;
			}
			foreach (PropertyInfo propertyInfo in properties)
			{
				if (propertyInfo.CanRead)
				{
					ParameterInfo[] indexParameters = propertyInfo.GetIndexParameters();
					if (indexParameters.Length == 0)
					{
						object[] customAttributes = propertyInfo.GetCustomAttributes(typeof(DesignerSerializationVisibilityAttribute), true);
						bool flag = true;
						if (customAttributes.Length > 0)
						{
							DesignerSerializationVisibilityAttribute designerSerializationVisibilityAttribute = (DesignerSerializationVisibilityAttribute)customAttributes[customAttributes.Length - 1];
							flag = (designerSerializationVisibilityAttribute.Visibility != DesignerSerializationVisibility.Hidden);
						}
						if (flag)
						{
							object value = propertyInfo.GetValue(ToCheck, new object[0]);
							if (value != null)
							{
								Type type = value.GetType();
								bool flag2 = type.IsValueType || type == typeof(string);
								bool result;
								if (flag2)
								{
									customAttributes = propertyInfo.GetCustomAttributes(typeof(DefaultValueAttribute), true);
									if (customAttributes.Length <= 0)
									{
										goto IL_13F;
									}
									DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)customAttributes[customAttributes.Length - 1];
									if (defaultValueAttribute != null)
									{
										if (value.Equals(defaultValueAttribute.Value))
										{
											goto IL_13F;
										}
										result = false;
									}
									else
									{
										if (string.IsNullOrEmpty(value.ToString()))
										{
											goto IL_13F;
										}
										result = false;
									}
								}
								else
								{
									if (this.CheckIsDefault(value))
									{
										goto IL_13F;
									}
									result = false;
								}
								return result;
							}
						}
					}
				}
				IL_13F:;
			}
			return true;
		}

		// Token: 0x0600AF7B RID: 44923 RVA: 0x00260A14 File Offset: 0x0025EC14
		public void CopyProperties(object source, object target)
		{
			if (this.VisitedObjects.Contains(source))
			{
				return;
			}
			this.VisitedObjects.Add(source);
			PropertyInfo[] properties = source.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
			if (properties.Length == 0)
			{
				return;
			}
			PropertyInfo[] properties2 = target.GetType().GetProperties(BindingFlags.Instance | BindingFlags.Public);
			if (properties2.Length == 0)
			{
				return;
			}
			for (int i = 0; i < properties.Length; i++)
			{
				PropertyInfo propertyInfo = properties[i];
				PropertyInfo propertyInfo2 = properties2[i];
				if ((propertyInfo.CanRead || propertyInfo2.CanWrite) && !typeof(Type).IsAssignableFrom(propertyInfo.PropertyType))
				{
					ParameterInfo[] indexParameters = propertyInfo.GetIndexParameters();
					if (indexParameters.Length == 0)
					{
						object[] customAttributes = propertyInfo.GetCustomAttributes(typeof(DesignerSerializationVisibilityAttribute), true);
						bool flag = true;
						if (customAttributes.Length > 0)
						{
							DesignerSerializationVisibilityAttribute designerSerializationVisibilityAttribute = (DesignerSerializationVisibilityAttribute)customAttributes[customAttributes.Length - 1];
							flag = (designerSerializationVisibilityAttribute.Visibility != DesignerSerializationVisibility.Hidden);
						}
						if (flag)
						{
							object value = propertyInfo.GetValue(source, new object[0]);
							object value2 = propertyInfo2.GetValue(target, new object[0]);
							if (value != null)
							{
								Type type = value.GetType();
								bool flag2 = type.IsValueType || type == typeof(string);
								if (flag2)
								{
									customAttributes = propertyInfo.GetCustomAttributes(typeof(DefaultValueAttribute), true);
									if (customAttributes.Length > 0)
									{
										DefaultValueAttribute defaultValueAttribute = (DefaultValueAttribute)customAttributes[customAttributes.Length - 1];
										if (defaultValueAttribute != null)
										{
											if (!value.Equals(defaultValueAttribute.Value))
											{
												propertyInfo2.SetValue(target, value, new object[0]);
											}
										}
										else if (!string.IsNullOrEmpty(value.ToString()))
										{
											propertyInfo2.SetValue(target, value, new object[0]);
										}
									}
								}
								else
								{
									this.CopyProperties(value, value2);
								}
							}
						}
					}
				}
			}
		}

		// Token: 0x04002E40 RID: 11840
		private object _objectToCheck;

		// Token: 0x04002E41 RID: 11841
		private ArrayList VisitedObjects = new ArrayList();
	}
}

using System;
using System.Collections;
using System.Collections.Specialized;
using System.Reflection;

namespace System.Web.UI.WebControls.WebParts
{
	// Token: 0x02000556 RID: 1366
	internal sealed class PersonalizableTypeEntry
	{
		// Token: 0x06004565 RID: 17765 RVA: 0x000E4EBC File Offset: 0x000E30BC
		public PersonalizableTypeEntry(Type type)
		{
			this._type = type;
			this.InitializePersonalizableProperties();
		}

		// Token: 0x1700147C RID: 5244
		// (get) Token: 0x06004566 RID: 17766 RVA: 0x000E4ED1 File Offset: 0x000E30D1
		public IDictionary PropertyEntries
		{
			get
			{
				return this._propertyEntries;
			}
		}

		// Token: 0x1700147D RID: 5245
		// (get) Token: 0x06004567 RID: 17767 RVA: 0x000E4EDC File Offset: 0x000E30DC
		public ICollection PropertyInfos
		{
			get
			{
				if (this._propertyInfos == null)
				{
					PropertyInfo[] array = new PropertyInfo[this._propertyEntries.Count];
					int num = 0;
					foreach (object obj in this._propertyEntries.Values)
					{
						PersonalizablePropertyEntry personalizablePropertyEntry = (PersonalizablePropertyEntry)obj;
						array[num] = personalizablePropertyEntry.PropertyInfo;
						num++;
					}
					this._propertyInfos = array;
				}
				return this._propertyInfos;
			}
		}

		// Token: 0x06004568 RID: 17768 RVA: 0x000E4F6C File Offset: 0x000E316C
		private void InitializePersonalizableProperties()
		{
			this._propertyEntries = new HybridDictionary(false);
			BindingFlags bindingAttr = BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic;
			PropertyInfo[] properties = this._type.GetProperties(bindingAttr);
			Array.Sort(properties, new PersonalizableTypeEntry.DeclaringTypeComparer());
			if (properties != null && properties.Length != 0)
			{
				foreach (PropertyInfo propertyInfo in properties)
				{
					string name = propertyInfo.Name;
					PersonalizableAttribute personalizableAttribute = Attribute.GetCustomAttribute(propertyInfo, PersonalizableAttribute.PersonalizableAttributeType, true) as PersonalizableAttribute;
					if (personalizableAttribute == null || !personalizableAttribute.IsPersonalizable)
					{
						this._propertyEntries.Remove(name);
					}
					else
					{
						ParameterInfo[] indexParameters = propertyInfo.GetIndexParameters();
						if ((indexParameters != null && indexParameters.Length != 0) || propertyInfo.GetGetMethod() == null || propertyInfo.GetSetMethod() == null)
						{
							throw new HttpException(SR.GetString("PersonalizableTypeEntry_InvalidProperty", new object[]
							{
								name,
								this._type.FullName
							}));
						}
						this._propertyEntries[name] = new PersonalizablePropertyEntry(propertyInfo, personalizableAttribute);
					}
				}
			}
		}

		// Token: 0x04002669 RID: 9833
		private Type _type;

		// Token: 0x0400266A RID: 9834
		private IDictionary _propertyEntries;

		// Token: 0x0400266B RID: 9835
		private PropertyInfo[] _propertyInfos;

		// Token: 0x020009ED RID: 2541
		private sealed class DeclaringTypeComparer : IComparer
		{
			// Token: 0x06006D19 RID: 27929 RVA: 0x001868B0 File Offset: 0x00184AB0
			public int Compare(object x, object y)
			{
				Type declaringType = ((PropertyInfo)x).DeclaringType;
				Type declaringType2 = ((PropertyInfo)y).DeclaringType;
				if (declaringType == declaringType2)
				{
					return 0;
				}
				if (declaringType.IsSubclassOf(declaringType2))
				{
					return 1;
				}
				return -1;
			}
		}
	}
}

using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.ModelConfiguration.Utilities
{
	// Token: 0x02000829 RID: 2089
	internal class AttributeProvider
	{
		// Token: 0x06005DBC RID: 23996 RVA: 0x00195580 File Offset: 0x00193780
		public virtual IEnumerable<Attribute> GetAttributes(MemberInfo memberInfo)
		{
			Type type = memberInfo as Type;
			if (type != null)
			{
				return this.GetAttributes(type);
			}
			return this.GetAttributes((PropertyInfo)memberInfo);
		}

		// Token: 0x06005DBD RID: 23997 RVA: 0x001955E4 File Offset: 0x001937E4
		public virtual IEnumerable<Attribute> GetAttributes(Type type)
		{
			List<Attribute> attrs = new List<Attribute>(AttributeProvider.GetTypeDescriptor(type).GetAttributes().Cast<Attribute>());
			foreach (Attribute item in from a in type.GetCustomAttributes(true)
			where a.GetType().FullName.Equals("System.Data.Services.Common.EntityPropertyMappingAttribute", StringComparison.Ordinal) && !attrs.Contains(a)
			select a)
			{
				attrs.Add(item);
			}
			return attrs;
		}

		// Token: 0x06005DBE RID: 23998 RVA: 0x001956E1 File Offset: 0x001938E1
		public virtual IEnumerable<Attribute> GetAttributes(PropertyInfo propertyInfo)
		{
			return this._discoveredAttributes.GetOrAdd(propertyInfo, delegate(PropertyInfo pi)
			{
				ICustomTypeDescriptor typeDescriptor = AttributeProvider.GetTypeDescriptor(pi.DeclaringType);
				PropertyDescriptorCollection properties = typeDescriptor.GetProperties();
				PropertyDescriptor propertyDescriptor = properties[pi.Name];
				IEnumerable<Attribute> enumerable = (propertyDescriptor != null) ? propertyDescriptor.Attributes.Cast<Attribute>() : pi.GetCustomAttributes(true);
				ICollection<Attribute> collection = (ICollection<Attribute>)this.GetAttributes(pi.PropertyType);
				if (collection.Count > 0)
				{
					enumerable = enumerable.Except(collection);
				}
				return enumerable.ToList<Attribute>();
			});
		}

		// Token: 0x06005DBF RID: 23999 RVA: 0x001956FB File Offset: 0x001938FB
		private static ICustomTypeDescriptor GetTypeDescriptor(Type type)
		{
			return new AssociatedMetadataTypeTypeDescriptionProvider(type).GetTypeDescriptor(type);
		}

		// Token: 0x06005DC0 RID: 24000 RVA: 0x00195709 File Offset: 0x00193909
		public virtual void ClearCache()
		{
			this._discoveredAttributes.Clear();
		}

		// Token: 0x0400250A RID: 9482
		private readonly ConcurrentDictionary<PropertyInfo, IEnumerable<Attribute>> _discoveredAttributes = new ConcurrentDictionary<PropertyInfo, IEnumerable<Attribute>>();
	}
}

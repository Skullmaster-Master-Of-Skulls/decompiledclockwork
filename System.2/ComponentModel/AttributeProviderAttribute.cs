using System;

namespace System.ComponentModel
{
	// Token: 0x02000516 RID: 1302
	[AttributeUsage(AttributeTargets.Property)]
	public class AttributeProviderAttribute : Attribute
	{
		// Token: 0x06003155 RID: 12629 RVA: 0x000DF62E File Offset: 0x000DD82E
		public AttributeProviderAttribute(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			this._typeName = typeName;
		}

		// Token: 0x06003156 RID: 12630 RVA: 0x000DF64B File Offset: 0x000DD84B
		public AttributeProviderAttribute(string typeName, string propertyName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			if (propertyName == null)
			{
				throw new ArgumentNullException("propertyName");
			}
			this._typeName = typeName;
			this._propertyName = propertyName;
		}

		// Token: 0x06003157 RID: 12631 RVA: 0x000DF67D File Offset: 0x000DD87D
		public AttributeProviderAttribute(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this._typeName = type.AssemblyQualifiedName;
		}

		// Token: 0x17000C13 RID: 3091
		// (get) Token: 0x06003158 RID: 12632 RVA: 0x000DF6A5 File Offset: 0x000DD8A5
		public string TypeName
		{
			get
			{
				return this._typeName;
			}
		}

		// Token: 0x17000C14 RID: 3092
		// (get) Token: 0x06003159 RID: 12633 RVA: 0x000DF6AD File Offset: 0x000DD8AD
		public string PropertyName
		{
			get
			{
				return this._propertyName;
			}
		}

		// Token: 0x0400291A RID: 10522
		private string _typeName;

		// Token: 0x0400291B RID: 10523
		private string _propertyName;
	}
}

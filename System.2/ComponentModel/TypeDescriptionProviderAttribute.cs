using System;

namespace System.ComponentModel
{
	// Token: 0x020005B5 RID: 1461
	[AttributeUsage(AttributeTargets.Class, Inherited = true)]
	public sealed class TypeDescriptionProviderAttribute : Attribute
	{
		// Token: 0x06003681 RID: 13953 RVA: 0x000ED217 File Offset: 0x000EB417
		public TypeDescriptionProviderAttribute(string typeName)
		{
			if (typeName == null)
			{
				throw new ArgumentNullException("typeName");
			}
			this._typeName = typeName;
		}

		// Token: 0x06003682 RID: 13954 RVA: 0x000ED234 File Offset: 0x000EB434
		public TypeDescriptionProviderAttribute(Type type)
		{
			if (type == null)
			{
				throw new ArgumentNullException("type");
			}
			this._typeName = type.AssemblyQualifiedName;
		}

		// Token: 0x17000D3D RID: 3389
		// (get) Token: 0x06003683 RID: 13955 RVA: 0x000ED25C File Offset: 0x000EB45C
		public string TypeName
		{
			get
			{
				return this._typeName;
			}
		}

		// Token: 0x04002AAE RID: 10926
		private string _typeName;
	}
}

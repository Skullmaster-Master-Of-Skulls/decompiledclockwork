using System;
using System.Globalization;

namespace System.ComponentModel
{
	// Token: 0x020005B3 RID: 1459
	[AttributeUsage(AttributeTargets.All)]
	public sealed class TypeConverterAttribute : Attribute
	{
		// Token: 0x0600366B RID: 13931 RVA: 0x000ECF73 File Offset: 0x000EB173
		public TypeConverterAttribute()
		{
			this.typeName = string.Empty;
		}

		// Token: 0x0600366C RID: 13932 RVA: 0x000ECF86 File Offset: 0x000EB186
		public TypeConverterAttribute(Type type)
		{
			this.typeName = type.AssemblyQualifiedName;
		}

		// Token: 0x0600366D RID: 13933 RVA: 0x000ECF9C File Offset: 0x000EB19C
		public TypeConverterAttribute(string typeName)
		{
			string text = typeName.ToUpper(CultureInfo.InvariantCulture);
			this.typeName = typeName;
		}

		// Token: 0x17000D3C RID: 3388
		// (get) Token: 0x0600366E RID: 13934 RVA: 0x000ECFC2 File Offset: 0x000EB1C2
		public string ConverterTypeName
		{
			get
			{
				return this.typeName;
			}
		}

		// Token: 0x0600366F RID: 13935 RVA: 0x000ECFCC File Offset: 0x000EB1CC
		public override bool Equals(object obj)
		{
			TypeConverterAttribute typeConverterAttribute = obj as TypeConverterAttribute;
			return typeConverterAttribute != null && typeConverterAttribute.ConverterTypeName == this.typeName;
		}

		// Token: 0x06003670 RID: 13936 RVA: 0x000ECFF6 File Offset: 0x000EB1F6
		public override int GetHashCode()
		{
			return this.typeName.GetHashCode();
		}

		// Token: 0x04002AAA RID: 10922
		private string typeName;

		// Token: 0x04002AAB RID: 10923
		public static readonly TypeConverterAttribute Default = new TypeConverterAttribute();
	}
}

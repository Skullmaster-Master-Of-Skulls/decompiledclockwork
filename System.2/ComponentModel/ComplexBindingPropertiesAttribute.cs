using System;

namespace System.ComponentModel
{
	// Token: 0x02000529 RID: 1321
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class ComplexBindingPropertiesAttribute : Attribute
	{
		// Token: 0x06003205 RID: 12805 RVA: 0x000E096D File Offset: 0x000DEB6D
		public ComplexBindingPropertiesAttribute()
		{
			this.dataSource = null;
			this.dataMember = null;
		}

		// Token: 0x06003206 RID: 12806 RVA: 0x000E0983 File Offset: 0x000DEB83
		public ComplexBindingPropertiesAttribute(string dataSource)
		{
			this.dataSource = dataSource;
			this.dataMember = null;
		}

		// Token: 0x06003207 RID: 12807 RVA: 0x000E0999 File Offset: 0x000DEB99
		public ComplexBindingPropertiesAttribute(string dataSource, string dataMember)
		{
			this.dataSource = dataSource;
			this.dataMember = dataMember;
		}

		// Token: 0x17000C47 RID: 3143
		// (get) Token: 0x06003208 RID: 12808 RVA: 0x000E09AF File Offset: 0x000DEBAF
		public string DataSource
		{
			get
			{
				return this.dataSource;
			}
		}

		// Token: 0x17000C48 RID: 3144
		// (get) Token: 0x06003209 RID: 12809 RVA: 0x000E09B7 File Offset: 0x000DEBB7
		public string DataMember
		{
			get
			{
				return this.dataMember;
			}
		}

		// Token: 0x0600320A RID: 12810 RVA: 0x000E09C0 File Offset: 0x000DEBC0
		public override bool Equals(object obj)
		{
			ComplexBindingPropertiesAttribute complexBindingPropertiesAttribute = obj as ComplexBindingPropertiesAttribute;
			return complexBindingPropertiesAttribute != null && complexBindingPropertiesAttribute.DataSource == this.dataSource && complexBindingPropertiesAttribute.DataMember == this.dataMember;
		}

		// Token: 0x0600320B RID: 12811 RVA: 0x000E09FD File Offset: 0x000DEBFD
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x0400295C RID: 10588
		private readonly string dataSource;

		// Token: 0x0400295D RID: 10589
		private readonly string dataMember;

		// Token: 0x0400295E RID: 10590
		public static readonly ComplexBindingPropertiesAttribute Default = new ComplexBindingPropertiesAttribute();
	}
}

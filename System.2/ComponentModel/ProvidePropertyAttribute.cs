using System;

namespace System.ComponentModel
{
	// Token: 0x0200059E RID: 1438
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
	public sealed class ProvidePropertyAttribute : Attribute
	{
		// Token: 0x0600358C RID: 13708 RVA: 0x000E8CAA File Offset: 0x000E6EAA
		public ProvidePropertyAttribute(string propertyName, Type receiverType)
		{
			this.propertyName = propertyName;
			this.receiverTypeName = receiverType.AssemblyQualifiedName;
		}

		// Token: 0x0600358D RID: 13709 RVA: 0x000E8CC5 File Offset: 0x000E6EC5
		public ProvidePropertyAttribute(string propertyName, string receiverTypeName)
		{
			this.propertyName = propertyName;
			this.receiverTypeName = receiverTypeName;
		}

		// Token: 0x17000D18 RID: 3352
		// (get) Token: 0x0600358E RID: 13710 RVA: 0x000E8CDB File Offset: 0x000E6EDB
		public string PropertyName
		{
			get
			{
				return this.propertyName;
			}
		}

		// Token: 0x17000D19 RID: 3353
		// (get) Token: 0x0600358F RID: 13711 RVA: 0x000E8CE3 File Offset: 0x000E6EE3
		public string ReceiverTypeName
		{
			get
			{
				return this.receiverTypeName;
			}
		}

		// Token: 0x17000D1A RID: 3354
		// (get) Token: 0x06003590 RID: 13712 RVA: 0x000E8CEB File Offset: 0x000E6EEB
		public override object TypeId
		{
			get
			{
				return base.GetType().FullName + this.propertyName;
			}
		}

		// Token: 0x06003591 RID: 13713 RVA: 0x000E8D04 File Offset: 0x000E6F04
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			ProvidePropertyAttribute providePropertyAttribute = obj as ProvidePropertyAttribute;
			return providePropertyAttribute != null && providePropertyAttribute.propertyName == this.propertyName && providePropertyAttribute.receiverTypeName == this.receiverTypeName;
		}

		// Token: 0x06003592 RID: 13714 RVA: 0x000E8D47 File Offset: 0x000E6F47
		public override int GetHashCode()
		{
			return this.propertyName.GetHashCode() ^ this.receiverTypeName.GetHashCode();
		}

		// Token: 0x04002A54 RID: 10836
		private readonly string propertyName;

		// Token: 0x04002A55 RID: 10837
		private readonly string receiverTypeName;
	}
}

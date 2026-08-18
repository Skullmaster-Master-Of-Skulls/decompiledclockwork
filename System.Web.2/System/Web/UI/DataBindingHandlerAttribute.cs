using System;

namespace System.Web.UI
{
	// Token: 0x02000271 RID: 625
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DataBindingHandlerAttribute : Attribute
	{
		// Token: 0x06001DC2 RID: 7618 RVA: 0x00060967 File Offset: 0x0005EB67
		public DataBindingHandlerAttribute()
		{
			this._typeName = string.Empty;
		}

		// Token: 0x06001DC3 RID: 7619 RVA: 0x0006097A File Offset: 0x0005EB7A
		public DataBindingHandlerAttribute(Type type)
		{
			this._typeName = type.AssemblyQualifiedName;
		}

		// Token: 0x06001DC4 RID: 7620 RVA: 0x0006098E File Offset: 0x0005EB8E
		public DataBindingHandlerAttribute(string typeName)
		{
			this._typeName = typeName;
		}

		// Token: 0x1700085F RID: 2143
		// (get) Token: 0x06001DC5 RID: 7621 RVA: 0x0006099D File Offset: 0x0005EB9D
		public string HandlerTypeName
		{
			get
			{
				if (this._typeName == null)
				{
					return string.Empty;
				}
				return this._typeName;
			}
		}

		// Token: 0x06001DC6 RID: 7622 RVA: 0x000609B4 File Offset: 0x0005EBB4
		public override bool Equals(object obj)
		{
			if (obj == this)
			{
				return true;
			}
			DataBindingHandlerAttribute dataBindingHandlerAttribute = obj as DataBindingHandlerAttribute;
			return dataBindingHandlerAttribute != null && string.Compare(this.HandlerTypeName, dataBindingHandlerAttribute.HandlerTypeName, StringComparison.Ordinal) == 0;
		}

		// Token: 0x06001DC7 RID: 7623 RVA: 0x000609E8 File Offset: 0x0005EBE8
		public override int GetHashCode()
		{
			return this.HandlerTypeName.GetHashCode();
		}

		// Token: 0x04001969 RID: 6505
		private string _typeName;

		// Token: 0x0400196A RID: 6506
		public static readonly DataBindingHandlerAttribute Default = new DataBindingHandlerAttribute();
	}
}

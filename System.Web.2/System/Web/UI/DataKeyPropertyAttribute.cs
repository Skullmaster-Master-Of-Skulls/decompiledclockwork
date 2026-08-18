using System;

namespace System.Web.UI
{
	// Token: 0x02000275 RID: 629
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DataKeyPropertyAttribute : Attribute
	{
		// Token: 0x06001DDF RID: 7647 RVA: 0x00060C11 File Offset: 0x0005EE11
		public DataKeyPropertyAttribute(string name)
		{
			this._name = name;
		}

		// Token: 0x17000863 RID: 2147
		// (get) Token: 0x06001DE0 RID: 7648 RVA: 0x00060C20 File Offset: 0x0005EE20
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x06001DE1 RID: 7649 RVA: 0x00060C28 File Offset: 0x0005EE28
		public override bool Equals(object obj)
		{
			DataKeyPropertyAttribute dataKeyPropertyAttribute = obj as DataKeyPropertyAttribute;
			return dataKeyPropertyAttribute != null && string.Equals(this._name, dataKeyPropertyAttribute.Name, StringComparison.Ordinal);
		}

		// Token: 0x06001DE2 RID: 7650 RVA: 0x00060C53 File Offset: 0x0005EE53
		public override int GetHashCode()
		{
			if (this.Name == null)
			{
				return 0;
			}
			return this.Name.GetHashCode();
		}

		// Token: 0x0400196F RID: 6511
		private readonly string _name;
	}
}

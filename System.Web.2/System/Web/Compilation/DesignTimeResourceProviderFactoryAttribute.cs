using System;

namespace System.Web.Compilation
{
	// Token: 0x02000839 RID: 2105
	[AttributeUsage(AttributeTargets.Class)]
	public sealed class DesignTimeResourceProviderFactoryAttribute : Attribute
	{
		// Token: 0x0600647D RID: 25725 RVA: 0x00160666 File Offset: 0x0015E866
		public DesignTimeResourceProviderFactoryAttribute(Type factoryType)
		{
			this._factoryTypeName = factoryType.AssemblyQualifiedName;
		}

		// Token: 0x0600647E RID: 25726 RVA: 0x0016067A File Offset: 0x0015E87A
		public DesignTimeResourceProviderFactoryAttribute(string factoryTypeName)
		{
			this._factoryTypeName = factoryTypeName;
		}

		// Token: 0x17001C50 RID: 7248
		// (get) Token: 0x0600647F RID: 25727 RVA: 0x00160689 File Offset: 0x0015E889
		public string FactoryTypeName
		{
			get
			{
				return this._factoryTypeName;
			}
		}

		// Token: 0x06006480 RID: 25728 RVA: 0x00160691 File Offset: 0x0015E891
		public override bool IsDefaultAttribute()
		{
			return this._factoryTypeName == null;
		}

		// Token: 0x040033E2 RID: 13282
		private string _factoryTypeName;
	}
}

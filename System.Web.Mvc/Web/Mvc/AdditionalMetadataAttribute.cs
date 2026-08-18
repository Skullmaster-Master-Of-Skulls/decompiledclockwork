using System;

namespace System.Web.Mvc
{
	// Token: 0x02000054 RID: 84
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Property | AttributeTargets.Interface, AllowMultiple = true)]
	public sealed class AdditionalMetadataAttribute : Attribute, IMetadataAware
	{
		// Token: 0x06000229 RID: 553 RVA: 0x00007B70 File Offset: 0x00005D70
		public AdditionalMetadataAttribute(string name, object value)
		{
			if (name == null)
			{
				throw new ArgumentNullException("name");
			}
			this.Name = name;
			this.Value = value;
		}

		// Token: 0x170000F6 RID: 246
		// (get) Token: 0x0600022A RID: 554 RVA: 0x00007B9F File Offset: 0x00005D9F
		public override object TypeId
		{
			get
			{
				return this._typeId;
			}
		}

		// Token: 0x170000F7 RID: 247
		// (get) Token: 0x0600022B RID: 555 RVA: 0x00007BA7 File Offset: 0x00005DA7
		// (set) Token: 0x0600022C RID: 556 RVA: 0x00007BAF File Offset: 0x00005DAF
		public string Name { get; private set; }

		// Token: 0x170000F8 RID: 248
		// (get) Token: 0x0600022D RID: 557 RVA: 0x00007BB8 File Offset: 0x00005DB8
		// (set) Token: 0x0600022E RID: 558 RVA: 0x00007BC0 File Offset: 0x00005DC0
		public object Value { get; private set; }

		// Token: 0x0600022F RID: 559 RVA: 0x00007BC9 File Offset: 0x00005DC9
		public void OnMetadataCreated(ModelMetadata metadata)
		{
			if (metadata == null)
			{
				throw new ArgumentNullException("metadata");
			}
			metadata.AdditionalValues[this.Name] = this.Value;
		}

		// Token: 0x04000067 RID: 103
		private object _typeId = new object();
	}
}

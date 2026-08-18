using System;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000046 RID: 70
	internal sealed class ReferenceAppId
	{
		// Token: 0x0600015B RID: 347 RVA: 0x00007256 File Offset: 0x00005456
		internal ReferenceAppId(IReferenceAppId id)
		{
			if (id == null)
			{
				throw new ArgumentNullException();
			}
			this._id = id;
		}

		// Token: 0x17000045 RID: 69
		// (get) Token: 0x0600015C RID: 348 RVA: 0x0000726E File Offset: 0x0000546E
		// (set) Token: 0x0600015D RID: 349 RVA: 0x0000727B File Offset: 0x0000547B
		public string SubscriptionId
		{
			get
			{
				return this._id.get_SubscriptionId();
			}
			set
			{
				this._id.put_SubscriptionId(value);
			}
		}

		// Token: 0x17000046 RID: 70
		// (get) Token: 0x0600015E RID: 350 RVA: 0x00007289 File Offset: 0x00005489
		// (set) Token: 0x0600015F RID: 351 RVA: 0x00007296 File Offset: 0x00005496
		public string Codebase
		{
			get
			{
				return this._id.get_Codebase();
			}
			set
			{
				this._id.put_Codebase(value);
			}
		}

		// Token: 0x17000047 RID: 71
		// (get) Token: 0x06000160 RID: 352 RVA: 0x000072A4 File Offset: 0x000054A4
		public EnumReferenceIdentity AppPath
		{
			get
			{
				return new EnumReferenceIdentity(this._id.EnumAppPath());
			}
		}

		// Token: 0x04000145 RID: 325
		internal IReferenceAppId _id;
	}
}

using System;

namespace System.Deployment.Internal.Isolation
{
	// Token: 0x02000044 RID: 68
	internal sealed class DefinitionAppId
	{
		// Token: 0x0600014F RID: 335 RVA: 0x000071E5 File Offset: 0x000053E5
		internal DefinitionAppId(IDefinitionAppId id)
		{
			if (id == null)
			{
				throw new ArgumentNullException();
			}
			this._id = id;
		}

		// Token: 0x17000042 RID: 66
		// (get) Token: 0x06000150 RID: 336 RVA: 0x000071FD File Offset: 0x000053FD
		// (set) Token: 0x06000151 RID: 337 RVA: 0x0000720A File Offset: 0x0000540A
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

		// Token: 0x17000043 RID: 67
		// (get) Token: 0x06000152 RID: 338 RVA: 0x00007218 File Offset: 0x00005418
		// (set) Token: 0x06000153 RID: 339 RVA: 0x00007225 File Offset: 0x00005425
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

		// Token: 0x17000044 RID: 68
		// (get) Token: 0x06000154 RID: 340 RVA: 0x00007233 File Offset: 0x00005433
		public EnumDefinitionIdentity AppPath
		{
			get
			{
				return new EnumDefinitionIdentity(this._id.EnumAppPath());
			}
		}

		// Token: 0x06000155 RID: 341 RVA: 0x00007245 File Offset: 0x00005445
		private void SetAppPath(IDefinitionIdentity[] Ids)
		{
			this._id.SetAppPath((uint)Ids.Length, Ids);
		}

		// Token: 0x04000144 RID: 324
		internal IDefinitionAppId _id;
	}
}

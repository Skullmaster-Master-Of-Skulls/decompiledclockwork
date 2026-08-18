using System;

namespace System.Security.Permissions
{
	// Token: 0x02000485 RID: 1157
	[AttributeUsage(AttributeTargets.Assembly | AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Constructor | AttributeTargets.Method, AllowMultiple = true, Inherited = false)]
	[Serializable]
	public sealed class StorePermissionAttribute : CodeAccessSecurityAttribute
	{
		// Token: 0x06002AEF RID: 10991 RVA: 0x000C3952 File Offset: 0x000C1B52
		public StorePermissionAttribute(SecurityAction action) : base(action)
		{
		}

		// Token: 0x17000A64 RID: 2660
		// (get) Token: 0x06002AF0 RID: 10992 RVA: 0x000C395B File Offset: 0x000C1B5B
		// (set) Token: 0x06002AF1 RID: 10993 RVA: 0x000C3963 File Offset: 0x000C1B63
		public StorePermissionFlags Flags
		{
			get
			{
				return this.m_flags;
			}
			set
			{
				StorePermission.VerifyFlags(value);
				this.m_flags = value;
			}
		}

		// Token: 0x17000A65 RID: 2661
		// (get) Token: 0x06002AF2 RID: 10994 RVA: 0x000C3972 File Offset: 0x000C1B72
		// (set) Token: 0x06002AF3 RID: 10995 RVA: 0x000C397F File Offset: 0x000C1B7F
		public bool CreateStore
		{
			get
			{
				return (this.m_flags & StorePermissionFlags.CreateStore) > StorePermissionFlags.NoFlags;
			}
			set
			{
				this.m_flags = (value ? (this.m_flags | StorePermissionFlags.CreateStore) : (this.m_flags & ~StorePermissionFlags.CreateStore));
			}
		}

		// Token: 0x17000A66 RID: 2662
		// (get) Token: 0x06002AF4 RID: 10996 RVA: 0x000C399D File Offset: 0x000C1B9D
		// (set) Token: 0x06002AF5 RID: 10997 RVA: 0x000C39AA File Offset: 0x000C1BAA
		public bool DeleteStore
		{
			get
			{
				return (this.m_flags & StorePermissionFlags.DeleteStore) > StorePermissionFlags.NoFlags;
			}
			set
			{
				this.m_flags = (value ? (this.m_flags | StorePermissionFlags.DeleteStore) : (this.m_flags & ~StorePermissionFlags.DeleteStore));
			}
		}

		// Token: 0x17000A67 RID: 2663
		// (get) Token: 0x06002AF6 RID: 10998 RVA: 0x000C39C8 File Offset: 0x000C1BC8
		// (set) Token: 0x06002AF7 RID: 10999 RVA: 0x000C39D5 File Offset: 0x000C1BD5
		public bool EnumerateStores
		{
			get
			{
				return (this.m_flags & StorePermissionFlags.EnumerateStores) > StorePermissionFlags.NoFlags;
			}
			set
			{
				this.m_flags = (value ? (this.m_flags | StorePermissionFlags.EnumerateStores) : (this.m_flags & ~StorePermissionFlags.EnumerateStores));
			}
		}

		// Token: 0x17000A68 RID: 2664
		// (get) Token: 0x06002AF8 RID: 11000 RVA: 0x000C39F3 File Offset: 0x000C1BF3
		// (set) Token: 0x06002AF9 RID: 11001 RVA: 0x000C3A01 File Offset: 0x000C1C01
		public bool OpenStore
		{
			get
			{
				return (this.m_flags & StorePermissionFlags.OpenStore) > StorePermissionFlags.NoFlags;
			}
			set
			{
				this.m_flags = (value ? (this.m_flags | StorePermissionFlags.OpenStore) : (this.m_flags & ~StorePermissionFlags.OpenStore));
			}
		}

		// Token: 0x17000A69 RID: 2665
		// (get) Token: 0x06002AFA RID: 11002 RVA: 0x000C3A20 File Offset: 0x000C1C20
		// (set) Token: 0x06002AFB RID: 11003 RVA: 0x000C3A2E File Offset: 0x000C1C2E
		public bool AddToStore
		{
			get
			{
				return (this.m_flags & StorePermissionFlags.AddToStore) > StorePermissionFlags.NoFlags;
			}
			set
			{
				this.m_flags = (value ? (this.m_flags | StorePermissionFlags.AddToStore) : (this.m_flags & ~StorePermissionFlags.AddToStore));
			}
		}

		// Token: 0x17000A6A RID: 2666
		// (get) Token: 0x06002AFC RID: 11004 RVA: 0x000C3A4D File Offset: 0x000C1C4D
		// (set) Token: 0x06002AFD RID: 11005 RVA: 0x000C3A5B File Offset: 0x000C1C5B
		public bool RemoveFromStore
		{
			get
			{
				return (this.m_flags & StorePermissionFlags.RemoveFromStore) > StorePermissionFlags.NoFlags;
			}
			set
			{
				this.m_flags = (value ? (this.m_flags | StorePermissionFlags.RemoveFromStore) : (this.m_flags & ~StorePermissionFlags.RemoveFromStore));
			}
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x06002AFE RID: 11006 RVA: 0x000C3A7A File Offset: 0x000C1C7A
		// (set) Token: 0x06002AFF RID: 11007 RVA: 0x000C3A8B File Offset: 0x000C1C8B
		public bool EnumerateCertificates
		{
			get
			{
				return (this.m_flags & StorePermissionFlags.EnumerateCertificates) > StorePermissionFlags.NoFlags;
			}
			set
			{
				this.m_flags = (value ? (this.m_flags | StorePermissionFlags.EnumerateCertificates) : (this.m_flags & ~StorePermissionFlags.EnumerateCertificates));
			}
		}

		// Token: 0x06002B00 RID: 11008 RVA: 0x000C3AB0 File Offset: 0x000C1CB0
		public override IPermission CreatePermission()
		{
			if (base.Unrestricted)
			{
				return new StorePermission(PermissionState.Unrestricted);
			}
			return new StorePermission(this.m_flags);
		}

		// Token: 0x04002661 RID: 9825
		private StorePermissionFlags m_flags;
	}
}

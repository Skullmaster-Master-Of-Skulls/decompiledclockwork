using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.IdentityModel.Metadata
{
	// Token: 0x020000F1 RID: 241
	public class EntityDescriptor : MetadataBase
	{
		// Token: 0x06000693 RID: 1683 RVA: 0x0001A8EA File Offset: 0x00018AEA
		public EntityDescriptor() : this(null)
		{
		}

		// Token: 0x06000694 RID: 1684 RVA: 0x0001A8F3 File Offset: 0x00018AF3
		public EntityDescriptor(EntityId entityId)
		{
			this.entityId = entityId;
		}

		// Token: 0x17000179 RID: 377
		// (get) Token: 0x06000695 RID: 1685 RVA: 0x0001A918 File Offset: 0x00018B18
		public ICollection<ContactPerson> Contacts
		{
			get
			{
				return this.contacts;
			}
		}

		// Token: 0x1700017A RID: 378
		// (get) Token: 0x06000696 RID: 1686 RVA: 0x0001A920 File Offset: 0x00018B20
		// (set) Token: 0x06000697 RID: 1687 RVA: 0x0001A928 File Offset: 0x00018B28
		public EntityId EntityId
		{
			get
			{
				return this.entityId;
			}
			set
			{
				this.entityId = value;
			}
		}

		// Token: 0x1700017B RID: 379
		// (get) Token: 0x06000698 RID: 1688 RVA: 0x0001A931 File Offset: 0x00018B31
		// (set) Token: 0x06000699 RID: 1689 RVA: 0x0001A939 File Offset: 0x00018B39
		public string FederationId
		{
			get
			{
				return this.federationId;
			}
			set
			{
				this.federationId = value;
			}
		}

		// Token: 0x1700017C RID: 380
		// (get) Token: 0x0600069A RID: 1690 RVA: 0x0001A942 File Offset: 0x00018B42
		// (set) Token: 0x0600069B RID: 1691 RVA: 0x0001A94A File Offset: 0x00018B4A
		public Organization Organization
		{
			get
			{
				return this.organization;
			}
			set
			{
				this.organization = value;
			}
		}

		// Token: 0x1700017D RID: 381
		// (get) Token: 0x0600069C RID: 1692 RVA: 0x0001A953 File Offset: 0x00018B53
		public ICollection<RoleDescriptor> RoleDescriptors
		{
			get
			{
				return this.roleDescriptors;
			}
		}

		// Token: 0x04000A65 RID: 2661
		private Collection<ContactPerson> contacts = new Collection<ContactPerson>();

		// Token: 0x04000A66 RID: 2662
		private EntityId entityId;

		// Token: 0x04000A67 RID: 2663
		private string federationId;

		// Token: 0x04000A68 RID: 2664
		private Organization organization;

		// Token: 0x04000A69 RID: 2665
		private Collection<RoleDescriptor> roleDescriptors = new Collection<RoleDescriptor>();
	}
}

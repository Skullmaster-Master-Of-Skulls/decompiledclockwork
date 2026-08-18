using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace System.IdentityModel.Metadata
{
	// Token: 0x020000F0 RID: 240
	public class EntitiesDescriptor : MetadataBase
	{
		// Token: 0x0600068B RID: 1675 RVA: 0x0001A841 File Offset: 0x00018A41
		public EntitiesDescriptor() : this(new Collection<EntityDescriptor>(), new Collection<EntitiesDescriptor>())
		{
		}

		// Token: 0x0600068C RID: 1676 RVA: 0x0001A853 File Offset: 0x00018A53
		public EntitiesDescriptor(Collection<EntitiesDescriptor> entityGroupList)
		{
			this.entityGroupCollection = new Collection<EntitiesDescriptor>();
			this.entityCollection = new Collection<EntityDescriptor>();
			base..ctor();
			this.entityGroupCollection = entityGroupList;
		}

		// Token: 0x0600068D RID: 1677 RVA: 0x0001A878 File Offset: 0x00018A78
		public EntitiesDescriptor(Collection<EntityDescriptor> entityList)
		{
			this.entityGroupCollection = new Collection<EntitiesDescriptor>();
			this.entityCollection = new Collection<EntityDescriptor>();
			base..ctor();
			this.entityCollection = entityList;
		}

		// Token: 0x0600068E RID: 1678 RVA: 0x0001A89D File Offset: 0x00018A9D
		public EntitiesDescriptor(Collection<EntityDescriptor> entityList, Collection<EntitiesDescriptor> entityGroupList)
		{
			this.entityGroupCollection = new Collection<EntitiesDescriptor>();
			this.entityCollection = new Collection<EntityDescriptor>();
			base..ctor();
			this.entityCollection = entityList;
			this.entityGroupCollection = entityGroupList;
		}

		// Token: 0x17000176 RID: 374
		// (get) Token: 0x0600068F RID: 1679 RVA: 0x0001A8C9 File Offset: 0x00018AC9
		public ICollection<EntityDescriptor> ChildEntities
		{
			get
			{
				return this.entityCollection;
			}
		}

		// Token: 0x17000177 RID: 375
		// (get) Token: 0x06000690 RID: 1680 RVA: 0x0001A8D1 File Offset: 0x00018AD1
		public ICollection<EntitiesDescriptor> ChildEntityGroups
		{
			get
			{
				return this.entityGroupCollection;
			}
		}

		// Token: 0x17000178 RID: 376
		// (get) Token: 0x06000691 RID: 1681 RVA: 0x0001A8D9 File Offset: 0x00018AD9
		// (set) Token: 0x06000692 RID: 1682 RVA: 0x0001A8E1 File Offset: 0x00018AE1
		public string Name
		{
			get
			{
				return this.name;
			}
			set
			{
				this.name = value;
			}
		}

		// Token: 0x04000A62 RID: 2658
		private Collection<EntitiesDescriptor> entityGroupCollection;

		// Token: 0x04000A63 RID: 2659
		private Collection<EntityDescriptor> entityCollection;

		// Token: 0x04000A64 RID: 2660
		private string name;
	}
}

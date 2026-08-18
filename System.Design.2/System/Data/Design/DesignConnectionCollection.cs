using System;
using System.Collections;

namespace System.Data.Design
{
	// Token: 0x02000239 RID: 569
	internal class DesignConnectionCollection : DataSourceCollectionBase, IDesignConnectionCollection, INamedObjectCollection, ICollection, IEnumerable
	{
		// Token: 0x06001569 RID: 5481 RVA: 0x00077CC7 File Offset: 0x00075EC7
		internal DesignConnectionCollection(DataSourceComponent collectionHost) : base(collectionHost)
		{
		}

		// Token: 0x170004C2 RID: 1218
		// (get) Token: 0x0600156A RID: 5482 RVA: 0x00078A34 File Offset: 0x00076C34
		protected override Type ItemType
		{
			get
			{
				return typeof(IDesignConnection);
			}
		}

		// Token: 0x170004C3 RID: 1219
		// (get) Token: 0x0600156B RID: 5483 RVA: 0x00077CD0 File Offset: 0x00075ED0
		protected override INameService NameService
		{
			get
			{
				return SimpleNameService.DefaultInstance;
			}
		}

		// Token: 0x0600156C RID: 5484 RVA: 0x00078A40 File Offset: 0x00076C40
		public IDesignConnection Get(string name)
		{
			return (IDesignConnection)NamedObjectUtil.Find(this, name);
		}

		// Token: 0x0600156D RID: 5485 RVA: 0x00078A50 File Offset: 0x00076C50
		protected override void OnSet(int index, object oldValue, object newValue)
		{
			base.OnSet(index, oldValue, newValue);
			base.ValidateType(newValue);
			IDesignConnection designConnection = (IDesignConnection)oldValue;
			IDesignConnection designConnection2 = (IDesignConnection)newValue;
			if (!StringUtil.EqualValue(designConnection.Name, designConnection2.Name))
			{
				this.ValidateUniqueName(designConnection2, designConnection2.Name);
			}
		}

		// Token: 0x0600156E RID: 5486 RVA: 0x00078A9C File Offset: 0x00076C9C
		public void Set(IDesignConnection connection)
		{
			INamedObject namedObject = NamedObjectUtil.Find(this, connection.Name);
			if (namedObject != null)
			{
				base.List.Remove(namedObject);
			}
			base.List.Add(connection);
		}

		// Token: 0x0600156F RID: 5487 RVA: 0x00057A39 File Offset: 0x00055C39
		public bool Contains(IDesignConnection connection)
		{
			return base.List.Contains(connection);
		}

		// Token: 0x06001570 RID: 5488 RVA: 0x0005799D File Offset: 0x00055B9D
		public int Add(IDesignConnection connection)
		{
			return base.List.Add(connection);
		}

		// Token: 0x06001571 RID: 5489 RVA: 0x00057A47 File Offset: 0x00055C47
		public void Remove(IDesignConnection connection)
		{
			base.List.Remove(connection);
		}
	}
}

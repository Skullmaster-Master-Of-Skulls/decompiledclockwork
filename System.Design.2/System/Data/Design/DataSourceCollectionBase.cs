using System;
using System.Collections;
using System.ComponentModel;

namespace System.Data.Design
{
	// Token: 0x02000221 RID: 545
	internal abstract class DataSourceCollectionBase : CollectionBase, INamedObjectCollection, ICollection, IEnumerable, IObjectWithParent
	{
		// Token: 0x06001445 RID: 5189 RVA: 0x000752E7 File Offset: 0x000734E7
		internal DataSourceCollectionBase(DataSourceComponent collectionHost)
		{
			this.collectionHost = collectionHost;
		}

		// Token: 0x17000465 RID: 1125
		// (get) Token: 0x06001446 RID: 5190 RVA: 0x000752F6 File Offset: 0x000734F6
		// (set) Token: 0x06001447 RID: 5191 RVA: 0x000752FE File Offset: 0x000734FE
		internal virtual DataSourceComponent CollectionHost
		{
			get
			{
				return this.collectionHost;
			}
			set
			{
				this.collectionHost = value;
			}
		}

		// Token: 0x17000466 RID: 1126
		// (get) Token: 0x06001448 RID: 5192 RVA: 0x00075307 File Offset: 0x00073507
		protected virtual Type ItemType
		{
			get
			{
				return typeof(IDataSourceNamedObject);
			}
		}

		// Token: 0x17000467 RID: 1127
		// (get) Token: 0x06001449 RID: 5193
		protected abstract INameService NameService { get; }

		// Token: 0x17000468 RID: 1128
		// (get) Token: 0x0600144A RID: 5194 RVA: 0x000752F6 File Offset: 0x000734F6
		[Browsable(false)]
		object IObjectWithParent.Parent
		{
			get
			{
				return this.collectionHost;
			}
		}

		// Token: 0x0600144B RID: 5195 RVA: 0x00075314 File Offset: 0x00073514
		protected virtual string CreateUniqueName(IDataSourceNamedObject value)
		{
			string proposedNameRoot = StringUtil.NotEmpty(value.Name) ? value.Name : value.PublicTypeName;
			return this.NameService.CreateUniqueName(this, proposedNameRoot, 1);
		}

		// Token: 0x0600144C RID: 5196 RVA: 0x0007534B File Offset: 0x0007354B
		protected internal virtual void EnsureUniqueName(IDataSourceNamedObject namedObject)
		{
			if (namedObject.Name == null || namedObject.Name.Length == 0 || this.FindObject(namedObject.Name) != null)
			{
				namedObject.Name = this.CreateUniqueName(namedObject);
			}
		}

		// Token: 0x0600144D RID: 5197 RVA: 0x00075380 File Offset: 0x00073580
		protected internal virtual IDataSourceNamedObject FindObject(string name)
		{
			foreach (object obj in base.InnerList)
			{
				IDataSourceNamedObject dataSourceNamedObject = (IDataSourceNamedObject)obj;
				if (StringUtil.EqualValue(dataSourceNamedObject.Name, name))
				{
					return dataSourceNamedObject;
				}
			}
			return null;
		}

		// Token: 0x0600144E RID: 5198 RVA: 0x000753C0 File Offset: 0x000735C0
		public void InsertBefore(object value, object refObject)
		{
			int num = base.List.IndexOf(refObject);
			if (num >= 0)
			{
				base.List.Insert(num, value);
				return;
			}
			base.List.Add(value);
		}

		// Token: 0x0600144F RID: 5199 RVA: 0x000753F9 File Offset: 0x000735F9
		protected override void OnValidate(object value)
		{
			base.OnValidate(value);
			this.ValidateType(value);
		}

		// Token: 0x06001450 RID: 5200 RVA: 0x0007540C File Offset: 0x0007360C
		public void Remove(string name)
		{
			INamedObject namedObject = NamedObjectUtil.Find(this, name);
			if (namedObject != null)
			{
				base.List.Remove(namedObject);
			}
		}

		// Token: 0x06001451 RID: 5201 RVA: 0x00075430 File Offset: 0x00073630
		protected internal virtual void ValidateName(IDataSourceNamedObject obj)
		{
			this.NameService.ValidateName(obj.Name);
		}

		// Token: 0x06001452 RID: 5202 RVA: 0x00075443 File Offset: 0x00073643
		protected internal virtual void ValidateUniqueName(IDataSourceNamedObject obj, string proposedName)
		{
			this.NameService.ValidateUniqueName(this, obj, proposedName);
		}

		// Token: 0x06001453 RID: 5203 RVA: 0x00075453 File Offset: 0x00073653
		protected void ValidateType(object value)
		{
			if (!this.ItemType.IsInstanceOfType(value))
			{
				throw new InternalException("{0} can hold only {1} objects", 20016, true);
			}
		}

		// Token: 0x06001454 RID: 5204 RVA: 0x00075474 File Offset: 0x00073674
		public INameService GetNameService()
		{
			return this.NameService;
		}

		// Token: 0x04000AD5 RID: 2773
		private DataSourceComponent collectionHost;
	}
}

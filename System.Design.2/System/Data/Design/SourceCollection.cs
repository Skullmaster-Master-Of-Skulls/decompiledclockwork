using System;
using System.Collections;
using System.Design;

namespace System.Data.Design
{
	// Token: 0x02000265 RID: 613
	internal class SourceCollection : DataSourceCollectionBase, ICloneable
	{
		// Token: 0x06001792 RID: 6034 RVA: 0x00077CC7 File Offset: 0x00075EC7
		internal SourceCollection(DataSourceComponent collectionHost) : base(collectionHost)
		{
		}

		// Token: 0x17000569 RID: 1385
		// (get) Token: 0x06001793 RID: 6035 RVA: 0x000819EB File Offset: 0x0007FBEB
		protected override Type ItemType
		{
			get
			{
				return typeof(Source);
			}
		}

		// Token: 0x1700056A RID: 1386
		// (get) Token: 0x06001794 RID: 6036 RVA: 0x000819F8 File Offset: 0x0007FBF8
		private DbSource MainSource
		{
			get
			{
				DesignTable designTable = this.CollectionHost as DesignTable;
				return designTable.MainSource as DbSource;
			}
		}

		// Token: 0x1700056B RID: 1387
		// (get) Token: 0x06001795 RID: 6037 RVA: 0x00081A1C File Offset: 0x0007FC1C
		protected override INameService NameService
		{
			get
			{
				return SourceNameService.DefaultInstance;
			}
		}

		// Token: 0x06001796 RID: 6038 RVA: 0x0005799D File Offset: 0x00055B9D
		public int Add(Source s)
		{
			return base.List.Add(s);
		}

		// Token: 0x06001797 RID: 6039 RVA: 0x00081A24 File Offset: 0x0007FC24
		public object Clone()
		{
			SourceCollection sourceCollection = new SourceCollection(null);
			foreach (object obj in this)
			{
				Source source = (Source)obj;
				sourceCollection.Add((Source)source.Clone());
			}
			return sourceCollection;
		}

		// Token: 0x06001798 RID: 6040 RVA: 0x00057A39 File Offset: 0x00055C39
		public bool Contains(Source s)
		{
			return base.List.Contains(s);
		}

		// Token: 0x06001799 RID: 6041 RVA: 0x00081A8C File Offset: 0x0007FC8C
		private bool DbSourceNameExist(DbSource dbSource, bool isFillName, string nameToBeChecked)
		{
			if (isFillName && StringUtil.EqualValue(nameToBeChecked, dbSource.GetMethodName, true))
			{
				return true;
			}
			if (!isFillName && StringUtil.EqualValue(nameToBeChecked, dbSource.FillMethodName, true))
			{
				return true;
			}
			foreach (object obj in this)
			{
				DbSource dbSource2 = (DbSource)obj;
				if (dbSource2 != dbSource && dbSource2.NameExist(nameToBeChecked))
				{
					return true;
				}
			}
			DbSource mainSource = this.MainSource;
			return dbSource != mainSource && mainSource != null && mainSource.NameExist(nameToBeChecked);
		}

		// Token: 0x0600179A RID: 6042 RVA: 0x00081B34 File Offset: 0x0007FD34
		protected internal override IDataSourceNamedObject FindObject(string name)
		{
			DbSource mainSource = this.MainSource;
			if (mainSource != null && mainSource.NameExist(name))
			{
				return mainSource;
			}
			foreach (object obj in base.InnerList)
			{
				DbSource dbSource = obj as DbSource;
				if (dbSource != null)
				{
					if (dbSource.NameExist(name))
					{
						return dbSource;
					}
				}
				else
				{
					IEnumerator enumerator;
					IDataSourceNamedObject dataSourceNamedObject = (IDataSourceNamedObject)enumerator.Current;
					if (StringUtil.EqualValue(dataSourceNamedObject.Name, name, false))
					{
						return dataSourceNamedObject;
					}
				}
			}
			return null;
		}

		// Token: 0x0600179B RID: 6043 RVA: 0x00057A2B File Offset: 0x00055C2B
		public int IndexOf(Source s)
		{
			return base.List.IndexOf(s);
		}

		// Token: 0x0600179C RID: 6044 RVA: 0x00057A47 File Offset: 0x00055C47
		public void Remove(Source s)
		{
			base.List.Remove(s);
		}

		// Token: 0x0600179D RID: 6045 RVA: 0x00081BA4 File Offset: 0x0007FDA4
		private void ValidateNameWithMainSource(object dbSourceToCheck, string nameToCheck)
		{
			DbSource mainSource = this.MainSource;
			if (dbSourceToCheck != mainSource && mainSource != null && mainSource.NameExist(nameToCheck))
			{
				throw new NameValidationException(SR.GetString("CM_NameExist", new object[]
				{
					nameToCheck
				}));
			}
		}

		// Token: 0x0600179E RID: 6046 RVA: 0x00081BE4 File Offset: 0x0007FDE4
		protected internal override void ValidateName(IDataSourceNamedObject obj)
		{
			DbSource dbSource = obj as DbSource;
			if (dbSource != null)
			{
				if ((dbSource.GenerateMethods & GenerateMethodTypes.Get) == GenerateMethodTypes.Get)
				{
					this.NameService.ValidateName(dbSource.GetMethodName);
				}
				if ((dbSource.GenerateMethods & GenerateMethodTypes.Fill) == GenerateMethodTypes.Fill)
				{
					this.NameService.ValidateName(dbSource.FillMethodName);
					return;
				}
			}
			else
			{
				base.ValidateName(obj);
			}
		}

		// Token: 0x0600179F RID: 6047 RVA: 0x00081C3B File Offset: 0x0007FE3B
		protected internal override void ValidateUniqueName(IDataSourceNamedObject obj, string proposedName)
		{
			this.ValidateNameWithMainSource(obj, proposedName);
			base.ValidateUniqueName(obj, proposedName);
		}

		// Token: 0x060017A0 RID: 6048 RVA: 0x00081C4D File Offset: 0x0007FE4D
		internal void ValidateUniqueDbSourceName(DbSource dbSource, string proposedName, bool isFillName)
		{
			if (this.DbSourceNameExist(dbSource, isFillName, proposedName))
			{
				throw new NameValidationException(SR.GetString("CM_NameExist", new object[]
				{
					proposedName
				}));
			}
			this.NameService.ValidateName(proposedName);
		}
	}
}

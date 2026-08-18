using System;
using System.Collections;
using System.Globalization;

namespace System.Data.Design
{
	// Token: 0x02000233 RID: 563
	internal class DbSourceParameterCollection : DataSourceCollectionBase, IDataParameterCollection, IList, ICollection, IEnumerable, ICloneable
	{
		// Token: 0x060014FD RID: 5373 RVA: 0x00077CC7 File Offset: 0x00075EC7
		internal DbSourceParameterCollection(DataSourceComponent collectionHost) : base(collectionHost)
		{
		}

		// Token: 0x17000495 RID: 1173
		// (get) Token: 0x060014FE RID: 5374 RVA: 0x00077CD0 File Offset: 0x00075ED0
		protected override INameService NameService
		{
			get
			{
				return SimpleNameService.DefaultInstance;
			}
		}

		// Token: 0x17000496 RID: 1174
		object IDataParameterCollection.this[string parameterName]
		{
			get
			{
				int index = this.RangeCheck(parameterName);
				return base.List[index];
			}
			set
			{
				int index = this.RangeCheck(parameterName);
				base.List[index] = value;
			}
		}

		// Token: 0x17000497 RID: 1175
		public DesignParameter this[int index]
		{
			get
			{
				return (DesignParameter)base.List[index];
			}
		}

		// Token: 0x06001502 RID: 5378 RVA: 0x00077D31 File Offset: 0x00075F31
		public bool Contains(string value)
		{
			return this.IndexOf(value) != -1;
		}

		// Token: 0x06001503 RID: 5379 RVA: 0x00077D40 File Offset: 0x00075F40
		public int IndexOf(string parameterName)
		{
			int count = base.InnerList.Count;
			for (int i = 0; i < count; i++)
			{
				if (StringUtil.EqualValue(parameterName, ((IDbDataParameter)base.InnerList[i]).ParameterName))
				{
					return i;
				}
			}
			return -1;
		}

		// Token: 0x06001504 RID: 5380 RVA: 0x00077D88 File Offset: 0x00075F88
		private int RangeCheck(string parameterName)
		{
			int num = this.IndexOf(parameterName);
			if (num < 0)
			{
				throw new InternalException(string.Format(CultureInfo.CurrentCulture, "No parameter named '{0}' found", new object[]
				{
					parameterName
				}), 20004);
			}
			return num;
		}

		// Token: 0x06001505 RID: 5381 RVA: 0x00077DC8 File Offset: 0x00075FC8
		public void RemoveAt(string parameterName)
		{
			int index = this.RangeCheck(parameterName);
			base.List.RemoveAt(index);
		}

		// Token: 0x17000498 RID: 1176
		// (get) Token: 0x06001506 RID: 5382 RVA: 0x00077DE9 File Offset: 0x00075FE9
		protected override Type ItemType
		{
			get
			{
				return typeof(DesignParameter);
			}
		}

		// Token: 0x06001507 RID: 5383 RVA: 0x00077DF8 File Offset: 0x00075FF8
		public object Clone()
		{
			DbSourceParameterCollection dbSourceParameterCollection = new DbSourceParameterCollection(null);
			foreach (object obj in this)
			{
				DesignParameter designParameter = (DesignParameter)obj;
				DesignParameter value = (DesignParameter)designParameter.Clone();
				((IList)dbSourceParameterCollection).Add(value);
			}
			return dbSourceParameterCollection;
		}
	}
}

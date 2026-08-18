using System;
using System.Collections.Generic;
using System.Data.Entity.Core;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Resources;
using System.Linq;

namespace System.Data.Entity.Internal
{
	// Token: 0x020007A6 RID: 1958
	internal class WrappedEntityKey
	{
		// Token: 0x0600585C RID: 22620 RVA: 0x0017C1D4 File Offset: 0x0017A3D4
		public WrappedEntityKey(EntitySet entitySet, string entitySetName, object[] keyValues, string keyValuesParamName)
		{
			if (keyValues == null)
			{
				object[] array = new object[1];
				keyValues = array;
			}
			List<string> list = (from m in entitySet.ElementType.KeyMembers
			select m.Name).ToList<string>();
			if (list.Count != keyValues.Length)
			{
				throw new ArgumentException(Strings.DbSet_WrongNumberOfKeyValuesPassed, keyValuesParamName);
			}
			this._keyValuePairs = list.Zip(keyValues, (string name, object value) => new KeyValuePair<string, object>(name, value));
			if (keyValues.All((object v) => v != null))
			{
				this._key = new EntityKey(entitySetName, this.KeyValuePairs);
			}
		}

		// Token: 0x17000F74 RID: 3956
		// (get) Token: 0x0600585D RID: 22621 RVA: 0x0017C2A0 File Offset: 0x0017A4A0
		public bool HasNullValues
		{
			get
			{
				return this._key == null;
			}
		}

		// Token: 0x17000F75 RID: 3957
		// (get) Token: 0x0600585E RID: 22622 RVA: 0x0017C2AE File Offset: 0x0017A4AE
		public EntityKey EntityKey
		{
			get
			{
				return this._key;
			}
		}

		// Token: 0x17000F76 RID: 3958
		// (get) Token: 0x0600585F RID: 22623 RVA: 0x0017C2B6 File Offset: 0x0017A4B6
		public IEnumerable<KeyValuePair<string, object>> KeyValuePairs
		{
			get
			{
				return this._keyValuePairs;
			}
		}

		// Token: 0x0400237C RID: 9084
		private readonly IEnumerable<KeyValuePair<string, object>> _keyValuePairs;

		// Token: 0x0400237D RID: 9085
		private readonly EntityKey _key;
	}
}

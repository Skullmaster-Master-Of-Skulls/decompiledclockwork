using System;
using System.Collections.Generic;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.Migrations.Model
{
	// Token: 0x020001A5 RID: 421
	public abstract class MigrationOperation
	{
		// Token: 0x06000E47 RID: 3655 RVA: 0x0003EE84 File Offset: 0x0003D084
		protected MigrationOperation(object anonymousArguments)
		{
			MigrationOperation <>4__this = this;
			if (anonymousArguments != null)
			{
				anonymousArguments.GetType().GetNonIndexerProperties().Each(delegate(PropertyInfo p)
				{
					<>4__this._anonymousArguments.Add(p.Name, p.GetValue(anonymousArguments, null));
				});
			}
		}

		// Token: 0x1700013D RID: 317
		// (get) Token: 0x06000E48 RID: 3656 RVA: 0x0003EEE6 File Offset: 0x0003D0E6
		public IDictionary<string, object> AnonymousArguments
		{
			get
			{
				return this._anonymousArguments;
			}
		}

		// Token: 0x1700013E RID: 318
		// (get) Token: 0x06000E49 RID: 3657 RVA: 0x0003EEEE File Offset: 0x0003D0EE
		public virtual MigrationOperation Inverse
		{
			get
			{
				return null;
			}
		}

		// Token: 0x1700013F RID: 319
		// (get) Token: 0x06000E4A RID: 3658
		public abstract bool IsDestructiveChange { get; }

		// Token: 0x040003D3 RID: 979
		private readonly IDictionary<string, object> _anonymousArguments = new Dictionary<string, object>();
	}
}

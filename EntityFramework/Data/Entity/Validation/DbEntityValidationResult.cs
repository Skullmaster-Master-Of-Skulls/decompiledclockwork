using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Internal;
using System.Data.Entity.Utilities;
using System.Linq;

namespace System.Data.Entity.Validation
{
	// Token: 0x02000832 RID: 2098
	[Serializable]
	public class DbEntityValidationResult
	{
		// Token: 0x06005DE7 RID: 24039 RVA: 0x00195BCD File Offset: 0x00193DCD
		public DbEntityValidationResult(DbEntityEntry entry, IEnumerable<DbValidationError> validationErrors)
		{
			Check.NotNull<DbEntityEntry>(entry, "entry");
			Check.NotNull<IEnumerable<DbValidationError>>(validationErrors, "validationErrors");
			this._entry = entry.InternalEntry;
			this._validationErrors = validationErrors.ToList<DbValidationError>();
		}

		// Token: 0x06005DE8 RID: 24040 RVA: 0x00195C05 File Offset: 0x00193E05
		internal DbEntityValidationResult(InternalEntityEntry entry, IEnumerable<DbValidationError> validationErrors)
		{
			this._entry = entry;
			this._validationErrors = validationErrors.ToList<DbValidationError>();
		}

		// Token: 0x17000FE8 RID: 4072
		// (get) Token: 0x06005DE9 RID: 24041 RVA: 0x00195C20 File Offset: 0x00193E20
		public DbEntityEntry Entry
		{
			get
			{
				if (this._entry == null)
				{
					return null;
				}
				return new DbEntityEntry(this._entry);
			}
		}

		// Token: 0x17000FE9 RID: 4073
		// (get) Token: 0x06005DEA RID: 24042 RVA: 0x00195C37 File Offset: 0x00193E37
		public ICollection<DbValidationError> ValidationErrors
		{
			get
			{
				return this._validationErrors;
			}
		}

		// Token: 0x17000FEA RID: 4074
		// (get) Token: 0x06005DEB RID: 24043 RVA: 0x00195C3F File Offset: 0x00193E3F
		public bool IsValid
		{
			get
			{
				return !this._validationErrors.Any<DbValidationError>();
			}
		}

		// Token: 0x04002511 RID: 9489
		[NonSerialized]
		private readonly InternalEntityEntry _entry;

		// Token: 0x04002512 RID: 9490
		private readonly List<DbValidationError> _validationErrors;
	}
}

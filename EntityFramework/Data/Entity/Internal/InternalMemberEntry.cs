using System;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Internal.Validation;
using System.Data.Entity.Validation;
using System.Linq;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000775 RID: 1909
	internal abstract class InternalMemberEntry
	{
		// Token: 0x06005676 RID: 22134 RVA: 0x0017657D File Offset: 0x0017477D
		protected InternalMemberEntry(InternalEntityEntry internalEntityEntry, MemberEntryMetadata memberMetadata)
		{
			this._internalEntityEntry = internalEntityEntry;
			this._memberMetadata = memberMetadata;
		}

		// Token: 0x17000EF7 RID: 3831
		// (get) Token: 0x06005677 RID: 22135 RVA: 0x00176593 File Offset: 0x00174793
		public virtual string Name
		{
			get
			{
				return this._memberMetadata.MemberName;
			}
		}

		// Token: 0x17000EF8 RID: 3832
		// (get) Token: 0x06005678 RID: 22136
		// (set) Token: 0x06005679 RID: 22137
		public abstract object CurrentValue { get; set; }

		// Token: 0x17000EF9 RID: 3833
		// (get) Token: 0x0600567A RID: 22138 RVA: 0x001765A0 File Offset: 0x001747A0
		public virtual InternalEntityEntry InternalEntityEntry
		{
			get
			{
				return this._internalEntityEntry;
			}
		}

		// Token: 0x17000EFA RID: 3834
		// (get) Token: 0x0600567B RID: 22139 RVA: 0x001765A8 File Offset: 0x001747A8
		public virtual MemberEntryMetadata EntryMetadata
		{
			get
			{
				return this._memberMetadata;
			}
		}

		// Token: 0x0600567C RID: 22140 RVA: 0x001765B0 File Offset: 0x001747B0
		public virtual IEnumerable<DbValidationError> GetValidationErrors()
		{
			ValidationProvider validationProvider = this.InternalEntityEntry.InternalContext.ValidationProvider;
			PropertyValidator propertyValidator = validationProvider.GetPropertyValidator(this._internalEntityEntry, this);
			if (propertyValidator == null)
			{
				return Enumerable.Empty<DbValidationError>();
			}
			return propertyValidator.Validate(validationProvider.GetEntityValidationContext(this._internalEntityEntry, null), this);
		}

		// Token: 0x0600567D RID: 22141
		public abstract DbMemberEntry CreateDbMemberEntry();

		// Token: 0x0600567E RID: 22142
		public abstract DbMemberEntry<TEntity, TProperty> CreateDbMemberEntry<TEntity, TProperty>() where TEntity : class;

		// Token: 0x04002300 RID: 8960
		private readonly InternalEntityEntry _internalEntityEntry;

		// Token: 0x04002301 RID: 8961
		private readonly MemberEntryMetadata _memberMetadata;
	}
}

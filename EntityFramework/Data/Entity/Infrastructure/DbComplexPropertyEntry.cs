using System;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x02000747 RID: 1863
	public class DbComplexPropertyEntry : DbPropertyEntry
	{
		// Token: 0x06005453 RID: 21587 RVA: 0x001710AF File Offset: 0x0016F2AF
		internal new static DbComplexPropertyEntry Create(InternalPropertyEntry internalPropertyEntry)
		{
			return (DbComplexPropertyEntry)internalPropertyEntry.CreateDbMemberEntry();
		}

		// Token: 0x06005454 RID: 21588 RVA: 0x001710BC File Offset: 0x0016F2BC
		internal DbComplexPropertyEntry(InternalPropertyEntry internalPropertyEntry) : base(internalPropertyEntry)
		{
		}

		// Token: 0x06005455 RID: 21589 RVA: 0x001710C5 File Offset: 0x0016F2C5
		public DbPropertyEntry Property(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbPropertyEntry.Create(((InternalPropertyEntry)this.InternalMemberEntry).Property(propertyName, null, false));
		}

		// Token: 0x06005456 RID: 21590 RVA: 0x001710EB File Offset: 0x0016F2EB
		[SuppressMessage("Microsoft.Naming", "CA1719:ParameterNamesShouldNotMatchMemberNames", MessageId = "0#", Justification = "Rule predates more fluent naming conventions.")]
		public DbComplexPropertyEntry ComplexProperty(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbComplexPropertyEntry.Create(((InternalPropertyEntry)this.InternalMemberEntry).Property(propertyName, null, true));
		}

		// Token: 0x06005457 RID: 21591 RVA: 0x00171114 File Offset: 0x0016F314
		public new DbComplexPropertyEntry<TEntity, TComplexProperty> Cast<TEntity, TComplexProperty>() where TEntity : class
		{
			MemberEntryMetadata entryMetadata = this.InternalMemberEntry.EntryMetadata;
			if (!typeof(TEntity).IsAssignableFrom(entryMetadata.DeclaringType) || !typeof(TComplexProperty).IsAssignableFrom(entryMetadata.ElementType))
			{
				throw Error.DbMember_BadTypeForCast(typeof(DbComplexPropertyEntry).Name, typeof(TEntity).Name, typeof(TComplexProperty).Name, entryMetadata.DeclaringType.Name, entryMetadata.MemberType.Name);
			}
			return DbComplexPropertyEntry<TEntity, TComplexProperty>.Create((InternalPropertyEntry)this.InternalMemberEntry);
		}
	}
}

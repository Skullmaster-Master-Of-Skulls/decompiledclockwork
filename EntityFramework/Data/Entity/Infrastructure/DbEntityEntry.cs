using System;
using System.ComponentModel;
using System.Data.Entity.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Data.Entity.Validation;
using System.Diagnostics.CodeAnalysis;
using System.Threading;
using System.Threading.Tasks;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200074D RID: 1869
	public class DbEntityEntry
	{
		// Token: 0x0600549F RID: 21663 RVA: 0x00171AFB File Offset: 0x0016FCFB
		internal DbEntityEntry(InternalEntityEntry internalEntityEntry)
		{
			this._internalEntityEntry = internalEntityEntry;
		}

		// Token: 0x17000E6D RID: 3693
		// (get) Token: 0x060054A0 RID: 21664 RVA: 0x00171B0A File Offset: 0x0016FD0A
		public object Entity
		{
			get
			{
				return this._internalEntityEntry.Entity;
			}
		}

		// Token: 0x17000E6E RID: 3694
		// (get) Token: 0x060054A1 RID: 21665 RVA: 0x00171B17 File Offset: 0x0016FD17
		// (set) Token: 0x060054A2 RID: 21666 RVA: 0x00171B24 File Offset: 0x0016FD24
		public EntityState State
		{
			get
			{
				return this._internalEntityEntry.State;
			}
			set
			{
				this._internalEntityEntry.State = value;
			}
		}

		// Token: 0x17000E6F RID: 3695
		// (get) Token: 0x060054A3 RID: 21667 RVA: 0x00171B32 File Offset: 0x0016FD32
		public DbPropertyValues CurrentValues
		{
			get
			{
				return new DbPropertyValues(this._internalEntityEntry.CurrentValues);
			}
		}

		// Token: 0x17000E70 RID: 3696
		// (get) Token: 0x060054A4 RID: 21668 RVA: 0x00171B44 File Offset: 0x0016FD44
		public DbPropertyValues OriginalValues
		{
			get
			{
				return new DbPropertyValues(this._internalEntityEntry.OriginalValues);
			}
		}

		// Token: 0x060054A5 RID: 21669 RVA: 0x00171B58 File Offset: 0x0016FD58
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public DbPropertyValues GetDatabaseValues()
		{
			InternalPropertyValues databaseValues = this._internalEntityEntry.GetDatabaseValues();
			if (databaseValues != null)
			{
				return new DbPropertyValues(databaseValues);
			}
			return null;
		}

		// Token: 0x060054A6 RID: 21670 RVA: 0x00171B7C File Offset: 0x0016FD7C
		public Task<DbPropertyValues> GetDatabaseValuesAsync()
		{
			return this.GetDatabaseValuesAsync(CancellationToken.None);
		}

		// Token: 0x060054A7 RID: 21671 RVA: 0x00171C94 File Offset: 0x0016FE94
		public async Task<DbPropertyValues> GetDatabaseValuesAsync(CancellationToken cancellationToken)
		{
			InternalPropertyValues storeValues = await this._internalEntityEntry.GetDatabaseValuesAsync(cancellationToken).WithCurrentCulture<InternalPropertyValues>();
			return (storeValues == null) ? null : new DbPropertyValues(storeValues);
		}

		// Token: 0x060054A8 RID: 21672 RVA: 0x00171CE2 File Offset: 0x0016FEE2
		public void Reload()
		{
			this._internalEntityEntry.Reload();
		}

		// Token: 0x060054A9 RID: 21673 RVA: 0x00171CEF File Offset: 0x0016FEEF
		public Task ReloadAsync()
		{
			return this._internalEntityEntry.ReloadAsync(CancellationToken.None);
		}

		// Token: 0x060054AA RID: 21674 RVA: 0x00171D01 File Offset: 0x0016FF01
		public Task ReloadAsync(CancellationToken cancellationToken)
		{
			return this._internalEntityEntry.ReloadAsync(cancellationToken);
		}

		// Token: 0x060054AB RID: 21675 RVA: 0x00171D0F File Offset: 0x0016FF0F
		public DbReferenceEntry Reference(string navigationProperty)
		{
			Check.NotEmpty(navigationProperty, "navigationProperty");
			return DbReferenceEntry.Create(this._internalEntityEntry.Reference(navigationProperty, null));
		}

		// Token: 0x060054AC RID: 21676 RVA: 0x00171D2F File Offset: 0x0016FF2F
		public DbCollectionEntry Collection(string navigationProperty)
		{
			Check.NotEmpty(navigationProperty, "navigationProperty");
			return DbCollectionEntry.Create(this._internalEntityEntry.Collection(navigationProperty, null));
		}

		// Token: 0x060054AD RID: 21677 RVA: 0x00171D4F File Offset: 0x0016FF4F
		public DbPropertyEntry Property(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbPropertyEntry.Create(this._internalEntityEntry.Property(propertyName, null, false));
		}

		// Token: 0x060054AE RID: 21678 RVA: 0x00171D70 File Offset: 0x0016FF70
		public DbComplexPropertyEntry ComplexProperty(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbComplexPropertyEntry.Create(this._internalEntityEntry.Property(propertyName, null, true));
		}

		// Token: 0x060054AF RID: 21679 RVA: 0x00171D91 File Offset: 0x0016FF91
		public DbMemberEntry Member(string propertyName)
		{
			Check.NotEmpty(propertyName, "propertyName");
			return DbMemberEntry.Create(this._internalEntityEntry.Member(propertyName, null));
		}

		// Token: 0x060054B0 RID: 21680 RVA: 0x00171DB4 File Offset: 0x0016FFB4
		public DbEntityEntry<TEntity> Cast<TEntity>() where TEntity : class
		{
			if (!typeof(TEntity).IsAssignableFrom(this._internalEntityEntry.EntityType))
			{
				throw Error.DbEntity_BadTypeForCast(typeof(DbEntityEntry).Name, typeof(TEntity).Name, this._internalEntityEntry.EntityType.Name);
			}
			return new DbEntityEntry<TEntity>(this._internalEntityEntry);
		}

		// Token: 0x060054B1 RID: 21681 RVA: 0x00171E1C File Offset: 0x0017001C
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public DbEntityValidationResult GetValidationResult()
		{
			return this._internalEntityEntry.InternalContext.Owner.CallValidateEntity(this);
		}

		// Token: 0x17000E71 RID: 3697
		// (get) Token: 0x060054B2 RID: 21682 RVA: 0x00171E34 File Offset: 0x00170034
		internal InternalEntityEntry InternalEntry
		{
			get
			{
				return this._internalEntityEntry;
			}
		}

		// Token: 0x060054B3 RID: 21683 RVA: 0x00171E3C File Offset: 0x0017003C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return !object.ReferenceEquals(null, obj) && !(obj.GetType() != typeof(DbEntityEntry)) && this.Equals((DbEntityEntry)obj);
		}

		// Token: 0x060054B4 RID: 21684 RVA: 0x00171E6C File Offset: 0x0017006C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public bool Equals(DbEntityEntry other)
		{
			return object.ReferenceEquals(this, other) || (!object.ReferenceEquals(null, other) && this._internalEntityEntry.Equals(other._internalEntityEntry));
		}

		// Token: 0x060054B5 RID: 21685 RVA: 0x00171E95 File Offset: 0x00170095
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return this._internalEntityEntry.GetHashCode();
		}

		// Token: 0x060054B6 RID: 21686 RVA: 0x00171EA2 File Offset: 0x001700A2
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x060054B7 RID: 21687 RVA: 0x00171EAA File Offset: 0x001700AA
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x04002295 RID: 8853
		private readonly InternalEntityEntry _internalEntityEntry;
	}
}

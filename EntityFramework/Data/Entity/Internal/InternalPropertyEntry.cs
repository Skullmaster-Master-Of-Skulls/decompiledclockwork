using System;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000779 RID: 1913
	internal abstract class InternalPropertyEntry : InternalMemberEntry
	{
		// Token: 0x060056B8 RID: 22200 RVA: 0x00177649 File Offset: 0x00175849
		protected InternalPropertyEntry(InternalEntityEntry internalEntityEntry, PropertyEntryMetadata propertyMetadata) : base(internalEntityEntry, propertyMetadata)
		{
		}

		// Token: 0x17000F0A RID: 3850
		// (get) Token: 0x060056B9 RID: 22201
		public abstract InternalPropertyEntry ParentPropertyEntry { get; }

		// Token: 0x17000F0B RID: 3851
		// (get) Token: 0x060056BA RID: 22202
		public abstract InternalPropertyValues ParentCurrentValues { get; }

		// Token: 0x17000F0C RID: 3852
		// (get) Token: 0x060056BB RID: 22203
		public abstract InternalPropertyValues ParentOriginalValues { get; }

		// Token: 0x060056BC RID: 22204
		protected abstract Func<object, object> CreateGetter();

		// Token: 0x060056BD RID: 22205
		protected abstract Action<object, object> CreateSetter();

		// Token: 0x060056BE RID: 22206
		public abstract bool EntityPropertyIsModified();

		// Token: 0x060056BF RID: 22207
		public abstract void SetEntityPropertyModified();

		// Token: 0x060056C0 RID: 22208
		public abstract void RejectEntityPropertyChanges();

		// Token: 0x060056C1 RID: 22209
		public abstract void UpdateComplexPropertyState();

		// Token: 0x17000F0D RID: 3853
		// (get) Token: 0x060056C2 RID: 22210 RVA: 0x00177653 File Offset: 0x00175853
		public Func<object, object> Getter
		{
			get
			{
				if (!this._getterIsCached)
				{
					this._getter = this.CreateGetter();
					this._getterIsCached = true;
				}
				return this._getter;
			}
		}

		// Token: 0x17000F0E RID: 3854
		// (get) Token: 0x060056C3 RID: 22211 RVA: 0x00177676 File Offset: 0x00175876
		public Action<object, object> Setter
		{
			get
			{
				if (!this._setterIsCached)
				{
					this._setter = this.CreateSetter();
					this._setterIsCached = true;
				}
				return this._setter;
			}
		}

		// Token: 0x17000F0F RID: 3855
		// (get) Token: 0x060056C4 RID: 22212 RVA: 0x0017769C File Offset: 0x0017589C
		// (set) Token: 0x060056C5 RID: 22213 RVA: 0x001776E0 File Offset: 0x001758E0
		public virtual object OriginalValue
		{
			get
			{
				this.ValidateNotDetachedAndInModel("OriginalValue");
				InternalPropertyValues parentOriginalValues = this.ParentOriginalValues;
				object obj = (parentOriginalValues == null) ? null : parentOriginalValues[this.Name];
				InternalPropertyValues internalPropertyValues = obj as InternalPropertyValues;
				if (internalPropertyValues != null)
				{
					obj = internalPropertyValues.ToObject();
				}
				return obj;
			}
			set
			{
				this.ValidateNotDetachedAndInModel("OriginalValue");
				this.CheckNotSettingComplexPropertyToNull(value);
				InternalPropertyValues parentOriginalValues = this.ParentOriginalValues;
				if (parentOriginalValues == null)
				{
					throw Error.DbPropertyValues_CannotSetPropertyOnNullOriginalValue(this.Name, this.ParentPropertyEntry.Name);
				}
				this.SetPropertyValueUsingValues(parentOriginalValues, value);
			}
		}

		// Token: 0x17000F10 RID: 3856
		// (get) Token: 0x060056C6 RID: 22214 RVA: 0x00177728 File Offset: 0x00175928
		// (set) Token: 0x060056C7 RID: 22215 RVA: 0x001777B8 File Offset: 0x001759B8
		public override object CurrentValue
		{
			get
			{
				if (this.Getter != null)
				{
					return this.Getter(this.InternalEntityEntry.Entity);
				}
				if (!this.InternalEntityEntry.IsDetached && this.EntryMetadata.IsMapped)
				{
					InternalPropertyValues parentCurrentValues = this.ParentCurrentValues;
					object obj = (parentCurrentValues == null) ? null : parentCurrentValues[this.Name];
					InternalPropertyValues internalPropertyValues = obj as InternalPropertyValues;
					if (internalPropertyValues != null)
					{
						obj = internalPropertyValues.ToObject();
					}
					return obj;
				}
				throw Error.DbPropertyEntry_CannotGetCurrentValue(this.Name, base.EntryMetadata.DeclaringType.Name);
			}
			set
			{
				this.CheckNotSettingComplexPropertyToNull(value);
				if (!this.EntryMetadata.IsMapped || this.InternalEntityEntry.IsDetached || this.InternalEntityEntry.State == EntityState.Deleted)
				{
					if (!this.SetCurrentValueOnClrObject(value))
					{
						throw Error.DbPropertyEntry_CannotSetCurrentValue(this.Name, base.EntryMetadata.DeclaringType.Name);
					}
				}
				else
				{
					InternalPropertyValues parentCurrentValues = this.ParentCurrentValues;
					if (parentCurrentValues == null)
					{
						throw Error.DbPropertyValues_CannotSetPropertyOnNullCurrentValue(this.Name, this.ParentPropertyEntry.Name);
					}
					this.SetPropertyValueUsingValues(parentCurrentValues, value);
					if (this.EntryMetadata.IsComplex)
					{
						this.SetCurrentValueOnClrObject(value);
					}
				}
			}
		}

		// Token: 0x060056C8 RID: 22216 RVA: 0x00177857 File Offset: 0x00175A57
		private void CheckNotSettingComplexPropertyToNull(object value)
		{
			if (value == null && this.EntryMetadata.IsComplex)
			{
				throw Error.DbPropertyValues_ComplexObjectCannotBeNull(this.Name, base.EntryMetadata.DeclaringType.Name);
			}
		}

		// Token: 0x060056C9 RID: 22217 RVA: 0x00177888 File Offset: 0x00175A88
		private bool SetCurrentValueOnClrObject(object value)
		{
			if (this.Setter == null)
			{
				return false;
			}
			if (this.Getter == null || !DbHelpers.PropertyValuesEqual(value, this.Getter(this.InternalEntityEntry.Entity)))
			{
				this.Setter(this.InternalEntityEntry.Entity, value);
				if (this.EntryMetadata.IsMapped && (this.InternalEntityEntry.State == EntityState.Modified || this.InternalEntityEntry.State == EntityState.Unchanged))
				{
					this.IsModified = true;
				}
			}
			return true;
		}

		// Token: 0x060056CA RID: 22218 RVA: 0x00177910 File Offset: 0x00175B10
		private void SetPropertyValueUsingValues(InternalPropertyValues internalValues, object value)
		{
			InternalPropertyValues internalPropertyValues = internalValues[this.Name] as InternalPropertyValues;
			if (internalPropertyValues == null)
			{
				internalValues[this.Name] = value;
				return;
			}
			if (!internalPropertyValues.ObjectType.IsAssignableFrom(value.GetType()))
			{
				throw Error.DbPropertyValues_AttemptToSetValuesFromWrongObject(value.GetType().Name, internalPropertyValues.ObjectType.Name);
			}
			internalPropertyValues.SetValues(value);
		}

		// Token: 0x060056CB RID: 22219 RVA: 0x00177976 File Offset: 0x00175B76
		public virtual InternalPropertyEntry Property(string property, Type requestedType = null, bool requireComplex = false)
		{
			return this.InternalEntityEntry.Property(this, property, requestedType ?? typeof(object), requireComplex);
		}

		// Token: 0x17000F11 RID: 3857
		// (get) Token: 0x060056CC RID: 22220 RVA: 0x00177995 File Offset: 0x00175B95
		// (set) Token: 0x060056CD RID: 22221 RVA: 0x001779B9 File Offset: 0x00175BB9
		public virtual bool IsModified
		{
			get
			{
				return !this.InternalEntityEntry.IsDetached && this.EntryMetadata.IsMapped && this.EntityPropertyIsModified();
			}
			set
			{
				this.ValidateNotDetachedAndInModel("IsModified");
				if (value)
				{
					this.SetEntityPropertyModified();
					return;
				}
				if (this.IsModified)
				{
					this.RejectEntityPropertyChanges();
				}
			}
		}

		// Token: 0x060056CE RID: 22222 RVA: 0x001779E0 File Offset: 0x00175BE0
		private void ValidateNotDetachedAndInModel(string method)
		{
			if (!this.EntryMetadata.IsMapped)
			{
				throw Error.DbPropertyEntry_NotSupportedForPropertiesNotInTheModel(method, base.EntryMetadata.MemberName, this.InternalEntityEntry.EntityType.Name);
			}
			if (this.InternalEntityEntry.IsDetached)
			{
				throw Error.DbPropertyEntry_NotSupportedForDetached(method, base.EntryMetadata.MemberName, this.InternalEntityEntry.EntityType.Name);
			}
		}

		// Token: 0x17000F12 RID: 3858
		// (get) Token: 0x060056CF RID: 22223 RVA: 0x00177A4B File Offset: 0x00175C4B
		public new PropertyEntryMetadata EntryMetadata
		{
			get
			{
				return (PropertyEntryMetadata)base.EntryMetadata;
			}
		}

		// Token: 0x060056D0 RID: 22224 RVA: 0x00177A58 File Offset: 0x00175C58
		public override DbMemberEntry CreateDbMemberEntry()
		{
			if (!this.EntryMetadata.IsComplex)
			{
				return new DbPropertyEntry(this);
			}
			return new DbComplexPropertyEntry(this);
		}

		// Token: 0x060056D1 RID: 22225 RVA: 0x00177A74 File Offset: 0x00175C74
		public override DbMemberEntry<TEntity, TProperty> CreateDbMemberEntry<TEntity, TProperty>()
		{
			if (!this.EntryMetadata.IsComplex)
			{
				return new DbPropertyEntry<TEntity, TProperty>(this);
			}
			return new DbComplexPropertyEntry<TEntity, TProperty>(this);
		}

		// Token: 0x0400230D RID: 8973
		private bool _getterIsCached;

		// Token: 0x0400230E RID: 8974
		private Func<object, object> _getter;

		// Token: 0x0400230F RID: 8975
		private bool _setterIsCached;

		// Token: 0x04002310 RID: 8976
		private Action<object, object> _setter;
	}
}

using System;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200077B RID: 1915
	internal class InternalNestedPropertyEntry : InternalPropertyEntry
	{
		// Token: 0x060056DC RID: 22236 RVA: 0x00177B7D File Offset: 0x00175D7D
		public InternalNestedPropertyEntry(InternalPropertyEntry parentPropertyEntry, PropertyEntryMetadata propertyMetadata) : base(parentPropertyEntry.InternalEntityEntry, propertyMetadata)
		{
			this._parentPropertyEntry = parentPropertyEntry;
		}

		// Token: 0x17000F16 RID: 3862
		// (get) Token: 0x060056DD RID: 22237 RVA: 0x00177B93 File Offset: 0x00175D93
		public override InternalPropertyEntry ParentPropertyEntry
		{
			get
			{
				return this._parentPropertyEntry;
			}
		}

		// Token: 0x17000F17 RID: 3863
		// (get) Token: 0x060056DE RID: 22238 RVA: 0x00177B9C File Offset: 0x00175D9C
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public override InternalPropertyValues ParentCurrentValues
		{
			get
			{
				InternalPropertyValues parentCurrentValues = this._parentPropertyEntry.ParentCurrentValues;
				object obj = (parentCurrentValues == null) ? null : parentCurrentValues[this._parentPropertyEntry.Name];
				return (InternalPropertyValues)obj;
			}
		}

		// Token: 0x17000F18 RID: 3864
		// (get) Token: 0x060056DF RID: 22239 RVA: 0x00177BD4 File Offset: 0x00175DD4
		[SuppressMessage("Microsoft.Performance", "CA1800:DoNotCastUnnecessarily")]
		public override InternalPropertyValues ParentOriginalValues
		{
			get
			{
				InternalPropertyValues parentOriginalValues = this._parentPropertyEntry.ParentOriginalValues;
				object obj = (parentOriginalValues == null) ? null : parentOriginalValues[this._parentPropertyEntry.Name];
				return (InternalPropertyValues)obj;
			}
		}

		// Token: 0x060056E0 RID: 22240 RVA: 0x00177C40 File Offset: 0x00175E40
		protected override Func<object, object> CreateGetter()
		{
			Func<object, object> parentGetter = this._parentPropertyEntry.Getter;
			if (parentGetter == null)
			{
				return null;
			}
			Func<object, object> getter;
			if (!DbHelpers.GetPropertyGetters(base.EntryMetadata.DeclaringType).TryGetValue(this.Name, out getter))
			{
				return null;
			}
			return delegate(object o)
			{
				object obj = parentGetter(o);
				if (obj != null)
				{
					return getter(obj);
				}
				return null;
			};
		}

		// Token: 0x060056E1 RID: 22241 RVA: 0x00177D00 File Offset: 0x00175F00
		protected override Action<object, object> CreateSetter()
		{
			Func<object, object> parentGetter = this._parentPropertyEntry.Getter;
			if (parentGetter == null)
			{
				return null;
			}
			Action<object, object> setter;
			if (!DbHelpers.GetPropertySetters(base.EntryMetadata.DeclaringType).TryGetValue(this.Name, out setter))
			{
				return null;
			}
			return delegate(object o, object v)
			{
				if (parentGetter(o) == null)
				{
					throw Error.DbPropertyValues_CannotSetPropertyOnNullCurrentValue(this.Name, this.ParentPropertyEntry.Name);
				}
				setter(parentGetter(o), v);
			};
		}

		// Token: 0x060056E2 RID: 22242 RVA: 0x00177D66 File Offset: 0x00175F66
		public override bool EntityPropertyIsModified()
		{
			return this._parentPropertyEntry.EntityPropertyIsModified();
		}

		// Token: 0x060056E3 RID: 22243 RVA: 0x00177D73 File Offset: 0x00175F73
		public override void SetEntityPropertyModified()
		{
			this._parentPropertyEntry.SetEntityPropertyModified();
		}

		// Token: 0x060056E4 RID: 22244 RVA: 0x00177D80 File Offset: 0x00175F80
		public override void RejectEntityPropertyChanges()
		{
			this.CurrentValue = this.OriginalValue;
			this.UpdateComplexPropertyState();
		}

		// Token: 0x060056E5 RID: 22245 RVA: 0x00177D94 File Offset: 0x00175F94
		public override void UpdateComplexPropertyState()
		{
			this._parentPropertyEntry.UpdateComplexPropertyState();
		}

		// Token: 0x04002311 RID: 8977
		private readonly InternalPropertyEntry _parentPropertyEntry;
	}
}

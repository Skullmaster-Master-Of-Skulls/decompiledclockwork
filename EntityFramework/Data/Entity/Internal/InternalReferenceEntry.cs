using System;
using System.Collections;
using System.Collections.Concurrent;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Linq;
using System.Reflection;

namespace System.Data.Entity.Internal
{
	// Token: 0x0200077C RID: 1916
	internal class InternalReferenceEntry : InternalNavigationEntry
	{
		// Token: 0x060056E6 RID: 22246 RVA: 0x00177DA1 File Offset: 0x00175FA1
		public InternalReferenceEntry(InternalEntityEntry internalEntityEntry, NavigationEntryMetadata navigationMetadata) : base(internalEntityEntry, navigationMetadata)
		{
		}

		// Token: 0x060056E7 RID: 22247 RVA: 0x00177DAC File Offset: 0x00175FAC
		protected override object GetNavigationPropertyFromRelatedEnd(object entity)
		{
			IEnumerator enumerator = base.RelatedEnd.GetEnumerator();
			if (!enumerator.MoveNext())
			{
				return null;
			}
			return enumerator.Current;
		}

		// Token: 0x060056E8 RID: 22248 RVA: 0x00177DD8 File Offset: 0x00175FD8
		protected virtual void SetNavigationPropertyOnRelatedEnd(object value)
		{
			Type type = base.RelatedEnd.GetType();
			Action<IRelatedEnd, object> action;
			if (!InternalReferenceEntry._entityReferenceValueSetters.TryGetValue(type, out action))
			{
				MethodInfo method = InternalReferenceEntry.SetValueOnEntityReferenceMethod.MakeGenericMethod(new Type[]
				{
					type.GetGenericArguments().Single<Type>()
				});
				action = (Action<IRelatedEnd, object>)Delegate.CreateDelegate(typeof(Action<IRelatedEnd, object>), method);
				InternalReferenceEntry._entityReferenceValueSetters.TryAdd(type, action);
			}
			action(base.RelatedEnd, value);
		}

		// Token: 0x060056E9 RID: 22249 RVA: 0x00177E51 File Offset: 0x00176051
		private static void SetValueOnEntityReference<TRelatedEntity>(IRelatedEnd entityReference, object value) where TRelatedEntity : class
		{
			((EntityReference<TRelatedEntity>)entityReference).Value = (TRelatedEntity)((object)value);
		}

		// Token: 0x17000F19 RID: 3865
		// (get) Token: 0x060056EA RID: 22250 RVA: 0x00177E64 File Offset: 0x00176064
		// (set) Token: 0x060056EB RID: 22251 RVA: 0x00177E6C File Offset: 0x0017606C
		public override object CurrentValue
		{
			get
			{
				return base.CurrentValue;
			}
			set
			{
				if (base.RelatedEnd != null && this.InternalEntityEntry.State != EntityState.Deleted)
				{
					this.SetNavigationPropertyOnRelatedEnd(value);
					return;
				}
				if (base.Setter != null)
				{
					base.Setter(this.InternalEntityEntry.Entity, value);
					return;
				}
				throw Error.DbPropertyEntry_SettingEntityRefNotSupported(this.Name, this.InternalEntityEntry.EntityType.Name, this.InternalEntityEntry.State);
			}
		}

		// Token: 0x060056EC RID: 22252 RVA: 0x00177EE2 File Offset: 0x001760E2
		public override DbMemberEntry CreateDbMemberEntry()
		{
			return new DbReferenceEntry(this);
		}

		// Token: 0x060056ED RID: 22253 RVA: 0x00177EEA File Offset: 0x001760EA
		public override DbMemberEntry<TEntity, TProperty> CreateDbMemberEntry<TEntity, TProperty>()
		{
			return new DbReferenceEntry<TEntity, TProperty>(this);
		}

		// Token: 0x04002312 RID: 8978
		private static readonly ConcurrentDictionary<Type, Action<IRelatedEnd, object>> _entityReferenceValueSetters = new ConcurrentDictionary<Type, Action<IRelatedEnd, object>>();

		// Token: 0x04002313 RID: 8979
		public static readonly MethodInfo SetValueOnEntityReferenceMethod = typeof(InternalReferenceEntry).GetOnlyDeclaredMethod("SetValueOnEntityReference");
	}
}

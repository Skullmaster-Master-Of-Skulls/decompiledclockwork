using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Reflection;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000777 RID: 1911
	internal class InternalCollectionEntry : InternalNavigationEntry
	{
		// Token: 0x0600568B RID: 22155 RVA: 0x001767BE File Offset: 0x001749BE
		public InternalCollectionEntry(InternalEntityEntry internalEntityEntry, NavigationEntryMetadata navigationMetadata) : base(internalEntityEntry, navigationMetadata)
		{
		}

		// Token: 0x0600568C RID: 22156 RVA: 0x001767C8 File Offset: 0x001749C8
		protected override object GetNavigationPropertyFromRelatedEnd(object entity)
		{
			return base.RelatedEnd;
		}

		// Token: 0x17000F00 RID: 3840
		// (get) Token: 0x0600568D RID: 22157 RVA: 0x001767D0 File Offset: 0x001749D0
		// (set) Token: 0x0600568E RID: 22158 RVA: 0x001767D8 File Offset: 0x001749D8
		public override object CurrentValue
		{
			get
			{
				return base.CurrentValue;
			}
			set
			{
				if (base.Setter != null)
				{
					base.Setter(this.InternalEntityEntry.Entity, value);
					return;
				}
				if (this.InternalEntityEntry.IsDetached || !object.ReferenceEquals(base.RelatedEnd, value))
				{
					throw Error.DbCollectionEntry_CannotSetCollectionProp(this.Name, this.InternalEntityEntry.Entity.GetType().ToString());
				}
			}
		}

		// Token: 0x0600568F RID: 22159 RVA: 0x00176841 File Offset: 0x00174A41
		public override DbMemberEntry CreateDbMemberEntry()
		{
			return new DbCollectionEntry(this);
		}

		// Token: 0x06005690 RID: 22160 RVA: 0x00176849 File Offset: 0x00174A49
		public override DbMemberEntry<TEntity, TProperty> CreateDbMemberEntry<TEntity, TProperty>()
		{
			return this.CreateDbCollectionEntry<TEntity, TProperty>(this.EntryMetadata.ElementType);
		}

		// Token: 0x06005691 RID: 22161 RVA: 0x0017685C File Offset: 0x00174A5C
		public virtual DbCollectionEntry<TEntity, TElement> CreateDbCollectionEntry<TEntity, TElement>() where TEntity : class
		{
			return new DbCollectionEntry<TEntity, TElement>(this);
		}

		// Token: 0x06005692 RID: 22162 RVA: 0x00176864 File Offset: 0x00174A64
		private DbMemberEntry<TEntity, TProperty> CreateDbCollectionEntry<TEntity, TProperty>(Type elementType) where TEntity : class
		{
			Type typeFromHandle = typeof(DbMemberEntry<TEntity, TProperty>);
			Func<InternalCollectionEntry, object> func;
			if (!InternalCollectionEntry._entryFactories.TryGetValue(typeFromHandle, out func))
			{
				Type type = typeof(DbCollectionEntry<, >).MakeGenericType(new Type[]
				{
					typeof(TEntity),
					elementType
				});
				if (!typeFromHandle.IsAssignableFrom(type))
				{
					throw Error.DbEntityEntry_WrongGenericForCollectionNavProp(typeof(TProperty), this.Name, this.EntryMetadata.DeclaringType, typeof(ICollection<>).MakeGenericType(new Type[]
					{
						elementType
					}));
				}
				MethodInfo declaredMethod = type.GetDeclaredMethod("Create", new Type[]
				{
					typeof(InternalCollectionEntry)
				});
				func = (Func<InternalCollectionEntry, object>)Delegate.CreateDelegate(typeof(Func<InternalCollectionEntry, object>), declaredMethod);
				InternalCollectionEntry._entryFactories.TryAdd(typeFromHandle, func);
			}
			return (DbMemberEntry<TEntity, TProperty>)func(this);
		}

		// Token: 0x04002307 RID: 8967
		private static readonly ConcurrentDictionary<Type, Func<InternalCollectionEntry, object>> _entryFactories = new ConcurrentDictionary<Type, Func<InternalCollectionEntry, object>>();
	}
}

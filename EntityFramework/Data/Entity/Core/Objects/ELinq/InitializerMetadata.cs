using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Entity.Core.Common.Internal.Materialization;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects.DataClasses;
using System.Data.Entity.Core.Objects.Internal;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

namespace System.Data.Entity.Core.Objects.ELinq
{
	// Token: 0x02000554 RID: 1364
	internal abstract class InitializerMetadata : IEquatable<InitializerMetadata>
	{
		// Token: 0x060034E6 RID: 13542 RVA: 0x000F9DF4 File Offset: 0x000F7FF4
		private InitializerMetadata(Type clrType)
		{
			this.ClrType = clrType;
			this.Identity = InitializerMetadata._identifierPrefix + Interlocked.Increment(ref InitializerMetadata.s_identifier).ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x170007E6 RID: 2022
		// (get) Token: 0x060034E7 RID: 13543
		internal abstract InitializerMetadataKind Kind { get; }

		// Token: 0x060034E8 RID: 13544 RVA: 0x000F9E35 File Offset: 0x000F8035
		internal static bool TryGetInitializerMetadata(TypeUsage typeUsage, out InitializerMetadata initializerMetadata)
		{
			initializerMetadata = null;
			if (BuiltInTypeKind.RowType == typeUsage.EdmType.BuiltInTypeKind)
			{
				initializerMetadata = ((RowType)typeUsage.EdmType).InitializerMetadata;
			}
			return null != initializerMetadata;
		}

		// Token: 0x060034E9 RID: 13545 RVA: 0x000F9E63 File Offset: 0x000F8063
		internal static InitializerMetadata CreateGroupingInitializer(EdmItemCollection itemCollection, Type resultType)
		{
			return itemCollection.GetCanonicalInitializerMetadata(new InitializerMetadata.GroupingInitializerMetadata(resultType));
		}

		// Token: 0x060034EA RID: 13546 RVA: 0x000F9E71 File Offset: 0x000F8071
		internal static InitializerMetadata CreateProjectionInitializer(EdmItemCollection itemCollection, MemberInitExpression initExpression)
		{
			return itemCollection.GetCanonicalInitializerMetadata(new InitializerMetadata.ProjectionInitializerMetadata(initExpression));
		}

		// Token: 0x060034EB RID: 13547 RVA: 0x000F9E7F File Offset: 0x000F807F
		internal static InitializerMetadata CreateProjectionInitializer(EdmItemCollection itemCollection, NewExpression newExpression)
		{
			return itemCollection.GetCanonicalInitializerMetadata(new InitializerMetadata.ProjectionNewMetadata(newExpression));
		}

		// Token: 0x060034EC RID: 13548 RVA: 0x000F9E8D File Offset: 0x000F808D
		internal static InitializerMetadata CreateEmptyProjectionInitializer(EdmItemCollection itemCollection, NewExpression newExpression)
		{
			return itemCollection.GetCanonicalInitializerMetadata(new InitializerMetadata.EmptyProjectionNewMetadata(newExpression));
		}

		// Token: 0x060034ED RID: 13549 RVA: 0x000F9E9B File Offset: 0x000F809B
		internal static InitializerMetadata CreateEntityCollectionInitializer(EdmItemCollection itemCollection, Type type, NavigationProperty navigationProperty)
		{
			return itemCollection.GetCanonicalInitializerMetadata(new InitializerMetadata.EntityCollectionInitializerMetadata(type, navigationProperty));
		}

		// Token: 0x060034EE RID: 13550 RVA: 0x000F9EAA File Offset: 0x000F80AA
		internal virtual void AppendColumnMapKey(ColumnMapKeyBuilder builder)
		{
			builder.Append("CLR-", this.ClrType);
		}

		// Token: 0x060034EF RID: 13551 RVA: 0x000F9EBD File Offset: 0x000F80BD
		public override bool Equals(object obj)
		{
			return this.Equals(obj as InitializerMetadata);
		}

		// Token: 0x060034F0 RID: 13552 RVA: 0x000F9ECB File Offset: 0x000F80CB
		public bool Equals(InitializerMetadata other)
		{
			return object.ReferenceEquals(this, other) || (this.Kind == other.Kind && this.ClrType.Equals(other.ClrType) && this.IsStructurallyEquivalent(other));
		}

		// Token: 0x060034F1 RID: 13553 RVA: 0x000F9F04 File Offset: 0x000F8104
		[SuppressMessage("Microsoft.Usage", "CA2303", Justification = "ClrType is not expected to be an Embedded Interop Type.")]
		public override int GetHashCode()
		{
			return this.ClrType.GetHashCode();
		}

		// Token: 0x060034F2 RID: 13554 RVA: 0x000F9F11 File Offset: 0x000F8111
		protected virtual bool IsStructurallyEquivalent(InitializerMetadata other)
		{
			return true;
		}

		// Token: 0x060034F3 RID: 13555
		internal abstract Expression Emit(List<TranslatorResult> propertyTranslatorResults);

		// Token: 0x060034F4 RID: 13556
		internal abstract IEnumerable<Type> GetChildTypes();

		// Token: 0x060034F5 RID: 13557 RVA: 0x000F9F1C File Offset: 0x000F811C
		protected static List<Expression> GetPropertyReaders(List<TranslatorResult> propertyTranslatorResults)
		{
			return (from s in propertyTranslatorResults
			select s.UnwrappedExpression).ToList<Expression>();
		}

		// Token: 0x040013CF RID: 5071
		internal readonly Type ClrType;

		// Token: 0x040013D0 RID: 5072
		private static long s_identifier;

		// Token: 0x040013D1 RID: 5073
		internal readonly string Identity;

		// Token: 0x040013D2 RID: 5074
		private static readonly string _identifierPrefix = typeof(InitializerMetadata).Name;

		// Token: 0x02000555 RID: 1365
		private class Grouping<K, T> : IGrouping<K, T>, IEnumerable<!1>, IEnumerable
		{
			// Token: 0x060034F8 RID: 13560 RVA: 0x000F9F69 File Offset: 0x000F8169
			public Grouping(K key, IEnumerable<T> group)
			{
				this._key = key;
				this._group = group;
			}

			// Token: 0x170007E7 RID: 2023
			// (get) Token: 0x060034F9 RID: 13561 RVA: 0x000F9F7F File Offset: 0x000F817F
			public K Key
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x170007E8 RID: 2024
			// (get) Token: 0x060034FA RID: 13562 RVA: 0x000F9F87 File Offset: 0x000F8187
			public IEnumerable<T> Group
			{
				get
				{
					return this._group;
				}
			}

			// Token: 0x060034FB RID: 13563 RVA: 0x000FA0D4 File Offset: 0x000F82D4
			IEnumerator<T> IEnumerable<!1>.GetEnumerator()
			{
				if (this._group != null)
				{
					foreach (T member in this._group)
					{
						yield return member;
					}
				}
				yield break;
			}

			// Token: 0x060034FC RID: 13564 RVA: 0x000FA0F0 File Offset: 0x000F82F0
			IEnumerator IEnumerable.GetEnumerator()
			{
				return ((IEnumerable<T>)this).GetEnumerator();
			}

			// Token: 0x040013D4 RID: 5076
			private readonly K _key;

			// Token: 0x040013D5 RID: 5077
			private readonly IEnumerable<T> _group;
		}

		// Token: 0x02000556 RID: 1366
		private class GroupingInitializerMetadata : InitializerMetadata
		{
			// Token: 0x060034FD RID: 13565 RVA: 0x000FA0F8 File Offset: 0x000F82F8
			internal GroupingInitializerMetadata(Type type) : base(type)
			{
			}

			// Token: 0x170007E9 RID: 2025
			// (get) Token: 0x060034FE RID: 13566 RVA: 0x000FA101 File Offset: 0x000F8301
			internal override InitializerMetadataKind Kind
			{
				get
				{
					return InitializerMetadataKind.Grouping;
				}
			}

			// Token: 0x060034FF RID: 13567 RVA: 0x000FA104 File Offset: 0x000F8304
			internal override Expression Emit(List<TranslatorResult> propertyTranslatorResults)
			{
				Type type = this.ClrType.GetGenericArguments()[0];
				Type type2 = this.ClrType.GetGenericArguments()[1];
				Type type3 = typeof(InitializerMetadata.Grouping<, >).MakeGenericType(new Type[]
				{
					type,
					type2
				});
				ConstructorInfo constructor = type3.GetConstructors().Single<ConstructorInfo>();
				return Expression.Convert(Expression.New(constructor, InitializerMetadata.GetPropertyReaders(propertyTranslatorResults)), this.ClrType);
			}

			// Token: 0x06003500 RID: 13568 RVA: 0x000FA2AC File Offset: 0x000F84AC
			internal override IEnumerable<Type> GetChildTypes()
			{
				Type keyType = this.ClrType.GetGenericArguments()[0];
				Type groupElementType = this.ClrType.GetGenericArguments()[1];
				yield return keyType;
				yield return typeof(IEnumerable<>).MakeGenericType(new Type[]
				{
					groupElementType
				});
				yield break;
			}
		}

		// Token: 0x02000557 RID: 1367
		private class ProjectionNewMetadata : InitializerMetadata
		{
			// Token: 0x06003501 RID: 13569 RVA: 0x000FA2C9 File Offset: 0x000F84C9
			internal ProjectionNewMetadata(NewExpression newExpression) : base(newExpression.Type)
			{
				this._newExpression = newExpression;
			}

			// Token: 0x170007EA RID: 2026
			// (get) Token: 0x06003502 RID: 13570 RVA: 0x000FA2DE File Offset: 0x000F84DE
			internal override InitializerMetadataKind Kind
			{
				get
				{
					return InitializerMetadataKind.ProjectionNew;
				}
			}

			// Token: 0x06003503 RID: 13571 RVA: 0x000FA2E4 File Offset: 0x000F84E4
			protected override bool IsStructurallyEquivalent(InitializerMetadata other)
			{
				InitializerMetadata.ProjectionNewMetadata projectionNewMetadata = (InitializerMetadata.ProjectionNewMetadata)other;
				if (this._newExpression.Members == null && projectionNewMetadata._newExpression.Members == null)
				{
					return true;
				}
				if (this._newExpression.Members == null || projectionNewMetadata._newExpression.Members == null)
				{
					return false;
				}
				if (this._newExpression.Members.Count != projectionNewMetadata._newExpression.Members.Count)
				{
					return false;
				}
				for (int i = 0; i < this._newExpression.Members.Count; i++)
				{
					MemberInfo memberInfo = this._newExpression.Members[i];
					MemberInfo obj = projectionNewMetadata._newExpression.Members[i];
					if (!memberInfo.Equals(obj))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x06003504 RID: 13572 RVA: 0x000FA39F File Offset: 0x000F859F
			internal override Expression Emit(List<TranslatorResult> propertyTranslatorResults)
			{
				Expression.Constant(null, this.ClrType);
				return Expression.New(this._newExpression.Constructor, InitializerMetadata.GetPropertyReaders(propertyTranslatorResults));
			}

			// Token: 0x06003505 RID: 13573 RVA: 0x000FA3CC File Offset: 0x000F85CC
			internal override IEnumerable<Type> GetChildTypes()
			{
				return from arg in this._newExpression.Arguments
				select arg.Type;
			}

			// Token: 0x06003506 RID: 13574 RVA: 0x000FA3FC File Offset: 0x000F85FC
			internal override void AppendColumnMapKey(ColumnMapKeyBuilder builder)
			{
				base.AppendColumnMapKey(builder);
				builder.Append(this._newExpression.Constructor.ToString());
				foreach (MemberInfo memberInfo in ((IEnumerable<MemberInfo>)(this._newExpression.Members ?? Enumerable.Empty<MemberInfo>())))
				{
					builder.Append("DT", memberInfo.DeclaringType);
					builder.Append("." + memberInfo.Name);
				}
			}

			// Token: 0x040013D6 RID: 5078
			private readonly NewExpression _newExpression;
		}

		// Token: 0x02000558 RID: 1368
		private class EmptyProjectionNewMetadata : InitializerMetadata.ProjectionNewMetadata
		{
			// Token: 0x06003508 RID: 13576 RVA: 0x000FA494 File Offset: 0x000F8694
			internal EmptyProjectionNewMetadata(NewExpression newExpression) : base(newExpression)
			{
			}

			// Token: 0x06003509 RID: 13577 RVA: 0x000FA49D File Offset: 0x000F869D
			internal override Expression Emit(List<TranslatorResult> propertyReaders)
			{
				return base.Emit(new List<TranslatorResult>());
			}

			// Token: 0x0600350A RID: 13578 RVA: 0x000FA570 File Offset: 0x000F8770
			internal override IEnumerable<Type> GetChildTypes()
			{
				yield return null;
				yield break;
			}
		}

		// Token: 0x02000559 RID: 1369
		private class ProjectionInitializerMetadata : InitializerMetadata
		{
			// Token: 0x0600350B RID: 13579 RVA: 0x000FA58D File Offset: 0x000F878D
			internal ProjectionInitializerMetadata(MemberInitExpression initExpression) : base(initExpression.Type)
			{
				this._initExpression = initExpression;
			}

			// Token: 0x170007EB RID: 2027
			// (get) Token: 0x0600350C RID: 13580 RVA: 0x000FA5A2 File Offset: 0x000F87A2
			internal override InitializerMetadataKind Kind
			{
				get
				{
					return InitializerMetadataKind.ProjectionInitializer;
				}
			}

			// Token: 0x0600350D RID: 13581 RVA: 0x000FA5A8 File Offset: 0x000F87A8
			protected override bool IsStructurallyEquivalent(InitializerMetadata other)
			{
				InitializerMetadata.ProjectionInitializerMetadata projectionInitializerMetadata = (InitializerMetadata.ProjectionInitializerMetadata)other;
				if (this._initExpression.Bindings.Count != projectionInitializerMetadata._initExpression.Bindings.Count)
				{
					return false;
				}
				for (int i = 0; i < this._initExpression.Bindings.Count; i++)
				{
					MemberBinding memberBinding = this._initExpression.Bindings[i];
					MemberBinding memberBinding2 = projectionInitializerMetadata._initExpression.Bindings[i];
					if (!memberBinding.Member.Equals(memberBinding2.Member))
					{
						return false;
					}
				}
				return true;
			}

			// Token: 0x0600350E RID: 13582 RVA: 0x000FA638 File Offset: 0x000F8838
			internal override Expression Emit(List<TranslatorResult> propertyReaders)
			{
				MemberBinding[] array = new MemberBinding[this._initExpression.Bindings.Count];
				MemberBinding[] array2 = new MemberBinding[array.Length];
				for (int i = 0; i < array.Length; i++)
				{
					MemberBinding memberBinding = this._initExpression.Bindings[i];
					Expression unwrappedExpression = propertyReaders[i].UnwrappedExpression;
					MemberBinding memberBinding2 = Expression.Bind(memberBinding.Member, unwrappedExpression);
					MemberBinding memberBinding3 = Expression.Bind(memberBinding.Member, Expression.Constant(TypeSystem.GetDefaultValue(unwrappedExpression.Type), unwrappedExpression.Type));
					array[i] = memberBinding2;
					array2[i] = memberBinding3;
				}
				return Expression.MemberInit(this._initExpression.NewExpression, array);
			}

			// Token: 0x0600350F RID: 13583 RVA: 0x000FA894 File Offset: 0x000F8A94
			internal override IEnumerable<Type> GetChildTypes()
			{
				foreach (MemberBinding binding in this._initExpression.Bindings)
				{
					string name;
					Type memberType;
					TypeSystem.PropertyOrField(binding.Member, out name, out memberType);
					yield return memberType;
				}
				yield break;
			}

			// Token: 0x06003510 RID: 13584 RVA: 0x000FA8B4 File Offset: 0x000F8AB4
			internal override void AppendColumnMapKey(ColumnMapKeyBuilder builder)
			{
				base.AppendColumnMapKey(builder);
				foreach (MemberBinding memberBinding in this._initExpression.Bindings)
				{
					builder.Append(",", memberBinding.Member.DeclaringType);
					builder.Append("." + memberBinding.Member.Name);
				}
			}

			// Token: 0x040013D8 RID: 5080
			private readonly MemberInitExpression _initExpression;
		}

		// Token: 0x0200055A RID: 1370
		internal class EntityCollectionInitializerMetadata : InitializerMetadata
		{
			// Token: 0x06003511 RID: 13585 RVA: 0x000FA938 File Offset: 0x000F8B38
			internal EntityCollectionInitializerMetadata(Type type, NavigationProperty navigationProperty) : base(type)
			{
				this._navigationProperty = navigationProperty;
			}

			// Token: 0x170007EC RID: 2028
			// (get) Token: 0x06003512 RID: 13586 RVA: 0x000FA948 File Offset: 0x000F8B48
			internal override InitializerMetadataKind Kind
			{
				get
				{
					return InitializerMetadataKind.EntityCollection;
				}
			}

			// Token: 0x06003513 RID: 13587 RVA: 0x000FA94C File Offset: 0x000F8B4C
			protected override bool IsStructurallyEquivalent(InitializerMetadata other)
			{
				InitializerMetadata.EntityCollectionInitializerMetadata entityCollectionInitializerMetadata = (InitializerMetadata.EntityCollectionInitializerMetadata)other;
				return this._navigationProperty.Equals(entityCollectionInitializerMetadata._navigationProperty);
			}

			// Token: 0x06003514 RID: 13588 RVA: 0x000FA974 File Offset: 0x000F8B74
			internal override Expression Emit(List<TranslatorResult> propertyTranslatorResults)
			{
				Type elementType = this.GetElementType();
				MethodInfo method = InitializerMetadata.EntityCollectionInitializerMetadata.CreateEntityCollectionMethod.MakeGenericMethod(new Type[]
				{
					elementType
				});
				ParameterExpression shaper_Parameter = CodeGenEmitter.Shaper_Parameter;
				Expression expression = propertyTranslatorResults[0].Expression;
				CollectionTranslatorResult collectionTranslatorResult = propertyTranslatorResults[1] as CollectionTranslatorResult;
				Expression expressionToGetCoordinator = collectionTranslatorResult.ExpressionToGetCoordinator;
				return Expression.Call(method, expression, expressionToGetCoordinator, Expression.Constant(this._navigationProperty.RelationshipType.FullName), Expression.Constant(this._navigationProperty.ToEndMember.Name));
			}

			// Token: 0x06003515 RID: 13589 RVA: 0x000FAA20 File Offset: 0x000F8C20
			public static EntityCollection<T> CreateEntityCollection<T>(IEntityWrapper wrappedOwner, Coordinator<T> coordinator, string relationshipName, string targetRoleName) where T : class
			{
				if (wrappedOwner.Entity == null)
				{
					return null;
				}
				EntityCollection<T> result = wrappedOwner.RelationshipManager.GetRelatedCollection<T>(relationshipName, targetRoleName);
				coordinator.RegisterCloseHandler(delegate(Shaper readerState, List<IEntityWrapper> elements)
				{
					result.Load(elements, readerState.MergeOption);
				});
				return result;
			}

			// Token: 0x06003516 RID: 13590 RVA: 0x000FAB78 File Offset: 0x000F8D78
			internal override IEnumerable<Type> GetChildTypes()
			{
				Type elementType = this.GetElementType();
				yield return null;
				yield return typeof(IEnumerable<>).MakeGenericType(new Type[]
				{
					elementType
				});
				yield break;
			}

			// Token: 0x06003517 RID: 13591 RVA: 0x000FAB95 File Offset: 0x000F8D95
			internal override void AppendColumnMapKey(ColumnMapKeyBuilder builder)
			{
				base.AppendColumnMapKey(builder);
				builder.Append(",NP" + this._navigationProperty.Name);
				builder.Append(",AT", this._navigationProperty.DeclaringType);
			}

			// Token: 0x06003518 RID: 13592 RVA: 0x000FABD0 File Offset: 0x000F8DD0
			private Type GetElementType()
			{
				Type type = this.ClrType.TryGetElementType(typeof(ICollection<>));
				if (type == null)
				{
					throw new InvalidOperationException(Strings.ELinq_UnexpectedTypeForNavigationProperty(this._navigationProperty, typeof(EntityCollection<>), typeof(ICollection<>), this.ClrType));
				}
				return type;
			}

			// Token: 0x040013D9 RID: 5081
			private readonly NavigationProperty _navigationProperty;

			// Token: 0x040013DA RID: 5082
			internal static readonly MethodInfo CreateEntityCollectionMethod = typeof(InitializerMetadata.EntityCollectionInitializerMetadata).GetOnlyDeclaredMethod("CreateEntityCollection");
		}
	}
}

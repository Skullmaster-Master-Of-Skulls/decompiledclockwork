using System;
using System.Collections;
using System.Collections.Generic;
using System.Data.Common.Internal.Materialization;
using System.Data.Entity;
using System.Data.Metadata.Edm;
using System.Data.Objects.DataClasses;
using System.Data.Objects.Internal;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Threading;

namespace System.Data.Objects.ELinq
{
	// Token: 0x020001A1 RID: 417
	internal abstract class InitializerMetadata : IEquatable<InitializerMetadata>
	{
		// Token: 0x06001E50 RID: 7760 RVA: 0x00068A40 File Offset: 0x00066C40
		private InitializerMetadata(Type clrType)
		{
			this.ClrType = clrType;
			this.Identity = InitializerMetadata.s_identifierPrefix + Interlocked.Increment(ref InitializerMetadata.s_identifier).ToString(CultureInfo.InvariantCulture);
		}

		// Token: 0x170005E0 RID: 1504
		// (get) Token: 0x06001E51 RID: 7761
		internal abstract InitializerMetadataKind Kind { get; }

		// Token: 0x06001E52 RID: 7762 RVA: 0x00068A81 File Offset: 0x00066C81
		internal static bool TryGetInitializerMetadata(TypeUsage typeUsage, out InitializerMetadata initializerMetadata)
		{
			initializerMetadata = null;
			if (BuiltInTypeKind.RowType == typeUsage.EdmType.BuiltInTypeKind)
			{
				initializerMetadata = ((RowType)typeUsage.EdmType).InitializerMetadata;
			}
			return initializerMetadata != null;
		}

		// Token: 0x06001E53 RID: 7763 RVA: 0x00068AAC File Offset: 0x00066CAC
		internal static InitializerMetadata CreateGroupingInitializer(EdmItemCollection itemCollection, Type resultType)
		{
			return itemCollection.GetCanonicalInitializerMetadata(new InitializerMetadata.GroupingInitializerMetadata(resultType));
		}

		// Token: 0x06001E54 RID: 7764 RVA: 0x00068ABA File Offset: 0x00066CBA
		internal static InitializerMetadata CreateProjectionInitializer(EdmItemCollection itemCollection, MemberInitExpression initExpression, MemberInfo[] members)
		{
			return itemCollection.GetCanonicalInitializerMetadata(new InitializerMetadata.ProjectionInitializerMetadata(initExpression, members));
		}

		// Token: 0x06001E55 RID: 7765 RVA: 0x00068AC9 File Offset: 0x00066CC9
		internal static InitializerMetadata CreateProjectionInitializer(EdmItemCollection itemCollection, NewExpression newExpression)
		{
			return itemCollection.GetCanonicalInitializerMetadata(new InitializerMetadata.ProjectionNewMetadata(newExpression));
		}

		// Token: 0x06001E56 RID: 7766 RVA: 0x00068AD7 File Offset: 0x00066CD7
		internal static InitializerMetadata CreateEmptyProjectionInitializer(EdmItemCollection itemCollection, NewExpression newExpression)
		{
			return itemCollection.GetCanonicalInitializerMetadata(new InitializerMetadata.EmptyProjectionNewMetadata(newExpression));
		}

		// Token: 0x06001E57 RID: 7767 RVA: 0x00068AE5 File Offset: 0x00066CE5
		internal static InitializerMetadata CreateEntityCollectionInitializer(EdmItemCollection itemCollection, Type type, NavigationProperty navigationProperty)
		{
			return itemCollection.GetCanonicalInitializerMetadata(new InitializerMetadata.EntityCollectionInitializerMetadata(type, navigationProperty));
		}

		// Token: 0x06001E58 RID: 7768 RVA: 0x00048AC0 File Offset: 0x00046CC0
		private static T MarkAsUserExpression<T>(T value)
		{
			return value;
		}

		// Token: 0x06001E59 RID: 7769 RVA: 0x00068AF4 File Offset: 0x00066CF4
		internal virtual void AppendColumnMapKey(ColumnMapKeyBuilder builder)
		{
			builder.Append("CLR-", this.ClrType);
		}

		// Token: 0x06001E5A RID: 7770 RVA: 0x00068B07 File Offset: 0x00066D07
		public override bool Equals(object obj)
		{
			return this.Equals(obj as InitializerMetadata);
		}

		// Token: 0x06001E5B RID: 7771 RVA: 0x00068B15 File Offset: 0x00066D15
		public bool Equals(InitializerMetadata other)
		{
			return this == other || (this.Kind == other.Kind && this.ClrType.Equals(other.ClrType) && this.IsStructurallyEquivalent(other));
		}

		// Token: 0x06001E5C RID: 7772 RVA: 0x00068B49 File Offset: 0x00066D49
		public override int GetHashCode()
		{
			return this.ClrType.GetHashCode();
		}

		// Token: 0x06001E5D RID: 7773 RVA: 0x00017938 File Offset: 0x00015B38
		protected virtual bool IsStructurallyEquivalent(InitializerMetadata other)
		{
			return true;
		}

		// Token: 0x06001E5E RID: 7774
		internal abstract Expression Emit(Translator translator, List<TranslatorResult> propertyTranslatorResults);

		// Token: 0x06001E5F RID: 7775
		internal abstract IEnumerable<Type> GetChildTypes();

		// Token: 0x06001E60 RID: 7776 RVA: 0x00068B58 File Offset: 0x00066D58
		protected static List<Expression> GetPropertyReaders(List<TranslatorResult> propertyTranslatorResults)
		{
			return (from s in propertyTranslatorResults
			select s.UnwrappedExpression).ToList<Expression>();
		}

		// Token: 0x04000C18 RID: 3096
		internal readonly Type ClrType;

		// Token: 0x04000C19 RID: 3097
		internal static readonly MethodInfo UserExpressionMarker = typeof(InitializerMetadata).GetMethod("MarkAsUserExpression", BindingFlags.Static | BindingFlags.NonPublic);

		// Token: 0x04000C1A RID: 3098
		private static long s_identifier;

		// Token: 0x04000C1B RID: 3099
		internal readonly string Identity;

		// Token: 0x04000C1C RID: 3100
		private static readonly string s_identifierPrefix = typeof(InitializerMetadata).Name;

		// Token: 0x02000503 RID: 1283
		private class Grouping<K, T> : IGrouping<K, T>, IEnumerable<!1>, IEnumerable
		{
			// Token: 0x06003D8F RID: 15759 RVA: 0x000E66F9 File Offset: 0x000E48F9
			public Grouping(K key, IEnumerable<T> group)
			{
				this._key = key;
				this._group = group;
			}

			// Token: 0x17000B08 RID: 2824
			// (get) Token: 0x06003D90 RID: 15760 RVA: 0x000E670F File Offset: 0x000E490F
			public K Key
			{
				get
				{
					return this._key;
				}
			}

			// Token: 0x17000B09 RID: 2825
			// (get) Token: 0x06003D91 RID: 15761 RVA: 0x000E6717 File Offset: 0x000E4917
			public IEnumerable<T> Group
			{
				get
				{
					return this._group;
				}
			}

			// Token: 0x06003D92 RID: 15762 RVA: 0x000E671F File Offset: 0x000E491F
			IEnumerator<T> IEnumerable<!1>.GetEnumerator()
			{
				if (this._group == null)
				{
					yield break;
				}
				foreach (T t in this._group)
				{
					yield return t;
				}
				IEnumerator<T> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06003D93 RID: 15763 RVA: 0x000E672E File Offset: 0x000E492E
			IEnumerator IEnumerable.GetEnumerator()
			{
				return ((IEnumerable<T>)this).GetEnumerator();
			}

			// Token: 0x04001AF3 RID: 6899
			private readonly K _key;

			// Token: 0x04001AF4 RID: 6900
			private readonly IEnumerable<T> _group;
		}

		// Token: 0x02000504 RID: 1284
		private class GroupingInitializerMetadata : InitializerMetadata
		{
			// Token: 0x06003D94 RID: 15764 RVA: 0x000E6736 File Offset: 0x000E4936
			internal GroupingInitializerMetadata(Type type) : base(type)
			{
			}

			// Token: 0x17000B0A RID: 2826
			// (get) Token: 0x06003D95 RID: 15765 RVA: 0x000173E2 File Offset: 0x000155E2
			internal override InitializerMetadataKind Kind
			{
				get
				{
					return InitializerMetadataKind.Grouping;
				}
			}

			// Token: 0x06003D96 RID: 15766 RVA: 0x000E6740 File Offset: 0x000E4940
			internal override Expression Emit(Translator translator, List<TranslatorResult> propertyTranslatorResults)
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

			// Token: 0x06003D97 RID: 15767 RVA: 0x000E67AE File Offset: 0x000E49AE
			internal override IEnumerable<Type> GetChildTypes()
			{
				Type type = this.ClrType.GetGenericArguments()[0];
				Type groupElementType = this.ClrType.GetGenericArguments()[1];
				yield return type;
				yield return typeof(IEnumerable<>).MakeGenericType(new Type[]
				{
					groupElementType
				});
				yield break;
			}
		}

		// Token: 0x02000505 RID: 1285
		private class ProjectionNewMetadata : InitializerMetadata
		{
			// Token: 0x06003D98 RID: 15768 RVA: 0x000E67BE File Offset: 0x000E49BE
			internal ProjectionNewMetadata(NewExpression newExpression) : base(newExpression.Type)
			{
				this._newExpression = newExpression;
			}

			// Token: 0x17000B0B RID: 2827
			// (get) Token: 0x06003D99 RID: 15769 RVA: 0x00017938 File Offset: 0x00015B38
			internal override InitializerMetadataKind Kind
			{
				get
				{
					return InitializerMetadataKind.ProjectionNew;
				}
			}

			// Token: 0x06003D9A RID: 15770 RVA: 0x000E67D4 File Offset: 0x000E49D4
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

			// Token: 0x06003D9B RID: 15771 RVA: 0x000E6890 File Offset: 0x000E4A90
			internal override Expression Emit(Translator translator, List<TranslatorResult> propertyTranslatorResults)
			{
				Expression expression = Expression.Constant(null, this.ClrType);
				Expression expression2 = Expression.New(this._newExpression.Constructor, InitializerMetadata.GetPropertyReaders(propertyTranslatorResults));
				return Expression.Call(InitializerMetadata.UserExpressionMarker.MakeGenericMethod(new Type[]
				{
					expression2.Type
				}), expression2);
			}

			// Token: 0x06003D9C RID: 15772 RVA: 0x000E68E0 File Offset: 0x000E4AE0
			internal override IEnumerable<Type> GetChildTypes()
			{
				return from arg in this._newExpression.Arguments
				select arg.Type;
			}

			// Token: 0x06003D9D RID: 15773 RVA: 0x000E6914 File Offset: 0x000E4B14
			internal override void AppendColumnMapKey(ColumnMapKeyBuilder builder)
			{
				base.AppendColumnMapKey(builder);
				builder.Append(this._newExpression.Constructor.ToString());
				IEnumerable<MemberInfo> members = this._newExpression.Members;
				foreach (MemberInfo memberInfo in (members ?? Enumerable.Empty<MemberInfo>()))
				{
					builder.Append("DT", memberInfo.DeclaringType);
					builder.Append("." + memberInfo.Name);
				}
			}

			// Token: 0x04001AF5 RID: 6901
			private readonly NewExpression _newExpression;
		}

		// Token: 0x02000506 RID: 1286
		private class EmptyProjectionNewMetadata : InitializerMetadata.ProjectionNewMetadata
		{
			// Token: 0x06003D9E RID: 15774 RVA: 0x000E69B0 File Offset: 0x000E4BB0
			internal EmptyProjectionNewMetadata(NewExpression newExpression) : base(newExpression)
			{
			}

			// Token: 0x06003D9F RID: 15775 RVA: 0x000E69B9 File Offset: 0x000E4BB9
			internal override Expression Emit(Translator translator, List<TranslatorResult> propertyReaders)
			{
				return base.Emit(translator, new List<TranslatorResult>());
			}

			// Token: 0x06003DA0 RID: 15776 RVA: 0x000E69C7 File Offset: 0x000E4BC7
			internal override IEnumerable<Type> GetChildTypes()
			{
				yield return null;
				yield break;
			}
		}

		// Token: 0x02000507 RID: 1287
		private class ProjectionInitializerMetadata : InitializerMetadata
		{
			// Token: 0x06003DA1 RID: 15777 RVA: 0x000E69D0 File Offset: 0x000E4BD0
			internal ProjectionInitializerMetadata(MemberInitExpression initExpression, MemberInfo[] members) : base(initExpression.Type)
			{
				this._initExpression = initExpression;
				this._members = members;
			}

			// Token: 0x17000B0C RID: 2828
			// (get) Token: 0x06003DA2 RID: 15778 RVA: 0x00033532 File Offset: 0x00031732
			internal override InitializerMetadataKind Kind
			{
				get
				{
					return InitializerMetadataKind.ProjectionInitializer;
				}
			}

			// Token: 0x06003DA3 RID: 15779 RVA: 0x000E69EC File Offset: 0x000E4BEC
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

			// Token: 0x06003DA4 RID: 15780 RVA: 0x000E6A7C File Offset: 0x000E4C7C
			internal override Expression Emit(Translator translator, List<TranslatorResult> propertyReaders)
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
				Expression expression = Expression.MemberInit(this._initExpression.NewExpression, array);
				return Expression.Call(InitializerMetadata.UserExpressionMarker.MakeGenericMethod(new Type[]
				{
					expression.Type
				}), expression);
			}

			// Token: 0x06003DA5 RID: 15781 RVA: 0x000E6B48 File Offset: 0x000E4D48
			internal override IEnumerable<Type> GetChildTypes()
			{
				foreach (MemberBinding memberBinding in this._initExpression.Bindings)
				{
					string text;
					Type type;
					TypeSystem.PropertyOrField(memberBinding.Member, out text, out type);
					yield return type;
				}
				IEnumerator<MemberBinding> enumerator = null;
				yield break;
				yield break;
			}

			// Token: 0x06003DA6 RID: 15782 RVA: 0x000E6B58 File Offset: 0x000E4D58
			internal override void AppendColumnMapKey(ColumnMapKeyBuilder builder)
			{
				base.AppendColumnMapKey(builder);
				foreach (MemberBinding memberBinding in this._initExpression.Bindings)
				{
					builder.Append(",", memberBinding.Member.DeclaringType);
					builder.Append("." + memberBinding.Member.Name);
				}
			}

			// Token: 0x04001AF6 RID: 6902
			private readonly MemberInitExpression _initExpression;

			// Token: 0x04001AF7 RID: 6903
			private readonly MemberInfo[] _members;
		}

		// Token: 0x02000508 RID: 1288
		private class EntityCollectionInitializerMetadata : InitializerMetadata
		{
			// Token: 0x06003DA7 RID: 15783 RVA: 0x000E6BDC File Offset: 0x000E4DDC
			internal EntityCollectionInitializerMetadata(Type type, NavigationProperty navigationProperty) : base(type)
			{
				this._navigationProperty = navigationProperty;
			}

			// Token: 0x17000B0D RID: 2829
			// (get) Token: 0x06003DA8 RID: 15784 RVA: 0x0003BF8C File Offset: 0x0003A18C
			internal override InitializerMetadataKind Kind
			{
				get
				{
					return InitializerMetadataKind.EntityCollection;
				}
			}

			// Token: 0x06003DA9 RID: 15785 RVA: 0x000E6BEC File Offset: 0x000E4DEC
			protected override bool IsStructurallyEquivalent(InitializerMetadata other)
			{
				InitializerMetadata.EntityCollectionInitializerMetadata entityCollectionInitializerMetadata = (InitializerMetadata.EntityCollectionInitializerMetadata)other;
				return this._navigationProperty.Equals(entityCollectionInitializerMetadata._navigationProperty);
			}

			// Token: 0x06003DAA RID: 15786 RVA: 0x000E6C14 File Offset: 0x000E4E14
			internal override Expression Emit(Translator translator, List<TranslatorResult> propertyTranslatorResults)
			{
				Type elementType = this.GetElementType();
				MethodInfo method = InitializerMetadata.EntityCollectionInitializerMetadata.s_createEntityCollectionMethod.MakeGenericMethod(new Type[]
				{
					elementType
				});
				Expression shaper_Parameter = Translator.Shaper_Parameter;
				Expression expression = propertyTranslatorResults[0].Expression;
				CollectionTranslatorResult collectionTranslatorResult = propertyTranslatorResults[1] as CollectionTranslatorResult;
				Expression expressionToGetCoordinator = collectionTranslatorResult.ExpressionToGetCoordinator;
				return Expression.Call(method, shaper_Parameter, expression, expressionToGetCoordinator, Expression.Constant(this._navigationProperty.RelationshipType.FullName), Expression.Constant(this._navigationProperty.ToEndMember.Name));
			}

			// Token: 0x06003DAB RID: 15787 RVA: 0x000E6CA0 File Offset: 0x000E4EA0
			public static EntityCollection<T> CreateEntityCollection<T>(Shaper state, IEntityWrapper wrappedOwner, Coordinator<T> coordinator, string relationshipName, string targetRoleName) where T : class
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

			// Token: 0x06003DAC RID: 15788 RVA: 0x000E6CE9 File Offset: 0x000E4EE9
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

			// Token: 0x06003DAD RID: 15789 RVA: 0x000E6CF9 File Offset: 0x000E4EF9
			internal override void AppendColumnMapKey(ColumnMapKeyBuilder builder)
			{
				base.AppendColumnMapKey(builder);
				builder.Append(",NP" + this._navigationProperty.Name);
				builder.Append(",AT", this._navigationProperty.DeclaringType);
			}

			// Token: 0x06003DAE RID: 15790 RVA: 0x000E6D34 File Offset: 0x000E4F34
			private Type GetElementType()
			{
				Type result;
				if (!EntityUtil.TryGetICollectionElementType(this.ClrType, out result))
				{
					throw EntityUtil.InvalidOperation(Strings.ELinq_UnexpectedTypeForNavigationProperty(this._navigationProperty, typeof(EntityCollection<>), typeof(ICollection<>), this.ClrType));
				}
				return result;
			}

			// Token: 0x04001AF8 RID: 6904
			private readonly NavigationProperty _navigationProperty;

			// Token: 0x04001AF9 RID: 6905
			private static readonly MethodInfo s_createEntityCollectionMethod = typeof(InitializerMetadata.EntityCollectionInitializerMetadata).GetMethod("CreateEntityCollection", BindingFlags.Static | BindingFlags.Public);
		}
	}
}

using System;
using System.Collections.Generic;
using System.Data.Entity.Resources;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;
using System.Text;
using System.Threading;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004C7 RID: 1223
	public abstract class EdmType : GlobalItem, INamedDataModelItem
	{
		// Token: 0x06002D13 RID: 11539 RVA: 0x000DAE98 File Offset: 0x000D9098
		internal static IEnumerable<T> SafeTraverseHierarchy<T>(T startFrom) where T : EdmType
		{
			HashSet<T> visitedTypes = new HashSet<T>();
			T thisType = startFrom;
			while (thisType != null && !visitedTypes.Contains(thisType))
			{
				visitedTypes.Add(thisType);
				yield return thisType;
				thisType = (thisType.BaseType as T);
			}
			yield break;
		}

		// Token: 0x06002D14 RID: 11540 RVA: 0x000DAEB5 File Offset: 0x000D90B5
		internal EdmType()
		{
		}

		// Token: 0x06002D15 RID: 11541 RVA: 0x000DAEBD File Offset: 0x000D90BD
		internal EdmType(string name, string namespaceName, DataSpace dataSpace)
		{
			Check.NotNull<string>(name, "name");
			Check.NotNull<string>(namespaceName, "namespaceName");
			EdmType.Initialize(this, name, namespaceName, dataSpace, false, null);
		}

		// Token: 0x17000644 RID: 1604
		// (get) Token: 0x06002D16 RID: 11542 RVA: 0x000DAEE8 File Offset: 0x000D90E8
		// (set) Token: 0x06002D17 RID: 11543 RVA: 0x000DAEF0 File Offset: 0x000D90F0
		internal string CacheIdentity { get; private set; }

		// Token: 0x17000645 RID: 1605
		// (get) Token: 0x06002D18 RID: 11544 RVA: 0x000DAEF9 File Offset: 0x000D90F9
		string INamedDataModelItem.Identity
		{
			get
			{
				return this.Identity;
			}
		}

		// Token: 0x17000646 RID: 1606
		// (get) Token: 0x06002D19 RID: 11545 RVA: 0x000DAF04 File Offset: 0x000D9104
		internal override string Identity
		{
			get
			{
				if (this.CacheIdentity == null)
				{
					StringBuilder stringBuilder = new StringBuilder(50);
					this.BuildIdentity(stringBuilder);
					this.CacheIdentity = stringBuilder.ToString();
				}
				return this.CacheIdentity;
			}
		}

		// Token: 0x17000647 RID: 1607
		// (get) Token: 0x06002D1A RID: 11546 RVA: 0x000DAF3A File Offset: 0x000D913A
		// (set) Token: 0x06002D1B RID: 11547 RVA: 0x000DAF42 File Offset: 0x000D9142
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public virtual string Name
		{
			get
			{
				return this._name;
			}
			internal set
			{
				Util.ThrowIfReadOnly(this);
				this._name = value;
			}
		}

		// Token: 0x17000648 RID: 1608
		// (get) Token: 0x06002D1C RID: 11548 RVA: 0x000DAF51 File Offset: 0x000D9151
		// (set) Token: 0x06002D1D RID: 11549 RVA: 0x000DAF59 File Offset: 0x000D9159
		[MetadataProperty(PrimitiveTypeKind.String, false)]
		public virtual string NamespaceName
		{
			get
			{
				return this._namespace;
			}
			internal set
			{
				Util.ThrowIfReadOnly(this);
				this._namespace = value;
			}
		}

		// Token: 0x17000649 RID: 1609
		// (get) Token: 0x06002D1E RID: 11550 RVA: 0x000DAF68 File Offset: 0x000D9168
		// (set) Token: 0x06002D1F RID: 11551 RVA: 0x000DAF72 File Offset: 0x000D9172
		[MetadataProperty(PrimitiveTypeKind.Boolean, false)]
		public bool Abstract
		{
			get
			{
				return base.GetFlag(MetadataItem.MetadataFlags.IsAbstract);
			}
			internal set
			{
				Util.ThrowIfReadOnly(this);
				base.SetFlag(MetadataItem.MetadataFlags.IsAbstract, value);
			}
		}

		// Token: 0x1700064A RID: 1610
		// (get) Token: 0x06002D20 RID: 11552 RVA: 0x000DAF83 File Offset: 0x000D9183
		// (set) Token: 0x06002D21 RID: 11553 RVA: 0x000DAF8B File Offset: 0x000D918B
		[MetadataProperty(BuiltInTypeKind.EdmType, false)]
		public virtual EdmType BaseType
		{
			get
			{
				return this._baseType;
			}
			internal set
			{
				Util.ThrowIfReadOnly(this);
				this.CheckBaseType(value);
				this._baseType = value;
			}
		}

		// Token: 0x06002D22 RID: 11554 RVA: 0x000DAFA4 File Offset: 0x000D91A4
		private void CheckBaseType(EdmType baseType)
		{
			for (EdmType edmType = baseType; edmType != null; edmType = edmType.BaseType)
			{
				if (edmType == this)
				{
					throw new ArgumentException(Strings.CannotSetBaseTypeCyclicInheritance(baseType.Name, this.Name));
				}
			}
			if (baseType != null && Helper.IsEntityTypeBase(this) && ((EntityTypeBase)baseType).KeyMembers.Count != 0 && ((EntityTypeBase)this).KeyMembers.Count != 0)
			{
				throw new ArgumentException(Strings.CannotDefineKeysOnBothBaseAndDerivedTypes);
			}
		}

		// Token: 0x1700064B RID: 1611
		// (get) Token: 0x06002D23 RID: 11555 RVA: 0x000DB014 File Offset: 0x000D9214
		public virtual string FullName
		{
			get
			{
				return this.Identity;
			}
		}

		// Token: 0x1700064C RID: 1612
		// (get) Token: 0x06002D24 RID: 11556 RVA: 0x000DB01C File Offset: 0x000D921C
		internal virtual Type ClrType
		{
			get
			{
				return null;
			}
		}

		// Token: 0x06002D25 RID: 11557 RVA: 0x000DB01F File Offset: 0x000D921F
		internal override void BuildIdentity(StringBuilder builder)
		{
			if (this.CacheIdentity != null)
			{
				builder.Append(this.CacheIdentity);
				return;
			}
			builder.Append(EdmType.CreateEdmTypeIdentity(this.NamespaceName, this.Name));
		}

		// Token: 0x06002D26 RID: 11558 RVA: 0x000DB050 File Offset: 0x000D9250
		internal static string CreateEdmTypeIdentity(string namespaceName, string name)
		{
			string str = string.Empty;
			if (!string.IsNullOrEmpty(namespaceName))
			{
				str = namespaceName + ".";
			}
			return str + name;
		}

		// Token: 0x06002D27 RID: 11559 RVA: 0x000DB080 File Offset: 0x000D9280
		internal static void Initialize(EdmType type, string name, string namespaceName, DataSpace dataSpace, bool isAbstract, EdmType baseType)
		{
			type._baseType = baseType;
			type._name = name;
			type._namespace = namespaceName;
			type.DataSpace = dataSpace;
			type.Abstract = isAbstract;
		}

		// Token: 0x06002D28 RID: 11560 RVA: 0x000DB0A7 File Offset: 0x000D92A7
		public override string ToString()
		{
			return this.FullName;
		}

		// Token: 0x06002D29 RID: 11561 RVA: 0x000DB0AF File Offset: 0x000D92AF
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public CollectionType GetCollectionType()
		{
			if (this._collectionType == null)
			{
				Interlocked.CompareExchange<CollectionType>(ref this._collectionType, new CollectionType(this), null);
			}
			return this._collectionType;
		}

		// Token: 0x06002D2A RID: 11562 RVA: 0x000DB0D2 File Offset: 0x000D92D2
		internal virtual bool IsSubtypeOf(EdmType otherType)
		{
			return Helper.IsSubtypeOf(this, otherType);
		}

		// Token: 0x06002D2B RID: 11563 RVA: 0x000DB0DB File Offset: 0x000D92DB
		internal virtual bool IsBaseTypeOf(EdmType otherType)
		{
			return otherType != null && otherType.IsSubtypeOf(this);
		}

		// Token: 0x06002D2C RID: 11564 RVA: 0x000DB0E9 File Offset: 0x000D92E9
		internal virtual bool IsAssignableFrom(EdmType otherType)
		{
			return Helper.IsAssignableFrom(this, otherType);
		}

		// Token: 0x06002D2D RID: 11565 RVA: 0x000DB0F4 File Offset: 0x000D92F4
		internal override void SetReadOnly()
		{
			if (!base.IsReadOnly)
			{
				base.SetReadOnly();
				EdmType baseType = this.BaseType;
				if (baseType != null)
				{
					baseType.SetReadOnly();
				}
			}
		}

		// Token: 0x06002D2E RID: 11566 RVA: 0x000DB11F File Offset: 0x000D931F
		internal virtual IEnumerable<FacetDescription> GetAssociatedFacetDescriptions()
		{
			return MetadataItem.GetGeneralFacetDescriptions();
		}

		// Token: 0x04001090 RID: 4240
		private CollectionType _collectionType;

		// Token: 0x04001091 RID: 4241
		private string _name;

		// Token: 0x04001092 RID: 4242
		private string _namespace;

		// Token: 0x04001093 RID: 4243
		private EdmType _baseType;
	}
}

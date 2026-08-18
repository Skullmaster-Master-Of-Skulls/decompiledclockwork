using System;
using System.Collections.Generic;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Core.Metadata.Edm;
using System.Data.Entity.Core.Objects;
using System.Diagnostics.CodeAnalysis;
using System.Reflection;

namespace System.Data.Entity.Internal.MockingProxies
{
	// Token: 0x020006C8 RID: 1736
	internal class ObjectContextProxy : IDisposable
	{
		// Token: 0x060044E1 RID: 17633 RVA: 0x00145054 File Offset: 0x00143254
		protected ObjectContextProxy()
		{
		}

		// Token: 0x060044E2 RID: 17634 RVA: 0x0014505C File Offset: 0x0014325C
		public ObjectContextProxy(ObjectContext objectContext)
		{
			this._objectContext = objectContext;
		}

		// Token: 0x060044E3 RID: 17635 RVA: 0x0014506B File Offset: 0x0014326B
		public static implicit operator ObjectContext(ObjectContextProxy proxy)
		{
			if (proxy != null)
			{
				return proxy._objectContext;
			}
			return null;
		}

		// Token: 0x17000A6B RID: 2667
		// (get) Token: 0x060044E4 RID: 17636 RVA: 0x00145078 File Offset: 0x00143278
		public virtual EntityConnectionProxy Connection
		{
			get
			{
				return new EntityConnectionProxy((EntityConnection)this._objectContext.Connection);
			}
		}

		// Token: 0x17000A6C RID: 2668
		// (get) Token: 0x060044E5 RID: 17637 RVA: 0x0014508F File Offset: 0x0014328F
		// (set) Token: 0x060044E6 RID: 17638 RVA: 0x0014509C File Offset: 0x0014329C
		public virtual string DefaultContainerName
		{
			get
			{
				return this._objectContext.DefaultContainerName;
			}
			set
			{
				this._objectContext.DefaultContainerName = value;
			}
		}

		// Token: 0x060044E7 RID: 17639 RVA: 0x001450AA File Offset: 0x001432AA
		public virtual void Dispose()
		{
			this._objectContext.Dispose();
		}

		// Token: 0x060044E8 RID: 17640 RVA: 0x001450B8 File Offset: 0x001432B8
		public virtual IEnumerable<GlobalItem> GetObjectItemCollection()
		{
			return this._objectItemCollection = (ObjectItemCollection)this._objectContext.MetadataWorkspace.GetItemCollection(DataSpace.OSpace);
		}

		// Token: 0x060044E9 RID: 17641 RVA: 0x001450E4 File Offset: 0x001432E4
		public virtual Type GetClrType(StructuralType item)
		{
			return this._objectItemCollection.GetClrType(item);
		}

		// Token: 0x060044EA RID: 17642 RVA: 0x001450F2 File Offset: 0x001432F2
		public virtual Type GetClrType(EnumType item)
		{
			return this._objectItemCollection.GetClrType(item);
		}

		// Token: 0x060044EB RID: 17643 RVA: 0x00145100 File Offset: 0x00143300
		public virtual void LoadFromAssembly(Assembly assembly)
		{
			this._objectContext.MetadataWorkspace.LoadFromAssembly(assembly);
		}

		// Token: 0x060044EC RID: 17644 RVA: 0x00145113 File Offset: 0x00143313
		[SuppressMessage("Microsoft.Reliability", "CA2000:Dispose objects before losing scope")]
		public virtual ObjectContextProxy CreateNew(EntityConnectionProxy entityConnection)
		{
			return new ObjectContextProxy(new ObjectContext(entityConnection));
		}

		// Token: 0x060044ED RID: 17645 RVA: 0x00145128 File Offset: 0x00143328
		public virtual void CopyContextOptions(ObjectContextProxy source)
		{
			this._objectContext.ContextOptions.LazyLoadingEnabled = source._objectContext.ContextOptions.LazyLoadingEnabled;
			this._objectContext.ContextOptions.ProxyCreationEnabled = source._objectContext.ContextOptions.ProxyCreationEnabled;
			this._objectContext.ContextOptions.UseCSharpNullComparisonBehavior = source._objectContext.ContextOptions.UseCSharpNullComparisonBehavior;
			this._objectContext.ContextOptions.UseConsistentNullReferenceBehavior = source._objectContext.ContextOptions.UseConsistentNullReferenceBehavior;
			this._objectContext.ContextOptions.UseLegacyPreserveChangesBehavior = source._objectContext.ContextOptions.UseLegacyPreserveChangesBehavior;
			this._objectContext.CommandTimeout = source._objectContext.CommandTimeout;
			this._objectContext.InterceptionContext = source._objectContext.InterceptionContext.WithObjectContext(this._objectContext);
		}

		// Token: 0x04001961 RID: 6497
		private readonly ObjectContext _objectContext;

		// Token: 0x04001962 RID: 6498
		private ObjectItemCollection _objectItemCollection;
	}
}

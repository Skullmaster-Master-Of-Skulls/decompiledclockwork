using System;
using System.ComponentModel;
using System.Data.Entity.Internal;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Infrastructure
{
	// Token: 0x0200074B RID: 1867
	public class DbContextConfiguration
	{
		// Token: 0x0600546F RID: 21615 RVA: 0x00171399 File Offset: 0x0016F599
		internal DbContextConfiguration(InternalContext internalContext)
		{
			this._internalContext = internalContext;
		}

		// Token: 0x06005470 RID: 21616 RVA: 0x001713A8 File Offset: 0x0016F5A8
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06005471 RID: 21617 RVA: 0x001713B0 File Offset: 0x0016F5B0
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06005472 RID: 21618 RVA: 0x001713B9 File Offset: 0x0016F5B9
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06005473 RID: 21619 RVA: 0x001713C1 File Offset: 0x0016F5C1
		[EditorBrowsable(EditorBrowsableState.Never)]
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x17000E5F RID: 3679
		// (get) Token: 0x06005474 RID: 21620 RVA: 0x001713C9 File Offset: 0x0016F5C9
		// (set) Token: 0x06005475 RID: 21621 RVA: 0x001713D6 File Offset: 0x0016F5D6
		public bool EnsureTransactionsForFunctionsAndCommands
		{
			get
			{
				return this._internalContext.EnsureTransactionsForFunctionsAndCommands;
			}
			set
			{
				this._internalContext.EnsureTransactionsForFunctionsAndCommands = value;
			}
		}

		// Token: 0x17000E60 RID: 3680
		// (get) Token: 0x06005476 RID: 21622 RVA: 0x001713E4 File Offset: 0x0016F5E4
		// (set) Token: 0x06005477 RID: 21623 RVA: 0x001713F1 File Offset: 0x0016F5F1
		public bool LazyLoadingEnabled
		{
			get
			{
				return this._internalContext.LazyLoadingEnabled;
			}
			set
			{
				this._internalContext.LazyLoadingEnabled = value;
			}
		}

		// Token: 0x17000E61 RID: 3681
		// (get) Token: 0x06005478 RID: 21624 RVA: 0x001713FF File Offset: 0x0016F5FF
		// (set) Token: 0x06005479 RID: 21625 RVA: 0x0017140C File Offset: 0x0016F60C
		public bool ProxyCreationEnabled
		{
			get
			{
				return this._internalContext.ProxyCreationEnabled;
			}
			set
			{
				this._internalContext.ProxyCreationEnabled = value;
			}
		}

		// Token: 0x17000E62 RID: 3682
		// (get) Token: 0x0600547A RID: 21626 RVA: 0x0017141A File Offset: 0x0016F61A
		// (set) Token: 0x0600547B RID: 21627 RVA: 0x00171427 File Offset: 0x0016F627
		public bool UseDatabaseNullSemantics
		{
			get
			{
				return this._internalContext.UseDatabaseNullSemantics;
			}
			set
			{
				this._internalContext.UseDatabaseNullSemantics = value;
			}
		}

		// Token: 0x17000E63 RID: 3683
		// (get) Token: 0x0600547C RID: 21628 RVA: 0x00171435 File Offset: 0x0016F635
		// (set) Token: 0x0600547D RID: 21629 RVA: 0x00171442 File Offset: 0x0016F642
		public bool AutoDetectChangesEnabled
		{
			get
			{
				return this._internalContext.AutoDetectChangesEnabled;
			}
			set
			{
				this._internalContext.AutoDetectChangesEnabled = value;
			}
		}

		// Token: 0x17000E64 RID: 3684
		// (get) Token: 0x0600547E RID: 21630 RVA: 0x00171450 File Offset: 0x0016F650
		// (set) Token: 0x0600547F RID: 21631 RVA: 0x0017145D File Offset: 0x0016F65D
		public bool ValidateOnSaveEnabled
		{
			get
			{
				return this._internalContext.ValidateOnSaveEnabled;
			}
			set
			{
				this._internalContext.ValidateOnSaveEnabled = value;
			}
		}

		// Token: 0x04002282 RID: 8834
		private readonly InternalContext _internalContext;
	}
}

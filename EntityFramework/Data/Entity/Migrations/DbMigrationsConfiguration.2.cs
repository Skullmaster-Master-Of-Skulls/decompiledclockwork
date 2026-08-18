using System;
using System.ComponentModel;
using System.Data.Entity.Infrastructure.DependencyResolution;
using System.Data.Entity.Utilities;
using System.Diagnostics.CodeAnalysis;

namespace System.Data.Entity.Migrations
{
	// Token: 0x020006CE RID: 1742
	public class DbMigrationsConfiguration<TContext> : DbMigrationsConfiguration where TContext : DbContext
	{
		// Token: 0x06004570 RID: 17776 RVA: 0x00146DAF File Offset: 0x00144FAF
		static DbMigrationsConfiguration()
		{
			DbConfigurationManager.Instance.EnsureLoadedForContext(typeof(TContext));
		}

		// Token: 0x06004571 RID: 17777 RVA: 0x00146DC5 File Offset: 0x00144FC5
		public DbMigrationsConfiguration()
		{
			base.ContextType = typeof(TContext);
			base.MigrationsAssembly = this.GetType().Assembly();
			base.MigrationsNamespace = this.GetType().Namespace;
		}

		// Token: 0x06004572 RID: 17778 RVA: 0x00146DFF File Offset: 0x00144FFF
		protected virtual void Seed(TContext context)
		{
			Check.NotNull<TContext>(context, "context");
		}

		// Token: 0x06004573 RID: 17779 RVA: 0x00146E0D File Offset: 0x0014500D
		internal override void OnSeed(DbContext context)
		{
			this.Seed((TContext)((object)context));
		}

		// Token: 0x06004574 RID: 17780 RVA: 0x00146E1B File Offset: 0x0014501B
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override string ToString()
		{
			return base.ToString();
		}

		// Token: 0x06004575 RID: 17781 RVA: 0x00146E23 File Offset: 0x00145023
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override bool Equals(object obj)
		{
			return base.Equals(obj);
		}

		// Token: 0x06004576 RID: 17782 RVA: 0x00146E2C File Offset: 0x0014502C
		[EditorBrowsable(EditorBrowsableState.Never)]
		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		// Token: 0x06004577 RID: 17783 RVA: 0x00146E34 File Offset: 0x00145034
		[SuppressMessage("Microsoft.Design", "CA1024:UsePropertiesWhereAppropriate")]
		[EditorBrowsable(EditorBrowsableState.Never)]
		public new Type GetType()
		{
			return base.GetType();
		}

		// Token: 0x06004578 RID: 17784 RVA: 0x00146E3C File Offset: 0x0014503C
		[EditorBrowsable(EditorBrowsableState.Never)]
		protected new object MemberwiseClone()
		{
			return base.MemberwiseClone();
		}
	}
}

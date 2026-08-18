using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Data.Entity.Core.Common.CommandTrees;
using System.Data.Entity.Core.EntityClient;
using System.Data.Entity.Infrastructure.Interception;
using System.Data.Entity.Utilities;

namespace System.Data.Entity.Internal
{
	// Token: 0x02000293 RID: 659
	internal sealed class CommandTracer : ICancelableDbCommandInterceptor, IDbCommandTreeInterceptor, ICancelableEntityConnectionInterceptor, IDbInterceptor, IDisposable
	{
		// Token: 0x0600170E RID: 5902 RVA: 0x00072CBE File Offset: 0x00070EBE
		public CommandTracer(DbContext context) : this(context, DbInterception.Dispatch)
		{
		}

		// Token: 0x0600170F RID: 5903 RVA: 0x00072CCC File Offset: 0x00070ECC
		internal CommandTracer(DbContext context, DbDispatchers dispatchers)
		{
			this._context = context;
			this._dispatchers = dispatchers;
			this._dispatchers.AddInterceptor(this);
		}

		// Token: 0x1700029C RID: 668
		// (get) Token: 0x06001710 RID: 5904 RVA: 0x00072D04 File Offset: 0x00070F04
		public IEnumerable<DbCommand> DbCommands
		{
			get
			{
				return this._commands;
			}
		}

		// Token: 0x1700029D RID: 669
		// (get) Token: 0x06001711 RID: 5905 RVA: 0x00072D0C File Offset: 0x00070F0C
		public IEnumerable<DbCommandTree> CommandTrees
		{
			get
			{
				return this._commandTrees;
			}
		}

		// Token: 0x06001712 RID: 5906 RVA: 0x00072D14 File Offset: 0x00070F14
		public bool CommandExecuting(DbCommand command, DbInterceptionContext interceptionContext)
		{
			if (interceptionContext.DbContexts.Contains(this._context, new Func<DbContext, DbContext, bool>(object.ReferenceEquals)))
			{
				this._commands.Add(command);
				return false;
			}
			return true;
		}

		// Token: 0x06001713 RID: 5907 RVA: 0x00072D44 File Offset: 0x00070F44
		public void TreeCreated(DbCommandTreeInterceptionContext interceptionContext)
		{
			if (interceptionContext.DbContexts.Contains(this._context, new Func<DbContext, DbContext, bool>(object.ReferenceEquals)))
			{
				this._commandTrees.Add(interceptionContext.Result);
			}
		}

		// Token: 0x06001714 RID: 5908 RVA: 0x00072D76 File Offset: 0x00070F76
		public bool ConnectionOpening(EntityConnection connection, DbInterceptionContext interceptionContext)
		{
			return !interceptionContext.DbContexts.Contains(this._context, new Func<DbContext, DbContext, bool>(object.ReferenceEquals));
		}

		// Token: 0x06001715 RID: 5909 RVA: 0x00072D98 File Offset: 0x00070F98
		void IDisposable.Dispose()
		{
			this._dispatchers.RemoveInterceptor(this);
		}

		// Token: 0x04000851 RID: 2129
		private readonly List<DbCommand> _commands = new List<DbCommand>();

		// Token: 0x04000852 RID: 2130
		private readonly List<DbCommandTree> _commandTrees = new List<DbCommandTree>();

		// Token: 0x04000853 RID: 2131
		private readonly DbContext _context;

		// Token: 0x04000854 RID: 2132
		private readonly DbDispatchers _dispatchers;
	}
}

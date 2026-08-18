using System;
using System.Data.SqlClient;
using System.Data.SqlTypes;
using System.Security.Principal;

namespace Microsoft.SqlServer.Server
{
	// Token: 0x0200004E RID: 78
	public sealed class SqlContext
	{
		// Token: 0x06000314 RID: 788 RVA: 0x0003CB20 File Offset: 0x0003BF20
		private SqlContext(SmiContext smiContext)
		{
			this._smiContext = smiContext;
			this._smiContext.OutOfScope += this.OnOutOfScope;
		}

		// Token: 0x17000052 RID: 82
		// (get) Token: 0x06000315 RID: 789 RVA: 0x0003CB54 File Offset: 0x0003BF54
		public static bool IsAvailable
		{
			get
			{
				return InOutOfProcHelper.InProc;
			}
		}

		// Token: 0x17000053 RID: 83
		// (get) Token: 0x06000316 RID: 790 RVA: 0x0003CB68 File Offset: 0x0003BF68
		public static SqlPipe Pipe
		{
			get
			{
				return SqlContext.CurrentContext.InstancePipe;
			}
		}

		// Token: 0x17000054 RID: 84
		// (get) Token: 0x06000317 RID: 791 RVA: 0x0003CB80 File Offset: 0x0003BF80
		public static SqlTriggerContext TriggerContext
		{
			get
			{
				return SqlContext.CurrentContext.InstanceTriggerContext;
			}
		}

		// Token: 0x17000055 RID: 85
		// (get) Token: 0x06000318 RID: 792 RVA: 0x0003CB98 File Offset: 0x0003BF98
		public static WindowsIdentity WindowsIdentity
		{
			get
			{
				return SqlContext.CurrentContext.InstanceWindowsIdentity;
			}
		}

		// Token: 0x17000056 RID: 86
		// (get) Token: 0x06000319 RID: 793 RVA: 0x0003CBB0 File Offset: 0x0003BFB0
		private static SqlContext CurrentContext
		{
			get
			{
				SmiContext currentContext = SmiContextFactory.Instance.GetCurrentContext();
				SqlContext sqlContext = (SqlContext)currentContext.GetContextValue(1);
				if (sqlContext == null)
				{
					sqlContext = new SqlContext(currentContext);
					currentContext.SetContextValue(1, sqlContext);
				}
				return sqlContext;
			}
		}

		// Token: 0x17000057 RID: 87
		// (get) Token: 0x0600031A RID: 794 RVA: 0x0003CBE8 File Offset: 0x0003BFE8
		private SqlPipe InstancePipe
		{
			get
			{
				if (this._pipe == null && this._smiContext.HasContextPipe)
				{
					this._pipe = new SqlPipe(this._smiContext);
				}
				return this._pipe;
			}
		}

		// Token: 0x17000058 RID: 88
		// (get) Token: 0x0600031B RID: 795 RVA: 0x0003CC24 File Offset: 0x0003C024
		private SqlTriggerContext InstanceTriggerContext
		{
			get
			{
				if (this._triggerContext == null)
				{
					SmiEventSink_Default smiEventSink_Default = new SmiEventSink_Default();
					bool[] columnsUpdated;
					TriggerAction triggerAction;
					SqlXml eventInstanceData;
					this._smiContext.GetTriggerInfo(smiEventSink_Default, out columnsUpdated, out triggerAction, out eventInstanceData);
					smiEventSink_Default.ProcessMessagesAndThrow();
					if (triggerAction != TriggerAction.Invalid)
					{
						this._triggerContext = new SqlTriggerContext(triggerAction, columnsUpdated, eventInstanceData);
					}
				}
				return this._triggerContext;
			}
		}

		// Token: 0x17000059 RID: 89
		// (get) Token: 0x0600031C RID: 796 RVA: 0x0003CC70 File Offset: 0x0003C070
		private WindowsIdentity InstanceWindowsIdentity
		{
			get
			{
				return this._smiContext.WindowsIdentity;
			}
		}

		// Token: 0x0600031D RID: 797 RVA: 0x0003CC88 File Offset: 0x0003C088
		private void OnOutOfScope(object s, EventArgs e)
		{
			if (Bid.AdvancedOn)
			{
				Bid.Trace("<sc.SqlContext.OutOfScope|ADV> SqlContext is out of scope\n");
			}
			if (this._pipe != null)
			{
				this._pipe.OnOutOfScope();
			}
			this._triggerContext = null;
		}

		// Token: 0x0400017C RID: 380
		private SmiContext _smiContext;

		// Token: 0x0400017D RID: 381
		private SqlPipe _pipe;

		// Token: 0x0400017E RID: 382
		private SqlTriggerContext _triggerContext;
	}
}

using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x020002ED RID: 749
	public class CallbackCommand : ICallbackCommandContext
	{
		// Token: 0x060019DB RID: 6619 RVA: 0x00054CEE File Offset: 0x00052EEE
		public CallbackCommand()
		{
			this.Tasks = new List<ITask>();
			this.Dependencies = new List<IDependency>();
		}

		// Token: 0x060019DC RID: 6620 RVA: 0x00054D0C File Offset: 0x00052F0C
		public CallbackCommand(CommandType command) : this()
		{
			this.Command = command;
		}

		// Token: 0x060019DD RID: 6621 RVA: 0x00054D1C File Offset: 0x00052F1C
		public static CallbackCommand FromEventArgument(string eventArgument, ITaskFactory taskFactory)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new CallbackCommandConverter(taskFactory)
			});
			return javaScriptSerializer.Deserialize<CallbackCommand>(eventArgument);
		}

		// Token: 0x170008AD RID: 2221
		// (get) Token: 0x060019DE RID: 6622 RVA: 0x00054D4D File Offset: 0x00052F4D
		// (set) Token: 0x060019DF RID: 6623 RVA: 0x00054D55 File Offset: 0x00052F55
		public CommandType Command { get; set; }

		// Token: 0x170008AE RID: 2222
		// (get) Token: 0x060019E0 RID: 6624 RVA: 0x00054D5E File Offset: 0x00052F5E
		// (set) Token: 0x060019E1 RID: 6625 RVA: 0x00054D66 File Offset: 0x00052F66
		public List<ITask> Tasks { get; set; }

		// Token: 0x170008AF RID: 2223
		// (get) Token: 0x060019E2 RID: 6626 RVA: 0x00054D6F File Offset: 0x00052F6F
		// (set) Token: 0x060019E3 RID: 6627 RVA: 0x00054D77 File Offset: 0x00052F77
		public List<IDependency> Dependencies { get; set; }

		// Token: 0x170008B0 RID: 2224
		// (get) Token: 0x060019E4 RID: 6628 RVA: 0x00054D80 File Offset: 0x00052F80
		public List<IAssignment> Assignments
		{
			get
			{
				if (this._assignments == null)
				{
					this._assignments = new List<IAssignment>();
				}
				return this._assignments;
			}
		}

		// Token: 0x040006AC RID: 1708
		private List<IAssignment> _assignments;
	}
}

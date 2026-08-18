using System;
using System.Collections.Generic;
using System.Web.Script.Serialization;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x02000344 RID: 836
	public class PostbackCommand : IPostbackCommandContext
	{
		// Token: 0x06001C76 RID: 7286 RVA: 0x0005A3FC File Offset: 0x000585FC
		public PostbackCommand()
		{
			this.InsertedTasks = new List<ITask>();
			this.UpdatedTasks = new List<ITask>();
			this.DeletedTasks = new List<ITask>();
			this.InsertedDependencies = new List<IDependency>();
			this.DeletedDependencies = new List<IDependency>();
		}

		// Token: 0x06001C77 RID: 7287 RVA: 0x0005A43B File Offset: 0x0005863B
		public PostbackCommand(CommandType command) : this()
		{
			this.Command = command;
		}

		// Token: 0x06001C78 RID: 7288 RVA: 0x0005A44C File Offset: 0x0005864C
		public static PostbackCommand FromEventArgument(string eventArgument, ITaskFactory taskFactory)
		{
			JavaScriptSerializer javaScriptSerializer = new JavaScriptSerializer();
			javaScriptSerializer.RegisterConverters(new JavaScriptConverter[]
			{
				new PostbackCommandConverter(taskFactory)
			});
			return javaScriptSerializer.Deserialize<PostbackCommand>(eventArgument);
		}

		// Token: 0x170009A5 RID: 2469
		// (get) Token: 0x06001C79 RID: 7289 RVA: 0x0005A47D File Offset: 0x0005867D
		// (set) Token: 0x06001C7A RID: 7290 RVA: 0x0005A485 File Offset: 0x00058685
		public CommandType Command { get; set; }

		// Token: 0x170009A6 RID: 2470
		// (get) Token: 0x06001C7B RID: 7291 RVA: 0x0005A48E File Offset: 0x0005868E
		// (set) Token: 0x06001C7C RID: 7292 RVA: 0x0005A496 File Offset: 0x00058696
		public List<ITask> InsertedTasks { get; set; }

		// Token: 0x170009A7 RID: 2471
		// (get) Token: 0x06001C7D RID: 7293 RVA: 0x0005A49F File Offset: 0x0005869F
		// (set) Token: 0x06001C7E RID: 7294 RVA: 0x0005A4A7 File Offset: 0x000586A7
		public List<ITask> UpdatedTasks { get; set; }

		// Token: 0x170009A8 RID: 2472
		// (get) Token: 0x06001C7F RID: 7295 RVA: 0x0005A4B0 File Offset: 0x000586B0
		// (set) Token: 0x06001C80 RID: 7296 RVA: 0x0005A4B8 File Offset: 0x000586B8
		public List<ITask> DeletedTasks { get; set; }

		// Token: 0x170009A9 RID: 2473
		// (get) Token: 0x06001C81 RID: 7297 RVA: 0x0005A4C1 File Offset: 0x000586C1
		// (set) Token: 0x06001C82 RID: 7298 RVA: 0x0005A4C9 File Offset: 0x000586C9
		public List<IDependency> InsertedDependencies { get; set; }

		// Token: 0x170009AA RID: 2474
		// (get) Token: 0x06001C83 RID: 7299 RVA: 0x0005A4D2 File Offset: 0x000586D2
		// (set) Token: 0x06001C84 RID: 7300 RVA: 0x0005A4DA File Offset: 0x000586DA
		public List<IDependency> DeletedDependencies { get; set; }

		// Token: 0x170009AB RID: 2475
		// (get) Token: 0x06001C85 RID: 7301 RVA: 0x0005A4E3 File Offset: 0x000586E3
		// (set) Token: 0x06001C86 RID: 7302 RVA: 0x0005A4FE File Offset: 0x000586FE
		public List<IAssignment> InsertedAssignments
		{
			get
			{
				if (this._insertedAssignments == null)
				{
					this._insertedAssignments = new List<IAssignment>();
				}
				return this._insertedAssignments;
			}
			set
			{
				this._insertedAssignments = value;
			}
		}

		// Token: 0x170009AC RID: 2476
		// (get) Token: 0x06001C87 RID: 7303 RVA: 0x0005A507 File Offset: 0x00058707
		// (set) Token: 0x06001C88 RID: 7304 RVA: 0x0005A522 File Offset: 0x00058722
		public List<IAssignment> UpdatedAssignments
		{
			get
			{
				if (this._updatedAssignments == null)
				{
					this._updatedAssignments = new List<IAssignment>();
				}
				return this._updatedAssignments;
			}
			set
			{
				this._updatedAssignments = value;
			}
		}

		// Token: 0x170009AD RID: 2477
		// (get) Token: 0x06001C89 RID: 7305 RVA: 0x0005A52B File Offset: 0x0005872B
		// (set) Token: 0x06001C8A RID: 7306 RVA: 0x0005A546 File Offset: 0x00058746
		public List<IAssignment> DeletedAssignments
		{
			get
			{
				if (this._deletedAssignments == null)
				{
					this._deletedAssignments = new List<IAssignment>();
				}
				return this._deletedAssignments;
			}
			set
			{
				this._deletedAssignments = null;
			}
		}

		// Token: 0x04000739 RID: 1849
		private List<IAssignment> _insertedAssignments;

		// Token: 0x0400073A RID: 1850
		private List<IAssignment> _updatedAssignments;

		// Token: 0x0400073B RID: 1851
		private List<IAssignment> _deletedAssignments;
	}
}

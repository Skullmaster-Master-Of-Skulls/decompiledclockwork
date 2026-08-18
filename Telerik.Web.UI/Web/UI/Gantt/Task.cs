using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Web.Script.Serialization;
using System.Web.UI;
using Telerik.Web.UI.Scheduling;

namespace Telerik.Web.UI.Gantt
{
	// Token: 0x020004AA RID: 1194
	[Serializable]
	public class Task : StateManager, ITask, ITaskBase, IMarkableStateManager, IStateManager
	{
		// Token: 0x06002A6F RID: 10863 RVA: 0x00089502 File Offset: 0x00087702
		public Task()
		{
			this._dependencies = new DependencyCollection();
			this._sucessors = new DependencyCollection();
			this._predecessors = new DependencyCollection();
			this._tasks = new TaskCollection();
		}

		// Token: 0x17000DA5 RID: 3493
		// (get) Token: 0x06002A70 RID: 10864 RVA: 0x00089536 File Offset: 0x00087736
		// (set) Token: 0x06002A71 RID: 10865 RVA: 0x00089548 File Offset: 0x00087748
		public object ID
		{
			get
			{
				return base.ViewState["ID"];
			}
			set
			{
				base.ViewState["ID"] = value;
			}
		}

		// Token: 0x17000DA6 RID: 3494
		// (get) Token: 0x06002A72 RID: 10866 RVA: 0x0008955B File Offset: 0x0008775B
		// (set) Token: 0x06002A73 RID: 10867 RVA: 0x0008956D File Offset: 0x0008776D
		public object ParentID
		{
			get
			{
				return base.ViewState["ParentID"];
			}
			set
			{
				base.ViewState["ParentID"] = value;
			}
		}

		// Token: 0x17000DA7 RID: 3495
		// (get) Token: 0x06002A74 RID: 10868 RVA: 0x00089580 File Offset: 0x00087780
		// (set) Token: 0x06002A75 RID: 10869 RVA: 0x00089592 File Offset: 0x00087792
		public object OrderID
		{
			get
			{
				return base.ViewState["OrderID"];
			}
			set
			{
				base.ViewState["OrderID"] = value;
			}
		}

		// Token: 0x17000DA8 RID: 3496
		// (get) Token: 0x06002A76 RID: 10870 RVA: 0x000895A5 File Offset: 0x000877A5
		// (set) Token: 0x06002A77 RID: 10871 RVA: 0x000895CA File Offset: 0x000877CA
		public DateTime Start
		{
			get
			{
				return (DateTime)(base.ViewState["Start"] ?? DateTime.MinValue);
			}
			set
			{
				base.ViewState["Start"] = DateHelper.AssumeUtc(value);
			}
		}

		// Token: 0x17000DA9 RID: 3497
		// (get) Token: 0x06002A78 RID: 10872 RVA: 0x000895E7 File Offset: 0x000877E7
		// (set) Token: 0x06002A79 RID: 10873 RVA: 0x000895FE File Offset: 0x000877FE
		public DateTime? PlannedStart
		{
			get
			{
				return (DateTime?)base.ViewState["PlannedStart"];
			}
			set
			{
				base.ViewState["PlannedStart"] = ((value == null) ? value : new DateTime?(DateHelper.AssumeUtc(value.Value)));
			}
		}

		// Token: 0x17000DAA RID: 3498
		// (get) Token: 0x06002A7A RID: 10874 RVA: 0x00089632 File Offset: 0x00087832
		// (set) Token: 0x06002A7B RID: 10875 RVA: 0x00089657 File Offset: 0x00087857
		public DateTime End
		{
			get
			{
				return (DateTime)(base.ViewState["End"] ?? DateTime.MaxValue);
			}
			set
			{
				base.ViewState["End"] = DateHelper.AssumeUtc(value);
			}
		}

		// Token: 0x17000DAB RID: 3499
		// (get) Token: 0x06002A7C RID: 10876 RVA: 0x00089674 File Offset: 0x00087874
		// (set) Token: 0x06002A7D RID: 10877 RVA: 0x0008968B File Offset: 0x0008788B
		public DateTime? PlannedEnd
		{
			get
			{
				return (DateTime?)base.ViewState["PlannedEnd"];
			}
			set
			{
				base.ViewState["PlannedEnd"] = ((value == null) ? value : new DateTime?(DateHelper.AssumeUtc(value.Value)));
			}
		}

		// Token: 0x17000DAC RID: 3500
		// (get) Token: 0x06002A7E RID: 10878 RVA: 0x000896BF File Offset: 0x000878BF
		[Browsable(false)]
		[ScriptIgnore]
		public TimeSpan Duration
		{
			get
			{
				return this.End - this.Start;
			}
		}

		// Token: 0x17000DAD RID: 3501
		// (get) Token: 0x06002A7F RID: 10879 RVA: 0x000896D2 File Offset: 0x000878D2
		// (set) Token: 0x06002A80 RID: 10880 RVA: 0x000896F3 File Offset: 0x000878F3
		[Description("Value that determines whether the task is a summary.")]
		[Category("Data")]
		[DefaultValue(false)]
		public bool Summary
		{
			get
			{
				return (bool)(base.ViewState["Summary"] ?? false);
			}
			set
			{
				base.ViewState["Summary"] = value;
			}
		}

		// Token: 0x17000DAE RID: 3502
		// (get) Token: 0x06002A81 RID: 10881 RVA: 0x0008970B File Offset: 0x0008790B
		// (set) Token: 0x06002A82 RID: 10882 RVA: 0x0008972C File Offset: 0x0008792C
		[Category("Behavior")]
		[DefaultValue(false)]
		[Description("Value that determines whether the tasks is expanded in the TreeLsit.")]
		public bool Expanded
		{
			get
			{
				return (bool)(base.ViewState["Expanded"] ?? true);
			}
			set
			{
				base.ViewState["Expanded"] = value;
			}
		}

		// Token: 0x17000DAF RID: 3503
		// (get) Token: 0x06002A83 RID: 10883 RVA: 0x00089744 File Offset: 0x00087944
		// (set) Token: 0x06002A84 RID: 10884 RVA: 0x0008976A File Offset: 0x0008796A
		[Description("Value that determines the percent of completion of the task.")]
		[Category("Data")]
		[DefaultValue(0)]
		public decimal PercentComplete
		{
			get
			{
				return (decimal)(base.ViewState["PercentComplete"] ?? 0m);
			}
			set
			{
				base.ViewState["PercentComplete"] = value;
			}
		}

		// Token: 0x17000DB0 RID: 3504
		// (get) Token: 0x06002A85 RID: 10885 RVA: 0x00089782 File Offset: 0x00087982
		// (set) Token: 0x06002A86 RID: 10886 RVA: 0x000897A2 File Offset: 0x000879A2
		[Description("Value that determines the title of the task.")]
		[DefaultValue("")]
		[Category("Data")]
		public string Title
		{
			get
			{
				return (string)(base.ViewState["Title"] ?? "");
			}
			set
			{
				base.ViewState["Title"] = value;
			}
		}

		// Token: 0x17000DB1 RID: 3505
		// (get) Token: 0x06002A87 RID: 10887 RVA: 0x000897B5 File Offset: 0x000879B5
		[DefaultValue(typeof(TaskType), "Task")]
		[Description("Value that determines the type of the task.")]
		[Category("Data")]
		public TaskType TaskType
		{
			get
			{
				if (this.Summary)
				{
					return TaskType.Summary;
				}
				if (this.Duration == TimeSpan.Zero)
				{
					return TaskType.Milestone;
				}
				return TaskType.Task;
			}
		}

		// Token: 0x17000DB2 RID: 3506
		// (get) Token: 0x06002A88 RID: 10888 RVA: 0x000897D6 File Offset: 0x000879D6
		// (set) Token: 0x06002A89 RID: 10889 RVA: 0x000897DE File Offset: 0x000879DE
		[ScriptIgnore]
		public IGantt Owner
		{
			get
			{
				return this._owner;
			}
			set
			{
				this._owner = value;
				this.OnOwnerSet();
			}
		}

		// Token: 0x17000DB3 RID: 3507
		// (get) Token: 0x06002A8A RID: 10890 RVA: 0x000897ED File Offset: 0x000879ED
		[ScriptIgnore]
		public TaskCollection Tasks
		{
			get
			{
				return this._tasks;
			}
		}

		// Token: 0x17000DB4 RID: 3508
		// (get) Token: 0x06002A8B RID: 10891 RVA: 0x000897F5 File Offset: 0x000879F5
		[ScriptIgnore]
		public DependencyCollection Dependencies
		{
			get
			{
				return this._dependencies;
			}
		}

		// Token: 0x17000DB5 RID: 3509
		// (get) Token: 0x06002A8C RID: 10892 RVA: 0x000897FD File Offset: 0x000879FD
		[ScriptIgnore]
		public DependencyCollection Predecessors
		{
			get
			{
				return this._predecessors;
			}
		}

		// Token: 0x17000DB6 RID: 3510
		// (get) Token: 0x06002A8D RID: 10893 RVA: 0x00089805 File Offset: 0x00087A05
		[ScriptIgnore]
		public DependencyCollection Successors
		{
			get
			{
				return this._sucessors;
			}
		}

		// Token: 0x06002A8E RID: 10894 RVA: 0x0008985C File Offset: 0x00087A5C
		protected virtual void PopulateDependencyCollections()
		{
			if (this.Owner != null)
			{
				this._dependencies.AddRange(from d in this.Owner.Dependencies
				where object.Equals(d.SuccessorID, this.ID) || object.Equals(d.PredecessorID, this.ID)
				select d);
				this._sucessors.AddRange(from d in this._dependencies
				where object.Equals(d.PredecessorID, this.ID)
				select d);
				this._predecessors.AddRange(from d in this._dependencies
				where object.Equals(d.SuccessorID, this.ID)
				select d);
			}
		}

		// Token: 0x06002A8F RID: 10895 RVA: 0x000898F1 File Offset: 0x00087AF1
		protected virtual void InitializeChildTasks()
		{
			this._tasks = new TaskCollection(this.Owner);
		}

		// Token: 0x06002A90 RID: 10896 RVA: 0x00089904 File Offset: 0x00087B04
		protected virtual void OnOwnerSet()
		{
			this.PopulateDependencyCollections();
			this.InitializeChildTasks();
		}

		// Token: 0x06002A91 RID: 10897 RVA: 0x00089914 File Offset: 0x00087B14
		protected internal virtual IDictionary<string, object> GetSerializationData()
		{
			Dictionary<string, object> dictionary = new Dictionary<string, object>();
			dictionary["ID"] = this.ID;
			dictionary["ParentID"] = this.ParentID;
			dictionary["Start"] = this.Start.ToString(CultureInfo.InvariantCulture);
			if (this.PlannedStart != null)
			{
				dictionary["PlannedStart"] = this.PlannedStart.Value.ToString(CultureInfo.InvariantCulture);
			}
			dictionary["End"] = this.End.ToString(CultureInfo.InvariantCulture);
			if (this.PlannedEnd != null)
			{
				dictionary["PlannedEnd"] = this.PlannedEnd.Value.ToString(CultureInfo.InvariantCulture);
			}
			dictionary["Title"] = this.Title;
			dictionary["PercentComplete"] = this.PercentComplete;
			dictionary["Summary"] = this.Summary;
			if (this.Expanded)
			{
				dictionary["Expanded"] = this.Expanded;
			}
			if (this.OrderID != null)
			{
				dictionary["OrderID"] = this.OrderID;
			}
			return dictionary;
		}

		// Token: 0x06002A92 RID: 10898 RVA: 0x00089A6C File Offset: 0x00087C6C
		public virtual IOrderedDictionary GetData()
		{
			OrderedDictionary orderedDictionary = new OrderedDictionary();
			orderedDictionary["ID"] = this.ID;
			orderedDictionary["ParentID"] = this.ParentID;
			orderedDictionary["Start"] = this.Start;
			orderedDictionary["PlannedStart"] = this.PlannedStart;
			orderedDictionary["End"] = this.End;
			orderedDictionary["PlannedEnd"] = this.PlannedEnd;
			orderedDictionary["Title"] = this.Title;
			orderedDictionary["PercentComplete"] = this.PercentComplete;
			orderedDictionary["Summary"] = this.Summary;
			if (this.Expanded)
			{
				orderedDictionary["Expanded"] = this.Expanded;
			}
			if (this.OrderID != null)
			{
				orderedDictionary["OrderID"] = this.OrderID;
			}
			return orderedDictionary;
		}

		// Token: 0x06002A93 RID: 10899 RVA: 0x00089C74 File Offset: 0x00087E74
		public virtual void LoadFromDictionary(IDictionary values)
		{
			Dictionary<string, Action<object>> dictionary = new Dictionary<string, Action<object>>();
			dictionary.Add("ID", delegate(object obj)
			{
				this.ID = obj;
			});
			dictionary.Add("OrderID", delegate(object obj)
			{
				this.OrderID = obj;
			});
			dictionary.Add("ParentID", delegate(object obj)
			{
				this.ParentID = obj;
			});
			dictionary.Add("Title", delegate(object obj)
			{
				this.Title = (string)obj;
			});
			dictionary.Add("Start", delegate(object obj)
			{
				this.Start = Convert.ToDateTime(obj).ToUniversalTime();
			});
			dictionary.Add("PlannedStart", delegate(object obj)
			{
				this.PlannedStart = ((obj == null) ? ((DateTime?)obj) : new DateTime?(Convert.ToDateTime(obj).ToUniversalTime()));
			});
			dictionary.Add("End", delegate(object obj)
			{
				this.End = Convert.ToDateTime(obj).ToUniversalTime();
			});
			dictionary.Add("PlannedEnd", delegate(object obj)
			{
				this.PlannedEnd = ((obj == null) ? ((DateTime?)obj) : new DateTime?(Convert.ToDateTime(obj).ToUniversalTime()));
			});
			dictionary.Add("PercentComplete", delegate(object obj)
			{
				this.PercentComplete = Convert.ToDecimal(obj, CultureInfo.InvariantCulture);
			});
			dictionary.Add("Summary", delegate(object obj)
			{
				this.Summary = Convert.ToBoolean(obj);
			});
			dictionary.Add("Expanded", delegate(object obj)
			{
				this.Expanded = Convert.ToBoolean(obj);
			});
			foreach (string key in Task.TaskDataKeys.Keys)
			{
				if (values.Contains(key))
				{
					object obj2 = values[key];
					if (obj2 is string)
					{
						try
						{
							Guid guid = new Guid(obj2.ToString());
							obj2 = guid;
						}
						catch
						{
						}
					}
					dictionary[key](obj2);
				}
			}
		}

		// Token: 0x04000B07 RID: 2823
		private readonly DependencyCollection _dependencies;

		// Token: 0x04000B08 RID: 2824
		private readonly DependencyCollection _sucessors;

		// Token: 0x04000B09 RID: 2825
		private readonly DependencyCollection _predecessors;

		// Token: 0x04000B0A RID: 2826
		private TaskCollection _tasks;

		// Token: 0x04000B0B RID: 2827
		private IGantt _owner;

		// Token: 0x020004AB RID: 1195
		internal class TaskDataKeys
		{
			// Token: 0x04000B0C RID: 2828
			public const string ID = "ID";

			// Token: 0x04000B0D RID: 2829
			public const string ParentID = "ParentID";

			// Token: 0x04000B0E RID: 2830
			public const string OrderID = "OrderID";

			// Token: 0x04000B0F RID: 2831
			public const string Title = "Title";

			// Token: 0x04000B10 RID: 2832
			public const string Start = "Start";

			// Token: 0x04000B11 RID: 2833
			public const string PlannedStart = "PlannedStart";

			// Token: 0x04000B12 RID: 2834
			public const string End = "End";

			// Token: 0x04000B13 RID: 2835
			public const string PlannedEnd = "PlannedEnd";

			// Token: 0x04000B14 RID: 2836
			public const string PercentageComplete = "PercentComplete";

			// Token: 0x04000B15 RID: 2837
			public const string Summary = "Summary";

			// Token: 0x04000B16 RID: 2838
			public const string Expanded = "Expanded";

			// Token: 0x04000B17 RID: 2839
			public static IList<string> Keys = new List<string>
			{
				"ID",
				"ParentID",
				"OrderID",
				"Title",
				"Start",
				"PlannedStart",
				"End",
				"PlannedEnd",
				"PercentComplete",
				"Summary",
				"Expanded"
			};
		}
	}
}

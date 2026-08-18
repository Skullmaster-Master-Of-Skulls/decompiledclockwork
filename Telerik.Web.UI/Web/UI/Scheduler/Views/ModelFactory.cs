using System;

namespace Telerik.Web.UI.Scheduler.Views
{
	// Token: 0x02000845 RID: 2117
	internal abstract class ModelFactory : ISchedulerModelFactory
	{
		// Token: 0x17001990 RID: 6544
		// (get) Token: 0x06004E36 RID: 20022 RVA: 0x000F512A File Offset: 0x000F332A
		public IScheduler Owner
		{
			get
			{
				return this._owner;
			}
		}

		// Token: 0x17001991 RID: 6545
		// (get) Token: 0x06004E37 RID: 20023
		protected abstract string GroupBy { get; }

		// Token: 0x17001992 RID: 6546
		// (get) Token: 0x06004E38 RID: 20024 RVA: 0x000F5132 File Offset: 0x000F3332
		protected bool GroupByDate
		{
			get
			{
				return this.GroupBy.Trim().ToLowerInvariant().StartsWith("date,");
			}
		}

		// Token: 0x17001993 RID: 6547
		// (get) Token: 0x06004E39 RID: 20025 RVA: 0x000F5150 File Offset: 0x000F3350
		protected string GroupingResourceName
		{
			get
			{
				if (this.GroupBy.Trim().ToLowerInvariant() == "date")
				{
					throw new ArgumentException("GroupBy property should be in one of the following formats: <[Resource name]> or <Date,[Resource name]>. Using only <Date> is not allowed. ");
				}
				string[] array = this.GroupBy.Split(new char[]
				{
					','
				});
				if (array.Length > 2)
				{
					throw new ArgumentException("GroupBy property should be in one of the following formats: <[Resource name]> or <Date,[Resource name]> ");
				}
				if (!this.GroupByDate)
				{
					return array[0].Trim();
				}
				if (array.Length == 2)
				{
					return array[1].Trim();
				}
				return string.Empty;
			}
		}

		// Token: 0x17001994 RID: 6548
		// (get) Token: 0x06004E3A RID: 20026 RVA: 0x000F51D3 File Offset: 0x000F33D3
		protected bool EnableGrouping
		{
			get
			{
				return !string.IsNullOrEmpty(this.GroupBy);
			}
		}

		// Token: 0x06004E3B RID: 20027 RVA: 0x000F51E3 File Offset: 0x000F33E3
		protected ModelFactory(IScheduler owner)
		{
			this._owner = owner;
		}

		// Token: 0x06004E3C RID: 20028
		public abstract ISchedulerModel CreateModel();

		// Token: 0x0400137C RID: 4988
		private readonly IScheduler _owner;
	}
}

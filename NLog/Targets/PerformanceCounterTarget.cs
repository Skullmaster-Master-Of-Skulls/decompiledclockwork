using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using NLog.Common;
using NLog.Config;
using NLog.Internal;
using NLog.Layouts;

namespace NLog.Targets
{
	// Token: 0x0200016C RID: 364
	[Target("PerfCounter")]
	public class PerformanceCounterTarget : Target, IInstallable
	{
		// Token: 0x06000DBD RID: 3517 RVA: 0x000210A6 File Offset: 0x0001F2A6
		public PerformanceCounterTarget()
		{
			this.CounterType = PerformanceCounterType.NumberOfItems32;
			this.IncrementValue = new SimpleLayout("1");
			this.InstanceName = string.Empty;
			this.CounterHelp = string.Empty;
		}

		// Token: 0x06000DBE RID: 3518 RVA: 0x000210DF File Offset: 0x0001F2DF
		public PerformanceCounterTarget(string name) : this()
		{
			base.Name = name;
		}

		// Token: 0x1700026A RID: 618
		// (get) Token: 0x06000DBF RID: 3519 RVA: 0x000210EE File Offset: 0x0001F2EE
		// (set) Token: 0x06000DC0 RID: 3520 RVA: 0x000210F6 File Offset: 0x0001F2F6
		public bool AutoCreate { get; set; }

		// Token: 0x1700026B RID: 619
		// (get) Token: 0x06000DC1 RID: 3521 RVA: 0x000210FF File Offset: 0x0001F2FF
		// (set) Token: 0x06000DC2 RID: 3522 RVA: 0x00021107 File Offset: 0x0001F307
		[RequiredParameter]
		public string CategoryName { get; set; }

		// Token: 0x1700026C RID: 620
		// (get) Token: 0x06000DC3 RID: 3523 RVA: 0x00021110 File Offset: 0x0001F310
		// (set) Token: 0x06000DC4 RID: 3524 RVA: 0x00021118 File Offset: 0x0001F318
		[RequiredParameter]
		public string CounterName { get; set; }

		// Token: 0x1700026D RID: 621
		// (get) Token: 0x06000DC5 RID: 3525 RVA: 0x00021121 File Offset: 0x0001F321
		// (set) Token: 0x06000DC6 RID: 3526 RVA: 0x00021129 File Offset: 0x0001F329
		public string InstanceName { get; set; }

		// Token: 0x1700026E RID: 622
		// (get) Token: 0x06000DC7 RID: 3527 RVA: 0x00021132 File Offset: 0x0001F332
		// (set) Token: 0x06000DC8 RID: 3528 RVA: 0x0002113A File Offset: 0x0001F33A
		public string CounterHelp { get; set; }

		// Token: 0x1700026F RID: 623
		// (get) Token: 0x06000DC9 RID: 3529 RVA: 0x00021143 File Offset: 0x0001F343
		// (set) Token: 0x06000DCA RID: 3530 RVA: 0x0002114B File Offset: 0x0001F34B
		[DefaultValue(PerformanceCounterType.NumberOfItems32)]
		public PerformanceCounterType CounterType { get; set; }

		// Token: 0x17000270 RID: 624
		// (get) Token: 0x06000DCB RID: 3531 RVA: 0x00021154 File Offset: 0x0001F354
		// (set) Token: 0x06000DCC RID: 3532 RVA: 0x0002115C File Offset: 0x0001F35C
		[DefaultValue(1)]
		public Layout IncrementValue { get; set; }

		// Token: 0x06000DCD RID: 3533 RVA: 0x00021178 File Offset: 0x0001F378
		public void Install(InstallationContext installationContext)
		{
			Dictionary<string, List<PerformanceCounterTarget>> dictionary = base.LoggingConfiguration.AllTargets.OfType<PerformanceCounterTarget>().BucketSort((PerformanceCounterTarget c) => c.CategoryName);
			string categoryName = this.CategoryName;
			if (dictionary[categoryName].Any((PerformanceCounterTarget c) => c.created))
			{
				installationContext.Trace("Category '{0}' has already been installed.", new object[]
				{
					categoryName
				});
				return;
			}
			try
			{
				PerformanceCounterCategoryType performanceCounterCategoryType;
				CounterCreationDataCollection counterCreationDataCollection = PerformanceCounterTarget.GetCounterCreationDataCollection(dictionary[this.CategoryName], out performanceCounterCategoryType);
				if (PerformanceCounterCategory.Exists(categoryName))
				{
					installationContext.Debug("Deleting category '{0}'", new object[]
					{
						categoryName
					});
					PerformanceCounterCategory.Delete(categoryName);
				}
				installationContext.Debug("Creating category '{0}' with {1} counter(s) (Type: {2})", new object[]
				{
					categoryName,
					counterCreationDataCollection.Count,
					performanceCounterCategoryType
				});
				foreach (object obj in counterCreationDataCollection)
				{
					CounterCreationData counterCreationData = (CounterCreationData)obj;
					installationContext.Trace("  Counter: '{0}' Type: ({1}) Help: {2}", new object[]
					{
						counterCreationData.CounterName,
						counterCreationData.CounterType,
						counterCreationData.CounterHelp
					});
				}
				PerformanceCounterCategory.Create(categoryName, "Category created by NLog", performanceCounterCategoryType, counterCreationDataCollection);
			}
			catch (Exception ex)
			{
				if (ex.MustBeRethrownImmediately())
				{
					throw;
				}
				if (!installationContext.IgnoreFailures)
				{
					installationContext.Error("Error creating category '{0}': {1}", new object[]
					{
						categoryName,
						ex.Message
					});
					throw;
				}
				installationContext.Warning("Error creating category '{0}': {1}", new object[]
				{
					categoryName,
					ex.Message
				});
				if (ex.MustBeRethrown())
				{
					throw;
				}
			}
			finally
			{
				foreach (PerformanceCounterTarget performanceCounterTarget in dictionary[categoryName])
				{
					performanceCounterTarget.created = true;
				}
			}
		}

		// Token: 0x06000DCE RID: 3534 RVA: 0x00021408 File Offset: 0x0001F608
		public void Uninstall(InstallationContext installationContext)
		{
			string categoryName = this.CategoryName;
			if (PerformanceCounterCategory.Exists(categoryName))
			{
				installationContext.Debug("Deleting category '{0}'", new object[]
				{
					categoryName
				});
				PerformanceCounterCategory.Delete(categoryName);
				return;
			}
			installationContext.Debug("Category '{0}' does not exist.", new object[]
			{
				categoryName
			});
		}

		// Token: 0x06000DCF RID: 3535 RVA: 0x00021459 File Offset: 0x0001F659
		public bool? IsInstalled(InstallationContext installationContext)
		{
			if (!PerformanceCounterCategory.Exists(this.CategoryName))
			{
				return new bool?(false);
			}
			return new bool?(PerformanceCounterCategory.CounterExists(this.CounterName, this.CategoryName));
		}

		// Token: 0x06000DD0 RID: 3536 RVA: 0x00021488 File Offset: 0x0001F688
		protected override void Write(LogEventInfo logEvent)
		{
			if (this.EnsureInitialized())
			{
				string text = this.IncrementValue.Render(logEvent);
				long value;
				if (long.TryParse(text, out value))
				{
					this.perfCounter.IncrementBy(value);
					return;
				}
				InternalLogger.Error("Error incrementing PerfCounter {0}. IncrementValue must be an integer but was <{1}>", new object[]
				{
					this.CounterName,
					text
				});
			}
		}

		// Token: 0x06000DD1 RID: 3537 RVA: 0x000214E1 File Offset: 0x0001F6E1
		protected override void CloseTarget()
		{
			base.CloseTarget();
			if (this.perfCounter != null)
			{
				this.perfCounter.Close();
				this.perfCounter = null;
			}
			this.initialized = false;
		}

		// Token: 0x06000DD2 RID: 3538 RVA: 0x0002150C File Offset: 0x0001F70C
		private static CounterCreationDataCollection GetCounterCreationDataCollection(IEnumerable<PerformanceCounterTarget> countersInCategory, out PerformanceCounterCategoryType categoryType)
		{
			categoryType = PerformanceCounterCategoryType.SingleInstance;
			CounterCreationDataCollection counterCreationDataCollection = new CounterCreationDataCollection();
			foreach (PerformanceCounterTarget performanceCounterTarget in countersInCategory)
			{
				if (!string.IsNullOrEmpty(performanceCounterTarget.InstanceName))
				{
					categoryType = PerformanceCounterCategoryType.MultiInstance;
				}
				counterCreationDataCollection.Add(new CounterCreationData(performanceCounterTarget.CounterName, performanceCounterTarget.CounterHelp, performanceCounterTarget.CounterType));
			}
			return counterCreationDataCollection;
		}

		// Token: 0x06000DD3 RID: 3539 RVA: 0x00021588 File Offset: 0x0001F788
		private bool EnsureInitialized()
		{
			if (!this.initialized)
			{
				this.initialized = true;
				if (this.AutoCreate)
				{
					using (InstallationContext installationContext = new InstallationContext())
					{
						this.Install(installationContext);
					}
				}
				try
				{
					this.perfCounter = new PerformanceCounter(this.CategoryName, this.CounterName, this.InstanceName, false);
				}
				catch (Exception ex)
				{
					InternalLogger.Error(ex, "Cannot open performance counter {0}/{1}/{2}.", new object[]
					{
						this.CategoryName,
						this.CounterName,
						this.InstanceName
					});
					if (ex.MustBeRethrown())
					{
						throw;
					}
				}
			}
			return this.perfCounter != null;
		}

		// Token: 0x040003BB RID: 955
		private PerformanceCounter perfCounter;

		// Token: 0x040003BC RID: 956
		private bool initialized;

		// Token: 0x040003BD RID: 957
		private bool created;
	}
}

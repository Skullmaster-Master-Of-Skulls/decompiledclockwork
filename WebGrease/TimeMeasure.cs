using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using WebGrease.Extensions;

namespace WebGrease
{
	// Token: 0x0200010E RID: 270
	public class TimeMeasure : ITimeMeasure
	{
		// Token: 0x060010EE RID: 4334 RVA: 0x0004B450 File Offset: 0x00049650
		public TimeMeasureResult[] GetResults()
		{
			return (from m in this.measurements.Last<IDictionary<string, double>>()
			orderby m.Value descending
			select new TimeMeasureResult
			{
				IdParts = WebGreaseContext.ToIdParts(m.Key),
				Duration = m.Value,
				Count = this.measurementCounts.Last<IDictionary<string, int>>()[m.Key]
			}).ToArray<TimeMeasureResult>();
		}

		// Token: 0x060010EF RID: 4335 RVA: 0x0004B4BC File Offset: 0x000496BC
		public void Start(bool isGroup, params string[] idParts)
		{
			string id = WebGreaseContext.ToStringId(idParts);
			if (this.timers.Any((TimeMeasureItem t) => t.Id.Equals(id)))
			{
				throw new BuildWorkflowException("An error occurred while starting timer for {0}, probably a wrong start/end for key: ".InvariantFormat(new object[]
				{
					id
				}));
			}
			this.PauseLastTimer();
			this.timers.Add(new TimeMeasureItem(id, DateTime.Now));
			if (isGroup)
			{
				this.BeginGroup();
			}
		}

		// Token: 0x060010F0 RID: 4336 RVA: 0x0004B540 File Offset: 0x00049740
		public void End(bool isGroup, params string[] idParts)
		{
			if (isGroup)
			{
				this.EndGroup();
			}
			string b = WebGreaseContext.ToStringId(idParts);
			TimeMeasureItem timeMeasureItem = this.timers.Last<TimeMeasureItem>();
			if (timeMeasureItem.Id != b)
			{
				throw new BuildWorkflowException("Trying to end a timer that was not started.");
			}
			this.StopTimer(timeMeasureItem);
			this.ResumeLastTimer();
		}

		// Token: 0x060010F1 RID: 4337 RVA: 0x0004B58F File Offset: 0x0004978F
		public void BeginGroup()
		{
			this.measurementCounts.Add(new Dictionary<string, int>());
			this.measurements.Add(new Dictionary<string, double>());
		}

		// Token: 0x060010F2 RID: 4338 RVA: 0x0004B5B4 File Offset: 0x000497B4
		public void EndGroup()
		{
			if (this.measurementCounts.Count<IDictionary<string, int>>() == 1)
			{
				throw new BuildWorkflowException("No measure sections available to end.");
			}
			IDictionary<string, int> dictionary = this.measurementCounts.Last<IDictionary<string, int>>();
			IDictionary<string, double> dictionary2 = this.measurements.Last<IDictionary<string, double>>();
			this.measurementCounts.RemoveAt(this.measurementCounts.Count<IDictionary<string, int>>() - 1);
			this.measurements.RemoveAt(this.measurements.Count<IDictionary<string, double>>() - 1);
			this.measurementCounts.Last<IDictionary<string, int>>().Add(dictionary);
			this.measurements.Last<IDictionary<string, double>>().Add(dictionary2);
		}

		// Token: 0x060010F3 RID: 4339 RVA: 0x0004B644 File Offset: 0x00049844
		public void WriteResults(string filePathWithoutExtension, string title, DateTimeOffset utcStart)
		{
			TimeMeasureResult[] results = this.GetResults();
			File.WriteAllText(filePathWithoutExtension + ".measure.txt", TimeMeasure.GetMeasureTable(title, results) + "\r\nTotal seconds: {0}".InvariantFormat(new object[]
			{
				(DateTimeOffset.Now - utcStart).TotalSeconds
			}));
			File.WriteAllText(filePathWithoutExtension + ".measure.csv", results.GetCsv());
		}

		// Token: 0x060010F4 RID: 4340 RVA: 0x0004B73C File Offset: 0x0004993C
		internal static void WriteResults(string filePathWithoutExtension, IEnumerable<Tuple<string, bool, IEnumerable<TimeMeasureResult>>> results, string title, DateTimeOffset startTime, string activityName)
		{
			DateTimeOffset now = DateTimeOffset.Now;
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.AppendFormat("Configuration file: {0}", title);
			stringBuilder.AppendLine();
			stringBuilder.AppendFormat("Activity: {0}", activityName);
			stringBuilder.AppendLine();
			stringBuilder.AppendFormat("Started at: {0:yy-MM-dd HH:mm:ss.fff}", startTime);
			stringBuilder.AppendLine();
			stringBuilder.AppendFormat("Ended at: {0:yy-MM-dd HH:mm:ss.fff}", now);
			stringBuilder.AppendLine();
			stringBuilder.AppendFormat("Total Seconds: {0}", (now - startTime).TotalSeconds);
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			foreach (Tuple<string, bool, IEnumerable<TimeMeasureResult>> tuple in from r in results
			orderby r.Item2, r.Item3.Sum((TimeMeasureResult v) => v.Duration) descending
			select r)
			{
				string item = tuple.Item1;
				IEnumerable<TimeMeasureResult> item2 = tuple.Item3;
				stringBuilder.AppendLine(item2.GetTextTable(item + " - Details"));
			}
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			stringBuilder.AppendLine();
			foreach (Tuple<string, bool, IEnumerable<TimeMeasureResult>> tuple2 in from r in results
			where !r.Item2
			orderby r.Item3.Sum((TimeMeasureResult v) => v.Duration) descending
			select r)
			{
				string item3 = tuple2.Item1;
				IEnumerable<TimeMeasureResult> item4 = tuple2.Item3;
				stringBuilder.AppendLine(item4.Group((TimeMeasureResult tm) => tm.IdParts.FirstOrDefault<string>()).GetTextTable(item3 + " - Summary"));
			}
			File.WriteAllText("{0}.{1}.measure.txt".InvariantFormat(new object[]
			{
				filePathWithoutExtension,
				activityName
			}), stringBuilder.ToString());
			foreach (Tuple<string, bool, IEnumerable<TimeMeasureResult>> tuple3 in results)
			{
				File.WriteAllText("{0}.{1}.{2}.measure.csv".InvariantFormat(new object[]
				{
					filePathWithoutExtension,
					activityName,
					tuple3.Item1
				}), tuple3.Item3.GetCsv());
			}
		}

		// Token: 0x060010F5 RID: 4341 RVA: 0x0004BA1C File Offset: 0x00049C1C
		private static string GetMeasureTable(string title, IEnumerable<TimeMeasureResult> measureTotal)
		{
			string format = "{0}\r\n\r\n{1}\r\n\r\nStarted at: {2:yy-MM-dd HH:mm:ss.fff}";
			object[] array = new object[3];
			array[0] = measureTotal.GetTextTable(title);
			array[1] = measureTotal.Group((TimeMeasureResult tm) => tm.IdParts.FirstOrDefault<string>()).GetTextTable(title);
			array[2] = DateTime.Now;
			return format.InvariantFormat(array);
		}

		// Token: 0x060010F6 RID: 4342 RVA: 0x0004BA80 File Offset: 0x00049C80
		private void AddToResult(TimeMeasureItem timer)
		{
			string id = timer.Id;
			if (!this.measurementCounts.Last<IDictionary<string, int>>().ContainsKey(id))
			{
				this.measurementCounts.Last<IDictionary<string, int>>().Add(id, 0);
			}
			IDictionary<string, int> dictionary;
			string key;
			(dictionary = this.measurementCounts.Last<IDictionary<string, int>>())[key = id] = dictionary[key] + 1;
			if (!this.measurements.Last<IDictionary<string, double>>().ContainsKey(id))
			{
				this.measurements.Last<IDictionary<string, double>>().Add(id, 0.0);
			}
			IDictionary<string, double> dictionary2;
			string key2;
			(dictionary2 = this.measurements.Last<IDictionary<string, double>>())[key2 = id] = dictionary2[key2] + (DateTime.Now - timer.Value).TotalMilliseconds;
		}

		// Token: 0x060010F7 RID: 4343 RVA: 0x0004BB3C File Offset: 0x00049D3C
		private void PauseLastTimer()
		{
			if (this.timers.Any<TimeMeasureItem>())
			{
				this.AddToResult(this.timers.Last<TimeMeasureItem>());
			}
		}

		// Token: 0x060010F8 RID: 4344 RVA: 0x0004BB5C File Offset: 0x00049D5C
		private void ResumeLastTimer()
		{
			if (this.timers.Any<TimeMeasureItem>())
			{
				this.timers.Last<TimeMeasureItem>().Value = DateTime.Now;
			}
		}

		// Token: 0x060010F9 RID: 4345 RVA: 0x0004BB80 File Offset: 0x00049D80
		private void StopTimer(TimeMeasureItem timer)
		{
			this.timers.Remove(timer);
			this.AddToResult(timer);
		}

		// Token: 0x040006A4 RID: 1700
		private readonly List<IDictionary<string, int>> measurementCounts = new List<IDictionary<string, int>>
		{
			new Dictionary<string, int>()
		};

		// Token: 0x040006A5 RID: 1701
		private readonly List<IDictionary<string, double>> measurements = new List<IDictionary<string, double>>
		{
			new Dictionary<string, double>()
		};

		// Token: 0x040006A6 RID: 1702
		private readonly IList<TimeMeasureItem> timers = new List<TimeMeasureItem>();
	}
}

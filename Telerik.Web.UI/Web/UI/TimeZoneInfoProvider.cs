using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using Telerik.Web.UI.Scheduler.TimeZones;

namespace Telerik.Web.UI
{
	// Token: 0x02000E71 RID: 3697
	public class TimeZoneInfoProvider : TimeZoneProviderBase, IDisposable
	{
		// Token: 0x06008C34 RID: 35892 RVA: 0x001FD1A6 File Offset: 0x001FB3A6
		public TimeZoneInfoProvider()
		{
		}

		// Token: 0x06008C35 RID: 35893 RVA: 0x001FD1AE File Offset: 0x001FB3AE
		public TimeZoneInfoProvider(string timeZoneId)
		{
			this.InitOperationZones(timeZoneId);
		}

		// Token: 0x06008C36 RID: 35894 RVA: 0x001FD1BD File Offset: 0x001FB3BD
		public override void Initialize(string name, NameValueCollection config)
		{
			base.Initialize(name, config);
			this.InitOperationZones(config["timeZoneId"]);
		}

		// Token: 0x06008C37 RID: 35895 RVA: 0x001FD1D8 File Offset: 0x001FB3D8
		private void InitOperationZones(string id)
		{
			if (!string.IsNullOrEmpty(id))
			{
				base.OperationTimeZone = new TimeZoneInfoModel(this.GetTimeZoneInfoById(id));
				return;
			}
			base.OperationTimeZone = new TimeZoneInfoModel(TimeZoneInfo.Utc);
		}

		// Token: 0x06008C38 RID: 35896 RVA: 0x001FD220 File Offset: 0x001FB420
		internal static TimeZoneInfoModel GetTimeZoneModelById(string id)
		{
			TimeZoneInfoModel result;
			try
			{
				if (string.IsNullOrEmpty(id))
				{
					id = TimeZoneInfo.Utc.Id;
				}
				result = new TimeZoneInfoModel(TimeZoneInfo.GetSystemTimeZones().First((TimeZoneInfo timeZoneInfo) => timeZoneInfo.Id == id));
			}
			catch (InvalidOperationException innerException)
			{
				throw new TimeZoneNotFoundException("Time Zone with the provided Id was not found", innerException);
			}
			catch (Exception innerException2)
			{
				throw new TimeZoneNotFoundException("Time Zone with the provided Id was not found", innerException2);
			}
			return result;
		}

		// Token: 0x06008C39 RID: 35897 RVA: 0x001FD2B8 File Offset: 0x001FB4B8
		internal static DateTime LocalToUtc(DateTime local, ITimeZoneModel timeZone)
		{
			TimeSpan utcOffset = timeZone.GetUtcOffset(local);
			return new DateTime(local.Add(-utcOffset).Ticks, DateTimeKind.Utc);
		}

		// Token: 0x06008C3A RID: 35898 RVA: 0x001FD2E8 File Offset: 0x001FB4E8
		internal static DateTime UtcToLocal(DateTime utc, ITimeZoneModel timeZone)
		{
			TimeSpan utcOffset = timeZone.GetUtcOffset(utc);
			return new DateTime(utc.Add(utcOffset).Ticks, DateTimeKind.Unspecified);
		}

		// Token: 0x06008C3B RID: 35899 RVA: 0x001FD330 File Offset: 0x001FB530
		internal TimeZoneInfo GetTimeZoneInfoById(string id)
		{
			TimeZoneInfo result;
			try
			{
				result = TimeZoneInfo.GetSystemTimeZones().First((TimeZoneInfo timeZoneInfo) => timeZoneInfo.Id == id);
			}
			catch (InvalidOperationException innerException)
			{
				throw new TimeZoneNotFoundException("Time Zone with the provided Id was not found", innerException);
			}
			return result;
		}

		// Token: 0x06008C3C RID: 35900 RVA: 0x001FD388 File Offset: 0x001FB588
		public override DateTime UtcToLocal(DateTime utc)
		{
			return TimeZoneInfoProvider.UtcToLocal(utc, base.OperationTimeZone);
		}

		// Token: 0x06008C3D RID: 35901 RVA: 0x001FD396 File Offset: 0x001FB596
		public override DateTime LocalToUtc(DateTime local)
		{
			return TimeZoneInfoProvider.LocalToUtc(local, base.OperationTimeZone);
		}

		// Token: 0x06008C3E RID: 35902 RVA: 0x001FD3D0 File Offset: 0x001FB5D0
		public override List<TimeZoneNamePair> GetAllTimeZones()
		{
			return (from timeZoneInfo in TimeZoneInfo.GetSystemTimeZones()
			select new TimeZoneNamePair
			{
				Id = timeZoneInfo.Id,
				DisplayName = timeZoneInfo.DisplayName
			}).ToList<TimeZoneNamePair>();
		}

		// Token: 0x06008C3F RID: 35903 RVA: 0x001FD3FE File Offset: 0x001FB5FE
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06008C40 RID: 35904 RVA: 0x001FD40D File Offset: 0x001FB60D
		protected virtual void Dispose(bool disposing)
		{
			if (disposing)
			{
				base.OperationTimeZone = null;
			}
		}
	}
}

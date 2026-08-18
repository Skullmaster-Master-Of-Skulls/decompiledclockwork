using System;
using System.Collections.Specialized;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Globalization;
using System.Security.Permissions;
using System.Threading;
using System.Web.DataAccess;
using System.Web.Util;

namespace System.Web.Management
{
	// Token: 0x0200017A RID: 378
	[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
	public class SqlWebEventProvider : BufferedWebEventProvider, IInternalWebEventProvider
	{
		// Token: 0x060014D3 RID: 5331 RVA: 0x0003F1DC File Offset: 0x0003D3DC
		protected internal SqlWebEventProvider()
		{
		}

		// Token: 0x060014D4 RID: 5332 RVA: 0x0003F200 File Offset: 0x0003D400
		public override void Initialize(string name, NameValueCollection config)
		{
			this._SchemaVersionCheck = 0;
			string text = null;
			ProviderUtil.GetAndRemoveStringAttribute(config, "connectionStringName", name, ref text);
			ProviderUtil.GetAndRemoveStringAttribute(config, "connectionString", name, ref this._sqlConnectionString);
			if (!string.IsNullOrEmpty(text))
			{
				if (!string.IsNullOrEmpty(this._sqlConnectionString))
				{
					throw new ConfigurationErrorsException(SR.GetString("Only_one_connection_string_allowed"));
				}
				this._sqlConnectionString = SqlConnectionHelper.GetConnectionString(text, true, true);
				if (this._sqlConnectionString == null || this._sqlConnectionString.Length < 1)
				{
					throw new ConfigurationErrorsException(SR.GetString("Connection_string_not_found", new object[]
					{
						text
					}));
				}
			}
			else
			{
				SqlConnectionStringBuilder sqlConnectionStringBuilder = new SqlConnectionStringBuilder(this._sqlConnectionString);
				if (sqlConnectionStringBuilder.IntegratedSecurity)
				{
					throw new ConfigurationErrorsException(SR.GetString("Cannot_use_integrated_security"));
				}
			}
			if (string.IsNullOrEmpty(this._sqlConnectionString))
			{
				throw new ConfigurationErrorsException(SR.GetString("Must_specify_connection_string_or_name", new object[]
				{
					text
				}));
			}
			ProviderUtil.GetAndRemovePositiveOrInfiniteAttribute(config, "maxEventDetailsLength", name, ref this._maxEventDetailsLength);
			if (this._maxEventDetailsLength == 2147483647)
			{
				this._maxEventDetailsLength = -1;
			}
			else if (this._maxEventDetailsLength > 1073741823)
			{
				throw new ConfigurationErrorsException(SR.GetString("Invalid_max_event_details_length", new object[]
				{
					name,
					this._maxEventDetailsLength.ToString(CultureInfo.CurrentCulture)
				}));
			}
			ProviderUtil.GetAndRemovePositiveAttribute(config, "commandTimeout", name, ref this._commandTimeout);
			base.Initialize(name, config);
		}

		// Token: 0x060014D5 RID: 5333 RVA: 0x0003F360 File Offset: 0x0003D560
		private void CheckSchemaVersion(SqlConnection connection)
		{
			string[] features = new string[]
			{
				"Health Monitoring"
			};
			string version = "1";
			SecUtility.CheckSchemaVersion(this, connection, features, version, ref this._SchemaVersionCheck);
		}

		// Token: 0x060014D6 RID: 5334 RVA: 0x0003F391 File Offset: 0x0003D591
		public override void ProcessEventFlush(WebEventBufferFlushInfo flushInfo)
		{
			this.WriteToSQL(flushInfo.Events, flushInfo.EventsDiscardedSinceLastNotification, flushInfo.LastNotificationUtc);
		}

		// Token: 0x060014D7 RID: 5335 RVA: 0x0003F3AC File Offset: 0x0003D5AC
		private void PrepareParams(SqlCommand sqlCommand)
		{
			sqlCommand.Parameters.Add(new SqlParameter("@EventId", SqlDbType.Char, 32));
			sqlCommand.Parameters.Add(new SqlParameter("@EventTimeUtc", SqlDbType.DateTime));
			sqlCommand.Parameters.Add(new SqlParameter("@EventTime", SqlDbType.DateTime));
			sqlCommand.Parameters.Add(new SqlParameter("@EventType", SqlDbType.NVarChar, 256));
			sqlCommand.Parameters.Add(new SqlParameter("@EventSequence", SqlDbType.Decimal));
			sqlCommand.Parameters.Add(new SqlParameter("@EventOccurrence", SqlDbType.Decimal));
			sqlCommand.Parameters.Add(new SqlParameter("@EventCode", SqlDbType.Int));
			sqlCommand.Parameters.Add(new SqlParameter("@EventDetailCode", SqlDbType.Int));
			sqlCommand.Parameters.Add(new SqlParameter("@Message", SqlDbType.NVarChar, 1024));
			sqlCommand.Parameters.Add(new SqlParameter("@ApplicationPath", SqlDbType.NVarChar, 256));
			sqlCommand.Parameters.Add(new SqlParameter("@ApplicationVirtualPath", SqlDbType.NVarChar, 256));
			sqlCommand.Parameters.Add(new SqlParameter("@MachineName", SqlDbType.NVarChar, 256));
			sqlCommand.Parameters.Add(new SqlParameter("@RequestUrl", SqlDbType.NVarChar, 1024));
			sqlCommand.Parameters.Add(new SqlParameter("@ExceptionType", SqlDbType.NVarChar, 256));
			sqlCommand.Parameters.Add(new SqlParameter("@Details", SqlDbType.NText));
		}

		// Token: 0x060014D8 RID: 5336 RVA: 0x0003F540 File Offset: 0x0003D740
		private void FillParams(SqlCommand sqlCommand, WebBaseEvent eventRaised)
		{
			Exception ex = null;
			WebRequestInformation webRequestInformation = null;
			WebApplicationInformation applicationInformation = WebBaseEvent.ApplicationInformation;
			int num = 0;
			sqlCommand.Parameters[num++].Value = eventRaised.EventID.ToString("N", CultureInfo.InstalledUICulture);
			sqlCommand.Parameters[num++].Value = eventRaised.EventTimeUtc;
			sqlCommand.Parameters[num++].Value = eventRaised.EventTime;
			sqlCommand.Parameters[num++].Value = eventRaised.GetType().ToString();
			sqlCommand.Parameters[num++].Value = eventRaised.EventSequence;
			sqlCommand.Parameters[num++].Value = eventRaised.EventOccurrence;
			sqlCommand.Parameters[num++].Value = eventRaised.EventCode;
			sqlCommand.Parameters[num++].Value = eventRaised.EventDetailCode;
			sqlCommand.Parameters[num++].Value = eventRaised.Message;
			sqlCommand.Parameters[num++].Value = applicationInformation.ApplicationPath;
			sqlCommand.Parameters[num++].Value = applicationInformation.ApplicationVirtualPath;
			sqlCommand.Parameters[num++].Value = applicationInformation.MachineName;
			if (eventRaised is WebRequestEvent)
			{
				webRequestInformation = ((WebRequestEvent)eventRaised).RequestInformation;
			}
			else if (eventRaised is WebRequestErrorEvent)
			{
				webRequestInformation = ((WebRequestErrorEvent)eventRaised).RequestInformation;
			}
			else if (eventRaised is WebErrorEvent)
			{
				webRequestInformation = ((WebErrorEvent)eventRaised).RequestInformation;
			}
			else if (eventRaised is WebAuditEvent)
			{
				webRequestInformation = ((WebAuditEvent)eventRaised).RequestInformation;
			}
			sqlCommand.Parameters[num++].Value = ((webRequestInformation != null) ? webRequestInformation.RequestUrl : Convert.DBNull);
			if (eventRaised is WebBaseErrorEvent)
			{
				ex = ((WebBaseErrorEvent)eventRaised).ErrorException;
			}
			sqlCommand.Parameters[num++].Value = ((ex != null) ? ex.GetType().ToString() : Convert.DBNull);
			string text = eventRaised.ToString();
			if (this._maxEventDetailsLength != -1 && text.Length > this._maxEventDetailsLength)
			{
				text = text.Substring(0, this._maxEventDetailsLength);
			}
			sqlCommand.Parameters[num++].Value = text;
		}

		// Token: 0x060014D9 RID: 5337 RVA: 0x0003F7F0 File Offset: 0x0003D9F0
		[PermissionSet(SecurityAction.InheritanceDemand, Unrestricted = true)]
		[SqlClientPermission(SecurityAction.Assert, Unrestricted = true)]
		[PermissionSet(SecurityAction.Assert, Unrestricted = true)]
		private void WriteToSQL(WebBaseEventCollection events, int eventsDiscardedByBuffer, DateTime lastNotificationUtc)
		{
			if (this._retryDate > DateTime.UtcNow)
			{
				return;
			}
			try
			{
				SqlConnectionHolder connection = SqlConnectionHelper.GetConnection(this._sqlConnectionString, true);
				SqlCommand sqlCommand = new SqlCommand("dbo.aspnet_WebEvent_LogEvent");
				this.CheckSchemaVersion(connection.Connection);
				sqlCommand.CommandType = CommandType.StoredProcedure;
				sqlCommand.Connection = connection.Connection;
				if (this._commandTimeout > -1)
				{
					sqlCommand.CommandTimeout = this._commandTimeout;
				}
				this.PrepareParams(sqlCommand);
				try
				{
					connection.Open(null, true);
					Interlocked.Increment(ref this._connectionCount);
					if (eventsDiscardedByBuffer != 0)
					{
						WebBaseEvent eventRaised = new WebBaseEvent(SR.GetString("Sql_webevent_provider_events_dropped", new object[]
						{
							eventsDiscardedByBuffer.ToString(CultureInfo.InstalledUICulture),
							lastNotificationUtc.ToString("r", CultureInfo.InstalledUICulture)
						}), null, 6001, 50301);
						this.FillParams(sqlCommand, eventRaised);
						sqlCommand.ExecuteNonQuery();
					}
					foreach (object obj in events)
					{
						WebBaseEvent eventRaised2 = (WebBaseEvent)obj;
						this.FillParams(sqlCommand, eventRaised2);
						sqlCommand.ExecuteNonQuery();
					}
				}
				finally
				{
					connection.Close();
					Interlocked.Decrement(ref this._connectionCount);
				}
				try
				{
					this.EventProcessingComplete(events);
				}
				catch
				{
				}
			}
			catch
			{
				double value = 30.0;
				if (this._commandTimeout > -1)
				{
					value = (double)this._commandTimeout;
				}
				this._retryDate = DateTime.UtcNow.AddSeconds(value);
				throw;
			}
		}

		// Token: 0x060014DA RID: 5338 RVA: 0x0003F9D0 File Offset: 0x0003DBD0
		public override void ProcessEvent(WebBaseEvent eventRaised)
		{
			if (base.UseBuffering)
			{
				base.ProcessEvent(eventRaised);
				return;
			}
			this.WriteToSQL(new WebBaseEventCollection(eventRaised), 0, new DateTime(0L));
		}

		// Token: 0x060014DB RID: 5339 RVA: 0x00006164 File Offset: 0x00004364
		protected virtual void EventProcessingComplete(WebBaseEventCollection raisedEvents)
		{
		}

		// Token: 0x060014DC RID: 5340 RVA: 0x0003F9F8 File Offset: 0x0003DBF8
		public override void Shutdown()
		{
			try
			{
				this.Flush();
			}
			finally
			{
				base.Shutdown();
			}
			if (this._connectionCount > 0)
			{
				int num = this._commandTimeout * 2;
				if (num <= 0)
				{
					num = 60;
				}
				while (this._connectionCount > 0 && num > 0)
				{
					num--;
					Thread.Sleep(1000);
				}
			}
		}

		// Token: 0x04001588 RID: 5512
		private const int SQL_MAX_NTEXT_SIZE = 1073741823;

		// Token: 0x04001589 RID: 5513
		private const int NO_LIMIT = -1;

		// Token: 0x0400158A RID: 5514
		private const string SP_LOG_EVENT = "dbo.aspnet_WebEvent_LogEvent";

		// Token: 0x0400158B RID: 5515
		private string _sqlConnectionString;

		// Token: 0x0400158C RID: 5516
		private int _maxEventDetailsLength = -1;

		// Token: 0x0400158D RID: 5517
		private int _commandTimeout = -1;

		// Token: 0x0400158E RID: 5518
		private int _SchemaVersionCheck;

		// Token: 0x0400158F RID: 5519
		private int _connectionCount;

		// Token: 0x04001590 RID: 5520
		private DateTime _retryDate = DateTime.MinValue;
	}
}

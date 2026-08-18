using System;
using System.Collections;
using System.Globalization;
using System.IO;
using log4net.Core;
using log4net.Filter;
using log4net.Layout;
using log4net.Util;

namespace log4net.Appender
{
	// Token: 0x02000006 RID: 6
	public abstract class AppenderSkeleton : IBulkAppender, IAppender, IOptionHandler, IFlushable
	{
		// Token: 0x06000008 RID: 8 RVA: 0x000020D0 File Offset: 0x000002D0
		protected AppenderSkeleton()
		{
			this.m_errorHandler = new OnlyOnceErrorHandler(base.GetType().Name);
		}

		// Token: 0x06000009 RID: 9 RVA: 0x000020F0 File Offset: 0x000002F0
		~AppenderSkeleton()
		{
			if (!this.m_closed)
			{
				LogLog.Debug(AppenderSkeleton.declaringType, "Finalizing appender named [" + this.m_name + "].");
				this.Close();
			}
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600000A RID: 10 RVA: 0x00002144 File Offset: 0x00000344
		// (set) Token: 0x0600000B RID: 11 RVA: 0x0000214C File Offset: 0x0000034C
		public Level Threshold
		{
			get
			{
				return this.m_threshold;
			}
			set
			{
				this.m_threshold = value;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x0600000C RID: 12 RVA: 0x00002155 File Offset: 0x00000355
		// (set) Token: 0x0600000D RID: 13 RVA: 0x00002160 File Offset: 0x00000360
		public virtual IErrorHandler ErrorHandler
		{
			get
			{
				return this.m_errorHandler;
			}
			set
			{
				lock (this)
				{
					if (value == null)
					{
						LogLog.Warn(AppenderSkeleton.declaringType, "You have tried to set a null error-handler.");
					}
					else
					{
						this.m_errorHandler = value;
					}
				}
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000E RID: 14 RVA: 0x000021B0 File Offset: 0x000003B0
		public virtual IFilter FilterHead
		{
			get
			{
				return this.m_headFilter;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x0600000F RID: 15 RVA: 0x000021B8 File Offset: 0x000003B8
		// (set) Token: 0x06000010 RID: 16 RVA: 0x000021C0 File Offset: 0x000003C0
		public virtual ILayout Layout
		{
			get
			{
				return this.m_layout;
			}
			set
			{
				this.m_layout = value;
			}
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000021C9 File Offset: 0x000003C9
		public virtual void ActivateOptions()
		{
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000012 RID: 18 RVA: 0x000021CB File Offset: 0x000003CB
		// (set) Token: 0x06000013 RID: 19 RVA: 0x000021D3 File Offset: 0x000003D3
		public string Name
		{
			get
			{
				return this.m_name;
			}
			set
			{
				this.m_name = value;
			}
		}

		// Token: 0x06000014 RID: 20 RVA: 0x000021DC File Offset: 0x000003DC
		public void Close()
		{
			lock (this)
			{
				if (!this.m_closed)
				{
					this.OnClose();
					this.m_closed = true;
				}
			}
		}

		// Token: 0x06000015 RID: 21 RVA: 0x00002228 File Offset: 0x00000428
		public void DoAppend(LoggingEvent loggingEvent)
		{
			lock (this)
			{
				if (this.m_closed)
				{
					this.ErrorHandler.Error("Attempted to append to closed appender named [" + this.m_name + "].");
				}
				else if (!this.m_recursiveGuard)
				{
					try
					{
						this.m_recursiveGuard = true;
						if (this.FilterEvent(loggingEvent) && this.PreAppendCheck())
						{
							this.Append(loggingEvent);
						}
					}
					catch (Exception e)
					{
						this.ErrorHandler.Error("Failed in DoAppend", e);
					}
					finally
					{
						this.m_recursiveGuard = false;
					}
				}
			}
		}

		// Token: 0x06000016 RID: 22 RVA: 0x000022E8 File Offset: 0x000004E8
		public void DoAppend(LoggingEvent[] loggingEvents)
		{
			lock (this)
			{
				if (this.m_closed)
				{
					this.ErrorHandler.Error("Attempted to append to closed appender named [" + this.m_name + "].");
				}
				else if (!this.m_recursiveGuard)
				{
					try
					{
						this.m_recursiveGuard = true;
						ArrayList arrayList = new ArrayList(loggingEvents.Length);
						foreach (LoggingEvent loggingEvent in loggingEvents)
						{
							if (this.FilterEvent(loggingEvent))
							{
								arrayList.Add(loggingEvent);
							}
						}
						if (arrayList.Count > 0 && this.PreAppendCheck())
						{
							this.Append((LoggingEvent[])arrayList.ToArray(typeof(LoggingEvent)));
						}
					}
					catch (Exception e)
					{
						this.ErrorHandler.Error("Failed in Bulk DoAppend", e);
					}
					finally
					{
						this.m_recursiveGuard = false;
					}
				}
			}
		}

		// Token: 0x06000017 RID: 23 RVA: 0x000023FC File Offset: 0x000005FC
		protected virtual bool FilterEvent(LoggingEvent loggingEvent)
		{
			if (!this.IsAsSevereAsThreshold(loggingEvent.Level))
			{
				return false;
			}
			IFilter filter = this.FilterHead;
			while (filter != null)
			{
				switch (filter.Decide(loggingEvent))
				{
				case FilterDecision.Deny:
					return false;
				case FilterDecision.Neutral:
					filter = filter.Next;
					break;
				case FilterDecision.Accept:
					filter = null;
					break;
				}
			}
			return true;
		}

		// Token: 0x06000018 RID: 24 RVA: 0x00002454 File Offset: 0x00000654
		public virtual void AddFilter(IFilter filter)
		{
			if (filter == null)
			{
				throw new ArgumentNullException("filter param must not be null");
			}
			if (this.m_headFilter == null)
			{
				this.m_tailFilter = filter;
				this.m_headFilter = filter;
				return;
			}
			this.m_tailFilter.Next = filter;
			this.m_tailFilter = filter;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x0000249C File Offset: 0x0000069C
		public virtual void ClearFilters()
		{
			this.m_headFilter = (this.m_tailFilter = null);
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000024B9 File Offset: 0x000006B9
		protected virtual bool IsAsSevereAsThreshold(Level level)
		{
			return this.m_threshold == null || level >= this.m_threshold;
		}

		// Token: 0x0600001B RID: 27 RVA: 0x000024D7 File Offset: 0x000006D7
		protected virtual void OnClose()
		{
		}

		// Token: 0x0600001C RID: 28
		protected abstract void Append(LoggingEvent loggingEvent);

		// Token: 0x0600001D RID: 29 RVA: 0x000024DC File Offset: 0x000006DC
		protected virtual void Append(LoggingEvent[] loggingEvents)
		{
			foreach (LoggingEvent loggingEvent in loggingEvents)
			{
				this.Append(loggingEvent);
			}
		}

		// Token: 0x0600001E RID: 30 RVA: 0x00002504 File Offset: 0x00000704
		protected virtual bool PreAppendCheck()
		{
			if (this.m_layout == null && this.RequiresLayout)
			{
				this.ErrorHandler.Error("AppenderSkeleton: No layout set for the appender named [" + this.m_name + "].");
				return false;
			}
			return true;
		}

		// Token: 0x0600001F RID: 31 RVA: 0x0000253C File Offset: 0x0000073C
		protected string RenderLoggingEvent(LoggingEvent loggingEvent)
		{
			if (this.m_renderWriter == null)
			{
				this.m_renderWriter = new ReusableStringWriter(CultureInfo.InvariantCulture);
			}
			string result;
			lock (this.m_renderWriter)
			{
				this.m_renderWriter.Reset(1024, 256);
				this.RenderLoggingEvent(this.m_renderWriter, loggingEvent);
				result = this.m_renderWriter.ToString();
			}
			return result;
		}

		// Token: 0x06000020 RID: 32 RVA: 0x000025C0 File Offset: 0x000007C0
		protected void RenderLoggingEvent(TextWriter writer, LoggingEvent loggingEvent)
		{
			if (this.m_layout == null)
			{
				throw new InvalidOperationException("A layout must be set");
			}
			if (!this.m_layout.IgnoresException)
			{
				this.m_layout.Format(writer, loggingEvent);
				return;
			}
			string exceptionString = loggingEvent.GetExceptionString();
			if (exceptionString != null && exceptionString.Length > 0)
			{
				this.m_layout.Format(writer, loggingEvent);
				writer.WriteLine(exceptionString);
				return;
			}
			this.m_layout.Format(writer, loggingEvent);
		}

		// Token: 0x17000007 RID: 7
		// (get) Token: 0x06000021 RID: 33 RVA: 0x00002630 File Offset: 0x00000830
		protected virtual bool RequiresLayout
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06000022 RID: 34 RVA: 0x00002633 File Offset: 0x00000833
		public virtual bool Flush(int millisecondsTimeout)
		{
			return true;
		}

		// Token: 0x04000001 RID: 1
		private const int c_renderBufferSize = 256;

		// Token: 0x04000002 RID: 2
		private const int c_renderBufferMaxCapacity = 1024;

		// Token: 0x04000003 RID: 3
		private ILayout m_layout;

		// Token: 0x04000004 RID: 4
		private string m_name;

		// Token: 0x04000005 RID: 5
		private Level m_threshold;

		// Token: 0x04000006 RID: 6
		private IErrorHandler m_errorHandler;

		// Token: 0x04000007 RID: 7
		private IFilter m_headFilter;

		// Token: 0x04000008 RID: 8
		private IFilter m_tailFilter;

		// Token: 0x04000009 RID: 9
		private bool m_closed;

		// Token: 0x0400000A RID: 10
		private bool m_recursiveGuard;

		// Token: 0x0400000B RID: 11
		private ReusableStringWriter m_renderWriter;

		// Token: 0x0400000C RID: 12
		private static readonly Type declaringType = typeof(AppenderSkeleton);
	}
}

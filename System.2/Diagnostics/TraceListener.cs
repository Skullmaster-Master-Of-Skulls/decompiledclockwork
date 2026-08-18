using System;
using System.Collections;
using System.Collections.Specialized;
using System.Globalization;
using System.Runtime.InteropServices;
using System.Security.Permissions;
using System.Text;

namespace System.Diagnostics
{
	// Token: 0x020004B3 RID: 1203
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public abstract class TraceListener : MarshalByRefObject, IDisposable
	{
		// Token: 0x06002CC1 RID: 11457 RVA: 0x000C9898 File Offset: 0x000C7A98
		protected TraceListener()
		{
		}

		// Token: 0x06002CC2 RID: 11458 RVA: 0x000C98AE File Offset: 0x000C7AAE
		protected TraceListener(string name)
		{
			this.listenerName = name;
		}

		// Token: 0x17000AD7 RID: 2775
		// (get) Token: 0x06002CC3 RID: 11459 RVA: 0x000C98CB File Offset: 0x000C7ACB
		public StringDictionary Attributes
		{
			get
			{
				if (this.attributes == null)
				{
					this.attributes = new StringDictionary();
				}
				return this.attributes;
			}
		}

		// Token: 0x17000AD8 RID: 2776
		// (get) Token: 0x06002CC4 RID: 11460 RVA: 0x000C98E6 File Offset: 0x000C7AE6
		// (set) Token: 0x06002CC5 RID: 11461 RVA: 0x000C98FC File Offset: 0x000C7AFC
		public virtual string Name
		{
			get
			{
				if (this.listenerName != null)
				{
					return this.listenerName;
				}
				return "";
			}
			set
			{
				this.listenerName = value;
			}
		}

		// Token: 0x17000AD9 RID: 2777
		// (get) Token: 0x06002CC6 RID: 11462 RVA: 0x000C9905 File Offset: 0x000C7B05
		public virtual bool IsThreadSafe
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06002CC7 RID: 11463 RVA: 0x000C9908 File Offset: 0x000C7B08
		public void Dispose()
		{
			this.Dispose(true);
			GC.SuppressFinalize(this);
		}

		// Token: 0x06002CC8 RID: 11464 RVA: 0x000C9917 File Offset: 0x000C7B17
		protected virtual void Dispose(bool disposing)
		{
		}

		// Token: 0x06002CC9 RID: 11465 RVA: 0x000C9919 File Offset: 0x000C7B19
		public virtual void Close()
		{
		}

		// Token: 0x06002CCA RID: 11466 RVA: 0x000C991B File Offset: 0x000C7B1B
		public virtual void Flush()
		{
		}

		// Token: 0x17000ADA RID: 2778
		// (get) Token: 0x06002CCB RID: 11467 RVA: 0x000C991D File Offset: 0x000C7B1D
		// (set) Token: 0x06002CCC RID: 11468 RVA: 0x000C9925 File Offset: 0x000C7B25
		public int IndentLevel
		{
			get
			{
				return this.indentLevel;
			}
			set
			{
				this.indentLevel = ((value < 0) ? 0 : value);
			}
		}

		// Token: 0x17000ADB RID: 2779
		// (get) Token: 0x06002CCD RID: 11469 RVA: 0x000C9935 File Offset: 0x000C7B35
		// (set) Token: 0x06002CCE RID: 11470 RVA: 0x000C993D File Offset: 0x000C7B3D
		public int IndentSize
		{
			get
			{
				return this.indentSize;
			}
			set
			{
				if (value < 0)
				{
					throw new ArgumentOutOfRangeException("IndentSize", value, SR.GetString("TraceListenerIndentSize"));
				}
				this.indentSize = value;
			}
		}

		// Token: 0x17000ADC RID: 2780
		// (get) Token: 0x06002CCF RID: 11471 RVA: 0x000C9965 File Offset: 0x000C7B65
		// (set) Token: 0x06002CD0 RID: 11472 RVA: 0x000C996D File Offset: 0x000C7B6D
		[ComVisible(false)]
		public TraceFilter Filter
		{
			get
			{
				return this.filter;
			}
			set
			{
				this.filter = value;
			}
		}

		// Token: 0x17000ADD RID: 2781
		// (get) Token: 0x06002CD1 RID: 11473 RVA: 0x000C9976 File Offset: 0x000C7B76
		// (set) Token: 0x06002CD2 RID: 11474 RVA: 0x000C997E File Offset: 0x000C7B7E
		protected bool NeedIndent
		{
			get
			{
				return this.needIndent;
			}
			set
			{
				this.needIndent = value;
			}
		}

		// Token: 0x17000ADE RID: 2782
		// (get) Token: 0x06002CD3 RID: 11475 RVA: 0x000C9987 File Offset: 0x000C7B87
		// (set) Token: 0x06002CD4 RID: 11476 RVA: 0x000C998F File Offset: 0x000C7B8F
		[ComVisible(false)]
		public TraceOptions TraceOutputOptions
		{
			get
			{
				return this.traceOptions;
			}
			set
			{
				if (value >> 6 != TraceOptions.None)
				{
					throw new ArgumentOutOfRangeException("value");
				}
				this.traceOptions = value;
			}
		}

		// Token: 0x06002CD5 RID: 11477 RVA: 0x000C99A8 File Offset: 0x000C7BA8
		internal void SetAttributes(Hashtable attribs)
		{
			TraceUtils.VerifyAttributes(attribs, this.GetSupportedAttributes(), this);
			this.attributes = new StringDictionary();
			this.attributes.ReplaceHashtable(attribs);
		}

		// Token: 0x06002CD6 RID: 11478 RVA: 0x000C99CE File Offset: 0x000C7BCE
		public virtual void Fail(string message)
		{
			this.Fail(message, null);
		}

		// Token: 0x06002CD7 RID: 11479 RVA: 0x000C99D8 File Offset: 0x000C7BD8
		public virtual void Fail(string message, string detailMessage)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(SR.GetString("TraceListenerFail"));
			stringBuilder.Append(" ");
			stringBuilder.Append(message);
			if (detailMessage != null)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(detailMessage);
			}
			this.WriteLine(stringBuilder.ToString());
		}

		// Token: 0x06002CD8 RID: 11480 RVA: 0x000C9A33 File Offset: 0x000C7C33
		protected internal virtual string[] GetSupportedAttributes()
		{
			return null;
		}

		// Token: 0x06002CD9 RID: 11481
		public abstract void Write(string message);

		// Token: 0x06002CDA RID: 11482 RVA: 0x000C9A36 File Offset: 0x000C7C36
		public virtual void Write(object o)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(null, "", TraceEventType.Verbose, 0, null, null, o))
			{
				return;
			}
			if (o == null)
			{
				return;
			}
			this.Write(o.ToString());
		}

		// Token: 0x06002CDB RID: 11483 RVA: 0x000C9A6C File Offset: 0x000C7C6C
		public virtual void Write(string message, string category)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(null, "", TraceEventType.Verbose, 0, message))
			{
				return;
			}
			if (category == null)
			{
				this.Write(message);
				return;
			}
			this.Write(category + ": " + ((message == null) ? string.Empty : message));
		}

		// Token: 0x06002CDC RID: 11484 RVA: 0x000C9AC0 File Offset: 0x000C7CC0
		public virtual void Write(object o, string category)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(null, "", TraceEventType.Verbose, 0, category, null, o))
			{
				return;
			}
			if (category == null)
			{
				this.Write(o);
				return;
			}
			this.Write((o == null) ? "" : o.ToString(), category);
		}

		// Token: 0x06002CDD RID: 11485 RVA: 0x000C9B14 File Offset: 0x000C7D14
		protected virtual void WriteIndent()
		{
			this.NeedIndent = false;
			for (int i = 0; i < this.indentLevel; i++)
			{
				if (this.indentSize == 4)
				{
					this.Write("    ");
				}
				else
				{
					for (int j = 0; j < this.indentSize; j++)
					{
						this.Write(" ");
					}
				}
			}
		}

		// Token: 0x06002CDE RID: 11486
		public abstract void WriteLine(string message);

		// Token: 0x06002CDF RID: 11487 RVA: 0x000C9B6B File Offset: 0x000C7D6B
		public virtual void WriteLine(object o)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(null, "", TraceEventType.Verbose, 0, null, null, o))
			{
				return;
			}
			this.WriteLine((o == null) ? "" : o.ToString());
		}

		// Token: 0x06002CE0 RID: 11488 RVA: 0x000C9BA8 File Offset: 0x000C7DA8
		public virtual void WriteLine(string message, string category)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(null, "", TraceEventType.Verbose, 0, message))
			{
				return;
			}
			if (category == null)
			{
				this.WriteLine(message);
				return;
			}
			this.WriteLine(category + ": " + ((message == null) ? string.Empty : message));
		}

		// Token: 0x06002CE1 RID: 11489 RVA: 0x000C9BFC File Offset: 0x000C7DFC
		public virtual void WriteLine(object o, string category)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(null, "", TraceEventType.Verbose, 0, category, null, o))
			{
				return;
			}
			this.WriteLine((o == null) ? "" : o.ToString(), category);
		}

		// Token: 0x06002CE2 RID: 11490 RVA: 0x000C9C38 File Offset: 0x000C7E38
		[ComVisible(false)]
		public virtual void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data))
			{
				return;
			}
			this.WriteHeader(source, eventType, id);
			string message = string.Empty;
			if (data != null)
			{
				message = data.ToString();
			}
			this.WriteLine(message);
			this.WriteFooter(eventCache);
		}

		// Token: 0x06002CE3 RID: 11491 RVA: 0x000C9C90 File Offset: 0x000C7E90
		[ComVisible(false)]
		public virtual void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, data))
			{
				return;
			}
			this.WriteHeader(source, eventType, id);
			StringBuilder stringBuilder = new StringBuilder();
			if (data != null)
			{
				for (int i = 0; i < data.Length; i++)
				{
					if (i != 0)
					{
						stringBuilder.Append(", ");
					}
					if (data[i] != null)
					{
						stringBuilder.Append(data[i].ToString());
					}
				}
			}
			this.WriteLine(stringBuilder.ToString());
			this.WriteFooter(eventCache);
		}

		// Token: 0x06002CE4 RID: 11492 RVA: 0x000C9D18 File Offset: 0x000C7F18
		[ComVisible(false)]
		public virtual void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
		{
			this.TraceEvent(eventCache, source, eventType, id, string.Empty);
		}

		// Token: 0x06002CE5 RID: 11493 RVA: 0x000C9D2A File Offset: 0x000C7F2A
		[ComVisible(false)]
		public virtual void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(eventCache, source, eventType, id, message))
			{
				return;
			}
			this.WriteHeader(source, eventType, id);
			this.WriteLine(message);
			this.WriteFooter(eventCache);
		}

		// Token: 0x06002CE6 RID: 11494 RVA: 0x000C9D64 File Offset: 0x000C7F64
		[ComVisible(false)]
		public virtual void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
		{
			if (this.Filter != null && !this.Filter.ShouldTrace(eventCache, source, eventType, id, format, args))
			{
				return;
			}
			this.WriteHeader(source, eventType, id);
			if (args != null)
			{
				this.WriteLine(string.Format(CultureInfo.InvariantCulture, format, args));
			}
			else
			{
				this.WriteLine(format);
			}
			this.WriteFooter(eventCache);
		}

		// Token: 0x06002CE7 RID: 11495 RVA: 0x000C9DC3 File Offset: 0x000C7FC3
		[ComVisible(false)]
		public virtual void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
		{
			this.TraceEvent(eventCache, source, TraceEventType.Transfer, id, message + ", relatedActivityId=" + relatedActivityId.ToString());
		}

		// Token: 0x06002CE8 RID: 11496 RVA: 0x000C9DEC File Offset: 0x000C7FEC
		private void WriteHeader(string source, TraceEventType eventType, int id)
		{
			this.Write(string.Format(CultureInfo.InvariantCulture, "{0} {1}: {2} : ", new object[]
			{
				source,
				eventType.ToString(),
				id.ToString(CultureInfo.InvariantCulture)
			}));
		}

		// Token: 0x06002CE9 RID: 11497 RVA: 0x000C9E2C File Offset: 0x000C802C
		private void WriteFooter(TraceEventCache eventCache)
		{
			if (eventCache == null)
			{
				return;
			}
			this.indentLevel++;
			if (this.IsEnabled(TraceOptions.ProcessId))
			{
				this.WriteLine("ProcessId=" + eventCache.ProcessId.ToString());
			}
			if (this.IsEnabled(TraceOptions.LogicalOperationStack))
			{
				this.Write("LogicalOperationStack=");
				Stack logicalOperationStack = eventCache.LogicalOperationStack;
				bool flag = true;
				foreach (object obj in logicalOperationStack)
				{
					if (!flag)
					{
						this.Write(", ");
					}
					else
					{
						flag = false;
					}
					this.Write(obj.ToString());
				}
				this.WriteLine(string.Empty);
			}
			if (this.IsEnabled(TraceOptions.ThreadId))
			{
				this.WriteLine("ThreadId=" + eventCache.ThreadId);
			}
			if (this.IsEnabled(TraceOptions.DateTime))
			{
				this.WriteLine("DateTime=" + eventCache.DateTime.ToString("o", CultureInfo.InvariantCulture));
			}
			if (this.IsEnabled(TraceOptions.Timestamp))
			{
				this.WriteLine("Timestamp=" + eventCache.Timestamp.ToString());
			}
			if (this.IsEnabled(TraceOptions.Callstack))
			{
				this.WriteLine("Callstack=" + eventCache.Callstack);
			}
			this.indentLevel--;
		}

		// Token: 0x06002CEA RID: 11498 RVA: 0x000C9FA0 File Offset: 0x000C81A0
		internal bool IsEnabled(TraceOptions opts)
		{
			return (opts & this.TraceOutputOptions) > TraceOptions.None;
		}

		// Token: 0x040026F1 RID: 9969
		private int indentLevel;

		// Token: 0x040026F2 RID: 9970
		private int indentSize = 4;

		// Token: 0x040026F3 RID: 9971
		private TraceOptions traceOptions;

		// Token: 0x040026F4 RID: 9972
		private bool needIndent = true;

		// Token: 0x040026F5 RID: 9973
		private string listenerName;

		// Token: 0x040026F6 RID: 9974
		private TraceFilter filter;

		// Token: 0x040026F7 RID: 9975
		private StringDictionary attributes;

		// Token: 0x040026F8 RID: 9976
		internal string initializeData;
	}
}

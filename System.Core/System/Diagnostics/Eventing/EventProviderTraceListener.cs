using System;
using System.Globalization;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Diagnostics.Eventing
{
	// Token: 0x020002A9 RID: 681
	[HostProtection(SecurityAction.LinkDemand, MayLeakOnAbort = true)]
	public class EventProviderTraceListener : TraceListener
	{
		// Token: 0x17000451 RID: 1105
		// (get) Token: 0x060018AC RID: 6316 RVA: 0x0005A580 File Offset: 0x00058780
		// (set) Token: 0x060018AD RID: 6317 RVA: 0x0005A634 File Offset: 0x00058834
		public string Delimiter
		{
			get
			{
				if (this.m_initializedDelim == 0)
				{
					object @lock = this.m_Lock;
					lock (@lock)
					{
						if (this.m_initializedDelim == 0)
						{
							if (base.Attributes.ContainsKey("delimiter"))
							{
								this.m_delimiter = base.Attributes["delimiter"];
							}
							this.m_initializedDelim = 1;
						}
					}
					if (this.m_delimiter == null)
					{
						throw new ArgumentNullException("Delimiter");
					}
					if (this.m_delimiter.Length == 0)
					{
						throw new ArgumentException(SR.GetString("Argument_NeedNonemptyDelimiter"));
					}
				}
				return this.m_delimiter;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Delimiter");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException(SR.GetString("Argument_NeedNonemptyDelimiter"));
				}
				object @lock = this.m_Lock;
				lock (@lock)
				{
					this.m_delimiter = value;
					this.m_initializedDelim = 1;
				}
			}
		}

		// Token: 0x060018AE RID: 6318 RVA: 0x0005A6A4 File Offset: 0x000588A4
		protected override string[] GetSupportedAttributes()
		{
			return new string[]
			{
				"delimiter"
			};
		}

		// Token: 0x060018AF RID: 6319 RVA: 0x0005A6B4 File Offset: 0x000588B4
		public EventProviderTraceListener(string providerId)
		{
			this.InitProvider(providerId);
		}

		// Token: 0x060018B0 RID: 6320 RVA: 0x0005A6D9 File Offset: 0x000588D9
		public EventProviderTraceListener(string providerId, string name) : base(name)
		{
			this.InitProvider(providerId);
		}

		// Token: 0x060018B1 RID: 6321 RVA: 0x0005A700 File Offset: 0x00058900
		public EventProviderTraceListener(string providerId, string name, string delimiter) : base(name)
		{
			if (delimiter == null)
			{
				throw new ArgumentNullException("delimiter");
			}
			if (delimiter.Length == 0)
			{
				throw new ArgumentException(SR.GetString("Argument_NeedNonemptyDelimiter"));
			}
			this.m_delimiter = delimiter;
			this.m_initializedDelim = 1;
			this.InitProvider(providerId);
		}

		// Token: 0x060018B2 RID: 6322 RVA: 0x0005A768 File Offset: 0x00058968
		private void InitProvider(string providerId)
		{
			Guid providerGuid = new Guid(providerId);
			this.m_provider = new EventProvider(providerGuid);
		}

		// Token: 0x060018B3 RID: 6323 RVA: 0x0005A789 File Offset: 0x00058989
		public sealed override void Flush()
		{
		}

		// Token: 0x17000452 RID: 1106
		// (get) Token: 0x060018B4 RID: 6324 RVA: 0x0005A78B File Offset: 0x0005898B
		public sealed override bool IsThreadSafe
		{
			get
			{
				return true;
			}
		}

		// Token: 0x060018B5 RID: 6325 RVA: 0x0005A78E File Offset: 0x0005898E
		public override void Close()
		{
			this.m_provider.Close();
		}

		// Token: 0x060018B6 RID: 6326 RVA: 0x0005A79B File Offset: 0x0005899B
		public sealed override void Write(string message)
		{
			if (!this.m_provider.IsEnabled())
			{
				return;
			}
			this.m_provider.WriteMessageEvent(message, 8, 0L);
		}

		// Token: 0x060018B7 RID: 6327 RVA: 0x0005A7BB File Offset: 0x000589BB
		public sealed override void WriteLine(string message)
		{
			this.Write(message);
		}

		// Token: 0x060018B8 RID: 6328 RVA: 0x0005A7C4 File Offset: 0x000589C4
		public sealed override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			if (!this.m_provider.IsEnabled())
			{
				return;
			}
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, null))
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(512);
			if (data != null)
			{
				stringBuilder.Append(data.ToString());
			}
			else
			{
				stringBuilder.Append(": null");
			}
			if (eventCache != null && (base.TraceOutputOptions & TraceOptions.Callstack) != TraceOptions.None)
			{
				stringBuilder.Append(" : CallStack:");
				stringBuilder.Append(eventCache.Callstack);
				this.m_provider.WriteMessageEvent(stringBuilder.ToString(), (byte)eventType, (long)eventType & (long)((ulong)-256));
				return;
			}
			this.m_provider.WriteMessageEvent(stringBuilder.ToString(), (byte)eventType, (long)eventType & (long)((ulong)-256));
		}

		// Token: 0x060018B9 RID: 6329 RVA: 0x0005A88C File Offset: 0x00058A8C
		public sealed override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			if (!this.m_provider.IsEnabled())
			{
				return;
			}
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, null))
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(512);
			if (data != null && data.Length != 0)
			{
				int i;
				for (i = 0; i < data.Length - 1; i++)
				{
					if (data[i] != null)
					{
						stringBuilder.Append(data[i].ToString());
						stringBuilder.Append(this.Delimiter);
					}
					else
					{
						stringBuilder.Append("null,");
					}
				}
				if (data[i] != null)
				{
					stringBuilder.Append(data[i].ToString());
				}
				else
				{
					stringBuilder.Append("null");
				}
			}
			else
			{
				stringBuilder.Append("null");
			}
			if (eventCache != null && (base.TraceOutputOptions & TraceOptions.Callstack) != TraceOptions.None)
			{
				stringBuilder.Append(" : CallStack:");
				stringBuilder.Append(eventCache.Callstack);
				this.m_provider.WriteMessageEvent(stringBuilder.ToString(), (byte)eventType, (long)eventType & (long)((ulong)-256));
				return;
			}
			this.m_provider.WriteMessageEvent(stringBuilder.ToString(), (byte)eventType, (long)eventType & (long)((ulong)-256));
		}

		// Token: 0x060018BA RID: 6330 RVA: 0x0005A9B0 File Offset: 0x00058BB0
		public sealed override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id)
		{
			if (!this.m_provider.IsEnabled())
			{
				return;
			}
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, null))
			{
				return;
			}
			if (eventCache != null && (base.TraceOutputOptions & TraceOptions.Callstack) != TraceOptions.None)
			{
				this.m_provider.WriteMessageEvent(" : CallStack:" + eventCache.Callstack, (byte)eventType, (long)eventType & (long)((ulong)-256));
				return;
			}
			this.m_provider.WriteMessageEvent(string.Empty, (byte)eventType, (long)eventType & (long)((ulong)-256));
		}

		// Token: 0x060018BB RID: 6331 RVA: 0x0005AA3C File Offset: 0x00058C3C
		public sealed override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			if (!this.m_provider.IsEnabled())
			{
				return;
			}
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, null))
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(512);
			stringBuilder.Append(message);
			if (eventCache != null && (base.TraceOutputOptions & TraceOptions.Callstack) != TraceOptions.None)
			{
				stringBuilder.Append(" : CallStack:");
				stringBuilder.Append(eventCache.Callstack);
				this.m_provider.WriteMessageEvent(stringBuilder.ToString(), (byte)eventType, (long)eventType & (long)((ulong)-256));
				return;
			}
			this.m_provider.WriteMessageEvent(stringBuilder.ToString(), (byte)eventType, (long)eventType & (long)((ulong)-256));
		}

		// Token: 0x060018BC RID: 6332 RVA: 0x0005AAEC File Offset: 0x00058CEC
		public sealed override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
		{
			if (!this.m_provider.IsEnabled())
			{
				return;
			}
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, null))
			{
				return;
			}
			if (args == null)
			{
				if (eventCache != null && (base.TraceOutputOptions & TraceOptions.Callstack) != TraceOptions.None)
				{
					this.m_provider.WriteMessageEvent(format + " : CallStack:" + eventCache.Callstack, (byte)eventType, (long)eventType & (long)((ulong)-256));
					return;
				}
				this.m_provider.WriteMessageEvent(format, (byte)eventType, (long)eventType & (long)((ulong)-256));
				return;
			}
			else
			{
				if (eventCache != null && (base.TraceOutputOptions & TraceOptions.Callstack) != TraceOptions.None)
				{
					this.m_provider.WriteMessageEvent(string.Format(CultureInfo.InvariantCulture, format, args) + " : CallStack:" + eventCache.Callstack, (byte)eventType, (long)eventType & (long)((ulong)-256));
					return;
				}
				this.m_provider.WriteMessageEvent(string.Format(CultureInfo.InvariantCulture, format, args), (byte)eventType, (long)eventType & (long)((ulong)-256));
				return;
			}
		}

		// Token: 0x060018BD RID: 6333 RVA: 0x0005ABE8 File Offset: 0x00058DE8
		public override void Fail(string message, string detailMessage)
		{
			StringBuilder stringBuilder = new StringBuilder(message);
			if (detailMessage != null)
			{
				stringBuilder.Append(" ");
				stringBuilder.Append(detailMessage);
			}
			this.TraceEvent(null, null, TraceEventType.Error, 0, stringBuilder.ToString());
		}

		// Token: 0x060018BE RID: 6334 RVA: 0x0005AC24 File Offset: 0x00058E24
		[SecurityCritical]
		public sealed override void TraceTransfer(TraceEventCache eventCache, string source, int id, string message, Guid relatedActivityId)
		{
			if (!this.m_provider.IsEnabled())
			{
				return;
			}
			StringBuilder stringBuilder = new StringBuilder(512);
			object obj = Trace.CorrelationManager.ActivityId;
			if (obj != null)
			{
				Guid guid = (Guid)obj;
				stringBuilder.Append("activityId=");
				stringBuilder.Append(guid.ToString());
				stringBuilder.Append(this.Delimiter);
			}
			stringBuilder.Append("relatedActivityId=");
			stringBuilder.Append(relatedActivityId.ToString());
			stringBuilder.Append(this.Delimiter + message);
			if (eventCache != null && (base.TraceOutputOptions & TraceOptions.Callstack) != TraceOptions.None)
			{
				stringBuilder.Append(" : CallStack:");
				stringBuilder.Append(eventCache.Callstack);
				this.m_provider.WriteMessageEvent(stringBuilder.ToString(), 0, 4096L);
				return;
			}
			this.m_provider.WriteMessageEvent(stringBuilder.ToString(), 0, 4096L);
		}

		// Token: 0x04000C0D RID: 3085
		private EventProvider m_provider;

		// Token: 0x04000C0E RID: 3086
		private const string s_nullStringValue = "null";

		// Token: 0x04000C0F RID: 3087
		private const string s_nullStringComaValue = "null,";

		// Token: 0x04000C10 RID: 3088
		private const string s_nullCStringValue = ": null";

		// Token: 0x04000C11 RID: 3089
		private const string s_activityIdString = "activityId=";

		// Token: 0x04000C12 RID: 3090
		private const string s_relatedActivityIdString = "relatedActivityId=";

		// Token: 0x04000C13 RID: 3091
		private const string s_callStackString = " : CallStack:";

		// Token: 0x04000C14 RID: 3092
		private const string s_optionDelimiter = "delimiter";

		// Token: 0x04000C15 RID: 3093
		private string m_delimiter = ";";

		// Token: 0x04000C16 RID: 3094
		private int m_initializedDelim;

		// Token: 0x04000C17 RID: 3095
		private const uint s_keyWordMask = 4294967040U;

		// Token: 0x04000C18 RID: 3096
		private const int s_defaultPayloadSize = 512;

		// Token: 0x04000C19 RID: 3097
		private object m_Lock = new object();
	}
}

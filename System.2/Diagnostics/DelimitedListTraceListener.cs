using System;
using System.Collections;
using System.Globalization;
using System.IO;
using System.Security.Permissions;
using System.Text;

namespace System.Diagnostics
{
	// Token: 0x02000498 RID: 1176
	[HostProtection(SecurityAction.LinkDemand, Synchronization = true)]
	public class DelimitedListTraceListener : TextWriterTraceListener
	{
		// Token: 0x06002BA7 RID: 11175 RVA: 0x000C5890 File Offset: 0x000C3A90
		public DelimitedListTraceListener(Stream stream) : base(stream)
		{
		}

		// Token: 0x06002BA8 RID: 11176 RVA: 0x000C58AF File Offset: 0x000C3AAF
		public DelimitedListTraceListener(Stream stream, string name) : base(stream, name)
		{
		}

		// Token: 0x06002BA9 RID: 11177 RVA: 0x000C58CF File Offset: 0x000C3ACF
		public DelimitedListTraceListener(TextWriter writer) : base(writer)
		{
		}

		// Token: 0x06002BAA RID: 11178 RVA: 0x000C58EE File Offset: 0x000C3AEE
		public DelimitedListTraceListener(TextWriter writer, string name) : base(writer, name)
		{
		}

		// Token: 0x06002BAB RID: 11179 RVA: 0x000C590E File Offset: 0x000C3B0E
		public DelimitedListTraceListener(string fileName) : base(fileName)
		{
		}

		// Token: 0x06002BAC RID: 11180 RVA: 0x000C592D File Offset: 0x000C3B2D
		public DelimitedListTraceListener(string fileName, string name) : base(fileName, name)
		{
		}

		// Token: 0x17000A89 RID: 2697
		// (get) Token: 0x06002BAD RID: 11181 RVA: 0x000C5950 File Offset: 0x000C3B50
		// (set) Token: 0x06002BAE RID: 11182 RVA: 0x000C59C4 File Offset: 0x000C3BC4
		public string Delimiter
		{
			get
			{
				lock (this)
				{
					if (!this.initializedDelim)
					{
						if (base.Attributes.ContainsKey("delimiter"))
						{
							this.delimiter = base.Attributes["delimiter"];
						}
						this.initializedDelim = true;
					}
				}
				return this.delimiter;
			}
			set
			{
				if (value == null)
				{
					throw new ArgumentNullException("Delimiter");
				}
				if (value.Length == 0)
				{
					throw new ArgumentException(SR.GetString("Generic_ArgCantBeEmptyString", new object[]
					{
						"Delimiter"
					}));
				}
				lock (this)
				{
					this.delimiter = value;
					this.initializedDelim = true;
				}
				if (this.delimiter == ",")
				{
					this.secondaryDelim = ";";
					return;
				}
				this.secondaryDelim = ",";
			}
		}

		// Token: 0x06002BAF RID: 11183 RVA: 0x000C5A64 File Offset: 0x000C3C64
		protected internal override string[] GetSupportedAttributes()
		{
			return new string[]
			{
				"delimiter"
			};
		}

		// Token: 0x06002BB0 RID: 11184 RVA: 0x000C5A74 File Offset: 0x000C3C74
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string format, params object[] args)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, format, args))
			{
				return;
			}
			this.WriteHeader(source, eventType, id);
			if (args != null)
			{
				this.WriteEscaped(string.Format(CultureInfo.InvariantCulture, format, args));
			}
			else
			{
				this.WriteEscaped(format);
			}
			this.Write(this.Delimiter);
			this.Write(this.Delimiter);
			this.WriteFooter(eventCache);
		}

		// Token: 0x06002BB1 RID: 11185 RVA: 0x000C5AEC File Offset: 0x000C3CEC
		public override void TraceEvent(TraceEventCache eventCache, string source, TraceEventType eventType, int id, string message)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, message))
			{
				return;
			}
			this.WriteHeader(source, eventType, id);
			this.WriteEscaped(message);
			this.Write(this.Delimiter);
			this.Write(this.Delimiter);
			this.WriteFooter(eventCache);
		}

		// Token: 0x06002BB2 RID: 11186 RVA: 0x000C5B48 File Offset: 0x000C3D48
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, object data)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, data))
			{
				return;
			}
			this.WriteHeader(source, eventType, id);
			this.Write(this.Delimiter);
			this.WriteEscaped(data.ToString());
			this.Write(this.Delimiter);
			this.WriteFooter(eventCache);
		}

		// Token: 0x06002BB3 RID: 11187 RVA: 0x000C5BAC File Offset: 0x000C3DAC
		public override void TraceData(TraceEventCache eventCache, string source, TraceEventType eventType, int id, params object[] data)
		{
			if (base.Filter != null && !base.Filter.ShouldTrace(eventCache, source, eventType, id, null, null, null, data))
			{
				return;
			}
			this.WriteHeader(source, eventType, id);
			this.Write(this.Delimiter);
			if (data != null)
			{
				for (int i = 0; i < data.Length; i++)
				{
					if (i != 0)
					{
						this.Write(this.secondaryDelim);
					}
					this.WriteEscaped(data[i].ToString());
				}
			}
			this.Write(this.Delimiter);
			this.WriteFooter(eventCache);
		}

		// Token: 0x06002BB4 RID: 11188 RVA: 0x000C5C34 File Offset: 0x000C3E34
		private void WriteHeader(string source, TraceEventType eventType, int id)
		{
			this.WriteEscaped(source);
			this.Write(this.Delimiter);
			this.Write(eventType.ToString());
			this.Write(this.Delimiter);
			this.Write(id.ToString(CultureInfo.InvariantCulture));
			this.Write(this.Delimiter);
		}

		// Token: 0x06002BB5 RID: 11189 RVA: 0x000C5C94 File Offset: 0x000C3E94
		private void WriteFooter(TraceEventCache eventCache)
		{
			if (eventCache != null)
			{
				if (base.IsEnabled(TraceOptions.ProcessId))
				{
					this.Write(eventCache.ProcessId.ToString(CultureInfo.InvariantCulture));
				}
				this.Write(this.Delimiter);
				if (base.IsEnabled(TraceOptions.LogicalOperationStack))
				{
					this.WriteStackEscaped(eventCache.LogicalOperationStack);
				}
				this.Write(this.Delimiter);
				if (base.IsEnabled(TraceOptions.ThreadId))
				{
					this.WriteEscaped(eventCache.ThreadId.ToString(CultureInfo.InvariantCulture));
				}
				this.Write(this.Delimiter);
				if (base.IsEnabled(TraceOptions.DateTime))
				{
					this.WriteEscaped(eventCache.DateTime.ToString("o", CultureInfo.InvariantCulture));
				}
				this.Write(this.Delimiter);
				if (base.IsEnabled(TraceOptions.Timestamp))
				{
					this.Write(eventCache.Timestamp.ToString(CultureInfo.InvariantCulture));
				}
				this.Write(this.Delimiter);
				if (base.IsEnabled(TraceOptions.Callstack))
				{
					this.WriteEscaped(eventCache.Callstack);
				}
			}
			else
			{
				for (int i = 0; i < 5; i++)
				{
					this.Write(this.Delimiter);
				}
			}
			this.WriteLine("");
		}

		// Token: 0x06002BB6 RID: 11190 RVA: 0x000C5DC0 File Offset: 0x000C3FC0
		private void WriteEscaped(string message)
		{
			if (!string.IsNullOrEmpty(message))
			{
				StringBuilder stringBuilder = new StringBuilder("\"");
				int num = 0;
				int num2;
				while ((num2 = message.IndexOf('"', num)) != -1)
				{
					stringBuilder.Append(message, num, num2 - num);
					stringBuilder.Append("\"\"");
					num = num2 + 1;
				}
				stringBuilder.Append(message, num, message.Length - num);
				stringBuilder.Append("\"");
				this.Write(stringBuilder.ToString());
			}
		}

		// Token: 0x06002BB7 RID: 11191 RVA: 0x000C5E38 File Offset: 0x000C4038
		private void WriteStackEscaped(Stack stack)
		{
			StringBuilder stringBuilder = new StringBuilder("\"");
			bool flag = true;
			foreach (object obj in stack)
			{
				if (!flag)
				{
					stringBuilder.Append(", ");
				}
				else
				{
					flag = false;
				}
				string text = obj.ToString();
				int num = 0;
				int num2;
				while ((num2 = text.IndexOf('"', num)) != -1)
				{
					stringBuilder.Append(text, num, num2 - num);
					stringBuilder.Append("\"\"");
					num = num2 + 1;
				}
				stringBuilder.Append(text, num, text.Length - num);
			}
			stringBuilder.Append("\"");
			this.Write(stringBuilder.ToString());
		}

		// Token: 0x04002690 RID: 9872
		private string delimiter = ";";

		// Token: 0x04002691 RID: 9873
		private string secondaryDelim = ",";

		// Token: 0x04002692 RID: 9874
		private bool initializedDelim;
	}
}

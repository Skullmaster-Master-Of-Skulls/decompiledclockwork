using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text;
using NLog.Common;
using NLog.Config;
using NLog.Internal;

namespace NLog.LayoutRenderers
{
	// Token: 0x020000CF RID: 207
	[ThreadAgnostic]
	[LayoutRenderer("exception")]
	public class ExceptionLayoutRenderer : LayoutRenderer
	{
		// Token: 0x0600060A RID: 1546 RVA: 0x0000D6B4 File Offset: 0x0000B8B4
		public ExceptionLayoutRenderer()
		{
			this.Format = "message";
			this.Separator = " ";
			this.InnerExceptionSeparator = EnvironmentHelper.NewLine;
			this.MaxInnerExceptionLevel = 0;
			this._renderingfunctions = new Dictionary<ExceptionRenderingFormat, Action<StringBuilder, Exception>>
			{
				{
					ExceptionRenderingFormat.Message,
					new Action<StringBuilder, Exception>(this.AppendMessage)
				},
				{
					ExceptionRenderingFormat.Type,
					new Action<StringBuilder, Exception>(this.AppendType)
				},
				{
					ExceptionRenderingFormat.ShortType,
					new Action<StringBuilder, Exception>(this.AppendShortType)
				},
				{
					ExceptionRenderingFormat.ToString,
					new Action<StringBuilder, Exception>(this.AppendToString)
				},
				{
					ExceptionRenderingFormat.Method,
					new Action<StringBuilder, Exception>(this.AppendMethod)
				},
				{
					ExceptionRenderingFormat.StackTrace,
					new Action<StringBuilder, Exception>(this.AppendStackTrace)
				},
				{
					ExceptionRenderingFormat.Data,
					new Action<StringBuilder, Exception>(this.AppendData)
				}
			};
		}

		// Token: 0x170000E7 RID: 231
		// (get) Token: 0x0600060B RID: 1547 RVA: 0x0000D793 File Offset: 0x0000B993
		// (set) Token: 0x0600060C RID: 1548 RVA: 0x0000D79B File Offset: 0x0000B99B
		[DefaultParameter]
		public string Format
		{
			get
			{
				return this.format;
			}
			set
			{
				this.format = value;
				this.Formats = ExceptionLayoutRenderer.CompileFormat(value);
			}
		}

		// Token: 0x170000E8 RID: 232
		// (get) Token: 0x0600060D RID: 1549 RVA: 0x0000D7B0 File Offset: 0x0000B9B0
		// (set) Token: 0x0600060E RID: 1550 RVA: 0x0000D7B8 File Offset: 0x0000B9B8
		public string InnerFormat
		{
			get
			{
				return this.innerFormat;
			}
			set
			{
				this.innerFormat = value;
				this.InnerFormats = ExceptionLayoutRenderer.CompileFormat(value);
			}
		}

		// Token: 0x170000E9 RID: 233
		// (get) Token: 0x0600060F RID: 1551 RVA: 0x0000D7CD File Offset: 0x0000B9CD
		// (set) Token: 0x06000610 RID: 1552 RVA: 0x0000D7D5 File Offset: 0x0000B9D5
		[DefaultValue(" ")]
		public string Separator { get; set; }

		// Token: 0x170000EA RID: 234
		// (get) Token: 0x06000611 RID: 1553 RVA: 0x0000D7DE File Offset: 0x0000B9DE
		// (set) Token: 0x06000612 RID: 1554 RVA: 0x0000D7E6 File Offset: 0x0000B9E6
		[DefaultValue(0)]
		public int MaxInnerExceptionLevel { get; set; }

		// Token: 0x170000EB RID: 235
		// (get) Token: 0x06000613 RID: 1555 RVA: 0x0000D7EF File Offset: 0x0000B9EF
		// (set) Token: 0x06000614 RID: 1556 RVA: 0x0000D7F7 File Offset: 0x0000B9F7
		public string InnerExceptionSeparator { get; set; }

		// Token: 0x170000EC RID: 236
		// (get) Token: 0x06000615 RID: 1557 RVA: 0x0000D800 File Offset: 0x0000BA00
		// (set) Token: 0x06000616 RID: 1558 RVA: 0x0000D808 File Offset: 0x0000BA08
		public List<ExceptionRenderingFormat> Formats { get; private set; }

		// Token: 0x170000ED RID: 237
		// (get) Token: 0x06000617 RID: 1559 RVA: 0x0000D811 File Offset: 0x0000BA11
		// (set) Token: 0x06000618 RID: 1560 RVA: 0x0000D819 File Offset: 0x0000BA19
		public List<ExceptionRenderingFormat> InnerFormats { get; private set; }

		// Token: 0x06000619 RID: 1561 RVA: 0x0000D824 File Offset: 0x0000BA24
		protected override void Append(StringBuilder builder, LogEventInfo logEvent)
		{
			if (logEvent.Exception != null)
			{
				StringBuilder stringBuilder = new StringBuilder(128);
				string value = string.Empty;
				foreach (ExceptionRenderingFormat key in this.Formats)
				{
					stringBuilder.Append(value);
					Action<StringBuilder, Exception> action = this._renderingfunctions[key];
					action(stringBuilder, logEvent.Exception);
					value = this.Separator;
				}
				Exception innerException = logEvent.Exception.InnerException;
				int num = 0;
				while (innerException != null && num < this.MaxInnerExceptionLevel)
				{
					stringBuilder.Append(this.InnerExceptionSeparator);
					value = string.Empty;
					foreach (ExceptionRenderingFormat key2 in (this.InnerFormats ?? this.Formats))
					{
						stringBuilder.Append(value);
						Action<StringBuilder, Exception> action2 = this._renderingfunctions[key2];
						action2(stringBuilder, innerException);
						value = this.Separator;
					}
					innerException = innerException.InnerException;
					num++;
				}
				builder.Append(stringBuilder.ToString());
			}
		}

		// Token: 0x0600061A RID: 1562 RVA: 0x0000D97C File Offset: 0x0000BB7C
		protected virtual void AppendMessage(StringBuilder sb, Exception ex)
		{
			try
			{
				sb.Append(ex.Message);
			}
			catch (Exception ex2)
			{
				string text = string.Format("Exception in {0}.AppendMessage(): {1}.", typeof(ExceptionLayoutRenderer).FullName, ex2.GetType().FullName);
				sb.Append("NLog message:" + text);
				if (InternalLogger.IsWarnEnabled)
				{
					InternalLogger.Warn(text);
				}
			}
		}

		// Token: 0x0600061B RID: 1563 RVA: 0x0000D9F0 File Offset: 0x0000BBF0
		protected virtual void AppendMethod(StringBuilder sb, Exception ex)
		{
			if (ex.TargetSite != null)
			{
				sb.Append(ex.TargetSite.ToString());
			}
		}

		// Token: 0x0600061C RID: 1564 RVA: 0x0000DA12 File Offset: 0x0000BC12
		protected virtual void AppendStackTrace(StringBuilder sb, Exception ex)
		{
			sb.Append(ex.StackTrace);
		}

		// Token: 0x0600061D RID: 1565 RVA: 0x0000DA21 File Offset: 0x0000BC21
		protected virtual void AppendToString(StringBuilder sb, Exception ex)
		{
			sb.Append(ex.ToString());
		}

		// Token: 0x0600061E RID: 1566 RVA: 0x0000DA30 File Offset: 0x0000BC30
		protected virtual void AppendType(StringBuilder sb, Exception ex)
		{
			sb.Append(ex.GetType().FullName);
		}

		// Token: 0x0600061F RID: 1567 RVA: 0x0000DA44 File Offset: 0x0000BC44
		protected virtual void AppendShortType(StringBuilder sb, Exception ex)
		{
			sb.Append(ex.GetType().Name);
		}

		// Token: 0x06000620 RID: 1568 RVA: 0x0000DA58 File Offset: 0x0000BC58
		protected virtual void AppendData(StringBuilder sb, Exception ex)
		{
			string value = string.Empty;
			foreach (object obj in ex.Data.Keys)
			{
				sb.Append(value);
				sb.AppendFormat("{0}: {1}", obj, ex.Data[obj]);
				value = ";";
			}
		}

		// Token: 0x06000621 RID: 1569 RVA: 0x0000DAD8 File Offset: 0x0000BCD8
		private static List<ExceptionRenderingFormat> CompileFormat(string formatSpecifier)
		{
			List<ExceptionRenderingFormat> list = new List<ExceptionRenderingFormat>();
			string[] array = formatSpecifier.Replace(" ", string.Empty).Split(new string[]
			{
				","
			}, StringSplitOptions.RemoveEmptyEntries);
			foreach (string text in array)
			{
				ExceptionRenderingFormat item;
				if (ExceptionLayoutRenderer._formatsMapping.TryGetValue(text, out item))
				{
					list.Add(item);
				}
				else
				{
					InternalLogger.Warn("Unknown exception data target: {0}", new object[]
					{
						text
					});
				}
			}
			return list;
		}

		// Token: 0x04000176 RID: 374
		private string format;

		// Token: 0x04000177 RID: 375
		private string innerFormat = string.Empty;

		// Token: 0x04000178 RID: 376
		private readonly Dictionary<ExceptionRenderingFormat, Action<StringBuilder, Exception>> _renderingfunctions;

		// Token: 0x04000179 RID: 377
		private static readonly Dictionary<string, ExceptionRenderingFormat> _formatsMapping = new Dictionary<string, ExceptionRenderingFormat>(StringComparer.OrdinalIgnoreCase)
		{
			{
				"MESSAGE",
				ExceptionRenderingFormat.Message
			},
			{
				"TYPE",
				ExceptionRenderingFormat.Type
			},
			{
				"SHORTTYPE",
				ExceptionRenderingFormat.ShortType
			},
			{
				"TOSTRING",
				ExceptionRenderingFormat.ToString
			},
			{
				"METHOD",
				ExceptionRenderingFormat.Method
			},
			{
				"STACKTRACE",
				ExceptionRenderingFormat.StackTrace
			},
			{
				"DATA",
				ExceptionRenderingFormat.Data
			}
		};
	}
}

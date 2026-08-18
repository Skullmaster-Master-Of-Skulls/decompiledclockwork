using System;
using System.Data.Entity;
using System.Globalization;

namespace System.Data.Metadata.Edm
{
	// Token: 0x020001BB RID: 443
	[Serializable]
	public sealed class EdmSchemaError : EdmError
	{
		// Token: 0x06001F00 RID: 7936 RVA: 0x0006D630 File Offset: 0x0006B830
		internal EdmSchemaError(string message, int errorCode, EdmSchemaErrorSeverity severity) : this(message, errorCode, severity, null)
		{
		}

		// Token: 0x06001F01 RID: 7937 RVA: 0x0006D63C File Offset: 0x0006B83C
		internal EdmSchemaError(string message, int errorCode, EdmSchemaErrorSeverity severity, Exception exception)
		{
			this._line = -1;
			this._column = -1;
			this._stackTrace = string.Empty;
			base..ctor(message);
			this.Initialize(errorCode, severity, null, -1, -1, exception);
		}

		// Token: 0x06001F02 RID: 7938 RVA: 0x0006D66B File Offset: 0x0006B86B
		internal EdmSchemaError(string message, int errorCode, EdmSchemaErrorSeverity severity, string schemaLocation, int line, int column) : this(message, errorCode, severity, schemaLocation, line, column, null)
		{
		}

		// Token: 0x06001F03 RID: 7939 RVA: 0x0006D680 File Offset: 0x0006B880
		internal EdmSchemaError(string message, int errorCode, EdmSchemaErrorSeverity severity, string schemaLocation, int line, int column, Exception exception)
		{
			this._line = -1;
			this._column = -1;
			this._stackTrace = string.Empty;
			base..ctor(message);
			if (severity < EdmSchemaErrorSeverity.Warning || severity > EdmSchemaErrorSeverity.Error)
			{
				throw new ArgumentOutOfRangeException("severity", severity, Strings.ArgumentOutOfRange(severity));
			}
			this.Initialize(errorCode, severity, schemaLocation, line, column, exception);
		}

		// Token: 0x06001F04 RID: 7940 RVA: 0x0006D6E4 File Offset: 0x0006B8E4
		private void Initialize(int errorCode, EdmSchemaErrorSeverity severity, string schemaLocation, int line, int column, Exception exception)
		{
			if (errorCode < 0)
			{
				throw new ArgumentOutOfRangeException("errorCode", errorCode, Strings.ArgumentOutOfRangeExpectedPostiveNumber(errorCode));
			}
			this._errorCode = errorCode;
			this._severity = severity;
			this._schemaLocation = schemaLocation;
			this._line = line;
			this._column = column;
			if (exception != null)
			{
				this._stackTrace = exception.StackTrace;
			}
		}

		// Token: 0x06001F05 RID: 7941 RVA: 0x0006D748 File Offset: 0x0006B948
		public override string ToString()
		{
			EdmSchemaErrorSeverity severity = this.Severity;
			string text;
			if (severity != EdmSchemaErrorSeverity.Warning)
			{
				if (severity == EdmSchemaErrorSeverity.Error)
				{
					text = Strings.GeneratorErrorSeverityError;
				}
				else
				{
					text = Strings.GeneratorErrorSeverityUnknown;
				}
			}
			else
			{
				text = Strings.GeneratorErrorSeverityWarning;
			}
			string result;
			if (string.IsNullOrEmpty(this.SchemaName) && this.Line < 0 && this.Column < 0)
			{
				result = string.Format(CultureInfo.CurrentCulture, "{0} {1:0000}: {2}", new object[]
				{
					text,
					this.ErrorCode,
					base.Message
				});
			}
			else
			{
				result = string.Format(CultureInfo.CurrentCulture, "{0}({1},{2}) : {3} {4:0000}: {5}", new object[]
				{
					(this.SchemaName == null) ? Strings.SourceUriUnknown : this.SchemaName,
					this.Line,
					this.Column,
					text,
					this.ErrorCode,
					base.Message
				});
			}
			return result;
		}

		// Token: 0x170005F6 RID: 1526
		// (get) Token: 0x06001F06 RID: 7942 RVA: 0x0006D831 File Offset: 0x0006BA31
		public int ErrorCode
		{
			get
			{
				return this._errorCode;
			}
		}

		// Token: 0x170005F7 RID: 1527
		// (get) Token: 0x06001F07 RID: 7943 RVA: 0x0006D839 File Offset: 0x0006BA39
		// (set) Token: 0x06001F08 RID: 7944 RVA: 0x0006D841 File Offset: 0x0006BA41
		public EdmSchemaErrorSeverity Severity
		{
			get
			{
				return this._severity;
			}
			set
			{
				this._severity = value;
			}
		}

		// Token: 0x170005F8 RID: 1528
		// (get) Token: 0x06001F09 RID: 7945 RVA: 0x0006D84A File Offset: 0x0006BA4A
		public int Line
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x170005F9 RID: 1529
		// (get) Token: 0x06001F0A RID: 7946 RVA: 0x0006D852 File Offset: 0x0006BA52
		public int Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x170005FA RID: 1530
		// (get) Token: 0x06001F0B RID: 7947 RVA: 0x0006D85A File Offset: 0x0006BA5A
		public string SchemaLocation
		{
			get
			{
				return this._schemaLocation;
			}
		}

		// Token: 0x170005FB RID: 1531
		// (get) Token: 0x06001F0C RID: 7948 RVA: 0x0006D862 File Offset: 0x0006BA62
		public string SchemaName
		{
			get
			{
				return EdmSchemaError.GetNameFromSchemaLocation(this.SchemaLocation);
			}
		}

		// Token: 0x170005FC RID: 1532
		// (get) Token: 0x06001F0D RID: 7949 RVA: 0x0006D86F File Offset: 0x0006BA6F
		public string StackTrace
		{
			get
			{
				return this._stackTrace;
			}
		}

		// Token: 0x06001F0E RID: 7950 RVA: 0x0006D878 File Offset: 0x0006BA78
		private static string GetNameFromSchemaLocation(string schemaLocation)
		{
			if (string.IsNullOrEmpty(schemaLocation))
			{
				return schemaLocation;
			}
			int num = Math.Max(schemaLocation.LastIndexOf('/'), schemaLocation.LastIndexOf('\\'));
			int num2 = num + 1;
			if (num < 0)
			{
				return schemaLocation;
			}
			if (num2 >= schemaLocation.Length)
			{
				return string.Empty;
			}
			return schemaLocation.Substring(num2);
		}

		// Token: 0x04000D05 RID: 3333
		private int _errorCode;

		// Token: 0x04000D06 RID: 3334
		private EdmSchemaErrorSeverity _severity;

		// Token: 0x04000D07 RID: 3335
		private string _schemaLocation;

		// Token: 0x04000D08 RID: 3336
		private int _line;

		// Token: 0x04000D09 RID: 3337
		private int _column;

		// Token: 0x04000D0A RID: 3338
		private string _stackTrace;
	}
}

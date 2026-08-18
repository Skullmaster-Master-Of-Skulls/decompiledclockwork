using System;
using System.Data.Entity.Resources;
using System.Globalization;

namespace System.Data.Entity.Core.Metadata.Edm
{
	// Token: 0x020004BD RID: 1213
	[Serializable]
	public sealed class EdmSchemaError : EdmError
	{
		// Token: 0x06002CA2 RID: 11426 RVA: 0x000D9AA8 File Offset: 0x000D7CA8
		public EdmSchemaError(string message, int errorCode, EdmSchemaErrorSeverity severity) : this(message, errorCode, severity, null)
		{
		}

		// Token: 0x06002CA3 RID: 11427 RVA: 0x000D9AB4 File Offset: 0x000D7CB4
		internal EdmSchemaError(string message, int errorCode, EdmSchemaErrorSeverity severity, Exception exception)
		{
			this._line = -1;
			this._column = -1;
			this._stackTrace = string.Empty;
			base..ctor(message);
			this.Initialize(errorCode, severity, null, -1, -1, exception);
		}

		// Token: 0x06002CA4 RID: 11428 RVA: 0x000D9AE3 File Offset: 0x000D7CE3
		internal EdmSchemaError(string message, int errorCode, EdmSchemaErrorSeverity severity, string schemaLocation, int line, int column) : this(message, errorCode, severity, schemaLocation, line, column, null)
		{
		}

		// Token: 0x06002CA5 RID: 11429 RVA: 0x000D9AF8 File Offset: 0x000D7CF8
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

		// Token: 0x06002CA6 RID: 11430 RVA: 0x000D9B5C File Offset: 0x000D7D5C
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

		// Token: 0x06002CA7 RID: 11431 RVA: 0x000D9BC0 File Offset: 0x000D7DC0
		public override string ToString()
		{
			string text;
			switch (this.Severity)
			{
			case EdmSchemaErrorSeverity.Warning:
				text = Strings.GeneratorErrorSeverityWarning;
				break;
			case EdmSchemaErrorSeverity.Error:
				text = Strings.GeneratorErrorSeverityError;
				break;
			default:
				text = Strings.GeneratorErrorSeverityUnknown;
				break;
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

		// Token: 0x17000617 RID: 1559
		// (get) Token: 0x06002CA8 RID: 11432 RVA: 0x000D9CBE File Offset: 0x000D7EBE
		public int ErrorCode
		{
			get
			{
				return this._errorCode;
			}
		}

		// Token: 0x17000618 RID: 1560
		// (get) Token: 0x06002CA9 RID: 11433 RVA: 0x000D9CC6 File Offset: 0x000D7EC6
		// (set) Token: 0x06002CAA RID: 11434 RVA: 0x000D9CCE File Offset: 0x000D7ECE
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

		// Token: 0x17000619 RID: 1561
		// (get) Token: 0x06002CAB RID: 11435 RVA: 0x000D9CD7 File Offset: 0x000D7ED7
		public int Line
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x1700061A RID: 1562
		// (get) Token: 0x06002CAC RID: 11436 RVA: 0x000D9CDF File Offset: 0x000D7EDF
		public int Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x1700061B RID: 1563
		// (get) Token: 0x06002CAD RID: 11437 RVA: 0x000D9CE7 File Offset: 0x000D7EE7
		public string SchemaLocation
		{
			get
			{
				return this._schemaLocation;
			}
		}

		// Token: 0x1700061C RID: 1564
		// (get) Token: 0x06002CAE RID: 11438 RVA: 0x000D9CEF File Offset: 0x000D7EEF
		public string SchemaName
		{
			get
			{
				return EdmSchemaError.GetNameFromSchemaLocation(this.SchemaLocation);
			}
		}

		// Token: 0x1700061D RID: 1565
		// (get) Token: 0x06002CAF RID: 11439 RVA: 0x000D9CFC File Offset: 0x000D7EFC
		public string StackTrace
		{
			get
			{
				return this._stackTrace;
			}
		}

		// Token: 0x06002CB0 RID: 11440 RVA: 0x000D9D04 File Offset: 0x000D7F04
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

		// Token: 0x04001074 RID: 4212
		private int _errorCode;

		// Token: 0x04001075 RID: 4213
		private EdmSchemaErrorSeverity _severity;

		// Token: 0x04001076 RID: 4214
		private string _schemaLocation;

		// Token: 0x04001077 RID: 4215
		private int _line;

		// Token: 0x04001078 RID: 4216
		private int _column;

		// Token: 0x04001079 RID: 4217
		private string _stackTrace;
	}
}

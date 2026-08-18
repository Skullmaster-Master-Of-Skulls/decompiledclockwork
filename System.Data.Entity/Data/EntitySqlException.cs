using System;
using System.Data.Common.EntitySql;
using System.Data.Entity;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security;
using System.Security.Permissions;
using System.Text;

namespace System.Data
{
	// Token: 0x0200000E RID: 14
	[Serializable]
	public sealed class EntitySqlException : EntityException
	{
		// Token: 0x06000038 RID: 56 RVA: 0x00002BC0 File Offset: 0x00000DC0
		public EntitySqlException() : this(Strings.GeneralQueryError)
		{
			base.HResult = -2146232006;
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002BD8 File Offset: 0x00000DD8
		public EntitySqlException(string message) : base(message)
		{
			base.HResult = -2146232006;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x00002BEC File Offset: 0x00000DEC
		public EntitySqlException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232006;
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00002C04 File Offset: 0x00000E04
		private EntitySqlException(SerializationInfo serializationInfo, StreamingContext streamingContext) : base(serializationInfo, streamingContext)
		{
			base.HResult = -2146232006;
			this._errorDescription = serializationInfo.GetString("ErrorDescription");
			this._errorContext = serializationInfo.GetString("ErrorContext");
			this._line = serializationInfo.GetInt32("Line");
			this._column = serializationInfo.GetInt32("Column");
		}

		// Token: 0x0600003C RID: 60 RVA: 0x00002C68 File Offset: 0x00000E68
		internal static EntitySqlException Create(ErrorContext errCtx, string errorMessage, Exception innerException)
		{
			return EntitySqlException.Create(errCtx.CommandText, errorMessage, errCtx.InputPosition, errCtx.ErrorContextInfo, errCtx.UseContextInfoAsResourceIdentifier, innerException);
		}

		// Token: 0x0600003D RID: 61 RVA: 0x00002C8C File Offset: 0x00000E8C
		internal static EntitySqlException Create(string commandText, string errorDescription, int errorPosition, string errorContextInfo, bool loadErrorContextInfoFromResource, Exception innerException)
		{
			int line;
			int column;
			string errorContext = EntitySqlException.FormatErrorContext(commandText, errorPosition, errorContextInfo, loadErrorContextInfoFromResource, out line, out column);
			string message = EntitySqlException.FormatQueryError(errorDescription, errorContext);
			return new EntitySqlException(message, errorDescription, errorContext, line, column, innerException);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x00002CBC File Offset: 0x00000EBC
		private EntitySqlException(string message, string errorDescription, string errorContext, int line, int column, Exception innerException) : base(message, innerException)
		{
			this._errorDescription = errorDescription;
			this._errorContext = errorContext;
			this._line = line;
			this._column = column;
			base.HResult = -2146232006;
		}

		// Token: 0x17000002 RID: 2
		// (get) Token: 0x0600003F RID: 63 RVA: 0x00002CF0 File Offset: 0x00000EF0
		public string ErrorDescription
		{
			get
			{
				return this._errorDescription ?? string.Empty;
			}
		}

		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000040 RID: 64 RVA: 0x00002D01 File Offset: 0x00000F01
		public string ErrorContext
		{
			get
			{
				return this._errorContext ?? string.Empty;
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x06000041 RID: 65 RVA: 0x00002D12 File Offset: 0x00000F12
		public int Line
		{
			get
			{
				return this._line;
			}
		}

		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000042 RID: 66 RVA: 0x00002D1A File Offset: 0x00000F1A
		public int Column
		{
			get
			{
				return this._column;
			}
		}

		// Token: 0x06000043 RID: 67 RVA: 0x00002D24 File Offset: 0x00000F24
		internal static string GetGenericErrorMessage(string commandText, int position)
		{
			int num = 0;
			int num2 = 0;
			return EntitySqlException.FormatErrorContext(commandText, position, "GenericSyntaxError", true, out num, out num2);
		}

		// Token: 0x06000044 RID: 68 RVA: 0x00002D48 File Offset: 0x00000F48
		internal static string FormatErrorContext(string commandText, int errorPosition, string errorContextInfo, bool loadErrorContextInfoFromResource, out int lineNumber, out int columnNumber)
		{
			if (loadErrorContextInfoFromResource)
			{
				errorContextInfo = ((!string.IsNullOrEmpty(errorContextInfo)) ? EntityRes.GetString(errorContextInfo) : string.Empty);
			}
			StringBuilder stringBuilder = new StringBuilder(commandText.Length);
			foreach (char c in commandText)
			{
				if (CqlLexer.IsNewLine(c))
				{
					c = '\n';
				}
				else if ((char.IsControl(c) || char.IsWhiteSpace(c)) && '\r' != c)
				{
					c = ' ';
				}
				stringBuilder.Append(c);
			}
			commandText = stringBuilder.ToString().TrimEnd(new char[]
			{
				'\n'
			});
			string[] array = commandText.Split(new char[]
			{
				'\n'
			}, StringSplitOptions.None);
			lineNumber = 0;
			columnNumber = errorPosition;
			while (lineNumber < array.Length && columnNumber > array[lineNumber].Length)
			{
				columnNumber -= array[lineNumber].Length + 1;
				lineNumber++;
			}
			lineNumber++;
			columnNumber++;
			stringBuilder = new StringBuilder();
			if (!string.IsNullOrEmpty(errorContextInfo))
			{
				stringBuilder.AppendFormat(CultureInfo.CurrentCulture, "{0}, ", new object[]
				{
					errorContextInfo
				});
			}
			if (errorPosition >= 0)
			{
				stringBuilder.AppendFormat(CultureInfo.CurrentCulture, "{0} {1}, {2} {3}", new object[]
				{
					Strings.LocalizedLine,
					lineNumber,
					Strings.LocalizedColumn,
					columnNumber
				});
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06000045 RID: 69 RVA: 0x00002EA8 File Offset: 0x000010A8
		private static string FormatQueryError(string errorMessage, string errorContext)
		{
			StringBuilder stringBuilder = new StringBuilder();
			stringBuilder.Append(errorMessage);
			if (!string.IsNullOrEmpty(errorContext))
			{
				stringBuilder.AppendFormat(CultureInfo.CurrentCulture, " {0} {1}", new object[]
				{
					Strings.LocalizedNear,
					errorContext
				});
			}
			return stringBuilder.Append(".").ToString();
		}

		// Token: 0x06000046 RID: 70 RVA: 0x00002F00 File Offset: 0x00001100
		[SecurityCritical]
		[PermissionSet(SecurityAction.LinkDemand, Unrestricted = true)]
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			base.GetObjectData(info, context);
			info.AddValue("ErrorDescription", this._errorDescription);
			info.AddValue("ErrorContext", this._errorContext);
			info.AddValue("Line", this._line);
			info.AddValue("Column", this._column);
		}

		// Token: 0x0400007F RID: 127
		private string _errorDescription;

		// Token: 0x04000080 RID: 128
		private string _errorContext;

		// Token: 0x04000081 RID: 129
		private int _line;

		// Token: 0x04000082 RID: 130
		private int _column;
	}
}

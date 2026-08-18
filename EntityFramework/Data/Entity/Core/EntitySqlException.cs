using System;
using System.Data.Entity.Core.Common.EntitySql;
using System.Data.Entity.Resources;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;
using System.Runtime.Serialization;
using System.Text;

namespace System.Data.Entity.Core
{
	// Token: 0x0200039F RID: 927
	[SuppressMessage("Microsoft.Design", "CA1032:ImplementStandardExceptionConstructors", Justification = "SerializeObjectState used instead")]
	[Serializable]
	public sealed class EntitySqlException : EntityException
	{
		// Token: 0x0600217E RID: 8574 RVA: 0x0009D9EC File Offset: 0x0009BBEC
		public EntitySqlException() : this(Strings.GeneralQueryError)
		{
		}

		// Token: 0x0600217F RID: 8575 RVA: 0x0009D9F9 File Offset: 0x0009BBF9
		public EntitySqlException(string message) : base(message)
		{
			base.HResult = -2146232006;
			this.SubscribeToSerializeObjectState();
		}

		// Token: 0x06002180 RID: 8576 RVA: 0x0009DA13 File Offset: 0x0009BC13
		public EntitySqlException(string message, Exception innerException) : base(message, innerException)
		{
			base.HResult = -2146232006;
			this.SubscribeToSerializeObjectState();
		}

		// Token: 0x06002181 RID: 8577 RVA: 0x0009DA2E File Offset: 0x0009BC2E
		internal static EntitySqlException Create(ErrorContext errCtx, string errorMessage, Exception innerException)
		{
			return EntitySqlException.Create(errCtx.CommandText, errorMessage, errCtx.InputPosition, errCtx.ErrorContextInfo, errCtx.UseContextInfoAsResourceIdentifier, innerException);
		}

		// Token: 0x06002182 RID: 8578 RVA: 0x0009DA50 File Offset: 0x0009BC50
		internal static EntitySqlException Create(string commandText, string errorDescription, int errorPosition, string errorContextInfo, bool loadErrorContextInfoFromResource, Exception innerException)
		{
			int line;
			int column;
			string errorContext = EntitySqlException.FormatErrorContext(commandText, errorPosition, errorContextInfo, loadErrorContextInfoFromResource, out line, out column);
			string message = EntitySqlException.FormatQueryError(errorDescription, errorContext);
			return new EntitySqlException(message, errorDescription, errorContext, line, column, innerException);
		}

		// Token: 0x06002183 RID: 8579 RVA: 0x0009DA80 File Offset: 0x0009BC80
		private EntitySqlException(string message, string errorDescription, string errorContext, int line, int column, Exception innerException) : base(message, innerException)
		{
			this._state.ErrorDescription = errorDescription;
			this._state.ErrorContext = errorContext;
			this._state.Line = line;
			this._state.Column = column;
			base.HResult = -2146232006;
			this.SubscribeToSerializeObjectState();
		}

		// Token: 0x17000448 RID: 1096
		// (get) Token: 0x06002184 RID: 8580 RVA: 0x0009DAD9 File Offset: 0x0009BCD9
		public string ErrorDescription
		{
			get
			{
				return this._state.ErrorDescription ?? string.Empty;
			}
		}

		// Token: 0x17000449 RID: 1097
		// (get) Token: 0x06002185 RID: 8581 RVA: 0x0009DAEF File Offset: 0x0009BCEF
		public string ErrorContext
		{
			get
			{
				return this._state.ErrorContext ?? string.Empty;
			}
		}

		// Token: 0x1700044A RID: 1098
		// (get) Token: 0x06002186 RID: 8582 RVA: 0x0009DB05 File Offset: 0x0009BD05
		public int Line
		{
			get
			{
				return this._state.Line;
			}
		}

		// Token: 0x1700044B RID: 1099
		// (get) Token: 0x06002187 RID: 8583 RVA: 0x0009DB12 File Offset: 0x0009BD12
		public int Column
		{
			get
			{
				return this._state.Column;
			}
		}

		// Token: 0x06002188 RID: 8584 RVA: 0x0009DB20 File Offset: 0x0009BD20
		internal static string GetGenericErrorMessage(string commandText, int position)
		{
			int num = 0;
			int num2 = 0;
			return EntitySqlException.FormatErrorContext(commandText, position, "GenericSyntaxError", true, out num, out num2);
		}

		// Token: 0x06002189 RID: 8585 RVA: 0x0009DB44 File Offset: 0x0009BD44
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

		// Token: 0x0600218A RID: 8586 RVA: 0x0009DCB8 File Offset: 0x0009BEB8
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

		// Token: 0x0600218B RID: 8587 RVA: 0x0009DD23 File Offset: 0x0009BF23
		private void SubscribeToSerializeObjectState()
		{
			base.SerializeObjectState += delegate(object _, SafeSerializationEventArgs a)
			{
				a.AddSerializedState(this._state);
			};
		}

		// Token: 0x04000BD4 RID: 3028
		private const int HResultInvalidQuery = -2146232006;

		// Token: 0x04000BD5 RID: 3029
		[NonSerialized]
		private EntitySqlException.EntitySqlExceptionState _state;

		// Token: 0x020003A0 RID: 928
		[Serializable]
		private struct EntitySqlExceptionState : ISafeSerializationData
		{
			// Token: 0x1700044C RID: 1100
			// (get) Token: 0x0600218D RID: 8589 RVA: 0x0009DD37 File Offset: 0x0009BF37
			// (set) Token: 0x0600218E RID: 8590 RVA: 0x0009DD3F File Offset: 0x0009BF3F
			public string ErrorDescription { get; set; }

			// Token: 0x1700044D RID: 1101
			// (get) Token: 0x0600218F RID: 8591 RVA: 0x0009DD48 File Offset: 0x0009BF48
			// (set) Token: 0x06002190 RID: 8592 RVA: 0x0009DD50 File Offset: 0x0009BF50
			public string ErrorContext { get; set; }

			// Token: 0x1700044E RID: 1102
			// (get) Token: 0x06002191 RID: 8593 RVA: 0x0009DD59 File Offset: 0x0009BF59
			// (set) Token: 0x06002192 RID: 8594 RVA: 0x0009DD61 File Offset: 0x0009BF61
			public int Line { get; set; }

			// Token: 0x1700044F RID: 1103
			// (get) Token: 0x06002193 RID: 8595 RVA: 0x0009DD6A File Offset: 0x0009BF6A
			// (set) Token: 0x06002194 RID: 8596 RVA: 0x0009DD72 File Offset: 0x0009BF72
			public int Column { get; set; }

			// Token: 0x06002195 RID: 8597 RVA: 0x0009DD7C File Offset: 0x0009BF7C
			public void CompleteDeserialization(object deserialized)
			{
				EntitySqlException ex = (EntitySqlException)deserialized;
				ex._state = this;
				ex.SubscribeToSerializeObjectState();
			}
		}
	}
}

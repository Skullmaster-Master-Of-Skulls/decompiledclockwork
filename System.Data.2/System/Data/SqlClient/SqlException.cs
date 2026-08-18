using System;
using System.Collections;
using System.ComponentModel;
using System.Data.Common;
using System.Globalization;
using System.Runtime.Serialization;
using System.Security.Permissions;
using System.Text;

namespace System.Data.SqlClient
{
	// Token: 0x020001CF RID: 463
	[Serializable]
	public sealed class SqlException : DbException
	{
		// Token: 0x06001D24 RID: 7460 RVA: 0x000CE7C4 File Offset: 0x000CDBC4
		private SqlException(string message, SqlErrorCollection errorCollection, Exception innerException, Guid conId) : base(message, innerException)
		{
			base.HResult = -2146232060;
			this._errors = errorCollection;
			this._clientConnectionId = conId;
		}

		// Token: 0x06001D25 RID: 7461 RVA: 0x000CE800 File Offset: 0x000CDC00
		private SqlException(SerializationInfo si, StreamingContext sc) : base(si, sc)
		{
			this._errors = (SqlErrorCollection)si.GetValue("Errors", typeof(SqlErrorCollection));
			base.HResult = -2146232060;
			foreach (SerializationEntry serializationEntry in si)
			{
				if ("ClientConnectionId" == serializationEntry.Name)
				{
					this._clientConnectionId = (Guid)serializationEntry.Value;
					return;
				}
			}
		}

		// Token: 0x06001D26 RID: 7462 RVA: 0x000CE88C File Offset: 0x000CDC8C
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo si, StreamingContext context)
		{
			if (si == null)
			{
				throw new ArgumentNullException("si");
			}
			si.AddValue("Errors", this._errors, typeof(SqlErrorCollection));
			si.AddValue("ClientConnectionId", this._clientConnectionId, typeof(Guid));
			base.GetObjectData(si, context);
		}

		// Token: 0x1700047B RID: 1147
		// (get) Token: 0x06001D27 RID: 7463 RVA: 0x000CE8EC File Offset: 0x000CDCEC
		[DesignerSerializationVisibility(DesignerSerializationVisibility.Content)]
		public SqlErrorCollection Errors
		{
			get
			{
				if (this._errors == null)
				{
					this._errors = new SqlErrorCollection();
				}
				return this._errors;
			}
		}

		// Token: 0x1700047C RID: 1148
		// (get) Token: 0x06001D28 RID: 7464 RVA: 0x000CE914 File Offset: 0x000CDD14
		public Guid ClientConnectionId
		{
			get
			{
				return this._clientConnectionId;
			}
		}

		// Token: 0x06001D29 RID: 7465 RVA: 0x000CE928 File Offset: 0x000CDD28
		private bool ShouldSerializeErrors()
		{
			return this._errors != null && 0 < this._errors.Count;
		}

		// Token: 0x1700047D RID: 1149
		// (get) Token: 0x06001D2A RID: 7466 RVA: 0x000CE950 File Offset: 0x000CDD50
		public byte Class
		{
			get
			{
				return this.Errors[0].Class;
			}
		}

		// Token: 0x1700047E RID: 1150
		// (get) Token: 0x06001D2B RID: 7467 RVA: 0x000CE970 File Offset: 0x000CDD70
		public int LineNumber
		{
			get
			{
				return this.Errors[0].LineNumber;
			}
		}

		// Token: 0x1700047F RID: 1151
		// (get) Token: 0x06001D2C RID: 7468 RVA: 0x000CE990 File Offset: 0x000CDD90
		public int Number
		{
			get
			{
				return this.Errors[0].Number;
			}
		}

		// Token: 0x17000480 RID: 1152
		// (get) Token: 0x06001D2D RID: 7469 RVA: 0x000CE9B0 File Offset: 0x000CDDB0
		public string Procedure
		{
			get
			{
				return this.Errors[0].Procedure;
			}
		}

		// Token: 0x17000481 RID: 1153
		// (get) Token: 0x06001D2E RID: 7470 RVA: 0x000CE9D0 File Offset: 0x000CDDD0
		public string Server
		{
			get
			{
				return this.Errors[0].Server;
			}
		}

		// Token: 0x17000482 RID: 1154
		// (get) Token: 0x06001D2F RID: 7471 RVA: 0x000CE9F0 File Offset: 0x000CDDF0
		public byte State
		{
			get
			{
				return this.Errors[0].State;
			}
		}

		// Token: 0x17000483 RID: 1155
		// (get) Token: 0x06001D30 RID: 7472 RVA: 0x000CEA10 File Offset: 0x000CDE10
		public override string Source
		{
			get
			{
				return this.Errors[0].Source;
			}
		}

		// Token: 0x06001D31 RID: 7473 RVA: 0x000CEA30 File Offset: 0x000CDE30
		public override string ToString()
		{
			StringBuilder stringBuilder = new StringBuilder(base.ToString());
			stringBuilder.AppendLine();
			stringBuilder.AppendFormat(SQLMessage.ExClientConnectionId(), this._clientConnectionId);
			if (this.Number != 0)
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendFormat(SQLMessage.ExErrorNumberStateClass(), this.Number, this.State, this.Class);
			}
			if (this.Data.Contains("OriginalClientConnectionId"))
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendFormat(SQLMessage.ExOriginalClientConnectionId(), this.Data["OriginalClientConnectionId"]);
			}
			if (this.Data.Contains("RoutingDestination"))
			{
				stringBuilder.AppendLine();
				stringBuilder.AppendFormat(SQLMessage.ExRoutingDestination(), this.Data["RoutingDestination"]);
			}
			return stringBuilder.ToString();
		}

		// Token: 0x06001D32 RID: 7474 RVA: 0x000CEB14 File Offset: 0x000CDF14
		internal static SqlException CreateException(SqlErrorCollection errorCollection, string serverVersion)
		{
			return SqlException.CreateException(errorCollection, serverVersion, Guid.Empty, null);
		}

		// Token: 0x06001D33 RID: 7475 RVA: 0x000CEB30 File Offset: 0x000CDF30
		internal static SqlException CreateException(SqlErrorCollection errorCollection, string serverVersion, SqlInternalConnectionTds internalConnection, Exception innerException = null)
		{
			Guid conId = (internalConnection == null) ? Guid.Empty : internalConnection._clientConnectionId;
			SqlException ex = SqlException.CreateException(errorCollection, serverVersion, conId, innerException);
			if (internalConnection != null)
			{
				if (internalConnection.OriginalClientConnectionId != Guid.Empty && internalConnection.OriginalClientConnectionId != internalConnection.ClientConnectionId)
				{
					ex.Data.Add("OriginalClientConnectionId", internalConnection.OriginalClientConnectionId);
				}
				if (!string.IsNullOrEmpty(internalConnection.RoutingDestination))
				{
					ex.Data.Add("RoutingDestination", internalConnection.RoutingDestination);
				}
			}
			return ex;
		}

		// Token: 0x06001D34 RID: 7476 RVA: 0x000CEBC0 File Offset: 0x000CDFC0
		internal static SqlException CreateException(SqlErrorCollection errorCollection, string serverVersion, Guid conId, Exception innerException = null)
		{
			StringBuilder stringBuilder = new StringBuilder();
			for (int i = 0; i < errorCollection.Count; i++)
			{
				if (i > 0)
				{
					stringBuilder.Append(Environment.NewLine);
				}
				stringBuilder.Append(errorCollection[i].Message);
			}
			if (innerException == null && errorCollection[0].Win32ErrorCode != 0 && errorCollection[0].Win32ErrorCode != -1)
			{
				innerException = new Win32Exception(errorCollection[0].Win32ErrorCode);
			}
			SqlException ex = new SqlException(stringBuilder.ToString(), errorCollection, innerException, conId);
			ex.Data.Add("HelpLink.ProdName", "Microsoft SQL Server");
			if (!ADP.IsEmpty(serverVersion))
			{
				ex.Data.Add("HelpLink.ProdVer", serverVersion);
			}
			ex.Data.Add("HelpLink.EvtSrc", "MSSQLServer");
			ex.Data.Add("HelpLink.EvtID", errorCollection[0].Number.ToString(CultureInfo.InvariantCulture));
			ex.Data.Add("HelpLink.BaseHelpUrl", "http://go.microsoft.com/fwlink");
			ex.Data.Add("HelpLink.LinkId", "20476");
			return ex;
		}

		// Token: 0x06001D35 RID: 7477 RVA: 0x000CECE0 File Offset: 0x000CE0E0
		internal SqlException InternalClone()
		{
			SqlException ex = new SqlException(this.Message, this._errors, base.InnerException, this._clientConnectionId);
			if (this.Data != null)
			{
				foreach (object obj in this.Data)
				{
					DictionaryEntry dictionaryEntry = (DictionaryEntry)obj;
					ex.Data.Add(dictionaryEntry.Key, dictionaryEntry.Value);
				}
			}
			ex._doNotReconnect = this._doNotReconnect;
			return ex;
		}

		// Token: 0x040010B6 RID: 4278
		private const string OriginalClientConnectionIdKey = "OriginalClientConnectionId";

		// Token: 0x040010B7 RID: 4279
		private const string RoutingDestinationKey = "RoutingDestination";

		// Token: 0x040010B8 RID: 4280
		private SqlErrorCollection _errors;

		// Token: 0x040010B9 RID: 4281
		[OptionalField(VersionAdded = 4)]
		private Guid _clientConnectionId = Guid.Empty;

		// Token: 0x040010BA RID: 4282
		internal bool _doNotReconnect;
	}
}

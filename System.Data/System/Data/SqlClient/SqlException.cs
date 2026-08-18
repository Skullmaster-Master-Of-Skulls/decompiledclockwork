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
	// Token: 0x020002F4 RID: 756
	[Serializable]
	public sealed class SqlException : DbException
	{
		// Token: 0x0600272A RID: 10026 RVA: 0x002AA1B8 File Offset: 0x002A95B8
		private SqlException(string message, SqlErrorCollection errorCollection) : base(message)
		{
			base.HResult = -2146232060;
			this._errors = errorCollection;
		}

		// Token: 0x0600272B RID: 10027 RVA: 0x002AA1E8 File Offset: 0x002A95E8
		private SqlException(SerializationInfo si, StreamingContext sc) : base(si, sc)
		{
			this._errors = (SqlErrorCollection)si.GetValue("Errors", typeof(SqlErrorCollection));
			base.HResult = -2146232060;
		}

		// Token: 0x0600272C RID: 10028 RVA: 0x002AA228 File Offset: 0x002A9628
		[SecurityPermission(SecurityAction.LinkDemand, Flags = SecurityPermissionFlag.SerializationFormatter)]
		public override void GetObjectData(SerializationInfo si, StreamingContext context)
		{
			if (si == null)
			{
				throw new ArgumentNullException("si");
			}
			si.AddValue("Errors", this._errors, typeof(SqlErrorCollection));
			base.GetObjectData(si, context);
		}

		// Token: 0x1700063C RID: 1596
		// (get) Token: 0x0600272D RID: 10029 RVA: 0x002AA268 File Offset: 0x002A9668
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

		// Token: 0x0600272E RID: 10030 RVA: 0x002AA298 File Offset: 0x002A9698
		private bool ShouldSerializeErrors()
		{
			return this._errors != null && 0 < this._errors.Count;
		}

		// Token: 0x1700063D RID: 1597
		// (get) Token: 0x0600272F RID: 10031 RVA: 0x002AA2C8 File Offset: 0x002A96C8
		public byte Class
		{
			get
			{
				return this.Errors[0].Class;
			}
		}

		// Token: 0x1700063E RID: 1598
		// (get) Token: 0x06002730 RID: 10032 RVA: 0x002AA2E8 File Offset: 0x002A96E8
		public int LineNumber
		{
			get
			{
				return this.Errors[0].LineNumber;
			}
		}

		// Token: 0x1700063F RID: 1599
		// (get) Token: 0x06002731 RID: 10033 RVA: 0x002AA308 File Offset: 0x002A9708
		public int Number
		{
			get
			{
				return this.Errors[0].Number;
			}
		}

		// Token: 0x17000640 RID: 1600
		// (get) Token: 0x06002732 RID: 10034 RVA: 0x002AA328 File Offset: 0x002A9728
		public string Procedure
		{
			get
			{
				return this.Errors[0].Procedure;
			}
		}

		// Token: 0x17000641 RID: 1601
		// (get) Token: 0x06002733 RID: 10035 RVA: 0x002AA348 File Offset: 0x002A9748
		public string Server
		{
			get
			{
				return this.Errors[0].Server;
			}
		}

		// Token: 0x17000642 RID: 1602
		// (get) Token: 0x06002734 RID: 10036 RVA: 0x002AA368 File Offset: 0x002A9768
		public byte State
		{
			get
			{
				return this.Errors[0].State;
			}
		}

		// Token: 0x17000643 RID: 1603
		// (get) Token: 0x06002735 RID: 10037 RVA: 0x002AA388 File Offset: 0x002A9788
		public override string Source
		{
			get
			{
				return this.Errors[0].Source;
			}
		}

		// Token: 0x06002736 RID: 10038 RVA: 0x002AA3A8 File Offset: 0x002A97A8
		internal static SqlException CreateException(SqlErrorCollection errorCollection, string serverVersion)
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
			SqlException ex = new SqlException(stringBuilder.ToString(), errorCollection);
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

		// Token: 0x06002737 RID: 10039 RVA: 0x002AA498 File Offset: 0x002A9898
		internal SqlException InternalClone()
		{
			SqlException ex = new SqlException(this.Message, this._errors);
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

		// Token: 0x040018F2 RID: 6386
		private SqlErrorCollection _errors;

		// Token: 0x040018F3 RID: 6387
		internal bool _doNotReconnect;
	}
}

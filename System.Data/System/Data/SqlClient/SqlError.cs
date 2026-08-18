using System;
using System.Runtime.Serialization;

namespace System.Data.SqlClient
{
	// Token: 0x020002F2 RID: 754
	[Serializable]
	public sealed class SqlError
	{
		// Token: 0x06002717 RID: 10007 RVA: 0x002A9F08 File Offset: 0x002A9308
		internal SqlError(int infoNumber, byte errorState, byte errorClass, string server, string errorMessage, string procedure, int lineNumber)
		{
			this.number = infoNumber;
			this.state = errorState;
			this.errorClass = errorClass;
			this.server = server;
			this.message = errorMessage;
			this.procedure = procedure;
			this.lineNumber = lineNumber;
			if (errorClass != 0)
			{
				Bid.Trace("<sc.SqlError.SqlError|ERR> infoNumber=%d, errorState=%d, errorClass=%d, errorMessage='%ls', procedure='%ls', lineNumber=%d\n", infoNumber, (int)errorState, (int)errorClass, errorMessage, (procedure == null) ? "None" : procedure, lineNumber);
			}
		}

		// Token: 0x06002718 RID: 10008 RVA: 0x002A9F88 File Offset: 0x002A9388
		public override string ToString()
		{
			return typeof(SqlError).ToString() + ": " + this.message;
		}

		// Token: 0x17000630 RID: 1584
		// (get) Token: 0x06002719 RID: 10009 RVA: 0x002A9FB8 File Offset: 0x002A93B8
		public string Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x17000631 RID: 1585
		// (get) Token: 0x0600271A RID: 10010 RVA: 0x002A9FD8 File Offset: 0x002A93D8
		public int Number
		{
			get
			{
				return this.number;
			}
		}

		// Token: 0x17000632 RID: 1586
		// (get) Token: 0x0600271B RID: 10011 RVA: 0x002A9FF8 File Offset: 0x002A93F8
		public byte State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000633 RID: 1587
		// (get) Token: 0x0600271C RID: 10012 RVA: 0x002AA018 File Offset: 0x002A9418
		public byte Class
		{
			get
			{
				return this.errorClass;
			}
		}

		// Token: 0x17000634 RID: 1588
		// (get) Token: 0x0600271D RID: 10013 RVA: 0x002AA038 File Offset: 0x002A9438
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x17000635 RID: 1589
		// (get) Token: 0x0600271E RID: 10014 RVA: 0x002AA058 File Offset: 0x002A9458
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x17000636 RID: 1590
		// (get) Token: 0x0600271F RID: 10015 RVA: 0x002AA078 File Offset: 0x002A9478
		public string Procedure
		{
			get
			{
				return this.procedure;
			}
		}

		// Token: 0x17000637 RID: 1591
		// (get) Token: 0x06002720 RID: 10016 RVA: 0x002AA098 File Offset: 0x002A9498
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		// Token: 0x040018E9 RID: 6377
		private string source = ".Net SqlClient Data Provider";

		// Token: 0x040018EA RID: 6378
		private int number;

		// Token: 0x040018EB RID: 6379
		private byte state;

		// Token: 0x040018EC RID: 6380
		private byte errorClass;

		// Token: 0x040018ED RID: 6381
		[OptionalField(VersionAdded = 2)]
		private string server;

		// Token: 0x040018EE RID: 6382
		private string message;

		// Token: 0x040018EF RID: 6383
		private string procedure;

		// Token: 0x040018F0 RID: 6384
		private int lineNumber;
	}
}

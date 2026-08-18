using System;
using System.Runtime.Serialization;

namespace System.Data.SqlClient
{
	// Token: 0x020001CD RID: 461
	[Serializable]
	public sealed class SqlError
	{
		// Token: 0x06001D0F RID: 7439 RVA: 0x000CE55C File Offset: 0x000CD95C
		internal SqlError(int infoNumber, byte errorState, byte errorClass, string server, string errorMessage, string procedure, int lineNumber, uint win32ErrorCode) : this(infoNumber, errorState, errorClass, server, errorMessage, procedure, lineNumber)
		{
			this.win32ErrorCode = (int)win32ErrorCode;
		}

		// Token: 0x06001D10 RID: 7440 RVA: 0x000CE584 File Offset: 0x000CD984
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
			this.win32ErrorCode = 0;
		}

		// Token: 0x06001D11 RID: 7441 RVA: 0x000CE600 File Offset: 0x000CDA00
		public override string ToString()
		{
			return typeof(SqlError).ToString() + ": " + this.message;
		}

		// Token: 0x1700046E RID: 1134
		// (get) Token: 0x06001D12 RID: 7442 RVA: 0x000CE62C File Offset: 0x000CDA2C
		public string Source
		{
			get
			{
				return this.source;
			}
		}

		// Token: 0x1700046F RID: 1135
		// (get) Token: 0x06001D13 RID: 7443 RVA: 0x000CE640 File Offset: 0x000CDA40
		public int Number
		{
			get
			{
				return this.number;
			}
		}

		// Token: 0x17000470 RID: 1136
		// (get) Token: 0x06001D14 RID: 7444 RVA: 0x000CE654 File Offset: 0x000CDA54
		public byte State
		{
			get
			{
				return this.state;
			}
		}

		// Token: 0x17000471 RID: 1137
		// (get) Token: 0x06001D15 RID: 7445 RVA: 0x000CE668 File Offset: 0x000CDA68
		public byte Class
		{
			get
			{
				return this.errorClass;
			}
		}

		// Token: 0x17000472 RID: 1138
		// (get) Token: 0x06001D16 RID: 7446 RVA: 0x000CE67C File Offset: 0x000CDA7C
		public string Server
		{
			get
			{
				return this.server;
			}
		}

		// Token: 0x17000473 RID: 1139
		// (get) Token: 0x06001D17 RID: 7447 RVA: 0x000CE690 File Offset: 0x000CDA90
		public string Message
		{
			get
			{
				return this.message;
			}
		}

		// Token: 0x17000474 RID: 1140
		// (get) Token: 0x06001D18 RID: 7448 RVA: 0x000CE6A4 File Offset: 0x000CDAA4
		public string Procedure
		{
			get
			{
				return this.procedure;
			}
		}

		// Token: 0x17000475 RID: 1141
		// (get) Token: 0x06001D19 RID: 7449 RVA: 0x000CE6B8 File Offset: 0x000CDAB8
		public int LineNumber
		{
			get
			{
				return this.lineNumber;
			}
		}

		// Token: 0x17000476 RID: 1142
		// (get) Token: 0x06001D1A RID: 7450 RVA: 0x000CE6CC File Offset: 0x000CDACC
		internal int Win32ErrorCode
		{
			get
			{
				return this.win32ErrorCode;
			}
		}

		// Token: 0x040010AC RID: 4268
		private string source = ".Net SqlClient Data Provider";

		// Token: 0x040010AD RID: 4269
		private int number;

		// Token: 0x040010AE RID: 4270
		private byte state;

		// Token: 0x040010AF RID: 4271
		private byte errorClass;

		// Token: 0x040010B0 RID: 4272
		[OptionalField(VersionAdded = 2)]
		private string server;

		// Token: 0x040010B1 RID: 4273
		private string message;

		// Token: 0x040010B2 RID: 4274
		private string procedure;

		// Token: 0x040010B3 RID: 4275
		private int lineNumber;

		// Token: 0x040010B4 RID: 4276
		[OptionalField(VersionAdded = 4)]
		private int win32ErrorCode;
	}
}

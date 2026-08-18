using System;

namespace System.Web.UI.Design
{
	// Token: 0x0200008B RID: 139
	internal sealed class XmlDocumentFieldSchema : IDataSourceFieldSchema
	{
		// Token: 0x06000446 RID: 1094 RVA: 0x00013B1A File Offset: 0x00011D1A
		public XmlDocumentFieldSchema(string name)
		{
			this._name = name;
		}

		// Token: 0x17000116 RID: 278
		// (get) Token: 0x06000447 RID: 1095 RVA: 0x00013B29 File Offset: 0x00011D29
		public Type DataType
		{
			get
			{
				return typeof(string);
			}
		}

		// Token: 0x17000117 RID: 279
		// (get) Token: 0x06000448 RID: 1096 RVA: 0x0000445B File Offset: 0x0000265B
		public bool Identity
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000118 RID: 280
		// (get) Token: 0x06000449 RID: 1097 RVA: 0x0000445B File Offset: 0x0000265B
		public bool IsReadOnly
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000119 RID: 281
		// (get) Token: 0x0600044A RID: 1098 RVA: 0x0000445B File Offset: 0x0000265B
		public bool IsUnique
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700011A RID: 282
		// (get) Token: 0x0600044B RID: 1099 RVA: 0x0000C1CD File Offset: 0x0000A3CD
		public int Length
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x1700011B RID: 283
		// (get) Token: 0x0600044C RID: 1100 RVA: 0x00013B35 File Offset: 0x00011D35
		public string Name
		{
			get
			{
				return this._name;
			}
		}

		// Token: 0x1700011C RID: 284
		// (get) Token: 0x0600044D RID: 1101 RVA: 0x00003B0F File Offset: 0x00001D0F
		public bool Nullable
		{
			get
			{
				return true;
			}
		}

		// Token: 0x1700011D RID: 285
		// (get) Token: 0x0600044E RID: 1102 RVA: 0x0000C1CD File Offset: 0x0000A3CD
		public int Precision
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x1700011E RID: 286
		// (get) Token: 0x0600044F RID: 1103 RVA: 0x0000445B File Offset: 0x0000265B
		public bool PrimaryKey
		{
			get
			{
				return false;
			}
		}

		// Token: 0x1700011F RID: 287
		// (get) Token: 0x06000450 RID: 1104 RVA: 0x0000C1CD File Offset: 0x0000A3CD
		public int Scale
		{
			get
			{
				return -1;
			}
		}

		// Token: 0x040001BB RID: 443
		private string _name;
	}
}

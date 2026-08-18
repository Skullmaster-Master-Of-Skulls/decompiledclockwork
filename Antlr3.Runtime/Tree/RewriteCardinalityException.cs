using System;
using System.Runtime.Serialization;

namespace Antlr.Runtime.Tree
{
	// Token: 0x0200004F RID: 79
	[Serializable]
	public class RewriteCardinalityException : Exception
	{
		// Token: 0x060003B8 RID: 952 RVA: 0x0000A34B File Offset: 0x0000854B
		public RewriteCardinalityException()
		{
		}

		// Token: 0x060003B9 RID: 953 RVA: 0x0000A353 File Offset: 0x00008553
		public RewriteCardinalityException(string elementDescription) : this(elementDescription, elementDescription)
		{
			this._elementDescription = elementDescription;
		}

		// Token: 0x060003BA RID: 954 RVA: 0x0000A364 File Offset: 0x00008564
		public RewriteCardinalityException(string elementDescription, Exception innerException) : this(elementDescription, elementDescription, innerException)
		{
		}

		// Token: 0x060003BB RID: 955 RVA: 0x0000A36F File Offset: 0x0000856F
		public RewriteCardinalityException(string message, string elementDescription) : base(message)
		{
			this._elementDescription = elementDescription;
		}

		// Token: 0x060003BC RID: 956 RVA: 0x0000A37F File Offset: 0x0000857F
		public RewriteCardinalityException(string message, string elementDescription, Exception innerException) : base(message, innerException)
		{
			this._elementDescription = elementDescription;
		}

		// Token: 0x060003BD RID: 957 RVA: 0x0000A390 File Offset: 0x00008590
		protected RewriteCardinalityException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			this._elementDescription = info.GetString("ElementDescription");
		}

		// Token: 0x060003BE RID: 958 RVA: 0x0000A3B9 File Offset: 0x000085B9
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			base.GetObjectData(info, context);
			info.AddValue("ElementDescription", this._elementDescription);
		}

		// Token: 0x040000C1 RID: 193
		private readonly string _elementDescription;
	}
}

using System;
using System.IO;
using System.Runtime.CompilerServices;

namespace System.Web
{
	// Token: 0x0200002C RID: 44
	[TypeForwardedFrom("System.Web.Abstractions, Version=3.5.0.0, Culture=Neutral, PublicKeyToken=31bf3856ad364e35")]
	public abstract class HttpPostedFileBase
	{
		// Token: 0x17000166 RID: 358
		// (get) Token: 0x060002D4 RID: 724 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual int ContentLength
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000167 RID: 359
		// (get) Token: 0x060002D5 RID: 725 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string ContentType
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000168 RID: 360
		// (get) Token: 0x060002D6 RID: 726 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual string FileName
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x17000169 RID: 361
		// (get) Token: 0x060002D7 RID: 727 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual Stream InputStream
		{
			get
			{
				throw new NotImplementedException();
			}
		}

		// Token: 0x060002D8 RID: 728 RVA: 0x00003ABB File Offset: 0x00001CBB
		public virtual void SaveAs(string filename)
		{
			throw new NotImplementedException();
		}
	}
}

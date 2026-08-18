using System;
using System.Runtime.InteropServices;

namespace System.Runtime.Remoting.Metadata
{
	// Token: 0x02000747 RID: 1863
	[ComVisible(true)]
	public class SoapAttribute : Attribute
	{
		// Token: 0x0600428A RID: 17034 RVA: 0x000E273B File Offset: 0x000E173B
		internal void SetReflectInfo(object info)
		{
			this.ReflectInfo = info;
		}

		// Token: 0x17000BB7 RID: 2999
		// (get) Token: 0x0600428B RID: 17035 RVA: 0x000E2744 File Offset: 0x000E1744
		// (set) Token: 0x0600428C RID: 17036 RVA: 0x000E274C File Offset: 0x000E174C
		public virtual string XmlNamespace
		{
			get
			{
				return this.ProtXmlNamespace;
			}
			set
			{
				this.ProtXmlNamespace = value;
			}
		}

		// Token: 0x17000BB8 RID: 3000
		// (get) Token: 0x0600428D RID: 17037 RVA: 0x000E2755 File Offset: 0x000E1755
		// (set) Token: 0x0600428E RID: 17038 RVA: 0x000E275D File Offset: 0x000E175D
		public virtual bool UseAttribute
		{
			get
			{
				return this._bUseAttribute;
			}
			set
			{
				this._bUseAttribute = value;
			}
		}

		// Token: 0x17000BB9 RID: 3001
		// (get) Token: 0x0600428F RID: 17039 RVA: 0x000E2766 File Offset: 0x000E1766
		// (set) Token: 0x06004290 RID: 17040 RVA: 0x000E276E File Offset: 0x000E176E
		public virtual bool Embedded
		{
			get
			{
				return this._bEmbedded;
			}
			set
			{
				this._bEmbedded = value;
			}
		}

		// Token: 0x0400216D RID: 8557
		protected string ProtXmlNamespace;

		// Token: 0x0400216E RID: 8558
		private bool _bUseAttribute;

		// Token: 0x0400216F RID: 8559
		private bool _bEmbedded;

		// Token: 0x04002170 RID: 8560
		protected object ReflectInfo;
	}
}

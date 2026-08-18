using System;
using System.CodeDom;
using System.Runtime.Serialization;

namespace System.ComponentModel.Design.Serialization
{
	// Token: 0x020001D8 RID: 472
	[Serializable]
	public class CodeDomSerializerException : SystemException
	{
		// Token: 0x060011D6 RID: 4566 RVA: 0x00065563 File Offset: 0x00063763
		public CodeDomSerializerException(string message, CodeLinePragma linePragma) : base(message)
		{
			this.linePragma = linePragma;
		}

		// Token: 0x060011D7 RID: 4567 RVA: 0x00065573 File Offset: 0x00063773
		public CodeDomSerializerException(Exception ex, CodeLinePragma linePragma) : base(ex.Message, ex)
		{
			this.linePragma = linePragma;
		}

		// Token: 0x060011D8 RID: 4568 RVA: 0x00065589 File Offset: 0x00063789
		public CodeDomSerializerException(string message, IDesignerSerializationManager manager) : base(message)
		{
			this.FillLinePragmaFromContext(manager);
		}

		// Token: 0x060011D9 RID: 4569 RVA: 0x00065599 File Offset: 0x00063799
		public CodeDomSerializerException(Exception ex, IDesignerSerializationManager manager) : base(ex.Message, ex)
		{
			this.FillLinePragmaFromContext(manager);
		}

		// Token: 0x060011DA RID: 4570 RVA: 0x000655AF File Offset: 0x000637AF
		protected CodeDomSerializerException(SerializationInfo info, StreamingContext context) : base(info, context)
		{
			this.linePragma = (CodeLinePragma)info.GetValue("linePragma", typeof(CodeLinePragma));
		}

		// Token: 0x170003F5 RID: 1013
		// (get) Token: 0x060011DB RID: 4571 RVA: 0x000655D9 File Offset: 0x000637D9
		public CodeLinePragma LinePragma
		{
			get
			{
				return this.linePragma;
			}
		}

		// Token: 0x060011DC RID: 4572 RVA: 0x000655E1 File Offset: 0x000637E1
		private void FillLinePragmaFromContext(IDesignerSerializationManager manager)
		{
			if (manager == null)
			{
				throw new ArgumentNullException("manager");
			}
		}

		// Token: 0x060011DD RID: 4573 RVA: 0x000655F1 File Offset: 0x000637F1
		public override void GetObjectData(SerializationInfo info, StreamingContext context)
		{
			if (info == null)
			{
				throw new ArgumentNullException("info");
			}
			info.AddValue("linePragma", this.linePragma);
			base.GetObjectData(info, context);
		}

		// Token: 0x040009DA RID: 2522
		private CodeLinePragma linePragma;
	}
}

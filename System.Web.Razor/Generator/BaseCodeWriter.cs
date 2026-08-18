using System;

namespace System.Web.Razor.Generator
{
	// Token: 0x02000024 RID: 36
	internal abstract class BaseCodeWriter : CodeWriter
	{
		// Token: 0x06000148 RID: 328 RVA: 0x00005155 File Offset: 0x00003355
		public override void WriteSnippet(string snippet)
		{
			base.InnerWriter.Write(snippet);
		}

		// Token: 0x06000149 RID: 329 RVA: 0x00005163 File Offset: 0x00003363
		protected internal override void EmitStartMethodInvoke(string methodName)
		{
			this.EmitStartMethodInvoke(methodName, new string[0]);
		}

		// Token: 0x0600014A RID: 330 RVA: 0x00005174 File Offset: 0x00003374
		protected internal override void EmitStartMethodInvoke(string methodName, params string[] genericArguments)
		{
			base.InnerWriter.Write(methodName);
			if (genericArguments != null && genericArguments.Length > 0)
			{
				this.WriteStartGenerics();
				for (int i = 0; i < genericArguments.Length; i++)
				{
					if (i > 0)
					{
						this.WriteParameterSeparator();
					}
					this.WriteSnippet(genericArguments[i]);
				}
				this.WriteEndGenerics();
			}
			base.InnerWriter.Write("(");
		}

		// Token: 0x0600014B RID: 331 RVA: 0x000051D3 File Offset: 0x000033D3
		protected internal override void EmitEndMethodInvoke()
		{
			base.InnerWriter.Write(")");
		}

		// Token: 0x0600014C RID: 332 RVA: 0x000051E5 File Offset: 0x000033E5
		protected internal override void EmitEndConstructor()
		{
			base.InnerWriter.Write(")");
		}

		// Token: 0x0600014D RID: 333 RVA: 0x000051F7 File Offset: 0x000033F7
		protected internal override void EmitEndLambdaExpression()
		{
		}

		// Token: 0x0600014E RID: 334 RVA: 0x000051F9 File Offset: 0x000033F9
		public override void WriteParameterSeparator()
		{
			base.InnerWriter.Write(", ");
		}

		// Token: 0x0600014F RID: 335 RVA: 0x0000520C File Offset: 0x0000340C
		protected internal void WriteCommaSeparatedList<T>(T[] items, Action<T> writeItemAction)
		{
			for (int i = 0; i < items.Length; i++)
			{
				if (i > 0)
				{
					base.InnerWriter.Write(", ");
				}
				writeItemAction(items[i]);
			}
		}

		// Token: 0x06000150 RID: 336 RVA: 0x00005248 File Offset: 0x00003448
		protected internal virtual void WriteStartGenerics()
		{
		}

		// Token: 0x06000151 RID: 337 RVA: 0x0000524A File Offset: 0x0000344A
		protected internal virtual void WriteEndGenerics()
		{
		}
	}
}

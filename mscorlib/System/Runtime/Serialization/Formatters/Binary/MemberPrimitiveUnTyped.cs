using System;
using System.Diagnostics;

namespace System.Runtime.Serialization.Formatters.Binary
{
	// Token: 0x020007E5 RID: 2021
	internal sealed class MemberPrimitiveUnTyped : IStreamable
	{
		// Token: 0x0600477E RID: 18302 RVA: 0x000F532F File Offset: 0x000F432F
		internal MemberPrimitiveUnTyped()
		{
		}

		// Token: 0x0600477F RID: 18303 RVA: 0x000F5337 File Offset: 0x000F4337
		internal void Set(InternalPrimitiveTypeE typeInformation, object value)
		{
			this.typeInformation = typeInformation;
			this.value = value;
		}

		// Token: 0x06004780 RID: 18304 RVA: 0x000F5347 File Offset: 0x000F4347
		internal void Set(InternalPrimitiveTypeE typeInformation)
		{
			this.typeInformation = typeInformation;
		}

		// Token: 0x06004781 RID: 18305 RVA: 0x000F5350 File Offset: 0x000F4350
		public void Write(__BinaryWriter sout)
		{
			sout.WriteValue(this.typeInformation, this.value);
		}

		// Token: 0x06004782 RID: 18306 RVA: 0x000F5364 File Offset: 0x000F4364
		public void Read(__BinaryParser input)
		{
			this.value = input.ReadValue(this.typeInformation);
		}

		// Token: 0x06004783 RID: 18307 RVA: 0x000F5378 File Offset: 0x000F4378
		public void Dump()
		{
		}

		// Token: 0x06004784 RID: 18308 RVA: 0x000F537A File Offset: 0x000F437A
		[Conditional("_LOGGING")]
		private void DumpInternal()
		{
			if (BCLDebug.CheckEnabled("BINARY"))
			{
				Converter.ToComType(this.typeInformation);
			}
		}

		// Token: 0x04002436 RID: 9270
		internal InternalPrimitiveTypeE typeInformation;

		// Token: 0x04002437 RID: 9271
		internal object value;
	}
}
